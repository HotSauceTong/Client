
using UnityEngine;

using UI.PrefabScripts;

namespace UI.MainScene
{
    // TODO : 메일 구조체 서버거 가져오기
    
    public class MailPanel : MonoBehaviour
    {
        #region Public Values

        

        #endregion

        #region Private Values
        
        [SerializeField] private AnnouncePanel announcePanel;
        
        // SerializedField private InfiniteScroll;
        // SerializedField private EmailSummary emailSummary;
        
        #endregion

        #region Public Methods

        public void Init()
        {
            // TODO
            // 1. Enable this gameobject and Loading animation
            // 2. Get Email data list from Server
            // 3. Load Email inspect by using emailSummary
            // 4. Disable Loading Animation
        }

        #endregion

        #region Mono Methods
        

        #endregion

        #region Private Methods

        #endregion
    }
}


