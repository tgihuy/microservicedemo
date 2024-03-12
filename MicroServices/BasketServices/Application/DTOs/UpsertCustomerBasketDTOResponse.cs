namespace BasketServices.Application.DTOs
{
    public class UpsertCustomerBasketDTOResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public UpsertCustomerBasketDTOResponse(string message, object data) {
            Message = message;
            Data = data;
        }
    }
}
