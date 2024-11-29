using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int SlotNumber;
    public string MainChar; //todo : 추후 API연동을 위한 클래스로 변경
    public int Day;
    public int Money;

    public GameData(int num)
    {
        SlotNumber = num;
        Day = 0;
        Money = 1000;
    }
}
