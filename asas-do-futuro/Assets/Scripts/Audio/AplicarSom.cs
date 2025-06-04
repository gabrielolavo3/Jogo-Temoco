using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AplicarSom : MonoBehaviour
{
    [Header("Áudio do objeto")]
    public AudioClip somDoClique;

    private Button botao;

    private void Awake()
    {
        botao = GetComponent<Button>();

        if (botao != null)
        {
            botao.onClick.AddListener(TocarSom);
        }
    }

    private void TocarSom()
    {
        if (SonsFX.instanciaFX != null && somDoClique != null)
        {
            SonsFX.instanciaFX.TocarEfeito(somDoClique);
        }
    }
}
