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
        private readonly SQLiteAsyncConnection Database;

        public InfiniteGameDatabase(string dbPath)
        {
            Database = new SQLiteAsyncConnection(dbPath);
            Database.CreateTableAsync<RecordInfiniteGame>();
        }
        public Task<List<RecordInfiniteGame>> GetGamesAsync()
        {
            return Database.Table<RecordInfiniteGame>().ToListAsync();
        }
        //GAMES OF THIS DIFFICULTY ONLY
        /*public Task<List<RecordInfiniteGame>> GetAllGamesWithDifficultyAsync(string difficulty)
        {

        }*/

        public Task<int> SaveGameAsync(RecordInfiniteGame game)
        {
            return Database.InsertAsync(game);
        }

        public Task<int> DeleteItemAsync(RecordInfiniteGame game)
        {
            return Database.DeleteAsync(game);
        }

        public Task<List<RecordInfiniteGame>> GetQueryAsync(string timeFrame, string operation, string level)
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
            return Database.QueryAsync<RecordInfiniteGame>("SELECT * FROM RecordInfiniteGame WHERE Date BETWEEN " + dtPast + " AND " + dtNow + " AND WHERE Map = " + operation + " AND WHERE Level = " + level);
        }
        public Task<List<RecordInfiniteGame>> GetLevelAsync(string operation)
        {
            return Database.QueryAsync<RecordInfiniteGame>("SELECT * FROM [RecordInfiniteGame] WHERE [Done] = 0");
        }
        public Task<RecordInfiniteGame> GetItemAsync(int id)
        {
            return Database.Table<RecordInfiniteGame>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
    }
}
