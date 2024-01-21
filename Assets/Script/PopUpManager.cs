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
        howMuch = int.Parse(Number.text);
    }

    // Update is called once per frame
    void Update()
    {
        Number.text = Convert.ToString(howMuch);
    }

    public void OnClickPlus()
    {
        howMuch++;
    }

    public void OnClickMinus()
    {
        howMuch--;
    }
}
