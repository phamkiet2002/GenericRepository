namespace WebTutorial.GenericRepository
{
    public interface IGenericRepository<T>  where T : class
    {
        Task<List<T>> GettAllAsync();
        Task<T> GetByIdAsync(int id);
        //Task<List<T>> FilterAsync(string keyword, string orderColumn, bool isAsc, int pageSize, out int totalPage);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
