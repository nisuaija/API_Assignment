using System.Runtime.CompilerServices;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using BookContext dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();
            dbContext.Database.Migrate();
        }
    }
}
