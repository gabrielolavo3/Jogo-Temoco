using UnityEngine;

public class PistaInterativa : MonoBehaviour
{
    public GameObject painelDoQuebraCabeca;
    public GameObject[] outrosPaineis;

    void OnMouseDown()
    {
        foreach (GameObject painel in outrosPaineis)
        {
            if (painel.activeSelf)
                painel.SetActive(false);
        }

        painelDoQuebraCabeca.SetActive(true);
        gameObject.SetActive(false);

        if (PontuacaoManager.Existe)
        {
            PontuacaoManager.instance.AdicionarPontos(100);
            PontuacaoManager.ignorarCliqueErrado = true;
        }
    }
}
