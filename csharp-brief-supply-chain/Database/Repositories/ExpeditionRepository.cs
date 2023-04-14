using csharp_brief_supply_chain.Database.Entities;
using Npgsql;

namespace csharp_brief_supply_chain.Database.Repositories
{
    public class ExpeditionRepository : IRepository<Expedition>
    {
        public NpgsqlConnection? Connection { get; set; }

        public ExpeditionRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        public IEnumerable<Expedition> GetAll()
        {
            List<Expedition> expeditions = new List<Expedition>();

            if (Connection == null)
                return expeditions;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions;", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expeditions.Add(new Expedition(reader));
                    }
                }
            }

            return expeditions;
        }

        public Expedition? GetById(int id)
        {
            if (Connection == null)
                return null;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from expeditions where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Expedition(reader);
                    }
                }
            }

            return null;
        }

        public void Insert(Expedition expedition)
        {
            if (Connection == null)
                return;

            if (expedition.Id < 0)
                return;

            var expeditionGetById = GetById(expedition.Id);
            if (expeditionGetById != null)
            {
                Console.WriteLine($"Error while trying to insert expedition {expedition.Id}, expedition already exist in database");
                return;
            }

            using (var cmd = new NpgsqlCommand(
                       "insert into expeditions (id, date_expedition, date_livraison, id_entrepot_source, id_entrepot_destination, poids, statut, date_livraison_prevu, id_client) VALUES (@id, @date_expedition, @date_livraison, @id_entrepot_source, @id_entrepot_destination, @poids, @statut, @date_livraison_prevu, @id_client)", Connection))
            {
                cmd.Parameters.AddWithValue("id", expedition.Id);
                cmd.Parameters.AddWithValue("date_expedition", expedition.DateExpedition);
                cmd.Parameters.AddWithValue("date_livraison", expedition.DateLivraison.HasValue ? expedition.IdClient.HasValue ? expedition.IdClient : DBNull.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("id_entrepot_source", expedition.EntrepotSourceId);
                cmd.Parameters.AddWithValue("id_entrepot_destination", expedition.EntrepotDestinationId);
                cmd.Parameters.AddWithValue("poids", expedition.Poids);
                cmd.Parameters.AddWithValue("statut", expedition.Statut);
                cmd.Parameters.AddWithValue("date_livraison_prevu", expedition.DateLivraisonPrevu);
                cmd.Parameters.AddWithValue("id_client", expedition.IdClient.HasValue ? expedition.IdClient : DBNull.Value);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Expedition {expedition.Id} saved in database");
                }
                else
                {
                    Console.WriteLine(
                        $"Error while trying to save expedition {expedition.Id} in database");
                }
            }
        }

        public void Update(Expedition expedition)
        {
            if (Connection == null)
                return;

            if (expedition.Id < 0)
                return;

            using (var cmd = new NpgsqlCommand("update expeditions set id=@id,  date_expedition=@date_expedition, date_livraison=@date_livraison, id_entrepot_source=@id_entrepot_source, id_entrepot_destination=@id_entrepot_destination, poids=@poids, statut=@statut, date_livraison_prevu=@date_livraison_prevu, id_client=@id_client)",
                       Connection))
            {
                cmd.Parameters.AddWithValue("id", expedition.Id);
                cmd.Parameters.AddWithValue("date_expedition", expedition.DateExpedition);
                cmd.Parameters.AddWithValue("date_livraison", expedition.DateLivraison.HasValue ? expedition.DateLivraison : DBNull.Value);
                cmd.Parameters.AddWithValue("id_entrepot_source", expedition.EntrepotSourceId);
                cmd.Parameters.AddWithValue("id_entrepot_destination", expedition.EntrepotDestinationId);
                cmd.Parameters.AddWithValue("poids", expedition.Poids);
                cmd.Parameters.AddWithValue("statut", expedition.Statut);
                cmd.Parameters.AddWithValue("date_livraison_prevu", expedition.DateLivraisonPrevu);
                cmd.Parameters.AddWithValue("id_client", expedition.IdClient.HasValue ? expedition.IdClient : DBNull.Value);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Expedition {expedition.Id} updated in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to update expedition {expedition.Id} in database");
                }
            }
        }

        public void Delete(int id)
        {
            if (Connection == null)
                return;

            Expedition? expedition = GetById(id);
            if (expedition == null)
                return;

            using (var cmd = new NpgsqlCommand("delete from expeditions where id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", expedition.Id);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Expedition {id} deleted in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to delete expedition {id} in database");
                }
            }
        }


        /// <summary>
        /// Retourne le dernier id disponible dans notre table et on y ajoute + 1 pour obtenir un id libre
        /// </summary>
        /// <returns>Le dernier id + 1 de notre table expeditions</returns>
        public int GetNextFreeId()
        {
            if (Connection == null)
                return -1;

            using (var cmd = new NpgsqlCommand("select id from expeditions order by id desc limit 1", Connection))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null)
                    return -1;

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
