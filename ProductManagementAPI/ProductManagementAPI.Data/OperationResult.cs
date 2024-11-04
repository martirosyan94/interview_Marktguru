namespace ProductManagementAPI.Data
{
    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public new static OperationResult<T> Ok(T? data, Status status = Status.Success)
        {
            return new OperationResult<T>()
            {
                Success = true,
                Data = data,
                Status = status
            };
        }

        public new static OperationResult<T> Error(string? message = null, Status status = Status.None)
        {
            return new OperationResult<T>()
            {
                Success = false,
                ErrorMessage = message,
                Status = status
            };
        }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public Status Status { get; set; }

        public static OperationResult Ok()
        {
            return new OperationResult()
            {
                Success = true,
                Status = Status.Success
            };
        }

        public static OperationResult Error(string? message = null, Status status = Status.None)
        {
            return new OperationResult()
            {
                Success = false,
                ErrorMessage = message,
                Status = status
            };
        }
    }

    public enum Status
    {
        None = 0,
        Success = 200,
        Created = 201,
        NotFound = 404,
        Unauthorized = 401,
        NetworkError = 500,
        BadRequest = 400,
    }
}
