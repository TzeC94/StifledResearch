using UnityEngine;

public class PaperScript : MonoBehaviour {

    private bool highlighted = false;
    
    [Header("Audio")]
    public AudioClip tearPaperClip;

    public void ChangeHighlighted() {

        if(highlighted)
            return;

        highlighted = true;
        
        Renderer rend = GetComponent<Renderer>();
        
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        mpb.SetColor("_EmissionColor", Color.white);

        rend.SetPropertyBlock(mpb);

    }

    public void CollectPaper() {

        AudioSource.PlayClipAtPoint(tearPaperClip, transform.position);

        GameManager.instance.PlayerWin();

        gameObject.SetActive(false);

    }

}
