using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetBasketById
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, OneOf<Success<BasketEntity>, Failed>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<OneOf<Success<BasketEntity>, Failed>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.GetBasketByIdAsync(request.Id, cancellationToken);
        }
    }
}
