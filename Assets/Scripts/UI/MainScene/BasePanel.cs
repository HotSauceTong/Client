
using System;
using System.Globalization;
using General;
using Network;
using Network.PacketStructure;
using UnityEngine;

using UI.PrefabScripts;

namespace UI.MainScene
{
    public class BasePanel : MonoBehaviour
    {
        #region Public Values

        #endregion
        
        #region private Values

        [SerializeField] private AnnouncePanel announcePanel;

        #endregion

        #region Public Methods

        public void ToMailPanel()
        {
            
        }

        #endregion

        #region Mono Methods

        private void Awake()
        {
            //if (_FirstOfDay())
            _PostAttendanceReq();
        }

        #endregion

        /// <summary>
        /// 서버에 일일보상 요청을 보내는 함수
        /// 마지막으로 로그인한 시간, 일일보상 스택값을 받아온다
        /// </summary>
        private void _PostAttendanceReq()
        {
            AttendanceRequest req = new AttendanceRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Attendance", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    AttendanceResponse res = JsonUtility.FromJson<AttendanceResponse>(response);
                    if (res.errorCode == 0)
                    {
                        PlayerPrefs.SetString("LastLogin", res.lastLoginDate.ToString(CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                        announcePanel.Init("출석보상 획득에 실패했습니다.\n\n" + errorName,
                            "확인", delegate { announcePanel.gameObject.SetActive(false); });
                    }
                }
                else
                {
                    announcePanel.Init( "서버 연결에 실패했습니다",
                        "확인",delegate { announcePanel.gameObject.SetActive(false); });
                }
            });
        }

        /// <summary>
        /// 오늘이 갱신되었는지 확인하는 함수
        /// 현재 시간이 이전에 로그인한 시간 이후로 오전 6시가 지났다면 true를, 아니면 false 반환
        /// </summary>
        private bool _FirstOfDay()
        {
            DateTime now = DateTime.Now;
            DateTime resetTime = new DateTime(now.Year, now.Month, now.Day, 6, 0, 0);
            
            DateTime lastLogin;
            if (DateTime.TryParse(PlayerPrefs.GetString("LastLogin"), out lastLogin))
            {
                return now >= resetTime && now >= lastLogin;
            }

            return false;
        }
    }
}


