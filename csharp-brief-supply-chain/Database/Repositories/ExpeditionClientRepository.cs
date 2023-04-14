using csharp_brief_supply_chain.Database.Entities;
using Npgsql;

namespace csharp_brief_supply_chain.Database.Repositories
{
    public class ExpeditionClientRepository : IRepository<ExpeditionClient>
    {
        public NpgsqlConnection? Connection { get; set; }

        public ExpeditionClientRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        public IEnumerable<ExpeditionClient> GetAll()
        {
            List<ExpeditionClient> expeditionsClients = new List<ExpeditionClient>();

            if (Connection == null)
                return expeditionsClients;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions_clients;", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expeditionsClients.Add(new ExpeditionClient(reader));
                    }
                }
            }

            return expeditionsClients;
        }

        public ExpeditionClient? GetById(int id)
        {
            if (Connection == null)
                return null;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions_clients where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ExpeditionClient(reader);
                    }
                }
            }

            return null;
        }

        public void Insert(ExpeditionClient expeditionClient)
        {
            if (Connection == null)
                return;

            if (expeditionClient.IdExpedition < 0)
                return;

            if (expeditionClient.IdClient < 0)
                return;

            var expeditionClientGetById = GetById(expeditionClient.IdExpedition);
            if (expeditionClient != null)
            {
                Console.WriteLine($"Error while trying to insert expeditionClient {expeditionClient.IdExpedition}, expeditionClient already exist in database");
                return;
            }

            using (var cmd = new NpgsqlCommand("insert into expeditions_clients (id_expedition, id_client) VALUES (@id_expedition, @id_client)", Connection))
            {
                cmd.Parameters.AddWithValue("id_expedition", expeditionClient.IdExpedition);
                cmd.Parameters.AddWithValue("id_client", expeditionClient.IdClient);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"ExpeditionClient {expeditionClient.IdExpedition}:{expeditionClient.IdClient} saved in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to save client {expeditionClient.IdExpedition}:{expeditionClient.IdClient} in database");
                }
            }
        }

        public void Update(ExpeditionClient expeditionClient)
        {
            if (Connection == null)
                return;

            if (expeditionClient.IdExpedition < 0)
                return;

            if (expeditionClient.IdExpedition < 0)
                return;

            using (var cmd = new NpgsqlCommand("update expeditions_clients set id_expedition=@id_expedition, id_client=@id_client)", Connection))
            {
                cmd.Parameters.AddWithValue("id_expedition", expeditionClient.IdExpedition);
                cmd.Parameters.AddWithValue("id_client", expeditionClient.IdClient);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"ExpeditionClient {expeditionClient.IdExpedition}:{expeditionClient.IdClient} updated in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to update ExpeditionClient {expeditionClient.IdExpedition}:{expeditionClient.IdClient} in database");
                }
            }
        }

        public void Delete(int id)
        {
            if (Connection == null)
                return;

            ExpeditionClient? expeditionClient = GetById(id);
            if (expeditionClient == null)
                return;

            using (var cmd = new NpgsqlCommand("delete from expeditions_clients where id_expedition=@id_expedition", Connection))
            {
                cmd.Parameters.AddWithValue("id", expeditionClient.IdExpedition);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"ExpeditionClient {id} deleted in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to delete ExpeditionClient {id} in database");
                }
            }
        }


        /// <summary>
        /// Retourne le dernier id disponible dans notre table et on y ajoute + 1 pour obtenir un id libre
        /// </summary>
        /// <returns>Le dernier id + 1 de notre table expeditions_clients</returns>
        public int GetNextFreeId()
        {
            if (Connection == null)
                return -1;

            using (var cmd = new NpgsqlCommand("select id from expeditions_clients order by id desc limit 1", Connection))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null)
                    return -1;

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
