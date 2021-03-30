using Microsoft.EntityFrameworkCore;
using StoreFile.DAL.Entities;

namespace StoreFile.DAL
{
    public class StoreFileDbContext : DbContext
    {
        public StoreFileDbContext(DbContextOptions<StoreFileDbContext> options) : base(options)
        {
        }
        public DbSet<StoredFile> Files { get; set; }
        public DbSet<StoredFileUser> Users { get; set; }
        public DbSet<Token> AccessTokens { get; set; }


    }
}
