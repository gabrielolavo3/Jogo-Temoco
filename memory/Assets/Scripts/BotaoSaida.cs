using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSaida : MonoBehaviour
{
    public void EnviarSinalDeSaida()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
                Application.ExternalEval("window.ReactNativeWebView.postMessage('exit');");
        #endif
                Debug.Log("Mensagem de saída enviada para React Native.");
    }
}
