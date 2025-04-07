using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundEffect : MonoBehaviour
{
    public Sprite unMutedFxSrpite;
    public Sprite mutedFxSrpite;

    private Button button;
    private SpriteState state;
    
    void Start()
    {
        button = GetComponent<Button>();

        if (GameSettings.Instance.IsSoundEffectMutedPernamently())
        {
            state.pressedSprite = mutedFxSrpite;
            state.highlightedSprite = mutedFxSrpite;
            button.GetComponent<Image>().sprite = mutedFxSrpite;
        }
        else
        {
            state.pressedSprite = unMutedFxSrpite;
            state.highlightedSprite = unMutedFxSrpite;
            button.GetComponent<Image>().sprite = unMutedFxSrpite;
        }
    }

    public void OnGUI()
    {
        if (GameSettings.Instance.IsSoundEffectMutedPernamently())
        {
            button.GetComponent<Image>().sprite = mutedFxSrpite;
        }
        else
        {
            button.GetComponent<Image>().sprite = unMutedFxSrpite;
        }
    }

    public void ToggleFxIcon()
    {
        if (GameSettings.Instance.IsSoundEffectMutedPernamently())
        {
            state.pressedSprite = unMutedFxSrpite;
            state.highlightedSprite = unMutedFxSrpite;
            GameSettings.Instance.MuteSoundEffectPermanently(false);
        }
        else
        {
            state.pressedSprite = mutedFxSrpite;
            state.highlightedSprite = mutedFxSrpite;
            GameSettings.Instance.MuteSoundEffectPermanently(true);
        }

        button.spriteState = state;
    }
}
