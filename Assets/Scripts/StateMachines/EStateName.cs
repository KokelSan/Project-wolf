public enum EStateName
{
    None = 0,

    // GameStateMachine's states
    GameBeginning,
    Rounds_SM,
    GameEnding,

    // Rounds_SM's states
    RoundBeginning,
    IndividualSkills_SM,
    GroupSkills_SM,
    GeneralVote,
    RoundEnding,

    // Skills_SM's states
    IndividualSkill,
    GroupSkill,
}