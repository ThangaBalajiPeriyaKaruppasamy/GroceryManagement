using GroceryManagement.API.Models;
using System.Collections.Generic;

namespace GroceryManagement.API.Services
{
    public interface IGroceryService
    {
        IEnumerable<GroceryItem> GetAll();
        GroceryItem? GetById(int id);
        GroceryItem Create(GroceryItem item);
        bool Update(int id, GroceryItem updated);
        bool Delete(int id);
    }
}
