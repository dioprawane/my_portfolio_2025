// Formation.cs
namespace BlazorPortfolio.Models // Assurez-vous que le namespace correspond à votre projet
{
    public class Formations
    {
        // Identifiant unique de la formation autogénéré par la base de données
        public int Id { get; set; } // Identifiant unique de la formation
        public string Titre { get; set; }
        public string Etablissement { get; set; }
        public string Lieu { get; set; }
        public int AnneeDebut { get; set; }
        public int AnneeFin { get; set; }
        public string Mention { get; set; } // Ex: "Très Bien", "Bien"
        public string DescriptionCourte { get; set; }
        public List<string> CompetencesAcquises { get; set; }
        public string CertificatUrl { get; set; } // Lien vers un certificat ou diplôme
        public string ImageEcole { get; set; } // Chemin vers le logo de l'école
    }
}