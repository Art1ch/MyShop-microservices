namespace StoreService.Application.Repsonses.CommandResponses.Product
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public CreateProductResponse(Guid id)
        {
            Id = id;
        }
    }
}
