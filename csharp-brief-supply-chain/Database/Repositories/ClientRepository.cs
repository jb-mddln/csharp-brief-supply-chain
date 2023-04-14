using csharp_brief_supply_chain.Database.Entities;
using Npgsql;

namespace csharp_brief_supply_chain.Database.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        public NpgsqlConnection? Connection { get; set; }

        public ClientRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        public IEnumerable<Client> GetAll()
        {
            List<Client> clients = new List<Client>();

            if (Connection == null)
                return clients;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from clients;", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client(reader));
                    }
                }
            }

            return clients;
        }

        public Client? GetById(int id)
        {
            if (Connection == null)
                return null;

            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from clients where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Client(reader);
                    }
                }
            }

            return null;
        }

        public void Insert(Client client)
        {
            if (Connection == null)
                return;

            if (client.Id < 0)
                return;

            var clientGetById = GetById(client.Id);
            if (clientGetById != null)
            {
                Console.WriteLine($"Error while trying to insert client {client.Id}, client already exist in database");
                return;
            }

            using (var cmd = new NpgsqlCommand("insert into clients (id, nom, adresse, ville, pays) VALUES (@id, @nom, @adresse, @ville, @pays)", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Parameters.AddWithValue("nom_entrepot", client.Nom);
                cmd.Parameters.AddWithValue("adresse", client.Adresse);
                cmd.Parameters.AddWithValue("ville", client.Ville);
                cmd.Parameters.AddWithValue("pays", client.Pays);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Client {client.Id} {client.Nom} saved in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to save client {client.Id} {client.Nom} in database");
                }
            }
        }

        public void Update(Client client)
        {
            if (Connection == null)
                return;

            if (client.Id < 0)
                return;
            
            using (var cmd = new NpgsqlCommand("update clients set id=@id, nom=@nom, adresse=@adresse, ville=@ville, pays=@pays)", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Parameters.AddWithValue("nom", client.Nom);
                cmd.Parameters.AddWithValue("adresse", client.Adresse);
                cmd.Parameters.AddWithValue("ville", client.Ville);
                cmd.Parameters.AddWithValue("pays", client.Pays);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Client {client.Id} {client.Nom} updated in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to update client {client.Id} {client.Nom} in database");
                }
            }
        }

        public void Delete(int id)
        {
            if (Connection == null)
                return;

            Client? client = GetById(id);
            if (client == null)
                return;

            using (var cmd = new NpgsqlCommand("delete from clients where id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);

                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Client {id} deleted in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to delete client {id} in database");
                }
            }
        }


        /// <summary>
        /// Retourne le dernier id disponible dans notre table et on y ajoute + 1 pour obtenir un id libre
        /// </summary>
        /// <returns>Le dernier id + 1 de notre table clients</returns>
        public int GetNextFreeId()
        {
            if (Connection == null)
                return -1;

            using (var cmd = new NpgsqlCommand("select id from clients order by id desc limit 1", Connection))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null)
                    return -1;

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
