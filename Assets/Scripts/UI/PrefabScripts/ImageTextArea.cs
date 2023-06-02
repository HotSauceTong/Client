
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace UI.PrefabScripts
{
    public class ImageTextArea : MonoBehaviour
    {
        #region Public values

        #endregion
        
        #region Private values

        private Image _image;
        private TMP_Text _valueText;

        #endregion
        
        #region Public methods

        /// <summary>
        /// 표시할 이미지를 리소스 경로로 불러오는 함수
        /// </summary>
        /// <param name="imgPath">표시할 이미지의 경로</param>
        public void InitImage(string imgPath)
        {
            _image.sprite = Resources.Load<Sprite>(imgPath);
        }

        /// <summary>
        /// 해당 타입에 해당하는 값을 초기화해주는 함수
        /// </summary>
        public void InitValue(string value)
        {
            // TODO : Get data from PlayerData, PlayerData will use UnityEvent
            _valueText.text = value;
        }

        #endregion

        #region Mono methods

        private void Awake()
        {
            _image = transform.GetChild(0).GetComponent<Image>();
            _valueText = transform.GetChild(1).GetComponent<TMP_Text>();
        }

        #endregion

        #region Private methods

        

        #endregion
    }
}


