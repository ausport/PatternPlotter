Imports System.Windows.Forms

Public Class frmOptions
    Friend WithEvents cboFieldColor As System.Windows.Forms.ComboBox
    Friend WithEvents cboFieldHighlights As System.Windows.Forms.ComboBox
    Friend WithEvents cboFieldLines As System.Windows.Forms.ComboBox
    Friend WithEvents cboFieldPerimeter As System.Windows.Forms.ComboBox
    Friend WithEvents cboStartColor As System.Windows.Forms.ComboBox
    Friend WithEvents cboEndColor As System.Windows.Forms.ComboBox
    Dim kColor_Field As KnownColor()
    Dim kColor_High As KnownColor()
    Dim kColor_Line As KnownColor()
    Dim kColor_Perim As KnownColor()
    Dim kColor_Start As KnownColor()
    Dim kColor_End As KnownColor()

    Dim Pen As New Pen(Color.Black, 1)
    Dim Path As New Drawing2D.GraphicsPath



    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SaveSettings()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With UserPrefs
            Me.lblDefaultCaptureDir.Text = .VideoCaptureDir
            Me.cboVideoDecoder.Text = .VideoCaptureFormat
            Me.chkAutoPlayOnCapture.Checked = .AutoPlay
            Me.chkPreviewAudio.Checked = .PreviewAudioOnCapture
            Me.chkShowContinuousPlaylist.Checked = .PlayContinuous
            Me.chkStopAtEndOfClip.Checked = .StopAtEndOfClip
            If Not System.IO.File.Exists(.dbPath) Then
                Me.lblDBLocation.Text = My.Application.Info.DirectoryPath & "\GamePath.mdb"
            Else
                Me.lblDBLocation.Text = .dbPath
            End If
            Me.chkAutoUpdateDB.Checked = .AutoUpdateDB
            Me.chkCacheAllData.Checked = .CacheAllData
            Me.chkSetSportDefault.Checked = True
            Me.chkAutoReload.Checked = .AutoReload
            Me.cboLineWidth.Value = .pmLineWidth
            Me.cboLineTension.Value = .pmLineTension
            Me.numDifferentiationThreshold.Value = .pmDifferntiateThreshold

            Me.cboHorizontalQ.Value = .clHorizontalQ
            Me.cboVerticalQ.Value = .clVerticalQ

            Me.chkShowHeatBallMovements.Checked = .boolShowAllBallMovementsInHeat

            Me.cboLeadTime.Value = .LeadTime
            Me.cboLagTime.Value = .LagTime

            Me.chkAddSlowMotion.Checked = .DuplicateInSlowMotion
            Me.chkAddFades.Checked = .AddFadeTransitions

            Me.lblVideoTransmitDestination.Text = .FileTransmitDestination
            Me.lblTempFiles.Text = .TempFileDestination
            Me.numVideoEpoch.Value = CDec(.VideoTransmitEpoch)

            Me.chkEnableiPhone.Checked = .iPhoneIsActive
            Me.txtXMLPassword.Text = .xmlPassword
            Me.txtXMLURL.Text = .xmlURL
            Me.txtXMLUserName.Text = .xmlUsername
            Me.chkiPhonePlotByTime.Checked = .iPhoneByTimes
            Me.chkiPhonePlotByTeam.Checked = .iPhoneByTeams
            Me.chkiPhoneIncludeStats.Checked = .iPhoneIncludeStats

            Me.chkSingleClickPass.Checked = .SingleClickForPass

            Pen.StartCap = Drawing2D.LineCap.RoundAnchor
            Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
            Path.AddLine(180, 55, 240, 25)
            Path.AddLine(240, 25, 300, 55)
            Path.AddLine(300, 55, 360, 25)


            Me.chkUseAlternativePaths.Checked = .boolSearchPaths
            Me.lstPaths.Enabled = .boolSearchPaths
            If Not .szAlternativePath Is Nothing Then
                For Each item As String In .szAlternativePath
                    Me.lstPaths.Items.Add(item)
                Next
            End If

            Me.chkIncludeDescriptors.Checked = .StatShowDescriptors
            Me.chkStatsShowTotals.Checked = .StatShowTotals
            ShowRegionList(Me.lstStatRegions, .Sport)

            Select Case .CameraResolution
                Case Is = Resolution.n736x480
                    Me.cboCameraResolution.Text = "736 x 480"
                Case Is = Resolution.n640x480
                    Me.cboCameraResolution.Text = "640 x 480"
                Case Is = Resolution.n480x360
                    Me.cboCameraResolution.Text = "480 x 360"
                Case Is = Resolution.n320x240
                    Me.cboCameraResolution.Text = "320 x 240"
                Case Is = Resolution.n160x120
                    Me.cboCameraResolution.Text = "160 x 120"
            End Select

            'Enumerate known colors.
            kColor_Field = [Enum].GetValues(GetType(KnownColor))
            kColor_High = [Enum].GetValues(GetType(KnownColor))
            kColor_Line = [Enum].GetValues(GetType(KnownColor))
            kColor_Perim = [Enum].GetValues(GetType(KnownColor))
            kColor_Start = [Enum].GetValues(GetType(KnownColor))
            kColor_End = [Enum].GetValues(GetType(KnownColor))

            'Set color swatch combo boxes.
            InitializeComboBox()

            'This is a work-around for the problem of trying to update the value of an owener-draw combo-box.
            'I haven't worked out how to do it, so this works by linking a non-visible combobox to the same datasource as 
            'the visible color-enabled boxes.  Setting the value of the mirror box automatically updates the value of the
            'otherwise inaccessable color box.
            'So... update the text for the mirror box to modify the color box.

            Me.cboMirror_Field.DataSource = kColor_Field
            Me.cboMirror_Field.Text = UserPrefs.FieldBackground.Name
            Me.cboMirror_Highlights.DataSource = kColor_High
            Me.cboMirror_Highlights.Text = UserPrefs.FieldHighlights.Name
            Me.cboMirror_Lines.DataSource = kColor_Line
            Me.cboMirror_Lines.Text = UserPrefs.FieldLines.Name
            Me.cboMirror_Perimeter.DataSource = kColor_Perim
            Me.cboMirror_Perimeter.Text = UserPrefs.FieldPerimeter.Name

            Me.cboMirror_Start.DataSource = kColor_Start
            Me.cboMirror_Start.Text = UserPrefs.pmStartColor.Name
            Me.cboMirror_End.DataSource = kColor_End
            Me.cboMirror_End.Text = UserPrefs.pmEndColor.Name


            'Setup sport items in combo
            Dim tS As tSports() = [Enum].GetValues(GetType(tSports))
            For Each spt As tSports In tS
                Me.cboSport.Items.Add(GetSportStringFromType(spt))
            Next
            Me.cboSport.Text = GetSportStringFromType(UserPrefs.Sport)

            Me.txtIPAddress.Text = .CameraIP

            Select Case .StatLayout
                Case Is = StatsLayout.statByGame
                    Me.optStatsByGame.Checked = True
                Case Else
                    Me.optStatsByTeam.Checked = True
            End Select

            ' DoDatabaseCheck()

        End With


    End Sub

    Private Sub DoDatabaseCheck()
        'Check database version
        If CheckDBVersion() Then
            Me.cmdUpdateDB.Enabled = False
            Me.lblDBStatus.Text = "This database is up to date."
        Else
            Me.cmdUpdateDB.Enabled = True
            Me.lblDBStatus.Text = "This database needs updating."
        End If

    End Sub

    Private Sub InitializeComboBox()
        'Set Field color box
        Me.cboFieldColor = New ComboBox
        Me.cboFieldColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboFieldColor.Location = New System.Drawing.Point(14, 32)
        Me.cboFieldColor.Name = "cboFieldColor"
        Me.cboFieldColor.Size = New System.Drawing.Size(150, 21)
        Me.cboFieldColor.DropDownWidth = 200
        Me.cboFieldColor.TabIndex = 0
        Me.cboFieldColor.DropDownStyle = ComboBoxStyle.DropDownList
        cboFieldColor.DataSource = kColor_Field
        Me.boxPitch.Controls.Add(Me.cboFieldColor)
        Me.cboFieldColor.Text = UserPrefs.FieldBackground.Name
        '     Me.cboFieldColor.DataBindings.Control.Text = UserPrefs.FieldBackground.Name

        'Set highlights color box
        Me.cboFieldHighlights = New ComboBox
        Me.cboFieldHighlights.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboFieldHighlights.Location = New System.Drawing.Point(14, 72)
        Me.cboFieldHighlights.Name = "cboFieldHighlights"
        Me.cboFieldHighlights.Size = New System.Drawing.Size(150, 21)
        Me.cboFieldHighlights.DropDownWidth = 200
        Me.cboFieldHighlights.TabIndex = 1
        Me.cboFieldHighlights.DropDownStyle = ComboBoxStyle.DropDownList
        cboFieldHighlights.DataSource = kColor_High
        Me.boxPitch.Controls.Add(Me.cboFieldHighlights)
        '      Me.cboFieldHighlights.DataBindings.Control.Text = UserPrefs.FieldHighlights.Name


        'Set lines color box
        Me.cboFieldLines = New ComboBox
        Me.cboFieldLines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboFieldLines.Location = New System.Drawing.Point(14, 112)
        Me.cboFieldLines.Name = "cboFieldLines"
        Me.cboFieldLines.Size = New System.Drawing.Size(150, 21)
        Me.cboFieldLines.DropDownWidth = 200
        Me.cboFieldLines.TabIndex = 2
        Me.cboFieldLines.DropDownStyle = ComboBoxStyle.DropDownList
        cboFieldLines.DataSource = kColor_Line
        Me.boxPitch.Controls.Add(Me.cboFieldLines)
        '      Me.cboFieldLines.DataBindings.Control.Text = UserPrefs.FieldLines.Name


        'Set perimeter color box
        Me.cboFieldPerimeter = New ComboBox
        Me.cboFieldPerimeter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboFieldPerimeter.Location = New System.Drawing.Point(14, 152)
        Me.cboFieldPerimeter.Name = "cboFieldPerimeter"
        Me.cboFieldPerimeter.Size = New System.Drawing.Size(150, 21)
        Me.cboFieldPerimeter.DropDownWidth = 200
        Me.cboFieldPerimeter.TabIndex = 3
        Me.cboFieldPerimeter.DropDownStyle = ComboBoxStyle.DropDownList
        cboFieldPerimeter.DataSource = kColor_Perim
        Me.boxPitch.Controls.Add(Me.cboFieldPerimeter)
        '       Me.cboFieldPerimeter.DataBindings.Control.Text = UserPrefs.FieldPerimeter.Name


        'Set start color box
        Me.cboStartColor = New ComboBox
        Me.cboStartColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboStartColor.Location = New System.Drawing.Point(9, 94)
        Me.cboStartColor.Name = "cboStartColor"
        Me.cboStartColor.Size = New System.Drawing.Size(150, 21)
        Me.cboStartColor.DropDownWidth = 200
        Me.cboStartColor.TabIndex = 3
        Me.cboStartColor.DropDownStyle = ComboBoxStyle.DropDownList
        cboStartColor.DataSource = kColor_Start
        Me.grpPathWays.Controls.Add(Me.cboStartColor)
        ' Me.cboStartColor.DataBindings.Control.Text = UserPrefs.pmStartColor.Name

        'Set start color box
        Me.cboEndColor = New ComboBox
        Me.cboEndColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboEndColor.Location = New System.Drawing.Point(174, 94)
        Me.cboEndColor.Name = "cboEndColor"
        Me.cboEndColor.Size = New System.Drawing.Size(150, 21)
        Me.cboEndColor.DropDownWidth = 200
        Me.cboEndColor.TabIndex = 3
        Me.cboEndColor.DropDownStyle = ComboBoxStyle.DropDownList
        cboEndColor.DataSource = kColor_End
        Me.grpPathWays.Controls.Add(Me.cboEndColor)
        '  Me.cboEndColor.DataBindings.Control.Text = UserPrefs.pmEndColor.Name


    End Sub



    Private Sub cboFieldColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboFieldColor.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Field(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Field(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub

    Private Sub cboFieldPerimeter_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboFieldPerimeter.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Perim(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Perim(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub

    Private Sub cboFieldLines_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboFieldLines.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Line(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Line(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub

    Private Sub cboFieldHighlights_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboFieldHighlights.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_High(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_High(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub


    Private Sub SaveSettings()
        With UserPrefs

            .FieldBackground = Color.FromName(Me.cboMirror_Field.Text)
            .FieldHighlights = Color.FromName(Me.cboMirror_Highlights.Text)
            .FieldLines = Color.FromName(Me.cboMirror_Lines.Text)
            .FieldPerimeter = Color.FromName(Me.cboMirror_Perimeter.Text)
            If .FieldBackground <> .FieldLines Then
                SaveSetting(AppName, "Settings", "FieldBackground", .FieldBackground.Name)
                SaveSetting(AppName, "Settings", "FieldHighlights", .FieldHighlights.Name)
                SaveSetting(AppName, "Settings", "FieldLines", .FieldLines.Name)
                SaveSetting(AppName, "Settings", "FieldPerimeter", .FieldPerimeter.Name)
            End If

            .pmStartColor = Color.FromName(Me.cboMirror_Start.Text)
            .pmEndColor = Color.FromName(Me.cboMirror_End.Text)
            If .pmStartColor <> .pmEndColor Then
                SaveSetting(AppName, "Settings", "pmStartColor", .pmStartColor.Name)
                SaveSetting(AppName, "Settings", "pmEndColor", .pmEndColor.Name)
            End If

            .boolShowAllBallMovementsInHeat = Me.chkShowHeatBallMovements.Checked

            .iPhoneIsActive = Me.chkEnableiPhone.Checked
            .xmlPassword = Me.txtXMLPassword.Text
            .xmlURL = Me.txtXMLURL.Text
            .xmlUsername = Me.txtXMLUserName.Text
            .iPhoneByTeams = Me.chkiPhonePlotByTeam.Checked
            .iPhoneByTimes = Me.chkiPhonePlotByTime.Checked
            .iPhoneIncludeStats = Me.chkiPhoneIncludeStats.Checked

            .VideoCaptureDir = Me.lblDefaultCaptureDir.Text
            If Microsoft.VisualBasic.Right(.VideoCaptureDir, 1) <> "\" Then .VideoCaptureDir = .VideoCaptureDir & "\"
            .VideoCaptureFormat = "DV Video Decoder"
            .AutoPlay = Me.chkAutoPlayOnCapture.Checked
            .PreviewAudioOnCapture = Me.chkPreviewAudio.Checked
            .PlayContinuous = Me.chkShowContinuousPlaylist.Checked
            .StopAtEndOfClip = Me.chkStopAtEndOfClip.Checked
            .dbPath = Me.lblDBLocation.Text
            CONNECT_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & UserPrefs.dbPath
            .AutoUpdateDB = Me.chkAutoUpdateDB.Checked
            .CacheAllData = Me.chkCacheAllData.Checked

            .pmLineWidth = Me.cboLineWidth.Value
            .pmLineTension = Me.cboLineTension.Value
            .pmDifferntiateThreshold = Me.numDifferentiationThreshold.Value

            .clHorizontalQ = Me.cboHorizontalQ.Value
            .clVerticalQ = Me.cboVerticalQ.Value

            .LeadTime = Me.cboLeadTime.Value
            .LagTime = Me.cboLagTime.Value

            .FileTransmitDestination = Me.lblVideoTransmitDestination.Text
            If Microsoft.VisualBasic.Right(.FileTransmitDestination, 1) <> "\" Then .FileTransmitDestination &= "\"
            .TempFileDestination = Me.lblTempFiles.Text
            If Microsoft.VisualBasic.Right(.TempFileDestination, 1) <> "\" Then .TempFileDestination &= "\"

            .VideoTransmitEpoch = CDbl(Me.numVideoEpoch.Value)
 
            .nAlternativePathCount = Me.lstPaths.Items.Count
            ReDim .szAlternativePath(.nAlternativePathCount - 1)
            For Each item As Object In Me.lstPaths.Items
                .szAlternativePath(lstPaths.Items.IndexOf(item)) = item
            Next
            .boolSearchPaths = Me.chkUseAlternativePaths.Checked

            .DuplicateInSlowMotion = Me.chkAddSlowMotion.Checked
            .AddFadeTransitions = Me.chkAddFades.Checked

            .SingleClickForPass = Me.chkSingleClickPass.Checked

            .StatShowDescriptors = Me.chkIncludeDescriptors.Checked
            .StatShowTotals = Me.chkStatsShowTotals.Checked
            .StatRegionCount = Me.lstStatRegions.CheckedItems.Count
            ReDim .StatIncludedRegions(.StatRegionCount - 1)
            For Each item As Object In Me.lstStatRegions.CheckedItems
                .StatIncludedRegions(lstStatRegions.CheckedItems.IndexOf(item)) = GetRegionFromString(item)
            Next

            .Sport = GetSportTypeFromString(Me.cboSport.Text)
            PitchOffset = GetPitchOffSets(.Sport)

            .AutoReload = Me.chkAutoReload.Checked
            .CameraIP = Me.txtIPAddress.Text
            Select Case Me.cboCameraResolution.Text
                Case Is = "736 x 480"
                    .CameraResolution = Resolution.n736x480
                Case Is = "640 x 480"
                    .CameraResolution = Resolution.n640x480
                Case Is = "480 x 360"
                    .CameraResolution = Resolution.n480x360
                Case Is = "320 x 240"
                    .CameraResolution = Resolution.n320x240
                Case Is = "160 x 120"
                    .CameraResolution = Resolution.n160x120
            End Select

            SaveSetting(AppName, "Settings", "AddFadeTransitions", .AddFadeTransitions)
            SaveSetting(AppName, "Settings", "DuplicateInSlowMotion", .DuplicateInSlowMotion)
            SaveSetting(AppName, "Settings", "VideoTransmitEnabled", .VideoTransmitEnabled)
            SaveSetting(AppName, "Settings", "FileTransmitDestination", .FileTransmitDestination)
            SaveSetting(AppName, "Settings", "TempFileDestination", .TempFileDestination)
            SaveSetting(AppName, "Settings", "VideoTransmitEpoch", .VideoTransmitEpoch)
            SaveSetting(AppName, "Settings", "VideoCaptureDir", .VideoCaptureDir)
            SaveSetting(AppName, "Settings", "SingleClickForPass", .SingleClickForPass)

            SaveSetting(AppName, "Settings", "VideoCaptureFormat", .VideoCaptureFormat)
            SaveSetting(AppName, "Settings", "AutoPlay", .AutoPlay)
            SaveSetting(AppName, "Settings", "PreviewAudioOnCapture", .PreviewAudioOnCapture)
            SaveSetting(AppName, "Settings", "PlayContinuous", .PlayContinuous)
            SaveSetting(AppName, "Settings", "StopAtEndOfClip", .StopAtEndOfClip)
            SaveSetting(AppName, "Settings", "dbPath", .dbPath)
            SaveSetting(AppName, "Settings", "AutoUpdateDB", .AutoUpdateDB)
            SaveSetting(AppName, "Settings", "CacheAllData", .CacheAllData)
            SaveSetting(AppName, "Settings", "AutoReload", .AutoReload)
            SaveSetting(AppName, "Settings", "CameraIP", .CameraIP)
            SaveSetting(AppName, "Settings", "CameraResolution", .CameraResolution)
            SaveSetting(AppName, "Settings", "pmLineWidth", .pmLineWidth)
            SaveSetting(AppName, "Settings", "pmLineTension", .pmLineTension)
            SaveSetting(AppName, "Settings", "pmDifferntiateThreshold", .pmDifferntiateThreshold)
            SaveSetting(AppName, "Settings", "boolSearchPaths", .boolSearchPaths)
            SaveSetting(AppName, "Settings", "nAlternativePathCount", .nAlternativePathCount)
            SaveSetting(AppName, "Settings", "clHorizontalQ", .clHorizontalQ)
            SaveSetting(AppName, "Settings", "clVerticalQ", .clVerticalQ)
            SaveSetting(AppName, "Settings", "LeadTime", .LeadTime)
            SaveSetting(AppName, "Settings", "LagTime", .LagTime)
            SaveSetting(AppName, "Settings", "StatRegionCount", .StatRegionCount)
            SaveSetting(AppName, "Settings", "StatShowDescriptors", .StatShowDescriptors)
            SaveSetting(AppName, "Settings", "StatLayout", .StatLayout)
            SaveSetting(AppName, "Settings", "boolShowAllBallMovementsInHeat", .boolShowAllBallMovementsInHeat)

            SaveSetting(AppName, "Settings", "iPhoneIsActive", .iPhoneIsActive)
            SaveSetting(AppName, "Settings", "xmlPassword", .xmlPassword)
            SaveSetting(AppName, "Settings", "xmlURL", .xmlURL)
            SaveSetting(AppName, "Settings", "xmlUsername", .xmlUsername)
            SaveSetting(AppName, "Settings", "iPhoneByTimes", .iPhoneByTimes)
            SaveSetting(AppName, "Settings", "iPhoneByTeams", .iPhoneByTeams)
            SaveSetting(AppName, "Settings", "iPhoneIncludeStats", .iPhoneIncludeStats)

            For Each item As tRegion In .StatIncludedRegions
                SaveSetting(AppName, "Settings", "StatIncludedRegions_" & Array.IndexOf(.StatIncludedRegions, item).ToString, GetRegionString(item))
            Next

            For Each item As String In .szAlternativePath
                SaveSetting(AppName, "Settings", "Path_" & Array.IndexOf(.szAlternativePath, item).ToString, item)
            Next

            If Me.chkSetSportDefault.Checked Then
                SaveSetting(AppName, "Settings", "Sport", Me.cboSport.Text)
            End If
            Application.DoEvents()

        End With

    End Sub

    Private Sub cmdBrowseCaptureDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseCaptureDir.Click
        Dim CapDir As FolderBrowserDialog
        CapDir = New FolderBrowserDialog

        CapDir.ShowNewFolderButton = True
        CapDir.RootFolder = Environment.SpecialFolder.Desktop
        CapDir.ShowDialog()

        If CapDir.SelectedPath <> "" Then
            Me.lblDefaultCaptureDir.Text = CapDir.SelectedPath
        End If
    End Sub

    Private Sub cmdSetPreviewQuality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetPreviewQuality.Click
        Dim cDVType As clsVideoCaptureClass
        cDVType = New clsVideoCaptureClass
        cDVType.SetDVType(frmMain)
        cDVType = Nothing
    End Sub

    Private Sub Apply_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Apply_Button.Click
        SaveSettings()
    End Sub

    Private Sub cmdBrowseDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseDB.Click
        'Load alternative database.
        Dim dlgFindDB As OpenFileDialog
        dlgFindDB = New OpenFileDialog

        With dlgFindDB
            .DefaultExt = "*.mdb"
            .Title = "Set Default Database..."
            .Filter = "MDB Database Files|*.mdb|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                'Add video handling code here...
                Me.lblDBLocation.Text = .FileName
                UserPrefs.dbPath = .FileName
            End If
        End With

        DoDatabaseCheck()


    End Sub

    Private Sub boxPitch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles boxPitch.Paint
        With e.Graphics
            .FillRectangle(New SolidBrush(Color.FromName(cboFieldPerimeter.Text)), 220, 15, 110, 170)
            .DrawRectangle(Pens.Black, 220, 15, 110, 170)
            .FillRectangle(New SolidBrush(Color.FromName(cboFieldColor.Text)), 230, 30, 90, 140)
            .DrawRectangle(New Pen(Color.FromName(cboFieldLines.Text), 2), 230, 30, 90, 140)
            .DrawLine(New Pen(Color.FromName(cboFieldLines.Text), 2), 230, 98, 320, 98)
            .FillEllipse(New SolidBrush(Color.FromName(cboFieldHighlights.Text)), 260, 82, 30, 30)
            .DrawEllipse(New Pen(Color.FromName(cboFieldLines.Text), 1), 260, 82, 30, 30)

        End With

    End Sub

    Private Sub cboFieldPerimeter_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldPerimeter.SelectedValueChanged
        boxPitch.Refresh()
    End Sub

    Private Sub cboFieldLines_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldLines.SelectedValueChanged
        boxPitch.Refresh()

    End Sub

    Private Sub cboFieldHighlights_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldHighlights.SelectedValueChanged
        boxPitch.Refresh()

    End Sub

    Private Sub cboFieldColor_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFieldColor.SelectedValueChanged
        boxPitch.Refresh()

    End Sub

    Private Sub cmdPing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPing.Click
        Try
            If My.Computer.Network.Ping(Me.txtIPAddress.Text) Then
                Me.lblConnectionStatus.Text = "Connected"
            Else
                Me.lblConnectionStatus.Text = "Not Connected"
            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, Application.ProductName)
        End Try


    End Sub

    Private Sub cmdUpdateDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateDB.Click
        modLoadSaveGame.UpgradeDBVersion()
        DoDatabaseCheck()
    End Sub

    Private Sub grpPathWays_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grpPathWays.Paint
        pen.Width = CSng(Me.cboLineWidth.Value)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.DrawCurve(Pen, Path.PathPoints, CSng(Me.cboLineTension.Value))
    End Sub

    Private Sub cboLineWidth_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLineWidth.ValueChanged
        Me.grpPathWays.Refresh()
    End Sub

    Private Sub cboLineTension_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLineTension.ValueChanged
        Me.grpPathWays.Refresh()
    End Sub

    Private Sub cboStartColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboStartColor.DrawItem
        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Start(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Start(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub

    Private Sub cboEndColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboEndColor.DrawItem
        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_End(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_End(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub

    Private Sub cmdSetTransmitDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetTransmitDestination.Click
        Dim CapDir As FolderBrowserDialog
        CapDir = New FolderBrowserDialog
        CapDir.ShowNewFolderButton = True
        CapDir.RootFolder = Environment.SpecialFolder.Desktop
        CapDir.ShowDialog()

        If IO.Directory.Exists(CapDir.SelectedPath) Then
            Me.lblVideoTransmitDestination.Text = CapDir.SelectedPath
            Me.lblVideoTransmitDirectoryStatus.Text = "Path Is Available..."
        End If
    End Sub

    Private Sub cmdCheckVidoTransmitDirectoryLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckVidoTransmitDirectoryLink.Click
        If IO.Directory.Exists(Me.lblVideoTransmitDestination.Text) Then
            Me.lblVideoTransmitDirectoryStatus.Text = "Path Is Available..."
            Me.lblVideoTransmitDirectoryStatus.ForeColor = Color.Green
        Else
            Me.lblVideoTransmitDirectoryStatus.Text = "Path Not Available..."
            Me.lblVideoTransmitDirectoryStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub cmdAddPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddPath.Click
        Dim fileBrowser As New FolderBrowserDialog
        fileBrowser.ShowNewFolderButton = True
        fileBrowser.ShowDialog()

        If Not String.IsNullOrEmpty(fileBrowser.SelectedPath) Then
            If Microsoft.VisualBasic.Right(fileBrowser.SelectedPath, 1) <> "\" Then
                Me.lstPaths.Items.Add(fileBrowser.SelectedPath & "\")
            Else
                Me.lstPaths.Items.Add(fileBrowser.SelectedPath)
            End If
        End If
    End Sub

    Private Sub cmdRemovePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemovePath.Click
        If lstPaths.SelectedItems.Count > 0 Then
            lstPaths.Items.Remove(lstPaths.SelectedItem)
        End If

    End Sub

    Private Sub chkUseAlternativePaths_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseAlternativePaths.CheckedChanged
        Me.lstPaths.Enabled = Me.chkUseAlternativePaths.Checked
    End Sub

    Private Sub cmdBrowseAutoSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseAutoSearch.Click
        Dim CapDir As FolderBrowserDialog
        CapDir = New FolderBrowserDialog
        CapDir.ShowNewFolderButton = True
        CapDir.RootFolder = Environment.SpecialFolder.Desktop
        CapDir.ShowDialog()

        If IO.Directory.Exists(CapDir.SelectedPath) Then
            Me.txtAutoSearch.Text = CapDir.SelectedPath
        End If

    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click

    End Sub

    Private Sub optStatsByGame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optStatsByGame.CheckedChanged
        If Me.optStatsByGame.Checked Then UserPrefs.StatLayout = StatsLayout.statByGame
    End Sub

    Private Sub optStatsByTeam_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optStatsByTeam.CheckedChanged
        If Me.optStatsByTeam.Checked Then UserPrefs.StatLayout = StatsLayout.statByTeam
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click

    End Sub

    Private Sub chkShowMapCaptions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox11_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox11.Enter

    End Sub

    Private Sub cmdSetTempDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetTempDir.Click
        Dim TempDir As FolderBrowserDialog
        TempDir = New FolderBrowserDialog
        TempDir.ShowNewFolderButton = True
        TempDir.RootFolder = Environment.SpecialFolder.Desktop
        TempDir.ShowDialog()

        If IO.Directory.Exists(TempDir.SelectedPath) Then
            Me.lblTempFiles.Text = TempDir.SelectedPath
        End If

    End Sub

    Private Sub cboSport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSport.SelectedIndexChanged
        ShowRegionList(Me.lstStatRegions, GetSportTypeFromString(Me.cboSport.Text))
    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click

    End Sub

    Private Sub Label24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label24.Click

    End Sub

    Private Sub Label25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label25.Click

    End Sub

    Private Sub chkEnableiPhone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableiPhone.CheckedChanged
        grpXML.Enabled = chkEnableiPhone.Checked
    End Sub

    Private Sub cboHorizontalQ_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboHorizontalQ.ValueChanged

    End Sub
End Class
