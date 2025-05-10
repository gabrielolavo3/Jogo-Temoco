using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvaliacoesFase", menuName = "Dados de Fase/PontuacaoConfigFase")]
public class PontuacaoConfigFase : ScriptableObject
{
    public int idFase;
    public int pontosMinimos;
    public int pontosIntermadiarios;
    public int pontosMaximos;
}
