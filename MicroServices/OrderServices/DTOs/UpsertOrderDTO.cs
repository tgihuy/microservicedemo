namespace OrderServices.DTOs
{
    public class UpsertOrderDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AdditionalAddress { get; set; }
        public string CustomerId { get; set; }
    }
}
