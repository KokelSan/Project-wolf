using UnityEngine;

[CreateAssetMenu(menuName = "Frequencies/New OnDeath Frequency", fileName = "OnDeath_Frequency")]
public class OnDeath_FrequencySO : ASkillFrequencySO
{
    private bool hasBeenExecuted = false;

    public override bool CanExecute()
    {
        CharacterSO owner = OwnerSO.OwnerSO as CharacterSO;
        return !hasBeenExecuted && !owner.IsAlive;
    }

    public override void Update() 
    {
        hasBeenExecuted = true;
    }
}
