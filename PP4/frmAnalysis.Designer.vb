<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnalysis
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
        Me.components = New System.ComponentModel.Container()
        Me.grpRunAnalysis = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTeamName = New System.Windows.Forms.ComboBox()
        Me.cboOutcome = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTimeCriteria = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstDescriptors = New System.Windows.Forms.CheckedListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdSelectNone = New System.Windows.Forms.Button()
        Me.cmdSelectAll = New System.Windows.Forms.Button()
        Me.grpAdvancedSearch = New System.Windows.Forms.GroupBox()
        Me.chkUseAdvancedSearch = New System.Windows.Forms.CheckBox()
        Me.cmdAdvancedSearch = New System.Windows.Forms.Button()
        Me.grpReportCharts = New System.Windows.Forms.GroupBox()
        Me.chkScatterPlot = New System.Windows.Forms.CheckBox()
        Me.chkPlayerMaps = New System.Windows.Forms.CheckBox()
        Me.lnkSetGraphs = New System.Windows.Forms.LinkLabel()
        Me.chkPossessionGraphs = New System.Windows.Forms.CheckBox()
        Me.chkPathwayMaps = New System.Windows.Forms.CheckBox()
        Me.chkOutcomeClusters = New System.Windows.Forms.CheckBox()
        Me.chkVideoPlaylist = New System.Windows.Forms.CheckBox()
        Me.chkPosessionHeat = New System.Windows.Forms.CheckBox()
        Me.AnalysisMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSelectNone = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkPossessTimeHeat = New System.Windows.Forms.CheckBox()
        Me.chkHeatAnimated = New System.Windows.Forms.CheckBox()
        Me.chkBallSpdHeat = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.grpAdvancedSearch.SuspendLayout()
        Me.grpReportCharts.SuspendLayout()
        Me.AnalysisMenu.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpRunAnalysis
        '
        Me.grpRunAnalysis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRunAnalysis.ColumnCount = 1
        Me.grpRunAnalysis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.grpRunAnalysis.Location = New System.Drawing.Point(22, 841)
        Me.grpRunAnalysis.Margin = New System.Windows.Forms.Padding(6)
        Me.grpRunAnalysis.Name = "grpRunAnalysis"
        Me.grpRunAnalysis.RowCount = 1
        Me.grpRunAnalysis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.grpRunAnalysis.Size = New System.Drawing.Size(460, 54)
        Me.grpRunAnalysis.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(23, 126)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(6)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(392, 42)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Run Analysis"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 41)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Team/Player:"
        '
        'cboTeamName
        '
        Me.cboTeamName.FormattingEnabled = True
        Me.cboTeamName.Location = New System.Drawing.Point(154, 35)
        Me.cboTeamName.Margin = New System.Windows.Forms.Padding(6)
        Me.cboTeamName.Name = "cboTeamName"
        Me.cboTeamName.Size = New System.Drawing.Size(288, 32)
        Me.cboTeamName.Sorted = True
        Me.cboTeamName.TabIndex = 3
        Me.cboTeamName.Text = "*"
        '
        'cboOutcome
        '
        Me.cboOutcome.FormattingEnabled = True
        Me.cboOutcome.Items.AddRange(New Object() {"*", "Positive Outcomes", "Negative Outcomes", "Descriptors"})
        Me.cboOutcome.Location = New System.Drawing.Point(154, 85)
        Me.cboOutcome.Margin = New System.Windows.Forms.Padding(6)
        Me.cboOutcome.Name = "cboOutcome"
        Me.cboOutcome.Size = New System.Drawing.Size(288, 32)
        Me.cboOutcome.TabIndex = 5
        Me.cboOutcome.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 90)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 25)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Outcome:"
        '
        'cboTimeCriteria
        '
        Me.cboTimeCriteria.FormattingEnabled = True
        Me.cboTimeCriteria.Location = New System.Drawing.Point(154, 135)
        Me.cboTimeCriteria.Margin = New System.Windows.Forms.Padding(6)
        Me.cboTimeCriteria.Name = "cboTimeCriteria"
        Me.cboTimeCriteria.Size = New System.Drawing.Size(288, 32)
        Me.cboTimeCriteria.Sorted = True
        Me.cboTimeCriteria.TabIndex = 7
        Me.cboTimeCriteria.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 140)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 25)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Time Criteria:"
        '
        'lstDescriptors
        '
        Me.lstDescriptors.FormattingEnabled = True
        Me.lstDescriptors.Location = New System.Drawing.Point(154, 183)
        Me.lstDescriptors.Margin = New System.Windows.Forms.Padding(6)
        Me.lstDescriptors.Name = "lstDescriptors"
        Me.lstDescriptors.Size = New System.Drawing.Size(288, 220)
        Me.lstDescriptors.Sorted = True
        Me.lstDescriptors.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(20, 183)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 57)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Selected Descriptors:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdSelectNone)
        Me.GroupBox1.Controls.Add(Me.cmdSelectAll)
        Me.GroupBox1.Controls.Add(Me.cboTeamName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lstDescriptors)
        Me.GroupBox1.Controls.Add(Me.cboOutcome)
        Me.GroupBox1.Controls.Add(Me.cboTimeCriteria)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 22)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Size = New System.Drawing.Size(460, 423)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Selection"
        '
        'cmdSelectNone
        '
        Me.cmdSelectNone.Location = New System.Drawing.Point(11, 369)
        Me.cmdSelectNone.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdSelectNone.Name = "cmdSelectNone"
        Me.cmdSelectNone.Size = New System.Drawing.Size(134, 42)
        Me.cmdSelectNone.TabIndex = 12
        Me.cmdSelectNone.Text = "Deselect All"
        Me.cmdSelectNone.UseVisualStyleBackColor = True
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Location = New System.Drawing.Point(13, 316)
        Me.cmdSelectAll.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(132, 42)
        Me.cmdSelectAll.TabIndex = 11
        Me.cmdSelectAll.Text = "Select All"
        Me.cmdSelectAll.UseVisualStyleBackColor = True
        '
        'grpAdvancedSearch
        '
        Me.grpAdvancedSearch.Controls.Add(Me.chkUseAdvancedSearch)
        Me.grpAdvancedSearch.Controls.Add(Me.cmdAdvancedSearch)
        Me.grpAdvancedSearch.Location = New System.Drawing.Point(22, 456)
        Me.grpAdvancedSearch.Margin = New System.Windows.Forms.Padding(6)
        Me.grpAdvancedSearch.Name = "grpAdvancedSearch"
        Me.grpAdvancedSearch.Padding = New System.Windows.Forms.Padding(6)
        Me.grpAdvancedSearch.Size = New System.Drawing.Size(460, 135)
        Me.grpAdvancedSearch.TabIndex = 12
        Me.grpAdvancedSearch.TabStop = False
        Me.grpAdvancedSearch.Text = "Advanced Search"
        '
        'chkUseAdvancedSearch
        '
        Me.chkUseAdvancedSearch.AutoSize = True
        Me.chkUseAdvancedSearch.Location = New System.Drawing.Point(24, 90)
        Me.chkUseAdvancedSearch.Margin = New System.Windows.Forms.Padding(6)
        Me.chkUseAdvancedSearch.Name = "chkUseAdvancedSearch"
        Me.chkUseAdvancedSearch.Size = New System.Drawing.Size(235, 29)
        Me.chkUseAdvancedSearch.TabIndex = 1
        Me.chkUseAdvancedSearch.Text = "Use Advanced Search"
        Me.chkUseAdvancedSearch.UseVisualStyleBackColor = True
        '
        'cmdAdvancedSearch
        '
        Me.cmdAdvancedSearch.Location = New System.Drawing.Point(13, 37)
        Me.cmdAdvancedSearch.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdAdvancedSearch.Name = "cmdAdvancedSearch"
        Me.cmdAdvancedSearch.Size = New System.Drawing.Size(433, 42)
        Me.cmdAdvancedSearch.TabIndex = 0
        Me.cmdAdvancedSearch.Text = "Advanced Search >>"
        Me.cmdAdvancedSearch.UseVisualStyleBackColor = True
        '
        'grpReportCharts
        '
        Me.grpReportCharts.Controls.Add(Me.chkScatterPlot)
        Me.grpReportCharts.Controls.Add(Me.chkPlayerMaps)
        Me.grpReportCharts.Controls.Add(Me.lnkSetGraphs)
        Me.grpReportCharts.Controls.Add(Me.chkPossessionGraphs)
        Me.grpReportCharts.Controls.Add(Me.chkPathwayMaps)
        Me.grpReportCharts.Controls.Add(Me.chkOutcomeClusters)
        Me.grpReportCharts.Controls.Add(Me.chkVideoPlaylist)
        Me.grpReportCharts.Location = New System.Drawing.Point(22, 604)
        Me.grpReportCharts.Margin = New System.Windows.Forms.Padding(6)
        Me.grpReportCharts.Name = "grpReportCharts"
        Me.grpReportCharts.Padding = New System.Windows.Forms.Padding(6)
        Me.grpReportCharts.Size = New System.Drawing.Size(460, 174)
        Me.grpReportCharts.TabIndex = 13
        Me.grpReportCharts.TabStop = False
        Me.grpReportCharts.Text = "Report Charts"
        '
        'chkScatterPlot
        '
        Me.chkScatterPlot.AutoSize = True
        Me.chkScatterPlot.Location = New System.Drawing.Point(215, 35)
        Me.chkScatterPlot.Margin = New System.Windows.Forms.Padding(6)
        Me.chkScatterPlot.Name = "chkScatterPlot"
        Me.chkScatterPlot.Size = New System.Drawing.Size(148, 29)
        Me.chkScatterPlot.TabIndex = 8
        Me.chkScatterPlot.Text = "Scatter Plots"
        Me.chkScatterPlot.UseVisualStyleBackColor = True
        '
        'chkPlayerMaps
        '
        Me.chkPlayerMaps.AutoSize = True
        Me.chkPlayerMaps.Location = New System.Drawing.Point(24, 122)
        Me.chkPlayerMaps.Margin = New System.Windows.Forms.Padding(6)
        Me.chkPlayerMaps.Name = "chkPlayerMaps"
        Me.chkPlayerMaps.Size = New System.Drawing.Size(147, 29)
        Me.chkPlayerMaps.TabIndex = 6
        Me.chkPlayerMaps.Text = "Player Maps"
        Me.chkPlayerMaps.UseVisualStyleBackColor = True
        '
        'lnkSetGraphs
        '
        Me.lnkSetGraphs.AutoSize = True
        Me.lnkSetGraphs.Enabled = False
        Me.lnkSetGraphs.Location = New System.Drawing.Point(336, 124)
        Me.lnkSetGraphs.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lnkSetGraphs.Name = "lnkSetGraphs"
        Me.lnkSetGraphs.Size = New System.Drawing.Size(111, 25)
        Me.lnkSetGraphs.TabIndex = 5
        Me.lnkSetGraphs.TabStop = True
        Me.lnkSetGraphs.Text = "Set Graphs"
        Me.lnkSetGraphs.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'chkPossessionGraphs
        '
        Me.chkPossessionGraphs.AutoSize = True
        Me.chkPossessionGraphs.Checked = True
        Me.chkPossessionGraphs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPossessionGraphs.Enabled = False
        Me.chkPossessionGraphs.Location = New System.Drawing.Point(215, 122)
        Me.chkPossessionGraphs.Margin = New System.Windows.Forms.Padding(6)
        Me.chkPossessionGraphs.Name = "chkPossessionGraphs"
        Me.chkPossessionGraphs.Size = New System.Drawing.Size(102, 29)
        Me.chkPossessionGraphs.TabIndex = 3
        Me.chkPossessionGraphs.Text = "Graphs"
        Me.chkPossessionGraphs.UseVisualStyleBackColor = True
        '
        'chkPathwayMaps
        '
        Me.chkPathwayMaps.AutoSize = True
        Me.chkPathwayMaps.Location = New System.Drawing.Point(24, 79)
        Me.chkPathwayMaps.Margin = New System.Windows.Forms.Padding(6)
        Me.chkPathwayMaps.Name = "chkPathwayMaps"
        Me.chkPathwayMaps.Size = New System.Drawing.Size(167, 29)
        Me.chkPathwayMaps.TabIndex = 2
        Me.chkPathwayMaps.Text = "Pathway Maps"
        Me.chkPathwayMaps.UseVisualStyleBackColor = True
        '
        'chkOutcomeClusters
        '
        Me.chkOutcomeClusters.AutoSize = True
        Me.chkOutcomeClusters.Location = New System.Drawing.Point(215, 78)
        Me.chkOutcomeClusters.Margin = New System.Windows.Forms.Padding(6)
        Me.chkOutcomeClusters.Name = "chkOutcomeClusters"
        Me.chkOutcomeClusters.Size = New System.Drawing.Size(195, 29)
        Me.chkOutcomeClusters.TabIndex = 1
        Me.chkOutcomeClusters.Text = "Outcome Clusters"
        Me.chkOutcomeClusters.UseVisualStyleBackColor = True
        '
        'chkVideoPlaylist
        '
        Me.chkVideoPlaylist.AutoSize = True
        Me.chkVideoPlaylist.Location = New System.Drawing.Point(24, 37)
        Me.chkVideoPlaylist.Margin = New System.Windows.Forms.Padding(6)
        Me.chkVideoPlaylist.Name = "chkVideoPlaylist"
        Me.chkVideoPlaylist.Size = New System.Drawing.Size(155, 29)
        Me.chkVideoPlaylist.TabIndex = 0
        Me.chkVideoPlaylist.Text = "Video Playlist"
        Me.chkVideoPlaylist.UseVisualStyleBackColor = True
        '
        'chkPosessionHeat
        '
        Me.chkPosessionHeat.AutoSize = True
        Me.chkPosessionHeat.Location = New System.Drawing.Point(22, 35)
        Me.chkPosessionHeat.Margin = New System.Windows.Forms.Padding(6)
        Me.chkPosessionHeat.Name = "chkPosessionHeat"
        Me.chkPosessionHeat.Size = New System.Drawing.Size(276, 29)
        Me.chkPosessionHeat.TabIndex = 7
        Me.chkPosessionHeat.Text = "Event Frequency Heat Map"
        Me.chkPosessionHeat.UseVisualStyleBackColor = True
        '
        'AnalysisMenu
        '
        Me.AnalysisMenu.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.AnalysisMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSelectAll, Me.mnuSelectNone})
        Me.AnalysisMenu.Name = "AnalysisMenu"
        Me.AnalysisMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.AnalysisMenu.ShowImageMargin = False
        Me.AnalysisMenu.Size = New System.Drawing.Size(174, 72)
        '
        'mnuSelectAll
        '
        Me.mnuSelectAll.Name = "mnuSelectAll"
        Me.mnuSelectAll.Size = New System.Drawing.Size(173, 34)
        Me.mnuSelectAll.Text = "Select All"
        '
        'mnuSelectNone
        '
        Me.mnuSelectNone.Name = "mnuSelectNone"
        Me.mnuSelectNone.Size = New System.Drawing.Size(173, 34)
        Me.mnuSelectNone.Text = "Select None"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OK_Button)
        Me.GroupBox2.Controls.Add(Me.chkPossessTimeHeat)
        Me.GroupBox2.Controls.Add(Me.chkHeatAnimated)
        Me.GroupBox2.Controls.Add(Me.chkBallSpdHeat)
        Me.GroupBox2.Controls.Add(Me.chkPosessionHeat)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 790)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Size = New System.Drawing.Size(458, 185)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Heat Charts"
        '
        'chkPossessTimeHeat
        '
        Me.chkPossessTimeHeat.AutoSize = True
        Me.chkPossessTimeHeat.Location = New System.Drawing.Point(16, 221)
        Me.chkPossessTimeHeat.Margin = New System.Windows.Forms.Padding(6)
        Me.chkPossessTimeHeat.Name = "chkPossessTimeHeat"
        Me.chkPossessTimeHeat.Size = New System.Drawing.Size(278, 29)
        Me.chkPossessTimeHeat.TabIndex = 10
        Me.chkPossessTimeHeat.Text = "Possession Time Heat Map"
        Me.chkPossessTimeHeat.UseVisualStyleBackColor = True
        '
        'chkHeatAnimated
        '
        Me.chkHeatAnimated.AutoSize = True
        Me.chkHeatAnimated.Location = New System.Drawing.Point(311, 222)
        Me.chkHeatAnimated.Margin = New System.Windows.Forms.Padding(6)
        Me.chkHeatAnimated.Name = "chkHeatAnimated"
        Me.chkHeatAnimated.Size = New System.Drawing.Size(121, 29)
        Me.chkHeatAnimated.TabIndex = 9
        Me.chkHeatAnimated.Text = "Animate?"
        Me.chkHeatAnimated.UseVisualStyleBackColor = True
        '
        'chkBallSpdHeat
        '
        Me.chkBallSpdHeat.AutoSize = True
        Me.chkBallSpdHeat.Location = New System.Drawing.Point(22, 78)
        Me.chkBallSpdHeat.Margin = New System.Windows.Forms.Padding(6)
        Me.chkBallSpdHeat.Name = "chkBallSpdHeat"
        Me.chkBallSpdHeat.Size = New System.Drawing.Size(223, 29)
        Me.chkBallSpdHeat.TabIndex = 8
        Me.chkBallSpdHeat.Text = "Ball Speed Heat Map"
        Me.chkBallSpdHeat.UseVisualStyleBackColor = True
        '
        'frmAnalysis
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(504, 991)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.grpReportCharts)
        Me.Controls.Add(Me.grpAdvancedSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpRunAnalysis)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAnalysis"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Analysis Criteria"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpAdvancedSearch.ResumeLayout(False)
        Me.grpAdvancedSearch.PerformLayout()
        Me.grpReportCharts.ResumeLayout(False)
        Me.grpReportCharts.PerformLayout()
        Me.AnalysisMenu.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpRunAnalysis As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTeamName As System.Windows.Forms.ComboBox
    Friend WithEvents cboOutcome As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboTimeCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstDescriptors As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpAdvancedSearch As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAdvancedSearch As System.Windows.Forms.Button
    Friend WithEvents chkUseAdvancedSearch As System.Windows.Forms.CheckBox
    Friend WithEvents grpReportCharts As System.Windows.Forms.GroupBox
    Friend WithEvents chkPathwayMaps As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutcomeClusters As System.Windows.Forms.CheckBox
    Friend WithEvents chkVideoPlaylist As System.Windows.Forms.CheckBox
    Friend WithEvents chkPossessionGraphs As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSelectAll As System.Windows.Forms.Button
    Friend WithEvents AnalysisMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectNone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSelectNone As System.Windows.Forms.Button
    Friend WithEvents lnkSetGraphs As System.Windows.Forms.LinkLabel
    Friend WithEvents chkPlayerMaps As System.Windows.Forms.CheckBox
    Friend WithEvents chkPosessionHeat As System.Windows.Forms.CheckBox
    Friend WithEvents chkScatterPlot As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBallSpdHeat As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeatAnimated As System.Windows.Forms.CheckBox
    Friend WithEvents chkPossessTimeHeat As System.Windows.Forms.CheckBox

End Class
