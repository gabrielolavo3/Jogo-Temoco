using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuseioCena : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private CaixaDeDialogos caixaDeDialogos;

    [Header("Configurações")]
    [SerializeField][Range(0, 100)] private int chanceDeReverHistoria = 30;

    private int chanceSorteada;
    private bool exibindoHistoria = false;

    void Start()
    {
        if (caixaDeDialogos == null)
        {
            Debug.LogError("CaixaDeDialogos não atribuída no Inspector!", this);
            IniciarFase();
            return;
        }

        if (InfoJogo.JogoFoiReiniciado)
        {
            InfoJogo.JogoFoiReiniciado = false;
            IniciarFase();
            return;
        }

        bool primeiraVez = !PlayerPrefs.HasKey("JaVisualizouHistoria");

        if (primeiraVez)
        {
            ExibirHistoria();
            PlayerPrefs.SetInt("JaVisualizouHistoria", 1);
            PlayerPrefs.Save();
        }
        else
        {
            chanceSorteada = Random.Range(0, 100);
            if (chanceSorteada < chanceDeReverHistoria)
            {
                ExibirHistoria();
            }
            else
            {
                IniciarFase();
            }
        }
    }

    private void ExibirHistoria()
    {
        if (exibindoHistoria || caixaDeDialogos == null) return;

        exibindoHistoria = true;
        Time.timeScale = 0f;

        // Acesso seguro através da propriedade pública
        caixaDeDialogos.ExibirDialogo(caixaDeDialogos.Sprite1);

        caixaDeDialogos.DialogoFinalizado += IniciarFase;
    }

    private void IniciarFase()
    {
        if (exibindoHistoria)
        {
            caixaDeDialogos.DialogoFinalizado -= IniciarFase;
            exibindoHistoria = false;
        }

        Time.timeScale = 1f;
    }

    // Método para debug
    public void ForcarExibicaoHistoria()
    {
        ExibirHistoria();
    }
}