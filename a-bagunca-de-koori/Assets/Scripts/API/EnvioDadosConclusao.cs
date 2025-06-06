using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JogoData
{
    public string nome;  // Corrigido: nomeJogo → nome
    public string descricao;
    public int estrelas;
    public int pontuacaoTotal;
    public float tempoJogado;
    public string dificuldadeTrabalhada;
    public DadosEspecificos dadosEspecificos;

    // Mantém "progresso" local se for necessário
    // [System.NonSerialized] ou simplesmente não incluir no envio
    // public int progresso;
}

[System.Serializable]
public class DadosEspecificos
{
    public int JaVisualizouHistoria;
    public int FaseDesbloqueada;
    public int LevelColumns;
    public int LevelRows;
    public float LevelSpacing;
    public int LevelDifficulty;

    public int MelhorPontuacaoFase1;
    public int MelhorPontuacaoFase2;
    public int MelhorPontuacaoFase3;

    public int AcessosFase1;
    public int AcessosFase2;
    public int AcessosFase3;
}

public class EnvioDadosConclusao : MonoBehaviour
{
    private string nomeDoJogo = "A Bagunça de Koori";
    private string descricaoDoJogo = "Um jogo educativo sobre organização e memorização.";
    public string dificuldadeFocada = "Memória";
  
    private string apiUrl = "http://localhost:3000/api/jogo/criar_atualizar";

    void Start()
    {
        StartCoroutine(EnviarDadosParaAPI());
    }

    IEnumerator EnviarDadosParaAPI()
    {
        yield return new WaitForSeconds(0.5f);

        int idFase = PlayerPrefs.GetInt("FaseAtual", 1);
        // int estrelas = PlayerPrefs.GetInt(chaveEstrelas, 0);
        int estrelas = PlayerPrefs.GetInt("EstrelasAcumuladasGeral", 0);
        int pontuacaoTotal = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);
        float tempoJogado = Mathf.Round(PlayerPrefs.GetFloat("TempoJogadoSalvo", 0f) / 36f) / 100f;

        DadosEspecificos especificos = new DadosEspecificos();
        especificos.JaVisualizouHistoria = PlayerPrefs.GetInt("JaVisualizouHistoria", 0);
        especificos.FaseDesbloqueada = PlayerPrefs.GetInt("FaseDesbloqueada", 1);
        especificos.LevelColumns = PlayerPrefs.GetInt("LevelColumns", 0);
        especificos.LevelRows = PlayerPrefs.GetInt("LevelRows", 0);
        especificos.LevelSpacing = PlayerPrefs.GetFloat("LevelSpacing", 0f);
        especificos.LevelDifficulty = PlayerPrefs.GetInt("LevelDifficulty", 0);
        especificos.MelhorPontuacaoFase1 = PlayerPrefs.GetInt("MelhorPontuacaoFase1", 0);
        especificos.MelhorPontuacaoFase2 = PlayerPrefs.GetInt("MelhorPontuacaoFase2", 0);
        especificos.MelhorPontuacaoFase3 = PlayerPrefs.GetInt("MelhorPontuacaoFase3", 0);
        especificos.AcessosFase1 = PlayerPrefs.GetInt("AcessosFase1", 0);
        especificos.AcessosFase2 = PlayerPrefs.GetInt("AcessosFase2", 0);
        especificos.AcessosFase3 = PlayerPrefs.GetInt("AcessosFase3", 0);

        JogoData dados = new JogoData
        {
            nome = nomeDoJogo,  // Corrigido
            descricao = descricaoDoJogo,
            estrelas = estrelas,
            pontuacaoTotal = pontuacaoTotal,
            tempoJogado = tempoJogado,
            dificuldadeTrabalhada = dificuldadeFocada,
            dadosEspecificos = especificos
        };

        string json = JsonUtility.ToJson(dados);
        Debug.Log("JSON Final: " + json);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Dados enviados com sucesso para a API.");
        }
        else
        {
            Debug.LogError("Erro ao enviar dados para API: " + request.error);
        }
    }
}
