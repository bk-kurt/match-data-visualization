using DataModels;
using DefaultNamespace;
using DefaultNamespace.GamePlay;

namespace GamePlay
{
    public class VisualElementFactory: IVisualElementFactory
    {
        private readonly VisualizationAssetsConfigSo _visualizationAssetsConfigSo;

        public VisualElementFactory(VisualizationAssetsConfigSo visualizationAssetsConfigSo)
        {
            _visualizationAssetsConfigSo = visualizationAssetsConfigSo;
        }

        public Person CreatePerson(PersonData personData)
        {
            return PersonFactory.Create(personData, _visualizationAssetsConfigSo);
        }

        public Ball CreateBall(BallData ballData)
        {
            return BallFactory.Create(ballData, _visualizationAssetsConfigSo);
        }
    }
}