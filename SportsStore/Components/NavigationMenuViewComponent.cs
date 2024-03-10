using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IProductRepository repository;
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        
        public IViewComponentResult Invoke()
        {
            //ViewBag.SelectedCategory = RouteData?.Values["category"];
            //Console.WriteLine($"RouteData: Category {RouteData?.Values["category"]}");
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
