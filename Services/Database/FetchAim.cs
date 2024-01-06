namespace Services.Database
{
	public enum FetchAim
	{
		/// <summary>
		/// Цель по-умолчанию
		/// </summary>
		None,

		/// <summary>
		/// Для списка объектов
		/// </summary>
		Index,

		/// <summary>
		/// Для представления в виде таблицы
		/// </summary>
		Table,

		/// <summary>
		/// Наиболее полное предтставление объекта
		/// </summary>
		Card
	}
}
