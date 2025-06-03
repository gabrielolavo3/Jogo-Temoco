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
        "Oi! Eu sou a Koori, a aprendiz de magia do Protetor da Floresta!",
        "Hoje, eu tava treinando uma magia de duplicar itens super complicada... e... ah, eu nem acredito...",
        "Acidentalmente lancei a magia no meu quarto inteiro! Agora tem coisas repetidas por todos os lados!",
        "Meus pais podem voltar para casa logo, e eu prometi que ia deixar tudo arrumado...",
        "Mas sozinha, eu nunca vou conseguir...",
        "Pode me ajudar a encontrar os pares de objetos iguais e desfazer a magia?"
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