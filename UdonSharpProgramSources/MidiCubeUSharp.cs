
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class MidiCubeUSharp : UdonSharpBehaviour
{
    public Image[] noteImages;
    public Text channelText;
    public Text velocityText;
    public Text numberText;
    public Color offColor;
    public Slider ccSlider;
    public Text ccSliderNumber;
    public Text ccSliderValue;
    public Color[] onColorArray;
    
    void Start()
    {
        foreach (var image in noteImages)
        {
            image.CrossFadeColor(offColor, 0, false, false);
        }
    }

    public override void MidiNoteOn(int channel, int number, int velocity)
    {
        /*
         * Set Image Color from Note in Octave
         */
        var remainder = number % 12; // noteImages.Length が 12 であることが前提
        noteImages[remainder].CrossFadeColor(onColorArray[remainder], 0, false, false);
        
        /*
         * Set Channel Text
         */
        channelText.text = string.Format($"C {channel}", channel);
        
        /*
         * Set Number Text
         */
        numberText.text = string.Format($"N {number}", number);
        
        /*
         * Set Velocity Text
         */
        velocityText.text = string.Format($"V {velocity}", velocity);
    }

    public override void MidiNoteOff(int channel, int number, int velocity)
    {
        var remainder = number % 12; // noteImages.Length が 12 であることが前提
        noteImages[remainder].CrossFadeColor(offColor, 0.25f, false, false);
    }

    public override void MidiControlChange(int channel, int number, int value)
    {
        ccSlider.value = Convert.ToSingle(value);
        ccSliderNumber.text = number.ToString();
        ccSliderValue.text = value.ToString();
    }
}
