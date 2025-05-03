using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class LevelData
{
    [Header("Configurações de Fase")]
    public int Colunas;
    public int Linhas;
    public float Espacamento;
    public int Pares;
}

public class LevelController : MonoBehaviour
{        
    public float tempoMemorizacao;
    public Temporizador temporizador;
    [HideInInspector] public bool jogoConcluido = false;
    
    private bool cliqueBloqueado = true;
    private LevelData configFaseAtual;
    private CardController cartaSelecionada;
    private ScoreManager scoreManager;
    private List<CardController> listaCartoes = new List<CardController>();
    [SerializeField] private CardController prefabCarta;
        
    void Start()
    {
        configFaseAtual = new LevelData
        {
            Colunas = PlayerPrefs.GetInt("LevelColumns", 4),
            Linhas = PlayerPrefs.GetInt("LevelRows", 3),
            Espacamento = PlayerPrefs.GetFloat("LevelSpacing", 0.5f),
            Pares = PlayerPrefs.GetInt("LevelDifficulty", 3)
        };

        if (temporizador != null)
        {
            temporizador.ReconfigurarTodoTemporizador();
        }

        StartLevel();
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.ReconfigurarPlacares();
    }

    public void StartLevel()
    {
        Debug.Assert((configFaseAtual.Linhas * configFaseAtual.Colunas) % 2 == 0);

        if (configFaseAtual.Pares > prefabCarta.conteudoMaxCartas)
        {
            configFaseAtual.Pares = Math.Min(configFaseAtual.Pares, prefabCarta.conteudoMaxCartas);
            Debug.Assert(false);
        }

        listaCartoes.ForEach(c => Destroy(c.gameObject));
        listaCartoes.Clear();

        List<int> cartasDisponiveis = new List<int>();
        for (int a = 0; a < prefabCarta.conteudoMaxCartas; a++)
        {
            cartasDisponiveis.Add(a);
        }

        List<int> cartasDaPartida = new List<int>();

        for (int i = 0; i < (configFaseAtual.Linhas * configFaseAtual.Colunas) / 2; i++)
        {
            if (cartasDisponiveis.Count == 0)
            {
                Debug.LogError("Não há tipos suficientes de cartas para o número de pares!");
                break;
            }

            int chosenType = cartasDisponiveis[UnityEngine.Random.Range(0, cartasDisponiveis.Count)];
            cartasDisponiveis.Remove(chosenType);
            cartasDaPartida.Add(chosenType);
        }

        List<int> paresDeCartas = new List<int>();

        foreach (var indexPar in cartasDaPartida)
        {
            paresDeCartas.Add(indexPar);
            paresDeCartas.Add(indexPar);
        }

        EmbaralharListaDeCartas(paresDeCartas);

        Vector3 offset = new Vector3((configFaseAtual.Colunas - 1) * (prefabCarta.tamanhoDoCartao + configFaseAtual.Espacamento), (configFaseAtual.Linhas - 1) * (prefabCarta.tamanhoDoCartao + configFaseAtual.Espacamento), 0f) * 0.5f;

        for (int linhas = 0; linhas < configFaseAtual.Linhas; linhas++)
        {
            for (int colunas = 0; colunas < configFaseAtual.Colunas; colunas++)
            {
                Vector3 posicaoCartao = new Vector3(colunas * (prefabCarta.tamanhoDoCartao + configFaseAtual.Espacamento), linhas * (prefabCarta.tamanhoDoCartao + configFaseAtual.Espacamento), 0f);
                var carta = Instantiate(prefabCarta, posicaoCartao - offset, Quaternion.identity);
                carta.conteudoDoCartao = paresDeCartas[0];
                paresDeCartas.RemoveAt(0);
                carta.cartaClicada.AddListener(AoClicarNaCarta);
                listaCartoes.Add(carta);
            }
        }

        jogoConcluido = false;
        ReconfigurarInteracaoDeCarta(false);
        cartaSelecionada = null;
        StartCoroutine(MostrarCartasTemporariamente());
    }

    private void EmbaralharListaDeCartas(List<int> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, lista.Count);
            (lista[i], lista[randomIndex]) = (lista[randomIndex], lista[i]);
        }
    }

    private IEnumerator MostrarCartasTemporariamente()
    {
        foreach (var carta in listaCartoes)
        {
            carta.RevelarCarta();
        }

        yield return new WaitForSeconds(tempoMemorizacao);

        foreach (var carta in listaCartoes)
        {
            carta.EsconderCarta();
        }

        ReconfigurarInteracaoDeCarta(true);
        cliqueBloqueado = false;
    }

    private void AoClicarNaCarta(CardController carta)
    {
        if (cliqueBloqueado || carta == cartaSelecionada)
        {
            return;
        }

        cliqueBloqueado = true;

        if (cartaSelecionada == null)
        {
            StartCoroutine(SelecionarCarta(carta));
            return;
        }

        if (carta.conteudoDoCartao == cartaSelecionada.conteudoDoCartao)
        {
            StartCoroutine(CartasConvergentes(carta));
            return;
        }

        StartCoroutine(ParIncorreto(carta));
    }

    private IEnumerator SelecionarCarta(CardController carta)
    {
        cartaSelecionada = carta;
        cartaSelecionada.RevelarCarta();
        yield return new WaitForSeconds(0.5f);
        cliqueBloqueado = false;
    }

    private IEnumerator CartasConvergentes(CardController carta)
    {
        carta.RevelarCarta();
        yield return new WaitForSeconds(1f);
        listaCartoes.Remove(cartaSelecionada);
        listaCartoes.Remove(carta);
        Destroy(carta.gameObject);
        Destroy(cartaSelecionada.gameObject);
        cartaSelecionada = null;
        scoreManager.AdicionarAcerto();

        if (listaCartoes.Count < 1)
        {
            Vitoria();
            yield break;
        }

        cliqueBloqueado = false;
    }

    private IEnumerator ParIncorreto(CardController carta)
    {
        carta.RevelarCarta();
        yield return new WaitForSeconds(1f);
        cartaSelecionada.EsconderCarta();
        carta.EsconderCarta();
        cartaSelecionada = null;
        scoreManager.AdicionarErro();

        yield return new WaitForSeconds(0.5f);
        cliqueBloqueado = false;
    }

    private void Vitoria()
    {
        Debug.Log("Victory");

        jogoConcluido = true;
        scoreManager.SalvarPontuacao();
        ReconfigurarInteracaoDeCarta(false);
        temporizador.PausarTemporizador();

        StartCoroutine(TransitionFinalScene());
    }

    private IEnumerator TransitionFinalScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("FinalScene");
    }

    private void ReconfigurarInteracaoDeCarta(bool interagivel)
    {
        foreach (var carta in listaCartoes)
        {
            carta.InteracaoPermitida = interagivel;
        }
    }
}