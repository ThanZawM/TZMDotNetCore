using DotNetMvcApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetMvcApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "THANZAWMYINT\\SQLEXPRESS",
                InitialCatalog = "TestDB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true,
            };
            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString); 
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
