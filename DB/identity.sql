/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 29.08.2017 14:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201708291049366_InitialCreate', N'KlienciSTP.Web.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F20FF20E82959382D5F7606B3463B81D3B6B346C6174C7BB2791BB025765B18895224CAB1B1D82FDB87FDA4FD852D4AD485375DBAE5EE7610209816C953C5E221592C16FDBFFFFC77FAE37318584F3849FD889CD9479343DBC2C48D3C9FACCEEC8C2EBFFF60FFF8C3377F995E7AE1B3F56B59EF84D58396243DB31F298D4F1D27751F7188D249E8BB4994464B3A71A3D0415EE41C1F1EFEDD393A723040D8806559D34F19A17E88F31FF073161117C73443C14DE4E120E5DFA1649EA35AB728C4698C5C7C66FF12F8A0A03F7FB89FFC132F264503DB3A0F7C04CACC71B0B42D4448441105554F3FA7784E9388ACE6317C40C1C34B8CA1DE120529E65D38ADABF7EDCDE131EB8D53372CA1DC2CA5513810F0E8849BC7919BAF6564BB321F18F0120C4D5F58AF73239ED9D71ECE3F7D8A0230802CF0741624ACF2997D5389384FE35B4C2765C34901799500DC1F51F275D2443CB07AB73BA8E8743C3964FF1D58B32CA05982CF08CE68828203EB3E5B04BEFB0B7E7988BE62727672B4589E7C78F71E7927EFFF864FDE357B0A7D857AC207F8749F44314E4037BCACFA6F5B8ED8CE911B56CD1A6D0AAB00976066D8D60D7AFE88C98A3EC29C39FE605B57FE33F6CA2F9C5C9F890F13091AD124839FB75910A04580AB72A75526FB7F8BD4E377EF47917A8B9EFC553EF4927C983809CCAB4F38C84BD3473F2EA69730DE5F78B5AB240AD96F915F45E9977994252EEB4C64ACF2809215A6A27653A7266F2F4A33A8F1695DA2EE3FB599A62ABDB5555987D69909A5886DCF8652DFD795DB9B71E7710C8397538B59A48D70DAFD6A22011C5862B59A40477D0944A0637FE6F5F032447E30C282D8430AB8234B3F0971D5CB9F22A01F228375BE47690AEB81F70F943EB6A80EFF1C41F53976B304683AA7288C5F5DDAFD6344F06D162E18FBB7276BB4A179F823BA422E8D924BC25A6D8CF73172BF4619BD24DE05A2F833754B40F6F3C10FFB038CA2CEB9EBE234BD0232636F1681B75D025E137A723C188EAD51BB76486601F243BD4722ADA65FCAAAB557A2AFA17826866A3AEFA44DD58FD1CA27FD542DAB9A552D6A74AACAAB0D559581F5D394D7342B9A57E8D4B3A8359ABF978FD0F80E5F0EBBFF1EDF669BB7692D6898710E2B24FE19139CC032E6DD234A7142EA11E8B36EECC259C8878F097DF5BD2997F42B0AB2B145AD351BF24560FCD990C3EEFF6CC8D584CF4FBEC7BC921EC7A0B232C0F7AAAF3F6175CF3949B36D4F07A19BDB16BE9D35C0345DCED33472FD7C166802603C7C21EA0F3E9CD51DCB287A23C743A06340749F6D79F005FA66CBA4BA231738C0145BE76E11209CA1D4459E6A46E8903740B17247D52856C74544E5FEAAC804A6E3843542EC1094C24CF50955A7850F27D518059D56925AF6DCC258DF2B1972C9058E3161023B2DD147B83E0CC214A8E44883D265A1A9D3605C3B110D5EAB69CCBB5CD87ADC95E8C45638D9E13B1B78C9FDB7572166BBC5B640CE7693F451C018D2DB0541F959A52F01E483CBBE11543A311908CA5DAAAD1054B4D80E082A9AE4CD11B438A2F61D7FE9BCBA6FF4140FCADBDFD65BCDB5036E0AF6D8336A16BE27B4A1D002272A3D2F16AC103F53CDE10CF4E4E7B394BBBA324518F81C53316453FBBB5A3FD469079149D4065813AD03945F062A40CA841AA05C19CB6BD58E7B110360CBB85B2B2C5FFB25D8060754ECE6A568A3A2F9EA542667AFD347D5B38A0D0AC97B1D161A381A42C88B97D8F11E4631C56555C3F4F1858778C38D8EF1C168315087E76A3052D999D1AD5452B3DB4A3A876C884BB6919524F7C960A5B233A35B8973B4DB481AA760805BB09189C42D7CA4C956463AAADDA62A9B3A45BA14FF30750C7955D31B14C73E5935F2ACF8176B5E2459CDBE9F0F4F3D0A0B0CC74D35194895B695241A256885A552100D9A5EF9494A2F10450BC4E23C332F54AA69F756C3F25F8A6C6E9FEA2096FB40599BFDBB68A1BFC217B65BD51FE13057D0C990393579245D43017D738BA5BEA100259AE0FD2C0AB290987D2C73EBE20AAFD9BEF8A2224C1D497FC587520CA678BAA2F57B8D8D3A2FC61BA7CA8B597FACCC10268B973E68D3E626BFD48C5286A99A28A6D0D5CEC6CEE4CE0C1D2FD9591C3E5C9D08AF33BB78864A13807F1A88D1487250C01A65FD51C53C9426A658D21F514A3669424A4503B46CA694084A360BD6C23358545FA3BF043589A489AE96F647D6A49334A135C56B606B7496CBFAA36A324E9AC09AE2FED875FA89BC8EEEF1FE653CC26CB2811507DDCD763003C6EB2C8AE36C808DFBFC2650E3F3402C7E63AF80F1EF7B4928E3696F134215218ECD0865C030AF3FC265B8B8FCB4DEE09B31851B6E61896FBBE137E30DA3EDAB924339EFC9552AE9D5B94F3ADF4DF959ABFB718D72F82AAAD8566946D8DE5F528AC309AB3099FF1ECC18C3685DE106117F89535A6475D8C78747C7D2E39CFD7928E3A4A91768CEAAA6D732E2986D21418B3CA1C47D44899A2EB1C163921A5489445F130F3F9FD9FFCA5B9DE6410DF6AFFCF381759D7E26FEEF19143C2419B6FEADA67F8E935CDF7EDADAD3A710FDAD7AFDDB97A2E9817597C08C39B50E255BAE33C2E2038941DA144D37D066ED67136F774209AF11B4A8D28458FFF1C1C2A7A33C3C28B5FC3644CFDF0D554DFBB8602344CD0382B1F04631A1E981C03A58C6C7011EFCA4F9E380619DD53F16584735E343019F0C07939F09F45F86CA963BDC6A34C7A26D2C49B99D3BD3AC37CAB9DCF5DEA464636F34D1D58CEB01701B6455AFC18C3796903CDAEEA8C9371E0D7B97D47EF524E37DC92BAE333E769B4EBCCD0CE296FBA13F55E2F01EA4BA695277769F1EBC6DAE9942B97B9E63392C0978CFC8C613BA769FEABB6DB299C2BC7B4EB64109BD7BC6B55DED9F3B665AEF2D74E7E9B96AA691E14A46170BEE4ABF2D02E770C25F444082C2A32C5E4DEAF3BDDA72553B04D655CC42CD8966B26065E22872951AED6287F5956FF8AD9DE575DAC51AD233DB64F3F5BF5536AFD32EDB90F4B88BC4616DDAA12E99BB631D6BCB867A4B89C2424F3AF2D2BB7CD6D6FBF5B794173C8A5184D963B8237E3B69C0A39864CCA93320ED57BDEE85BDB3F1171761FF4EFD550DC1FEFE22C1AEB06B5675AEC9322A376F49A3B28A14A1B9C11479B0A59E27D45F229742318B31E7CFBEF3B81DBBE95860EF9ADC6534CE287419878B4008783127A04D7E9EDB2CEA3CBD8BF3BF60324617404D9FC5E6EFC84F991F7895DE579A9890018279173CA2CBC692B2C8EEEAA542BA8D484F206EBECA297AC0611C00587A47E6E809AFA31BD0EF235E21F7A58E009A40BA074234FBF4C247AB048529C7A8DBC34FE0B0173EFFF07FCADE96D178540000, N'6.1.3-40302')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8c9ce9c8-28b5-472c-b6be-ad71a11ae6b8', N'j.krzacz@gmail.com', 0, N'AEWaRIwOSmSfrnvCBU/lHX5fLxNzugeSZ5S4GtKNWcimRAdhKxJrgHXX6msbQ5/MTA==', N'21ffa543-cbd3-4887-83c5-fd165b536fa1', NULL, 0, 0, NULL, 1, 0, N'j.krzacz@gmail.com')
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 29.08.2017 14:10:20 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 29.08.2017 14:10:20 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 29.08.2017 14:10:20 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 29.08.2017 14:10:20 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 29.08.2017 14:10:20 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 29.08.2017 14:10:20 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO