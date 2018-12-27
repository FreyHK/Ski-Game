using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(PolygonCollider2D))]
public class ChunkMesh : MonoBehaviour {

    MeshFilter meshFilter;
    PolygonCollider2D polygonCollider;

    void Awake () {
        meshFilter = GetComponent<MeshFilter>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public int X;

    public void Init(int x, float[] heights) {
        this.X = x;
        CreateMesh(heights);
    }

    float vertexSpacing;

    Mesh mesh;

    public void CreateMesh(float[] heights) {
        meshFilter.mesh = mesh = new Mesh();
        mesh.name = "Chunk";

        vertexSpacing = (ChunkGenerator.ChunkSize) / (heights.Length-1);

        Vector3[] vertices = new Vector3[heights.Length * 2];
        for (int i = 0, y = 0; y < 2; y++) {
            for (int x = 0; x < heights.Length; x++, i++) {
                //Make sure we have lots of snow below playable area.
                float h = -25f;
                if (y == 1) {
                    h = heights[x];
                }
                vertices[i] = new Vector3(x * vertexSpacing, h);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[(heights.Length - 1) * 6];
        //Triangle index
        int ti = 0;
        for (int i = 0; i < heights.Length-1; i++) {
            triangles[ti] = i;
            triangles[ti + 1] = i + heights.Length + 1;
            triangles[ti + 2] = i + 1;

            triangles[ti + 3] = i;
            triangles[ti + 4] = i + heights.Length;
            triangles[ti + 5] = i + heights.Length + 1;

            ti += 6;
        }
        mesh.triangles = triangles;

        GenerateCollider(heights);
    }

    void GenerateCollider(float[] heights) {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0f, 0f));
        points.Add(new Vector2(ChunkGenerator.ChunkSize, 0f));

        for (int i = 0; i < heights.Length; i++) {
            points.Add(new Vector3(i * vertexSpacing, heights[i]));
        }

        polygonCollider.points = points.ToArray();
    }
}
