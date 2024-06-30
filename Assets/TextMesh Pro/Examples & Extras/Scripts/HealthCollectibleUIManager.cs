using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCollectibleUIManager : MonoBehaviour
{
    // public TextMeshProUGUI healthCollectibleText;
    public Text healthCollectibleText;
    private int healthCollectibleCount = 3;

    void Start()
    {
        UpdateHealthCollectibleText();
    }

    public void IncrementHealthCollectibleCount()
    {
        healthCollectibleCount++;
        UpdateHealthCollectibleText();
    }

    public void UpdateHealthCollectibleText()
    {
        healthCollectibleText.text = "Health Collectibles: " + healthCollectibleCount.ToString();
        
    }
}
