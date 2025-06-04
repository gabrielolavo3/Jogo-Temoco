using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleFase : MonoBehaviour
{
    public Button botaoFase2;
    public Button botaoFase3;

    void Start() => AtualizarBotoes();
    void OnEnable() => AtualizarBotoes();

    public void Fase1() => CarregarFase("Fase1");
    public void Fase2() => CarregarFase("Fase2");
    public void Fase3() => CarregarFase("Fase3");

    void CarregarFase(string nomeCena)
    {
        // Antes de carregar a fase, salva o nome para o AutoConclusao
        PlayerPrefs.SetString("FaseAnterior", nomeCena);
        PlayerPrefs.Save();

        // Reseta a pontuação temporária
        PontuacaoManager.instancia.ResetarPontuacao();

        // Carrega a cena da fase
        SceneManager.LoadScene(nomeCena);

        Debug.Log($"[ControleFase] Fase carregada: {nomeCena}");
    }

    void AtualizarBotoes()
    {
        botaoFase2.interactable = DesbloqueioDeFases.Fase2EstaLiberada();
        botaoFase3.interactable = DesbloqueioDeFases.Fase3EstaLiberada();

        Debug.Log($"[ControleFase] Botões atualizados - Fase2: {botaoFase2.interactable} | Fase3: {botaoFase3.interactable}");
    }
}
