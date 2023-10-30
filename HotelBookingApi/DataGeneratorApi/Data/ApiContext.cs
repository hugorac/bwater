using Microsoft.EntityFrameworkCore;
using DataGeneratorApi.Models;

namespace DataGeneratorApi.Data {
    public class ApiContext : DbContext  {
        public DbSet<TableModel> Tables { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
 