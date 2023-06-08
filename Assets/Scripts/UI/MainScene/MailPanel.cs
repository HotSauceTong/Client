
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
        [SerializeField] private Transform content;
        [SerializeField] private GameObject mailCell;
        [SerializeField] private MailDetail detail;

        private List<MailListElement> _mailList;

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Data source method. return the list length.
        /// </summary>
        public int GetItemCount()
        {
            if (_mailList != null)
                return _mailList.Count;
            return 0;
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
                item.InitData(_mailList[index], detail);
            }
        }

        public void RequestDeleteMail()
        {
            foreach (var mail in _mailList)
            {
                if (mail.ReadDate < DateTime.Now)
                {
                    _GetMailListData("Mail/DeleteAllRecvedMails");
                }
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
            _GetMailListData("Mail/MailList");
            scroll.ReloadData();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 서버에서 메일 리스트 가져오기
        /// </summary>
        private void _GetMailListData(string urlPath)
        {
            MailListRequest req = new MailListRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + urlPath, JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    MailListResponse res = JsonUtility.FromJson<MailListResponse>(response);
                    if (res.errorCode == 0)
                    {
                        _mailList = new List<MailListElement>(res.mailList);
                        UpdateMailCell();
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

        /// <summary>
        /// 받아온 데이터를 바탕으로 메일셀 재생성
        /// 추후 무한 스크롤 등 사용 필요
        /// </summary>
        private void UpdateMailCell()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }

            foreach (var mail in _mailList)
            {
                GameObject mailCellInstance = Instantiate(mailCell, content);
                mailCellInstance.GetComponent<MailCell>().InitData(mail, detail);
            }
        }

        #endregion
    }
}


