using System.Data;
using csharp_brief_supply_chain.Database.Entities;
using Npgsql;

namespace csharp_brief_supply_chain.Database.Repositories
{
    public class ClientRepository : AbstractRepository<Client>
    {
        public ClientRepository(NpgsqlConnection? connection) : base(connection)
        {
        }

        protected override string TableName { get; set; } = "clients";

        protected override Client SqlToEntity(IDataRecord data)
        {
            return new Client(data);
        }
    }
}
