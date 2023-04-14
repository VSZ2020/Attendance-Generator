using AttendanceGenerator.Infrastructure.Logger;
using AttendanceGenerator.Model.Establishment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database
{
    public class EstablishmentController
    {
        public static void AddEstablishment(Establishment est)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (!context.Establishments.Contains(est))
                {
                    context.Establishments.Add(est);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1123", $"Организация с таким названием {est.Name} уже есть в базе");
                }
            }
        }

        public static void RemoveEstablishment(Establishment est)
        {
            //Внимание! При удалении организации удаляются все сотрудники, принадлежащие ей
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if(context.Establishments.Contains(est))
                {
                    context.Establishments.Remove(est);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1124", $"Организации {est.Name} (ID = {est.Id}) не существует");
                }
            }
        }

        public static void UpdateEstablishment(Establishment est)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Establishments.Update(est);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Получает список организаций, созданных в базе
        /// </summary>
        /// <returns></returns>
        public static List<Establishment> GetEstablishments()
        {
            List<Establishment>? ests;
            using (ApplicationDbContext ctx = ApplicationDbContext.GetContext())
            {
                ests = ctx.Establishments.Include(est=>est.Departments).ToList();
            }
            return ests != null ? ests : new List<Establishment>();
        }

        public static Establishment GetDefaultEstablishment()
        {
            return new Establishment()
            {
                Name = "Название организации"
            };
        }

    }
}
