using EditorAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum SkillOptions
{
    Nothing = 0,
    CanSelfTarget = 1,
    CanTargetAllCharacters = 2,
}

[Serializable]
public class TargetedCharacters
{
    public List<CharacterSO> Characters;
}

public abstract class ASkillSO : ScriptableObject
{  
    public string ActionVerb;
    [TextArea(2, 10)] public string Description;

    public int TargetNb;

    public ASkillFrequencySO Frequency;

    [SelectionButtons(showLabel: false)]
    public SkillOptions Options;
    private bool showTargets => !Options.HasFlag(SkillOptions.CanTargetAllCharacters);

    [ShowIf(nameof(showTargets))] 
    public TargetedCharacters Targets;

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
