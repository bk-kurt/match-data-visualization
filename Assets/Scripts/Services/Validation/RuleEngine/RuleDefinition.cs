using System.Collections.Generic;

namespace Services.Validation.RuleEngine
{
    public class RuleDefinition
    {
        public string RuleType { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        
        public RuleDefinition(string ruleType, Dictionary<string, object> parameters)
        {
            RuleType = ruleType;
            Parameters = parameters;
        }
    }

}