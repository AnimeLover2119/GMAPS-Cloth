using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISliderController : MonoBehaviour
{

    public WindController windController;
    public Slider windForce;
    public Slider flutterIntensity;
    public Slider flutterFrequency;
    public Slider oscillationAmplitude;
    public Slider oscillationFrequency;


    public TextMeshProUGUI windForceText;
    public TextMeshProUGUI flutterIntensityText;
    public TextMeshProUGUI flutterFrequencyText;
    public TextMeshProUGUI oscillationAmplitudeText;
    public TextMeshProUGUI oscillationFrequencyText;


    void Start()
    {
        // set the current slider values to the default values assigned in each script
        windForce.value = windController.baseWindForce;
        flutterIntensity.value = windController.flutterIntensity;
        flutterFrequency.value = windController.flutterFrequency;
        oscillationAmplitude.value = windController.amplitude;
        oscillationFrequency.value = windController.frequency;
    }

    void Update()
    {
        // display current slider values for each wind attribute
        windForceText.text = "Wind Force: " + windController.baseWindForce.ToString("F1");
        flutterIntensityText.text = "Intensity: " + windController.flutterIntensity.ToString("F1");
        flutterFrequencyText.text = "Frequency: " + windController.flutterFrequency.ToString("F1");
        oscillationAmplitudeText.text = "Amplitude: " + windController.amplitude.ToString("F1");
        oscillationFrequencyText.text = "Frequency: " + windController.frequency.ToString("F1");
    }


    // below are methods that are assigned to each slider when an attribute is changed
    // it will simply set the new value for the respective WindController attributes based on what is selected on the slider
    public void OnWindForceChanged (float value)
    {
        windController.baseWindForce = value;
    }

    public void OnFlutterIntensityChanged (float value)
    {
        windController.flutterIntensity = value;
    }

    public void OnFlutterFrequencyChanged(float value)
    {
        windController.flutterFrequency = value;
    }

    public void OnOscillationAmplitudeChanged(float value)
    {
        windController.amplitude = value;
    }

    public void OnOscillationFrequencyChanged(float value)
    {
        windController.frequency = value;
    }


}
