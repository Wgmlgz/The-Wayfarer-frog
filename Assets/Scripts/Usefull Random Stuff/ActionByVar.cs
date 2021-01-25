using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionByVar : MonoBehaviour {
    [Header("When Work?")]
        public bool doAwake;
        public bool doStart;
        public bool doUpdate;

    [Header("Str Action")]
        public bool do_by_str;
        public string str_name;
        public string str_cmp;
    [Header("Bool Action")]
        public bool doByBool;
        public string boolName;
    [Header("Events")]
        public UnityEvent onTrue;
        public UnityEvent onFalse;
        public UnityEvent onRefresh;

    string tmpName;
    [SerializeField] UnityEvent evt;
    public void SetName(string name) {
        tmpName = name;
    }
    public void SetStr(string value) {
        PlayerPrefs.SetString(tmpName, value);
    }
    public void SetBool(int value) {
        PlayerPrefs.SetInt(tmpName, value);
    }
    public void SetBool(bool value) {
        PlayerPrefs.SetInt(tmpName, value ? 1 : 0);
    }
    public void DoIfBool(string name) {
        if (PlayerPrefs.GetInt(name) != 0) {
            evt.Invoke();
        }
    }
    public void IncInt(string name) {
        PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + 1);
    }
    void Refresh() {
        if (doByBool) {
            if (boolName != null || boolName != "") {
                SetName(boolName);
            }
            bool t = PlayerPrefs.GetInt(boolName) != 0;
            if (t) {
                onTrue.Invoke();
            } else {
                onFalse.Invoke();
            }
        }
        if (do_by_str) {
            if (str_name != null || str_name != "") {
                SetName(str_name);
            }
            bool t = PlayerPrefs.GetString(str_name) == str_cmp;
            if (t) {
                onTrue.Invoke();
            } else {
                onFalse.Invoke();
            }
        }
        onRefresh.Invoke();
    }

    void Awake() {
        if (doAwake) {
            Refresh();
        }
    }
    void Start() {
        if (doStart) {
            Refresh();
        }
    }

    void Update() {
        if (doUpdate) {
            Refresh();
        }
    }
}
