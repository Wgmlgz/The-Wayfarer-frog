using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour {
    public List<GameObject> hats;
    public void selectHat(int id) {
        SelectUtil.selectOne(hats, id);
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
