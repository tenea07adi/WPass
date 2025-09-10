using ConsoleApp.Models.Entity;

namespace ConsoleApp.Repository.Abstraction
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        public List<T> GetAll();
        public void Add(T entity);
    }
}
