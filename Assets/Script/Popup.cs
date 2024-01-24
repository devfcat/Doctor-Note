using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TMP_Text memo; 
    public TMP_InputField inputField; 

    private static Popup _instance;
    public static Popup Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(Popup)) as Popup;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        scrollRect.onValueChanged.AddListener(OnScroll);
        //memo.text = PopUpManager.Instance.clickedDayMemo;
        //PopUpManager.Instance.thisDayMemo = memo.text;
        SetFunction_UI();
    }

    void Update()
    {
        inputField.text = PopUpManager.Instance.thisDayMemo;
    }

    private void SetFunction_UI()
    {
        //button.onClick.AddListener(Function_Button);
        inputField.onValueChanged.AddListener(Function_InputField); 
        inputField.onEndEdit.AddListener(Function_InputField_EndEdit); 
    }

    private void Function_InputField(string _data)
    {
        string txt = _data;
        PopUpManager.Instance.thisDayMemo = txt;
        //Debug.Log("InputField Typing!\n" + txt);
    }
    private void Function_InputField_EndEdit(string _data)
    {
        string txt = _data;
        try
        {
            PopUpManager.Instance.thisDayMemo = txt;
        }
        catch
        {
            PopUpManager.Instance.thisDayMemo = PopUpManager.Instance.thisDayMemo;
        }
        
        // Debug.Log("InputField EndEdit!\n" + txt);
        //inputField.text = "";
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
