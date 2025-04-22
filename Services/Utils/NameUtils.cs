using AG.Data.Entities;

namespace AG.Services.Utils
{
    public class NameUtils
    {
        public static string ToShortName(string lastName, string? firstName, string? middleName)
        {
            return $"{lastName}{(string.IsNullOrEmpty(firstName) ? "" : " " + firstName[0] + ".")}{(string.IsNullOrEmpty(middleName) ? "" : " " + middleName[0] + ".")}";
        }

        public static string ToLongName(string lastName, string? firstName, string? middleName) => string.Join(" ", lastName, firstName, middleName);


        public static string ToShortName(EmployeeEntity? entity)
        {
            return entity != null ? ToShortName(entity.LastName, entity.FirstName, entity.MiddleName) : "";
        }

        public static string ToLongName(EmployeeEntity? entity)
        {
            return entity != null ? ToLongName(entity.LastName, entity.FirstName, entity.MiddleName) : "";
        }
    }
}
