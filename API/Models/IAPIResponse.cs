namespace API.Models
{
    public interface IAPIResponse
    {
        object Data { get; set; }
        string ErrorMessage { get; set; }
        bool IsSuccess { get; set; }
    }
}