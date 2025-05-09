using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloqueioFase : MonoBehaviour
{
    public GameObject infoBloqueioFase;
    public Text tituloText;
    public Text requisitosFasesText;
    public Button cancelarBtn;
    private int pontuacaoMinimaFase1 = 300;
    private int pontuacaoMinimaFase2 = 500;

    void Start()
    {
        infoBloqueioFase.SetActive(false);        
    }

    public void MostrarInformacaoBloqueio(int indexFaseBloqueada)
    {
        infoBloqueioFase.SetActive(true);

        if (indexFaseBloqueada == 2)
        {
            tituloText.text = "Fase 2 Bloqueada";
            requisitosFasesText.text = "Complete a Fase 1 com pelo menos " + pontuacaoMinimaFase1 + " pontos.";
        }
        else if (indexFaseBloqueada == 3)
        {
            tituloText.text = "Fase 3 Bloqueada";
            requisitosFasesText.text = "Complete a Fase 2 com pelo menos " + pontuacaoMinimaFase2 + " pontos.";
        }
    }

    public void DesativarPopUp()
    {
        infoBloqueioFase.SetActive(false);
    }
}
