using csharp_brief_supply_chain.database;
using csharp_brief_supply_chain.database.tables;
using Npgsql;

namespace csharp_brief_supply_chain
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Entrepot> entrepots = new List<Entrepot>();
            List<Expedition> expeditions = new List<Expedition>();

            DatabaseManager databaseManager = new DatabaseManager
            {
                Host = "localhost",
                Username = "postgres",
                Password = "root",
                Database = "transport_logistique"
            };

            databaseManager.OpenConnection();

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from entrepots;", databaseManager.Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Entrepot entrepot = new Entrepot(reader);

                        entrepots.Add(entrepot);
                    }
                }
            }

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions;", databaseManager.Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Expedition expedition = new Expedition();
                        expedition.Id = (int)reader["id"];
                        expedition.DateExpedition = (DateTime)reader["date_expedition"];
                        expedition.DateLivraison = reader["date_livraison"] is DBNull ? null : (DateTime)reader["date_livraison"];
                        expedition.EntrepotSourceId = (int)reader["id_entrepot_source"];
                        expedition.EntrepotDestinationId = (int)reader["id_entrepot_destination"];
                        expedition.Poids = (decimal)reader["poids"];
                        expedition.Statut = (string)reader["statut"];
                        expedition.DateLivraisonPrevu = (DateTime)reader["date_livraison_prevu"];

                        expeditions.Add(expedition);
                    }
                }
            }

            Console.WriteLine(string.Join("\n", entrepots.Select(entrepot => entrepot.ToString())));
            // Console.WriteLine(string.Join("\n", expeditions.Select(expedition => expedition.ToString())));

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}