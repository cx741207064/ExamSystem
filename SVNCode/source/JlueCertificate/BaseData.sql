USE [JlueCertificate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'O（order:顺序），D（disorder：无顺序，单独）'  , @level0type=N'SCHEMA',@level0name=N'dbo',
@level1type=N'TABLE',@level1name=N'T_Certificate', @level2type=N'COLUMN',@level2name=N'Rule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'O（order:顺序），D（disorder：无顺序，单独）'  , @level0type=N'SCHEMA',@level0name=N'dbo',
@level1type=N'TABLE',@level1name=N'T_CertifiSubject', @level2type=N'COLUMN',@level2name=N'Rule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:好会计电脑账, 2: 报税 '  , @level0type=N'SCHEMA',@level0name=N'dbo',
@level1type=N'TABLE',@level1name=N'T_SchemeNode', @level2type=N'COLUMN',@level2name=N'Type'
Go

--[T_Certificate]
INSERT INTO [dbo].[T_Certificate]([Id],[Name],[AbbName],[Rule],[Describe]) VALUES(70001,'岗位会计能力证书','岗位会计能力证书','D','岗位会计能力证书');
INSERT INTO [dbo].[T_Certificate]([Id],[Name],[AbbName],[Rule],[Describe]) VALUES(70002,'行业会计能力证书','行业会计能力证书','D','行业会计能力证书');
INSERT INTO [dbo].[T_Certificate]([Id],[Name],[AbbName],[Rule],[Describe]) VALUES(70003,'财税专项技能证书','财税专项技能证书','D','财税专项技能证书');
GO

--[T_CertifiSubject]
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70001,80001,'出纳','出纳','D',
N'*线上练习:出纳视频、出纳单判、单据仿真线上练习、网银 *书本教材:出纳教材、出纳练习单据簿、出纳练习单据 *平时分数:出纳教材视频、出纳单判、（仿真线上练习和网银平时练习用，不纳入平时成绩） *考试分数:仿真线上练习、网银');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70001,80002,'成本','成本','D',
N'*线上练习:成本视频、成本单判、成本线上练习（表格应用、业务题目） *书本教材:成本教材、成本会计单据簿 *平时分数:成本教材视频、成本单判 *考试分数:成本的业务题目（表格应用）');


INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80003,'工业','工业','D',
N'*线上练习:查看综合版会计实务视频、练习工业电脑账、通用报税 *书本教材:综合版会计实务、工业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（工业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:工业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80004,'商业','商业','D',
N'*线上练习:查看综合版会计实务视频、练习商业电脑账、通用报税 *书本教材:综合版会计实务、商业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（商业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:商业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80005,'物业','物业','D',
N'*线上练习:查看综合版会计实务视频、练习物业电脑账、通用报税 *书本教材:综合版会计实务、物业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（物业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:物业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80006,'酒店业','酒店业','D',
N'*线上练习:查看综合版会计实务视频、练习酒店业电脑账、通用报税 *书本教材:综合版会计实务、酒店业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（酒店业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:酒店业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80007,'物流业','物流业','D',
N'*线上练习:查看综合版会计实务视频、练习物流业电脑账、通用报税 *书本教材:综合版会计实务、物流业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（物流业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:物流业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80008,'广告业','广告业','D',
N'*线上练习:查看综合版会计实务视频、练习广告业电脑账、通用报税 *书本教材:综合版会计实务、广告业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（广告业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:广告业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80009,'房地产业','房地产业','D',
N'*线上练习:查看综合版会计实务视频、练习房地产业电脑账、通用报税 *书本教材:综合版会计实务、房地产业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（房地产业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:房地产业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80010,'工业外贸业','工业外贸业','D',
N'*线上练习:查看综合版会计实务视频、练习工业外贸业电脑账、通用报税 *书本教材:综合版会计实务、工业外贸业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（工业外贸业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:工业外贸业电脑账、通用报税');
INSERT INTO [dbo].[T_CertifiSubject]([CertificateId],[Id],[Name],[AbbName],[Rule],[Describe])VALUES(70002,80011,'商业外贸业','商业外贸业','D',
N'*线上练习:查看综合版会计实务视频、练习商业外贸业电脑账、通用报税 *书本教材:综合版会计实务、商业外贸业会计单据簿 *平时分数:看综合版会计实务视频、做对应综合版会计实务题库单多判（商业外贸业电脑账、通用报税只是平时练习，不纳入平时分数） *考试分数:商业外贸业电脑账、通用报税');

GO

GO
