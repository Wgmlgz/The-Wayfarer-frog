using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCamOffset : MonoBehaviour {
    private GameObject ZHB;
    private ZhabaController ZH;
    private GameObject cam_target;
    public GameObject ded;
    public float offset;
    public float cam_size;

    private void Awake() {
        ZH = GameObject.FindGameObjectWithTag("Player").GetComponent<ZhabaController>();
        ZHB = GameObject.FindGameObjectWithTag("ZhabkaBody");
        cam_target = GameObject.FindGameObjectWithTag("CameraTarget");
    }

    void OnTriggerStay2D() {
        cam_target.GetComponent<ZhCamTarget>().da = true;
        cam_target.GetComponent<ZhCamTarget>().timer = 0;
        var t = ZHB.transform.position;
        if ((new Vector3(t.x, ded.transform.position.y + offset, t.z) - cam_target.GetComponent<ZhCamTarget>().lt).magnitude > 10) {
            Debug.LogError(new Vector3(t.x, ded.transform.position.y + offset, t.z));
        }
        cam_target.transform.position = new Vector3(t.x, ded.transform.position.y + offset, t.z);
        ZH.const_cam_size = Mathf.Lerp(ZH.gameCam.m_Lens.OrthographicSize, cam_size, Time.deltaTime * 10);
    }
}
