using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metres : MonoBehaviour
{
    // Current values for fuel, scrap, and energy
    public static float fuel;
    public static float scrap;
    public static float energy;

    // Maximum values for fuel, scrap, and energy
    public static float maxFuel = 100f;
    public static float maxScrap = 100f;
    public static float maxEnergy = 100f;

    // UI sliders for fuel, scrap, and energy
    public Slider fuelSlider;
    public Slider scrapSlider;
    public Slider energySlider;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial values for the sliders
        InitializeSliders();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the sliders to reflect the current values
        UpdateSliders();
    }

    // Set up the sliders with max values
    void InitializeSliders()
    {
        // Set the max values for each slider
        fuelSlider.maxValue = maxFuel;
        scrapSlider.maxValue = maxScrap;
        energySlider.maxValue = maxEnergy;

        // Set the current values for each slider
        fuelSlider.value = fuel;
        scrapSlider.value = scrap;
        energySlider.value = energy;
    }


    // Update the sliders to reflect the current values
    void UpdateSliders()
    {
        // Update the slider values based on current fuel, scrap, and energy levels
        fuelSlider.value = fuel;
        scrapSlider.value = scrap;
        energySlider.value = energy;
    }
}
