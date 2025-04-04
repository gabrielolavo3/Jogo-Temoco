using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetGameButton : MonoBehaviour
{
    public enum EButtonType
    {
        NotSet,
        PairNumberBtn,
        PuzzleCategoryBtn
    };

    [SerializeField] public EButtonType ButtonType = EButtonType.NotSet;
    [HideInInspector] public GameSettings.EPairsNumber PairNumber = GameSettings.EPairsNumber.NotSet;
    [HideInInspector] public GameSettings.EPuzzleCategories PiuzzleCategories = GameSettings.EPuzzleCategories.NotSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGameOption(string GameSceneName)
    {
        var comp = gameObject.GetComponent<SetGameButton>();

        switch(comp.ButtonType)
        {
            case SetGameButton.EButtonType.PairNumberBtn:
                GameSettings.Instance.SetPairsNumber(comp.PairNumber);
                break;

            case EButtonType.PuzzleCategoryBtn:
                GameSettings.Instance.SetPuzzleCategories(comp.PiuzzleCategories);
                break;
        }

        if (GameSettings.Instance.AllSettingsReady())
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
