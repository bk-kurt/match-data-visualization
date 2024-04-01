using System.Collections.Generic;
using DataModels;
using DefaultNamespace;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
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
                
            }

            person.UpdateState(personData);
        }

        private void RemoveInactivePersons(HashSet<int> updatedIds)
        {
            
        }

        private void UpdateBallState(BallData ballData)
        {
            if (_instantiatedBall == null)
            {
                
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