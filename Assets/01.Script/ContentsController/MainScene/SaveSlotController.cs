using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotController : MonoBehaviour
{
    public GameObject emptyObject;
    public GameObject dataUIObject;
    public SlotData slotData;

    public void Init(GameData data)
    {
        emptyObject.SetActive(data == null);
        dataUIObject.SetActive(data != null);

        if (data == null) return;

        slotData.slotNumber.SetText($"Save {data.SlotNumber}");
        // slotData.mainChar.sprite = mainChar;
        slotData.day.SetText($"D - {data.Day}");
        slotData.money.SetText($"{data.Money:N0}");
    }
}

[Serializable]
public class SlotData
{
    public TMP_Text slotNumber;
    public Image mainChar;
    public TMP_Text day;
    public TMP_Text money;
}
