using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventsDude : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onUpdate;

    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        onUpdate.Invoke();
    }
}
