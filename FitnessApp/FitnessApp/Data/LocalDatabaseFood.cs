using FitnessApp.Interfaces;
using FitnessApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Data
{
    public class LocalDatabaseFood : ILocalDatabase<Food>
    {
        //private readonly SQLiteAsyncConnection database;

        public LocalDatabaseFood(string dbPath)
        {
            //database = new SQLiteAsyncConnection(dbPath);
            //database.CreateTableAsync<Food>().Wait();
        }
        
        public async Task<List<Food>> GetItemsAsync()
        {
            return await database.Table<Food>().ToListAsync();
        }

        public async Task<Food> GetItemAsync(string id)
        {
            return await database.Table<Food>().Where(food => food.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Food item)
        {
            var f = await database.Table<Food>().Where(menu => menu.ID == item.ID).FirstOrDefaultAsync();

            if (f != null)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Food item)
        {
            return await database.DeleteAsync(item);
        }
    }
}
