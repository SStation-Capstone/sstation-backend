﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Application.Interfaces.Services.Notifications.Common;
using ShipperStation.Application.Interfaces.Services.Payments;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.Persistence.Data;
using ShipperStation.Infrastructure.Persistence.Interceptors;
using ShipperStation.Infrastructure.Persistence.SeedData;
using ShipperStation.Infrastructure.Repositories;
using ShipperStation.Infrastructure.Services;
using ShipperStation.Infrastructure.Services.Notifications;
using ShipperStation.Infrastructure.Services.Notifications.Common;
using ShipperStation.Infrastructure.Services.Payments;
using ShipperStation.Shared.Helpers;

namespace ShipperStation.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddServices();
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddInitialiseDatabase();
        services.AddDefaultIdentity();

    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IStorageService, StorageService>()
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IMomoPaymentService, MomoPaymentService>()
            .AddScoped<IVnPayPaymentService, VnPayPaymentService>()
            .AddScoped<INotifier, Notifier>()
            .AddScoped<INotificationAdapter, NotificationAdapter>()
            .AddScoped<INotificationProvider, NotificationProvider>()
            .AddScoped<ISignalRNotificationService, SignalRNotificationService>()
            .AddScoped<IZaloNotificationService, ZnsNotificationService>()
            .AddScoped<IFirebaseNotificationService, FirebaseNotificationService>()
            .AddScoped<ISmsNotificationService, SmsNotificationService>()
            .AddScoped<ICallerNotificationService, CallerNotificationService>()
            .AddScoped<ZaloAuthService>()
            .AddTransient<IEmailSender, EmailSender>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        string defaultConnection = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
           options.UseMySql(defaultConnection, ServerVersion.AutoDetect(defaultConnection),
               builder =>
               {
                   builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                   builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
               })
                  .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
                  .EnableSensitiveDataLogging()
                  //.UseLazyLoadingProxies()
                  .EnableDetailedErrors()
                  .UseProjectables());

    }

    private static void AddDefaultIdentity(this IServiceCollection services)
    {

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 0;

            options.User.RequireUniqueEmail = true;
            //options.Stores.ProtectPersonalData = true;

        }).AddEntityFrameworkStores<ApplicationDbContext>()
          //.AddPersonalDataProtection<LookupProtector, KeyRing>()
          .AddDefaultTokenProviders();
    }

    private static void AddInitialiseDatabase(this IServiceCollection services)
    {
        services
            .AddScoped<ApplicationDbContextInitialiser>();
    }

    public static async Task UseInitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        if (app.Environment.IsDevelopment())
        {
            //await initialiser.DeletedDatabaseAsync();
            await initialiser.MigrateAsync();
            //await initialiser.SeedAsync();
        }

        if (app.Environment.IsProduction())
        {
            await initialiser.MigrateAsync();
        }
    }
}
