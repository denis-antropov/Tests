namespace Workers.DataLayer
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> GetEntities();

        T Get(long id);
        void Save(T entity);
        void Delete(T entity);
    }
}
