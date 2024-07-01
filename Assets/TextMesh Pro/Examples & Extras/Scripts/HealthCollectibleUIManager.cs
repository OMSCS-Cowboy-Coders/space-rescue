using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCollectibleUIManager : MonoBehaviour
{
    // public TextMeshProUGUI healthCollectibleText;
    public TextMeshProUGUI healthCollectibleText;

    void Start()
    {
        UpdateHealthCollectibleText(3);
    }

    public void UpdateHealthCollectibleText(int health)
    {
        healthCollectibleText.text = "Health: " + health.ToString();
    }
}
