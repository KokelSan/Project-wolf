using UnityEngine;

[CreateAssetMenu(menuName = "Frequencies/New Limited Frequency", fileName = "_Frequency")]
public class Limited_Frequency : ASkillFrequency
{
    [SerializeField] private int MaxUseNb;

    private int executionCount = 0;

    public override bool CanExecute()
    {
        return executionCount < MaxUseNb;
    }
    
    public override void Update() 
    {
        executionCount++;
    }
}
