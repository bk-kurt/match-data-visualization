using System.Threading.Tasks;
using Managers.Data;
using Managers.State;
using Scriptables.Data;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class MatchStateEditorWindow : EditorWindow
    {
        private MatchStateManager _matchStateManager;
        private MatchDataLoader _matchDataLoader;
        private FrameDataStorage _frameDataStorage;
        private int _currentFrameIndex;
        private int _maxFrameIndex;
        private float _playbackSpeed;
        private static bool _hasInitialized;

        [MenuItem("Tools/Match State Control")]
        private static async void Init()
        {
            var window =
                GetWindow();
            await Task.Delay(500);
            window.SetUpDependencies();
            window.Show();

            _hasInitialized = true;
        }

        private static MatchStateEditorWindow GetWindow()
        {
            return (MatchStateEditorWindow)GetWindow(typeof(MatchStateEditorWindow), false, "Match State Control",
                true);
        }

        private void OnGUI()
        {
            if (!_hasInitialized)
            {
                Init();
                return;
            }

            if (!HasDependenciesSet())
            {
                Debug.Log("Lost the Dependencies or not correctly set, reinitializing controller");
                _hasInitialized = false;
                return;
            }

            _maxFrameIndex = Mathf.Max(0, _matchDataLoader.GetFrameCount() - 1);
            _playbackSpeed = _matchStateManager.playbackSpeed;

            EditorGUI.BeginChangeCheck();
            DrawSliders();

            if (EditorGUI.EndChangeCheck())
            {
                _matchStateManager.SetCurrentFrameIndex(_currentFrameIndex);
                _matchStateManager.playbackSpeed = _playbackSpeed;
            }

            DrawButtons();

            _frameDataStorage = (FrameDataStorage)EditorGUILayout.ObjectField("Frame Data Storage", _frameDataStorage,
                typeof(FrameDataStorage), false);
        }


        private void OnFocus()
        {
            if (HasDependenciesSet())
            {
                return;
            }

            SetUpDependencies();
        }


        private void SetUpDependencies()
        {
            _matchStateManager = MatchStateManager.Instance;
            _matchDataLoader = MatchDataLoader.Instance;
            if (_matchDataLoader && _matchDataLoader.frameDataStorage)
            {
                _frameDataStorage = _matchDataLoader.frameDataStorage;
            }
        }

        private bool HasDependenciesSet()
        {
            return _matchDataLoader && _matchStateManager && _matchDataLoader.frameDataStorage;
        }

        private void DrawButtons()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (EditorApplication.isPlaying &&
                GUILayout.Button(_matchStateManager.IsPlaying ? "Pause" : "Play", GUILayout.Width(200)))
            {
                _matchStateManager.TogglePlayback(!_matchStateManager.IsPlaying);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (GUILayout.Button(!EditorApplication.isPlaying ? "Start visualization" : "End visualization"))
            {
                EditorApplication.isPlaying = !EditorApplication.isPlaying;
            }


            GUILayout.Space(10);
        }

        private void DrawSliders()
        {
            GUILayout.Space(10);

            _currentFrameIndex =
                EditorGUILayout.IntSlider("Current Frame Index", _matchStateManager.GetCurrentFrameIndex(), 0,
                    _maxFrameIndex);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            _playbackSpeed = EditorGUILayout.Slider("Playback Speed", _playbackSpeed, -3f, 3f, GUILayout.Width(400));

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
        
    }
}