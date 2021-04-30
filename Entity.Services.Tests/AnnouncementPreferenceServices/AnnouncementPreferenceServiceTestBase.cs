using Entities;
using Entity.Services.Tests.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services.Tests.AnnouncementPreferenceServices
{
    public abstract class AnnouncementPreferenceServiceTestBase : IDisposable
    {
        protected readonly IEntityService<AnnouncementPreference> Service;
        protected readonly PocDbContext Context;
        protected readonly DateTime UtcNow = DateTime.UtcNow;
        private readonly DbContextOptions _options;
        private readonly SqliteConnection _connection;

        [DbFunction]
        private static Guid NewId() => Guid.NewGuid();

        public AnnouncementPreferenceServiceTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _connection.CreateFunction("newid", NewId);


            _options = new DbContextOptionsBuilder<PocDbContext>()
                .EnableSensitiveDataLogging()
                .UseSqlite(_connection)
                .AddInterceptors(new SqliteCommandInterceptor())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            Context = new PocDbContext(_options);

            Context.Database.EnsureDeleted();
            Context.Database.Migrate();
            Context.Database.EnsureCreated();

            var services = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            Service = new AnnouncementPreferenceService(Context, services.GetRequiredService<ILogger<EntityService<AnnouncementPreference>>>());
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Dispose();
        }

        protected void InitializeData(AnnouncementPreference[] entities)
        {
            using (var context = new PocDbContext(_options))
            {
                foreach (var entity in entities)
                {
                    context.Add(entity);
                }
                context.SaveChanges();
            }
        }
    }
}
