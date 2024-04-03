using Managers;
using Managers.Data;
using Managers.State;
using Scriptables.Data;
using UnityEditor;
using UnityEngine;

namespace Editor.Visualization
{
    public class MatchStateEditorWindow : EditorWindow
    {
        private MatchStateManager _matchStateManager;
        private MatchDataManager _matchDataManager;
        private FrameDataStorage _frameDataStorage;
        private int _currentFrameIndex;
        private int _maxFrameIndex;
        private float _playbackSpeed;

        [MenuItem("Tools/Match State Control")]
        private static void Init()
        {
            var window = (MatchStateEditorWindow)GetWindow(typeof(MatchStateEditorWindow));
            window.titleContent = new GUIContent("Match State Control");
            window.Show();
        }

        private void OnGUI()
        {
            if (_matchStateManager == null)
            {
                _matchStateManager = FindObjectOfType<MatchStateManager>();
            }

            if (_matchDataManager == null)
            {
                _matchDataManager = MatchDataManager.Instance;
            }

            if (_frameDataStorage == null)
            {
                _frameDataStorage = _matchDataManager.frameDataStorage;
            }

            if (MatchDataManager.Instance == null || MatchDataManager.Instance.frameDataStorage.frameDataList == null ||
                _matchStateManager == null)
            {
                EditorGUILayout.HelpBox("Match Data Manager or Match State Manager is not available.",
                    MessageType.Warning);
                return;
            }

            _maxFrameIndex = Mathf.Max(0, _matchDataManager.GetFrameCount() - 1);
            _playbackSpeed = _matchStateManager.playbackSpeed;

            EditorGUI.BeginChangeCheck();
            _currentFrameIndex =
                EditorGUILayout.IntSlider("Current Frame Index", _matchStateManager.GetCurrentFrameIndex(), 0, _maxFrameIndex);
            _playbackSpeed = EditorGUILayout.Slider("Playback Speed", _playbackSpeed, -3f, 3f);

            if (EditorGUI.EndChangeCheck())
            {
                _matchStateManager.SetCurrentFrameIndex(_currentFrameIndex);
                _matchStateManager.playbackSpeed = _playbackSpeed;
            }

            if (EditorApplication.isPlaying && GUILayout.Button(_matchStateManager.IsPlaying ? "Pause" : "Play"))
            {
                _matchStateManager.TogglePlayback(!_matchStateManager.IsPlaying);
            }

            if (GUILayout.Button(!EditorApplication.isPlaying ? "Start visualization" : "End visualization"))
            {
                EditorApplication.isPlaying = !EditorApplication.isPlaying;
            }

            _frameDataStorage = (FrameDataStorage)EditorGUILayout.ObjectField("Frame Data Storage", _frameDataStorage,
                typeof(FrameDataStorage), false);
        }

        private void OnFocus()
        {
            if (_matchStateManager == null)
            {
                _matchStateManager = FindObjectOfType<MatchStateManager>();
            }

            SceneView.RepaintAll();
        }
    }
}