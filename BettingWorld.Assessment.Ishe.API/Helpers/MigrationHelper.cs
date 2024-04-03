using BettingWorld.Assessment.Ishe.API.Data;
using Microsoft.EntityFrameworkCore;


namespace BettingWorld.Assessment.Ishe.API.Helpers
{
    public static class MigrationHelper
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using RatesContext dbContext =
                scope.ServiceProvider.GetRequiredService<RatesContext>();
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
            }
        }
    }
}
