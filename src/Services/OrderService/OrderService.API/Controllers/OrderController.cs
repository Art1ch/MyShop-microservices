using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Queries.GetAll;
using OrderService.Application.Queries.GetOrderById;
using OrderService.Application.Responses.QueriesResponses;

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
        public async Task<ActionResult<GetOrderByIdResponse>> GetOrder([FromQuery]Guid id)
        {
            var result = await _sender.Send(new GetOrderByIdQuery(id));
            return result.Match<ActionResult<GetOrderByIdResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }

        [HttpGet("/orders")]
        public async Task<ActionResult<GetAllOrdersResponse>> GetOrders()
        {
            var result = await _sender.Send(new GetAllQuery());
            return result.Match<ActionResult<GetAllOrdersResponse>>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Message)
            );
        }
    }
}
