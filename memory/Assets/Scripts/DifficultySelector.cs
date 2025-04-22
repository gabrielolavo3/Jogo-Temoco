using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AbrirCena(string nome_cena)
    {
        SceneManager.LoadScene(nome_cena);
    }
}
