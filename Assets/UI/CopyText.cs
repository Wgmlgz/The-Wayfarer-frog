using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI from;
    public TMPro.TextMeshProUGUI to;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        to.SetText(from.text);
    }
}
