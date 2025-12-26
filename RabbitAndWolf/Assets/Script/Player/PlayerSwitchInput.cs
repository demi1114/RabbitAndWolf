using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchInput : MonoBehaviour
{
    private InputAction switchAction;

    void Awake()
    {
        switchAction = new InputAction(
            "Switch",
            InputActionType.Button,
            "<Mouse>/rightButton");

        switchAction.performed += _ =>
        {
            PlayerManager.Instance.SwitchPlayer();
        };
    }

    void OnEnable() => switchAction.Enable();
    void OnDisable() => switchAction.Disable();
}
