using UnityEngine;
using UnityEngine.UI;

public class TelaFases : MonoBehaviour
{
    [Header("Botões")]
    public Button botaoFase2;
    public Button botaoFase3;

    [Header("Configuração")]
    public bool usarDebug = true;

    void Start() => AtualizarBotoes();
    void OnEnable() => AtualizarBotoes();

    public void AtualizarBotoes()
    {
        bool fase2Liberada = DesbloqueioDeFases.Fase2EstaLiberada();
        bool fase3Liberada = DesbloqueioDeFases.Fase3EstaLiberada();

        botaoFase2.interactable = fase2Liberada;
        botaoFase3.interactable = fase3Liberada;

        if (usarDebug)
        {
            Debug.Log($"[TelaFases] Fase2: {fase2Liberada} | Fase3: {fase3Liberada}");
            Debug.Log($"[TelaFases] Chaves PlayerPrefs - Fase2: {PlayerPrefs.GetInt("Fase2Liberada_V3")} | Fase3: {PlayerPrefs.GetInt("Fase3Liberada_V3")}");
        }
    }

    [ContextMenu("Forçar Atualização")]
    private void DebugAtualizarBotoes()
    {
        AtualizarBotoes();
        Debug.Log("[TelaFases] Botões atualizados manualmente!");
    }
}
