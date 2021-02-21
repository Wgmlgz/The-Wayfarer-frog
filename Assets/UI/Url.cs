using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Url : MonoBehaviour
{
    public void openUrl(string url)
    {
        Application.OpenURL(url);
    }
}
