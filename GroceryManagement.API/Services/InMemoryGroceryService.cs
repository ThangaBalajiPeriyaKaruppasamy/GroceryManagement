using GroceryManagement.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace GroceryManagement.API.Services
{
    public class InMemoryGroceryService : IGroceryService
    {
        private readonly List<GroceryItem> _items = new();
        private int _nextId = 1;

        public IEnumerable<GroceryItem> GetAll() => _items;

        public GroceryItem? GetById(int id) =>
            _items.FirstOrDefault(x => x.Id == id);

        public GroceryItem Create(GroceryItem item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return item;
        }

        public bool Update(int id, GroceryItem updated)
        {
            var existing = GetById(id);
            if (existing is null) return false;

            existing.Name = updated.Name;
            existing.Quantity = updated.Quantity;
            existing.Price = updated.Price;
            return true;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item is null) return false;

            _items.Remove(item);
            return true;
        }
    }
}
