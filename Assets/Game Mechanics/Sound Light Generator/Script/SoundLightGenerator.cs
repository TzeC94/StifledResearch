using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SoundLightGenerator : MonoBehaviour {

    //Component instance
    private Light lightComp;

    public float effectRange = 1.5f;
    public float spreadSpeed = 1f;
    public float fadeOutSpeed = 0.1f;
    public float effectDuration = 2f;

    private bool isDoing = false;

    [Header("Raycast")]
    public bool doItemCheck = true;
    public LayerMask layerCast;

    [Header("Pool")]
    public PoolingScript pool;

    //String Tag
    private string tagEnemy = "Enemy";
    private string tagPaper = "Paper";
    private string tagEmi = "Emi Change";

    void Awake() {

        lightComp = GetComponent<Light>();

    }

    void OnEnable() {
        
        StartEffect();
        
        if(doItemCheck) {

            CheckEnemyAround();

        }

    }

    void SetPool(GameObject pool) {

        this.pool = pool.GetComponent<PoolingScript>();

    }

    public void StartEffect() {

        if(isDoing)
            return;

        lightComp.range = 0f;
        lightComp.intensity = 1f;

        isDoing = true;

        StartCoroutine(EffectFunction());

    }

    IEnumerator EffectFunction() {
        
        //Spread
        while(lightComp.range <= effectRange) {

            lightComp.range += spreadSpeed * Time.deltaTime;

            //lightComp.range = Mathf.Lerp(lightComp.range, effectRange, spreadSpeed * Time.deltaTime);

            yield return null;

        }
        
        yield return new WaitForSeconds(effectDuration);

        while(lightComp.intensity > 0f) {

            lightComp.intensity -= fadeOutSpeed * Time.deltaTime;

            //lightComp.intensity = Mathf.Lerp(lightComp.intensity, 0f, fadeOutSpeed * Time.deltaTime);
            
            yield return null;

        }
        
        isDoing = false;

        gameObject.SetActive(false);

        pool.KeepThis(gameObject);

    }

    void CheckEnemyAround() {

        /*
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, effectRange, Vector3.up, layerCast);
        
        if(hits.Length != 0) {
            
            foreach(RaycastHit hit in hits) {

                if(hit.collider.gameObject.tag == "Enemy") {

                    hit.collider.gameObject.GetComponent<AIBaseScript>().TrackPlayerPosition.Invoke(transform.position);
                    
                }

                if(hit.collider.gameObject.tag == "Paper") {
                    
                    hit.collider.gameObject.GetComponent<PaperScript>().ChangeHighlighted();

                }

                if(hit.collider.gameObject.tag == "Emi Change") {

                    hit.collider.gameObject.GetComponent<SwitchEmiScript>().ChangeHighlighted();

                }

            }

        }
        */

        Collider[] colli;
        colli = Physics.OverlapSphere(transform.position, effectRange, layerCast);

        if(colli.Length != 0) {

            foreach(Collider col in colli) {

                if(col.gameObject.tag == tagEnemy) {
                    
                    col.gameObject.GetComponent<AIBaseScript>().TrackPlayerPosition.Invoke(transform.position);
                    
                }

                if(col.gameObject.tag == tagPaper) {
                    
                    col.gameObject.GetComponent<PaperScript>().ChangeHighlighted();

                }

                if(col.gameObject.tag == tagEmi) {

                    col.gameObject.GetComponent<SwitchEmiScript>().ChangeHighlighted();

                }

            }

        }

    }

}
