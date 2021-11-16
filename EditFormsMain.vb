#Region "Source File Header"

'FileName:      EditFormsMain.vb 
'
'Class:         EditFormsMain
'
'Description:   UI for central forms editing. User-selection of companies, return types and options.
'
'Author(s):     Peter Rhebergen - PR0525 (based on Jim McDonald's "XMLDialog")
'
'Copyright (c) ELM Computer Systems Inc.  All rights reserved.

#End Region

Option Explicit On
Option Strict Off

#Region "Imports"

Imports BTO = BT.BTObjects
Imports CG = Common.Globals
Imports CG2 = Common.Globals2
Imports CG3 = Common.Globals3
Imports CREM = Common.RemoteClass
Imports CRC = Common.RatesConstants
Imports CC = Common.LanguageConstants
Imports DAI = DT.DataAccessInterface
Imports SWF = System.Windows.Forms
Imports WWD = WeifenLuo.WinFormsUI.Docking
Imports System.Runtime.InteropServices
Imports System.Security.Permissions
Imports System.Drawing 'SS1241
Imports SREF = System.Reflection
#End Region


Public Class EditFormsMain
    Inherits WWD.DockContent
    Public Event CloseMe(ByVal frm As Form)
    Public Event CloseAllButThis(ByVal frm As Form)


#Region "Internal Variables"
    Public IsRLReturn As Boolean
    'Public IsPart18 As Boolean = False 'JM2086
    'Public IsPart19 As Boolean = False 'JM2086
    Public IsT5013 As Boolean = False 'PR0270c
    Public IsT5013FIN As Boolean = False 'SS1836
    Public LockTime As DateTime    ' current time for Company.LockedWhen field  JM0569
    Private SlipsToProcessTotal As Long     'JM0613
    Private ValidationReportOnly As Boolean 'JM0870

    'JM1517 - to display friendly or internal name of return type, e.g. RRSP is REER in French
    Private Structure ReturnNames
        Private _FriendlyName As String
        Private _InternalName As String
        Friend Sub New(f, i)
            FriendlyName = f
            InternalName = i
        End Sub
        Friend Property FriendlyName As String
            Get
                Return _FriendlyName
            End Get
            Set(value As String)
                _FriendlyName = value
            End Set
        End Property
        Friend Property InternalName As String
            Get
                Return _InternalName
            End Get
            Set(value As String)
                _InternalName = value
            End Set
        End Property
        Overrides Function ToString() As String
            Return _FriendlyName
        End Function
    End Structure
#End Region

#Region "Methods"

    ' Handle Validation Options controls, i.e. # of warnings and errors to show
    Private Sub ConfigOptions()

    End Sub

    Public Sub ConfigureControls(ByVal isProcessing As Boolean)

        Me.btnSlip.Visible = False
        Me.btnSummary.Visible = False
        Me.btnCancel.Visible = True

        If Me.ctlReturnTypes.CheckedItems.Count > 0 Then
            Me.btnSummary.Visible = True
            Me.btnSlip.Visible = True
        End If

    End Sub


    ' Save all user settings and options for this form type
    Private Sub SaveUserSettings()
    End Sub


    ' This is called from outside to load the form and set some properties
    Public Sub Startup(ByVal returnType As String)

        'JM1517
        Me.ctlReturnTypes.DisplayMember = "FriendlyName"
        Me.ctlReturnTypes.ValueMember = "InternalName"


        'JM1049
        Call CG2.SetControlsAppearance(Me)
        'Different color for "Next" buttons
        Dim NextButtonBackColor As String = "#7ac143"
        Dim NextButtonForeColor As String = "#000000"

        Try

            For Each RtnType As String In CG.MMTypes    'JM0526
                Dim Rtn As New ReturnNames
                Rtn.FriendlyName = CG.FriendlyReturnName(RtnType)
                Rtn.InternalName = RtnType
                Me.ctlReturnTypes.Items.Add(Rtn)

                If RtnType = "T5013" Then  'SS1836
                    Dim Rtn2 As New ReturnNames
                    Rtn2.FriendlyName = CG.FriendlyReturnName("T5013FIN")
                    Rtn2.InternalName = "T5013FIN"
                End If

            Next


            'JM2086 - Add Fixed status to dropdown if Part1819 is enabled and present in this database
            If CG.IsValidCustomFormCode("PART1819", CG2.GetSystemSettingsIniSetting("Custom Pdfs", "Code1819")) AndAlso _
                (CG.DoesTableExist("Part18", "") OrElse CG.DoesTableExist("Part19", "")) Then
            End If

            Me.ctlReturnTypes.Items.Clear()

            For Each RtnType As String In CG.ReturnTypes
                Dim Rtn As New ReturnNames
                Rtn.FriendlyName = CG.FriendlyReturnName(RtnType)
                Rtn.InternalName = RtnType
                Me.ctlReturnTypes.Items.Add(Rtn)

                If RtnType = "T5013" Then  'SS1836
                    Dim Rtn2 As New ReturnNames
                    Rtn2.FriendlyName = CG.FriendlyReturnName("T5013FIN")
                    Rtn2.InternalName = "T5013FIN"
                End If

            Next

            ' Load user-selected defaults for options. Set defaults if no user setting yet
            If returnType.ToLower.Contains("valid") Then         ' JM0589 - bilingual
                'Me.btnPrepare.Enabled = False
            End If

            Me.ConfigureControls(False)

        Catch ex As Exception

            CREM.ShowErrorMessage(ex)

        End Try

    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Close the form
    ''' </summary>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.SaveUserSettings()
        Me.Close()

    End Sub

    Private Sub CloseAllButOne(ByVal frm As Form)
        PT_Common.MDIParentLink.MDIPARENT.CloseAllForms("B", frm)
    End Sub

    Private Sub CloseOne(ByVal frm As Form)
        PT_Common.MDIParentLink.MDIPARENT.CloseAllForms("1", frm)
    End Sub


    Private Sub chkShowXml_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ConfigOptions()
    End Sub

    ''' <summary>
    ''' control multi-selection or single selection for the returntypes 
    ''' </summary>
    Private Sub ctlReturnTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctlReturnTypes.SelectedIndexChanged

redo:
        For i As Integer = 0 To ctlReturnTypes.CheckedIndices.Count - 1
            ctlReturnTypes.SetItemChecked(ctlReturnTypes.CheckedIndices.Item(i), False)
            GoTo redo
        Next
        If ctlReturnTypes.SelectedIndex <> -1 Then ctlReturnTypes.SetItemChecked(ctlReturnTypes.SelectedIndex, True) 'SS1576 - added If ctlReturnTypes.SelectedIndex <> -1 Then
        ConfigureControls(True)

    End Sub

    Private Sub EditFormsMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        ' If this form needs to close when the Step 1 or Step 2 user controls are visible, execute
        ' the appropriate Cancel button procedure for the User control and return to the main form without closing it.  'JM0822

        If CG.NoKillMe Then 'SS1370
            e.Cancel = True
            Debug.Print("Sorry, I can't stop now ...")
            Exit Sub
        End If

    End Sub

    Private Sub EditFormsMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        CG.SetContextHelp(Me, Me.HelpProvider1)

        Dim x As New Windows.Forms.ContextMenu
        x.MenuItems.Add(CC.TextClose, New System.EventHandler(AddressOf CloseMeX))
        x.MenuItems.Add(CG.EF(CC.CloseAllButThis, CC.FR_CloseAllButThis), New System.EventHandler(AddressOf CloseAllButThisX))
        Me.TabPageContextMenu = x

        ConfigOptions()

    End Sub

    Private Sub txtWarnings_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " ", Chr(8)
            Case Else
                e.KeyChar = Chr(0)
        End Select
    End Sub

#End Region

#Region "CloseMe"
    Private Sub CloseMeX(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent CloseMe(Me)
    End Sub

    Private Sub CloseAllButThisX(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent CloseAllButThis(Me)
    End Sub

#End Region

#Region "Forms Menu Event Handlers"

    ''' <summary>
    ''' Edit Summaries
    ''' </summary>
    ''' <remarks>Menu item text must be formatted correctly for this to work, e.g. "T4 Edit List"</remarks>
    Public Sub EditSummaries(ByVal sender As Object, ByVal e As EventArgs) Handles btnSummary.Click

        If ctlReturnTypes.SelectedIndex < 0 Then
            CREM.ShowMessageBox(CG.EF("You must select a slip type before you can prepare this report", "Les sociétés suivantes ont été retirées de la liste de sélection."))
            Exit Sub
        End If

        'CUSTOM_CASE 

        BT.UsageInformation.CollectUsageInfo(SREF.MethodInfo.GetCurrentMethod, sender.ToString)  'JM1443

        Dim REturnType As String = ctlReturnTypes.SelectedItem.ToString

        REturnType = REturnType.Replace("RL-", "R")      'Correct the returnType for RL slips and summaries
        ReturnType = ReturnType.Replace("-", "")         'Correct the returnType for T4A-NR, etc
        ReturnType = ReturnType.Replace("(", "")         'Correct the returnType for T4(P)
        ReturnType = ReturnType.Replace(")", "")         'Correct the returnType for T4(P)
        ReturnType = ReturnType.Replace("REER", "RRSP")         'Correct the returnType for REER (French RRSP)  'JM0893
        ReturnType = ReturnType.Replace("CELI", "TFSA")         'Correct the returnType for CELI (French TFSA)  'JM0893
        'ReturnType = ReturnType.Replace("TP64.3V", "TP64")         'TC0199 'TC0214

        If ReturnType.StartsWith("TP64") Then 'TC0214
            ReturnType = "TP64"
        End If

        ReturnType = ReturnType & "SUM"

        PT.MDIParent.OpenDataEntryForm(ReturnType)

    End Sub

    ''' <summary>
    ''' Edit slips
    ''' </summary>
    ''' <remarks>Menu item text must be formatted correctly for this to work, e.g. "T4 Edit List"</remarks>
    Private Sub EditSlips(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlip.Click

        If ctlReturnTypes.SelectedIndex < 0 Then
            CREM.ShowMessageBox(CG.EF("You must select a slip type before you can prepare this report", "Les sociétés suivantes ont été retirées de la liste de sélection."))
            Exit Sub
        End If

        'CUSTOMCASE_PRINT

        BT.UsageInformation.CollectUsageInfo(SREF.MethodInfo.GetCurrentMethod, sender.ToString)  'JM1443

        Dim ReturnType As String = ctlReturnTypes.SelectedItem.ToString

        ReturnType = ReturnType.Replace("RL-", "R")             'Correct the returnType for RL slips and summaries
        ReturnType = ReturnType.Replace("-", "")                'Correct the returnType for T4A-NR, etc
        ReturnType = ReturnType.Replace("(", "")                'Correct the returnType for T4A(P)
        ReturnType = ReturnType.Replace(")", "")                'Correct the returnType for T4A(P)

        If ReturnType.StartsWith("TP64") Then 'TC0214
            ReturnType = "TP64"
        End If

        PT.MDIParent.OpenDataEntryForm(ReturnType)

    End Sub

#End Region

End Class
