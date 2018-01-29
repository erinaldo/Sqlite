Public Class MDIMain
    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AgL Is Nothing Then
            If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                'If FOpenIni(StrPath + IniName, "Ashish", "hnu12345") Then
                AgIniVar.FOpenConnection("4", "1", False)
            End If


            AgL.PubDivCode = "R"
            AgL.PubDivName = AgL.Dman_Execute("Select D.Div_Name From Division D Where D.Div_Code = '" & AgL.PubDivCode & "' ", AgL.GCn).ExecuteScalar
            ReportPath = AgL.PubReportPath & "\" & ClsMain.ModuleName

            Dim ClsObj As New ClsMain(AgL)
            Dim ClsObjTemplate As New AgTemplate.ClsMain(AgL)
            Dim ClsObjStructure As New AgStructure.ClsMain(AgL)


            'ClsObj.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObjStructure.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObjTemplate.UpdateTableStructurePurchase(AgL.PubMdlTable)
            'ClsObjTemplate.UpdateTableStructureFA(AgL.PubMdlTable)
            'ClsObjTemplate.UpdateTableStructureSales(AgL.PubMdlTable)
            'ClsObjTemplate.UpdateTableStructure(AgL.PubMdlTable)
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)


            'ClsObjTemplate.UpdateTableInitialiser()
            'ClsObjStructure.UpdateTableInitialiser()
            'ClsObj.UpdateTableInitialiser()
            'ClsObj = Nothing

            Call IniDtEnviro()

            'MnuDimension1Master.Text = AgTemplate.ClsMain.FGetDimension1Caption() + " Master"
            'MnuDimension2Master.Text = AgTemplate.ClsMain.FGetDimension2Caption() + " Master"
        End If
    End Sub


    Private Sub Mnu_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles _
                                    MnuStoreMaster.DropDownItemClicked, MnuStoreTransactions.DropDownItemClicked, MnuStoreReports.DropDownItemClicked

        Dim FrmObj As Form = Nothing
        Dim CFOpen As New ClsFunction
        Dim bIsEntryPoint As Boolean

        If e.ClickedItem.Tag Is Nothing Then e.ClickedItem.Tag = ""
        If e.ClickedItem.Tag.Trim = "" Then
            bIsEntryPoint = True
        Else
            bIsEntryPoint = False
        End If

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, bIsEntryPoint)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

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

    Private Sub MnuStockInHand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuStockInHand.Click

    End Sub
End Class