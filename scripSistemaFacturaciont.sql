USE [SistemaFacturacion]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 03/07/2023 2:39:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](60) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[Email] [varchar](100) NULL,
	[DNI] [varchar](40) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Correlative]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Correlative](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastNumber] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_Correlative] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NInvoice] [varchar](10) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IdClient] [int] NOT NULL,
	[SubTotal] [decimal](16, 2) NOT NULL,
	[ITBIS] [decimal](16, 2) NOT NULL,
	[Total] [decimal](16, 2) NOT NULL,
	[IdUser] [int] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetails]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdInvoice] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[Total] [decimal](16, 2) NOT NULL,
	[Discount] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](200) NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[IdCategory] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Producto_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RolName] [varchar](50) NULL,
	[Status] [bit] NULL,
	[DateCreate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/07/2023 2:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[idRol] [int] NULL,
	[Password] [varchar](200) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (2, N'Componentes', CAST(N'2023-06-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (4, N'Unidades de Almacenamiento', CAST(N'2023-07-02T00:54:40.297' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (5, N'Audio', CAST(N'2023-07-02T00:20:40.260' AS DateTime), 0)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (6, N'probando categorias', CAST(N'2023-07-02T21:46:02.870' AS DateTime), NULL)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (7, N'probando otra mas', CAST(N'2023-07-02T21:50:25.300' AS DateTime), NULL)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (8, N'probando  parte 3', CAST(N'2023-07-02T21:52:49.020' AS DateTime), NULL)
INSERT [dbo].[Category] ([Id], [Name], [DateCreate], [Status]) VALUES (9, N'yerba buena', CAST(N'2023-07-03T02:26:40.773' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([Id], [FirstName], [LastName], [Direccion], [Phone], [Email], [DNI], [Status]) VALUES (1, N'Wilson ', N'Garcia nova', N'calle los cerros #24', N'8492873549', N'wilson@gmail.com', N'00119036804', 1)
INSERT [dbo].[Client] ([Id], [FirstName], [LastName], [Direccion], [Phone], [Email], [DNI], [Status]) VALUES (2, N'Mayted ', N'Tavarez Brand', N'Colinas del rio apto. 24-03', N'8292873549', N'mayted@gmail.com', N'12132435', 1)
INSERT [dbo].[Client] ([Id], [FirstName], [LastName], [Direccion], [Phone], [Email], [DNI], [Status]) VALUES (4, N'Willainy', N'Garcia Tavarez', N'Colinas del rio', N'2435465465', N'Willainy@gmail', N'2323232354', 1)
INSERT [dbo].[Client] ([Id], [FirstName], [LastName], [Direccion], [Phone], [Email], [DNI], [Status]) VALUES (2006, N'Carmen', N'garcia Nova', N'24 de abril', N'43435353535', N'carmen@gmail.com', N'23245353', 1)
INSERT [dbo].[Client] ([Id], [FirstName], [LastName], [Direccion], [Phone], [Email], [DNI], [Status]) VALUES (2007, N'Jose', N'Cespedes', N'24 de abril', N'343535456', N'josen@gmail.com', N'3232353', 1)
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Correlative] ON 

INSERT [dbo].[Correlative] ([Id], [LastNumber], [DateCreate]) VALUES (1, 8, CAST(N'2023-07-02T22:10:16.623' AS DateTime))
SET IDENTITY_INSERT [dbo].[Correlative] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice] ON 

INSERT [dbo].[Invoice] ([Id], [NInvoice], [DateCreate], [IdClient], [SubTotal], [ITBIS], [Total], [IdUser]) VALUES (1033, N'000004', CAST(N'2023-07-02T17:56:07.763' AS DateTime), 1, CAST(16400.00 AS Decimal(16, 2)), CAST(3600.00 AS Decimal(16, 2)), CAST(20000.00 AS Decimal(16, 2)), 1)
INSERT [dbo].[Invoice] ([Id], [NInvoice], [DateCreate], [IdClient], [SubTotal], [ITBIS], [Total], [IdUser]) VALUES (1034, N'000005', CAST(N'2023-07-02T17:56:55.180' AS DateTime), 2, CAST(20910.00 AS Decimal(16, 2)), CAST(4590.00 AS Decimal(16, 2)), CAST(25500.00 AS Decimal(16, 2)), 1)
INSERT [dbo].[Invoice] ([Id], [NInvoice], [DateCreate], [IdClient], [SubTotal], [ITBIS], [Total], [IdUser]) VALUES (1035, N'000006', CAST(N'2023-07-02T17:59:46.717' AS DateTime), 1, CAST(15580.00 AS Decimal(16, 2)), CAST(3420.00 AS Decimal(16, 2)), CAST(19000.00 AS Decimal(16, 2)), 1)
INSERT [dbo].[Invoice] ([Id], [NInvoice], [DateCreate], [IdClient], [SubTotal], [ITBIS], [Total], [IdUser]) VALUES (1036, N'000007', CAST(N'2023-07-02T22:05:56.657' AS DateTime), 4, CAST(4920.00 AS Decimal(16, 2)), CAST(1080.00 AS Decimal(16, 2)), CAST(6000.00 AS Decimal(16, 2)), 1)
INSERT [dbo].[Invoice] ([Id], [NInvoice], [DateCreate], [IdClient], [SubTotal], [ITBIS], [Total], [IdUser]) VALUES (1037, N'000008', CAST(N'2023-07-02T22:10:16.623' AS DateTime), 4, CAST(4920.00 AS Decimal(16, 2)), CAST(1080.00 AS Decimal(16, 2)), CAST(6000.00 AS Decimal(16, 2)), 1)
SET IDENTITY_INSERT [dbo].[Invoice] OFF
GO
SET IDENTITY_INSERT [dbo].[InvoiceDetails] ON 

INSERT [dbo].[InvoiceDetails] ([Id], [IdInvoice], [IdProduct], [Amount], [Price], [Total], [Discount]) VALUES (1035, 1033, 1, 2, CAST(10000.00 AS Decimal(16, 2)), CAST(20000.00 AS Decimal(16, 2)), CAST(0.00 AS Decimal(16, 2)))
INSERT [dbo].[InvoiceDetails] ([Id], [IdInvoice], [IdProduct], [Amount], [Price], [Total], [Discount]) VALUES (1036, 1034, 2, 3, CAST(8500.00 AS Decimal(16, 2)), CAST(25500.00 AS Decimal(16, 2)), CAST(0.00 AS Decimal(16, 2)))
INSERT [dbo].[InvoiceDetails] ([Id], [IdInvoice], [IdProduct], [Amount], [Price], [Total], [Discount]) VALUES (1037, 1035, 1, 2, CAST(10000.00 AS Decimal(16, 2)), CAST(19000.00 AS Decimal(16, 2)), CAST(1000.00 AS Decimal(16, 2)))
INSERT [dbo].[InvoiceDetails] ([Id], [IdInvoice], [IdProduct], [Amount], [Price], [Total], [Discount]) VALUES (1038, 1036, 8, 3, CAST(2000.00 AS Decimal(16, 2)), CAST(6000.00 AS Decimal(16, 2)), CAST(0.00 AS Decimal(16, 2)))
INSERT [dbo].[InvoiceDetails] ([Id], [IdInvoice], [IdProduct], [Amount], [Price], [Total], [Discount]) VALUES (1039, 1037, 8, 3, CAST(2000.00 AS Decimal(16, 2)), CAST(6000.00 AS Decimal(16, 2)), CAST(0.00 AS Decimal(16, 2)))
SET IDENTITY_INSERT [dbo].[InvoiceDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (1, N'CPU', N'Intel core I5 11600k', CAST(10000.00 AS Decimal(16, 2)), 2, -2, CAST(N'2023-06-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (2, N'MotherBoard', N'Asus Gaming soket 1200', CAST(8500.00 AS Decimal(16, 2)), 2, 6, CAST(N'2023-06-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (3, N'Memoria Ram', N'CCorsair Vengeance RGB Pro 16GB (2x8GB) DDR4 3200MHz ', CAST(4300.00 AS Decimal(16, 2)), 2, 33, CAST(N'2023-06-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (4, N'SSD', N'Samsung 970 EVO Plus Series - PCIe NVMe  1TB', CAST(4800.00 AS Decimal(16, 2)), 4, 8, CAST(N'2023-06-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (8, N'USB Kistong', N'Memoria USB de 1TB', CAST(2000.00 AS Decimal(16, 2)), 4, -1, CAST(N'2023-07-01T21:38:26.267' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [IdCategory], [Stock], [DateCreate], [Status]) VALUES (10, N'pepito', N'klk', CAST(4.00 AS Decimal(16, 2)), 4, 21, CAST(N'2023-07-01T21:47:32.840' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([Id], [RolName], [Status], [DateCreate]) VALUES (1, N'Admin', 1, CAST(N'2023-06-07T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [idRol], [Password], [Status]) VALUES (1, N'Wilson', N'garcia Nova', N'wilson@gmail.com', 1, N'123456', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Factura] FOREIGN KEY([IdInvoice])
REFERENCES [dbo].[Invoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_DetalleFactura_Factura]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Producto] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_DetalleFactura_Producto]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Categoria_Producto] FOREIGN KEY([IdCategory])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Categoria_Producto]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Producto] FOREIGN KEY([Id])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Producto_Producto]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([Id])
GO
