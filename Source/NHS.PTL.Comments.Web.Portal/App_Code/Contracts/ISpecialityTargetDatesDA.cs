using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhs.Ptl.Comments.Contracts.Dto;

/// <summary>
/// Summary description for IWardDA
/// </summary>
namespace Nhs.Ptl.Comments
{
    public interface ISpecialityTargetDatesDA
    {
        List<SpecialityTargetDate> GetAllActiveSpecialityTargetDates();
        List<SpecialityTargetDate> GetAllSpecialityTargetDates();
        SpecialityTargetDate GetSpecialityTargetDateRecord(int id);
        bool AddSpecialityTargetDate(SpecialityTargetDate specialityTargetDate);
        bool UpdateSpecialityTargetDate(SpecialityTargetDate specialityTargetDate);
        bool ActivateInactivateSpecialityTargetDate(int id, int state);
    }
}