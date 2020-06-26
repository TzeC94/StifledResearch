using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseScript : MonoBehaviour {

    public delegate void PassWithPosition(Vector3 point);
    public delegate void NormalFunction();

    public PassWithPosition TrackPlayerPosition;
    public NormalFunction Chase;

}
