using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private readonly Dictionary<EPuzzleCategories, string> _puzzleCatDirectory = new Dictionary<EPuzzleCategories, string>();
    private int _settings;
    private const int settingsNumber = 2;
    private bool muteFxPermanently = false;

    public enum EPairsNumber
    {
        NotSet = 0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20Pairs = 20,
    }

    public enum EPuzzleCategories 
    {
        NotSet,
        Fruits,
        Vegetables
    }

    public struct Settings
    {
        public EPairsNumber ParisNumber;
        public EPuzzleCategories PuzzleCategory;
    };

    private Settings _gameSettings;

    public static GameSettings Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPuzzleCatDirectory();
        _gameSettings = new Settings();
        ResetGameSettings();
    }

    private void SetPuzzleCatDirectory()
    {
        _puzzleCatDirectory.Add(EPuzzleCategories.Fruits, "Fruits");
        _puzzleCatDirectory.Add(EPuzzleCategories.Vegetables, "Vegetables");
    }

    public void SetPairsNumber(EPairsNumber Number)
    {
        if (_gameSettings.ParisNumber == EPairsNumber.NotSet)
        {
            _settings++;
        }

        _gameSettings.ParisNumber = Number;
    }

    public void SetPuzzleCategories(EPuzzleCategories Cat)
    { 
        if(_gameSettings.PuzzleCategory == EPuzzleCategories.NotSet)
        {
            _settings++;
        }

        _gameSettings.PuzzleCategory = Cat;
    }

    public EPairsNumber GetPairsNumber() 
    { 
        return _gameSettings.ParisNumber;
    }

    public EPuzzleCategories GetEPuzzleCategories()
    {
        return _gameSettings.PuzzleCategory;
    }

    public void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.PuzzleCategory = EPuzzleCategories.NotSet;
        _gameSettings.ParisNumber = EPairsNumber.NotSet;
    }

    public bool AllSettingsReady()
    {
        return _settings == settingsNumber;
    }

    public string GetMaterialDirectoryName()
    {
        return "Materials/";
    }

    public string GetPuzzleCategoryTextureDirectoryName()
    {
        if (_puzzleCatDirectory.ContainsKey(_gameSettings.PuzzleCategory))
        {
            return "Graphics/PuzzleCat/" + _puzzleCatDirectory[_gameSettings.PuzzleCategory] + "/";            
        }
        else
        {
            Debug.LogError("ERROR: CANNOT GET DIRECTORY");
            return "";
        }
    }

    public void MuteSoundEffectPermanently(bool muted)
    {
        muteFxPermanently = muted;
    }

    public bool IsSoundEffectMutedPernamently()
    {
        return muteFxPermanently;
    }
}
