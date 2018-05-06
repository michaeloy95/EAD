using FitnessApp.Interfaces;
using FitnessApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data
{
    public class LocalDatabaseMeal : ILocalDatabase<Meal>
    {
        private readonly SQLiteAsyncConnection database;

        public LocalDatabaseMeal(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Meal>().Wait();
        }

        public async Task<List<Meal>> GetItemsAsync()
        {
            return await database.Table<Meal>().ToListAsync();
        }

        public async Task<Meal> GetItemAsync(string id)
        {
            return await database.Table<Meal>().Where(food => food.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Meal item)
        {
            var f = await database.Table<Meal>().Where(menu => menu.ID == item.ID).FirstOrDefaultAsync();

            if (f != null)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Meal item)
        {
            return await database.DeleteAsync(item);
        }
    }
}
