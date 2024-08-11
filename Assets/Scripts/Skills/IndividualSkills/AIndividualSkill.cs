using System.Collections.Generic;

public abstract class AIndividualSkill : ASkill
{
    public int TargetNb;

    public ASkillFrequency Frequency;

    public bool CanPerformOnAllCharacters = true;
    public bool CanPerformOnSelf = true;
    public List<Character> PotentialTargetTypes;

    public override void Perform(ref List<Character> targets)
    {
        targets.ForEach(target => PerformOnTarget(target));
        Frequency.Update();
    }

    protected abstract void PerformOnTarget(Character target);
}
