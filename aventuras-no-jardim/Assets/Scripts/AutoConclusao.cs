using UnityEngine;

public class AutoConclusao : MonoBehaviour
{
    void Start()
    {
        string faseAnterior = PlayerPrefs.GetString("FaseAnterior", "");
        int pontos = PontuacaoManager.instancia.pontuacaoAtual;

        Debug.Log($"[AutoConclusao] Fase anterior: {faseAnterior} | Pontos: {pontos}");

        // Checa desbloqueio
        DesbloqueioDeFases.ChecarDesbloqueio(faseAnterior, pontos);

        // Salva pontuação
        PontuacaoManager.instancia.ConfirmarPontuacao();

        Debug.Log("[AutoConclusao] Desbloqueio e salvamento concluídos.");
    }
}
