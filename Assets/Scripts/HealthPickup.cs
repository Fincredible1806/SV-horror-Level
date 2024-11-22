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
            if (controller != null)
            {
                if(pickupClip != null)
                {
                    AudioSource.PlayClipAtPoint(pickupClip, gameObject.transform.position);

                }
                controller.PlayerHeal();
                Destroy(gameObject);
            }
        }
    }
}
