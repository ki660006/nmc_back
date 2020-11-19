'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON00.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : ���� ����ü ����                                                       */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Namespace SVar
#Region " ���� ����ü ���� : ä������ ����"

    Public Class STU_GVINFO
        Public REGNO As String = ""
        Public STATUS As String = ""
        Public DEPTCD_USR As String = ""
        Public DEPTNM_USR As String = ""
        Public ORDCD As String = ""
        Public ORDCD2 As String = ""
        Public ORDDRID As String = ""
        Public ORDDRNM As String = ""
        Public SUGACD As String = ""
        Public SUGACD2 As String = ""
        Public SPCCD As String = ""
    End Class


    Public Class STU_COLLWEB
        Public OWNGBN As String = ""
        Public REGNO As String = ""     ' ��Ϲ�ȣ
        Public ORDDT As String = ""     ' 
        Public FKOCS As String = ""     '
        Public IOGBN As String = ""
        Public IOFLAG As String = ""

        Public BCCLSCD As String = ""   '
        Public STATGBN As String = ""   '
        Public TCDGBN As String = ""    '
        Public SPCCD As String = ""     ' ��ü�ڵ�
        Public TCLSCD As String = ""    ' �˻��ڵ�

        Public SERIES As Boolean = False    ' ���Ӱ˻� ���� �Ǻ���

        Public HEIGHT As String = ""    ' Ű
        Public WEIGHT As String = ""    ' ü��

        Public DIAGCD As String = ""
        Public DIAGNM As String = ""
        Public DIAGNM_ENG As String = ""

        '-- 
        Public BCNO As String = ""
        Public COLLDT As String = ""
        Public SPCFLG As String = ""

        Public ERPRTYN As String = "" '<<<20180802 ���� ����Ʈ ���� 

    End Class
    Public Class REFLIST
        Public RHospiCd As String = ""
        Public RHospiNm As String = ""
        Public RHospiUsr As String = ""
        Public SpcName As String = ""
        Public SpcSex As String = ""
        Public SpcBirTh As String = ""
        Public SpcRegno As String = ""
        Public SpcDept As String = ""
        Public Spc As String = ""
        Public Spcetc As String = ""
        Public Test As String = ""
        Public Testetc As String = ""
        Public Refcd As String = ""
        Public Tkdt As String = ""
        Public fndt As String = ""
        Public TestUsr As String = ""
        Public RptUsr As String = ""
        Public Bcno As String = ""
        Public Groupcd As String = ""
    End Class
    Public Class STU_CANCELWEB
        Public JOBGBN As String = ""
        Public CMTCD As String = ""
        Public CMTCONT As String = ""
        Public REGNO As String = ""
        Public OWNGBN As String = ""
        Public SPCCD As String = ""
        Public BCNOS As String = ""
        Public TESTCDS As String = ""
        Public FKOCSS As String = ""
    End Class

    Public Class STU_CollectInfo
        Public REGNO As String = ""     ' ��Ϲ�ȣ
        Public TCLSCD As String = ""    ' �˻��ڵ�
        Public SPCCD As String = ""     ' ��ü�ڵ�
        Public PATNM As String = ""     ' ����
        Public SEX As String = ""       ' ����
        Public AGE As String = ""       ' ����
        Public DAGE As String = ""      ' �� ȯ�� ����
        Public BIRTHDAY As String = ""  ' ����
        Public IDNOL As String = ""     ' �ֹε�Ϲ�ȣ_����
        Public IDNOR As String = ""     ' �ֹε�Ϲ�ȣ_������
        Public TEL1 As String = ""      ' ����ó1
        Public TEL2 As String = ""      ' ����ó2
        Public DOCTORCD As String = ""  ' �Ƿ��ǻ��ڵ�
        Public DOCTORNM As String = ""  ' �Ƿ��ǻ��ڵ�
        Public GENDRCD As String = ""   ' ��ġ���ڵ�
        Public DEPTCD As String = ""    ' ��
        Public DEPTNM As String = ""    ' �������
        Public DEPTABBR As String = ""    ' �������
        Public WARDNO As String = ""    ' �����ڵ�
        Public WARDNM As String = ""    ' �����̸�
        Public WARDABBR As String = ""    ' �����̸�
        Public ROOMNO As String = ""    ' ���ǹ�ȣ
        Public BEDNO As String = ""     ' ħ���ȣ
        Public ENTDT As String = ""     ' �Կ�����
        Public STATGBN As String = ""   ' ���ޱ���
        Public OPDT As String = ""      ' ����������
        Public REMARK As String = ""    ' �Ƿ��ǻ� REMARK
        Public REMARK_NRS As String = "" ' �Ƿ��ǻ� REMARK2
        Public IOGBN As String = ""     ' �Կ�/�ܷ� ����
        Public FKOCS As String = ""     ' OCSKEY
        Public BCPRTDT As String = ""   ' ���ڵ�����Ͻ�
        Public ORDDT As String = ""     ' ó���Ͻ�
        Public RESDT As String = ""     ' �����Ͻ�
        Public JUBSUGBN As String = ""  ' ��������
        Public SUGACD As String = ""    ' �����ڵ�

        Public LISCMT As String = ""    '-- �ŷ�ó����

        '< yjlee 2009-01-05 ��õ��õ�⺴�� 
        Public TORDCD As String = ""    ' ó���ڵ�
        Public SUNABYN As String = ""
        '> 

        Public COLLVOL As String = ""   ' ä����
        Public COLLID As String = ""    ' ä����
        Public COLLDT As String = ""    ' ä���Ͻ�

        Public HEIGHT As String = ""    ' Ű
        Public WEIGHT As String = ""    ' ü��
        Public OWNGBN As String = ""    ' OCSó�� or LISó��
        Public COMMENT As String = ""   ' ���� COMMENT

        Public BCCLSCD As String = ""   ' �˻�з�
        Public EXLABCD As String = ""   ' ��Ź����ڵ�
        Public EXLABYN As String = ""   ' ���ֿ���
        Public POCTYN As String = ""    ' ����˻翩��
        Public BCONEYN As String = ""
        Public TUBECD As String = ""    ' Ʃ���ڵ�
        Public NRS_TIME As String = ""  ' ��ȣ�� Ȯ�νð�
        Public ORDSLIP As String = ""   ' ó�潽��(���� ���̺� �ִ� ����Ÿ)
        Public PARTGBN As String = ""   ' L:����, R:������, P:����

        '���Ӱ˻� ���� �Ǻ���
        Public SEQTMI As Integer = 0
        Public BCKEY As String = ""
        Public BCKEY2 As String = ""
        Public BCKEY3 As String = ""
        Public SERIES As Boolean = False

        '���ڵ� ��¿�
        Public BCNO As String = ""
        Public PRTBCNO As String = ""
        Public TNMBP As String = ""
        Public SPCNMBP As String = ""
        Public TUBENMBP As String = ""
        Public TCDGBN As String = ""

        Public HREGNO As String = ""
        Public TKDT As String = ""
        Public ORDPART As String = ""
        Public INFINFO As String = ""

        Public TGRPNM As String = ""
        Public BCCNT As String = ""
        Public CPRTGBN As String = ""

        Public ERPRTYN As String = "" '<<<20180801 ���޹��ڵ� �߰� 
    End Class

    Public Class STU_DiagInfo
        Public DIAGCD As String = ""
        Public DIAGNM As String = ""
        Public DIAGNM_ENG As String = ""
    End Class

    Public Class STU_DrugInfo
        Public DRUGCD As String = ""
        Public DRUGNM As String = ""
    End Class

    Public Class STU_EntInfo
        Public WARDCD As String = ""
        Public WARDNM As String = ""
        Public SRCD As String = ""
        Public SRNM As String = ""
        Public SBCD As String = ""
        Public ENTDT As String = ""
    End Class


    Public Class STU_OrderInfo
        Public BCKEY As String = ""
        Public GRPNO As String = ""
        Public ORDDT As String = ""
        Public TCLSCD As String = ""
        Public REGNO As String = ""
        Public NRSDT As String = ""
    End Class

    Public Class STU_PatInfo
        Public REGNO As String = ""
        Public PATNM As String = ""
        Public SEX As String = ""
        Public AGE As String = ""
        Public DAGE As String = ""
        Public IDNOL As String = ""
        Public IDNOR As String = ""
        Public IDNO As String = ""  ' ��ȣȭ�� �ֹι�ȣ( 030101-1****** )
        Public BIRTHDAY As String = ""
        Public TEL1 As String = ""
        Public TEL2 As String = ""
        Public WARD As String = ""
        Public WARDNM As String = ""
        Public ROOMNO As String = ""
        Public BEDNO As String = ""
        Public ENTDT As String = ""
        Public ORDDT As String = ""
        Public ERFLG As String = ""

        Public RESDT As String = ""

        Public HEIGHT As String = ""
        Public WEIGHT As String = ""

        Public SRNM As String = ""
        Public DIAG_K As String = ""
        Public DIAG_E As String = ""
        Public DRUG As String = ""
        Public OWNGBN As String = ""
        Public IOGBN As String = ""
        Public DEPTCD As String = ""
        Public DEPTNM As String = ""
        Public DOCTORCD As String = ""
        Public DOCTORNM As String = ""
        '����Ϲ�ȣ
        Public WHOSPID As String = ""
        '��������
        Public INFINFO As String = ""
        Public INFINFOP As String = ""
        Public IsInfected As Boolean = False
        Public SPCOMMENT As String = ""     '���󳻿� 
        Public ABORh As String = ""

        Public INJONG As String = ""        '���� 
        Public GUBUN As String = ""         'ȯ������
        Public SOGAE As String = ""         '��������
        Public VIP As String = ""           'VIP���� 

        Public PathologyYN As String = ""   ' �������� ����

        Public DiagLeukemia As Boolean = False '������ ���ܸ� Y/N

    End Class

    Public Class STU_TestItemInfo
        Public REGNo As String = ""         ' ��Ϲ�ȣ

        Public SPCFLG As String = ""       ' ��ü����
        Public RSTFLG As String = ""       ' �������
        Public ORDDT As String = ""         ' ó���Ͻ�
        Public DEPTNM As String = ""        ' ����
        Public DOCTORNM As String = ""      ' �Ƿ��ǻ�� 
        Public TNMD As String = ""          ' �˻��
        Public SPCNMD As String = ""        ' ��ü��
        Public STATGBN As String = ""       ' ���ޱ���
        Public APPEND_YN As String = ""     ' �߰�����
        Public REMARK As String = ""        ' �Ƿ��ǻ� Remark
        Public HOPEDT As String = ""        ' �˻�����Ͻ�
        Public COMMENT As String = ""       ' LAB COMMENT
        Public CWARNING As String = ""      ' ä�������ǻ���
        Public RESDT As String = ""         ' ���Ό���Ͻ�

        Public DEPTCD As String = ""        ' ���ڵ�
        Public DOCTORCD As String = ""      ' �Ƿ��ǻ��ڵ�
        Public ORDCD As String = ""         ' ó���ڵ�
        Public TESTCD As String = ""        ' �˻��ڵ�
        Public SPCCD As String = ""         ' ��ü�ڵ�
        Public BCCLSCD As String = ""       ' ��ü�з�
        Public MINSPCVOL As String = ""     ' �ּ� ä����
        Public SUGACD As String = ""        ' �����ڵ�
        Public EXLABCD As String = ""       ' ��Ź����ڵ�
        Public EXLABYN As String = ""       ' ��Ÿ�˻�����
        Public EXEDAY As String = ""        ' �˻����
        Public SEQTYN As String = ""        ' ���Ӱ˻� ��/��
        Public SEQTMI As String = ""        ' ���Ӱ˻�ð�
        Public HEIGHT As String = ""        ' Ű
        Public WEIGHT As String = ""        ' ü��
        Public TUBECD As String = ""        ' ��ü����ڵ�
        Public SPCNMBP As String = ""       ' ��ü�� ���ڵ� ���
        Public TUBENMBP As String = ""      ' ��ü���� ���ڵ� ���
        Public TNMBP As String = ""         ' �˻�� ���ڵ� ���
        Public OWNGBN As String = ""        ' OCSó�� or LISó��
        Public FKOCS As String = ""         ' OCSKEY
        Public BCKEY As String = ""         ' BCKEY
        Public INPUT_PART As String = ""    ' ORDPART
        Public BCCNT As String = "1"        ' ����� ���ڵ� ��
        Public DCFLAG As String = ""        ' DCFLAG
        Public BCNO As String = ""          ' ��ü��ȣ
        Public TCLS_SPC As String           ' ��ü�ڵ�

        Public INSUGBN As String = ""       ' ���豸��
        Public IOGBN As String = ""         ' �ܷ�/�Կ� ����
        Public ORDDT_APPEND As String = ""  ' �߰�ó���Ͻ�
        'Public PLGBN As String = ""         ' �����׽�Ʈ ��������
        Public DBLTSEQ As String = ""       ' �ٸ���ü ������������ ó��
        Public PARTCD As String = ""        ' ��Ʈ����
        Public TCDGBN As String = ""        ' �˻��ڵ屸��
        Public ORDTCLSCD As String = ""     ' ó���׸��ڵ�
        Public WORKNO As String = ""        ' �۾���ȣ
        Public INPUT_PARTNM As String = ""  '
        Public NRS_CFM_YN As String = ""    ' ��ȣȮ��
        Public NRS_TIME As String = ""      ' ��ȣȮ�� �ð�

        Public REQ_REMARK As String = ""

        Public VIRUS_YN As String = ""      ' ��������

        Public ROOMNO As String = ""    '����
        Public WARDCD As String = ""  '����
        Public ENTDT As String = ""  '�Կ���

    End Class

#End Region

#Region "���ڵ�, ���׶� ����"
    Public Class STU_BCPRTINFO
        Public BCNOPRT As String = ""       '-- ��¿� ���ڵ��ȣ
        Public BCNO As String = ""          '-- ���ڵ� FULL ��ȣ
        Public REGNO As String = ""         '-- ��Ϲ�ȣ
        Public PATNM As String = ""         '-- ȯ�ڸ�
        Public SEXAGE As String = ""        '-- ����/����
        Public BCCLSCD As String = ""       '-- ��ü����
        Public DEPTWARD As String = ""      '-- �����/����
        Public IOGBN As String = ""         '-- �Կܱ���
        Public SPCNM As String = ""         '-- ��ü��
        Public TUBENM As String = ""        '-- Tube name
        Public TESTNMS As String = ""       '-- �˻��
        Public EMER As String = ""          '-- ���޿���(Y)
        Public INFINFO As String = ""       '-- ��������
        Public TGRPNM As String = ""        '-- �˻�׷�
        Public XMATCH As String = ""        '-- Cross Matching ����(A)
        Public REMARK As String = ""        '-- �ǻ� Remark
        Public BCCNT As String = ""         '-- ��¸ż�
        Public BCTYPE As String = ""        '-- ��¾��
        Public HREGNO As String = ""        '-- 
        Public BCNO_MB As String = ""       '-- �̻����� ���
        Public ERPRTYN As String = ""       '-- ��������Ʈ <<<20180802
        Public ABOCHK As String = ""        '-- ������ ���� üũ 2019-04-19
    End Class

    Public Class STU_BLDLABEL
        Public REGNO As String = ""
        Public PATNM As String = ""
        Public SEXAGE As String = ""
        Public DEPTWARD As String = ""
        Public COMNM As String = ""
        Public BLD_ABORH As String = ""
        Public PAT_ABORH As String = ""
        Public BLDNO As New ArrayList

        Public TESTDT As String = ""
        Public TESTNM As String = ""
        Public BEFOUTDT As String = ""
        Public BEFOUTNM As String = ""
        Public OUTDT As String = ""
        Public OUTNM As String = ""
        Public RECDT As String = ""
        Public RECNM As String = ""
        Public BLDCD As String = ""

        Public IDNO As String = ""          '-- �ֹι�ȣ
        Public XMATCH1 As String = ""       '-- CrossMatching 1�� ���
        Public XMATCH2 As String = ""       '-- CrossMatching 2�� ���
        Public XMATCH3 As String = ""       '-- CrossMatching 3�� ���
        Public XMATCH4 As String = ""       '-- CrossMatching 4�� ���
        Public IR As String = ""            '-- IR 
        Public FITER As String = ""         '-- Filter
        Public Hb_RST As String = ""        '-- 
    End Class

    Public Class STU_GOODSBCINFO
        Public GoodsCd As String = ""
        Public GoodsNm As String = ""
        Public LotNo As String = ""
        Public InDt As String = ""
        Public ValidDt As String = ""
        Public KeepStatus As String = ""
        Public InQnt As String = "1"
    End Class

#End Region

    '-- ������ü
    Public Class STU_KsRack    ' ������ü �������� 
        Public Bcclscd As String = ""
        Public RackId As String = ""
        Public SpcCd As String = ""
        Public Bcno As String = ""
        Public RegDt As String = ""
        Public RegId As String = ""
        Public NumCol As String = ""
        Public NumRow As String = ""
        Public AlarmTerm As String = ""
        Public Other As String = ""       ' ���� Comment

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    Public Class STU_COLLINFO
        Public ORDDT1 As String = ""
        Public ORDDT2 As String = ""
        Public SPCFLG1 As String = ""
        Public SPCFLG2 As String = ""
        Public REGNO As String = ""
        Public DEPTCD As String = ""
        Public WARDCD As String = ""
        Public IOGBN As String = ""
        Public PARTGBN As String = ""       '-- L(����)/R(������)
    End Class

    Public Class STU_CANCELINFO
        Public BCNO As String = ""
        Public TCLSCD As String = ""
        Public SPCCD As String = ""
        Public TCDGBN As String = ""
        Public IOGBN As String = ""
        Public FKOCS As String = ""
        Public TORDCD As String = ""
        Public OWNGBN As String = ""
        Public BCCLSCD As String = ""
        Public CANCELCD As String = ""
        Public CANCELCMT As String = ""

        Public REGNO As String = ""
        Public SPCFLG As String = ""

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    Public Class STU_PrtItemInfo
        Public CHECK As String = ""
        Public TITLE As String = ""
        Public FIELD As String = ""
        Public WIDTH As String = ""
    End Class

    Public Class STU_DataColInfo
        Public ColName As String
        Public ColType As Type
        Public ColCapt As String
    End Class

    Public Class STU_TCLSCD
        Public mTESTCD As String        ' �˻��ڵ�
        Public mTNM As String           ' �˻��
        Public mTCDGBN As String        ' �˻籸��
        Public mSPCCD As String         ' ��ü�ڵ�
        Public mTNMP As String          ' ��°˻��
    End Class

    Public Class STU_RVInfo
        Public Shared msRegNo As String = ""
        Public Shared msStartDt As String = ""
        Public Shared msEndDt As String = ""
    End Class

    Public Class STU_StDataInfo
        Public Data As Object = Nothing
        Public Data2 As Object = Nothing
        Public Alignment As Integer = 0
    End Class

    Public Class STU_StDataInfo_NCOV
        Public Data As Object = Nothing
        Public Data2 As Object = Nothing
        Public sResult As String = ""
        Public Alignment As Integer = 0
    End Class

    Public Class STU_RptSrchInfo
        Public RptRegNo As String = ""
        Public RptIOFlg As String = ""
        Public RptDayB As String = ""
        Public RptDayE As String = ""
        Public RptDeptCd As String = ""
        Public RptDrCd As String = ""
    End Class

    Public Class STU_RptTypeInfo
        Public RptType As String = ""
        Public RptFmtCd As String = ""
        Public RptForm As String = ""
        Public RptSlip As String = ""
    End Class

    Public Class STU_UserWkListInfo
        Public WLCDay As String = ""
        Public WLCTime As String = ""
        Public WLCId As String = ""
        Public WLTitle As String = ""
        Public WKSeq As Integer = 0
        Public BcNo As String = ""
        Public TClsCd As String = ""
        Public SpcCd As String = ""
        Public WkCmt As String = ""
    End Class

#Region " ���� ����ü ���� : ����Է�"

    Public Class STU_RstInfo
        '����
        Public TestCd As String
        Public OrgRst As String
        Public ViewRst As String
        Public HlMark As String
        Public RegStep As String
        Public CfmNm As String = ""
        Public CfmSign As String = ""
        Public CfmSignRst As String = ""

        'SAMPLE only
        Public RstCmt As String
        Public DeltaMark As String
        Public PanicMark As String
        Public CriticalMark As String
        Public AlertMark As String

        Public EqFlag As String

        Public RstRTF As String = ""
        Public RstTXT As String = ""

        Public ChageRst As String = ""

        Public AddFileNm1 As String = ""
        Public AddFileNm2 As String = ""

        Public RstDt As String = ""

    End Class

    Public Class STU_SampleInfo
        Public RegStep As String
        Public BCNo As String
        Public EqCd As String
        Public UsrID As String
        Public UsrIP As String
        Public IntSeqNo As String
        Public Rack As String
        Public Pos As String
        Public EqBCNo As String
        Public SenderID As String
        Public BfRst As String '-JJH �������
    End Class

    Public Class STU_RstInfo_ep
        Public TestCd As String
        Public OrgRst As String
        Public ViewRst As String
        Public JudgMark As String
        Public RegStep As String

        'SAMPLE only
        Public Cmt As String
        Public DeltaMark As String
        Public PanicMark As String
        Public CriticalMark As String
        Public AlertMark As String

        '����
        Public Graph As String
        Public FrNo As String
        Public FrNm As String
        Public Rst1 As String
        Public Rst2 As String
        Public HL As String
        Public Refrmk As String
        Public RstUnit As String
        Public RstGbn As String
    End Class

    Public Class STU_RstInfo_calc
        Public CalForm As String = ""
        Public CalItems As String = ""
        Public CTestCd As String = ""
        Public TestCd As String = ""
        Public TNmD As String = ""
        Public OrgRst As String = ""
        Public RstFlg As String = ""
        Public BcNo As String = ""

        Public CalDsys As String = ""   '-- 2009/03/27 YEJ Add
        Public CalRange As String = ""  '--2010/03/16 yjlee Add
    End Class

    Public Class STU_RstInfo_cvt
        Public TestCd As String = ""
        Public SpcCd As String = ""
        Public RstCdSeq As String = ""
        Public CvtFldGbn As String = ""
        Public CvtRange As String = ""
        Public CvtForm As String = ""
        Public CvtParam As String = ""
        Public CTestCd As String = ""
        Public TnmD As String = ""
        Public RstFlg As String = ""
        Public BcNo As String = ""
        Public CondiExp As String = ""
        Public OrgRst As String = ""
        Public ViewRst As String = ""
        Public RstCmt As String = ""
        Public HlMark As String = ""
        Public RstCont As String = ""
    End Class

    Public Class STU_CvtCmtInfo
        Public CmtCd As String = ""
        Public CvtForm As String = ""
        Public CvtParam As String = ""
        Public TestCd As String = ""
        Public TNmD As String = ""
        Public RstFlg As String = ""
        Public BcNo As String = ""
        Public CondiExp As String = ""
        Public OrgRst As String = ""
        Public ViewRst As String = ""
        Public EqFlag As String = ""
        Public HlMark As String = ""
        Public CmtCont As String = ""
        Public SlipCd As String = ""
        Public CmtCont_Base As String = ""
    End Class
#End Region

#Region " ���� ����ü ���� : TAT ��ȸ"
    '< add yjlee 2009-03-27
    Public Class STU_TATCmtInfo
        Public bcno As String = ""
        Public tclscd As String = ""
        Public spccd As String = ""
        Public cmtcont As String = ""
        Public cmtcd As String = ""
        Public regid As String = ""
    End Class
    '> add yjlee 2009-03-27
#End Region

    Public Class STU_TnsJubsu
        Public REGNO As String = ""         ' ��Ϲ�ȣ
        Public PATNM As String = ""         ' ȯ�ڸ�
        Public SEX As String = ""           ' ����
        Public AGE As String = ""           ' ����
        Public ORDDATE As String = ""       ' ó������
        Public DEPTCD As String = ""        ' �����
        Public DRCD As String = ""          ' ������
        Public WARDCD As String = ""        ' ����
        Public ROOMNO As String = ""        ' ����
        Public COMCD As String = ""         ' ���������ڵ�
        Public COMNM As String = ""         ' ���������ڵ�
        Public COMORDCD As String = ""      ' ��ó���ڵ�
        Public SPCCD As String = ""         ' ��ü�ڵ�
        Public OWNGBN As String = ""        ' ó���������
        Public TNSJUBSUNO As String = ""    ' �����Ƿ�������ȣ
        Public FKOCS As String = ""         ' �ܷ�ó��Ű
        Public SEQ As String = ""           ' ����
        Public BLDNO As String = ""         ' ���׹�ȣ
        Public IOGBN As String = ""         ' �Կܱ���
        Public BCNO As String = ""          ' ��ü��ȣ
        Public STATE As String = ""         ' ����
        Public FILTER As String = ""        ' ����
        Public WORKID As String = ""        ' 
        Public RST1 As String = ""          ' ũ�ν����4
        Public RST2 As String = ""          ' ũ�ν����4
        Public RST3 As String = ""          ' ũ�ν����4
        Public RST4 As String = ""          ' ũ�ν����4
        Public CMRMK As String = ""         ' ����ũ
        Public TESTGBN As String = ""       ' �˻籸��
        Public TESTID As String = ""        ' �˻���
        Public BEFOUTID As String = ""      ' �������̵�
        Public OUTID As String = ""         ' ����ھ��̵�
        Public RECID As String = ""         ' �����ھ��̵�
        Public RECNM As String = ""         ' �����ڸ�
        Public RTNREQID As String = ""      ' �ݳ�/��� �Ƿ���
        Public RTNREQNM As String = ""      ' �ݳ�/��� �Ƿ��ڸ�
        Public RTNRSNCD As String = ""      ' �ݳ������ڵ�
        Public RTNRSNCMT As String = ""     ' �ݳ�����
        Public EMER As String = ""          ' ����
        Public IR As String = ""            ' �̶��̼�
        Public COMCD_OUT As String = ""     ' ���� ��������
        Public EDITIP As String = ""        ' ������IP
        Public TEMP01 As String = ""        ' ����1
        Public TEMP02 As String = ""        ' ����2
        Public TEMP03 As String = ""        ' ����3

        Public ABO As String = ""           ' ABO ������
        Public RH As String = ""            ' Rh ������

        Public RTNDT As String = ""   ' �ݳ�/����Ͻ�
    End Class

    Public Class STU_TNSCHG
        Public REGNO As String = ""             ' ��Ϲ�ȣ
        Public CRETNO As String = ""            ' ���� ������ȣ
        Public ADMDATE As String = ""           ' ��������
        Public MEDAMTESTMYN As String = ""      ' �������������
        Public IOFLAG As String = ""            ' �ܷ�/�Կ� ����
        Public ORDDATE As String = ""           ' ó������
        Public ORDNO As String = ""             ' ó���ȣ
        Public ORDHISTNO As String = ""         ' ó���ȣ his
        Public ORDCD_CHG As String = ""         ' ���� ó���ڵ�
        Public SPCCD_CHG As String = ""         ' ���� ��ü�ڵ�
        Public SUGACD_CHG As String = ""        ' ���� �����ڵ�
        Public ORDSTATCD As String = ""         ' ó������ڵ�
        Public BLDNO_CHG As String = ""         ' ���� ���׹�ȣ
        Public DEPTCD_USR As String = ""        ' �μ��ڵ�
        Public DEPTNM_USR As String = ""        ' �μ���
        Public TNSNO As String = ""             ' �������� ��ȣ
        Public EXECPRCPUNIQNO As String = ""
    End Class

    '/// �˻��Ƿ���ħ ���ΰ˻�
    Public Class TESTINFO_DTEST
        Public TESTCD As String = ""
        Public SPCCD As String = ""
        Public TNMD As String = ""
        Public SEQ As String = ""
    End Class

End Namespace
