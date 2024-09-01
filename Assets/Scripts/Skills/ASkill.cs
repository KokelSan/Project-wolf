using System.Collections.Generic;
using UnityEngine;

public abstract class ASkill : ScriptableObject
{
    public string ActionVerb;
    public string Description;

    public int TargetNb;

    public ASkillFrequency Frequency;

    public bool CanTargetAllCharacters = true;
    public List<Character> TargetedCharacters;
    public bool CanSelfTarget = false;

    protected abstract void Execute(Character target);

    public virtual SkillExecutionReport Execute(List<Character> performers, List<Character> targets)
    {
        targets.ForEach(target => Execute(target));
        Frequency.Update();

        return GenerateExecutionReport(performers, targets);
    }    

    protected SkillExecutionReport GenerateExecutionReport(List<Character> performers, List<Character> targets)
    {
        return new SkillExecutionReport()
        {
            SkillName = name,
            Targets = targets,            
            Log = $"{CharactersUtils.GetFormattedNames(performers)} {ActionVerb.ToLower()} {CharactersUtils.GetFormattedNames(targets)}",            
        };
    }
}
