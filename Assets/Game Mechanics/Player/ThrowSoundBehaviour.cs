using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ThrowSoundBehaviour : MonoBehaviour {

    [Header("Throw Behaviour")]
    public Transform spawnPosition;
    public float throwForce = 20f;

    public int throwAmount = 0;

    public PoolingScript soundGeneratorPool;
	
    private PlayerBehaviourScript pbs;

    void Awake() {

        pbs = GetComponent<PlayerBehaviourScript>();

    }

    // Update is called once per frame
    void Update () {

        if(CrossPlatformInputManager.GetButtonDown("Throw")) {

            Throw();

        }
        
    }
    
    public void SetThrowAmount(int amount) {

        throwAmount = amount;

    }

    public void Throw() {

        if(Time.timeScale == 0f)
            return;

        if(pbs.isPlayerLock())
            return;

        if(throwAmount == 0)
            return;

        soundGeneratorPool.Spawn(spawnPosition);
        Rigidbody spawnObject = soundGeneratorPool.GetLatestSpawnObject().GetComponent<Rigidbody>();
        
        spawnObject.AddForce(spawnPosition.forward * throwForce);
        spawnObject.AddTorque(transform.right * throwForce);

        throwAmount--;
        UIScript.instance.UpdateRemainingText(throwAmount);

    }

}
