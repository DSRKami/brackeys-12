using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntryIncrease : MonoBehaviour
{
    public TextMeshProUGUI gForceText;
    public TextMeshProUGUI distanceText;

    public float gForceValue = 4.78f;
    public float targetgForceValue = 24.89f;
    public float distanceValue = 159000f;
    public float targetDistanceValue = 69000f;

    public float totalTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the gForce value variation
        StartCoroutine(Approach.LerpValues(gForceValue, targetgForceValue, totalTime, UpdateGForce));

        // Start the distance value variation
        StartCoroutine(Approach.LerpValues(distanceValue, targetDistanceValue, totalTime, UpdateDistance));
    }

    // Update the gForce value as LerpValue progresses
    void UpdateGForce(float newGValue)
    {
        gForceValue = newGValue;
        gForceText.text = $"{gForceValue:F2}M/S^2"; // Update UI Text
    }

    // Update the distance value as LerpValue progresses
    void UpdateDistance(float newDistanceValue)
    {
        distanceValue = newDistanceValue;
        distanceText.text = $"{distanceValue:F0}km"; // Update UI Text
    }
}
