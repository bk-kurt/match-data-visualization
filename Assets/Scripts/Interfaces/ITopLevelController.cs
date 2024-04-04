using DataModels;


//qqq6 since the visual output is relying on actual frame data, we may want to apply custom controls on objects,
// like some custom behaviors... dancing & celebration on goal scoring. and so one...
public interface ITopLevelController
{
    void ApplyTopLevelChanges(IInterpolatedStateData interpolatedStateData);
}