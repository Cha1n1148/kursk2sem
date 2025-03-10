USE [SnackStore]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 03.03.2025 14:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone] [varchar](20) NULL,
	[address] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_items]    Script Date: 03.03.2025 14:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_items](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[snack_id] [int] NULL,
	[quantity] [int] NULL,
	[price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 03.03.2025 14:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[order_date] [datetime] NULL,
	[total_amount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[snacks]    Script Date: 03.03.2025 14:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snacks](
	[snack_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[description] [text] NULL,
	[price] [decimal](10, 2) NOT NULL,
	[quantity] [int] NOT NULL,
	[category] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[snack_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[customers] ON 

INSERT [dbo].[customers] ([customer_id], [first_name], [last_name], [email], [phone], [address]) VALUES (1, N'John', N'Doe', N'john.doe@example.com', N'123-456-7890', N'123 Elm Street')
INSERT [dbo].[customers] ([customer_id], [first_name], [last_name], [email], [phone], [address]) VALUES (2, N'Jane', N'Smith', N'jane.smith@example.com', N'987-654-3210', N'456 Oak Avenue')
SET IDENTITY_INSERT [dbo].[customers] OFF
GO
SET IDENTITY_INSERT [dbo].[order_items] ON 

INSERT [dbo].[order_items] ([order_item_id], [order_id], [snack_id], [quantity], [price]) VALUES (1, 1, 1, 2, CAST(2.50 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [snack_id], [quantity], [price]) VALUES (2, 1, 2, 1, CAST(3.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([order_item_id], [order_id], [snack_id], [quantity], [price]) VALUES (3, 2, 3, 3, CAST(1.20 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[order_items] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([order_id], [customer_id], [order_date], [total_amount]) VALUES (1, 1, CAST(N'2025-03-03T13:33:32.373' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[orders] ([order_id], [customer_id], [order_date], [total_amount]) VALUES (2, 2, CAST(N'2025-03-03T13:33:32.373' AS DateTime), CAST(6.50 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[snacks] ON 

INSERT [dbo].[snacks] ([snack_id], [name], [description], [price], [quantity], [category]) VALUES (1, N'Chips', N'Delicious salty chips', CAST(2.50 AS Decimal(10, 2)), 100, N'Chips')
INSERT [dbo].[snacks] ([snack_id], [name], [description], [price], [quantity], [category]) VALUES (2, N'Peanuts', N'Roasted peanuts with salt', CAST(3.00 AS Decimal(10, 2)), 50, N'Nuts')
INSERT [dbo].[snacks] ([snack_id], [name], [description], [price], [quantity], [category]) VALUES (3, N'Candy', N'Sweet fruit candy', CAST(1.20 AS Decimal(10, 2)), 200, N'Sweets')
SET IDENTITY_INSERT [dbo].[snacks] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__customer__AB6E6164D62C7CDB]    Script Date: 03.03.2025 14:09:21 ******/
ALTER TABLE [dbo].[customers] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([order_id])
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([snack_id])
REFERENCES [dbo].[snacks] ([snack_id])
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([customer_id])
GO
