using Npgsql;

namespace csharp_brief_supply_chain.database
{
    public class DatabaseManager
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
    }
}