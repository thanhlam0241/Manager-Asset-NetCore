using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> FindUserAsync(string credential)
        {
            var user = await _userRepository.FindUserAsync(credential) ?? throw new NotFoundException(Domain.Resources.Authentication.Authentication.CredentialFail);
            return user;
        }
    }
}
