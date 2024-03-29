USE [LHFS.Models.LHFSContext]
GO
/****** Object:  Table [dbo].[Ranks]    Script Date: 11/21/2012 22:25:47 ******/
SET IDENTITY_INSERT [dbo].[Ranks] ON
INSERT [dbo].[Ranks] ([ID], [Title], [Shorttitle], [HourLimit], [VacationWeeks], [NumberOfTyperatings], [MinNumberOfFlightsPerMonth], [SmallRankIconPath]) VALUES (1, N'Captain', N'CPT', 450, 7, 3, 2, N'/Images/Ranks/rank_cpt.jpg')
INSERT [dbo].[Ranks] ([ID], [Title], [Shorttitle], [HourLimit], [VacationWeeks], [NumberOfTyperatings], [MinNumberOfFlightsPerMonth], [SmallRankIconPath]) VALUES (2, N'First Officer', N'FO', 100, 4, 3, 3, N'/Images/Ranks/rank_fo.jpg')
INSERT [dbo].[Ranks] ([ID], [Title], [Shorttitle], [HourLimit], [VacationWeeks], [NumberOfTyperatings], [MinNumberOfFlightsPerMonth], [SmallRankIconPath]) VALUES (3, N'Officer in Training', N'OIT', 0, 3, 2, 4, N'/Images/Ranks/norank.jpg')
INSERT [dbo].[Ranks] ([ID], [Title], [Shorttitle], [HourLimit], [VacationWeeks], [NumberOfTyperatings], [MinNumberOfFlightsPerMonth], [SmallRankIconPath]) VALUES (4, N'Senior First Officer', N'SFO', 300, 3, 3, 2, N'/Images/Ranks/rank_fo.jpg')
SET IDENTITY_INSERT [dbo].[Ranks] OFF
