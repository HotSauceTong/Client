using System;
using System.Collections.Generic;

namespace Network.PacketStructure
{
    /// <summary>
    /// 메일 리스트용 클래스
    /// </summary>
    public class MailListElement
    {
        public Int64 mailId;
        public Int64 collectionCode;
        public int collectionCount;
        public string mailTitle;
        public String sender;
        public DateTime readDate;
        public DateTime expirationDate;
    }

    /// <summary>
    /// 메일 세부내용 클래스
    /// </summary>
    public class Mail
    {
        public Int64 mailId;
        public Int64 collectionCode;
        public int collectionCount;
        public string mailTitle;
        public string mailBody;
        public String sender;
        public DateTime readDate;
        public DateTime receiveDate;
        public DateTime expirationDate;
    }

    /// <summary>
    /// 메일에 첨부되는 아이템의 코드와 개수
    /// </summary>
    public class CollectionBundle
    {
        public Int64 collectionCode;
        public Int32 collectionCount;
    }


    /// <summary>
    /// 메일 리스트용 리스트를 요청하고 응답
    /// </summary>
    public class MailListRequest : BaseRequest
    {
    }
    
    public class MailListResponse : BaseResponse
    {
        public List<MailListElement> mailList;
    }

    /// <summary>
    /// 특정 mailId의 메일을 읽음 처리 하고 응답으로 세부 데이터를 던짐
    /// </summary>
    public class MailReadRequest : BaseRequest
    {
        public Int64 mailId;
    }
    
    public class MailReadResponse : BaseResponse
    {
        public Mail mail;
    }

    /// <summary>
    /// 메일에 첨부된 콜렉션을 유저 콜렉션에 추가
    /// </summary>
    public class MailItemReceiveRequest : BaseRequest
    {
        public Int64 mailId;
    }
    
    public class MailItemReceiveResponse : BaseResponse
    {
        public CollectionBundle collectionBundle;
    }
}