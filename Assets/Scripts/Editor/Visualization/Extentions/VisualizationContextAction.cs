using Managers;
using Managers.Data;
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
            // scene reference is left hard coded for now.
            string targetScenePath = "Assets/Scenes/Visualization_Demo.unity";

            if (!System.IO.File.Exists(targetScenePath))
            {
                Debug.LogWarning("Scene file not found: " + targetScenePath+"please rename a scene");
                return;
            }

            OpenSceneIfNeeded(targetScenePath);

            string jsonFilePath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (MatchDataLoader.Instance != null)
            {
                MatchDataLoader.Instance.OnDataLoadingComplete += HandleDataLoadingComplete;
                MatchDataLoader.Instance.LoadJsonDataAsync(jsonFilePath).ConfigureAwait(false);
            }
            else
            {
                Debug.LogError("MatchDataManager instance not found.");
            }
        }

        private static void OpenSceneIfNeeded(string targetScenePath)
        {
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
                    return;
                }
            }
        }

        private static void HandleDataLoadingComplete()
        {
            MatchDataLoader.Instance.OnDataLoadingComplete -= HandleDataLoadingComplete;

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