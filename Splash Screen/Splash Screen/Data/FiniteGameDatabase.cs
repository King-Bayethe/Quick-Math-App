using Splash_Screen.Views;
using Splash_Screen.Special;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Splash_Screen.Models;
using Azure;
using Xamarin.Essentials;

namespace Splash_Screen.Data
{
    public class FiniteGameDatabase
    {
        private readonly SQLiteAsyncConnection Database;

      
        public FiniteGameDatabase(string dbPath)
        {
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<RecordFiniteGame>();
        }

        public Task<List<RecordFiniteGame>> GetGamesAsync()
        {
            return Database.Table<RecordFiniteGame>().ToListAsync();
        }
        
        public Task<int> SaveGameAsync(RecordFiniteGame game)
        {
            return Database.InsertAsync(game);
        }
        
        public Task<int> DeleteItemAsync(RecordFiniteGame game)
        {
            return Database.DeleteAsync(game);
        }

        public Task<List<RecordFiniteGame>> GetQueryAsync(string timeFrame, string operation, string level)
        {
            
            DateTime date;
            switch (timeFrame)
            {

                case "30 Days":
                    date = DateTime.Now.AddDays(-30);
                    break;
                case "3 Months":
                    date = DateTime.Now.AddMonths(-3);
                    break;
                case "6 Months":
                    date = DateTime.Now.AddMonths(-6);
                    break;

                case "9 Months":
                    date = DateTime.Now.AddMonths(-9);
                    break;
                case "1 Year":
                    date = DateTime.Now.AddYears(-1);
                    break;
                case "All Time":
                    date = DateTime.Now.AddYears(-100);
                    break;
                default:
                    date = DateTime.Now.AddDays(-30); 
                    break;

            }
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd");
            string dtPast = date.ToString("yyyy-MM-dd");
            return Database.QueryAsync<RecordFiniteGame>("SELECT * FROM RecordFiniteGame WHERE Date BETWEEN " + dtPast + " AND " + dtNow + " AND WHERE Map = " + operation + " AND WHERE Level = " + level);
        }
        public Task<List<RecordFiniteGame>> GetLevelAsync(string operation)
        {
            return Database.QueryAsync<RecordFiniteGame>("SELECT * FROM [RecordFiniteGame] WHERE [Done] = 0");
        }
        public Task<RecordFiniteGame> GetItemAsync(int id)
        {
            return Database.Table<RecordFiniteGame>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<RecordFiniteGame> GetGameAsync(int id)
        {
            return Database.Table<RecordFiniteGame>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        //GAMES OF THIS OPERATION ONLY
        public Task<List<RecordFiniteGame>> GetAllGamesWithOperationAsync(string operation)
        {
            return Database.QueryAsync<RecordFiniteGame>("SELECT * FROM [RecordFiniteGame] WHERE [Operation] = " + operation);
        }


    }
}
