using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableObject : MonoBehaviour
{
    public bool active = true;
    [SerializeField] GameObject pickupUI;
    [SerializeField] UnityEvent interactEvent;
    [SerializeField] Collider rangeCollider;
    private bool isInRange;
    private GameObject player;
    [SerializeField]private string playerName;
    [SerializeField] private KeyCode interactKey = KeyCode.F;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && active)
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
            if(pickupUI != null && active) 
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

    public void Activate()
    {
        active = true;
        if (isInRange && pickupUI != null)
        {
            pickupUI.SetActive(true);
        }
    }
}
