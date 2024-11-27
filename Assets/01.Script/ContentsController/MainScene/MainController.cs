using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        quitButton.onClick.AddListener(Application.Quit);
        creditButton.onClick.AddListener(() => creditCanvas.gameObject.SetActive(true));
        newGameButton.onClick.AddListener(NewGameSet);
    }

    private void NewGameSet()
    {
        saveSlotCanvas.gameObject.SetActive(true);
    }
}
