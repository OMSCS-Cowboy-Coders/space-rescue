using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCollectibleUIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    void Start()
    {
        int initialHealth = 3;
        updateHealth(initialHealth);
    }

    public void updateHealth(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
}
