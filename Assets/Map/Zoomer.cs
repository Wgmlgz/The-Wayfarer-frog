using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Zoomer : MonoBehaviour {
    public float min;
    public float max;
    public Scrollbar sb;
    public Camera cam;

    private void Update() {
        cam.orthographicSize = sb.value * (max - min) + min;
    }
}
