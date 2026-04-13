using ItemRazor.Comperators;
using ItemRazorV1.MockData;
using ItemRazorV1.Models;

namespace ItemRazorV1.Service
{
    public class ItemService : IItemService
    {
        private List<Item> _items;

        private JsonFileService<Item> _jsonFileItemService;
        private DbGenericService<Item> _dbService;

        public ItemService(JsonFileService<Item> jsonFileItemService, DbGenericService<Item> dbService)
        {
            _jsonFileItemService = jsonFileItemService;
            _dbService = dbService;
            // _items = MockItems.GetMockItems();
            //_items = _jsonFileItemService.GetJsonObjects().ToList();
            //_dbService.SaveObjects(_items);
            _items = _dbService.GetObjectsAsync().Result.ToList();
        }

        public ItemService()
        {
            _items = MockItems.GetMockItems();
        }

        public async Task AddItemAsync(Item item)
        {
            _items.Add(item);
            //_jsonFileItemService.SaveJsonObjects(_items);
            await _dbService.AddObjectAsync(item);
        }

        public Item GetItem(int id)
        {
            foreach (Item item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            return null;
        }

        public async Task UpdateItemAsync(Item item)
        {
            if (item != null)
            {
                foreach (Item i in _items)
                {
                    if (i.Id == item.Id)
                    {
                        i.Name = item.Name;
                        i.Price = item.Price;
                        i.Description = item.Description;
                        i.ItemImage = item.ItemImage;
                    }
                }
                //_jsonFileItemService.SaveJsonObjects(_items);
                await _dbService.UpdateObjectAsync(item);
            }
        }

        public async Task<Item> DeleteItemAsync(int? itemId)
        {
            Item itemToBeDeleted = null;
            foreach (Item item in _items)
            {
                if (item.Id == itemId)
                {
                    itemToBeDeleted = item;
                    break;
                }
            }

            if (itemToBeDeleted != null)
            {
                _items.Remove(itemToBeDeleted);
                //_jsonFileItemService.SaveJsonObjects(_items);
                await _dbService.DeleteObjectAsync(itemToBeDeleted);
            }

            return itemToBeDeleted;
        }

        public List<Item> GetItems() { return _items; }

        //public IEnumerable<Item> NameSearch(string str)
        //{
        //    List<Item> nameSearch = new List<Item>();
        //    foreach (Item item in _items)
        //    {
        //        if (string.IsNullOrEmpty(str) || item.Name.ToLower().Contains(str.ToLower()))
        //        {
        //            nameSearch.Add(item);
        //        }
        //    }

        //    return nameSearch;
        //}

        public IEnumerable<Item> NameSearch(string str)
        {
            if (string.IsNullOrEmpty(str)) return _items;
            return from item in _items where item.Name.ToLower().Contains(str.ToLower()) select item;
        }

        //public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        //{
        //    List<Item> filterList = new List<Item>();
        //    foreach (Item item in _items)
        //    {
        //        if ((minPrice == 0 && item.Price <= maxPrice) || (maxPrice == 0 && item.Price >= minPrice) || (item.Price >= minPrice && item.Price <= maxPrice))
        //        {
        //            filterList.Add(item);
        //        }
        //    }

        //    return filterList;
        //}

        public IEnumerable<Item> PriceFilter(int maxPrice, int minPrice = 0)
        {
            return from item in _items
                   where (minPrice == 0 && item.Price <= maxPrice) ||
                     (maxPrice == 0 && item.Price >= minPrice) ||
                     (item.Price >= minPrice && item.Price <= maxPrice)
                   select item;
        }


        //public IEnumerable<Item> SortById()
        //{
        //    _items.Sort();
        //    return _items;
        //}

        //public IEnumerable<Item> SortByIdDescending()
        //{
        //    _items.Sort();
        //    return _items.Reverse<Item>();
        //}

        public IEnumerable<Item> SortById()
        {
            return from item in _items
                   orderby item.Id
                   select item;
        }

        public IEnumerable<Item> SortByIdDescending()
        {
            return from item in _items
                   orderby item.Id descending
                   select item;
        }


        //public IEnumerable<Item> SortByName()
        //{
        //    _items.Sort(new NameComperator());
        //    return _items;
        //}

        //public IEnumerable<Item> SortByNameDescending()
        //{
        //    _items.Sort(new NameComperator());
        //    return _items.Reverse<Item>();
        //}

        public IEnumerable<Item> SortByName()
        {
            return from item in _items
                   orderby item.Name
                   select item;
        }

        public IEnumerable<Item> SortByNameDescending()
        {
            return from item in _items
                   orderby item.Name descending
                   select item;
        }

        //public IEnumerable<Item> SortByPrice()
        //{
        //    _items.Sort(new PriceComperator());
        //    return _items;
        //}

        //public IEnumerable<Item> SortByPriceDescending()
        //{
        //    _items.Sort(new PriceComperator());
        //    return _items.Reverse<Item>();
        //}

        public IEnumerable<Item> SortByPrice()
        {
            return from item in _items
                   orderby item.Price
                   select item;
        }

        public IEnumerable<Item> SortByPriceDescending()
        {
            return from item in _items
                   orderby item.Price descending
                   select item;
        }
    }
}
