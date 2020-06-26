using UnityEngine;

public class SoundLightManager : MonoBehaviour {

    public static SoundLightManager instance;

    [Header("Pool")]
    public PoolingScript lightGeneratorPool;
    public PoolingScript shortLightGeneratorPool;

    void Awake() {

        instance = this;

    }

    public void SpawnThrowingSound(Vector3 position) {

        lightGeneratorPool.Spawn(position);

    }

    public void SpawnShortSound(Vector3 position) {

        shortLightGeneratorPool.Spawn(position);

    }

}
