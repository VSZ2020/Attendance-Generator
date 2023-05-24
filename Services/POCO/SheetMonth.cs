﻿namespace Services.POCO
{
	public class SheetMonth
	{
		public SheetMonth(int year, int month) { this.Year = year; this.Month = month; }

		private IList<SheetDay> Days = new List<SheetDay>();

		public int DaysCount => Days?.Count ?? 0;
		public int Year { get; set; }

		public int Month { get; set; }

		public SheetDay this[int id]
		{
			get => Days[id];
			set => Days[id] = value;
		}

		public void AddDay(SheetDay day) 
		{  
			Days.Add(day); 
		}
	}
}
