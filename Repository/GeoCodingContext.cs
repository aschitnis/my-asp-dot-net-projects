using Microsoft.EntityFrameworkCore;

namespace GeocodingApi.Models.Db
{
    public class GeoCodingContext : DbContext
    {
        
        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
            => optionbuilder.UseSqlite(@"Data Source=geocoding.db");

        public DbSet<GeoAddress> GeoAddresses { get; set; }
    }
}