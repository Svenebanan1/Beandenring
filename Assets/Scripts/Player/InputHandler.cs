using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public SwitchRocks switchRocks;

    private InputAction _switchRockAction;

    void Update()
    {
        _switchRockAction = InputSystem.actions.FindAction("SwitchRock");

        _switchRockAction.performed += OnSwitchRockPerformed;

    }

     private void OnSwitchRockPerformed(InputAction.CallbackContext context)
     {
        switchRocks.OnSwitch();
     }

}