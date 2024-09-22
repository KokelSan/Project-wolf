
public abstract class ASkillFrequencySO : InstantiableSO
{
    protected override void Initialize() { }

    public abstract bool CanExecute();    

    public abstract void Update();
}
