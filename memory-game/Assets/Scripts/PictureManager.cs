using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public Picture picturePrefab;
    public Transform picSpawPosition;
    public Vector2 startPosition = new Vector2(-2.15f, 3.62f);
    
    [HideInInspector]
    public List<Picture> pictureList;

    private Vector2 _offset = new Vector2(1.5f, 1.52f);

    private List<Material> materialList = new List<Material>();
    private List<string> texturePathList = new List<string>();
    private Material firstMaterial;
    private string firstTexturePath;


    void Start()
    {
        LoadMaterials();
        SpawPictureMesh(4, 5, startPosition, _offset, false);
        MovePicture(4, 5, startPosition, _offset);
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
        
    }

    private void SpawPictureMesh(int rows, int columns, Vector2 pos, Vector2 offset, bool scaleDown)
    {
        for (int col = 0; col < columns; col++)
        {
            for(int row = 0; row < rows; row++)
            {
                var tempPicture = (Picture)Instantiate(picturePrefab, picSpawPosition.position, picturePrefab.transform.rotation);
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
            for (int row = 0; row < rows;row++)
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
