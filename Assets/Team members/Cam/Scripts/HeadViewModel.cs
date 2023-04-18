using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadViewModel : MonoBehaviour
{
    public List<Texture2D> texture2D;
    
    // Start is called before the first frame update
    void Start()
    {
        int sheetIndex = Random.Range(0, texture2D.Count);

        int  xFaces = 4;
        // Vector2 selectedUV = new Vector2(sheetIndex % xFaces);
        // GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", selectedUV);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
