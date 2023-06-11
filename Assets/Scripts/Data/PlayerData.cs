
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using General;
using Network;
using Network.PacketStructure;
using UI.PrefabScripts;

namespace Data
{
    public enum CollectionCode : long
    {
        Dust = 0,
        Coin = 1,
    }
    
    public class PlayerData : MonoBehaviour
    {
        #region Private values

        // [SerializeField] private AnnouncePanel announce;
        
        private List<CollectionBundle> _collection;

        [SerializeField] private UnityEvent<int> onCoinChanged;
        [SerializeField] private UnityEvent<int> onDustChanged;

        #endregion

        #region Public Values

        public static PlayerData Instance { get; private set; }
        
        public int Coin
        {
            set => onCoinChanged?.Invoke(value);
            get => _collection[_FindDataIndex(CollectionCode.Coin)].collectionCount;
        }

        public int Dust
        {
            set => onDustChanged?.Invoke(value);
            get => _collection[_FindDataIndex(CollectionCode.Dust)].collectionCount;
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 재화 리스트들을 Set하는 함수
        /// </summary>
        public void SetData(List<CollectionBundle> col)
        {
            _collection = col;
            Coin = _collection[_FindDataIndex(CollectionCode.Coin)].collectionCount;
            Dust = _collection[_FindDataIndex(CollectionCode.Dust)].collectionCount;
        }

        /// <summary>
        /// 미완성(필요한가?)
        /// 모든 데이터를 서버에서 불러오는 함수
        /// </summary>
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
        }

        #endregion
        
        #region Private Methods

        private int _FindDataIndex(CollectionCode code)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                if (_collection[i].collectionCode == (long)code)
                {
                    return i;
                }
            }

            return -1;
        }
        
        #endregion
    }
}


