using DataModels;
using GamePlay.Environment;
using Scriptables.Configuration;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Factory
{
    public static class PersonFactory
    {
        private static IVisualizationAssetConfigProvider _visualizationAssetsConfigProvider;

        public static void Initialize(IVisualizationAssetConfigProvider config)
        {
            _visualizationAssetsConfigProvider = config;
        }

        public static Person Create(PersonData personData)
        {
            if (personData == null)
            {
                Debug.LogError("PersonFactory: Person data is null.");
                return null;
            }

            if (_visualizationAssetsConfigProvider == null)
            {
                Debug.LogError("PersonFactory: Visualization assets config provider is null.");
                return null;
            }

            var assetsConfigProviderSo = _visualizationAssetsConfigProvider as VisualizationAssetsConfigProviderSo;
            if (assetsConfigProviderSo == null)
            {
                Debug.LogError("PersonFactory: Incorrect type for visualization assets config provider.");
                return null;
            }

            var personConfig = assetsConfigProviderSo.GetConfiguredPersonByTeamSide(personData.TeamSide);
            if (personConfig == null || personConfig.personPrefab == null)
            {
                Debug.LogError($"PersonFactory: Configuration or prefab is null for team side {personData.TeamSide}.");
                return null;
            }

            var instantiatedPerson = InstantiatePerson(personData, personConfig);
            if (instantiatedPerson == null)
            {
                return null; // Early return if person could not be instantiated correctly.
            }

            InitializePerson(instantiatedPerson, personData);
            return instantiatedPerson;
        }

        private static Person InstantiatePerson(PersonData personData, PersonConfigSo personConfig)
        {
            var parentTransform = EnvironmentSetUp.Instance.teamTransforms[personData.TeamSide];
            if (parentTransform == null)
            {
                Debug.LogError("PersonFactory: Parent transform is null.");
                return null;
            }

            var instantiatedPersonGo = Object.Instantiate(personConfig.personPrefab, personData.TargetPosition, Quaternion.identity, parentTransform);
            if (instantiatedPersonGo == null)
            {
                Debug.LogError("PersonFactory: Instantiation of person prefab failed.");
                return null;
            }
            
            var personComponent = instantiatedPersonGo.GetComponent<Person>() ?? instantiatedPersonGo.AddComponent<Person>();
            return personComponent;
        }

        private static void InitializePerson(Person person, PersonData personData)
        {
            person.Initialize(personData);
        }
    }
}
