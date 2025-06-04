using UnityEngine;
using UnityEngine.UI;

public class ControleTravaFase : MonoBehaviour
{
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;

    void Start()
    {
        AtualizarTravas();
    }

    void AtualizarTravas()
    {
        botaoFase1.interactable = true;
        botaoFase2.interactable = DesbloqueioDeFases.Fase2EstaLiberada();
        botaoFase3.interactable = DesbloqueioDeFases.Fase3EstaLiberada();

        Debug.Log($"Travas atualizadas - Fase2: {botaoFase2.interactable} | Fase3: {botaoFase3.interactable}");
    }
}
