using Microsoft.EntityFrameworkCore;
using VoucherApi.Models;

namespace VoucherApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Voucher> Vouchers { get; set; }

        //Seed Database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Voucher>().HasData(new Voucher
            {
                VoucherId = 1,
                VoucherCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20,
            });

            modelBuilder.Entity<Voucher>().HasData(new Voucher
            {
                VoucherId = 2,
                VoucherCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40,
            });

        }
    }
}
