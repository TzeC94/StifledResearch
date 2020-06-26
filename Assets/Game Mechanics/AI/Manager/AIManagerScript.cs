using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManagerScript : MonoBehaviour {

    [Header("Skeleton AI")]
    public GameObject skeletonAIPrafab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnAISkeleton(Vector3 spawnPosition) {

        Instantiate(skeletonAIPrafab, spawnPosition, Quaternion.identity, transform);

    }

}
