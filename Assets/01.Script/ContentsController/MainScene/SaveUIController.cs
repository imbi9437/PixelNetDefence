using System;
using System.Collections;
using System.Collections.Generic;
using Lim.ScriptableObjects;
using Lim.System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveUIController : MonoBehaviour
{
    [Serializable]
    public class Warning
    {
        public GameObject warningUI;
        public Button yesButton;
        public Button noButton;
    }
    
    [SerializeField] private Button backGround;
    [SerializeField] private List<SaveSlotController> slots;
    [SerializeField] private Warning warning;
    
    private int _selectSlotNum;
    private UnityAction _slotAction;
    public bool isNewGame;
    
    private void OnEnable()
    {
        foreach (var data in GameManager.Instance.UserData.GameData)
        {
            _slotAction = null;
            _slotAction += () =>
            {
                _selectSlotNum = data.Key;
                if (isNewGame) NewGame();
                else LoadGame();
            };
            
            slots[data.Key].Init(data.Value,_slotAction);
        }
    }

    private void Start()
    {
        WarningUIInit();
        
        backGround.onClick.AddListener(() => gameObject.SetActive(false));
    }
    
    private void NewGame()
    {
        Debug.Log("New");
        
        if (GameManager.Instance.UserData.GameData[_selectSlotNum] == null)
        {
            SaveLoadSystem.Instance.CreateNewGameData(_selectSlotNum);
            SaveLoadSystem.Instance.SaveUserData(GameManager.Instance.UserData);
            SceneSystem.Instance.ChangeScene(SceneType.InGame,3);
        }
        else
        {
            warning.warningUI.SetActive(true);
        }
    }
    private void LoadGame()
    {
        Debug.Log("Load");
        if (GameManager.Instance.UserData.GameData[_selectSlotNum] == null) return;
        SceneSystem.Instance.ChangeScene(SceneType.InGame,3);
    }
    
    #region Warning Function
    
    private void WarningUIInit()
    {
        warning.noButton.onClick.AddListener(WarningNo);
        warning.yesButton.onClick.AddListener(WarningYes);
    }

    private void WarningYes()
    {
        SaveLoadSystem.Instance.CreateNewGameData(_selectSlotNum);
        SaveLoadSystem.Instance.SaveUserData(GameManager.Instance.UserData);
        warning.warningUI.SetActive(false);
        SceneSystem.Instance.ChangeScene(SceneType.InGame,3);
    }

    private void WarningNo()
    {
        warning.warningUI.SetActive(false);
    }
    
    #endregion
}
