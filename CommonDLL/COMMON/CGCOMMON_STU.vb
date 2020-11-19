'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON00.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 공통 구조체 선언                                                       */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Namespace SVar
#Region " 공통 구조체 선언 : 채혈관련 정보"

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
        Public REGNO As String = ""     ' 등록번호
        Public ORDDT As String = ""     ' 
        Public FKOCS As String = ""     '
        Public IOGBN As String = ""
        Public IOFLAG As String = ""

        Public BCCLSCD As String = ""   '
        Public STATGBN As String = ""   '
        Public TCDGBN As String = ""    '
        Public SPCCD As String = ""     ' 검체코드
        Public TCLSCD As String = ""    ' 검사코드

        Public SERIES As Boolean = False    ' 연속검사 샘플 판별용

        Public HEIGHT As String = ""    ' 키
        Public WEIGHT As String = ""    ' 체중

        Public DIAGCD As String = ""
        Public DIAGNM As String = ""
        Public DIAGNM_ENG As String = ""

        '-- 
        Public BCNO As String = ""
        Public COLLDT As String = ""
        Public SPCFLG As String = ""

        Public ERPRTYN As String = "" '<<<20180802 응급 프린트 여부 

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
        Public REGNO As String = ""     ' 등록번호
        Public TCLSCD As String = ""    ' 검사코드
        Public SPCCD As String = ""     ' 검체코드
        Public PATNM As String = ""     ' 성명
        Public SEX As String = ""       ' 성별
        Public AGE As String = ""       ' 나이
        Public DAGE As String = ""      ' 일 환산 나이
        Public BIRTHDAY As String = ""  ' 생일
        Public IDNOL As String = ""     ' 주민등록번호_왼쪽
        Public IDNOR As String = ""     ' 주민등록번호_오른쪽
        Public TEL1 As String = ""      ' 연락처1
        Public TEL2 As String = ""      ' 연락처2
        Public DOCTORCD As String = ""  ' 의뢰의사코드
        Public DOCTORNM As String = ""  ' 의뢰의사코드
        Public GENDRCD As String = ""   ' 주치의코드
        Public DEPTCD As String = ""    ' 과
        Public DEPTNM As String = ""    ' 진료과명
        Public DEPTABBR As String = ""    ' 진료과명
        Public WARDNO As String = ""    ' 병동코드
        Public WARDNM As String = ""    ' 병동이름
        Public WARDABBR As String = ""    ' 병동이름
        Public ROOMNO As String = ""    ' 병실번호
        Public BEDNO As String = ""     ' 침상번호
        Public ENTDT As String = ""     ' 입원일자
        Public STATGBN As String = ""   ' 응급구분
        Public OPDT As String = ""      ' 수술예정일
        Public REMARK As String = ""    ' 의뢰의사 REMARK
        Public REMARK_NRS As String = "" ' 의뢰의사 REMARK2
        Public IOGBN As String = ""     ' 입원/외래 구분
        Public FKOCS As String = ""     ' OCSKEY
        Public BCPRTDT As String = ""   ' 바코드출력일시
        Public ORDDT As String = ""     ' 처방일시
        Public RESDT As String = ""     ' 예약일시
        Public JUBSUGBN As String = ""  ' 접수구분
        Public SUGACD As String = ""    ' 수가코드

        Public LISCMT As String = ""    '-- 거래처정보

        '< yjlee 2009-01-05 부천순천향병원 
        Public TORDCD As String = ""    ' 처방코드
        Public SUNABYN As String = ""
        '> 

        Public COLLVOL As String = ""   ' 채혈량
        Public COLLID As String = ""    ' 채혈자
        Public COLLDT As String = ""    ' 채혈일시

        Public HEIGHT As String = ""    ' 키
        Public WEIGHT As String = ""    ' 체중
        Public OWNGBN As String = ""    ' OCS처방 or LIS처방
        Public COMMENT As String = ""   ' 전달 COMMENT

        Public BCCLSCD As String = ""   ' 검사분류
        Public EXLABCD As String = ""   ' 위탁기관코드
        Public EXLABYN As String = ""   ' 외주여부
        Public POCTYN As String = ""    ' 현장검사여부
        Public BCONEYN As String = ""
        Public TUBECD As String = ""    ' 튜브코드
        Public NRS_TIME As String = ""  ' 간호사 확인시간
        Public ORDSLIP As String = ""   ' 처방슬립(오더 테이블에 있는 데이타)
        Public PARTGBN As String = ""   ' L:진다, R:핵의학, P:병리

        '연속검사 샘플 판별용
        Public SEQTMI As Integer = 0
        Public BCKEY As String = ""
        Public BCKEY2 As String = ""
        Public BCKEY3 As String = ""
        Public SERIES As Boolean = False

        '바코드 출력용
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

        Public ERPRTYN As String = "" '<<<20180801 응급바코드 추가 
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
        Public IDNO As String = ""  ' 암호화된 주민번호( 030101-1****** )
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
        '양방등록번호
        Public WHOSPID As String = ""
        '감염정보
        Public INFINFO As String = ""
        Public INFINFOP As String = ""
        Public IsInfected As Boolean = False
        Public SPCOMMENT As String = ""     '진상내용 
        Public ABORh As String = ""

        Public INJONG As String = ""        '인종 
        Public GUBUN As String = ""         '환자유형
        Public SOGAE As String = ""         '직원관계
        Public VIP As String = ""           'VIP관계 

        Public PathologyYN As String = ""   ' 병리오더 여부

        Public DiagLeukemia As Boolean = False '백혈병 진단명 Y/N

    End Class

    Public Class STU_TestItemInfo
        Public REGNo As String = ""         ' 등록번호

        Public SPCFLG As String = ""       ' 검체상태
        Public RSTFLG As String = ""       ' 결과상태
        Public ORDDT As String = ""         ' 처방일시
        Public DEPTNM As String = ""        ' 과명
        Public DOCTORNM As String = ""      ' 의뢰의사명 
        Public TNMD As String = ""          ' 검사명
        Public SPCNMD As String = ""        ' 검체명
        Public STATGBN As String = ""       ' 응급구분
        Public APPEND_YN As String = ""     ' 추가여부
        Public REMARK As String = ""        ' 의뢰의사 Remark
        Public HOPEDT As String = ""        ' 검사희망일시
        Public COMMENT As String = ""       ' LAB COMMENT
        Public CWARNING As String = ""      ' 채혈시주의사항
        Public RESDT As String = ""         ' 진료예약일시

        Public DEPTCD As String = ""        ' 과코드
        Public DOCTORCD As String = ""      ' 의뢰의사코드
        Public ORDCD As String = ""         ' 처방코드
        Public TESTCD As String = ""        ' 검사코드
        Public SPCCD As String = ""         ' 검체코드
        Public BCCLSCD As String = ""       ' 검체분류
        Public MINSPCVOL As String = ""     ' 최소 채혈량
        Public SUGACD As String = ""        ' 수가코드
        Public EXLABCD As String = ""       ' 위탁기관코드
        Public EXLABYN As String = ""       ' 위타검사유무
        Public EXEDAY As String = ""        ' 검사요일
        Public SEQTYN As String = ""        ' 연속검사 유/무
        Public SEQTMI As String = ""        ' 연속검사시간
        Public HEIGHT As String = ""        ' 키
        Public WEIGHT As String = ""        ' 체중
        Public TUBECD As String = ""        ' 검체용기코드
        Public SPCNMBP As String = ""       ' 검체명 바코드 출력
        Public TUBENMBP As String = ""      ' 검체용기명 바코드 출력
        Public TNMBP As String = ""         ' 검사명 바코드 출력
        Public OWNGBN As String = ""        ' OCS처방 or LIS처방
        Public FKOCS As String = ""         ' OCSKEY
        Public BCKEY As String = ""         ' BCKEY
        Public INPUT_PART As String = ""    ' ORDPART
        Public BCCNT As String = "1"        ' 출력할 바코드 수
        Public DCFLAG As String = ""        ' DCFLAG
        Public BCNO As String = ""          ' 검체번호
        Public TCLS_SPC As String           ' 검체코드

        Public INSUGBN As String = ""       ' 보험구분
        Public IOGBN As String = ""         ' 외래/입원 구분
        Public ORDDT_APPEND As String = ""  ' 추가처방일시
        'Public PLGBN As String = ""         ' 더블테스트 복수구분
        Public DBLTSEQ As String = ""       ' 다른검체 복수구분으로 처리
        Public PARTCD As String = ""        ' 파트구분
        Public TCDGBN As String = ""        ' 검사코드구분
        Public ORDTCLSCD As String = ""     ' 처방항목코드
        Public WORKNO As String = ""        ' 작업번호
        Public INPUT_PARTNM As String = ""  '
        Public NRS_CFM_YN As String = ""    ' 간호확인
        Public NRS_TIME As String = ""      ' 간호확인 시간

        Public REQ_REMARK As String = ""

        Public VIRUS_YN As String = ""      ' 감염여부

        Public ROOMNO As String = ""    '병실
        Public WARDCD As String = ""  '병동
        Public ENTDT As String = ""  '입원일

    End Class

#End Region

#Region "바코드, 혈액라벨 정의"
    Public Class STU_BCPRTINFO
        Public BCNOPRT As String = ""       '-- 출력용 바코드번호
        Public BCNO As String = ""          '-- 바코드 FULL 번호
        Public REGNO As String = ""         '-- 등록번호
        Public PATNM As String = ""         '-- 환자명
        Public SEXAGE As String = ""        '-- 성별/나이
        Public BCCLSCD As String = ""       '-- 검체구분
        Public DEPTWARD As String = ""      '-- 진료과/병동
        Public IOGBN As String = ""         '-- 입외구분
        Public SPCNM As String = ""         '-- 검체명
        Public TUBENM As String = ""        '-- Tube name
        Public TESTNMS As String = ""       '-- 검사명
        Public EMER As String = ""          '-- 응급여부(Y)
        Public INFINFO As String = ""       '-- 감염정보
        Public TGRPNM As String = ""        '-- 검사그룹
        Public XMATCH As String = ""        '-- Cross Matching 여부(A)
        Public REMARK As String = ""        '-- 의사 Remark
        Public BCCNT As String = ""         '-- 출력매수
        Public BCTYPE As String = ""        '-- 출력양식
        Public HREGNO As String = ""        '-- 
        Public BCNO_MB As String = ""       '-- 미생물인 경우
        Public ERPRTYN As String = ""       '-- 응급프린트 <<<20180802
        Public ABOCHK As String = ""        '-- 혈액형 여부 체크 2019-04-19
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

        Public IDNO As String = ""          '-- 주민번호
        Public XMATCH1 As String = ""       '-- CrossMatching 1차 결과
        Public XMATCH2 As String = ""       '-- CrossMatching 2차 결과
        Public XMATCH3 As String = ""       '-- CrossMatching 3차 결과
        Public XMATCH4 As String = ""       '-- CrossMatching 4차 결과
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

    '-- 보관검체
    Public Class STU_KsRack    ' 보관검체 관리정보 
        Public Bcclscd As String = ""
        Public RackId As String = ""
        Public SpcCd As String = ""
        Public Bcno As String = ""
        Public RegDt As String = ""
        Public RegId As String = ""
        Public NumCol As String = ""
        Public NumRow As String = ""
        Public AlarmTerm As String = ""
        Public Other As String = ""       ' 보관 Comment

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
        Public PARTGBN As String = ""       '-- L(진단)/R(핵의학)
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
        Public mTESTCD As String        ' 검사코드
        Public mTNM As String           ' 검사명
        Public mTCDGBN As String        ' 검사구분
        Public mSPCCD As String         ' 검체코드
        Public mTNMP As String          ' 출력검사명
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

#Region " 공통 구조체 선언 : 결과입력"

    Public Class STU_RstInfo
        '공통
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
        Public BfRst As String '-JJH 이전결과
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

        '공통
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

#Region " 공통 구조체 선언 : TAT 조회"
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
        Public REGNO As String = ""         ' 등록번호
        Public PATNM As String = ""         ' 환자명
        Public SEX As String = ""           ' 성별
        Public AGE As String = ""           ' 나이
        Public ORDDATE As String = ""       ' 처방일자
        Public DEPTCD As String = ""        ' 진료과
        Public DRCD As String = ""          ' 진료의
        Public WARDCD As String = ""        ' 병동
        Public ROOMNO As String = ""        ' 병실
        Public COMCD As String = ""         ' 성분제제코드
        Public COMNM As String = ""         ' 성분제제코드
        Public COMORDCD As String = ""      ' 원처방코드
        Public SPCCD As String = ""         ' 검체코드
        Public OWNGBN As String = ""        ' 처방소유구분
        Public TNSJUBSUNO As String = ""    ' 수혈의뢰접수번호
        Public FKOCS As String = ""         ' 외래처방키
        Public SEQ As String = ""           ' 순번
        Public BLDNO As String = ""         ' 혈액번호
        Public IOGBN As String = ""         ' 입외구분
        Public BCNO As String = ""          ' 검체번호
        Public STATE As String = ""         ' 상태
        Public FILTER As String = ""        ' 필터
        Public WORKID As String = ""        ' 
        Public RST1 As String = ""          ' 크로스결과4
        Public RST2 As String = ""          ' 크로스결과4
        Public RST3 As String = ""          ' 크로스결과4
        Public RST4 As String = ""          ' 크로스결과4
        Public CMRMK As String = ""         ' 리마크
        Public TESTGBN As String = ""       ' 검사구분
        Public TESTID As String = ""        ' 검사자
        Public BEFOUTID As String = ""      ' 가출고아이디
        Public OUTID As String = ""         ' 출고자아이디
        Public RECID As String = ""         ' 수령자아이디
        Public RECNM As String = ""         ' 수령자명
        Public RTNREQID As String = ""      ' 반납/폐기 의뢰자
        Public RTNREQNM As String = ""      ' 반납/폐기 의뢰자명
        Public RTNRSNCD As String = ""      ' 반납사유코드
        Public RTNRSNCMT As String = ""     ' 반납사유
        Public EMER As String = ""          ' 응급
        Public IR As String = ""            ' 이라데이션
        Public COMCD_OUT As String = ""     ' 출고용 성분제제
        Public EDITIP As String = ""        ' 수정자IP
        Public TEMP01 As String = ""        ' 여유1
        Public TEMP02 As String = ""        ' 여유2
        Public TEMP03 As String = ""        ' 여유3

        Public ABO As String = ""           ' ABO 혈액형
        Public RH As String = ""            ' Rh 혈액형

        Public RTNDT As String = ""   ' 반납/폐기일시
    End Class

    Public Class STU_TNSCHG
        Public REGNO As String = ""             ' 등록번호
        Public CRETNO As String = ""            ' 내원 생성번호
        Public ADMDATE As String = ""           ' 내원일자
        Public MEDAMTESTMYN As String = ""      ' 진찰료산정여부
        Public IOFLAG As String = ""            ' 외래/입원 구분
        Public ORDDATE As String = ""           ' 처방일자
        Public ORDNO As String = ""             ' 처방번호
        Public ORDHISTNO As String = ""         ' 처방번호 his
        Public ORDCD_CHG As String = ""         ' 변경 처방코드
        Public SPCCD_CHG As String = ""         ' 변경 검체코드
        Public SUGACD_CHG As String = ""        ' 변경 수가코드
        Public ORDSTATCD As String = ""         ' 처방상태코드
        Public BLDNO_CHG As String = ""         ' 변경 혈액번호
        Public DEPTCD_USR As String = ""        ' 부서코드
        Public DEPTNM_USR As String = ""        ' 부서명
        Public TNSNO As String = ""             ' 혈액접수 번호
        Public EXECPRCPUNIQNO As String = ""
    End Class

    '/// 검사의뢰지침 세부검사
    Public Class TESTINFO_DTEST
        Public TESTCD As String = ""
        Public SPCCD As String = ""
        Public TNMD As String = ""
        Public SEQ As String = ""
    End Class

End Namespace
