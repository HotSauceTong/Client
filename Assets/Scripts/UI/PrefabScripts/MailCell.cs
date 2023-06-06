using System;

using Network.PacketStructure;
using PolyAndCode.UI;
using TMPro;
using UI.MainScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PrefabScripts
{
    public class MailCell : MonoBehaviour, ICell
    {
        #region private Values

        private int _index;
        private bool _isOpened;
        private MailListElement _data;

        [SerializeField] private Sprite notRead;
        [SerializeField] private Sprite alreadyRead;
        
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text mailTitle;
        [SerializeField] private TMP_Text sender;
        [SerializeField] private TMP_Text expireDate;
        [SerializeField] private Image mailIcon;
        // TODO : 아이템 이미지 챙기기
        // [SerializeField] private Image itemSprite;
        // [SerializeField] private TMP_Text itemCount;

        private MailDetail _detail;

        #endregion

        #region Public Method

        public void Init(MailListElement data, int index)
        {
            _index = index;
            _data = data;
            mailTitle.text = _data.mailTitle;
            sender.text = _data.sender;
            
            if (_data.readDate > DateTime.Now)
            {
                mailIcon.sprite = notRead;
                _isOpened = false;
            }
            else
            {
                mailIcon.sprite = alreadyRead;
                _isOpened = true;
            }
            
            expireDate.text = _CalculateRemainTime(_data.expirationDate);
            
            button.onClick.AddListener(OpenMail);
        }

        #endregion

        #region Mono Method

        private void Awake()
        {
            _detail = GameObject.Find("DetailMail").GetComponent<MailDetail>();
        }

        #endregion
        
        #region Private Methods

        private void OpenMail()
        {
            Debug.Log("Request detailed Email");
            // TODO : 리퀘스트 보내고 응답이 정상적으로 됬으면 해당 메일 읽음처리
            
            _detail.InitMail(_data.mailId, _isOpened);
            _detail.gameObject.SetActive(true);
        }
        
        private string _CalculateRemainTime(DateTime expire)
        {
            TimeSpan remainingTime = expire - DateTime.Now;
            string formattedTime;

            if (remainingTime.TotalDays >= 1)
            {
                formattedTime = remainingTime.Days.ToString() + "일";
            }
            else if (remainingTime.TotalHours >= 1)
            {
                formattedTime = remainingTime.Hours.ToString() + "시간";
            }
            else if (remainingTime.TotalMinutes >= 0)
            {
                formattedTime = remainingTime.Minutes.ToString() + "분";
            }
            else
            {
                formattedTime = "유효하지 않은 시간";
            }

            return formattedTime;
        }

        #endregion
    }
}


