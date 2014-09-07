use NhsPtlCommentsWebPortal

GO

CREATE TABLE [Status]
(
[Name] nvarchar(300),
);

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,29-08-2014>
-- Description:	<Description,Insert InsertStatus>
-- =============================================
CREATE PROCEDURE [dbo].[InsertStatus]
	@Name varchar(300)
AS
BEGIN
INSERT INTO [dbo].[Status]
           ([Name])
     VALUES
           (@Name)
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,29-08-2014>
-- Description:	<Description,Get All GetStatuses>
-- =============================================
Create PROCEDURE [dbo].[GetStatuses]
AS
BEGIN
SELECT [Name]
  FROM [dbo].[Status]
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,29-08-2014>
-- Description:	<Description,Delete Status>
-- =============================================
Create PROCEDURE [dbo].[DeleteStatus]
	@Name varchar(300)
AS
BEGIN
DELETE FROM [dbo].[Status]
      WHERE [Name]=@Name
END

GO 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author : Brian>
-- Create date: <Create Date,29-08-2014>
-- Description:	<Description,Update Status>
-- =============================================
Create PROCEDURE [dbo].[UpdateStatus]
	@OldName varchar(300),
	@Name varchar(300)
AS
BEGIN
UPDATE [dbo].[Status]
   SET [Name] = @Name
 WHERE [Name]=@OldName
END

GO

INSERT [dbo].[Status] values('Booked')
INSERT [dbo].[Status] values('Validated off')
INSERT [dbo].[Status] values('Capacity required')
INSERT [dbo].[Status] values('Waiting for tests')
INSERT [dbo].[Status] values('Added to IP WL')
INSERT [dbo].[Status] values('Bring forward')
