
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace UI.PrefabScripts
{
    public class AnnouncePanel : MonoBehaviour
    {
        #region Public values

        #endregion
        
        #region Private values

        private TMP_Text _infoText;
        private Button _button1;
        private TMP_Text _btn1Text;
        private Button _button2;
        private TMP_Text _btn2Text;
        
        #endregion
        
        #region Public methods

        /// <summary>
        /// 정보 패널을 활성화하고 초기화하는 함수
        /// </summary>
        /// <param name="info">패널에 알려줄 정보</param>
        /// <param name="btn1Str">오른쪽 버튼에 작성할 문자열, 필수 입력</param>
        /// /// <param name="btn2Str">왼쪽 버튼에 작성할 문자열, 미입력시 버튼이 disabled됨</param>
        public void Init(string info, string btn1Str, UnityAction btn1OnClick ,string btn2Str = null, UnityAction btn2OnClick = null)
        {
            gameObject.SetActive(true);
            
            _infoText.text = info;
         
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            
            _btn1Text.text = btn1Str;
            _button1.onClick.AddListener(btn1OnClick);
            
            if (btn2Str == null) {
                _button2.gameObject.SetActive(false);
            } else {
                _button2.gameObject.SetActive(true);
                _btn2Text.text = btn2Str;
                _button2.onClick.AddListener(btn2OnClick);
            }
        }

        #endregion

        #region Mono methods

        private void Awake()
        {
            _infoText = transform.GetChild(0).GetComponent<TMP_Text>();
            
            _button1 = transform.GetChild(2).GetComponent<Button>();
            _button2 = transform.GetChild(1).GetComponent<Button>();
            
            _btn1Text = _button1.transform.GetChild(0).GetComponent<TMP_Text>();
            _btn2Text = _button2.transform.GetChild(0).GetComponent<TMP_Text>();
        }

        #endregion

        #region Private methods

        #endregion
    }
}
