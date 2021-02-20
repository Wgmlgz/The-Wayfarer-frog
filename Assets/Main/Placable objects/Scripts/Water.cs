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
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == PL) {
            AudioManager.AudioManager.m_instance.PlaySFX("water_in");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == PL) {
            AudioManager.AudioManager.m_instance.PlaySFX("water_out");
        }
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
