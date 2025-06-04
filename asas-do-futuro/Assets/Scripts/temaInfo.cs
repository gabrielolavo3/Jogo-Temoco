using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temaInfo : MonoBehaviour
{

    public int idTema;

    private int notaFinal;

    // Start is called before the first frame update
    void Start()
    {
        int notaFinal = PlayerPrefs.GetInt("notaFinal"+idTema.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
