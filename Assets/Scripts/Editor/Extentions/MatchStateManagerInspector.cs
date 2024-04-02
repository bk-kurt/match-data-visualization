using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor.Extentions
{
    [CustomEditor(typeof(MatchStateManager))]
    public class MatchStateManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            MatchStateManager matchStateManager = (MatchStateManager)target;

            DrawDefaultInspector();

            int maxFrameIndex = 0;
            if (MatchDataManager.Instance != null && MatchDataManager.Instance.frameDataStorage.frameDataList != null)
            {
                // determine the slider's max value
                maxFrameIndex = Mathf.Max(0, MatchDataManager.Instance.frameDataStorage.frameDataList.Count - 1);
            }

            EditorGUI.BeginChangeCheck();
            int newFrameIndex = EditorGUILayout.IntSlider("Current Frame Index",
                matchStateManager.GetCurrentFrameIndex(), 0, maxFrameIndex);
            if (EditorGUI.EndChangeCheck())
            {
                matchStateManager.SetCurrentFrameIndex(newFrameIndex);
                EditorUtility.SetDirty(target);
            }
        }
    }
}