
using System;
using General;
using Network;
using Network.PacketStructure;
using TMPro;
using UI.PrefabScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class MailDetail : MonoBehaviour
    {
        #region Private Values

        private long _mailID;
        private Mail _data;

        private Action _action;

        [SerializeField] private TMP_Text mailTitle;
        [SerializeField] private TMP_Text mailBody;
        [SerializeField] private TMP_Text sender;
        [SerializeField] private TMP_Text expire;

        [SerializeField] private AnnouncePanel announcePanel;
        
        // TODO : 버튼으로 바꿔서 서버에 통신하도록 하기
        [SerializeField] private Button itemButton;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemCount;
        
        #endregion

        #region Mono Method

        private void OnEnable()
        {
            _RequestDetailMail();
        }

        private void OnDisable()
        {
            _action.Invoke();
            _action = null;
        }

        #endregion
        
        #region Public Methods

        public void InitMail(long mailID, Action ev)
        {
            _mailID = mailID;
            _action = ev;
        }

        #endregion

        

        #region Private Methods

        /// <summary>
        /// 값 초기화 및 표시 함수
        /// </summary>
        private void _InitUI()
        {
            mailTitle.text = _data.mailTitle;
            mailBody.text = _data.mailBody;
            sender.text = _data.sender;
            expire.text = _data.ExpirationDate.ToShortDateString();
        }
        
        private void _InitUIItem()
        {
            itemIcon.sprite = Resources.Load<Sprite>("Texture/Item/" + _data.collectionCode);
            itemCount.text = _data.collectionCount == -1 ? "" : _data.collectionCount.ToString();
            if (_data.collectionCount != -1)
            {
                itemButton.onClick.AddListener(_RequestMailItemReceive);
            }
        }
        
        /// <summary>
        /// 상세 이메일 요청 함수
        /// </summary>
        private void _RequestDetailMail()
        {
            MailReadRequest req = new MailReadRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            req.mailId = _mailID;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Mail/MailRead", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    MailReadResponse res = JsonUtility.FromJson<MailReadResponse>(response);
                    if (res.errorCode == 0)
                    {
                        _data = res.mail;
                        _InitUI();
                        _InitUIItem();
                    }
                    else
                    {
                        string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                        announcePanel.Init("이메일 로딩에 실패했습니다.\n\n" + errorName,
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
        /// 메일 아이템 요청 함수
        /// </summary>
        private void _RequestMailItemReceive()
        {
            MailItemReceiveRequest req = new  MailItemReceiveRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            req.mailId = _mailID;
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Mail/MailItemReceive", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                { 
                    MailItemReceiveResponse res = JsonUtility.FromJson<MailItemReceiveResponse>(response); 
                    if (res.errorCode == 0)
                    {
                        _data.collectionCode = -1;
                        _data.collectionCount = -1;
                        itemButton.onClick.RemoveAllListeners();
                        _InitUIItem();
                    }
                    else
                    { 
                        { 
                            string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                            announcePanel.Init("아이템 수령에 실패했습니다.\n\n" + errorName,
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


