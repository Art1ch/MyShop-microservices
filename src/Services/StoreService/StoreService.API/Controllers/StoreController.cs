using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Commands.Basket.MakeOrder;
using StoreService.Application.Commands.Product.CreateProduct;
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
        public StoreController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/basket/{id}")]
        public async Task<ActionResult<BasketEntity>> GetBasket([FromQuery] Guid id)
        {
            var result = await _sender.Send(new GetBasketByIdQuery(id));
            return result.Match<ActionResult<BasketEntity>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/product/{id}")]
        public async Task<ActionResult<ProductEntity>> GetProduct([FromQuery] Guid id)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id));
            return result.Match<ActionResult<ProductEntity>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/baskets")]
        public async Task<ActionResult<List<BasketEntity>>> GetBaskets()
        {
            var result = await _sender.Send(new Application.Queries.Basket.GetAll.GetAllQuery());
            return result.Match<ActionResult<List<BasketEntity>>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/products")]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts()
        {
            var result = await _sender.Send(new Application.Queries.Product.GetAll.GetAllQuery());
            return result.Match<ActionResult<List<ProductEntity>>> (
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/create_product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<IActionResult>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/add_product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductToBasketCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<IActionResult>(
                success => Ok(success),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/make_order")]
        public async Task<IActionResult> MakeOrder([FromBody] MakeOrderCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}   
