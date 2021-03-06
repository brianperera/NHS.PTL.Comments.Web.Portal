USE [NhsPtlCommentsWebPortal]
GO
/****** Object:  Table [dbo].[SpecialityTargetDateLookup]    Script Date: 09/07/2014 16:26:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialityTargetDateLookup](
	[ID] [int] NOT NULL,
	[Spec] [nvarchar](250) NULL,
	[Resp] [nvarchar](250) NULL,
	[TargetDays] [int] NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (328, N'STROKE MEDICINE', N'OPD', 49, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (107, N'VASCULAR SURGERY', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (100, N'GENERAL SURGERY', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (101, N'UROLOGY', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (103, N'BREAST SURGERY', N'OPD', 14, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (110, N'TRAUMA & ORTHOPAEDICS', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (120, N'ENT', N'OPD', 56, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (130, N'OPHTHALMOLOGY', N'OPD - OPH', 63, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (140, N'ORAL SURGERY', N'OPD - Oral', 63, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (143, N'ORTHODONTICS', N'OPD - Oral', NULL, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (160, N'PLASTIC SURGERY', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (191, N'PAIN MANAGEMENT', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (257, N'PAEDIATRIC DERMATOLOGY', N'OPD - Derm', NULL, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (300, N'GENERAL MEDICINE', N'OPD', NULL, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (301, N'GASTROENTEROLOGY', N'OPD', 77, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (302, N'ENDOCRINOLOGY', N'OPD', 91, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (307, N'DIABETIC MEDICINE', N'OPD', 91, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (317, N'ALLERGY', N'OPD', 56, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (320, N'CARDIOLOGY', N'OPD', 42, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (330, N'DERMATOLOGY', N'OPD - Derm', 56, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (340, N'RESPIRATORY MEDICINE', N'OPD', 77, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (400, N'NEUROLOGY', N'OPD', 70, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (410, N'RHEUMATOLOGY', N'OPD', 91, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (430, N'GERIATRIC MEDICINE', N'OPD', 70, 1)
INSERT [dbo].[SpecialityTargetDateLookup] ([ID], [Spec], [Resp], [TargetDays], [Active]) VALUES (502, N'GYNAECOLOGY', N'OPD - Gynae', 42, 1)


GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Get All SpecialityTargetDates>
-- =============================================
CREATE PROCEDURE [dbo].[GetSpecialityTargetDates]
AS
BEGIN
SELECT [ID], [Spec], [TargetDays], [Active]
  FROM [dbo].[SpecialityTargetDateLookup]
END

GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Get All ActiveSpecialityTargetDates>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllActiveSpecialityTargetDates]
AS
BEGIN
SELECT [ID], [Spec], [TargetDays], [Active]
  FROM [dbo].[SpecialityTargetDateLookup]
  WHERE [Active]='1'
END

GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Get Single SpecialityTargetDateRecord>
-- =============================================
CREATE PROCEDURE [dbo].[GetSpecialityTargetDateRecord]
	@ID int
AS
BEGIN
SELECT [ID], [Spec], [TargetDays], [Active]
  FROM [dbo].[SpecialityTargetDateLookup]
  WHERE [ID]=@ID
END

GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Insert SpecialityTargetDate>
-- =============================================
CREATE PROCEDURE [dbo].[InsertSpecialityTargetDate]
	@ID int,
	@Spec varchar(250),
	@TargetDays int,
	@Active bit
AS
BEGIN
INSERT INTO [dbo].[SpecialityTargetDateLookup]
           ([ID], [Spec], [TargetDays], [Active])
     VALUES
           (@ID, @Spec, @TargetDays, @Active)
END

GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Update UpdateSpecialityTargetDate>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSpecialityTargetDate]
	@ID int,
	@Spec varchar(250),
	@TargetDays int,
	@Active int
AS
BEGIN
UPDATE [dbo].[SpecialityTargetDateLookup]
   SET [Spec] = @Spec, [TargetDays] = @TargetDays, [Active] = @Active
 WHERE [ID]=@ID
END

GO

-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,07-09-2014>
-- Description:	<Description,Delete SpecialityTargetDate>
-- =============================================
CREATE PROCEDURE [dbo].[ActiveInactiveSpecialityTargetDate]
	@ID int, @Active bit
AS
BEGIN
UPDATE [dbo].[SpecialityTargetDateLookup]
   SET [Active] = @Active
 WHERE [ID]=@ID
END