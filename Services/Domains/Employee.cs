using Core.Converters;
using Core.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace Services.Domains
{
	public class Employee : BaseDomain, IEntityConverter<EmployeeEntity>
	{
		#region ctor
		public Employee()
		{

		}

		public Employee(EmployeeEntity entity)
		{
			Id = entity.Id;
			FirstName = entity.FirstName;
			LastName = entity.LastName;
			MiddleName = entity.MiddleName;
			Rate = entity.Rate;
			FunctionId = entity.FunctionId;
			DepartmentId = entity.DepartmentId;
			IsConcurrent = entity.IsInternalConcurrent;
			StatusId = entity.StatusId;
			PhoneNumber = entity.PhoneNumber;
			Email = entity.Email;
		}

		#endregion
		[Display(Name = "Имя")]
		public string FirstName { get; set; } = "Имя";

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; } = "Отчество";

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = "Фамилия";

		public Guid FunctionId { get; set; }
		public EmployeeFunction? Function { get; set; }

        [Display(Name = "Должность")]
        public string FunctionName => Function?.Name ?? "Не указана";

        [Display(Name = "Доля ставки")]
		[Range(0.05, 1.0)]
        public float Rate { get; set; } = 1f;

        [Display(Name = "Внешний совместитель")]
        public bool IsConcurrent { get; set; }

		public Guid DepartmentId { get; set; }
		public Department? Department { get; set; }

        [Display(Name = "Подразделение")]
        public string DepartmentName => Department?.Name ?? "";

		public Guid StatusId { get; set; }

		public EmployeeStatus? Status { get; set; }

        [Display(Name = "Статус")]
        public string StatusName => Status?.Name ?? "";

        [Display(Name = "Номер телефона")]
        public string? PhoneNumber { get; set; } = "";

        [Display(Name = "Почтовый адрес")]
        public string? Email { get; set; } = "";

        [Display(Name = "Сокращенное имя")]
        public string ShortName => string.Join(" ", LastName, !string.IsNullOrEmpty(FirstName) ? FirstName[0] + ". " : "", !string.IsNullOrEmpty(MiddleName) ? MiddleName[0] + "." : "");

        [Display(Name = "ФИО")]
        public string FullName => string.Join(" ", LastName, FirstName, MiddleName);

		public EmployeeEntity ConvertToEntity()
		{
			return new EmployeeEntity()
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				MiddleName = this.MiddleName,
				Rate = this.Rate,
				IsInternalConcurrent = this.IsConcurrent,
				PhoneNumber = this.PhoneNumber,
				DepartmentId = this.DepartmentId,
				StatusId = this.StatusId,
				FunctionId = this.FunctionId,
				Email = this.Email,
			};
		}
	}
}
