using ItemRazorV1.DAO;
using ItemRazorV1.MockData;
using ItemRazorV1.Models;

namespace ItemRazorV1.Service
{
    public class UserService
    {
        public List<User> Users { get;}
        private JsonFileService<User> _jsonFileService;
        private UserDbService _dbService;

        public UserService(JsonFileService<User> jsonFileService, UserDbService dbService)
        {
            _jsonFileService = jsonFileService;
            _dbService = dbService;
            //Users = MockUsers.GetMockUsers();
            //Users = _jsonFileService.GetJsonObjects().ToList();
            //_jsonFileService.SaveJsonObjects(Users);
            //_dbService.SaveObjects(Users);
            Users = _dbService.GetObjectsAsync().Result.ToList();

        }

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            //_jsonFileService.SaveJsonObjects(Users);
            await _dbService.AddObjectAsync(user);
        }

        public User GetUserByUserName(string username)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Users.Find(user => user.UserName == username);
        }

        //public IEnumerable<OrderDAO> GetUserOrders(User user)
        //{
        //    return _dbService.GetOrdersByUserIdAsync(user.UserId).Result;
        //}

        public async Task<User> GetUserOrdersAsync(User user)
        {
            return await _dbService.GetOrdersByUserIdAsync(user.UserId);
        }

    }
}
