Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmPurchOrderCancel
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Dim FRH_Multiple() As DMHelpGrid.FrmHelpGrid_Multi

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1PurchIndent As String = "Indent No"
    Protected Const Col1PurchIndentSr As String = "Purch Indent Sr"
    Protected Const Col1PurchOrder As String = "Purch Order"
    Protected Const Col1PurchOrderSr As String = "Purch Order Sr"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "MeasureUnit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"

    Dim mLastKeyPressed As Keys


    Dim BlnIsMeasurePerPcsVisible As Boolean = False
    Dim BlnIsMeasurePerPcsEditable As Boolean = False
    Dim BlnIsMeasureVisible As Boolean = False
    Dim BlnIsMeasureEditable As Boolean = False
    Dim BlnIsMeasureUnitVisible As Boolean = False
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents BtnShow As System.Windows.Forms.Button
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForOrder As System.Windows.Forms.RadioButton
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Dim BlnIsMeasureUnitEditable As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        'mItemType = Replace(ItemTypeStr, ",", "','")
    End Sub

    Public Sub FSetMeasureParameter(ByVal IsMeasurePerPcsVisible As Boolean, ByVal IsMeasurePerPcsEditable As Boolean, ByVal IsMeasureVisible As Boolean, ByVal IsMeasureEditable As Boolean, ByVal IsMeasureUnitVisible As Boolean, ByVal IsMeasureUnitEditable As Boolean)
        BlnIsMeasurePerPcsVisible = IsMeasurePerPcsVisible
        BlnIsMeasurePerPcsEditable = IsMeasurePerPcsEditable
        BlnIsMeasureVisible = IsMeasureVisible
        BlnIsMeasureEditable = IsMeasureEditable
        BlnIsMeasureUnitVisible = IsMeasureUnitVisible
        BlnIsMeasureUnitEditable = IsMeasureUnitEditable
    End Sub


#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblVendor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
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
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.BtnShow = New System.Windows.Forms.Button
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtForOrder = New System.Windows.Forms.RadioButton
        Me.TxtNature = New AgControls.AgTextBox
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
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(142, 18)
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
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 104)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
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
        'TxtVendor
        '
        Me.TxtVendor.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendor.AgMandatory = True
        Me.TxtVendor.AgMasterHelp = False
        Me.TxtVendor.AgNumberLeftPlaces = 8
        Me.TxtVendor.AgNumberNegetiveAllow = False
        Me.TxtVendor.AgNumberRightPlaces = 2
        Me.TxtVendor.AgPickFromLastValue = False
        Me.TxtVendor.AgRowFilter = ""
        Me.TxtVendor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendor.AgSelectedValue = Nothing
        Me.TxtVendor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor.Location = New System.Drawing.Point(329, 61)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(363, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(217, 61)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(48, 16)
        Me.LblVendor.TabIndex = 693
        Me.LblVendor.Text = "Vendor"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(2, 391)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(979, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalMeasure.Location = New System.Drawing.Point(377, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(266, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(81, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Area :"
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
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(194, 19)
        Me.LblJobReceiveDetail.TabIndex = 1003
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Purchase Order Cancel Detail"
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
        'BtnShow
        '
        Me.BtnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnShow.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnShow.Location = New System.Drawing.Point(306, 153)
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
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(205, 147)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(100, 26)
        Me.GrpDirectInvoice.TabIndex = 3003
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtForOrder
        '
        Me.RbtForOrder.AutoSize = True
        Me.RbtForOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForOrder.Location = New System.Drawing.Point(4, 8)
        Me.RbtForOrder.Name = "RbtForOrder"
        Me.RbtForOrder.Size = New System.Drawing.Size(88, 17)
        Me.RbtForOrder.TabIndex = 0
        Me.RbtForOrder.TabStop = True
        Me.RbtForOrder.Text = "For Order"
        Me.RbtForOrder.UseVisualStyleBackColor = True
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
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
        'FrmPurchOrderCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 627)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.BtnShow)
        Me.EntryNCat = "SO"
        Me.MainLineTableCsv = "PurchOrderDetail"
        Me.MainTableName = "PurchOrder"
        Me.Name = "FrmPurchOrderCancel"
        Me.Text = "Template Purchase Order"
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblVendor As System.Windows.Forms.Label
    Protected WithEvents TxtVendor As AgControls.AgTextBox
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
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
#End Region

    Private Sub FrmPurchOrderCancel_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And IfNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [Purch_Order_Cancel_Type],  " &
                    " H.V_Date AS [Purch_Order_Cancel_Date], H.V_No AS [Purch_Order_Cancel_No], H.ReferenceNo As  [Manual_Ref_No], " &
                    " H.VendorName , H.VendorAdd1 , " &
                    " H.VendorAdd2 , H.VendorCityName ,  " &
                    " H.VendorState , H.VendorCountry , " &
                    " H.Remarks, Abs(H.TotalQty) AS [Total_Qty], Abs(H.TotalMeasure) AS [Total_Measure], Abs(H.TotalAmount) AS [Total_Amount],  " &
                    " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type], " &
                    " H.EntryStatus AS [Entry_Status], H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date], " &
                    " H.Status, D.Div_Name AS [Division]," &
                    " SM.Name AS [Site_Name] " &
                    " FROM PurchOrder  H " &
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type " &
                    " LEFT JOIN SubGroup SGA ON SGA.SubCode  = H.Agent  " &
                    " LEFT JOIN SeaPort DP ON H.DestinationPort = DP.Code  " &
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Purch_Order_Cancel_Date]"
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        LogTableName = "PurchOrder_Log"
        LogLineTableCsv = "PurchOrderDetail_Log,Structure_TransFooter_Log,Structure_TransLine_Log"

        MainTableName = "PurchOrder"
        MainLineTableCsv = "PurchOrderDetail,Structure_TransFooter,Structure_TransLine"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)


        AgCalcGrid1.AgLibVar = AgL
    End Sub


    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = "Select DocID As SearchCode " &
                " From PurchOrder H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndent, 80, 0, Col1PurchIndent, False, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndentSr, 60, 0, Col1PurchIndentSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchOrder, 70, 0, Col1PurchOrder, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchOrderSr, 70, 0, Col1PurchOrderSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, True, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 0, Col1BillingType, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 0, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, BlnIsMeasurePerPcsVisible, Not BlnIsMeasurePerPcsEditable, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 100, 50, Col1MeasureUnit, BlnIsMeasureUnitVisible, Not BlnIsMeasureUnitEditable, False)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, False, False, False)

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


    Private Sub FrmPurchOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        Dim bSelectionQry$ = ""


        Dim FrmObj As New FrmPurchPartyDetail
        FrmObj = BtnFillPartyDetail.Tag


        mQry = "UPDATE PurchOrder " &
                "   SET " &
                "   ReferenceNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                "   Vendor = " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & ", " &
                "	VendorName = " & AgL.Chk_Text(TxtVendor.Text) & ", " &
                "	VendorAdd1 = " & AgL.Chk_Text(FrmObj.TxtVendorAdd1.Text) & ", " &
                "	VendorAdd2 = " & AgL.Chk_Text(FrmObj.TxtVendorAdd2.Text) & ", " &
                "	VendorCity = " & AgL.Chk_Text(FrmObj.TxtVendorCity.Tag) & ", " &
                "	VendorCityName = " & AgL.Chk_Text(FrmObj.TxtVendorCity.Text) & ", " &
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " &
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                "	Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                "   TotalQty = " & -Val(LblTotalQty.Text) & ", " &
                "   TotalAmount = " & -Val(LblTotalAmount.Text) & ", " &
                "   TotalMeasure = " & -Val(LblTotalMeasure.Text) & ", " &
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PurchOrderDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PurchOrderDeliveryDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO PurchOrderDetail(DocId, Sr, Item, PurchIndent, PurchIndentSr, Specification, SalesTaxGroupItem, Qty, " &
                  " Unit, Rate, Amount, T_Nature, " &
                  " MeasurePerPcs, TotalMeasure, MeasureUnit, " &
                  " PurchOrder, PurchOrderSr, BillingType, " & AgCalcGrid1.FLineTableFieldNameStr() & ") "

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += "Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1SalesTaxGroup, I)) & ", " &
                            " " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1PurchOrder, I)) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrderSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                End If
            Next
        End With
        mQry = mQry + bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        FPostInStockVirtual(Conn, Cmd)
    End Sub

    Private Sub FPostInStockVirtual(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim StockVirtual As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO StockVirtual(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, Site_Code, SubCode, " &
                  " CostCenter, Item, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, Rate, Amount, " &
                  " ReferenceDocID, ReferenceDocIDSr) " &
                  " SELECT H.DocID, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, H.Div_Code, " &
                  " H.Site_Code, H.Vendor, Null, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit, " &
                  " l.Rate, L.Amount, L.PurchIndent, L.PurchIndentSr " &
                  " FROM PurchOrder H  " &
                  " LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocId " &
                  " Where H.DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, Po.ReferenceNo As PurchOrderRefNo " &
                " From PurchOrder H " &
                " LEFT JOIN PurchOrder Po On H.PurchOrder = Po.DocId " &
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
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorName"))

                Dim FrmObj As New FrmPurchPartyDetail
                FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("VendorName"))
                FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAdd1"))
                FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("VendorAdd2"))
                FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))
                BtnFillPartyDetail.Tag = FrmObj
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                LblTotalQty.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalQty")))
                LblTotalAmount.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalAmount")))
                LblTotalMeasure.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalMeasure")))


                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, Po.ReferenceNo As PurchOrderRefNo, I.Description,  " &
                        "U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                        " from PurchOrderDetail L " &
                        " LEFT JOIN PurchOrder Po On L.PurchOrder = Po.DocId " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " Left Join Unit U On I.Unit = U.Code " &
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " &
                        " Where L.DocId = '" & SearchCode & "' Order By Sr"

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
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

                            Dgl1.Item(Col1PurchOrder, I).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                            Dgl1.Item(Col1PurchOrder, I).Value = AgL.XNull(.Rows(I)("PurchOrderRefNo"))
                            Dgl1.Item(Col1PurchOrderSr, I).Value = AgL.XNull(.Rows(I)("PurchOrderSr"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With


                'Calculation()
                '-------------------------------------------------------------
            End If
        End With

    End Sub

    Private Sub FrmPurchOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtV_Type.Focus()
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtVendor.Focus()
    End Sub

    Private Sub TxtVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVendor.Enter
        Try
            Select Case sender.Name
                'Case TxtPurchOrder.Name
                '    If TxtPurchOrder.AgHelpDataSet Is Nothing Then
                '        mQry = " SELECT H.DocID, H.ReferenceNo As OrderNo, H.TotalQty, IfNull(V1.ShippedQty,0) AS ShippedQty, " & _
                '                " IfNull(V2.CancelQty,0) AS CancelQty, " & _
                '                " H.TotalQty - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0)  AS BalQty, H.Status, H.Vendor, Vt.NCat  " & _
                '                " FROM PurchOrder H  " & _
                '                " Left Join " & _
                '                " 	(SELECT C.PurchOrder, Sum(C.Qty) AS ShippedQty, Sum(C.TotalMeasure) AS ShippedMeasure " & _
                '                " 	 FROM PurchChallanDetail C  " & _
                '                " 	 GROUP BY C.PurchOrder) AS V1 " & _
                '                " ON H.DocId = V1.PurchOrder " & _
                '                " Left Join " & _
                '                " 	(SELECT Sd.PurchOrder, Sum(Sd.Qty) AS CancelQty " & _
                '                " 	 FROM PurchOrder S  " & _
                '                " 	 LEFT JOIN PurchOrderDetail Sd ON S.DocID = Sd.DocId " & _
                '                " 	 LEFT JOIN Voucher_Type Vt ON S.V_Type = Vt.V_Type  " & _
                '                " 	 WHERE Vt.NCat = '" & LblV_Type.Tag & "' " & _
                '                " 	 GROUP BY Sd.PurchOrder) AS V2 " & _
                '                " ON H.DocId = V2.PurchOrder " & _
                '                " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                '                " Where H.Vendor = '" & TxtVendor.AgSelectedValue & "'" & _
                '                " And H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                '                " And H.TotalQty - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0) > 0 "
                '        TxtPurchOrder.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
                Dgl1.Item(Col1BillingType, mRow).Value = ""
                Dgl1.Item(Col1PurchOrder, mRow).Tag = ""
                Dgl1.Item(Col1PurchOrder, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = ""
                Dgl1.Item(Col1PurchIndent, mRow).Value = ""
                Dgl1.Item(Col1PurchIndentSr, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Amount, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Amount").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillingType").Value)
                    Dgl1.Item(Col1PurchOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrder").Value)
                    Dgl1.Item(Col1PurchOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrderManualNo").Value)
                    Dgl1.Item(Col1PurchOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrderSr").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxGroupItem").Value)
                    Dgl1.Item(Col1PurchIndent, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndent").Value)
                    Dgl1.Item(Col1PurchIndentSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndentSr").Value)
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

            Case Col1Item
                If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                    mQry = " SELECT I.Code, I.Description AS Item, L.PurchIndent, L.PurchIndentSr, H.ReferenceNo As OrderNo, " &
                            " IfNull(L.Qty,0) AS Qty ,IfNull(V1.ShippedQty,0) AS ShippedQty ,IfNull(V2.CancelQty,0) AS CancelQty,  " &
                            " IfNull(L.Qty,0)  + IfNull(V2.CancelQty,0)  - IfNull(V1.ShippedQty,0) AS BalQty, " &
                            " L.Unit, L.Rate, L.Amount, L.SPECIFICATION, " &
                            " L.SalesTaxGroupItem, L.MeasurePerPcs, L.MeasureUnit, H.BillingType, " &
                            " L.DocId As PurchOrder, L.Sr as PurchOrderSr, IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, H.Vendor, I.ItemType, Vt.NCat, H.ReferenceNo As PurchOrderManualNo, L.SalesTaxGroupItem, " &
                            " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                            " FROM PurchOrderDetail L  " &
                            " LEFT JOIN PurchOrder H ON L.DocId = H.DocID " &
                            " Left Join " &
                            " 	(SELECT C.PurchOrderSr, IfNull(C.PurchOrder,'') AS PurchOrder, Sum(C.Qty) AS ShippedQty " &
                            " 	 FROM PurchChallanDetail C " &
                            " 	 GROUP BY  C.PurchOrder, C.PurchOrderSr) AS V1 " &
                            " ON L.DocId = V1.PurchOrder AND L.Sr = V1.PurchOrderSr " &
                            " Left Join " &
                            " 	(SELECT Sd.PurchOrderSr, Sd.PurchOrder, Sum(Sd.Qty) AS CancelQty " &
                            " 	 FROM PurchOrder S  " &
                            " 	 LEFT JOIN PurchOrderDetail Sd ON S.DocID = Sd.DocId " &
                            " 	 LEFT JOIN Voucher_Type Vt ON S.V_Type = Vt.V_Type  " &
                            " 	 WHERE SD.Qty < 0 " &
                            " 	 GROUP BY  Sd.PurchOrder, SD.PurchOrderSr) AS V2	 " &
                            " ON L.DocId = V2.PurchOrder AND L.Sr = V2.PurchOrderSr " &
                            " LEFT JOIN Item I ON L.Item = I.Code " &
                            " Left Join Unit U On I.Unit = U.Code " &
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " &
                            " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " &
                            " Where H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                            " And H.Vendor = '" & TxtVendor.AgSelectedValue & "' " &
                            " And IfNull(L.Qty,0)  + IfNull(V2.CancelQty,0)  - IfNull(V1.ShippedQty,0) > 0 "
                    Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
                End If
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
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) > 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.0000")
                End If
                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Area") Or AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.AgVoucherCategory = "PURCH"
        AgCalcGrid1.Calculation()


        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.00")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.0000")
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1PurchOrder).Index & "," & Dgl1.Columns(Col1PurchOrderSr).Index & "") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    mQry = " SELECT Round(IfNull(Sum(L.Qty),0) - IfNull(Max(Dispatch.Qty),0),3) AS BalQty " &
                            " FROM PurchOrderDetail L  " &
                            " Left Join " &
                            " 	(SELECT C.PurchOrderSr , C.PurchOrder, Sum(C.Qty) AS Qty " &
                            " 	 FROM PurchChallanDetail C " &
                            "    Where C.PurchOrder = '" & .AgSelectedValue(Col1PurchOrder, I) & "' And C.PurchOrderSr = '" & Val(.Item(Col1PurchOrderSr, I).Value) & "'   " &
                            " 	 GROUP BY C.PurchOrder, C.PurchOrderSr) AS Dispatch " &
                            " ON L.DocId = Dispatch.PurchOrder AND L.Sr = Dispatch.PurchOrderSr " &
                            " Where L.DocID <> '" & mSearchCode & "'  And L.PurchOrder = '" & .AgSelectedValue(Col1PurchOrder, I) & "' " &
                            " And L.PurchOrderSr = '" & Val(.Item(Col1PurchOrderSr, I).Value) & "' "
                    If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) < Val(.Item(Col1Qty, I).Value) Then
                        MsgBox("Balance Qty Of """ & .Item(Col1Item, I).Value & """ In Purch Order """ & .Item(Col1PurchOrder, I).Value & """ Is Less Then " & Val(.Item(Col1Qty, I).Value) & " At Row No " & .Item(ColSNo, I).Value & ".", MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub TempPurchOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FillPurchOrderDetail(ByVal PurchOrder As String)
        Dim I As Integer
        Dim DsTemp As DataSet

        Try
            mQry = "Select H.*, Sg.SalesTaxPostingGroup " &
                    " From PurchOrder H " &
                    " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode  " &
                    " Where H.DocID in (" & PurchOrder & ")"
            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                    TxtVendor.Text = AgL.XNull(.Rows(0)("VendorName"))


                    If AgL.StrCmp(TxtNature.Text, "Cash") Then
                        Dim FrmObj As New FrmPurchPartyDetail
                        FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                        FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                        FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAdd1"))
                        FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("VendorAdd2"))
                        FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                        FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))
                        BtnFillPartyDetail.Tag = FrmObj
                    End If

                    TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                    TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                    LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                    LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                    LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))

                    mQry = " SELECT L.DocId, L.Sr , L.PurchIndent, L.PurchIndentSr, L.Item, L.SPECIFICATION, H.BillingType, " &
                            " Round(IfNull(L.Qty,0) - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0),3) AS BalQty, L.Unit, L.Rate, L.Amount, " &
                            " L.SalesTaxGroupItem, L.MeasurePerPcs, L.MeasureUnit, " &
                            " L.DocId As PurchOrder, I.Description As ItemDesc, H.ReferenceNO As PurchOrderManualNo, " &
                            " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                            " FROM PurchOrderDetail L  " &
                            " LEFT JOIN PurchOrder H ON L.DocId = H.DocID " &
                            " LEFT JOIN " &
                            " 	(SELECT C.PurchOrderSr, C.PurchOrder AS PurchOrder, Sum(C.Qty) AS ShippedQty, Sum(C.TotalMeasure) as ShippedMeasure " &
                            " 	 FROM PurchChallanDetail C " &
                            " 	 GROUP BY C.PurchOrderSr, C.PurchOrder) AS V1 " &
                            " ON L.DocId = V1.PurchOrder AND L.Sr = V1.PurchOrderSr " &
                            " LEFT JOIN " &
                            " 	(SELECT Sd.PurchOrderSr, Sd.PurchOrder, Sum(Sd.Qty) AS CancelQty, Sum(Sd.TotalMeasure) as CancelMeasure " &
                            " 	 FROM PurchOrder S  " &
                            " 	 LEFT JOIN PurchOrderDetail Sd ON S.DocID = Sd.DocId " &
                            " 	 LEFT JOIN Voucher_Type Vt ON S.V_Type = Vt.V_Type  " &
                            " 	 WHERE Sd.Qty < 0 " &
                            " 	 GROUP BY Sd.PurchOrderSr, Sd.PurchOrder) AS V2	 " &
                            " ON L.DocId = V2.PurchOrder AND L.Sr = V2.PurchOrderSr " &
                            " LEFT JOIN Item I ON L.Item = I.Code " &
                            " Left Join Unit U On I.Unit = U.Code " &
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " &
                            " Where L.DocId In (" & PurchOrder & ") " &
                            " And Round(IfNull(L.Qty,0) - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0),3) > 0 " &
                            " Order By L.Sr "


                    DsTemp = AgL.FillData(mQry, AgL.GCn)
                    With DsTemp.Tables(0)
                        Dgl1.RowCount = 1
                        Dgl1.Rows.Clear()
                        If .Rows.Count > 0 Then
                            For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                                Dgl1.Rows.Add()
                                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                                Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                                Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                                Dgl1.Item(Col1PurchIndent, I).Value = AgL.XNull(.Rows(I)("PurchIndent"))
                                Dgl1.Item(Col1PurchIndentSr, I).Value = AgL.VNull(.Rows(I)("PurchIndentSr"))
                                Dgl1.Item(Col1PurchOrder, I).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                                Dgl1.Item(Col1PurchOrder, I).Value = AgL.XNull(.Rows(I)("PurchOrderManualNo"))
                                Dgl1.Item(Col1PurchOrderSr, I).Value = AgL.XNull(.Rows(I)("Sr"))
                                Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                                Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("BalQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                                Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                                Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                                Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                                Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                                Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                                Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                                Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                                Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))


                                Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))

                                'AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("DocId")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                            Next I
                        End If
                    End With
                    Calculation()
                    '-------------------------------------------------------------
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FrmPurchOrder_YarnSku_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim DsRep1 As New DataSet
        Dim DsRep2 As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = "", bThirdTableName As String = ""
        Dim bStructJoin As String = ""
        Dim bSubQry As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Purchase Order Cancel"
            RepName = "RUG_PurchOrderCancel_Print" : RepTitle = "Purchase Order Cancel"
            bTableName = "PurchOrder" : bSecTableName = "PurchOrderDetail L ON L.DocID =H.DocID"
            bCondstr = "WHERE H.DocID='" & mSearchCode & "'"

            mQry = " SELECT  H.DocID, H.V_Type || ' - ' ||convert(NVARCHAR(5),H.V_No) AS VoucherNo, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code,  H.ReferenceNo, " &
                     " H.Vendor, H.VendorName, H.VendorAdd1, H.VendorAdd2, H.VendorCity, H.VendorCityName, H.VendorState,  " &
                     " H.VendorCountry,  H.VendorOrderNo, H.VendorOrderDate, H.VendorDeliveryDate, H.VendorOrderCancelDate,  " &
                     " H.PaymentTerms,H.PriceMode, H.SalesTaxGroupParty,SG.Mobile,SG.EMail, CASE WHEN H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' THEN '' ELSE H.Status END AS Status , " &
                     " H.Currency , H.PurchQuotaion, H.PurchIndent,  H.TermsAndConditions, H.Remarks, H.TotalQty, H.TotalMeasure,  " &
                     " H.EntryBy, H.EntryDate,H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, L.SR, " &
                     " H.TotalAmount,  H.IsDeleted, L.PurchIndent AS LinePurchIndent, L.Item, Abs(L.Qty) As Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " &
                     " L.TotalMeasure AS LineTotalMeasure, L.Rate, Abs(L.Amount) As Amount , I.Description AS ItemDesc, SM.Name AS SiteName , L.Specification, L.Remark ," &
                     " P.V_No AS IndentNo, P.V_Type AS IndentType,  " &
                     " YD.Description AS YarnDesc,S.Description AS ShadeDesc, S.Colour  AS ColourDesc,  " &
                     " IfNull(V1.ChallanQty,0) AS ChallanQty, Sg.TinNO As vendorTinNo, Po.V_Type || '-' || Po.ReferenceNo As OrderRefNo, '' As ItemType, " &
                     " H.Gross_Amount, H.Basic_Excise_Duty_Per, H.Basic_Excise_Duty, H.Excise_ECess_Per, H.Excise_ECess, H.Excise_HECess_Per, H.Excise_HECess, " &
                     " H.Total_Excise_Duty, H.Discount_Pre_Tax_Per, H.Discount_Pre_Tax, H.Other_Additions_Pre_Tax, H.Sales_Tax_Taxable_Amt, H.Vat_Per, H.Vat, " &
                     " H.Sat_Per, H.Sat, H.Cst_Per, H.Cst, H.Custom_Duty_Taxable_Amt, H.Custom_Duty_Per, H.Custom_Duty, H.Custom_Duty_ECess_Per, " &
                     " H.Custom_Duty_ECess, H.Custom_Duty_HECess_Per, H.Custom_Duty_HECess, H.Additional_Duty_Per, H.Additional_Duty, H.Total_Custom_Duty, " &
                     " H.Sub_Total, H.Insurance_Per, H.Insurance, H.Freight_Per, H.Freight, H.Handling_Charges_Per, H.Handling_Charges, H.Other_Charges_Per, " &
                     " H.Other_Charges, H.Discount_Per, H.Discount, H.Round_Off_Per, H.Round_Off, H.Net_Amount, H.Freight_Outward    " &
                     " FROM " & bTableName & " H " &
                     " LEFT JOIN " & bSecTableName & "  " &
                     " LEFT JOIN Item I ON I.Code=L.Item    " &
                     " LEFT JOIN Rug_YarnSKU YS ON YS.Code=L.Item " &
                     " LEFT JOIN Rug_Yarn YD ON YD.Code=YS.Yarn    " &
                     " LEFT JOIN Rug_Shade S ON S.Code=YS.Shade    " &
                     " LEFT JOIN PurchIndent P on P.DocId=L.PurchIndent  " &
                     " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                     " LEFT JOIN Voucher_Type Vt ON Vt.V_Type= H.V_Type   " &
                     " LEFT JOIN SubGroup SG ON SG.SubCode= H.Vendor " &
                     " LEFT JOIN PurchOrder Po ON L.PurchOrder = Po.DocId " &
                     " Left Join ( " &
                     "       SELECT PC.PurchOrder,PC.Item,sum(PC.Qty) AS ChallanQty    " &
                     "       FROM PurchChallanDetail PC   " &
                     "       WHERE PC.PurchOrder Is Not NULL " &
                     "       GROUP BY PC.PurchOrder,PC.Item    " &
                     " )V1 ON V1.PurchOrder=L.DocId AND V1.Item=L.Item " &
                     " " & bCondstr & ""
            AgL.ADMain = New SQLiteDataAdapter(mQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)

            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TxtVendor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVendor.KeyDown
        Select Case sender.name
            Case TxtVendor.Name
                If e.KeyCode <> Keys.Enter Then
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Sg.SubCode As Code, Sg.DispName AS [Name], C.CityName AS [City], C.State, C.Country, C.CityCode, " &
                                " Sg.Add1, Sg.Add2, Sg.Add3, Sg.Currency, Sg.SalesTaxPostingGroup  " &
                                " FROM SubGroup Sg  " &
                                " LEFT JOIN City C ON Sg.CityCode = C.CityCode  "
                        TxtVendor.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub TxtVendor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVendor.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.Name
                Case TxtVendor.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                            Dim FrmObj As New FrmPurchPartyDetail
                            If BtnFillPartyDetail.Tag Is Nothing Then
                                BtnFillPartyDetail.Tag = FrmObj
                            Else
                                FrmObj = BtnFillPartyDetail.Tag
                            End If
                            FrmObj.TxtVendorName.Text = AgL.XNull(DrTemp(0)("Name"))
                            FrmObj.TxtVendorAdd1.Text = AgL.XNull(DrTemp(0)("Add1"))
                            FrmObj.TxtVendorAdd2.Text = AgL.XNull(DrTemp(0)("Add2"))
                            FrmObj.TxtVendorCity.Tag = AgL.XNull(DrTemp(0)("CityCode"))
                            FrmObj.TxtVendorCity.Text = AgL.XNull(DrTemp(0)("City"))
                            BtnFillPartyDetail.Tag = FrmObj
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
        End Try
        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        Dim strTicked As String
        strTicked = FHPGD_PendingPurchOrder()
        If strTicked <> "" Then
            FillPurchOrderDetail(strTicked)
        End If
    End Sub

    Private Function FHPGD_PendingPurchOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""
        Dim strCond As String = ""


        'If mItemType.Trim <> "" Then
        '    strCond += " And I.ItemType In ('" & mItemType & "') "
        'End If

        StrSendText = RbtForOrder.Tag
        'mQry = " SELECT  'o' As Tick, Max(H.DocID) AS Code, Max(H.ReferenceNo) As OrderNo, Max(H.V_Date) as V_Date, " & _
        '        " Round(Sum(L.Qty) - IfNull(Sum(ShippingDetail.ShippedQty),0),3) AS BalQty, " & _
        '        " Sum(L.Qty) as Qty, IfNull(Sum(ShippingDetail.ShippedQty),0) AS ShippedQty " & _
        '        " FROM PurchOrderDetail L  " & _
        '        " Left Join PurchOrder H  On L.PurchOrder = H.DocID " & _
        '        " Left Join Item I  On L.Item = I.Code " & _
        '        " Left Join " & _
        '        " 	(SELECT C.PurchOrder, C.PurchOrderSr, Sum(C.Qty) AS ShippedQty, Sum(C.TotalMeasure) AS ShippedMeasure " & _
        '        " 	 FROM PurchChallanDetail C  " & _
        '        " 	 GROUP BY C.PurchOrder, C.PurchOrderSr) AS ShippingDetail " & _
        '        " ON L.DocId = ShippingDetail.PurchOrder And L.Sr = ShippingDetail.PurchOrderSr " & _
        '        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
        '        " Where L.DocID <> '" & mInternalCode & "' And H.Vendor = '" & TxtVendor.AgSelectedValue & "'" & _
        '        " And H.Site_Code = '" & TxtSite_Code.Tag & "' And H.Div_Code = '" & TxtDivision.Tag & "' " & _
        '        " And H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & _
        '        " Group By L.PurchOrder " & _
        '        " Having Round(Sum(L.Qty) - IfNull(Sum(ShippingDetail.ShippedQty),0),3) <> 0  " & _
        '        " Order By Max(H.V_Date) "

        mQry = " SELECT 'o' As Tick,VMain.DocId AS Code, Max(PO.ReferenceNo) AS OrderNo, Max(PO.V_Date) AS V_Date, Sum(VMain.BalQty) AS BalQty " &
                "  FROM " &
                "  ( " &
                "  SELECT L.DocId, Round(IfNull(L.Qty,0) - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0),3) AS BalQty  " &
                "  FROM PurchOrderDetail L  " &
                "  LEFT JOIN PurchOrder H ON L.DocId = H.DocID  " &
                "  LEFT JOIN  " &
                "  (SELECT C.PurchOrderSr, C.PurchOrder AS PurchOrder, Sum(C.Qty) AS ShippedQty, Sum(C.TotalMeasure) as ShippedMeasure  " &
                "   FROM PurchChallanDetail C  " &
                "   GROUP BY C.PurchOrderSr, C.PurchOrder " &
                "  ) AS V1  ON L.DocId = V1.PurchOrder AND L.Sr = V1.PurchOrderSr  " &
                "  LEFT JOIN  " &
                "  (SELECT Sd.PurchOrderSr, Sd.PurchOrder, Sum(Sd.Qty) AS CancelQty, Sum(Sd.TotalMeasure) as CancelMeasure  " &
                "  FROM PurchOrder S   " &
                "  LEFT JOIN PurchOrderDetail Sd ON S.DocID = Sd.DocId   " &
                "  LEFT JOIN Voucher_Type Vt ON S.V_Type = Vt.V_Type   " &
                "  WHERE Sd.Qty < 0   " &
                "  GROUP BY Sd.PurchOrderSr, Sd.PurchOrder) AS V2	  ON L.DocId = V2.PurchOrder AND L.Sr = V2.PurchOrderSr   " &
                " WHERE Round(IfNull(L.Qty,0) - IfNull(V1.ShippedQty,0) + IfNull(V2.CancelQty,0),3) > 0    " &
                " AND H.DocID <> '" & mInternalCode & "' And H.Vendor = '" & TxtVendor.AgSelectedValue & "'" &
                " And H.Site_Code = '" & TxtSite_Code.Tag & "' And H.Div_Code = '" & TxtDivision.Tag & "' " &
                " ) VMain " &
                " LEFT JOIN PurchOrder PO ON PO.DocId = VMain.DocId " &
                " GROUP BY VMain.DocId  " &
                " Order By Max(PO.V_Date) "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Balance Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(5, "Total Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(6, "Shipped Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(7, "Cancelled Qty", 90, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

End Class

