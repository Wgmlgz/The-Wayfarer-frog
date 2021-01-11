using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public bool fast;
    GameObject PL;
    private void Start() {
        PL = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject == PL){
            if(!fast){
                PL.GetComponent<ZhabaController>().Slow();
            }else{
                PL.GetComponent<ZhabaController>().Fast();
            }
        }
    }
}
