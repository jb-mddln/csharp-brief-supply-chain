using Npgsql;


namespace csharp_brief_supply_chain.database
{
    public class QueryManager
    {
        /*public async Task<Expedition> GetExpeditionById(int id)
        {
            string commandText = $"SELECT * FROM expeditions WHERE ID = @id";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        BoardGame game = ReadBoardGame(reader);
                        return game;
                    }
            }
            return null;
        }*/
    }
}
