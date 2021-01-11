using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ImgLine : MonoBehaviour {
    public RectTransform from;
    public RectTransform target;

    public RectTransform obj;

    void Start() {

    }

    // Update is called once per frame
    void LateUpdate() {
        obj.position = from.position;

        var delta = target.position - from.position;
        obj.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);

        obj.localScale = new Vector2(delta.magnitude, obj.localScale.y);
    }
}
