namespace BlazorPortfolio.Models
{
    public class Competence
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Level { get; set; } = 3; // Default level if not specified
        public string Icon { get; set; } = string.Empty; // Path to the icon image
    }
}