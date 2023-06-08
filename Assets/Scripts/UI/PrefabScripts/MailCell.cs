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
        
        private MailListElement _data;

        [SerializeField] private Sprite notRead;
        [SerializeField] private Sprite alreadyRead;
        
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text mailTitle;
        [SerializeField] private TMP_Text sender;
        [SerializeField] private TMP_Text expireDate;
        [SerializeField] private Image mailIcon;
        // TODO : 아이템 이미지 챙기기
        [SerializeField] private Image itemSprite;
        [SerializeField] private TMP_Text itemCount;

        private MailDetail _detail;

        #endregion

        #region Public Method

        /// <summary>
        /// 메일리스트 요소값 초기화
        /// </summary>
        /// <param name="data">메일리스트 데이터</param>
        public void InitData(MailListElement data, MailDetail detail)
        {
            _data = data;
            _detail = detail;
            
            _InitUI();
            _InitUIItem();
        }

        #endregion

        #region Mono Method
        

        #endregion
        
        #region Private Methods

        private void _InitUI()
        {
            mailTitle.text = _data.mailTitle;
            sender.text = _data.sender;
            
            if (_data.ReadDate > DateTime.Now)
            {
                mailIcon.sprite = notRead;
            }
            else
            {
                mailIcon.sprite = alreadyRead;
            }
            
            expireDate.text = _CalculateRemainTime(_data.ExpirationDate);
        }

        private void _InitUIItem()
        {
            button.onClick.AddListener(OpenMail);
            itemSprite.sprite = Resources.Load<Sprite>("Texture/Item/" + _data.collectionCode);
            itemCount.text = _data.collectionCount == -1 ? "" : _data.collectionCount.ToString();
        }
        private void _InitUIItemAsNone()
        {
            _data.collectionCode = -1;
            _data.collectionCount = -1;
            itemSprite.sprite = Resources.Load<Sprite>("Texture/Item/" + _data.collectionCode);
            itemCount.text = "";
        }
        
        /// <summary>
        /// 상세 메일을 오픈하는 함수.
        /// </summary>
        private void OpenMail()
        {
            Debug.Log("Request detailed Email");
            // TODO : 리퀘스트 보내고 응답이 정상적으로 됬으면 해당 메일 읽음처리
            _detail.InitMail(_data.mailId, () => _InitUIItemAsNone());
            _detail.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// 현재 시간과 대비하여 남는 시간을 string형식으로 반환하는 함수
        /// </summary>
        /// <param name="expire">계산할 시간</param>
        /// <returns>남은 시간 문자열</returns>
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


