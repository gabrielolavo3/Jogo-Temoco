// EnvioDadosConclusao.cs usando JsonUtility e classes fixas
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JogoData
{
    public int idUsuario;
    public string nomeJogo;
    public int estrelas;
    public int progresso;
    public int pontuacaoTotal;
    public float tempoJogado;
    public DadosEspecificos dadosEspecificos;
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
    private string apiUrl = "http://localhost:3000/api/jogo/salvar";

    void Start()
    {
        StartCoroutine(EnviarDadosParaAPI());
    }

    IEnumerator EnviarDadosParaAPI()
    {
        yield return new WaitForSeconds(0.5f); // Garante tempo para salvar PlayerPrefs

        string token = PlayerPrefs.GetString("token", "");

        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("Token de autenticação não encontrado. Envio cancelado.");
            yield break;
        }

        int idFase = PlayerPrefs.GetInt("FaseAtual", 1);
        string chaveEstrelas = "EstrelasFase" + idFase;
        int estrelas = PlayerPrefs.GetInt(chaveEstrelas, 0);
        int progresso = PlayerPrefs.GetInt("Conclusao", 0);
        int pontuacaoTotal = PlayerPrefs.GetInt("UltimaPontuacaoTotal", 0);
        float tempoJogado = Mathf.Round(PlayerPrefs.GetFloat("TempoJogadoSalvo", 0f) / 36f) / 100f; // Arredonda para 2 dígitos
        int idUsuario = PlayerPrefs.GetInt("idUsuario", 0);

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
            idUsuario = idUsuario,
            nomeJogo = nomeDoJogo,
            estrelas = estrelas,
            progresso = progresso,
            pontuacaoTotal = pontuacaoTotal,
            tempoJogado = tempoJogado,
            dadosEspecificos = especificos
        };

        string json = JsonUtility.ToJson(dados);
        Debug.Log("JSON Final: " + json);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + token);

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
