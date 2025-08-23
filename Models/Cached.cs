namespace BlazorPortfolio.Models
{
    public class Cached<T>
    {
        public T? Data { get; set; }
        public DateTimeOffset SavedAt { get; set; }
    }
}
