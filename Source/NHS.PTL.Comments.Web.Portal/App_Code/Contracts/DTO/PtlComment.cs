using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PtlComment
/// </summary>
/// 
namespace Nhs.Ptl.Comments.Contracts.Dto
{
    public class PtlComment
    {
        public int CommentId { get; set; }
        public string UniqueCdsRowIdentifier { get; set; }        
        public string Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Comment { get; set; }
        public string PatientPathwayIdentifier { get; set; }
        public string Spec { get; set; }
        public DateTime ReferralRequestReceivedDate { get; set; }
        public DateTime RttBreachDate { get; set; }
        public DateTime FutureClinicDate { get; set; }
        public string CreatedBy { get; set; }
    }
}