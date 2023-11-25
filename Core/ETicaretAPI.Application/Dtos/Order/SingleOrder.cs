namespace ETicaretAPI.Application.Dtos.Order
{
    public class SingleOrder
    {
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public object BasketItems { get; set; }
    }
}
