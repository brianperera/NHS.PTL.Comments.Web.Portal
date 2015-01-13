USE [NhsPtlCommentsWebPortal]
GO

/****** Object:  UserDefinedFunction [dbo].[GetLastUpdatedTime]    Script Date: 01/12/2015 00:13:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetLastUpdatedTime] 
(
	-- Add the parameters for the function here
	@UniqueId AS VARCHAR(100)
)
RETURNS DateTime
AS
BEGIN
	-- Declare the return variable here
	DECLARE @UpdatedTime AS DateTime

	-- Add the T-SQL statements to compute the return value here
	SELECT @UpdatedTime = MAX(Comments.UpdatedDate)
	FROM PTL_Comments Comments
	WHERE Comments.UniqueCDSRowIdentifier = @UniqueId
	 

	-- Return the result of the function
	RETURN @UpdatedTime

END

GO


