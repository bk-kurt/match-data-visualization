using DataModels;
using DefaultNamespace;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay
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

            PersonConfigSo personConfigSo = visualizationAssetsConfigSo.GetConfiguredPersonByTeamSide(personData.TeamSide);
            if (personConfigSo == null || personConfigSo.personPrefab == null)
            {
                Debug.LogError($"PersonFactory: Person configuration or prefab is null for team side {personData.TeamSide}.");
                return null;
            }

            Vector3 personPosition = personData.targetPosition;
            Person instantiatedPersonGo = Object.Instantiate(personConfigSo.personPrefab, personPosition, Quaternion.identity);
            
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