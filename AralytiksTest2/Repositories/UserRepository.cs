using AralytiksTest2.Contracts;
using AralytiksTest2.Models;

namespace AralytiksTest2.Repositories
{
    public class UserRepository : BaseRepository<int, User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {
            
        }
    }
}
