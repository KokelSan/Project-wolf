using System.Collections.Generic;

public abstract class AIndividualSkill : ASkill
{
    public int TargetNb;

    public ASkillFrequency Frequency;

    public bool TargetAllCharacters = true;
    public bool CanSelfTarget = true;
    public List<Character> TargetedCharacters;

    public override void Perform(ref List<Character> targets)
    {
        targets.ForEach(target => PerformOnTarget(target));
        Frequency.Update();
    }

    protected abstract void PerformOnTarget(Character target);
}
