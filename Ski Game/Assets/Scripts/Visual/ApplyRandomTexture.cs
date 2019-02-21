using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandomTexture : MonoBehaviour
{
    public Texture2D[] textures;
    
    void Start()
    {
        Texture2D text = textures[Random.Range(0, textures.Length)];
        GetComponent<MeshRenderer>().material.mainTexture = text;
    }
}
