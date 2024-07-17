using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCollectibleUIManager : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;

    void Start()
    {
        int initialHealth = 3;
        updateHealth(initialHealth);
    }

    public void updateHealth(int health)
    {
        if (health==3) {
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        } else if (health == 2) {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        } else if (health == 1) {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(true);
        }
    }
}
