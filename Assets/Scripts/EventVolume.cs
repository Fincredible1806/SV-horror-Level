using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventVolume : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public string playerString;
    [Header("Leave")]
    public UnityEvent leaveEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEvent != null && other.gameObject.name == playerString)
        {
            triggerEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (leaveEvent != null && other.gameObject.name == playerString)
        {
            leaveEvent.Invoke();
        }
    }
}
