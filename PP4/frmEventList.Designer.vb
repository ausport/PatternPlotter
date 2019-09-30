<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEventList
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEventList))
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.Bookmark = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.gridPathcount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridCriteria = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridTeamName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridStatus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridOutcome = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridRegion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.gridVideoTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuEventListOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditRows = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRelinkVideo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuChangeTimeCriteria = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.NudgeVideoTimesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExportEDL = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuExportVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuShowAll = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuEventListOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.AllowUserToResizeColumns = False
        Me.dataGrid.AllowUserToResizeRows = False
        Me.dataGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader
        Me.dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Bookmark, Me.gridPathcount, Me.gridTime, Me.gridCriteria, Me.gridTeamName, Me.gridStatus, Me.gridOutcome, Me.gridType, Me.gridRegion, Me.gridID, Me.gridVideoTC})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dataGrid.DefaultCellStyle = DataGridViewCellStyle1
        Me.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dataGrid.Location = New System.Drawing.Point(12, 12)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.ReadOnly = True
        Me.dataGrid.RowHeadersVisible = False
        Me.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGrid.Size = New System.Drawing.Size(611, 469)
        Me.dataGrid.TabIndex = 0
        '
        'Bookmark
        '
        Me.Bookmark.HeaderText = "Select"
        Me.Bookmark.Name = "Bookmark"
        Me.Bookmark.ReadOnly = True
        Me.Bookmark.Width = 5
        '
        'gridPathcount
        '
        Me.gridPathcount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridPathcount.HeaderText = "ID"
        Me.gridPathcount.Name = "gridPathcount"
        Me.gridPathcount.ReadOnly = True
        Me.gridPathcount.Width = 43
        '
        'gridTime
        '
        Me.gridTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridTime.HeaderText = "Time"
        Me.gridTime.Name = "gridTime"
        Me.gridTime.ReadOnly = True
        Me.gridTime.Width = 55
        '
        'gridCriteria
        '
        Me.gridCriteria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridCriteria.HeaderText = "Criteria"
        Me.gridCriteria.Name = "gridCriteria"
        Me.gridCriteria.ReadOnly = True
        Me.gridCriteria.Width = 64
        '
        'gridTeamName
        '
        Me.gridTeamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridTeamName.HeaderText = "Team"
        Me.gridTeamName.Name = "gridTeamName"
        Me.gridTeamName.ReadOnly = True
        Me.gridTeamName.Width = 59
        '
        'gridStatus
        '
        Me.gridStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridStatus.HeaderText = "Status"
        Me.gridStatus.Name = "gridStatus"
        Me.gridStatus.ReadOnly = True
        Me.gridStatus.Width = 62
        '
        'gridOutcome
        '
        Me.gridOutcome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridOutcome.HeaderText = "Outcome"
        Me.gridOutcome.Name = "gridOutcome"
        Me.gridOutcome.ReadOnly = True
        Me.gridOutcome.Width = 75
        '
        'gridType
        '
        Me.gridType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridType.HeaderText = "Type"
        Me.gridType.Name = "gridType"
        Me.gridType.ReadOnly = True
        Me.gridType.Width = 56
        '
        'gridRegion
        '
        Me.gridRegion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.gridRegion.HeaderText = "Region"
        Me.gridRegion.Name = "gridRegion"
        Me.gridRegion.ReadOnly = True
        Me.gridRegion.Visible = False
        '
        'gridID
        '
        Me.gridID.HeaderText = "ID"
        Me.gridID.Name = "gridID"
        Me.gridID.ReadOnly = True
        Me.gridID.Visible = False
        Me.gridID.Width = 5
        '
        'gridVideoTC
        '
        Me.gridVideoTC.HeaderText = "VideoTC"
        Me.gridVideoTC.Name = "gridVideoTC"
        Me.gridVideoTC.ReadOnly = True
        Me.gridVideoTC.Visible = False
        Me.gridVideoTC.Width = 5
        '
        'mnuEventListOptions
        '
        Me.mnuEventListOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditRows, Me.mnuRelinkVideo, Me.mnuChangeTimeCriteria, Me.ToolStripSeparator2, Me.NudgeVideoTimesToolStripMenuItem, Me.ToolStripSeparator1, Me.mnuExportEDL, Me.mnuExportVPL, Me.ToolStripSeparator5, Me.mnuShowAll})
        Me.mnuEventListOptions.Name = "mnuEventListOptions"
        Me.mnuEventListOptions.ShowCheckMargin = True
        Me.mnuEventListOptions.ShowImageMargin = False
        Me.mnuEventListOptions.Size = New System.Drawing.Size(253, 176)
        '
        'mnuEditRows
        '
        Me.mnuEditRows.Name = "mnuEditRows"
        Me.mnuEditRows.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuEditRows.Size = New System.Drawing.Size(252, 22)
        Me.mnuEditRows.Text = "Edit Row(s)"
        '
        'mnuRelinkVideo
        '
        Me.mnuRelinkVideo.Name = "mnuRelinkVideo"
        Me.mnuRelinkVideo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.mnuRelinkVideo.Size = New System.Drawing.Size(252, 22)
        Me.mnuRelinkVideo.Text = "Re-Link to Video Source"
        '
        'mnuChangeTimeCriteria
        '
        Me.mnuChangeTimeCriteria.Name = "mnuChangeTimeCriteria"
        Me.mnuChangeTimeCriteria.Size = New System.Drawing.Size(252, 22)
        Me.mnuChangeTimeCriteria.Text = "Change Time Criteria"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(249, 6)
        '
        'NudgeVideoTimesToolStripMenuItem
        '
        Me.NudgeVideoTimesToolStripMenuItem.Name = "NudgeVideoTimesToolStripMenuItem"
        Me.NudgeVideoTimesToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.NudgeVideoTimesToolStripMenuItem.Text = "Shift Timeline Points"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(249, 6)
        '
        'mnuExportEDL
        '
        Me.mnuExportEDL.Name = "mnuExportEDL"
        Me.mnuExportEDL.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuExportEDL.Size = New System.Drawing.Size(252, 22)
        Me.mnuExportEDL.Text = "Export Time Criteria to EDL"
        '
        'mnuExportVPL
        '
        Me.mnuExportVPL.Name = "mnuExportVPL"
        Me.mnuExportVPL.Size = New System.Drawing.Size(252, 22)
        Me.mnuExportVPL.Text = "Export Time Criteria to VPL"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(249, 6)
        '
        'mnuShowAll
        '
        Me.mnuShowAll.CheckOnClick = True
        Me.mnuShowAll.Name = "mnuShowAll"
        Me.mnuShowAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.mnuShowAll.Size = New System.Drawing.Size(252, 22)
        Me.mnuShowAll.Text = "Show All Items"
        '
        'frmEventList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 493)
        Me.ControlBox = False
        Me.Controls.Add(Me.dataGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEventList"
        Me.Text = "Events List"
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuEventListOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents mnuEventListOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRelinkVideo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuShowAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChangeTimeCriteria As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NudgeVideoTimesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExportEDL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportVPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Bookmark As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents gridPathcount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridCriteria As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridTeamName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridOutcome As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents gridVideoTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuEditRows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
