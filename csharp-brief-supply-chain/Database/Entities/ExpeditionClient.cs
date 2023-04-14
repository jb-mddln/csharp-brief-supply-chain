using System.Data;

namespace csharp_brief_supply_chain.Database.Entities
{
    /// <summary>
    /// Représente un objet de notre table expeditions_clients
    /// </summary>
    public class ExpeditionClient
    {
        /// <summary>
        /// Champ id_expedition sur notre table expeditions_clients
        /// </summary>
        public int IdExpedition { get; set; }

        /// <summary>
        /// Champ id_client sur notre table expeditions_clients
        /// </summary>
        public int IdClient { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public ExpeditionClient()
        {
        }

        /// <summary>
        /// Constructeur qui initialise notre objet avec le NpgsqlDataReader
        /// </summary>
        /// <param name="reader">NpgsqlDataReader de notre commande PostgreSQL</param>
        public ExpeditionClient(IDataRecord reader)
        {
            this.IdExpedition = (int)reader["id_expedition"];
            this.IdClient = (int)reader["id_client"];
        }

        /// <summary>
        /// Retourne les informations des expeditions d'un client sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos des expeditions d'un client</returns>
        public override string ToString()
        {
            return $"IdExpedition: {this.IdExpedition}, IdClient: {this.IdClient}";
        }
    }
}