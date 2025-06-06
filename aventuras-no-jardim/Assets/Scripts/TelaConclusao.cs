using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaConclusao : MonoBehaviour
{
    [Header("UI Elements")]
    public Text textoPontuacaoAtual;
    public Text textoMelhorPontuacao;

    [Header("Estrelas")]
    public Image estrela1;
    public Image estrela2;
    public Image estrela3;
    public Sprite estrelaApagada;
    public Sprite estrelaDourada;

    void Start()
    {
        if (PontuacaoManager.instance == null)
        {
            Debug.LogError("[TelaConclusao] PontuacaoManager não encontrado!");
            return;
        }

        int pontuacaoAtual = PontuacaoManager.instance.pontuacaoAtual;
        AtualizarPontuacaoUI(pontuacaoAtual);

        // Atualiza as estrelas com base na pontuação e fase
        string fase = PlayerPrefs.GetString("FaseAnterior", "Fase1");
        int numEstrelas = CalcularEstrelas(fase, pontuacaoAtual);
        AtualizarEstrelas(numEstrelas);

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

    int CalcularEstrelas(string fase, int pontos)
    {
        if (fase == "Fase1")
        {
            if (pontos >= 300) return 3;
            if (pontos >= 200) return 2;
            if (pontos >= 100) return 1;
        }
        else if (fase == "Fase2")
        {
            if (pontos >= 400) return 3;
            if (pontos >= 300) return 2;
            if (pontos >= 150) return 1;
        }
        else if (fase == "Fase3")
        {
            if (pontos >= 500) return 3;
            if (pontos >= 350) return 2;
            if (pontos >= 200) return 1;
        }
        return 0;
    }

    void AtualizarEstrelas(int numEstrelas)
    {
        estrela1.sprite = numEstrelas >= 1 ? estrelaDourada : estrelaApagada;
        estrela2.sprite = numEstrelas >= 2 ? estrelaDourada : estrelaApagada;
        estrela3.sprite = numEstrelas >= 3 ? estrelaDourada : estrelaApagada;

        Debug.Log($"Estrelas atualizadas: {numEstrelas} estrelas");
    }
}
