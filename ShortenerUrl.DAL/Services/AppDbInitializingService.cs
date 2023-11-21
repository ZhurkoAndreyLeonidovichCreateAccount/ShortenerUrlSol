using Microsoft.Extensions.DependencyInjection;
using ShortenerUrl.DAL.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ShortenerUrl.DAL.Services
{
    [ExcludeFromCodeCoverage]
    public class AppDbInitializingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostEnvironment _hostEnvironment;

        public AppDbInitializingService(IServiceProvider serviceProvider, IHostEnvironment hostEnvironment)
        {
            _serviceProvider = serviceProvider;
            _hostEnvironment = hostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await InitializeDatabaseAsync(db, cancellationToken);
        }

        public async Task InitializeDatabaseAsync(ApplicationDbContext db, CancellationToken cancellationToken)
        {
            CheckDatabaseConnection(db, 5000, cancellationToken);
            Console.WriteLine("Attempting to initialize database...");

            if (_hostEnvironment.IsDevelopment())
            {
                await DebugMigrateAsync(db, cancellationToken);
            }
            else
            {
                await db.Database.MigrateAsync(cancellationToken);
            }

            Console.WriteLine("Database initialized successfully");
        }

        public static void CheckDatabaseConnection(ApplicationDbContext db, int timeout, CancellationToken cancellationToken)
        {
            var connected = db.Database.CanConnectAsync(cancellationToken).Wait(timeout, cancellationToken);        
            if (!connected) throw new Exception("Unable to connect to database. Consider checking connection strings or something...");
            Console.WriteLine("Database connection established.");
        }

        public static async Task DebugMigrateAsync(ApplicationDbContext db, CancellationToken cancellationToken)
        {
            try
            {
                await db.Database.MigrateAsync(cancellationToken);
            }
            catch (Exception)
            {
                Console.WriteLine("An exception occured while attempting to initialize database.");
                Console.WriteLine("DEBUG: Resetting database. All data will be cleared!");
                await db.Database.EnsureDeletedAsync(cancellationToken);
                Console.WriteLine("DEBUG: Database cleared successfully.");
                await db.Database.MigrateAsync(cancellationToken);
                Console.WriteLine("DEBUG: Database re-created successfully.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
