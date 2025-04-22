using AG.Data;
using AG.Data.Entities;
using AG.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace AG.Services.Repository
{
    public class AuthenticationService
    {
        public AuthenticationService(DataContext context)
        {
            _context = context;
        }

        readonly DataContext _context;

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Username == username);
            return entity != null;
        }

        public async Task<UserEntity?> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            var hash = HashUtils.HashPassword(password);
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Username == username && e.PasswordHash == hash);
            return entity;
        }
    }
}
