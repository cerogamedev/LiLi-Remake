using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject panel;

    void Start()
    {
        
    }

    void Update()
    {
 
    }
    public void BackButton()
    {
        pauseMenu.SetActive(false);
        panel.SetActive(true);

    }
    public void OptionsButton()
    {
        pauseMenu.SetActive(true);
        panel.SetActive(false);

    }
    public void SetQuality(int qual)
    {
        QualitySettings.SetQualityLevel(qual);
    }
    public void StartTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull; 
    }
    public void appQuit( )
    {
        Application.Quit();
    }
}
