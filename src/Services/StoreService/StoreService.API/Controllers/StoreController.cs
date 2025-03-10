using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Commands.Basket.MakeOrder;
using StoreService.Application.Commands.Product.CreateProduct;
using StoreService.Application.Queries.Basket.GetBasketById;
using StoreService.Application.Queries.Product.GetProductById;
using StoreService.Application.Repsonses.CommandResponses.Basket;
using StoreService.Application.Repsonses.CommandResponses.Product;
using StoreService.Application.Repsonses.QueriesResponses.Basket;
using StoreService.Application.Repsonses.QueriesResponses.Product;

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
        public async Task<ActionResult<GetBasketByIdResponse>> GetBasket([FromQuery] Guid id)
        {
            var result = await _sender.Send(new GetBasketByIdQuery(id));
            return result.Match<ActionResult<GetBasketByIdResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/product/{id}")]
        public async Task<ActionResult<GetProductByIdResponse>> GetProduct([FromQuery] Guid id)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id));
            return result.Match<ActionResult<GetProductByIdResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/baskets")]
        public async Task<ActionResult<GetAllProductsResponse>> GetBaskets()
        {
            var result = await _sender.Send(new Application.Queries.Basket.GetAll.GetAllQuery());
            return result.Match<ActionResult<GetAllProductsResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/products")]
        public async Task<ActionResult<GetAllProductsResponse>> GetProducts()
        {
            var result = await _sender.Send(new Application.Queries.Product.GetAll.GetAllQuery());
            return result.Match<ActionResult<GetAllProductsResponse>> (
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/create_product")]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<CreateProductResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/add_product")]
        public async Task<ActionResult<AddProductToBasketResponse>> AddProduct([FromBody] AddProductToBasketCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<AddProductToBasketResponse>>(
                success => Ok(success),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpPost("/make_order")]
        public async Task<ActionResult<MakeOrderResponse>> MakeOrder([FromBody] MakeOrderCommand command)
        {
            var result = await _sender.Send(command);
            return result.Match<ActionResult<MakeOrderResponse>>(
                success => Ok(success),
                failed => BadRequest(failed.Message)
            );
        }
    }
}   
