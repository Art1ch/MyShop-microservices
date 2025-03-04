using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, OneOf<Success<List<BasketEntity>>, Failed>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetAllQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<OneOf<Success<List<BasketEntity>>, Failed>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.GetAllAsync(cancellationToken);
        }
    }
}
