
using UnityEngine;

using UI.PrefabScripts;

namespace UI.MainScene
{
    public class BasePanel : MonoBehaviour
    {
        #region Public Values

        #endregion
        
        #region private Values

        [SerializeField] private AnnouncePanel announcePanel;
        [SerializeField] private MailPanel mailPanel;
        [SerializeField] private StorePanel storePanel;
        [SerializeField] private SettingPanel settingPanel;
        [SerializeField] private ProfilePanel profilePanel;

        #endregion

        #region Public Methods

        public void ToMailPanel()
        {
            
        }

        #endregion

        #region Mono Methods

        private void Awake()
        {
            
        }

        #endregion
    }
}


