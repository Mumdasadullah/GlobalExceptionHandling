namespace InMemoryDBSpecificationRepositoryUOWProject.DTOs
{
    public class ApiResponse
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
