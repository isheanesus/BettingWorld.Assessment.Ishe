namespace BettingWorld.Assessment.Ishe.API.Interfaces
{
    public interface IRepository<T> where T : class
    {

        public Task CreateAsync(T entity);
        public Task<ICollection<T>> ListAsync();


    }
}
