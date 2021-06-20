using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    public float Scalar = 0.001f;
    Material mat;
    float startOffset;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startOffset = Random.value;
    }

    void Update()
    {
        float offset = target.position.x / 1024 * Scalar;
        mat.mainTextureOffset = new Vector2(startOffset + offset, mat.mainTextureOffset.y);
    }
}
