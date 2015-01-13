
CREATE TYPE UniqueIds AS TABLE
(
	UniqueCDSRowIdentifier VARCHAR(100)
)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE GetPtlCommentsByUniqueIds 
	@uniqueIds UniqueIds READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM PTL_Comments
	WHERE UniqueCDSRowIdentifier IN (SELECT UniqueCDSRowIdentifier FROM @uniqueIds)
END
GO
