using ItemRazorV1.Models;

namespace ItemRazorV1.Service
{
    public class OrderService
    {
        public List<Order> OrderList { get; set; }

        public DbGenericService<Order> _dbService { get; set; }

        public OrderService(DbGenericService<Order> dbService)
        {
            _dbService = dbService;
            OrderList = _dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddOrderAsync(Order order)
        {
            OrderList.Add(order);
            await _dbService.AddObjectAsync(order);
        }
    }
}
