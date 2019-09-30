<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChart
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdPublishChart = New System.Windows.Forms.Button
        Me.cmdPDF = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.mnuPathwayMapDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuPlayItemVideo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAddItem2VPL = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExcludeItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuShowCaptions = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuClusterDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuClusterAddVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuClusterAddToPathway = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPathwayMapDropdown2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuShowCaptions2 = New System.Windows.Forms.ToolStripMenuItem
        Me.grpBoxOptions = New System.Windows.Forms.GroupBox
        Me.chShowOtherPlay = New System.Windows.Forms.CheckBox
        Me.chkShowPossession = New System.Windows.Forms.CheckBox
        Me.numPathOpacity = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkShowDeliveries = New System.Windows.Forms.CheckBox
        Me.chkShowReceives = New System.Windows.Forms.CheckBox
        Me.mnuScatterPlotDropDown = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuScatterPlayVideo = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuScatterItem2VPL = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuScatterItem2PathMap = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuScatterChangeColor = New System.Windows.Forms.ToolStripMenuItem
        Me.picPitch = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.mnuPathwayMapDropDown.SuspendLayout()
        Me.mnuClusterDropDown.SuspendLayout()
        Me.mnuPathwayMapDropdown2.SuspendLayout()
        Me.grpBoxOptions.SuspendLayout()
        CType(Me.numPathOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuScatterPlotDropDown.SuspendLayout()
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Location = New System.Drawing.Point(378, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 400)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Chart Information"
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
        Me.lblDescriptors.Size = New System.Drawing.Size(157, 200)
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
        Me.lblChartType.Size = New System.Drawing.Size(0, 13)
        Me.lblChartType.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPublishChart, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPDF, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPrint, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(378, 517)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(260, 51)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'cmdPublishChart
        '
        Me.cmdPublishChart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPublishChart.Enabled = False
        Me.cmdPublishChart.Image = Global.PatternPlotter4.My.Resources.Resources.iFeed
        Me.cmdPublishChart.Location = New System.Drawing.Point(3, 5)
        Me.cmdPublishChart.Name = "cmdPublishChart"
        Me.cmdPublishChart.Size = New System.Drawing.Size(46, 40)
        Me.cmdPublishChart.TabIndex = 4
        Me.cmdPublishChart.Visible = False
        '
        'cmdPDF
        '
        Me.cmdPDF.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPDF.Image = Global.PatternPlotter4.My.Resources.Resources.Print_Preview32
        Me.cmdPDF.Location = New System.Drawing.Point(55, 5)
        Me.cmdPDF.Name = "cmdPDF"
        Me.cmdPDF.Size = New System.Drawing.Size(46, 40)
        Me.cmdPDF.TabIndex = 3
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.AutoSize = True
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Image = Global.PatternPlotter4.My.Resources.Resources.Exit_010
        Me.Cancel_Button.Location = New System.Drawing.Point(214, 5)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(39, 40)
        Me.Cancel_Button.TabIndex = 1
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.OK_Button.Image = Global.PatternPlotter4.My.Resources.Resources.Camera
        Me.OK_Button.Location = New System.Drawing.Point(159, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(46, 40)
        Me.OK_Button.TabIndex = 0
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPrint.Image = Global.PatternPlotter4.My.Resources.Resources.Print32
        Me.cmdPrint.Location = New System.Drawing.Point(107, 5)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(46, 40)
        Me.cmdPrint.TabIndex = 2
        '
        'mnuPathwayMapDropDown
        '
        Me.mnuPathwayMapDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPlayItemVideo, Me.mnuAddItem2VPL, Me.ToolStripSeparator1, Me.mnuExcludeItem, Me.mnuShowCaptions})
        Me.mnuPathwayMapDropDown.Name = "mnuChartDropDown"
        Me.mnuPathwayMapDropDown.ShowImageMargin = False
        Me.mnuPathwayMapDropDown.Size = New System.Drawing.Size(178, 98)
        '
        'mnuPlayItemVideo
        '
        Me.mnuPlayItemVideo.Name = "mnuPlayItemVideo"
        Me.mnuPlayItemVideo.Size = New System.Drawing.Size(177, 22)
        Me.mnuPlayItemVideo.Text = "Play Item Video"
        '
        'mnuAddItem2VPL
        '
        Me.mnuAddItem2VPL.Name = "mnuAddItem2VPL"
        Me.mnuAddItem2VPL.Size = New System.Drawing.Size(177, 22)
        Me.mnuAddItem2VPL.Text = "Add Item to VPL"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'mnuExcludeItem
        '
        Me.mnuExcludeItem.Enabled = False
        Me.mnuExcludeItem.Name = "mnuExcludeItem"
        Me.mnuExcludeItem.Size = New System.Drawing.Size(177, 22)
        Me.mnuExcludeItem.Text = "Exclude Item from Chart"
        '
        'mnuShowCaptions
        '
        Me.mnuShowCaptions.Name = "mnuShowCaptions"
        Me.mnuShowCaptions.Size = New System.Drawing.Size(177, 22)
        Me.mnuShowCaptions.Text = "Show Event Captions"
        '
        'mnuClusterDropDown
        '
        Me.mnuClusterDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuClusterAddVPL, Me.mnuClusterAddToPathway})
        Me.mnuClusterDropDown.Name = "mnuChartDropDown"
        Me.mnuClusterDropDown.ShowImageMargin = False
        Me.mnuClusterDropDown.Size = New System.Drawing.Size(191, 48)
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
        'mnuPathwayMapDropdown2
        '
        Me.mnuPathwayMapDropdown2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShowCaptions2})
        Me.mnuPathwayMapDropdown2.Name = "mnuPathwayMapDropdown2"
        Me.mnuPathwayMapDropdown2.ShowCheckMargin = True
        Me.mnuPathwayMapDropdown2.ShowImageMargin = False
        Me.mnuPathwayMapDropdown2.Size = New System.Drawing.Size(188, 26)
        '
        'mnuShowCaptions2
        '
        Me.mnuShowCaptions2.CheckOnClick = True
        Me.mnuShowCaptions2.Name = "mnuShowCaptions2"
        Me.mnuShowCaptions2.Size = New System.Drawing.Size(187, 22)
        Me.mnuShowCaptions2.Text = "Show Event Captions"
        '
        'grpBoxOptions
        '
        Me.grpBoxOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpBoxOptions.Controls.Add(Me.chShowOtherPlay)
        Me.grpBoxOptions.Controls.Add(Me.chkShowPossession)
        Me.grpBoxOptions.Controls.Add(Me.numPathOpacity)
        Me.grpBoxOptions.Controls.Add(Me.Label2)
        Me.grpBoxOptions.Controls.Add(Me.chkShowDeliveries)
        Me.grpBoxOptions.Controls.Add(Me.chkShowReceives)
        Me.grpBoxOptions.Location = New System.Drawing.Point(378, 418)
        Me.grpBoxOptions.Name = "grpBoxOptions"
        Me.grpBoxOptions.Size = New System.Drawing.Size(260, 93)
        Me.grpBoxOptions.TabIndex = 4
        Me.grpBoxOptions.TabStop = False
        Me.grpBoxOptions.Text = "Player Possession Path Options"
        Me.grpBoxOptions.Visible = False
        '
        'chShowOtherPlay
        '
        Me.chShowOtherPlay.AutoSize = True
        Me.chShowOtherPlay.Location = New System.Drawing.Point(151, 66)
        Me.chShowOtherPlay.Name = "chShowOtherPlay"
        Me.chShowOtherPlay.Size = New System.Drawing.Size(105, 17)
        Me.chShowOtherPlay.TabIndex = 5
        Me.chShowOtherPlay.Text = "Show Other Play"
        Me.chShowOtherPlay.UseVisualStyleBackColor = True
        '
        'chkShowPossession
        '
        Me.chkShowPossession.AutoSize = True
        Me.chkShowPossession.Checked = True
        Me.chkShowPossession.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowPossession.Location = New System.Drawing.Point(9, 43)
        Me.chkShowPossession.Name = "chkShowPossession"
        Me.chkShowPossession.Size = New System.Drawing.Size(141, 17)
        Me.chkShowPossession.TabIndex = 4
        Me.chkShowPossession.Text = "Show Player Possession"
        Me.chkShowPossession.UseVisualStyleBackColor = True
        '
        'numPathOpacity
        '
        Me.numPathOpacity.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numPathOpacity.Location = New System.Drawing.Point(155, 37)
        Me.numPathOpacity.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numPathOpacity.Name = "numPathOpacity"
        Me.numPathOpacity.Size = New System.Drawing.Size(65, 20)
        Me.numPathOpacity.TabIndex = 3
        Me.numPathOpacity.Value = New Decimal(New Integer() {155, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(152, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pathway Opacity"
        '
        'chkShowDeliveries
        '
        Me.chkShowDeliveries.AutoSize = True
        Me.chkShowDeliveries.Checked = True
        Me.chkShowDeliveries.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowDeliveries.Location = New System.Drawing.Point(9, 66)
        Me.chkShowDeliveries.Name = "chkShowDeliveries"
        Me.chkShowDeliveries.Size = New System.Drawing.Size(102, 17)
        Me.chkShowDeliveries.TabIndex = 1
        Me.chkShowDeliveries.Text = "Show Deliveries"
        Me.chkShowDeliveries.UseVisualStyleBackColor = True
        '
        'chkShowReceives
        '
        Me.chkShowReceives.AutoSize = True
        Me.chkShowReceives.Checked = True
        Me.chkShowReceives.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowReceives.Location = New System.Drawing.Point(9, 20)
        Me.chkShowReceives.Name = "chkShowReceives"
        Me.chkShowReceives.Size = New System.Drawing.Size(101, 17)
        Me.chkShowReceives.TabIndex = 0
        Me.chkShowReceives.Text = "Show Receives"
        Me.chkShowReceives.UseVisualStyleBackColor = True
        '
        'mnuScatterPlotDropDown
        '
        Me.mnuScatterPlotDropDown.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScatterPlayVideo, Me.mnuScatterItem2VPL, Me.mnuScatterItem2PathMap, Me.ToolStripSeparator2, Me.mnuScatterChangeColor})
        Me.mnuScatterPlotDropDown.Name = "mnuChartDropDown"
        Me.mnuScatterPlotDropDown.ShowImageMargin = False
        Me.mnuScatterPlotDropDown.Size = New System.Drawing.Size(186, 98)
        '
        'mnuScatterPlayVideo
        '
        Me.mnuScatterPlayVideo.Name = "mnuScatterPlayVideo"
        Me.mnuScatterPlayVideo.Size = New System.Drawing.Size(185, 22)
        Me.mnuScatterPlayVideo.Text = "Play Item Video"
        '
        'mnuScatterItem2VPL
        '
        Me.mnuScatterItem2VPL.Name = "mnuScatterItem2VPL"
        Me.mnuScatterItem2VPL.Size = New System.Drawing.Size(185, 22)
        Me.mnuScatterItem2VPL.Text = "Add Item to VPL"
        '
        'mnuScatterItem2PathMap
        '
        Me.mnuScatterItem2PathMap.Name = "mnuScatterItem2PathMap"
        Me.mnuScatterItem2PathMap.Size = New System.Drawing.Size(185, 22)
        Me.mnuScatterItem2PathMap.Text = "Add Item to Pathway Map"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(182, 6)
        '
        'mnuScatterChangeColor
        '
        Me.mnuScatterChangeColor.Name = "mnuScatterChangeColor"
        Me.mnuScatterChangeColor.Size = New System.Drawing.Size(185, 22)
        Me.mnuScatterChangeColor.Text = "Change Cluster Color"
        '
        'picPitch
        '
        Me.picPitch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picPitch.BackColor = System.Drawing.Color.Gainsboro
        Me.picPitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPitch.Location = New System.Drawing.Point(13, 12)
        Me.picPitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picPitch.Name = "picPitch"
        Me.picPitch.Size = New System.Drawing.Size(358, 556)
        Me.picPitch.TabIndex = 1
        Me.picPitch.TabStop = False
        '
        'frmChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 580)
        Me.Controls.Add(Me.picPitch)
        Me.Controls.Add(Me.grpBoxOptions)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmChart"
        Me.Text = "Game Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.mnuPathwayMapDropDown.ResumeLayout(False)
        Me.mnuClusterDropDown.ResumeLayout(False)
        Me.mnuPathwayMapDropdown2.ResumeLayout(False)
        Me.grpBoxOptions.ResumeLayout(False)
        Me.grpBoxOptions.PerformLayout()
        CType(Me.numPathOpacity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuScatterPlotDropDown.ResumeLayout(False)
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picPitch As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
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
    Friend WithEvents mnuPathwayMapDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAddItem2VPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayItemVideo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExcludeItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClusterDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuClusterAddVPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClusterAddToPathway As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowCaptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPathwayMapDropdown2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuShowCaptions2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpBoxOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowReceives As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDeliveries As System.Windows.Forms.CheckBox
    Friend WithEvents numPathOpacity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkShowPossession As System.Windows.Forms.CheckBox
    Friend WithEvents chShowOtherPlay As System.Windows.Forms.CheckBox
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdPDF As System.Windows.Forms.Button
    Friend WithEvents mnuScatterPlotDropDown As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuScatterItem2VPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScatterItem2PathMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScatterPlayVideo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuScatterChangeColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdPublishChart As System.Windows.Forms.Button
End Class
