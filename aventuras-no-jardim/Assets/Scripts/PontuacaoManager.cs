using UnityEngine;

public class PontuacaoManager : MonoBehaviour
{
    public static PontuacaoManager instance => _instance;
    public static PontuacaoManager instancia => _instance;
    private static PontuacaoManager _instance;

    public static bool ignorarCliqueErrado = false;
    private const int PONTUACAO_MAXIMA = 1000;

    private int _pontos = 0;
    private int _pontosTemporarios = 0;

    public static bool Existe => _instance != null;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        CarregarProgresso();
    }

    public void AdicionarPontos(int valor)
    {
        _pontos = Mathf.Min(_pontos + valor, PONTUACAO_MAXIMA);
        _pontosTemporarios = _pontos;
        Debug.Log($"[Pontuacao] +{valor} pts | Total: {_pontos}");
    }

    public void RemoverPontos(int valor)
    {
        _pontos = Mathf.Max(0, _pontos - valor);
        _pontosTemporarios = _pontos;
        Debug.Log($"[Pontuacao] -{valor} pts | Total: {_pontos}");
    }

    public void ResetarPontuacao()
    {
        _pontos = 0;
        _pontosTemporarios = 0;
        Debug.Log("[Pontuacao] Pontuação resetada");
    }

    private void CarregarProgresso()
    {
        _pontos = PlayerPrefs.GetInt("PontuacaoTemp", 0);
        _pontosTemporarios = _pontos;
        Debug.Log($"[Pontuacao] Progresso carregado: {_pontos} pts");
    }

    public void ConfirmarPontuacao()
    {
        PlayerPrefs.SetInt("PontuacaoTemp", _pontos);
        PlayerPrefs.Save();
        Debug.Log($"[Pontuacao] Progresso salvo: {_pontos} pts");
    }

    public void DescartarPontuacao()
    {
        _pontos = _pontosTemporarios;
        Debug.Log("[Pontuacao] Alterações descartadas");
    }

    public int pontuacaoAtual => _pontos;

    [ContextMenu("Debug Pontuação")]
    private void DebugPontuacao()
    {
        Debug.Log($"Pontos Atuais: {_pontos} | Temporários: {_pontosTemporarios} | Salvos: {PlayerPrefs.GetInt("PontuacaoTemp")}");
    }
}
