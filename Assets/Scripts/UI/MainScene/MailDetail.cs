
using System;
using General;
using Network;
using Network.PacketStructure;
using TMPro;
using UI.PrefabScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainScene
{
    public class MailDetail : MonoBehaviour
    {
        #region Private Values

        private long _mailID;
        private bool _isOpened;
        private Mail _data;

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
            MailReadRequest req = new MailReadRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            req.mailId = _mailID;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "MailRead", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    MailReadResponse res = JsonUtility.FromJson<MailReadResponse>(response);
                    if (res.errorCode == 0)
                    {
                        _data = res.mail;
                        _InitData();
                    }
                    else
                    {
                        {
                            string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                            announcePanel.Init("이메일 로딩에 실패했습니다.\n\n" + errorName,
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
        
        #region Public Methods

        public void InitMail(long mailID, bool isOpened)
        {
            _mailID = mailID;
            _isOpened = isOpened;
        }

        #endregion

        private void _InitData()
        {
            mailTitle.text = _data.mailTitle;
            mailBody.text = _data.mailBody;
            sender.text = _data.sender;
            expire.text = _data.expirationDate.ToShortDateString();
            // itemIcon.sprite = _data.collectionCode
            if (!_isOpened)
            {
                itemButton.onClick.AddListener(_RequestMailItemReceive);
                itemCount.text = _data.collectionCount.ToString();
            }
            else
            {
                Color c = itemIcon.color;
                itemIcon.color = new Color(c.r, c.g, c.b, c.a / 2);
                itemCount.text = "";
            }
        }

        private void _RequestMailItemReceive()
        {
            MailItemReceiveRequest req = new  MailItemReceiveRequest();
            req.token = PlayerPrefs.GetString("Token");
            req.email = PlayerPrefs.GetString("EmailID");
            req.version = NetworkValues.ClientVersion;
            req.mailId = _mailID;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "MailItemReceive", JsonUtility.ToJson(req), response =>
            {
                if (response != null)
                {
                    MailItemReceiveResponse res = JsonUtility.FromJson<MailItemReceiveResponse>(response);
                    if (res.errorCode == 0)
                    {
                        // TODO : Add Item On Data
                        Debug.Log("Item received Success");
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
    }
}


