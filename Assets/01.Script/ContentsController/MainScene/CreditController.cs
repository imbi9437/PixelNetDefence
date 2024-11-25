using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private TMP_Text creditText;

    private Sequence _sequence;
    public float fadeDuration;
    public float creditDuration;
    public float height;
    
    private void OnEnable()
    {
        InitCredit();
        CreditSetting();
        
        //크레딧 사운드 출력
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sequence.Kill(true);
        }
    }

    //Fade 효과 제거 할지 고민
    private void CreditSetting()
    {
        _sequence = DOTween.Sequence();
        _sequence.SetEase(Ease.Linear);

        var fadeIn = backGround.DOFade(1, fadeDuration);
        var creditMove = creditText.rectTransform.DOAnchorPosY(height, 60f);
        
        _sequence.Append(fadeIn);
        _sequence.AppendInterval(0.5f);
        _sequence.Append(creditMove);

        _sequence.onComplete += () => gameObject.SetActive(false);
    }

    private void InitCredit()
    {
        backGround.color = new Color(0, 0, 0, 0);
        
        RectTransform canvasRect = (RectTransform)creditText.rectTransform.parent;
        height = (canvasRect.rect.height + creditText.rectTransform.rect.height) * 0.5f;
        
        creditText.rectTransform.anchoredPosition = new Vector2(0, height * -1);
    }
}
