Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports System.Data.SQLite

Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public Const ModuleName As String = "AgTemplate"

    Public PubIsLinkWithFA As Byte = 0
    Public PubIsNegativeStockAllowed As Byte = 0
    Public PubIsLotNoApplicable As Byte = 0

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        AgIniVar = New AgLibrary.ClsIniVariables(AgL)        
    End Sub

    Public Enum EntryPointIniMode
        Insertion
        Add
        Browse
        Approval
        Search
    End Enum

    Public Enum T_Nature
        NA = 0
        Cancellation = 1
        Amendment = 2
        Returned = 3
    End Enum

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Enum ManualRefType
        Max
        MonthWise
        YearWise
        DayWise
    End Enum

    Public Class Colours
        Public Shared ReadOnly GridRow_Locked As Color = Color.AliceBlue
    End Class

    Public Class ItemType
        Public Const FinishedMaterial As String = "FM"
        Public Const RawMaterial As String = "RM"
        Public Const SemiFinishedMaterial As String = "SFM"
        Public Const Other As String = "Other"
        Public Const Machine As String = "Machine"
        Public Const ItemRateGroup As String = "IRG"
    End Class

    Public Class Temp_Process
        Public Const Purchase As String = "PURCH"
        Public Const Sale As String = "SALE"
    End Class

    Public Class SubGroupNature
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
    End Class

    Public Class SubgroupType
        Public Const Party As String = "Party"
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
        Public Const Agent As String = "Agent"
        Public Const Employee As String = "Employee"
        Public Const JobWorker As String = "Job Worker"
        Public Const Manufacturer As String = "Manufacturer"
        Public Const PartyRateGroup As String = "Party Rate Group"
    End Class

    Public Class EntryStatus
        Public Const Active As String = "Active"
        Public Const Inactive As String = "Inactive"
        Public Const Cancelled As String = "Cancelled"
        Public Const Closed As String = "Closed"
    End Class

    Public Class LogStatus
        Public Const LogOpen As String = "Open"
        Public Const LogDiscard As String = "Discard"
        Public Const LogApproved As String = "Approved"
        Public Const LogAdd As String = "Add"
        Public Const LogImport As String = "Import"
        Public Const LogImportClear As String = "Import Clr"
    End Class

    Public Class PaymentReceiptType
        Public Const Payment As String = "Payment"
        Public Const Receipt As String = "Receipt"
        Public Const DebitNote As String = "Debit Note"
        Public Const CreditNote As String = "Credit Note"
    End Class

    Public Class RateMasterType
        Public Const General As String = "General"
        Public Const SizeWise As String = "Size Wise"
    End Class

    Class AcNature
        Public Const Bank As String = "Bank"
        Public Const Cash As String = "Cash"
        Public Const Customer As String = "Customer"
        Public Const Employee As String = "Employee"
        Public Const Expenses As String = "Expenses"
        Public Const Others As String = "Others"
        Public Const Purchase As String = "Purchase"
        Public Const Sales As String = "Sales"
        Public Const Supplier As String = "Supplier"
        Public Const TDS As String = "T.D.S."
        Public Const Transporter As String = "Transporter"
        Public Const Unloader As String = "Unloader"
    End Class

    Class AcGroup
        Public Const CapitalAccount As String = "0001"
        Public Const LoanLiability As String = "0002"
        Public Const CurrentLiabilities As String = "0003"
        Public Const FixedAssets As String = "0004"
        Public Const Investments As String = "0005"
        Public Const CurrentAssets As String = "0006"
        Public Const BranchDivisions As String = "0007"
        Public Const MiscExpencesAsset As String = "0008"
        Public Const SuspenseAc As String = "0009"
        Public Const ClosingStock As String = "000A"
        Public Const ReservesSurplus As String = "0010"
        Public Const BankODAc As String = "0011"
        Public Const SecuredLoans As String = "0012"
        Public Const UnsecuredLoans As String = "0013"
        Public Const DutiesTaxes As String = "0014"
        Public Const Provisions As String = "0015"
        Public Const SundryCreditors As String = "0016"
        Public Const OpeningStock As String = "0017"
        Public Const DepositsAsset As String = "0018"
        Public Const LoansAdvancesAsset As String = "0019"
        Public Const SundryDebtors As String = "0020"
        Public Const CashInHand As String = "0021"
        Public Const BankAccounts As String = "0022"
        Public Const SalesAccounts As String = "0023"
        Public Const PurchaseAccounts As String = "0024"
        Public Const DirectIncomes As String = "0025"
        Public Const DirectExpenses As String = "0026"
        Public Const IndirectIncomes As String = "0027"
        Public Const IndirectExpenses As String = "0028"
        Public Const ProfitLossAc As String = "0029"

        Public Sub New()

        End Sub
    End Class

    Public Class Temp_VCategory
        Public Const Sale As String = "SALES"
        Public Const Purchase As String = "PURCHASE"

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

    Class Temp_NCat
        'FA NCat
        Public Const FAOpeningBalance As String = "OPBAL"

        'Common NCat
        Public Const StockOpening As String = "OPSTK"
        Public Const StoreRequisition As String = "STREQ"
        Public Const StoreIssue As String = "STISS"
        Public Const StoreReceive As String = "STREC"
        Public Const StockTransferIssue As String = "TRFI"
        Public Const StockTransferReceive As String = "TRFR"
        Public Const StockTransfer As String = "STRF"
        Public Const PhysicalStockEntry As String = "PHSTK"
        Public Const PhysicalStockAdjustmentEntry As String = "PHSTA"
        Public Const InternalProcess As String = "INPRO"

        'Production NCat
        Public Const ProductionOrder As String = "PRO"
        Public Const ProductionOrderCancel As String = "PROCL"
        Public Const ProductionPlan As String = "PRP"
        Public Const MaterialPlan As String = "MP"
        Public Const MaterialPlanAmendment As String = "MPLAM"

        Public Const JobOrder As String = "JO"       
        Public Const JobReceive As String = "JR"
        Public Const JobInvoice As String = "JINV"
        Public Const JobOrderCancel As String = "JOCNL"
        Public Const JobOrderAmendment As String = "JOAMD"
        Public Const JobInvoiceAmendment As String = "JIAMD"
        Public Const JobConsumption As String = "JCON"
        Public Const JobRateConversion As String = "JRCON"

        'Work NCat
        Public Const WorkOrder As String = "WO"
        Public Const WorkOrderPlan As String = "WOPLN"
        Public Const WorkOrderPlanAmendment As String = "WOPAM"
        Public Const WorkDispatch As String = "WDP"
        Public Const WorkInvoice As String = "WINV"
        Public Const WorkOrderCancel As String = "WOCNL"
        Public Const WorkOrderAmendment As String = "WOAMD"


        'Purchase NCat
        Public Const PurchaseIndent As String = "IND"
        Public Const PurchaseIndentCancel As String = "PINCL"
        Public Const PurchaseQuotation As String = "PQOT"
        Public Const PurchaseQuotSelection As String = "PQOTS"
        Public Const PurchaseOrder As String = "PO"
        Public Const PurchaseOrderAmendment As String = "POAMD"
        Public Const PurchaseOrderCancel As String = "POCNL"
        Public Const GoodsReceipt As String = "GR"
        Public Const PurchaseChallanReturn As String = "PCRET"
        Public Const PurchaseInvoice As String = "PINV"
        Public Const PurchaseReturn As String = "PRET"
        Public Const Requisition As String = "PRET"


        'Sale
        Public Const SaleOrder As String = "SO"
        Public Const SaleOrderCancel As String = "SOCNL"
        Public Const SaleOrderAmendment As String = "SOAMD"
        Public Const SaleInvoice As String = "SI"
        Public Const SaleChallan As String = "SCHLN"
        Public Const SaleReturn As String = "SRET"
        Public Const SaleQCRequest As String = "SQREQ"
        Public Const SaleQC As String = "SQC"
        Public Const SaleEnquiry As String = "SENQ"
        Public Const SaleQuotation As String = "SQUOT"
        Public Const SaleRateContract As String = "SRCON"
        Public Const SaleOrderPlan As String = "SOPLN"
        Public Const SaleOrderPlanAmendment As String = "SOPAM"

        'Machine
        'Public Const MachineBreakDown As String = "SO"

        'Payment
        Public Const Payment As String = "PMNT"
        Public Const Receipt As String = "RECT"
        Public Const DebitNote As String = "DNOTE"
        Public Const CreditNote As String = "CNOTE"

        'For Import Export
        Public Const DocumentEntry As String = "DOC"
        Public Const ShipmentEntry As String = "SHIP"

        'For Form Management
        Public Const FormReceive As String = "FRCV"
        Public Const FormIssue As String = "FISS"

        'Gate InOut
        Public Const VehicleGate As String = "VGATE"

        'Visitor InOut
        Public Const VisitorGate As String = "PGATE"
        Public Const ChequeCancel As String = "CHCNL"
    End Class

    Class Temp_VType
        Public Const JobOrder As String = "JO"
        Public Const JobIssue As String = "JI"
        Public Const JobReturn As String = "JRET"
        Public Const JobReceive As String = "JR"
        Public Const JobInvoice As String = "JINV"
        Public Const JobOrderCancel As String = "JOCNL"
        Public Const JobOrderAmendment As String = "JOAMD"
        Public Const JobConsumption As String = "JCON"
        Public Const JobConsumptionAdjustment As String = "JCADJ"
        Public Const JobRateConversion As String = "JRCON"


        Public Const WorkOrder As String = "WO"
        Public Const WorkMaterialReceive As String = "WREC"
        Public Const WorkMaterialReturn As String = "WRET"
        Public Const WorkDispatch As String = "WDP"
        Public Const WorkInvoice As String = "WINV"
        Public Const WorkOrderCancel As String = "WOCNL"
        Public Const WorkOrderAmendment As String = "WOAMD"




    End Class


    Public Class Charges
        Public Const ADDITIONALDUTY As String = "ADUTY"
        Public Const CUSTOMDUTY As String = "CD"
        Public Const CUSTOMDUTYECESS As String = "CDEC"
        Public Const CUSTOMDUTYHECESS As String = "CDHEC"
        Public Const CUSTOMDUTYTAXABLEAMT As String = "CDTA"
        Public Const CST As String = "CST"
        Public Const DISCOUNT As String = "DIS"
        Public Const DISCOUNTPRETAX As String = "DPTAX"
        Public Const FREIGHT As String = "FRT"
        Public Const GROSSAMOUNT As String = "GAMT"
        Public Const HANDLINGCHARGES As String = "HCHRG"
        Public Const INCENTIVE As String = "INCENT"
        Public Const INSURANCE As String = "INS"
        Public Const NETAMOUNT As String = "NAMT"
        Public Const OTHERADDITIONSPRETAX As String = "OAPTAX"
        Public Const OTHERCHARGES As String = "OC"
        Public Const PENALTY As String = "PENALTY"
        Public Const ROUNDOFF As String = "RO"
        Public Const SAT As String = "SAT"
        Public Const SUBTOTAL As String = "STOT"
        Public Const SALESTAXTAXABLEAMT As String = "STTA"
        Public Const TOTALCUSTOMDUTY As String = "TCD"
        Public Const VAT As String = "VAT"
        Public Const LANDEDVALUE As String = "LV"
        Public Const SERVICETAXECESS As String = "SECESS"
        Public Const SERVICETAX As String = "SERV"
        Public Const SERVICETAXASSESABLEAMT As String = "SERV_AA"
        Public Const SERVICETAXHECESS As String = "SHECESS"
        Public Const TotalFreight As String = "TFRT"
    End Class

    Public Class JobReceiveFor
        Public Const JobOrder As String = "Job Order"
        Public Const JobIssue As String = "Job Issue"
    End Class

    Public Class JobType
        Public Const Inside As String = "Inside"
        Public Const Outside As String = "Outside"
    End Class

    Public Enum JobWithMaterialYN
        NA
        Yes
        No
    End Enum

    Public Enum JobReceiveBillPosting
        Dues
        ToBeBilled
        Dues_JobOrderWise
        NA
        None
    End Enum

    Public Enum JobOrderType
        JobOrder
        JobOrder_With_Issue
        JobOrder_Cancel
        NA
    End Enum

    Public Class JobOrderFor
        Public Const ProductionOrder As String = "Production Order"
        Public Const Stock As String = "Stock"
        Public Const Sample As String = "Sample"
    End Class

    Public Class StockStatus
        Public Const Standard As String = "Standard"
        Public Const Rejected As String = "Rejected"
        Public Const Damaged As String = "Damaged"
    End Class

    Public Class SaleOrderStatus
        Public Const Active As String = "Active"
        Public Const Hold As String = "Hold"
        Public Const Cancelled As String = "Cancelled"
        Public Const Closed As String = "Closed"
    End Class

    Public Class Unit
        Public Const Pcs As String = "Pcs"
        Public Const Kg As String = "Kg"
        Public Const Meter As String = "Meter"
        Public Const Liter As String = "Liter"
        Public Const SqYard As String = "Sq.Yard"
    End Class

    Public Structure StructStock
        Dim DocID As String
        Dim Sr As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As String
        Dim RecID As String
        Dim Div_Code As String
        Dim Site_Code As String
        Dim CostCenter As String
        Dim SubCode As String
        Dim Currency As String
        Dim SalesTaxGroupParty As String
        Dim Structure1 As String
        Dim BillingType As String
		Dim Item_Uid As String
        Dim Item As String
        Dim ProcessGroup As String
        Dim Godown As String
        Dim Qty_Iss As Double
        Dim Qty_Rec As Double
        Dim Unit As String
        Dim LotNo As String
        Dim MeasurePerPcs As Double
        Dim Measure_Iss As Double
        Dim Measure_Rec As Double
        Dim MeasureUnit As String
        Dim Rate As Double
        Dim Amount As Double
        Dim Addition As Double
        Dim Deduction As Double
        Dim NetAmount As Double
        Dim Remarks As String
        Dim Status As String
        Dim UID As String
        Dim Process As String
		Dim ToProcess As String
        Dim FIFORate As Double
        Dim FIFOAmt As Double
        Dim AVGRate As Double
        Dim AVGAmt As Double
        Dim Cost As Double
        Dim Doc_Qty As Double
        Dim ReferenceDocID As String
        Dim ReferenceDocIDSr As Integer
    End Structure

#Region "Public Help Queries"
    Public Class HelpQueries
        Public Const StockStatus As String = "Select '" & ClsMain.StockStatus.Standard & "' as Code, '" & ClsMain.StockStatus.Standard & "' as Description " & _
                                                " Union All Select '" & ClsMain.StockStatus.Rejected & "' as Code, '" & ClsMain.StockStatus.Rejected & "' as Description " & _
                                                " Union All Select '" & ClsMain.StockStatus.Damaged & "' as Code, '" & ClsMain.StockStatus.Damaged & "' as Description "

        Public Const BillingType As String = "Select 'Qty' as Code, 'Qty' as Description " & _
                                            " Union All Select 'Area' as Code, 'Area' as Description "

    End Class

#End Region


#Region " Structure Update Code "
    Public Sub UpdateTableStructure_Production(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FProcessGroup(MdlTable, "ProcessGroup", EntryPointType.Main)

        FProcess(MdlTable, "Process", EntryPointType.Main)
        FProcess(MdlTable, "Process_Log", EntryPointType.Log)

        FProcess_VType(MdlTable, "Process_VType", EntryPointType.Main)
        FProcess_VType(MdlTable, "Process_VType_Log", EntryPointType.Log)

        FProcessSequence(MdlTable, "ProcessSequence", EntryPointType.Main)
        FProcessSequence(MdlTable, "ProcessSequence_Log", EntryPointType.Log)

        FProcessSequenceDetail(MdlTable, "ProcessSequenceDetail", EntryPointType.Main)
        FProcessSequenceDetail(MdlTable, "ProcessSequenceDetail_Log", EntryPointType.Log)

        FBom(MdlTable, "BOM", EntryPointType.Main)
        FBom(MdlTable, "Bom_Log", EntryPointType.Log)

        FBomDetail(MdlTable, "BOMDetail", EntryPointType.Main)
        FBomDetail(MdlTable, "BomDetail_Log", EntryPointType.Log)

        FStock(MdlTable, "StockProcess", EntryPointType.Main)
        FStock(MdlTable, "StockVirtual", EntryPointType.Main)

        FStockUID(MdlTable, "StockUID", EntryPointType.Main)
        FStockUID(MdlTable, "StockUID_Log", EntryPointType.Log)



        FProdOrder(MdlTable, "ProdOrder", EntryPointType.Main)
        FProdOrder(MdlTable, "ProdOrder_Log", EntryPointType.Log)

        FProdOrderDetail(MdlTable, "ProdOrderDetail", EntryPointType.Main)
        FProdOrderDetail(MdlTable, "ProdOrderDetail_Log", EntryPointType.Log)

        FMaterialPlan(MdlTable, "MaterialPlan", EntryPointType.Main)
        FMaterialPlan(MdlTable, "MaterialPlan_Log", EntryPointType.Log)

        FMaterialPlanForDetail(MdlTable, "MaterialPlanForDetail", EntryPointType.Main)
        FMaterialPlanForDetail(MdlTable, "MaterialPlanForDetail_Log", EntryPointType.Log)

        FMaterialPlanDetail(MdlTable, "MaterialPlanDetail", EntryPointType.Main)
        FMaterialPlanDetail(MdlTable, "MaterialPlanDetail_Log", EntryPointType.Log)

        FMaterialPlanProcess(MdlTable, "MaterialPlanProcess", EntryPointType.Main)
    End Sub

    Public Sub UpdateTableStructureWorkOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FWorkOrder(MdlTable, "WorkOrder", EntryPointType.Main)
        FWorkOrder(MdlTable, "WorkOrder_Log", EntryPointType.Log)

        FWorkOrderDetail(MdlTable, "WorkOrderDetail", EntryPointType.Main)
        FWorkOrderDetail(MdlTable, "WorkOrderDetail_Log", EntryPointType.Log)

        FWorkOrderDeliveryDetail(MdlTable, "WorkOrderDeliveryDetail", EntryPointType.Main)
        FWorkOrderDeliveryDetail(MdlTable, "WorkOrderDeliveryDetail_Log", EntryPointType.Log)

        FWorkOrderBom(MdlTable, "WorkOrderBom", EntryPointType.Main)
        FWorkOrderBom(MdlTable, "WorkOrderBom_Log", EntryPointType.Log)

        FWorkDispatch(MdlTable, "WorkDispatch", EntryPointType.Main)
        FWorkDispatch(MdlTable, "WorkDispatch_Log", EntryPointType.Log)

        FWorkDispatchDetail(MdlTable, "WorkDispatchDetail", EntryPointType.Main)
        FWorkDispatchDetail(MdlTable, "WorkDispatchDetail_Log", EntryPointType.Log)

        FWorkInvoice(MdlTable, "WorkInvoice", EntryPointType.Main)
        FWorkInvoice(MdlTable, "WorkInvoice_Log", EntryPointType.Log)

        FWorkInvoiceDetail(MdlTable, "WorkInvoiceDetail", EntryPointType.Main)
        FWorkInvoiceDetail(MdlTable, "WorkInvoiceDetail_Log", EntryPointType.Log)
    End Sub

    Public Sub UpdateTableStructureJob(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FJobWorker(MdlTable, "JobWorker", EntryPointType.Main)
        FJobWorker(MdlTable, "JobWorker_Log", EntryPointType.Log)

        FJobWorkerProcess(MdlTable, "JobWorkerProcess", EntryPointType.Main)
        FJobWorkerProcess(MdlTable, "JobWorkerProcess_Log", EntryPointType.Log)

        FJobEnviro(MdlTable, "JobEnviro")

        FJobOrder(MdlTable, "JobOrder", EntryPointType.Main)
        FJobOrder(MdlTable, "JobOrder_Log", EntryPointType.Log)

        FStatus_Update(MdlTable, "Status_Update", EntryPointType.Main)

        FJobOrderDetail(MdlTable, "JobOrderDetail", EntryPointType.Main)
        FJobOrderDetail(MdlTable, "JobOrderDetail_Log", EntryPointType.Log)

        FJobOrderBom(MdlTable, "JobOrderBOM", EntryPointType.Main)
        FJobOrderBom(MdlTable, "JobOrderBOM_Log", EntryPointType.Log)

        FJobOrderQCInstruction(MdlTable, "JobOrderQCInstruction", EntryPointType.Main)
        FJobOrderQCInstruction(MdlTable, "JobOrderQCInstruction_Log", EntryPointType.Log)

        FJobOrderByProduct(MdlTable, "JobOrderByProduct", EntryPointType.Main)
        FJobOrderByProduct(MdlTable, "JobOrderByProduct_Log", EntryPointType.Log)

        FJobIssRecEnviro(MdlTable, "JobIssRecEnviro")

        FPurchaseEnviro(MdlTable, "PurchaseEnviro")

        FJobIssRec(MdlTable, "JobIssRec", EntryPointType.Main)
        FJobIssRec(MdlTable, "JobIssRec_Log", EntryPointType.Log)

        FJobIssRecUID(MdlTable, "JobIssRecUID", EntryPointType.Main)
        FJobIssRecUID(MdlTable, "JobIssRecUID_Log", EntryPointType.Log)

        FJobIssueDetail(MdlTable, "JobIssueDetail", EntryPointType.Main)
        FJobIssueDetail(MdlTable, "JobIssueDetail_Log", EntryPointType.Log)

        FJobReceiveDetail(MdlTable, "JobReceiveDetail", EntryPointType.Main)
        FJobReceiveDetail(MdlTable, "JobReceiveDetail_Log", EntryPointType.Log)

        FJobReceiveByProduct(MdlTable, "JobReceiveByProduct", EntryPointType.Main)
        FJobReceiveByProduct(MdlTable, "JobReceiveByProduct_Log", EntryPointType.Main)

        FJobReceiveBOM(MdlTable, "JobReceiveBOM", EntryPointType.Main)
        FJobReceiveBOM(MdlTable, "JobReceiveBOM_Log", EntryPointType.Log)

        FJobExchangeDetail(MdlTable, "JobExchangeDetail", EntryPointType.Main)
        FJobExchangeDetail(MdlTable, "JobExchangeDetail_Log", EntryPointType.Log)

        FJobInvoice(MdlTable, "JobInvoice", EntryPointType.Main)
        FJobInvoice(MdlTable, "JobInvoice_Log", EntryPointType.Log)

        FJobInvoiceDetail(MdlTable, "JobInvoiceDetail", EntryPointType.Main)
        FJobInvoiceDetail(MdlTable, "JobInvoiceDetail_Log", EntryPointType.Log)

        FJobInvoiceBOM(MdlTable, "JobInvoiceBom", EntryPointType.Main)
        FJobInvoiceBOM(MdlTable, "JobInvoiceBom_Log", EntryPointType.Log)

        FJobWorkerRateGroup(MdlTable, "JobWorkerRateGroup", EntryPointType.Main)
        FJobWorkerRateGroup(MdlTable, "JobWorkerRateGroup_Log", EntryPointType.Log)

		FJobWorkerRate(MdlTable, "JobWorkerRate", EntryPointType.Main)
        FJobWorkerRate(MdlTable, "JobWorkerRate_Log", EntryPointType.Log)

        FJobWorkerRateDetail(MdlTable, "JobWorkerRateDetail", EntryPointType.Main)
        FJobWorkerRateDetail(MdlTable, "JobWorkerRateDetail_Log", EntryPointType.Log)
    End Sub

    Public Sub UpdateTableStructurePurchase(ByRef MdlTable() As AgLibrary.ClsMain.LITable)

        FQCGroup(MdlTable, "QcGroup", EntryPointType.Main)
        FQCGroup(MdlTable, "QcGroup_Log", EntryPointType.Log)

        FQCGroupDetail(MdlTable, "QcGroupDetail", EntryPointType.Main)
        FQCGroupDetail(MdlTable, "QcGroupDetail_Log", EntryPointType.Log)

        FAgent(MdlTable, "Agent", EntryPointType.Main)
        FAgent(MdlTable, "Agent_Log", EntryPointType.Log)

        FAgentItem(MdlTable, "AgentItem", EntryPointType.Main)
        FAgentItem(MdlTable, "AgentItem_Log", EntryPointType.Log)

        FTransporter(MdlTable, "Transporter", EntryPointType.Main)
        FTransporter(MdlTable, "Transporter_Log", EntryPointType.Log)

        FVendor(MdlTable, "Vendor", EntryPointType.Main)
        FVendor(MdlTable, "Vendor_Log", EntryPointType.Log)

        FRequisition(MdlTable, "Requisition", EntryPointType.Main)
        FRequisition(MdlTable, "Requisition_Log", EntryPointType.Log)

        FRequisitionDetail(MdlTable, "RequisitionDetail", EntryPointType.Main)
        FRequisitionDetail(MdlTable, "RequisitionDetail_Log", EntryPointType.Log)

        FPurchaseIndent(MdlTable, "PurchIndent", EntryPointType.Main)
        FPurchaseIndent(MdlTable, "PurchIndent_Log", EntryPointType.Log)

        FPurchaseIndentDetail(MdlTable, "PurchIndentDetail", EntryPointType.Main)
        FPurchaseIndentDetail(MdlTable, "PurchIndentDetail_Log", EntryPointType.Log)

        FPurchaseIndentReq(MdlTable, "PurchIndentReq", EntryPointType.Main)
        FPurchaseIndentReq(MdlTable, "PurchIndentReq_Log", EntryPointType.Log)

        FPurchaseQuotation(MdlTable, "PurchQuotation", EntryPointType.Main)
        FPurchaseQuotation(MdlTable, "PurchQuotation_Log", EntryPointType.Log)

        FPurchaseQuotationDetail(MdlTable, "PurchQuotationDetail", EntryPointType.Main)
        FPurchaseQuotationDetail(MdlTable, "PurchQuotationDetail_Log", EntryPointType.Log)

        FPurchaseQuotSelection(MdlTable, "PurchQuotSelection", EntryPointType.Main)
        FPurchaseQuotSelection(MdlTable, "PurchQuotSelection_Log", EntryPointType.Log)

        FPurchaseQuotSelectionDetail(MdlTable, "PurchQuotSelectionDetail", EntryPointType.Main)
        FPurchaseQuotSelectionDetail(MdlTable, "PurchQuotSelectionDetail_Log", EntryPointType.Log)

        FPurchaseOrder(MdlTable, "PurchOrder", EntryPointType.Main)
        FPurchaseOrder(MdlTable, "PurchOrder_Log", EntryPointType.Log)

        FPurchaseOrderDetail(MdlTable, "PurchOrderDetail", EntryPointType.Main)
        FPurchaseOrderDetail(MdlTable, "PurchOrderDetail_Log", EntryPointType.Log)

        FPurchChallan(MdlTable, "PurchChallan", EntryPointType.Main)
        FPurchChallan(MdlTable, "PurchChallan_Log", EntryPointType.Log)

        FPurchChallanDetail(MdlTable, "PurchChallanDetail", EntryPointType.Main)
        FPurchChallanDetail(MdlTable, "PurchChallanDetail_Log", EntryPointType.Log)

        FQC(MdlTable, "QC", EntryPointType.Main)
        FQC(MdlTable, "QC_Log", EntryPointType.Log)

        FQcDetail(MdlTable, "QcDetail", EntryPointType.Main)
        FQcDetail(MdlTable, "QCDetail_Log", EntryPointType.Log)

        FQcParameterDetail(MdlTable, "QcParameterDetail", EntryPointType.Main)
        FQcParameterDetail(MdlTable, "QCParameterDetail_Log", EntryPointType.Log)

        FPurchInvoice(MdlTable, "PurchInvoice", EntryPointType.Main)
        FPurchInvoice(MdlTable, "PurchInvoice_Log", EntryPointType.Log)

        FPurchInvoiceDetail(MdlTable, "PurchInvoiceDetail", EntryPointType.Main)
        FPurchInvoiceDetail(MdlTable, "PurchInvoiceDetail_Log", EntryPointType.Log)

        FPurchaseOrderDeliveryDetail(MdlTable, "PurchOrderDeliveryDetail", EntryPointType.Main)
        FPurchaseOrderDeliveryDetail(MdlTable, "PurchOrderDeliveryDetail_Log", EntryPointType.Log)


    End Sub

    Public Sub UpdateTableStructureSales(ByRef MdlTable() As AgLibrary.ClsMain.LITable)

        FAgent(MdlTable, "Agent", EntryPointType.Main)
        FAgent(MdlTable, "Agent_Log", EntryPointType.Log)

        FAgentItem(MdlTable, "AgentItem", EntryPointType.Main)
        FAgentItem(MdlTable, "AgentItem_Log", EntryPointType.Log)

        FTransporter(MdlTable, "Transporter", EntryPointType.Main)
        FTransporter(MdlTable, "Transporter_Log", EntryPointType.Log)

        FSaleQuotation(MdlTable, "SaleQuotation", EntryPointType.Main)
        FSaleQuotation(MdlTable, "SaleQuotation_Log", EntryPointType.Log)

        FSaleQuotationDetail(MdlTable, "SaleQuotationDetail", EntryPointType.Main)
        FSaleQuotationDetail(MdlTable, "SaleQuotationDetail_Log", EntryPointType.Log)

        FSaleOrder(MdlTable, "SaleOrder", EntryPointType.Main)
        FSaleOrder(MdlTable, "SaleOrder_Log", EntryPointType.Log)

        FSalesOrderPriority(MdlTable, "FSaleOrderPriority", EntryPointType.Main)
        FSalesOrderPriority(MdlTable, "FSaleOrderPriority_Log", EntryPointType.Log)

        FSalesOrderPriorityDetail(MdlTable, "FSaleOrderPriorityDetail", EntryPointType.Main)
        FSalesOrderPriorityDetail(MdlTable, "FSaleOrderPriorityDetail_Log", EntryPointType.Log)

        FSaleOrderDetail(MdlTable, "SaleOrderDetail", EntryPointType.Main)
        FSaleOrderDetail(MdlTable, "SaleOrderDetail_Log", EntryPointType.Log)

        FSaleOrderDeliveryDetail(MdlTable, "SaleOrderDeliveryDetail", EntryPointType.Main)
        FSaleOrderDeliveryDetail(MdlTable, "SaleOrderDeliveryDetail_Log", EntryPointType.Log)

        FSaleOrderDeliveryChange(MdlTable, "SaleOrderDeliveryChange", EntryPointType.Main)
        FSaleOrderDeliveryChange(MdlTable, "SaleOrderDeliveryChange_Log", EntryPointType.Log)

        FSaleOrderDeliveryChangeOld(MdlTable, "SaleOrderDeliveryChangeOld", EntryPointType.Main)
        FSaleOrderDeliveryChangeOld(MdlTable, "SaleOrderDeliveryChangeOld_Log", EntryPointType.Log)

        FSaleOrderDeliveryChangeNew(MdlTable, "SaleOrderDeliveryChangeNew", EntryPointType.Main)
        FSaleOrderDeliveryChangeNew(MdlTable, "SaleOrderDeliveryChangeNew_Log", EntryPointType.Log)

        FDeliveryOrder(MdlTable, "DeliveryOrder", EntryPointType.Main)
        FDeliveryOrder(MdlTable, "DeliveryOrder_Log", EntryPointType.Log)

        FDeliveryOrderDetail(MdlTable, "DeliveryOrderDetail", EntryPointType.Main)
        FDeliveryOrderDetail(MdlTable, "DeliveryOrderDetail_Log", EntryPointType.Log)

        FSaleEnquiry(MdlTable, "SaleEnquiry", EntryPointType.Main)
        FSaleEnquiry(MdlTable, "SaleEnquiry_Log", EntryPointType.Log)

        FSaleEnquiryDetail(MdlTable, "SaleEnquiryDetail", EntryPointType.Main)
        FSaleEnquiryDetail(MdlTable, "SaleEnquiryDetail_Log", EntryPointType.Log)

        FSaleInvoice(MdlTable, "SaleInvoice", EntryPointType.Main)
        FSaleInvoice(MdlTable, "SaleInvoice_Log", EntryPointType.Log)

        FSaleInvoiceDetail(MdlTable, "SaleInvoiceDetail", EntryPointType.Main)
        FSaleInvoiceDetail(MdlTable, "SaleInvoiceDetail_Log", EntryPointType.Log)

        FSaleChallan(MdlTable, "SaleChallan", EntryPointType.Main)
        FSaleChallan(MdlTable, "SaleChallan_Log", EntryPointType.Log)

        FSaleChallanDetail(MdlTable, "SaleChallanDetail", EntryPointType.Main)
        FSaleChallanDetail(MdlTable, "SaleChallanDetail_Log", EntryPointType.Log)

        FSaleInvoiceOtherDetail(MdlTable, "SaleInvoiceOtherDetail", EntryPointType.Main)
        FSaleInvoiceOtherDetail(MdlTable, "SaleInvoiceOtherDetail_Log", EntryPointType.Main)

        FSaleQCReq(MdlTable, "SaleQCReq", EntryPointType.Main)
        FSaleQCReq(MdlTable, "SaleQCReq_Log", EntryPointType.Log)

        FSaleQCReqDetail(MdlTable, "SaleQCReqDetail", EntryPointType.Main)
        FSaleQCReqDetail(MdlTable, "SaleQCReqDetail_Log", EntryPointType.Log)

        FSaleQC(MdlTable, "SaleQC", EntryPointType.Main)
        FSaleQC(MdlTable, "SaleQC_Log", EntryPointType.Log)

        FSaleQCDetail(MdlTable, "SaleQCDetail", EntryPointType.Main)
        FSaleQCDetail(MdlTable, "SaleQCDetail_Log", EntryPointType.Log)
    End Sub

    Public Sub UpdateTableStructureExport(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FIE_Doc(MdlTable, "IE_Doc", EntryPointType.Main)
        FIE_Doc(MdlTable, "IE_Doc_Log", EntryPointType.Log)

        FIE_DocEntry(MdlTable, "IE_DocEntry", EntryPointType.Main)
        FIE_DocEntry(MdlTable, "IE_DocEntry_Log", EntryPointType.Log)

        FIE_DocAmend(MdlTable, "IE_DocAmend", EntryPointType.Main)
        FIE_DocAmend(MdlTable, "IE_DocAmend_Log", EntryPointType.Log)

        FIE_DocItem(MdlTable, "IE_DocItem", EntryPointType.Main)
        FIE_DocItem(MdlTable, "IE_DocItem_Log", EntryPointType.Log)

        FIE_Shipment(MdlTable, "IE_Shipment", EntryPointType.Main)
        FIE_Shipment(MdlTable, "IE_Shipment_Log", EntryPointType.Log)

        FIE_ShipmentDoc(MdlTable, "IE_ShipmentDoc", EntryPointType.Main)
        FIE_ShipmentDoc(MdlTable, "IE_ShipmentDoc_Log", EntryPointType.Log)

        FIE_ShipmentBOE(MdlTable, "IE_ShipmentBOE", EntryPointType.Main)
        FIE_ShipmentBOE(MdlTable, "IE_ShipmentBOE_Log", EntryPointType.Log)

        FIE_ShipmentItem(MdlTable, "IE_ShipmentItem", EntryPointType.Main)
        FIE_ShipmentItem(MdlTable, "IE_ShipmentItem_Log", EntryPointType.Log)

    End Sub

    Public Sub UpdateTableStructureForm(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FForm_Master(MdlTable, "Form_Master", EntryPointType.Main)
        FForm_Master(MdlTable, "Form_Master_Log", EntryPointType.Log)

        FForm_Receive(MdlTable, "Form_Receive", EntryPointType.Main)
        FForm_Receive(MdlTable, "Form_Receive_Log", EntryPointType.Log)

        FForm_ReceiveDetail(MdlTable, "Form_ReceiveDetail", EntryPointType.Main)
        FForm_ReceiveDetail(MdlTable, "Form_ReceiveDetail_Log", EntryPointType.Log)
    End Sub

    Public Sub UpdateTableStructureMachine(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FMachine_Breakdown_Reason(MdlTable, "Machine_Breakdown_Reason", EntryPointType.Main)
        FMachine_Breakdown_Reason(MdlTable, "Machine_Breakdown_Reason_Log", EntryPointType.Log)
        FMachine_Breakdown(MdlTable, "Machine_Breakdown", EntryPointType.Main)
        FMachine_Breakdown(MdlTable, "Machine_Breakdown_Log", EntryPointType.Log)
    End Sub

    Public Sub UpdateTableStructureFA(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FLedgerM(MdlTable, "LedgerM", EntryPointType.Main)
		FLedgerM(MdlTable, "LedgerM_Temp", EntryPointType.Main)
        FLedger(MdlTable, "Ledger", EntryPointType.Main)
        FLedger(MdlTable, "Ledger_Temp", EntryPointType.Main)
        FVoucher(MdlTable, "Voucher", EntryPointType.Main)
        FVoucherDetail(MdlTable, "VoucherDetail", EntryPointType.Main)
        FBank(MdlTable, "Bank", EntryPointType.Main)
        FBankBranch(MdlTable, "BankBranch", EntryPointType.Main)
        FNarrMast(MdlTable, "NarrMast", EntryPointType.Main)
        FCountry(MdlTable, "Country", EntryPointType.Main)
        FNature(MdlTable, "Nature", EntryPointType.Main)
        FChequePrint(MdlTable, "ChequePrint", EntryPointType.Main)
        FPermitForm(MdlTable, "PermitForm", EntryPointType.Main)
        FTdsCat_Det(MdlTable, "TdsCat_Det", EntryPointType.Main)
        FTDSCat(MdlTable, "TdsCat", EntryPointType.Main)
        FTDSCat_Description(MdlTable, "TDSCat_Description", EntryPointType.Main)
        FTDSLedger(MdlTable, "TdsLedger")
        FFaEnviro(MdlTable, "FaEnviro")
        FFaChequeDatail(MdlTable, "FaChequeDatail", EntryPointType.Main)
        FNarrationMast(MdlTable, "NarrationMast")

        FCostCenterMast(MdlTable, "CostCenterMast")
        FAcGroupPath(MdlTable, "AcGroupPath")
        FEnviro_Accounts(MdlTable, "Enviro_Accounts")
        FLedgerAdj(MdlTable, "LedgerAdj_Temp")
        FLedgerAdj(MdlTable, "LedgerAdj")
        FLedgerItemAdj(MdlTable, "LedgerItemAdj")
        FAcFilteration(MdlTable, "AcFilteration")
        FLedgerGroup(MdlTable, "LedgerGroup")
        FZoneMast(MdlTable, "ZoneMast")
        FDataTrfd(MdlTable, "DataTrfd")
    End Sub

    Public Sub UpdateTableStructure(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            FSubGroupType(MdlTable, "SubGroupType")

            FSynchronise_Error(MdlTable, "Synchronise_Error")

            FTable_SearchField(MdlTable, "Table_SearchField")

            FComputer(MdlTable, "Computer")
            FUnit(MdlTable, "Unit")

            FItemTags(MdlTable, "ItemTags")
            FTag(MdlTable, "Tag")

            FUnitConversion(MdlTable, "UnitConversion")

            FAcGroup(MdlTable, "AcGroup")

            FCompany(MdlTable, "Company")

            FCurrency(MdlTable, "Currency")

            FDivision(MdlTable, "Division")

            FEntryPointPermission(MdlTable, "EntryPointPermission")

            FEnviro(MdlTable, "Enviro")

            FLog_TablePermission(MdlTable, "Log_TablePermission")

            FLog_TableRecords(MdlTable, "Log_TableRecords")

            FLogin_Log(MdlTable, "Login_Log")

            FLogTable(MdlTable, "LogTable")

            FSiteMast(MdlTable, "SiteMast")

            FSubGroup(MdlTable, "Shift", EntryPointType.Main)
            FSubGroup(MdlTable, "Shift_Log", EntryPointType.Log)

            FSubGroup(MdlTable, "SubGroup", EntryPointType.Main)
            FSubGroup(MdlTable, "SubGroup_Log", EntryPointType.Log)

            FSubGroup_Image(MdlTable, "SubGroup_Image", EntryPointType.Main)
            FSubGroup_Image(MdlTable, "SubGroup_Image_Log", EntryPointType.Log)

            FCity(MdlTable, "City", EntryPointType.Main)
            FCity(MdlTable, "City_Log", EntryPointType.Log)

            FSeaPort(MdlTable, "SeaPort", EntryPointType.Main)
            FSeaPort(MdlTable, "SeaPort_Log", EntryPointType.Log)

            FItemInvoiceGroup(MdlTable, "ItemInvoiceGroup", EntryPointType.Main)
            FItemInvoiceGroup(MdlTable, "ItemInvoiceGroup_Log", EntryPointType.Log)

            FRateType(MdlTable, "RateType", EntryPointType.Main)
            FRateType(MdlTable, "RateType_Log", EntryPointType.Log)

            FRateList(MdlTable, "RateList", EntryPointType.Main)
            FRateList(MdlTable, "RateList_Log", EntryPointType.Log)

            FRateListDetail(MdlTable, "RateListDetail", EntryPointType.Main)
            FRateListDetail(MdlTable, "RateListDetail_Log", EntryPointType.Log)

            FItemProcessDetail(MdlTable, "ItemProcessDetail", EntryPointType.Main)
            FItemProcessDetail(MdlTable, "ItemProcessDetail_Log", EntryPointType.Log)

            FGodown(MdlTable, "Godown", EntryPointType.Main)
            FGodown(MdlTable, "Godown_Log", EntryPointType.Log)

            FGodownSection(MdlTable, "GodownSection", EntryPointType.Main)
            FGodownSection(MdlTable, "GodownSection_Log", EntryPointType.Log)

            FBuyer(MdlTable, "Buyer", EntryPointType.Main)
            FBuyer(MdlTable, "Buyer_Log", EntryPointType.Log)

            FItem(MdlTable, "Item", EntryPointType.Main)
            FItem(MdlTable, "Item_Log", EntryPointType.Log)

            FItem_Image(MdlTable, "Item_Image", EntryPointType.Main)

            FItemSiteDetail(MdlTable, "ItemSiteDetail", EntryPointType.Main)
            FItemSiteDetail(MdlTable, "ItemSiteDetail_Log", EntryPointType.Log)

            FItemRate(MdlTable, "ItemRate", EntryPointType.Main)
            FItemRate(MdlTable, "ItemRate_Log", EntryPointType.Log)

            FItemNature(MdlTable, "ItemNature", EntryPointType.Main)
            FItemNature(MdlTable, "ItemNature_Log", EntryPointType.Log)

            FItemCategory(MdlTable, "ItemCategory", EntryPointType.Main)
            FItemCategory(MdlTable, "ItemCategory_Log", EntryPointType.Log)

            FItemGroup(MdlTable, "ItemGroup", EntryPointType.Main)
            FItemGroup(MdlTable, "ItemGroup_Log", EntryPointType.Log)

            FItemBuyer(MdlTable, "ItemBuyer", EntryPointType.Main)
            FItemBuyer(MdlTable, "ItemBuyer_Log", EntryPointType.Log)

            FItem_UID(MdlTable, "Item_UID", EntryPointType.Main)
            FItem_UID(MdlTable, "Item_UID_Log", EntryPointType.Log)

            FDimension1(MdlTable, "Dimension1", EntryPointType.Main)
            FDimension1(MdlTable, "Dimension1_Log", EntryPointType.Main)

            FDimension2(MdlTable, "Dimension2", EntryPointType.Main)
            FDimension2(MdlTable, "Dimension2_Log", EntryPointType.Main)

            FStockHead(MdlTable, "StockHead", EntryPointType.Main)
            FStockHead(MdlTable, "StockHead_Log", EntryPointType.Log)

            FStock(MdlTable, "Stock", EntryPointType.Main)
            FStock(MdlTable, "Stock_Log", EntryPointType.Log)

            FStockAdj(MdlTable, "StockAdj")

            FStockHeadDetail(MdlTable, "StockHeadDetail", EntryPointType.Main)
            FStockHeadDetail(MdlTable, "StockHeadDetail_Log", EntryPointType.Log)

            FEmployee(MdlTable, "Employee", EntryPointType.Main)
            FEmployee(MdlTable, "Employee_Log", EntryPointType.Log)

            FDepartment(MdlTable, "Department", EntryPointType.Main)
            FDepartment(MdlTable, "Department_Log", EntryPointType.Log)

            FDesignation(MdlTable, "Designation", EntryPointType.Main)
            FDesignation(MdlTable, "Designation_Log", EntryPointType.Log)

            FEnviroDefaultGodown(MdlTable, "EnviroDefaultGodown")

            FUser_Control_Permission(MdlTable, "User_Control_Permission")

            FUser_Permission(MdlTable, "User_Permission")

            FUser_Exlude_VType(MdlTable, "User_Exclude_VType", EntryPointType.Main)
            FUser_Exlude_VType(MdlTable, "User_Exclude_VType_Log", EntryPointType.Log)

            FUser_Exlude_VTypeDetail(MdlTable, "User_Exclude_VTypeDetail", EntryPointType.Main)
            FUser_Exlude_VTypeDetail(MdlTable, "User_Exclude_VTypeDetail_Log", EntryPointType.Log)


            FUserMast(MdlTable, "UserMast")

            FUserSite(MdlTable, "UserSite")

            FVoucher_Exclude(MdlTable, "Voucher_Exclude")

            FVoucher_Include(MdlTable, "Voucher_Include")

            FVoucher_Prefix(MdlTable, "Voucher_Prefix")

            FVoucher_Prefix_Type(MdlTable, "Voucher_Prefix_Type")

            FVoucher_Type(MdlTable, "Voucher_Type")

            FVoucherCat(MdlTable, "VoucherCat")

            FGateInOut(MdlTable, "GateInOut", EntryPointType.Main)
            FGateInOut(MdlTable, "GateInOut_Log", EntryPointType.Log)

            FVisitorsGateInOut(MdlTable, "Visitor_GateInOut", EntryPointType.Main)
            FVisitorsGateInOut(MdlTable, "Visitor_GateInOut_Log", EntryPointType.Log)

            FDues(MdlTable, "Dues", EntryPointType.Main)
            FDues(MdlTable, "Dues_Log", EntryPointType.Main)

            FDuesPaymentEnviro(MdlTable, "DuesPaymentEnviro")

            FDuesPayment(MdlTable, "DuesPayment", EntryPointType.Main)
            FDuesPayment(MdlTable, "DuesPayment_Log", EntryPointType.Log)

            FDuesPaymentDetail(MdlTable, "DuesPaymentDetail", EntryPointType.Main)
            FDuesPaymentDetail(MdlTable, "DuesPaymentDetail_Log", EntryPointType.Log)

            FPhysicalStockDetail(MdlTable, "PhysicalStockDetail", EntryPointType.Main)
            FPhysicalStockDetail(MdlTable, "PhysicalStockDetail_Log", EntryPointType.Log)

            FTermsCondition(MdlTable, "TermsCondition", EntryPointType.Main)
            FTermsCondition(MdlTable, "TermsCondition_Log", EntryPointType.Log)

            FVoucher_Type_Settings(MdlTable, "Voucher_Type_Settings", EntryPointType.Main)
            FVoucher_Type_Settings(MdlTable, "Voucher_Type_Settings_Log", EntryPointType.Log)

            FMaster_Settings(MdlTable, "Master_Settings")

            FProcessGroup(MdlTable, "ProcessGroup", EntryPointType.Main)

            FItemType(MdlTable, "ItemType", EntryPointType.Main)

            FMailEnviro(MdlTable, "MailEnviro")
            FMailEnviroDetail(MdlTable, "MailEnviroDetail")

            FMailOutBox(MdlTable, "MailOutBox", EntryPointType.Main)
            FMailOutBox(MdlTable, "MailOutBox_Log", EntryPointType.Log)

            FMailOutBoxDetail(MdlTable, "MailOutBoxDetail", EntryPointType.Main)
            FMailOutBoxDetail(MdlTable, "MailOutBoxDetail_Log", EntryPointType.Log)

            FMailOutBoxAttachments(MdlTable, "MailOutBoxAttachments", EntryPointType.Main)
            FMailOutBoxAttachments(MdlTable, "MailOutBoxAttachments_Log", EntryPointType.Log)

            FMailSender(MdlTable, "MailSender", EntryPointType.Main)
            FMailSender(MdlTable, "MailSender_Log", EntryPointType.Log)

            FFollowUpType(MdlTable, "FollowUpType", EntryPointType.Main)
            FFollowUpType(MdlTable, "FollowUpType_Log", EntryPointType.Log)

            FFollowUpTypeDetail(MdlTable, "FollowUpTypeDetail", EntryPointType.Main)
            FFollowUpTypeDetail(MdlTable, "FollowUpTypeDetail_Log", EntryPointType.Log)

            FManufacturer(MdlTable, "Manufacturer", EntryPointType.Main)
            FManufacturer(MdlTable, "Manufacturer_Log", EntryPointType.Log)

            FVatCommodityCode(MdlTable, "VatCommodityCode", EntryPointType.Main)
            FVatCommodityCode(MdlTable, "VatCommodityCode_Log", EntryPointType.Log)

            FTariffHead(MdlTable, "TariffHead", EntryPointType.Main)
            FTariffHead(MdlTable, "TariffHead_Log", EntryPointType.Log)

            FItemReportingGroup(MdlTable, "ItemReportingGroup", EntryPointType.Main)
            FItemReportingGroup(MdlTable, "ItemReportingGroup_Log", EntryPointType.Log)

        'For Custom Fields

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTableInitialiser()
        Call CreateVType()

        Call CreateView()

        Call FInitialize_Unit()

        Call FInitialize_Charges()

        FInitialize_ItemType()
    End Sub

    Private Sub FInitialize_ItemType()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From ItemType ", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.ItemType (Code, Name) VALUES ('" & ItemType.FinishedMaterial & "', 'Finished Material') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('" & ItemType.SemiFinishedMaterial & "', 'Semi Finished Material') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('" & ItemType.RawMaterial & "', 'Raw Material') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('" & ItemType.ItemRateGroup & "', 'Item Rate Group') "
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub UpdateTableInitialiserProduction()
        Call FInitialize_ProcessGroup()
    End Sub

    Public Sub UpdateTableInitialiserFA()
        FInitialize_Nature()
        Finitialize_AcGroup()
        FInitialize_Subcode()
    End Sub

    Private Sub FInitialize_Subcode()
        Dim mQry$


        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'PURCHASE'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('PURCHASE', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Purchase A/c', 'Purchase A/c', '" & AcGroup.PurchaseAccounts & "', 'L', 'PURCHASE', '" & AcNature.Purchase & "', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'DISCOUNT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('DISCOUNT', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Discount A/c', 'Discount A/c', '" & AcGroup.DirectExpenses & "', 'L', 'DISCOUNT', 'Others', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'CASH'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('CASH', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Cash A/c', 'Cash A/c', '0021', 'A', 'CASH', 'Cash', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'BANK'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('BANK', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Bank A/c', 'Bank A/c', '0022', 'A', 'BANK', 'BANK', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'DNOTE'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('DNOTE', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Debit Note A/c', 'Debit Note A/c', '0009', 'A', 'DNOTE', 'Others', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'CNOTE'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('CNOTE', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'Credit Note A/c', 'Credit Note A/c', '0009', 'A', 'CNOTE', 'Others', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Subgroup Where Subcode = 'TDS'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.SubGroup (SubCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, U_Name, U_EntDt, U_AE, CommonAc) " & _
                 "VALUES ('TDS', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '|" & AgL.PubSiteCode & "|', 'TDS A/c', 'TDS A/c', '0014', 'L', 'TDS', 'Others', 'SA', '2008-10-21', 'A', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FInitialize_Unit()
        Dim mQry$
        If AgL.Dman_Execute("Select Count(*) From Unit Where Code = '" & Unit.Pcs & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into Unit (Code) Values ('" & Unit.Pcs & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) From Unit Where Code = '" & Unit.Kg & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into Unit (Code) Values ('" & Unit.Kg & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) From Unit Where Code = '" & Unit.Meter & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into Unit (Code) Values ('" & Unit.Meter & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) From Unit Where Code = '" & Unit.Liter & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into Unit (Code) Values ('" & Unit.Liter & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FInitialize_ProcessGroup()
        Dim mQry$
        If AgL.Dman_Execute("Select Count(*) From ProcessGroup Where Code = 'Finishing'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into ProcessGroup (Code, Description) Values ('Finishing','Finishing')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From ProcessGroup Where Code = 'Packing'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into ProcessGroup (Code, Description) Values ('Packing','Packing')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From ProcessGroup Where Code = 'Other'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into ProcessGroup (Code, Description) Values ('Other','Other')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub

    Public Class AcGroupCode
        Public Const CapitalAccount As String = "0001"
        Public Const Loan_Liability As String = "0002"
        Public Const CurrentLiabilities As String = "0003"
        Public Const FixedAssets As String = "0004"
        Public Const Investments As String = "0005"
        Public Const CurrentAssets As String = "0006"
        Public Const Branch_Divisions As String = "0007"
        Public Const MiscExp As String = "0008"
        Public Const SuspenseAc As String = "0009"
        Public Const ClosingStock As String = "000A"
        Public Const Reserves_Surplus As String = "0010"
        Public Const BankOdAc As String = "0011"
        Public Const SecuredLoan As String = "0012"
        Public Const UnSecuredLoan As String = "0013"
        Public Const Duties_Taxes As String = "0014"
        Public Const Provisions As String = "0015"
        Public Const SundryCreditors As String = "0016"
        Public Const OpeningStock As String = "0017"
        Public Const Deposits As String = "0018"
        Public Const Loans_Advances_Asset As String = "0019"
        Public Const SundryDebotors As String = "0020"
        Public Const CashInHand As String = "0021"
        Public Const BankAc As String = "0022"
        Public Const SalesAc As String = "0023"
        Public Const PurchaseAc As String = "0024"
        Public Const DirectIncomes As String = "0025"
        Public Const DirectExpenses As String = "0026"
        Public Const IndirectIncomes As String = "0027"
        Public Const IndirectExpenses As String = "0028"
        Public Const Profit_LossAc As String = "0029"
    End Class

    Private Sub Finitialize_AcGroup()
        Dim mQry$
        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Capital Account'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0001', NULL, 'Capital Account', 'Capital Account', NULL, 'A', 'Customer', 'Y', 'SA', '2003-04-01', 'A', 'N', '010', 10, 3, 1, '1', 'Capital Account', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Loan (Liability)'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0002', NULL, 'Loan (Liability)', 'Loan (Liability)', NULL, 'L', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '020', 20, 3, 2, '1', 'Loan (Liability)', 0, 4, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Current Liabilities'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0003', NULL, 'Current Liabilities', 'Current Liabilities', NULL, 'L', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '030', 30, 3, 3, '1', 'Current Liabilities', 0, 4, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Fixed Assets'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0004', NULL, 'Fixed Assets', 'Fixed Assets', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '040', 40, 3, 4, '1', 'Fixed Assets', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Investments'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0005', NULL, 'Investments', 'Investments', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '050', 50, 3, 5, '1', 'Investments', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Current Assets'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0006', NULL, 'Current Assets', 'Current Assets', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '060', 60, 3, 6, '1', 'Current Assets', 0, 8, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Branch/Divisions'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                  "VALUES ('0007', NULL, 'Branch/Divisions', 'Branch/Divisions', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '070', 70, 3, 7, '1', 'Branch/Divisions', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Misc. Expences (Asset)'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0008', NULL, 'Misc. Expences (Asset)', 'Misc. Expences (Asset)', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '080', 80, 3, 8, '1', 'Misc. Expences (Asset)', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Suspense A/c'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0009', NULL, 'Suspense A/c', 'Suspense A/c', NULL, 'A', 'Others', 'Y', 'SA', '2008-07-02', 'E', 'N', '090', 90, 3, 9, '1', 'Suspense A/c', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Closing Stock'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('000A', NULL, 'Closing Stock', 'Closing Stock', NULL, 'R', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '060007', 0, 6, 10, '1', 'Closing Stock', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Reserves & Surplus'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0010', NULL, 'Reserves & Surplus', 'Reserves & Surplus', NULL, 'L', 'Others', 'Y', 'SA', '2008-07-02', 'E', 'N', '010001', 100, 6, 11, '1', 'Reserves & Surplus', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Bank OD A/c'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0011', NULL, 'Bank OD A/c', 'Bank OD A/c', NULL, 'L', 'Others', 'Y', 'SA', '2003-04-01', 'E', 'N', '020001', 110, 6, 12, '1', 'Bank OD A/c', 1, 1, 0, 'N', 'N', NULL, 0)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Secured Loans'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "   VALUES ('0012', NULL, 'Secured Loans', 'Secured Loans', NULL, 'L', 'Others', 'Y', 'SA', '2008-07-02', 'E', 'N', '020002', 120, 6, 13, '1', 'Secured Loans', 1, 1, 0, 'N', 'N', NULL, 0)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Unsecured Loans'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "   VALUES ('0013', NULL, 'Unsecured Loans', 'Unsecured Loans', NULL, 'L', 'Others', 'Y', 'SA', '2008-07-02', 'E', 'N', '020003', 130, 6, 14, '1', 'Unsecured Loans', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Duties & Taxes'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0014', NULL, 'Duties & Taxes', 'Duties & Taxes', NULL, 'L', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '030001', 140, 6, 15, '1', 'Duties & Taxes', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Provisions'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                  "  VALUES ('0015', NULL, 'Provisions', 'Provisions', NULL, 'L', 'Expenses', 'Y', 'SA', '2008-07-02', 'E', 'N', '030002', 150, 6, 16, '1', 'Provisions', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Sundry Creditors'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0016', NULL, 'Sundry Creditors', 'Sundry Creditors', NULL, 'L', 'Supplier', 'Y', 'SA', '2008-07-02', 'E', 'N', '030003', 160, 6, 17, '1', 'Sundry Creditors', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Opening Stock'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0017', NULL, 'Opening Stock', 'Opening Stock', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '060001', 170, 6, 18, '1', 'Opening Stock', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Deposits (Asset)'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                   "VALUES ('0018', NULL, 'Deposits (Asset)', 'Deposits (Asset)', NULL, 'A', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '060002', 180, 6, 19, '1', 'Deposits (Asset)', 1, 1, 0, 'N', 'N', NULL, 0)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Loans & Advances (Asset)'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0019', NULL, 'Loans & Advances (Asset)', 'Loans & Advances (Asset)', NULL, 'A', 'Others', 'Y', 'SA', '2008-07-02', 'E', 'N', '060003', 190, 6, 20, '1', 'Loans & Advances (Asset)', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Sundry Debtors'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0020', NULL, 'Sundry Debtors', 'Sundry Debtors', NULL, 'A', 'Customer', 'Y', 'SA', '2008-07-07', 'E', 'N', '060004', 200, 6, 21, '1', 'Sundry Debtors', 1, 1, 0, 'N', 'N', NULL, 0)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Cash-in-Hand'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                  "VALUES ('0021', NULL, 'Cash-in-Hand', 'Cash-in-Hand', NULL, 'A', 'Cash', 'Y', 'SA', '2008-07-05', 'E', 'N', '060005', 210, 6, 22, '1', 'Cash-in-Hand', 1, 1, 0, 'N', 'N', NULL, 0)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Bank Accounts'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0022', NULL, 'Bank Accounts', 'Bank Accounts', NULL, 'A', 'Bank', 'Y', 'SA', '2003-04-01', 'E', 'N', '060006', 220, 6, 23, '1', 'Bank Accounts', 1, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Sales Accounts'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0023', NULL, 'Sales Accounts', 'Sales Accounts', NULL, 'R', 'Sale', 'Y', 'SA', '2003-04-01', 'A', 'Y', '230', 230, 3, 24, '1', 'Sales Accounts', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Purchase Accounts'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0024', NULL, 'Purchase Accounts', 'Purchase Accounts', NULL, 'E', 'Purchase', 'Y', 'SA', '2003-04-01', 'A', 'Y', '240', 240, 3, 25, '1', 'Purchase Accounts', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Direct Incomes'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0025', NULL, 'Direct Incomes', 'Direct Incomes', NULL, 'R', 'Others', 'Y', 'SA', '2003-04-01', 'E', 'Y', '250', 250, 3, 26, '1', 'Direct Incomes', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Direct Expenses'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                   "VALUES ('0026', NULL, 'Direct Expenses', 'Direct Expenses', NULL, 'E', 'Others', 'Y', 'SA', '2003-04-01', 'E', 'Y', '260', 260, 3, 27, '1', 'Direct Expenses', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Indirect Incomes'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0027', NULL, 'Indirect Incomes', 'Indirect Incomes', NULL, 'R', 'Others', 'Y', 'SA', '2003-04-01', 'E', 'N', '270', 270, 3, 28, '1', 'Indirect Incomes', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Indirect Expenses'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                 "VALUES ('0028', NULL, 'Indirect Expenses', 'Indirect Expenses', NULL, 'E', 'Others', 'Y', 'SA', '2008-07-05', 'E', 'N', '280', 280, 3, 29, '1', 'Indirect Expenses', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From AcGroup Where GroupName = 'Profit & Loss A/c'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance) " & _
                   "VALUES ('0029', NULL, 'Profit & Loss A/c', 'Profit & Loss A/c', NULL, 'L', 'Others', 'Y', 'SA', '2003-04-01', 'A', 'N', '999', 290, 3, 30, '1', 'Profit & Loss A/c', 0, 1, 0, 'N', 'N', NULL, 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FInitialize_Nature()
        Dim mQry$

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Bank'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "   VALUES ('Bank', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Cash'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Cash', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Customer'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Customer', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Employee'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Employee', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Expenses'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Expenses', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Others'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Others', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Purchase'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Purchase', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Sales'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Sales', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Supplier'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Supplier', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'T.D.S.'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('T.D.S.', 0) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Transporter'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Transporter', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Nature Where Nature = 'Unloader'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Nature (Nature, Personal_Nature) " & _
                 "VALUES ('Unloader', 1) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

    End Sub


    Private Sub FInitialize_Charges()
        Dim mQry$
        Dim mTableName As String = "Charges"

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'ADUTY'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate) " & _
                    " Values ('ADUTY', 'Additional Duty', 'ADUTY', 'D', '1', 'SUPER', '2011-09-17', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'CD'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
                    " Values ('CD', 'Custom Duty', 'CD', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'CDEC'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('CDEC', 'Custom Duty ECess', 'CDEC', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'CDHEC'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('CDHEC', 'Custom Duty HECess', 'CDHEC', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'CDTA'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('CDTA', 'Custom Duty Taxable Amt', 'CDTA', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'CST'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('CST', 'Cst', 'CST', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'DIS'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('DIS', 'Discount', 'DIS', 'M', '1', 'SUPER', '26/Aug/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'DPTAX'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('DPTAX', 'Discount Pre Tax', 'DPTAX', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'FRT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('FRT', 'Freight', 'FRT', 'D', '1', 'SUPER', '23/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'GAMT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('GAMT', 'Gross Amount', 'GAMT', 'D', '1', 'sa', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'HCHRG'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('HCHRG', 'Handling Charges', 'HCHRG', 'D', '1', 'SUPER', '23/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'INCENT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('INCENT', 'Incentive', 'INCENT', 'D', '1', 'sa', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'INS'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('INS', 'Insurance', 'INS', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'NAMT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('NAMT', 'Net Amount', 'NAMT', 'D', '1', 'sa', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'OAPTAX'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('OAPTAX', 'Other Additions Pre Tax', 'OAPTAX', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'OC'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('OC', 'Other Charges', 'OC', 'M', '1', 'SUPER', '26/Aug/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'PENALTY'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('PENALTY', 'Penalty', 'PENALTY', 'D', '1', 'sa', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'RO'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('RO', 'Round Off', 'RO', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'SAT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('SAT', 'Sat', 'SAT', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'STOT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('STOT', 'Sub Total', 'STOT', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'STTA'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('STTA', 'Sales Tax Taxable Amt', 'STTA', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'TCD'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('TCD', 'Total Custom Duty', 'TCD', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'VAT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)    " & _
            " Values ('VAT', 'Vat', 'VAT', 'D', '1', 'SUPER', '17/Sep/2011 12:00:00 AM', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'LV'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('LV', 'Landed Value', 'LV', 'M', '1', 'SUPER', '2011-08-26', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'SECESS'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('SECESS', 'Service Tax ECess', 'SECESS', 'M', '1', 'SA', '2011-10-23', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'SERV'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('SERV', 'Service Tax', 'SERV', 'M', '1', 'SA', '2011-10-19', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'SERV_AA'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('SERV_AA', 'Service Tax Assesable Amt', 'SERV_AA', 'M', '1', 'SA', '2011-10-23', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'SHECESS'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('SHECESS', 'Service Tax HECess', 'SHECESS', 'M', '1', 'SA', '2011-10-23', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From Charges Where Code = 'TFRT'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "INSERT INTO dbo.Charges (Code, Description, ManualCode, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)" & _
                " VALUES ('TFRT', 'Total Freight', 'TFRT', 'M', '1', 'SA', '2011-10-20', 'A', NULL, NULL, NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub




    Sub AddNewField()
        Dim mQry$ = ""
        Try
            ''============================< Table Name >====================================================
            'AgL.AddNewField(AgL.GCn, "Sch_Subject", "IsGeneralProficiency", "bit", 0, False)
            ''============================< ************************* >=====================================
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Sub EditField()
        Try
            'AgL.EditField("Sch_Admission", "AdmissionID", "nVarChar(61)", AgL.GCn, False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub CreateView()
        Dim mQry$ = ""
        '' Note Write Each View in Separate <Try---Catch> Section

        Try
            'mQry = "CREATE VIEW dbo.ViewSch_SessionProgramme AS " & _
            '        " SELECT  SP.*, S.ManualCode AS SessionManualCode, S.Description AS SessionDescription, S.StartDate AS SessionStartDate, S.EndDate AS SessionEndDate, P.Description AS ProgrammeDescription, P.ManualCode AS ProgrammeManualCode, P.ProgrammeDuration, P.Semesters AS ProgrammeSemesters, P.SemesterDuration AS ProgrammeSemesterDuration, P.ProgrammeNature , PN.Description AS ProgrammeNatureDescription  , P.ManualCode  +'/' + S.ManualCode   AS SessionProgramme " & _
            '        " FROM Sch_SessionProgramme SP " & _
            '        " LEFT JOIN Sch_Session S ON sp.Session =S.Code  " & _
            '        " LEFT JOIN Sch_Programme P ON SP.Programme =P.Code " & _
            '        " LEFT JOIN Sch_ProgrammeNature PN ON P.ProgrammeNature =PN.Code "

            'AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GCn, True)
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            'If AgL.PubOfflineApplicable Then
            '    AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GcnSite, True)
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            mQry = " Create View [ViewSubGroup] As  " & _
                    " SELECT SubGroup.SubCode AS AcId, SubGroup.SubCode AS AcCode, SubGroup.Site_Code, " & _
                    " SubGroup.SubCode, LEFT(SubGroup.Name COLLATE DATABASE_DEFAULT + IfNull(', ' + City.CityName  COLLATE DATABASE_DEFAULT , ''), 50) AS Name,  " & _
                    " SubGroup.GroupCode, SubGroup.GroupNature, " & _
                    " SubGroup.Nature, 'N' AS AliasYN, SubGroup.Add1, SubGroup.Add2, SubGroup.Add3, SubGroup.CityCode, " & _
                    " SubGroup.PIN, SubGroup.Phone AS PHONEl, AcGroup.MainGrCode AS MainGrCodeS, 'N' AS GAliasYn,  " & _
                    " AcGroup.GroupName AS GName, AcGroup.SysGroup AS SysGrp,  " & _
                    " LEFT(SubGroup.Name  COLLATE DATABASE_DEFAULT + IfNull(', ' + City.CityName  COLLATE DATABASE_DEFAULT, ''), 50) AS NameWithCity,  City.CityName,  Mobile, Fax, Email,                         AcGroup.GroupName, AcGroup.TradingYn, AcGroup.BlOrd, 'N' AS SubAliasYn, SubGroup.FatherName AS FName,                        SubGroup.CreditLimit, SubGroup.Phone, SubGroup.DueDays, SubGroup.Curr_Bal, IfNull(SubGroup.CommonAc,0) As CommonAc   " & _
                    " FROM  SubGroup  " & _
                    " LEFT OUTER JOIN AcGroup ON SubGroup.GroupCode = AcGroup.GroupCode                        " & _
                    " LEFT OUTER JOIN City ON SubGroup.CityCode = City.CityCode "

            AgL.IsViewExist("ViewSubGroup", AgL.GCn, True)
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            If AgL.PubOfflineApplicable Then
                AgL.IsViewExist("ViewSubGroup", AgL.GcnSite, True)
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub DropTable()
        Dim mQry$ = ""
        Try
            'If AgL.IsTableExist("Test", AgL.GCn) Then
            '    AgL.Dman_ExecuteNonQry("DROP TABLE Test", AgL.GCn)
            'End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateVType()
        Try
            'FA VType
            '===================================================< Stock Opening V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.FAOpeningBalance, Temp_NCat.FAOpeningBalance, "Opening Balance", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.FAOpeningBalance, Temp_NCat.FAOpeningBalance, Temp_NCat.FAOpeningBalance, "Opening Balance", Temp_NCat.FAOpeningBalance, AgL.PubUserName, AgL.PubLoginDate, AgL.RetDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)).ToString), AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrderAmendment)
            End Try


            'Common VType
            '===================================================< Stock Opening V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StockOpening, Temp_NCat.StockOpening, "Stock Opening", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StockOpening, Temp_NCat.StockOpening, Temp_NCat.StockOpening, "Stock Opening", Temp_NCat.StockOpening, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Store Store Requisition V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StoreRequisition, Temp_NCat.StoreRequisition, "Store Requisition", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreRequisition, Temp_NCat.StoreRequisition, Temp_NCat.StoreRequisition, "Store Requisition", Temp_NCat.StoreRequisition, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Store Issue Issue V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StoreIssue, Temp_NCat.StoreIssue, "Store Issue", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreIssue, Temp_NCat.StoreIssue, Temp_NCat.StoreIssue, "Store Issue", Temp_NCat.StoreIssue, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Store Receive V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StoreReceive, Temp_NCat.StoreReceive, "Store Receive", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreReceive, Temp_NCat.StoreReceive, Temp_NCat.StoreReceive, "Store Receive", Temp_NCat.StoreReceive, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Stock Transfer  V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StockTransfer, Temp_NCat.StockTransfer, "Stock Transfer", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StockTransfer, Temp_NCat.StockTransfer, Temp_NCat.StockTransfer, "Stock Transfer", Temp_NCat.StockTransfer, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Stock Transfer Issue V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StockTransferIssue, Temp_NCat.StockTransferIssue, "Stock Transfer Issue", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StockTransferIssue, Temp_NCat.StockTransferIssue, Temp_NCat.StockTransferIssue, "Stock Transfer Issue", Temp_NCat.StockTransferIssue, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Stock Transfer Receive V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.StockTransferReceive, Temp_NCat.StockTransferReceive, "Stock Transfer Receive", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StockTransferReceive, Temp_NCat.StockTransferReceive, Temp_NCat.StockTransferReceive, "Stock Transfer Receive", Temp_NCat.StockTransferReceive, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Physical Stock Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.PhysicalStockEntry, Temp_NCat.PhysicalStockEntry, "Physical Stock Entry", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PhysicalStockEntry, Temp_NCat.PhysicalStockEntry, Temp_NCat.PhysicalStockEntry, "Physical Stock Entry", Temp_NCat.PhysicalStockEntry, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Physical Stock Adjustment Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.PhysicalStockAdjustmentEntry, Temp_NCat.PhysicalStockAdjustmentEntry, "Physical Stock Adjustment Entry", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PhysicalStockAdjustmentEntry, Temp_NCat.PhysicalStockAdjustmentEntry, Temp_NCat.PhysicalStockAdjustmentEntry, "Physical Stock Adjustment Entry", Temp_NCat.PhysicalStockAdjustmentEntry, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Internal Process V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.InternalProcess, Temp_NCat.InternalProcess, "Internal Process", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.InternalProcess, Temp_NCat.InternalProcess, Temp_NCat.InternalProcess, "Internal Process", Temp_NCat.InternalProcess, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            'Production VType
            '===================================================< Production Order V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.ProductionOrder, Temp_NCat.ProductionOrder, "Production Order", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.ProductionOrder, Temp_NCat.ProductionOrder, Temp_NCat.ProductionOrder, "Production Order", Temp_NCat.ProductionOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Production Order Cancel V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.ProductionOrderCancel, Temp_NCat.ProductionOrderCancel, "Production Order Cancel", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.ProductionOrderCancel, Temp_NCat.ProductionOrderCancel, Temp_NCat.ProductionOrderCancel, "Production Order Cancel", Temp_NCat.ProductionOrderCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Production Plan V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.ProductionPlan, Temp_NCat.ProductionPlan, "Production Plan", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.ProductionPlan, Temp_NCat.ProductionPlan, Temp_NCat.ProductionPlan, "Production Plan", Temp_NCat.ProductionPlan, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Material Plan V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.MaterialPlan, Temp_NCat.MaterialPlan, "Material Plan", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.MaterialPlan, Temp_NCat.MaterialPlan, Temp_NCat.MaterialPlan, "Material Plan", Temp_NCat.MaterialPlan, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Material Plan Amendment V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, ClsMain.Temp_NCat.MaterialPlanAmendment, ClsMain.Temp_NCat.MaterialPlanAmendment, "Material Plan Amendment", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, ClsMain.Temp_NCat.MaterialPlanAmendment, ClsMain.Temp_NCat.MaterialPlanAmendment, ClsMain.Temp_NCat.MaterialPlanAmendment, "Material Plan Amendment", ClsMain.Temp_NCat.MaterialPlanAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.MaterialPlanAmendment)
            End Try

            '===================================================< Work Order V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkOrder, Temp_NCat.WorkOrder, "Work Order", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkOrder, Temp_NCat.WorkOrder, Temp_NCat.WorkOrder, "Work Order", Temp_NCat.WorkOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrder)
            End Try


            '===================================================< Work Order Plan V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkOrderPlan, Temp_NCat.WorkOrderPlan, "Work Order Plan", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkOrderPlan, Temp_NCat.WorkOrderPlan, Temp_NCat.WorkOrderPlan, "Work Order Plan", Temp_NCat.WorkOrderPlan, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrderPlan)
            End Try

            '===================================================< Work Order Plan Amendment V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkOrderPlanAmendment, Temp_NCat.WorkOrderPlanAmendment, "Work Order Plan Amendment", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkOrderPlanAmendment, Temp_NCat.WorkOrderPlanAmendment, Temp_NCat.WorkOrderPlanAmendment, "Work Order Plan Amendment", Temp_NCat.WorkOrderPlanAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrderPlanAmendment)
            End Try


            '===================================================< Work Order Cancel V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkOrderCancel, Temp_NCat.WorkOrderCancel, "Work Order Cancel", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkOrderCancel, Temp_NCat.WorkOrderCancel, Temp_NCat.WorkOrderCancel, "Work Order Cancel", Temp_NCat.WorkOrderCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrderCancel)
            End Try

            '===================================================< Work Order Amendment V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkOrderAmendment, Temp_NCat.WorkOrderAmendment, "Work Order Amendment", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkOrderAmendment, Temp_NCat.WorkOrderAmendment, Temp_NCat.WorkOrderAmendment, "Work Order Amendment", Temp_NCat.WorkOrderAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkOrderAmendment)
            End Try


            '===================================================< Work Dispatch V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkDispatch, Temp_NCat.WorkDispatch, "Work Dispatch", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkDispatch, Temp_NCat.WorkDispatch, Temp_NCat.WorkDispatch, "Work Dispatch", Temp_NCat.WorkDispatch, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkDispatch)
            End Try

            '===================================================< Work Invoice V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.WorkInvoice, Temp_NCat.WorkInvoice, "Work Invoice", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.WorkInvoice, Temp_NCat.WorkInvoice, Temp_NCat.WorkInvoice, "Work Invoice", Temp_NCat.WorkInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.WorkInvoice)
            End Try



            '===================================================< Job Order V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobOrder, Temp_NCat.JobOrder, "Job Order", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobOrder, Temp_NCat.JobOrder, Temp_NCat.JobOrder, "Job Order", Temp_NCat.JobOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Issue V_Type >===================================================

            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreIssue, Temp_NCat.StoreIssue, Temp_VType.JobIssue, "Material Issue To JobWork", Temp_VType.JobIssue, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreReceive, Temp_NCat.StoreReceive, Temp_VType.JobReturn, "Material Returned From JobWork", Temp_VType.JobReturn, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreReceive, Temp_NCat.StoreReceive, Temp_VType.WorkMaterialReceive, "Material Recieve From WorkOrder", Temp_VType.WorkMaterialReceive, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            AgL.CreateVType(AgL.GCn, Temp_NCat.StoreIssue, Temp_NCat.StoreIssue, Temp_VType.WorkMaterialReturn, "Material Returned To WorkOrder", Temp_VType.WorkMaterialReturn, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Receive V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobReceive, Temp_NCat.JobReceive, "Job Receive", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobReceive, Temp_NCat.JobReceive, Temp_NCat.JobReceive, "Job Receive", Temp_NCat.JobReceive, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Invoice V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobInvoice, Temp_NCat.JobInvoice, "Job Invoice", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobInvoice, Temp_NCat.JobInvoice, Temp_NCat.JobInvoice, "Job Invoice", Temp_NCat.JobInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Order Cancel V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobOrderCancel, Temp_NCat.JobOrderCancel, "Job Order Cancel", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobOrderCancel, Temp_NCat.JobOrderCancel, Temp_NCat.JobOrderCancel, "Job Order Cancel", Temp_NCat.JobOrderCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Order Amendment V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobOrderAmendment, Temp_NCat.JobOrderAmendment, "Job Order Amendment", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobOrderAmendment, Temp_NCat.JobOrderAmendment, Temp_NCat.JobOrderAmendment, "Job Order Amendment", Temp_NCat.JobOrderAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Job Invoice Amendment V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.JobInvoiceAmendment, Temp_NCat.JobInvoiceAmendment, "Job Invoice Amendment", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.JobInvoiceAmendment, Temp_NCat.JobInvoiceAmendment, Temp_NCat.JobInvoiceAmendment, "Job Invoice Amendment", Temp_NCat.JobInvoiceAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.JobConsumption, Temp_NCat.JobConsumption, "Job Consumption", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.JobConsumption, Temp_NCat.JobConsumption, Temp_NCat.JobConsumption, "Job Consumption", Temp_NCat.JobConsumption, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.JobConsumption)
            End Try

            '===================================================< Rate Conversion V_Type >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.JobRateConversion, Temp_NCat.JobRateConversion, "Rate Conversion", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.JobRateConversion, Temp_NCat.JobRateConversion, Temp_NCat.JobRateConversion, "Rate Conversion", Temp_NCat.JobRateConversion, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.JobRateConversion)
            End Try


            'Purchase VType

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseIndent, Temp_NCat.PurchaseIndent, "Purchase Indent", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseIndent, Temp_NCat.PurchaseIndent, Temp_NCat.PurchaseIndent, "Purchase Indent", Temp_NCat.PurchaseIndent, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseIndentCancel, Temp_NCat.PurchaseIndentCancel, "Purchase Indent Cancel", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseIndentCancel, Temp_NCat.PurchaseIndentCancel, Temp_NCat.PurchaseIndentCancel, "Purchase Indent Cancel", Temp_NCat.PurchaseIndentCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseQuotation, Temp_NCat.PurchaseQuotation, "Purchase Quotation", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseQuotation, Temp_NCat.PurchaseQuotation, Temp_NCat.PurchaseQuotation, "Purchase Quotation", Temp_NCat.PurchaseQuotation, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseQuotSelection, Temp_NCat.PurchaseQuotSelection, "Purchase Quotation Selection", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseQuotSelection, Temp_NCat.PurchaseQuotSelection, Temp_NCat.PurchaseQuotSelection, "Purchase Quotation Selection", Temp_NCat.PurchaseQuotSelection, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseOrder, Temp_NCat.PurchaseOrder, "Purchase Order", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseOrder, Temp_NCat.PurchaseOrder, Temp_NCat.PurchaseOrder, "Purchase Order", Temp_NCat.PurchaseOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseOrderAmendment, Temp_NCat.PurchaseOrderAmendment, "Purchase Order Amendment", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseOrderAmendment, Temp_NCat.PurchaseOrderAmendment, Temp_NCat.PurchaseOrderAmendment, "Purchase Order Amendment", Temp_NCat.PurchaseOrderAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseOrderCancel, Temp_NCat.PurchaseOrderCancel, "Purchase Order Cancel", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseOrderCancel, Temp_NCat.PurchaseOrderCancel, Temp_NCat.PurchaseOrderCancel, "Purchase Order Cancel", Temp_NCat.PurchaseOrderCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.GoodsReceipt, Temp_NCat.GoodsReceipt, "Purchase Challan", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.GoodsReceipt, Temp_NCat.GoodsReceipt, Temp_NCat.GoodsReceipt, "Purchase Challan", Temp_NCat.GoodsReceipt, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseInvoice, Temp_NCat.PurchaseInvoice, "Purchase Invoice", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseInvoice, Temp_NCat.PurchaseInvoice, Temp_NCat.PurchaseInvoice, "Purchase Invoice", Temp_NCat.PurchaseInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseReturn, Temp_NCat.PurchaseReturn, "Purchase Return", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseReturn, Temp_NCat.PurchaseReturn, Temp_NCat.PurchaseReturn, "Purchase Return", Temp_NCat.PurchaseReturn, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            Try
                AgL.CreateNCat(AgL.GCn, Temp_NCat.PurchaseChallanReturn, Temp_NCat.PurchaseChallanReturn, "Purchase Challan Return", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, Temp_NCat.PurchaseChallanReturn, Temp_NCat.PurchaseChallanReturn, Temp_NCat.PurchaseChallanReturn, "Purchase Challan Return", Temp_NCat.PurchaseChallanReturn, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.PurchaseReturn)
            End Try

            'Sale VType

            '===================================================< Sale Order V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleOrder, Temp_NCat.SaleOrder, "Sale Order", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleOrder, Temp_NCat.SaleOrder, Temp_NCat.SaleOrder, "Sale Order", Temp_NCat.SaleOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Order Cancel V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleOrderCancel, Temp_NCat.SaleOrderCancel, "Sale Order Cancel", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleOrderCancel, Temp_NCat.SaleOrderCancel, Temp_NCat.SaleOrderCancel, "Sale Order Cancel", Temp_NCat.SaleOrderCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Order Amendment V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleOrderAmendment, Temp_NCat.SaleOrderAmendment, "Sale Order Amendment", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleOrderAmendment, Temp_NCat.SaleOrderAmendment, Temp_NCat.SaleOrderAmendment, "Sale Order Amendment", Temp_NCat.SaleOrderAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Challan V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleChallan, Temp_NCat.SaleChallan, "Sale Challan", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleChallan, Temp_NCat.SaleChallan, Temp_NCat.SaleChallan, "Sale Challan", Temp_NCat.SaleChallan, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Invoice V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleInvoice, Temp_NCat.SaleInvoice, "Sale Invoice", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleInvoice, Temp_NCat.SaleInvoice, Temp_NCat.SaleInvoice, "Sale Invoice", Temp_NCat.SaleInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Return V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleReturn, Temp_NCat.SaleReturn, "Sale Return", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleReturn, Temp_NCat.SaleReturn, Temp_NCat.SaleReturn, "Sale Return", Temp_NCat.SaleReturn, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale QC Request V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleQCRequest, Temp_NCat.SaleQCRequest, "Sale QC Request", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleQCRequest, Temp_NCat.SaleQCRequest, Temp_NCat.SaleQCRequest, "Sale QC Request", Temp_NCat.SaleQCRequest, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Enquiry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleEnquiry, Temp_NCat.SaleEnquiry, "Sale Enquiry", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleEnquiry, Temp_NCat.SaleEnquiry, Temp_NCat.SaleEnquiry, "Sale Enquiry", Temp_NCat.SaleEnquiry, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Quotation V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleQuotation, Temp_NCat.SaleQuotation, "Sale Quotation", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleQuotation, Temp_NCat.SaleQuotation, Temp_NCat.SaleQuotation, "Sale Quotation", Temp_NCat.SaleQuotation, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Rate Contract V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleRateContract, Temp_NCat.SaleRateContract, "Sale Rate Contract", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleRateContract, Temp_NCat.SaleRateContract, Temp_NCat.SaleRateContract, "Sale Rate Contract", Temp_NCat.SaleRateContract, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale QC V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.SaleQC, Temp_NCat.SaleQC, "Sale QC", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.SaleQC, Temp_NCat.SaleQC, Temp_NCat.SaleQC, "Sale QC", Temp_NCat.SaleQC, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Sale Order Plan >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, ClsMain.Temp_NCat.SaleOrderPlan, ClsMain.Temp_NCat.SaleOrderPlan, "Sale Order Plan", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, ClsMain.Temp_NCat.SaleOrderPlan, ClsMain.Temp_NCat.SaleOrderPlan, ClsMain.Temp_NCat.SaleOrderPlan, "Sale Order Plan", ClsMain.Temp_NCat.SaleOrderPlan, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.SaleOrderPlan)
            End Try

            '===================================================< Sale Order Plan Amendment >===================================================
            Try
                AgL.CreateNCat(AgL.GCn, ClsMain.Temp_NCat.SaleOrderPlanAmendment, ClsMain.Temp_NCat.SaleOrderPlanAmendment, "Sale Order Plan Amendment", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, ClsMain.Temp_NCat.SaleOrderPlanAmendment, ClsMain.Temp_NCat.SaleOrderPlanAmendment, ClsMain.Temp_NCat.SaleOrderPlanAmendment, "Sale Order Plan Amendment", ClsMain.Temp_NCat.SaleOrderPlanAmendment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.SaleOrderPlanAmendment)
            End Try





            '===================================================< Document Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.DocumentEntry, Temp_NCat.DocumentEntry, "Document Entry", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.DocumentEntry, Temp_NCat.DocumentEntry, Temp_NCat.DocumentEntry, "Document Entry", Temp_NCat.DocumentEntry, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Shipment Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.ShipmentEntry, Temp_NCat.ShipmentEntry, "Shipment Entry", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.ShipmentEntry, Temp_NCat.ShipmentEntry, Temp_NCat.ShipmentEntry, "Shipment Entry", Temp_NCat.ShipmentEntry, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Form Receive Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.FormReceive, Temp_NCat.FormReceive, "Form Receive", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.FormReceive, Temp_NCat.FormReceive, Temp_NCat.FormReceive, "Form Receive", Temp_NCat.FormReceive, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================< Form Issue Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.FormIssue, Temp_NCat.FormIssue, "Form Issue", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.FormIssue, Temp_NCat.FormIssue, Temp_NCat.FormIssue, "Form Issue", Temp_NCat.FormIssue, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            'Payment VType
            '===================================================<Payment Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.Payment, Temp_NCat.Payment, "Payment", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.Payment, Temp_NCat.Payment, Temp_NCat.Payment, "Payment", Temp_NCat.Payment, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.Receipt, Temp_NCat.Receipt, "Receipt", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.Receipt, Temp_NCat.Receipt, Temp_NCat.Receipt, "Receipt", Temp_NCat.Receipt, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.DebitNote, Temp_NCat.DebitNote, "Debit Note.", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.DebitNote, Temp_NCat.DebitNote, Temp_NCat.DebitNote, "Debit Note.", Temp_NCat.DebitNote, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            AgL.CreateNCat(AgL.GCn, Temp_NCat.CreditNote, Temp_NCat.CreditNote, "Credit Note.", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.CreditNote, Temp_NCat.CreditNote, Temp_NCat.CreditNote, "Credit Note.", Temp_NCat.CreditNote, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================<Vehicle Gate In/Out Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.VehicleGate, Temp_NCat.VehicleGate, "Vehicle In/Out", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.VehicleGate, Temp_NCat.VehicleGate, Temp_NCat.VehicleGate, "Vehicle In/Out", Temp_NCat.VehicleGate, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)

            '===================================================<Visitor Gate In/Out Entry V_Type >===================================================
            AgL.CreateNCat(AgL.GCn, Temp_NCat.VisitorGate, Temp_NCat.VisitorGate, "Visitor In/Out", AgL.PubSiteCode)
            AgL.CreateVType(AgL.GCn, Temp_NCat.VisitorGate, Temp_NCat.VisitorGate, Temp_NCat.VisitorGate, "Visitor In/Out", Temp_NCat.VisitorGate, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)


            Try
                AgL.CreateNCat(AgL.GCn, ClsMain.Temp_NCat.ChequeCancel, ClsMain.Temp_NCat.ChequeCancel, "Cheque Cancel", AgL.PubSiteCode)
                AgL.CreateVType(AgL.GCn, ClsMain.Temp_NCat.ChequeCancel, ClsMain.Temp_NCat.ChequeCancel, ClsMain.Temp_NCat.ChequeCancel, "Cheque Cancel", ClsMain.Temp_NCat.ChequeCancel, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.ChequeCancel)
            End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub CreateDatabase(ByRef MdlTable() As AgLibrary.ClsMain.LITable)

    End Sub

#Region "Table Functions"

    Private Sub FItem_UID(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "GenDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "GenSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item_ManualUID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "RecDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "RecSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Subcode", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "IsInStock", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "RollNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Subcode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub

    Private Sub FEmployee(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Designation", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Department", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If
        AgL.FSetFKeyValue(MdlTable, "Department", "Code", "Department")
    End Sub

    Private Sub FDepartment(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FDesignation(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FAcGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 4, True)
        AgL.FSetColumnValue(MdlTable, "SNo", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "GroupName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ContraGroupName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "GroupUnder", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "GroupNature", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 15)
        AgL.FSetColumnValue(MdlTable, "SysGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TradingYn", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MainGrCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "BlOrd", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MainGrLen", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "ID", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "GroupNameBiLang", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "GroupLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CurrentCount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CurrentBalance", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "SubLedYn", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "AliasYn", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "GroupHelp", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LastYearBalance", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "GroupUnder", "GroupCode", "AcGroup")
    End Sub

    Private Sub FCompany(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Comp_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Comp_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CentralData_Path", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrevDBName", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DbPrefix", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Repo_Path", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Start_Dt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "End_Dt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "address1", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "address2", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "city", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "pin", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "phone", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "fax", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "lstno", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "lstdate", AgLibrary.ClsMain.SQLDataType.nVarChar, 12)
        AgL.FSetColumnValue(MdlTable, "cstno", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "cstdate", AgLibrary.ClsMain.SQLDataType.nVarChar, 12)
        AgL.FSetColumnValue(MdlTable, "cyear", AgLibrary.ClsMain.SQLDataType.nVarChar, 9)
        AgL.FSetColumnValue(MdlTable, "pyear", AgLibrary.ClsMain.SQLDataType.nVarChar, 9)
        AgL.FSetColumnValue(MdlTable, "SerialKeyNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "SName", AgLibrary.ClsMain.SQLDataType.nVarChar, 15)
        AgL.FSetColumnValue(MdlTable, "EMail", AgLibrary.ClsMain.SQLDataType.VarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Gram", AgLibrary.ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "Desc1", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Desc2", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Desc3", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ECCCode", AgLibrary.ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "ExDivision", AgLibrary.ClsMain.SQLDataType.VarChar, 30)
        AgL.FSetColumnValue(MdlTable, "ExRegNo", AgLibrary.ClsMain.SQLDataType.VarChar, 30)
        AgL.FSetColumnValue(MdlTable, "ExColl", AgLibrary.ClsMain.SQLDataType.VarChar, 30)
        AgL.FSetColumnValue(MdlTable, "ExRange", AgLibrary.ClsMain.SQLDataType.VarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Desc4", AgLibrary.ClsMain.SQLDataType.VarChar, 150)
        AgL.FSetColumnValue(MdlTable, "VatNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VatDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TinNo", AgLibrary.ClsMain.SQLDataType.VarChar, 12)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "LogSiteCode", AgLibrary.ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PANNo", AgLibrary.ClsMain.SQLDataType.VarChar, 25)
        AgL.FSetColumnValue(MdlTable, "State", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "DeletedYN", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Country", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "NotificationNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "WorkAddress1", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "WorkAddress2", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "WorkCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "WorkCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "WorkPin", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "WorkPhone", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "WorkFax", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "WebServer", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "WebUser", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "WebPassword", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Webdatabase", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "UseSiteNameAsCompanyName", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FCurrency(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FDivision(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "Div_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "DataPath", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "address1", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "address2", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "address3", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "city", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "pin", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SitewiseV_No", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
    End Sub

    Private Sub FEntryPointPermission(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "MnuModule", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100, True)
        AgL.FSetColumnValue(MdlTable, "IsOnLineEntry", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsOffLineEntry", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FMaster_Settings(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "MasterName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MnuAttachedInModule", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
    End Sub

    Private Sub FEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "ID", AgLibrary.ClsMain.SQLDataType.nVarChar, 4, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "CashAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BankAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TdsAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AdditionAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DeductionAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RoundOffAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ECessPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "HECessPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DefaultSalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DefaultSalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PurchOrderShowIndentInLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsNegetiveStockAllowed", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "IsLinkWithFA", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsNegativeStockAllowed", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "IsLotNoApplicable", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "DefaultDueDays", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SaleAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchaseAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsVisible_PurchOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_PurchChallan", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)

        AgL.FSetColumnValue(MdlTable, "DefaultCurrency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Caption_Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Caption_Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "UrgentList", AgLibrary.ClsMain.SQLDataType.nVarChar, 500)
        AgL.FSetColumnValue(MdlTable, "UrgentItemList", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetFKeyValue(MdlTable, "CashAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "BankAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "AdditionAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "DeductionAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "TdsAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ServiceTaxAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "HECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "RoundOffAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FLog_TablePermission(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "CreateLogYn", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FLog_TableRecords(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "SearchKey", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "AED", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "UpdateDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, , True)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Site", AgLibrary.ClsMain.SQLDataType.nVarChar, -1)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FLogin_Log(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "User_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MachineName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Comp_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FLogTable(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 36)
        AgL.FSetColumnValue(MdlTable, "EntryPoint", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MachineName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyDetail", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FSiteMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "HO_YN", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Add1", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Add2", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Add3", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "City_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 7)
        AgL.FSetColumnValue(MdlTable, "Phone", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Mobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PinNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 15)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "AcCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SqlServer", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DataPath", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DataPathMain", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SqlUser", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SqlPassword", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IEC", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "TIN", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Director", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ExciseDivision", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Photo", AgLibrary.ClsMain.SQLDataType.image)
        AgL.FSetColumnValue(MdlTable, "LastNarration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
    End Sub

    Private Sub FSubGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "SiteList", AgLibrary.ClsMain.SQLDataType.nVarChar, 500)
        AgL.FSetColumnValue(MdlTable, "DivisionList", AgLibrary.ClsMain.SQLDataType.nVarChar, 500)
        AgL.FSetColumnValue(MdlTable, "NamePrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 123)
        AgL.FSetColumnValue(MdlTable, "DispName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "GroupNature", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 11)
        AgL.FSetColumnValue(MdlTable, "Add1", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Add2", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Add3", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CityCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "CountryCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PIN", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Phone", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Mobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "FAX", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "EMail", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CSTNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)
        AgL.FSetColumnValue(MdlTable, "LSTNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)
        AgL.FSetColumnValue(MdlTable, "TINNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PAN", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "TDS_Catg", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "TDSCat_Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ActiveYN", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "DueDays", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "ContactPerson", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Party_Type", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "PAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PAdd3", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PCityCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PCountryCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 7)
        AgL.FSetColumnValue(MdlTable, "PPin", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PPhone", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "PMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "PFax", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Curr_Bal", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OpBal_DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "FatherName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "FatherNamePrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HusbandName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HusbandNamePrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DOB", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, -1)
        AgL.FSetColumnValue(MdlTable, "Location", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SiteStr", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "STRegNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "ECCNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "EXREGNO", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "CEXRANGE", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "CEXDIV", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "COMMRATE", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "VATNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "CommonAc", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ChequeReport", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SisterConcernYn", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetFKeyValue(MdlTable, "City", "CityCode", "City")
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "SalesTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ExcisePostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "EntryTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "MasterType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SisterConcernSite", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "Parent", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Upline", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "PartyRateGroup", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Guarantor", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "InsideOutside", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Department", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Designation", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "LedgerGroup", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Zone", AgLibrary.ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "DuplicateTIN", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Distributor", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "STNo", AgLibrary.ClsMain.SQLDataType.VarChar, 40)
        AgL.FSetColumnValue(MdlTable, "IECCode", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Range", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Division", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "PartyType", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PartyCat", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ECCCode", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Excise", AgLibrary.ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "FBTOnPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FBTPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PolicyNo", AgLibrary.ClsMain.SQLDataType.VarChar, 50)


        AgL.FSetFKeyValue(MdlTable, "CityCode", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "PCityCode", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "SisterConcernSite", "Code", "SiteMast")
    End Sub

    Private Sub FSubGroup_Image(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Photo", AgLibrary.ClsMain.SQLDataType.image)
        AgL.FSetColumnValue(MdlTable, "Signature", AgLibrary.ClsMain.SQLDataType.image)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

    End Sub

    Private Sub FItem_Image(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Photo", AgLibrary.ClsMain.SQLDataType.image)        
    End Sub


    Private Sub FSubGroupType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Party_Type", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FSynchronise_Error(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY, , True)
        AgL.FSetColumnValue(MdlTable, "Message", AgLibrary.ClsMain.SQLDataType.nVarChar, -1)
    End Sub

    Private Sub FTable_SearchField(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "TABLE_NAME", AgLibrary.ClsMain.SQLDataType.nVarChar, 128)
        AgL.FSetColumnValue(MdlTable, "SEARCH_FIELD", AgLibrary.ClsMain.SQLDataType.nVarChar, 128)
    End Sub

    Private Sub FUnit(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "IsActive", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "DecimalPlaces", AgLibrary.ClsMain.SQLDataType.Int)
    End Sub

    Private Sub FItemTags(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Tag", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Item")
    End Sub

    Private Sub FTag(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

    End Sub

    Private Sub FComputer(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Id", AgLibrary.ClsMain.SQLDataType.IDENTITY, , True)
        AgL.FSetColumnValue(MdlTable, "IP", AgLibrary.ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "ComputerName", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Default_Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Item")
    End Sub


    Private Sub FUnitConversion(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)        
        AgL.FSetColumnValue(MdlTable, "FromUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ToUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Multiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rounding", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)


        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "FromUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "ToUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Code", "Unit")
    End Sub

    Private Sub FVoucher_Exclude(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "Dr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Cr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FVoucher_Include(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "Dr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Cr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "SITE_CODE", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FVoucher_Prefix(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Date_From", AgLibrary.ClsMain.SQLDataType.SmallDateTime, )
        AgL.FSetColumnValue(MdlTable, "Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Start_Srl_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Date_To", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Comp_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status_Add", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Status_Edit", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Status_Delete", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Status_Print", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Ref_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Ref_PadLength", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FVoucher_Prefix_Type(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FVoucher_Type(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "NCat", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Category", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Short_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SystemDefine", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "DivisionWise", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "SiteWise", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IssRec", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "Description_Help", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Description_BiLang", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Short_Name_BiLang", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Report_Index", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "Number_Method", AgLibrary.ClsMain.SQLDataType.nVarChar, 9)
        AgL.FSetColumnValue(MdlTable, "Start_No", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Last_Ent_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Form_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, -1)
        AgL.FSetColumnValue(MdlTable, "Saperate_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Common_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Print_VNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Header_Desc", AgLibrary.ClsMain.SQLDataType.nVarChar, 80)
        AgL.FSetColumnValue(MdlTable, "Term_Desc", AgLibrary.ClsMain.SQLDataType.nVarChar, 150)
        AgL.FSetColumnValue(MdlTable, "Footer_Desc", AgLibrary.ClsMain.SQLDataType.nVarChar, 150)
        AgL.FSetColumnValue(MdlTable, "Exclude_Ac_Grp", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SerialNo_From_Table", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ChqNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ChqDt", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ClgDt", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "DefaultCrAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DefaultDrAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FirstDrCr", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TrnType", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "TdsDed", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ContraNarr", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TdsOnAmt", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Contra_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Separate_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MnuAttachedInModule", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "AuditAllowed", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Affect_FA", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "IsShowVoucherReference", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "HeaderTable", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "LogHeaderTable", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "SerialNo", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "DefaultAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
    End Sub

    Private Sub FUser_Exlude_VType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "UserName", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))

    End Sub

    Private Sub FUser_Exlude_VTypeDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "UserName", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub


    Private Sub FUser_Control_Permission(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UserName", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "MnuModule", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100, True)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "GroupText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100, True)
        AgL.FSetColumnValue(MdlTable, "GroupName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100, True)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FUser_Permission(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UserName", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "MnuModule", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100, True)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SNo", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "MnuLevel", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Parent", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Permission", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "ReportFor", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MainStreamCode", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "GroupLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ControlPermissionGroups", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "IsParent", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FUserMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "USER_NAME", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 15)
        AgL.FSetColumnValue(MdlTable, "PASSWD", AgLibrary.ClsMain.SQLDataType.nVarChar, 16)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Admin", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FUserSite(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "User_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "CompCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "Sitelist", AgLibrary.ClsMain.SQLDataType.nVarChar, 250)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "DivisionList", AgLibrary.ClsMain.SQLDataType.nVarChar, 250)

        AgL.FSetFKeyValue(MdlTable, "CompCode", "Comp_Code", "Company")
    End Sub

    Private Sub FVoucherCat(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "NCat", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Category", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "NCatDescription", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "SITE_CODE", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "UserTypeYN", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FCity(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "CityCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "CityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "State", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Country", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "STDCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 15)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
    End Sub

    Private Sub FSeaPort(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "City", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "City", "CityCode", "City")
    End Sub



    Private Sub FItemInvoiceGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRateType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRateList(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "RateInside", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RateOutside", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MasterType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRateListDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FItemProcessDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemRateGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FFollowUpType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "MailFormat", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FFollowUpTypeDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "Schedule", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "AfterDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FBank(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)


        AgL.FSetColumnValue(MdlTable, "Bank_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 8, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Bank_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)        
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ChequeReport", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

    End Sub


    Private Sub FBankBranch(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "BankBranch_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 8, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "BankBranch_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)        
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FNarrMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 8, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)        
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "LogSiteCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "LogCompCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SiteCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub


    Private Sub FCountry(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FNature(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Personal_Nature", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UploadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FPermitForm(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 8, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ActiveYn", AgLibrary.ClsMain.SQLDataType.Bit, 0)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt, 0)
    End Sub


    Private Sub FTDSCat_Description(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, True)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 25)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

    End Sub


    Private Sub FTDSCat(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
    End Sub

    Private Sub FFaEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Age1", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Age2", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Age3", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Age4", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Age5", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Age6", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Amt1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amt2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amt3", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amt4", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amt5", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amt6", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TagadaHeader1", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaHeader2", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaHeader3", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaHeader4", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaHeader5", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaFooter1", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaFooter2", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaFooter3", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaFooter4", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "TagadaFooter5", AgLibrary.ClsMain.SQLDataType.nVarChar, 75)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "DebitLimit", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "NegativeCashBalance", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ShowGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "DonotShowGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ShowCurrentBalance", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "VerticleBalanceSheet", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ShowQty", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ShowCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "LedDivCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "LedSiteCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "LedPrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "linefiller", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "daterfill", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "titlerfill", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "pagenofill", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RunPIF", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "FilterAC", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "DateLock", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "AddressHelp", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "CashBookBalance", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "MonthTotal", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "OnLineAdjustment", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "OpStockQTY", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OpStockValue", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ClStockQTY", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ClStockValue", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CashBookPage", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "RepToBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "PreBal", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "PDCDt", AgLibrary.ClsMain.SQLDataType.nVarChar, 3)
        AgL.FSetColumnValue(MdlTable, "ToBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CityNameDisp", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub


    Private Sub FTdsCat_Det(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 6, True)
        AgL.FSetColumnValue(MdlTable, "SrNo", AgLibrary.ClsMain.SQLDataType.Int, 0, True)
        AgL.FSetColumnValue(MdlTable, "AcCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Percentage", AgLibrary.ClsMain.SQLDataType.Float, 0)
        AgL.FSetColumnValue(MdlTable, "TdsDesc", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
    End Sub


    Private Sub FTDSLedger(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.BigInt, , True)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PaidAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TdsOnAmt", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TDSPer", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TDSAmt", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarCharMax)


        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "DocID", "DocId", "Ledger")
    End Sub



    Private Sub FChequePrint(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "AgainstAcSr", AgLibrary.ClsMain.SQLDataType.Int, 0)
        AgL.FSetColumnValue(MdlTable, "PrintDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "InFavourOf", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "AcPayee", AgLibrary.ClsMain.SQLDataType.Bit, 0)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt, 0)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime, 0)
    End Sub

    Private Sub FProcessGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FProcess(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "NCat", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ParentProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RateGroupTable", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MeasureFieldStr", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "StockHead", AgLibrary.ClsMain.SQLDataType.VarChar, 50)


        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
    End Sub

    Private Sub FProcess_VType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "NCat", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_TypeDescription", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "NCat", "NCat", "VoucherCat")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub


    Private Sub FProcessSequence(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub

    Private Sub FProcessSequenceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Sequence", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "SaleOrderUrgentList")
        Else
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleOrderUrgentList_Log")
        End If

        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub

    Private Sub FItemReportingGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ItemList", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Buyer", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "OrderBy", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")

    End Sub

    Private Sub FGodown(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "Address", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "City", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ContactPerson", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Mobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OwnerName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "RentedYN", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "RestrictNegetiveStock", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "AlertOnNegetiveStock", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FGodownSection(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Section", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Godown_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Godown")
        End If

    End Sub

    Private Sub FBom(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ForQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ForWeight", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ForUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FBomDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BaseItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BatchQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BatchUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "IsMarkedForMainItem", AgLibrary.ClsMain.SQLDataType.Bit) ' if value is 1 then new item is created from BomDetail.BaseItem + Item.MarkText.
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ConsumptionPer", AgLibrary.ClsMain.SQLDataType.Float)        
        AgL.FSetColumnValue(MdlTable, "WastagePer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        If EntryType = EntryPointType.Log Then
            'AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Bom_Log")
        Else
            'AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Bom")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "FromProcess", "NCat", "Process")
    End Sub

    Private Sub FItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "DisplayName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Measure", AgLibrary.ClsMain.SQLDataType.Float)
		AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Prod_Measure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemNature", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemInvoiceGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "GodownSection", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "QcGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CurrentStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CurrentIssued", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CurrentRequisition", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "UpcCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Bom", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProcessSequence", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ItemImportExportGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BillingOn", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "ProcessList", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "ProdBatchQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdBatchUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "Manufacturer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VatCommodityCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReorderLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "StockYN", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxYN", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "StockOn", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Operators_Required", AgLibrary.ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "UPCCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SalesTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ExcisePostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "EntryTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "TariffHead", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "LastPurchaseRate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LastPurchaseDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "LastPurchaseInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "GenTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "GenCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "ProcessSequence", "Code", "ProcessSequence")
        AgL.FSetFKeyValue(MdlTable, "Bom", "Code", "BOM")
        AgL.FSetFKeyValue(MdlTable, "SalesTaxPostingGroup", "Description", "PostingGroupSalesTaxItem")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "QcGroup", "Code", "QcGroup")
        AgL.FSetFKeyValue(MdlTable, "ItemGroup", "Code", "ItemGroup")
        AgL.FSetFKeyValue(MdlTable, "TariffHead", "Code", "TariffHead")
    End Sub

    Private Sub FItemNature(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))        
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub


    Private Sub FItemSiteDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)

        AgL.FSetColumnValue(MdlTable, "MinimumStockLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MaximumStockLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReOrderStockLevel", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IsRequired_LotNo", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsMandatory_UnitConvertion", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "PurchQtyAllowedWithoutPO", AgLibrary.ClsMain.SQLDataType.Float, , , , "-1")
        AgL.FSetColumnValue(MdlTable, "BinLocation", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub



    Private Sub FItemRate(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub


    Private Sub FManufacturer(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FVatCommodityCode(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SalesTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FTariffHead(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub


    Private Sub FItemBuyer(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Buyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BuyerSku", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BuyerSpecification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "BuyerUpcCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 12)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Item_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Item")
        End If
    End Sub

    Private Sub FStockHead(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "OrderBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromGodown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToGodown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Addition", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Deduction", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "ReferenceDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "FromProcess", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "ToProcess", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "FromGodown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "ToGodown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "OrderBy", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
    End Sub

    Private Sub FStock(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.VarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "RecId", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Manufacturer", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ProcessGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EType_IR", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Qty_Iss", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty_Rec", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Measure_Iss", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Measure_Rec", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Addition", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Deduction", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Sale_Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "CurrentStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FIFORate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FIFOAmt", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AVGRate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AVGAmt", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "FIFOValue", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Landed_Value", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OtherAdjustment", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Doc_Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocID", AgLibrary.ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocIDSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NDP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ExpiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "ProcessGroup", "Code", "ProcessGroup")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Manufacturer", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")

    End Sub

    Private Sub FStockAdj(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)


        AgL.FSetColumnValue(MdlTable, "StockInDocId", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "StockInSr", AgLibrary.ClsMain.SQLDataType.Int, , True, False)
        AgL.FSetColumnValue(MdlTable, "StockOutDocID", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "StockOutSr", AgLibrary.ClsMain.SQLDataType.Int, , True, False)
        AgL.FSetColumnValue(MdlTable, "AdjQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.VarChar, 2, , False)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.VarChar, 1, , False)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "StockInDocId,StockInSr", "DocId,Sr", "Stock")
        AgL.FSetFKeyValue(MdlTable, "StockOutDocId,StockOutSr", "DocId,Sr", "Stock")
    End Sub


    Private Sub FStockHeadDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Manufacturer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "CurrentStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CurrentStockMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "DifferenceQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DifferenceMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Requisition", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "RequisitionSr", AgLibrary.ClsMain.SQLDataType.Int)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "Requisition,RequisitionSr", "DocID,Sr", "RequisitionDetail")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Manufacturer", "Subcode", "Subgroup")
    End Sub
    Private Sub FEnviroDefaultGodown(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)

        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FGateInOut(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "InOut", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VehicleType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VehicleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PassNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeterReading", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Weight", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Transporter", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Driver", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "LrNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "LrDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)        
        AgL.FSetColumnValue(MdlTable, "Manual_RefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "Close_MeterReading", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Close_GatePassNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Close_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Close_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Close_Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Close_EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Transporter", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")

        AgL.DeleteField(StrTableName, "Out_Date", AgL.GCn)
        AgL.DeleteField(StrTableName, "Out_Time", AgL.GCn)
        AgL.DeleteField(StrTableName, "Out_Remarks", AgL.GCn)
        AgL.DeleteField(StrTableName, "Out_EntryBy", AgL.GCn)
    End Sub



    Private Sub FReminder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "ID", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Ref_ID", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "RemindTo", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Reminder_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Reminder_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetFKeyValue(MdlTable, "Ref_ID", "ID", "Reminder")
    End Sub

    Private Sub FReminderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "ID", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Reminder_To", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "ActReminder_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ActReminder_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetFKeyValue(MdlTable, "ID", "ID", "Reminder")
    End Sub


    Private Sub FDues(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        'AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "CashCredit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "RefV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "RefV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "RefManualNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "RefPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "RefPartyAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "RefPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PaybleAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReceivableAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AdjustedAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        'AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub


    Private Sub FDuesPaymentEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "DiscountAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CashAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BankAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DebitNoteAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CreditNoteAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PrintOnAddSave", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PrintOnEditSave", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarCharMax)


        AgL.FSetFKeyValue(MdlTable, "DiscountAc", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "CashAc", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "BankAc", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "DebitNoteAc", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "CreditNoteAc", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub


































    Private Sub FMailEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Sender", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Subject", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Message", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FMailEnviroDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

		AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "RecepientType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Recepient", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
                
    End Sub



    Private Sub FMailSender(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "DispName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "FromEmailAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "FromEmailPassword", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SMTPHost", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SMTPPort", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FMailOutBox(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)

        AgL.FSetColumnValue(MdlTable, "Sender", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Subject", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "IsSend", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FMailOutBoxAttachments(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "FileId", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "FileName", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Extension", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Content", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)
    End Sub

    Private Sub FMailOutBoxDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "RecepientType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Recepient", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "RecepientEMail", AgLibrary.ClsMain.SQLDataType.nVarChar, 300)
        AgL.FSetColumnValue(MdlTable, "WithAttachment", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)
    End Sub

    Private Sub FJobIssRecEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ExcessBomAllowedPer", AgLibrary.ClsMain.SQLDataType.Float)


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub

    Private Sub FPurchaseEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "GenerateItem_UidFromPO", AgLibrary.ClsMain.SQLDataType.Bit)


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub

    Private Sub FJobEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "IsVisible_LossPer", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Loss", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsAllowed_MaterialIssue", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "AllowExcessOrderQty", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsPostedInJobOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsMandatory_ItemBOM", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub


    Private Sub FDuesPayment(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)        
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "CurrBalance", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TDSPer", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TDSAmt", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PaidAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Discount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "CashBank", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CashBankAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ChqNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ChqDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "PaymentType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "CashBankAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "PartyCity", "CityCode", "City")
    End Sub

    Private Sub FDuesPaymentDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "CurrBalance", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PaidAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Discount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "CashBank", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CashBankAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ChqNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ChqDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "AmtPendingForTds", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TdsOnAmt", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TdsPer", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TdsAmt", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TdsAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Reference_Sr", AgLibrary.ClsMain.SQLDataType.Int, , False)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "DuesPayment_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "DuesPayment")
        End If

        AgL.FSetFKeyValue(MdlTable, "ReferenceDocId", "DocID", "Dues")
    End Sub




    Private Sub FProdOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocID", "SaleOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")        
    End Sub

    Private Sub FProdOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProdPlanQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "MaterialPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "MaterialPlanSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "ProdOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "ProdOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")

    End Sub

    Private Sub FMaterialPlan(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ProdPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalComputerConsumptionPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalComputerConsumptionPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalUserConsumptionPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalUserConsumptionPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalUserPurchPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalUserPurchPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalConsumptionQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocID", "SaleOrder")
        AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocID", "ProdOrder")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
    End Sub

    Private Sub FMaterialPlanForDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "MaterialPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "MaterialPlanSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "MaterialPlan_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "MaterialPlan")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocId", "ProdOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")

    End Sub

    Private Sub FMaterialPlanDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BomQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StockQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IssuedQty_ProdPlan", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ComputerMaterialPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ComputerMaterialPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UserMaterialPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UserMaterialPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UserMaterialPlanRemarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchOrdQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PurchOrdMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PurchQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PurchMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ProdIssQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ProdIssMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PendingPurchaseOrderQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "PendingJobOrderQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)


        AgL.FSetColumnValue(MdlTable, "JobRecQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "JobRecMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)


        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "StkQty_SemiFinished", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "StkQtyReq_OpenSaleOrder", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ExcessQty_Finished", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ExcessQty_SemiFinished", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UserPurchPlanQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UserPurchPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "OpeningQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "OpeningMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "GenDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "MaterialPlan_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "MaterialPlan")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocId", "ProdOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")

    End Sub

    Private Sub FMaterialPlanProcess(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Measure", AgLibrary.ClsMain.SQLDataType.Float)

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "MaterialPlan_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "MaterialPlan")
        End If
        AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocId", "ProdOrder")
        AgL.FSetFKeyValue(MdlTable, "MaterialPlan", "DocId", "MaterialPlan")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub

    Private Sub FJobWorker(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "Guarantor", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "JobWithMaterialYN", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "InsideOutside", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If
    End Sub

    Private Sub FJobWorkerProcess(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If
    End Sub

    Private Sub FWorkOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)        

        AgL.FSetColumnValue(MdlTable, "ReferenceParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReferencePartyDocumentNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ReferencePartyDocumentDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "PartyOrderNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyOrderDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyDeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyDeliveryTime", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyOrderCancelDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
            AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
            AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
            AgL.FSetFKeyValue(MdlTable, "Party", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "PartyCity", "CityCode", "City")
            AgL.FSetFKeyValue(MdlTable, "ShipToPartyCity", "CityCode", "City")
            AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
            AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
            AgL.FSetFKeyValue(MdlTable, "Agent", "SubCode", "SubGroup")
        End If
    End Sub

    Private Sub FWorkOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)        
        AgL.FSetColumnValue(MdlTable, "PartySKU", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PartyUPC", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartySpecification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate_Ord", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate_Amd", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AffectRate", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "WorkOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocId", "WorkOrder")
            AgL.FSetFKeyValue(MdlTable, "WorkOrder,WorkOrderSr", "DocID,Sr", "WorkOrder")
            AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "DeliveryMeasure", "Code", "Unit")
        End If
    End Sub

    Private Sub FWorkOrderDeliveryDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryInstructions", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "WorkOrderDelSchSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "WorkOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "WorkOrder")
            AgL.FSetFKeyValue(MdlTable, "WorkOrder", "DocId", "WorkOrder")
            AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        End If
    End Sub

    Private Sub FWorkOrderBom(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "WorkOrderBOMSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "WorkOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "WorkOrder")
            AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        End If
    End Sub

    Private Sub FWorkDispatch(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
            AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
            AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
            AgL.FSetFKeyValue(MdlTable, "Party", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "BillToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
            AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
            AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
            AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
        End If
    End Sub

    Private Sub FWorkDispatchDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "WorkDispatch", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkDispatchSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)


        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LossQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalLossDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.Int)


        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "WorkDispatch_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "WorkDispatch")
            AgL.FSetFKeyValue(MdlTable, "WorkDispatch,WorkDispatchSr", "DocID,Sr", "WorkDispatchDetail")
            AgL.FSetFKeyValue(MdlTable, "WorkOrder,WorkOrderSr", "DocID,Sr", "WorkOrderDetail")
            AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "SalesTaxGroupItem", "Description", "PostingGroupSalesTaxItem")
            AgL.FSetFKeyValue(MdlTable, "Item_UID", "Code", "Item_UID")
        End If
    End Sub


    Private Sub FWorkInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "PartyTinNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyCstNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)
        AgL.FSetColumnValue(MdlTable, "PartyLstNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
            AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
            AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
            AgL.FSetFKeyValue(MdlTable, "SaleToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "BillToParty", "SubCode", "SubGroup")
            AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
            AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
            AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
        End If
    End Sub

    Private Sub FWorkInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "WorkOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "WorkDispatch", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkDispatchSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemInvoiced", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LossQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "WorkInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "WorkInvoiceSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalLossDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "WorkInvoice_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "WorkInvoice")
            AgL.FSetFKeyValue(MdlTable, "WorkOrder,WorkOrderSr", "DocID,Sr", "WorkOrderDetail")
            AgL.FSetFKeyValue(MdlTable, "WorkDispatch,WorkDispatchSr", "DocID,Sr", "WorkDispatchDetail")
            AgL.FSetFKeyValue(MdlTable, "WorkInvoice,WorkInvoiceSr", "DocID,Sr", "WorkInvoiceDetail")
            AgL.FSetFKeyValue(MdlTable, "Item_UID", "Code", "Item_UID")
            AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "ItemInvoiced", "Code", "Item")
            AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
            AgL.FSetFKeyValue(MdlTable, "DeliveryMeasure", "Code", "Unit")
        End If
    End Sub

    Private Sub FJobOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobWorker", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OrderBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalBomQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalBomMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.VarChar, 21)

        AgL.FSetColumnValue(MdlTable, "IsAllowed_MaterialIssue", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "JobInstructions", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "InsideOutside", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWithMaterialYN", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.VarChar, 20)

        AgL.FSetColumnValue(MdlTable, "LastIssueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "LastReceiveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "JobOrderFor", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "StatusRemark", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "StatusChangeBy", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StatusChangeDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "JobWorker", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "OrderBy", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
    End Sub

    Private Sub FStatus_Update(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StatusRemark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


    End Sub

    Private Sub FJobOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item_Uid", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CurrStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "LossPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Loss", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "TransactionNature", AgLibrary.ClsMain.SQLDataType.VarChar, 100)

        'Field For Material which was applied by Job Worker In The Process.
        'It Will Increase the weight of Product Delivered.
        AgL.FSetColumnValue(MdlTable, "JobMaterialPer", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "JobMaterialQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Incentive", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BOM", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobOrder")            
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocID", "ProdOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "BOM", "Code", "BOM")
    End Sub

    Private Sub FJobOrderBom(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)        
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)        
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PrevProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "JobOrderBOMSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "ConsumptionPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "PrevProcess", "NCat", "Process")
    End Sub

    Private Sub FJobOrderQCInstruction(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Parameter", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "StdValue", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobOrder")
        End If
    End Sub

    Private Sub FJobOrderByProduct(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CancelQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CancelMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReceivedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReceivedMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub

    Private Sub FJobWorkerRateGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub



    Private Sub FJobWorkerRate(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWorkerRateGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "GeneralRate", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "GeneralRateOutSide", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "ItemCategory", "Code", "ItemCategory")
        AgL.FSetFKeyValue(MdlTable, "ItemGroup", "Code", "ItemGroup")
    End Sub

    Private Sub FJobWorkerRateDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "JobWorkerRateGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWorker", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RateOutSide", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "JobWorker", "Subcode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "ItemCategory", "Code", "ItemCategory")
        AgL.FSetFKeyValue(MdlTable, "ItemGroup", "Code", "ItemGroup")
        AgL.FSetFKeyValue(MdlTable, "JobWorkerRateGroup", "Code", "JobWorkerRateGroup")
    End Sub


    Private Sub FSalesOrderPriority(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Buyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OrderBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Buyer", "SubCode", "Subgroup")
    End Sub

    Private Sub FSalesOrderPriorityDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)        
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Priority", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocID", "SaleOrder")        
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")        
        AgL.FSetFKeyValue(MdlTable, "Priority", "Code", "Priority")
    End Sub


    Private Sub FJobIssRec(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobWorker", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWorkerDocNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IssQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IssMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RecQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RecMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "JobReceiveFor", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "OrderBy", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWithMaterialYn", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ToJobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobReceiveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsPostedInJobOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")

        AgL.FSetColumnValue(MdlTable, "TotalConsumptionQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalConsumptionMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "TotalByProductQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "TotalByProductMeasure", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "JobWorker", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "OrderBy", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
        AgL.FSetFKeyValue(MdlTable, "JobReceiveBy", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
    End Sub

    Private Sub FJobIssRecUID(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item_ManualUID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "RollNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobRecDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ISSREC", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.VarChar, 5)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RecId", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.DateTime)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub

    Private Sub FJobIssueDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "StockItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PrevProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReceiveQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReceiveMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReturnQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReturnMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReceiveLoss", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "CurrStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobIssue_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobIssue")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item_UID", "Code", "Item_UID")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
        AgL.FSetFKeyValue(MdlTable, "PrevProcess", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocID", "ProdOrder")
    End Sub

    Private Sub FJobReceiveDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "JobReceive", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobReceiveSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)

        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)

        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RetQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "LossPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LossQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "DocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RetMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Bom", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobIssRec_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobIssRec")
            AgL.FSetFKeyValue(MdlTable, "JobReceive,JobReceiveSr", "DocID,Sr", "JobReceiveDetail")
            AgL.FSetFKeyValue(MdlTable, "JobOrder,JobOrderSr", "DocID,Sr", "JobOrderDetail")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder,ProdOrderSr", "DocID,Sr", "ProdOrderDetail")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item_UID", "Code", "Item_UID")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "CostCenterMast", "Code", "CostCenterMast")
    End Sub


    Private Sub FJobReceiveBOM(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StockItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BOMItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PrevProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "JobOrderBomSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobReceive_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobReceive")
            AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocID", "ProdOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "BOMItem", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "PrevProcess", "NCat", "Process")
    End Sub


    Private Sub FJobReceiveByProduct(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "StockItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobReceive_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobReceive")
            AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocID", "ProdOrder")

        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub


    Private Sub FJobInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "FromDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ToDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


        AgL.FSetColumnValue(MdlTable, "JobWorker", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobWorkerDocNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)

        AgL.FSetColumnValue(MdlTable, "JobReceiveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsPostedInJobOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")


        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "JobWorker", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "CostCenterMast", "Code", "CostCenterMast")
    End Sub


    Private Sub FJobInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "JobInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobInvoiceSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Item_Uid", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "JobReceive", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobReceiveSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "JobOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.VarChar, 21)

        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)

        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RetQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "LossPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LossQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RetMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "LossMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Rate_Inv", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate_Amd", AgLibrary.ClsMain.SQLDataType.Float)


        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "AffectRate", AgLibrary.ClsMain.SQLDataType.Bit)

        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "JobWorker", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobInvoice_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobInvoice")
            AgL.FSetFKeyValue(MdlTable, "JobInvoice,JobInvoiceSr", "DocID,Sr", "JobInvoiceDetail")
            AgL.FSetFKeyValue(MdlTable, "JobReceive,JobReceiveSr", "DocID,Sr", "JobReceiveDetail")
            AgL.FSetFKeyValue(MdlTable, "JobOrder,JobOrderSr", "DocID,Sr", "JobOrderDetail")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder,ProdOrderSr", "DocID,Sr", "ProdOrderDetail")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
    End Sub


    Private Sub FJobInvoiceBOM(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobInvoice_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobInvoice")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub

    Private Sub FJobExchangeDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "ReceiveItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReceiveQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ReceiveItemUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReceiveItemMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReceiveItemTotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReceiveItemMeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IssueItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IssueQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IssueItemUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IssueItemMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IssueItemTotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IssueItemMeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "JobOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PrevProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "JobIssRec_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "JobIssRec")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "JobOrder", "DocID", "JobOrder")
        AgL.FSetFKeyValue(MdlTable, "PrevProcess", "NCat", "Process")
    End Sub

    Private Sub FRequisition(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Department", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RequisitionBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Department", "Code", "Department")
        AgL.FSetFKeyValue(MdlTable, "RequisitionBy", "SubCode", "Subgroup")
    End Sub

    Private Sub FRequisitionDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RequireDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ApproveQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)        
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Requisition_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Requisition")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "PurchaseIndex", "DocID", "PurchaseIndent")
    End Sub

    Private Sub FPurchaseIndent(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Department", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ProdPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ProdOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Indentor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)



        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "ProdPlan", "DocId", "MaterialPlan")
            AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocId", "ProdOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Department", "Code", "Department")
        AgL.FSetFKeyValue(MdlTable, "Indentor", "SubCode", "Subgroup")
    End Sub

    Private Sub FPurchaseIndentReq(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Requisition", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "RequisitionSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RequireDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchaseIndent_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "PurchaseIndent")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Requisition,RequisitionSr", "DocId,Sr", "RequisitionDetail")
    End Sub

    Private Sub FPurchaseIndentDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndentSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "CurrentStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReqQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IndentQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalReqMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalIndentMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RequireDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MaterialPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "MaterialPlanSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchaseIndent_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "PurchaseIndent")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "ProdOrder", "DocId", "ProdOrder")
    End Sub


    Private Sub FQCGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FQCGroupDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Parameter", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "StdValue", AgLibrary.ClsMain.SQLDataType.nVarChar, 50, True)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "QcGroup_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "QcGroup")
        End If
    End Sub


    Private Sub FQC(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Department", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "QcBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejectQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejectMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocId", "PurchChallan")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Department", "Code", "Department")
        AgL.FSetFKeyValue(MdlTable, "QcBy", "SubCode", "Subgroup")

    End Sub

    Private Sub FQcDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PassedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RejectQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejectMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "QC_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "QC")
            AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocId", "PurchChallan")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")

    End Sub




    Private Sub FQcParameterDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Parameter", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "StdValue", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ActValue", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PassedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RejectQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejectMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub

    Private Sub FSaleQCReq(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Buyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "QcDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalOrderQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalOrderMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FSaleQCReqDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OrderQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalOrderMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleQCReq_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "SaleQCReq")
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub

    Private Sub FSaleQC(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "QCBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Buyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Buyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalOrderQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalQCQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalOrderMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalQCMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub


    Private Sub FSaleQCDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleQCReq", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleQCReqSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OrderQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcReqQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CheckedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PassedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalOrderMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalQCReqMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalQCMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalCheckedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalPassedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleQC_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "SaleQC")
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub



    Private Sub FPurchaseQuotation(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorQuoteNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorQuoteDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)


        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocId", "PurchIndent")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "VendorCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")

    End Sub

    Private Sub FPurchaseQuotationDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndentSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "OrdQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OrdMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PurchQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PurchMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "PurchQuotation", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchQuotationSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "QuotSelection", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "QuotSelectionIndex", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "QuotSelectionV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "QuotSelectionV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "QuotSelectionV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchQuotation_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "PurchQuotation")
            AgL.FSetFKeyValue(MdlTable, "QuotSelection", "DocId", "PurchQuotSelection")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")

    End Sub


    Private Sub FPurchaseQuotSelection(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Employee", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))



        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Employee", "SubCode", "Subgroup")
    End Sub

    Private Sub FPurchaseQuotSelectionDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)


        AgL.FSetColumnValue(MdlTable, "SelectionIndex", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "PurchQuot", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchQuotV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "PurchQuotV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "PurchQuotV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorQuoteNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorQuoteDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchQuotSelection_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "PurchQuotSelection")
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocId", "PurchIndent")
            AgL.FSetFKeyValue(MdlTable, "PurchQuot", "DocId", "PurchQuotation")

        End If
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
    End Sub

    Private Sub FVoucherCatPurchase(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "PurchaseInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "PurchaseChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchaseOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchaseOrderCancel", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchaseIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchaseReturn", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "GoodsReturn", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetFKeyValue(MdlTable, "PurchaseInvoice", "NCat", "VoucherCat")
        AgL.FSetFKeyValue(MdlTable, "PurchaseChallan", "NCat", "VoucherCat")
        AgL.FSetFKeyValue(MdlTable, "PurchaseOrder", "NCat", "VoucherCat")
        AgL.FSetFKeyValue(MdlTable, "PurchaseIndent", "NCat", "VoucherCat")
    End Sub



    Private Sub FPurchaseOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VendorCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PurchQuotaion", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "VendorOrderNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorOrderDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "VendorDeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "VendorOrderCancelDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "DestinationPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FinalPlaceOfDelivery", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)





        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "PreCarriageBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PlaceOfReceipt", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipmentThrough", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAcNoVendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BankNameVendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAddressVendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PaymentTerms", AgLibrary.ClsMain.SQLDataType.VarCharMax)



        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "PurchQuotation", "DocID", "PurchQuotation")
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocID", "PurchIndent")
            AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "VendorCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "ShipToPartyCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "PortOfLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "DestinationLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Agent", "SubCode", "SubGroup")
    End Sub

    Private Sub FPurchaseOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "GenDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VendorSKU", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorUPC", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorSpecification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FreeQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PartySKU", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PurchQuotation", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchQuotationSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndentSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "MaterialPlan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "MaterialPlanSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "PurchOrder")
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocID", "PurchIndent")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FPurchaseOrderDeliveryDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryInstructions", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "PurchOrderDelSchSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "PurchOrder")
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocID", "PurchIndent")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub


    Private Sub FPurchChallan(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "VendorAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VendorCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "GateEntryNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "TruckNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "VendorDocNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorDocDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Transporter", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Transport", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "LrNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "LrDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
        End If

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
    End Sub

    Private Sub FPurchChallanDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FreeQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RejQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchChallanSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndentSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Sale_Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "NDP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ExpiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "InvoicedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "InvoicedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "QcMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleCount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchChallan_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "PurchChallan")
            AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
            AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocID", "PurchChallan")
        End If


        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Item_UID", "Code", "Item_UID")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "DeliveryMeasure", "Code", "Unit")
    End Sub

    Private Sub FPurchInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "VendorAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VendorCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "VendorCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "VendorDocNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VendorDocDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "TINNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "CSTNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)
        AgL.FSetColumnValue(MdlTable, "LSTNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)

        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
		AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
            AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocID", "PurchChallan")

        End If
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "BillToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
    End Sub

    Private Sub FPurchInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "PurchOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchIndentSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchChallanSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "PurchInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchInvoiceSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Item_Uid", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FreeQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RejQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalRejDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "AffectRate", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Sale_Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ExpiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Dimension1", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Dimension2", AgLibrary.ClsMain.SQLDataType.VarChar, 10)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "PurchInvoice_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "PurchInvoice")
            AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
            AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocID", "PurchChallan")
            AgL.FSetFKeyValue(MdlTable, "PurchIndent", "DocID", "PurchIndent")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub


    Private Sub FBuyer(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "SeaPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BankName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "BankAcNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContactPerson", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "MAddress1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MAddress2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "MContactNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "MFaxNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Consignee", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CAddress1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CAddress2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CPhoneNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "CMobileNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 12)
        AgL.FSetColumnValue(MdlTable, "CFaxNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "CEmail", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If

        AgL.FSetFKeyValue(MdlTable, "MCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "SeaPort", "Code", "SeaPort")
    End Sub

    Private Sub FVendor(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "SeaPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BankName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "BankAcNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContactPerson", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "MAddress1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MAddress2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "MContactNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "MFaxNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Consignee", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CAddress1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CAddress2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CPhoneNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "CMobileNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 12)
        AgL.FSetColumnValue(MdlTable, "CFaxNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "CEmail", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If

        AgL.FSetFKeyValue(MdlTable, "MCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "SeaPort", "Code", "SeaPort")
    End Sub

    Private Sub FAgent(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "Commission_Fix", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Commission_Per", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If


    End Sub

    Private Sub FAgentItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Subcode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Commission_Per", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "Subgroup")
        Else
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SubGroup_Log")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub


    Private Sub FSaleQuotation(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyDocNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyDocDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)




        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyEnquiryNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyEnquiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Party", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
    End Sub

    Private Sub FSaleQuotationDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleEnquiry", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "OrdQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OrdMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DispatchQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DispatchMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SaleQuotation", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleQuotationSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "GenDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleQuotation_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "SaleQuotation")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "MeasureUnit", "Code", "Unit")
    End Sub

    Private Sub FSaleOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
		AgL.FSetColumnValue(MdlTable, "SaleToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ReferenceParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ReferencePartyDocumentNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ReferencePartyDocumentDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "OrderType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "PartyOrderNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyOrderDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyDeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyDeliveryTime", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyOrderCancelDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetColumnValue(MdlTable, "DestinationPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FinalPlaceOfDelivery", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PreCarriageBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PlaceOfReceipt", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipmentThrough", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAcNoBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BankNameBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAddressBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "SaleToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SaleToPartyCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "ShipToPartyCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "PortOfLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "DestinationLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Agent", "SubCode", "SubGroup")
    End Sub

    Private Sub FSaleOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "GenDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "GenDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartySKU", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PartyUPC", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartySpecification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)


        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "SaleQuotation", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleQuotationSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CurrentQty", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "StockMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "StockTotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

		AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Priority", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "SaleOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FSaleOrderDeliveryDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryInstructions", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "SaleOrderDelSchSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "SaleOrder")
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
        End If


        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FSaleOrderDeliveryChange(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub

    Private Sub FSaleOrderDeliveryChangeNew(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleOrderDelSchSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FSaleOrderDeliveryChangeOld(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleOrderDelSchSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FDeliveryOrder(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyState", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCountry", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "PartyOrderNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PartyOrderDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyDeliveryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PartyOrderCancelDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "DestinationPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FinalPlaceOfDelivery", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "TermsAndConditions", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "StockTotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetColumnValue(MdlTable, "PreCarriageBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PlaceOfReceipt", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipmentThrough", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAcNoBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BankNameBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "BankAddressBuyer", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PriceMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "SaleToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SaleToPartyCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "ShipToPartyCity", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "PortOfLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "DestinationLoading", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Agent", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
    End Sub

    Private Sub FDeliveryOrderDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartySKU", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PartyUPC", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "StockMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "StockTotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ShippedQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ShippedMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdOrdQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdOrdMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdPlanQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdPlanMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PurchQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PurchMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdIssQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdIssMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdRecQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ProdRecMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "DeliveryOrder_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "DeliveryOrder")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
        AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocId", "SaleOrder")
    End Sub


    Private Sub FSaleChallan(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DeliveryOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)        
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "GateEntryNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "TruckNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UpLine", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)

        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)



        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Vendor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SaleToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "BillToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
    End Sub

    Private Sub FSaleChallanDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "SaleChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleChallanSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "DeliveryOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "DeliveryOrderSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ExpiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FreeQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)        
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ChargeType", AgLibrary.ClsMain.SQLDataType.VarChar, 20)

        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "JobReceiveDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)


        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleChallan_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "SaleChallan")
        End If

        AgL.FSetFKeyValue(MdlTable, "PurchOrder", "DocID", "PurchOrder")
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FSaleEnquiry(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "LastDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalAmount", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FSaleEnquiryDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleEnquiry_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "SaleEnquiry")
        End If

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub

    Private Sub FSaleEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)


        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SupplierVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "DeliveryDetailVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ItemCodeVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcsVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcsEditable", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MeasureUnitVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "MeasureUnitEditable", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TotalMeasureVisible", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TotalMeasureEditable", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "CurrencyDefault", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupPartyDefault", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub



    Private Sub FSaleInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetColumnValue(MdlTable, "Vendor", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyAdd2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCityName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyTinNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyCstNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)
        AgL.FSetColumnValue(MdlTable, "SaleToPartyLstNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 40)

        AgL.FSetColumnValue(MdlTable, "ShipToParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ShipToPartyMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)

        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Agent", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UpLine", AgLibrary.ClsMain.SQLDataType.VarCharMax)





        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)


        AgL.FSetColumnValue(MdlTable, "Transporter", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Vehicle", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VehicleDescription", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Driver", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DriverName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DriverContactNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "LrNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "LrDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PrivateMark", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)



        AgL.FSetColumnValue(MdlTable, "PortOfLoading", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DestinationPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FinalPlaceOfDelivery", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PreCarriageBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PlaceOfPreCarriage", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ShipmentThrough", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "GateInOut", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "BaleNoStr", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "GateEntryNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "InvoiceGenType", AgLibrary.ClsMain.SQLDataType.VarChar, 50)

        AgL.FSetColumnValue(MdlTable, "TotalBale", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PaidAmt", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "SaleToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ShipToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "BillToParty", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Form", "Code", "Form_Master")
    End Sub

    Private Sub FSaleInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "T_Nature", AgLibrary.ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SaleOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleOrderSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "SaleChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleChallanSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemInvoiced", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "SalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DocQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FreeQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeasureUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalDocMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MRP", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PcsPerMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "RatePerMeasure", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "LotNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ExpiryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "BaleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ItemInvoiceGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "Deal", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)

        AgL.FSetColumnValue(MdlTable, "Supplier", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "SaleInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleInvoiceSr", AgLibrary.ClsMain.SQLDataType.Int)


        AgL.FSetColumnValue(MdlTable, "V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)


        AgL.FSetColumnValue(MdlTable, "DeliveryMeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasureMultiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalDocDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TotalFreeDeliveryMeasure", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DeliveryMeasure", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "RateType", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "BillingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "SaleInvoice_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "SaleInvoice")
            AgL.FSetFKeyValue(MdlTable, "SaleOrder", "DocID", "SaleOrder")
        End If


        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub


    Private Sub FIE_Doc(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FIE_DocEntry(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "Document", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DocumentNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "OpenedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FileNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FileDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Period", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Tollerance", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "ImportPayMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ExportPayMode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Bank", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "ExpiryDateImport", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ExpiryDateExport", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Document", "Code", "IE_Doc")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Currency", "Code", "Currency")
        AgL.FSetFKeyValue(MdlTable, "Party", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Bank", "SubCode", "SubGroup")
    End Sub

    Private Sub FIE_DocAmend(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "AmendDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ExpiryDateImport", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ExpiryDateExport", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "IE_DocEntry_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "IE_DocEntry")
        End If
    End Sub

    Private Sub FIE_DocItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemImportExportGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AmountFC", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmountINR", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "IE_DocEntry_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "IE_DocEntry")
        End If
    End Sub

    Private Sub FIE_Shipment(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "PurchaseOrder", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchaseOrderReferenceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "InvoiceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "InvoiceDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "BillOfEntryNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "BillOfEntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ShippingLine", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CountryOfOrigin", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "InsuranceDetail", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "PortOfDispatch", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PortOfDicharge", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "FinalPlaceOfDelivery", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ETAAtIndianSeaPort", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ETAAtICD", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CHA", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ShipperInformationDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ShipperInformationRemark", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "DocSubmissionDateToCHA", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "DocRealisationDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "DutyAmountPaidDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ShipmentReleaseDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Transporter", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VehicleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DriverName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ArrivalDateAtFactory", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "VehicleReturnDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ProofSubmissionDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "BankAdviceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BankAdviceDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ExchangeRate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "BillOfLadingNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "BillOfLadingDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ShipmentOnBoardDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "VesselName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "CHA", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Transporter", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "PortOfDispatch", "Code", "SeaPort")
        AgL.FSetFKeyValue(MdlTable, "PortOfDicharge", "Code", "SeaPort")
    End Sub

    Private Sub FIE_ShipmentDoc(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "Document", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DocumentNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "IE_Shipment_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "IE_Shipment")
        End If
        AgL.FSetFKeyValue(MdlTable, "Document", "Code", "IE_Doc")
    End Sub

    Private Sub FIE_ShipmentBOE(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "BOE", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FCValue", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "INRValue", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Term", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "DueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "IE_Shipment_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "IE_Shipment")
        End If
    End Sub

    Private Sub FIE_ShipmentItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)

        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemDescription", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ContainerNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "KindsOfPackages", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "IE_Shipment_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "IE_Shipment")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FSaleInvoiceOtherDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "InvoiceNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FTransporter(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        End If
    End Sub

    Private Sub FForm_Master(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Category", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FMachine_Breakdown_Reason(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FMachine_Breakdown(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "Machine", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Operator", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromDateTime", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ToDateTime", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Shift", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Reason", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Remarks", AgLibrary.ClsMain.SQLDataType.VarChar, 255)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Machine", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Operator", "SubCode", "Subgroup")
        AgL.FSetFKeyValue(MdlTable, "Shift", "Code", "Shift")
        AgL.FSetFKeyValue(MdlTable, "Reason", "Code", "Machine_Breakdown_Reason")
    End Sub

    Private Sub FForm_Receive(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)

        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormPrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FormNoFrom", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "FormNoUpTo", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Party", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsFromDepartment", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))


        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Party", "SubCode", "SubGroup")
    End Sub

    Private Sub FForm_ReceiveDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Form", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FormNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "ReceiveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ReceiveFrom", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "IssueDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IssueTo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsInStock", AgLibrary.ClsMain.SQLDataType.Bit, , , False, 0)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IssueDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "FormPrefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FormNoSerial", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "IsUtilised", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)

        AgL.FSetColumnValue(MdlTable, "PurchChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchChallanV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "PurchChallanV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "PurchChallanV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "PurchInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "PurchInvoiceV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "PurchInvoiceV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "PurchInvoiceV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "SaleChallan", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleChallanV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "SaleChallanV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "SaleChallanV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "SaleInvoice", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SaleInvoiceV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "SaleInvoiceV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "SaleInvoiceV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Form_Receive_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocID", "DocID", "Form_Receive")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "PurchChallan", "DocID", "PurchChallan")
        AgL.FSetFKeyValue(MdlTable, "PurchInvoice", "DocID", "PurchInvoice")
    End Sub

    Private Sub FPhysicalStockDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "CurrentStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PhysicalStock", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Unit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Differnce", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "StockHead_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "DocId", "DocID", "StockHead")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Unit", "Code", "Unit")
    End Sub

    Private Sub FLedgerM(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PreparedBY", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostedBY", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OldDocid", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "RecId", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
    End Sub

    Private Sub FLedger(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_SNo", AgLibrary.ClsMain.SQLDataType.Int, 0, True)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContraSub", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AmtDr", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmtCr", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Chq_No", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Chq_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Clg_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TDSCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "TdsDesc", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "TdsOnAmt", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TdsPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Tds_Of_V_Sno", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "DivCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "SQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AgRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 4)
        AgL.FSetColumnValue(MdlTable, "GroupNature", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "AddBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AddDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifyBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ModifyDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OldDocid", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "System_Generated", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ContraText", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "RecId", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FormulaString", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)

        AgL.FSetColumnValue(MdlTable, "CreditDays", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OrignalAmt", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TDSDeductFrom", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocIdSr", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ContraSub", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")        
    End Sub

    Private Sub FVoucher(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "CommonBankDetail", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OldDocid", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Subcode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsAutoApproved", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "AuditedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AuditedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FVoucherDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "ReferenceTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ReferenceDocId", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContraSub", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AmtDr", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmtCr", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Narration", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Bank_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Chq_No", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Chq_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Clg_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowId", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ApprovedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApprovedDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "GPX1", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPX2", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "GPN1", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "GPN2", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Bank_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Favouring_Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CrossText", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "IsCrossCheque", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "OldDocid", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "CostCenter", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "SubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ContraSub", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "DocId", "DocId", "Voucher")

    End Sub

    Private Sub FTermsCondition(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        AgL.FSetFKeyValue(MdlTable, "Code", "NCat", "Voucher_Type")
    End Sub

    

    Private Sub FVoucher_Type_Settings(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Query", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Report_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Report_Heading", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Report_HeadingUnapproved", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Report_Format", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "SubReport_QueryList", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "SubReport_NameList", AgLibrary.ClsMain.SQLDataType.VarCharMax)

        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IndustryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TermsCondition", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "IsPostInSaleInvoice", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")

        AgL.FSetColumnValue(MdlTable, "IsMandatory_Approval", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_SubCode", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")

        AgL.FSetColumnValue(MdlTable, "IsMandatory_SubCode", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Unit", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsVisible_MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_MeasurePerPcs", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Measure", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_Measure", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_MeasureUnit", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_MeasureUnit", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_ProdOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Process", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_ProcessLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_ProcessLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Qty", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsEditable_Qty", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsMandatory_Rate", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Rate", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsEditable_Rate", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Amount", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsEditable_Amount", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_LotNo", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_BaleNo", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsPostedInStock", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsPostedInStockProcess", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")


        AgL.FSetColumnValue(MdlTable, "IsVisible_TransactionType", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsPostConsumption", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")


        AgL.FSetColumnValue(MdlTable, "IsPostedInStockVirtual", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_ItemUID", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_ItemCode", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_ItemCode", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsEditable_ItemName", AgLibrary.ClsMain.SQLDataType.Bit, , , , "1")
        AgL.FSetColumnValue(MdlTable, "IsVisible_Specification", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_BillingType", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_Process", AgLibrary.ClsMain.SQLDataType.VarCharMax)        
        AgL.FSetColumnValue(MdlTable, "IsVisible_CostCenter", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")


        AgL.FSetColumnValue(MdlTable, "IsVisible_Specification", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_BillingType", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "IsVisible_RateType", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")

        AgL.FSetColumnValue(MdlTable, "FilterInclude_ContraV_Type", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_AcGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)        
        AgL.FSetColumnValue(MdlTable, "FilterExclude_AcGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_SubGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterExclude_SubGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_SubGroupDivision", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterExclude_SubGroupDivision", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_SubGroupSite", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterExclude_SubGroupSite", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_ItemType", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_ItemGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterExclude_ItemGroup", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_Item", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterExclude_Item", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_ItemDivision", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_ItemSite", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "FilterInclude_Process", AgLibrary.ClsMain.SQLDataType.VarCharMax)        
        AgL.FSetColumnValue(MdlTable, "Default_Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Default_SubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Default_V_Nature", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ShowLastPoRates", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "ShowRecordCount", AgLibrary.ClsMain.SQLDataType.BigInt)

        AgL.FSetColumnValue(MdlTable, "IsVisible_TransactionHistory", AgLibrary.ClsMain.SQLDataType.Bit, , , , "0")
        AgL.FSetColumnValue(MdlTable, "TransactionHistory_SqlQuery", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "TransactionHistory_ColumnWidthCsv", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "TransactionEdit_AllowedDays", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "TransactionEdit_AllowedDaysAdmin", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "TransactionDelete_AllowedDays", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "TransactionDelete_AllowedDaysAdmin", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "IsVisible_ProdOrder", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_RateType", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_RejQty", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_RejMeasure", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_FreeQty", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_FreeMeasure", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_MRP", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Deal", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_SaleRate", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_ExpiryDate", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsEditable_ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_DeliveryDetail", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "IsVisible_ShippingDetail", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Supplier", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Manufacturer", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_PartySKU", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_PartyUPC", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_PartySpecification", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Dimension1", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsVisible_Dimension2", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)


        AgL.FSetColumnValue(MdlTable, "ImportMnuName", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ImportMnuText", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "ImportMnuAttachedInModule", AgLibrary.ClsMain.SQLDataType.VarChar, 100)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FStockUID(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "ManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FromSubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item_UID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Godown", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "iDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
        AgL.FSetColumnValue(MdlTable, "iTSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "iSr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "iV_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "iV_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "iV_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "iV_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "iManualRefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ToSubCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToProcess", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "RDocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
        AgL.FSetFKeyValue(MdlTable, "FromSubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "FromProcess", "NCat", "Process")
        AgL.FSetFKeyValue(MdlTable, "iV_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "ToSubCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ToProcess", "NCat", "Process")

    End Sub

    Private Sub FItemType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "MnuName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
    End Sub

    Private Sub FItemCategory(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "ItemType", "Code", "ItemType")
    End Sub

    Private Sub FDimension1(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub


    Private Sub FDimension2(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)


        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FShift(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "FromTime", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ToTime", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LunchFromTime", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LunchToTime", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub


    Private Sub FItemGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ItemCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "ItemCategory", "Code", "ItemCategory")
        AgL.FSetFKeyValue(MdlTable, "ItemType", "Code", "ItemType")
    End Sub
#End Region
#End Region


#Region "Module Functions"
    Public Shared Function RetDivFilterStr() As String
        Try
            RetDivFilterStr = "IfNull(Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "'"
        Catch ex As Exception
            RetDivFilterStr = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FunRetStock(ByVal bItem As String, ByVal bInternalCode As String, Optional ByVal bCondStr As String = "", _
                                       Optional ByVal bGodown As String = "", _
                                       Optional ByVal bProcess As String = "", _
                                       Optional ByVal bStatus As String = "", _
                                       Optional ByVal bV_Date As String = "", _
                                       Optional ByVal bLotNo As String = "") As Double
        Dim bConStr$ = ""
        Dim bStockQty As Double = 0
        Dim mQry$

        Try
            bConStr = "WHERE 1=1 And S.Item = '" & bItem & "' "
            If bCondStr <> "" Then bConStr += bCondStr
            If bGodown <> "" Then bConStr += " And IfNull(S.Godown,'') = '" & bGodown & "'"
            If bProcess <> "" Then bConStr += " And IfNull(S.Process,'') = '" & bProcess & "'"
            If bStatus <> "" Then bConStr += " And IfNull(S.Status,'" & ClsMain.StockStatus.Standard & "') = '" & bStatus & "'"
            If bV_Date <> "" Then bConStr += " And S.V_Date <= '" & bV_Date & "'"
            If bLotNo <> "" Then bConStr += " And S.LotNo = '" & bLotNo & "'"
            bConStr += " And S.DocId <> '" & bInternalCode & "' "
            'bConStr += " And S.Div_Code = '" & AgL.PubDivCode & "' "
            'bConStr += " And S.Site_Code = '" & AgL.PubSiteCode & "' "

            mQry = "SELECT IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) " & _
                    " FROM Stock S  " & bConStr
            bStockQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
            FunRetStock = bStockQty
        Catch ex As Exception
            FunRetStock = 0
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

    Public Shared Function FunCheckStock(ByVal bItem As String, ByVal bInternalCode As String, _
                               Optional ByVal bGodown As String = "", _
                               Optional ByVal bProcess As String = "", _
                               Optional ByVal bStatus As String = "") As Double
        Dim bConStr$ = ""
        Dim bStockQty As Double = 0
        Dim mQry$

        Try
            bConStr = "WHERE S.Item = '" & bItem & "' "
            bConStr += " And " & IIf(bGodown <> "", "S.Godown = '" & bGodown & "'", "1=1") & ""
            bConStr += " And " & IIf(bProcess <> "", "IfNull(S.Process,'') = '" & bProcess & "'", "1=1") & ""
            bConStr += " And " & IIf(bStatus <> "", "IfNull(S.Status,'" & ClsMain.StockStatus.Standard & "') = '" & bStatus & "'", "1=1") & ""
            bConStr += " And S.DocId <> '" & bInternalCode & "' "

            mQry = "SELECT IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) " & _
                    " FROM Stock S " & bConStr
            bStockQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
            FunCheckStock = bStockQty
        Catch ex As Exception
            FunCheckStock = 0
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub FVisitorsGateInOut(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nVarChar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "V_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.BigInt)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Manual_RefNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "VisitorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VisitorID", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PassNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Address", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "Phone", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "VehicleNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Employee", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Person_To_Meet", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Perpose", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

        AgL.FSetColumnValue(MdlTable, "Out_EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Out_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Out_Time", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub


    Private Sub FFaChequeDatail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "GUID", AgLibrary.ClsMain.SQLDataType.nVarChar, 36, True)
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nvarchar, 21)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.int)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nvarchar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nvarchar, 2)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nvarchar, 5)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", AgLibrary.ClsMain.SQLDataType.nvarchar, 5)
        AgL.FSetColumnValue(MdlTable, "V_No", AgLibrary.ClsMain.SQLDataType.bigint)
        AgL.FSetColumnValue(MdlTable, "V_Date", AgLibrary.ClsMain.SQLDataType.smalldatetime)
        AgL.FSetColumnValue(MdlTable, "ReferenceNo", AgLibrary.ClsMain.SQLDataType.nvarchar, 50)
        AgL.FSetColumnValue(MdlTable, "BankAc", AgLibrary.ClsMain.SQLDataType.nvarchar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyAc", AgLibrary.ClsMain.SQLDataType.nvarchar, 10)
        AgL.FSetColumnValue(MdlTable, "BankName", AgLibrary.ClsMain.SQLDataType.nvarchar, 100)
        AgL.FSetColumnValue(MdlTable, "ChequeNo", AgLibrary.ClsMain.SQLDataType.nvarchar, 20)
        AgL.FSetColumnValue(MdlTable, "ChequeDate", AgLibrary.ClsMain.SQLDataType.smalldatetime)
        AgL.FSetColumnValue(MdlTable, "ClearingDate", AgLibrary.ClsMain.SQLDataType.smalldatetime)
        AgL.FSetColumnValue(MdlTable, "DishonourDate", AgLibrary.ClsMain.SQLDataType.smalldatetime)
        AgL.FSetColumnValue(MdlTable, "AmtDr", AgLibrary.ClsMain.SQLDataType.float)
        AgL.FSetColumnValue(MdlTable, "AmtCr", AgLibrary.ClsMain.SQLDataType.float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.nvarchar, 255)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "PartyAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "BankAc", "SubCode", "SubGroup")
    End Sub



    Public Shared Sub ProcOpenLinkForm(ByVal Mnu As System.Windows.Forms.ToolStripItem, ByVal SearchCode As String, ByVal Parent As Form)
        Dim FrmObj As AgTemplate.TempTransaction
        Dim CFOpen As New ClsFunction
        Try
            FrmObj = CFOpen.FOpen(Mnu.Name, Mnu.Text, True)
            If FrmObj IsNot Nothing Then
                FrmObj.MdiParent = Parent
                FrmObj.Show()
                FrmObj.FindMove(SearchCode)
                FrmObj = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Public Shared Sub ProcStockPost(ByVal bTableName As String, ByVal bStock As ClsMain.StructStock,
                              ByVal bConn As SQLiteConnection,
                              ByVal bCmd As SQLiteCommand)
        Dim mQry$

        With bStock
            mQry = "INSERT INTO " & bTableName & "(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " CostCenter, SubCode, Currency, SalesTaxGroupParty, Structure, BillingType, Item_Uid, Item, ProcessGroup, " &
                    " Godown, Qty_Iss, Qty_Rec, Unit, LotNo, MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " &
                    " Rate, Amount, Addition, Deduction, NetAmount, Remarks, Status, UID, Process, " &
                    " FIFORate, FIFOAmt, AVGRate, AVGAmt, Doc_Qty, ReferenceDocID) " &
                    " VALUES (" & AgL.Chk_Text(.DocID) & ", " & Val(.Sr) & ", " &
                    " " & AgL.Chk_Text(.V_Type) & ", " & AgL.Chk_Text(.V_Prefix) & ", " &
                    " " & AgL.ConvertDate(.V_Date) & ",	" & Val(.V_No) & ",	" & AgL.Chk_Text(.RecID) & ", " & AgL.Chk_Text(.Div_Code) & ", " &
                    " " & AgL.Chk_Text(.Site_Code) & ",	" & AgL.Chk_Text(.CostCenter) & ",	" & AgL.Chk_Text(.SubCode) & ",	" &
                    " " & AgL.Chk_Text(.Currency) & ", " & AgL.Chk_Text(.SalesTaxGroupParty) & ", " &
                    " " & AgL.Chk_Text(.Structure1) & ", " & AgL.Chk_Text(.BillingType) & ", " &
                    " " & AgL.Chk_Text(.Item_Uid) & ",	" & AgL.Chk_Text(.Item) & ",	" & AgL.Chk_Text(.ProcessGroup) & ", " &
                    " " & AgL.Chk_Text(.Godown) & ", " & Val(.Qty_Iss) & ",	" & Val(.Qty_Rec) & ",  " &
                    " " & AgL.Chk_Text(.Unit) & ", " & AgL.Chk_Text(.LotNo) & ", " &
                    " " & Val(.MeasurePerPcs) & ",	" & Val(.Measure_Iss) & ",	" & Val(.Measure_Rec) & ",	" &
                    " " & AgL.Chk_Text(.MeasureUnit) & ", " & Val(.Rate) & ", " &
                    " " & Val(.Amount) & ",	" & Val(.Addition) & ",	" & Val(.Deduction) & ", " &
                    " " & Val(.NetAmount) & ",	" & AgL.Chk_Text(.Remarks) & ",	" & AgL.Chk_Text(.Status) & ",	" &
                    " " & AgL.Chk_Text(.UID) & ", " & AgL.Chk_Text(.Process) & ", " & Val(.FIFORate) & ", " &
                    " " & Val(.FIFOAmt) & ", " & Val(.AVGRate) & ",	" & Val(.AVGAmt) & ", " &
                    " " & Val(.Doc_Qty) & ", " & AgL.Chk_Text(.ReferenceDocID) & ") "
            AgL.Dman_ExecuteNonQry(mQry, bConn, bCmd)
        End With
    End Sub


    Public Shared Sub PostStructureLineToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                               ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                               ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                               ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand, Optional ByVal mCostCenter As String = "")
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing

        bSelectionQry = ""
        For I = 0 To FGMain.Rows.Count - 1
            For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
                If FGMain.AgLineGrid.Rows(J).Visible Then
                    If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc)) <> "" Then
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                        bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "
                    ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                        bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "

                    End If
                End If
            Next
        Next

        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                " From (" & bSelectionQry & ") As V1 Group by tmpCol " &
                " Having Sum(Case When IfNull(V1.Amount,0) > 0 Then IfNull(V1.Amount,0) Else 0 End) <> abs(Sum(Case When IfNull(V1.Amount,0) < 0 Then IfNull(V1.Amount,0) Else 0 End))  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Console.Write(mQry)
                Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
            End If
        End If



        mQry = " Select V1.PostAc, IfNull(Sum(V1.Amount),0) As Amount, " &
                " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
                "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        mQry = "Delete from Ledger where docId='" & mDocID & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText, CostCenter) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," &
                         " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "', " & AgL.Chk_Text(mCostCenter) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                       ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub

    Public Structure Dues
        Dim DocID As String
        Dim Sr As Integer
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As Long
        Dim Div_Code As String
        Dim Site_Code As String
        Dim CashCredit As String
        Dim SubCode As String
        Dim Narration As String
        Dim ReferenceDocID As String
        Dim RefV_Type As String
        Dim RefV_No As String
        Dim RefPartyName As String
        Dim RefPartyAddress As String
        Dim RefPartyCity As String
        Dim PaybleAmount As Double
        Dim ReceivableAmount As Double
        Dim AdjustedAmount As Double
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim IsDeleted As String
        Dim Status As String
        Dim UID As String
    End Structure

    Public Shared Sub ProcPostInDues(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand,
                              ByVal StructDue As ClsMain.Dues)
        Dim mQry$ = ""

        mQry = " Delete From Dues Where DocId = '" & StructDue.DocID & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO Dues(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, CashCredit, " &
                    " Site_Code, SubCode, Narration, ReferenceDocID, RefV_Type, RefV_No, RefPartyName, RefPartyAddress, RefPartyCity, PaybleAmount, ReceivableAmount, AdjustedAmount, " &
                    "  EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, " &
                    " ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status,	UID	) " &
                    " VALUES(" & AgL.Chk_Text(StructDue.DocID) & ", " & Val(StructDue.Sr) & ", " &
                    " " & AgL.Chk_Text(StructDue.V_Type) & " , " &
                    " " & AgL.Chk_Text(StructDue.V_Prefix) & ", " & AgL.Chk_Text(StructDue.V_Date) & ", " &
                    " " & Val(StructDue.V_No) & ", " &
                    " " & AgL.Chk_Text(StructDue.Div_Code) & ", " & AgL.Chk_Text(StructDue.CashCredit) & ", " & AgL.Chk_Text(StructDue.Site_Code) & ",  " &
                    " " & AgL.Chk_Text(StructDue.SubCode) & ", " & AgL.Chk_Text(StructDue.Narration) & ", " &
                    " " & AgL.Chk_Text(StructDue.ReferenceDocID) & ",  " &
                    " " & AgL.Chk_Text(StructDue.RefV_Type) & ",  " &
                    " " & AgL.Chk_Text(StructDue.RefV_No) & ",  " &
                    " " & AgL.Chk_Text(StructDue.RefPartyName) & ",  " &
                    " " & AgL.Chk_Text(StructDue.RefPartyAddress) & ",  " &
                    " " & AgL.Chk_Text(StructDue.RefPartyCity) & ",  " &
                    " " & Val(StructDue.PaybleAmount) & ", " &
                    " " & Val(StructDue.ReceivableAmount) & ", " & Val(StructDue.AdjustedAmount) & ", " &
                    " " & AgL.Chk_Text(StructDue.EntryBy) & ", " & AgL.Chk_Text(StructDue.EntryDate) & ", " &
                    " " & AgL.Chk_Text(StructDue.EntryType) & ",  " &
                    " " & AgL.Chk_Text(StructDue.EntryStatus) & ", " & AgL.Chk_Text(StructDue.ApproveBy) & ", " &
                    " " & AgL.Chk_Text(StructDue.ApproveDate) & ", " & AgL.Chk_Text(StructDue.MoveToLog) & ", " &
                    " " & AgL.Chk_Text(StructDue.MoveToLogDate) & ",  " &
                    " " & AgL.Chk_Text(StructDue.IsDeleted) & ", " & AgL.Chk_Text(StructDue.Status) & ", " &
                    " " & AgL.Chk_Text(StructDue.UID) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Public Structure ToBeBilled
        Dim DocID As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim V_Date As String
        Dim V_No As Long
        Dim Div_Code As String
        Dim Site_Code As String
        Dim ReferenceNo As String
        Dim SubCode As String
        Dim PartyName As String
        Dim PartyAddress As String
        Dim PartyCity As String
        Dim TotalQty As Double
        Dim ReceivableAmount As Double
        Dim PaybleAmount As Double
        Dim AdjustedAmount As Double
        Dim BilledAmount As Double
        Dim PaidAmount As Double
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim ApproveBy As String
        Dim ApproveDate As String
        Dim MoveToLog As String
        Dim MoveToLogDate As String
        Dim IsDeleted As Byte
        Dim Status As String
        Dim UID
    End Structure

    Public Shared Sub ProcPostInToBeBilled(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand, ByVal StructToBeBilled As ToBeBilled)
        Dim mQry$ = ""
        With StructToBeBilled
            mQry = " DELETE FROM ToBeBilled Where DocId = '" & .DocID & "'   "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " INSERT INTO ToBeBilled(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, " &
                    " ReferenceNo, SubCode, PartyName, PartyAddress, PartyCity, TotalQty, ReceivableAmount, " &
                    " PaybleAmount, AdjustedAmount, BilledAmount, PaidAmount, EntryBy, EntryDate, " &
                    " EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, " &
                    " IsDeleted, Status, UID) " &
                    " VALUES (" & AgL.Chk_Text(.DocID) & ", " & AgL.Chk_Text(.V_Type) & ", " &
                    " " & AgL.Chk_Text(.V_Prefix) & ", " & AgL.Chk_Text(.V_Date) & ", " &
                    " " & Val(.V_No) & ", " & AgL.Chk_Text(.Div_Code) & ", " &
                    " " & AgL.Chk_Text(.Site_Code) & ", " &
                    " " & AgL.Chk_Text(.ReferenceNo) & ", " & AgL.Chk_Text(.SubCode) & ", " &
                    " " & AgL.Chk_Text(.PartyName) & ", " & AgL.Chk_Text(.PartyAddress) & ", " &
                    " " & AgL.Chk_Text(.PartyCity) & ", " & Val(.TotalQty) & ", " &
                    " " & Val(.ReceivableAmount) & ", " & Val(.PaybleAmount) & ", " &
                    " " & Val(.AdjustedAmount) & ", " & Val(.BilledAmount) & ", " &
                    " " & Val(.PaidAmount) & ", " &
                    " " & AgL.Chk_Text(.EntryBy) & ", " & AgL.Chk_Text(.EntryDate) & ", " &
                    " " & AgL.Chk_Text(.EntryType) & ", " & AgL.Chk_Text(.EntryStatus) & ", " &
                    " " & AgL.Chk_Text(.ApproveBy) & ", " & AgL.Chk_Text(.ApproveDate) & ", " &
                    " " & AgL.Chk_Text(.MoveToLog) & ", " & AgL.Chk_Text(.MoveToLogDate) & ", " &
                    " " & AgL.Chk_Text(.IsDeleted) & ", " & AgL.Chk_Text(.Status) & ", " &
                    " " & AgL.Chk_Text(.UID) & ") "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End With
    End Sub


    Public Shared Sub ProcReminderSave(ByVal EntryDate As String, ByVal EntryTime As String, ByVal RemindDate As String, ByVal RemindTime As String, ByVal RemindNarration As String, ByVal StrRemindTo As String, Optional ByVal CurrentReminderId As String = "", Optional ByVal ParrentReminderId As String = "")
        Dim mQry As String
        Dim mSearchCode As String = ""
        Dim mTrans As Boolean = False
        Dim mID As String
        Dim I As Integer, mSr As Integer
        Dim mstrArrUser As String()
        Try
            mstrArrUser = StrRemindTo.Split(",")
            If CurrentReminderId = "" Then
                mQry = " SELECT IfNull(max(substring(R.ID,7,4)),0)+ 1  FROM Reminder R WHERE R.V_Date = " & AgL.Chk_Text(EntryDate) & ""
                mID = AgL.Dman_Execute(mQry, AgL.GCn, AgL.ECmd).ExecuteScalar().ToString().PadLeft(4, "0")
                mSearchCode = Format(CDate(EntryDate), "ddMMyy") + mID

                mQry = " INSERT INTO Reminder (ID, Ref_ID, V_Date, V_Time, EntryBy, Narration, RemindTo ," &
                        " Reminder_Date, Reminder_Time) " &
                        " VALUES ('" & mSearchCode & "'," & AgL.Chk_Text(ParrentReminderId) & ", " & AgL.Chk_Text(EntryDate) & ", " & AgL.Chk_Text(EntryTime) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(RemindNarration) & ", " & AgL.Chk_Text(StrRemindTo) & ", " & AgL.Chk_Text(RemindDate) & "," &
                        " " & AgL.Chk_Text(RemindTime) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            Else
                mSearchCode = CurrentReminderId
                mQry = " UPDATE Reminder " &
                         " SET Narration = " & AgL.Chk_Text(RemindNarration) & ", " &
                         " RemindTo = " & AgL.Chk_Text(StrRemindTo) & ", " &
                         " Reminder_Date = " & AgL.Chk_Text(RemindDate) & ", " &
                         " Reminder_Time = " & AgL.Chk_Text(RemindTime) & " " &
                         " WHERE ID = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From ReminderDetail Where ID = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            For I = 0 To mstrArrUser.Length - 1
                mSr += 1
                mQry = " INSERT INTO ReminderDetail (ID, Sr, Reminder_To, Status) " &
                        " VALUES ('" & mSearchCode & "', " & mSr & ", " & AgL.Chk_Text(mstrArrUser(I)) & ", 'Active') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub ProcGetPartyAddress(ByVal Party As String, ByRef Address As String, ByRef City As String, ByVal Conn As SQLiteConnection)
        Dim mQry$ = ""
        Dim DtTemp As DataTable = Nothing
        Try
            mQry = " SELECT CityCode As City, IfNull(Add1,'') + IfNull(Add2,'') + IfNull(Add3,'') AS Address FROM SubGroup WHERE SubCode = '" & Party & "' "
            DtTemp = AgL.FillData(mQry, Conn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
                    City = AgL.XNull(.Rows(0)("City"))
                    Address = Strings.Left(AgL.XNull(.Rows(0)("Address")), 100)
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message & " In AgTemplate.ClsMain.ProGetPartyAddress.", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Public Shared Function FRetTermsCondition(ByVal VType As String) As String
        Dim mQry As String
        mQry = " SELECT IfNull(H.Description,'')  FROM TermsCondition H " &
                " WHERE H.Code ='" & VType & "' "
        FRetTermsCondition = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn, AgL.ECmd).ExecuteScalar)
    End Function

    Public Shared Sub ProcCreateLink(ByVal DGL As DataGridView, ByVal ColumnName As String)
        Try
            DGL.Columns(ColumnName).CellTemplate.Style.Font = New Font(DGL.DefaultCellStyle.Font.FontFamily, DGL.DefaultCellStyle.Font.Size, FontStyle.Underline)
            DGL.Columns(ColumnName).CellTemplate.Style.ForeColor = Color.Blue

            If DGL.Rows.Count > 0 Then
                DGL.Item(ColumnName, 0).Style.Font = New Font(DGL.DefaultCellStyle.Font.FontFamily, DGL.DefaultCellStyle.Font.Size, FontStyle.Underline)
                DGL.Item(ColumnName, 0).Style.ForeColor = Color.Blue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Shared Function FGetManualRefNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, ByVal V_Date As String, ByVal Div_Code As String, ByVal Site_Code As String, Optional ByVal RefType As ManualRefType = ManualRefType.Max) As String
    '    Dim mQry$
    '    Dim mStartSrlNo As Integer = 0
    '    Dim mStartDate As String, mEndDate As String

    '    If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
    '        mStartDate = "01/Apr/2013"
    '        mEndDate = "31/Mar/2014"            
    '    ElseIf CDate(V_Date) >= CDate("01/Apr/2012") And CDate(V_Date) <= CDate("31/Mar/2013") Then
    '        mStartDate = "01/Apr/2012"
    '        mEndDate = "31/Mar/2013"
    '    ElseIf CDate(V_Date) >= CDate("01/Apr/2011") And CDate(V_Date) <= CDate("31/Mar/2012") Then
    '        mStartDate = "01/Apr/2011"
    '        mEndDate = "31/Mar/2012"
    '    ElseIf CDate(V_Date) >= CDate("01/Apr/2010") And CDate(V_Date) <= CDate("31/Mar/2011") Then
    '        mStartDate = "01/Apr/2010"
    '        mEndDate = "31/Mar/2011"
    '    Else
    '        mStartDate = "01/Apr/2009"
    '        mEndDate = "31/Mar/2010"
    '    End If

    '    Select Case RefType
    '        Case ManualRefType.DayWise
    '            mQry = "Select IfNull(Max(Convert(Numeric,Replace(Replace(" & FieldName & ",'-',''),'.',''))),0)+1 From " & TableName & "  WHERE isnumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date = '" & AgL.PubLoginDate & "'  AND IsNumeric(" & FieldName & ") = 0 "
    '            FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    '            FGetManualRefNo = CDate(AgL.PubLoginDate).ToString("yyMMdd").ToString + "-" + FGetManualRefNo.ToString.PadLeft(4, "0")

    '        Case Else
    '            If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
    '                mQry = "Select IfNull(Max(Convert(Numeric,Replace(Replace(" & FieldName & ",'-',''),'.',''))),0)+1 From " & TableName & "  WHERE isnumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date Between '" & mStartDate & "' and  '" & mEndDate & "'  AND IsNumeric(" & FieldName & ") = 0 "
    '                FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    '                FGetManualRefNo = IIf(Val(FGetManualRefNo) > 130000, Val(FGetManualRefNo) - 130000, FGetManualRefNo)
    '                FGetManualRefNo = "13-" + FGetManualRefNo.ToString.PadLeft(4, "0")
    '            Else
    '                mQry = "Select IfNull(Max(Convert(Numeric,Replace(Replace(" & FieldName & ",'-',''),'.',''))),0)+1 From " & TableName & "  WHERE isnumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date Between '" & mStartDate & "' and  '" & mEndDate & "'  AND IsNumeric(" & FieldName & ") = 0  "
    '                FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    '            End If
    '    End Select
    'End Function

    Public Shared Function FGetManualRefNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, ByVal V_Date As String, ByVal Div_Code As String, ByVal Site_Code As String, Optional ByVal RefType As ManualRefType = ManualRefType.Max) As String
        Dim mQry$
        Dim mStartSrlNo As Integer = 0
        Dim mStartDate As String, mEndDate As String
        Dim mRef_Prefix$ = ""
        Dim mRef_PadLength As Integer = 0



        If CDate(V_Date) >= CDate("01/Apr/2018") And CDate(V_Date) <= CDate("31/Mar/2019") Then
            mStartDate = "01/Apr/2018"
            mEndDate = "31/Mar/2019"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2017") And CDate(V_Date) <= CDate("31/Mar/2018") Then
            mStartDate = "01/Apr/2017"
            mEndDate = "31/Mar/2018"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2016") And CDate(V_Date) <= CDate("31/Mar/2017") Then
            mStartDate = "01/Apr/2016"
            mEndDate = "31/Mar/2017"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2015") And CDate(V_Date) <= CDate("31/Mar/2016") Then
            mStartDate = "01/Apr/2015"
            mEndDate = "31/Mar/2016"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2014") And CDate(V_Date) <= CDate("31/Mar/2015") Then
            mStartDate = "01/Apr/2014"
                mEndDate = "31/Mar/2015"
            ElseIf CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
            mStartDate = "01/Apr/2013"
            mEndDate = "31/Mar/2014"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2012") And CDate(V_Date) <= CDate("31/Mar/2013") Then
            mStartDate = "01/Apr/2012"
            mEndDate = "31/Mar/2013"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2011") And CDate(V_Date) <= CDate("31/Mar/2012") Then
            mStartDate = "01/Apr/2011"
            mEndDate = "31/Mar/2012"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2010") And CDate(V_Date) <= CDate("31/Mar/2011") Then
            mStartDate = "01/Apr/2010"
            mEndDate = "31/Mar/2011"
        Else
            mStartDate = "01/Apr/2009"
            mEndDate = "31/Mar/2010"
        End If

        Select Case RefType
            Case ManualRefType.DayWise
                mQry = "Select IfNull(Max(Convert(Numeric,Replace(Replace(" & FieldName & ",'-',''),'.',''))),0)+1 From " & TableName & "  WHERE isnumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date = '" & AgL.PubLoginDate & "'  AND IsNumeric(" & FieldName & ") = 0 "
                FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                FGetManualRefNo = CDate(AgL.PubLoginDate).ToString("yyMMdd").ToString + "-" + FGetManualRefNo.ToString.PadLeft(4, "0")

            Case Else
                mRef_Prefix = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date_From = '" & CDate(mStartDate).ToString("u") & "' And Date_To = '" & CDate(mEndDate).ToString("u") & "'", AgL.GcnRead).ExecuteScalar)
                If mRef_Prefix = "" Then
                    If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
                        mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & "  WHERE  ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date Between '" & CDate(mStartDate).ToString("u") & "' and  '" & CDate(mEndDate).ToString("u") & "' "
                        FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                        If Val(FGetManualRefNo) > 1300000 Then
                            FGetManualRefNo = Val(FGetManualRefNo) - 1300000
                        ElseIf Val(FGetManualRefNo) > 130000 Then
                            FGetManualRefNo = Val(FGetManualRefNo) - 130000
                        Else
                            FGetManualRefNo = FGetManualRefNo
                        End If
                        FGetManualRefNo = "13-" + FGetManualRefNo.ToString.PadLeft(4, "0")

                    Else
                        mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & "  WHERE ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date Between '" & CDate(mStartDate).ToString("u") & "' and  '" & CDate(mEndDate).ToString("u") & "'    "
                        FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                    End If
                Else
                    mQry = "Select IfNull(Max(Cast(Replace(Replace(Replace(" & FieldName & ",'-',''),'.',''),'" & mRef_Prefix & "','') as Integer)),0) + 1 From " & TableName & "  WHERE Abs(Replace(Replace(Replace(" & FieldName & ",'-',''),'.',''),'" & mRef_Prefix & "',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "' And EntryStatus <> 'Discard' And V_Date Between '" & CDate(mStartDate).ToString("u") & "' and  '" & CDate(mEndDate).ToString("u") & "'   "
                    FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                    mRef_PadLength = AgL.VNull(AgL.Dman_Execute("Select Ref_PadLength From Voucher_Prefix Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date_From = '" & CDate(mStartDate).ToString("u") & "' And Date_To = '" & CDate(mEndDate).ToString("u") & "'", AgL.GcnRead).ExecuteScalar)
                    If mRef_PadLength = 0 Then
                        FGetManualRefNo = mRef_Prefix & "-" + FGetManualRefNo.ToString.PadLeft(4, "0")
                    Else
                        FGetManualRefNo = mRef_Prefix & "-" + FGetManualRefNo.ToString.PadLeft(mRef_PadLength, "0")
                    End If
                End If
        End Select
    End Function

    Public Shared Function FCheckDuplicateRefNo(ByVal FieldName As String, ByVal TableName As String,
                                          ByVal V_Type As String, ByVal V_Date As String,
                                          ByVal Div_Code As String, ByVal Site_Code As String,
                                          ByVal EntryMode As String, ByVal ReferenceNo As String,
                                          ByVal InternalCode As String) As Boolean
        Dim mQry$ = ""
        FCheckDuplicateRefNo = True

        Dim mStartDate As String, mEndDate As String
        If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
            mStartDate = "01/Apr/2013"
            mEndDate = "31/Mar/2014"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2012") And CDate(V_Date) <= CDate("31/Mar/2013") Then
            mStartDate = "01/Apr/2012"
            mEndDate = "31/Mar/2013"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2011") And CDate(V_Date) <= CDate("31/Mar/2012") Then
            mStartDate = "01/Apr/2011"
            mEndDate = "31/Mar/2012"
        ElseIf CDate(V_Date) >= CDate("01/Apr/2010") And CDate(V_Date) <= CDate("31/Mar/2011") Then
            mStartDate = "01/Apr/2010"
            mEndDate = "31/Mar/2011"
        Else
            mStartDate = "01/Apr/2009"
            mEndDate = "31/Mar/2010"
        End If

        If AgL.StrCmp(EntryMode, "Add") Then
            mQry = " SELECT COUNT(*) " &
                    " FROM " & TableName & " " &
                    " WHERE " & FieldName & " = '" & ReferenceNo & "'   " &
                    " AND V_Type ='" & V_Type & "'  " &
                    " And Div_Code = '" & Div_Code & "' " &
                    " And Site_Code = '" & Site_Code & "' " &
                    " And IfNull(IsDeleted,0) = 0  " &
                    " And V_Date Between '" & mStartDate & "' and  '" & mEndDate & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists")
        Else
            mQry = " SELECT COUNT(*) " &
                    " FROM " & TableName & " " &
                    " WHERE " & FieldName & " = '" & ReferenceNo & "'  " &
                    " AND V_Type ='" & V_Type & "'  " &
                    " And Div_Code = '" & Div_Code & "' " &
                    " And Site_Code = '" & Site_Code & "' " &
                    " And IfNull(IsDeleted,0) = 0 " &
                    " AND DocID <>'" & InternalCode & "'  " &
                    " And V_Date Between '" & mStartDate & "' and  '" & mEndDate & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists")
        End If
    End Function

    Private Sub FCostCenterMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.VarChar, 30, , False)
        AgL.FSetColumnValue(MdlTable, "CostCenterType", AgLibrary.ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "Parent", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Parent", "Code", "CostCenterMast")
    End Sub

    Private Sub FAcGroupPath(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)

        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "GroupCode", AgLibrary.ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "SNo", AgLibrary.ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "GroupUnder", AgLibrary.ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "GroupUnder", "GroupCode", "AcGroup")
    End Sub

    Private Sub FEnviro_Accounts(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "ID", AgLibrary.ClsMain.SQLDataType.VarChar, 1, True, False)
        AgL.FSetColumnValue(MdlTable, "MaintainTDS", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "AutoPosting", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "VRNumberSystem", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TDSROff", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "SrvTaxAdjRefType", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
    End Sub

    Private Sub FLedgerAdj(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Adj_Type", AgLibrary.ClsMain.SQLDataType.VarChar, 20, , False)
        AgL.FSetColumnValue(MdlTable, "Vr_DocId", AgLibrary.ClsMain.SQLDataType.VarChar, 21, , False)
        AgL.FSetColumnValue(MdlTable, "Vr_V_SNo", AgLibrary.ClsMain.SQLDataType.SmallInt, , , False)
        AgL.FSetColumnValue(MdlTable, "Adj_DocID", AgLibrary.ClsMain.SQLDataType.VarChar, 21, , False)
        AgL.FSetColumnValue(MdlTable, "Adj_V_SNo", AgLibrary.ClsMain.SQLDataType.SmallInt, , , False)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.VarChar, 2, , False)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        If UCase("LedgerAdj") = UCase(Trim(StrTableName)) Then
            AgL.FSetFKeyValue(MdlTable, "Adj_DocId,Adj_V_SNo", "DocId,V_SNo", "Ledger")
        Else
            AgL.FSetFKeyValue(MdlTable, "Adj_DocId,Adj_V_SNo", "DocId,V_SNo", "Ledger_Temp")
        End If
    End Sub

    Private Sub FLedgerItemAdj(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_SNo", AgLibrary.ClsMain.SQLDataType.SmallInt, , True)
        AgL.FSetColumnValue(MdlTable, "ItemCode", AgLibrary.ClsMain.SQLDataType.VarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Quantity", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", AgLibrary.ClsMain.SQLDataType.VarChar, 100)

        AgL.FSetFKeyValue(MdlTable, "ItemCode", "Code", "ItemMast")
    End Sub

    Private Sub FNarrationMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
    End Sub

    Private Sub FAcFilteration(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.VarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Nature", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
    End Sub

    Private Sub FLedgerGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
    End Sub

    Private Sub FZoneMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", AgLibrary.ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", AgLibrary.ClsMain.SQLDataType.VarChar, 1)
    End Sub

    Private Sub FDataTrfd(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", AgLibrary.ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "U_Name", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub


    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String,
         Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
         Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
         Optional ByVal SubReport_QueryList As String = "",
         Optional ByVal SubReport_NameList As String = "", Optional ByVal PartyCode As String = "", Optional ByVal V_Date As String = "")

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try
            mQry = "Select * from Voucher_Type_Settings  " &
                       "Where V_Type = '" & V_Type & "' " &
                       "And Site_Code = '" & AgL.PubSiteCode & "' " &
                       "And Div_Code  = '" & AgL.PubDivCode & "' "
            DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtVTypeSetting.Rows.Count <> 0 Then
                If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) <> "" Then
                    Report_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<SEARCHCODE>`", "'" & objFrm.mSearchCode & "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<PARTYCODE>`", "'" & PartyCode & "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<VOUCHERDATE>`", "'" & V_Date & "'")
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) <> "" Then
                    Report_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) <> "" Then
                    Report_TitleList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format")) <> "" Then
                    Report_FormatList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList")) <> "" Then
                    SubReport_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList"))
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<SEARCHCODE>`", "'" & objFrm.mSearchCode & "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<PARTYCODE>`", "'" & PartyCode & "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<VOUCHERDATE>`", "'" & V_Date & "'")
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList")) <> "" Then
                    SubReport_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList"))
                End If
            End If

            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "|")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If

            AgL.ADMain = New SQLiteDataAdapter(RepQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    AgL.ADMain = New SQLiteDataAdapter(SubReport_QryArr(I).ToString, AgL.GCn)
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    AgL.ADMain.Fill(SubReport_DataSetArr(I))
                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & Report_NameList & (I + 1).ToString & ".ttx", True)
                Next
            End If

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                Next
            End If

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Public Shared Function FOpenMaster(ByVal FrmParent As Object, ByVal MasterName As String, ByVal V_Type As String) As String
        Dim FrmObjMDI As Object
        Dim FrmObj As Object
        Dim DtTemp As DataTable = Nothing

        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""
        Dim mQry As String = ""
        FOpenMaster = ""

        Try
            mQry = " Select * From Master_Settings Where MasterName = '" & MasterName & "' And V_Type = '" & V_Type & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count = 0 Then
                mQry = " Select * From Master_Settings Where MasterName = '" & MasterName & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            End If

            If DtTemp.Rows.Count > 0 Then
                StrModuleName = AgL.XNull(DtTemp.Rows(0)("MnuAttachedInModule"))

                StrMnuName = AgL.XNull(DtTemp.Rows(0)("MnuName"))
                StrMnuText = AgL.XNull(DtTemp.Rows(0)("MnuText"))

                FrmObjMDI = FrmParent.MdiParent

                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
                FrmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
                FrmObj.StartPosition = FormStartPosition.CenterParent
                FrmObj.ShowDialog()
                FOpenMaster = FrmObj.mSearchCode
                FrmObj = Nothing
            Else
                FrmObj = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function FOpenForm(ByVal StrModuleName, ByVal StrMnuName, ByVal StrMnuText) As Form
        Select Case UCase(StrModuleName)
            Case UCase(ClsMain.ModuleName)
                Dim CFOpen As New ClsFunction
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing

            Case Else
                FOpenForm = Nothing
        End Select
    End Function

    'Public Shared Function FIsNegativeStock(ByVal mSelectionQry As String, ByVal SearchCode As String, ByVal Godown As String, ByVal V_Date As String) As Boolean
    '    Dim DtTemp As DataTable = Nothing
    '    Dim DtGodownSettings As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Dim mItemInTransactionQry$ = "", mTillTransactionDateStockQry$ = "", mCurrentStockQry$ = "", bTempTable$ = "", mQry$ = "", ErrorQry$ = "", ErrorMsg$ = ""

    '    FIsNegativeStock = True

    '    mQry = " Select RestrictNegetiveStock, AlertOnNegetiveStock From Godown Where Code = '" & Godown & "'"
    '    DtGodownSettings = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    If AgL.VNull(DtGodownSettings.Rows(0)("RestrictNegetiveStock")) <> 0 Or AgL.VNull(DtGodownSettings.Rows(0)("AlertOnNegetiveStock")) <> 0 Then
    '        bTempTable = AgL.GetGUID(AgL.GCn).ToString
    '        mQry = "CREATE TABLE [#" & bTempTable & "] " & _
    '              " ( " & _
    '              " Item nVarchar(100), " & _
    '              " LotNo nVarchar(100), " & _
    '              " Process nVarchar(10), " & _
    '              " Dimension1 nVarchar(10), " & _
    '              " Dimension2 nVarchar(10), " & _
    '              " Qty Float, " & _
    '              " CurrentStock Float " & _
    '              " ) "
    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    '        If mSelectionQry <> "" Then
    '            mQry = "Insert Into [#" & bTempTable & "]  (Item, LotNo, Process, Dimension1, Dimension2, Qty) " & mSelectionQry
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    '        End If

    '        mItemInTransactionQry = " Select L.Item, " & _
    '                        " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then L.LotNo ELse Null End As LotNo, " & _
    '                        " L.Process, L.Dimension1, L.Dimension2, " & _
    '                        " Sum(L.Qty) As Qty " & _
    '                        " From [#" & bTempTable & "] As L  " & _
    '                        " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') Isd ON L.Item = Isd.Code  COLLATE DATABASE_DEFAULT " & _
    '                        " Group By L.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then L.LotNo ELse Null End, " & _
    '                        " L.Process, L.Dimension1, L.Dimension2 "

    '        mTillTransactionDateStockQry = " Select S.Item, " & _
    '                            " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End As LotNo, " & _
    '                            " S.Process, S.Dimension1, S.Dimension2, " & _
    '                            " IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As TillTransactionDateStock " & _
    '                            " From (Select DocId, Godown, V_Date, Item, LotNo, Qty_Rec, Qty_Iss, Process, Dimension1, Dimension2  From Stock Where Item In (Select Item COLLATE DATABASE_DEFAULT From [#" & bTempTable & "])) As S " & _
    '                            " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') As Isd ON S.Item = Isd.Code  COLLATE DATABASE_DEFAULT " & _
    '                            " Where S.DocId <> '" & SearchCode & "' " & _
    '                            " And S.Godown = '" & Godown & "' " & _
    '                            " And S.V_Date <= '" & V_Date & "' " & _
    '                            " Group By S.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End, " & _
    '                            " S.Process, S.Dimension1, S.Dimension2 "

    '        mCurrentStockQry = " Select S.Item, " & _
    '                " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End As LotNo, " & _
    '                " S.Process, S.Dimension1, S.Dimension2, " & _
    '                " Round(IfNull(Sum(S.Qty_Rec),0),IfNull(Max(U.Decimalplaces),0)) - Round(IfNull(Sum(S.Qty_Iss),0),IfNull(Max(U.Decimalplaces),0)) As CurrentStock " & _
    '                " From (Select DocId, Godown, V_Date, Unit, Item, LotNo, Qty_Rec, Qty_Iss, Process, Dimension1, Dimension2 From Stock Where Item In (Select Item COLLATE DATABASE_DEFAULT From [#" & bTempTable & "])) As S " & _
    '                " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') As Isd ON S.Item = Isd.Code  COLLATE DATABASE_DEFAULT " & _
    '                " LEFT JOIN Unit U ON U.Code = S.Unit COLLATE DATABASE_DEFAULT " & _
    '                " Where S.DocId <> '" & SearchCode & "' " & _
    '                " And S.Godown = '" & Godown & "' " & _
    '                " Group By S.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End, " & _
    '                " S.Process, S.Dimension1, S.Dimension2 "

    '        mQry = " Select H.Item, H.LotNo, I.Description As ItemDesc, P.Description As ProcessDesc, IfNull(H.Qty,0) As Qty, Round(IfNull(L.CurrentStock,0),IfNull(U.Decimalplaces,0)) As Stock " & _
    '                " From (" & mItemInTransactionQry & ") As H " & _
    '                " LEFT JOIN (" & mCurrentStockQry & ") As L On H.Item = L.Item COLLATE DATABASE_DEFAULT  " & _
    '                "                  And IfNull(H.LotNo,'') = IfNull(L.LotNo,'') COLLATE DATABASE_DEFAULT  " & _
    '                "                  And IfNull(H.Process,'') = IfNull(L.Process,'') COLLATE DATABASE_DEFAULT  " & _
    '                "                  And IfNull(H.Dimension1,'') = IfNull(L.Dimension1,'') COLLATE DATABASE_DEFAULT  " & _
    '                "                  And IfNull(H.Dimension2,'') = IfNull(L.Dimension2,'') COLLATE DATABASE_DEFAULT  " & _
    '                " LEFT JOIN Item I On H.Item = I.Code COLLATE DATABASE_DEFAULT " & _
    '                " LEFT JOIN Unit U ON U.Code = I.Unit COLLATE DATABASE_DEFAULT " & _
    '                " LEFT JOIN Process P On H.Process = P.NCat COLLATE DATABASE_DEFAULT " & _
    '                " Where Round(IfNull(L.CurrentStock,0),IfNull(U.Decimalplaces,0)) - IfNull(H.Qty,0) < 0 "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                ErrorMsg = "Current Stock <" & AgL.VNull(DtTemp.Rows(I)("Stock")) & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And LotNo """ & AgL.XNull(DtTemp.Rows(I)("LotNo")) & """ And Process """ & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & """ Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">."
    '                If ErrorQry <> "" Then ErrorQry += " UNION ALL "
    '                ErrorQry += " Select '" & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & "' As Item, " & _
    '                        " '" & AgL.XNull(DtTemp.Rows(I)("LotNo")) & "' As LotNo, " & _
    '                        " '" & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & "' As Process, " & _
    '                        " " & AgL.VNull(DtTemp.Rows(I)("Qty")) & " As Qty, " & _
    '                        " " & AgL.VNull(DtTemp.Rows(I)("Stock")) & " As Stock, " & _
    '                        " '" & ErrorMsg & "' As Message "
    '            Next
    '        Else
    '            mQry = " Select H.Item, L.LotNo, I.Description As ItemDesc, P.Description As ProcessDesc, IfNull(H.Qty,0) As Qty, Round(IfNull(L.TillTransactionDateStock,0),IfNull(U.Decimalplaces,0)) As Stock " & _
    '                    " From (" & mItemInTransactionQry & ") As H " & _
    '                    " LEFT JOIN (" & mTillTransactionDateStockQry & ") As L On H.Item = L.Item COLLATE DATABASE_DEFAULT " & _
    '                    "                  And IfNull(H.LotNo,'') = IfNull(L.LotNo,'') COLLATE DATABASE_DEFAULT  " & _
    '                    "                  And IfNull(H.Process,'') = IfNull(L.Process,'') COLLATE DATABASE_DEFAULT  " & _
    '                    "                  And IfNull(H.Dimension1,'') = IfNull(L.Dimension1,'') COLLATE DATABASE_DEFAULT  " & _
    '                    "                  And IfNull(H.Dimension2,'') = IfNull(L.Dimension2,'') COLLATE DATABASE_DEFAULT  " & _
    '                    " LEFT JOIN Item I On H.Item = I.Code COLLATE DATABASE_DEFAULT " & _
    '                    " LEFT JOIN Unit U ON U.Code = I.Unit COLLATE DATABASE_DEFAULT " & _
    '                    " LEFT JOIN Process P On H.Process = P.NCat COLLATE DATABASE_DEFAULT " & _
    '                    " Where  Round(IfNull(L.TillTransactionDateStock,0),IfNull(U.Decimalplaces,0)) - IfNull(H.Qty,0) < 0 "
    '            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '            If DtTemp.Rows.Count > 0 Then
    '                For I = 0 To DtTemp.Rows.Count - 1
    '                    ErrorMsg = "Stock Till Date " & V_Date & "  <" & AgL.VNull(DtTemp.Rows(I)("Stock")) & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And Lot No """ & AgL.XNull(DtTemp.Rows(I)("LotNo")) & """ And Process """ & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & """ Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">."
    '                    If ErrorQry <> "" Then ErrorQry += " UNION ALL "
    '                    ErrorQry += " Select '" & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & "' As Item, " & _
    '                                " '" & AgL.XNull(DtTemp.Rows(I)("LotNo")) & "' As LotNo, " & _
    '                                " '" & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & "' As Process, " & _
    '                                " " & AgL.VNull(DtTemp.Rows(I)("Qty")) & " As Qty, " & _
    '                                " " & AgL.VNull(DtTemp.Rows(I)("Stock")) & " As Stock, " & _
    '                                " '" & ErrorMsg & "' As Message "
    '                Next
    '            End If
    '        End If
    '    End If
    '    If ErrorQry <> "" Then
    '        Dim FrmObj As New AgTemplate.FrmErrorBox(ErrorQry)
    '        If AgL.VNull(DtGodownSettings.Rows(0)("RestrictNegetiveStock")) <> 0 Then FrmObj.BtnContinue.Enabled = False
    '        FrmObj.Dgl1.AutoResizeRows()
    '        FrmObj.ShowDialog()
    '        If FrmObj.mQuitButtonPressed Then FIsNegativeStock = False Else FIsNegativeStock = True
    '    End If
    'End Function

    Public Shared Function FIsNegativeStock(ByVal mSelectionQry As String, ByVal SearchCode As String, ByVal Godown As String, ByVal V_Date As String) As Boolean
        Dim DtTemp As DataTable = Nothing
        Dim DtGodownSettings As DataTable = Nothing
        Dim I As Integer = 0
        Dim mItemInTransactionQry$ = "", mTillTransactionDateStockQry$ = "", mCurrentStockQry$ = "", bTempTable$ = "", mQry$ = "", ErrorQry$ = "", ErrorMsg$ = ""

        FIsNegativeStock = True

        mQry = " Select RestrictNegetiveStock, AlertOnNegetiveStock From Godown Where Code = '" & Godown & "'"
        DtGodownSettings = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If AgL.VNull(DtGodownSettings.Rows(0)("RestrictNegetiveStock")) <> 0 Or AgL.VNull(DtGodownSettings.Rows(0)("AlertOnNegetiveStock")) <> 0 Then
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = "CREATE TABLE [#" & bTempTable & "] " &
                  " ( " &
                  " Item nVarchar(100), " &
                  " LotNo nVarchar(100), " &
                  " Qty Float, " &
                  " CurrentStock Float " &
                  " ) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            If mSelectionQry <> "" Then
                mQry = "Insert Into [#" & bTempTable & "]  (Item, LotNo, Qty) " & mSelectionQry
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mItemInTransactionQry = " Select L.Item, " &
                            " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then L.LotNo ELse Null End As LotNo, " &
                            " Sum(L.Qty) As Qty " &
                            " From [#" & bTempTable & "] As L  " &
                            " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') Isd ON L.Item = Isd.Code  COLLATE DATABASE_DEFAULT " &
                            " Group By L.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then L.LotNo ELse Null End "


            mTillTransactionDateStockQry = " Select S.Item, " &
                                " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End As LotNo, " &
                                " IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As TillTransactionDateStock " &
                                " From (Select DocId, Godown, V_Date, Item, LotNo, Qty_Rec, Qty_Iss From Stock Where Item In (Select Item COLLATE DATABASE_DEFAULT From [#" & bTempTable & "])) As S " &
                                " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') As Isd ON S.Item = Isd.Code  COLLATE DATABASE_DEFAULT " &
                                " Where S.DocId <> '" & SearchCode & "' " &
                                " And S.Godown = '" & Godown & "' " &
                                " And S.V_Date <= '" & V_Date & "' " &
                                " Group By S.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End "

            mCurrentStockQry = " Select S.Item, " &
                    " Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End As LotNo, " &
                    " Round(IfNull(Sum(S.Qty_Rec),0),IfNull(Max(U.Decimalplaces),0)) - Round(IfNull(Sum(S.Qty_Iss),0),IfNull(Max(U.Decimalplaces),0)) As CurrentStock " &
                    " From (Select DocId, Godown, V_Date, Unit, Item, LotNo, Qty_Rec, Qty_Iss From Stock Where Item In (Select Item COLLATE DATABASE_DEFAULT From [#" & bTempTable & "])) As S " &
                    " LEFT JOIN (Select Code, IsRequired_LotNo From ItemSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "') As Isd ON S.Item = Isd.Code  COLLATE DATABASE_DEFAULT " &
                    " LEFT JOIN Unit U ON U.Code = S.Unit COLLATE DATABASE_DEFAULT " &
                    " Where S.DocId <> '" & SearchCode & "' " &
                    " And S.Godown = '" & Godown & "' " &
                    " Group By S.Item, Case When IfNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo ELse Null End "

            mQry = " Select H.Item, H.LotNo, I.Description As ItemDesc, IfNull(H.Qty,0) As Qty, Round(IfNull(L.CurrentStock,0),IfNull(U.Decimalplaces,0)) As Stock " &
                    " From (" & mItemInTransactionQry & ") As H " &
                    " LEFT JOIN (" & mCurrentStockQry & ") As L On H.Item = L.Item COLLATE DATABASE_DEFAULT  " &
                    "                  And IfNull(H.LotNo,'') = IfNull(L.LotNo,'') COLLATE DATABASE_DEFAULT  " &
                    " LEFT JOIN Item I On H.Item = I.Code COLLATE DATABASE_DEFAULT " &
                    " LEFT JOIN Unit U ON U.Code = I.Unit COLLATE DATABASE_DEFAULT " &
                    " Where Round(IfNull(L.CurrentStock,0),IfNull(U.Decimalplaces,0)) - IfNull(H.Qty,0) < 0 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    ErrorMsg = "Current Stock <" & AgL.VNull(DtTemp.Rows(I)("Stock")) & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And LotNo """ & AgL.XNull(DtTemp.Rows(I)("LotNo")) & """ Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">."
                    If ErrorQry <> "" Then ErrorQry += " UNION ALL "
                    ErrorQry += " Select '" & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & "' As Item, " &
                            " '" & AgL.XNull(DtTemp.Rows(I)("LotNo")) & "' As LotNo, " &
                            " " & AgL.VNull(DtTemp.Rows(I)("Qty")) & " As Qty, " &
                            " " & AgL.VNull(DtTemp.Rows(I)("Stock")) & " As Stock, " &
                            " '" & ErrorMsg & "' As Message "
                Next
            Else
                mQry = " Select H.Item, L.LotNo, I.Description As ItemDesc, IfNull(H.Qty,0) As Qty, Round(IfNull(L.TillTransactionDateStock,0),IfNull(U.Decimalplaces,0)) As Stock " &
                        " From (" & mItemInTransactionQry & ") As H " &
                        " LEFT JOIN (" & mTillTransactionDateStockQry & ") As L On H.Item = L.Item COLLATE DATABASE_DEFAULT " &
                        "                  And IfNull(H.LotNo,'') = IfNull(L.LotNo,'') COLLATE DATABASE_DEFAULT  " &
                        " LEFT JOIN Item I On H.Item = I.Code COLLATE DATABASE_DEFAULT " &
                        " LEFT JOIN Unit U ON U.Code = I.Unit COLLATE DATABASE_DEFAULT " &
                        " Where  Round(IfNull(L.TillTransactionDateStock,0),IfNull(U.Decimalplaces,0)) - IfNull(H.Qty,0) < 0 "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        ErrorMsg = "Stock Till Date " & V_Date & "  <" & AgL.VNull(DtTemp.Rows(I)("Stock")) & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And Lot No """ & AgL.XNull(DtTemp.Rows(I)("LotNo")) & """ Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">."
                        If ErrorQry <> "" Then ErrorQry += " UNION ALL "
                        ErrorQry += " Select '" & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & "' As Item, " &
                                    " '" & AgL.XNull(DtTemp.Rows(I)("LotNo")) & "' As LotNo, " &
                                    " " & AgL.VNull(DtTemp.Rows(I)("Qty")) & " As Qty, " &
                                    " " & AgL.VNull(DtTemp.Rows(I)("Stock")) & " As Stock, " &
                                    " '" & ErrorMsg & "' As Message "
                    Next
                End If
            End If
        End If
        If ErrorQry <> "" Then
            Dim FrmObj As New AgTemplate.FrmErrorBox(ErrorQry)
            If AgL.VNull(DtGodownSettings.Rows(0)("RestrictNegetiveStock")) <> 0 Then FrmObj.BtnContinue.Enabled = False
            FrmObj.Dgl1.AutoResizeRows()
            FrmObj.ShowDialog()
            If FrmObj.mQuitButtonPressed Then FIsNegativeStock = False Else FIsNegativeStock = True
        End If
    End Function

    Public Shared Sub ProcExecuteQry_FromTextFile(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim Sr As StreamReader
        Dim SrW As StreamWriter
        Try
            StrPath = My.Application.Info.DirectoryPath & "\ExecuteQry.txt"
            Sr = New StreamReader(StrPath)
            Dim mQry As String = ""
            Dim Line As String = ""
            Do
                Line = Sr.ReadLine()
                mQry += Line & vbCrLf
            Loop Until Line Is Nothing
            Sr.Close()

            If mQry.Trim <> "" Then
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                SrW = New StreamWriter(StrPath)
                SrW.WriteLine("")
                SrW.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function FGetJobRate(ByVal mProcess As String, ByVal mParty As String, ByVal mItem As String) As Double
        Dim mQry$ = ""
        mQry = " Select Rate From RateListDetail L  " &
                " Where IfNull(SubCode,'') = (SELECT CASE WHEN  Count(*) > 0 THEN IfNull(Max(PartyRateGroup),'') ELSE '' END From SubGroup Where SubCode = '" & mParty & "') " &
                " And IfNull(Item,'') = (SELECT CASE WHEN Count(*) > 0 THEN IfNull(Max(ItemRateGroup),'') ELSE '' END  From ItemProcessDetail Where Code = '" & mItem & "' And Process = '" & mProcess & "') " &
                " And Process ='" & mProcess & "'"
        FGetJobRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Function

    Public Shared Sub FGetTransactionHistory(ByVal FrmObj As Form, ByVal mSearchCode As String, ByVal mQry As String,
                                             ByVal DGL As AgControls.AgDataGrid, ByVal DtV_TypeSettings As DataTable, ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Dim CSV_Qry As String = ""
        Dim CSV_QryArr() As String = Nothing
        Dim I As Integer, J As Integer
        Dim IGridWidth As Integer = 0
        Try
            If DtV_TypeSettings.Rows.Count <> 0 Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
                    mQry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery"))
                    mQry = Replace(mQry.ToString.ToUpper, "`<ITEMCODE>`", "'" & Item & "'")
                    mQry = Replace(mQry.ToString.ToUpper, "`<SEARCHCODE>`", "'" & mSearchCode & "'")
                End If

                If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv")) <> "" Then
                    CSV_Qry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv"))
                End If
            End If

            If CSV_Qry <> "" Then CSV_QryArr = Split(CSV_Qry, ",")
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count = 0 Then DGL.DataSource = Nothing : DGL.Visible = False : Exit Sub

            DGL.DataSource = DtTemp
            DGL.Visible = True
            FrmObj.Controls.Add(DGL)
            DGL.Left = FrmObj.Left + 3
            DGL.Top = FrmObj.Bottom - DGL.Height - 130
            DGL.Height = 130
            DGL.Width = 450
            DGL.ColumnHeadersHeight = 40
            DGL.AllowUserToAddRows = False

            If DGL.Columns.Count > 0 Then
                If CSV_Qry <> "" Then J = CSV_QryArr.Length
                For I = 0 To DGL.ColumnCount - 1
                    If CSV_Qry <> "" Then
                        If I < J Then
                            If Val(CSV_QryArr(I)) > 0 Then
                                DGL.Columns(I).Width = Val(CSV_QryArr(I))
                            Else
                                DGL.Columns(I).Width = 100
                            End If
                        Else
                            DGL.Columns(I).Width = 100
                        End If
                    Else
                        DGL.Columns(I).Width = 100
                    End If
                    DGL.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    IGridWidth += DGL.Columns(I).Width
                Next

                DGL.ScrollBars = ScrollBars.None
                DGL.Width = IGridWidth - 50
                DGL.RowHeadersVisible = False
                DGL.EnableHeadersVisualStyles = False
                DGL.AllowUserToResizeRows = False
                DGL.ReadOnly = True
                DGL.AutoResizeRows()
                DGL.AutoResizeColumnHeadersHeight()
                DGL.BackgroundColor = Color.Cornsilk
                DGL.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                DGL.DefaultCellStyle.BackColor = Color.Cornsilk
                DGL.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                DGL.CellBorderStyle = DataGridViewCellBorderStyle.None
                DGL.Font = New Font(New FontFamily("Verdana"), 8)
                DGL.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                DGL.BringToFront()
                DGL.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Public Shared Sub FSaveAlerts(ByVal SearchCode As String, ByVal SearchField As String, ByVal SearchTable As String,
                                  ByVal AlertType As Integer, ByVal AlertMsg As String)
        Dim mQry$ = ""
        Dim mConn As New SQLiteConnection
        Dim mCmd As New SQLiteCommand

        Try
            mConn = AgL.GCn
            mCmd.Connection = mConn

            mQry = " INSERT INTO Alerts(SearchCode, SearchField, SearchTable, AlertType, AlertMsg, AlertDateTime) " &
                    " VALUES (" & AgL.Chk_Text(SearchCode) & ", " &
                    " " & AgL.Chk_Text(SearchField) & ", " &
                    " " & AgL.Chk_Text(SearchTable) & ", " &
                    " " & AlertType & ", " &
                    " " & AgL.Chk_Text(AlertMsg) & ", " &
                    " GetDate()) "
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
        Catch ex As Exception
        End Try
    End Sub

    'Public Shared Sub FPostInStockWithProcess(ByVal mSearchCode As String, ByVal Godown As String, ByVal V_Date As String, _
    '                                          ByVal Conn As SqliteConnection, _
    '                                          ByVal Cmd As SqliteCommand)
    '    Dim mQry$ = ""
    '    Dim DtTemp As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Dim mSr As Integer = 0
    '    Dim mDifferenceStock As Double = 0

    '    mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    mQry = " Select L.Item, L.FromProcess, Sum(L.Qty) As IssueQty, IfNull(Max(VStock.Stock),0) As CurrentStock " & _
    '            " From JobOrderDetail L  " & _
    '            " LEFT JOIN ( " & _
    '            "       Select S.Item, S.Process, IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As Stock " & _
    '            "       From Stock S  " & _
    '            "       Where S.Item In (Select Item From JobOrderDetail  Where DocId = '" & mSearchCode & "') " & _
    '            "       And S.Site_Code = '" & AgL.PubSiteCode & "' " & _
    '            "       And S.Godown = '" & Godown & "' " & _
    '            "       And S.V_Date <= '" & V_Date & "' " & _
    '            "       And S.DocId <> '" & mSearchCode & "'" & _
    '            "       Group By S.Item, S.Process " & _
    '            " ) As VStock ON L.Item = VStock.Item And IfNull(L.FromProcess,'') = IfNull(VStock.Process,'') " & _
    '            " Where L.DocId = '" & mSearchCode & "' " & _
    '            " Group By L.Item, L.FromProcess "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    For I = 0 To DtTemp.Rows.Count - 1
    '        If AgL.VNull(DtTemp.Rows(I)("CurrentStock")) >= AgL.VNull(DtTemp.Rows(I)("IssueQty")) Then
    '            mSr += 1
    '            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
    '                    " SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
    '                    " Remarks, Process) " & _
    '                    " Select L.DocID, " & mSr & " As Sr,Max(H.V_Type), " & _
    '                    " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " & _
    '                    " Max(H.JobWorker), L.Item, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " & _
    '                    " Sum(L.Qty * L.MeasurePerPcs), Max(L.MeasureUnit),   " & _
    '                    " Max(Remark), L.FromProcess " & _
    '                    " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " & _
    '                    " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " & _
    '                    " Where IfNull(L.Item,'') = '" & AgL.XNull(DtTemp.Rows(I)("Item")) & "' " & _
    '                    " And IfNull(L.FromProcess,'') = '" & AgL.XNull(DtTemp.Rows(I)("FromProcess")) & "' " & _
    '                    " Group By L.DocId, L.Item, L.FromProcess "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        Else
    '            If AgL.VNull(DtTemp.Rows(I)("CurrentStock")) > 0 Then
    '                mDifferenceStock = AgL.VNull(DtTemp.Rows(I)("IssueQty")) - AgL.VNull(DtTemp.Rows(I)("CurrentStock"))
    '            Else
    '                mDifferenceStock = AgL.VNull(DtTemp.Rows(I)("IssueQty"))
    '            End If

    '            If AgL.VNull(DtTemp.Rows(I)("CurrentStock")) > 0 Then
    '                mSr += 1
    '                mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
    '                        " SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
    '                        " Remarks, Process) " & _
    '                        " Select L.DocID, " & mSr & " As Sr,Max(H.V_Type), " & _
    '                        " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " & _
    '                        " Max(H.JobWorker), L.Item, Max(H.Godown), " & _
    '                        " " & AgL.VNull(DtTemp.Rows(I)("CurrentStock")) & ", Max(L.Unit), Max(L.MeasurePerPcs), " & _
    '                        " " & AgL.VNull(DtTemp.Rows(I)("CurrentStock")) & " * Max(L.MeasurePerPcs) As TotalMeasure, " & _
    '                        " Max(L.MeasureUnit),   " & _
    '                        " Max(Remark), L.FromProcess " & _
    '                        " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " & _
    '                        " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " & _
    '                        " Where IfNull(L.Item,'') = '" & AgL.XNull(DtTemp.Rows(I)("Item")) & "' " & _
    '                        " And IfNull(L.FromProcess,'') = '" & AgL.XNull(DtTemp.Rows(I)("FromProcess")) & "' " & _
    '                        " Group By L.DocId, L.Item, L.FromProcess "
    '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '            End If

    '            If mDifferenceStock > 0 Then
    '                mSr += 1
    '                mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
    '                        " SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
    '                        " Remarks, Process) " & _
    '                        " Select L.DocID, " & mSr & " As Sr,Max(H.V_Type), " & _
    '                        " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " & _
    '                        " Max(H.JobWorker), L.Item, Max(H.Godown), " & _
    '                        " " & mDifferenceStock & ", Max(L.Unit), Max(L.MeasurePerPcs), " & _
    '                        " " & mDifferenceStock & " * Max(L.MeasurePerPcs) As TotalMeasure, " & _
    '                        " Max(L.MeasureUnit),   " & _
    '                        " Max(Remark), Null " & _
    '                        " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " & _
    '                        " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " & _
    '                        " Where IfNull(L.Item,'') = '" & AgL.XNull(DtTemp.Rows(I)("Item")) & "' " & _
    '                        " And IfNull(L.FromProcess,'') = '" & AgL.XNull(DtTemp.Rows(I)("FromProcess")) & "' " & _
    '                        " Group By L.DocId, L.Item, L.FromProcess "
    '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '            End If
    '        End If
    '    Next
    'End Sub

    Public Shared Sub FPostInStockWithProcess(ByVal StockView As String,
                                              ByVal mSearchCode As String, ByVal Godown As String,
                                              ByVal V_Date As String,
                                              ByVal Conn As SQLiteConnection,
                                              ByVal Cmd As SQLiteCommand)
        Dim mQry$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim mAvailableInPrevProcessQry$ = ""
        Dim mInstertionQry$ = ""
        Dim mPendingToSaveQry$ = ""
        Dim mProcessWiseStockQry$ = ""
        Dim mAdjustedStockQry$ = ""

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mInstertionQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " SubCode, Item, Godown, Unit, MeasurePerPcs, MeasureUnit, Process, Qty_Iss, Measure_Iss) "

        'First Insertion Qry is posting stock with process
        'Which is actually posted in parent table like jobOrderDetail, SaleChallanDetail Or StockHeadDetail 
        'It will post stock upto the stock qty of that process
        'if stock of that item is not available in that process then it will go to second qry

        mAvailableInPrevProcessQry = " SELECT L.DocId, row_number() Over (Order By L.Item) As Sr, Max(L.V_Type) As V_Type, Max(L.V_Prefix) As V_Prefix, " &
                    " Max(L.V_Date) As V_Date, Max(L.V_No) As V_No, Max(L.RecId) As RecId, " &
                    " Max(L.Div_Code) As Div_Code, Max(L.Site_Code) As Site_Code, " &
                    " Max(L.SubCode) As SubCode, L.Item, Max(L.Godown) As Godown, " &
                    " Max(L.Unit) As Unit, " &
                    " Max(L.MeasurePerPcs) As MeasurePerPcs, " &
                    " Max(L.MeasureUnit) As MeasureUnit, " &
                    " L.Process, " &
                    " Case When IfNull(Sum(L.Qty),0) > IfNull(Max(VStock.CurrentStock),0) " &
                    "      Then IfNull(Max(VStock.CurrentStock),0) " &
                    "      Else IfNull(Sum(L.Qty),0) End As Qty, " &
                    " 0 As TotalMeasure " &
                    " FROM (" & StockView & ") As L " &
                    " LEFT JOIN ( " &
                    "       Select S.Item, S.Process, IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As CurrentStock " &
                    "       From Stock S  " &
                    "       Where S.Item In (Select Item From (" & StockView & ") As L Where DocId = '" & mSearchCode & "') " &
                    "       And S.Site_Code = '" & AgL.PubSiteCode & "' " &
                    "       And S.V_Date <= '" & V_Date & "' " &
                    "       Group By S.Item, S.Process " &
                    " ) As VStock ON L.Item = VStock.Item And IfNull(L.Process,'') = IfNull(VStock.Process,'') " &
                    " WHERE L.DocId = '" & mSearchCode & "' " &
                    " GROUP BY L.DocId, L.Item, L.Process " &
                    " HAVING IfNull(Max(VStock.CurrentStock),0) > 0 "
        mQry = mInstertionQry + mAvailableInPrevProcessQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'in second qry we adjust the remaining saved data in stock
        'it will issue the stock from that process which have stock of that item

        mPendingToSaveQry = " SELECT L.DocId, Max(L.V_Type) As V_Type, Max(L.V_Prefix) As V_Prefix, " &
                    " Max(L.V_Date) As V_Date, Max(L.V_No) As V_No, Max(L.RecId) As RecId, " &
                    " Max(L.Div_Code) As Div_Code, Max(L.Site_Code) As Site_Code, " &
                    " Max(L.SubCode) As SubCode, L.Item, Max(L.Godown) As Godown, " &
                    " IfNull(Sum(L.Qty),0) - IfNull(Max(VStock.StockSavedQty),0) AS PendingToSaveQty, " &
                    " Max(L.Unit) As Unit, " &
                    " Max(L.MeasurePerPcs) As MeasurePerPcs, " &
                    " (IfNull(Sum(L.Qty),0) - IfNull(Max(VStock.StockSavedQty),0)) * Max(L.MeasurePerPcs) AS Measure_Iss, " &
                    " Max(L.MeasureUnit) As MeasureUnit " &
                    " FROM (" & StockView & ") As L " &
                    " LEFT JOIN ( " &
                    " 	    SELECT L.Item, IfNull(Sum(L.Qty_Iss),0) AS StockSavedQty " &
                    " 	    FROM Stock L   " &
                    " 	    WHERE L.DocID = '" & mSearchCode & "' " &
                    " 	    GROUP BY L.Item " &
                    " ) AS VStock ON L.Item = VStock.Item " &
                    " WHERE L.DocId = '" & mSearchCode & "' " &
                    " GROUP BY L.DocId, L.Item " &
                    " HAVING IfNull(Sum(L.Qty),0) - IfNull(Max(VStock.StockSavedQty),0) > 0 "

        mProcessWiseStockQry = " SELECT L.Item, L.Process, IfNull(Max(L.Sr),0) As ProcessSr, IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) AS Qty " &
                " FROM Stock L   " &
                " LEFT JOIN Process P ON L.Process = P.NCat " &
                " WHERE L.Item IN (SELECT Item FROM (" & StockView & ") As L WHERE DocId = '" & mSearchCode & "') " &
                " And L.V_Date <= '" & V_Date & "' " &
                " And L.Site_Code = '" & AgL.PubSiteCode & "'" &
                " GROUP BY L.Item, L.Process " &
                " Having IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) > 0 "

        mSr = AgL.VNull(AgL.Dman_Execute(" Select Max(Sr) From Stock  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        mAdjustedStockQry = " Select p.DocId, " & mSr & " + row_number() Over (Order By p.Item) As Sr, p.V_Type, " &
                " p.V_Prefix, p.V_Date, p.V_No, p.RecID, p.Div_Code, p.Site_Code,  " &
                " p.SubCode, p.Item, p.Godown, P.Unit, P.MeasurePerPcs, " &
                " P.MeasureUnit, s.Process, " &
                " CASE WHEN IfNull(p.sum_order,0) - IfNull(s.sum_stock,0) + IfNull(s.Qty,0) < IfNull(s.sum_stock,0) -IfNull(p.sum_order,0) + IfNull(p.PendingToSaveQty,0) " &
                "      THEN CASE WHEN IfNull(p.PendingToSaveQty,0) <  IfNull(p.sum_order,0)  - IfNull(s.sum_stock,0)  + IfNull(s.Qty,0)  " &
                "                THEN IfNull(p.PendingToSaveQty,0)  " &
                "                ELSE CASE WHEN IfNull(s.Qty,0) < IfNull(p.sum_order,0)  - IfNull(s.sum_stock,0)  + IfNull(s.Qty,0) " &
                "                          THEN IfNull(s.Qty,0) ELSE IfNull(p.sum_order,0)  - IfNull(s.sum_stock,0)  + IfNull(s.Qty,0) END END " &
                " ELSE " &
                " CASE WHEN IfNull(p.PendingToSaveQty,0) <  IfNull(s.sum_stock,0)  - IfNull(p.sum_order,0)  + IfNull(p.PendingToSaveQty,0) " &
                "      THEN IfNull(p.PendingToSaveQty,0) ELSE  " &
                "           CASE WHEN IfNull(s.Qty,0) < IfNull(s.sum_stock,0)  - IfNull(p.sum_order,0)  + IfNull(p.PendingToSaveQty,0) " &
                "                THEN IfNull(s.Qty,0) ELSE IfNull(s.sum_stock,0)  - IfNull(p.sum_order,0)  + IfNull(p.PendingToSaveQty,0) END END END AS Qty, " &
                " 0 As MeasureIss " &
                " FROM ( " &
                "       SELECT p.*,  SUM(PendingToSaveQty) OVER(PARTITION BY Item ORDER BY Item) sum_order  " &
                "       FROM  (" & mPendingToSaveQry & ") p  " &
                "       ) As p  " &
                " LEFT JOIN ( " &
                "       SELECT s.*, SUM(Qty) OVER( PARTITION BY Item ORDER BY Process) sum_stock  " &
                "       FROM  (" & mProcessWiseStockQry & ") s  " &
                "       WHERE s.Qty > 0)  s  " &
                " ON  s.Item   = p.Item  " &
                " AND IfNull(p.sum_order,0)  > IfNull(s.sum_stock,0)  - IfNull(s.Qty,0)   " &
                " AND IfNull(s.sum_stock,0)  > IfNull(p.sum_order,0)  - IfNull(p.PendingToSaveQty,0)   " &
                " ORDER BY  p.Item, s.ProcessSr Desc "

        mQry = mInstertionQry + mAdjustedStockQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute(" Select Max(Sr) From Stock  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)


        'Finally if stock is not available any where 
        'then it will issue with null process
        mPendingToSaveQry = " SELECT L.DocId, " & mSr & " + row_number() Over (Order By L.Item) As Sr, Max(L.V_Type) As V_Type, Max(L.V_Prefix) As V_Prefix, " &
               " Max(L.V_Date) As V_Date, Max(L.V_No) As V_No, Max(L.RecId) As RecId, " &
               " Max(L.Div_Code) As Div_Code, Max(L.Site_Code) As Site_Code, " &
               " Max(L.SubCode) As SubCode, L.Item, Max(L.Godown) As Godown, " &
               " Max(L.Unit) As Unit, " &
               " Max(L.MeasurePerPcs) As MeasurePerPcs, " &
               " Max(L.MeasureUnit) As MeasureUnit, " &
               " Null As Process, " &
               " IfNull(Sum(L.Qty),0) - IfNull(Max(VStock.StockSavedQty),0) AS PendingToSaveQty, " &
               " 0 AS Measure_Iss " &
               " FROM (" & StockView & ") As L " &
               " LEFT JOIN ( " &
               " 	    SELECT L.Item, IfNull(Sum(L.Qty_Iss),0) AS StockSavedQty " &
               " 	    FROM Stock L   " &
               " 	    WHERE L.DocID = '" & mSearchCode & "' " &
               " 	    GROUP BY L.Item " &
               " ) AS VStock ON L.Item = VStock.Item " &
               " WHERE L.DocId = '" & mSearchCode & "' " &
               " GROUP BY L.DocId, L.Item " &
               " HAVING IfNull(Sum(L.Qty),0) - IfNull(Max(VStock.StockSavedQty),0) > 0 "

        mQry = mInstertionQry + mPendingToSaveQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'updating total measure value
        mQry = " UPDATE Stock " &
                " Set Measure_Iss = IfNull(Qty_Iss,0) * IfNull(MeasurePerPcs,0), " &
                " Measure_Rec = IfNull(Qty_Rec,0) * IfNull(MeasurePerPcs,0) " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'if any qty is saved with 0 then it will be deleted
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "' And IfNull(Qty_Rec,0) + IfNull(Qty_Iss,0) = 0 "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub


    Public Shared Function FIsApplyVTypePermission(ByVal mUser As String, ByVal mNCat As String)
        FIsApplyVTypePermission = AgL.Dman_Execute("SELECT (Case When Count(*) > 0 Then 1 Else 0 End) " & _
                                                   "FROM User_VType_Permission H " & _
                                                   "LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type  " & _
                                                   "WHERE H.Div_Code ='" & AgL.PubDivCode & "' " & _
                                                   "And H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                                                   "AND Vt.NCat In ('" & mNCat & "')  " & _
                                                   "AND H.UserName ='" & mUser & "'", AgL.GCn).ExecuteScalar

    End Function

    Public Shared Function FRetImportForm(ByVal OwnerWindow As Form, ByVal V_Type As String) As Form
        Dim FrmObjMDI As Object
        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""
        Dim DtMenu As DataTable = Nothing
        Dim mQry$ = ""

        Try
            mQry = " Select ImportMnuName,ImportMnuText,ImportMnuAttachedInModule " & _
                    " From Voucher_Type_Settings Where V_Type = '" & V_Type & "' " & _
                    " And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "'"
            DtMenu = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtMenu.Rows.Count > 0 Then
                StrModuleName = AgL.XNull(DtMenu.Rows(0).Item("ImportMnuAttachedInModule"))
                StrMnuName = AgL.XNull(DtMenu.Rows(0).Item("ImportMnuName"))
                StrMnuText = AgL.XNull(DtMenu.Rows(0).Item("ImportMnuText"))

                FrmObjMDI = OwnerWindow.MdiParent
                FRetImportForm = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
            Else
                MsgBox("Define Details For This Voucher Type.")
                FRetImportForm = Nothing
            End If
        Catch ex As Exception
            MsgBox("No Import Settings Found...!", MsgBoxStyle.Information)
            FRetImportForm = Nothing
        End Try
    End Function

    Public Shared Function FGetDimension1Caption() As String
        If AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")) = "" Then
            FGetDimension1Caption = "Dimension1Desc"
        Else
            FGetDimension1Caption = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1"))
        End If
    End Function

    Public Shared Function FGetDimension2Caption() As String
        If AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")) = "" Then
            FGetDimension2Caption = "Dimension2Desc"
        Else
            FGetDimension2Caption = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2"))
        End If
    End Function
End Class