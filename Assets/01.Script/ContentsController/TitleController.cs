using System;
using System.Collections;
using System.Collections.Generic;
using Lim.ScriptableObjects;
using Lim.System;
using TMPro;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    //todo : 게임 로고 애니메이션 및 개인 로고 인트로 추가
    //todo : 애니메이션 재생 후 인풋 확인
    //todo : 텍스트 애니메이션 추가
    
    public Canvas titleCanvas;
    public TMP_Text versionText;
    public TMP_Text pressGuide;

    private void Start()
    {
        SetVersionText();
        SetPressGuidePos();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneSystem.Instance.ChangeScene(SceneType.Main);
        }
    }

    private void SetVersionText()
    {
        versionText.SetText($"Version : {Application.version}");

        var rect = versionText.rectTransform.rect;
        Vector2 newPos = new Vector2(rect.width * 0.5f, rect.height * 0.5f);
        versionText.rectTransform.anchoredPosition = newPos;
    }

    private void SetPressGuidePos()
    {
        var canvasRect = ((RectTransform)titleCanvas.transform).rect;
        
        Vector2 newPos = new Vector2(0, canvasRect.height * 0.3f);
        pressGuide.rectTransform.anchoredPosition = newPos;
    }
}
