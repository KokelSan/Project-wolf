using System.Collections.Generic;
using UnityEngine;

public abstract class ASkillSO : ScriptableObject
{
    public string ActionVerb;
    public string Description;

    public int TargetNb;

    public ASkillFrequencySO Frequency;

    public bool CanTargetAllCharacters = true;
    public List<CharacterSO> TargetedCharacters;
    public bool CanSelfTarget = false;

    protected abstract void Execute(CharacterSO target);

    public virtual SkillExecutionReport Execute(List<CharacterSO> performers, List<CharacterSO> targets)
    {
        targets.ForEach(target => Execute(target));
        Frequency.Update();

        return GenerateExecutionReport(performers, targets);
    }    

    protected SkillExecutionReport GenerateExecutionReport(List<CharacterSO> performers, List<CharacterSO> targets)
    {
        return new SkillExecutionReport()
        {
            SkillName = name,
            Targets = targets,            
            Log = $"{CharactersUtils.GetFormattedNames(performers)} {ActionVerb.ToLower()} {CharactersUtils.GetFormattedNames(targets)}",            
        };
    }
}
