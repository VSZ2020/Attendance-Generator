using AG.Data;
using Microsoft.EntityFrameworkCore;

namespace AG.Services.Repository
{
    public class UserService
    {
        public UserService(DataContext ctx)
        {
            _context = ctx;
        }

        readonly DataContext _context;

        public async Task<bool> CheckEmployeeVacantedAsync(Guid? employeeId, Guid? userId)
        {
            if (!employeeId.HasValue)
                return true;

            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == employeeId && (!e.UserId.HasValue || (e.UserId.HasValue && e.UserId == userId))) != null;
        }
    }
}
