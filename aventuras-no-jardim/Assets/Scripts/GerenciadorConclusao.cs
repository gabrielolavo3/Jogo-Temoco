using UnityEngine;
using UnityEngine.SceneManagement; // Importante!

public class TelaDeConclusao : MonoBehaviour
{
    // Função para voltar pra seleção de fases
    public void TelaDeFases()
    {
        SceneManager.LoadScene("TelaFases"); // Nome da sua cena de seleção de fases
    }

    // Função para reiniciar a fase
    public void ReiniciarFase()
    {
        string ultimaFase = PlayerPrefs.GetString("UltimaFase");
        SceneManager.LoadScene(ultimaFase);
    }
}
