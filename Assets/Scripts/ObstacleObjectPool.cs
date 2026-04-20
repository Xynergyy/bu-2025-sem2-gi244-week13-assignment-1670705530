using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool staticInstance;

    void Awake()
    {
        staticInstance = this;

        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObstacle(obstacleBarrelPrefab, obstacleBarrelPool);
            CreateNewObstacle(obstacleBarrierPrefab, obstacleBarrierPool);
            CreateNewObstacle(obstacleStoneWallPrefab, obstacleStoneWallPool);
            if (i % 20 == 0)
            {
                yield return null;
            }
        }
    }

    private void CreateNewObstacle(GameObject prefab, List<GameObject> pool)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        pool.Add(go);
    }

    public GameObject Acquire(int obstacleType)
    {
        //return null;
        if (obstacleType == 1)
        {
            if (obstacleBarrelPool.Count == 0) CreateNewObstacle(obstacleBarrelPrefab, obstacleBarrelPool);
            var go = obstacleBarrelPool[0];
            obstacleBarrelPool.RemoveAt(0);
            go.SetActive(true);
            return go;
        }
        else if (obstacleType == 2)
        {
            if (obstacleBarrierPool.Count == 0) CreateNewObstacle(obstacleBarrierPrefab, obstacleBarrierPool);
            var go = obstacleBarrierPool[0];
            obstacleBarrierPool.RemoveAt(0);
            go.SetActive(true);
            return go;
        }
        else
        {
            if (obstacleStoneWallPool.Count == 0) CreateNewObstacle(obstacleStoneWallPrefab, obstacleStoneWallPool);
            var go = obstacleStoneWallPool[0];
            obstacleStoneWallPool.RemoveAt(0);
            go.SetActive(true);
            return go;
        }
    }

    public void Return(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);
        if (obstacleType == 1)
        {
            obstacleBarrelPool.Add(obstacle);
        }
        else if (obstacleType == 2)
        {
            obstacleBarrierPool.Add(obstacle);
        }
        else
        {
            obstacleStoneWallPool.Add(obstacle);
        }
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        Return(obstacle, obstacleType);
    }
}
