using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository
{
	public abstract class BaseContext: DbContext
	{
		protected BaseContext() 
		{
			this.FullDatabasePath = Path.Combine(DatabasesPath, DatabaseName);
			ConnectionString = $"Data Source={FullDatabasePath}.db";

			CheckDatabaseExists();
		}

		public abstract string DatabaseName { get; }

		public static string DatabasesPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"AG", "Data");

		public readonly string FullDatabasePath;

		public readonly string ConnectionString;

		public void RecreateDb()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public void CheckDatabaseExists()
		{
			if (!Directory.Exists(DatabasesPath)) 
				Directory.CreateDirectory(DatabasesPath);

			if (!File.Exists(FullDatabasePath))
				Database.EnsureCreated();
		}
	}
}
