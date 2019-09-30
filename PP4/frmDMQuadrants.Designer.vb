<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDMQuadrants
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDMQuadrants))
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
        Me.numSequenceLength = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.numMinimumSupport = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.picPitch = New System.Windows.Forms.PictureBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.numHorizontalQ = New System.Windows.Forms.NumericUpDown
        Me.numVerticalQ = New System.Windows.Forms.NumericUpDown
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.DMGrid = New System.Windows.Forms.DataGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdPDF = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.numSequenceLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMinimumSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.numHorizontalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numVerticalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DMGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
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
        Me.GroupBox1.Size = New System.Drawing.Size(256, 258)
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
        'numSequenceLength
        '
        Me.numSequenceLength.Location = New System.Drawing.Point(157, 45)
        Me.numSequenceLength.Maximum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.numSequenceLength.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numSequenceLength.Name = "numSequenceLength"
        Me.numSequenceLength.Size = New System.Drawing.Size(48, 20)
        Me.numSequenceLength.TabIndex = 19
        Me.numSequenceLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSequenceLength.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Minimum Sequence Length:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'numMinimumSupport
        '
        Me.numMinimumSupport.Location = New System.Drawing.Point(157, 19)
        Me.numMinimumSupport.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numMinimumSupport.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.numMinimumSupport.Name = "numMinimumSupport"
        Me.numMinimumSupport.Size = New System.Drawing.Size(48, 20)
        Me.numMinimumSupport.TabIndex = 17
        Me.numMinimumSupport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numMinimumSupport.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(60, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Minimum Support:"
        '
        'picPitch
        '
        Me.picPitch.BackColor = System.Drawing.Color.Gainsboro
        Me.picPitch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picPitch.Location = New System.Drawing.Point(0, 0)
        Me.picPitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picPitch.Name = "picPitch"
        Me.picPitch.Size = New System.Drawing.Size(431, 638)
        Me.picPitch.TabIndex = 3
        Me.picPitch.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.numHorizontalQ)
        Me.GroupBox2.Controls.Add(Me.numVerticalQ)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.numMinimumSupport)
        Me.GroupBox2.Controls.Add(Me.numSequenceLength)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 276)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(256, 129)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Quadrant Parameters"
        '
        'numHorizontalQ
        '
        Me.numHorizontalQ.Location = New System.Drawing.Point(157, 71)
        Me.numHorizontalQ.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numHorizontalQ.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.numHorizontalQ.Name = "numHorizontalQ"
        Me.numHorizontalQ.Size = New System.Drawing.Size(48, 20)
        Me.numHorizontalQ.TabIndex = 21
        Me.numHorizontalQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numHorizontalQ.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'numVerticalQ
        '
        Me.numVerticalQ.Location = New System.Drawing.Point(157, 97)
        Me.numVerticalQ.Maximum = New Decimal(New Integer() {40, 0, 0, 0})
        Me.numVerticalQ.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVerticalQ.Name = "numVerticalQ"
        Me.numVerticalQ.Size = New System.Drawing.Size(48, 20)
        Me.numVerticalQ.TabIndex = 23
        Me.numVerticalQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numVerticalQ.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(42, 73)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Horizontal Quadrants:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(54, 99)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 13)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Vertical Quadrants:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DMGrid
        '
        Me.DMGrid.AllowUserToAddRows = False
        Me.DMGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DMGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DMGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DMGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DMGrid.BackgroundColor = System.Drawing.SystemColors.GrayText
        Me.DMGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DMGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DMGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DMGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.DMGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DMGrid.Location = New System.Drawing.Point(0, 0)
        Me.DMGrid.Name = "DMGrid"
        Me.DMGrid.ReadOnly = True
        Me.DMGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DMGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DMGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DMGrid.ShowEditingIcon = False
        Me.DMGrid.Size = New System.Drawing.Size(162, 638)
        Me.DMGrid.TabIndex = 21
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(274, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.picPitch)
        Me.SplitContainer1.Panel1MinSize = 267
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DMGrid)
        Me.SplitContainer1.Size = New System.Drawing.Size(605, 642)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 22
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 601)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(256, 51)
        Me.TableLayoutPanel2.TabIndex = 23
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
        'frmDMQuadrants
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(889, 668)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MinimumSize = New System.Drawing.Size(561, 490)
        Me.Name = "frmDMQuadrants"
        Me.Text = "Data Mining Tools: Quadrants Analysis"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numSequenceLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMinimumSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.numHorizontalQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numVerticalQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DMGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
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
    Friend WithEvents numMinimumSupport As System.Windows.Forms.NumericUpDown
    Friend WithEvents numSequenceLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DMGrid As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents numHorizontalQ As System.Windows.Forms.NumericUpDown
    Friend WithEvents numVerticalQ As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdPDF As System.Windows.Forms.Button
End Class
