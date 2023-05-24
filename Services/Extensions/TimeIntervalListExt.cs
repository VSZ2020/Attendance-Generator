using Services.POCO;

namespace Services.Extensions
{
	public static class TimeIntervalListExt
	{
		public static bool ContainsInterval(this IList<TimeInterval> list, DateTime date)
		{
			for(int i = 0; i < list.Count; i++)
			{
				if (list[i].Begin <= date && date <= list[i].End)
					return true;
			}
			return false;
		}

		public static TimeInterval? InsideInterval(this IList<TimeInterval> list, DateTime date)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Begin <= date && date <= list[i].End)
					return list[i];
			}
			return null;
		}
	}
}
