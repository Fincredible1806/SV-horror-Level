using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] GameObject pickupUI;
    [SerializeField] UnityEvent interactEvent;
    [SerializeField] Collider rangeCollider;
    private bool isInRange;
    [SerializeField] GameObject player;
    [SerializeField] KeyCode interactKey;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactEvent.Invoke();
                Debug.Log("Event Triggered");
                Destroy(pickupUI.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player) 
        {
            isInRange = true;
            if(pickupUI != null) 
            {
                pickupUI.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            isInRange = false;
            if (pickupUI != null)
            {
                pickupUI.SetActive(false);
            }
        }
    }
}
