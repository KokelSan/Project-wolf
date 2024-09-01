using UnityEngine;

[CreateAssetMenu(menuName = "Frequencies/New Unlimited Frequency", fileName = "Unlimited_Frequency")]
public class Unlimited_Frequency : ASkillFrequency
{
    public override bool CanExecute()
    {
        return true;
    }

    public override void Update() { }

}
