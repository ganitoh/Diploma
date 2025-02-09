namespace Common.API;

public class ApiResponse<TResponse>
{
    public TResponse Response { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    
    public static ApiResponse<TResponse> Fail(string errorMessage)
    {
        return new ApiResponse<TResponse> { Succeeded = false, Message = errorMessage };
    }
    public static ApiResponse<TResponse> Success(TResponse resp)
    {
        return new ApiResponse<TResponse> { Succeeded = true, Response = resp };
    }
}