using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Picture : MonoBehaviour
{
    public AudioClip pressSound;
    private Material firstMaterial;
    private Material secondtMaterial;
    private Quaternion currentRotation;
    [HideInInspector] public bool revealed = false;
    private PictureManager pictureManager;
    private bool clicked = false;
    private int _index;

    private AudioSource audio;

    public void SetIndex(int id)
    {
        _index = id;
    }

    public int GetIndex()
    {
        return _index;
    }

    // Start is called before the first frame update
    void Start()
    {
        revealed = false;
        clicked = false;
        pictureManager = GameObject.Find("[PictureManager]").GetComponent<PictureManager>();
        currentRotation = gameObject.transform.rotation;

        audio = GetComponent<AudioSource>();
        audio.clip = pressSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (clicked == false)
        {
            pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotation;

            if (GameSettings.Instance.IsSoundEffectMutedPernamently() == false)
            {
                audio.Play();
            }

            StartCoroutine(LoopRotation(45, false));
            clicked = true;
        }
    }

    public void FlipBack()
    {
        if (gameObject.activeSelf)
        {
            pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.PuzzleRotation;
            revealed = false;

            if (GameSettings.Instance.IsSoundEffectMutedPernamently() == false)
            {
                audio.Play();
            }

            StartCoroutine(LoopRotation(45, true));
        }
    }

    IEnumerator LoopRotation(float angle, bool FirstMat)
    {
        var rot = 0f;
        const float dir = 1f;
        const float rotSpeed = 180.0f;
        const float rotSpeed_1 = 90.0f;
        var startAngle = angle;
        var assigned = false;

        if (FirstMat)
        {
            while(rot < angle)
            {
                var step = Time.deltaTime * rotSpeed_1;
                gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);

                if (rot >= (startAngle - 2) && assigned == false)
                {
                    ApplyFirstMaterial();
                    assigned = true;
                }

                rot += (1 * step * dir);
                yield return null;
            }
        }
        else
        {
            while (angle > 0)
            {
                float step = Time.deltaTime * rotSpeed;
                gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
                angle -= (1 * step * dir);
                yield return null;
            }
        }

        gameObject.GetComponent<Transform>().rotation = currentRotation;

        if (!FirstMat)
        {
            revealed = true;
            ApplySecondMaterial();
            pictureManager.CheckPicture();
        }
        else
        {
            pictureManager.PuzzleRevealedNumber = PictureManager.RevealedState.NoRevealed;
            pictureManager.CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
        }

        clicked = false;
    }

    public void SetFirstMaterial(Material mat, string texturePath)
    {
        firstMaterial = mat;
        firstMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void SetSecondMaterial(Material mat, string texturePath)
    {
        secondtMaterial = mat;
        secondtMaterial.mainTexture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
    }

    public void ApplyFirstMaterial()
    {
        gameObject.GetComponent<Renderer>().material = firstMaterial;
    }

    public void ApplySecondMaterial()
    {
        gameObject.GetComponent<Renderer>().material = secondtMaterial;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
