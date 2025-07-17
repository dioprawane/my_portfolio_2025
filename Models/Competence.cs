namespace BlazorPortfolio.Models
{
    public class Competence
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Level { get; set; } = 3; // Default level if not specified
        public string Icon { get; set; } // Radzen icon name (e.g., "code", "storage", "html")
    }
}