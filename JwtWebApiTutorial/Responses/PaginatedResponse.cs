namespace JwtWebApiTutorial.Responses
{
    public class PaginatedResponse<T>
        where T : class
    {
        public PaginatedResponse()
        {
            Data = new List<T>();
        }

        public int TotalData { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public List<T> Data { get; set; }
    }
}
