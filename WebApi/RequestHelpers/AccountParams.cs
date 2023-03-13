namespace WebApi.RequestHelpers
{
    public class AccountParams : PaginationParams
    {
        public string Status { get; set; }
        public string SearchName { get; set; }
        public string SearchEmail { get; set; }
        public string SearchPhoneNumber { get; set; }

    }
}
