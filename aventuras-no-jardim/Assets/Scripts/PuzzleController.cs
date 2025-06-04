using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleController : MonoBehaviour
{
    public GameObject painelDoQuebraCabeca; // Painel do quebra-cabeça
    public GameObject[] pecas; // Peças do quebra-cabeça
    public GameObject proximaPista; // Nova pista ou painel
    public bool painelFinal = false; // Indica se este é o último painel

    private bool resolvido = false;

    void Start()
    {
        // Quando o painel do quebra-cabeça aparece, marca como aberto
        EstadoDoJogo.painelAberto = true;
    }

    void Update()
    {
        if (resolvido) return;

        int encaixadas = 0;

        foreach (GameObject peca in pecas)
        {
            if (peca.GetComponent<ArrastarPecaUI>().encaixada)
            {
                encaixadas++;
            }
        }

        if (encaixadas == pecas.Length)
        {
            resolvido = true;
            StartCoroutine(FecharPainelDepoisDeTempo());
        }
    }

    IEnumerator FecharPainelDepoisDeTempo()
    {
        yield return new WaitForSeconds(3f);

        painelDoQuebraCabeca.SetActive(false);
        PlayerMovement.jogadorPodeMover = true;

        EstadoDoJogo.painelAberto = false;  // FECHOU o painel!

        // Libera a próxima pista ou painel
        if (proximaPista != null)
        {
            proximaPista.SetActive(true);
        }

        // Só vai para Tela de Conclusão se for o último painel
        if (painelFinal)
        {
            FinalizarQuebraCabeca();
        }

        this.enabled = false;
    }

    public void FinalizarQuebraCabeca()
    {
        EstadoDoJogo.painelAberto = false;  // Garante que está fechado ao mudar de cena

        // Vai para a Tela de Conclusão
        SceneManager.LoadScene("TelaConclusao");
    }
}
