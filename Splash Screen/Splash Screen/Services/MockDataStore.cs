using Splash_Screen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splash_Screen.Services
{
    public class MockDataStore : IDataStore<GamesHistory>
    {
        readonly List<GamesHistory> games;

        public MockDataStore()
        {
            
        }

        public async Task<bool> AddItemAsync(GamesHistory game)
        {
            games.Add(game);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(GamesHistory game)
        {
            var oldItem = games.Where((GamesHistory arg) => arg.ID == game.ID).FirstOrDefault();
            games.Remove(oldItem);
            games.Add(game);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = games.Where((GamesHistory arg) => arg.ID == id).FirstOrDefault();
            games.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<GamesHistory> GetItemAsync(int id)
        {
            return await Task.FromResult(games.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<GamesHistory>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }
    }
}