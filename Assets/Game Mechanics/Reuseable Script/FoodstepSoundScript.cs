using UnityEngine;

public class FoodstepSoundScript : MonoBehaviour {

    public AudioSource audioSource;

    [Header("Footstep")]
    public AudioClip[] walkClip;

    void WalkStep() {

        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.PlayOneShot(walkClip[Random.Range(0, walkClip.Length)]);

        SoundLightManager.instance.SpawnShortSound(audioSource.gameObject.transform.position);

    }
	
}
