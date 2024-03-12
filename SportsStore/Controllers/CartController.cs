using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        } 
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl) 
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(prod);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        } 
        private void SaveCart(Cart cart) 
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
