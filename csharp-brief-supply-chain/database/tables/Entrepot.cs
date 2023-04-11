using System.Data;

namespace csharp_brief_supply_chain.database.tables
{
    /// <summary>
    /// Représente un objet de notre table entrepots
    /// </summary>
    public class Entrepot
    {
        /// <summary>
        /// Champ id sur notre table entrepots
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Champ id sur notre table entrepots
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Champ id sur notre table entrepots
        /// </summary>
        public string Adresse { get; set; }

        /// <summary>
        /// Champ id sur notre table entrepots
        /// </summary>
        public string Ville { get; set; }

        /// <summary>
        /// Champ id sur notre table entrepots
        /// </summary>
        public string Pays { get; set; }

        /// <summary>
        /// Constructeur qui initialise notre objet avec le NpgsqlDataReader
        /// </summary>
        /// <param name="reader">NpgsqlDataReader de notre commande PostgreSQL</param>
        public Entrepot(IDataRecord reader)
        {
            Id = (int)reader["id"];
            Nom = (string)reader["nom_entrepot"];
            Adresse = (string)reader["adresse"];
            Ville = (string)reader["ville"];
            Pays = (string)reader["pays"];
        }

        /// <summary>
        /// Retourne les informations de l'entrepôt sous forme de chaine de caractères
        /// </summary>
        /// <returns>Infos de l'entrepôt</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Nom: {Nom}, Adresse: {Adresse}, Ville: {Ville}, Pays: {Pays}";
        }
    }
}
