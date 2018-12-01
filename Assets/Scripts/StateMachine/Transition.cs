/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as StateMachine class uses it.
/// </summary>
public enum Transition
{
    NullTransition = 0, // Use this transition to represent a non-existing transition in your system

    // Game states
    GameStartTransition,
    GameLoseTransition,
    GameRestartTransition,

    // player
    PlayerMoveTransition,
    PlayerPushTransition,
    PlayerClimbTransition,
    PlayerCrushTransition,
    PlayerReturnToRegularTransition
}
