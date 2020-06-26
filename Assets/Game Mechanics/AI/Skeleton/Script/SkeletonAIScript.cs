using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAIScript : MonoBehaviour {

    public enum SkeletonState {Patrol, Checking, Chase, Scream };

    public delegate void NextStep();
    private NextStep nextStep;

    //Animation
    private Animator anim;
    //Animation Hash String
    private int anim_Speed = Animator.StringToHash("Speed");
    private int anim_Attack = Animator.StringToHash("Attack");
    private int anim_Walk = Animator.StringToHash("Walk");
    private int anim_Chase = Animator.StringToHash("Chase");

    [Header("Skeleton AI")]
    public GameObject spawnPos;
    public GameObject[] patrolPoints;

    //Nav Mesh
    private NavMeshAgent navAgent;
    private float oriSpeed;

    //State
    SkeletonState skeletonState = SkeletonState.Patrol;

    //Component
    AIBaseScript aiBase;
    ForcePlayerToLookScript fpts;

    //IDLE
    private bool idleing = false;
    private IEnumerator idleEnumrator;
    [Header("IDLE")]
    public float checkSoundIDLE = 1.5f;

    [Header("Chase")]
    public float chaseSpeed = 3f;
    public AudioSource chasingAudioSource;

    [Header("Scream")]
    public GameObject bodyLight;
    public AudioSource screamAudioSource;

    void Awake() {

        anim = GetComponentInChildren<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        oriSpeed = navAgent.speed;

        aiBase = GetComponent<AIBaseScript>();
        aiBase.TrackPlayerPosition += CheckSound;
        aiBase.Chase += ChangeToChase;

        fpts = GetComponent<ForcePlayerToLookScript>();

    }

    // Use this for initialization
    void Start () {
		
	}

    void OnEnable() {

        transform.position = spawnPos.transform.position;

        navAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position);

    }

    // Update is called once per frame
    void Update () {
		
	}


    void FixedUpdate() {

        switch(skeletonState) {

            case SkeletonState.Patrol:
                IDLEWalkAnimationUpdate();
                MovingBehaviour();
                break;
            case SkeletonState.Checking:
                IDLEWalkAnimationUpdate();
                CheckingBehaviour();
                break;
            case SkeletonState.Chase:
                IDLEWalkAnimationUpdate();
                ChaseBehaviour();
                break;
            case SkeletonState.Scream:
                //Debug.Log(transform.position);
                break;

        }
        

    }

    //Function update animation speed
    void IDLEWalkAnimationUpdate() {

        anim.SetFloat(anim_Speed, navAgent.velocity.magnitude);

    }

    //Function to play attack animation
    void AttackAnimation() {

        anim.SetTrigger(anim_Attack);

    }


    void Patrol() {

        skeletonState = SkeletonState.Patrol;
        navAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position);

    }

    void MovingBehaviour() {

        Vector3 newPos = patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position;

        while(newPos == navAgent.desiredVelocity) {

            newPos = patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position;

        }

        if(NearTargetPos())
            navAgent.SetDestination(newPos);

    }

    bool NearTargetPos() {

        bool result = false;
        
        if(navAgent.stoppingDistance >= navAgent.remainingDistance)
            result = true;
        //Debug.Log(result);
        return result;

    }



    //Chase Script
    void ChangeToChase() {
        
        if(skeletonState == SkeletonState.Chase)
            return;

        skeletonState = SkeletonState.Chase;

        navAgent.speed = chaseSpeed;

        navAgent.SetDestination(GameManager.instance.playerObject.transform.position);

        anim.SetTrigger(anim_Chase);

        if(idleing) {

            StopCoroutine(idleEnumrator);
            idleing = false;
            idleEnumrator = null;

        }

        chasingAudioSource.Play();

    }

    void ChaseBehaviour() {

        navAgent.SetDestination(GameManager.instance.playerObject.transform.position);

    }


    //Scream Behaviour, will be call from the vision detection
    void ChangeToScream() {
        
        skeletonState = SkeletonState.Scream;

        navAgent.isStopped = true;
        navAgent.velocity = Vector3.zero;
        
        transform.LookAt(GameManager.instance.playerObject.transform.position, Vector3.up);

        anim.SetTrigger(anim_Attack);
        anim.SetFloat(anim_Speed, 0f);

        bodyLight.SetActive(true);

        chasingAudioSource.Stop();
        screamAudioSource.Play();

        GameManager.instance.playerBehaviour.LockPlayer();

        fpts.enabled = true;

    }

    public void KillPlayer() {

        GameManager.instance.PlayerDead();

    }

    //Function to check the sound
    void CheckSound(Vector3 point) {
        
        if(skeletonState == SkeletonState.Chase || skeletonState == SkeletonState.Scream)
            return;
       
        skeletonState = SkeletonState.Checking;

        if(idleing) {

            idleing = false;
            StopCoroutine(idleEnumrator);
            idleEnumrator = null;

        }

        navAgent.SetDestination(point);

    }

    void CheckingBehaviour() {

        if(idleing)
            return;

        if(NearTargetPos()) {

            //Patrol();
            nextStep = Patrol;
            idleEnumrator = IDLECoroutine(checkSoundIDLE);
            StartCoroutine(idleEnumrator);

        }

    }

    IEnumerator IDLECoroutine(float duration) {

        idleing = true;

        yield return new WaitForSeconds(duration);

        idleing = false;
        idleEnumrator = null;

        nextStep.Invoke();
        nextStep = null;

    }



    void OnTriggerEnter(Collider other) {

        if(skeletonState == SkeletonState.Chase) {

            if(other.gameObject == GameManager.instance.playerObject) {

                ChangeToScream();

            }


        }

    }


}
