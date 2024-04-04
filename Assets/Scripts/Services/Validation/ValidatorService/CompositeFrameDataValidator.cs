using System.Collections.Generic;
using System.Linq;

namespace Services.Validation.ValidatorService
{
    public class CompositeFrameDataValidator : IFrameDataValidator
    {
        private readonly List<IFrameDataValidator> validators = new List<IFrameDataValidator>();

        public void AddValidator(IFrameDataValidator validator)
        {
            validators.Add(validator);
        }

        public bool IsValid(FrameData frameData)
        {
            return validators.All(validator => validator.IsValid(frameData));
        }
    }

}