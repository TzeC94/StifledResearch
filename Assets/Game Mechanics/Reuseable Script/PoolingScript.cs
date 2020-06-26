using System.Collections.Generic;
using UnityEngine;

public class PoolingScript : MonoBehaviour {

	private List<GameObject> usingList = new List<GameObject>();
    private List<GameObject> IDLEList = new List<GameObject>();

    [HideInInspector]
    public GameObject prefab;
    
    [HideInInspector]
    public bool findGameObjectWithTag = false;
    [HideInInspector]
    public string poolTagName;
    [HideInInspector]
    public GameObject pool;

    private GameObject latestSpawnObject;

    void Awake() {

        if(findGameObjectWithTag)
            pool = GameObject.FindWithTag(poolTagName);

    }

    public void SetPrefab(GameObject go) {

        prefab = go;

    }

    public void Spawn(Transform position) {

        GameObject go;

        if(IDLEList.Count == 0) {

            go = Instantiate(prefab, position.position, Quaternion.identity, pool.transform);

            go.SetActive(true);

            go.SendMessage("SetPool", gameObject);

        }else {

            go = IDLEList[0];
            IDLEList.Remove(go);
            usingList.Add(go);

            go.transform.position = position.position;
            go.transform.rotation = Quaternion.identity;

            go.SetActive(true);

        }

        latestSpawnObject = go;

    }

    public void Spawn(Vector3 position) {

        GameObject go;

        if(IDLEList.Count == 0) {

            go = Instantiate(prefab, position, Quaternion.identity, pool.transform);

            go.SetActive(true);

            go.SendMessage("SetPool", gameObject);

        }else {

            go = IDLEList[0];
            IDLEList.Remove(go);
            usingList.Add(go);

            go.transform.position = position;
            go.transform.rotation = Quaternion.identity;

            go.SetActive(true);

        }

        latestSpawnObject = go;

    }

    public void Spawn(Vector3 position, Quaternion rotation) {

        GameObject go;

        if(IDLEList.Count == 0) {

            go = Instantiate(prefab, position, rotation, pool.transform);

            go.SetActive(true);

            go.SendMessage("SetPool", gameObject);

        }else {

            go = IDLEList[0];
            IDLEList.Remove(go);
            usingList.Add(go);

            go.transform.position = position;
            go.transform.rotation = rotation;

            go.SetActive(true);

        }

        latestSpawnObject = go;

    }

    public void KeepThis(GameObject thisObject) {

        usingList.Remove(thisObject);
        IDLEList.Add(thisObject);

        thisObject.transform.localPosition = Vector3.zero;

    }

    public void KeepAll() {

        foreach(GameObject go in usingList) {

            go.transform.position = transform.position;

            go.SetActive(false);

        }

        IDLEList.AddRange(usingList);

    }

    public GameObject GetLatestSpawnObject() {

        return latestSpawnObject;

    }

}
