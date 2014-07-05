USE [RTT]
GO

/****** Object:  Table [dbo].[OP_Referral_PTL]    Script Date: 07/05/2014 17:07:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OP_Referral_PTL](
	[UniqueCDSRowIdentifier] [varchar](100) NULL,
	[LocalPatientID] [varchar](50) NULL,
	[NHSNumber] [varchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[PatientForename] [varchar](50) NULL,
	[PatientSurname] [varchar](50) NULL,
	[PatientPathwayIdentifier] [varchar](50) NULL,
	[Consultant] [varchar](50) NULL,
	[Spec] [varchar](50) NULL,
	[SpecName] [varchar](50) NULL,
	[NewDivision] [varchar](50) NULL,
	[RTTStatus] [varchar](50) NULL,
	[RTTText] [varchar](100) NULL,
	[ReferralRequestReceivedDate] [datetime] NULL,
	[RTTClockStart] [datetime] NULL,
	[RTTBreachDate] [datetime] NULL,
	[Weekswait] [int] NULL,
	[WeekswaitGrouped] [varchar](10) NULL,
	[SourceOfReferralText] [varchar](255) NULL,
	[FirstAtt] [varchar](50) NULL,
	[FutureClinicDate] [datetime] NULL,
	[FutureAppointmentType] [varchar](50) NULL,
	[FutureAppointmentLocation] [varchar](50) NULL,
	[FutureClinicBookedDate] [datetime] NULL,
	[FutureClinicFirstBookedDate] [datetime] NULL,
	[WaitAtFutureClinicDate] [int] NULL,
	[WaitAtFutureClinicDateGrouped] [varchar](10) NULL,
	[PriorityType] [varchar](50) NULL,
	[AttendanceDate] [datetime] NULL,
	[AttendanceAppointmentType] [varchar](50) NULL,
	[AttendanceAppointmentLocation] [varchar](50) NULL,
	[AttendanceBookedDate] [datetime] NULL,
	[AttendanceFirstBookedDate] [datetime] NULL,
	[AttStatus] [varchar](50) NULL,
	[WaitingListStatus] [varchar](50) NULL,
	[Census] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

