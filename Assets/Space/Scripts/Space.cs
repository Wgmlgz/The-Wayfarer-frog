using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public Camera cam;
    public Animation god;

    private float stSize;
    private void Start()
    {
        stSize = cam.orthographicSize;
    }
    private void Update()
    {
        //float t = (cam.orthographicSize / stSize);
        //transform.localScale = new Vector3(t,t,t);
    }

    public void GodStart() {
        god.Play("GodAppear");
    }
    public void GodEnd() {
        god.Play("GodDisappear");
    }
}
