using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Commands.Basket.MakeOrder;
using StoreService.Application.Commands.Product.CreateProduct;
using StoreService.Application.Contracts;
using StoreService.Application.Queries.Basket.GetBasketById;
using StoreService.Application.Queries.Product.GetProductById;
using StoreService.Core.Entities;

namespace StoreService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IBasketRepository _basketRepository;
        public StoreController(ISender sender, IBasketRepository basketRepository)
        {
            _sender = sender;
            _basketRepository = basketRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            var basket = new BasketEntity()
            {
                Id = Guid.NewGuid(),
                Price = 0,
                Products = new List<ProductEntity>()
            };
            await _basketRepository.CreateBasketAsync(basket);

            return Ok(basket);
        }

        [HttpGet("/basket/{id}")]
        public async Task<ActionResult<BasketEntity>> GetBasket(Guid id)
        {
            var basket = await _sender.Send(new GetBasketByIdQuery(id));
            return Ok(basket);
        }

        [HttpGet("/product/{id}")]
        public async Task<ActionResult<ProductEntity>> GetProduct(Guid id)
        {
            var product = await _sender.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpGet("/baskets")]
        public async Task<ActionResult<List<BasketEntity>>> GetBaskets()
        {
            var result = await _sender.Send(new Application.Queries.Basket.GetAll.GetAllQuery());
            return Ok(result);
        }

        [HttpGet("/products")]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts()
        {
            var result = await _sender.Send(new Application.Queries.Product.GetAll.GetAllQuery());
            return Ok(result);
        }

        [HttpPost("/create_product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost("/add_product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductToBasketCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost("/make_order")]
        public async Task<IActionResult> MakeOrder([FromBody] MakeOrderCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}   
