using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> objectsToPool;

    public GameObject RetreivePooledObject()
    {
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            if (!objectsToPool[i].activeInHierarchy)
            {
                return objectsToPool[i];
            }
        }

        return null;
    }
}
