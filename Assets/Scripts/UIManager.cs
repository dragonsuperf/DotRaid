using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private void Start(){
        PlayerPrefs.SetInt("AlreadySeenIntro", 1);
    }

    public void OnStartButtonClicked()
    {
        if (PlayerPrefs.GetInt("AlreadySeenIntro") == 1) // 빌드할때 PlayerPrefs.GetInt("AlreadySeenIntro") < 1로 바꿔야함
        {
            PlayerPrefs.SetInt("AlreadySeenIntro", 1);
            SceneManager.LoadScene("Lobby");
        }
        else
            SceneManager.LoadScene("SampleScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnEnterPortal(string portal){
        
    }
}
