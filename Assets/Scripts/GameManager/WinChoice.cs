using UnityEngine;
using UnityEngine.SceneManagement;

public class WinChoice : MonoBehaviour
{
    private WaveManager wavemanager;

    void Start()
    {
        // Find the WaveManager in the scene, even when it is inactive.
        wavemanager = FindAnyObjectByType<WaveManager>(FindObjectsInactive.Include);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void InfWave()
    {
        if (wavemanager == null)
        {
            Debug.LogError("No WaveManager was found in the scene.", this);
            return;
        }

        Time.timeScale = 1f;

        // Activate the GameObject that holds the WaveManager script.
        wavemanager.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Close the victory UI.
        // This assumes this script is attached to the Canvas itself.
        gameObject.SetActive(false);
    }
}
