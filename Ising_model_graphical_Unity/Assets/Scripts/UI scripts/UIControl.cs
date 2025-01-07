using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIControl : MonoBehaviour
{
    GameObject IsingModelSimulation_;

    public Slider coldnessSlider;
    public TextMeshProUGUI coldnessIndicator;

    public Slider ambientMagneticFieldSlider;
    public TextMeshProUGUI ambientMagneticFieldIndicator;

    public Slider dimensionsXSlider;
    public TextMeshProUGUI dimensionsXIndicator;

    public Slider dimensionsYSlider;
    public TextMeshProUGUI dimensionsYIndicator;

    public Slider dimensionsZSlider;
    public TextMeshProUGUI dimensionsZIndicator;

    void Start()
    {
        coldnessSlider.value = .5f;
        ambientMagneticFieldSlider.value = .5f;
        dimensionsXSlider.value = .6f;
        dimensionsYSlider.value = .6f;
        dimensionsZSlider.value = .01f;
    }

    void Update()
    {
        IsingModelSimulation_ = GameObject.Find("IsingModelSimulation");
        IsingModelSim.IsingModelSim IsingModelSim_ = IsingModelSimulation_.GetComponent<IsingModelSim.IsingModelSim>();

        coldnessIndicator.text = coldnessSlider.value.ToString("0.0");
        IsingModelSim_.beta = 0.6f*coldnessSlider.value;

        ambientMagneticFieldIndicator.text = (-10 + 20f*ambientMagneticFieldSlider.value).ToString("0.0");
        IsingModelSim_.ambientMagneticField = (-10 + 20f*ambientMagneticFieldSlider.value);

        dimensionsXIndicator.text = ((int) 1 + 99f*dimensionsXSlider.value).ToString("0");
        IsingModelSim_.dimensions.x = (int) (1 + 99f*dimensionsXSlider.value);

        dimensionsYIndicator.text = ((int) 1 + 99f*dimensionsYSlider.value).ToString("0");
        IsingModelSim_.dimensions.y = (int) (1 + 99f*dimensionsYSlider.value);

        dimensionsZIndicator.text = ((int) 1 + 99f*dimensionsZSlider.value).ToString("0");
        IsingModelSim_.dimensions.z = (int) (1 + 99f*dimensionsZSlider.value);
    }
}
