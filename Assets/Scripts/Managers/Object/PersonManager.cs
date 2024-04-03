using System.Collections.Generic;
using DataModels;
using GamePlay.Factory;
using Utilities;

namespace Managers.Object
{
    public class PersonManager : MonoSingleton<PersonManager>
    {
        private readonly Dictionary<int, Person> _activePersons = new();
        private VisualElementFactory _visualElementFactory;

        public void SetVisualElementFactory(VisualElementFactory factory)
        {
            _visualElementFactory = factory;
        }
        public void UpdatePersonsState(List<PersonData> personsData)
        {
            var updatedIds = new HashSet<int>();
            foreach (var personData in personsData)
            {
                updatedIds.Add(personData.Id);
                UpdateOrCreatePerson(personData);
            }
            RemoveInactivePersons(updatedIds);
        }

        public void UpdateOrCreatePerson(PersonData personData)
        {
            if (!_activePersons.TryGetValue(personData.Id, out var person))
            {
                person = _visualElementFactory.CreatePerson(personData);
                _activePersons.Add(personData.Id, person);
            }
            person.UpdateState(personData);
        }

        public void RemoveInactivePersons(HashSet<int> updatedIds)
        {
            var idsToRemove = new List<int>();
            foreach (var id in _activePersons.Keys)
            {
                if (!updatedIds.Contains(id))
                {
                    Destroy(_activePersons[id].gameObject);
                    idsToRemove.Add(id);
                }
            }
            foreach (var id in idsToRemove)
            {
                _activePersons.Remove(id);
            }
        }

        public void ClearAllPersons()
        {
            foreach (var person in _activePersons.Values)
            {
                Destroy(person.gameObject);
            }
            _activePersons.Clear();
        }
    }
}