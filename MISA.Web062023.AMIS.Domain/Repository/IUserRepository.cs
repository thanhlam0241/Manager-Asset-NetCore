using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IUserRepository
    {
        public Task<int> CreateUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);

        public Task<int> DeleteUserAsync(Guid userId);

        public Task<User> FindUserAsync(string userCredential);
    }
}
