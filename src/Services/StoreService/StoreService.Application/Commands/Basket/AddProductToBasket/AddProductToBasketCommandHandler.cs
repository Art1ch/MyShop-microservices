using MediatR;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Contracts;

namespace StoreService.Application.Commands.Basket.AddProductToBasket
{
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public AddProductToBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
        {
            await _basketRepository.AddProductToBasket(request.BasketId, request.ProductId, request.Amount, cancellationToken);
        }
    }
}
