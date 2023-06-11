
using UnityEngine;

namespace Network
{
    public class NetworkModule : MonoBehaviour
    {
        #region Public values

        public static NetworkModule Instance { get; private set; }

        public WebHandler WebHandler;
        
        #endregion
        
        #region Private values

        

        #endregion

         
        #region Public Methods


        #endregion

        #region Mono Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }

            WebHandler = new WebHandler(this);
        }

        #endregion

        #region Private Methods
        
        

        #endregion
    }
}