using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketViewModelService.GetBasketViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(int productId, int quantity = 1)
        {
            if (quantity < 1) return BadRequest();

            var basketViewModel = await _basketViewModelService.AddItemToBasketAsync(productId, quantity);

            return Json(basketViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyBasket()
        {
            await _basketViewModelService.EmptyBasketAsync();
            TempData["SuccessMessage"] = "Items removed from the basket.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBasketItem(int productId)
        {
            await _basketViewModelService.RemoveBasketItemAsync(productId);
            TempData["SuccessMessage"] = "Item removed from the basket.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBasket([ModelBinder(Name = "quantities")] Dictionary<int, int> quantities)
        {
            await _basketViewModelService.UpdateBasketAsync(quantities);
            TempData["SuccessMessage"] = "Basket updated.";

            return RedirectToAction(nameof(Index));
        }
    }
}
