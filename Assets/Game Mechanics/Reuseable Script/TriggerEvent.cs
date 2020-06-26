using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour {

    public UnityEvent triggerEvent;

    void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "Player") {

            triggerEvent.Invoke();

        }

    }

}
