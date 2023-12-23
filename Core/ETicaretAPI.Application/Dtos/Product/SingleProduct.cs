namespace ETicaretAPI.Application.Dtos.Product
{
    public class SingleProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        // public List<ProductImageFile> ProductImageFiles { get; set; }
        public List<ProductImageFileDto> ProductImageFiles { get; set; }
    }
}
