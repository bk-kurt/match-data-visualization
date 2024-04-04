using Scriptables.Configuration;

public interface IVisualizationAssetConfigProvider
{
    IVisualizationAssetConfigProvider GetGameAssetsConfig();
    IVisualizationAssetConfigProvider SetGameAssetsConfig(IVisualizationAssetConfigProvider configProvider);
}