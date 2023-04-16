using System;
using System.Collections.Generic;

namespace AttendanceGenerator.Infrastructure.Configuration
{
    public class ConfigManager
    {
        private Dictionary<Type, IConfig> _configs;

        public ConfigManager()
        {
            _configs = new Dictionary<Type, IConfig>();
            _configs.Add(typeof(DatabaseConfig), new DatabaseConfig());
            _configs.Add(typeof(UpdaterConfig), new UpdaterConfig());
        }

        public TConfig Get<TConfig>() where TConfig : class, IConfig
        {
            if (typeof(TConfig) is not TConfig config)
            {
                throw new ArgumentException($"Не найдена конфигурация с типом {typeof(TConfig)}");
            }
            return config;
        }
    }
}
