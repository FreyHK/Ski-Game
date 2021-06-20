using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandomTexture : MonoBehaviour
{
    public Texture2D[] textures;
    public Color[] mountainColors;
    public Renderer mountainsRenderer;
    public Renderer mountainsFillRenderer;

    void Start()
    {
        int index = Random.Range(0, textures.Length);
        GetComponent<MeshRenderer>().material.mainTexture = textures[index];
        mountainsRenderer.material.color = mountainColors[index];
        mountainsFillRenderer.material.color = mountainColors[index];
    }
}
