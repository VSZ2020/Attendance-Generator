using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class FunctionEntity: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }



        public ICollection<EmployeeToFunction> EmployeeToFunctionTable { get; set; }
    }
}
