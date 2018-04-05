using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDss.Infrastructure.Configuration
{
    public class SettingService
    {
        private readonly SettingRepository _settingRepository;

        public SettingService(SettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            Initialize();
        }

        private void Initialize()
        {
            var existingSettings = _settingRepository.GetAllSettings();
            var seedSettings = SettingSeed.Settings;

            foreach (var setting in seedSettings)
            {
                if (existingSettings.Any(x => x.Name == setting.Name))
                {
                    continue;
                }
                _settingRepository.AddSetting(setting.Name, setting.Value);
            }

            foreach (var existingSetting in existingSettings)
            {
                if (!seedSettings.Any(x => x.Name == existingSetting.Name))
                {
                    _settingRepository.RemoveSetting(existingSetting.Name);
                }
            }
        }

        internal string GetSettingValue(string settingName)
        {
            return _settingRepository.GetSettingValue(settingName);
        }

        internal async Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
            return await _settingRepository.GetAllSettingsAsync().ConfigureAwait(false);
        }

        internal async Task UpdateSettingAsync(Setting setting)
        {
            await _settingRepository.UpdateSettingAsync(setting).ConfigureAwait(false);
        }

        internal void UpdateSetting(Setting setting)
        {
            _settingRepository.UpdateSetting(setting);
        }
    }
}
