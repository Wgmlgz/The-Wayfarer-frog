using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstZhabkaPos : MonoBehaviour {
    private GameObject ZHB;
    private GameObject ZH;
    public GameObject cam_target;
    public GameObject ded;
    public Vector2 vel;
    public float offset;
    public float speed;
    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player");
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        //cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame

    public void UpdateZhPos() {
        cam_target.GetComponent<ZhCamTarget>().da = true;
        var t = ZHB.transform.position;

        cam_target.transform.position = new Vector3(t.x, ded.transform.position.y + offset, t.z);
        t = ZH.transform.position;
        ZH.transform.position = new Vector3(t.x,
            (t.y - ded.transform.position.y) < 2f * offset? t.y: ded.transform.position.y + 2f*offset
            ,
            t.z);

        ZH.GetComponent<Rigidbody2D>().velocity = new Vector2(
            speed, (t.y - ded.transform.position.y) < 2f*offset ? ZH.GetComponent<Rigidbody2D>().velocity.y : 0
            );
    }
}
