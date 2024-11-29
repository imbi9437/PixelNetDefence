using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public int Cash = 0;
    public Dictionary<int, GameData> GameData;

    public UserData()
    {
        Cash = 200;
        GameData = new Dictionary<int, GameData>();

        for (int i = 0; i < 5; i++)
        {
            GameData.Add(i,null);
        }
    }
}
