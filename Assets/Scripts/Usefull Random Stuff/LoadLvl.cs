using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    public string lvlName;

    public void Load()
    {
        SceneManager.LoadScene(lvlName);
    }
    public void LoadByStr(string s)
    {
        SceneManager.LoadScene(s);
    }
    public void SetStr(string s)
    {
        lvlName = s;
    }
}
