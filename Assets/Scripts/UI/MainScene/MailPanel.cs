
using System;
using System.Collections.Generic;
using General;
using Network;
using Network.PacketStructure;
using PolyAndCode.UI;
using UnityEngine;

using UI.PrefabScripts;

namespace UI.MainScene
{
    public class MailPanel : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        #region Public Values

        

        #endregion

        #region Private Values
        
        [SerializeField] private AnnouncePanel announcePanel;

        [SerializeField] private RecyclableScrollRect scroll;

        private List<MailListElement> _mailList;

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Data source method. return the list length.
        /// </summary>
        public int GetItemCount()
        {
            return _mailList.Count;
        }

        /// <summary>
        /// Data source method. Called for a cell every time it is recycled.
        /// Implement this method to do the necessary cell configuration.
        /// </summary>
        public void SetCell(ICell cell, int index)
        {
            //Casting to the implemented Cell
            var item = cell as MailCell;
            if (item)
            {
                item.Init(_mailList[index], index);
            }
        }

        #endregion

        #region Mono Methods

        private void Awake()
        {
            scroll.DataSource = this;
        }

        private void OnEnable()
        {
            _GetMailListData();
        }

        #endregion

        #region Private Methods

        private void _GetMailListData()
        {
            MailListRequest req = new MailListRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "MailList", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    MailListResponse res = JsonUtility.FromJson<MailListResponse>(response);
                    if (res.errorCode == 0)
                    {
                        _mailList = res.mailList;
                    }
                    else
                    {
                        {
                            string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                            announcePanel.Init("이메일 리스트 로딩에 실패했습니다.\n\n" + errorName,
                                "확인", delegate { announcePanel.gameObject.SetActive(false); });
                        }
                    }
                }
                else
                {
                    announcePanel.Init( "서버 연결에 실패했습니다",
                        "확인",delegate { announcePanel.gameObject.SetActive(false); });
                }
            });
        }
        
        #endregion
    }
}


