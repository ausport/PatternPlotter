<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetGraphs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetGraphs))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstCurrentGraphs = New System.Windows.Forms.CheckedListBox
        Me.cmdRemoveGraph = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboXAxis = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lstYAxisList = New System.Windows.Forms.CheckedListBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cboDataGroup = New System.Windows.Forms.ComboBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.ChartPreview = New AxMSChart20Lib.AxMSChart
        Me.chkGraphStacked = New System.Windows.Forms.CheckBox
        Me.cboGraphTypes = New System.Windows.Forms.ComboBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        'CType(Me.ChartPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(520, 293)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(91, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "<< Add Graph"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(100, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(92, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Close"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstCurrentGraphs)
        Me.GroupBox1.Controls.Add(Me.cmdRemoveGraph)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 273)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Graphs"
        '
        'lstCurrentGraphs
        '
        Me.lstCurrentGraphs.FormattingEnabled = True
        Me.lstCurrentGraphs.Location = New System.Drawing.Point(6, 20)
        Me.lstCurrentGraphs.Name = "lstCurrentGraphs"
        Me.lstCurrentGraphs.Size = New System.Drawing.Size(233, 214)
        Me.lstCurrentGraphs.TabIndex = 4
        '
        'cmdRemoveGraph
        '
        Me.cmdRemoveGraph.Location = New System.Drawing.Point(164, 245)
        Me.cmdRemoveGraph.Name = "cmdRemoveGraph"
        Me.cmdRemoveGraph.Size = New System.Drawing.Size(75, 23)
        Me.cmdRemoveGraph.TabIndex = 2
        Me.cmdRemoveGraph.Text = "Remove"
        Me.cmdRemoveGraph.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboXAxis)
        Me.GroupBox2.Location = New System.Drawing.Point(264, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(194, 49)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "X-Axis (Category Labels)"
        '
        'cboXAxis
        '
        Me.cboXAxis.FormattingEnabled = True
        Me.cboXAxis.Items.AddRange(New Object() {"GameID", "TimeCriteria", "TeamName", "Region", "EventName", "Time (Minutes)"})
        Me.cboXAxis.Location = New System.Drawing.Point(7, 20)
        Me.cboXAxis.Name = "cboXAxis"
        Me.cboXAxis.Size = New System.Drawing.Size(181, 21)
        Me.cboXAxis.TabIndex = 0
        Me.cboXAxis.Text = "TimeCriteria"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lstYAxisList)
        Me.GroupBox3.Location = New System.Drawing.Point(264, 124)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(194, 163)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Y-Axis (Values)"
        '
        'lstYAxisList
        '
        Me.lstYAxisList.FormattingEnabled = True
        Me.lstYAxisList.Location = New System.Drawing.Point(7, 20)
        Me.lstYAxisList.Name = "lstYAxisList"
        Me.lstYAxisList.Size = New System.Drawing.Size(181, 124)
        Me.lstYAxisList.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cboDataGroup)
        Me.GroupBox4.Location = New System.Drawing.Point(264, 68)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(194, 49)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Data Groups (Series)"
        '
        'cboDataGroup
        '
        Me.cboDataGroup.FormattingEnabled = True
        Me.cboDataGroup.Items.AddRange(New Object() {"None", "GameID", "TimeCriteria", "TeamName", "Region", "EventName"})
        Me.cboDataGroup.Location = New System.Drawing.Point(7, 20)
        Me.cboDataGroup.Name = "cboDataGroup"
        Me.cboDataGroup.Size = New System.Drawing.Size(181, 21)
        Me.cboDataGroup.TabIndex = 0
        Me.cboDataGroup.Text = "None"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ChartPreview)
        Me.GroupBox5.Controls.Add(Me.chkGraphStacked)
        Me.GroupBox5.Controls.Add(Me.cboGraphTypes)
        Me.GroupBox5.Location = New System.Drawing.Point(464, 13)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(250, 273)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Chart Type"
        '
        'ChartPreview
        '
        Me.ChartPreview.DataSource = Nothing
        Me.ChartPreview.Location = New System.Drawing.Point(7, 70)
        Me.ChartPreview.Name = "ChartPreview"
        Me.ChartPreview.OcxState = CType(resources.GetObject("ChartPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.ChartPreview.Size = New System.Drawing.Size(236, 198)
        Me.ChartPreview.TabIndex = 3
        '
        'chkGraphStacked
        '
        Me.chkGraphStacked.AutoSize = True
        Me.chkGraphStacked.Location = New System.Drawing.Point(6, 46)
        Me.chkGraphStacked.Name = "chkGraphStacked"
        Me.chkGraphStacked.Size = New System.Drawing.Size(66, 17)
        Me.chkGraphStacked.TabIndex = 2
        Me.chkGraphStacked.Text = "Stacked"
        Me.chkGraphStacked.UseVisualStyleBackColor = True
        '
        'cboGraphTypes
        '
        Me.cboGraphTypes.FormattingEnabled = True
        Me.cboGraphTypes.Location = New System.Drawing.Point(6, 19)
        Me.cboGraphTypes.Name = "cboGraphTypes"
        Me.cboGraphTypes.Size = New System.Drawing.Size(237, 21)
        Me.cboGraphTypes.TabIndex = 1
        '
        'frmSetGraphs
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(727, 334)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetGraphs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set Graphs"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.ChartPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemoveGraph As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboXAxis As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lstYAxisList As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cboDataGroup As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cboGraphTypes As System.Windows.Forms.ComboBox
    Friend WithEvents chkGraphStacked As System.Windows.Forms.CheckBox
    Friend WithEvents lstCurrentGraphs As System.Windows.Forms.CheckedListBox
    Friend WithEvents ChartPreview As AxMSChart20Lib.AxMSChart

End Class
