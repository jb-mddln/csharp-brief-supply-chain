using csharp_brief_supply_chain.Database.Entities;
using Npgsql;
using System.Data;

namespace csharp_brief_supply_chain.Database
{
    public abstract class AbstractRepository<T>
    {
        public NpgsqlConnection? Connection { get; set; }

        protected AbstractRepository(NpgsqlConnection? connection)
        {
            Connection = connection;
        }

        protected abstract string TableName { get; set; }

        protected abstract T SqlToEntity(IDataRecord data);

        public IEnumerable<T> GetAll()
        {
            var all = new List<T>();
            if (Connection == null)
                return all;

            using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from {TableName};", Connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        all.Add(SqlToEntity(reader));
                    }
                }
            }
            return all;
        }

        public T? GetById(int id)
        {
            if (Connection == null)
                return default;

            using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from {TableName} where id = @id;", Connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SqlToEntity(reader);
                    }
                }
            }

            return default;
        }
    }
}
