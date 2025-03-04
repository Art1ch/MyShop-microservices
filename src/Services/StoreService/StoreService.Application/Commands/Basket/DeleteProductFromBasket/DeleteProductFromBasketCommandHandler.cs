using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommandHandler : IRequestHandler<DeleteProductFromBasketCommand, OneOf<Success, Failed>>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteProductFromBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<OneOf<Success, Failed>> Handle(DeleteProductFromBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteProductFromBasketAsync(request.ProductId, request.ProductId, cancellationToken);
        }
    }
}
