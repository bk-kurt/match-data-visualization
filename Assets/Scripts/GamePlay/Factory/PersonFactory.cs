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
            if (personData == null || _visualizationAssetsConfigProvider == null)
            {
                Debug.LogError("PersonFactory: Person data or person config provider is null.");
                return null;
            }

            VisualizationAssetsConfigProviderSo assetsConfigProviderSo =
                _visualizationAssetsConfigProvider as VisualizationAssetsConfigProviderSo;
            if (assetsConfigProviderSo != null)
            {
              var personConfig=  assetsConfigProviderSo.GetConfiguredPersonByTeamSide(personData.TeamSide);
                if (assetsConfigProviderSo == null || personConfig == null)
                {
                    Debug.LogError(
                        $"PersonFactory: Configuration or prefab is null for team side {personData.TeamSide}.");
                    return null;
                }

                Vector3 personPosition = personData.TargetPosition;
                var parentTransform = EnvironmentSetUp.Instance.teamTransforms[personData.TeamSide];
                Person instantiatedPersonGo = Object.Instantiate(personConfig.personPrefab, personPosition,
                    Quaternion.identity, parentTransform);

                Person personComponent = instantiatedPersonGo.GetComponent<Person>();
                if (personComponent == null)
                {
                    personComponent = instantiatedPersonGo.AddComponent<Person>();
                }

                personComponent.Initialize(personData);
                return personComponent;
            }

            Debug.LogError(
                $"PersonFactory: Configuration provider is null");
            return null;
        }
        
    }
}