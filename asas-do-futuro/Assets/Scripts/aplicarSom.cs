using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aplicarSom : MonoBehaviour
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
        if (sonsFX.instanciaFX != null && somDoClique != null)
        {
            sonsFX.instanciaFX.TocarEfeito(somDoClique);
        }
    }
}