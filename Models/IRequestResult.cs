namespace ProductApi.Models
{
    public interface IRequestResult
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}