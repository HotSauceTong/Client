
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UI.PrefabScripts;

namespace UI.TitleScene
{
    public class LoginPanel : MonoBehaviour
    {
        #region Private values

        [SerializeField] private AnnouncePanel confirmPanel;
        
        private TMP_Text _mainText;
        private InputArea _emailId;
        private InputArea _password;
        private Button _confirmButton;

        #endregion

        #region Mono Method

        private void Awake()
        {
            _mainText = transform.GetChild(0).GetComponent<TMP_Text>();
            _emailId = transform.GetChild(1).GetComponent<InputArea>();
            _password = transform.GetChild(2).GetComponent<InputArea>();
            _confirmButton = transform.GetChild(3).GetComponent<Button>();
        }

        private void Start()
        {
            _mainText.text = "로그인";
            _emailId.Init("Email ID", TMP_InputField.ContentType.EmailAddress, TMP_InputField.CharacterValidation.EmailAddress, 255);
            _password.Init("Password", TMP_InputField.ContentType.Password, TMP_InputField.CharacterValidation.None, 12);
            _confirmButton.onClick.AddListener(_ConfirmInputs);
        }

        #endregion
        
        #region Public Method

        #endregion

        #region Private Method

        private void _ConfirmInputs()
        {
            if (!Regex.IsMatch(_emailId.GetInputText(),  General.RexValues.EmailIdRex))
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init("이메일ID가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else if (!Regex.IsMatch(_password.GetInputText(), General.RexValues.PassWordRex))
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init("비밀번호가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else
            {
                Debug.Log("Success");
                // TODO : send Request
            }
        }

        #endregion
    }
}


