using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public void RestartGame(string scene_name)
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(scene_name);
    }

    public void GoHome()
    {
        Debug.Log("Carregando a tela inicial....");        
    }    
}
