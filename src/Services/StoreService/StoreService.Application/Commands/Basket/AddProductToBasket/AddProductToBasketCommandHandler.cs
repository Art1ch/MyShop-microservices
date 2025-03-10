using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.AddProductToBasket
{
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommand, OneOf<Success<AddProductToBasketResponse>, Failed>>
    {
        private readonly IBasketRepository _basketRepository;

        public AddProductToBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<OneOf<Success<AddProductToBasketResponse>, Failed>> Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.AddProductToBasket(request.BasketId, request.ProductId, request.Amount, cancellationToken);
        }
    }
}
