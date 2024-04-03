using DataModels;
using DefaultNamespace;
using GamePlay.Controllers;
using Managers;
using Scriptables.Configuration;
using UnityEngine;
using Utilities;

public class Person : BaseInterpolatedObject
{
    [SerializeField] private PersonController controller;
    [SerializeField] private PersonUI UI;
    private PersonData _initialPersonData;

    void OnEnable()
    {
        BallEventManager.Instance.OnBallPossessionChanged += HandleBallPossessionChange;
    }

    public void Initialize(PersonData personData)
    {
        if (personData == null || personData.Position == null || personData.Position.Length < 3)
        {
            Debug.LogError("Invalid PersonData provided.");
            return;
        }

        _initialPersonData = personData;
        UpdateState(personData);

        string mappedName = PersonNameMapper.GetMappedNameById(personData.Id);
        UI.Initialize(mappedName);
    }

    public override void UpdateState(IInterpolatedStateData interpolatedStateData)
    {
        base.UpdateState(interpolatedStateData);

        var personData = interpolatedStateData as PersonData;

        if (personData != null && personData.PersonContext.HasBallPossession)
        {
            BallEventManager.Instance.ChangeBallPossession(personData);
        }

        controller.ApplyTopLevelChanges(interpolatedStateData);
    }

    private void HandleBallPossessionChange(int playerId, Possession possession) //999
    {
        if (playerId == _initialPersonData.Id)
        {
            bool hasPossession = (possession == Possession.HomeTeam && _initialPersonData.TeamSide == 0) ||
                                 (possession == Possession.AwayTeam && _initialPersonData.TeamSide == 1);

            UI.UpdateView(hasPossession);
        }
    }

    public void RefreshConfig(VisualizationAssetsConfigSo visualizationAssetsConfigSo)
    {
        // For example, changing the character model, updating textures,
        // or applying new animation sets based on the provided 
        throw new System.NotImplementedException();
    }

    void OnDisable()
    {
        if (BallEventManager.Instance != null)
        {
            BallEventManager.Instance.OnBallPossessionChanged -= HandleBallPossessionChange;
        }
    }
}