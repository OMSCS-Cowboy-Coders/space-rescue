using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsUIManager : MonoBehaviour
{
    public TextMeshProUGUI sprintPowerupsText;

    void Start()
    {
        int initialNumSprintPowerups = 0;
        updateSprintPowerups(initialNumSprintPowerups);
    }

    public void updateSprintPowerups(int sprintPowerups)
    {
        sprintPowerupsText.text = "x " + sprintPowerups.ToString();
    }
}
