using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamps : MonoBehaviour
{
    public GameObject god;
    public GameObject box;
    public GameObject triangle;
    public GameObject coin;
    public GameObject ok;

    private void Start() {
        if (PlayerPrefs.GetInt("SGod") == 1) {
            god.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (PlayerPrefs.GetInt("SBox") == 1) {
            box.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    
}
