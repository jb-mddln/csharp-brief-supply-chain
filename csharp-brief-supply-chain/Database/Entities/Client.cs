using System.Data;

namespace csharp_brief_supply_chain.Database.Entities
{
    /// <summary>
    /// Représente un objet de notre table clients
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        public string Nom { get; set; } = "";

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        public string Adresse { get; set; } = "";

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        public string Ville { get; set; } = "";

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        public string Pays { get; set; } = "";

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Client()
        {
        }

        /// <summary>
        /// Constructeur qui initialise notre objet avec le NpgsqlDataReader
        /// </summary>
        /// <param name="reader">NpgsqlDataReader de notre commande PostgreSQL</param>
        public Client(IDataRecord reader)
        {
            this.Id = (int)reader["id"];
            this.Nom = (string)reader["nom"];
            this.Adresse = (string)reader["adresse"];
            this.Ville = (string)reader["ville"];
            this.Pays = (string)reader["pays"];
        }

        /// <summary>
        /// Retourne les informations du client sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos du client</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Nom: {this.Nom}, Adresse: {this.Adresse}, Ville: {this.Ville}, Pays: {this.Pays}";
        }
    }
}