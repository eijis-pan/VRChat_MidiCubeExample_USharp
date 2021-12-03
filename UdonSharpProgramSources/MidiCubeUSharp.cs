
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

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNoteOn))]
    private Int32 syncMidiNoteOn;
    public Int32 SyncMidiNoteOn
    {
        get => syncMidiNoteOn;
        set
        {
            syncMidiNoteOn = value;
            if (!Networking.IsMaster)
            {
                int channel = (value >> 16) & 0x7F;
                int number = (value >> 8) & 0x7F;
                int v = value & 0x7F;
                MidiNoteOn(channel, number, v);
            }
        }
    }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNoteOff))]
    private Int32 syncMidiNoteOff;
    public Int32 SyncMidiNoteOff
    {
        get => syncMidiNoteOff;
        set
        {
            syncMidiNoteOff = value;
            if (!Networking.IsMaster)
            {
                int channel = (value >> 16) & 0x7F;
                int number = (value >> 8) & 0x7F;
                int v = value & 0x7F;
                MidiNoteOff(channel, number, v);
            }
        }
    }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiControlChange))]
    private Int32 syncMidiControlChange;
    public Int32 SyncMidiControlChange
    {
        get => syncMidiControlChange;
        set
        {
            syncMidiControlChange = value;
            if (!Networking.IsMaster)
            {
                int channel = (value >> 16) & 0x7F;
                int number = (value >> 8) & 0x7F;
                int v = value & 0x7F;
                MidiControlChange(channel, number, v);
            }
        }
    }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_0))]
    private bool syncMidiNote_0;
    public bool SyncMidiNote_0 { get => syncMidiNote_0; set { syncMidiNote_0 = value; if (!Networking.IsMaster) { Fade(0, value); }}}

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_1))]
    private bool syncMidiNote_1;
    public bool SyncMidiNote_1 { get => syncMidiNote_1; set { syncMidiNote_1 = value; if (!Networking.IsMaster) { Fade(1, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_2))]
    private bool syncMidiNote_2;
    public bool SyncMidiNote_2 { get => syncMidiNote_2; set { syncMidiNote_2 = value; if (!Networking.IsMaster) { Fade(2, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_3))]
    private bool syncMidiNote_3;
    public bool SyncMidiNote_3 { get => syncMidiNote_3; set { syncMidiNote_3 = value; if (!Networking.IsMaster) { Fade(3, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_4))]
    private bool syncMidiNote_4;
    public bool SyncMidiNote_4 { get => syncMidiNote_4; set { syncMidiNote_4 = value; if (!Networking.IsMaster) { Fade(4, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_5))]
    private bool syncMidiNote_5;
    public bool SyncMidiNote_5 { get => syncMidiNote_5; set { syncMidiNote_5 = value; if (!Networking.IsMaster) { Fade(5, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_6))]
    private bool syncMidiNote_6;
    public bool SyncMidiNote_6 { get => syncMidiNote_6; set { syncMidiNote_6 = value; if (!Networking.IsMaster) { Fade(6, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_7))]
    private bool syncMidiNote_7;
    public bool SyncMidiNote_7 { get => syncMidiNote_7; set { syncMidiNote_7 = value; if (!Networking.IsMaster) { Fade(7, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_8))]
    private bool syncMidiNote_8;
    public bool SyncMidiNote_8 { get => syncMidiNote_8; set { syncMidiNote_8 = value; if (!Networking.IsMaster) { Fade(8, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_9))]
    private bool syncMidiNote_9;
    public bool SyncMidiNote_9 { get => syncMidiNote_9; set { syncMidiNote_9 = value; if (!Networking.IsMaster) { Fade(9, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_A))]
    private bool syncMidiNote_A;
    public bool SyncMidiNote_A { get => syncMidiNote_A; set { syncMidiNote_A = value; if (!Networking.IsMaster) { Fade(10, value); } } }

    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(SyncMidiNote_B))]
    private bool syncMidiNote_B;
    public bool SyncMidiNote_B { get => syncMidiNote_B; set { syncMidiNote_B = value; if (!Networking.IsMaster) { Fade(11, value); } } }

    private void Fade(int index, bool on)
    {
        var color = on ? onColorArray[index] : offColor;
        var duration = on ? 0f : 0.25f;
        noteImages[index].CrossFadeColor(color, duration, false, false);
    }

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

        if (Networking.IsMaster)
        {
            var on = true;

            /*
             * Set Image Color from Note in Octave
             */
            var remainder = number % 12; // noteImages.Length が 12 であることが前提
            Fade(remainder, on);

            switch (remainder)
            {
                case 0: { syncMidiNote_0 = on; break; }
                case 1: { syncMidiNote_1 = on; break; }
                case 2: { syncMidiNote_2 = on; break; }
                case 3: { syncMidiNote_3 = on; break; }
                case 4: { syncMidiNote_4 = on; break; }
                case 5: { syncMidiNote_5 = on; break; }
                case 6: { syncMidiNote_6 = on; break; }
                case 7: { syncMidiNote_7 = on; break; }
                case 8: { syncMidiNote_8 = on; break; }
                case 9: { syncMidiNote_9 = on; break; }
                case 10: { syncMidiNote_A = on; break; }
                case 11: { syncMidiNote_B = on; break; }
            }
            PackingValues(1, channel, number, velocity);
        }
    }

    public override void MidiNoteOff(int channel, int number, int velocity)
    {
        if (Networking.IsMaster)
        {
            var on = false;

            var remainder = number % 12; // noteImages.Length が 12 であることが前提
            Fade(remainder, on);

            switch (remainder)
            {
                case 0: { syncMidiNote_0 = on; break; }
                case 1: { syncMidiNote_1 = on; break; }
                case 2: { syncMidiNote_2 = on; break; }
                case 3: { syncMidiNote_3 = on; break; }
                case 4: { syncMidiNote_4 = on; break; }
                case 5: { syncMidiNote_5 = on; break; }
                case 6: { syncMidiNote_6 = on; break; }
                case 7: { syncMidiNote_7 = on; break; }
                case 8: { syncMidiNote_8 = on; break; }
                case 9: { syncMidiNote_9 = on; break; }
                case 10: { syncMidiNote_A = on; break; }
                case 11: { syncMidiNote_B = on; break; }
            }
            PackingValues(2, channel, number, velocity);
        }
    }

    public override void MidiControlChange(int channel, int number, int value)
    {
        ccSlider.value = Convert.ToSingle(value);
        ccSliderNumber.text = number.ToString();
        ccSliderValue.text = value.ToString();

        if (Networking.IsMaster)
        {
            PackingValues(0, channel, number, value);
        }
    }

    private void PackingValues(int type, int channel, int number, int v)
    {
        Int32 packingValues = ((channel & 0x7F) << 16) + ((number & 0x7F) << 8) + (v & 0x7F);
        switch (type)
        {
            case 0:
                {
                    syncMidiControlChange = packingValues;
                    break;
                }
            case 1:
                {
                    syncMidiNoteOn = packingValues;
                    break;
                }
            case 2:
                {
                    syncMidiNoteOff = packingValues;
                    break;
                }
        }
    }
}
