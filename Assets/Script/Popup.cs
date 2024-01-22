using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnScroll(Vector2 position)
    {			
		if (scrollRect.content.anchoredPosition.y < 0f)
        {
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
        }
        else if (scrollRect.content.anchoredPosition.y >= 430f)
		{
            scrollRect.content.anchoredPosition = new Vector2( // 화면 끝에서 더 가지 않도록 만듦
				scrollRect.content.anchoredPosition.x, 430f);
        }
		else
		{
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;			
        }
    }
}
