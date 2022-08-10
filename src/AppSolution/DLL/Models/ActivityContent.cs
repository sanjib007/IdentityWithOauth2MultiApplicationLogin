using System;
using System.ComponentModel.DataAnnotations;

namespace DLL.Models
{
    public class ActivityContent
    {
        [Key]
        public long ActivityContentId { get; set; }
        public string Process { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsSMSSent { get; set; }
        public bool IsPushNotiSent { get; set; }
        public bool IsShownInActivity { get; set; }
        public string eMailSub { get; set; }
        public string MailContent { get; set; }
        public string SMS { get; set; }
        public string PushContent { get; set; }
        public string ActivityMsg { get; set; }
        public string ContentFor { get; set; }
        public bool IsTransaction { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public string PushBody { get; set; }
        public string PushType { get; set; }
        public string PushImage { get; set; }
    }
}