namespace csharp_brief_supply_chain.Database
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        int GetNextFreeId();
    }
}