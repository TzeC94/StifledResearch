using UnityEngine;
   
public class ForcePlayerToLookScript : MonoBehaviour {

    public float forceRotatingSpeed = 10f;

    [Tooltip("IF this is blank, it will default center position as target")]
    public Transform cameraLookingPoint;
    
    void Awake() {

        if(cameraLookingPoint == null)
            cameraLookingPoint = transform;

    }

    // Update is called once per frame
    void Update() {

        RotatePlayerObject();

    }

    void RotatePlayerObject() {

        //Player model
        Vector3 direction = transform.position - GameManager.instance.playerObject.transform.position;
        Quaternion targetPlayerRotation =  Quaternion.LookRotation(direction);

        targetPlayerRotation.x = 0.0f;
        targetPlayerRotation.z = 0.0f;

            
        GameManager.instance.playerObject.transform.rotation = Quaternion.Lerp(GameManager.instance.playerObject.transform.rotation, targetPlayerRotation, forceRotatingSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation (cameraLookingPoint.position - GameManager.instance.playerBehaviour.fpsController.m_Camera.transform.position);
        GameManager.instance.playerBehaviour.fpsController.m_Camera.transform.rotation = Quaternion.Lerp(GameManager.instance.playerBehaviour.fpsController.m_Camera.transform.rotation, targetRotation, forceRotatingSpeed * Time.deltaTime);

    }


}
