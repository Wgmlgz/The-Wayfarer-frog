using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionByVar : MonoBehaviour
{
    [Header("When Work?")]
        public bool doAwake;
        public bool doStart;
        public bool doUpdate;

    [Header("Bool Action")]
        public bool doByBool;
        public string boolName;
        public UnityEvent onTrue;
        public UnityEvent onFalse;

    string tmpName;
    [SerializeField] UnityEvent evt;
    public void SetName(string name)
    {
        tmpName = name;
    }
    public void SetBool(int value)
    {
        PlayerPrefs.SetInt(tmpName, value);
    }
    public void SetBool(bool value)
    {
        PlayerPrefs.SetInt(tmpName, value?1:0);
    }
    public void DoIfBool(string name)
    {
        if (PlayerPrefs.GetInt(name)!=0)
        {
            evt.Invoke();
        }
    }
    void Refresh()
    {
        if (doByBool)
        {
            if(boolName != null || boolName != "")
            {
                SetName(boolName);
            }
            bool t = PlayerPrefs.GetInt(boolName) != 0;
            if (t)
            {
                onTrue.Invoke();
            }
            else
            {
                onFalse.Invoke();
            }
        }
    }

    void Awake()
    {
        if (doAwake)
        {
            Refresh();
        }
    }
    void Start()
    {
        if (doStart)
        {
            Refresh();
        }
    }

    void Update()
    {
        if (doUpdate)
        {
            Refresh();
        }
    }
}
