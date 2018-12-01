/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as StateMachine class uses it.
/// </summary>
public enum StateID
{
    NullStateID = 0, // Use this ID to represent a non-existing State in your system	

    // game states
    GameStartStateID,
    GamePlayStateID,
    GameOverStateID,

    // player
    PlayerRegularStateID,
    PlayerMovingStateID,
    PlayerPushingStateID,
    PlayerClimbingStateID,
    PlayerCrushedStateID
}
