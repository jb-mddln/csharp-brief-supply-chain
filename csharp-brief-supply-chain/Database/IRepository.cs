namespace csharp_brief_supply_chain.Database
{
    /// <summary>
    /// Interface pour définir nos différentes actions de base sur nos repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
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