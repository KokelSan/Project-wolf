using UnityEngine;

[CreateAssetMenu(menuName = "Frequencies/New Limited Frequency", fileName = "_Frequency")]
public class Limited_Frequency : ASkillFrequency
{
    [SerializeField] private int MaxUseNb;

    private int useCount = 0;

    public override bool CanPerform()
    {
        return useCount < MaxUseNb;
    }
    
    public override void Update() 
    {
        useCount++;
    }
}
