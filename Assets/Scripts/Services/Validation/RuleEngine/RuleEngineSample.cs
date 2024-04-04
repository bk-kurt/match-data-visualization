using System.Collections.Generic;
using UnityEngine;

namespace Services.Validation.RuleEngine
{
     public class RuleEngineSample
    {
        public RuleEngine<FrameData> FrameDataRuleEngine(string frameDataRuleDefinitionsJson)
        {
            var ruleDefinitions = JsonUtility.FromJson<List<RuleDefinition>>(frameDataRuleDefinitionsJson);
            var ruleEngine = new RuleEngine<FrameData>();

            foreach (var definition in ruleDefinitions)
            {
                var rule = DynamicRuleSetupSample.CreateRule(definition);
                ruleEngine.AddRule(rule);
            }

            return ruleEngine;
        }
    }
    
}