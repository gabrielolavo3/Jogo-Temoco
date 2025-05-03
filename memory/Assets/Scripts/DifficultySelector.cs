using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LevelController;

public class DifficultySelector : MonoBehaviour
{    
    public LevelData faseUm;
    public LevelData faseDois;
    public LevelData faseTres;
    [HideInInspector] public AudioPlayer audioPlayer;

    public void ModoFacilSelecionado(string cena)
    {
        ReconfigurarDificuldade(faseUm);
        LoadingDeCena(cena);
    }

    public void ModoMedioSelecionado(string cena)
    {
        ReconfigurarDificuldade(faseDois);
        LoadingDeCena(cena);
    }

    public void ModoDificilSelecionado(string cena)
    {
        ReconfigurarDificuldade(faseTres);
        LoadingDeCena(cena);
    }

    private void ReconfigurarDificuldade(LevelData levelData)
    {
        PlayerPrefs.SetInt("LevelColumns", levelData.Colunas);
        PlayerPrefs.SetInt("LevelRows", levelData.Linhas);
        PlayerPrefs.SetFloat("LevelSpacing", levelData.Espacamento);
        PlayerPrefs.SetInt("LevelDifficulty", levelData.Pares);
    }

    public void LoadingDeCena(string nome_cena)
    {
        SceneManager.LoadScene(nome_cena);

        if (AudioPlayer.instanciaAudioPlayer != null)
        {
            AudioPlayer.instanciaAudioPlayer.TocarSegundaMusica();            
        }
    }
}
