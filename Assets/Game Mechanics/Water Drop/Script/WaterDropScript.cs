using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropScript : MonoBehaviour {

    private ParticleSystem ps;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    public AudioSource waterDripAudioSource;
    public Transform soundLightGeneratePos;
    public PoolingScript waterDropLightPool;

    void Awake() {

        ps = GetComponent<ParticleSystem>();

    }

    void OnParticleTrigger(){
        
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.remainingLifetime = 0f;
            waterDropLightPool.Spawn(soundLightGeneratePos.position);
            waterDripAudioSource.Play();
        }

    }

}
