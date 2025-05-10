using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EstrelasPorPontuacao : MonoBehaviour
{
    [Header("Sprites Personalizados")]
    public Image[] estrelasIcon;
    public Sprite spriteEstrela;
    public Sprite spriteEstrela02;
    public Sprite spriteEstrela03;
    public Sprite estrelaVazia;

    [Header("Lista de ScriptableObject")]
    public PontuacaoConfigFase[] avaliacoesFases;

    private int idFase;
    private PontuacaoConfigFase dadosPorFase;

    // Start is called before the first frame update
    void Start()
    {
        idFase = PlayerPrefs.GetInt("FaseAtual", 0);
        dadosPorFase = ObterDados(idFase);

        if (dadosPorFase != null) 
        {
            ExibirEstrelasPorPontos();
        }
        else
        {
            Debug.LogError("Dados não encontrados");
        }
    }

    PontuacaoConfigFase ObterDados(int id)
    {
        foreach (var avaliacao in avaliacoesFases)
        {
            if (avaliacao.idFase == id)
            {
                return avaliacao;
            }
        }

        return null;
    }

    public void ExibirEstrelasPorPontos()
    {
        int pontuacaoFinal = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);
        int qtdEstrelas = 0;

        if (pontuacaoFinal >= dadosPorFase.pontosMaximos)
        {
            qtdEstrelas = 3;
        }
        else if (pontuacaoFinal >= dadosPorFase.pontosIntermadiarios)
        {
            qtdEstrelas = 2;
        }
        else if (pontuacaoFinal >= dadosPorFase.pontosMinimos) 
        {
            qtdEstrelas = 1;
        } 

        for (int i = 0; i < estrelasIcon.Length; i++)
        {
            if (i == 0 && qtdEstrelas >= 1)
            {
                estrelasIcon[i].sprite = spriteEstrela;
            }
                
            else if (i == 1 && qtdEstrelas >= 2)
            {
                estrelasIcon[i].sprite = spriteEstrela02;
            }
                
            else if (i == 2 && qtdEstrelas == 3)
            {
                estrelasIcon[i].sprite = spriteEstrela03;
            }

            else
            {
                estrelasIcon[i].sprite = estrelaVazia;
            }                
        }

        // Salvar estrelas conquistadas
        string chave = "EstrelasFase" + idFase;
        int estrelasSalvas = PlayerPrefs.GetInt(chave, 0);
        if (qtdEstrelas > estrelasSalvas)
        {
            PlayerPrefs.SetInt(chave, qtdEstrelas);
            PlayerPrefs.Save();
        }
    }
}
