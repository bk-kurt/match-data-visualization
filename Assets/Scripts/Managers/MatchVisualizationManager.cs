using System;
using System.Collections.Generic;
using DataModels;
using DefaultNamespace;
using GamePlay;
using GamePlay.Controllers;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
        private readonly Dictionary<int, Person> _activePersons = new Dictionary<int, Person>();
        private Ball _instantiatedBall;
        
        private VisualizationAssetsConfigSo _visualizationAssetsConfigSo;
        private VisualElementFactory _visualElementFactory;

        protected void OnEnable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged += SetGameAssetConfiguration;
            MatchStateManager.Instance.OnFrameDataChanged += UpdateVisualStateFromFrameData;
            SetGameAssetConfiguration(ConfigurationManager.Instance.GetCurrentConfiguration());
        }

        public void SetGameAssetConfiguration(VisualizationAssetsConfigSo config)
        {
            _visualizationAssetsConfigSo = config;
            _visualElementFactory = new VisualElementFactory(_visualizationAssetsConfigSo);
            
            RefreshVisualStateFromConfigData();
        }
        
        private void RefreshVisualStateFromConfigData()
        {
            try
            {
                foreach (var person in _activePersons.Values)
                {
                    if (person != null)
                    {
                        person.RefreshConfig(_visualizationAssetsConfigSo);
                    }
                }
                if (_instantiatedBall != null)
                {
                    _instantiatedBall.RefreshConfig(_visualizationAssetsConfigSo);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to refresh config state: {ex.Message}");
            }
        }

        private void UpdateVisualStateFromFrameData(FrameData frameData)
        {
            UpdatePersonsState(frameData.Persons);
            UpdateBallState(frameData.Ball);
        }

        // selective updates
        private void UpdatePersonsState(List<PersonData> personsData)
        {
            var updatedIds = new HashSet<int>();
            foreach (var personData in personsData)
            {
                updatedIds.Add(personData.Id);
                UpdateOrCreatePerson(personData);
            }

            RemoveInactivePersons(updatedIds);
        }

        private void UpdateOrCreatePerson(PersonData personData)
        {
            if (!_activePersons.TryGetValue(personData.Id, out var person))
            {
                person = _visualElementFactory.CreatePerson(personData);
                _activePersons[personData.Id] = person;
            }

            person.UpdateState(personData);
        }

        private void RemoveInactivePersons(HashSet<int> updatedIds)
        {
            foreach (var id in new List<int>(_activePersons.Keys))
            {
                if (!updatedIds.Contains(id))
                {
                    Destroy(_activePersons[id].gameObject);
                    _activePersons.Remove(id);
                }
            }
        }

        private void UpdateBallState(BallData ballData)
        {
            if (_instantiatedBall == null)
            {
                _instantiatedBall = _visualElementFactory.CreateBall(ballData);
                CameraController.Instance.SetTarget(_instantiatedBall.gameObject.transform);
            }
            else
            {
                _instantiatedBall.UpdateState(ballData);
            }
        }


        private void OnDisable()
        {
            MatchStateManager.Instance.OnFrameDataChanged -= UpdateVisualStateFromFrameData;
            ConfigurationManager.Instance.OnConfigurationChanged -= SetGameAssetConfiguration;
        }
    }
}