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
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Champ nom sur notre table clients
        /// </summary>
        [Column("nom")]
        public string Nom { get; set; } = "";

        /// <summary>
        /// Champ adresse sur notre table clients
        /// </summary>
        [Column("adresse")]
        public string Adresse { get; set; } = "";

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        [Column("ville")]
        public string Ville { get; set; } = "";

        /// <summary>
        /// Champ id sur notre table clients
        /// </summary>
        [Column("pays")]
        public string Pays { get; set; } = "";

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Client()
        {
        }

        public Client(int id, string nom, string adresse, string ville, string pays)
        {
            Id = id;
            Nom = nom;
            Adresse = adresse;
            Ville = ville;
            Pays = pays;
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