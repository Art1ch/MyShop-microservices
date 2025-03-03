using MediatR;
using StoreService.Application.Contracts;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommandHandler : IRequestHandler<DeleteProductFromBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteProductFromBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(DeleteProductFromBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteProductFromBasketAsync(request.ProductId, request.ProductId, cancellationToken);
        }

    }
}
