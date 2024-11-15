using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventVolume : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public string playerString;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerEvent != null && other.gameObject.name == playerString)
        {
            triggerEvent.Invoke();
        }
    }
}
