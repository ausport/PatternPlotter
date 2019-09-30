<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Apply_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OptionsTab = New System.Windows.Forms.TabControl
        Me.tabVideo = New System.Windows.Forms.TabPage
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.chkAddFades = New System.Windows.Forms.CheckBox
        Me.chkAddSlowMotion = New System.Windows.Forms.CheckBox
        Me.chkShowContinuousPlaylist = New System.Windows.Forms.CheckBox
        Me.chkStopAtEndOfClip = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.cboLagTime = New System.Windows.Forms.NumericUpDown
        Me.Label18 = New System.Windows.Forms.Label
        Me.cboLeadTime = New System.Windows.Forms.NumericUpDown
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdBrowseCaptureDir = New System.Windows.Forms.Button
        Me.lblDefaultCaptureDir = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdSetPreviewQuality = New System.Windows.Forms.Button
        Me.cboVideoDecoder = New System.Windows.Forms.ComboBox
        Me.chkPreviewAudio = New System.Windows.Forms.CheckBox
        Me.chkAutoPlayOnCapture = New System.Windows.Forms.CheckBox
        Me.tabPaths = New System.Windows.Forms.TabPage
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.lstPaths = New System.Windows.Forms.ListBox
        Me.chkUseAlternativePaths = New System.Windows.Forms.CheckBox
        Me.cmdRemovePath = New System.Windows.Forms.Button
        Me.cmdAddPath = New System.Windows.Forms.Button
        Me.tabDatabase = New System.Windows.Forms.TabPage
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.lblDBStatus = New System.Windows.Forms.Label
        Me.cmdUpdateDB = New System.Windows.Forms.Button
        Me.cmdBrowseDB = New System.Windows.Forms.Button
        Me.lblDBLocation = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkCacheAllData = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkAutoUpdateDB = New System.Windows.Forms.CheckBox
        Me.tabCharting = New System.Windows.Forms.TabPage
        Me.grpPathWays = New System.Windows.Forms.GroupBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.numDifferentiationThreshold = New System.Windows.Forms.NumericUpDown
        Me.cboMirror_End = New System.Windows.Forms.ComboBox
        Me.cboMirror_Start = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cboLineTension = New System.Windows.Forms.NumericUpDown
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboLineWidth = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.tabReports = New System.Windows.Forms.TabPage
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.chkShowHeatBallMovements = New System.Windows.Forms.CheckBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.cboVerticalQ = New System.Windows.Forms.NumericUpDown
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboHorizontalQ = New System.Windows.Forms.NumericUpDown
        Me.tabStatistics = New System.Windows.Forms.TabPage
        Me.chkStatsShowTotals = New System.Windows.Forms.CheckBox
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.optStatsByTeam = New System.Windows.Forms.RadioButton
        Me.optStatsByGame = New System.Windows.Forms.RadioButton
        Me.chkIncludeDescriptors = New System.Windows.Forms.CheckBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.lstStatRegions = New System.Windows.Forms.CheckedListBox
        Me.tabGame = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.chkSingleClickPass = New System.Windows.Forms.CheckBox
        Me.chkAutoReload = New System.Windows.Forms.CheckBox
        Me.tabPitch = New System.Windows.Forms.TabPage
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkSetSportDefault = New System.Windows.Forms.CheckBox
        Me.cboSport = New System.Windows.Forms.ComboBox
        Me.boxPitch = New System.Windows.Forms.GroupBox
        Me.cboMirror_Perimeter = New System.Windows.Forms.ComboBox
        Me.cboMirror_Lines = New System.Windows.Forms.ComboBox
        Me.cboMirror_Highlights = New System.Windows.Forms.ComboBox
        Me.cboMirror_Field = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.tabVideoTransmit = New System.Windows.Forms.TabPage
        Me.GroupBox17 = New System.Windows.Forms.GroupBox
        Me.chkiPhoneIncludeStats = New System.Windows.Forms.CheckBox
        Me.chkiPhonePlotByTime = New System.Windows.Forms.CheckBox
        Me.chkiPhonePlotByTeam = New System.Windows.Forms.CheckBox
        Me.chkEnableiPhone = New System.Windows.Forms.CheckBox
        Me.grpXML = New System.Windows.Forms.GroupBox
        Me.txtXMLUserName = New System.Windows.Forms.TextBox
        Me.txtXMLPassword = New System.Windows.Forms.TextBox
        Me.txtXMLURL = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.tabRemoteCamera = New System.Windows.Forms.TabPage
        Me.cmdPing = New System.Windows.Forms.Button
        Me.lblConnectionStatus = New System.Windows.Forms.Label
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cboCameraResolution = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.lblTempFiles = New System.Windows.Forms.TextBox
        Me.cmdSetTempDir = New System.Windows.Forms.Button
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtAutoSearch = New System.Windows.Forms.TextBox
        Me.cmdBrowseAutoSearch = New System.Windows.Forms.Button
        Me.chkShowChartOnActive = New System.Windows.Forms.CheckBox
        Me.numUpdateFrequency = New System.Windows.Forms.NumericUpDown
        Me.Label19 = New System.Windows.Forms.Label
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.numVideoEpoch = New System.Windows.Forms.NumericUpDown
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.lblVideoTransmitDestination = New System.Windows.Forms.TextBox
        Me.lblVideoTransmitDirectoryStatus = New System.Windows.Forms.Label
        Me.cmdCheckVidoTransmitDirectoryLink = New System.Windows.Forms.Button
        Me.cmdSetTransmitDestination = New System.Windows.Forms.Button
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.TableLayoutPanel1.SuspendLayout()
        Me.OptionsTab.SuspendLayout()
        Me.tabVideo.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cboLagTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLeadTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tabPaths.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.tabDatabase.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.tabCharting.SuspendLayout()
        Me.grpPathWays.SuspendLayout()
        CType(Me.numDifferentiationThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLineTension, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabReports.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        CType(Me.cboVerticalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboHorizontalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabStatistics.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.tabGame.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.tabPitch.SuspendLayout()
        Me.boxPitch.SuspendLayout()
        Me.tabVideoTransmit.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.grpXML.SuspendLayout()
        Me.tabRemoteCamera.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        CType(Me.numUpdateFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        CType(Me.numVideoEpoch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.Apply_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(208, 296)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(219, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Apply_Button
        '
        Me.Apply_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Apply_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Apply_Button.Location = New System.Drawing.Point(76, 3)
        Me.Apply_Button.Name = "Apply_Button"
        Me.Apply_Button.Size = New System.Drawing.Size(67, 23)
        Me.Apply_Button.TabIndex = 3
        Me.Apply_Button.Text = "Apply"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(149, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OptionsTab
        '
        Me.OptionsTab.Controls.Add(Me.tabVideo)
        Me.OptionsTab.Controls.Add(Me.tabPaths)
        Me.OptionsTab.Controls.Add(Me.tabDatabase)
        Me.OptionsTab.Controls.Add(Me.tabCharting)
        Me.OptionsTab.Controls.Add(Me.tabReports)
        Me.OptionsTab.Controls.Add(Me.tabStatistics)
        Me.OptionsTab.Controls.Add(Me.tabGame)
        Me.OptionsTab.Controls.Add(Me.tabPitch)
        Me.OptionsTab.Controls.Add(Me.tabVideoTransmit)
        Me.OptionsTab.Controls.Add(Me.tabRemoteCamera)
        Me.OptionsTab.Location = New System.Drawing.Point(12, 12)
        Me.OptionsTab.Name = "OptionsTab"
        Me.OptionsTab.SelectedIndex = 0
        Me.OptionsTab.Size = New System.Drawing.Size(418, 277)
        Me.OptionsTab.TabIndex = 3
        '
        'tabVideo
        '
        Me.tabVideo.Controls.Add(Me.GroupBox11)
        Me.tabVideo.Controls.Add(Me.GroupBox2)
        Me.tabVideo.Controls.Add(Me.GroupBox1)
        Me.tabVideo.Location = New System.Drawing.Point(4, 22)
        Me.tabVideo.Name = "tabVideo"
        Me.tabVideo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabVideo.Size = New System.Drawing.Size(410, 251)
        Me.tabVideo.TabIndex = 0
        Me.tabVideo.Text = "Video"
        Me.tabVideo.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.chkAddFades)
        Me.GroupBox11.Controls.Add(Me.chkAddSlowMotion)
        Me.GroupBox11.Controls.Add(Me.chkShowContinuousPlaylist)
        Me.GroupBox11.Controls.Add(Me.chkStopAtEndOfClip)
        Me.GroupBox11.Location = New System.Drawing.Point(196, 137)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(208, 108)
        Me.GroupBox11.TabIndex = 4
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Video Playlist Settings"
        '
        'chkAddFades
        '
        Me.chkAddFades.AutoSize = True
        Me.chkAddFades.Location = New System.Drawing.Point(22, 85)
        Me.chkAddFades.Name = "chkAddFades"
        Me.chkAddFades.Size = New System.Drawing.Size(138, 17)
        Me.chkAddFades.TabIndex = 6
        Me.chkAddFades.Text = "Add Fade In Transitions"
        Me.chkAddFades.UseVisualStyleBackColor = True
        '
        'chkAddSlowMotion
        '
        Me.chkAddSlowMotion.AutoSize = True
        Me.chkAddSlowMotion.Location = New System.Drawing.Point(22, 63)
        Me.chkAddSlowMotion.Name = "chkAddSlowMotion"
        Me.chkAddSlowMotion.Size = New System.Drawing.Size(159, 17)
        Me.chkAddSlowMotion.TabIndex = 5
        Me.chkAddSlowMotion.Text = "Add Slow Motion Duplicates"
        Me.chkAddSlowMotion.UseVisualStyleBackColor = True
        '
        'chkShowContinuousPlaylist
        '
        Me.chkShowContinuousPlaylist.AutoSize = True
        Me.chkShowContinuousPlaylist.Location = New System.Drawing.Point(22, 19)
        Me.chkShowContinuousPlaylist.Name = "chkShowContinuousPlaylist"
        Me.chkShowContinuousPlaylist.Size = New System.Drawing.Size(159, 17)
        Me.chkShowContinuousPlaylist.TabIndex = 2
        Me.chkShowContinuousPlaylist.Text = "Auto Jump to Next VPL Item"
        Me.chkShowContinuousPlaylist.UseVisualStyleBackColor = True
        '
        'chkStopAtEndOfClip
        '
        Me.chkStopAtEndOfClip.AutoSize = True
        Me.chkStopAtEndOfClip.Location = New System.Drawing.Point(22, 41)
        Me.chkStopAtEndOfClip.Name = "chkStopAtEndOfClip"
        Me.chkStopAtEndOfClip.Size = New System.Drawing.Size(129, 17)
        Me.chkStopAtEndOfClip.TabIndex = 3
        Me.chkStopAtEndOfClip.Text = "Stop at End of Playlist"
        Me.chkStopAtEndOfClip.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.cboLagTime)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.cboLeadTime)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 137)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(184, 108)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Video Playback Settings"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(19, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 13)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Lag Time:"
        '
        'cboLagTime
        '
        Me.cboLagTime.Location = New System.Drawing.Point(79, 48)
        Me.cboLagTime.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.cboLagTime.Name = "cboLagTime"
        Me.cboLagTime.Size = New System.Drawing.Size(53, 20)
        Me.cboLagTime.TabIndex = 6
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(13, 25)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Lead Time:"
        '
        'cboLeadTime
        '
        Me.cboLeadTime.Location = New System.Drawing.Point(79, 22)
        Me.cboLeadTime.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.cboLeadTime.Name = "cboLeadTime"
        Me.cboLeadTime.Size = New System.Drawing.Size(53, 20)
        Me.cboLeadTime.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdBrowseCaptureDir)
        Me.GroupBox1.Controls.Add(Me.lblDefaultCaptureDir)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmdSetPreviewQuality)
        Me.GroupBox1.Controls.Add(Me.cboVideoDecoder)
        Me.GroupBox1.Controls.Add(Me.chkPreviewAudio)
        Me.GroupBox1.Controls.Add(Me.chkAutoPlayOnCapture)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(398, 125)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Video Capture Settings"
        '
        'cmdBrowseCaptureDir
        '
        Me.cmdBrowseCaptureDir.Location = New System.Drawing.Point(306, 92)
        Me.cmdBrowseCaptureDir.Name = "cmdBrowseCaptureDir"
        Me.cmdBrowseCaptureDir.Size = New System.Drawing.Size(73, 22)
        Me.cmdBrowseCaptureDir.TabIndex = 6
        Me.cmdBrowseCaptureDir.Text = "Browse"
        Me.cmdBrowseCaptureDir.UseVisualStyleBackColor = True
        '
        'lblDefaultCaptureDir
        '
        Me.lblDefaultCaptureDir.AutoEllipsis = True
        Me.lblDefaultCaptureDir.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDefaultCaptureDir.Location = New System.Drawing.Point(10, 97)
        Me.lblDefaultCaptureDir.Name = "lblDefaultCaptureDir"
        Me.lblDefaultCaptureDir.Size = New System.Drawing.Size(290, 17)
        Me.lblDefaultCaptureDir.TabIndex = 5
        Me.lblDefaultCaptureDir.Text = "c:\Video Files"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Default Video Capture Directory"
        '
        'cmdSetPreviewQuality
        '
        Me.cmdSetPreviewQuality.Location = New System.Drawing.Point(6, 46)
        Me.cmdSetPreviewQuality.Name = "cmdSetPreviewQuality"
        Me.cmdSetPreviewQuality.Size = New System.Drawing.Size(159, 22)
        Me.cmdSetPreviewQuality.TabIndex = 3
        Me.cmdSetPreviewQuality.Text = "Set Preview Quality"
        Me.cmdSetPreviewQuality.UseVisualStyleBackColor = True
        '
        'cboVideoDecoder
        '
        Me.cboVideoDecoder.Enabled = False
        Me.cboVideoDecoder.FormattingEnabled = True
        Me.cboVideoDecoder.Items.AddRange(New Object() {"DV Video Decoder"})
        Me.cboVideoDecoder.Location = New System.Drawing.Point(6, 19)
        Me.cboVideoDecoder.Name = "cboVideoDecoder"
        Me.cboVideoDecoder.Size = New System.Drawing.Size(159, 21)
        Me.cboVideoDecoder.TabIndex = 2
        Me.cboVideoDecoder.Text = "DV Video Decoder"
        '
        'chkPreviewAudio
        '
        Me.chkPreviewAudio.AutoSize = True
        Me.chkPreviewAudio.Location = New System.Drawing.Point(196, 42)
        Me.chkPreviewAudio.Name = "chkPreviewAudio"
        Me.chkPreviewAudio.Size = New System.Drawing.Size(94, 17)
        Me.chkPreviewAudio.TabIndex = 1
        Me.chkPreviewAudio.Text = "Preview Audio"
        Me.chkPreviewAudio.UseVisualStyleBackColor = True
        '
        'chkAutoPlayOnCapture
        '
        Me.chkAutoPlayOnCapture.AutoSize = True
        Me.chkAutoPlayOnCapture.Location = New System.Drawing.Point(196, 19)
        Me.chkAutoPlayOnCapture.Name = "chkAutoPlayOnCapture"
        Me.chkAutoPlayOnCapture.Size = New System.Drawing.Size(126, 17)
        Me.chkAutoPlayOnCapture.TabIndex = 0
        Me.chkAutoPlayOnCapture.Text = "Auto Play on Capture"
        Me.chkAutoPlayOnCapture.UseVisualStyleBackColor = True
        '
        'tabPaths
        '
        Me.tabPaths.Controls.Add(Me.GroupBox9)
        Me.tabPaths.Location = New System.Drawing.Point(4, 22)
        Me.tabPaths.Name = "tabPaths"
        Me.tabPaths.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPaths.Size = New System.Drawing.Size(410, 251)
        Me.tabPaths.TabIndex = 7
        Me.tabPaths.Text = "Paths"
        Me.tabPaths.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.lstPaths)
        Me.GroupBox9.Controls.Add(Me.chkUseAlternativePaths)
        Me.GroupBox9.Controls.Add(Me.cmdRemovePath)
        Me.GroupBox9.Controls.Add(Me.cmdAddPath)
        Me.GroupBox9.Location = New System.Drawing.Point(7, 7)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(397, 238)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Select Alternative Video Paths"
        '
        'lstPaths
        '
        Me.lstPaths.FormattingEnabled = True
        Me.lstPaths.Location = New System.Drawing.Point(7, 50)
        Me.lstPaths.Name = "lstPaths"
        Me.lstPaths.Size = New System.Drawing.Size(384, 160)
        Me.lstPaths.TabIndex = 4
        '
        'chkUseAlternativePaths
        '
        Me.chkUseAlternativePaths.AutoSize = True
        Me.chkUseAlternativePaths.Location = New System.Drawing.Point(7, 215)
        Me.chkUseAlternativePaths.Name = "chkUseAlternativePaths"
        Me.chkUseAlternativePaths.Size = New System.Drawing.Size(219, 17)
        Me.chkUseAlternativePaths.TabIndex = 3
        Me.chkUseAlternativePaths.Text = "Search for video using alternative paths?"
        Me.chkUseAlternativePaths.UseVisualStyleBackColor = True
        '
        'cmdRemovePath
        '
        Me.cmdRemovePath.Location = New System.Drawing.Point(108, 19)
        Me.cmdRemovePath.Name = "cmdRemovePath"
        Me.cmdRemovePath.Size = New System.Drawing.Size(95, 23)
        Me.cmdRemovePath.TabIndex = 1
        Me.cmdRemovePath.Text = "Remove Path"
        Me.cmdRemovePath.UseVisualStyleBackColor = True
        '
        'cmdAddPath
        '
        Me.cmdAddPath.Location = New System.Drawing.Point(7, 20)
        Me.cmdAddPath.Name = "cmdAddPath"
        Me.cmdAddPath.Size = New System.Drawing.Size(95, 23)
        Me.cmdAddPath.TabIndex = 0
        Me.cmdAddPath.Text = "+ Add Path"
        Me.cmdAddPath.UseVisualStyleBackColor = True
        '
        'tabDatabase
        '
        Me.tabDatabase.Controls.Add(Me.GroupBox6)
        Me.tabDatabase.Controls.Add(Me.GroupBox4)
        Me.tabDatabase.Controls.Add(Me.GroupBox3)
        Me.tabDatabase.Location = New System.Drawing.Point(4, 22)
        Me.tabDatabase.Name = "tabDatabase"
        Me.tabDatabase.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDatabase.Size = New System.Drawing.Size(410, 251)
        Me.tabDatabase.TabIndex = 1
        Me.tabDatabase.Text = "Database"
        Me.tabDatabase.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblDBStatus)
        Me.GroupBox6.Controls.Add(Me.cmdUpdateDB)
        Me.GroupBox6.Controls.Add(Me.cmdBrowseDB)
        Me.GroupBox6.Controls.Add(Me.lblDBLocation)
        Me.GroupBox6.Location = New System.Drawing.Point(13, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(369, 76)
        Me.GroupBox6.TabIndex = 12
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Database Location"
        '
        'lblDBStatus
        '
        Me.lblDBStatus.AutoSize = True
        Me.lblDBStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblDBStatus.Location = New System.Drawing.Point(104, 49)
        Me.lblDBStatus.Name = "lblDBStatus"
        Me.lblDBStatus.Size = New System.Drawing.Size(138, 13)
        Me.lblDBStatus.TabIndex = 11
        Me.lblDBStatus.Text = "This database is up to date."
        '
        'cmdUpdateDB
        '
        Me.cmdUpdateDB.Enabled = False
        Me.cmdUpdateDB.Location = New System.Drawing.Point(6, 44)
        Me.cmdUpdateDB.Name = "cmdUpdateDB"
        Me.cmdUpdateDB.Size = New System.Drawing.Size(92, 22)
        Me.cmdUpdateDB.TabIndex = 10
        Me.cmdUpdateDB.Text = "Update Version"
        Me.cmdUpdateDB.UseVisualStyleBackColor = True
        '
        'cmdBrowseDB
        '
        Me.cmdBrowseDB.Location = New System.Drawing.Point(290, 16)
        Me.cmdBrowseDB.Name = "cmdBrowseDB"
        Me.cmdBrowseDB.Size = New System.Drawing.Size(73, 22)
        Me.cmdBrowseDB.TabIndex = 9
        Me.cmdBrowseDB.Text = "Browse"
        Me.cmdBrowseDB.UseVisualStyleBackColor = True
        '
        'lblDBLocation
        '
        Me.lblDBLocation.AutoEllipsis = True
        Me.lblDBLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDBLocation.Location = New System.Drawing.Point(6, 16)
        Me.lblDBLocation.Name = "lblDBLocation"
        Me.lblDBLocation.Size = New System.Drawing.Size(278, 18)
        Me.lblDBLocation.TabIndex = 8
        Me.lblDBLocation.Text = "c:\Video Files"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.chkCacheAllData)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 140)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(369, 96)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Data Cache"
        Me.GroupBox4.Visible = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label1.Location = New System.Drawing.Point(3, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(360, 57)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'chkCacheAllData
        '
        Me.chkCacheAllData.AutoSize = True
        Me.chkCacheAllData.Location = New System.Drawing.Point(16, 19)
        Me.chkCacheAllData.Name = "chkCacheAllData"
        Me.chkCacheAllData.Size = New System.Drawing.Size(203, 17)
        Me.chkCacheAllData.TabIndex = 0
        Me.chkCacheAllData.Text = "Cache All Data when Loading Games"
        Me.chkCacheAllData.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkAutoUpdateDB)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 88)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(369, 46)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Timeline Properties"
        Me.GroupBox3.Visible = False
        '
        'chkAutoUpdateDB
        '
        Me.chkAutoUpdateDB.AutoSize = True
        Me.chkAutoUpdateDB.Location = New System.Drawing.Point(16, 19)
        Me.chkAutoUpdateDB.Name = "chkAutoUpdateDB"
        Me.chkAutoUpdateDB.Size = New System.Drawing.Size(274, 17)
        Me.chkAutoUpdateDB.TabIndex = 0
        Me.chkAutoUpdateDB.Text = "Automatically Update Timeline Changes to Database"
        Me.chkAutoUpdateDB.UseVisualStyleBackColor = True
        '
        'tabCharting
        '
        Me.tabCharting.Controls.Add(Me.grpPathWays)
        Me.tabCharting.Location = New System.Drawing.Point(4, 22)
        Me.tabCharting.Name = "tabCharting"
        Me.tabCharting.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCharting.Size = New System.Drawing.Size(410, 251)
        Me.tabCharting.TabIndex = 5
        Me.tabCharting.Text = "Charting"
        Me.tabCharting.UseVisualStyleBackColor = True
        '
        'grpPathWays
        '
        Me.grpPathWays.Controls.Add(Me.Label22)
        Me.grpPathWays.Controls.Add(Me.Label21)
        Me.grpPathWays.Controls.Add(Me.numDifferentiationThreshold)
        Me.grpPathWays.Controls.Add(Me.cboMirror_End)
        Me.grpPathWays.Controls.Add(Me.cboMirror_Start)
        Me.grpPathWays.Controls.Add(Me.Label12)
        Me.grpPathWays.Controls.Add(Me.Label13)
        Me.grpPathWays.Controls.Add(Me.cboLineTension)
        Me.grpPathWays.Controls.Add(Me.Label11)
        Me.grpPathWays.Controls.Add(Me.cboLineWidth)
        Me.grpPathWays.Controls.Add(Me.Label2)
        Me.grpPathWays.Location = New System.Drawing.Point(7, 7)
        Me.grpPathWays.Name = "grpPathWays"
        Me.grpPathWays.Size = New System.Drawing.Size(397, 238)
        Me.grpPathWays.TabIndex = 0
        Me.grpPathWays.TabStop = False
        Me.grpPathWays.Text = "Pathway Maps"
        '
        'Label22
        '
        Me.Label22.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label22.Location = New System.Drawing.Point(6, 159)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(221, 44)
        Me.Label22.TabIndex = 11
        Me.Label22.Text = "NB: Large data sets can be slow if lines differentiate passes and carries.  Reduc" & _
            "e threshold if experiencing poor performance."
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(2, 138)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(107, 13)
        Me.Label21.TabIndex = 10
        Me.Label21.Text = "Line Type Threshold:"
        '
        'numDifferentiationThreshold
        '
        Me.numDifferentiationThreshold.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numDifferentiationThreshold.Location = New System.Drawing.Point(115, 136)
        Me.numDifferentiationThreshold.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numDifferentiationThreshold.Name = "numDifferentiationThreshold"
        Me.numDifferentiationThreshold.Size = New System.Drawing.Size(49, 20)
        Me.numDifferentiationThreshold.TabIndex = 9
        Me.numDifferentiationThreshold.ThousandsSeparator = True
        Me.numDifferentiationThreshold.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'cboMirror_End
        '
        Me.cboMirror_End.Location = New System.Drawing.Point(171, 94)
        Me.cboMirror_End.Name = "cboMirror_End"
        Me.cboMirror_End.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_End.TabIndex = 8
        Me.cboMirror_End.Visible = False
        '
        'cboMirror_Start
        '
        Me.cboMirror_Start.Location = New System.Drawing.Point(9, 94)
        Me.cboMirror_Start.Name = "cboMirror_Start"
        Me.cboMirror_Start.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_Start.TabIndex = 7
        Me.cboMirror_Start.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(168, 78)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "End Color"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 78)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 13)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Start Color"
        '
        'cboLineTension
        '
        Me.cboLineTension.DecimalPlaces = 2
        Me.cboLineTension.Increment = New Decimal(New Integer() {5, 0, 0, 131072})
        Me.cboLineTension.Location = New System.Drawing.Point(83, 45)
        Me.cboLineTension.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cboLineTension.Name = "cboLineTension"
        Me.cboLineTension.Size = New System.Drawing.Size(53, 20)
        Me.cboLineTension.TabIndex = 3
        Me.cboLineTension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 47)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Line Tension:"
        '
        'cboLineWidth
        '
        Me.cboLineWidth.DecimalPlaces = 1
        Me.cboLineWidth.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.cboLineWidth.Location = New System.Drawing.Point(83, 19)
        Me.cboLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.cboLineWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cboLineWidth.Name = "cboLineWidth"
        Me.cboLineWidth.Size = New System.Drawing.Size(53, 20)
        Me.cboLineWidth.TabIndex = 1
        Me.cboLineWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cboLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Line Width:"
        '
        'tabReports
        '
        Me.tabReports.Controls.Add(Me.GroupBox15)
        Me.tabReports.Controls.Add(Me.GroupBox10)
        Me.tabReports.Location = New System.Drawing.Point(4, 22)
        Me.tabReports.Name = "tabReports"
        Me.tabReports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabReports.Size = New System.Drawing.Size(410, 251)
        Me.tabReports.TabIndex = 9
        Me.tabReports.Text = "Reports"
        Me.tabReports.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.chkShowHeatBallMovements)
        Me.GroupBox15.Location = New System.Drawing.Point(7, 65)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(397, 84)
        Me.GroupBox15.TabIndex = 3
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Heat Maps"
        '
        'chkShowHeatBallMovements
        '
        Me.chkShowHeatBallMovements.AutoSize = True
        Me.chkShowHeatBallMovements.Location = New System.Drawing.Point(17, 20)
        Me.chkShowHeatBallMovements.Name = "chkShowHeatBallMovements"
        Me.chkShowHeatBallMovements.Size = New System.Drawing.Size(211, 17)
        Me.chkShowHeatBallMovements.TabIndex = 0
        Me.chkShowHeatBallMovements.Text = "Show All Ball Movements in Heat Maps"
        Me.chkShowHeatBallMovements.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.Label16)
        Me.GroupBox10.Controls.Add(Me.cboVerticalQ)
        Me.GroupBox10.Controls.Add(Me.Label15)
        Me.GroupBox10.Controls.Add(Me.cboHorizontalQ)
        Me.GroupBox10.Location = New System.Drawing.Point(4, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(400, 52)
        Me.GroupBox10.TabIndex = 2
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Outcome Clusters"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(205, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Vertical Quadrants:"
        '
        'cboVerticalQ
        '
        Me.cboVerticalQ.Location = New System.Drawing.Point(308, 19)
        Me.cboVerticalQ.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cboVerticalQ.Name = "cboVerticalQ"
        Me.cboVerticalQ.Size = New System.Drawing.Size(53, 20)
        Me.cboVerticalQ.TabIndex = 2
        Me.cboVerticalQ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(17, 21)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 13)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Horizontal Quadrants:"
        '
        'cboHorizontalQ
        '
        Me.cboHorizontalQ.Location = New System.Drawing.Point(132, 19)
        Me.cboHorizontalQ.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cboHorizontalQ.Name = "cboHorizontalQ"
        Me.cboHorizontalQ.Size = New System.Drawing.Size(53, 20)
        Me.cboHorizontalQ.TabIndex = 0
        Me.cboHorizontalQ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'tabStatistics
        '
        Me.tabStatistics.Controls.Add(Me.chkStatsShowTotals)
        Me.tabStatistics.Controls.Add(Me.GroupBox14)
        Me.tabStatistics.Controls.Add(Me.chkIncludeDescriptors)
        Me.tabStatistics.Controls.Add(Me.GroupBox13)
        Me.tabStatistics.Location = New System.Drawing.Point(4, 22)
        Me.tabStatistics.Name = "tabStatistics"
        Me.tabStatistics.Padding = New System.Windows.Forms.Padding(3)
        Me.tabStatistics.Size = New System.Drawing.Size(410, 251)
        Me.tabStatistics.TabIndex = 8
        Me.tabStatistics.Text = "Statistics"
        Me.tabStatistics.UseVisualStyleBackColor = True
        '
        'chkStatsShowTotals
        '
        Me.chkStatsShowTotals.AutoSize = True
        Me.chkStatsShowTotals.Location = New System.Drawing.Point(240, 106)
        Me.chkStatsShowTotals.Name = "chkStatsShowTotals"
        Me.chkStatsShowTotals.Size = New System.Drawing.Size(117, 17)
        Me.chkStatsShowTotals.TabIndex = 4
        Me.chkStatsShowTotals.Text = "Show Group Totals"
        Me.chkStatsShowTotals.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.optStatsByTeam)
        Me.GroupBox14.Controls.Add(Me.optStatsByGame)
        Me.GroupBox14.Location = New System.Drawing.Point(223, 6)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(181, 71)
        Me.GroupBox14.TabIndex = 3
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Default Layout"
        '
        'optStatsByTeam
        '
        Me.optStatsByTeam.AutoSize = True
        Me.optStatsByTeam.Location = New System.Drawing.Point(17, 42)
        Me.optStatsByTeam.Name = "optStatsByTeam"
        Me.optStatsByTeam.Size = New System.Drawing.Size(127, 17)
        Me.optStatsByTeam.TabIndex = 3
        Me.optStatsByTeam.TabStop = True
        Me.optStatsByTeam.Text = "Show by Team Name"
        Me.optStatsByTeam.UseVisualStyleBackColor = True
        '
        'optStatsByGame
        '
        Me.optStatsByGame.AutoSize = True
        Me.optStatsByGame.Location = New System.Drawing.Point(17, 19)
        Me.optStatsByGame.Name = "optStatsByGame"
        Me.optStatsByGame.Size = New System.Drawing.Size(111, 17)
        Me.optStatsByGame.TabIndex = 2
        Me.optStatsByGame.TabStop = True
        Me.optStatsByGame.Text = "Show by Game ID"
        Me.optStatsByGame.UseVisualStyleBackColor = True
        '
        'chkIncludeDescriptors
        '
        Me.chkIncludeDescriptors.AutoSize = True
        Me.chkIncludeDescriptors.Location = New System.Drawing.Point(240, 83)
        Me.chkIncludeDescriptors.Name = "chkIncludeDescriptors"
        Me.chkIncludeDescriptors.Size = New System.Drawing.Size(117, 17)
        Me.chkIncludeDescriptors.TabIndex = 1
        Me.chkIncludeDescriptors.Text = "Include Descriptors"
        Me.chkIncludeDescriptors.UseVisualStyleBackColor = True
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.lstStatRegions)
        Me.GroupBox13.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(210, 231)
        Me.GroupBox13.TabIndex = 0
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Included Regions in Statistics"
        '
        'lstStatRegions
        '
        Me.lstStatRegions.FormattingEnabled = True
        Me.lstStatRegions.Location = New System.Drawing.Point(7, 20)
        Me.lstStatRegions.Name = "lstStatRegions"
        Me.lstStatRegions.Size = New System.Drawing.Size(197, 199)
        Me.lstStatRegions.TabIndex = 0
        '
        'tabGame
        '
        Me.tabGame.Controls.Add(Me.GroupBox5)
        Me.tabGame.Location = New System.Drawing.Point(4, 22)
        Me.tabGame.Name = "tabGame"
        Me.tabGame.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGame.Size = New System.Drawing.Size(410, 251)
        Me.tabGame.TabIndex = 3
        Me.tabGame.Text = "Data Entry"
        Me.tabGame.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkSingleClickPass)
        Me.GroupBox5.Controls.Add(Me.chkAutoReload)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(398, 85)
        Me.GroupBox5.TabIndex = 11
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Game Management"
        '
        'chkSingleClickPass
        '
        Me.chkSingleClickPass.AutoSize = True
        Me.chkSingleClickPass.Location = New System.Drawing.Point(16, 51)
        Me.chkSingleClickPass.Name = "chkSingleClickPass"
        Me.chkSingleClickPass.Size = New System.Drawing.Size(160, 17)
        Me.chkSingleClickPass.TabIndex = 1
        Me.chkSingleClickPass.Text = "Single-Click Sets a Ball Pass"
        Me.chkSingleClickPass.UseVisualStyleBackColor = True
        '
        'chkAutoReload
        '
        Me.chkAutoReload.AutoSize = True
        Me.chkAutoReload.Location = New System.Drawing.Point(16, 28)
        Me.chkAutoReload.Name = "chkAutoReload"
        Me.chkAutoReload.Size = New System.Drawing.Size(223, 17)
        Me.chkAutoReload.TabIndex = 0
        Me.chkAutoReload.Text = "Automatically Reload Game at Completion"
        Me.chkAutoReload.UseVisualStyleBackColor = True
        '
        'tabPitch
        '
        Me.tabPitch.Controls.Add(Me.Label4)
        Me.tabPitch.Controls.Add(Me.chkSetSportDefault)
        Me.tabPitch.Controls.Add(Me.cboSport)
        Me.tabPitch.Controls.Add(Me.boxPitch)
        Me.tabPitch.Location = New System.Drawing.Point(4, 22)
        Me.tabPitch.Name = "tabPitch"
        Me.tabPitch.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPitch.Size = New System.Drawing.Size(410, 251)
        Me.tabPitch.TabIndex = 2
        Me.tabPitch.Text = "Playing Field"
        Me.tabPitch.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Set Sport Type"
        '
        'chkSetSportDefault
        '
        Me.chkSetSportDefault.AutoSize = True
        Me.chkSetSportDefault.Location = New System.Drawing.Point(176, 29)
        Me.chkSetSportDefault.Name = "chkSetSportDefault"
        Me.chkSetSportDefault.Size = New System.Drawing.Size(91, 17)
        Me.chkSetSportDefault.TabIndex = 1
        Me.chkSetSportDefault.Text = "Set as default"
        Me.chkSetSportDefault.UseVisualStyleBackColor = True
        '
        'cboSport
        '
        Me.cboSport.FormattingEnabled = True
        Me.cboSport.Location = New System.Drawing.Point(6, 25)
        Me.cboSport.Name = "cboSport"
        Me.cboSport.Size = New System.Drawing.Size(164, 21)
        Me.cboSport.Sorted = True
        Me.cboSport.TabIndex = 0
        '
        'boxPitch
        '
        Me.boxPitch.Controls.Add(Me.cboMirror_Perimeter)
        Me.boxPitch.Controls.Add(Me.cboMirror_Lines)
        Me.boxPitch.Controls.Add(Me.cboMirror_Highlights)
        Me.boxPitch.Controls.Add(Me.cboMirror_Field)
        Me.boxPitch.Controls.Add(Me.Label8)
        Me.boxPitch.Controls.Add(Me.Label7)
        Me.boxPitch.Controls.Add(Me.Label6)
        Me.boxPitch.Controls.Add(Me.Label5)
        Me.boxPitch.Location = New System.Drawing.Point(6, 52)
        Me.boxPitch.Name = "boxPitch"
        Me.boxPitch.Size = New System.Drawing.Size(397, 193)
        Me.boxPitch.TabIndex = 0
        Me.boxPitch.TabStop = False
        Me.boxPitch.Text = "Pitch Setup"
        '
        'cboMirror_Perimeter
        '
        Me.cboMirror_Perimeter.Location = New System.Drawing.Point(14, 152)
        Me.cboMirror_Perimeter.Name = "cboMirror_Perimeter"
        Me.cboMirror_Perimeter.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_Perimeter.TabIndex = 4
        Me.cboMirror_Perimeter.Visible = False
        '
        'cboMirror_Lines
        '
        Me.cboMirror_Lines.Location = New System.Drawing.Point(14, 112)
        Me.cboMirror_Lines.Name = "cboMirror_Lines"
        Me.cboMirror_Lines.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_Lines.TabIndex = 4
        Me.cboMirror_Lines.Visible = False
        '
        'cboMirror_Highlights
        '
        Me.cboMirror_Highlights.Location = New System.Drawing.Point(14, 72)
        Me.cboMirror_Highlights.Name = "cboMirror_Highlights"
        Me.cboMirror_Highlights.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_Highlights.TabIndex = 4
        Me.cboMirror_Highlights.Visible = False
        '
        'cboMirror_Field
        '
        Me.cboMirror_Field.Location = New System.Drawing.Point(14, 32)
        Me.cboMirror_Field.Name = "cboMirror_Field"
        Me.cboMirror_Field.Size = New System.Drawing.Size(129, 21)
        Me.cboMirror_Field.TabIndex = 3
        Me.cboMirror_Field.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Perimeter Color"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Line Color"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Field Highlights"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Field Background"
        '
        'tabVideoTransmit
        '
        Me.tabVideoTransmit.Controls.Add(Me.GroupBox17)
        Me.tabVideoTransmit.Controls.Add(Me.chkEnableiPhone)
        Me.tabVideoTransmit.Controls.Add(Me.grpXML)
        Me.tabVideoTransmit.Location = New System.Drawing.Point(4, 22)
        Me.tabVideoTransmit.Name = "tabVideoTransmit"
        Me.tabVideoTransmit.Padding = New System.Windows.Forms.Padding(3)
        Me.tabVideoTransmit.Size = New System.Drawing.Size(410, 251)
        Me.tabVideoTransmit.TabIndex = 6
        Me.tabVideoTransmit.Text = "iPhone"
        Me.tabVideoTransmit.UseVisualStyleBackColor = True
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.chkiPhoneIncludeStats)
        Me.GroupBox17.Controls.Add(Me.chkiPhonePlotByTime)
        Me.GroupBox17.Controls.Add(Me.chkiPhonePlotByTeam)
        Me.GroupBox17.Location = New System.Drawing.Point(6, 141)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(398, 104)
        Me.GroupBox17.TabIndex = 2
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "XML Chart Data"
        '
        'chkiPhoneIncludeStats
        '
        Me.chkiPhoneIncludeStats.AutoSize = True
        Me.chkiPhoneIncludeStats.Enabled = False
        Me.chkiPhoneIncludeStats.Location = New System.Drawing.Point(16, 65)
        Me.chkiPhoneIncludeStats.Name = "chkiPhoneIncludeStats"
        Me.chkiPhoneIncludeStats.Size = New System.Drawing.Size(137, 17)
        Me.chkiPhoneIncludeStats.TabIndex = 5
        Me.chkiPhoneIncludeStats.Text = "Include Event Statistics"
        Me.chkiPhoneIncludeStats.UseVisualStyleBackColor = True
        '
        'chkiPhonePlotByTime
        '
        Me.chkiPhonePlotByTime.AutoSize = True
        Me.chkiPhonePlotByTime.Location = New System.Drawing.Point(16, 42)
        Me.chkiPhonePlotByTime.Name = "chkiPhonePlotByTime"
        Me.chkiPhonePlotByTime.Size = New System.Drawing.Size(119, 17)
        Me.chkiPhonePlotByTime.TabIndex = 4
        Me.chkiPhonePlotByTime.Text = "Plot by Time Criteria"
        Me.chkiPhonePlotByTime.UseVisualStyleBackColor = True
        '
        'chkiPhonePlotByTeam
        '
        Me.chkiPhonePlotByTeam.AutoSize = True
        Me.chkiPhonePlotByTeam.Location = New System.Drawing.Point(16, 19)
        Me.chkiPhonePlotByTeam.Name = "chkiPhonePlotByTeam"
        Me.chkiPhonePlotByTeam.Size = New System.Drawing.Size(124, 17)
        Me.chkiPhonePlotByTeam.TabIndex = 3
        Me.chkiPhonePlotByTeam.Text = "Plot by Team Names"
        Me.chkiPhonePlotByTeam.UseVisualStyleBackColor = True
        '
        'chkEnableiPhone
        '
        Me.chkEnableiPhone.AutoSize = True
        Me.chkEnableiPhone.Checked = True
        Me.chkEnableiPhone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnableiPhone.Location = New System.Drawing.Point(19, 6)
        Me.chkEnableiPhone.Name = "chkEnableiPhone"
        Me.chkEnableiPhone.Size = New System.Drawing.Size(182, 17)
        Me.chkEnableiPhone.TabIndex = 1
        Me.chkEnableiPhone.Text = "Enable iPhone FTP Transmission"
        Me.chkEnableiPhone.UseVisualStyleBackColor = True
        '
        'grpXML
        '
        Me.grpXML.Controls.Add(Me.txtXMLUserName)
        Me.grpXML.Controls.Add(Me.txtXMLPassword)
        Me.grpXML.Controls.Add(Me.txtXMLURL)
        Me.grpXML.Controls.Add(Me.Label25)
        Me.grpXML.Controls.Add(Me.Label24)
        Me.grpXML.Controls.Add(Me.Label23)
        Me.grpXML.Location = New System.Drawing.Point(6, 29)
        Me.grpXML.Name = "grpXML"
        Me.grpXML.Size = New System.Drawing.Size(398, 106)
        Me.grpXML.TabIndex = 0
        Me.grpXML.TabStop = False
        Me.grpXML.Text = "XML Repository"
        '
        'txtXMLUserName
        '
        Me.txtXMLUserName.Enabled = False
        Me.txtXMLUserName.Location = New System.Drawing.Point(75, 46)
        Me.txtXMLUserName.Name = "txtXMLUserName"
        Me.txtXMLUserName.Size = New System.Drawing.Size(111, 20)
        Me.txtXMLUserName.TabIndex = 5
        Me.txtXMLUserName.WordWrap = False
        '
        'txtXMLPassword
        '
        Me.txtXMLPassword.Enabled = False
        Me.txtXMLPassword.Location = New System.Drawing.Point(75, 72)
        Me.txtXMLPassword.Name = "txtXMLPassword"
        Me.txtXMLPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtXMLPassword.Size = New System.Drawing.Size(111, 20)
        Me.txtXMLPassword.TabIndex = 4
        Me.txtXMLPassword.UseSystemPasswordChar = True
        Me.txtXMLPassword.WordWrap = False
        '
        'txtXMLURL
        '
        Me.txtXMLURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtXMLURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl
        Me.txtXMLURL.Location = New System.Drawing.Point(75, 20)
        Me.txtXMLURL.Name = "txtXMLURL"
        Me.txtXMLURL.Size = New System.Drawing.Size(254, 20)
        Me.txtXMLURL.TabIndex = 3
        Me.txtXMLURL.Text = "http://"
        Me.txtXMLURL.WordWrap = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Enabled = False
        Me.Label25.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label25.Location = New System.Drawing.Point(13, 75)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(56, 13)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "Password:"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Enabled = False
        Me.Label24.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label24.Location = New System.Drawing.Point(11, 49)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(58, 13)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "Username:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label23.Location = New System.Drawing.Point(37, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(32, 13)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "URL:"
        '
        'tabRemoteCamera
        '
        Me.tabRemoteCamera.Controls.Add(Me.cmdPing)
        Me.tabRemoteCamera.Controls.Add(Me.lblConnectionStatus)
        Me.tabRemoteCamera.Controls.Add(Me.txtIPAddress)
        Me.tabRemoteCamera.Controls.Add(Me.Label9)
        Me.tabRemoteCamera.Controls.Add(Me.cboCameraResolution)
        Me.tabRemoteCamera.Controls.Add(Me.Label10)
        Me.tabRemoteCamera.Location = New System.Drawing.Point(4, 22)
        Me.tabRemoteCamera.Name = "tabRemoteCamera"
        Me.tabRemoteCamera.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRemoteCamera.Size = New System.Drawing.Size(410, 251)
        Me.tabRemoteCamera.TabIndex = 4
        Me.tabRemoteCamera.Text = "Sony Remote"
        Me.tabRemoteCamera.UseVisualStyleBackColor = True
        '
        'cmdPing
        '
        Me.cmdPing.Location = New System.Drawing.Point(281, 16)
        Me.cmdPing.Name = "cmdPing"
        Me.cmdPing.Size = New System.Drawing.Size(54, 21)
        Me.cmdPing.TabIndex = 15
        Me.cmdPing.Text = "Ping"
        Me.cmdPing.UseVisualStyleBackColor = True
        '
        'lblConnectionStatus
        '
        Me.lblConnectionStatus.AutoSize = True
        Me.lblConnectionStatus.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lblConnectionStatus.Location = New System.Drawing.Point(111, 40)
        Me.lblConnectionStatus.Name = "lblConnectionStatus"
        Me.lblConnectionStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblConnectionStatus.TabIndex = 14
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(114, 17)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.Size = New System.Drawing.Size(161, 20)
        Me.txtIPAddress.TabIndex = 13
        Me.txtIPAddress.Text = "192.168.1.100"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label9.Location = New System.Drawing.Point(51, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Resolution"
        '
        'cboCameraResolution
        '
        Me.cboCameraResolution.FormattingEnabled = True
        Me.cboCameraResolution.Items.AddRange(New Object() {"736 x 544", "640 x 480", "320 x 240", "160 x 120"})
        Me.cboCameraResolution.Location = New System.Drawing.Point(114, 60)
        Me.cboCameraResolution.Name = "cboCameraResolution"
        Me.cboCameraResolution.Size = New System.Drawing.Size(162, 21)
        Me.cboCameraResolution.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label10.Location = New System.Drawing.Point(11, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Camera IP Address"
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.lblTempFiles)
        Me.GroupBox16.Controls.Add(Me.cmdSetTempDir)
        Me.GroupBox16.Enabled = False
        Me.GroupBox16.Location = New System.Drawing.Point(462, 73)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(397, 48)
        Me.GroupBox16.TabIndex = 4
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Temporary Files Directory"
        Me.GroupBox16.Visible = False
        '
        'lblTempFiles
        '
        Me.lblTempFiles.Location = New System.Drawing.Point(9, 22)
        Me.lblTempFiles.Name = "lblTempFiles"
        Me.lblTempFiles.Size = New System.Drawing.Size(278, 20)
        Me.lblTempFiles.TabIndex = 14
        '
        'cmdSetTempDir
        '
        Me.cmdSetTempDir.Location = New System.Drawing.Point(293, 22)
        Me.cmdSetTempDir.Name = "cmdSetTempDir"
        Me.cmdSetTempDir.Size = New System.Drawing.Size(73, 22)
        Me.cmdSetTempDir.TabIndex = 11
        Me.cmdSetTempDir.Text = "Browse"
        Me.cmdSetTempDir.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.Label20)
        Me.GroupBox12.Controls.Add(Me.txtAutoSearch)
        Me.GroupBox12.Controls.Add(Me.cmdBrowseAutoSearch)
        Me.GroupBox12.Controls.Add(Me.chkShowChartOnActive)
        Me.GroupBox12.Controls.Add(Me.numUpdateFrequency)
        Me.GroupBox12.Controls.Add(Me.Label19)
        Me.GroupBox12.Enabled = False
        Me.GroupBox12.Location = New System.Drawing.Point(462, 211)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(397, 91)
        Me.GroupBox12.TabIndex = 3
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Analysis Updates"
        Me.GroupBox12.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label20.Location = New System.Drawing.Point(17, 47)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(181, 13)
        Me.Label20.TabIndex = 17
        Me.Label20.Text = "Automated Analysis Template Folder:"
        '
        'txtAutoSearch
        '
        Me.txtAutoSearch.Location = New System.Drawing.Point(18, 63)
        Me.txtAutoSearch.Name = "txtAutoSearch"
        Me.txtAutoSearch.Size = New System.Drawing.Size(278, 20)
        Me.txtAutoSearch.TabIndex = 16
        Me.txtAutoSearch.Text = "None"
        '
        'cmdBrowseAutoSearch
        '
        Me.cmdBrowseAutoSearch.Location = New System.Drawing.Point(302, 63)
        Me.cmdBrowseAutoSearch.Name = "cmdBrowseAutoSearch"
        Me.cmdBrowseAutoSearch.Size = New System.Drawing.Size(73, 22)
        Me.cmdBrowseAutoSearch.TabIndex = 15
        Me.cmdBrowseAutoSearch.Text = "Browse"
        Me.cmdBrowseAutoSearch.UseVisualStyleBackColor = True
        '
        'chkShowChartOnActive
        '
        Me.chkShowChartOnActive.AutoSize = True
        Me.chkShowChartOnActive.Location = New System.Drawing.Point(221, 20)
        Me.chkShowChartOnActive.Name = "chkShowChartOnActive"
        Me.chkShowChartOnActive.Size = New System.Drawing.Size(154, 17)
        Me.chkShowChartOnActive.TabIndex = 4
        Me.chkShowChartOnActive.Text = "Show Chart on Active CPU"
        Me.chkShowChartOnActive.UseVisualStyleBackColor = True
        '
        'numUpdateFrequency
        '
        Me.numUpdateFrequency.Location = New System.Drawing.Point(155, 19)
        Me.numUpdateFrequency.Name = "numUpdateFrequency"
        Me.numUpdateFrequency.Size = New System.Drawing.Size(51, 20)
        Me.numUpdateFrequency.TabIndex = 3
        Me.numUpdateFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(140, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Update Frequency (minutes)"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.numVideoEpoch)
        Me.GroupBox8.Controls.Add(Me.Label14)
        Me.GroupBox8.Enabled = False
        Me.GroupBox8.Location = New System.Drawing.Point(462, 12)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(397, 56)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Video Transmit"
        Me.GroupBox8.Visible = False
        '
        'numVideoEpoch
        '
        Me.numVideoEpoch.Location = New System.Drawing.Point(169, 24)
        Me.numVideoEpoch.Name = "numVideoEpoch"
        Me.numVideoEpoch.Size = New System.Drawing.Size(87, 20)
        Me.numVideoEpoch.TabIndex = 1
        Me.numVideoEpoch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(157, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Video Capture Epoch (seconds)"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblVideoTransmitDestination)
        Me.GroupBox7.Controls.Add(Me.lblVideoTransmitDirectoryStatus)
        Me.GroupBox7.Controls.Add(Me.cmdCheckVidoTransmitDirectoryLink)
        Me.GroupBox7.Controls.Add(Me.cmdSetTransmitDestination)
        Me.GroupBox7.Enabled = False
        Me.GroupBox7.Location = New System.Drawing.Point(462, 127)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(397, 78)
        Me.GroupBox7.TabIndex = 1
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Remote Destination Directory"
        Me.GroupBox7.Visible = False
        '
        'lblVideoTransmitDestination
        '
        Me.lblVideoTransmitDestination.Location = New System.Drawing.Point(9, 22)
        Me.lblVideoTransmitDestination.Name = "lblVideoTransmitDestination"
        Me.lblVideoTransmitDestination.Size = New System.Drawing.Size(278, 20)
        Me.lblVideoTransmitDestination.TabIndex = 14
        '
        'lblVideoTransmitDirectoryStatus
        '
        Me.lblVideoTransmitDirectoryStatus.AutoSize = True
        Me.lblVideoTransmitDirectoryStatus.ForeColor = System.Drawing.Color.Red
        Me.lblVideoTransmitDirectoryStatus.Location = New System.Drawing.Point(6, 53)
        Me.lblVideoTransmitDirectoryStatus.Name = "lblVideoTransmitDirectoryStatus"
        Me.lblVideoTransmitDirectoryStatus.Size = New System.Drawing.Size(128, 13)
        Me.lblVideoTransmitDirectoryStatus.TabIndex = 13
        Me.lblVideoTransmitDirectoryStatus.Text = "Check Link to verify path."
        Me.lblVideoTransmitDirectoryStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdCheckVidoTransmitDirectoryLink
        '
        Me.cmdCheckVidoTransmitDirectoryLink.Location = New System.Drawing.Point(155, 48)
        Me.cmdCheckVidoTransmitDirectoryLink.Name = "cmdCheckVidoTransmitDirectoryLink"
        Me.cmdCheckVidoTransmitDirectoryLink.Size = New System.Drawing.Size(132, 22)
        Me.cmdCheckVidoTransmitDirectoryLink.TabIndex = 12
        Me.cmdCheckVidoTransmitDirectoryLink.Text = "Check Link"
        Me.cmdCheckVidoTransmitDirectoryLink.UseVisualStyleBackColor = True
        '
        'cmdSetTransmitDestination
        '
        Me.cmdSetTransmitDestination.Location = New System.Drawing.Point(293, 22)
        Me.cmdSetTransmitDestination.Name = "cmdSetTransmitDestination"
        Me.cmdSetTransmitDestination.Size = New System.Drawing.Size(73, 22)
        Me.cmdSetTransmitDestination.TabIndex = 11
        Me.cmdSetTransmitDestination.Text = "Browse"
        Me.cmdSetTransmitDestination.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 337)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox12)
        Me.Controls.Add(Me.GroupBox16)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.OptionsTab)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.OptionsTab.ResumeLayout(False)
        Me.tabVideo.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cboLagTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLeadTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tabPaths.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.tabDatabase.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.tabCharting.ResumeLayout(False)
        Me.grpPathWays.ResumeLayout(False)
        Me.grpPathWays.PerformLayout()
        CType(Me.numDifferentiationThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLineTension, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabReports.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.cboVerticalQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboHorizontalQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabStatistics.ResumeLayout(False)
        Me.tabStatistics.PerformLayout()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox13.ResumeLayout(False)
        Me.tabGame.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.tabPitch.ResumeLayout(False)
        Me.tabPitch.PerformLayout()
        Me.boxPitch.ResumeLayout(False)
        Me.boxPitch.PerformLayout()
        Me.tabVideoTransmit.ResumeLayout(False)
        Me.tabVideoTransmit.PerformLayout()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        Me.grpXML.ResumeLayout(False)
        Me.grpXML.PerformLayout()
        Me.tabRemoteCamera.ResumeLayout(False)
        Me.tabRemoteCamera.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        CType(Me.numUpdateFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.numVideoEpoch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OptionsTab As System.Windows.Forms.TabControl
    Friend WithEvents tabVideo As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdBrowseCaptureDir As System.Windows.Forms.Button
    Friend WithEvents lblDefaultCaptureDir As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdSetPreviewQuality As System.Windows.Forms.Button
    Friend WithEvents cboVideoDecoder As System.Windows.Forms.ComboBox
    Friend WithEvents chkPreviewAudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoPlayOnCapture As System.Windows.Forms.CheckBox
    Friend WithEvents tabDatabase As System.Windows.Forms.TabPage
    Friend WithEvents Apply_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkStopAtEndOfClip As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowContinuousPlaylist As System.Windows.Forms.CheckBox
    Friend WithEvents cmdBrowseDB As System.Windows.Forms.Button
    Friend WithEvents lblDBLocation As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoUpdateDB As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkCacheAllData As System.Windows.Forms.CheckBox
    Friend WithEvents tabPitch As System.Windows.Forms.TabPage
    Friend WithEvents cboSport As System.Windows.Forms.ComboBox
    Friend WithEvents boxPitch As System.Windows.Forms.GroupBox
    Friend WithEvents chkSetSportDefault As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents cboMirror_Field As System.Windows.Forms.ComboBox
    Friend WithEvents cboMirror_Perimeter As System.Windows.Forms.ComboBox
    Friend WithEvents cboMirror_Lines As System.Windows.Forms.ComboBox
    Friend WithEvents cboMirror_Highlights As System.Windows.Forms.ComboBox
    Friend WithEvents tabGame As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoReload As System.Windows.Forms.CheckBox
    Friend WithEvents tabRemoteCamera As System.Windows.Forms.TabPage
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboCameraResolution As System.Windows.Forms.ComboBox
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblConnectionStatus As System.Windows.Forms.Label
    Friend WithEvents cmdPing As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDBStatus As System.Windows.Forms.Label
    Friend WithEvents cmdUpdateDB As System.Windows.Forms.Button
    Friend WithEvents tabCharting As System.Windows.Forms.TabPage
    Friend WithEvents grpPathWays As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cboLineTension As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboMirror_End As System.Windows.Forms.ComboBox
    Friend WithEvents cboMirror_Start As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tabVideoTransmit As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetTransmitDestination As System.Windows.Forms.Button
    Friend WithEvents lblVideoTransmitDirectoryStatus As System.Windows.Forms.Label
    Friend WithEvents cmdCheckVidoTransmitDirectoryLink As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents numVideoEpoch As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblVideoTransmitDestination As System.Windows.Forms.TextBox
    Friend WithEvents tabPaths As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRemovePath As System.Windows.Forms.Button
    Friend WithEvents cmdAddPath As System.Windows.Forms.Button
    Friend WithEvents chkUseAlternativePaths As System.Windows.Forms.CheckBox
    Friend WithEvents lstPaths As System.Windows.Forms.ListBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cboLagTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboLeadTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowChartOnActive As System.Windows.Forms.CheckBox
    Friend WithEvents numUpdateFrequency As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtAutoSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowseAutoSearch As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tabStatistics As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents lstStatRegions As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkIncludeDescriptors As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents optStatsByTeam As System.Windows.Forms.RadioButton
    Friend WithEvents optStatsByGame As System.Windows.Forms.RadioButton
    Friend WithEvents chkStatsShowTotals As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents numDifferentiationThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents chkAddSlowMotion As System.Windows.Forms.CheckBox
    Friend WithEvents chkSingleClickPass As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddFades As System.Windows.Forms.CheckBox
    Friend WithEvents tabReports As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboVerticalQ As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboHorizontalQ As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowHeatBallMovements As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTempFiles As System.Windows.Forms.TextBox
    Friend WithEvents cmdSetTempDir As System.Windows.Forms.Button
    Friend WithEvents chkEnableiPhone As System.Windows.Forms.CheckBox
    Friend WithEvents grpXML As System.Windows.Forms.GroupBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtXMLUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtXMLPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtXMLURL As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents chkiPhonePlotByTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkiPhonePlotByTeam As System.Windows.Forms.CheckBox
    Friend WithEvents chkiPhoneIncludeStats As System.Windows.Forms.CheckBox

End Class
