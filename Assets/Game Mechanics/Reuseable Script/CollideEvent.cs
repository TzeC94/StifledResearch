using UnityEngine;
using UnityEngine.Events;

public class CollideEvent : MonoBehaviour {

    public UnityEvent collideEvent;

    void OnCollisionEnter(Collision collision) {
        
        collideEvent.Invoke();

    }

}
