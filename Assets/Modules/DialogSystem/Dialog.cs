using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SubList
{
    [TextArea] public string[] replicas;
    public bool isEnd;
    public bool isNext;
    public int nextSequence;
    public bool isSequence;
    public int firstSequence;
    public int secondSequence;
}

public class Dialog : MonoBehaviour
{
    public List<SubList> sequences;
    public UnityEvent[] Events;

    public void StartDialog()
    {
        GameObject.FindObjectOfType<DialogCore>().StartDialog(GetComponent<Dialog>());
    }
}
