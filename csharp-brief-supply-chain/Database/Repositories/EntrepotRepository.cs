using csharp_brief_supply_chain.Database.Entities;
using Npgsql;

namespace csharp_brief_supply_chain.Database.Repositories
{
    public class EntrepotRepository : IRepository<Entrepot>
    {
        public NpgsqlConnection? Connection { get; set; }

        public EntrepotRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        public IEnumerable<Entrepot> GetAll()
        {
            List<Entrepot> entrepots = new List<Entrepot>();

            if (Connection == null)
                return entrepots;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from entrepots;", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entrepots.Add(new Entrepot(reader));
                    }
                }
            }

            return entrepots;
        }

        public Entrepot? GetById(int id)
        {
            if (Connection == null)
                return null;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from entrepots where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Entrepot(reader);
                    }
                }
            }

            return null;
        }

        public void Insert(Entrepot entrepot)
        {
            if (Connection == null)
                return;

            if (entrepot.Id < 0)
                return;

            using (var cmd = new NpgsqlCommand("insert into entrepots (id, nom_entrepot, adresse, ville, pays) VALUES (@id, @nom_entrepot, @adresse, @ville, @pays)", Connection))
            {
                cmd.Parameters.AddWithValue("id", entrepot.Id);
                cmd.Parameters.AddWithValue("nom_entrepot", entrepot.Nom);
                cmd.Parameters.AddWithValue("adresse", entrepot.Adresse);
                cmd.Parameters.AddWithValue("ville", entrepot.Ville);
                cmd.Parameters.AddWithValue("pays", entrepot.Pays);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Entrepot {entrepot.Id} {entrepot.Nom} saved in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to save entrepot {entrepot.Id} {entrepot.Nom} in database");
                }
            }
        }

        public void Update(Entrepot entrepot)
        {
            if (Connection == null)
                return;

            if (entrepot.Id < 0)
                return;

            using (var cmd = new NpgsqlCommand("update entrepots set id=@id, nom_entrepot=@nom_entrepot, adresse=@adresse, ville=@ville, pays=@pays)",
                       Connection))
            {
                cmd.Parameters.AddWithValue("id", entrepot.Id);
                cmd.Parameters.AddWithValue("nom_entrepot", entrepot.Nom);
                cmd.Parameters.AddWithValue("adresse", entrepot.Adresse);
                cmd.Parameters.AddWithValue("ville", entrepot.Ville);
                cmd.Parameters.AddWithValue("pays", entrepot.Pays);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Entrepot {entrepot.Id} {entrepot.Nom} updated in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to update entrepot {entrepot.Id} {entrepot.Nom} in database");
                }
            }
        }

        public void Delete(int id)
        {
            if (Connection == null)
                return;

            Entrepot? entrepot = GetById(id);
            if (entrepot == null)
                return;

            using (var cmd = new NpgsqlCommand("delete from entrepots where id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entrepot.Id);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Entrepot {id} deleted in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to delete entrepot {id} in database");
                }
            }
        }


        /// <summary>
        /// Retourne le dernier id disponible dans notre table et on y ajoute + 1 pour obtenir un id libre
        /// </summary>
        /// <returns>Le dernier id + 1 de notre table entrepots</returns>
        public int GetNextFreeId()
        {
            if (Connection == null)
                return -1;

            using (var cmd = new NpgsqlCommand("select id from entrepots order by id desc limit 1", Connection))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null)
                    return -1;

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
