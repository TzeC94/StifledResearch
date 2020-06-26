using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour {

    public UnityEvent targetEvent;
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetMouseButtonDown(0)) {

            targetEvent.Invoke();

        }
        	
	}
}
