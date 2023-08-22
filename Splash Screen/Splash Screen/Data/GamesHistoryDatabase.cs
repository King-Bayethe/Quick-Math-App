using Splash_Screen.Views;
using Splash_Screen.Special;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Splash_Screen.Models;

namespace Splash_Screen.Data
{
    class GamesHistoryDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<GamesHistoryDatabase> Instance = new AsyncLazy<GamesHistoryDatabase>(async () =>
        {
            var instance = new GamesHistoryDatabase();
            CreateTableResult result = await Database.CreateTableAsync<GamesHistory>();
            return instance;
        });

        public GamesHistoryDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<GamesHistory>> GetItemsAsync()
        {
            return Database.Table<GamesHistory>().ToListAsync();
        }

        public Task<List<GamesHistory>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<GamesHistory>("SELECT * FROM [GamesHistory] WHERE [Done] = 0");
        }

        public Task<GamesHistory> GetItemAsync(int id)
        {
            return Database.Table<GamesHistory>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(GamesHistory item)
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

        public Task<int> DeleteItemAsync(GamesHistory item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
