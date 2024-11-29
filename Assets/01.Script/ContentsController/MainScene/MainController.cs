using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum MainUIControllerType
{
    None,
    SaveSlot,
    Credit,
}

public class MainController : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button libraryButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    [Header("Canvas")]
    [SerializeField] private Canvas creditCanvas;
    [SerializeField] private Canvas saveSlotCanvas;

    [SerializeField] private SaveUIController saveUIController;
    
    private void Start()
    {
        quitButton.onClick.AddListener(Application.Quit);
        creditButton.onClick.AddListener(() => creditCanvas.gameObject.SetActive(true));
        newGameButton.onClick.AddListener(SetNewGame);
        loadGameButton.onClick.AddListener(SetLoadGame);
    }

    private void SetNewGame()
    {
        saveUIController.isNewGame = true;
        saveUIController.gameObject.SetActive(true);
    }

    private void SetLoadGame()
    {
        saveUIController.isNewGame = false;
        saveUIController.gameObject.SetActive(true);
    }
}
