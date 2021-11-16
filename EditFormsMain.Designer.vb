<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditFormsMain
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditFormsMain))
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.lblStep1 = New System.Windows.Forms.Label()
        Me.lblStep2 = New System.Windows.Forms.Label()
        Me.ctlReturnTypes = New System.Windows.Forms.CheckedListBox()
        Me.btnSlip = New System.Windows.Forms.Button()
        Me.btnSummary = New System.Windows.Forms.Button()
        Me.lblForm = New System.Windows.Forms.Label()
        Me.PanelStep1 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelStep1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStep1
        '
        Me.lblStep1.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblStep1, "lblStep1")
        Me.lblStep1.ForeColor = System.Drawing.Color.White
        Me.lblStep1.Name = "lblStep1"
        Me.HelpProvider1.SetShowHelp(Me.lblStep1, CType(resources.GetObject("lblStep1.ShowHelp"), Boolean))
        '
        'lblStep2
        '
        Me.lblStep2.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.lblStep2, "lblStep2")
        Me.lblStep2.ForeColor = System.Drawing.Color.White
        Me.lblStep2.Name = "lblStep2"
        Me.HelpProvider1.SetShowHelp(Me.lblStep2, CType(resources.GetObject("lblStep2.ShowHelp"), Boolean))
        '
        'ctlReturnTypes
        '
        Me.ctlReturnTypes.CheckOnClick = True
        resources.ApplyResources(Me.ctlReturnTypes, "ctlReturnTypes")
        Me.ctlReturnTypes.FormattingEnabled = True
        Me.ctlReturnTypes.Items.AddRange(New Object() {resources.GetString("ctlReturnTypes.Items"), resources.GetString("ctlReturnTypes.Items1"), resources.GetString("ctlReturnTypes.Items2"), resources.GetString("ctlReturnTypes.Items3")})
        Me.ctlReturnTypes.MultiColumn = True
        Me.ctlReturnTypes.Name = "ctlReturnTypes"
        Me.HelpProvider1.SetShowHelp(Me.ctlReturnTypes, CType(resources.GetObject("ctlReturnTypes.ShowHelp"), Boolean))
        Me.ToolTip1.SetToolTip(Me.ctlReturnTypes, resources.GetString("ctlReturnTypes.ToolTip"))
        '
        'btnSlip
        '
        Me.btnSlip.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.btnSlip, "btnSlip")
        Me.btnSlip.Name = "btnSlip"
        Me.HelpProvider1.SetShowHelp(Me.btnSlip, CType(resources.GetObject("btnSlip.ShowHelp"), Boolean))
        Me.btnSlip.UseVisualStyleBackColor = True
        '
        'btnSummary
        '
        Me.btnSummary.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.btnSummary, "btnSummary")
        Me.btnSummary.Name = "btnSummary"
        Me.HelpProvider1.SetShowHelp(Me.btnSummary, CType(resources.GetObject("btnSummary.ShowHelp"), Boolean))
        Me.btnSummary.UseVisualStyleBackColor = True
        '
        'lblForm
        '
        resources.ApplyResources(Me.lblForm, "lblForm")
        Me.lblForm.Name = "lblForm"
        Me.HelpProvider1.SetShowHelp(Me.lblForm, CType(resources.GetObject("lblForm.ShowHelp"), Boolean))
        '
        'PanelStep1
        '
        resources.ApplyResources(Me.PanelStep1, "PanelStep1")
        Me.PanelStep1.Controls.Add(Me.btnCancel)
        Me.PanelStep1.Controls.Add(Me.lblForm)
        Me.PanelStep1.Controls.Add(Me.btnSummary)
        Me.PanelStep1.Controls.Add(Me.btnSlip)
        Me.PanelStep1.Controls.Add(Me.ctlReturnTypes)
        Me.PanelStep1.Controls.Add(Me.lblStep2)
        Me.PanelStep1.Controls.Add(Me.lblStep1)
        Me.PanelStep1.Name = "PanelStep1"
        Me.HelpProvider1.SetShowHelp(Me.PanelStep1, CType(resources.GetObject("PanelStep1.ShowHelp"), Boolean))
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        Me.HelpProvider1.SetShowHelp(Me.btnCancel, CType(resources.GetObject("btnCancel.ShowHelp"), Boolean))
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'EditFormsMain
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.PanelStep1)
        Me.Name = "EditFormsMain"
        Me.HelpProvider1.SetShowHelp(Me, CType(resources.GetObject("$this.ShowHelp"), Boolean))
        Me.PanelStep1.ResumeLayout(False)
        Me.PanelStep1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    'SS1241
    'SS1241
    'SS1241
    'SS1241
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents lblStep1 As System.Windows.Forms.Label
    Friend WithEvents lblStep2 As System.Windows.Forms.Label
    Friend WithEvents ctlReturnTypes As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnSlip As System.Windows.Forms.Button
    Friend WithEvents btnSummary As System.Windows.Forms.Button
    Friend WithEvents lblForm As System.Windows.Forms.Label
    Friend WithEvents PanelStep1 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
