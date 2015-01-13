USE [NhsPtlCommentsWebPortal]
GO

/****** Object:  StoredProcedure [dbo].[SelectAllOpReferralData]    Script Date: 01/12/2015 00:15:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectAllOpReferralData]
	@StartRowNum AS INT,
	@EndRowNum AS INT,
	@TotalRowCount AS INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
	
SELECT * FROM
(
SELECT	ROW_NUMBER() OVER (ORDER BY(SELECT 1)) AS RowNum,
		Ref.UniqueCDSRowIdentifier,
		Ref.LocalPatientID,
		Ref.NHSNumber,
		Ref.DateOfBirth,
		Ref.PatientForename,
		Ref.PatientSurname,
		Ref.PatientPathwayIdentifier,
		Ref.Consultant,
		Ref.Spec,
		Ref.SpecName,
		Ref.NewDivision,
		Ref.RTTStatus,
		Ref.RTTText,
		Ref.ReferralRequestReceivedDate,
		Ref.RTTClockStart,
		Ref.RTTBreachDate,
		Ref.Weekswait,
		Ref.WeekswaitGrouped,
		Ref.SourceOfReferralText,
		Ref.FirstAtt,
		Ref.FutureClinicDate,
		Ref.FutureAppointmentType,
		Ref.FutureAppointmentLocation,
		Ref.FutureClinicBookedDate,
		Ref.FutureClinicFirstBookedDate,
		Ref.WaitAtFutureClinicDate,
		Ref.WaitAtFutureClinicDateGrouped,
		Ref.PriorityType,
		Ref.AttendanceDate,
		Ref.AttendanceAppointmentType,
		Ref.AttendanceAppointmentLocation,
		Ref.AttendanceBookedDate,
		Ref.AttStatus,
		Ref.WaitingListStatus,
		Ref.Census,
		dbo.GetLastUpdatedStatus
		(
			Ref.UniqueCDSRowIdentifier, 
			dbo.GetLastUpdatedTime(Ref.UniqueCDSRowIdentifier)
		) AS Status

FROM OP_Referral_PTL.dbo.OP_Referral_PTL Ref
)  AS Temp
WHERE Temp.RowNum BETWEEN @StartRowNum AND @EndRowNum
	
SELECT @TotalRowCount = COUNT(*) FROM OP_Referral_PTL.dbo.OP_Referral_PTL		

END

GO


