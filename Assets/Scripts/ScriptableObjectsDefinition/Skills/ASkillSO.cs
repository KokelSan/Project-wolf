using EditorAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum SkillOptions
{
    Nothing = 0,
    CanSelfTarget = 1,
    TargetSpecificCharacters = 2
}

[Serializable]
public class AuthorizedTargets
{
    public List<CharacterSO> Characters;
}

public abstract class ASkillSO : InstantiableSO
{
    public string ActionVerb;
    [TextArea(2, 10)] public string Description;

    public int TargetNb;

    public ASkillFrequencySO Frequency;
    private ASkillFrequencySO frequency_Instantiated;

    [SelectionButtons(showLabel: false)] public SkillOptions Options = SkillOptions.CanSelfTarget;
    [HideInInspector] public bool CanSelfTarget, TargetSpecificCharacters; private bool hideTargetsList;

    [ShowIf(nameof(TargetSpecificCharacters)), HideIf(nameof(hideTargetsList))]
    public AuthorizedTargets AuthorizedTargets;

    private void OnValidate()
    {
        hideTargetsList = !Options.HasFlag(SkillOptions.TargetSpecificCharacters);

        if (TargetSpecificCharacters && hideTargetsList) // last state was to show the list, the new state is to hide it
        {
            AuthorizedTargets.Characters.Clear();
        }

        TargetSpecificCharacters = !hideTargetsList;
        CanSelfTarget = Options.HasFlag(SkillOptions.CanSelfTarget);  
    }

    protected override void Initialize()
    {
        frequency_Instantiated = ScriptableObjectFactory.CreateInstance(Frequency);
    }

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
