using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaConclusao : MonoBehaviour
{
    [Header("UI Elements")]
    public Text textoPontuacaoAtual;
    public Text textoMelhorPontuacao;

    void Start()
    {
        if (PontuacaoManager.instance == null)
        {
            Debug.LogError("[TelaConclusao] PontuacaoManager não encontrado!");
            return;
        }

        int pontuacaoAtual = PontuacaoManager.instance.pontuacaoAtual;
        AtualizarPontuacaoUI(pontuacaoAtual);

        // Chamada direta de desbloqueio
        DesbloqueioDeFases.ChecarDesbloqueio(SceneManager.GetActiveScene().name, pontuacaoAtual);
        Debug.Log("Chamou ChecarDesbloqueio na TelaConclusao");

        PontuacaoManager.instance.ConfirmarPontuacao();
    }

    void AtualizarPontuacaoUI(int pontuacaoAtual)
    {
        textoPontuacaoAtual.text = pontuacaoAtual.ToString() + "x";

        if (pontuacaoAtual > PlayerPrefs.GetInt("MelhorPontuacao", 0))
        {
            PlayerPrefs.SetInt("MelhorPontuacao", pontuacaoAtual);
        }

        textoMelhorPontuacao.text = PlayerPrefs.GetInt("MelhorPontuacao", 0).ToString() + "x";
    }
}
