using Core.Database.AppEntities;
using Core.Database.Entities;
using Core.Security;

namespace SQLiteRepository
{
	public class DefaultEntitiesProvider
	{
		#region Default GUIDs
		public static Guid DefaultEstablishmentId = Guid.Parse("6F6E41F9-4956-44E5-81F8-63DA4A342670");
		public static Guid DefaultDepartmentId_1 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A1B");
		public static Guid DefaultDepartmentId_2 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A1C");
		public static Guid DefaultDepartmentId_3 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A1D");
		public static Guid DefaultDepartmentId_4 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A1E");
		public static Guid DefaultDepartmentId_5 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A1F");
		public static Guid DefaultDepartmentId_6 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A2F");
		public static Guid DefaultDepartmentId_7 = Guid.Parse("1259C949-A6FE-4F83-8F61-54C1785A7A3F");

		public static Guid DefaultStatusActiveId = Guid.Parse("92F64502-F25A-4B87-A581-8CE351701E53");
		public static Guid DefaultStatusDeactivatedId = Guid.Parse("92F64502-F25A-4B87-A581-8CE351701E54");

		public static Guid DefaultFunctionGroupId_1 = Guid.Parse("8B5B6F4B-BBBD-436C-B7F9-F4D733E10C44");
		public static Guid DefaultFunctionGroupId_2 = Guid.Parse("8B5B6F4B-BBBD-436C-B7F9-F4D733E10C45");

		public static Guid DefaultFunctionId_1 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869C8F");
		public static Guid DefaultFunctionId_2 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869C9F");
		public static Guid DefaultFunctionId_3 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CAF");
		public static Guid DefaultFunctionId_4 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CAA");
		public static Guid DefaultFunctionId_5 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CAB");
		public static Guid DefaultFunctionId_6 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CAC");
		public static Guid DefaultFunctionId_7 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CAD");
		public static Guid DefaultFunctionId_8 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CBA");
		public static Guid DefaultFunctionId_9 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CBB");
		public static Guid DefaultFunctionId_10 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CBC");
		public static Guid DefaultFunctionId_11 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CBD");
		public static Guid DefaultFunctionId_12 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CBF");
		public static Guid DefaultFunctionId_13 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCA");
		public static Guid DefaultFunctionId_14 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCB");
		public static Guid DefaultFunctionId_15 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCC");
		public static Guid DefaultFunctionId_16 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCD");
		public static Guid DefaultFunctionId_17 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCE");
		public static Guid DefaultFunctionId_18 = Guid.Parse("E0173B7E-1872-4021-9D5D-F1C257869CCF");

		public static Guid DefaultUserAccountId = Guid.Parse("36923D85-8B7C-4101-AC8B-39F636D3E280");
		public static Guid DefaultUserAccountId_2 = Guid.Parse("36923D85-8B7C-4101-AC8B-39F636D3E281");
		public static Guid DefaultUserAccountId_3 = Guid.Parse("36923D85-8B7C-4101-AC8B-39F636D3E282");

		public static Guid DefaultEmployeeId_1 = Guid.Parse("D2A6150F-5B08-4F09-8A65-15108F7A8DA1");
		public static Guid DefaultEmployeeId_2 = Guid.Parse("D2A6150F-5B08-4F09-8A65-15108F7A8DA2");
		public static Guid DefaultEmployeeId_3 = Guid.Parse("D2A6150F-5B08-4F09-8A65-15108F7A8DA3");

		public static Guid DefaultTimeIntervalTypeId_1 = Guid.Parse("E055AE0A-753A-427E-AD05-AF2EE6682B6F");
		public static Guid DefaultTimeIntervalTypeId_2 = Guid.Parse("E055AE0A-753A-427E-AD05-AF3EE6682B6F");
		public static Guid DefaultTimeIntervalTypeId_3 = Guid.Parse("E055AE0A-753A-427E-AD05-AF4EE6682B6F");
		public static Guid DefaultTimeIntervalTypeId_4 = Guid.Parse("E055AE0A-753A-427E-AD05-AF5EE6682B6F");
		public static Guid DefaultTimeIntervalTypeId_5 = Guid.Parse("E055AE0A-753A-427E-AD05-AF6EE6682B6F");
		public static Guid DefaultTimeIntervalTypeId_6 = Guid.Parse("E055AE0A-753A-427E-AD05-AF7EE6682B6F");
		#endregion

		#region GetDefaultEmployees
		public static IList<EmployeeEntity> GetDefaultEmployees()
		{
			return new List<EmployeeEntity>()
			{
				new EmployeeEntity(){
					Id = DefaultEmployeeId_1,
					FirstName = "Имя 1",
					LastName = "Фамилия 1",
					MiddleName = "Отчество 1",
					Email = "user1@user.ru",
					Rate = 1f,
					DepartmentId = DefaultDepartmentId_1,
					StatusId = DefaultStatusActiveId,
					FunctionId = DefaultFunctionId_1,
					IsInternalConcurrent = false
				},
				new EmployeeEntity(){
					Id = DefaultEmployeeId_2,
					FirstName = "Имя 2",
					LastName = "Фамилия 2",
					MiddleName = "Отчество 2",
					Email = "user2@user.ru",
					Rate = 0.5f,
					DepartmentId = DefaultDepartmentId_2,
					StatusId = DefaultStatusActiveId,
					FunctionId = DefaultFunctionId_2,
					IsInternalConcurrent = false
				},
				new EmployeeEntity(){
					Id = DefaultEmployeeId_3,
					FirstName = "Name",
					LastName = "Last name",
					MiddleName = "Middle name",
					Email = "concurrent@user.ru",
					Rate = 0.2f,
					DepartmentId = DefaultDepartmentId_3,
					StatusId = DefaultStatusActiveId,
					FunctionId = DefaultFunctionId_3,
					IsInternalConcurrent = true
				},
			};
		}
		#endregion

		#region GetDefaultEstablishments
		public static IList<EstablishmentEntity> GetDefaultEstablishments()
		{
			return new List<EstablishmentEntity>()
			{
				new EstablishmentEntity()
				{
					Id = DefaultEstablishmentId,
					Name = "Институт промышленной экологии ИПЭ УрО РАН",
					ShortName = "ИПЭ УрО РАН",
					Address = "620108, Свердловская область, город Екатеринбург, Софьи Ковалевской ул., д.20",
					INN = "6660001481",
					OGRN = "1026604959370",
					RegistrationDate = "19.12.1992",
				}
			};
		}
		#endregion

		#region GetDefaultDepartments
		public static IList<DepartmentEntity> GetDefaultDepartments()
		{
			return new List<DepartmentEntity>()
			{
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_1,
					Name = "Лаборатория урбанизированной среды",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_2,
					Name = "Радиационная лаборатория",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_3,
					Name = "Лаборатория математического моделирования",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_4,
					Name = "Лаборатория физики и экологии",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_5,
					Name = "Лаборатория эколого-климатических проблем Арктики",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_6,
					Name = "Химико-аналитический центр",
					EstablishmentId = DefaultEstablishmentId,
				},
				new DepartmentEntity()
				{
					Id = DefaultDepartmentId_7,
					Name = "ЦКП Арктических исследований",
					EstablishmentId = DefaultEstablishmentId,
				}

			};
		}
		#endregion

		#region GetDefaultUserAccounts
		public static IList<UserAccountEntity> GetDefaultUserAccounts()
		{
			return new List<UserAccountEntity>
			{
				new UserAccountEntity() {
					Id = DefaultUserAccountId,
					UserName = "Администратор",
					Login = "admin",
					Email = "admin@admin.org",
					PasswordHash = "",
					Roles = new string[] {RolesDefault.ADMINISTRATOR },
					EstablishmentId = DefaultEstablishmentId
				},
				new UserAccountEntity() {
					Id = DefaultUserAccountId_2,
					UserName = "Модератор",
					Login = "moder",
					Email = "",
					PasswordHash = "",
					Roles = new string[] {RolesDefault.MODERATOR },
					DepartmentId = DefaultUserAccountId_2,
					EstablishmentId = DefaultEstablishmentId
				},
				new UserAccountEntity() {
					Id = DefaultUserAccountId_3,
					UserName = "Обычный Пользователь",
					Login = "user",
					Email = "",
					PasswordHash = "",
					Roles = new string[] {RolesDefault.USER },
					DepartmentId = DefaultDepartmentId_1,
					EstablishmentId = DefaultEstablishmentId
				}
			};
		}
		#endregion

		#region GetDefaultFunctions
		public static IList<FunctionEntity> GetDefaultFunctions()
		{
			return new List<FunctionEntity> { 
                //Административные должности
                new FunctionEntity(){ Id = DefaultFunctionId_1, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Директор", ShortName = "дир."},
				new FunctionEntity(){ Id = DefaultFunctionId_2, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Заместитель директора", ShortName = "зам.дир."},
				new FunctionEntity(){ Id = DefaultFunctionId_3, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Исполняющий обязанности директора", ShortName = "и.о.дир."},
				new FunctionEntity(){ Id = DefaultFunctionId_4, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Помощник директора", ShortName = "пом.дир."},
				new FunctionEntity(){ Id = DefaultFunctionId_5, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Ученый секретарь", ShortName = "уч.сек."},
				new FunctionEntity(){ Id = DefaultFunctionId_6, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Заведующий лабораторией", ShortName = "зав.лаб."},
				new FunctionEntity(){ Id = DefaultFunctionId_7, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Заместитель заведующего лабораторией", ShortName = "зам.зав.лаб."},
				new FunctionEntity(){ Id = DefaultFunctionId_8, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Исполняющий обязанности заведующего лабораторией", ShortName = "и.о.зав.лаб."},
				new FunctionEntity(){ Id = DefaultFunctionId_9, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Ведущий специалист по кадрам", ShortName = "вед.спец.кадр."},
				new FunctionEntity(){ Id = DefaultFunctionId_10, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Специалист по кадрам", ShortName = "спец.кадр."},
				new FunctionEntity(){ Id = DefaultFunctionId_11, FunctionGroupId = DefaultFunctionGroupId_1, Name = "Сотрудник охраны", ShortName = "сот.охр."},
                //Научные должности
                new FunctionEntity(){ Id = DefaultFunctionId_12, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Инженер", ShortName = "инж."},
				new FunctionEntity(){ Id = DefaultFunctionId_13, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Инженер-исследователь", ShortName = "инж.-иссл."},
				new FunctionEntity(){ Id = DefaultFunctionId_14, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Младший научный сотрудник", ShortName = "м.н.с"},
				new FunctionEntity(){ Id = DefaultFunctionId_15, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Научный сотрудник", ShortName = "н.с."},
				new FunctionEntity(){ Id = DefaultFunctionId_16, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Старший научный сотрудник", ShortName = "с.н.с."},
				new FunctionEntity(){ Id = DefaultFunctionId_17, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Ведущий научный сотрудник", ShortName = "в.н.с."},
				new FunctionEntity(){ Id = DefaultFunctionId_18, FunctionGroupId = DefaultFunctionGroupId_2, Name = "Главный научный сотрудник", ShortName = "г.н.с."},
			};
		}
		#endregion

		#region GetDefaultFunctionGroups
		public static IList<FunctionGroupEntity> GetDefaultFunctionGroups()
		{
			return new List<FunctionGroupEntity>()
			{
				new FunctionGroupEntity()
				{
					Id = DefaultFunctionGroupId_1,
					GroupName = "Административные должности"
				},
				new FunctionGroupEntity()
				{
					Id = DefaultFunctionGroupId_2,
					GroupName = "Научные должности"
				},
			};
		}
		#endregion

		#region GetDefaultEmployeeStatuses
		public static IList<EmployeeStatusEntity> GetDefaultEmployeeStatuses()
		{
			return new List<EmployeeStatusEntity>()
			{
				new EmployeeStatusEntity() {
					Id = DefaultStatusActiveId,
					Name = "Активный"
				},
				new EmployeeStatusEntity() {
					Id = DefaultStatusDeactivatedId,
					Name = "Неактивен"
				}
			};
		}
		#endregion

		#region GetDefaultTimeIntervalTypes
		public static IList<TimeIntervalTypeEntity> GetDefaultTimeIntervalTypes()
		{
			return new List<TimeIntervalTypeEntity>()
			{
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_1, Name = "Командировка", ShortName = "К", Description = ""},
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_2, Name = "Отпуск", ShortName = "О", Description = ""},
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_3, Name = "Отпуск без содержания", ShortName = "Б/С", Description = ""},
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_4, Name = "Больничный", ShortName = "Б", Description = ""},
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_5, Name = "Учебный отпуск", ShortName = "УО", Description = ""},
				new TimeIntervalTypeEntity(){ Id = DefaultTimeIntervalTypeId_6, Name = "Прочие неявки", ShortName = "ПН", Description = ""},
			};
		} 
		#endregion
	}
}
