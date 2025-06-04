using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class notaFinal : MonoBehaviour
{
    private int idTema;

    public Text txtNota;
    public Text txtInfoNotaMaxima;

    public Image estrela1;
    public Image estrela2;
    public Image estrela3;

    public Sprite estrelaCheia;
    public Sprite estrelaVazia;

    private int notaF;
    private int acertos;
    private int notaMaxima;

    void Start()
    {
        idTema = PlayerPrefs.GetInt("idTema");

        notaF = PlayerPrefs.GetInt("notaFinalTemp" + idTema.ToString());
        notaMaxima = PlayerPrefs.GetInt("notaFinal" + idTema.ToString());
        acertos = PlayerPrefs.GetInt("acertosTemp" + idTema.ToString());

        txtNota.text = notaF.ToString() + "x";
        txtInfoNotaMaxima.text = notaMaxima.ToString() + "x";

        int estrelasObtidas = 0;

        if (notaF == 1000)
        {
            estrelasObtidas = 3;
        }
        else if (notaF >= 700)
        {
            estrelasObtidas = 2;
        }
        else if (notaF >= 550)
        {
            estrelasObtidas = 1;
        }
        else
        {
            estrelasObtidas = 0;
        }

        AtualizarEstrelas(estrelasObtidas);
    }

    void AtualizarEstrelas(int quantidade)
    {
        estrela1.sprite = quantidade >= 1 ? estrelaCheia : estrelaVazia;
        estrela2.sprite = quantidade >= 2 ? estrelaCheia : estrelaVazia;
        estrela3.sprite = quantidade >= 3 ? estrelaCheia : estrelaVazia;
    }

    public void jogarNovamente()
    {
        SceneManager.LoadScene("T" + idTema.ToString());
    }
}
