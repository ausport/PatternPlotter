<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdvancedSearch
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.chkAND1 = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DropDown1 = New System.Windows.Forms.ComboBox
        Me.chkCriteria1 = New System.Windows.Forms.CheckedListBox
        Me.lblPreviewSQL = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkAND2 = New System.Windows.Forms.CheckBox
        Me.DropDown2 = New System.Windows.Forms.ComboBox
        Me.chkCriteria2 = New System.Windows.Forms.CheckedListBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkAND3 = New System.Windows.Forms.CheckBox
        Me.DropDown3 = New System.Windows.Forms.ComboBox
        Me.chkCriteria3 = New System.Windows.Forms.CheckedListBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkAND4 = New System.Windows.Forms.CheckBox
        Me.DropDown4 = New System.Windows.Forms.ComboBox
        Me.chkCriteria4 = New System.Windows.Forms.CheckedListBox
        Me.AnalysisMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuSelectAll = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSelectNone = New System.Windows.Forms.ToolStripMenuItem
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.AnalysisMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(814, 298)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(79, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(6, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Close"
        '
        'chkAND1
        '
        Me.chkAND1.AutoSize = True
        Me.chkAND1.Location = New System.Drawing.Point(6, 254)
        Me.chkAND1.Name = "chkAND1"
        Me.chkAND1.Size = New System.Drawing.Size(198, 17)
        Me.chkAND1.TabIndex = 11
        Me.chkAND1.Text = "Select combinations (Boolean AND):"
        Me.chkAND1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DropDown1)
        Me.GroupBox1.Controls.Add(Me.chkCriteria1)
        Me.GroupBox1.Controls.Add(Me.chkAND1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 280)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Criteria"
        '
        'DropDown1
        '
        Me.DropDown1.Enabled = False
        Me.DropDown1.FormattingEnabled = True
        Me.DropDown1.Items.AddRange(New Object() {"Game ID"})
        Me.DropDown1.Location = New System.Drawing.Point(7, 20)
        Me.DropDown1.Name = "DropDown1"
        Me.DropDown1.Size = New System.Drawing.Size(204, 21)
        Me.DropDown1.TabIndex = 14
        Me.DropDown1.Text = "Game ID"
        '
        'chkCriteria1
        '
        Me.chkCriteria1.FormattingEnabled = True
        Me.chkCriteria1.Location = New System.Drawing.Point(6, 49)
        Me.chkCriteria1.Name = "chkCriteria1"
        Me.chkCriteria1.Size = New System.Drawing.Size(205, 199)
        Me.chkCriteria1.TabIndex = 13
        '
        'lblPreviewSQL
        '
        Me.lblPreviewSQL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreviewSQL.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblPreviewSQL.Location = New System.Drawing.Point(12, 295)
        Me.lblPreviewSQL.Name = "lblPreviewSQL"
        Me.lblPreviewSQL.Size = New System.Drawing.Size(791, 34)
        Me.lblPreviewSQL.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkAND2)
        Me.GroupBox2.Controls.Add(Me.DropDown2)
        Me.GroupBox2.Controls.Add(Me.chkCriteria2)
        Me.GroupBox2.Location = New System.Drawing.Point(235, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(217, 280)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search Criteria"
        '
        'chkAND2
        '
        Me.chkAND2.AutoSize = True
        Me.chkAND2.Location = New System.Drawing.Point(7, 254)
        Me.chkAND2.Name = "chkAND2"
        Me.chkAND2.Size = New System.Drawing.Size(198, 17)
        Me.chkAND2.TabIndex = 15
        Me.chkAND2.Text = "Select combinations (Boolean AND):"
        Me.chkAND2.UseVisualStyleBackColor = True
        '
        'DropDown2
        '
        Me.DropDown2.FormattingEnabled = True
        Me.DropDown2.Items.AddRange(New Object() {"None", "Game ID", "Team Name", "Time Criteria", "Region", "Event Name", "Game Author", "Game Venue", "Game Opponent"})
        Me.DropDown2.Location = New System.Drawing.Point(7, 20)
        Me.DropDown2.Name = "DropDown2"
        Me.DropDown2.Size = New System.Drawing.Size(204, 21)
        Me.DropDown2.TabIndex = 14
        Me.DropDown2.Text = "None"
        '
        'chkCriteria2
        '
        Me.chkCriteria2.Enabled = False
        Me.chkCriteria2.FormattingEnabled = True
        Me.chkCriteria2.Location = New System.Drawing.Point(6, 49)
        Me.chkCriteria2.Name = "chkCriteria2"
        Me.chkCriteria2.Size = New System.Drawing.Size(205, 199)
        Me.chkCriteria2.TabIndex = 13
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkAND3)
        Me.GroupBox3.Controls.Add(Me.DropDown3)
        Me.GroupBox3.Controls.Add(Me.chkCriteria3)
        Me.GroupBox3.Location = New System.Drawing.Point(457, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(217, 280)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Search Criteria"
        '
        'chkAND3
        '
        Me.chkAND3.AutoSize = True
        Me.chkAND3.Location = New System.Drawing.Point(6, 254)
        Me.chkAND3.Name = "chkAND3"
        Me.chkAND3.Size = New System.Drawing.Size(198, 17)
        Me.chkAND3.TabIndex = 15
        Me.chkAND3.Text = "Select combinations (Boolean AND):"
        Me.chkAND3.UseVisualStyleBackColor = True
        '
        'DropDown3
        '
        Me.DropDown3.FormattingEnabled = True
        Me.DropDown3.Items.AddRange(New Object() {"None", "Game ID", "Team Name", "Time Criteria", "Region", "Event Name", "Game Author", "Game Venue", "Game Opponent"})
        Me.DropDown3.Location = New System.Drawing.Point(7, 20)
        Me.DropDown3.Name = "DropDown3"
        Me.DropDown3.Size = New System.Drawing.Size(204, 21)
        Me.DropDown3.TabIndex = 14
        Me.DropDown3.Text = "None"
        '
        'chkCriteria3
        '
        Me.chkCriteria3.Enabled = False
        Me.chkCriteria3.FormattingEnabled = True
        Me.chkCriteria3.Location = New System.Drawing.Point(6, 49)
        Me.chkCriteria3.Name = "chkCriteria3"
        Me.chkCriteria3.Size = New System.Drawing.Size(205, 199)
        Me.chkCriteria3.TabIndex = 13
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chkAND4)
        Me.GroupBox4.Controls.Add(Me.DropDown4)
        Me.GroupBox4.Controls.Add(Me.chkCriteria4)
        Me.GroupBox4.Location = New System.Drawing.Point(680, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(217, 280)
        Me.GroupBox4.TabIndex = 17
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Search Criteria"
        '
        'chkAND4
        '
        Me.chkAND4.AutoSize = True
        Me.chkAND4.Location = New System.Drawing.Point(6, 254)
        Me.chkAND4.Name = "chkAND4"
        Me.chkAND4.Size = New System.Drawing.Size(198, 17)
        Me.chkAND4.TabIndex = 15
        Me.chkAND4.Text = "Select combinations (Boolean AND):"
        Me.chkAND4.UseVisualStyleBackColor = True
        '
        'DropDown4
        '
        Me.DropDown4.FormattingEnabled = True
        Me.DropDown4.Items.AddRange(New Object() {"None", "Game ID", "Team Name", "Time Criteria", "Region", "Event Name", "Game Author", "Game Venue", "Game Opponent"})
        Me.DropDown4.Location = New System.Drawing.Point(7, 20)
        Me.DropDown4.Name = "DropDown4"
        Me.DropDown4.Size = New System.Drawing.Size(204, 21)
        Me.DropDown4.TabIndex = 14
        Me.DropDown4.Text = "None"
        '
        'chkCriteria4
        '
        Me.chkCriteria4.Enabled = False
        Me.chkCriteria4.FormattingEnabled = True
        Me.chkCriteria4.Location = New System.Drawing.Point(6, 49)
        Me.chkCriteria4.Name = "chkCriteria4"
        Me.chkCriteria4.Size = New System.Drawing.Size(205, 199)
        Me.chkCriteria4.TabIndex = 13
        '
        'AnalysisMenu
        '
        Me.AnalysisMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSelectAll, Me.mnuSelectNone})
        Me.AnalysisMenu.Name = "AnalysisMenu"
        Me.AnalysisMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.AnalysisMenu.ShowImageMargin = False
        Me.AnalysisMenu.Size = New System.Drawing.Size(118, 48)
        '
        'mnuSelectAll
        '
        Me.mnuSelectAll.Name = "mnuSelectAll"
        Me.mnuSelectAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuSelectAll.Text = "Select All"
        '
        'mnuSelectNone
        '
        Me.mnuSelectNone.Name = "mnuSelectNone"
        Me.mnuSelectNone.Size = New System.Drawing.Size(117, 22)
        Me.mnuSelectNone.Text = "Select None"
        '
        'frmAdvancedSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 339)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblPreviewSQL)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdvancedSearch"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Advanced Search"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.AnalysisMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents chkAND1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DropDown1 As System.Windows.Forms.ComboBox
    Friend WithEvents chkCriteria1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblPreviewSQL As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DropDown2 As System.Windows.Forms.ComboBox
    Friend WithEvents chkCriteria2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DropDown3 As System.Windows.Forms.ComboBox
    Friend WithEvents chkCriteria3 As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents DropDown4 As System.Windows.Forms.ComboBox
    Friend WithEvents chkCriteria4 As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkAND2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkAND3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkAND4 As System.Windows.Forms.CheckBox
    Friend WithEvents AnalysisMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectNone As System.Windows.Forms.ToolStripMenuItem

End Class
