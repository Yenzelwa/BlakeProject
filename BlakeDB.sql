USE [master]
GO
/****** Object:  Database [Blake]    Script Date: 15/07/2019 15:21:01 ******/
CREATE DATABASE [Blake]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Blake', FILENAME = N'C:\Users\NokukhanyaD\Blake.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Blake_log', FILENAME = N'C:\Users\NokukhanyaD\Blake_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Blake] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Blake].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Blake] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Blake] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Blake] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Blake] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Blake] SET ARITHABORT OFF 
GO
ALTER DATABASE [Blake] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Blake] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Blake] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Blake] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Blake] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Blake] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Blake] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Blake] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Blake] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Blake] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Blake] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Blake] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Blake] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Blake] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Blake] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Blake] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Blake] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Blake] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Blake] SET  MULTI_USER 
GO
ALTER DATABASE [Blake] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Blake] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Blake] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Blake] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Blake] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Blake] SET QUERY_STORE = OFF
GO
USE [Blake]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Blake]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[person_code] [int] NOT NULL,
	[account_number] [varchar](50) NOT NULL,
	[outstanding_balance] [money] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[surname] [varchar](50) NULL,
	[id_number] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[account_code] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[capture_date] [datetime] NOT NULL,
	[amount] [money] NOT NULL,
	[description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Account_num]    Script Date: 15/07/2019 15:21:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Account_num] ON [dbo].[Accounts]
(
	[account_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20190713-064138]    Script Date: 15/07/2019 15:21:01 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20190713-064138] ON [dbo].[Accounts]
(
	[account_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Person_id]    Script Date: 15/07/2019 15:21:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Person_id] ON [dbo].[Persons]
(
	[id_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20190713-064214]    Script Date: 15/07/2019 15:21:01 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20190713-064214] ON [dbo].[Persons]
(
	[surname] ASC,
	[id_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Account_Person] FOREIGN KEY([person_code])
REFERENCES [dbo].[Persons] ([code])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Account_Person]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([account_code])
REFERENCES [dbo].[Accounts] ([code])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transaction_Account]
GO
/****** Object:  StoredProcedure [dbo].[GetAccount]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nokukhanya Dumakude
-- Create date: 2019/07/13
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccount] 
	@code int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT acc.code,
	account_number,
	trans.code AS tranCode,
	 outstanding_balance, 
	 trans.amount,transaction_date,
	 trans.description,
	 acc.person_code
	FROM Transactions  trans
	RIGHT JOIN Accounts  acc ON acc.code= trans.account_code
	Where acc.code = @code
END
GO
/****** Object:  StoredProcedure [dbo].[GetPersonById]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nokukhanya Dumakude
-- Create date: 2019/07/14
-- Description:	Get all list of persons
-- =============================================
CREATE PROCEDURE [dbo].[GetPersonById]
	@code int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	p.code,
	p.name,
	p.surname,
	p.id_number,
	acc.code AS AccCode,
	acc.account_number,
	acc.outstanding_balance
	       FROM Persons p
	LEFT JOIN Accounts acc ON acc.person_code = p.code
		   where p.code = @code
END
GO
/****** Object:  StoredProcedure [dbo].[GetPersons]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nokukhanya Dumakude
-- Create date: 2019/07/14
-- Description:	Get all list of persons
-- =============================================
CREATE PROCEDURE [dbo].[GetPersons] 
	@PageSize int ,
	@PageNumber int,
	@SearchString varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @TotalPages INT = 0
	IF(@PageNumber IS NULL)
	     SET @PageNumber = 1
	IF(@PageSize IS NULL)
	   SET @PageSize = 10 

	   CREATE TABLE  #Persons(
	   [code] [int] NOT NULL,
		[name] [varchar](50) NULL,
		[surname] [varchar](50) NULL,
		[id_number] [varchar](50) NOT NULL
	   
	   )	 
	IF (@SearchString IS NOT NULL OR @SearchString !='')
	BEGIN
	INSERT INTO #Persons
	    SELECT 
		   p.code,
	       [name],
		   surname,
		   id_number
	      
	FROM Persons p
	LEFT JOIN Accounts acc ON acc.person_code =  p.code
	WHERE p.id_number = @SearchString OR surname = @SearchString OR acc.account_number = @SearchString 
	SELECT @TotalPages = CEILING(COUNT(code) / CAST(15 AS DECIMAL(18,2))) FROM #Persons
	 
	END
	ELSE
	BEGIN
		INSERT INTO #Persons
	    SELECT  code,
	       [name],
		   surname,
		   id_number  
	FROM Persons 
	SELECT @TotalPages = CEILING(COUNT(code) / CAST(15 AS DECIMAL(18,2))) FROM #Persons
	END


	SELECT 

	     code,
	       [name],
		   surname,	
		      
		@TotalPages AS TotalPages
		FROM (
		  SELECT ROW_NUMBER() OVER (ORDER BY code DESC ) AS RowNum,
		 code,
	       [name],
		   surname,
		   id_number,
		  @TotalPages AS TotalPages
	  
	FROM #Persons  
		) AS PersonsList
		WHERE RowNum >=((@PageNumber -1)*@PageSize) +1 AND
		      RowNum < (@PageNumber *@PageSize) +1
			
		DROP TABLE #Persons
END
GO
/****** Object:  StoredProcedure [dbo].[GetTransaction]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nokukhanya Dumakude
-- Create date: 2019/07/13
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTransaction]
	@code int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT trans.code,trans.transaction_date , trans.capture_date, trans.amount,trans.description
	FROM Transactions trans
	Where code = @code
END
GO
/****** Object:  StoredProcedure [dbo].[Search_Person]    Script Date: 15/07/2019 15:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nokukhanya Dumakude
-- Create date: 2019/07/13
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Search_Person] 
	@stringParam varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.code,p.name,p.surname
	FROM Persons p
	lEFT JOIN Accounts acc ON acc.person_code = p.code
	Where P.id_number = @stringParam OR acc.account_number = @stringParam
END
GO
USE [master]
GO
ALTER DATABASE [Blake] SET  READ_WRITE 
GO
