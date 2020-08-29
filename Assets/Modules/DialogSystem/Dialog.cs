using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SubList
{
    [TextArea] public string[] replicas;
    public int firstSequence = -1;
    public int secondSequence = -1;
}

public class Dialog : MonoBehaviour
{
    public List<SubList> sequences;
    public UnityEvent[] Events;
    public Cinemachine.CinemachineVirtualCamera cam;
    public Transform stayPos;
    public Transform target;

    public void StartDialog()
    {
        GameObject.FindObjectOfType<DialogCore>().StartDialog(GetComponent<Dialog>());
        if(stayPos != null && target != null)
        {
            target.position = stayPos.position;
            target.rotation = stayPos.rotation;
        }
    }
}
