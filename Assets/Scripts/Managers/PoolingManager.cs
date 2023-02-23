using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObject
{
    CANNON_BALL,
    SHOOTER,
    CHASER,
    HEALTH_BAR,
    BOAT_EXPLOSION,
    FIRE_EXPLOSION,
    TARGET,
}

[Serializable]
public class Pool
{
    public PoolObject tag;
    public GameObject prefab;
    public int size;
}

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance { get; private set; }

    [SerializeField] private Transform m_instantiationParent;
    [SerializeField] private List<Pool> m_poolsToCreate;
    private Dictionary<PoolObject, Queue<GameObject>> m_pools;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        
        // Start pool
        m_pools = new Dictionary<PoolObject, Queue<GameObject>>();
        
        Populate();
    }
    
    private void Populate()
    {
        foreach (Pool pool in m_poolsToCreate)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            GameObject gameObj;
            for (int i = 0; i < pool.size; i++)
            {
                gameObj = Instantiate(pool.prefab, m_instantiationParent);
                gameObj.SetActive(false);
                queue.Enqueue(gameObj);
            }
            m_pools.Add(pool.tag, queue);
        }
    }
    
    private GameObject InstantiateOfType(PoolObject type)
    {
        Pool pool = m_poolsToCreate.Find((x) => x.tag == type);
        GameObject gameObj = Instantiate(pool.prefab, m_instantiationParent);
        gameObj.SetActive(false);
        return gameObj;
    }

    public GameObject TakeFromPool(PoolObject poolObject, Vector3 position)
    {
        GameObject gameObj = m_pools[poolObject].Dequeue();
        if (gameObj.activeSelf) // Makes more of the item if needed
        {
            m_pools[poolObject].Enqueue(gameObj);
            gameObj = InstantiateOfType(poolObject);
        }
        gameObj.SetActive(true);
        gameObj.transform.position = position;
        m_pools[poolObject].Enqueue(gameObj);
        return gameObj;
    }

    public void DisableAll()
    {
        foreach (KeyValuePair<PoolObject, Queue<GameObject>> pool in m_pools)
        {
            foreach (GameObject gameObj in pool.Value)
            {
                gameObj.SetActive(false);
            }
        }
    }

    
    
    
}
