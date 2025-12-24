using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Jumping
}

public class PlayerStateController : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;

    public bool CanMove => CurrentState == PlayerState.Idle;
    public bool CanJump => CurrentState == PlayerState.Idle;

    public void SetState(PlayerState state)
    {
        CurrentState = state;
    }
}
