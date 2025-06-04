using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class responder : MonoBehaviour
{

    public int idTema;

    public Text pergunta;
    public Text respostaA;
    public Text respostaB;
    public Text respostaC;
    public Text respostaD;


    public string[] perguntas; //Armazena todas as perguntas
    public string[] alternativaA; 
    public string[] alternativaB;
    public string[] alternativaC;
    public string[] alternativaD;
    public string[] corretas; //Armazena as alternativas corretas

    private int idPergunta;

    private float acertos;
    private float questoes;
    private float media;
    private int notaFinal;
    private int erros;

    // Start is called before the first frame update
    void Start()
    {
        idTema = PlayerPrefs.GetInt("idTema");
        idPergunta = 0;
        questoes = perguntas.Length;

        pergunta.text = perguntas[idPergunta];
        respostaA.text = alternativaA[idPergunta];
        respostaB.text = alternativaB[idPergunta];
        respostaC.text = alternativaC[idPergunta];
        respostaD.text = alternativaD[idPergunta];
    }

    public void resposta(string alternativa)
    {
        if(alternativa == "A"){
            if(alternativaA[idPergunta] == corretas[idPergunta]){
                acertos+= 100;
            } else {
                if(acertos > 0){
                    acertos -= 50;
                }
                erros++;
            }
        } else if(alternativa == "B"){
            if(alternativaB[idPergunta] == corretas[idPergunta]){
                acertos+= 100;
            } else {
                if(acertos > 0){
                    acertos -= 50;
                }
                erros++;
            }
        } else if(alternativa == "C"){
            if(alternativaC[idPergunta] == corretas[idPergunta]){
                acertos+= 100;
            } else {
                if(acertos > 0){
                    acertos -= 50;
                }
                erros++;
            }
        } else if(alternativa == "D"){
            if(alternativaD[idPergunta] == corretas[idPergunta]){
                acertos+= 100;
            } else {
                if(acertos > 0){
                    acertos -= 50;
                }
                erros++;
            }
        }

        proximaPergunta();
    }

    void proximaPergunta(){
        idPergunta += 1;

        if(idPergunta <= (questoes-1)){
        pergunta.text = perguntas[idPergunta];
        respostaA.text = alternativaA[idPergunta];
        respostaB.text = alternativaB[idPergunta];
        respostaC.text = alternativaC[idPergunta];
        respostaD.text = alternativaD[idPergunta];


        } else {

            media = 10 * (acertos/questoes);
            notaFinal = Mathf.RoundToInt(media);

            if(notaFinal > PlayerPrefs.GetInt("notaFinal"+idTema.ToString())){
                PlayerPrefs.SetInt("notaFinal"+idTema.ToString(), notaFinal);
                PlayerPrefs.SetInt("acertos"+idTema.ToString(), (int) acertos);
            }

            PlayerPrefs.SetInt("notaFinalTemp"+idTema.ToString(), notaFinal);
            PlayerPrefs.SetInt("acertosTemp"+idTema.ToString(), (int) acertos);

            SceneManager.LoadScene("notaFinal");
        }
    }

}