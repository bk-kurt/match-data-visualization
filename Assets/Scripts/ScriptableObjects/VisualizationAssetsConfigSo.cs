using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAssets", menuName = "ScriptableObjects/VisualizationAssets", order = 1)]
public class VisualizationAssetsConfigSo : ScriptableObject
{
    public List<PersonConfigSo> personConfigs;
    public BallConfigSo defaultBallConfig;

    public PersonConfigSo GetConfiguredPersonByTeamSide(int teamSide)
    {
        foreach (var config in personConfigs)
        {
            if (config.teamSide == teamSide)
            {
                return config;
            }
        }
        Debug.LogError($"No PersonConfiguration found for team side: {teamSide}");
        return null;
    }

    public BallConfigSo GetConfiguredBall(/* potential parameters*/)
    {
        return defaultBallConfig;
    }
}