using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpManager : MonoBehaviour
{
    public Text Number;
    //public GameObject ButtonGroup;

    public int howMuch;
    public GameObject ButtonGroup;

    public bool IsPopupNumChanged = false;

    private static PopUpManager _instance;
    public static PopUpManager Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(PopUpManager)) as PopUpManager;

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
        GameManager.Instance.StartData();
        howMuch = GameManager.Instance.thisNumData;

    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.LoadOneDayData(GameManager.Instance.thisYear, GameManager.Instance.thisMonth, GameManager.Instance.thisDay);
        howMuch =  GameManager.Instance.selectedNumData;

        Number.text = Convert.ToString(howMuch);
    }



    public void OnClickPlus()
    {
        howMuch++;
        GameManager.Instance.SaveData(GameManager.Instance.thisYear, GameManager.Instance.thisMonth, GameManager.Instance.thisDay);
        PopUpManager.Instance.IsPopupNumChanged = true;
    }

    public void OnClickMinus()
    {
        if (howMuch - 1 >= 0)
        {
            howMuch--;
        }
        else
        {
            // 0 이하로는 내려갈 수 없다
        }
        GameManager.Instance.SaveData(GameManager.Instance.thisYear, GameManager.Instance.thisMonth, GameManager.Instance.thisDay);
        PopUpManager.Instance.IsPopupNumChanged = true;
    }
}
