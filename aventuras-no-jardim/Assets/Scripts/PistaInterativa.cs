using UnityEngine;

public class PistaInterativa : MonoBehaviour
{
    public GameObject painelDoQuebraCabeca;
    public GameObject[] outrosPaineis;

    public string nomeDaPista; // Ex: "galho", "pegadas", "gato"

    void OnMouseDown()
    {
        // Fecha outros painéis
        foreach (GameObject painel in outrosPaineis)
        {
            if (painel.activeSelf)
                painel.SetActive(false);
        }

        // Abre o painel desta pista
        painelDoQuebraCabeca.SetActive(true);

        // Desativa o objeto da pista
        gameObject.SetActive(false);

        if (PontuacaoManager.Existe)
        {
            // Adiciona pontuação
            PontuacaoManager.instance.AdicionarPontos(100);
            PontuacaoManager.ignorarCliqueErrado = true;

            // Marca missão oculta: Encontrou pista
            MissoesOcultas.EncontrouPista(nomeDaPista);
        }
    }
}

