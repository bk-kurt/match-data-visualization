namespace DynamicRuleDefinitionService
{
    public interface IValidationRule<T>
    {
        bool Validate(T data);
    }

}