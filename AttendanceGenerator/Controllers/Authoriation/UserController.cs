using AttendanceGenerator.Controllers.Database;
using AttendanceGenerator.Model.Session.UserAccount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Authoriation
{
    public static class UserController
    {
        public static UserAccount? AuthUser(string login, string password)
        {
            UserAccount? account;
            using (ApplicationDbContext dbC = ApplicationDbContext.GetContext())
            {
                account = dbC.Accounts.Where(a => a.Login == login && a.Password == password).Include(acc => acc.UserRole).Include(acc => acc.Departments).FirstOrDefault();
            }
            return account;
        }

        public static void AddUserAccount(UserAccount user)
        {
            using (ApplicationDbContext dbC = ApplicationDbContext.GetContext())
            {
                dbC.Entry(user).State = EntityState.Added;
                //dbC.Accounts.Add(user);
                dbC.SaveChanges();
            }
        }

        public static void UpdateUserAccount(UserAccount user)
        {
            using (ApplicationDbContext dbC = ApplicationDbContext.GetContext())
            {
                dbC.Accounts.Update(user);
                dbC.SaveChanges();
            }
        }

        public static void RemoveUserAccount(UserAccount user)
        {
            using (ApplicationDbContext dbC = ApplicationDbContext.GetContext())
            {
                if (dbC.Accounts.Contains(user))
                {
                    dbC.Accounts.Remove(user);
                    dbC.SaveChanges();
                }
            }
        }

        public static List<UserAccount> GetAll(int establishmentId = 0, int departmentId = 0)
        {
            List<UserAccount> accounts = new List<UserAccount>();
            using (ApplicationDbContext dbC = ApplicationDbContext.GetContext())
            {
                if (establishmentId == 0)
                {
                    accounts = dbC.Accounts.ToList();
                }
                else
                {
                    if (departmentId == 0)
                        accounts = dbC.Accounts.Where(a => a.EstablishmentId == establishmentId).ToList();
                    //else
                        //TODO
                }
            }
            return accounts;
        }
    }
}
