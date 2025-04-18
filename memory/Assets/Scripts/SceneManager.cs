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

    public void IniciarJogo()
    {        
        StartCoroutine(TempoParaInciaJogo());
    }

    private IEnumerator TempoParaInciaJogo()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }
}
