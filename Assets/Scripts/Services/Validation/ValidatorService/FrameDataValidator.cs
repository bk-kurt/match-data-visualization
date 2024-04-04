using UnityEngine;

namespace Services.Validation.ValidatorService
{
    public class FrameDataValidator
    {
        private readonly IFrameDataValidator validator;

        public FrameDataValidator(IFrameDataValidator validator)
        {
            this.validator = validator;
        }

        public void ValidateFrameData(FrameData frameData)
        {
            if (!validator.IsValid(frameData))
            {
                Debug.LogWarning("Invalid frame data received.");
                return;
            }

            // Proceed with processing valid frame data
        }
    }

}