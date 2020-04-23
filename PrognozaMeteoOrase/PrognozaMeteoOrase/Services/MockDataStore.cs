using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrognozaMeteoOrase.Models;

namespace PrognozaMeteoOrase.Services
{
    public class MockDataStore : IDataStore<Oras>
    {
        readonly List<Oras> items;

        public MockDataStore()
        {
            items = new List<Oras>()
            {
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "Bucharest", Descriere="This is an item description." },
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "London", Descriere="This is an item description." },
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "New York", Descriere="This is an item description." },
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "Vienna", Descriere="This is an item description." },
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "Paris", Descriere="This is an item description." },
                new Oras { Id = Guid.NewGuid().ToString(), Denumire = "Barcelona", Descriere="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Oras item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Oras item)
        {
            var oldItem = items.Where((Oras arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Oras arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Oras> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Oras>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}