using DataModels;
using GamePlay.Environment;
using Scriptables.Configuration;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Factory
{
    public static class PersonFactory
    {
        public static Person Create(PersonData personData, VisualizationAssetsConfigSo visualizationAssetsConfigSo)
        {
            if (personData == null || visualizationAssetsConfigSo == null)
            {
                Debug.LogError("PersonFactory: Person data or game assets configuration is null.");
                return null;
            }

            PersonConfigSo personConfigSo =
                visualizationAssetsConfigSo.GetConfiguredPersonByTeamSide(personData.TeamSide);
            if (personConfigSo == null || personConfigSo.personPrefab == null)
            {
                Debug.LogError(
                    $"PersonFactory: Person configuration or prefab is null for team side " +
                    $"{personData.TeamSide}.");
                return null;
            }

            Vector3 personPosition = personData.TargetPosition;
            var parentTransform = EnvironmentSetUp.Instance.teamTransforms[personData.TeamSide];
            Person instantiatedPersonGo = Object.Instantiate(personConfigSo.personPrefab, personPosition,
                Quaternion.identity, parentTransform);

            Person personComponent = instantiatedPersonGo.GetComponent<Person>();
            if (personComponent == null)
            {
                personComponent = instantiatedPersonGo.AddComponent<Person>();
            }

            personComponent.Initialize(personData);
            return personComponent;
        }
    }
}