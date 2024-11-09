public enum EStateName
{
    None = 0,

    // GameStateMachine
    GameBeginning,
    RoundsLoop,
    GameEnding,

    // RoundStateMachine
    RoundBeginning,
    SkillsLoop,
    RoundEnding,

    // SkillsLoop
    IndividualSkill,
    GroupSkill,

}