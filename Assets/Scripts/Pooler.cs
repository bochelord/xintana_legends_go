using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// Static class Pooler that serves objects indicated as a prefab
    /// Normally it's used to create enemies, powerups and maybe visual effects
    /// </summary>
    public class Pooler : MonoBehaviour {

        #region Inspector Fields Variables
        public static Pooler current;
        public GameObject pooledObject;
        public int pooledAmount = 20;
        public bool willGrow = true;

        public List<GameObject> pooledObjects;
        #endregion


        public virtual void Awake()
        {
            current = this;
        }

        public virtual void Start()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.transform.parent = current.transform;
                obj.transform.position = Vector3.zero;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }

        }

        /// <summary>
        /// Look for an inactive child and returns it
        /// </summary>
        /// <returns>GameObject</returns>
        public virtual GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            /// willGrow controls if we need to instantiate more objects as we
            /// don't have them
            if (willGrow)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.transform.parent = current.transform;
                pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }

        /// <summary>
        /// Disable and reset given object
        /// </summary>
        /// <param name="item">obj transform to disable</param>
        public virtual void RemoveElement(Transform item)
        {
            item.transform.SetParent(this.transform);
            //Reset it's position
            item.transform.position = new Vector3(0, 0, 0);
            item.gameObject.SetActive(false);
        }
    }
