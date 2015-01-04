using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Utility;

public partial class DbTest : Page
{
    private const string Specialty = "ALLERGY";
    private const string FutureAppointmentStatus = "All";    

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Literal1.Text = "Ruquesting all referral data";
        DateTime start = DateTime.Now;
        IList<OpReferral> opReferral = CommentsManager.GetAllOpReferrals();
        Literal2.Text = "Operation took " 
            + DateTime.Now.Subtract(start).TotalMilliseconds 
            + " milliseconds. <br/ > Row count: "
            + opReferral.Count;

        Literal3.Text = "Requesting with parameters. Specialty = " + Specialty + " , Future Appointment Status = " + FutureAppointmentStatus;

        start = DateTime.Now;
        opReferral = CommentsManager.GetOpReferralsByParams(Specialty, new List<string>(), FutureAppointmentStatus);        
        Literal4.Text = "Operation took "
            + DateTime.Now.Subtract(start).TotalMilliseconds
            + " milliseconds. <br/ > Row count: "
            + opReferral.Count;
    }
}