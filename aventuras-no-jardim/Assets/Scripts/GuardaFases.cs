using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardaNomeDaFase : MonoBehaviour
{
    void Start()
    {
        // Pega o nome da cena atual e guarda
        GerenciadorDeJogo.instance.GuardarFaseAtual(SceneManager.GetActiveScene().name);
    }
}
