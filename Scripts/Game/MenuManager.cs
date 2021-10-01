using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playMenu;
    [SerializeField] GameObject settingsMenu;

    public void Play()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Settings()
    {
        // switch to the settings menu
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GoBack()
    {
        // go back to the main menu
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        playMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
