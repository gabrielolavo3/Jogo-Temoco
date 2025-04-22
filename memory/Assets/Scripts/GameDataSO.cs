using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/Datas")]
public class GameDataSO : ScriptableObject
{
    [Header("Difficulty Game Settings")]
    public Difficulty difficulty;
    public int rows;
    public int columns;
}
