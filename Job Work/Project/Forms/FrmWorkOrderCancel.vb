Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmWorkOrderCancel
    Inherits AgTemplate.TempTransaction
    Dim mQry$
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Dim FRH_Multiple() As DMHelpGrid.FrmHelpGrid_Multi

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Item_UID As String = "Item UID"
    Public Const Col1Item As String = "Item"
    Public Const Col1WorkOrder As String = "Work Order"
    Public Const Col1WorkOrderSr As String = "Work Order Sr"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1BillingType As String = "Billing Type"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1RatePerQty As String = "Rate Per Qty"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1TotalMeasure As String = "Total Measure"
    Public Const Col1MeasureUnit As String = "MeasureUnit"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1FromProcess As String = "From Process"
    Public Const Col1LotNo As String = "LotNo"
    Public Const Col1PartySKU As String = "Party SKU"
    Public Const Col1PartyUPC As String = "Party UPC"
    Public Const Col1PartySpecification As String = "Party Specification"
    Public Const Col1DeliveryMeasure As String = "Delivery Measure"
    Public Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Public Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Public Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Public Const Col1Remark As String = "Remark"

    Dim mLastKeyPressed As Keys
    Dim BlnIsMeasurePerPcsVisible As Boolean = False
    Dim BlnIsMeasurePerPcsEditable As Boolean = False
    Dim BlnIsMeasureVisible As Boolean = False
    Dim BlnIsMeasureEditable As Boolean = False
    Dim BlnIsMeasureUnitVisible As Boolean = False
    Dim BlnIsMeasureUnitEditable As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        'mItemType = Replace(ItemTypeStr, ",", "','")
        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

    'Public Sub FSetMeasureParameter(ByVal IsMeasurePerPcsVisible As Boolean, ByVal IsMeasurePerPcsEditable As Boolean, ByVal IsMeasureVisible As Boolean, ByVal IsMeasureEditable As Boolean, ByVal IsMeasureUnitVisible As Boolean, ByVal IsMeasureUnitEditable As Boolean)
    '    BlnIsMeasurePerPcsVisible = IsMeasurePerPcsVisible
    '    BlnIsMeasurePerPcsEditable = IsMeasurePerPcsEditable
    '    BlnIsMeasureVisible = IsMeasureVisible
    '    BlnIsMeasureEditable = IsMeasureEditable
    '    BlnIsMeasureUnitVisible = IsMeasureUnitVisible
    '    BlnIsMeasureUnitEditable = IsMeasureUnitEditable
    'End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWorkOrderCancel))
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtParty = New AgControls.AgTextBox
        Me.LblVendor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnShow = New System.Windows.Forms.Button
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtForOrder = New System.Windows.Forms.RadioButton
        Me.RbtCancelForWorkOrderItem = New System.Windows.Forms.RadioButton
        Me.TxtNature = New AgControls.AgTextBox
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox
        Me.BtnImprt = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrpDirectInvoice.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(832, 583)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 583)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(29, 19)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 583)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 583)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 583)
        Me.GrpUP.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 571)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 583)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(234, 201)
        Me.LblV_No.Size = New System.Drawing.Size(64, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Order No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(342, 200)
        Me.TxtV_No.Size = New System.Drawing.Size(231, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(313, 47)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(217, 42)
        Me.LblV_Date.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Order Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(528, 27)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(329, 41)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(435, 23)
        Me.LblV_Type.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Order Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(543, 21)
        Me.TxtV_Type.Size = New System.Drawing.Size(173, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(313, 27)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(217, 22)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(329, 21)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(294, 201)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(992, 130)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 104)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Dgl1
        '
        Me.Dgl1.AgAllowFind = True
        Me.Dgl1.AgLastColumn = -1
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.AgSkipReadOnlyColumns = False
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(313, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtParty
        '
        Me.TxtParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtParty.AgLastValueTag = Nothing
        Me.TxtParty.AgLastValueText = Nothing
        Me.TxtParty.AgMandatory = True
        Me.TxtParty.AgMasterHelp = False
        Me.TxtParty.AgNumberLeftPlaces = 8
        Me.TxtParty.AgNumberNegetiveAllow = False
        Me.TxtParty.AgNumberRightPlaces = 2
        Me.TxtParty.AgPickFromLastValue = False
        Me.TxtParty.AgRowFilter = ""
        Me.TxtParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParty.AgSelectedValue = Nothing
        Me.TxtParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(329, 61)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(363, 18)
        Me.TxtParty.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(217, 61)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(39, 16)
        Me.LblVendor.TabIndex = 693
        Me.LblVendor.Text = "Party"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(2, 391)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(979, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(649, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 3005
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(420, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(213, 22)
        Me.LblTotalDeliveryMeasureText.TabIndex = 3004
        Me.LblTotalDeliveryMeasureText.Text = "Deilvery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.LblTotalAmount.Location = New System.Drawing.Point(871, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(767, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.Color.Black
        Me.LblTotalQty.Location = New System.Drawing.Point(140, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(55, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(2, 175)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 216)
        Me.Pnl1.TabIndex = 3
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
        Me.TxtStructure.AgLastValueTag = Nothing
        Me.TxtStructure.AgLastValueText = Nothing
        Me.TxtStructure.AgMandatory = False
        Me.TxtStructure.AgMasterHelp = False
        Me.TxtStructure.AgNumberLeftPlaces = 8
        Me.TxtStructure.AgNumberNegetiveAllow = False
        Me.TxtStructure.AgNumberRightPlaces = 2
        Me.TxtStructure.AgPickFromLastValue = False
        Me.TxtStructure.AgRowFilter = ""
        Me.TxtStructure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStructure.AgSelectedValue = ""
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStructure.Location = New System.Drawing.Point(853, 126)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(104, 18)
        Me.TxtStructure.TabIndex = 23
        Me.TxtStructure.Tag = ""
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(754, 127)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'TxtSalesTaxGroupParty
        '
        Me.TxtSalesTaxGroupParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroupParty.AgLastValueText = Nothing
        Me.TxtSalesTaxGroupParty.AgMandatory = False
        Me.TxtSalesTaxGroupParty.AgMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgNumberLeftPlaces = 8
        Me.TxtSalesTaxGroupParty.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroupParty.AgNumberRightPlaces = 2
        Me.TxtSalesTaxGroupParty.AgPickFromLastValue = False
        Me.TxtSalesTaxGroupParty.AgRowFilter = ""
        Me.TxtSalesTaxGroupParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroupParty.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroupParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroupParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroupParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroupParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(625, 126)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(126, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 22
        Me.TxtSalesTaxGroupParty.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(515, 127)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 16)
        Me.Label27.TabIndex = 717
        Me.Label27.Text = "Sales Tax Group"
        Me.Label27.Visible = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemarks.AgLastValueTag = Nothing
        Me.TxtRemarks.AgLastValueText = Nothing
        Me.TxtRemarks.AgMandatory = False
        Me.TxtRemarks.AgMasterHelp = False
        Me.TxtRemarks.AgNumberLeftPlaces = 0
        Me.TxtRemarks.AgNumberNegetiveAllow = False
        Me.TxtRemarks.AgNumberRightPlaces = 0
        Me.TxtRemarks.AgPickFromLastValue = False
        Me.TxtRemarks.AgRowFilter = ""
        Me.TxtRemarks.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemarks.AgSelectedValue = Nothing
        Me.TxtRemarks.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemarks.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(4, 436)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(300, 138)
        Me.TxtRemarks.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(1, 418)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'LblJobReceiveDetail
        '
        Me.LblJobReceiveDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobReceiveDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobReceiveDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobReceiveDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(2, 155)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(152, 19)
        Me.LblJobReceiveDetail.TabIndex = 1003
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Work Order Cancel Detail"
        Me.LblJobReceiveDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlCalcGrid.Location = New System.Drawing.Point(653, 415)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(322, 155)
        Me.PnlCalcGrid.TabIndex = 5
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgLastValueTag = Nothing
        Me.TxtManualRefNo.AgLastValueText = Nothing
        Me.TxtManualRefNo.AgMandatory = False
        Me.TxtManualRefNo.AgMasterHelp = False
        Me.TxtManualRefNo.AgNumberLeftPlaces = 8
        Me.TxtManualRefNo.AgNumberNegetiveAllow = False
        Me.TxtManualRefNo.AgNumberRightPlaces = 2
        Me.TxtManualRefNo.AgPickFromLastValue = False
        Me.TxtManualRefNo.AgRowFilter = ""
        Me.TxtManualRefNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualRefNo.AgSelectedValue = Nothing
        Me.TxtManualRefNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualRefNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualRefNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualRefNo.Location = New System.Drawing.Point(543, 41)
        Me.TxtManualRefNo.MaxLength = 20
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(173, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(435, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 16)
        Me.Label3.TabIndex = 738
        Me.Label3.Text = "Manual Ref No"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(528, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 739
        Me.Label5.Text = "Ä"
        '
        'BtnShow
        '
        Me.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnShow.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnShow.Location = New System.Drawing.Point(503, 153)
        Me.BtnShow.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(24, 19)
        Me.BtnShow.TabIndex = 2
        Me.BtnShow.TabStop = False
        Me.BtnShow.Text = "..."
        Me.BtnShow.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnShow.UseVisualStyleBackColor = True
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtForOrder)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtCancelForWorkOrderItem)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(170, 147)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(324, 26)
        Me.GrpDirectInvoice.TabIndex = 3003
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtForOrder
        '
        Me.RbtForOrder.AutoSize = True
        Me.RbtForOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForOrder.Location = New System.Drawing.Point(4, 8)
        Me.RbtForOrder.Name = "RbtForOrder"
        Me.RbtForOrder.Size = New System.Drawing.Size(126, 17)
        Me.RbtForOrder.TabIndex = 0
        Me.RbtForOrder.TabStop = True
        Me.RbtForOrder.Text = "For Work Order"
        Me.RbtForOrder.UseVisualStyleBackColor = True
        '
        'RbtCancelForWorkOrderItem
        '
        Me.RbtCancelForWorkOrderItem.AutoSize = True
        Me.RbtCancelForWorkOrderItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtCancelForWorkOrderItem.Location = New System.Drawing.Point(145, 7)
        Me.RbtCancelForWorkOrderItem.Name = "RbtCancelForWorkOrderItem"
        Me.RbtCancelForWorkOrderItem.Size = New System.Drawing.Size(168, 17)
        Me.RbtCancelForWorkOrderItem.TabIndex = 743
        Me.RbtCancelForWorkOrderItem.TabStop = True
        Me.RbtCancelForWorkOrderItem.Text = "For Work Order Items"
        Me.RbtCancelForWorkOrderItem.UseVisualStyleBackColor = True
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = True
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 8
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 2
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(817, 80)
        Me.TxtNature.MaxLength = 0
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(68, 18)
        Me.TxtNature.TabIndex = 3002
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(692, 61)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(24, 18)
        Me.BtnFillPartyDetail.TabIndex = 3001
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.Text = "..."
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprt)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(556, 516)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(90, 55)
        Me.GBoxImportFromExcel.TabIndex = 3004
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        '
        'BtnImprt
        '
        Me.BtnImprt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprt.Image = CType(resources.GetObject("BtnImprt.Image"), System.Drawing.Image)
        Me.BtnImprt.Location = New System.Drawing.Point(53, 14)
        Me.BtnImprt.Name = "BtnImprt"
        Me.BtnImprt.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprt.TabIndex = 670
        Me.BtnImprt.TabStop = False
        Me.BtnImprt.UseVisualStyleBackColor = True
        '
        'FrmWorkOrderCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 627)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.BtnShow)
        Me.EntryNCat = "SO"
        Me.MainLineTableCsv = "WorkOrderDetail"
        Me.MainTableName = "WorkOrder"
        Me.Name = "FrmWorkOrderCancel"
        Me.Text = "Template Workase Order"
        Me.Controls.SetChildIndex(Me.BtnShow, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GrpDirectInvoice.ResumeLayout(False)
        Me.GrpDirectInvoice.PerformLayout()
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblVendor As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents BtnShow As System.Windows.Forms.Button
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForOrder As System.Windows.Forms.RadioButton
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Protected WithEvents RbtCancelForWorkOrderItem As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Protected WithEvents BtnImprt As System.Windows.Forms.Button
#End Region

    Private Sub FrmWorkOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And IfNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mCondStr = mCondStr & " And H.V_Type NOT IN " &
                    " ( Select L.V_Type " &
                    " FROM User_Exclude_VTypeDetail L  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = L.V_Type  " &
                    " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " And Vt.NCat in ('" & EntryNCat & "') ) "

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type,   H.V_Date AS [Date], H.V_No , H.ReferenceNo As  [Manual_No], " &
                    " H.PartyName , H.PartyAdd1 ,  H.PartyAdd2 , H.PartyCityName ,   H.PartyState , H.PartyCountry ,   " &
                    " H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type],  H.EntryStatus AS [Entry_Status], H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date],  " &
                    " H.Status, D.Div_Name AS [Division], SM.Name AS [Site_Name] " &
                    " FROM WorkOrder  H " &
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                    " LEFT JOIN SubGroup SGA ON SGA.SubCode  = H.Agent  " &
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Date]"
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        LogTableName = "WorkOrder_Log"
        LogLineTableCsv = "WorkOrderDetail_Log"
        MainTableName = "WorkOrder"
        MainLineTableCsv = "WorkOrderDetail"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mCondStr = mCondStr & " And H.V_Type NOT IN " &
            " ( Select L.V_Type " &
            " FROM User_Exclude_VTypeDetail L  " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = L.V_Type  " &
            " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " And Vt.NCat in ('" & EntryNCat & "') ) "

        mQry = "Select DocID As SearchCode " &
                " From WorkOrder H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0)=0  " & mCondStr & "  Order By H.V_Date, H.V_No  "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)

    End Sub

    Private Sub FrmWorkOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1WorkOrder, 80, 0, Col1WorkOrder, True, True, False)
            .AddAgTextColumn(Dgl1, Col1WorkOrderSr, 70, 0, Col1WorkOrderSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 0, Col1BillingType, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Qty")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, BlnIsMeasurePerPcsVisible, Not BlnIsMeasurePerPcsEditable, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 100, 50, Col1MeasureUnit, BlnIsMeasureUnitVisible, Not BlnIsMeasureUnitEditable, False)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 90, 0, Col1FromProcess, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1PartySKU, 110, 50, Col1PartySKU, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartySKU")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartyUPC, 110, 20, Col1PartyUPC, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartyUPC")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartySpecification, 110, 20, Col1PartySpecification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartySpecification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 85, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RatePerQty, 100, 8, 2, False, Col1RatePerQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 100, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.ColumnHeadersHeight = 35
        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgFixedRows = 6

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        Dim FrmObj As New FrmPartyDetail
        FrmObj = BtnFillPartyDetail.Tag

        mQry = " UPDATE WorkOrder " &
                "   SET " &
                "   ReferenceNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                "   ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                "   Party = " & AgL.Chk_Text(TxtParty.Tag) & ", " &
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " &
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                "	Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                "   PartyName = '" & BtnFillPartyDetail.Tag.TxtPartyName.Text & "', " &
                "   PartyAdd1 = '" & BtnFillPartyDetail.Tag.TxtPartyAdd1.Text & "', " &
                "   PartyAdd2 = '" & BtnFillPartyDetail.Tag.TxtPartyAdd2.Text & "', " &
                "   PartyCity = '" & BtnFillPartyDetail.Tag.TxtPartyCity.Tag & "', " &
                "   PartyCityName = '" & BtnFillPartyDetail.Tag.TxtPartyCity.Text & "', " &
                "   PartyMobile = '" & BtnFillPartyDetail.Tag.TxtPartyMobile.Text & "', " &
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                            " " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1RatePerQty, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1WorkOrderSr, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "

                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE WorkOrderDetail " &
                                    " SET " &
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                    " FromProcess = " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " &
                                    " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                    " T_Nature = " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                    " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                    " Qty = " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " RatePerQty = " & Val(Dgl1.Item(Col1RatePerQty, I).Value) & ", " &
                                    " PartySKU = " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " &
                                    " PartyUPC = " & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " &
                                    " PartySpecification = " & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & ", " &
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " TotalMeasure = " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                    " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                    " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                    " TotalDeliveryMeasure = " & -Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                    " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " Amount = " & -Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                    " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From WorkOrderDeliveryDetail Where DocId = '" & mSearchCode & "' And TSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " Delete From WorkOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "INSERT INTO WorkOrderDetail (DocId, Sr, Item, Dimension1, Dimension2, FromProcess, LotNo, T_Nature, PartySKU, PartyUPC, PartySpecification, " &
                    " Specification, SalesTaxGroupItem, RatePerQty, Qty, " &
                    " Unit, Rate, Amount, WorkOrder, WorkOrderSr, " &
                    " MeasurePerPcs, TotalMeasure, DeliveryMeasureMultiplier, TotalDeliveryMeasure, DeliveryMeasure, MeasureUnit, " &
                    " BillingType, Remark, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
        End If

    End Sub

    Private Sub FrmWorkOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        mQry = "Select H.* " &
                " From WorkOrder H " &
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag

                IniGrid()

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("Party"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))

                Dim FrmObj As New FrmPartyDetail
                FrmObj.TxtPartyName.Text = AgL.XNull(.Rows(0)("PartyName"))
                FrmObj.TxtPartyAdd1.Text = AgL.XNull(.Rows(0)("PartyAdd1"))
                FrmObj.TxtPartyAdd2.Text = AgL.XNull(.Rows(0)("PartyAdd2"))
                FrmObj.TxtPartyCity.Tag = AgL.XNull(.Rows(0)("PartyCity"))
                FrmObj.TxtPartyCity.Text = AgL.XNull(.Rows(0)("PartyCityName"))
                BtnFillPartyDetail.Tag = FrmObj
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                'LblTotalQty.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalQty")))
                'LblTotalAmount.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalAmount")))
                'LblTotalMeasure.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalMeasure")))


                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, PO.V_Type +'-'+ Po.ManualRefNo As WorkOrderRefNo, I.Description, P.Description AS FromProcessDesc, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DU.DecimalPlaces as DeliveryMeasureDecimalPlaces, " &
                        " D1.Description As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", D2.Description As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
                        " from WorkOrderDetail L " &
                        " LEFT JOIN WorkOrder Po On L.WorkOrder = Po.DocId " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " Left Join Unit U On I.Unit = U.Code " &
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " &
                        " Left Join Unit DU On L.DeliveryMeasure = DU.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " LEFT JOIN Process P On L.FromProcess = P.NCat " &
                        " Where L.DocId = '" & SearchCode & "' Order By Sr"

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension1Caption()))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension2Caption()))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("FromProcess"))
                            Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("FromProcessDesc"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1Amount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1PartySKU, I).Value = AgL.XNull(.Rows(I)("PartySKU"))
                            Dgl1.Item(Col1PartyUPC, I).Value = AgL.XNull(.Rows(I)("PartyUPC"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1PartySpecification, I).Value = AgL.XNull(.Rows(I)("PartySpecification"))
                            Dgl1.Item(Col1WorkOrder, I).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                            Dgl1.Item(Col1WorkOrder, I).Value = AgL.XNull(.Rows(I)("WorkOrderRefNo"))
                            Dgl1.Item(Col1WorkOrderSr, I).Value = AgL.XNull(.Rows(I)("WorkOrderSr"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RatePerQty, I).Value = AgL.VNull(.Rows(I)("RatePerQty"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With


                Calculation()
                '-------------------------------------------------------------
            End If
        End With

    End Sub

    Private Sub FrmWorkOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating
        Dim I As Integer = 0
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtV_Type.Focus()
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtParty.Focus()
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer, ByVal bFillArea As Boolean)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Specification, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = ""
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Rate, mRow).Value = ""
                Dgl1.Item(Col1Amount, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
                Dgl1.Item(Col1BillingType, mRow).Value = ""
                Dgl1.Item(Col1WorkOrder, mRow).Tag = ""
                Dgl1.Item(Col1WorkOrder, mRow).Value = ""
                Dgl1.Item(Col1WorkOrderSr, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)

                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DeliveryMeasure").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1TotalDeliveryMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("TotalDeliveryMeasure").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DeliveryMeasureMultiplier").Value)
                    Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillingType").Value)
                    Dgl1.Item(Col1WorkOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrder").Value)
                    Dgl1.Item(Col1WorkOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderNo").Value)
                    Dgl1.Item(Col1WorkOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderSr").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxGroupItem").Value)
                End If
            End If
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex, True)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        'Dim I As Integer

        'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        'LblTotalQty.Text = 0
        'LblTotalMeasure.Text = 0
        'LblTotalAmount.Text = 0

        'For I = 0 To Dgl1.RowCount - 1
        '    If Dgl1.Item(Col1Item, I).Value <> "" Then
        '        If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) > 0 Then
        '            Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.0000")
        '        End If
        '        If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
        '            Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
        '        ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Area") Or AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
        '            Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
        '        End If
        '        LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
        '        LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
        '        LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
        '    End If
        'Next

        'AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        'AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        'AgCalcGrid1.AgVoucherCategory = "Work"
        'AgCalcGrid1.Calculation()


        'LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.00")
        'LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.0000")
        'LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")

        Dim I As Integer

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        LblTotalQty.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                'New Calculation

                'In Case of Carpet Calculation
                'User Will feed Qty first
                'THen TotalMeasure is calculated on hte basis Of Measure Per Pcs
                'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                If AgL.VNull(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                    If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Else
                        Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)
                    End If
                End If

                'Delivery measure calculation
                'By Default Deliver Measure Unit is Equal To Qty Unit
                'Now if user changes the Deliver Measure
                'Then the Delivery Measure Multiplier will auto come from Unit Conversio Table
                'Delivery measure will be automatically calculated on the basis of delivery measure multiplier.

                'For General Purpose Calculation
                'User will feed Delivery Measure And Qty will calculate automatically on the basis of Delivery Measure Multiplier
                'If the Deivery Measure Multiplier is 0 Or Unit Conversion factor does not exist in Unit Conversion
                'Table the user will feed qty manually and Qty will not calculated automatically
                If AgL.VNull(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then

                End If

                If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then Dgl1.Item(Col1RatePerQty, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) / Val(Dgl1.Item(Col1Qty, I).Value)
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End New Calculation


                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)


                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")


        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"

    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1WorkOrder).Index & "," & Dgl1.Columns(Col1WorkOrderSr).Index & "") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    mQry = " SELECT Round(IfNull(L.Qty,0) - IfNull(Dispatch.Qty,0),3) AS BalQty " &
                            " FROM WorkOrderDetail L  " &
                            " Left Join " &
                            " 	( SELECT C.WorkOrderSr , C.WorkOrder, Sum(C.Qty) AS Qty " &
                            " 	 FROM WorkDispatchDetail C " &
                            "    Where C.WorkOrder = '" & .AgSelectedValue(Col1WorkOrder, I) & "' And C.WorkOrderSr = '" & Val(.Item(Col1WorkOrderSr, I).Value) & "'   " &
                            " 	 GROUP BY C.WorkOrder, C.WorkOrderSr) AS Dispatch " &
                            " ON L.DocId = Dispatch.WorkOrder AND L.Sr = Dispatch.WorkOrderSr " &
                            " Where L.DocID <> '" & mSearchCode & "'  And L.WorkOrder = '" & .AgSelectedValue(Col1WorkOrder, I) & "' " &
                            " And L.WorkOrderSr = '" & Val(.Item(Col1WorkOrderSr, I).Value) & "' "
                    If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(.Item(Col1Qty, I).Value) Then
                        MsgBox("Balance Qty Of """ & .Item(Col1Item, I).Value & """ In Work Order """ & .Item(Col1WorkOrder, I).Value & """ Is Less Then " & Val(.Item(Col1Qty, I).Value) & " At Row No " & .Item(ColSNo, I).Value & ".", MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub TempWorkOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        mLastKeyPressed = e.KeyCode
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 660, 992, 0, 0)
    End Sub


    Private Sub FrmWorkOrder_YarnSku_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT  H.DocID, Convert(NVARCHAR(5),H.V_No) AS VoucherNo, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code,  " &
                 " H.Party, H.PartyName, H.PartyAdd1, H.PartyAdd2, H.PartyCity, H.PartyCityName, H.PartyState,  " &
                 " H.SalesTaxGroupParty,SG.Mobile,SG.EMail, CASE WHEN H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' THEN '' ELSE H.Status END AS Status , " &
                 " H.Currency , H.TermsAndConditions, H.Remarks,  " &
                 " H.EntryBy, H.EntryDate,H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.UID,  L.SR, " &
                 " H.IsDeleted, L.Item, Abs(L.Qty) As Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " &
                 " L.TotalMeasure AS LineTotalMeasure, L.Rate, Abs(L.Amount) As Amount , I.Description AS ItemDesc, SM.Name AS SiteName , L.Specification ," &
                 " IfNull(V1.ChallanQty,0) AS ChallanQty, Sg.TinNO As WorkToPartyTinNo, H.V_Type + ' - ' + So.ManualRefNo As WorkOrderManualNo, H.ReferenceNo, " &
                 " E.Caption_Dimension1, E.Caption_Dimension2, " &
                 " D1.Description As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", D2.Description As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                 " " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & " " &
                 " FROM WorkOrder H " &
                 " LEFT JOIN WorkOrderDetail L On H.DocId = L.DocId  " &
                 " LEFT JOIN Item I ON I.Code=L.Item    " &
                 " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                 " LEFT JOIN Voucher_Type Vt ON Vt.V_Type= H.V_Type   " &
                 " LEFT JOIN SubGroup SG ON SG.SubCode= H.Party " &
                 " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                 " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                 " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                 " LEFT JOIN WorkOrder So ON L.WorkOrder = So.DocId " &
                 " Left Join ( " &
                 "       SELECT PC.WorkOrder,PC.Item,sum(PC.Qty) AS ChallanQty    " &
                 "       FROM WorkDispatchDetail PC   " &
                 "       WHERE PC.WorkOrder Is Not NULL " &
                 "       GROUP BY PC.WorkOrder,PC.Item    " &
                 " )V1 ON V1.WorkOrder=L.DocId AND V1.Item=L.Item " &
                 " WHERE H.DocID='" & mInternalCode & "'"
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Work_WorkOrderCancel_Print", "Work Order Cancel")
    End Sub

    Private Sub TxtVendor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown
        Select Case sender.name
            Case TxtParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If TxtParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Sg.SubCode As Code, Sg.DispName AS [Name], C.CityName AS [City], SG.Mobile, C.State, C.Country, C.CityCode, " &
                                " Sg.Add1, Sg.Add2, Sg.Add3, Sg.Currency, Sg.SalesTaxPostingGroup  " &
                                " FROM SubGroup Sg  " &
                                " LEFT JOIN City C ON Sg.CityCode = C.CityCode  "
                        TxtParty.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub TxtVendor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtParty.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.Name
                Case TxtParty.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                            Dim FrmObj As New FrmPartyDetail
                            If BtnFillPartyDetail.Tag Is Nothing Then
                                BtnFillPartyDetail.Tag = FrmObj
                            Else
                                FrmObj = BtnFillPartyDetail.Tag
                            End If
                            FrmObj.TxtPartyName.Text = AgL.XNull(DrTemp(0)("Name"))
                            FrmObj.TxtPartyAdd1.Text = AgL.XNull(DrTemp(0)("Add1"))
                            FrmObj.TxtPartyAdd2.Text = AgL.XNull(DrTemp(0)("Add2"))
                            FrmObj.TxtPartyCity.Tag = AgL.XNull(DrTemp(0)("CityCode"))
                            FrmObj.TxtPartyCity.Text = AgL.XNull(DrTemp(0)("City"))
                            FrmObj.TxtPartyMobile.Text = AgL.XNull(DrTemp(0)("Mobile"))
                            BtnFillPartyDetail.Tag = FrmObj
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmWorkInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
        End Try
        If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet.Dispose() : TxtParty.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String
            If RbtCancelForWorkOrderItem.Checked Then
                StrTicked = FHPGD_PendingWorkOrderItems()
            Else
                StrTicked = FHPGD_PendingWorkOrder()
            End If

            If StrTicked <> "" Then
                FFillItemsForPendingWorkOrders(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function FHPGD_PendingWorkOrderItems() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.WorkOrder + Convert(nVarChar, VMain.WorkOrderSr) As WorkOrderDocIdSr, " &
                " Max(VMain.WorkOrderNo) AS WorkOrderNo,  " &
                " Max(VMain.WorkOrderDate) AS WorkOrderDate, Max(VMain.ItemDesc) As ItemDesc, SUM(VMain.Qty) AS BalQty " &
                " FROM ( " & FRetFillItemWiseQry("And Party = '" & TxtParty.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.WorkOrder, VMain.WorkOrderSr " &
                " Order By WorkOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 750, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Work Order No.", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Work Order Date", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingWorkOrderItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingWorkOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.WorkOrder, Max(VMain.WorkOrderNo) AS WorkOrderNo,  " &
                " Max(VMain.WorkOrderDate) AS WorkOrderDate , SUM(VMain.Qty) AS BalQty   " &
                " FROM ( " & FRetFillItemWiseQry("And Party = '" & TxtParty.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.WorkOrder " &
                " Order By WorkOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingWorkOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FRetFillItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                LineConStr += " And CharIndex('|' + I.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                HeaderConStr += " And CharIndex('|' + V_Type + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If


        FRetFillItemWiseQry = " SELECT Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS WorkOrderNo, " &
                  " Max(H.V_Date) As WorkOrderDate, L.WorkOrder, L.WorkOrderSr, " &
                  " Max(L.Item) As Item, Max(I.Description) AS ItemDesc, " &
                  " IfNull(Sum(L.Qty),0) - IfNull(Max(VJobRec.ChallanQty), 0) As Qty, " &
                  " IfNull(Sum(L.TotalDeliveryMeasure),0) - IfNull(Max(VJobRec.TotalDeliveryMeasure), 0) As TotalDeliveryMeasure, " &
                  " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.Rate) AS Rate, Max(L.BillingType) AS BillingType," &
                  " Max(L.MeasureUnit) As MeasureUnit, Max(L.DeliveryMeasure) As DeliveryMeasure, Max(L.Specification) AS Specification, Max(L.SalesTaxGroupItem) AS SalesTaxGroupItem, " &
                  " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                  " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                  " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2,  " &
                  " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier " &
                  " FROM (    " &
                  "     SELECT DocID, V_Type, ManualRefNo , V_Date     " &
                  "     FROM WorkOrder  Where 1=1 " & HeaderConStr & " " &
                  " ) As H     " &
                  " LEFT JOIN WorkOrderDetail L  ON H.DocID = L.WorkOrder " &
                  " LEFT JOIN ( " &
                  "     SELECT L.WorkOrder, L.WorkOrderSr, Sum(L.Qty) AS ChallanQty, Sum(L.TotalDeliveryMeasure) AS TotalDeliveryMeasure " &
                  "     FROM WorkDispatchDetail  L  " &
                  "     GROUP BY L.WorkOrder, L.WorkOrderSr  " &
                  " ) AS VJobRec ON L.DocId = VJobRec.WorkOrder AND L.Sr = VJobRec.WorkOrderSr   " &
                  " LEFT JOIN Item I On L.Item = I.Code " &
                  " Left Join Unit U On L.Unit = U.Code " &
                  " Left Join Unit MU On L.DeliveryMeasure = MU.Code " &
                  " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                  " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                  " WHERE 1 = 1 " & LineConStr &
                  " GROUP BY L.WorkOrder, L.WorkOrderSr "
        FRetFillItemWiseQry += " HAVING IfNull(Sum(L.Qty),0) - IfNull(Max(VJobRec.ChallanQty),0) > 0 "
    End Function

    Private Sub FFillItemsForPendingWorkOrders(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            If RbtCancelForWorkOrderItem.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.WorkOrder + Convert(nVarChar, L.WorkOrderSr) In (" & bOrderNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry("", " And L.WorkOrder In (" & bOrderNoStr & ") ")
            End If
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()

            With DtTemp
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1WorkOrder, I).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                        Dgl1.Item(Col1WorkOrder, I).Value = AgL.XNull(.Rows(I)("WorkOrderNo"))
                        Dgl1.Item(Col1WorkOrderSr, I).Value = AgL.XNull(.Rows(I)("WorkOrderSr"))

                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension1Caption()))
                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension2Caption()))

                        Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))

                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("WorkOrder")), Dgl1, I, AgL.VNull(.Rows(I)("WorkOrderSr")))
                        CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                        CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        FOpenPartyDetail()
    End Sub

    Private Sub FOpenPartyDetail()
        Dim FrmObj As FrmPartyDetail
        Try
            If BtnFillPartyDetail.Tag Is Nothing Then
                FrmObj = New FrmPartyDetail
            Else
                FrmObj = BtnFillPartyDetail.Tag
            End If
            FrmObj.DispText(IIf(Topctrl1.Mode = "Browse", False, True))
            FrmObj.ShowDialog()
            If FrmObj.mOkButtonPressed Then BtnFillPartyDetail.Tag = FrmObj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        mQry = " SELECT Max(I.Code) AS Code,  Max(I.Description) AS Item,  Max(H.V_Type + '-' + H.ManualrefNo) AS WorkOrderNo, " &
                                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                                " Sum(l.Qty) - IfNull(Max(VChallan.ShippedQty),0) AS BalQty, Max(L.Unit) AS Unit, Max(L.Specification) AS Specification," &
                                " max(L.Rate) AS Rate, max(U.Decimalplaces) AS QtyDecimalplaces, max(UM.Decimalplaces) AS MeasureDecimalplaces, max(L.MeasurePerPcs) AS MeasurePerPcs, Max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, " &
                                " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2,  " &
                                " Sum(l.TotalDeliveryMeasure) - IfNull(Max(VChallan.TotalDeliveryMeasure),0) AS TotalDeliveryMeasure, max(L.DeliveryMeasure) AS DeliveryMeasure, max(L.DeliveryMeasure) AS BillingType, max(L.SalesTaxGroupItem) AS SalesTaxGroupItem, L.WorkOrder, L.WorkOrderSr " &
                                " FROM WorkOrderDetail L  " &
                                " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID " &
                                " Left Join " &
                                " 	(SELECT L.WorkOrder, L.WorkOrderSr, Sum(L.Qty) AS ShippedQty, Sum(L.TotalDeliveryMeasure) AS TotalDeliveryMeasure " &
                                " 	 FROM WorkDispatchDetail L  " &
                                " 	 GROUP BY L.WorkOrder, L.WorkOrderSr) AS VChallan  " &
                                " ON L.WorkOrder = VChallan.WorkOrder AND L.WorkOrderSr = VChallan.WorkOrderSr " &
                                " LEFT JOIN Item I ON L.Item = I.Code " &
                                " LEFT JOIN Unit U On U.Code = L.Unit " &
                                " LEFT JOIN Unit UM On UM.Code = L.DeliveryMeasure " &
                                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " &
                                " And H.Party = '" & TxtParty.Tag & "' And H.Div_Code = '" & TxtDivision.Tag & "'  And H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " AND L.DocId <> '" & mInternalCode & "' " &
                                " GROUP BY L.WorkOrder, L.WorkOrderSr " &
                                " Having Sum(l.Qty) - IfNull(Max(VChallan.ShippedQty),0) > 0 "
                        Dgl1.AgHelpDataSet(Col1Item, 12) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension1  "
                            Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension2  "
                            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprt.Click

        If Topctrl1.Mode <> "Add" Then
            MsgBox("Import can be done only on Add mode...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If TxtParty.Text = "" Then
            MsgBox("Please Select Party Name First...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If BtnImprt.Tag Is Nothing Then
            Dim FrmObj As Form
            FrmObj = AgTemplate.ClsMain.FRetImportForm(Me, TxtV_Type.Tag)
            If FrmObj IsNot Nothing Then
                FrmObj.Owner = Me
                BtnImprt.Tag = FrmObj
                FrmObj.ShowDialog()
                FrmObj = Nothing
            End If
        Else
            BtnImprt.Tag.ShowDialog()
        End If
        Call Calculation()
        Dgl1.Focus()
    End Sub

    Private Sub FrmWorkOrderCancel_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        GBoxImportFromExcel.Enabled = True
    End Sub
End Class

