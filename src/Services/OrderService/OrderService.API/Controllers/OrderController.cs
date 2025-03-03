using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Queries.GetAll;
using OrderService.Application.Queries.GetOrderById;
using OrderService.Core.Entities;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;
        public OrderController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("/order/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            await _sender.Send(new GetOrderByIdQuery(id));
            return Ok();
        }

        [HttpGet("/orders")]
        public async Task<ActionResult<List<OrderEntity>>> GetOrders()
        {
            return await _sender.Send(new GetAllQuery());
        }
    }
}
