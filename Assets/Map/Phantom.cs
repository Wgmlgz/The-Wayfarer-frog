using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour {
    public RectTransform space;
    public RectTransform target;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //transform.position = new Vector2(Mathf.Clamp(target.transform.position.x, space.position.x, space.position.x + space.sizeDelta.x), 0);
    }
}
