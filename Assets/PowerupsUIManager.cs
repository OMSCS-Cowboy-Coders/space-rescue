using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI sprintPowerupsText;

    void Start()
    {
        updateSprintPowerupsText(0);
    }

    // Update is called once per frame
    public void updateSprintPowerupsText(int n)
    {
        sprintPowerupsText.text = "Sprint powerups: " + n.ToString();
    }
}
