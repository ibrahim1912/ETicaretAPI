namespace ETicaretAPI.Application.Dtos.Order
{
    public class CompletedOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }

    }
}
