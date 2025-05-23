using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Panel_Main : MonoBehaviour
{
    public Text DateText;
    public GameObject ButtonGroup;
    public GameObject PopupWrite;
    public GameObject ClickedButton;
    public Sprite ClickedButtonImage;
    public Sprite NoClickedButtonImage;

    public bool IsPopupOpen;

    [Header("# Now Date Info")]
    public int thisDay;
    public int thisMonth;
    public int thisYear;

    private int thisMonthDays;

    private int m_month;
    private int m_year;
    private int m_day;

    [Header("# Date Caculate")]
    public int startDayThisYear;
    public int startDay2023 = 0; // 0: 일, 1:월, 2:화

    public int [,] m_month_list;
    public int [] thisYearStartDay_list;

    // 초기화
    void Start()
    {
        m_month_list = new int [,] {{0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0}};

        thisYearStartDay_list = new int [] {0,0,0,0,0, 0,0,0,0,0, 0,0};
        
        string Date = System.DateTime.Now.ToString("yyyy.MM.dd");
        thisDay = int.Parse(Date.Split(".")[2]);
        thisMonth = int.Parse(Date.Split(".")[1]);
        thisYear = int.Parse(Date.Split(".")[0]);

        //thisDay = 4;
        //thisMonth = 2;
        //thisYear = 2003;

        m_month = thisMonth;
        m_day = thisDay;

        DateText.text = thisYear + "년 " + thisMonth + "월";

        PopupInit();
    }

    // Update is called once per frame
    void Update()
    {
        DateText.text = thisYear + "년 " + thisMonth + "월";

        if (IsPopupOpen)
        {
            ClickedButton.GetComponent<Image>().sprite = ClickedButtonImage;
        }
        else{
            ClickedButton.GetComponent<Image>().sprite = NoClickedButtonImage;
        }

    }

    void LoadData()
    {

    }

    void SaveData()
    {

    }

    private void CalendarMaking(int Year, int Month)
    {
        m_month_list = new int [,] {{0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0}}; // 시작 전 초기화

        // 이 해의 시작 요일 계산 (0:일, 1:월, 2:화, 3:수, 4:목, 5:금, 6:토)
        DateTime date = new DateTime(Year,1,1);
        //Debug.Log(date.DayOfWeek.ToString().Substring(0,3).ToUpper());
        switch (date.DayOfWeek.ToString().Substring(0,3).ToUpper())
        {
            case "MON":
                thisYearStartDay_list[0] = 1;
                break;
            case "TUE":
                thisYearStartDay_list[0] = 2;
                break;
            case "WED":
                thisYearStartDay_list[0] = 3;
                break;
            case "THU":
                thisYearStartDay_list[0] = 4;
                break;
            case "FRI":
                thisYearStartDay_list[0] = 5;
                break;
            case "SAT":
                thisYearStartDay_list[0] = 6;
                break;
            case "SUN":
                thisYearStartDay_list[0] = 0;
                break;
            default: break;
        }
        int thisYearStartDay = thisYearStartDay_list[0];

        // Debug.Log(Year + "년의 시작 요일은 " + thisYearStartDay + " 입니다.");
        
        // 이 달의 최대 일수 계산
        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
        {
            thisMonthDays = 31;
        }
        else if (Month == 4 || Month == 6 || Month == 9 || Month == 11)
        {
            thisMonthDays = 30;
        }
        else // 2월일 경우 평년/윤년 계산해야함.
        {
            if (Year%4 == 0)
            {
                if (Year%100 == 0)
                {
                    if (Year%400 == 0)
                    {
                        thisMonthDays = 29;
                    }
                    else 
                    {
                        thisMonthDays = 28;
                    }
                }
                else 
                {
                    thisMonthDays = 29;
                }
            }
            else 
            {
                thisMonthDays = 28;
            }
        }

        // 이번 해의 각 달의 시작 요일 계산
        for(int i = 1; i < 13; i++)
        {
            if (i == 1)
            {
                thisYearStartDay_list[0] = thisYearStartDay;
            }
            else
            {
                if (i == 2 || i == 4 || i == 6 || i == 8 || i == 9 || i == 11)
                {
                    thisYearStartDay_list[i-1] = (31 + thisYearStartDay_list[i-2])%7;
                }
                else if (i == 5 || i == 7 || i == 10 || i == 12)
                {
                    thisYearStartDay_list[i-1] = (30 + thisYearStartDay_list[i-2])%7;
                }
                else
                {
                    int IsFeb = 0;
                    if (Year%4 == 0)
                    {
                        if (Year%100 == 0)
                        {
                            if (Year%400 == 0)
                            {
                                IsFeb = 29;
                            }
                            else 
                            {
                                IsFeb = 28;
                            }
                        }
                        else 
                        {
                            IsFeb = 29;
                        }
                    }
                    else 
                    {
                        IsFeb = 28;
                    }
                    thisYearStartDay_list[i-1] = (IsFeb + thisYearStartDay_list[i-2])%7;
                }
            }
        }

        //Debug.Log(thisYearStartDay_list[5] + " " + thisYearStartDay_list[6] + " " + thisYearStartDay_list[7]+ " " +thisYearStartDay_list[8]+ " " +thisYearStartDay_list[9]+ " " +thisYearStartDay_list[10]+ " " +thisYearStartDay_list[11]);

        // 이번 달의 달력 제조
        for(int j = 0; j < 6; j++)
        {
            if (j == 0)
            {
                int firstCount = 0;
                if (thisYearStartDay_list[Month-1] == 0)
                {
                    for(int index = 0; index < 7; index++)
                    {
                        m_month_list[0,index] = index+1;
                    }
                }
                else
                {
                    for(int index = thisYearStartDay_list[Month-1]; (index+firstCount)%7 != 0; firstCount++)
                    {
                        m_month_list[0,index+firstCount] = firstCount+1;
                    }
                }
            }
            else 
            {
                for(int index = 0; index < 7; index++)
                {
                    if ((7-thisYearStartDay_list[Month-1] + index + 7*(j-1) +1) > thisMonthDays) // 그 달의 최대 일수를 넘으면 0으로 저장.
                    {
                        m_month_list[j,index] = 0;
                    }
                    else
                    {
                        m_month_list[j,index] = 7-thisYearStartDay_list[Month-1] + index + 7*(j-1) +1;
                    }
                }
            }
            
        }
 
        /*for(int j =0; j <6;j++)
        {
            Debug.Log(j+": " + m_month_list[j,0] + " " + m_month_list[j,1] + " " + m_month_list[j,2] + " " + m_month_list[j,3] + " " + m_month_list[j,4] + " " + m_month_list[j,5] + " " + m_month_list[j,6]);
        }
        */


        // 제조한 달력을 화면에 보여줌
        for(int j =0; j <6;j++)
        {
            for (int i =0; i < 7; i++)
            {
                if(m_month_list[j,i] != 0) // 값이 0이 아닌 유효한 값이면
                {
                    ButtonGroup.transform.Find("DayButton"+Convert.ToString(i+j*7)).gameObject.SetActive(true);
                    ButtonGroup.transform.Find("DayButton"+Convert.ToString(i+j*7)).transform.Find("Day").gameObject.SetActive(true);
                    Text ButtonText = ButtonGroup.transform.Find("DayButton"+Convert.ToString(i+j*7)).transform.Find("Day").gameObject.GetComponent<Text>();
                    ButtonText.text = Convert.ToString(m_month_list[j,i]);
                }
                else
                {
                    ButtonGroup.transform.Find("DayButton"+Convert.ToString(i+j*7)).gameObject.SetActive(false);
                    ButtonGroup.transform.Find("DayButton"+Convert.ToString(i+j*7)).transform.Find("Day").gameObject.SetActive(false);
                }
            }
            
        }
        
        


            
    }

    public void OnclickDayButton()
    {
        ClickedButton.GetComponent<Image>().sprite = NoClickedButtonImage;
        string day = "";
        if (EventSystem.current.currentSelectedGameObject.transform.Find("Day").gameObject != null)
        {
            day = EventSystem.current.currentSelectedGameObject.transform.Find("Day").gameObject.GetComponent<Text>().text;
        } else {}
        ClickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        ClickedButton.GetComponent<Image>().sprite = ClickedButtonImage;
        PopupWrite.transform.Find("DateBar").transform.Find("Date").gameObject.GetComponent<Text>().text = 
            thisMonth + "월 " + day + "일";
        OpenPopupWrite();
    }

    public void PopupInit()
    {
        CalendarMaking(thisYear, thisMonth);

        PopupWrite.transform.Find("DateBar").transform.Find("Date").gameObject.GetComponent<Text>().text = 
            thisMonth + "월 " + thisDay + "일";
        ClickedButton = ButtonGroup.transform.Find("DayButton"+Convert.ToString(thisYearStartDay_list[thisMonth-1]+thisDay-1)).gameObject;
        OpenPopupWrite();
    }


    public void OpenPopupWrite()
    {
        IsPopupOpen = true;
        PopupWrite.SetActive(true);

    }

    public void ClosePopupWrite()
    {
        IsPopupOpen = false;
        PopupWrite.SetActive(false);
    }

    public void OnclickNextMonth()
    {
        ClosePopupWrite();
        if (thisMonth == 12){
            thisMonth = 1;
            thisYear++;
        }
        else
        {
            thisMonth = thisMonth + 1;
        }
        CalendarMaking(thisYear, thisMonth);

        if(thisMonth == m_month)
        {
            thisDay = m_day;
            ClickedButton = ButtonGroup.transform.Find("DayButton"+Convert.ToString(thisYearStartDay_list[thisMonth-1]+thisDay-1)).gameObject;
            PopupWrite.transform.Find("DateBar").transform.Find("Date").gameObject.GetComponent<Text>().text = 
                thisMonth + "월 " + thisDay + "일";
            OpenPopupWrite();
        }
    }

    public void OnclickPrevMonth()
    {
        ClosePopupWrite();
        if (thisMonth == 1)
        {
            thisMonth = 12;
            thisYear--;
        }
        else
        {
            thisMonth = thisMonth - 1;
        }
        CalendarMaking(thisYear, thisMonth);

        if(thisMonth == m_month)
        {
            thisDay = m_day;
            ClickedButton = ButtonGroup.transform.Find("DayButton"+Convert.ToString(thisYearStartDay_list[thisMonth-1]+thisDay-1)).gameObject;
            PopupWrite.transform.Find("DateBar").transform.Find("Date").gameObject.GetComponent<Text>().text = 
                thisMonth + "월 " + thisDay + "일";
            OpenPopupWrite();
        }
    }


}
