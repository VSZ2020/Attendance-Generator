using Core.Converters;
using Core.Database.Entities;

namespace Services.Domains
{
	public class Establishment : BaseDomain, IEntityConverter<EstablishmentEntity>
	{
		#region ctor
		public Establishment()
		{
		}

		public Establishment(EstablishmentEntity entity)
		{
			this.Id = entity.Id;
			this.Name = entity.Name;
			this.ShortName = entity.ShortName;
			this.Address = entity.Address;
			this.OGRN = entity.OGRN;
			this.INN = entity.INN;
			this.Phones = entity.Phones;
			this.Email = entity.Email;
			this.RegistrationDate = entity.RegistrationDate;
			this.Header = entity.Header;

		} 
		#endregion

		/// <summary>
		/// Название организации
		/// </summary>
		public string Name { get; set; } = "Организация без названия";

		/// <summary>
		/// Краткое название организации
		/// </summary>
		public string? ShortName { get; set; }

		/// <summary>
		/// ОГРН организации
		/// </summary>
		public string? OGRN { get; set; }
		/// <summary>
		/// ИНН организации
		/// </summary>
		public string? INN { get; set; }
		public string? RegistrationDate { get; set; }

		/// <summary>
		/// Юридический адрес организации
		/// </summary>
		public string? Address { get; set; }

		/// <summary>
		/// Электронный адрес почты организации
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Номера телефонов организации
		/// </summary>
		public string? Phones { get; set; }

		/// <summary>
		/// Руководитель организации
		/// </summary>
		public string? Header { get; set; }

		public IList<Department>? Departments { get; set; }


		public EstablishmentEntity ConvertToEntity()
		{
			return new EstablishmentEntity()
			{
				Id = this.Id,
				Name = this.Name,
				ShortName = this.ShortName,
				OGRN = this.OGRN,
				INN = this.INN,
				RegistrationDate = this.RegistrationDate,
				Address = this.Address,
				Email = this.Email,
				Phones = this.Phones,
				Header = this.Header,

			};
		}
	}
}
