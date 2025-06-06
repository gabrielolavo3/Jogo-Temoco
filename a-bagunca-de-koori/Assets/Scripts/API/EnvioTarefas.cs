using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class MissaoData
{
    public string nomeTarefa;
    public string descricao;
    public string estadoConclusao;
    public string nomeJogo;  // Agora enviamos o nome do jogo, não mais o ID
}

public class EnvioTarefas : MonoBehaviour
{
    public string nomeDoJogo = "A Bagunça de Koori"; // Nome do jogo que será enviado
    private string baseURL = "http://localhost:3000/api/jogo/missao/criar_atualizar"; // Ajuste conforme necessário    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnviarTodasAsMissoes());
    }

    IEnumerator EnviarTodasAsMissoes()
    {
        yield return new WaitForSeconds(0.5f);

        // Missão 1 - Koori
        MissaoData missao1 = new MissaoData
        {
            nomeTarefa = PlayerPrefs.GetString("MissaoKoori_Nome", "Missao Koori"),
            descricao = PlayerPrefs.GetString("MissaoKoori_Descricao", "Descrição Koori"),
            estadoConclusao = PlayerPrefs.GetString("EstadoMissaoVisualizarHistoria_Texto", "NÃO INICIADO"),
            nomeJogo = nomeDoJogo
        };
        StartCoroutine(PostMissao(missao1));

        // Missão 2 - Tanuki
        MissaoData missao2 = new MissaoData
        {
            nomeTarefa = PlayerPrefs.GetString("MissaoTanuki_Nome", "Missao Tanuki"),
            descricao = PlayerPrefs.GetString("MissaoTanuki_Descricao", "Descrição Tanuki"),
            estadoConclusao = PlayerPrefs.GetString("EstadoMissaoTanuki_Texto", "NÃO INICIADO"),
            nomeJogo = nomeDoJogo
        };
        StartCoroutine(PostMissao(missao2));

        // Missão 3 - Pequenas Vitórias
        MissaoData missao3 = new MissaoData
        {
            nomeTarefa = PlayerPrefs.GetString("MissaoVitorias_Nome", "Missao Pequenas Vitorias"),
            descricao = PlayerPrefs.GetString("MissaoVitorias_Descricao", "Descrição Pequenas Vitorias"),
            estadoConclusao = PlayerPrefs.GetString("EstadoMissaoPequenasVitorias_Texto", "NÃO INICIADO"),
            nomeJogo = nomeDoJogo
        };
        StartCoroutine(PostMissao(missao3));
    }

    private IEnumerator PostMissao(MissaoData missao)
    {
        string jsonData = JsonUtility.ToJson(missao);

        UnityWebRequest request = new UnityWebRequest(baseURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Enviando missão: " + missao.nomeTarefa + " | Estado: " + missao.estadoConclusao);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Missão enviada com sucesso: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Erro ao enviar missão: " + request.error);
        }
    }
}
