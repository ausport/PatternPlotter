<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTags
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTags))
        Me.lblTagsHistory = New System.Windows.Forms.Label
        Me.lblTagsStatus = New System.Windows.Forms.Label
        Me.ContextMenuStripTags = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewTeam = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuPosOutcome = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNegOutcome = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNewDescriptor = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSaveTemplate = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuLoadTemplate = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuLockAll = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuClearAll = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuButtonMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuButtonSize = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBTN_Small = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBTN_Medium = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuBTN_Large = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuRenameButton = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRenameTextbox = New System.Windows.Forms.ToolStripTextBox
        Me.mnuEditButton = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuLockButton = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuDuplicate = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuDeleteButton = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.ContextMenuStripTags.SuspendLayout()
        Me.ContextMenuButtonMenu.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTagsHistory
        '
        Me.lblTagsHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTagsHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTagsHistory.ForeColor = System.Drawing.Color.Green
        Me.lblTagsHistory.Location = New System.Drawing.Point(8, 28)
        Me.lblTagsHistory.Name = "lblTagsHistory"
        Me.lblTagsHistory.Size = New System.Drawing.Size(511, 18)
        Me.lblTagsHistory.TabIndex = 1
        Me.lblTagsHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTagsStatus
        '
        Me.lblTagsStatus.AutoSize = True
        Me.lblTagsStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTagsStatus.Location = New System.Drawing.Point(7, 11)
        Me.lblTagsStatus.Name = "lblTagsStatus"
        Me.lblTagsStatus.Size = New System.Drawing.Size(40, 13)
        Me.lblTagsStatus.TabIndex = 0
        Me.lblTagsStatus.Text = "Status:"
        '
        'ContextMenuStripTags
        '
        Me.ContextMenuStripTags.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewTeam, Me.ToolStripSeparator1, Me.mnuPosOutcome, Me.mnuNegOutcome, Me.mnuNewDescriptor, Me.ToolStripSeparator2, Me.mnuSaveTemplate, Me.mnuLoadTemplate, Me.ToolStripSeparator3, Me.mnuLockAll, Me.ToolStripSeparator7, Me.mnuClearAll})
        Me.ContextMenuStripTags.Name = "ContextMenuStripTags"
        Me.ContextMenuStripTags.Size = New System.Drawing.Size(234, 204)
        '
        'mnuNewTeam
        '
        Me.mnuNewTeam.Image = Global.PatternPlotter4.My.Resources.Resources.Add_Folder
        Me.mnuNewTeam.Name = "mnuNewTeam"
        Me.mnuNewTeam.Size = New System.Drawing.Size(233, 22)
        Me.mnuNewTeam.Text = "New Team Name"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(230, 6)
        '
        'mnuPosOutcome
        '
        Me.mnuPosOutcome.Name = "mnuPosOutcome"
        Me.mnuPosOutcome.Size = New System.Drawing.Size(233, 22)
        Me.mnuPosOutcome.Text = "New Positive Outcome Button"
        '
        'mnuNegOutcome
        '
        Me.mnuNegOutcome.Name = "mnuNegOutcome"
        Me.mnuNegOutcome.Size = New System.Drawing.Size(233, 22)
        Me.mnuNegOutcome.Text = "New Negative Outcome Button"
        '
        'mnuNewDescriptor
        '
        Me.mnuNewDescriptor.Name = "mnuNewDescriptor"
        Me.mnuNewDescriptor.Size = New System.Drawing.Size(233, 22)
        Me.mnuNewDescriptor.Text = "New Descriptor Button"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(230, 6)
        '
        'mnuSaveTemplate
        '
        Me.mnuSaveTemplate.Image = Global.PatternPlotter4.My.Resources.Resources.Save
        Me.mnuSaveTemplate.Name = "mnuSaveTemplate"
        Me.mnuSaveTemplate.Size = New System.Drawing.Size(233, 22)
        Me.mnuSaveTemplate.Text = "Save Buttons Template"
        '
        'mnuLoadTemplate
        '
        Me.mnuLoadTemplate.Image = Global.PatternPlotter4.My.Resources.Resources.Folder_Utilities
        Me.mnuLoadTemplate.Name = "mnuLoadTemplate"
        Me.mnuLoadTemplate.Size = New System.Drawing.Size(233, 22)
        Me.mnuLoadTemplate.Text = "Load Buttons Template"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(230, 6)
        '
        'mnuLockAll
        '
        Me.mnuLockAll.Name = "mnuLockAll"
        Me.mnuLockAll.Size = New System.Drawing.Size(233, 22)
        Me.mnuLockAll.Text = "Lock All"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(230, 6)
        '
        'mnuClearAll
        '
        Me.mnuClearAll.Name = "mnuClearAll"
        Me.mnuClearAll.Size = New System.Drawing.Size(233, 22)
        Me.mnuClearAll.Text = "Clear All Items"
        '
        'ContextMenuButtonMenu
        '
        Me.ContextMenuButtonMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuButtonSize, Me.ToolStripSeparator4, Me.mnuRenameButton, Me.mnuEditButton, Me.mnuLockButton, Me.ToolStripSeparator5, Me.mnuDuplicate, Me.ToolStripSeparator6, Me.mnuDeleteButton})
        Me.ContextMenuButtonMenu.Name = "ContextMenuButtonMenu"
        Me.ContextMenuButtonMenu.ShowCheckMargin = True
        Me.ContextMenuButtonMenu.ShowImageMargin = False
        Me.ContextMenuButtonMenu.Size = New System.Drawing.Size(191, 154)
        '
        'mnuButtonSize
        '
        Me.mnuButtonSize.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBTN_Small, Me.mnuBTN_Medium, Me.mnuBTN_Large})
        Me.mnuButtonSize.Name = "mnuButtonSize"
        Me.mnuButtonSize.Size = New System.Drawing.Size(190, 22)
        Me.mnuButtonSize.Text = "Button Size"
        '
        'mnuBTN_Small
        '
        Me.mnuBTN_Small.Name = "mnuBTN_Small"
        Me.mnuBTN_Small.Size = New System.Drawing.Size(156, 22)
        Me.mnuBTN_Small.Text = "Small Button"
        '
        'mnuBTN_Medium
        '
        Me.mnuBTN_Medium.Name = "mnuBTN_Medium"
        Me.mnuBTN_Medium.Size = New System.Drawing.Size(156, 22)
        Me.mnuBTN_Medium.Text = "Medium Button"
        '
        'mnuBTN_Large
        '
        Me.mnuBTN_Large.Name = "mnuBTN_Large"
        Me.mnuBTN_Large.Size = New System.Drawing.Size(156, 22)
        Me.mnuBTN_Large.Text = "Large Button"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(187, 6)
        '
        'mnuRenameButton
        '
        Me.mnuRenameButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRenameTextbox})
        Me.mnuRenameButton.Name = "mnuRenameButton"
        Me.mnuRenameButton.Size = New System.Drawing.Size(190, 22)
        Me.mnuRenameButton.Text = "Rename Button"
        '
        'mnuRenameTextbox
        '
        Me.mnuRenameTextbox.Name = "mnuRenameTextbox"
        Me.mnuRenameTextbox.Size = New System.Drawing.Size(100, 21)
        '
        'mnuEditButton
        '
        Me.mnuEditButton.Name = "mnuEditButton"
        Me.mnuEditButton.Size = New System.Drawing.Size(190, 22)
        Me.mnuEditButton.Text = "Edit Button Properties"
        '
        'mnuLockButton
        '
        Me.mnuLockButton.Name = "mnuLockButton"
        Me.mnuLockButton.Size = New System.Drawing.Size(190, 22)
        Me.mnuLockButton.Text = "Lock Button"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(187, 6)
        '
        'mnuDuplicate
        '
        Me.mnuDuplicate.Name = "mnuDuplicate"
        Me.mnuDuplicate.Size = New System.Drawing.Size(190, 22)
        Me.mnuDuplicate.Text = "Duplicate Button"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(187, 6)
        '
        'mnuDeleteButton
        '
        Me.mnuDeleteButton.Name = "mnuDeleteButton"
        Me.mnuDeleteButton.Size = New System.Drawing.Size(190, 22)
        Me.mnuDeleteButton.Text = "Delete Button"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTagsHistory)
        Me.GroupBox1.Controls.Add(Me.lblTagsStatus)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(525, 53)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'frmTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 352)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTags"
        Me.Text = "New Tags Window"
        Me.TopMost = True
        Me.ContextMenuStripTags.ResumeLayout(False)
        Me.ContextMenuButtonMenu.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTagsHistory As System.Windows.Forms.Label
    Friend WithEvents lblTagsStatus As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStripTags As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewTeam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNegOutcome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewDescriptor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPosOutcome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSaveTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuButtonMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuButtonSize As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBTN_Small As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBTN_Medium As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBTN_Large As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRenameButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDeleteButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRenameTextbox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents mnuLockButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLoadTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLockAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mnuDuplicate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuClearAll As System.Windows.Forms.ToolStripMenuItem
End Class
