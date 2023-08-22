using Splash_Screen.Models;
using Splash_Screen.Special;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splash_Screen.Data
{
    public class InfiniteGameDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<InfiniteGameDatabase> Instance = new AsyncLazy<InfiniteGameDatabase>(async () =>
        {
            var instance = new InfiniteGameDatabase();
            CreateTableResult result = await Database.CreateTableAsync<RecordInfiniteGame>();
            return instance;
        });

        public InfiniteGameDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<RecordInfiniteGame>> GetItemsAsync()
        {
            return Database.Table<RecordInfiniteGame>().ToListAsync();
        }

        public Task<List<RecordInfiniteGame>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<RecordInfiniteGame>("SELECT * FROM [RecordInfiniteGame] WHERE [Done] = 0");
        }

        public Task<RecordInfiniteGame> GetItemAsync(int id)
        {
            return Database.Table<RecordInfiniteGame>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(RecordInfiniteGame item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(RecordInfiniteGame item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
