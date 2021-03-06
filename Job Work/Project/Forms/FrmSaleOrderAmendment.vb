Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmSaleOrderAmendment
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Supplier As String = "Supplier"
    Protected Const Col1WorkOrder As String = "Work Order"
    Protected Const Col1WorkOrderSr As String = "Work Order Sr"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1CurrentQty As String = "Current Qty"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"

    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1GenDocID As String = "Gen. DocId"
    Protected Const Col1GenDocIDSr As String = "Gen. DocId Sr"

    Protected Const Col1WorkOrderDate As String = "Work Order Date"

    Dim mLastKeyPressed As Keys

    Dim BlnIsMeasurePerPcsEditable As Boolean = False
    Dim BlnIsMeasureUnitEditable As Boolean = False
    Dim BlnIsMeasureEditable As Boolean = False
    Dim BlnIsMeasurePerPcsVisible As Boolean = False
    Dim BlnIsMeasureVisible As Boolean = False
    Dim BlnIsMeasureUnitVisible As Boolean = False
    Dim BlnIsDeliveryMeasureVisible As Boolean = False
    Dim BlnIsTotalDeliveryMeasureVisible As Boolean = False
    Dim BlnIsItemCodeVisible As Boolean = False
    Dim BlnIsRateTypeVisible As Boolean = False
    Protected WithEvents BtnFillWorkOrder As System.Windows.Forms.Button

    Dim BlnIsBillingTypeVisible As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        Me.EntryNCat = strNCat
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtAddNewItem = New System.Windows.Forms.RadioButton
        Me.RbtQtyAmendment = New System.Windows.Forms.RadioButton
        Me.BtnFillWorkOrder = New System.Windows.Forms.Button
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
        Me.GroupBox2.Location = New System.Drawing.Point(832, 582)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 582)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 582)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(116, 18)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 582)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 582)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 570)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 582)
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
        Me.LblV_No.Location = New System.Drawing.Point(224, 167)
        Me.LblV_No.Size = New System.Drawing.Size(64, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Order No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(332, 166)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(345, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(249, 36)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(561, 21)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(361, 35)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(467, 17)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(575, 15)
        Me.TxtV_Type.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(345, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(249, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(361, 15)
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
        Me.LblPrefix.Location = New System.Drawing.Point(284, 167)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(992, 124)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label9)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 98)
        Me.TP1.Text = "Document Detail"
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
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label9, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 3
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
        Me.Label4.Location = New System.Drawing.Point(345, 62)
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
        Me.TxtParty.Location = New System.Drawing.Point(361, 55)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(377, 18)
        Me.TxtParty.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(249, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 693
        Me.Label5.Text = "Party Name"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(2, 402)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(979, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(682, 2)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 714
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(470, 2)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(213, 22)
        Me.LblTotalDeliveryMeasureText.TabIndex = 713
        Me.LblTotalDeliveryMeasureText.Text = "Delivery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalMeasure.Location = New System.Drawing.Point(364, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(194, 2)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(169, 22)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Measure :"
        Me.LblTotalMeasureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.LblTotalAmountText.Location = New System.Drawing.Point(807, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(65, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Amount :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.Color.Black
        Me.LblTotalQty.Location = New System.Drawing.Point(135, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(3, 2)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(131, 22)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Qty :"
        Me.LblTotalQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(2, 169)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 233)
        Me.Pnl1.TabIndex = 1
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(670, 431)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(307, 133)
        Me.PnlCalcGrid.TabIndex = 1002
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
        Me.TxtRemarks.Location = New System.Drawing.Point(4, 450)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(579, 114)
        Me.TxtRemarks.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(1, 429)
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
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(2, 147)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(198, 21)
        Me.LblJobReceiveDetail.TabIndex = 1003
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Sale Order Amendment Detail"
        Me.LblJobReceiveDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(561, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 742
        Me.Label3.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(575, 35)
        Me.TxtManualRefNo.MaxLength = 20
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(163, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(467, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 16)
        Me.Label9.TabIndex = 741
        Me.Label9.Text = "Entry No"
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtAddNewItem)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtQtyAmendment)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(213, 138)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(269, 26)
        Me.GrpDirectInvoice.TabIndex = 1004
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtAddNewItem
        '
        Me.RbtAddNewItem.AutoSize = True
        Me.RbtAddNewItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtAddNewItem.Location = New System.Drawing.Point(137, 11)
        Me.RbtAddNewItem.Name = "RbtAddNewItem"
        Me.RbtAddNewItem.Size = New System.Drawing.Size(116, 17)
        Me.RbtAddNewItem.TabIndex = 1
        Me.RbtAddNewItem.TabStop = True
        Me.RbtAddNewItem.Text = "Add New Item"
        Me.RbtAddNewItem.UseVisualStyleBackColor = True
        '
        'RbtQtyAmendment
        '
        Me.RbtQtyAmendment.AutoSize = True
        Me.RbtQtyAmendment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtQtyAmendment.Location = New System.Drawing.Point(-1, 11)
        Me.RbtQtyAmendment.Name = "RbtQtyAmendment"
        Me.RbtQtyAmendment.Size = New System.Drawing.Size(129, 17)
        Me.RbtQtyAmendment.TabIndex = 0
        Me.RbtQtyAmendment.TabStop = True
        Me.RbtQtyAmendment.Text = "Qty Amendment"
        Me.RbtQtyAmendment.UseVisualStyleBackColor = True
        '
        'BtnFillWorkOrder
        '
        Me.BtnFillWorkOrder.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillWorkOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillWorkOrder.Location = New System.Drawing.Point(492, 148)
        Me.BtnFillWorkOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillWorkOrder.Name = "BtnFillWorkOrder"
        Me.BtnFillWorkOrder.Size = New System.Drawing.Size(34, 20)
        Me.BtnFillWorkOrder.TabIndex = 1005
        Me.BtnFillWorkOrder.Text = "..."
        Me.BtnFillWorkOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillWorkOrder.UseVisualStyleBackColor = True
        '
        'FrmWorkOrderAmendment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.BtnFillWorkOrder)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.EntryNCat = "SO"
        Me.LogLineTableCsv = "SaleOrderDetail_LOG"
        Me.LogTableName = "SaleOrder_Log"
        Me.MainLineTableCsv = "SaleOrderDetail"
        Me.MainTableName = "SaleOrder"
        Me.Name = "FrmWorkOrderAmendment"
        Me.Text = "Template Sale Order"
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnFillWorkOrder, 0)
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
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents RbtAddNewItem As System.Windows.Forms.RadioButton
    Protected WithEvents RbtQtyAmendment As System.Windows.Forms.RadioButton
#End Region

    Private Sub FrmWorkOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And IsNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Date AS [Work_Order_Cancel_Date], H.ReferenceNo AS [Work_Order_Amendment_No], " & _
                    " H.WorkToPartyName AS [Work_To_Party_Name], H.WorkToPartyAdd1 AS [Work_To_Party_Add1], " & _
                    " H.WorkToPartyAdd2 AS [Work_To_Party_Add2], H.WorkToPartyCityName AS [Work_TO_Party_City_Name],  " & _
                    " H.WorkToPartyState AS [Work_TO_Party_State], H.WorkToPartyCountry AS [Work_TO_Party_Country] " & _
                    " FROM WorkOrder  H " & _
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type " & _
                    " LEFT JOIN SubGroup SGA ON SGA.SubCode  = H.Agent  " & _
                    " LEFT JOIN SeaPort DP ON H.DestinationPort = DP.Code  " & _
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "WorkOrder"
        LogTableName = "WorkOrder_Log"
        MainLineTableCsv = "WorkOrderDetail"
        LogLineTableCsv = "WorkOrderDetail_LOG"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub


    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"


        mQry = "Select DocID As SearchCode " & _
            " From WorkOrder H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Public Sub FSetParameter(ByVal IsMeasurePerPcsEditable As Boolean, ByVal IsMeasureUnitEditable As Boolean, ByVal IsMeasureEditable As Boolean, ByVal IsMeasurePerPcsVisible As Boolean, ByVal IsMeasureVisible As Boolean, ByVal IsMeasureUnitVisible As Boolean, ByVal IsDeliveryMeasureVisible As Boolean, ByVal IsTotalDeliveryMeasureVisible As Boolean, ByVal IsItemCodeVisible As Boolean, ByVal IsRateTypeVisible As Boolean, ByVal IsBillingTypeVisible As Boolean)
        BlnIsMeasurePerPcsEditable = IsMeasurePerPcsEditable
        BlnIsMeasureEditable = IsMeasureEditable
        BlnIsMeasureUnitEditable = IsMeasureUnitEditable
        BlnIsMeasurePerPcsVisible = IsMeasurePerPcsVisible
        BlnIsMeasureVisible = IsMeasureVisible
        BlnIsMeasureUnitVisible = IsMeasureUnitVisible
        BlnIsDeliveryMeasureVisible = IsDeliveryMeasureVisible
        BlnIsTotalDeliveryMeasureVisible = IsTotalDeliveryMeasureVisible
        BlnIsItemCodeVisible = IsItemCodeVisible
        BlnIsRateTypeVisible = IsRateTypeVisible
        BlnIsBillingTypeVisible = IsBillingTypeVisible
    End Sub


    Private Sub FrmWorkOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, BlnIsItemCodeVisible, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1WorkOrder, 100, 0, Col1WorkOrder, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Supplier, 100, 0, Col1Supplier, True, True, False)
            .AddAgTextColumn(Dgl1, Col1WorkOrderSr, 100, 0, Col1WorkOrderSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, True, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 0, Col1BillingType, BlnIsBillingTypeVisible, True, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 70, 0, Col1RateType, BlnIsRateTypeVisible, True, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, BlnIsDeliveryMeasureVisible, False, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentQty, 80, 8, 0, False, Col1CurrentQty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 0, True, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, BlnIsMeasurePerPcsVisible, Not BlnIsMeasurePerPcsEditable, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 50, Col1MeasureUnit, BlnIsMeasureUnitVisible, Not BlnIsMeasureUnitEditable, False)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 80, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 100, 8, 4, False, Col1TotalDeliveryMeasure, BlnIsDeliveryMeasureVisible, True, True)
            .AddAgTextColumn(Dgl1, Col1GenDocID, 150, 0, Col1GenDocID, False, True, False)
            .AddAgTextColumn(Dgl1, Col1GenDocIDSr, 100, 0, Col1GenDocIDSr, False, True, False)
            .AddAgDateColumn(Dgl1, Col1WorkOrderDate, 100, Col1WorkOrderDate, True, True)
        End With

        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.ColumnHeadersHeight = 35

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1WorkOrder)


        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index

        FrmWorkOrder_BaseFunction_FIniList()
    End Sub


    Private Sub FrmWorkOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer, mWorkOrderSr As Integer
        Dim bSelectionQry$ = "", strWorkOrderQry As String = ""

        mQry = "UPDATE WorkOrder " & _
                "   SET " & _
                "   ManualrefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                "   Party = " & AgL.Chk_Text(TxtParty.AgSelectedValue) & ", " & _
                "	PartyName = " & AgL.Chk_Text(TxtParty.Text) & ", " & _
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.AgSelectedValue) & ", " & _
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                "	Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " & _
                "   TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                "   TotalAmount = " & Val(LblTotalAmount.Text) & ", " & _
                "   TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " & _
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar) + 1
                        mWorkOrderSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail With (NoLock) Where DocID = '" & Dgl1.Item(Col1WorkOrder, I).Tag & "'", AgL.GcnRead).ExecuteScalar) + 1

                        If Val(.Item(Col1WorkOrderSr, I).Value) = 0 Then
                            mQry = "INSERT INTO WorkOrderDetail(DocId, Sr, GenDocId, GenDocIdSr, Supplier, Item, Specification, WorksTaxGroupItem, RateType, Qty, " & _
                                      " Unit, Rate, Amount, " & _
                                      " MeasurePerPcs, TotalMeasure, MeasureUnit, DeliveryMeasure, DeliveryMeasureMultiplier, TotalDeliveryMeasure, " & _
                                      " WorkOrder, WorkOrderSr, BillingType) " & _
                                      " Values(" & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " & Val(mWorkOrderSr) & ", " & _
                                      " " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ", " & _
                                      " 0, " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                      " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                      " 0, " & _
                                      " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                      " 0, " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " & _
                                      " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                                      " 0, " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " & _
                                      " " & Val(mWorkOrderSr) & ", " & _
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ") "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            .Item(Col1WorkOrderSr, I).Value = mWorkOrderSr
                        End If

                        mQry = "INSERT INTO WorkOrderDetail(DocId, Sr, Supplier, Item, Specification, WorksTaxGroupItem, RateType, Qty, " & _
                              " Unit, CurrentQty, Rate, Amount, " & _
                              " MeasurePerPcs, TotalMeasure, MeasureUnit, DeliveryMeasure, DeliveryMeasureMultiplier, TotalDeliveryMeasure, " & _
                              " WorkOrder, WorkOrderSr, BillingType, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & _
                              " Values(" & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ", " & _
                              " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1CurrentQty, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                              " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1WorkOrder, I)) & ", " & _
                              " " & Val(Dgl1.Item(Col1WorkOrderSr, I).Value) & ", " & _
                              " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " & _
                              " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        If Dgl1.Rows(I).Visible = True Then
                            mQry = " Update WorkOrderDetail Set " & _
                                        " Supplier = " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & ", " & _
                                        " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                        " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                                        " WorksTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " & _
                                        " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ", " & _
                                        " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                                        " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                        " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                        " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                        " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                        " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                                        " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                        " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " & _
                                        " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                                        " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                                        " WorkOrder = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1WorkOrder, I)) & ", " & _
                                        " WorkOrderSr = " & Val(Dgl1.Item(Col1WorkOrderSr, I).Value) & ", " & _
                                        " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " & _
                                        " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " & _
                                        " Where DocId = '" & SearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = " Update WorkOrderDetail Set " & _
                                    " Supplier = " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & ", " & _
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                                    " WorksTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " & _
                                    " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ", " & _
                                    " Qty = 0, " & _
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                    " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                    " TotalMeasure = 0, " & _
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                    " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " & _
                                    " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                                    " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                                    " WorkOrder = " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1WorkOrder, I)) & ", " & _
                                    " WorkOrderSr = " & Val(Dgl1.Item(Col1WorkOrderSr, I).Value) & ", " & _
                                    " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " " & _
                                    " Where GenDocId = '" & SearchCode & "' And GenDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        Else
                            mQry = " Delete From WorkOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = " Delete From WorkOrderDetail Where GenDocId = '" & mSearchCode & "' And GenDocIDSr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet
        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0


        mQry = "Select H.*, So.ReferenceNo As WorkOrderRefNo " & _
                " From WorkOrder H " & _
                " LEFT JOIN WorkOrder So On H.WorkOrder = So.DocId " & _
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)


        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                IniGrid()

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("WorkToParty"))
                TxtParty.Text = AgL.XNull(.Rows(0)("WorkToPartyName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                'LblTotalQty.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalQty")))
                'LblTotalAmount.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalAmount")))
                'LblTotalMeasure.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalMeasure")))


                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "  Select L.*, I.ManualCode as ItemCode, I.Description As ItemDesc, So.V_Type + '-' + So.ReferenceNo As WorkOrderRefNo, Supplier.ManualCode as Supplier_Code,  " & _
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces, So.V_Date As WorkOrderDate " & _
                        " From WorkOrderDetail L " & _
                        " LEFT JOIN Item I ON L.Item = I.Code " & _
                        " Left Join Unit U ON I.Unit = U.Code " & _
                        " Left Join Unit MU ON I.MeasureUnit = MU.Code " & _
                        " Left Join Unit DMU ON L.DeliveryMeasure = DMU.Code " & _
                        " Left Join Subgroup Supplier On L.Supplier = Supplier.SubCode " & _
                        " LEFT JOIN WorkOrder So On L.WorkOrder = So.DocId " & _
                        " Where L.DocId = '" & SearchCode & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.VNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1GenDocID, I).Value = AgL.XNull(.Rows(I)("GenDocId"))
                            Dgl1.Item(Col1GenDocIDSr, I).Value = AgL.XNull(.Rows(I)("GenDocIdSr"))
                            Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(.Rows(I)("Supplier"))
                            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(.Rows(I)("Supplier_Code"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1CurrentQty, I).Value = Math.Abs(AgL.VNull(.Rows(I)("CurrentQty")))
                            Dgl1.Item(Col1Amount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("WorksTaxGroupItem"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                            Dgl1.Item(Col1WorkOrder, I).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                            Dgl1.Item(Col1WorkOrder, I).Value = AgL.XNull(.Rows(I)("WorkOrderRefNo"))
                            Dgl1.Item(Col1WorkOrderSr, I).Value = AgL.XNull(.Rows(I)("WorkOrderSr"))

                            Dgl1.Item(Col1WorkOrderDate, I).Value = AgL.XNull(.Rows(I)("WorkOrderDate"))

                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))

                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))


                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                            If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)


                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                            LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If

                    If IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
                    If IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
                    If IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"

                End With

                LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
                LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
                LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
                LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
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
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    IniGrid()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.Tag
        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        RbtQtyAmendment.Checked = True
        TxtV_Date.Focus()
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        'TxtParty.AgHelpDataSet(11, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.WorkToParty
        'Dgl1.AgHelpDataSet(Col1Item, 17) = HelpDataSet.Item
        'TxtPartyCity.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.City
        'TxtStructure.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.AgStructure
        'TxtSalesTaxGroupParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.WorksTaxGroupParty
        'TxtStatus.AgHelpDataSet(0, GroupBox2.Top - 150, GroupBox2.Left) = HelpDataSet.Status
        'Dgl1.AgHelpDataSet(Col1Design) = HelpDataSet.Design
        'Dgl1.AgHelpDataSet(Col1Size) = HelpDataSet.Size
        'Dgl1.AgHelpDataSet(Col1WorkOrder) = HelpDataSet.WorkOrder
        'TxtWorkOrder.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.WorkOrder
    End Sub

    Private Sub TxtParty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown
        Try


            Select Case sender.Name
                Case TxtParty.Name
                    If TxtParty.AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.DispName AS [Name], C.CityName AS [City], C.State, C.Country, C.CityCode, " & _
                                    " Sg.Add1, Sg.Add2, Sg.Add3, Sg.Currency, Sg.WorksTaxPostingGroup   " & _
                                    " FROM SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " & _
                                    " Where Sg.MasterType = '" & AgTemplate.ClsMain.SubgroupType.Customer & "' " & _
                                    " And IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                            TxtParty.AgHelpDataSet(9, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                    'Case TxtWorkOrder.Name
                    '    If TxtWorkOrder.AgHelpDataSet Is Nothing Then
                    '        If e.KeyCode <> Keys.Enter Then
                    '            mQry = " SELECT L.WorkOrder AS Code, Max(H.V_Type + '-' + H.ReferenceNo) AS WorkOrderNo, Max(H.PartyOrderNo) as [Party Order No.], " & _
                    '                    " Sum(L.Qty) - IsNull(Max(VChallan.ChallanQty),0) AS BalQty " & _
                    '                    " FROM WorkOrderDetail L  " & _
                    '                    " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID " & _
                    '                    " Left Join " & _
                    '                    " 	(SELECT L.WorkOrder, Sum(L.Qty) AS ChallanQty " & _
                    '                    " 	 FROM WorkChallanDetail L  " & _
                    '                    " 	 GROUP BY L.WorkOrder) AS VChallan  " & _
                    '                    " ON L.WorkOrder = VChallan.WorkOrder " & _
                    '                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    '                    " And H.WorkToParty = '" & TxtParty.Tag & "'" & _
                    '                    " GROUP BY L.WorkOrder " & _
                    '                    "  "
                    '            TxtWorkOrder.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    '        End If
                    '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingWorkOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, L.WorkOrder AS Code, Max(H.V_Type + '-' + H.ReferenceNo) AS WorkOrderNo, " & _
                " Sum(L.Qty) AS BalQty " & _
                " FROM WorkOrderDetail L  " & _
                " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID " & _
                " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                " And H.WorkToParty = '" & TxtParty.Tag & "'" & _
                " GROUP BY L.WorkOrder "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 600, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingWorkOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub TxtParty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtParty.Validating, TxtV_Date.Validating
        Select Case sender.name
            Case TxtParty.Name

        End Select
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer, ByVal bFillArea As Boolean)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim mQry As String
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
                Dgl1.Item(Col1WorkOrder, mRow).Tag = ""
                Dgl1.Item(Col1WorkOrder, mRow).Value = ""
                Dgl1.Item(Col1WorkOrderSr, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = ""
            Else
                If RbtQtyAmendment.Checked Then
                    If Dgl1.AgDataRow IsNot Nothing Then
                        mQry = "SELECT L.WorksTaxGroupItem, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.BillingType, L.Rate, " & _
                               "L.DeliveryMeasure, L.Supplier, L.DeliveryMeasureMultiplier,L.RateType, " & _
                               "Supplier.ManualCode as Supplier_Code,  " & _
                               "U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces " & _
                               "FROM WorkOrderDetail L With (NoLock) " & _
                               "Left Join Item I With (NoLock) On L.Item = I.Code " & _
                               "Left Join Unit U ON I.Unit = U.Code " & _
                               "Left Join Unit MU ON I.MeasureUnit = MU.Code " & _
                               "Left Join Unit DMU ON L.DeliveryMeasure = DMU.Code " & _
                               "Left Join Subgroup Supplier On L.Supplier = Supplier.SubCode " & _
                               "Where L.DocID = '" & AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrder").Value) & "' And L.Sr = " & AgL.VNull(Dgl1.AgDataRow.Cells("WorkOrderSr").Value) & "  "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)

                        Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("DeliveryMeasureDecimalPlaces"))

                        Dgl1.Item(Col1Supplier, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Supplier"))
                        Dgl1.Item(Col1Supplier, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Supplier_Code"))
                        Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                        Dgl1.Item(Col1CurrentQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                        Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                        Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs"))
                        Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                        Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BillingType"))
                        Dgl1.Item(Col1RateType, mRow).Value = AgL.XNull(DtTemp.Rows(0)("RateType"))
                        Dgl1.Item(Col1WorkOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrder").Value)
                        Dgl1.Item(Col1WorkOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderRefNo").Value)
                        Dgl1.Item(Col1WorkOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderSr").Value)

                        Dgl1.Item(Col1WorkOrderDate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderDate").Value)

                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("WorksTaxGroupItem"))
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(DtTemp.Rows(0)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.XNull(DtTemp.Rows(0)("DeliveryMeasureMultiplier"))
                    End If
                Else
                    mQry = " SELECT I.BillingOn As BillingType, I.Unit, I.Measure As MeasurePerPcs, I.MeasureUnit, " & _
                            " I.WorksTaxPostingGroup As WorksTaxGroupItem, I.Rate, " & _
                            " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, I.Specification   " & _
                            " FROM Item I  " & _
                            " Left Join Unit U ON I.Unit = U.Code  " & _
                            " Left Join Unit MU ON I.MeasureUnit = MU.Code  " & _
                            " WHERE I.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)

                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))

                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Specification"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                    Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BillingType"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("WorksTaxGroupItem"))

                End If
            End If
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure, Col1TotalDeliveryMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1WorkOrder
                    If Dgl1.Item(Col1WorkOrderSr, Dgl1.CurrentCell.RowIndex).Value = "" Then
                        Dgl1.Columns(Col1WorkOrder).ReadOnly = False
                    Else
                        Dgl1.Columns(Col1WorkOrder).ReadOnly = True
                    End If

                Case Col1Supplier
                    If Dgl1.Item(Col1WorkOrderSr, Dgl1.CurrentCell.RowIndex).Value = "" Then
                        Dgl1.Columns(Col1Supplier).ReadOnly = False
                    Else
                        Dgl1.Columns(Col1Supplier).ReadOnly = True
                    End If

                Case Col1BillingType
                    If Dgl1.Item(Col1WorkOrderSr, Dgl1.CurrentCell.RowIndex).Value = "" Then
                        Dgl1.Columns(Col1BillingType).ReadOnly = False
                    Else
                        Dgl1.Columns(Col1BillingType).ReadOnly = True
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

                Case Col1WorkOrder
                    If Dgl1.Item(Col1WorkOrder, mRowIndex).Value.ToString.Trim = "" Or Dgl1.Item(Col1WorkOrder, mRowIndex).Tag.ToString.Trim = "" Then
                        Dgl1.Item(Col1Specification, mRowIndex).Value = ""
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1WorkOrderDate, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderDate").Value)
                        End If
                    End If
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
        Dim I As Integer

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0


        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.0000")
                Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                Else : AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Area")
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If


                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)


                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
        LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")


        If IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
        If IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If .Item(Col1WorkOrder, I).Value = "" Then
                        MsgBox("Work Order Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1WorkOrder, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If TxtV_Date.Text <> "" And Dgl1.Item(Col1WorkOrderDate, I).Value <> "" Then
                        If CDate(TxtV_Date.Text) < CDate(Dgl1.Item(Col1WorkOrderDate, I).Value) Then
                            MsgBox("Order Date Of " & Dgl1.Item(Col1WorkOrder, I).Value & " Is After Then " & TxtV_Date.Text & " At Row No " & Dgl1.Item(ColSNo, I).Value & ".Can't Amend It.", MsgBoxStyle.Information)
                            .CurrentCell = .Item(Col1WorkOrder, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
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

    Private Sub FillWorkOrderDetail(ByVal WorkOrder As String)
        Dim I As Integer
        Dim DsTemp As DataSet
        Try
            mQry = "Select H.*, Sg.WorksTaxPostingGroup " & _
                    " From WorkOrder H " & _
                    " LEFT JOIN SubGroup Sg On H.WorkToParty = Sg.SubCode  " & _
                    " Where H.DocID In (" & WorkOrder & ") "
            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtParty.Tag = AgL.XNull(.Rows(0)("WorkToParty"))
                    TxtParty.Text = AgL.XNull(.Rows(0)("WorkToPartyName"))
                    TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("WorksTaxPostingGroup"))
                    TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                    LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                    LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                    LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))


                    mQry = " SELECT Max(I.Code) AS Item, " & _
                            " Max(I.Description) AS ItemDesc, L.WorkOrder,  " & _
                            " Max(H.V_Type + '-' + H.ReferenceNo) AS WorkOrderRefNo, " & _
                            " Sum(l.Qty) AS CurrentQty, Max(L.Unit) AS Unit,   " & _
                            " Max(L.Rate) AS Rate, Max(L.Amount) AS Amount, Max(L.Specification) AS Specification,   " & _
                            " Max(L.PartySKU) AS PartySKU, Max(L.PartyUPC) AS PartyUPC,    " & _
                            " Max(L.WorksTaxGroupItem) AS WorksTaxGroupItem, Max(L.Supplier) as Supplier, Max(Supplier.ManualCode) as Supplier_Code,    " & _
                            " Max(L.MeasurePerPcs) AS MeasurePerPcs, Max(L.MeasureUnit) AS MeasureUnit,    " & _
                            " Max(L.BillingType) AS BillingType, Max(L.RateType) as RateType, " & _
                            " Max(L.DeliveryMeasure) as DeliveryMeasure, Max(L.DeliveryMeasureMultiplier) as DeliveryMeasureMultiplier, " & _
                            " L.WorkOrder As DocId, L.WorkOrderSr As Sr   " & _
                            " FROM WorkOrderDetail L    " & _
                            " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID   " & _
                            " LEFT JOIN Item I ON L.Item = I.Code   " & _
                            " Left Join Subgroup Supplier On L.Supplier = Supplier.SubCode " & _
                            " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                            " And H.DocId In (" & WorkOrder & ")  " & _
                            " GROUP BY L.WorkOrder, L.WorkOrderSr   " & _
                            " Having Sum(l.Qty)  > 0 "
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
                                Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(.Rows(I)("Supplier"))
                                Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(.Rows(I)("Supplier_Code"))
                                Dgl1.Item(Col1WorkOrder, I).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                                Dgl1.Item(Col1WorkOrder, I).Value = AgL.XNull(.Rows(I)("WorkOrderRefNo"))
                                Dgl1.Item(Col1WorkOrderSr, I).Value = AgL.XNull(.Rows(I)("Sr"))
                                Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                                Dgl1.Item(Col1CurrentQty, I).Value = AgL.VNull(.Rows(I)("CurrentQty"))
                                Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                                Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                                Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                                Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("WorksTaxGroupItem"))
                                Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                                Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                                Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                                Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasureMultiplier"))
                                Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                                Dgl1.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("RateType"))


                                AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("DocId")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
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

    Private Sub FrmGoodsReceipt_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT  H.DocID, H.V_Type + ' - ' + Convert(NVARCHAR(5),H.V_No) AS VoucherNo, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code,  " & _
                 " H.WorkToParty, H.WorkToPartyName, H.WorkToPartyAdd1, H.WorkToPartyAdd2, H.WorkToPartyCity, H.WorkToPartyCityName, H.WorkToPartyState,  " & _
                 " H.WorkToPartyCountry,H.PartyOrderNo,  " & _
                 " H.PriceMode, H.WorksTaxGroupParty,SG.Mobile,SG.EMail, CASE WHEN H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' THEN '' ELSE H.Status END AS Status , " & _
                 " H.Currency , H.TermsAndConditions, H.Remarks, H.TotalQty, H.TotalMeasure,  " & _
                 " H.EntryBy, H.EntryDate,H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.UID,  L.SR, " & _
                 " H.TotalAmount,  H.IsDeleted, L.Item, Abs(L.Qty) As Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " & _
                 " L.TotalMeasure AS LineTotalMeasure, L.Rate, Abs(L.Amount) As Amount , I.Description AS ItemDesc, SM.Name AS SiteName , L.Specification ," & _
                 " isnull(V1.ChallanQty,0) AS ChallanQty, Sg.TinNO As WorkToPartyTinNo, So.PartyOrderNo As WorkOrderManualNo, H.ReferenceNo, " & _
                 " " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & " " & _
                 " FROM WorkOrder H " & _
                 " LEFT JOIN WorkOrderDetail L On H.DocId = L.DocId  " & _
                 " LEFT JOIN Item I ON I.Code=L.Item    " & _
                 " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                 " LEFT JOIN Voucher_Type Vt ON Vt.V_Type= H.V_Type   " & _
                 " LEFT JOIN SubGroup SG ON SG.SubCode= H.WorkToParty " & _
                 " LEFT JOIN WorkOrder So ON L.WorkOrder = So.DocId " & _
                 " Left Join ( " & _
                 "       SELECT PC.WorkOrder,PC.Item,sum(PC.Qty) AS ChallanQty    " & _
                 "       FROM WorkChallanDetail PC   " & _
                 "       WHERE PC.WorkOrder Is Not NULL " & _
                 "       GROUP BY PC.WorkOrder,PC.Item    " & _
                 " )V1 ON V1.WorkOrder=L.DocId AND V1.Item=L.Item " & _
                 " WHERE H.DocID='" & mInternalCode & "'"

        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "RUG_WorkOrderAmendment_Print", "Work Order Amendment")
    End Sub

    'Private Sub FrmWorkOrder_YarnSku_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
    '    Dim mCrd As New ReportDocument
    '    Dim ReportView As New AgLibrary.RepView
    '    Dim DsRep As New DataSet
    '    Dim DsRep1 As New DataSet
    '    Dim DsRep2 As New DataSet
    '    Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
    '    Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = "", bThirdTableName As String = ""
    '    Dim bStructJoin As String = ""
    '    Dim bSubQry As String = ""

    '    Try
    '        Me.Cursor = Cursors.Default

    '        AgL.PubReportTitle = "Work Order Cancel"
    '        RepName = "RUG_WorkOrderCancel_Print" : RepTitle = "Work Order Cancel"

    '        mQry = " SELECT  H.DocID, H.V_Type + ' - ' +convert(NVARCHAR(5),H.V_No) AS VoucherNo, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code,  " & _
    '                 " H.WorkToParty, H.WorkToPartyName, H.WorkToPartyAdd1, H.WorkToPartyAdd2, H.WorkToPartyCity, H.WorkToPartyCityName, H.WorkToPartyState,  " & _
    '                 " H.WorkToPartyCountry,H.PartyOrderNo,  " & _
    '                 " H.PriceMode, H.WorksTaxGroupParty,SG.Mobile,SG.EMail, CASE WHEN H.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' THEN '' ELSE H.Status END AS Status , " & _
    '                 " H.Currency , H.TermsAndConditions, H.Remarks, H.TotalQty, H.TotalMeasure,  " & _
    '                 " H.EntryBy, H.EntryDate,H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.UID,  L.SR, " & _
    '                 " H.TotalAmount,  H.IsDeleted, L.Item, Abs(L.Qty) As Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " & _
    '                 " L.TotalMeasure AS LineTotalMeasure, L.Rate, Abs(L.Amount) As Amount , I.Description AS ItemDesc, SM.Name AS SiteName , L.Specification ," & _
    '                 " isnull(V1.ChallanQty,0) AS ChallanQty, Sg.TinNO As WorkToPartyTinNo, So.PartyOrderNo As WorkOrderManualNo, H.ReferenceNo, " & _
    '                 " " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & " " & _
    '                 " FROM WorkOrder H " & _
    '                 " LEFT JOIN WorkOrderDetail L On H.DocId = L.DocId  " & _
    '                 " LEFT JOIN Item I ON I.Code=L.Item    " & _
    '                 " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
    '                 " LEFT JOIN Voucher_Type Vt ON Vt.V_Type= H.V_Type   " & _
    '                 " LEFT JOIN SubGroup SG ON SG.SubCode= H.WorkToParty " & _
    '                 " LEFT JOIN WorkOrder So ON L.WorkOrder = So.DocId " & _
    '                 " Left Join ( " & _
    '                 "       SELECT PC.WorkOrder,PC.Item,sum(PC.Qty) AS ChallanQty    " & _
    '                 "       FROM WorkChallanDetail PC   " & _
    '                 "       WHERE PC.WorkOrder Is Not NULL " & _
    '                 "       GROUP BY PC.WorkOrder,PC.Item    " & _
    '                 " )V1 ON V1.WorkOrder=L.DocId AND V1.Item=L.Item " & _
    '                 " WHERE H.DocID='" & mInternalCode & "'"
    '        AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GCn)
    '        AgL.ADMain.Fill(DsRep)

    '        AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
    '        mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
    '        mCrd.SetDataSource(DsRep.Tables(0))

    '        CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
    '        AgPL.Formula_Set(mCrd, RepTitle)
    '        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

    '        Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub TxtVendor_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtParty.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.Name
                Case TxtParty.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(DrTemp(0)("WorksTaxPostingGroup"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub BtnFillWorkChallan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Topctrl1.Mode = "Browse" Then Exit Sub
    '        Dim StrTicked As String

    '        StrTicked = FHPGD_PendingWorkOrder()
    '        If StrTicked <> "" Then
    '            FillWorkOrderDetail(StrTicked)
    '        Else
    '            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '        End If
    '        Dgl1.Focus()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
    '    FillWorkOrderDetail(TxtWorkOrder.AgSelectedValue)
    '    Dgl1.Focus()
    'End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            If RbtQtyAmendment.Checked Then
                                mQry = " SELECT Max(I.Code) AS Code, " & _
                                        " Max(I.Description) AS Item, Max(L.Specification) AS Specification, " & _
                                        " Max(H.V_Type + '-' + H.ReferenceNo) AS WorkOrderRefNo, Max(H.PartyOrderNo) as [Party Order No], Max(Supplier.ManualCode) as [Supplier Code], " & _
                                        " Sum(l.Qty) AS BalQty, Max(L.Unit) AS Unit, " & _
                                        " L.WorkOrder, L.WorkOrderSr, Max(H.V_Date) As WorkOrderDate " & _
                                        " FROM WorkOrderDetail L  " & _
                                        " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID " & _
                                        " LEFT JOIN Item I ON L.Item = I.Code " & _
                                        " Left Join SubGroup Supplier On L.Supplier = Supplier.Subcode " & _
                                        " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                                        " And H.WorkToParty = '" & TxtParty.Tag & "' " & _
                                        " GROUP BY L.WorkOrder, L.WorkOrderSr "
                                Dgl1.AgHelpDataSet(Col1Item, 3) = AgL.FillData(mQry, AgL.GCn)
                            Else
                                mQry = " SELECT I.Code, I.Description FROM Item I " & _
                                        " WHERE IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                                Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)

                                Dgl1.Columns(Col1WorkOrder).ReadOnly = False
                                Dgl1.Columns(Col1Supplier).ReadOnly = False
                            End If
                        End If
                    End If

                Case Col1WorkOrder
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1WorkOrder) Is Nothing Then
                            mQry = " SELECT H.DocID AS Code, H.V_Type + '-' + H.ReferenceNo AS WorkOrderNo, H.V_Date As WorkOrderDate FROM WorkOrder H  "
                            Dgl1.AgHelpDataSet(Col1WorkOrder) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1BillingType
                    If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                        mQry = " Select 'Qty' As Code, 'Qty' As Description " & _
                                " UNION All " & _
                                " Select 'Measure' As Code, 'Measure' As Description "
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Supplier
                    If Dgl1.AgHelpDataSet(Col1Supplier) Is Nothing Then
                        mQry = "SELECT SubCode AS Code, Sg.ManualCode As Supplier  " & _
                                " FROM SubGroup Sg " & _
                                " LEFT JOIN City C On Sg.CityCode = C.CityCode " & _
                                " Where IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1Supplier) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFillWorkChallan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillWorkOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            StrTicked = FHPGD_PendingWorkOrder()
            If StrTicked <> "" Then
                FillWorkOrderDetail(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RbtAddNewItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtAddNewItem.Click, RbtQtyAmendment.Click
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub FrmWorkOrderAmendment_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1WorkOrder) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1WorkOrder) = Nothing
        If Dgl1.AgHelpDataSet(Col1Supplier) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Supplier) = Nothing
        If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet = Nothing
    End Sub

    Private Sub FrmWorkOrderAmendment_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        RbtQtyAmendment.Checked = True
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1WorkOrder
                    Call AgTemplate.ClsMain.ProcOpenLinkForm(Mdi.MnuWorkOrder, Dgl1.Item(Col1WorkOrder, e.RowIndex).Tag, Me.MdiParent)
            End Select
        Catch ex As Exception
        End Try
    End Sub
End Class

