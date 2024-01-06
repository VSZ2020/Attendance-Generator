using Core.Calendar;
using Core.Converters;
using Core.Database.Entities;
using Services.Database;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Services.Domains
{
	public class CorrectionDay : BaseDomain, IEntityConverter<CorrectionDayEntity>, INotifyPropertyChanged
	{
		public CorrectionDay(DateTime date, DayType type, Guid establishmentId) 
		{
			this.Id = default;
			this.date = date;
			this.Type = type;
			this.EstablishmentId = establishmentId;
		}

		public CorrectionDay(CorrectionDayEntity entity)
		{
			this.Id = entity.Id;
			this.Date = entity.Date;
			this.Type = entity.Type;
			this.EstablishmentId = entity.EstablishmentId;
		}

		private DateTime date;
		private DayType type;

		public DateTime Date { get => date; set { date = value; OnChanged(); } }
		public DayType Type { get => type; set { type = value; OnChanged(); OnChanged(nameof(DayTypeName)); } }
		public string DayTypeName { get => DepartmentServiceUtils.DayTypeNameTranslator(type); }
		public Guid EstablishmentId { get; set; }


		#region IEntityConverter
		public CorrectionDayEntity ConvertToEntity()
		{
			return new CorrectionDayEntity()
			{
				Id = this.Id,
				Date = this.Date,
				Type = this.Type,
				EstablishmentId = this.EstablishmentId,
			};
		} 
		#endregion

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnChanged([CallerMemberName]string? name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
