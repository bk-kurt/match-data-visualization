using Interfaces.Configuration;
using Managers.Configuration;
using Managers.Data;
using Providers;
using Scriptables.Configuration;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.Visualization.Extentions
{
    public static class VisualizationContextAction
    {
        [MenuItem("Assets/>> Visualize JSON", true, priority: 0)]
        private static bool ValidateOpenCustomLayout()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return !EditorApplication.isPlaying && (path.EndsWith(".json") || path.EndsWith(".JSON"));
        }

        [MenuItem("Assets/>> Visualize JSON", false, priority: 0)]
        private static void OpenCustomLayout()
        {
            if (IsSceneChangeRequired) return;
            HandFileLoading();
        }

        private static void HandFileLoading()
        {
            HandleFilePath();
            
            if (MatchDataLoader.Instance == null)
            {
                Debug.LogError("MatchDataManager instance not found.");
                return;
            }

            MatchDataLoader.Instance.LoadJsonDataAsync().ConfigureAwait(false);
            MatchDataLoader.Instance.OnDataLoadingComplete += HandleDataLoadingComplete;
        }

        private static bool IsSceneChangeRequired
        {
            get
            {
                string targetScenePath =
                    "Assets/Scenes/Visualization_Demo.unity"; // left as string for early development cycle

                if (!System.IO.File.Exists(targetScenePath))
                {
                    Debug.LogWarning("Scene file not found: " + targetScenePath + "please rename a scene");
                    return true;
                }

                string currentScenePath = SceneManager.GetActiveScene().path;
                if (!currentScenePath.Equals(targetScenePath, System.StringComparison.Ordinal))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(targetScenePath, OpenSceneMode.Single);
                    }
                    else
                    {
                        Debug.LogWarning("Scene switch was canceled by the user.");
                    }
                }

                return false;
            }
        }

        private static void HandleFilePath()
        {
            var currentPath = VisualizationSettingsProvider.CurrentSettings.DataPathConfigSo.GetJsonDataPath();
            string selectedJsonFilePath = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (currentPath != selectedJsonFilePath)
            {
                IDataPathConfigProvider dataPathConfigProvider = ScriptableObject.CreateInstance<DataPathConfigSo>();
                dataPathConfigProvider.SetJsonDataPath(selectedJsonFilePath);
                ConfigurationManager.Instance.SetDataPathConfiguration(dataPathConfigProvider);
            }
        }

        private static void HandleDataLoadingComplete()
        {
            MatchDataLoader.Instance.OnDataLoadingComplete -= HandleDataLoadingComplete;
            SwitchToFixedLayout();
        }

        private static void SwitchToFixedLayout()
        {
            string layoutPath = "Assets/Resources/Layouts/Visualization-02.wlt";
            if (System.IO.File.Exists(layoutPath))
            {
                EditorUtility.LoadWindowLayout(layoutPath);
                if (!EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = true;
                }
            }
            else
            {
                Debug.LogWarning("Layout file not found: " + layoutPath);
            }
        }
    }
}