using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviourScript : MonoBehaviour {

    [HideInInspector]
    public FirstPersonController fpsController;
    [HideInInspector]
    public ThrowSoundBehaviour throwSoundBehaviour;

    [Header("Raycast")]
    public LayerMask touchScreenRaycastLayer;
    public float touchScreenRaycastDistance = 1f;

    //Tag Register
    private string paperTag = "Paper";

    private bool playerLock = false;

    void Awake() {

        fpsController = GetComponent<FirstPersonController>();
        throwSoundBehaviour = GetComponent<ThrowSoundBehaviour>();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        TouchScreenRaycast();

	}

    void TouchScreenRaycast() {

        if(playerLock)
            return;

        if(Input.touchCount > 0){

            for(int i = 0; i<Input.touchCount; i++){

                if(Input.touches[i].phase==TouchPhase.Ended){
                    
                    Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
                    RaycastHit hit;

                    if(Physics.Raycast(ray, out hit, touchScreenRaycastDistance, touchScreenRaycastLayer)){
                         
                        hit.collider.gameObject.GetComponent<PaperScript>().CollectPaper();

                    }

                } //End of Phase

            } //End of loop

        }   //End of touchCount

        if(CrossPlatformInputManager.GetButtonUp("Collect")) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, touchScreenRaycastDistance, touchScreenRaycastLayer)){
                         
                if(hit.collider.gameObject.tag == paperTag) {

                    hit.collider.gameObject.GetComponent<PaperScript>().CollectPaper();

                }

            }

        }

    }

    public void LockPlayer() {

        fpsController.enabled = false;
        playerLock = true;

    }

    public void ReleasePlayer() {

        fpsController.enabled = true;
        playerLock = false;

    }

    public bool isPlayerLock() {

        return playerLock;

    }

}
