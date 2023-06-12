
using System;
using System.Globalization;
using Data;
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
            _PostAttendanceReq();
        }

        private void OnEnable()
        {
            _PostCollectionReq();
        }

        #endregion

        /// <summary>
        /// 서버에서 재화 종류와 양을 받아오는 함수
        /// </summary>
        private void _PostCollectionReq()
        {
            CurrencyListUpRequest req = new CurrencyListUpRequest();
            req.InitBase();

            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Collection/CurrencyListUp", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    CurrencyListUpResponse res = JsonUtility.FromJson<CurrencyListUpResponse>(response);
                    if (res.errorCode == 0)
                    {
                        PlayerData.Instance.SetData(res.currencyList);
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
        /// 서버에 일일보상 요청을 보내는 함수
        /// 마지막으로 로그인한 시간, 일일보상 스택값을 받아온다
        /// </summary>
        private void _PostAttendanceReq()
        {
            AttendanceRequest req = new AttendanceRequest();
            req.InitBase();
            
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
    }
}


