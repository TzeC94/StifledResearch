using System.Collections;
using UnityEngine;

public class ThrowingObjectHandler : MonoBehaviour {

    private string playerString = "Player";
    private bool triggerd = false;

    [Header("Throwing")]
    public float deadDurationAfterCollide = 2f;

    private PoolingScript poolScript;

    void OnDisable() {

        triggerd = false;

    }

    void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.tag != playerString) {

            SoundLightManager.instance.SpawnThrowingSound(transform.position);

            if(triggerd == false) {

                triggerd = true;
                    
                //SoundLightManager.instance.SpawnThrowingSound(transform.position);

                StartCoroutine(DeadCount());

            }

        }

    }

    void SetPool(GameObject pool) {

        poolScript = pool.GetComponent<PoolingScript>();

    }

    IEnumerator DeadCount() {

        yield return new WaitForSeconds(deadDurationAfterCollide);

        gameObject.SetActive(false);

        poolScript.KeepThis(gameObject);


    }

}
