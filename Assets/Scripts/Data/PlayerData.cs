
using UnityEngine;
using UnityEngine.Events;

using General;
using Network;

namespace Data
{
    public struct PData
    {
        public int Coin;
        public int Dust;
    }
    
    
    public class PlayerData : MonoBehaviour
    {
        #region Private values

        private PData _pdata;

        [SerializeField] private UnityEvent<int> onCoinChanged;
        [SerializeField] private UnityEvent<int> onDustChanged;

        #endregion

        #region Public Values

        public int Coin
        {
            set
            {
                _pdata.Coin = value;
                onCoinChanged.Invoke(value);
            }
            get => _pdata.Coin;
        }

        public int Dust
        {
            set
            {
                _pdata.Dust = value;
                onDustChanged.Invoke(value);
            }
            get => _pdata.Dust;
        }

        #endregion

        #region Public Methods

        public PData GetData()
        {
            return _pdata;
        }

        public void SetData(PData data)
        {
            _pdata = data;
        }

        public void LoadDataFromServer()
        {
            NetworkModule.Instance.WebHandler.Get(NetworkValues.Url + "", response =>
            {
                Debug.Log(response);
                // if (response != null)
                // {
                //     if (res.errorCode == 0)
                //     {
                //         // TODO : convert response to pdata;
                //     }
                //     else
                //     {
                //         Debug.LogError("Fail to load player data");
                //     }
                // }
            });
        }
        
        #endregion
        
        
        #region Private Methods

        // private void Awake()
        // {
        //
        // }
        
        #endregion
    }
}


