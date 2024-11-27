using System;
using System.Collections;
using System.Collections.Generic;
using Lim.Generic;
using Lim.System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private const int GameDataSlotCount = 5;
    public UserData UserData;
    public GameData SelectData;

    protected override void Awake()
    {
        base.Awake();
        SaveLoadSystem.SubScribe.OnLoadUserDataComplete += SetLoadedUserData;
    }

    private void Start()
    {
        SaveLoadSystem.Instance.Initialize();
    }

    private void SetLoadedUserData(object obj, LoadUserDataArgs args)
    {
        UserData = args.Data;
    }
}
