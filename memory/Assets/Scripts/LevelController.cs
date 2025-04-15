using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Serializable]
    public class LevelData
    {
        public int Columns;
        public int Rows;
        public float Spacing;
        public int Difficulty;
        public int Movements;
    }

    private List<CardController> _cards = new List<CardController>();

    [SerializeField] private CardController _cardPrefab;
    [SerializeField] private RectTransform cardsGrid;

    //[Header("UI References")]
    //[SerializeField] private TMP_Text _levelText;
    //[SerializeField] private TMP_Text _movementText;
    //[SerializeField] private GameObject _gamerOverButton;

    [Header("LevelData")]
    [SerializeField] private List<LevelData> _levels = new List<LevelData>();

    //[SerializeField] private int _columns = 4;
    //[SerializeField] private int _rows = 4;
    //[SerializeField] private float _spacing = 0.5f;
    //[SerializeField] private int _difficulty = 4;
    //[SerializeField] private int _movements = 10;

    private CardController _activeCard;    
    private int _movementsUsed = 0;
    private bool _blockInput = true;
    private int _level = 0;
    private bool _gameEnded = false;

    void Start()
    {
        _level = PlayerPrefs.GetInt("Level", 0);
        StartLevel();
    }

    public void StartLevel()
    {
        //_gamerOverButton.SetActive(false);

        Debug.Assert((_levels[_level].Rows * _levels[_level].Columns) % 2 == 0);

        if (_levels[_level].Difficulty > _cardPrefab.maxCardTypes)
        {
            _levels[_level].Difficulty = Math.Min(_levels[_level].Difficulty, _cardPrefab.maxCardTypes);
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

        for (int i = 0; i < (_levels[_level].Rows * _levels[_level].Columns) / 2; i++) // Número de pares necessários
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
            chosenTypes.Add(type); // Cada tipo duas vezes (um par)
        }

        // Embaralhar as cartas (opcional mas recomendado para aleatoriedade)
        Shuffle(chosenTypes);

        Vector3 offset = new Vector3((_levels[_level].Columns - 1) * (_cardPrefab.cardSize + _levels[_level].Spacing), (_levels[_level].Rows - 1) * (_cardPrefab.cardSize + _levels[_level].Spacing), 0f) * 0.5f;

        for (int y = 0; y < _levels[_level].Rows; y++)
        {
            for (int col = 0; col < _levels[_level].Columns; col++)
            {
                Vector3 position = new Vector3(col * (_cardPrefab.cardSize + _levels[_level].Spacing), y * (_cardPrefab.cardSize + _levels[_level].Spacing), 0f);

                var card = Instantiate(_cardPrefab, position - offset, Quaternion.identity);

                //         Vector3 position = new Vector3(
                //    col * (_cardPrefab.cardSize + _levels[_level].Spacing),
                //    -y * (_cardPrefab.cardSize + _levels[_level].Spacing), // Inverter Y se necessário
                //    0f
                //);

                card.cardtype = chosenTypes[0];
                chosenTypes.RemoveAt(0);
                card.onClicked.AddListener(OnCardClicked);
                _cards.Add(card);
            }
        }


        _blockInput = false;
        _gameEnded = false;
        SetCardsInteractable(true);
        _movementsUsed = 0;
        //_levelText.text = $"Level: {_level}";
        //_movementText.text = $"Moves: {_levels[_level].Movements}";
    }

    // Função para embaralhar uma lista
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
        //card.TestAnimation();

        if (_blockInput)
        {
            return;
        }

        _blockInput = true;

        if (_activeCard == null)
        {
            StartCoroutine(SelectCard(card));
            return;
        }

        _movementsUsed++;
        //_movementText.text = $"Moves: {_levels[_level].Movements - _movementsUsed}";

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
        _blockInput = false;

        if (_cards.Count < 1)
        {
            Win();
            yield break;
        }

        if (_movementsUsed >= _levels[_level].Movements)
        {
            Lose();
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
        yield return new WaitForSeconds(0.5f);
        _blockInput = false;

        if (_movementsUsed >= _levels[_level].Movements)
        {
            Lose();
            yield break;
        }

        _blockInput = false;
    }

    private void Win()
    {
        _level++;

        if (_level > _levels.Count)
        {
            _level = 0;
        }

        PlayerPrefs.SetInt("Level", _level);
        Debug.Log("Victory");

        _gameEnded = true;
        SetCardsInteractable(false);
        //_gamerOverButton.SetActive(true);
    }

    private void Lose()
    {
        Debug.Log("Defeat");
        _gameEnded = true;
        SetCardsInteractable(false);
        //_gamerOverButton.SetActive(true);
    }

    private void SetCardsInteractable(bool interactable)
    {
        foreach (var card in _cards)
        {
            card.IsInteractable = interactable;
        }
    }
}