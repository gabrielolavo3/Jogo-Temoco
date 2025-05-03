using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixaDeDialogos : MonoBehaviour
{
    [Header("Refer�ncias da Hist�ria")]
    public GameObject historyPanel;
    public Image iconePersonagem;
    public Text textoDialogo;

    [Header("Imagens")]
    public Sprite sprite1;
    public Sprite sprite2;

    private int fraseAtual = 0;
    private string[] vetorFrases = new string[]
    {
        "Oi! Eu sou o Koori, o aprendiz de magia do Guardi�o da Floresta!",
        "Hoje, eu tava treinando um feiti�o de duplica��o super dif�cil... e... ah, voc� n�o vai acreditar...",
        "Eu acidentalmente lancei a magia na minha casa inteira! Agora tem coisas duplicadas por todos os lados!",
        "Minha mam�e vai chegar logo, e eu prometi que ia deixar tudo arrumado...",
        "Mas sozinho eu n�o vou conseguir...",
        "Voc� pode me ajudar a encontrar os pares de objetos iguais e desfazer a m�gica?"
    };

    void Start()
    {
        iconePersonagem.sprite = sprite1;
        ExibirDialogo();
    }

    public void ExibirDialogo()
    {
        Time.timeScale = 0f;
        historyPanel.SetActive(true);
        fraseAtual = 0;
        textoDialogo.text = vetorFrases[fraseAtual];
    }

    public void ProximaFrase()
    {
        fraseAtual++;

        if (fraseAtual < vetorFrases.Length)
        {
            textoDialogo.text = vetorFrases[fraseAtual];

            if (fraseAtual == 5)
            {
                iconePersonagem.sprite = sprite2;
            }
        }
        else
        {
            historyPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
