namespace ETicaretAPI.Application.Features.Queries.ProductImageFilE.GetProductImage
{
    public class GetProductImagesQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public Guid Id { get; set; }

        public bool Showcase { get; set; }
    }
}
