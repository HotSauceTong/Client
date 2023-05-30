
using System;
using UnityEngine;

using TMPro;

namespace UI.PrefabScripts
{
    public class InputArea : MonoBehaviour
    {
        #region Public values

        #endregion
        
        #region Private values

        private TMP_Text _infoText;
        private TMP_InputField _inputField;
        
        #endregion
        
        #region Public methods

        /// <summary>
        /// 인풋 에이리어를 초기화하는 함수
        /// </summary>
        /// <param name="info">인풋필드에 어떤 값을 입력해야하는지에 대한 정보</param>
        /// <param name="contentType">인풋필드의 타입(이메일, 비밀번호 등)</param>
        /// /// <param name="validType">인풋필드의 규격 타입</param>
        /// <param name="limit">인풋필드에 넣을 입력값의 길이, 미입력시 무한</param>
        public void Init(string info, TMP_InputField.ContentType contentType, TMP_InputField.CharacterValidation validType, int limit = 0)
        {
            _infoText.text = info;
            _inputField.contentType = contentType;
            _inputField.characterValidation = validType;
            _inputField.characterLimit = limit;
        }

        /// <summary>
        /// 인풋필드에 입력된 값을 반환하는 함수
        /// </summary>
        /// <returns>인풋필드에 입력된 값</returns>
        public String GetInputText()
        {
            return _inputField.text;
        }

        #endregion

        #region Mono methods

        private void Awake()
        {
            _infoText = transform.GetChild(0).GetComponent<TMP_Text>();
            _inputField = transform.GetChild(1).GetComponent<TMP_InputField>();
        }

        #endregion

        #region Private methods

        

        #endregion
    }
}

