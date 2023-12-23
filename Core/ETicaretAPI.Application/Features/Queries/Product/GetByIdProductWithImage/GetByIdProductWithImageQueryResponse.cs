namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProductWithImage;

using ETicaretAPI.Application.Dtos.Product;

public class GetByIdProductWithImageQueryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    public List<ProductImageFileDto> ProductImageFiles { get; set; }
}