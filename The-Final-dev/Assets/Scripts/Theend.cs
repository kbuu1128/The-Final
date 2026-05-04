using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Theend : MonoBehaviour
{
    [Header("Settings")]
    public float delaySeconds = 10f;
    public bool allowSkip = true;

    [Header("Scene To Load")]
    public string mainMenuScene = "MenuScene";

    void Start()
    {
        StartCoroutine(ReturnToMenu());
    }

    void Update()
    {
        if (allowSkip && Input.anyKeyDown)
            LoadMenu();
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(delaySeconds);
        LoadMenu();
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}