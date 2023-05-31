
using System;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using General;
using Network;
using UI.PrefabScripts;
using Network.PacketStructure;

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
            if (!Regex.IsMatch(_emailId.GetInputText(), RexValues.EmailIdRex))
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init("이메일ID가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else if (!Regex.IsMatch(_password.GetInputText(), RexValues.PassWordRex))
            {
                confirmPanel.Init("비밀번호가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else if (!Regex.IsMatch(_nickname.GetInputText(), RexValues.NickNameRex))
            {
                confirmPanel.Init("닉네임이 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else
            {
                confirmPanel.Init(_nickname.GetInputText() + "\n이 닉네임으로 하시겠습니까?", 
                    "예", delegate { _PostSignInRequest(); },
                    "아니요", delegate { confirmPanel.gameObject.SetActive(false); });
            }
        }

        private void _PostSignInRequest()
        {
            SignUpRequest request = new SignUpRequest(
                _emailId.GetInputText(),
                _nickname.GetInputText(),
                _password.GetInputText(),
                NetworkValues.ClientVersion );

            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Regist", JsonUtility.ToJson(request), response =>
            {
                if (response != null)
                {
                    SignUpResponse res = JsonUtility.FromJson<SignUpResponse>(response);
                    if (res.errorCode == 0)
                    {
                        confirmPanel.Init("회원가입에 성공했습니다.\n환영합니다, " + _nickname.GetInputText(), 
                            "확인",delegate { confirmPanel.gameObject.SetActive(false); });
                    }
                    else
                    {
                        string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                        
                        confirmPanel.Init( "회원가입에 실패했습니다.\n\n" + errorName,
                            "확인",delegate { confirmPanel.gameObject.SetActive(false); });
                    }
                }
                else
                {
                    confirmPanel.Init( "서버가 응답하지 않습니다",
                        "확인",delegate { confirmPanel.gameObject.SetActive(false); });
                }
            });
        }

        #endregion
    }
}


