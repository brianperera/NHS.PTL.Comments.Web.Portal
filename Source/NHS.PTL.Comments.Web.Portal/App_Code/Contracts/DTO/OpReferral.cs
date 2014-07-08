using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OpReferral
/// </summary>
public class OpReferral
{
    public string UniqueCdsRowIdentifier { get; set; }
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
    public int WaitAtFutureClinicDate { get; set; }
    public string Status { get; set; }
    public string WeekswaitGrouped { get; set; }

    public OpReferral()
    {
    }

    public OpReferral DeepClone(OpReferral referral)
    {
        OpReferral clonedRef = new OpReferral();
        clonedRef.AttendanceDate = referral.AttendanceDate;
        clonedRef.AttStatus = referral.AttStatus;
        clonedRef.Consultant = referral.Consultant;
        clonedRef.DateOfBirth = referral.DateOfBirth;
        clonedRef.FutureClinicDate = referral.FutureClinicDate;
        clonedRef.LocalPatientID = referral.LocalPatientID;
        clonedRef.NewDivision = referral.NewDivision;
        clonedRef.NhsNumber = referral.NhsNumber;
        clonedRef.PatientForename = referral.PatientForename;
        clonedRef.PatientPathwayIdentifier = referral.PatientPathwayIdentifier;
        clonedRef.PatientSurname = referral.PatientSurname;
        clonedRef.PriorityType = referral.PriorityType;
        clonedRef.ReferralRequestReceivedDate = referral.ReferralRequestReceivedDate;
        clonedRef.RttBreachDate = referral.RttBreachDate;
        clonedRef.RttClockStart = referral.RttClockStart;
        clonedRef.RttStatus = referral.RttStatus;
        clonedRef.RttText = referral.RttText;
        clonedRef.SourceOfReferralText = referral.SourceOfReferralText;
        clonedRef.Spec = referral.Spec;
        clonedRef.SpecName = referral.SpecName;
        clonedRef.Status = referral.Status;
        clonedRef.UniqueCdsRowIdentifier = referral.UniqueCdsRowIdentifier;
        clonedRef.WaitAtFutureClinicDate = referral.WaitAtFutureClinicDate;
        clonedRef.WaitingListStatus = referral.WaitingListStatus;
        clonedRef.WeekswaitGrouped = referral.WeekswaitGrouped;

        return clonedRef;
    }   
}