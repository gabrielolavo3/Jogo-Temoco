using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{    
    [Serializable] public class LevelData
    {
        [Header("Configurações da Dificuldade")]
        public int Columns;
        public int Rows;
        public float Spacing;
        public int Difficulty;
    }

    private List<CardController> _cards = new List<CardController>();

    [SerializeField] private CardController _cardPrefab;
    //[SerializeField] private List<LevelData> _levels = new List<LevelData>();

    private CardController _activeCard;
    private ScoreManager _scoreManager;
    private LevelData _currentLevel;
    private bool _blockInput = true;
    //private int _level = 0;
    private bool _gameEnded = false;

    void Start()
    {
        //_level = PlayerPrefs.GetInt("Level", 0);

        _currentLevel = new LevelData
        {
            Columns = PlayerPrefs.GetInt("LevelColumns", 4),
            Rows = PlayerPrefs.GetInt("LevelRows", 3),
            Spacing = PlayerPrefs.GetFloat("LevelSpacing", 0.5f),
            Difficulty = PlayerPrefs.GetInt("LevelDifficulty", 3)
        };

        StartLevel();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _scoreManager.ReconfigurarPlacares();
    }

    public void StartLevel()
    {
        Debug.Assert((_currentLevel.Rows * _currentLevel.Columns) % 2 == 0);

        if (_currentLevel.Difficulty > _cardPrefab.maxCardTypes)
        {
            _currentLevel.Difficulty = Math.Min(_currentLevel.Difficulty, _cardPrefab.maxCardTypes);
            Debug.Assert(false);
        }

        _cards.ForEach(c => Destroy(c.gameObject));
        _cards.Clear();

        List<int> allTypes = new List<int>();
        for (int a = 0; a < _cardPrefab.maxCardTypes; a++)
        {
            allTypes.Add(a);
        }

        List<int> gameTypes = new List<int>();

        for (int i = 0; i < (_currentLevel.Rows * _currentLevel.Columns) / 2; i++)
        {
            if (allTypes.Count == 0)
            {
                Debug.LogError("Não há tipos suficientes de cartas para o número de pares!");
                break;
            }

            int chosenType = allTypes[UnityEngine.Random.Range(0, allTypes.Count)];
            allTypes.Remove(chosenType);
            gameTypes.Add(chosenType);
        }

        List<int> chosenTypes = new List<int>();

        foreach (var type in gameTypes)
        {
            chosenTypes.Add(type);
            chosenTypes.Add(type);
        }

        Shuffle(chosenTypes);

        Vector3 offset = new Vector3((_currentLevel.Columns - 1) * (_cardPrefab.cardSize + _currentLevel.Spacing), (_currentLevel.Rows - 1) * (_cardPrefab.cardSize + _currentLevel.Spacing), 0f) * 0.5f;

        for (int y = 0; y < _currentLevel.Rows; y++)
        {
            for (int col = 0; col < _currentLevel.Columns; col++)
            {
                Vector3 position = new Vector3(col * (_cardPrefab.cardSize + _currentLevel.Spacing), y * (_cardPrefab.cardSize + _currentLevel.Spacing), 0f);
                var card = Instantiate(_cardPrefab, position - offset, Quaternion.identity);
                card.cardtype = chosenTypes[0];
                chosenTypes.RemoveAt(0);
                card.onClicked.AddListener(OnCardClicked);
                _cards.Add(card);
            }
        }

        _blockInput = false;
        _gameEnded = false;
        SetCardsInteractable(true);
        _activeCard = null;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private void OnCardClicked(CardController card)
    {
        if (_blockInput)
            return;

        _blockInput = true;

        if (_activeCard == null)
        {
            StartCoroutine(SelectCard(card));
            return;
        }

        if (card.cardtype == _activeCard.cardtype)
        {
            StartCoroutine(Score(card));
            return;
        }

        StartCoroutine(Fall(card));
    }

    private IEnumerator SelectCard(CardController card)
    {
        _activeCard = card;
        _activeCard.Reveal();
        yield return new WaitForSeconds(0.5f);
        _blockInput = false;
    }

    private IEnumerator Score(CardController card)
    {
        card.Reveal();
        yield return new WaitForSeconds(1f);
        _cards.Remove(_activeCard);
        _cards.Remove(card);
        Destroy(card.gameObject);
        Destroy(_activeCard.gameObject);
        _activeCard = null;
        _scoreManager.AddAcerto();

        if (_cards.Count < 1)
        {
            Win();
            yield break;
        }

        _blockInput = false;
    }

    private IEnumerator Fall(CardController card)
    {
        card.Reveal();
        yield return new WaitForSeconds(1f);
        _activeCard.Hide();
        card.Hide();
        _activeCard = null;
        _scoreManager.AddErro();

        yield return new WaitForSeconds(0.5f);
        _blockInput = false;
    }

    private void Win()
    {
        //_level++;

        //if (_level >= _levels.Count)
        //{
        //    _level = 0;
        //}

        //PlayerPrefs.SetInt("Level", _level);
        Debug.Log("Victory");

        _gameEnded = true;
        _scoreManager.SalvarPontuacao();
        SetCardsInteractable(false);

        StartCoroutine(TransitionFinalScene());
    }

    private IEnumerator TransitionFinalScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("FinalScene");
    }

    private void SetCardsInteractable(bool interactable)
    {
        foreach (var card in _cards)
        {
            card.IsInteractable = interactable;
        }
    }
}