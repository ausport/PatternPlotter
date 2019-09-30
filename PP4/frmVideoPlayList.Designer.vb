<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVideoPlayList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVideoPlayList))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.mnuVPL_PlayAll = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Save = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_PlaySelection = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Preview = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Export2DV = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Export2File = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVPL_StartExport = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_PauseExport = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_StopExport = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Help = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVPL_Prev = New System.Windows.Forms.ToolStripButton
        Me.mnuVPL_Next = New System.Windows.Forms.ToolStripButton
        Me.vplGrid = New System.Windows.Forms.DataGridView
        Me.PathID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GameID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TeamName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Session = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.InPoint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OutPoint = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descriptors = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.uRegion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Video = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Video2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.timerPlayAll = New System.Windows.Forms.Timer(Me.components)
        Me.timExport = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip_VPL = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuVPLPlayItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuVPLShowPathways = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVPSSelectItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuVPLUnselectItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripVPL_RemoveItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuVPLRemoveUnSelected = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAdd2OtherVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1.SuspendLayout()
        CType(Me.vplGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip_VPL.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVPL_PlayAll, Me.mnuVPL_Save, Me.mnuVPL_PlaySelection, Me.mnuVPL_Preview, Me.mnuVPL_Export2DV, Me.mnuVPL_Export2File, Me.ToolStripSeparator1, Me.mnuVPL_StartExport, Me.mnuVPL_PauseExport, Me.mnuVPL_StopExport, Me.mnuVPL_Help, Me.ToolStripSeparator2, Me.mnuVPL_Prev, Me.mnuVPL_Next})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(916, 68)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'mnuVPL_PlayAll
        '
        Me.mnuVPL_PlayAll.Image = Global.PatternPlotter4.My.Resources.Resources.Play_Media
        Me.mnuVPL_PlayAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_PlayAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_PlayAll.Name = "mnuVPL_PlayAll"
        Me.mnuVPL_PlayAll.Padding = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.mnuVPL_PlayAll.Size = New System.Drawing.Size(65, 65)
        Me.mnuVPL_PlayAll.Text = "Play All"
        Me.mnuVPL_PlayAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVPL_Save
        '
        Me.mnuVPL_Save.Image = Global.PatternPlotter4.My.Resources.Resources.Save
        Me.mnuVPL_Save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Save.Name = "mnuVPL_Save"
        Me.mnuVPL_Save.Padding = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.mnuVPL_Save.Size = New System.Drawing.Size(65, 65)
        Me.mnuVPL_Save.Text = "Save"
        Me.mnuVPL_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVPL_PlaySelection
        '
        Me.mnuVPL_PlaySelection.AutoSize = False
        Me.mnuVPL_PlaySelection.Image = CType(resources.GetObject("mnuVPL_PlaySelection.Image"), System.Drawing.Image)
        Me.mnuVPL_PlaySelection.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_PlaySelection.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_PlaySelection.Name = "mnuVPL_PlaySelection"
        Me.mnuVPL_PlaySelection.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.mnuVPL_PlaySelection.Size = New System.Drawing.Size(77, 65)
        Me.mnuVPL_PlaySelection.Text = "Play Selection"
        Me.mnuVPL_PlaySelection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.mnuVPL_PlaySelection.Visible = False
        '
        'mnuVPL_Preview
        '
        Me.mnuVPL_Preview.Image = Global.PatternPlotter4.My.Resources.Resources.Media2
        Me.mnuVPL_Preview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Preview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Preview.Name = "mnuVPL_Preview"
        Me.mnuVPL_Preview.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_Preview.Text = "Preview"
        Me.mnuVPL_Preview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVPL_Export2DV
        '
        Me.mnuVPL_Export2DV.Image = Global.PatternPlotter4.My.Resources.Resources.FireWire_48
        Me.mnuVPL_Export2DV.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Export2DV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Export2DV.Name = "mnuVPL_Export2DV"
        Me.mnuVPL_Export2DV.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.mnuVPL_Export2DV.Size = New System.Drawing.Size(78, 65)
        Me.mnuVPL_Export2DV.Text = "Export to DV"
        Me.mnuVPL_Export2DV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVPL_Export2File
        '
        Me.mnuVPL_Export2File.Image = Global.PatternPlotter4.My.Resources.Resources.Folder_Movies
        Me.mnuVPL_Export2File.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Export2File.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Export2File.Name = "mnuVPL_Export2File"
        Me.mnuVPL_Export2File.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.mnuVPL_Export2File.Size = New System.Drawing.Size(81, 65)
        Me.mnuVPL_Export2File.Text = "Export to File"
        Me.mnuVPL_Export2File.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 68)
        Me.ToolStripSeparator1.Visible = False
        '
        'mnuVPL_StartExport
        '
        Me.mnuVPL_StartExport.Image = Global.PatternPlotter4.My.Resources.Resources.Play_Export
        Me.mnuVPL_StartExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_StartExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_StartExport.Name = "mnuVPL_StartExport"
        Me.mnuVPL_StartExport.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_StartExport.Text = "Export"
        Me.mnuVPL_StartExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.mnuVPL_StartExport.ToolTipText = "Export"
        Me.mnuVPL_StartExport.Visible = False
        '
        'mnuVPL_PauseExport
        '
        Me.mnuVPL_PauseExport.Image = Global.PatternPlotter4.My.Resources.Resources.Pause_Export
        Me.mnuVPL_PauseExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_PauseExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_PauseExport.Name = "mnuVPL_PauseExport"
        Me.mnuVPL_PauseExport.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_PauseExport.Text = "Pause"
        Me.mnuVPL_PauseExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.mnuVPL_PauseExport.ToolTipText = "Pause"
        Me.mnuVPL_PauseExport.Visible = False
        '
        'mnuVPL_StopExport
        '
        Me.mnuVPL_StopExport.Image = Global.PatternPlotter4.My.Resources.Resources.Stop_Export
        Me.mnuVPL_StopExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_StopExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_StopExport.Name = "mnuVPL_StopExport"
        Me.mnuVPL_StopExport.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_StopExport.Text = "Stop"
        Me.mnuVPL_StopExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.mnuVPL_StopExport.ToolTipText = "Stop"
        Me.mnuVPL_StopExport.Visible = False
        '
        'mnuVPL_Help
        '
        Me.mnuVPL_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuVPL_Help.Image = Global.PatternPlotter4.My.Resources.Resources.Help004
        Me.mnuVPL_Help.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Help.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Help.Name = "mnuVPL_Help"
        Me.mnuVPL_Help.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_Help.Text = "Help"
        Me.mnuVPL_Help.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 68)
        '
        'mnuVPL_Prev
        '
        Me.mnuVPL_Prev.Enabled = False
        Me.mnuVPL_Prev.Image = Global.PatternPlotter4.My.Resources.Resources.left_48x48
        Me.mnuVPL_Prev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Prev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Prev.Name = "mnuVPL_Prev"
        Me.mnuVPL_Prev.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_Prev.Text = "Previous"
        Me.mnuVPL_Prev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVPL_Next
        '
        Me.mnuVPL_Next.Enabled = False
        Me.mnuVPL_Next.Image = Global.PatternPlotter4.My.Resources.Resources.right_48x48
        Me.mnuVPL_Next.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVPL_Next.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVPL_Next.Name = "mnuVPL_Next"
        Me.mnuVPL_Next.Size = New System.Drawing.Size(52, 65)
        Me.mnuVPL_Next.Text = "Next"
        Me.mnuVPL_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'vplGrid
        '
        Me.vplGrid.AllowUserToAddRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.vplGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.vplGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vplGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.vplGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PathID, Me.GameID, Me.TeamName, Me.Session, Me.InPoint, Me.OutPoint, Me.Descriptors, Me.uRegion, Me.Video, Me.Video2})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.vplGrid.DefaultCellStyle = DataGridViewCellStyle7
        Me.vplGrid.Location = New System.Drawing.Point(12, 71)
        Me.vplGrid.Name = "vplGrid"
        Me.vplGrid.ReadOnly = True
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlDarkDark
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.NullValue = "0"
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.vplGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.vplGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.vplGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.vplGrid.Size = New System.Drawing.Size(892, 451)
        Me.vplGrid.TabIndex = 2
        '
        'PathID
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PathID.DefaultCellStyle = DataGridViewCellStyle6
        Me.PathID.HeaderText = "PathID"
        Me.PathID.Name = "PathID"
        Me.PathID.ReadOnly = True
        Me.PathID.Width = 65
        '
        'GameID
        '
        Me.GameID.HeaderText = "Game ID"
        Me.GameID.Name = "GameID"
        Me.GameID.ReadOnly = True
        Me.GameID.Width = 74
        '
        'TeamName
        '
        Me.TeamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.TeamName.HeaderText = "Team Name"
        Me.TeamName.Name = "TeamName"
        Me.TeamName.ReadOnly = True
        Me.TeamName.Width = 90
        '
        'Session
        '
        Me.Session.HeaderText = "Session"
        Me.Session.Name = "Session"
        Me.Session.ReadOnly = True
        Me.Session.Width = 69
        '
        'InPoint
        '
        Me.InPoint.HeaderText = "InPoint"
        Me.InPoint.Name = "InPoint"
        Me.InPoint.ReadOnly = True
        Me.InPoint.Width = 65
        '
        'OutPoint
        '
        Me.OutPoint.HeaderText = "OutPoint"
        Me.OutPoint.Name = "OutPoint"
        Me.OutPoint.ReadOnly = True
        Me.OutPoint.Width = 73
        '
        'Descriptors
        '
        Me.Descriptors.HeaderText = "Descriptors"
        Me.Descriptors.Name = "Descriptors"
        Me.Descriptors.ReadOnly = True
        Me.Descriptors.Width = 85
        '
        'uRegion
        '
        Me.uRegion.HeaderText = "Region"
        Me.uRegion.Name = "uRegion"
        Me.uRegion.ReadOnly = True
        Me.uRegion.Width = 66
        '
        'Video
        '
        Me.Video.HeaderText = "Video"
        Me.Video.Name = "Video"
        Me.Video.ReadOnly = True
        Me.Video.Visible = False
        Me.Video.Width = 59
        '
        'Video2
        '
        Me.Video2.HeaderText = "Video2"
        Me.Video2.Name = "Video2"
        Me.Video2.ReadOnly = True
        Me.Video2.Visible = False
        Me.Video2.Width = 65
        '
        'timerPlayAll
        '
        Me.timerPlayAll.Interval = 250
        '
        'timExport
        '
        '
        'ContextMenuStrip_VPL
        '
        Me.ContextMenuStrip_VPL.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVPLPlayItem, Me.mnuVPLShowPathways, Me.ToolStripSeparator3, Me.mnuAdd2OtherVPL, Me.mnuVPSSelectItem, Me.mnuVPLUnselectItem, Me.ToolStripSeparator4, Me.ToolStripVPL_RemoveItem, Me.mnuVPLRemoveUnSelected})
        Me.ContextMenuStrip_VPL.Name = "ContextMenuStrip_VPL"
        Me.ContextMenuStrip_VPL.Size = New System.Drawing.Size(243, 192)
        '
        'mnuVPLPlayItem
        '
        Me.mnuVPLPlayItem.Name = "mnuVPLPlayItem"
        Me.mnuVPLPlayItem.Size = New System.Drawing.Size(242, 22)
        Me.mnuVPLPlayItem.Text = "Play Item"
        '
        'mnuVPLShowPathways
        '
        Me.mnuVPLShowPathways.Name = "mnuVPLShowPathways"
        Me.mnuVPLShowPathways.Size = New System.Drawing.Size(242, 22)
        Me.mnuVPLShowPathways.Text = "Show Item Pathways"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(239, 6)
        '
        'mnuVPSSelectItem
        '
        Me.mnuVPSSelectItem.Name = "mnuVPSSelectItem"
        Me.mnuVPSSelectItem.Size = New System.Drawing.Size(242, 22)
        Me.mnuVPSSelectItem.Text = "Select Item"
        '
        'mnuVPLUnselectItem
        '
        Me.mnuVPLUnselectItem.Name = "mnuVPLUnselectItem"
        Me.mnuVPLUnselectItem.Size = New System.Drawing.Size(242, 22)
        Me.mnuVPLUnselectItem.Text = "De-Select Item"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(239, 6)
        '
        'ToolStripVPL_RemoveItem
        '
        Me.ToolStripVPL_RemoveItem.Name = "ToolStripVPL_RemoveItem"
        Me.ToolStripVPL_RemoveItem.Size = New System.Drawing.Size(242, 22)
        Me.ToolStripVPL_RemoveItem.Text = "Remove Item"
        '
        'mnuVPLRemoveUnSelected
        '
        Me.mnuVPLRemoveUnSelected.Name = "mnuVPLRemoveUnSelected"
        Me.mnuVPLRemoveUnSelected.Size = New System.Drawing.Size(242, 22)
        Me.mnuVPLRemoveUnSelected.Text = "Remove All Unselected Items"
        '
        'mnuAdd2OtherVPL
        '
        Me.mnuAdd2OtherVPL.Name = "mnuAdd2OtherVPL"
        Me.mnuAdd2OtherVPL.Size = New System.Drawing.Size(242, 22)
        Me.mnuAdd2OtherVPL.Text = "Add Selected Items to Other VPL"
        '
        'frmVideoPlayList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 534)
        Me.Controls.Add(Me.vplGrid)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVideoPlayList"
        Me.Opacity = 0.1
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Video Playlist"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.vplGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip_VPL.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents mnuVPL_PlayAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_PlaySelection As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_Export2DV As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_Export2File As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_Help As System.Windows.Forms.ToolStripButton
    Friend WithEvents vplGrid As System.Windows.Forms.DataGridView
    Friend WithEvents PathID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GameID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TeamName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Session As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InPoint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutPoint As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descriptors As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents uRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Video As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Video2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents timerPlayAll As System.Windows.Forms.Timer
    Friend WithEvents mnuVPL_StartExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_PauseExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_StopExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents timExport As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuVPL_Prev As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVPL_Next As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuStrip_VPL As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripVPL_RemoveItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVPLPlayItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuVPLShowPathways As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVPSSelectItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVPLUnselectItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuVPLRemoveUnSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVPL_Preview As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuAdd2OtherVPL As System.Windows.Forms.ToolStripMenuItem

End Class
