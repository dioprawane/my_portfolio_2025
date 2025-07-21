namespace BlazorPortfolio.Models
{
    public class Projet
    {
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Technologies { get; set; } = new List<string>();
        public List<string> Images { get; set; } = new List<string>();
        public string LienGithub { get; set; } = string.Empty;
        public string LienDemo { get; set; } = string.Empty;
    }
}
