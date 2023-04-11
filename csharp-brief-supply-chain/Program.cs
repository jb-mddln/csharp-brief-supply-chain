using csharp_brief_supply_chain.database;
using csharp_brief_supply_chain.database.tables;
using Npgsql;
using System.Linq;

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
                        entrepots.Add(new Entrepot(reader));
                    }
                }
            }

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions;", databaseManager.Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expeditions.Add(new Expedition(reader));
                    }
                }
            }

            // Console.WriteLine(string.Join("\n", entrepots.Select(entrepot => entrepot.ToString())));
            // Console.WriteLine(string.Join("\n", expeditions.Select(expedition => expedition.ToString())));

            var testSelect = databaseManager.SelectAll("entrepots")
                .Select(dictionary => new Entrepot(dictionary));

            Console.WriteLine(string.Join("\n", testSelect.Select(entrepot => entrepot.ToString())));

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}