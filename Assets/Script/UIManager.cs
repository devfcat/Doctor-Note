using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public eState m_state;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_state = GameManager.Instance.m_state;
    }

    void Awake()
    {

    }

}
