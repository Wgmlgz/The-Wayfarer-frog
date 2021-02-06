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

    private void Awake() {
        string s = gameObject.name + ": \n";
        int c = 0;
        foreach (var i in sequences) {
            s += "  ";
            s += c.ToString();
            s += " ----\n";
            ++c;
            foreach (var j in i.replicas) {
                s += "    ";
                s += j;
                s += "\n";
            }
            s += "      ";
            s += i.firstSequence;
            s += " / ";
            s += i.secondSequence;
            s += "";
            s += "\n";
        }
        Debug.Log(s);
    }
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
