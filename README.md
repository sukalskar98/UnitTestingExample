This is a demo application that contains Unit Test case examples for
1. Controllers
2. Business
3. Repository

**Steps to load and run the project -**
1. Clone the repository to Visual Studio 2022
2. Connect to your local server through SQL Server Management Studio
3. Create a database
4. Create a table called **Employee** using the below query

```/****** Object:  Table [dbo].[Employee]    Script Date: 8/17/2023 2:58:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](10) NULL,
	[LastName] [varchar](10) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
4. Go to DemoDB2Context file and change the connection string with the following changes -
```
"Server=<ServerName>;Initial Catalog=<DBName>;Integrated Security=SSPI;TrustServerCertificate=False;Encrypt=False;"

Note - Add your server name and db name in the connection string
```
5. Now run the solution
