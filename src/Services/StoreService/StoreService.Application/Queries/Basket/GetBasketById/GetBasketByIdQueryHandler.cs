using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetBasketById
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, BasketEntity>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<BasketEntity?> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.GetBasketByIdAsync(request.Id, cancellationToken);
        }
    }
}
