using UnityEngine;


public class DoorScript : MonoBehaviour {

    [Header("Door")]
    public bool openOnStart;

    [Header("Audio")]
    public AudioSource closeDoorSound;

    //Animator hash
    private int openDefault = Animator.StringToHash("OpenDefault");
    private int open = Animator.StringToHash("Open");
    private int close = Animator.StringToHash("Close");

    //Component Instance
    private Animator anim;

    void Awake() {

        anim = GetComponent<Animator>();

    }

    // Use this for initialization
    void Start () {
		
        if(openOnStart) {

            anim.SetTrigger(openDefault);

        }

	}
	
    public void OpenDoor() {

        anim.SetTrigger(open);

    }

    public void CloseDoor() {

        anim.SetTrigger(close);

    }

    public void PlayCloseDoorSound() {

        closeDoorSound.Play();
        
    }

}
