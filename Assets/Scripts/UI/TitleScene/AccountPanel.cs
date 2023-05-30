
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UI.PrefabScripts;


namespace UI.TitleScene
{
    public class AccountPanel : MonoBehaviour
    {
        #region Private values

        [SerializeField] private AnnouncePanel confirmPanel;
        
        private TMP_Text _mainText;
        private InputArea _emailId;
        private InputArea _password;
        private InputArea _nickname;
        private Button _confirmButton;

        #endregion

        #region Mono Method

        private void Awake()
        {
            _mainText = transform.GetChild(0).GetComponent<TMP_Text>();
            _emailId = transform.GetChild(1).GetComponent<InputArea>();
            _password = transform.GetChild(2).GetComponent<InputArea>();
            _nickname = transform.GetChild(3).GetComponent<InputArea>();
            _confirmButton = transform.GetChild(4).GetComponent<Button>();
        }

        private void Start()
        {
            _mainText.text = "회원가입";
            _emailId.Init("Email ID", TMP_InputField.ContentType.EmailAddress, TMP_InputField.CharacterValidation.EmailAddress, 255);
            _password.Init("Password", TMP_InputField.ContentType.Password, TMP_InputField.CharacterValidation.None, 12);
            _nickname.Init("Nickname", TMP_InputField.ContentType.Alphanumeric, TMP_InputField.CharacterValidation.None, 12);
            _confirmButton.onClick.AddListener(_ConfirmInputs);
        }

        #endregion
        
        #region Public Method

        #endregion

        #region Private Method

        private void _ConfirmInputs()
        {
            if (!Regex.IsMatch(_emailId.GetInputText(), General.RexValues.EmailIdRex))
            {}
            else if (!Regex.IsMatch(_password.GetInputText(), General.RexValues.PassWordRex))
            {}
            else if (!Regex.IsMatch(_nickname.GetInputText(), General.RexValues.NickNameRex))
            {}
            else
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init(_nickname.GetInputText() + "\n이 닉네임으로 하시겠습니까?", 
                    "예",   delegate { _PostSignInRequest(); },
                    "아니요", delegate { confirmPanel.gameObject.SetActive(false); });
            }
        }

        private void _PostSignInRequest()
        {
            // TODO : send Request
            Debug.Log("Success");
        }

        #endregion
    }
}


