using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] string playerTag;
    AudioClip pickupClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {

            ThirdPersonController controller = other.GetComponent<ThirdPersonController>();
            HealthStorage storage = other.GetComponent <HealthStorage>();
            if (controller != null)
            {
                if(pickupClip != null)
                {
                    AudioSource.PlayClipAtPoint(pickupClip, gameObject.transform.position);

                }
                if (controller.Health < 100)
                {
                    controller.PlayerHeal();
                }
                if (controller.Health >= 100)
                {
                    storage.addItem();
                }
                Destroy(gameObject);
            }
        }
    }
}
