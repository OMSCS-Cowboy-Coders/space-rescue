using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    private PlayerMetrics playerMetrics;
    public RectTransform sprintFill;
    public float initialSprintFillWidth;
    // Start is called before the first frame update
    void Start()
    {
        playerMetrics = FindObjectOfType<PlayerMetrics>();
        initialSprintFillWidth = sprintFill.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {

        float sprintLeft = playerMetrics.getSprintEnergy();
        updateSprintBar(sprintLeft);
        // print("This is sprint left: " + sprintLeft);
    }
    void updateSprintBar(float sprintLeft) {
        float percentage = sprintLeft / 100; // sprintFill is expressed as a percentage.
        float width = Mathf.Min(initialSprintFillWidth * percentage, initialSprintFillWidth);
        sprintFill.sizeDelta = new Vector2(width, sprintFill.sizeDelta.y);
    }
}