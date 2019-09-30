<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGraph
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGraph))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblTeamNames = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblDescriptors = New System.Windows.Forms.Label
        Me.lblOutcomes = New System.Windows.Forms.Label
        Me.lblTimeCriterion = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblGameIDs = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblChartType = New System.Windows.Forms.Label
        Me.Graph = New AxMSChart20Lib.AxMSChart
        Me.mnuGraphType = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuGraph2DLine = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraph3DLine = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraph2DBar = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraph3DBar = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraphPie = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraph2DStep = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraph3DStep = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuGraphRowAsSeries = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraphStacked = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGraphHideGridLines = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuGraphTitle = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuEditChartProps = New System.Windows.Forms.ToolStripMenuItem
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdPDF = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.Graph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuGraphType.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTeamNames)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblDescriptors)
        Me.GroupBox1.Controls.Add(Me.lblOutcomes)
        Me.GroupBox1.Controls.Add(Me.lblTimeCriterion)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblGameIDs)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblChartType)
        Me.GroupBox1.Location = New System.Drawing.Point(728, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 532)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Graph Information"
        '
        'lblTeamNames
        '
        Me.lblTeamNames.AutoSize = True
        Me.lblTeamNames.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTeamNames.Location = New System.Drawing.Point(98, 110)
        Me.lblTeamNames.Name = "lblTeamNames"
        Me.lblTeamNames.Size = New System.Drawing.Size(25, 13)
        Me.lblTeamNames.TabIndex = 10
        Me.lblTeamNames.Text = "Info"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Team Name(s):"
        '
        'lblDescriptors
        '
        Me.lblDescriptors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescriptors.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDescriptors.Location = New System.Drawing.Point(98, 197)
        Me.lblDescriptors.Name = "lblDescriptors"
        Me.lblDescriptors.Size = New System.Drawing.Size(157, 332)
        Me.lblDescriptors.TabIndex = 8
        Me.lblDescriptors.Text = "Info"
        '
        'lblOutcomes
        '
        Me.lblOutcomes.AutoSize = True
        Me.lblOutcomes.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblOutcomes.Location = New System.Drawing.Point(98, 168)
        Me.lblOutcomes.Name = "lblOutcomes"
        Me.lblOutcomes.Size = New System.Drawing.Size(25, 13)
        Me.lblOutcomes.TabIndex = 7
        Me.lblOutcomes.Text = "Info"
        '
        'lblTimeCriterion
        '
        Me.lblTimeCriterion.AutoSize = True
        Me.lblTimeCriterion.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTimeCriterion.Location = New System.Drawing.Point(98, 139)
        Me.lblTimeCriterion.Name = "lblTimeCriterion"
        Me.lblTimeCriterion.Size = New System.Drawing.Size(25, 13)
        Me.lblTimeCriterion.TabIndex = 6
        Me.lblTimeCriterion.Text = "Info"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Descriptor List:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 168)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Outcome Types:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 139)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Time Criterion:"
        '
        'lblGameIDs
        '
        Me.lblGameIDs.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblGameIDs.Location = New System.Drawing.Point(98, 52)
        Me.lblGameIDs.Name = "lblGameIDs"
        Me.lblGameIDs.Size = New System.Drawing.Size(157, 58)
        Me.lblGameIDs.TabIndex = 2
        Me.lblGameIDs.Text = "Info"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Game ID(s):"
        '
        'lblChartType
        '
        Me.lblChartType.AutoSize = True
        Me.lblChartType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChartType.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblChartType.Location = New System.Drawing.Point(6, 25)
        Me.lblChartType.Name = "lblChartType"
        Me.lblChartType.Size = New System.Drawing.Size(45, 13)
        Me.lblChartType.TabIndex = 0
        Me.lblChartType.Text = "Label1"
        '
        'Graph
        '
        Me.Graph.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Graph.DataSource = Nothing
        Me.Graph.Location = New System.Drawing.Point(12, 12)
        Me.Graph.Name = "Graph"
        Me.Graph.OcxState = CType(resources.GetObject("Graph.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Graph.Size = New System.Drawing.Size(710, 589)
        Me.Graph.TabIndex = 4
        '
        'mnuGraphType
        '
        Me.mnuGraphType.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGraph2DLine, Me.mnuGraph3DLine, Me.mnuGraph2DBar, Me.mnuGraph3DBar, Me.mnuGraphPie, Me.mnuGraph2DStep, Me.mnuGraph3DStep, Me.ToolStripSeparator1, Me.mnuGraphRowAsSeries, Me.mnuGraphStacked, Me.mnuGraphHideGridLines, Me.ToolStripSeparator2, Me.mnuGraphTitle, Me.ToolStripSeparator3, Me.mnuEditChartProps})
        Me.mnuGraphType.Name = "mnuGraphType"
        Me.mnuGraphType.ShowCheckMargin = True
        Me.mnuGraphType.ShowImageMargin = False
        Me.mnuGraphType.Size = New System.Drawing.Size(205, 287)
        '
        'mnuGraph2DLine
        '
        Me.mnuGraph2DLine.Name = "mnuGraph2DLine"
        Me.mnuGraph2DLine.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph2DLine.Text = "2D Line Graph"
        '
        'mnuGraph3DLine
        '
        Me.mnuGraph3DLine.Name = "mnuGraph3DLine"
        Me.mnuGraph3DLine.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph3DLine.Text = "3D Line Graph"
        '
        'mnuGraph2DBar
        '
        Me.mnuGraph2DBar.Name = "mnuGraph2DBar"
        Me.mnuGraph2DBar.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph2DBar.Text = "2D Bar Chart"
        '
        'mnuGraph3DBar
        '
        Me.mnuGraph3DBar.Name = "mnuGraph3DBar"
        Me.mnuGraph3DBar.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph3DBar.Text = "3D Bar Chart"
        '
        'mnuGraphPie
        '
        Me.mnuGraphPie.Name = "mnuGraphPie"
        Me.mnuGraphPie.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraphPie.Text = "2D Pie Chart"
        '
        'mnuGraph2DStep
        '
        Me.mnuGraph2DStep.Name = "mnuGraph2DStep"
        Me.mnuGraph2DStep.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph2DStep.Text = "2D Step Chart"
        '
        'mnuGraph3DStep
        '
        Me.mnuGraph3DStep.Name = "mnuGraph3DStep"
        Me.mnuGraph3DStep.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraph3DStep.Text = "3D Step Chart"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(201, 6)
        '
        'mnuGraphRowAsSeries
        '
        Me.mnuGraphRowAsSeries.CheckOnClick = True
        Me.mnuGraphRowAsSeries.Name = "mnuGraphRowAsSeries"
        Me.mnuGraphRowAsSeries.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraphRowAsSeries.Text = "Swap Rows and Columns"
        '
        'mnuGraphStacked
        '
        Me.mnuGraphStacked.CheckOnClick = True
        Me.mnuGraphStacked.Name = "mnuGraphStacked"
        Me.mnuGraphStacked.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraphStacked.Text = "Show as Stacked"
        '
        'mnuGraphHideGridLines
        '
        Me.mnuGraphHideGridLines.CheckOnClick = True
        Me.mnuGraphHideGridLines.Name = "mnuGraphHideGridLines"
        Me.mnuGraphHideGridLines.Size = New System.Drawing.Size(204, 22)
        Me.mnuGraphHideGridLines.Text = "Hide Gridlines"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(201, 6)
        '
        'mnuGraphTitle
        '
        Me.mnuGraphTitle.Name = "mnuGraphTitle"
        Me.mnuGraphTitle.Size = New System.Drawing.Size(100, 21)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(201, 6)
        '
        'mnuEditChartProps
        '
        Me.mnuEditChartProps.Name = "mnuEditChartProps"
        Me.mnuEditChartProps.Size = New System.Drawing.Size(204, 22)
        Me.mnuEditChartProps.Text = "Edit Graph Properties"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPDF, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPrint, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(760, 550)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(229, 51)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'cmdPDF
        '
        Me.cmdPDF.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPDF.Image = Global.PatternPlotter4.My.Resources.Resources.Print_Preview32
        Me.cmdPDF.Location = New System.Drawing.Point(4, 5)
        Me.cmdPDF.Name = "cmdPDF"
        Me.cmdPDF.Size = New System.Drawing.Size(49, 40)
        Me.cmdPDF.TabIndex = 3
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.AutoSize = True
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Image = Global.PatternPlotter4.My.Resources.Resources.Exit_010
        Me.Cancel_Button.Location = New System.Drawing.Point(175, 5)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(49, 40)
        Me.Cancel_Button.TabIndex = 1
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.OK_Button.Image = Global.PatternPlotter4.My.Resources.Resources.Camera
        Me.OK_Button.Location = New System.Drawing.Point(118, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(49, 40)
        Me.OK_Button.TabIndex = 0
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPrint.Image = Global.PatternPlotter4.My.Resources.Resources.Print32
        Me.cmdPrint.Location = New System.Drawing.Point(61, 5)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(49, 40)
        Me.cmdPrint.TabIndex = 2
        '
        'frmGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 613)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Graph)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGraph"
        Me.Text = "Game Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Graph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuGraphType.ResumeLayout(False)
        Me.mnuGraphType.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblChartType As System.Windows.Forms.Label
    Friend WithEvents lblDescriptors As System.Windows.Forms.Label
    Friend WithEvents lblOutcomes As System.Windows.Forms.Label
    Friend WithEvents lblTimeCriterion As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblGameIDs As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTeamNames As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Graph As AxMSChart20Lib.AxMSChart
    Friend WithEvents mnuGraphType As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuGraph2DLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph3DLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph2DBar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph3DBar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraphPie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph2DStep As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph3DStep As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuGraphRowAsSeries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraphStacked As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuGraphTitle As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents mnuGraphHideGridLines As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdPDF As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditChartProps As System.Windows.Forms.ToolStripMenuItem
End Class
