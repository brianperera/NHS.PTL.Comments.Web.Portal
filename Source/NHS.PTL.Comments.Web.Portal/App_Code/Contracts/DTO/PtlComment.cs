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
        public double UniqueCdsRowIdentifier { get; set; }
        public string PatientPathwayIdentifier { get; set; }
        public string LocalPatientID { get; set; }
        public string NhsNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PatientForename { get; set; }
        public string PatientSurname { get; set; }
        public string Spec { get; set; }
        public string SpecName { get; set; }
        public string NewDivision { get; set; }
        public string Consultant { get; set; }
        public DateTime ReferralRequestReceivedDate { get; set; }
        public string SourceOfReferralText { get; set; }
        public string PriorityType { get; set; }
        public DateTime RttClockStart { get; set; }
        public DateTime RttBreachDate { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttStatus { get; set; }
        public string RttStatus { get; set; }
        public string RttText { get; set; }
        public string WaitingListStatus { get; set; }
        public DateTime FutureClinicDate { get; set; }
        public string WaitAtFutureClinicDate { get; set; }
    }
}