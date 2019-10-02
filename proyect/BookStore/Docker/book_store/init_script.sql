USE [master];
GO
IF EXISTS (SELECT [name] FROM [master].[sys].[databases] WHERE [name] = N'BookStoreDB')
    DROP DATABASE [BookStoreDB]
GO
CREATE DATABASE [BookStoreDB]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
    EXEC [BookStoreDB].[dbo].[sp_fulltext_database] @action = 'enable'
END
GO
ALTER DATABASE [BookStoreDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [BookStoreDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [BookStoreDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [BookStoreDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [BookStoreDB] SET ARITHABORT OFF
GO
ALTER DATABASE [BookStoreDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [BookStoreDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [BookStoreDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [BookStoreDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [BookStoreDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [BookStoreDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [BookStoreDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [BookStoreDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [BookStoreDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [BookStoreDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [BookStoreDB] SET ALLOW_SNAPSHOT_ISOLATION ON
GO
ALTER DATABASE [BookStoreDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [BookStoreDB] SET READ_COMMITTED_SNAPSHOT ON
GO
ALTER DATABASE [BookStoreDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [BookStoreDB] SET  MULTI_USER
GO
ALTER DATABASE [BookStoreDB] SET DB_CHAINING OFF
GO
USE [BookStoreDB]
GO
/****** Object:  Table [dbo].[Authors] */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[FirstName] NVARCHAR(50) NULL,
	[LastName] NVARCHAR(70) NULL,
 CONSTRAINT [PK_authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Books] */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Books](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(90) NULL,
	[InsertDate] DATETIME NULL,
	[AuthorId] INT NULL,
	[CategoryId] INT NULL,
 CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories] */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(30) NULL,
    [InsertDate] DATETIME NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users] */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserName] NVARCHAR(10) NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reviews] */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Review] NVARCHAR(1000) NULL,
    [InsertDate] DATETIME NULL,
	[UserId] INT NULL,
	[BookId] INT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Authors] ON
 
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (1, N'William Dean', N'Howells')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (2, N'Frederic', N'Brown')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (3, N'Jack', N'London')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (4, N'Albert', N'Blaisdell')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (5, N'Ellis', N'Butler')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (6, N'Arthur', N'Machen')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (7, N'Titus', N'Lucretius')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (8, N'Rabindranath', N'Tagore')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (9, N'Isaac', N'Asimov')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (10, N'Charles', N'Dickens')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (11, N'Ralph Waldo', N'Emerson')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (12, N'Dorothy', N'Canfield')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (13, N'Givoanni', N'Boccaccio')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (14, N'George', N'Orwell')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (15, N'Publius', N'Ovid')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (16, N'Robert Louis', N'Stevenson')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (17, N'Virginia', N'Woolf')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (18, N'George', N'Eliot')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (19, N'Amelia B.', N'Edwards')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (20, N'Fyodor', N'Dostoevsky')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (21, N'Emily', N'Dickinson')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (22, N'Edna', N'Ferber')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (23, N'Joseph Sheridan', N'LeFanu')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (24, N'John', N'DosPassos')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (25, N'Ruth', N'Stuart')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (26, N'Vladimir', N'Nabokov')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (27, N'Johanna', N'Spyri')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (28, N'Ernest', N'Dowson')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (29, N'Mary Hallock', N'Foote')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (30, N'Zane', N'Grey')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (31, N'H. P.', N'Lovecraft')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (32, N'Samuel', N'Pepys')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (33, N'Kate Dickinson', N'Sweetser')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (34, N'William', N'Lampton')
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName]) VALUES (35, N'Mother', N'Goose')

SET IDENTITY_INSERT [dbo].[Authors] OFF

SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (1, N'A Daughter of the Snows', CAST(N'2019-09-08' AS DateTime), 3, 9)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (2, N'The Near East: 10,000 Years of History', CAST(N'2019-09-18' AS DateTime), 9, 13)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (3, N'The Cocoon: A Rest-Cure Comedy', CAST(N'2019-09-11' AS DateTime), 25, 12)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (4, N'The Freakshow Murders', CAST(N'2019-09-08' AS DateTime), 2, 3)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (5, N'Pharaohs, Fellahs and Explorers', CAST(N'2019-09-12' AS DateTime),19, 9)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (6, N'Hard Times', CAST(N'2019-09-13' AS DateTime), 10, 1)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (7, N'A Modern Instance', CAST(N'2019-09-18' AS DateTime), 1, 12)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (8, N'The Real Mother Goose', CAST(N'2019-09-17' AS DateTime), 35, 1)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (9, N'A Thousand Miles Up the Nile', CAST(N'2019-09-18' AS DateTime), 19, 18)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (10, N'Children of Blood and Bone', CAST(N'2019-09-18' AS DateTime), 7, 13)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (11, N'A pushcart at the curb', CAST(N'2019-09-19' AS DateTime), 24, 1)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (12, N'The Desert and the Sown', CAST(N'2019-09-20' AS DateTime), 29, 10)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (13, N'Three Soldiers', CAST(N'2019-09-21' AS DateTime), 24, 16)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (14, N'The End of Eternity', CAST(N'2019-09-19' AS DateTime), 9, 1)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (15, N'Annie Kilburn', CAST(N'2019-09-20' AS DateTime), 1, 12)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (16, N'A Touch of Sun and Other Stories', CAST(N'2019-09-21' AS DateTime), 29, 9)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (17, N'Show Boat', CAST(N'2019-09-22' AS DateTime), 22, 18)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (18, N'The Call of the Wild', CAST(N'2019-09-22' AS DateTime), 3, 18)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (19, N'My Mark Twain', CAST(N'2019-09-23' AS DateTime), 1, 9)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (20, N'Broken Ties', CAST(N'2019-09-23' AS DateTime), 8, 5)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (21, N'Short Stories From American History', CAST(N'2019-09-08' AS DateTime), 4, 8)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (22, N'Mrs Rosie and the Priest', CAST(N'2019-07-18' AS DateTime), 13, 14)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (23, N'So Big', CAST(N'2019-05-18' AS DateTime), 22, 3)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (24, N'Monsieur Maurice ', CAST(N'2019-09-18' AS DateTime), 19, 8)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (25, N'The Master of Ballantrae', CAST(N'2019-08-08' AS DateTime), 16, 13)
INSERT [dbo].[Books] ([Id], [Name], [InsertDate], [AuthorId], [CategoryId]) VALUES (26, N'The Unlived Life of Little Mary Ellen', CAST(N'2019-09-08' AS DateTime), 25, 6)

SET IDENTITY_INSERT [dbo].[Books] OFF

SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (1, N'Science fiction', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (2, N'Satire', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (3, N'Drama', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (4, N'Action and Adventure', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (5, N'Romance', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (6, N'Mystery', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (7, N'Horror', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (8, N'Health', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (9, N'Guide', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (10, N'Diaries', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (11, N'Comics', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (12, N'Diaries', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (13, N'Journals', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (14, N'Biographies', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (15, N'Fantasy', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (16, N'History', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (17, N'Science', CAST(N'2019-09-08' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [InsertDate]) VALUES (18, N'Art', CAST(N'2019-09-08' AS DateTime))

SET IDENTITY_INSERT [dbo].[Categories] OFF

SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName]) VALUES (1, N'HaGreen')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (2, N'Ashall')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (3, N'Aneen')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (4, N'Alpman')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (5, N'Melor')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (6, N'Grght')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (7, N'Paxter')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (8, N'Gng')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (9, N'Jonlins')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (10, N'Leslung')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (11, N'Haill')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (12, N'Washtin')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (13, N'Ramirez')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (14, N'Morgan')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (15, N'Baker')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (16, N'Alfie')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (17, N'Madison')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (18, N'Bevere')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (19, N'Kaden')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (20, N'Carter')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (21, N'Adams')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (22, N'Parker')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (23, N'Henders')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (24, N'Wright')
INSERT [dbo].[Users] ([Id], [UserName]) VALUES (25, N'Wilki')

SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_books_authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_books_authors]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_books_categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_books_categories]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_reviews_users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_reviews_users]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_reviews_books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_reviews_books]
GO