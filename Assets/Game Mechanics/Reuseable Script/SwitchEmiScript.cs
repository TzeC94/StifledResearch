using UnityEngine;

public class SwitchEmiScript : MonoBehaviour {

    private bool highlighted = false;

    public void ChangeHighlighted() {

        if(highlighted)
            return;

        highlighted = true;
        
        Renderer rend = GetComponent<Renderer>();
        
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        mpb.SetColor("_EmissionColor", Color.white);

        rend.SetPropertyBlock(mpb);

    }

}
