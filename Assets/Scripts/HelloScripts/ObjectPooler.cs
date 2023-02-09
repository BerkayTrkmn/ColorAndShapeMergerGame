using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler instance;

        public Dictionary<string, List<GameObject>> poolList;
        private void Awake()
        {
            #region Singleton
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
            #endregion

            poolList = new Dictionary<string, List<GameObject>>();
        }

        public List<GameObject> PoolObject(GameObject poolobject, int objectNumber)
        {
            List<GameObject> pool = new List<GameObject>();

            GameObject parent = new GameObject();
            parent.name = poolobject.name + "Holder";
            parent.transform.position = Vector3.zero;

            for (int i = 0; i < objectNumber; i++)
            {
                GameObject currentGO = Instantiate(poolobject);
                currentGO.transform.parent = parent.transform;
                currentGO.name = poolobject.name;
                currentGO.SetActive(false);
                pool.Add(currentGO);

            }
            poolList.Add(poolobject.name, pool);
            return pool;
        }

        public GameObject GetPooledObject(GameObject poolobject)
        {
            List<GameObject> pool = poolList[poolobject.name];

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                    return pool[i];

            }
            //if all object in use then create one and send

            return CreateObjectForPool(poolobject, pool);
        }

        private GameObject CreateObjectForPool(GameObject poolobject, List<GameObject> pool)
        {
            GameObject currentGO = Instantiate(poolobject);
            currentGO.transform.parent = pool[0].transform.parent;
            currentGO.name = poolobject.name;
            currentGO.SetActive(false);
            pool.Add(currentGO);
            return currentGO;
        }

    }
}