Imports System.IO
Public Class FrmStoreReceive
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1ItemGroup As String = "Item Group"

    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"

    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1Manufacturer As String = "Manufacturer"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1ProdOrder As String = "Prod. Order"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remarks As String = "Remarks"
    Protected WithEvents LblCurrentStock As System.Windows.Forms.Label
    Protected WithEvents LblCurrentStockText As System.Windows.Forms.Label
    Protected WithEvents ChkShowOnlyImportedRecords As System.Windows.Forms.CheckBox
    Protected WithEvents BtnImprtFromText As System.Windows.Forms.Button

    Dim BlnIsLotNoVisible As Boolean = False
    Dim ImportMessegeStr$ = ""
    Dim ImportMode As Boolean = False
    Dim ImportAction_NewImport As String = "New Import"
    Protected WithEvents TxtReason As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Dim ImportAction_ClearImport As String = "Clear Import"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtV_TypeSettings.Rows.Count = 0 Then
            MsgBox("Voucher Type Settings are not defined. Can't Continue!")
        End If

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrentStock = New System.Windows.Forms.Label
        Me.LblCurrentStockText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblFromGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblReq_SubCode = New System.Windows.Forms.Label
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ChkShowOnlyImportedRecords = New System.Windows.Forms.CheckBox
        Me.BtnImprtFromText = New System.Windows.Forms.Button
        Me.TxtReason = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
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
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(733, 531)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 531)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 531)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 531)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 531)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 527)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 531)
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
        Me.LblV_No.Location = New System.Drawing.Point(348, 232)
        Me.LblV_No.Size = New System.Drawing.Size(78, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Transfer No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(470, 231)
        Me.TxtV_No.Size = New System.Drawing.Size(217, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(295, 45)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(170, 40)
        Me.LblV_Date.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Receive Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(525, 25)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(313, 39)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(439, 21)
        Me.LblV_Type.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Receive Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(541, 19)
        Me.TxtV_Type.Size = New System.Drawing.Size(187, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(295, 25)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(170, 20)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(313, 19)
        Me.TxtSite_Code.Size = New System.Drawing.Size(120, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(711, 192)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-9, 5)
        Me.TabControl1.Size = New System.Drawing.Size(907, 173)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtReason)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.LblReq_SubCode)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblFromGodownReq)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(899, 147)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReq_SubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReason, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(889, 41)
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
        'TxtGodown
        '
        Me.TxtGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtGodown.AgLastValueTag = Nothing
        Me.TxtGodown.AgLastValueText = Nothing
        Me.TxtGodown.AgMandatory = True
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 8
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 2
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(541, 79)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(187, 18)
        Me.TxtGodown.TabIndex = 6
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(440, 79)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblCurrentStock)
        Me.Panel1.Controls.Add(Me.LblCurrentStockText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(5, 505)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblCurrentStock
        '
        Me.LblCurrentStock.AutoSize = True
        Me.LblCurrentStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCurrentStock.Location = New System.Drawing.Point(133, 3)
        Me.LblCurrentStock.Name = "LblCurrentStock"
        Me.LblCurrentStock.Size = New System.Drawing.Size(12, 16)
        Me.LblCurrentStock.TabIndex = 668
        Me.LblCurrentStock.Text = "."
        Me.LblCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCurrentStockText
        '
        Me.LblCurrentStockText.AutoSize = True
        Me.LblCurrentStockText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStockText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCurrentStockText.Location = New System.Drawing.Point(25, 3)
        Me.LblCurrentStockText.Name = "LblCurrentStockText"
        Me.LblCurrentStockText.Size = New System.Drawing.Size(102, 16)
        Me.LblCurrentStockText.TabIndex = 667
        Me.LblCurrentStockText.Text = "Current Stock :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(759, 3)
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
        Me.Label33.Location = New System.Drawing.Point(648, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(451, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(366, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 212)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(880, 291)
        Me.Pnl1.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(170, 120)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
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
        Me.TxtRemarks.Location = New System.Drawing.Point(313, 119)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(415, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'LblFromGodownReq
        '
        Me.LblFromGodownReq.AutoSize = True
        Me.LblFromGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFromGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFromGodownReq.Location = New System.Drawing.Point(525, 86)
        Me.LblFromGodownReq.Name = "LblFromGodownReq"
        Me.LblFromGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFromGodownReq.TabIndex = 724
        Me.LblFromGodownReq.Text = "�"
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgLastValueTag = Nothing
        Me.TxtManualRefNo.AgLastValueText = Nothing
        Me.TxtManualRefNo.AgMandatory = True
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(541, 39)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(187, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(439, 39)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(77, 16)
        Me.LblManualRefNo.TabIndex = 731
        Me.LblManualRefNo.Text = "Receive No."
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(4, 187)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(111, 19)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 804
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Received Items"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(525, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 732
        Me.Label1.Text = "�"
        '
        'LblReq_SubCode
        '
        Me.LblReq_SubCode.AutoSize = True
        Me.LblReq_SubCode.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReq_SubCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReq_SubCode.Location = New System.Drawing.Point(295, 66)
        Me.LblReq_SubCode.Name = "LblReq_SubCode"
        Me.LblReq_SubCode.Size = New System.Drawing.Size(10, 7)
        Me.LblReq_SubCode.TabIndex = 735
        Me.LblReq_SubCode.Text = "�"
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
        Me.TxtParty.Location = New System.Drawing.Point(313, 59)
        Me.TxtParty.MaxLength = 20
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(415, 18)
        Me.TxtParty.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(170, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 16)
        Me.Label4.TabIndex = 734
        Me.Label4.Text = "Receive From (A/c)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(295, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 738
        Me.Label3.Text = "�"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = Nothing
        Me.TxtProcess.AgMandatory = True
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 8
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 2
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(313, 79)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(121, 18)
        Me.TxtProcess.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(170, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 737
        Me.Label5.Text = "Process"
        '
        'ChkShowOnlyImportedRecords
        '
        Me.ChkShowOnlyImportedRecords.AutoSize = True
        Me.ChkShowOnlyImportedRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowOnlyImportedRecords.Location = New System.Drawing.Point(589, 188)
        Me.ChkShowOnlyImportedRecords.Name = "ChkShowOnlyImportedRecords"
        Me.ChkShowOnlyImportedRecords.Size = New System.Drawing.Size(214, 17)
        Me.ChkShowOnlyImportedRecords.TabIndex = 810
        Me.ChkShowOnlyImportedRecords.Text = "Show Only Imported Records"
        Me.ChkShowOnlyImportedRecords.UseVisualStyleBackColor = True
        '
        'BtnImprtFromText
        '
        Me.BtnImprtFromText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprtFromText.Location = New System.Drawing.Point(813, 181)
        Me.BtnImprtFromText.Name = "BtnImprtFromText"
        Me.BtnImprtFromText.Size = New System.Drawing.Size(70, 25)
        Me.BtnImprtFromText.TabIndex = 809
        Me.BtnImprtFromText.TabStop = False
        Me.BtnImprtFromText.Text = "Import"
        Me.BtnImprtFromText.UseVisualStyleBackColor = True
        '
        'TxtReason
        '
        Me.TxtReason.AgAllowUserToEnableMasterHelp = False
        Me.TxtReason.AgLastValueTag = Nothing
        Me.TxtReason.AgLastValueText = Nothing
        Me.TxtReason.AgMandatory = False
        Me.TxtReason.AgMasterHelp = False
        Me.TxtReason.AgNumberLeftPlaces = 8
        Me.TxtReason.AgNumberNegetiveAllow = False
        Me.TxtReason.AgNumberRightPlaces = 2
        Me.TxtReason.AgPickFromLastValue = False
        Me.TxtReason.AgRowFilter = ""
        Me.TxtReason.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReason.AgSelectedValue = Nothing
        Me.TxtReason.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReason.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReason.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReason.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReason.Location = New System.Drawing.Point(313, 99)
        Me.TxtReason.MaxLength = 100
        Me.TxtReason.Name = "TxtReason"
        Me.TxtReason.Size = New System.Drawing.Size(415, 18)
        Me.TxtReason.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(170, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 743
        Me.Label6.Text = "Reason"
        '
        'FrmStoreReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(889, 572)
        Me.Controls.Add(Me.ChkShowOnlyImportedRecords)
        Me.Controls.Add(Me.BtnImprtFromText)
        Me.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmStoreReceive"
        Me.Text = "Material Issue from Store Entry"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblMaterialPlanForFollowingItems, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.BtnImprtFromText, 0)
        Me.Controls.SetChildIndex(Me.ChkShowOnlyImportedRecords, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblFromGodownReq As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LblReq_SubCode As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label5 As System.Windows.Forms.Label

#End Region

    Private Sub FrmStoreReceive_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_UID, I).Tag, mSearchCode, Conn, Cmd)
            End If
        Next


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Delete from JobIssRecUid Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobIssRecUid Set JobRecDocID = Null Where JobRecDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock"
        MainLineTableCsv = "Stock,StockHeadDetail"
        LogLineTableCsv = "Stock_LOG,StockHeadDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Issue_Type], H.V_Date AS Date, " & _
                " H.ManualRefNo, Sg.Name + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, " & _
                " GF.Description as To_Godown, H.Remarks,   P.Description as Process, " & _
                " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type], H.ApproveBy as Approved_By, H.ApproveDate as Approve_Date  " & _
                " FROM  StockHead H With (NoLock)  " & _
                " Left Join Subgroup Sg With (NoLock) on H.SubCode = Sg.SubCode " & _
                " Left Join City C With (NoLock) on Sg.CityCode = C.CityCode " & _
                " LEFT JOIN Division D With (NoLock) ON D.Div_Code=H.Div_Code  " & _
                " LEFT JOIN Process P ON H.Process=P.NCat  " & _
                " LEFT JOIN SiteMast SM With (NoLock) ON SM.Code=H.Site_Code  " & _
                " LEFT JOIN Voucher_Type Vt With (NoLock) ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Godown GF With (NoLock) ON GF.Code = H.FromGodown  " & _
                " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$ = ""
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = " Select H.DocID As SearchCode " & _
            " From StockHead H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date, H.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, True)

            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)

            .AddAgTextColumn(Dgl1, Col1Specification, 100, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Manufacturer, 100, 0, Col1Manufacturer, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Manufacturer")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 100, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 4, False, Col1CurrentStock, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Remarks, 250, 0, Col1Remarks, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead " & _
                " SET " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " SubCode = " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                " FromGodown = " & AgL.Chk_Text(TxtGodown.Tag) & ", " & _
                " Reason = " & AgL.Chk_Text(TxtReason.Tag) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            mQry = " SELECT Item_UID FROM StockHeadDetail With (NoLock) WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
            Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtItem_Uid.Rows.Count > 0 Then
                For I = 0 To DtItem_Uid.Rows.Count - 1
                    AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
                Next
            End If
        End If

        mQry = "Delete From StockHeadDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                mQry = " INSERT INTO dbo.StockHeadDetail ( DocID, Sr, Item_UID, Item, Dimension1, Dimension2, Specification, Manufacturer, LotNo, BaleNo, Godown, Qty, Unit, " & _
                        " MeasurePerPcs, TotalMeasure, MeasureUnit, Rate, Amount, Remarks, Process, " & _
                        " CurrentStock ) " & _
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " " & mSr & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Manufacturer, I).Tag) & ",  " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                        " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                'mQry = "INSERT INTO Stock(DocId, Sr, V_Type, V_Prefix, " & _
                '        " V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                '        " Item_UID, Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Rec, Unit, " & _
                '        " MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
                '        " Rate, Amount, Cost, LotNo, BaleNo, Process, Remarks,CurrentStock, " & _
                '        " ReferenceDocId, ReferenceDocIdSr) " & _
                '        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                '        " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                '        " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                '        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & _
                '        " " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Manufacturer, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                '        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                '        " " & AgL.Chk_Text(IIf(TxtProcess.Text = "", Dgl1.Item(Col1Process, I).Tag, TxtProcess.Tag)) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ") "




                'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
                'mQry = "INSERT INTO StockProcess (DocId, Sr, V_Type, V_Prefix, " & _
                '        " V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                '        " Item_UID, Item, Dimension1, Dimension2, Godown, Qty_Iss, Unit, " & _
                '        " MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
                '        " Rate, Amount, Cost, LotNo, BaleNo, Process, Remarks,CurrentStock, " & _
                '        " ReferenceDocId, ReferenceDocIdSr) " & _
                '        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                '        " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                '        " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                '        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & _
                '        " " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                '        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                '        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                '        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " & _
                '        " " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ") "

                'End If
            End If
        Next

        mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Rec, Unit,  MeasurePerPcs, Measure_Rec, MeasureUnit,  Rate, Amount, Landed_Value, EType_IR, " & _
                " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
                " SELECT H.DocID, row_number() OVER (ORDER BY L.Item), max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
                " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.FromGodown) AS Godown, " & _
                " sum(L.Qty) AS Qty_Rec, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Rec, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
                " sum(L.Amount) AS Amount, sum(L.Amount) AS Amount, 'R', max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, L.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
                " FROM StockHeadDetail L " & _
                " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
                " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
                " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, L.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = " INSERT INTO StockProcess (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                    " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Iss, Unit,  MeasurePerPcs, Measure_Iss, MeasureUnit,  Rate, Amount, " & _
                    " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
                    " SELECT H.DocID, row_number() OVER (ORDER BY L.Item), max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
                    " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.FromGodown) AS Godown, " & _
                    " sum(L.Qty) AS Qty_Iss, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Iss, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
                    " sum(L.Amount) AS Amount, max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, H.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
                    " FROM StockHeadDetail L " & _
                    " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
                    " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
                    " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, H.Process "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        Call FPostInJobIssRecUID(SearchCode, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_UID, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, TxtParty.Tag, TxtGodown.Tag, TxtProcess.Tag, AgTemplate.ClsMain.Item_UidStatus.Receive, TxtManualRefNo.Text, Conn, Cmd)
            End If
        Next

        If ImportMode = True Then
            mQry = " UPDATE StockHead Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, JobRecDocID, Item, Item_UID, " & _
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate) " & _
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'R', H.Process, JIR.DocId, L.Item, L.Item_Uid, " & _
                 " H.FromGodown, H.Site_Code, H.V_Date, H.V_Type, H.SubCode, H.Div_Code, H.ManualRefNo, H.EntryDate " & _
                 " From (Select * From StockHeadDetail With (NoLock) Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " & _
                 " LEFT JOIN StockHead H With (NoLock) On L.DocId = H.DocId " & _
                 " LEFT JOIN JobIssRecUID JIR ON JIR.Item_UID = L.Item_Uid  AND JobRecDocID IS NULL "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update JobIssRecUID " & _
                " SET JobRecDocID = " & AgL.Chk_Text(mInternalCode) & " " & _
                " WHERE JobRecDocID Is Null " & _
                " And Item_UID In (Select Item_UID From StockHeadDetail With (NoLock) Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) " & _
                " And Process = '" & TxtProcess.Tag & "' " & _
                " AND ISSREC = 'I' " & _
                " And Site_Code = '" & AgL.PubSiteCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, G.Description as FromGodownDesc, P.Description as ProcessDesc, R.Description As ReasonDesc, " & _
               " Sg.Name + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName " & _
                " From StockHead H " & _
                " Left Join Godown G on H.FromGodown = G.Code " & _
                " Left Join Subgroup Sg on H.SubCode = Sg.SubCode " & _
                " Left Join City C on Sg.CityCode = C.CityCode " & _
                " Left Join Reason R on H.Reason = R.Code " & _
                " Left Join Process P on H.Process = P.NCat " & _
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtGodown.Tag = AgL.XNull(.Rows(0)("FromGodown"))
                TxtGodown.Text = AgL.XNull(.Rows(0)("FromGodownDesc"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtReason.Tag = AgL.XNull(.Rows(0)("Reason"))
                TxtReason.Text = AgL.XNull(.Rows(0)("ReasonDesc"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select S.Item_UID, S.Item, S.Specification, S.Qty, S.Unit, S.MeasurePerPcs, " & _
                       "S.TotalMeasure, S.MeasureUnit, S.Rate, S.Amount, S.LotNo, S.BaleNo, S.Process, " & _
                       "S.Remarks, S.CurrentStock, S.Manufacturer, Iu.Item_Uid As Item_UidDesc, " & _
                       "S.Dimension1, S.Dimension2, " & _
                       "I.ManualCode as Item_No, I.Description as Item_Desc, " & _
                       "U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, IG.Description AS ItemGroupDesc, " & _
                       "P.Description as Process_Desc, " & _
                       "M.Name + (Case When IsNull(M.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as ManufacturerName, " & _
                       "D1.Description As Dimension1Desc, D2.Description As Dimension2Desc " & _
                       "from (Select * From StockHeadDetail With (NoLock) where DocId = '" & SearchCode & "') S " & _
                       "Left Join Item I With (NoLock) On S.Item = I.Code " & _
                       "LEFT JOIN Item_Uid Iu ON S.Item_Uid = Iu.Code " & _
                       "LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " & _
                       "Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                       "Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code " & _
                       "Left Join Dimension1 D1 With (Nolock)  On S.Dimension1 = D1.Code " & _
                       "Left Join Dimension2 D2 With (Nolock)  On S.Dimension2 = D2.Code " & _
                       "Left Join Process P With (NoLock) On S.Process = P.NCat " & _
                       "Left Join Subgroup M With (NoLock) On S.Manufacturer = M.SubCode " & _
                       "Left Join City C  With (NoLock) On M.CityCode = C.CityCode " & _
                       "Order By S.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1Item_UID, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("Item_No"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Desc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))


                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1Manufacturer, I).Tag = AgL.XNull(.Rows(I)("Manufacturer"))
                            Dgl1.Item(Col1Manufacturer, I).Value = AgL.XNull(.Rows(I)("ManufacturerName"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("Process_Desc"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 600, 895)
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtGodown.KeyDown, TxtParty.KeyDown, TxtProcess.KeyDown, TxtReason.KeyDown
        Select Case sender.Name
            Case TxtGodown.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = "SELECT G.Code, G.Description " & _
                                " FROM Godown G " & _
                                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code  " & _
                                " Where G.Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " & _
                                " And IsNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " Order By G.Description "
                        sender.AgHelpDataset(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        FCreateHelpSubgroup()
                    End If
                End If

            Case TxtReason.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT H.Code, H.Description AS Reason FROM Reason H "
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtProcess.Name
                'If e.KeyCode <> Keys.Enter Then
                '    If sender.AgHelpDataSet Is Nothing Then
                '        mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                '        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                '    End If
                'End If


                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                            mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                        Else
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                        End If
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtGodown.Validating, TxtProcess.Validating, TxtParty.Validating

        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                IniGrid()
                FAsignProcess()
                If TxtV_Type.AgLastValueTag <> TxtV_Type.Tag Then
                    TxtParty.AgHelpDataSet = Nothing
                    Dgl1.AgHelpDataSet(Col1Item) = Nothing
                End If

            Case TxtParty.Name, TxtProcess.Name
                Dgl1.AgHelpDataSet(Col1Item) = Nothing

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) <> "" Then
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown"))
            TxtGodown.Text = AgL.Dman_Execute("Select Description from Godown Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) & "' ", AgL.GCn).ExecuteScalar
        End If
        FAsignProcess()
        BtnImprtFromText.Text = ImportAction_NewImport

        TxtGodown.Tag = PubDefaultGodownCode
        TxtGodown.Text = PubDefaultGodownName
    End Sub

    Private Sub FAsignProcess()
        Dim DtTemp As DataTable = Nothing
        TxtProcess.Enabled = False
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    If DtTemp.Rows.Count = 1 Then
                        TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                        TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                        TxtProcess.Enabled = False
                    Else
                        TxtProcess.Enabled = True
                        TxtProcess.Tag = ""
                        TxtProcess.Text = ""
                    End If
                End If
            Else
                TxtProcess.Enabled = True
                TxtProcess.Tag = ""
                TxtProcess.Text = ""
            End If
        Else
            TxtProcess.Enabled = False
            TxtProcess.Tag = ""
            TxtProcess.Text = ""
            TxtProcess.AgHelpDataSet = Nothing
        End If

        If TxtGodown.Tag = "" Then
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute("SELECT Description  FROM Godown WHERE Code = " & AgL.Chk_Text(TxtGodown.Tag) & " ", AgL.GCn).ExecuteScalar)
        End If
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1ProdOrder
                Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer = 0
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item_UID
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, mRowIndex).Value, mRowIndex)
                    FCreateHelpLotNo()
                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    FCreateHelpLotNo()
                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    FCreateHelpLotNo()
                Case Col1Process
                    If Dgl1.Item(Col1Process, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item_UID, I).Value = "" And Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1Process, I).Tag = Dgl1.Item(Col1Process, mRowIndex).Tag
                                    Dgl1.Item(Col1Process, I).Value = Dgl1.Item(Col1Process, mRowIndex).Value
                                End If
                            Next
                        End If
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_Name").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)

                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_No").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Measure").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtGodown.AgSelectedValue, , , TxtV_Date.Text)
                    If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                        LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , , , , TxtV_Date.Text), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim BalQty As Double = 0

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If AgL.RequiredField(TxtProcess, "Process") Then passed = False : Exit Sub
        End If

        If AgL.RequiredField(TxtGodown, "From Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index) & "," & CStr(Dgl1.Columns(Col1Item_UID).Index) & "," & CStr(Dgl1.Columns(Col1LotNo).Index) & "," & CStr(Dgl1.Columns(Col1Dimension1).Index) & "," & CStr(Dgl1.Columns(Col1Dimension2).Index) & "," & CStr(Dgl1.Columns(Col1Specification).Index) & "," & CStr(Dgl1.Columns(Col1Process).Index)) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    ' For Validation of Stock Process 
                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemForStock")), Boolean) Then
                        mQry = "SELECT isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0) AS BalQty " & _
                                " FROM StockProcess H with (Nolock) " & _
                                " WHERE H.DocID <> " & AgL.Chk_Text(mSearchCode) & " AND H.SubCode = " & AgL.Chk_Text(TxtParty.Tag) & " " & _
                                " AND H.Item = " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & " AND H.Process = " & AgL.Chk_Text(TxtProcess.Tag) & " " & _
                                " AND ISNULL(H.LotNo,'') = '" & .Item(Col1LotNo, I).Value & "' AND ISNULL(H.Dimension1,'') = '" & .Item(Col1Dimension1, I).Tag & "' AND ISNULL(H.Dimension2,'') = '" & .Item(Col1Dimension2, I).Tag & "'" & _
                                " GROUP BY H.Item, isnull(H.LotNo,''), isnull(H.Dimension1,''), isnull(H.Dimension2,'') "
                        BalQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
                        If Math.Round(BalQty, 4) < Math.Round(Val(.Item(Col1Qty, I).Value), 4) Then
                            MsgBox("Balance Qty of " & Dgl1.Item(Col1Item, I).Value & " is " & BalQty & " For Lot No = '" & Dgl1.Item(Col1LotNo, I).Value & "'")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_ProcessLine")), Boolean) Then
                        If Dgl1.Item(Col1Process, I).Value = "" Then
                            MsgBox(" Process Is Required At Line No " & Dgl1.Item(ColSNo, I).Value & "")
                            Dgl1.CurrentCell = Dgl1.Item(Col1Process, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        Dim StrMsg1$ = ""
        StrMsg1 = FDataValidation_Item_UID()

        If StrMsg1 <> "" Then
            If ImportMode = True Then
                ImportMessegeStr += StrMsg1
            Else
                MsgBox(StrMsg1)
            End If
            passed = False : Exit Sub
        End If

    End Sub

    Public Function FDataValidation_Item_UID() As String
        Dim DtTemp As DataTable = Nothing
        Dim DtTemp1 As DataTable = Nothing
        Dim I As Integer = 0
        Dim mItem_UidStr$ = ""
        'Dim mItem_UidPlusJobOrderStr$ = ""
        Dim MsgStr$ = ""

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                If mItem_UidStr = "" Then
                    mItem_UidStr = AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag)
                    'mItem_UidPlusJobOrderStr = AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag + Dgl1.Item(Col1JobOrder, I).Tag)
                Else
                    mItem_UidStr += "," & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag)
                    'mItem_UidPlusJobOrderStr += "," & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag + Dgl1.Item(Col1JobOrder, I).Tag)
                End If
            End If
        Next

        If mItem_UidStr = "" Then FDataValidation_Item_UID = "" : Exit Function

        mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code In (" & mItem_UidStr & ") And I.Div_Code <> '" & AgL.PubDivCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        mQry = " Select Item_Uid From Item_Uid With (NoLock) " & _
                " Where Code In (" & mItem_UidStr & ") " & _
                " And RecDocId Is Null "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is Not Received From Weaving Process." & vbCrLf
            Next
        End If

        mQry = " SELECT Sg.DispName, H.ManualRefNo, H.V_Date, P.Description AS ProcessDesc, Iu.Item_Uid As Item_UidDesc " & _
                " FROM JobIssRecUID L WITH (NoLock) " & _
                " LEFT JOIN StockHead H  WITH (NoLock)ON L.JobRecDocID = H.DocID  " & _
                " LEFT JOIN SubGroup Sg  WITH (NoLock) ON H.SubCode = Sg.SubCode " & _
                " LEFT JOIN Process P WITH (NoLock)ON H.Process =  P.NCat " & _
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " & _
                " WHERE L.Item_UID In (" & mItem_UidStr & ")  " & _
                " AND L.ISSREC = 'R' " & _
                " AND L.Process = '" & TxtProcess.Tag & "' " & _
                " AND L.Item_Uid In (" & mItem_UidStr & ") " & _
                " AND L.DocId <> '" & mSearchCode & "'" & _
                " ORDER BY H.EntryDate DESC	 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_UidDesc") & " Is Already Received From " & AgL.XNull(DtTemp.Rows(I)("DispName")) & " From Process  " & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(I)("V_Date")) & " Against Ref No. " & AgL.XNull(DtTemp.Rows(I)("ManualRefNo")) & " " & vbCrLf
            Next
        End If

        mQry = " SELECT Iu.Item_Uid " & _
                " FROM (Select * From JobIssRecUID WITH (NoLock) " & _
                "       Where Item_UID In (" & mItem_UidStr & ") And ISSREC = 'I' " & _
                "       And Process ='" & TxtProcess.Tag & "') L " & _
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " & _
                " LEFT JOIN JobIssRecUID L1 With (NoLock) On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " & _
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "')  " & _
                " And L.Process <> '" & TxtProcess.Tag & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_Uid") & " Is Not In " & TxtProcess.Text & "." & vbCrLf
            Next
        End If

        mQry = " SELECT Iu.Item_Uid " & _
                " FROM (Select * From JobIssRecUID WITH (NoLock) " & _
                "       Where Item_UID In (" & mItem_UidStr & ") And ISSREC = 'I' " & _
                "       And Process ='" & TxtProcess.Tag & "') L  " & _
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " & _
                " LEFT JOIN JobOrder H ON L.DocID = H.DocID " & _
                " LEFT JOIN JobIssRecUID L1 With (NoLock) On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " & _
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "') " & _
                " And H.JobWorker <> '" & TxtParty.Tag & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_Uid") & " Is Not Issued To this Job Worker." & vbCrLf
            Next
        End If
        FDataValidation_Item_UID = MsgStr
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempStockTransferIssue_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet

    End Sub

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 566, 895)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.Prod_Measure, I.MeasureUnit, " & _
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, IG.Description AS ItemGroupDesc, UI.Code as ItemUIDCode, UI.ProdOrder as ProdOrderDocID, PO.V_Type + '-' + PO.ManualRefNo as ProdOrderNo  " & _
                   " FROM (Select Item, Code, ProdOrder From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " & _
                   " Left Join ProdOrder PO With (NoLock) On UI.ProdOrder = PO.DocID " & _
                   " Left Join Item I With (NoLock) On UI.Item  = I.Code " & _
                   " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                   " Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                   " Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
            Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
            Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
            Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ProdOrderDocID"))
            Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ProdOrderNo"))
            Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ItemGroupDesc"))
            Dgl1.Item(Col1Qty, mRow).Value = 1
            Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
            Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("Prod_Measure")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
            Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Prod_Measure"))
            Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
            Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
            Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtGodown.AgSelectedValue, , , TxtV_Date.Text)

            mQry = " SELECT TOP 1 P.NCat As ProcessCode, P.Description As ProcessDesc " & _
                    " FROM JobIssRecUID L  " & _
                    " LEFT JOIN JobIssRec H ON L.DocID = H.DocID " & _
                    " LEFT JOIN Process P ON L.Process = P.NCat " & _
                    " WHERE Item_UID = '" & Dgl1.Item(Col1Item_UID, mRow).Tag & "' " & _
                    " ORDER BY H.V_Date DESC, P.Sr DESC "
            Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtProcess.Rows.Count > 0 Then
                Dgl1.Item(Col1Process, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                Dgl1.Item(Col1Process, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item_Uid Function ")
        End Try
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And CharIndex('|' + H.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' + H.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        If TxtProcess.Text = "" Then
            mQry = " SELECT H.SubCode AS Code, H.Name  + (Case When IsNull(H.CityCode,'')<>'' Then ', ' + City.CityName Else '' End) as Name " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City On H.CityCode = City.CityCode" & _
                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT H.SubCode AS Code, H.Name  + (Case When IsNull(H.CityCode,'')<>'' Then ', ' + City.CityName Else '' End) AS Name " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City On H.CityCode = City.CityCode" & _
                    " Left Join JobworkerProcess JP On H.SubCode = JP.SubCode" & _
                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' And JP.Process = '" & TxtProcess.Tag & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpItem()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + H.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + H.Item + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + H.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + H.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        mQry = "SELECT H.Code, H.Description as Item_Name, H.ManualCode as Item_No, H.Unit, IG.Description AS ItemGroupDesc, " & _
            " H.Measure, H.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " & _
            " NULL AS BalQty, NULL AS Process, NULL AS LotNo, NULL AS Dimension1, NULL AS Dimension2,  NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & " " & _
            " FROM Item H " & _
            " LEFT JOIN ItemGroup IG On Ig.Code = H.ItemGroup " & _
            "Left Join Unit U On H.Unit = U.Code " & _
            "Left Join Unit MU On H.MeasureUnit = MU.Code " & _
            "Where IsNull(H.IsDeleted ,0)  = 0 And " & _
            "IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 11) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemFromStockProcess()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + I.Item + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        mQry = " SELECT H.Item AS Code, I.Description as Item_Name, H.LotNo, D1.Description AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", D2.Description AS " & AgTemplate.ClsMain.FGetDimension2Caption() & " , Round(H.BalQty,4) AS BalQty, I.ManualCode as Item_No, I.Unit, IG.Description AS ItemGroupDesc, " & _
                " I.Measure, I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, H.SubCode, H.Process, H.Dimension1, H.Dimension2 " & _
                " FROM " & _
                " ( " & _
                " SELECT S.SubCode, S.process, S.Item, S.Dimension1, S.Dimension2, S.LotNo, isnull(sum(S.Qty_Rec),0) - isnull(sum(S.Qty_Iss),0) AS BalQty  " & _
                " FROM StockProcess S " & _
                " Where S.SubCode = " & AgL.Chk_Text(TxtParty.Tag) & " AND S.Process = " & AgL.Chk_Text(TxtProcess.Tag) & " " & _
                " GROUP BY S.Item ,S.SubCode, S.process, S.Dimension1, S.Dimension2, S.LotNo " & _
                " HAVING isnull(sum(S.Qty_Rec),0) - isnull(sum(S.Qty_Iss),0) > 0   " & _
                " ) H " & _
                " LEFT JOIN Item I ON I.Code = H.Item  " & _
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup  " & _
                " Left Join Unit U On I.Unit = U.Code  " & _
                " Left Join Unit MU On I.MeasureUnit = MU.Code  " & _
                " LEFT JOIN Dimension1 D1 ON D1.Code = H.Dimension1  " & _
                " LEFT JOIN Dimension2 D2 ON D2.Code = H.Dimension2  " & _
                " Where IsNull(I.IsDeleted ,0)  = 0 " & _
                " AND IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 9) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1ItemCode
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
                            FCreateHelpItemFromStockProcess()
                        Else
                            FCreateHelpItem()
                        End If
                    End If
                End If

            Case Col1Item
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemForStock")), Boolean) Then
                            FCreateHelpItemFromStockProcess()
                        Else
                            FCreateHelpItem()
                        End If
                    End If
                End If

            Case Col1Manufacturer
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then

                        mQry = " SELECT H.SubCode AS Code, H.Name  + (Case When IsNull(H.CityCode,'')<>'' Then ', ' + City.CityName Else '' End) as Name " & _
                                " FROM Subgroup H  With (NoLock) " & _
                                " Left Join City On H.CityCode = City.CityCode" & _
                                " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " And IsNull(H.MasterType,'') = '" & AgTemplate.ClsMain.SubgroupType.Manufacturer & "' "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1ProdOrder
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = "SELECT H.DocId AS Code, " & _
                                " Max(H.ManualRefNo) As Prod_Order_No, Max(H.V_Type) as Prod_Order_Type " & _
                                " FROM (Select DocID From ProdOrderDetail With (NoLock) Where Item ='" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "') L " & _
                                " Left Join ProdOrder H With (NoLock) On L.DocID = H.DocId  " & _
                                " Group By H.DocId"
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Process
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT P.NCat AS Code, P.Description  " & _
                                " FROM Process P  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Unit
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT H.Code, H.Code as Description  " & _
                                " FROM Unit H Order by H.Code  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
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
    End Sub

    Private Sub FCreateHelpLotNo()
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            If AgL.VNull(AgL.Dman_Execute("Select IsRequired_LotNo From ItemSiteDetail L Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "' ", AgL.GcnRead).ExecuteScalar) <> 0 Then
                mQry = " SELECT L.LotNo AS Code, L.LotNo, IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) AS Qty " & _
                        " FROM StockProcess L  " & _
                        " WHERE L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' AND isnull(l.LotNo,'') <> '' " & _
                        " AND L.SubCode='" & TxtParty.Tag & "'" & _
                        " GROUP BY L.LotNo " & _
                        " HAVING IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) <> 0 "
                Dgl1.AgHelpDataSet(Col1LotNo) = AgL.FillData(mQry, AgL.GCn)
            End If
        Else
            Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
        End If

    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DOCID, H.V_TYPE, H.V_DATE, H.V_NO, H.MANUALREFNO, H.REMARKS, H.ENTRYBY, H.ENTRYDATE, " & _
                " H.ENTRYTYPE, H.ENTRYSTATUS,  H.APPROVEBY, H.APPROVEDATE,  H.STATUS,  " & _
                " L.SR, L.ITEM, ISNULL(L.QTY,0) AS QTY, L.UNIT, L.REMARKS AS LINEREMARKS,  L.LOTNO, S.NAME AS JOBWORKERNAME, S.DISPNAME AS JOBWORKERDISPNAME,   S.ADD1, " & _
                " S.ADD2,S.ADD3,C.CITYNAME,S.MOBILE,S.PHONE, S.PAN,  G.DESCRIPTION AS GODOWNDESC,  I.DESCRIPTION AS ITEMDESC,   " & _
                " '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Caption_Dimension1,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Caption_Dimension2, " & _
                " D1.Description AS D1Desc,  D2.Description AS D2Desc, U.DecimalPlaces, " & _
                " I.ITEMGROUP ,   I.ITEMTYPE, IG.DESCRIPTION AS ITEMGROUPDESC,P.Description AS ProcessDesc, FP.Description AS FromProcessDesc, CM.NAME AS COSTCENTERNAME " & _
                " FROM STOCKHEAD H   " & _
                " LEFT JOIN STOCKHEADDETAIL L ON H.DOCID = L.DOCID   " & _
                " LEFT JOIN VOUCHER_TYPE VT ON H.V_TYPE = VT.V_TYPE   " & _
                " LEFT JOIN SUBGROUP S ON H.SUBCODE = S.SUBCODE   " & _
                " LEFT JOIN CITY C ON S.CITYCODE = C.CITYCODE   " & _
                " LEFT JOIN GODOWN G ON H.FROMGODOWN = G.CODE   " & _
                " LEFT JOIN ITEM I ON L.ITEM = I.CODE   " & _
                " LEFT JOIN ITEMGROUP  IG ON I.ITEMGROUP = IG.CODE  " & _
                " LEFT JOIN COSTCENTERMAST CM ON L.COSTCENTER = CM.CODE " & _
                " LEFT JOIN Process P ON P.NCat = H.Process " & _
                " LEFT JOIN Enviro E ON E.Site_Code= H.Site_Code AND E.Div_Code = H.Div_Code " & _
                " LEFT JOIN Unit U ON U.Code = L.Unit " & _
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " & _
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                " LEFT JOIN Process FP ON FP.NCat = L.Process " & _
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_Receive_Print", "Store Receive")
    End Sub

    Private Sub FrmStoreReceiveNew_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        TxtParty.AgHelpDataSet = Nothing
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            LblCurrentStock.Visible = True : LblCurrentStockText.Visible = True
            LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, e.RowIndex).Tag, mSearchCode, , , , , TxtV_Date.Text), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, e.RowIndex).Value + 2, "0"))
        Else
            LblCurrentStock.Visible = False : LblCurrentStockText.Visible = False
        End If
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprtFromText.Click
        If AgL.StrCmp(BtnImprtFromText.Text, ImportAction_NewImport) Then
            FImportFromTextFile()
            ChkShowOnlyImportedRecords.Checked = True
            ChkShowOnlyImportedRecords.Visible = True
        Else
            mQry = " UPDATE StockHead Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImportClear & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            FIniMaster(1)
            MoveRec()
        End If
    End Sub

    Private Sub FImportFromTextFile()
        Dim Sr As StreamReader
        Dim Opn As New OpenFileDialog
        Dim mItemDivisionCode$ = ""
        Dim mItemDivisionText$ = ""

        Dim Line$ = "", mDateTime$ = "", mMachine$ = "", mProcess$ = "", mJobRecBy$ = "", mBarcode$ = "", mSKU$ = ""
        Dim mDefaultGodown$ = "", mJobType$ = "", mJobWorker$ = "", mIssRec$ = "", StrQry$ = ""
        Dim mMeasurePerPcs As Double = 0
        Dim ErrorLog$ = "", StrMessage$ = ""
        Dim mItem_UidDesc$ = ""

        Dim I As Integer, J As Integer = 0, bBarCodeQty As Integer = 0
        Dim DtTemp As DataTable, DtLineRec As DataTable
        Dim strArr() As String

        DtTemp = AgL.FillData("Select Godown from EnviroDefaultGodown Where Div_Code = '" & AgL.PubDivCode & "' and Site_Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            mDefaultGodown = DtTemp.Rows(0)("Godown")
        End If


        If Topctrl1.Mode <> "Add" Then
            MsgBox("Import can be done only on Add mode")
            Exit Sub
            If TxtProcess.Text = "" Then
                MsgBox("Process is mandatory to import records")
                Exit Sub
            End If
        End If

        ImportMessegeStr = ""
        ImportMode = True

        Opn.ShowDialog()

        If Opn.FileName = "" Then Exit Sub

        Sr = New StreamReader(Opn.FileName)

        StrMessage = ""

        StrQry = "  Declare @TmpTable as Table " & _
                    " ( " & _
                    " Process nVarchar(10), " & _
                    " IssRec nVarchar(10), " & _
                    " JobWorker nVarchar(10), " & _
                    " OrderBy nVarchar(10), " & _
                    " BarCode nVarchar(10), " & _
                    " Sku nVarchar(10), " & _
                    " MeasurePerPcs Float " & _
                    " ) "

        Do
            I += 1
            Line = Sr.ReadLine()
            If Line IsNot Nothing Then
                strArr = Split(Line, ",")

                If strArr.Length <> 14 Then
                    MsgBox("Invalid records in file")
                    Exit Sub
                End If

                mDateTime = strArr(1)
                mMachine = strArr(3)
                mIssRec = strArr(5)
                mProcess = strArr(7)
                mJobWorker = strArr(9)
                mJobRecBy = strArr(11)
                mBarcode = strArr(13)

                mSKU = ""
                mItem_UidDesc = strArr(13)



                If mIssRec <> "R" Then MsgBox("IssRec Is Not Equal To ""R"".Can't Proceed.") : Exit Sub

                DtTemp = AgL.FillData("Select Process From ProcessCode Where Code = '" & mProcess & "' and Div_Code = '" & AgL.PubDivCode & "' ", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mProcess = DtTemp.Rows(0)("Process")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in Process Field at Row No. " & I
                End If

                If mProcess <> TxtProcess.Tag Then
                    MsgBox("Process In Text File Is Not Equal To " & TxtProcess.Text & "", MsgBoxStyle.Information)
                    Exit Sub
                End If

                DtTemp = AgL.FillData("Select SubCode From SubGroup WITH (NoLock) Where ManualCode = '" & mJobWorker & "'  And CharIndex('|' + '" & AgL.PubDivCode & "' + '|', DivisionList) > 0  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobWorker = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobWorker Field at Row No. " & I
                End If

                DtTemp = AgL.FillData("Select SubCode From SubGroup WITH (NoLock) Where ManualCode = '" & mJobRecBy & "'  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobRecBy = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobRecBy Field at Row No. " & I
                End If

                If mBarcode.Trim = "" Then
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "No value defined in Barcode Field at Row No. " & I
                End If

                If mBarcode.Trim <> "" Then
                    DtTemp = AgL.FillData("Select Item_Uid.Code, Item_Uid.Item, Item.Measure From Item_UID LEFT JOIN Item On Item_Uid.Item = Item.Code Where Item_Uid.Item_UID = '" & mBarcode & "' ", AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        mBarcode = DtTemp.Rows(0)("Code")
                        mSKU = DtTemp.Rows(0)("Item")
                        mMeasurePerPcs = AgL.VNull(DtTemp.Rows(0)("Measure"))
                    Else
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        MsgBox("Invalid Value Found in Barcode Field at Row No. " & I)
                    End If
                End If

                If StrMessage <> "" Then
                    MsgBox(StrMessage)
                    Exit Sub
                End If

                Dim Item_UidError$ = ""
                Item_UidError = FCheck_Item_UID(mItem_UidDesc, mJobWorker)
                If Item_UidError = "" Then
                    StrQry += " Insert Into @TmpTable (Process, IssRec, JobWorker, OrderBy, Barcode, Sku, MeasurePerPcs) "
                    StrQry += " Values (" & AgL.Chk_Text(mProcess) & ", " & AgL.Chk_Text(mIssRec) & ", " & _
                                " " & AgL.Chk_Text(mJobWorker) & ", " & AgL.Chk_Text(mJobRecBy) & ", " & _
                                " " & AgL.Chk_Text(mBarcode) & ", " & AgL.Chk_Text(mSKU) & ", " & AgL.Chk_Text(mMeasurePerPcs) & ") "
                Else
                    ImportMessegeStr += Item_UidError & vbCrLf
                End If

            End If
        Loop Until Line Is Nothing
        Sr.Close()


        mQry = StrQry & " Select Process, IssRec, JobWorker, OrderBy " & _
                " From @TmpTable " & _
                " Where Process = '" & mProcess & "' And IssRec = 'R' " & _
                " Group by Process, IssRec, JobWorker, OrderBy "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            If I > 0 Then Topctrl1.FButtonClick(0)

            Dgl1.Focus()

            TxtProcess.Tag = mProcess
            TxtProcess.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Process Where NCat = '" & TxtProcess.Tag & "' ", AgL.GCn).ExecuteScalar)

            TxtParty.Tag = DtTemp.Rows(I)("JobWorker")
            TxtParty.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtParty.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtGodown.Tag = mDefaultGodown
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)

            mQry = StrQry & " Select Process, Sku, BarCode, Max(MeasurePerPcs) As MeasurePerPcs From @TmpTable " & _
                    " Where Process = '" & TxtProcess.Tag & "' And Jobworker = '" & TxtParty.Tag & "' " & _
                    " Group By Process, Sku, BarCode " & _
                    " Order By MeasurePerPcs, Sku "
            DtLineRec = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For J = 0 To DtLineRec.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, Dgl1.Rows.Count - 2).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Tag = DtLineRec.Rows(J)("BarCode")
                Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & DtLineRec.Rows(J)("BarCode") & "'", AgL.GCn).ExecuteScalar)
                Validating_Item_Uid(Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
            Next

            Calculation()
            Topctrl1.FButtonClick(13)

        Next

        If ImportMessegeStr <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt", ImportMessegeStr, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt").Dispose()
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt", ImportMessegeStr, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt")
            Exit Sub
        End If

        ImportMode = False
    End Sub

    'Public Function FCheck_Item_UID(ByVal Item_UID As String) As String
    '    Dim Item_UidCode$ = "", ErrMsgStr$ = ""
    '    Dim DtTemp As DataTable = Nothing
    '    Dim mProcessSequence$ = ""
    '    Dim mProcessIterationsAllowed As Integer = 0

    '    mQry = " SELECT Code FROM Item_UID With (NoLock) WHERE Item_UID = '" & Item_UID & "'"
    '    Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
    '    If Item_UidCode = "" Then
    '        FCheck_Item_UID = "Carpet Id Is Not Valid."
    '        Exit Function
    '    Else
    '        FCheck_Item_UID = ""
    '    End If

    '    mQry = "Select ProcessSequence, " & _
    '            "       (Select Count(*) from ProcessSequenceDetail " & _
    '            "        Where Code = H.ProcessSequence And Process = '" & TxtProcess.Tag & "') As IterationsAllowed " & _
    '            " From Item H Where Code = (Select Item From Item_Uid Where Code = '" & Item_UidCode & "') "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
    '    If DtTemp.Rows.Count > 0 Then
    '        mProcessSequence = AgL.XNull(DtTemp.Rows(0)("ProcessSequence"))
    '        mProcessIterationsAllowed = AgL.VNull(DtTemp.Rows(0)("IterationsAllowed"))
    '    End If


    '    If mProcessSequence <> "" Then
    '        If Val(mProcessIterationsAllowed) > 0 Then
    '            mQry = "Select IsNull(Count(*),0) from JobIssRecUID " & _
    '                    " Where IssRec='I' And Process = '" & TxtProcess.Tag & "' " & _
    '                    " And Item_UID = '" & Item_UidCode & "' " & _
    '                    " And DocID <> '" & mSearchCode & "'  "
    '            If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar + 1 > Val(mProcessIterationsAllowed) Then
    '                If MsgBox("Carpet Id " & Item_UID & " has already completed this process.Do you want to issue it again", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
    '                    FCheck_Item_UID = "Carpet Id " & Item_UID & " has already completed this process"
    '                    Exit Function
    '                Else
    '                    FCheck_Item_UID = ""
    '                End If
    '            End If
    '        End If
    '    End If


    '    mQry = " Select RecDocID From Item_Uid With (NoLock) Where Code = '" & Item_UidCode & "' "
    '    If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
    '        FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
    '        Exit Function
    '    Else
    '        FCheck_Item_UID = ""
    '    End If

    '    'mQry = "SELECT Count(I.DocID) " & _
    '    '       " FROM (SELECT DocID, Item_UID FROM JobIssRecUID WITH (NoLock) " & _
    '    '       " WHERE Item_UID ='" & Item_UidCode & "' And IssRec= 'R') I " & _
    '    '       " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
    '    '       " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "'"
    '    'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
    '    '    mQry = "SELECT TOP 1 Sg.Name, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " & _
    '    '            " FROM (SELECT DocID, Item_UID FROM JobIssRecUID WITH (NoLock) " & _
    '    '            " WHERE Item_UID ='" & Item_UidCode & "' And IssRec='R') I " & _
    '    '            " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
    '    '            " LEFT JOIN JobOrder H WITH (NoLock) ON I.DocID = H.DocID " & _
    '    '            " LEFT JOIN SubGroup Sg WITH (NoLock) ON H.JobWorker = Sg.SubCode " & _
    '    '            " LEFT JOIN VoucherCat Vc WITH (NoLock) ON H.Process = Vc.NCat " & _
    '    '            " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " & _
    '    '            " ORDER BY H.V_Date Desc "
    '    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
    '    '    FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Issued To " & AgL.XNull(DtTemp.Rows(0)("Name")) & " For " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & "."
    '    '    Exit Function
    '    'Else
    '    '    FCheck_Item_UID = ""
    '    'End If
    'End Function

    Public Function FCheck_Item_UID(ByVal Item_UID As String, ByVal JobWorker As String) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim bIssueCnt As Integer = 0

        mQry = " SELECT Code FROM Item_UID With (NoLock) WHERE Item_UID = '" & Item_UID & "'"
        Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Item_UidCode = "" Then
            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " Select RecDocID From Item_Uid With (NoLock) Where Code = '" & Item_UidCode & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT L.Process " & _
                " FROM (Select * From JobIssRecUID WITH (NoLock) Where Item_UID = '" & Item_UidCode & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L " & _
                " Left Join JobIssRecUID L1 With (NoLock) On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " & _
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "')  "
        If AgL.FillData(mQry, AgL.GCn).Tables(0).rows.Count <= 0 Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not In " & TxtProcess.Text & "."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT H.SubCode " & _
                " FROM (Select * From JobIssRecUID WITH (NoLock) Where Item_UID = '" & Item_UidCode & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L  " & _
                " LEFT JOIN StockHead H ON L.DocID = H.DocID " & _
                " Left Join JobIssRecUID L1 With (NoLock) On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " & _
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "') "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> JobWorker Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Issued To this Job Worker."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        'mQry = " Select L.DocId " & _
        '       " From JobIssRecUID L " & _
        '       " Where L.Item_Uid = '" & Item_UidCode & "' " & _
        '       " And L.Process = '" & TxtProcess.Tag & "' " & _
        '       " AND L.ISSREC = 'I' " & _
        '       " And (L.JobRecDocID Is Null Or L.JobRecDocId = '" & mSearchCode & "') "
        'If AgL.FillData(mQry, AgL.GCn).Tables(0).rows.Count <= 0 Then
        '    FCheck_Item_UID = "No Order Pending For Carpet Id " & Item_UID & "."
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If


        'mQry = " SELECT TOP 1 Sg.DispName, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " & _
        '        " FROM JobIssRecUID L WITH (NoLock) " & _
        '        " LEFT JOIN JobIssRec H  WITH (NoLock)ON L.DocID = H.DocID  " & _
        '        " LEFT JOIN SubGroup Sg  WITH (NoLock) ON H.JobWorker = Sg.SubCode " & _
        '        " LEFT JOIN VoucherCat  Vc  WITH (NoLock) ON H.Process =  Vc.NCat " & _
        '        " WHERE L.Item_UID = '" & Item_UidCode & "'  " & _
        '        " AND L.ISSREC = 'R' " & _
        '        " AND L.Process = '" & TxtProcess.Tag & "' " & _
        '        " AND L.JobRecDocID = '" & Dgl1.Item(Col1JobOrder, mRowIndex).Tag & "' " & _
        '        " And L.DocId <> '" & mSearchCode & "'" & _
        '        " ORDER BY H.EntryDate DESC	 "
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        'If DtTemp.Rows.Count > 0 Then
        '    FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Received From " & AgL.XNull(DtTemp.Rows(0)("DispName")) & " From Process  " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No.  " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & " "
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If
    End Function
End Class
