using csharp_brief_supply_chain.database.tables;
using Npgsql;

namespace csharp_brief_supply_chain.database
{
    public class DatabaseManager : IQuery<Dictionary<string, object>>
    {
        public string Host { get; set; } = "localhost";

        public string Username { get; set; } = "";
        
        public string Password { get; set; } = "";

        public string Database { get; set; } = "";

        public NpgsqlConnection? Connection { get; set; }

        public void OpenConnection()
        {
            NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder($"host={Host};username={Username};password={Password};database={Database};");
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();

            try
            {
                Connection = dataSource.OpenConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Retourne les données d'une table sous forme d'une liste contenant un dictionnaire avec pour clé le nom de nos colonnes et leurs valeurs associées
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>Les données d'une table sous forme d'une liste contenant un dictionnaire avec pour clé le nom de nos colonnes et leurs valeurs associées</returns>
        public List<Dictionary<string, object>> SelectAll(string tableName)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from {tableName};", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Stock le nom de notre colonne et sa valeur exemple id, int 1 ou nom_entrepot string Entrepot #6
                        Dictionary<string, object> values = new Dictionary<string, object>();

                        // Boucle sur le nombre de colonnes présent dans notre table (id, nom_entrepot, adresse ...) exemple table entrepots = 5
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // Récupère le nom de notre colonne exemple id, nom_entrepot ...
                            string columnName = reader.GetName(i);

                            // Récupère sa valeur
                            object columnValue = reader.GetValue(i);

                            // Stock le résultat associés dans notre dictionnaire
                            values.Add(columnName, columnValue);
                        }
                        results.Add(values);
                    }
                }
            }
            return results;
        }
    }
}