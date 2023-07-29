using Services.Infrastructure.Configuration.Configs;

namespace Services.Infrastructure.Configuration
{
	public class ConfigManager
	{
		#region ctor
		public ConfigManager() {
			Register<WorkingWeekConfig>();
			Register<ReportViewerConfig>();
		}
		#endregion

		#region fields
		private static readonly Dictionary<Type, IConfig> _configs = new();
		#endregion fields

		#region properties

		#endregion properties

		#region Instance
		private static ConfigManager? instance;
		public static ConfigManager Instance { 
			get
			{
				if (instance == null)
					instance = new ConfigManager();
				return instance;
			}
		}
		#endregion

		public void Register<TConfig>() where TConfig : IConfig, new()
		{
			var config = new TConfig();
			_configs.Add(typeof(TConfig), config);
		}

		/// <summary>
		/// Получить конфигурацию
		/// </summary>
		/// <typeparam name="TConfig">Класс конфигурации, наследуемый от <seealso cref="IConfig"/></typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public TConfig Get<TConfig>() where TConfig: IConfig
		{
			if (_configs[typeof(TConfig)] is not TConfig config)
				throw new ArgumentException($"Отсутствует конфигурация с названием {nameof(config)}");
			return config;
		}

		/// <summary>
		/// Обновляет конфигурацию
		/// </summary>
		/// <typeparam name="TConfig"></typeparam>
		/// <param name="configToUpdate"></param>
		public void Update<TConfig>(TConfig configToUpdate) where TConfig: IConfig
		{
			_configs[typeof(TConfig)] = configToUpdate;
		}

		/// <summary>
		/// Обновляет все конфигурации, указанные в списке
		/// </summary>
		/// <param name="configs"></param>
		public void Update(IList<IConfig> configs)
		{
			foreach (IConfig config in configs)
			{
				Update(config);
			}
		}
	}
}
