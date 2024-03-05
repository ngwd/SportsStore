namespace SportsStore.Models
{
    public class EFProductRepository:IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
