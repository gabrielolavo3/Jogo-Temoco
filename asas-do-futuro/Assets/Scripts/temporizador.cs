using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporizador : MonoBehaviour
{
    private pauseBox pauseBox;
    private float tempoAtualDeJogo = 0f;
    private float tempoTotalJogado = 0f;
    private bool temporizadorAtivo = false;

    void Start()
    {
        ReconfigurarTodoTemporizador();       
        temporizadorAtivo = true;
        pauseBox = FindObjectOfType<pauseBox>();
        tempoAtualDeJogo = 0f;
        tempoTotalJogado = PlayerPrefs.GetFloat("TempoJogadoSalvo", 0f); 
    }
    
    void Update()
    {
        if (temporizadorAtivo && !pauseBox.jogoEstaPausado)
        {
            tempoAtualDeJogo += Time.deltaTime;
            Debug.Log(FormatarTempoTotal());
        }
    }

    public void PausarTemporizador()
    {
        temporizadorAtivo = false;
        SalvarTempo();
    }

    public void RetomarTemporizador()
    {
        temporizadorAtivo = true;        
    }

    public void ReconfigurarTodoTemporizador()
    {
        tempoAtualDeJogo = 0f;
        tempoTotalJogado = 0f;
        PlayerPrefs.SetFloat("TempoJogadoSalvo", 0f);
        PlayerPrefs.Save();
    }

    public void SalvarTempo()
    {
        //_tempoTotalJogado = PlayerPrefs.GetFloat("TempoJogadoSalvo", 0f);
        tempoTotalJogado += tempoAtualDeJogo;
        PlayerPrefs.SetFloat("TempoJogadoSalvo", tempoTotalJogado);
        PlayerPrefs.Save();

        tempoAtualDeJogo = 0f;
    }

    public float PegarTempoTotal()
    {
        return tempoTotalJogado + tempoAtualDeJogo;
    }

    // Método para formatar o tempo em horas:minutos:segundos
    public string FormatarTempoTotal()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(PegarTempoTotal());
        string tempo = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            timeSpan.Hours,
                            timeSpan.Minutes,
                            timeSpan.Seconds);

        return tempo;
    }
}