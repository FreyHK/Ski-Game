using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    public Transform chunkRoot;
    public Transform playerTransform;
    public ChunkMesh chunkPrefab;

    public const float ChunkSize = 10f;
    int chunkResolution = 16;

    List<ChunkMesh> chunks = new List<ChunkMesh>();

    void Start () {
        UpdateChunks();
    }

    private void Update() {
        UpdateChunks();
    }

    int CurrentChunkX = -2;
    int ChunkViewDistance = 2;

    void UpdateChunks() {
        //Player's position in chunk space
        int playerX = Mathf.FloorToInt(playerTransform.position.x / ChunkSize);

        while (playerX + ChunkViewDistance > CurrentChunkX) {
            CurrentChunkX++;
            CreateChunkAt(CurrentChunkX);
        }

        List<int> toRemove = new List<int>();
        for (int i = 0; i < chunks.Count; i++) {
            if (chunks[i].X < playerX - ChunkViewDistance)
                toRemove.Add(i);
        }
        for (int i = 0; i < toRemove.Count; i++) {
            Destroy(chunks[toRemove[i]].gameObject);
            chunks.RemoveAt(i);
        }
    }
    
    float lastChunkHeight = 0f;

    ChunkMesh CreateChunkAt(int x) {
        ChunkMesh c = Instantiate(chunkPrefab);
        c.transform.position = new Vector3(x * ChunkSize, lastChunkHeight);
        c.transform.parent = chunkRoot;

        float[] heights = GetHeights();
        c.Init(x, heights);
        
        chunks.Add(c);
        lastChunkHeight -= (maxElevation - heights[chunkResolution-1]);

        return c;
    }

    const float minElevation = 0f;
    const float maxElevation = 10f;
    const float minDecrease = .1f;
    const float maxDecrease = 1f;
    const float maxDecreaseAmount = .3f;

    float curDecrease = minDecrease;

    float[] GetHeights() {
        float[] heights = new float[chunkResolution];
        float curElevation = maxElevation;

        float scale = ChunkSize / chunkResolution;

        for (int i = 0; i < chunkResolution; i++) {
            heights[i] = curElevation;
            curElevation -= curDecrease * scale;
            curElevation = Mathf.Clamp(curElevation, minElevation, maxElevation);
            curDecrease += Random.Range(-maxDecreaseAmount, maxDecreaseAmount) * scale;
            curDecrease = Mathf.Clamp(curDecrease, minDecrease, maxDecrease);
        }
        return heights;
    }
}
