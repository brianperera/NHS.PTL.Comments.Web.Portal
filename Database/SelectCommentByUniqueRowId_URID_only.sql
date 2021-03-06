USE [WebFormsDataCaptureOutpatientPTLs]
GO
/****** Object:  StoredProcedure [dbo].[SelectCommentByUniqueRowId]    Script Date: 01/18/2015 09:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SelectCommentByUniqueRowId] 
	-- Add the parameters for the stored procedure here
	@UniqueCDSRowIdentifier AS NVARCHAR(255)
	
	
AS
BEGIN
	SELECT 	[Status],
			AppointmentDate,
			UpdatedDate,
			Comment,
			CreatedBy
			
	FROM	PTL_Comments
	WHERE	UniqueCDSRowIdentifier = @UniqueCDSRowIdentifier			

END
