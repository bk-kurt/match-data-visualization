using Scriptables.Settings;
using UnityEngine;


namespace Providers
{
    public static class VisualizationSettingsProvider
    {
        private static VisualizationSettingsSo _currentSettings;

        public static VisualizationSettingsSo CurrentSettings
        {
            get
            {
                if (_currentSettings == null)
                {
                    // I decided to not reference this via one of managers, because
                    // Once the settings object is loaded from the resources
                    // it's kept in memory and reused across the application
                    // its efficient way to access these settings without repetitively loading from disk
                    _currentSettings = Resources.Load<VisualizationSettingsSo>($"Settings/VisualizationSettings");
                }

                return _currentSettings;
            }
        }
    }
}
