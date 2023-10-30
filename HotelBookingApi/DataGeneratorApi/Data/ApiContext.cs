using Microsoft.EntityFrameworkCore;
using DataGeneratorApi.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace DataGeneratorApi.Data {
    public class ApiContext : DbContext  {
        public DbSet<TableModel> Tables { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
 