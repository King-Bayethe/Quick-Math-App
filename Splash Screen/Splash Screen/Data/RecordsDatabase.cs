using Splash_Screen.Views;
using Splash_Screen.Models;
using Splash_Screen.Special;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace Splash_Screen.Data
{
    public class RecordsDatabase
    {
        private readonly SQLiteAsyncConnection Database;

        public RecordsDatabase(string dbPath)
        {
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<Records>();
        }

        public RecordsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Records>> GetgamesAsync()
        {
            return Database.Table<Records>().ToListAsync();
        }

        public Task<List<Records>> GetgamesNotDoneAsync()
        {
            return Database.QueryAsync<Records>("SELECT * FROM [Records] WHERE [] = 0");
        }

        public Task<List<Records>> GetBestGameAsync(string map)
        {
            return Database.QueryAsync<Records>("SELECT * FROM [Records] WHERE [Map] = ?", map);
        }
        public async void UpdateBestGameAsync(Game match)
        {
            bool exists = await Database.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM [Records] WHERE HighScore<? && WHERE Map=?)", match.Score, match.Map);
            if (exists)
            {
                await Database.UpdateAsync(match);
            }
        }
        

/*public Task<Records> GetGameAsync(int id)
        {
            return Database.Table<Records>().Where(i => i.GameID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavegameAsync(Records game)
        {
            if (game.GameID != 0)
            {
                return Database.UpdateAsync(game);
            }
            else
            {
                return Database.InsertAsync(game);
            }
        }*/

        public Task<int> DeletegameAsync(Records game)
        {
            return Database.DeleteAsync(game);
        }
    }
}
