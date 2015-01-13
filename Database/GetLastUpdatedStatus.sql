USE [NhsPtlCommentsWebPortal]
GO

/****** Object:  UserDefinedFunction [dbo].[GetLastUpdatedStatus]    Script Date: 01/12/2015 00:14:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetLastUpdatedStatus]
(
	-- Add the parameters for the function here
	@UniqueId AS VARCHAR(100),
	@LastUpdatedDate AS DATETIME
)
RETURNS VARCHAR(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @LastUpdatedStatus VARCHAR(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @LastUpdatedStatus = Status
	FROM PTL_Comments
	WHERE UniqueCDSRowIdentifier = @UniqueId AND UpdatedDate = @LastUpdatedDate

	-- Return the result of the function
	RETURN @LastUpdatedStatus

END

GO


