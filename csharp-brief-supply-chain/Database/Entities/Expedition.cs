using System.Data;

namespace csharp_brief_supply_chain.Database.Entities
{
    /// <summary>
    /// Représente un objet de notre table expeditions
    /// </summary>
    public class Expedition
    {
        /// <summary>
        /// Champ id sur notre table expeditions
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Champ date_expedition sur notre table expeditions
        /// </summary>
        public DateTime DateExpedition { get; set; }

        /// <summary>
        /// Champ date_livraison sur notre table expeditions
        /// </summary>
        public DateTime? DateLivraison { get; set; }

        /// <summary>
        /// Champ id_entrepot_source sur notre table expeditions
        /// </summary>
        public int EntrepotSourceId { get; set; }

        /// <summary>
        /// Champ id_entrepot_destination sur notre table expeditions
        /// </summary>
        public int EntrepotDestinationId { get; set; }

        /// <summary>
        /// Champ poids sur notre table expeditions
        /// </summary>
        public decimal Poids { get; set; }

        /// <summary>
        /// Champ statut sur notre table expeditions
        /// </summary>
        public string Statut { get; set; }

        /// <summary>
        /// Champ date_livraison_prevu sur notre table expeditions
        /// </summary>
        public DateTime DateLivraisonPrevu { get; set; }

        /// <summary>
        /// Champ id_client sur notre table expeditions
        /// </summary>
        public int? IdClient { get; set; }

        /// <summary>
        /// Constructeur qui initialise notre objet avec le NpgsqlDataReader
        /// </summary>
        /// <param name="reader">NpgsqlDataReader de notre commande PostgreSQL</param>
        public Expedition(IDataRecord reader)
        {
            this.Id = (int)reader["id"];
            this.DateExpedition = (DateTime)reader["date_expedition"];
            this.DateLivraison = reader["date_livraison"] is DBNull ? null : (DateTime)reader["date_livraison"];
            this.EntrepotSourceId = (int)reader["id_entrepot_source"];
            this.EntrepotDestinationId = (int)reader["id_entrepot_destination"];
            this.Poids = (decimal)reader["poids"];
            this.Statut = (string)reader["statut"];
            this.DateLivraisonPrevu = (DateTime)reader["date_livraison_prevu"];
            this.IdClient = reader["id_client"] is DBNull ? null : (int)reader["id_client"];
        }

        /// <summary>
        /// Retourne les informations de l'expédition sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos de l'expédition</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Date Expedition: {this.DateExpedition:dd/MM/yyyy}, Date Livraison: {(DateLivraison.HasValue ? this.DateLivraison.Value.ToString("dd/MM/yyyy") : "null" )}, Entrepot Source Id: {this.EntrepotSourceId}, Entrepot Destination Id: {this.EntrepotDestinationId}, Poids: {this.Poids}, Statut: {this.Statut}, DateLivraisonPrevu: {this.DateLivraisonPrevu:dd/MM/yyyy}, IdClient: {(IdClient.HasValue ? this.IdClient.Value : "null")}";
        }
    }
}
