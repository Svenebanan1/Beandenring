using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchRocks : MonoBehaviour
{
    public GameObject redRock;
    public GameObject blueRock;
    public GameObject iceImage;
    public GameObject fireImage;

    private InputAction _switchRockAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redRock.SetActive(false);
        blueRock.SetActive(true);
    }

    private void Update()
    {
        _switchRockAction = InputSystem.actions.FindAction("SwitchRock");

        OnSwitch();
    }

    public void OnSwitch()
    {
        if (_switchRockAction.WasPressedThisFrame())
        {
            if (redRock.activeSelf)
            {
                redRock.SetActive(false);
                fireImage.SetActive(false);
                blueRock.SetActive(true);
                iceImage.SetActive(true);
            }
            else
            {
                redRock.SetActive(true);
                fireImage.SetActive(true);
                blueRock.SetActive(false);
                iceImage.SetActive(false);
            }
        }
    }

}
