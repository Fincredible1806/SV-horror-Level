using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private ThirdPersonController player;
    [SerializeField] private string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            player = other.GetComponent<ThirdPersonController>();
            player.PlayerTakeDamage();
        }
    }
}
