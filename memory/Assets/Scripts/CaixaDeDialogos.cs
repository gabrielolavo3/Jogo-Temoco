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
        "Oi! Eu sou o Koori, o aprendiz de magia do Guardião da Floresta!",
        "Hoje, eu tava treinando um feitiço de duplicação super difícil... e... ah, você não vai acreditar...",
        "Eu acidentalmente lancei a magia na minha casa inteira! Agora tem coisas duplicadas por todos os lados!",
        "Minha mamãe vai chegar logo, e eu prometi que ia deixar tudo arrumado...",
        "Mas sozinho eu não vou conseguir...",
        "Você pode me ajudar a encontrar os pares de objetos iguais e desfazer a mágica?"
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

            //if (fraseAtual == 5)
            //{
            //    iconePersonagem.sprite = sprite2;
            //}
        }
        else
        {
            historyPanel.SetActive(false);
            DialogoFinalizado?.Invoke();
        }
    }
}