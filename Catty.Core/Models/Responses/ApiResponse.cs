namespace Catty.Core.Models.Responses
{
    public class ApiResponse<T> where T : class
    {
        public ApiResponse() { }
        public ApiResponse(string code, string message) => (Code, Message) = (code, message);
        public string? Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
