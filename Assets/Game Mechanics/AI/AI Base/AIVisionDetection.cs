using UnityEngine;

public class AIVisionDetection : MonoBehaviour {

    [Header("Vision")]
    public float visionAngle = 45f;
    public LayerMask visionRaycastMask;

    [HideInInspector]
    public bool playerInVision = false;

    private float raycastDistance;

	// Use this for initialization
	void Start () {
		
        raycastDistance = GetComponent<SphereCollider>().radius;

	}

    void OnTriggerStay(Collider other) {
        
        if(playerInVision)
            return;

        if(other.gameObject == GameManager.instance.playerObject) {

            float angle = Vector3.Angle(transform.position, GameManager.instance.playerObject.transform.position);

            if(angle < visionAngle / 2f){

                Vector3 direction = GameManager.instance.playerObject.transform.position - transform.position;

                Ray ray = new Ray(transform.position, direction.normalized);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, raycastDistance, visionRaycastMask)) {

                    if(hit.collider.gameObject == GameManager.instance.playerObject) {

                        playerInVision = true;
                        GetComponentInParent<AIBaseScript>().Chase.Invoke();

                    }

                }
                

            }

        }

    }

    void OnTriggerExit(Collider other) {

        if(other.gameObject == GameManager.instance.playerObject)
            playerInVision = false;

    }

}
