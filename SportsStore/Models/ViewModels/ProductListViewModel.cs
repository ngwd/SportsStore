using SportsStore.Models;
namespace SportsStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set;}
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
