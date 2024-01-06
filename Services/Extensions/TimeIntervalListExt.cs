using Services.Domains;

namespace Services.Extensions
{
	public static class TimeIntervalListExt
	{
		/// <summary>
		/// Проверяет, находится ли указанное число внутри одного из временных интервалов
		/// </summary>
		/// <param name="list"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public static bool ContainsInterval(this IList<TimeInterval> list, DateTime date)
		{
			
			for(int i = 0; i < list.Count; i++)
			{
				if (list[i].Begin <= date && date <= list[i].End)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Возвращает временной интервал, внутри которого находится указанная дата. Если дата не содержится ни в одном временном интервале из списка, то возвращается null
		/// </summary>
		/// <param name="list">Список проверяемых временных интервалов</param>
		/// <param name="date">Проверяемая дата</param>
		/// <returns></returns>
		public static TimeInterval? InsideInterval(this IList<TimeInterval> list, DateTime date)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Begin <= date && date <= list[i].End)
					return list[i];
			}
			return null;
		}

		public static bool HasCollisionWith(this IEnumerable<TimeInterval> list1, IEnumerable<TimeInterval> list2)
		{
			foreach(var timeInterval1 in list1)
			{
				foreach (var timeInterval2 in list2)
				{
					if (timeInterval1.HasCollisionWith(timeInterval2))
						return true;
				}
			}
			return false;
		}

		public static ICollection<(TimeInterval TI1, TimeInterval TI2)> GetCollisionsWith(this IEnumerable<TimeInterval> list1, IEnumerable<TimeInterval> list2)
		{
			var collisionsList = new List<(TimeInterval, TimeInterval)>();
			foreach (var timeInterval1 in list1)
			{
				foreach (var timeInterval2 in list2)
				{
					if (timeInterval1.HasCollisionWith(timeInterval2))
						collisionsList.Add((timeInterval1, timeInterval2));
				}
			}
			return collisionsList;
		}

		public static ICollection<TimeInterval> GetCollisionsWith(this IEnumerable<TimeInterval> list1, TimeInterval timeInterval)
		{
			var collisionsList = new List<TimeInterval>();
			foreach (var ti in list1)
			{
				if (timeInterval.HasCollisionWith(ti))
					collisionsList.Add(ti);
			}
			return collisionsList;
		}

		/// <summary>
		/// Проверяет пересечение двух временных интервалов
		/// </summary>
		/// <param name="TI1"></param>
		/// <param name="TI2"></param>
		/// <returns></returns>
		public static bool HasCollisionWith(this TimeInterval TI1, TimeInterval TI2)
		{
			//bool leftT1EdgeIntercept = TI2.Begin.Date <= TI1.Begin.Date && TI1.Begin.Date <= TI2.End.Date;
			//bool rightT1EdgeIntercept = TI2.Begin.Date <= TI1.End.Date && TI1.End.Date <= TI2.End.Date;

			//bool leftT2EdgeIntercept = TI1.Begin.Date <= TI2.Begin.Date && TI2.Begin.Date <= TI1.End.Date;
			//bool rightT2EdgeIntercept = TI1.Begin.Date <= TI2.End.Date && TI2.End.Date <= TI1.End.Date;
			//return leftT1EdgeIntercept || rightT1EdgeIntercept || leftT2EdgeIntercept || rightT2EdgeIntercept;
			return !(TI1.Begin > TI2.End || TI2.Begin > TI1.End);
		}
	}
}
