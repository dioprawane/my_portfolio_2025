// Formation.cs
namespace BlazorPortfolio.Models // Assurez-vous que le namespace correspond à votre projet
{
    public class Formations
    {
        // Identifiant unique de la formation autogénéré par la base de données
        public int Id { get; set; } // Identifiant unique de la formation
        public string Titre { get; set; } = string.Empty;
        public string Etablissement { get; set; } = string.Empty;
        public string Lieu { get; set; } = string.Empty;
        public int AnneeDebut { get; set; }
        public int AnneeFin { get; set; }
        public string Mention { get; set; } = string.Empty;
        public string DescriptionCourte { get; set; } = string.Empty;
        public List<string> CompetencesAcquises { get; set; } = new List<string>();
        public string CertificatUrl { get; set; } = string.Empty;
        public string ImageEcole { get; set; } = string.Empty; // Chemin vers le logo de l'école
    }
}