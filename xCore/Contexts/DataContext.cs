using Microsoft.EntityFrameworkCore;
using xCore.Models.Entities;

namespace xCore.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<ContentTypeEntity> Categorys { get; set; }
    }
}
