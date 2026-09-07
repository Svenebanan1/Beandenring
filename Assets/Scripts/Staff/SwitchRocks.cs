using UnityEngine;

public class SwitchRocks : MonoBehaviour
{

    public GameObject redRock;
    public GameObject blueRock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redRock.SetActive(true);
        blueRock.SetActive(false);
    }

    public void OnSwitch()
    {
        if (redRock.activeSelf)
        {
            redRock.SetActive(false);
            blueRock.SetActive(true);
        }
        else
        {
            redRock.SetActive(true);
            blueRock.SetActive(false);
        }
    }
}
