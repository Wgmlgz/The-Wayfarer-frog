using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public string var_name;
    private void Awake()
    {
        GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat(var_name);
    }
    public void valueChanged() {
        PlayerPrefs.SetFloat(var_name, GetComponent<Scrollbar>().value);
    }
}
