using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public enum eState
{
    Panel_Main,             // 메인 달력 화면
    Panel_Notice,           // 공지사항 탭
}

public class GameManager : MonoBehaviour
{
    public GameObject Panel_Main;
    public GameObject Panel_Notice;

    public eState m_state;
    public bool IsNewUSER = true; // 앱을 처음 실행했는가

    // 인스턴스화
    private static GameManager _instance;
    public static GameManager Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

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

#if (UNITY_ANDROID) && !UNITY_EDITOR
        // ApplicationChrome.navigationBarState = ApplicationChrome.States.Visible;
#endif
    }

    void Start()
    {
        m_state = eState.Panel_Main;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        // SetState();

#if (UNITY_ANDROID) || UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnClickBack(m_state);
            }
#endif
    }

    public void SetState()
    {
        if (Panel_Main.activeSelf)
        {
            m_state = eState.Panel_Main;
        }
        else if (Panel_Notice.activeSelf)
        {
            m_state = eState.Panel_Notice;
        }
        else {Debug.Log("패널 상태 예외 발생");}
    }

    public void OnClickBack(eState m_state)
    {
        if (m_state == eState.Panel_Main) // 메인 패널에서만 앱 나가기 가능
        {
            #if UNITY_EDITOR
			    UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
            return;
        }
    }


}
