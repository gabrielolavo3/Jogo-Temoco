using UnityEngine;

public class GerenciadorDeJogo : MonoBehaviour
{
    public static GerenciadorDeJogo instance;

    private void Awake()
    {
        // Faz esse objeto não destruir entre cenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Guarda o nome da fase atual
    public void GuardarFaseAtual(string nomeDaFase)
    {
        PlayerPrefs.SetString("UltimaFase", nomeDaFase);
    }
}
