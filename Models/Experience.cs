namespace BlazorPortfolio.Models
{
    public class Experience
    {
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public List<string> Responsibilities { get; set; } = new();
        public List<string> Skills { get; set; } = new(); // <-- Ajouté
        public string ImageCompetence { get; set; } = string.Empty; // Chemin vers le logo de l'entreprise
    }
}
