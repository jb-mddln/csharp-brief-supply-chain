namespace csharp_brief_supply_chain.database
{
    public interface IQuery<T> where T : class, new()
    {
        List<T> SelectAll(string tableName);
    }
}
