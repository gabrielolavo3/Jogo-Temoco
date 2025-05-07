using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    public void JogarNovamente(string nome_cena)
    {        
        SceneManager.LoadScene(nome_cena);
    }

    public void CarregarTelaInicial()
    {
        Debug.Log("Carregando a tela inicial....");        
    }

    public void IniciarJogo(string nome_cena)
    {        
        StartCoroutine(TempoParaInciaJogo(nome_cena));
    }

    private IEnumerator TempoParaInciaJogo(string nome_cena)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nome_cena);
    }
}
