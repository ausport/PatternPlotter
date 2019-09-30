<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowStats
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShowStats))
        Me.gridStat = New System.Windows.Forms.DataGridView
        Me.colRegion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colEvent = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuStatsDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuShowByID = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuShowByTeam = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExportStats2Excel = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.gridStat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStatsDropDown.SuspendLayout()
        Me.SuspendLayout()
        '
        'gridStat
        '
        Me.gridStat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridStat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.gridStat.BackgroundColor = System.Drawing.SystemColors.MenuBar
        Me.gridStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridStat.ColumnHeadersVisible = False
        Me.gridStat.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRegion, Me.colEvent, Me.colTotal})
        Me.gridStat.Location = New System.Drawing.Point(13, 13)
        Me.gridStat.Name = "gridStat"
        Me.gridStat.RowHeadersVisible = False
        Me.gridStat.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.gridStat.RowTemplate.DefaultCellStyle.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.gridStat.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridStat.RowTemplate.ReadOnly = True
        Me.gridStat.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridStat.Size = New System.Drawing.Size(768, 535)
        Me.gridStat.TabIndex = 0
        '
        'colRegion
        '
        Me.colRegion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colRegion.HeaderText = "colRegion"
        Me.colRegion.MinimumWidth = 200
        Me.colRegion.Name = "colRegion"
        Me.colRegion.ReadOnly = True
        Me.colRegion.Width = 200
        '
        'colEvent
        '
        Me.colEvent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colEvent.HeaderText = "colEvent"
        Me.colEvent.MinimumWidth = 200
        Me.colEvent.Name = "colEvent"
        Me.colEvent.ReadOnly = True
        Me.colEvent.Width = 200
        '
        'colTotal
        '
        Me.colTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        Me.colTotal.HeaderText = "colTotal"
        Me.colTotal.MinimumWidth = 50
        Me.colTotal.Name = "colTotal"
        Me.colTotal.ReadOnly = True
        Me.colTotal.Width = 50
        '
        'mnuStatsDropDown
        '
        Me.mnuStatsDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShowByID, Me.mnuShowByTeam, Me.ToolStripSeparator1, Me.mnuExportStats2Excel})
        Me.mnuStatsDropDown.Name = "mnuStatsDropDown"
        Me.mnuStatsDropDown.ShowCheckMargin = True
        Me.mnuStatsDropDown.ShowImageMargin = False
        Me.mnuStatsDropDown.Size = New System.Drawing.Size(226, 76)
        '
        'mnuShowByID
        '
        Me.mnuShowByID.CheckOnClick = True
        Me.mnuShowByID.Name = "mnuShowByID"
        Me.mnuShowByID.Size = New System.Drawing.Size(225, 22)
        Me.mnuShowByID.Text = "Show by Game ID"
        '
        'mnuShowByTeam
        '
        Me.mnuShowByTeam.CheckOnClick = True
        Me.mnuShowByTeam.Name = "mnuShowByTeam"
        Me.mnuShowByTeam.Size = New System.Drawing.Size(225, 22)
        Me.mnuShowByTeam.Text = "Show by Team Name"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(222, 6)
        '
        'mnuExportStats2Excel
        '
        Me.mnuExportStats2Excel.Name = "mnuExportStats2Excel"
        Me.mnuExportStats2Excel.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuExportStats2Excel.Size = New System.Drawing.Size(225, 22)
        Me.mnuExportStats2Excel.Text = "Export Table to Excel"
        '
        'frmShowStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 560)
        Me.Controls.Add(Me.gridStat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmShowStats"
        Me.Text = "Game Statistics"
        CType(Me.gridStat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStatsDropDown.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gridStat As System.Windows.Forms.DataGridView
    Friend WithEvents colRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEvent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuStatsDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuExportStats2Excel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowByID As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuShowByTeam As System.Windows.Forms.ToolStripMenuItem
End Class
