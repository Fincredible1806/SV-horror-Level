using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthStorage : MonoBehaviour
{
    public int healthItems = 0;
    [SerializeField] TextMeshProUGUI healthText;
    private float maxHealth;

    ThirdPersonController player;
    [SerializeField] KeyCode healButton;


    private void Start()
    {
        player = GetComponent<ThirdPersonController>();
        maxHealth = player.Health;
        healthUIupdate();
    }
    private void Update()
    {
        if(Input.GetKeyDown(healButton))
        {
            if (healthItems > 0 && player.Health < maxHealth)
            {
                player.PlayerHeal();
                healthItems--;
                healthUIupdate();
            }
        }
    }

    public void addItem()
    {
        healthItems++;
        healthUIupdate();
    }

    private void healthUIupdate()
    {
        healthText.text = ":" + healthItems.ToString();
    }
}
