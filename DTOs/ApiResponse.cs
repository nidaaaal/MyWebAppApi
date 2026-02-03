namespace MyWebAppApi.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCodes { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

    }

    public static class ApiResponseBuilder
    {
        public static ApiResponse<T> Fail<T>(string message,int statusCode)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                StatusCodes = statusCode,
                Message = message,
                Data = default
            };

        }

        public static ApiResponse<T> Success<T>(T data, string message = "Operation succeeded")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                StatusCodes = 200,
                Message = message,
                Data = data
            };
        }

    }



}
