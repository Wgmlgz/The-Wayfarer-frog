using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI text1;
    public TMPro.TextMeshProUGUI text2;

    public GameObject regularGM;
    public GameObject sequenceGM;

    [Header("private")]
    public Dialog npc;
    public int activeSequence;
    public int activeReplica;

    public void StartDialog(Dialog dialogRequester)
    {
        npc = dialogRequester;
        StartSequence(0);
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
    private void HideUI()
    {
        regularGM.SetActive(false);
        sequenceGM.SetActive(false);
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
        if (npc.sequences[activeSequence].isNext)
        {
            StartSequence(npc.sequences[activeSequence].nextSequence);
        }
        else if (npc.sequences[activeSequence].isSequence)
        {
            DisplaySequence(
                npc.sequences[npc.sequences[activeSequence].firstSequence].replicas[0],
                npc.sequences[npc.sequences[activeSequence].secondSequence].replicas[0]
            );
        }
        else
        {
            HideUI();
        }
    }
    private void Start()
    {
        HideUI();
    }
}
