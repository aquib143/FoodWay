using FoodWay.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodWay.Data
{
    public class FoodWayContext: IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FoodWayContext()
        {

        }
        public FoodWayContext(DbContextOptions<FoodWayContext> options, IHttpContextAccessor httpContextAccessor) :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var now = DateTime.UtcNow;
            var currentUser = !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.User?.Identity?.Name) ? _httpContextAccessor.HttpContext.User.Identity.Name
            : "Anonymous";
            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            entity.CreatedBy = currentUser;
                            Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
                            Entry(entity).Property(x => x.UpdatedDate).IsModified = false;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.UpdatedDate = now;
                            entity.UpdatedBy = currentUser;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
