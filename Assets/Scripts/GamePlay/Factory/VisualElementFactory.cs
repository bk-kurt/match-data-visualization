using DataModels;
using Scriptables.Configuration;

// sample starting point of dynamic factory building with configs, IVisualElementFactory
namespace GamePlay.Factory
{
    public class VisualElementFactory: IVisualElementFactory
    {
        private readonly IVisualizationAssetConfigProvider _visualizationAssetsConfig;

        public VisualElementFactory(IVisualizationAssetConfigProvider visualizationAssetsConfigSo)
        {
            _visualizationAssetsConfig = visualizationAssetsConfigSo;
        }

        public Person CreatePerson(PersonData personData)
        {
            return PersonFactory.Create(personData);
        }

        public Ball CreateBall(BallData ballData)
        {
            return BallFactory.Create(ballData);
        }
    }
}