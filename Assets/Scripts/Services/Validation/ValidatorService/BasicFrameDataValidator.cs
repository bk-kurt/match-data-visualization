using Services.Validation.ValidatorService;

namespace Validation
{
    public class BasicFrameDataValidator : IFrameDataValidator
    {
        public bool IsValid(FrameData frameData)
        {
            //qqq26 genereal basic rules
            return frameData != null && frameData.Persons != null && frameData.Ball != null;
        }
    }
}