using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpriteAnimator : MonoBehaviour
{
    public enum Direction
    {
        Down = 0,
        Left = 1,
        Right = 2,
        Up = 3
    }

    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStateController state;

    private Direction currentDirection = Direction.Down;

    void Awake()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        if (!state)
            state = GetComponent<PlayerStateController>();
    }

    void Update()
    {
        UpdateDirection();
        UpdateAnimation();
    }

    /// <summary>
    /// 入力方向から向きを更新（移動できなくても向きは変える）
    /// </summary>
    void UpdateDirection()
    {
        Vector2 input = GetInputDirection();
        if (input == Vector2.zero) return;

        if (input == Vector2.up)
            currentDirection = Direction.Up;
        else if (input == Vector2.down)
            currentDirection = Direction.Down;
        else if (input == Vector2.left)
            currentDirection = Direction.Left;
        else if (input == Vector2.right)
            currentDirection = Direction.Right;

        animator.SetInteger("Direction", (int)currentDirection);
    }

    /// <summary>
    /// 移動状態に応じて Idle / Walk を切り替え
    /// </summary>
    void UpdateAnimation()
    {
        bool isMoving = state != null && state.CurrentState == PlayerState.Moving;
        animator.SetBool("IsMoving", isMoving);
    }

    Vector2 GetInputDirection()
    {
        if (Keyboard.current.wKey.isPressed) return Vector2.up;
        if (Keyboard.current.sKey.isPressed) return Vector2.down;
        if (Keyboard.current.aKey.isPressed) return Vector2.left;
        if (Keyboard.current.dKey.isPressed) return Vector2.right;
        return Vector2.zero;
    }
}
