


// WHEN YOU WANT TO GENERATE a new rule that you can change its definition dynamically, you need to do 3 things:

// 1. create the class of rule                                                                  >>>  class BallInBoundsRule
// 2. Add creation case of the rule to IValidationRule switch-case block                         >>>  CreateRule() 
// 3  Add your rule object (type, definition) to the JSON file where you have your all rules.   >>>  DynamicRuleDefinitionsSample.json



// WHEN YOU WANT TO USE

// Now You can dynamically change your rule definitions by just changing JSON values.            >>> "RuleType": "BallBoundsRule" "Parameters": { "xMax": 95++ , "yMax":109--, "xMin": 10 , "yMin":15 }
//                                                                                                  
//  then use ValidationService to and validate your data you load with dynamically defined rules.
//  you are free to build your design ValidationService like arranging different filters in your hand.



// FUTURE WORK > this engine can be improved to handle dynamic rule types, currently it only handles dynamic rule definitions not dynamic type declarations.
// if we can manage to do that, we can take the advantage of having infinitely combinable flexible system
// (when we think json objects as key-value combinations, we will be able to use JSON like a dictionary)

using System;
using DynamicRuleDefinitionService;

namespace Services.Validation.RuleEngine
{
    public static class DynamicRuleSetupSample
    {
        public static IValidationRule<FrameData> CreateRule(RuleDefinition definition)
        {
            switch (definition.RuleType)
            {
                case "PlayerCountRule":
                    int minCount = Convert.ToInt32(definition.Parameters["minCount"]);
                    return new PlayerCountRule(minCount);

                case "BallInBoundsRule":
                    float xMin = Convert.ToSingle(definition.Parameters["xMin"]);
                    float xMax = Convert.ToSingle(definition.Parameters["xMax"]);
                    float yMin = Convert.ToSingle(definition.Parameters["yMin"]);
                    float yMax = Convert.ToSingle(definition.Parameters["yMax"]);
                    return new BallInBoundsRule(xMin, xMax, yMin, yMax);

                default:
                    throw new ArgumentException($"Unknown rule type: {definition.RuleType}");
            }
        }
    }
}

public class PlayerCountRule : IValidationRule<FrameData>
{
    private readonly int _minCount;

    public PlayerCountRule(int minCount)
    {
        _minCount = minCount;
    }

    public bool Validate(FrameData data) => data.Persons.Count >= _minCount;
}

public class BallInBoundsRule : IValidationRule<FrameData>
{
    private readonly float _xMin, _xMax, _yMin, _yMax;

    public BallInBoundsRule(float xMin, float xMax, float yMin, float yMax)
    {
        _xMin = xMin;
        _xMax = xMax;
        _yMin = yMin;
        _yMax = yMax;
    }

    public bool Validate(FrameData data)
    {
        var position = data.Ball.Position;
        return position[0] >= _xMin && position[0] <= _xMax && position[1] >= _yMin && position[2] <= _yMax;
    }
}