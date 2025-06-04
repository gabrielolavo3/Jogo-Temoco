using UnityEngine;
using UnityEngine.UI;
using System;

public class CaixaDeDialogos : MonoBehaviour
{
    [Header("Referências da História")]
    [SerializeField] private GameObject historyPanel;
    [SerializeField] private Image iconePersonagem;
    [SerializeField] private Text textoDialogo;

    [Header("Imagens")]
    [SerializeField] private Sprite sprite1; // Mantido como private
    [SerializeField] private Sprite sprite2;

    // Propriedade pública para acessar sprite1
    public Sprite Sprite1 => sprite1;

    public event Action DialogoFinalizado;
    private int fraseAtual = 0;
    private string[] vetorFrases = new string[]
    {
        "Oi! Eu sou Dona Maria, sou sua vizinha, quer um biscoito meu filho?",
        "Bom Menino, meu filho vc poderia me ajudar em uma coisa?... é que... ah, estou muito triste...",
        "Minha gatinha branca sumiu hj de manhã! Ela é so um filhote e se perde o tempo td nesse quintal!",
        "Eu fico preocupada, tenho medo dela se machucar...",
        "Mas sozinho eu não vou conseguir...",
        "Você pode me ajudar a encontrar ela?"
    };

    void Awake()
    {
        // Verificação de segurança ao iniciar
        if (historyPanel == null)
            Debug.LogError("HistoryPanel não atribuído no Inspector!", this);

        if (iconePersonagem == null)
            Debug.LogError("iconePersonagem não atribuído no Inspector!", this);

        if (textoDialogo == null)
            Debug.LogError("textoDialogo não atribuído no Inspector!", this);
    }

    public void ExibirDialogo(Sprite sprite)
    {
        try
        {
            if (historyPanel == null || iconePersonagem == null || textoDialogo == null || sprite == null)
            {
                Debug.LogError("Tentativa de exibir diálogo com referências faltando!", this);
                return;
            }

            Time.timeScale = 0f;
            historyPanel.SetActive(true);
            fraseAtual = 0;
            textoDialogo.text = vetorFrases[fraseAtual];
            iconePersonagem.sprite = sprite;
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao exibir diálogo: {e.Message}", this);
        }
    }

    public void ProximaFrase()
    {
        if (!historyPanel.activeSelf) return;

        fraseAtual++;

        if (fraseAtual < vetorFrases.Length)
        {
            textoDialogo.text = vetorFrases[fraseAtual];
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void FinalizarDialogo()
    {
        historyPanel.SetActive(false);
        Time.timeScale = 1f;
        DialogoFinalizado?.Invoke();
    }
}