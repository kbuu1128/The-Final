using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            string levelToRestart = PlayerPrefs.GetString("RestartLevel", "Level1");
            SceneManager.LoadScene(levelToRestart);
        }
    }
}