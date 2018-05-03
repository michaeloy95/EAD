using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessApp.Interfaces
{
    public interface ILocalDatabase<T>
    {
        Task<List<T>> GetItemsAsync();

        Task<T> GetItemAsync(string id);

        Task<int> SaveItemAsync(T item);

        Task<int> DeleteItemAsync(T item);
    }
}
