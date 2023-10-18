namespace Persistent
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {

        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TKey id);
        List<TEntity> GetAll();
        void AddRange(List<TEntity> entities);
    }
}
