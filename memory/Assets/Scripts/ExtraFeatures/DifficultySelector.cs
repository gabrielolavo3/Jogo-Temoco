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
    public Button fase2Btn;
    public Button fase3Btn;
    [HideInInspector] public AudioPlayer audioPlayer;
    [HideInInspector] public int faseDesbloqueada;
    [HideInInspector] public int pontuacaoFase1;
    [HideInInspector] public int pontuacaoFase2;

    private BloqueioFase bloqueioFase;

    void Start()
    {
        bloqueioFase = FindObjectOfType<BloqueioFase>();
        faseDesbloqueada = PlayerPrefs.GetInt("FaseDesbloqueada", 1);
        Debug.Log("Fase desbloqueada: " + faseDesbloqueada);                
    }

    public void ModoFacilSelecionado(string cena)
    {
        Debug.Log("Fase1");
        ReconfigurarDificuldade(faseUm);
        PlayerPrefs.SetInt("FaseAtual", 1);
        LoadingDeCena(cena);
    }

    public void ModoMedioSelecionado(string cena)
    {
        Debug.Log("Fase2");

        faseDesbloqueada = PlayerPrefs.GetInt("FaseDesbloqueada", 1);
        pontuacaoFase1 = PlayerPrefs.GetInt("MelhorPontuacaoFase1", 0);

        if (faseDesbloqueada >= 2 && pontuacaoFase1 >= 300)
        {
            ReconfigurarDificuldade(faseDois);
            PlayerPrefs.SetInt("FaseAtual", 2);
            LoadingDeCena(cena);
        }
        else
        {
            bloqueioFase.MostrarInformacaoBloqueio(2);
        }
    }

    public void ModoDificilSelecionado(string cena)
    {
        Debug.Log("Fase3");

        faseDesbloqueada = PlayerPrefs.GetInt("FaseDesbloqueada", 1);
        pontuacaoFase2 = PlayerPrefs.GetInt("MelhorPontuacaoFase2", 0);

        if (faseDesbloqueada >= 3 && pontuacaoFase2 >= 500)
        {
            ReconfigurarDificuldade(faseTres);
            PlayerPrefs.SetInt("FaseAtual", 3);
            LoadingDeCena(cena);
        }
        else
        {
            bloqueioFase.MostrarInformacaoBloqueio(3);
        }
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
