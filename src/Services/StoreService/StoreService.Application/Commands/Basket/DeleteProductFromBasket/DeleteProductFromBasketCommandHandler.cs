using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommandHandler : IRequestHandler<DeleteProductFromBasketCommand, OneOf<Success<DeleteProductFromBasketResponse>, Failed>>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteProductFromBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<OneOf<Success<DeleteProductFromBasketResponse>, Failed>> Handle(DeleteProductFromBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteProductFromBasketAsync(request.ProductId, request.ProductId, cancellationToken);
        }
    }
}
