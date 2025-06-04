using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class temaJogo : MonoBehaviour
{

    public Button btnPlay;
    public Text txtNomeTema;
    public int numeroQuestoes;
    
    public string[] nomeTema;

    private int idTema;

    void Start()
    {
        idTema = 0;
        txtNomeTema.text = nomeTema[idTema];
        btnPlay.interactable = false;
    }

    public void selecioneTema (int i){
        idTema = i;
        PlayerPrefs.SetInt("idTema", idTema);
        txtNomeTema.text = nomeTema[idTema];
            int notaFinal = PlayerPrefs.GetInt("notaFinal"+idTema.ToString());
            int acertos = PlayerPrefs.GetInt("acertos"+idTema.ToString());
        btnPlay.interactable = true;
    }

    public void jogar(){
        SceneManager.LoadScene("T"+idTema.ToString());
    }
}
