
using System;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UI.PrefabScripts;

using General;
using Network;
using Network.PacketStructure;
using UnityEngine.SceneManagement;

namespace UI.TitleScene
{
    public class LoginPanel : MonoBehaviour
    {
        #region Private values

        [SerializeField] private AnnouncePanel confirmPanel;
        
        private TMP_Text _mainText;
        private TextInputArea _emailId;
        private TextInputArea _password;
        private Button _confirmButton;

        #endregion

        #region Mono Method

        private void Awake()
        {
            _mainText = transform.GetChild(0).GetComponent<TMP_Text>();
            _emailId = transform.GetChild(1).GetComponent<TextInputArea>();
            _password = transform.GetChild(2).GetComponent<TextInputArea>();
            _confirmButton = transform.GetChild(3).GetComponent<Button>();
        }

        private void Start()
        {
            _mainText.text = "로그인";
            _emailId.Init("Email ID", TMP_InputField.ContentType.EmailAddress, TMP_InputField.CharacterValidation.EmailAddress, 255);
            _password.Init("Password", TMP_InputField.ContentType.Password, TMP_InputField.CharacterValidation.None, 12);
            _confirmButton.onClick.AddListener(_CheckAndPostInputs);
        }

        #endregion
        
        #region Public Method

        #endregion

        #region Private Method

        private void _CheckAndPostInputs()
        {
            if (!Regex.IsMatch(_emailId.GetInputText(),  RexValues.EmailIdRex))
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init("이메일ID가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else if (!Regex.IsMatch(_password.GetInputText(), RexValues.PassWordRex))
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.Init("비밀번호가 규격에 맞지 않습니다", 
                    "확인",delegate { confirmPanel.gameObject.SetActive(false); });
            }
            else
            {
                _PostLoginRequest();
            }
        }

        private void _PostLoginRequest()
        {
            LoginRequest request = new LoginRequest();
            request.email = _emailId.GetInputText();
            request.password = _password.GetInputText();
            request.version = NetworkValues.ClientVersion;
            
            NetworkModule.Instance.WebHandler.Post(NetworkValues.Url + "Login", JsonUtility.ToJson(request), response =>
            {
                if (response != null)
                {
                    LoginResponse res = JsonUtility.FromJson<LoginResponse>(response);
                    if (res.errorCode == 0)
                    {
                        PlayerPrefs.SetString("Token", res.token);
                        PlayerPrefs.SetString("EmailID", request.email);
                        SceneManager.LoadSceneAsync("MainScene");
                    }
                    else
                    {
                        string errorName = Enum.GetName(typeof(ErrorCode), res.errorCode);
                        
                        confirmPanel.Init( "로그인에 실패했습니다.\n\n" + errorName,
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


