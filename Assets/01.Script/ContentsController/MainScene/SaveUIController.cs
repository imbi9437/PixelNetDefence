using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIController : MonoBehaviour
{
    [Serializable]
    public class Warning
    {
        public Button yesButton;
        public Button noButton;
    }
    
    [SerializeField] private Button backGround;
    [SerializeField] private List<SaveSlotController> slots;
    [SerializeField] private Warning warning;

    private void OnEnable()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.Instance.UserData.GameData.Count > i)
            {
                var data = GameManager.Instance.UserData.GameData[i];
                slots[i].Init(data);
            }
            else
                slots[i].Init(null);
        }
    }

    private void Start()
    {
        backGround.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
