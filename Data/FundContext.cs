using Microsoft.EntityFrameworkCore;
using WebFund.Models;

namespace WebFund.Data
{
    public class FundContext : DbContext
    {
        public FundContext(DbContextOptions<FundContext> options)
            : base(options)
        {

        }

        public DbSet<Fund> Funds { get; set; }
    }
}

