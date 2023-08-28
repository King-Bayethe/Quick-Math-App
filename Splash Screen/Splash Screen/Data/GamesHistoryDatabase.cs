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
    public class GamesHistoryDatabase
    {
        private readonly SQLiteAsyncConnection Database;


        public GamesHistoryDatabase(string dbPath)
        {
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<GamesHistory>();
        }

        //ALL GAMES
        public Task<List<GamesHistory>> GetAllGamesAsync()
        {
            return Database.Table<GamesHistory>().ToListAsync();
        }/*
        //COMPETITIVE GAMES ONLY
        public Task<List<GamesHistory>> GetAllCompetitiveGamesAsync()
        {
            
        }
        //INFINITE GAMES ONLY
        public Task<List<GamesHistory>> GetAllInfiniteGamesAsync()
        {
            
        }
        //GAMES OF THIS OPERATION ONLY
        public Task<List<GamesHistory>> GetAllGamesWithOperationAsync(string operation)
        {
            
        }
        //GAMES OF THIS LEVEL ONLY
        public Task<List<GamesHistory>> GetAllGamesWithLevelAsync(int level)
        {

        }
        //GAMES OF THIS DIFFICULTY ONLY
        public Task<List<GamesHistory>> GetAllGamesWithDifficultyAsync(string difficulty)
        {

        }
        //GAMES OF THIS MULTI ONLY
        public Task<List<GamesHistory>> GetAllGamesMULTIAsync(string mode, string operation, string difficulty)
        {

        }*/
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
