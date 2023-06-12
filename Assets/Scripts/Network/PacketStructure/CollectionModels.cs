

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network.PacketStructure
{
    public class AllCollectionListUpRequest : BaseRequest
    {
    }

    public class CurrencyListUpRequest : BaseRequest
    {
    }

    public class CardListUpRequest : BaseRequest
    {
    }

    public class CurrencyListUpResponse : BaseResponse
    {
        public List<CollectionBundle> currencyList;
    }

    [Serializable]
    public class AllCollectionListUpResponse : BaseResponse
    {
        [SerializeField] public List<CollectionBundle> collectionList;
    }

    public class CardListUpResponse : BaseResponse
    {
        public List<CollectionBundle> cardList;
    }
}


