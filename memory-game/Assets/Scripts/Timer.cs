using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GUIStyle clockStyle;
    public float timer;
    private float minutes;
    private float seconds;

    private const float virtualWidth = 1080.0f;
    private const float virtualHeigth = 1920.0f;

    private bool stopTimer;
    private Matrix4x4 matrix;
    private Matrix4x4 oldMatrix;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / virtualWidth, Screen.height / virtualHeigth, 1.0f));
        oldMatrix = GUI.matrix;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        GUI.matrix = matrix;
        minutes = Mathf.Floor(timer / 60);
        seconds = Mathf.RoundToInt(timer % 60);

        GUI.Label(new Rect(Camera.main.rect.x + 20, 10, 120, 50), "" + minutes.ToString("00") + ":" + seconds.ToString("00"), clockStyle);
        GUI.matrix = oldMatrix;
    }
}
