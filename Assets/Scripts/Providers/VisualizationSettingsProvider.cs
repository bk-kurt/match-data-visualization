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
                    //qqq21 I decided to not reference VisualizationSettings via one of managers or interfaces,
                    // because Once the settings object is loaded from the resources
                    // it's kept in memory and reused across the application
                    // its efficient way to access these settings without repetitively loading from disk
                    // and that is an advantage for a realtime visualizations that has features like interpolation.
                    _currentSettings = Resources.Load<VisualizationSettingsSo>($"Settings/VisualizationSettings");
                }

                return _currentSettings;
            }
        }
    }
}