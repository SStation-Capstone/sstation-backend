﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Infrastructure.Persistence.Data;

namespace ShipperStation.Infrastructure.Persistence.SeedData;

public class ApplicationDbContextInitialiser(
   ILogger<ApplicationDbContextInitialiser> logger,
   ApplicationDbContext context,
   UserManager<User> userManager,
   RoleManager<Role> roleManager,
   IUnitOfWork unitOfWork)
{
    public async Task MigrateAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task DeletedDatabaseAsync()
    {
        try
        {
            await context.Database.EnsureDeletedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {

        if (!await unitOfWork.Repository<Station>().ExistsByAsync())
        {
            await unitOfWork.Repository<Station>().CreateRangeAsync(StationSeed.Default);
            await unitOfWork.CommitAsync();
        }

        if (!await unitOfWork.Repository<Role>().ExistsByAsync())
        {
            foreach (var item in RoleSeed.Default)
            {
                await roleManager.CreateAsync(item);
            }
        }

        if (!await unitOfWork.Repository<User>().ExistsByAsync())
        {
            var user = new User
            {
                UserName = "admin",
                IsActive = true,
            };
            await userManager.CreateAsync(user, "admin");
            await userManager.AddToRolesAsync(user, new[] { RoleName.Admin });

            user = new User
            {
                UserName = "user",
                IsActive = true,
            };
            await userManager.CreateAsync(user, "user");
            await userManager.AddToRolesAsync(user, new[] { RoleName.User });

            user = new User
            {
                UserName = "store",
                IsActive = true,
            };
            await userManager.CreateAsync(user, "store");
            await userManager.AddToRolesAsync(user, new[] { RoleName.StationManager });
            var station = await unitOfWork.Repository<Station>().FindByAsync(_ => !_.IsDeleted);
            user.UserStations.Add(new UserStation
            {
                UserId = user.Id,
                StationId = station.Id,
            });

            user = new User
            {
                UserName = "staff",
                IsActive = true,
            };
            await userManager.CreateAsync(user, "staff");
            await userManager.AddToRolesAsync(user, new[] { RoleName.Staff });
            user.UserStations.Add(new UserStation
            {
                UserId = user.Id,
                StationId = station.Id,
            });

            await unitOfWork.CommitAsync();
        }

    }
}
