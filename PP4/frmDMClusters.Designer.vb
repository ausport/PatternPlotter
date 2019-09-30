<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDMClusters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDMClusters))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboGameID = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdSelectNone = New System.Windows.Forms.Button
        Me.cmdSelectAll = New System.Windows.Forms.Button
        Me.lstDescriptors = New System.Windows.Forms.CheckedListBox
        Me.cboTeamName = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboTimeCriteria = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboOutcome = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.numIterations = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.numClusters = New System.Windows.Forms.NumericUpDown
        Me.rdoKMeans = New System.Windows.Forms.RadioButton
        Me.rdoMeloids = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.mnuTabsDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuClusterAddVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdPDF = New System.Windows.Forms.Button
        Me.picPitch = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.numIterations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numClusters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.mnuTabsDropDown.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboGameID)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmdSelectNone)
        Me.GroupBox1.Controls.Add(Me.cmdSelectAll)
        Me.GroupBox1.Controls.Add(Me.lstDescriptors)
        Me.GroupBox1.Controls.Add(Me.cboTeamName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboTimeCriteria)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboOutcome)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 257)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Parameters"
        '
        'cboGameID
        '
        Me.cboGameID.FormattingEnabled = True
        Me.cboGameID.Items.AddRange(New Object() {"*"})
        Me.cboGameID.Location = New System.Drawing.Point(91, 16)
        Me.cboGameID.Name = "cboGameID"
        Me.cboGameID.Size = New System.Drawing.Size(159, 21)
        Me.cboGameID.Sorted = True
        Me.cboGameID.TabIndex = 15
        Me.cboGameID.Text = "*"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Game ID:"
        '
        'cmdSelectNone
        '
        Me.cmdSelectNone.Location = New System.Drawing.Point(13, 224)
        Me.cmdSelectNone.Name = "cmdSelectNone"
        Me.cmdSelectNone.Size = New System.Drawing.Size(73, 23)
        Me.cmdSelectNone.TabIndex = 12
        Me.cmdSelectNone.Text = "Deselect All"
        Me.cmdSelectNone.UseVisualStyleBackColor = True
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Location = New System.Drawing.Point(14, 195)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(72, 23)
        Me.cmdSelectAll.TabIndex = 11
        Me.cmdSelectAll.Text = "Select All"
        Me.cmdSelectAll.UseVisualStyleBackColor = True
        '
        'lstDescriptors
        '
        Me.lstDescriptors.FormattingEnabled = True
        Me.lstDescriptors.Location = New System.Drawing.Point(91, 123)
        Me.lstDescriptors.Name = "lstDescriptors"
        Me.lstDescriptors.Size = New System.Drawing.Size(159, 124)
        Me.lstDescriptors.Sorted = True
        Me.lstDescriptors.TabIndex = 8
        '
        'cboTeamName
        '
        Me.cboTeamName.FormattingEnabled = True
        Me.cboTeamName.Items.AddRange(New Object() {"*"})
        Me.cboTeamName.Location = New System.Drawing.Point(91, 43)
        Me.cboTeamName.Name = "cboTeamName"
        Me.cboTeamName.Size = New System.Drawing.Size(159, 21)
        Me.cboTeamName.Sorted = True
        Me.cboTeamName.TabIndex = 3
        Me.cboTeamName.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Time Criteria:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Team/Player:"
        '
        'cboTimeCriteria
        '
        Me.cboTimeCriteria.FormattingEnabled = True
        Me.cboTimeCriteria.Items.AddRange(New Object() {"*"})
        Me.cboTimeCriteria.Location = New System.Drawing.Point(91, 97)
        Me.cboTimeCriteria.Name = "cboTimeCriteria"
        Me.cboTimeCriteria.Size = New System.Drawing.Size(159, 21)
        Me.cboTimeCriteria.Sorted = True
        Me.cboTimeCriteria.TabIndex = 7
        Me.cboTimeCriteria.Text = "*"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(18, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 31)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Selected Descriptors:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboOutcome
        '
        Me.cboOutcome.FormattingEnabled = True
        Me.cboOutcome.Items.AddRange(New Object() {"*", "Positive Outcomes", "Negative Outcomes", "Descriptors"})
        Me.cboOutcome.Location = New System.Drawing.Point(91, 70)
        Me.cboOutcome.Name = "cboOutcome"
        Me.cboOutcome.Size = New System.Drawing.Size(159, 21)
        Me.cboOutcome.TabIndex = 5
        Me.cboOutcome.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Outcome:"
        '
        'numIterations
        '
        Me.numIterations.Location = New System.Drawing.Point(113, 45)
        Me.numIterations.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.numIterations.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numIterations.Name = "numIterations"
        Me.numIterations.Size = New System.Drawing.Size(48, 20)
        Me.numIterations.TabIndex = 19
        Me.numIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numIterations.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Maximum Iterations:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numClusters
        '
        Me.numClusters.Location = New System.Drawing.Point(113, 19)
        Me.numClusters.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numClusters.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numClusters.Name = "numClusters"
        Me.numClusters.Size = New System.Drawing.Size(48, 20)
        Me.numClusters.TabIndex = 17
        Me.numClusters.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numClusters.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'rdoKMeans
        '
        Me.rdoKMeans.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoKMeans.AutoSize = True
        Me.rdoKMeans.Checked = True
        Me.rdoKMeans.Location = New System.Drawing.Point(33, 81)
        Me.rdoKMeans.Name = "rdoKMeans"
        Me.rdoKMeans.Size = New System.Drawing.Size(66, 17)
        Me.rdoKMeans.TabIndex = 6
        Me.rdoKMeans.TabStop = True
        Me.rdoKMeans.Text = "k-Means"
        Me.rdoKMeans.UseVisualStyleBackColor = True
        '
        'rdoMeloids
        '
        Me.rdoMeloids.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoMeloids.AutoSize = True
        Me.rdoMeloids.Location = New System.Drawing.Point(113, 81)
        Me.rdoMeloids.Name = "rdoMeloids"
        Me.rdoMeloids.Size = New System.Drawing.Size(70, 17)
        Me.rdoMeloids.TabIndex = 3
        Me.rdoMeloids.Text = "k-Meloids"
        Me.rdoMeloids.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Apriori Clusters:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.numClusters)
        Me.GroupBox2.Controls.Add(Me.numIterations)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.rdoKMeans)
        Me.GroupBox2.Controls.Add(Me.rdoMeloids)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 272)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(256, 113)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Cluster Parameters"
        '
        'mnuTabsDropDown
        '
        Me.mnuTabsDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClusterAddVPL})
        Me.mnuTabsDropDown.Name = "mnuChartDropDown"
        Me.mnuTabsDropDown.ShowImageMargin = False
        Me.mnuTabsDropDown.Size = New System.Drawing.Size(150, 26)
        '
        'mnuClusterAddVPL
        '
        Me.mnuClusterAddVPL.Name = "mnuClusterAddVPL"
        Me.mnuClusterAddVPL.Size = New System.Drawing.Size(149, 22)
        Me.mnuClusterAddVPL.Text = "Add Cluster to VPL"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 5
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.cmdRefresh, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Button1, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.OK_Button, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdPrint, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdPDF, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 507)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(256, 51)
        Me.TableLayoutPanel2.TabIndex = 21
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.Location = New System.Drawing.Point(3, 5)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(45, 40)
        Me.cmdRefresh.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.AutoSize = True
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Image = Global.PatternPlotter4.My.Resources.Resources.Exit_010
        Me.Button1.Location = New System.Drawing.Point(207, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(45, 40)
        Me.Button1.TabIndex = 4
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.OK_Button.Image = Global.PatternPlotter4.My.Resources.Resources.Camera
        Me.OK_Button.Location = New System.Drawing.Point(156, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(45, 40)
        Me.OK_Button.TabIndex = 0
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPrint.Image = Global.PatternPlotter4.My.Resources.Resources.Print32
        Me.cmdPrint.Location = New System.Drawing.Point(105, 5)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(45, 40)
        Me.cmdPrint.TabIndex = 2
        '
        'cmdPDF
        '
        Me.cmdPDF.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPDF.Image = Global.PatternPlotter4.My.Resources.Resources.Print_Preview32
        Me.cmdPDF.Location = New System.Drawing.Point(54, 5)
        Me.cmdPDF.Name = "cmdPDF"
        Me.cmdPDF.Size = New System.Drawing.Size(45, 40)
        Me.cmdPDF.TabIndex = 3
        '
        'picPitch
        '
        Me.picPitch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picPitch.BackColor = System.Drawing.Color.Gainsboro
        Me.picPitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPitch.Location = New System.Drawing.Point(275, 12)
        Me.picPitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picPitch.Name = "picPitch"
        Me.picPitch.Size = New System.Drawing.Size(444, 546)
        Me.picPitch.TabIndex = 3
        Me.picPitch.TabStop = False
        '
        'frmDMClusters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(732, 574)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.picPitch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.MinimumSize = New System.Drawing.Size(561, 492)
        Me.Name = "frmDMClusters"
        Me.Text = "Data Mining Tools: Clustering"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numIterations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numClusters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.mnuTabsDropDown.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoKMeans As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMeloids As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboGameID As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdSelectNone As System.Windows.Forms.Button
    Friend WithEvents cmdSelectAll As System.Windows.Forms.Button
    Friend WithEvents lstDescriptors As System.Windows.Forms.CheckedListBox
    Friend WithEvents cboTeamName As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTimeCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboOutcome As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents picPitch As System.Windows.Forms.PictureBox
    Friend WithEvents numClusters As System.Windows.Forms.NumericUpDown
    Friend WithEvents numIterations As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents mnuTabsDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuClusterAddVPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdPDF As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
