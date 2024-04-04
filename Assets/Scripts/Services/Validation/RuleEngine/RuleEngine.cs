using System.Collections.Generic;
using System.Linq;
using DynamicRuleDefinitionService;

namespace Services.Validation.RuleEngine
{
    public class RuleEngine<T>
    {
        private readonly List<IValidationRule<T>> _rules = new List<IValidationRule<T>>();

        public void AddRule(IValidationRule<T> rule)
        {
            _rules.Add(rule);
        }

        public bool Validate(T data)
        {
            return _rules.All(rule => rule.Validate(data));
        }
    }

}