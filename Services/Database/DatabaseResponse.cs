namespace Services.Database
{
    public class DatabaseResponse<TEntity> where TEntity : class
    {
        public enum ResponseCode
        {
            Success,
            PermissionsError,
            NoData
        }
        public readonly string Message;
        public readonly ResponseCode StatusCode;
        public readonly IList<TEntity>? Results;

        public DatabaseResponse(ResponseCode statusCode, IList<TEntity>? results = null, string? msg = null)
        {
            this.StatusCode = statusCode;
            this.Results = results ?? new List<TEntity>();
            this.Message = msg;
        }
    }
}
