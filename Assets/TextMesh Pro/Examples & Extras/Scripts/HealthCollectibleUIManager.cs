using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthCollectibleUIManager : MonoBehaviour
{
    public TextMeshProUGUI healthCollectibleText;
    private int healthCollectibleCount = 0;

    void Start()
    {
        UpdateHealthCollectibleText();
    }

    public void IncrementHealthCollectibleCount()
    {
        healthCollectibleCount++;
        UpdateHealthCollectibleText();
    }

    private void UpdateHealthCollectibleText()
    {
        healthCollectibleText.text = "Health Collectibles: " + healthCollectibleCount.ToString();
    }
}
