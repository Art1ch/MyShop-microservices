namespace StoreService.Application.Repsonses.CommandResponses.Product
{
    public class UpdateProductResponse
    {
        public Guid Id { get; set; }
        public UpdateProductResponse(Guid id)
        {
            Id = id;
        }
    }
}
