using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogCore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI text1;
    public TMPro.TextMeshProUGUI text2;

    public GameObject regularGM;
    public GameObject sequenceGM;

    [Header("Events")]
    public UnityEvent onStart;
    public UnityEvent onEnd;
    [Header("private")]
    public Dialog npc;
    public int activeSequence;
    public int activeReplica;

    public void StartDialog(Dialog dialogRequester)
    {
        npc = dialogRequester;
        StartSequence(0);
        if (npc.cam != null)
        {
            Cinemachine.CinemachineVirtualCamera cam = npc.cam;
            cam.Priority = 9999;
        }
        onStart.Invoke();
    }
    public void DisplayNext()
    {
        if(activeReplica < npc.sequences[activeSequence].replicas.Length - 1)
        {
            activeReplica += 1;
            DisplaySimple(npc.sequences[activeSequence].replicas[activeReplica]);
        }
        else
        {
            FinishSequence();
        }
    }
    public void SelectFirst()
    {
        StartSequence(npc.sequences[activeSequence].firstSequence, 1);
    }
    public void SelectSecond()
    {
        StartSequence(npc.sequences[activeSequence].secondSequence, 1);
    }

    //private methods
    private void DisplaySimple(string s)
    {
        regularGM.SetActive(true);
        sequenceGM.SetActive(false);

        text.text = s;
        text.gameObject.GetComponent<TextAnimation>().StartAnimation();

        string[] tmp = s.Split('#');
        if(tmp.Length > 1)
        {
            npc.Events[int.Parse(tmp[1])].Invoke();
            DisplayNext();
        }
    }
    private void DisplaySequence(string s1, string s2)
    {
        regularGM.SetActive(false);
        sequenceGM.SetActive(true);

        text1.text = s1;
        text1.gameObject.GetComponent<TextAnimation>().StartAnimation();
        text2.text = s2;
        text2.gameObject.GetComponent<TextAnimation>().StartAnimation();
    }
    public void HideUI(bool b = true)
    {
        regularGM.SetActive(false);
        sequenceGM.SetActive(false);

        if(npc){
            if (npc.cam)
            {
                Cinemachine.CinemachineVirtualCamera cam = npc.cam;
                cam.Priority = -9999;
            }
        }
        if(b) onEnd.Invoke();
    }
    private void StartSequence(int n, int startReplica = 0)
    {
        activeSequence = n;
        activeReplica = startReplica;

        if (npc.sequences[activeSequence].replicas.Length > 0)
        {
            DisplaySimple(npc.sequences[activeSequence].replicas[activeReplica]);
        }
        else
        {
            FinishSequence();
        }
    }
    private void FinishSequence()
    {
        int t1 = npc.sequences[activeSequence].firstSequence;
        int t2 = npc.sequences[activeSequence].secondSequence;

        if(t1 == -1)
        {
            HideUI();
        }
        else if(t2 == -1)
        {
            StartSequence(t1);
        }
        else
        {
            DisplaySequence(
                npc.sequences[npc.sequences[activeSequence].firstSequence].replicas[0],
                npc.sequences[npc.sequences[activeSequence].secondSequence].replicas[0]
            );
        }
    }
    private void Start()
    {
        HideUI(false);
    }
}
