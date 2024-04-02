using System.Collections.Generic;
using DataModels;
using DefaultNamespace;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
        [SerializeField] private VisualizationAssetsConfigSo visualizationAssetsConfigSo;

        private readonly Dictionary<int, Person> _activePersons = new();
        private Ball _instantiatedBall;

        private void OnEnable()
        {
            MatchStateManager.Instance.OnFrameDataChanged += UpdateVisualStateFromFrameData;
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
                var visualElementFactory = new VisualElementFactory(visualizationAssetsConfigSo);
                person = visualElementFactory.CreatePerson(personData);
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
                var visualElementFactory = new VisualElementFactory(visualizationAssetsConfigSo);
                _instantiatedBall = visualElementFactory.CreateBall(ballData);
            }
            else
            {
                _instantiatedBall.UpdateState(ballData);
            }
        }


        private void OnDisable()
        {
            MatchStateManager.Instance.OnFrameDataChanged -= UpdateVisualStateFromFrameData;
        }
    }
}