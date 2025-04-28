using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LevelController;

public class DifficultySelector : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public LevelData nivelFacil;
    public LevelData nivelMedio;
    public LevelData nivelDificil;    

    public void ModoFacilSelecionado(string cena)
    {
        ReconfigurarDificuldade(nivelFacil);
        LoadingDeCena(cena);
    }

    public void ModoMedioSelecionado(string cena)
    {
        ReconfigurarDificuldade(nivelMedio);
        LoadingDeCena(cena);
    }

    public void ModoDificilSelecionado(string cena)
    {
        ReconfigurarDificuldade(nivelDificil);
        LoadingDeCena(cena);
    }

    private void ReconfigurarDificuldade(LevelData levelData)
    {
        PlayerPrefs.SetInt("LevelColumns", levelData.Columns);
        PlayerPrefs.SetInt("LevelRows", levelData.Rows);
        PlayerPrefs.SetFloat("LevelSpacing", levelData.Spacing);
        PlayerPrefs.SetInt("LevelDifficulty", levelData.Difficulty);
    }

    public void LoadingDeCena(string nome_cena)
    {
        SceneManager.LoadScene(nome_cena);

        if (AudioPlayer.instanciaAudioPlayer != null)
        {
            AudioPlayer.instanciaAudioPlayer.TocarSegundaMusica();
            Destroy(audioPlayer);
        }
    }
}
