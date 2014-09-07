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
    public class SpecialityTargetDate
    {
        public int ID { get; set; }
        public string Speciality { get; set; }
        public string Resp { get; set; }
        public int TargetDay { get; set; }
        public bool Activate { get; set; }
        public string DisplayActivate { get; set; }
    }
}