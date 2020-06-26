using UnityEngine;

public class CollideSoundScript : MonoBehaviour {

    [Header("Collide Sound")]
    public AudioSource collideAudioSource;

    private string playerTag = "Player";

    void OnCollisionEnter(Collision collision) {

        if(collision.gameObject.tag != playerTag) {
            
            PlayCollideSound(collision.impulse.magnitude);

        }

    }

    void PlayCollideSound(float impulse) {

        collideAudioSource.pitch = Random.Range(0.5f, 1.5f);
        collideAudioSource.PlayOneShot(collideAudioSource.clip, impulse / 10f);

    }

}
