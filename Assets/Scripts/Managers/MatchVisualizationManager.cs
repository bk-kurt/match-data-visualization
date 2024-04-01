using System.Collections.Generic;
using DataModels;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
        private void OnEnable()
        {
            MatchStateManager.Instance.OnFrameDataChanged += UpdateVisualStateFromFrameData;
        }

        private void UpdateVisualStateFromFrameData(FrameData frameData)
        {
            UpdatePersonsState(frameData.Persons);
            UpdateBallState(frameData.Ball);
        }

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
        }

        private void RemoveInactivePersons(HashSet<int> updatedIds)
        {
        }

        private void UpdateBallState(BallData ballData)
        {
        }

        
        private void OnDisable()
        {
            MatchStateManager.Instance.OnFrameDataChanged -= UpdateVisualStateFromFrameData;
        }
    }
}