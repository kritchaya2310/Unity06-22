using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start() 
    {
        AudioManager.instance.PlayMainMenuMusic();

        
    }
    public void StartGame(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
        AudioManager.instance.PlayLevelMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
