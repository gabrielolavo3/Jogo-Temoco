using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaixaDeDialogos : MonoBehaviour
{
    [Header("Referências da História")]
    public GameObject historyPanel;
    public Image iconePersonagem;
    public Text textoDialogo;

    [Header("Imagens")]
    public Sprite sprite1;
    public Sprite sprite2;

    public event Action DialogoFinalizado;
    private int fraseAtual = 0;
    private string[] vetorFrases = new string[]
    {
        "Oi! Eu sou o Shuun, um guerreiro de um Reino antigo da Era Medieval!",
        "Mas um brilho misterioso apareceu DO NADA! e me levou pro futuro... ou pro presente?...",
        "Ouvi uma voz que me pedia para eu aprender a enfretar a Rotina do Futuro. ",
        "Não sei direito o que ele quis dizer, mas você parece que conhece bem este lugar",
        "Talvez seja difícil, mas nada vai ser impossível se juntarmos nossas forças!",
        "Você quer me ajudar a aprender mais desse novo mundo?"
    };

    public void ExibirDialogo()
    {
        Time.timeScale = 0f;
        historyPanel.SetActive(true);
        fraseAtual = 0;
        textoDialogo.text = vetorFrases[fraseAtual];
        iconePersonagem.sprite = sprite1;
    }

    public void ProximaFrase()
    {
        fraseAtual++;

        if (fraseAtual < vetorFrases.Length)
        {
            textoDialogo.text = vetorFrases[fraseAtual];
        }
        else
        {
            historyPanel.SetActive(false);
            DialogoFinalizado?.Invoke();
        }
    }
}