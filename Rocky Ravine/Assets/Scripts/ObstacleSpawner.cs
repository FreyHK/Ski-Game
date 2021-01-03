using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstaclePrefab {
    public GameObject prefab;
    public bool StayUpright = false;
}

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstacleRoot;
    public Transform playerTransform;

    public ObstaclePrefab[] obstaclePrefabs;

    //How far in front of the player do obstacles spawn?
    float spawnDistance = 40f;

    //How many units are there between obstacles
    float minSpawnSpacing = 10f;
    float maxSpawnSpacing = 20f;

    float currentX;

    List<Transform> obstacles = new List<Transform>();

    //Public fields used by scripts
    public float SpeedScale = 1f;

    void Start()
    {
        currentX = playerTransform.position.x;
        spacing = Random.Range(minSpawnSpacing, maxSpawnSpacing);
    }

    float spacing;

    void Update()
    {
        if (playerTransform.position.x + spawnDistance > currentX + spacing * SpeedScale)
        {
            currentX += spacing * SpeedScale;

            SpawnStaticObstacle(currentX);

            spacing = Random.Range(minSpawnSpacing, maxSpawnSpacing);
        }

        //Deleting obstacles
        List<int> toRemove = new List<int>();
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].position.x < playerTransform.position.x - spawnDistance)
                toRemove.Add(i);
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            Transform t = obstacles[toRemove[i]];
            Destroy(t.gameObject);
            obstacles.Remove(t);
        }
    }

    public LayerMask groundMask;

    void SpawnStaticObstacle(float x)
    {
        Vector3 origin = new Vector3(x, playerTransform.position.y + 10f);
        RaycastHit2D hit = Physics2D.Raycast(origin, -Vector2.up, 9999f, groundMask);

        if (hit.collider == null)
        {
            Debug.LogWarning("Raycast did not hit ground. Can't spawn obstacle.");
            return;
        }
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);

        //Find rotation
        Quaternion rot;
        if (obstaclePrefabs[prefabIndex].StayUpright) {
            rot = Quaternion.identity;
        } else {
            rot = Quaternion.LookRotation(Vector3.forward, (Vector3)hit.normal);
        }
        Vector3 pos = hit.point;
        GameObject gm = Instantiate(obstaclePrefabs[prefabIndex].prefab, pos, rot);
        gm.transform.parent = obstacleRoot;
        obstacles.Add(gm.transform);
    }
}
