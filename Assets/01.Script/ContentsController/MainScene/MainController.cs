using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button libraryButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Canvas creditCanvas;

    private void Start()
    {
        quitButton.onClick.AddListener(Application.Quit);
        creditButton.onClick.AddListener(() => creditCanvas.gameObject.SetActive(true));
    }
}
