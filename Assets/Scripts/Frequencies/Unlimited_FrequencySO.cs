using UnityEngine;

[CreateAssetMenu(menuName = "Frequencies/New Unlimited Frequency", fileName = "Unlimited_Frequency")]
public class Unlimited_FrequencySO : ASkillFrequencySO
{
    public override bool CanExecute()
    {
        return true;
    }

    public override void Update() { }

}
