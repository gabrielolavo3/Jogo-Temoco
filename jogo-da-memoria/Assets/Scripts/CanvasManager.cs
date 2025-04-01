using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO collection;
    public GameDataSO easyData;
    public GameDataSO normalData;
    public GameDataSO hardData;
    GameDataSO gameDatas;
    List<CardControler> cardControlers;

    private void Awake()
    {
        cardControlers = new List<CardControler>();
        GetGameDatasDifficulty();
        
        if (gameDatas != null)
        {
            SetCardGridLayoutParams();
        }

        GenerateCards();
    }

    private void GenerateCards()
    {
        int cardCount = gameDatas.rows * gameDatas.columns;

        for (int a = 0; a < cardCount; a++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform);
            card.transform.name = "Card (" + a.ToString() + ")";
            cardControlers.Add(card.GetComponent<CardControler>());
        }
    }

    private void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

        cardGridLayout.padding = new RectOffset(gameDatas.topBottomPadding, gameDatas.topBottomPadding, gameDatas.topBottomPadding, gameDatas.topBottomPadding);

        cardGridLayout.rows = gameDatas.rows;
        cardGridLayout.columns = gameDatas.columns;
        cardGridLayout.spacing = gameDatas.spacing;        
    }

    public void GetGameDatasDifficulty()
    {
        Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", (int)Difficulty.NORMAL);

        switch (difficulty)
        {
            case Difficulty.EASY:
                gameDatas = easyData;
                break;

            case Difficulty.NORMAL:
                gameDatas = normalData;
                break;

            case Difficulty.HARD:
                gameDatas = hardData;
                break;
        }
    }
}
