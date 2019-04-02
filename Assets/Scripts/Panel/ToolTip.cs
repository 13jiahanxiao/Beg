using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {
    Text toolTipText;
    Text contentText;
    CanvasGroup canvasGroup;
    float targetAlpha = 0;
    float smoothing = 6;

    private void Start()
    {
        toolTipText = GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha=Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if(Mathf.Abs(canvasGroup.alpha- targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
        
    }
    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }
    public void Hide()
    {
        targetAlpha = 0;
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
