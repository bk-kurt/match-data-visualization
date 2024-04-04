using Validation;

namespace Services.Validation.ValidatorService
{
    public class ValidatorSetups
    {
        public static void ValidateFrameData(FrameData frameData)
        {
            var validator = new FrameDataValidator(CustomCompositeValidator());
            
            validator.ValidateFrameData(frameData);
        }
        public static CompositeFrameDataValidator CustomCompositeValidator()
        {
            var basicValidator = new BasicFrameDataValidator();
            var gameRulesValidator = new MatchRulesFrameDataValidator();
            var compositeValidator = new CompositeFrameDataValidator();
            compositeValidator.AddValidator(basicValidator);
            compositeValidator.AddValidator(gameRulesValidator);
            return compositeValidator;
        }
    }
}