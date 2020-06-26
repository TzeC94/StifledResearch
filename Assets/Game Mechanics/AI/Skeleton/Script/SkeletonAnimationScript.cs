using UnityEngine;

public class SkeletonAnimationScript : MonoBehaviour {

    void KillPlayer() {

        GetComponentInParent<SkeletonAIScript>().KillPlayer();

    }
	
}
