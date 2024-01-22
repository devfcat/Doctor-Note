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

    [Header("# Now Date Info")]
    public int thisDay;
    public int thisMonth;
    public int thisYear;

    public int thisNumData; // 오늘 날짜의 데이터
    public int selectedNumData; // 현재 선택한 날짜의 개수 데이터

    public int [] m_month_data_list;

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
    }

    void Start()
    {
        m_state = eState.Panel_Main;
        Application.targetFrameRate = 60;

        m_month_data_list = new int [] {0,0,0,0,0,0,0,
                                        0,0,0,0,0,0,0,
                                        0,0,0,0,0,0,0,
                                        0,0,0,0,0,0,0,
                                        0,0,0,0,0,0,0,
                                        0,0,0,0,0,0,0}; // 시작 전 초기화
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

#if (UNITY_EDITOR)
        if (Input.GetKeyUp(KeyCode.Delete)) // 개발 시에만 사용할 것.
        {
            DeleteAllData();
            PopUpManager.Instance.IsPopupNumChanged = true;
            Debug.Log("모든 데이터 삭제됨");
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

    public void LoadOneDayData(int thisYear, int thisMonth, int thisDay)
    {
        string dataKey = Convert.ToString(thisYear)+"."+Convert.ToString(thisMonth)+"."+Convert.ToString(thisDay)+"num";
        GameManager.Instance.selectedNumData = PlayerPrefs.GetInt(dataKey, 0);
    }

    public void LoadMonthData(int thisYear, int thisMonth)
    {
        for(int i=1; i < 32; i++)
        {
            string dataKey = Convert.ToString(thisYear)+"."+Convert.ToString(thisMonth)+"."+Convert.ToString(i)+"num";
            try // 오늘 날의 데이터가 있다면
            {
                m_month_data_list[i-1] = PlayerPrefs.GetInt(dataKey, 0);
            }
            catch // 없으면 0으로 초기화
            {
                m_month_data_list[i-1] = 0;
            }
        }
    }

    public void StartData()
    {
        string dataKey = Convert.ToString(GameManager._instance.thisYear)+"."+Convert.ToString(GameManager._instance.thisMonth)+"."+Convert.ToString(GameManager._instance.thisDay)+"num";
        try // 오늘 날의 데이터가 있다면
        {
            thisNumData = PlayerPrefs.GetInt(dataKey, 0);
        }
        catch // 없으면 0으로 초기화
        {
            thisNumData = 0;
        }
        
    }

    public void SaveData(int thisYear, int thisMonth, int thisDay)
    {
        string dataKey = Convert.ToString(thisYear)+"."+Convert.ToString(thisMonth)+"."+Convert.ToString(thisDay)+"num";
        PlayerPrefs.SetInt(dataKey, PopUpManager.Instance.howMuch);

        //Debug.Log(dataKey + ": " + PlayerPrefs.GetInt(dataKey, 0));
        //dataKey = Convert.ToString(GameManager._instance.thisYear)+"."+Convert.ToString(GameManager._instance.thisMonth)+"."+Convert.ToString(GameManager._instance.thisDay)+"Ismemo";
        //PlayerPrefs.SetInt(dataKey, PopUpManager.Instance.howMuch);
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }


}
