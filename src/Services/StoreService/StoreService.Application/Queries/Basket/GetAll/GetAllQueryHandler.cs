using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<BasketEntity>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetAllQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<List<BasketEntity>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.GetAllAsync(cancellationToken);
        }
    }
}
