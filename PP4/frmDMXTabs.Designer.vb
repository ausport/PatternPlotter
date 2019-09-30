<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDMXTabs
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.cmdRefreshDataSet = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboUseOrigins = New System.Windows.Forms.CheckBox
        Me.chkShowQuadrants = New System.Windows.Forms.CheckBox
        Me.chkRuleConfidence = New System.Windows.Forms.CheckBox
        Me.chkRuleProbability = New System.Windows.Forms.CheckBox
        Me.chkRuleLift = New System.Windows.Forms.CheckBox
        Me.rdoPositionSets = New System.Windows.Forms.RadioButton
        Me.numSupport = New System.Windows.Forms.NumericUpDown
        Me.lblMinSupport = New System.Windows.Forms.Label
        Me.rdoAssociations = New System.Windows.Forms.RadioButton
        Me.rdoFrequency = New System.Windows.Forms.RadioButton
        Me.rdoInterestingness = New System.Windows.Forms.RadioButton
        Me.cboDM_Xaxis = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboDM_Yaxis = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
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
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DMGrid = New System.Windows.Forms.DataGridView
        Me.mnuTabsDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuClusterAddVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuClusterAddToPathway = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExportCSV = New System.Windows.Forms.ToolStripMenuItem
        Me.chkExlcudeTeam = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DMGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuTabsDropDown.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdRefreshDataSet, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(123, 631)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'cmdRefreshDataSet
        '
        Me.cmdRefreshDataSet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRefreshDataSet.Location = New System.Drawing.Point(3, 3)
        Me.cmdRefreshDataSet.Name = "cmdRefreshDataSet"
        Me.cmdRefreshDataSet.Size = New System.Drawing.Size(67, 23)
        Me.cmdRefreshDataSet.TabIndex = 0
        Me.cmdRefreshDataSet.Text = "Refresh"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkExlcudeTeam)
        Me.GroupBox1.Controls.Add(Me.cboUseOrigins)
        Me.GroupBox1.Controls.Add(Me.chkShowQuadrants)
        Me.GroupBox1.Controls.Add(Me.chkRuleConfidence)
        Me.GroupBox1.Controls.Add(Me.chkRuleProbability)
        Me.GroupBox1.Controls.Add(Me.chkRuleLift)
        Me.GroupBox1.Controls.Add(Me.rdoPositionSets)
        Me.GroupBox1.Controls.Add(Me.numSupport)
        Me.GroupBox1.Controls.Add(Me.lblMinSupport)
        Me.GroupBox1.Controls.Add(Me.rdoAssociations)
        Me.GroupBox1.Controls.Add(Me.rdoFrequency)
        Me.GroupBox1.Controls.Add(Me.rdoInterestingness)
        Me.GroupBox1.Controls.Add(Me.cboDM_Xaxis)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cboDM_Yaxis)
        Me.GroupBox1.Controls.Add(Me.Label7)
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
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 615)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Parameters"
        '
        'cboUseOrigins
        '
        Me.cboUseOrigins.AutoSize = True
        Me.cboUseOrigins.Location = New System.Drawing.Point(58, 519)
        Me.cboUseOrigins.Name = "cboUseOrigins"
        Me.cboUseOrigins.Size = New System.Drawing.Size(151, 17)
        Me.cboUseOrigins.TabIndex = 32
        Me.cboUseOrigins.Text = "Calculate from Play Origins"
        Me.cboUseOrigins.UseVisualStyleBackColor = True
        '
        'chkShowQuadrants
        '
        Me.chkShowQuadrants.AutoSize = True
        Me.chkShowQuadrants.Enabled = False
        Me.chkShowQuadrants.Location = New System.Drawing.Point(58, 496)
        Me.chkShowQuadrants.Name = "chkShowQuadrants"
        Me.chkShowQuadrants.Size = New System.Drawing.Size(191, 17)
        Me.chkShowQuadrants.TabIndex = 31
        Me.chkShowQuadrants.Text = "Show Independent Quadrant Prob."
        Me.chkShowQuadrants.UseVisualStyleBackColor = True
        '
        'chkRuleConfidence
        '
        Me.chkRuleConfidence.AutoSize = True
        Me.chkRuleConfidence.Enabled = False
        Me.chkRuleConfidence.Location = New System.Drawing.Point(58, 450)
        Me.chkRuleConfidence.Name = "chkRuleConfidence"
        Me.chkRuleConfidence.Size = New System.Drawing.Size(170, 17)
        Me.chkRuleConfidence.TabIndex = 30
        Me.chkRuleConfidence.Text = "Show Rule Confidence (A=>B)"
        Me.chkRuleConfidence.UseVisualStyleBackColor = True
        '
        'chkRuleProbability
        '
        Me.chkRuleProbability.AutoSize = True
        Me.chkRuleProbability.Enabled = False
        Me.chkRuleProbability.Location = New System.Drawing.Point(58, 427)
        Me.chkRuleProbability.Name = "chkRuleProbability"
        Me.chkRuleProbability.Size = New System.Drawing.Size(175, 17)
        Me.chkRuleProbability.TabIndex = 28
        Me.chkRuleProbability.Text = "Show Rule Probability: p(A U B)"
        Me.chkRuleProbability.UseVisualStyleBackColor = True
        '
        'chkRuleLift
        '
        Me.chkRuleLift.AutoSize = True
        Me.chkRuleLift.Enabled = False
        Me.chkRuleLift.Location = New System.Drawing.Point(58, 473)
        Me.chkRuleLift.Name = "chkRuleLift"
        Me.chkRuleLift.Size = New System.Drawing.Size(123, 17)
        Me.chkRuleLift.TabIndex = 27
        Me.chkRuleLift.Text = "Show Rule Lift Ratio"
        Me.chkRuleLift.UseVisualStyleBackColor = True
        '
        'rdoPositionSets
        '
        Me.rdoPositionSets.AutoSize = True
        Me.rdoPositionSets.Location = New System.Drawing.Point(20, 404)
        Me.rdoPositionSets.Name = "rdoPositionSets"
        Me.rdoPositionSets.Size = New System.Drawing.Size(182, 17)
        Me.rdoPositionSets.TabIndex = 25
        Me.rdoPositionSets.Text = "Ball Movement Association Rules"
        Me.rdoPositionSets.UseVisualStyleBackColor = True
        '
        'numSupport
        '
        Me.numSupport.Enabled = False
        Me.numSupport.Location = New System.Drawing.Point(131, 583)
        Me.numSupport.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numSupport.Name = "numSupport"
        Me.numSupport.Size = New System.Drawing.Size(48, 20)
        Me.numSupport.TabIndex = 22
        Me.numSupport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSupport.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblMinSupport
        '
        Me.lblMinSupport.AutoSize = True
        Me.lblMinSupport.Enabled = False
        Me.lblMinSupport.Location = New System.Drawing.Point(34, 585)
        Me.lblMinSupport.Name = "lblMinSupport"
        Me.lblMinSupport.Size = New System.Drawing.Size(91, 13)
        Me.lblMinSupport.TabIndex = 21
        Me.lblMinSupport.Text = "Minimum Support:"
        '
        'rdoAssociations
        '
        Me.rdoAssociations.AutoSize = True
        Me.rdoAssociations.Location = New System.Drawing.Point(20, 559)
        Me.rdoAssociations.Name = "rdoAssociations"
        Me.rdoAssociations.Size = New System.Drawing.Size(160, 17)
        Me.rdoAssociations.TabIndex = 20
        Me.rdoAssociations.Text = "Frequent Event Associations"
        Me.rdoAssociations.UseVisualStyleBackColor = True
        '
        'rdoFrequency
        '
        Me.rdoFrequency.AutoSize = True
        Me.rdoFrequency.Checked = True
        Me.rdoFrequency.Location = New System.Drawing.Point(20, 358)
        Me.rdoFrequency.Name = "rdoFrequency"
        Me.rdoFrequency.Size = New System.Drawing.Size(75, 17)
        Me.rdoFrequency.TabIndex = 6
        Me.rdoFrequency.TabStop = True
        Me.rdoFrequency.Text = "Frequency"
        Me.rdoFrequency.UseVisualStyleBackColor = True
        '
        'rdoInterestingness
        '
        Me.rdoInterestingness.AutoSize = True
        Me.rdoInterestingness.Location = New System.Drawing.Point(20, 381)
        Me.rdoInterestingness.Name = "rdoInterestingness"
        Me.rdoInterestingness.Size = New System.Drawing.Size(96, 17)
        Me.rdoInterestingness.TabIndex = 3
        Me.rdoInterestingness.Text = "Interestingness"
        Me.rdoInterestingness.UseVisualStyleBackColor = True
        '
        'cboDM_Xaxis
        '
        Me.cboDM_Xaxis.FormattingEnabled = True
        Me.cboDM_Xaxis.Items.AddRange(New Object() {"Event Name", "Game ID", "Outcome Type", "Region", "Team Name", "Time Criteria"})
        Me.cboDM_Xaxis.Location = New System.Drawing.Point(91, 293)
        Me.cboDM_Xaxis.Name = "cboDM_Xaxis"
        Me.cboDM_Xaxis.Size = New System.Drawing.Size(159, 21)
        Me.cboDM_Xaxis.Sorted = True
        Me.cboDM_Xaxis.TabIndex = 19
        Me.cboDM_Xaxis.Text = "Outcome Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 296)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "X Axis:"
        '
        'cboDM_Yaxis
        '
        Me.cboDM_Yaxis.FormattingEnabled = True
        Me.cboDM_Yaxis.Items.AddRange(New Object() {"Event Name", "Game ID", "Outcome Type", "Region", "Team Name", "Time Criteria"})
        Me.cboDM_Yaxis.Location = New System.Drawing.Point(91, 320)
        Me.cboDM_Yaxis.Name = "cboDM_Yaxis"
        Me.cboDM_Yaxis.Size = New System.Drawing.Size(159, 21)
        Me.cboDM_Yaxis.Sorted = True
        Me.cboDM_Yaxis.TabIndex = 17
        Me.cboDM_Yaxis.Text = "Event Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(43, 323)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Y Axis:"
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
        Me.cmdSelectNone.Location = New System.Drawing.Point(13, 248)
        Me.cmdSelectNone.Name = "cmdSelectNone"
        Me.cmdSelectNone.Size = New System.Drawing.Size(73, 23)
        Me.cmdSelectNone.TabIndex = 12
        Me.cmdSelectNone.Text = "Deselect All"
        Me.cmdSelectNone.UseVisualStyleBackColor = True
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Location = New System.Drawing.Point(14, 219)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(72, 23)
        Me.cmdSelectAll.TabIndex = 11
        Me.cmdSelectAll.Text = "Select All"
        Me.cmdSelectAll.UseVisualStyleBackColor = True
        '
        'lstDescriptors
        '
        Me.lstDescriptors.FormattingEnabled = True
        Me.lstDescriptors.Location = New System.Drawing.Point(91, 147)
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
        Me.Label3.Location = New System.Drawing.Point(14, 124)
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
        Me.cboTimeCriteria.Location = New System.Drawing.Point(91, 121)
        Me.cboTimeCriteria.Name = "cboTimeCriteria"
        Me.cboTimeCriteria.Size = New System.Drawing.Size(159, 21)
        Me.cboTimeCriteria.Sorted = True
        Me.cboTimeCriteria.TabIndex = 7
        Me.cboTimeCriteria.Text = "*"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(18, 147)
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
        Me.cboOutcome.Location = New System.Drawing.Point(91, 94)
        Me.cboOutcome.Name = "cboOutcome"
        Me.cboOutcome.Size = New System.Drawing.Size(159, 21)
        Me.cboOutcome.TabIndex = 5
        Me.cboOutcome.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Outcome:"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Enabled = False
        Me.NumericUpDown1.Location = New System.Drawing.Point(23, 483)
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(48, 20)
        Me.NumericUpDown1.TabIndex = 24
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Enabled = False
        Me.Label8.Location = New System.Drawing.Point(48, 506)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Maximum Iterations:"
        Me.Label8.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.DMGrid)
        Me.GroupBox2.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(275, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(656, 644)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Output"
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
        Me.DMGrid.Location = New System.Drawing.Point(3, 16)
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
        Me.DMGrid.Size = New System.Drawing.Size(650, 625)
        Me.DMGrid.TabIndex = 0
        '
        'mnuTabsDropDown
        '
        Me.mnuTabsDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClusterAddVPL, Me.mnuClusterAddToPathway, Me.ToolStripSeparator1, Me.mnuExportCSV})
        Me.mnuTabsDropDown.Name = "mnuChartDropDown"
        Me.mnuTabsDropDown.ShowImageMargin = False
        Me.mnuTabsDropDown.Size = New System.Drawing.Size(191, 76)
        '
        'mnuClusterAddVPL
        '
        Me.mnuClusterAddVPL.Name = "mnuClusterAddVPL"
        Me.mnuClusterAddVPL.Size = New System.Drawing.Size(190, 22)
        Me.mnuClusterAddVPL.Text = "Add Items to VPL"
        '
        'mnuClusterAddToPathway
        '
        Me.mnuClusterAddToPathway.Name = "mnuClusterAddToPathway"
        Me.mnuClusterAddToPathway.Size = New System.Drawing.Size(190, 22)
        Me.mnuClusterAddToPathway.Text = "Add Items to Pathway Map"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(187, 6)
        '
        'mnuExportCSV
        '
        Me.mnuExportCSV.Name = "mnuExportCSV"
        Me.mnuExportCSV.Size = New System.Drawing.Size(190, 22)
        Me.mnuExportCSV.Text = "Export To CSV"
        '
        'chkExlcudeTeam
        '
        Me.chkExlcudeTeam.AutoSize = True
        Me.chkExlcudeTeam.Location = New System.Drawing.Point(91, 71)
        Me.chkExlcudeTeam.Name = "chkExlcudeTeam"
        Me.chkExlcudeTeam.Size = New System.Drawing.Size(139, 17)
        Me.chkExlcudeTeam.TabIndex = 33
        Me.chkExlcudeTeam.Text = "Exclude Selected Team"
        Me.chkExlcudeTeam.UseVisualStyleBackColor = True
        '
        'frmDMXTabs
        '
        Me.AcceptButton = Me.cmdRefreshDataSet
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(946, 672)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MinimumSize = New System.Drawing.Size(784, 572)
        Me.Name = "frmDMXTabs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data Mining Tools: CrossTabs"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DMGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuTabsDropDown.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DMGrid As System.Windows.Forms.DataGridView
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
    Friend WithEvents cmdRefreshDataSet As System.Windows.Forms.Button
    Friend WithEvents cboGameID As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rdoInterestingness As System.Windows.Forms.RadioButton
    Friend WithEvents cboDM_Xaxis As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboDM_Yaxis As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rdoFrequency As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAssociations As System.Windows.Forms.RadioButton
    Friend WithEvents numSupport As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinSupport As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mnuTabsDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuClusterAddVPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClusterAddToPathway As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExportCSV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rdoPositionSets As System.Windows.Forms.RadioButton
    Friend WithEvents chkRuleLift As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuleProbability As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuleConfidence As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowQuadrants As System.Windows.Forms.CheckBox
    Friend WithEvents cboUseOrigins As System.Windows.Forms.CheckBox
    Friend WithEvents chkExlcudeTeam As System.Windows.Forms.CheckBox

End Class
