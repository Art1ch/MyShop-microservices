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
            var result = await _sender.Send(new GetOrderByIdQuery(id));
            return result.Match<IActionResult>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/orders")]
        public async Task<ActionResult<List<OrderEntity>>> GetOrders()
        {
            var result = await _sender.Send(new GetAllQuery());
            return result.Match<ActionResult<List<OrderEntity>>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }
    }
}
