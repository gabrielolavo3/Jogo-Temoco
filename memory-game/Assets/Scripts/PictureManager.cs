using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture picturePrefab;
    public Transform picSpawPosition;
    public Vector2 startPosition = new Vector2(-2.15f, 3.62f);

    public enum GameState
    {
        NoAction,
        MovingOnPositions,
        DeletingPuzzles,
        FlipBack,
        Checking,
        GameEnd
    };

    public enum PuzzleState
    {
        PuzzleRotation,
        CanRotate
    };

    public enum RevealedState
    {
        NoRevealed,
        OneRevealed,
        TwoRevealed
    };

    [HideInInspector] public GameState CurrentGameState;
    [HideInInspector] public PuzzleState CurrentPuzzleState;
    [HideInInspector] public RevealedState PuzzleRevealedNumber;

    [HideInInspector] public List<Picture> pictureList;

    private Vector2 _offset = new Vector2(1.5f, 1.52f);
    private Vector2 _offsetFor15Pairs = new Vector2(1.08f, 1.22f);
    private Vector2 _offsetFor20Pairs = new Vector2(1.08f, 1.0f);
    private Vector3 _newScaleDown = new Vector3(0.9f, 0.9f, 0.001f);

    private List<Material> materialList = new List<Material>();
    private List<string> texturePathList = new List<string>();
    private Material firstMaterial;
    private string firstTexturePath;

    private int firstRevealedPic;
    private int secondRevealedPic;
    private int revealedPicNumber = 0;

    void Start()
    {
        CurrentGameState = GameState.NoAction;
        CurrentPuzzleState = PuzzleState.CanRotate;
        PuzzleRevealedNumber = RevealedState.NoRevealed;
        revealedPicNumber = 0;
        firstRevealedPic = -1;
        secondRevealedPic = -1;

        LoadMaterials();

        if (GameSettings.Instance.GetPairsNumber() == GameSettings.EPairsNumber.E10Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpawPictureMesh(4, 5, startPosition, _offset, false);
            MovePicture(4, 5, startPosition, _offset);
        }  
        else if (GameSettings.Instance.GetPairsNumber() == GameSettings.EPairsNumber.E15Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpawPictureMesh(5, 6, startPosition, _offset, false);
            MovePicture(5, 6, startPosition, _offsetFor15Pairs);
        }
        else if (GameSettings.Instance.GetPairsNumber() == GameSettings.EPairsNumber.E20Pairs)
        {
            CurrentGameState = GameState.MovingOnPositions;
            SpawPictureMesh(5, 8, startPosition, _offset, true);
            MovePicture(5, 8, startPosition, _offsetFor20Pairs);
        }
    }

    public void CheckPicture()
    {
        CurrentGameState = GameState.Checking;
        revealedPicNumber = 0;

        for (int id = 0; id < pictureList.Count; id++)
        {
            if (pictureList[id].revealed && revealedPicNumber < 2)
            {
                if (revealedPicNumber == 0)
                {
                    firstRevealedPic = id;
                    revealedPicNumber++;
                }
                else if (revealedPicNumber == 1)
                {
                    secondRevealedPic = id;
                    revealedPicNumber++;
                }
            }
        }

        if (revealedPicNumber == 2)
        {
            CurrentGameState = GameState.FlipBack;
        }

        CurrentPuzzleState = PictureManager.PuzzleState.CanRotate;
        
        if (CurrentGameState == GameState.Checking)
        {
            CurrentGameState = GameState.NoAction;
        }
    }

    private void FlipBack()
    {
        System.Threading.Thread.Sleep(700);

        pictureList[firstRevealedPic].FlipBack();
        pictureList[secondRevealedPic].FlipBack();

        pictureList[firstRevealedPic].revealed = false;
        pictureList[secondRevealedPic].revealed = false;

        PuzzleRevealedNumber = RevealedState.NoRevealed;
        CurrentGameState = GameState.NoAction;
    }

    private void LoadMaterials()
    {
        var materialFillePath = GameSettings.Instance.GetMaterialDirectoryName();
        var textureFillePath = GameSettings.Instance.GetPuzzleCategoryTextureDirectoryName();
        var pairNumber = (int)GameSettings.Instance.GetPairsNumber();
        const string matBaseName = "Pic";
        var firstMaterialName = "Back";

        for (var index = 1; index <= pairNumber; index++)
        {
            var currentFillePath = materialFillePath + matBaseName + index;
            Material mat = Resources.Load(currentFillePath, typeof(Material)) as Material;
            materialList.Add(mat);

            var currentTextureFillePath = textureFillePath + matBaseName + index;
            texturePathList.Add(currentTextureFillePath);
        }

        firstTexturePath = textureFillePath + firstMaterialName;
        firstMaterial = Resources.Load(materialFillePath + firstMaterialName, typeof(Material)) as Material;
    }


    void Update()
    {
        if (CurrentGameState == GameState.FlipBack)
        {
            if (CurrentPuzzleState == PuzzleState.CanRotate)
            {
                FlipBack();
            }
        }
    }

    private void SpawPictureMesh(int rows, int columns, Vector2 pos, Vector2 offset, bool scaleDown)
    {
        for (int col = 0; col < columns; col++)
        {
            for(int row = 0; row < rows; row++)
            {
                var tempPicture = (Picture)Instantiate(picturePrefab, picSpawPosition.position, picturePrefab.transform.rotation);

                if (scaleDown)
                {
                    tempPicture.transform.localScale = _newScaleDown;
                }

                tempPicture.name = tempPicture.name + 'c' + col + 'r' + row;
                pictureList.Add(tempPicture);
            }
        }

        ApplyTextures();
    }

    public void ApplyTextures()
    {
        var rndMatIndex = Random.Range(0, materialList.Count);
        var AppliedTimes = new int[materialList.Count];

        for (int i = 0; i < materialList.Count; i++)
        {
            AppliedTimes[i] = 0;
        }

        foreach(var o in pictureList)
        {
            var randPrevious = rndMatIndex;
            var counter = 0;
            var forceMat = false;

            while (AppliedTimes[rndMatIndex] >= 2 || ((randPrevious == rndMatIndex) && !forceMat))
            {
                rndMatIndex = Random.Range(0, materialList.Count);
                counter++;
                if (counter > 100)
                {
                    for (var j = 0; j < materialList.Count; j++)
                    {
                        if (AppliedTimes[j] < 2)
                        {
                            rndMatIndex = j;
                            forceMat = true;
                        }
                    }

                    if (forceMat == false)
                    {
                        return;
                    }
                }
            }

            o.SetFirstMaterial(firstMaterial, firstTexturePath);
            o.ApplyFirstMaterial();
            o.SetSecondMaterial(materialList[rndMatIndex], texturePathList[rndMatIndex]);
            AppliedTimes[rndMatIndex] += 1;
            forceMat = false;
        }
    }

    private void MovePicture(int rows, int columns, Vector2 pos, Vector2 offset)
    {
        var index = 0;
        for (var col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                var targetPosition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 0.0f);
                StartCoroutine(MoveToPosition(targetPosition, pictureList[index]));
                index++;
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, Picture obj)
    {
        var randomDis = 7;

        while(obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomDis * Time.deltaTime);
            yield return 0;
        }
    }
}
