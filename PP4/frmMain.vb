Imports System.IO
Imports System.Drawing.Printing


Public Class frmMain
    Public WithEvents pDocMulti As PrintDocument
    Private PrintRange() As frmChart
    Private bLastPage As Boolean = False

    Private ctNumber As Integer = 0
    Dim PageCount As Integer = 0

    Private Sub PrintToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dlgPrint As PrintDialog
        dlgPrint = New PrintDialog

        dlgPrint.ShowDialog()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub mnuOpenSettingsTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenSettingsTemplate.Click
        frmTags.MdiParent = Me
        frmTags.Show()
        frmTags.mnuLoadTemplate_Click(Nothing, Nothing)
    End Sub

    Public Sub toolOpenVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolOpenVideo.Click
        Me.mnuOpenVideo_Click(Me, e)
    End Sub

    Public Sub mnuVideoCapture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVideoCapture.Click
        frmVideoCapture.MdiParent = Me
        frmVideoCapture.Show()
        frmVideoCapture.Focus()
    End Sub

    Private Sub toolVideoCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolVideoCapture.Click
        Me.mnuVideoCapture_Click(sender, e)
    End Sub

    Private Sub mnuOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmOptions.MdiParent = Me
        frmOptions.Show()
        frmOptions.Focus()
    End Sub

    Private Sub toolOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolOptions.Click
        Me.mnuOptions_Click(sender, e)
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting(AppName, "Settings", "GameID", propsCurrentGame.GameID)
        SaveSetting(AppName, "Settings", "Competition", propsCurrentGame.Competition)
        SaveSetting(AppName, "Settings", "GameAuthor", propsCurrentGame.GameAuthor)
        SaveSetting(AppName, "Settings", "GameNotes", propsCurrentGame.GameNotes)
        SaveSetting(AppName, "Settings", "GameVenue", propsCurrentGame.GameVenue)
        SaveSetting(AppName, "Settings", "GameOpponent", propsCurrentGame.GameOpponent)

        If SonyCameraIsConnected Then
            CAM.CloseConnection()
            CAM = Nothing
            SonyCameraIsConnected = False
        End If

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetSettings()

        Customise_ShowText.Checked = CBool(GetSetting(AppName, "Settings", "ToolStrip_MainMenu_ShowText", "True"))
        Me.Customise_ShowText_Click(sender, e)

        Me.toolVersion.Text = "Ver: " & AppVersion

        'Set icon sizes
        Select Case GetSetting(AppName, "Settings", "ToolStrip_MainMenu_Size", 64)
            Case Is = 64
                Me.mnuSetSize_Large_Click(sender, e)
            Case Is = 48
                Me.mnuSetSize_Normal_Click(sender, e)
            Case Is = 24
                Me.mnuSetSize_Small_Click(sender, e)
        End Select

        'Set toolbar locations

        'NB - for future iterations of this - save the location of the toolbar as well as the panel.  This will help control several toolbars.
        Select Case GetSetting(AppName, "Settings", "ToolStrip_MainMenu", "Right")
            Case Is = "Left"
                Me.ToolStripPanelLeft.Controls.Add(Me.ToolStrip_MainMenu)
                Me.ToolStrip_MainMenu.Location = New Point(0, 0)
            Case Is = "Right"
                Me.ToolStripPanelRight.Controls.Add(Me.ToolStrip_MainMenu)
                Me.ToolStrip_MainMenu.Location = New Point(0, 0)
            Case Is = "Top"
                Me.ToolStripPanelTop.Controls.Add(Me.ToolStrip_MainMenu)
                Me.ToolStrip_MainMenu.Location = New Point(0, 0)
        End Select
    End Sub

    Private Sub toolOpenGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolOpenGame.Click
        Me.mnuOpenGame_Click1(sender, e)
    End Sub

    Private Sub AnalysisControlWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalysisControlWindowToolStripMenuItem.Click
        frmAnalysis.MdiParent = Me
        frmAnalysis.Show()
    End Sub

    Public Sub EventTagsWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventTagsWindowToolStripMenuItem.Click
        frmTags.MdiParent = Me
        frmTags.Show()
        frmTags.TopMost = True
    End Sub

    Private Sub toolStartStopGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolStartStopGame.Click
        If Not bGameIsActive Then
            'Close open games.
            Me.mnuCloseGame_Click1(sender, e)

            frmSetGame.MdiParent = Me
            frmSetGame.TopMost = True
            frmSetGame.Show()
        Else
            'Save outstanding records..
            If PathCount > LAST_PATH_SAVED Then
                AddRecord(LAST_PATH_SAVED)
            End If
            toolActionStatus.Text = "Stopped..."
            toolActionStatus.Font = New Font(toolActionStatus.Font, FontStyle.Regular)
            StatusStrip1.BackColor = Color.FromKnownColor(KnownColor.Control)
            toolStartStopGame.Image = My.Resources.Chrono_Red
            toolStartStopGame.Text = "Start Game"
            toolStartStopGame.Font = New Font(toolStartStopGame.Font, FontStyle.Regular)
            bGameIsActive = False
            bTimeCriteriaIsIncremented = False
            frmTags.IsActive = bGameIsActive
            Me.timNoVid.Enabled = False
            Me.toolResetTimer2Zero.Visible = False
            Me.mnuResetTimer.Visible = False
            Me.toolUndo.Visible = False

            Me.mnuUpdateiPhone.Enabled = False
            Me.toolUpdateiPhone.Visible = False

            toolVideoTimeStatus.Text = GetTimeStringFromSeconds(0)
            TotalTC = 0
            Application.DoEvents()

            'Reload game if option specifies...
            Me.mnuCloseGame_Click1(sender, e)
            If UserPrefs.AutoReload Then
                GetRecords(propsCurrentGame.GameID, GetRecordCount(propsCurrentGame.GameID))
            End If

        End If
        GAME_HEADER_SAVED = False
    End Sub

    Private Sub mnuOpenGame_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOpenGame.Click
        frmOpenGames.MdiParent = Me
        frmOpenGames.Show()
        frmOpenGames.Focus()
    End Sub

    Public Sub mnuCloseGame_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCloseGame.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.GetType Is GetType(frmEventList) Then
                frm.Close()
            End If
        Next
        frmAnalysis.Close()

        PathCount = 0
        GameCount = 0
        PlayCount = Nothing
        Erase szGamesLoaded
        Erase GamePath
        Erase GamesCurrentlyOpen
        Me.mnuGenerateEDL.Enabled = False

        'Reset analysis criteria arrays
        Erase VisibleDescriptorList

        Me.toolProgressBar.Minimum = 0
        Me.toolProgressBar.Value = 0
        Me.toolGameCount.Text = GameCount.ToString & " (" & PathCount.ToString & ")"
        Me.toolActionStatus.Text = "Ready..."
    End Sub

    Private Sub WirelessCameraControlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WirelessCameraControlToolStripMenuItem.Click
        Try
            If Not My.Computer.Network.Ping(UserPrefs.CameraIP) Then
                MsgBox("The Sony Remote device is not available at this IP address: " & UserPrefs.CameraIP, MsgBoxStyle.Exclamation, Application.ProductName)
                Exit Sub
            End If

            frmWireless.MdiParent = Me
            frmWireless.Show()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Application.ProductName)
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub timNoVid_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timNoVid.Tick
        'GameStartTime = Now.Second
        GameTime_NoVideo = (My.Computer.Clock.TickCount / 1000) - GameTime_Start

        Me.toolVideoTimeStatus.Text = GetTimeStringFromSeconds(GameTime_NoVideo)
        Application.DoEvents()

    End Sub

    Private Sub toolRemoteCamera_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolRemoteCamera.Click
        Me.WirelessCameraControlToolStripMenuItem_Click(sender, e)

    End Sub

    Public Sub mnuNewPitch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewPitch.Click
        frmPitch.MdiParent = Me
        frmPitch.Show()

    End Sub

    Private Sub mnuOpenPlaylist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOpenPlaylist.Click
        '*
        '* Open new VPL window.
        '*

        Dim dlgPlaylist As New OpenFileDialog

        With dlgPlaylist
            .Title = "Open video playlist..."
            .Filter = "Plotter Video Playlist (*.vpl)|*.vpl"
            .Multiselect = True
            Dim res As DialogResult = .ShowDialog(Me)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

            For Each szFile As String In .FileNames()
                countVPL = countVPL + 1
                ReDim Preserve frmVPL(countVPL)
                frmVPL(countVPL) = New frmVideoPlayList(countVPL)
                frmVPL(countVPL).LoadVPL(szFile)
                frmVPL(countVPL).Show()
                frmVPL(countVPL).MdiParent = Me
            Next

            'SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uVideoPlaylist)
            'If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid) > 0 Then frmVPL(countVPL).formDirty = True
        End With
    End Sub

    Private Sub mnuOpenVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenVideo.Click
        'Load transient video into video window.
        Dim dlgOpenVideo As OpenFileDialog
        dlgOpenVideo = New OpenFileDialog

        With dlgOpenVideo
            .DefaultExt = "*.avi"
            .Title = "Open Video File..."
            .Filter = "AVI Video Files|*.avi|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                frmVideo.MdiParent = Me
                frmVideo.Show()
                frmVideo.Text = .FileName

                'Add video handling code here...
                If Not frmVideo.LoadVideoClip(.FileName, False) Then
                    frmVideo.Close()
                    Exit Sub
                End If
            End If
        End With

    End Sub


    Private Sub Customise_ShowText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Customise_ShowText.Click

        Dim style As ToolStripItemDisplayStyle = ToolStripItemDisplayStyle.None

        If Customise_ShowText.Checked Then
            style = ToolStripItemDisplayStyle.ImageAndText
        Else
            style = ToolStripItemDisplayStyle.Image
        End If

        For Each button As Object In Me.ToolStrip_MainMenu.Items
            button.DisplayStyle = style
        Next

        SaveSetting(AppName, "Settings", "ToolStrip_MainMenu_ShowText", Customise_ShowText.Checked)

    End Sub

    Private Sub mnuShowMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowMain.Click
        Me.ToolStrip_MainMenu.Visible = mnuShowMain.Checked
    End Sub

    Private Sub mnuSetSize_Large_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSetSize_Large.Click
        'Set autosize menu to 64x64
        Dim size As New Drawing.Size(64, 64)
        Me.ToolStrip_MainMenu.ImageScalingSize = size
        mnuSetSize_Small.Checked = False
        mnuSetSize_Normal.Checked = False
        mnuSetSize_Large.Checked = True
        SaveSetting(AppName, "Settings", "ToolStrip_MainMenu_Size", 64)

        Dim ref As ToolStripLayoutStyle = Me.ToolStrip_MainMenu.LayoutStyle
        Me.ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.Flow
        Me.ToolStrip_MainMenu.LayoutStyle = ref

    End Sub

    Private Sub mnuSetSize_Normal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSetSize_Normal.Click
        'Set autosize menu to 48x48
        Dim size As New Drawing.Size(48, 48)
        Me.ToolStrip_MainMenu.ImageScalingSize = size
        mnuSetSize_Small.Checked = False
        mnuSetSize_Normal.Checked = True
        mnuSetSize_Large.Checked = False
        SaveSetting(AppName, "Settings", "ToolStrip_MainMenu_Size", 48)

        Dim ref As ToolStripLayoutStyle = Me.ToolStrip_MainMenu.LayoutStyle
        Me.ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.Flow
        Me.ToolStrip_MainMenu.LayoutStyle = ref

    End Sub

    Private Sub mnuSetSize_Small_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetSize_Small.Click
        'Set autosize menu to 16x16
        Dim size As New Drawing.Size(16, 16)
        Me.ToolStrip_MainMenu.ImageScalingSize = size
        mnuSetSize_Small.Checked = True
        mnuSetSize_Normal.Checked = False
        mnuSetSize_Large.Checked = False
        SaveSetting(AppName, "Settings", "ToolStrip_MainMenu_Size", 24)

        Dim ref As ToolStripLayoutStyle = Me.ToolStrip_MainMenu.LayoutStyle
        Me.ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.Flow
        Me.ToolStrip_MainMenu.LayoutStyle = ref
    End Sub

    Private Sub ToolStripPanelLeft_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles ToolStripPanelLeft.ControlAdded
        Select Case e.Control.Name
            Case Is = "ToolStrip_MainMenu"
                ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
                SaveSetting(AppName, "Settings", "ToolStrip_MainMenu", "Left")
        End Select
    End Sub

    Private Sub ToolStripPanelRight_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles ToolStripPanelRight.ControlAdded
        Select Case e.Control.Name
            Case Is = "ToolStrip_MainMenu"
                ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
                SaveSetting(AppName, "Settings", "ToolStrip_MainMenu", "Right")
        End Select
    End Sub

    Private Sub ToolStripPanelTop_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles ToolStripPanelTop.ControlAdded
        Select Case e.Control.Name
            Case Is = "ToolStrip_MainMenu"
                ToolStrip_MainMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
                SaveSetting(AppName, "Settings", "ToolStrip_MainMenu", "Top")
        End Select
    End Sub



 

    Private Sub mnuExportGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportGame.Click

        If PathCount = 0 Then Exit Sub

        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
        '*  Verify newGP.mdb
        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

        'Hold empty database path
        Dim EmptyDB As String = My.Application.Info.DirectoryPath & "\newGP.mdb"

        'Create a blank copy of NewGP.mdb
        Dim ExportDialog As New SaveFileDialog
        ExportDialog.DefaultExt = "*.mdb"
        ExportDialog.Title = "Export current games..."
        ExportDialog.Filter = "MDB Database Files|*.mdb|All Files|*.*"
        ExportDialog.FileName = "Exported Games.mdb"

        If ExportDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'Execute database check
        If Not File.Exists(EmptyDB) Then
            MsgBox("File 'NewGP.mdb' not found." & vbCr & "Please re-install Pattern Plotter.", vbCritical, Application.ProductName)
            Exit Sub
        End If

        Dim szDestinationDB As String = ExportDialog.FileName

        'Show details of this operation:
        Dim szGames As String = Nothing
        For Each Game As GameProperties In GamesCurrentlyOpen
            szGames &= Game.GameID & vbNewLine
        Next

        Dim res As DialogResult = MsgBox("This action will save the following game(s):" & vbNewLine & vbNewLine & szGames & vbNewLine & _
        "...into the file:" & vbNewLine & vbNewLine & ExportDialog.FileName & vbNewLine & vbNewLine & _
        "Do you want to procede?", MsgBoxStyle.YesNo, "Export Games to file")
        If res = Windows.Forms.DialogResult.No Then Exit Sub

        If Not String.IsNullOrEmpty(ExportDialog.FileName) Then
            FileCopy(EmptyDB, ExportDialog.FileName)
        Else
            Exit Sub
        End If

        If Not CheckDBVersion(szDestinationDB) Then UpgradeDBVersion(szDestinationDB)


        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
        '*  Transfer data
        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

        'Save game information data to gamepath table.
        For Each Game As GameProperties In GamesCurrentlyOpen
            GAME_HEADER_SAVED = AddGameHeader(Game, szDestinationDB)
        Next

        'Save path and outcome information data to pathdata and outcomedata tables.
        For Each item As GameProperties In GamesCurrentlyOpen
            TransferGameFromDatabase(item.GameID, UserPrefs.dbPath, szDestinationDB)
        Next

        Me.toolActionStatus.Text = "Ready..."
        MsgBox("Data export complete...", MsgBoxStyle.Information, Application.ProductName)


    End Sub

    Private Sub mnuCloseCharts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCloseCharts.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.GetType Is GetType(frmChart) Or frm.GetType Is GetType(frmVideoPlayList) Or frm.GetType Is GetType(frmGraph) Then
                frm.Close()
            End If
        Next

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        frmTags.MdiParent = Me
        frmTags.Show()
        frmTags.mnuSaveTemplate_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenu_Options_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenu_Options.Click
        Me.toolOptions_Click(Nothing, Nothing)
    End Sub

    Private Sub mnuImportGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportGame.Click

        'Load transient video into video window.
        Dim dlgImportGame As OpenFileDialog
        dlgImportGame = New OpenFileDialog

        With dlgImportGame
            .DefaultExt = "*.mdb"
            .Title = "Import games from file"
            .Filter = "MDB Database Files|*.mdb|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else

                'Show details of this operation:
                Dim res As DialogResult = MsgBox("This action will add the game(s) stored in the file:" & vbNewLine & vbNewLine & .FileName & vbNewLine & vbNewLine & _
                "This game data will be added to the Pattern Plotter game database." & vbNewLine & _
                "Do you want to procede?", MsgBoxStyle.YesNo, "Import Games from file")
                If res = Windows.Forms.DialogResult.No Then Exit Sub

                'Import data into defult database.
                Dim LoadGames() As String = ImportDB(.FileName)

                If Not LoadGames Is Nothing Then
                    Dim boolGameLoaded As Boolean = False
                    For Each Game As String In LoadGames
                        Dim nLastCount As Integer = GetRecordCount(Game)
                        If GetRecords(Game, nLastCount, boolGameLoaded) > 0 Then boolGameLoaded = True
                    Next
                End If
            End If
        End With

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub mnuCreateSavedSearchSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmAutoSearch = New frmAnalysis
        frmAutoSearch.MdiParent = Me

        'Reformat window for automated search function.
        frmAutoSearch.Text = "New Automated Search"
        frmAutoSearch.grpAdvancedSearch.Visible = False
        frmAutoSearch.grpReportCharts.Top = frmAutoSearch.grpAdvancedSearch.Top
        frmAutoSearch.Height = 435
        frmAutoSearch.Height = frmAutoSearch.grpRunAnalysis.Bottom + 5
        frmAutoSearch.OK_Button.Text = "Save Search"

        'Populate criteria with current tags
        VisibleDescriptorList = Nothing
        If Not frmTags.Visible Then
            MsgBox("Please open a code/tags window first.", MsgBoxStyle.Exclamation, Application.ProductName)
            frmAutoSearch = Nothing

        Else

            'Add team/player names
            For Each c As Control In frmTags.Controls
                Dim button As EventButton.ctlEventButton = TryCast(c, EventButton.ctlEventButton)
                If Not button Is Nothing Then
                    Select Case DirectCast(button, EventButton.ctlEventButton).ButtonType
                        Case EventButton.ctlEventButton.ctlButtonType.Team
                            frmAutoSearch.AddPlayers(DirectCast(button, EventButton.ctlEventButton).Caption)
                        Case Is = EventButton.ctlEventButton.ctlButtonType.OutcomePos
                            frmAutoSearch.AddDescriptors(DirectCast(button, EventButton.ctlEventButton).Caption, OutcomeType.outPositive)
                        Case Is = EventButton.ctlEventButton.ctlButtonType.OutcomeNeg
                            frmAutoSearch.AddDescriptors(DirectCast(button, EventButton.ctlEventButton).Caption, OutcomeType.outNegative)
                        Case Is = EventButton.ctlEventButton.ctlButtonType.Descriptor
                            frmAutoSearch.AddDescriptors(DirectCast(button, EventButton.ctlEventButton).Caption, OutcomeType.outDescriptor)
                    End Select
                End If
            Next
            frmAutoSearch.AllowOutcomesUpdate = True


            frmAutoSearch.TopMost = True
            frmAutoSearch.Show()

        End If


    End Sub

    Public Sub mnuUpdateAutoCharts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdateiPhone.Click

        iPhoneUpdate()

    End Sub

    Private Sub toolResetTimer2Zero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolResetTimer2Zero.Click
        Me.mnuResetTimer_Click(sender, e)
    End Sub

    Private Sub toolUpdateChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolUpdateiPhone.Click
        Me.mnuUpdateAutoCharts_Click(sender, e)
    End Sub

    Private Sub mnuSystemInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSystemInfo.Click
        frmSystemInfo.MdiParent = Me
        frmSystemInfo.Show()
    End Sub

    Public Sub StatisticsReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatisticsReportsToolStripMenuItem.Click
        frmShowStats.MdiParent = Me
        frmShowStats.TopMost = True
        frmShowStats.UpdateMe()
        frmShowStats.Show()
    End Sub

    Private Sub toolReCalcRegions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolReCalcRegions.Click
        ReCalcRegions()
    End Sub

    Private Sub toolUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolUndo.Click
        frmTags.UndoLastEntry()
    End Sub

    Private Sub tooShowPitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooShowPitch.Click
        Me.mnuNewPitch2_Click(sender, e)
    End Sub

    Private Sub toolShowTags_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolShowTags.Click
        Me.EventTagsWindowToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub toolShowStatistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolShowStatistics.Click
        Me.StatisticsReportsToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub frmMain_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
    End Sub

    Private Sub ToolEnableVidTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolEnableVidTrans.Click
        UserPrefs.VideoTransmitEnabled = Me.ToolEnableVidTrans.Checked

        Me.toolAxillary.Visible = UserPrefs.VideoTransmitEnabled

        If UserPrefs.VideoTransmitEnabled Then
            If IO.Directory.Exists(UserPrefs.FileTransmitDestination) Then
                Me.toolAxillary.Text = "Transmit Status: Ready..."
            Else
                Me.toolAxillary.Text = "Transmit Status: Not available..."
            End If
        End If

    End Sub

    Private Sub AboutPatternPlotterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutPatternPlotterToolStripMenuItem.Click
        Try
            frmSplash.Show()
        Catch ex As Exception
            Dim wait As New Stopwatch
            wait.Start()
            Do
                Application.DoEvents()
            Loop Until wait.ElapsedMilliseconds > 500
            Try
                frmSplash.Show()
            Catch ex2 As Exception
            End Try
        Finally

        End Try

    End Sub

    Private Sub CrossTabsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrossTabsToolStripMenuItem.Click
        frmDMXTabs.MdiParent = Me
        frmDMXTabs.Text = "Data Mining Tools - CrossTabs"
        frmDMXTabs.Show()
    End Sub

    Private Sub mnuDM_SpatialClusters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDM_SpatialClusters.Click
        frmDMClusters.MdiParent = Me
        frmDMClusters.Text = "Data Mining Tools - Clustering"
        frmDMClusters.Show()

    End Sub

    Private Sub PathAssociationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PathAssociationsToolStripMenuItem.Click
        frmDMQuadrants.MdiParent = Me
        frmDMQuadrants.Text = "Data Mining Tools - Path Quadrant Analysis"
        frmDMQuadrants.Show()

    End Sub

    Private Sub mnuDataMining_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDataMining.Click

    End Sub

    Private Sub TimeSeriesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeSeriesToolStripMenuItem.Click
        frmDMTimeSeries.MdiParent = Me
        frmDMTimeSeries.Text = "Data Mining Tools - Time Series Analysis"
        frmDMTimeSeries.Show()

    End Sub

    Public Sub mnuPrintCharts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintCharts.Click
        pDocMulti = New PrintDocument
        PageCount = 1
        ctNumber = 0

        Dim dlgPrint As New PrintDialog
        dlgPrint.Document = pDocMulti
        Dim res As Windows.Forms.DialogResult = dlgPrint.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then

            Try
                dlgPrint.Document.Print()
            Catch ex As Exception
                MsgBox("This document has not printed.  Please check the printer is online.", MsgBoxStyle.Critical)
            End Try
        End If
        'If res = Windows.Forms.DialogResult.OK Then dlgPrint.Document.Print()

    End Sub

    Private Sub mnuPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintAll.Click
        Me.mnuPrintCharts_Click(sender, e)
    End Sub

    Private Sub pDocMulti_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles pDocMulti.BeginPrint
    End Sub

    Private Sub pDocMulti_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDocMulti.PrintPage

        Dim zX As Single = 0
        Dim zY As Single = 0
        Dim ChartCount As Integer = 0

        Dim gSize As Rectangle = e.MarginBounds
        gSize.Height /= 2


        Dim Scale As RectangleF = e.MarginBounds
        Scale.Width /= 2
        Scale.Height /= 2

        'Set window size parameters
        Dim Rect As RectangleF = New RectangleF(Scale.Width / 10, Scale.Height / 10, Scale.Width / 1.25, Scale.Height / 1.25)
        Dim frm As frmChart

        Dim formCount As Integer = 0

        For Each mdifrm As Form In Me.MdiChildren
            If mdifrm.GetType Is GetType(frmChart) Then
                formCount += 1
            End If
        Next

        For Each mdifrm As Form In Me.MdiChildren
            If mdifrm.GetType Is GetType(frmChart) Then
                If ctNumber < PageCount * 4 And ctNumber >= (PageCount - 1) * 4 Then
                    frm = mdifrm
                    Select Case ChartCount
                        Case Is = 0
                            Rect.X = e.MarginBounds.Left
                            Rect.Y = e.MarginBounds.Top - 40
                        Case Is = 1
                            Rect.X = (e.MarginBounds.Width / 2) + e.MarginBounds.Left + 20
                            Rect.Y = e.MarginBounds.Top - 40
                        Case Is = 2
                            Rect.X = e.MarginBounds.Left
                            Rect.Y = (e.MarginBounds.Height / 2) + e.MarginBounds.Top + 20
                        Case Is = 3
                            Rect.X = (e.MarginBounds.Width / 2) + e.MarginBounds.Left + 20
                            Rect.Y = (e.MarginBounds.Height / 2) + e.MarginBounds.Top + 20
                    End Select
                    Scale.Location = Rect.Location

                    'Draw Base Pitch
                    DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality, , , KnownColor.Black, KnownColor.Transparent)

                    '   e.Graphics.ScaleTransform(zX, zY)

                    Select Case UserPrefs.Sport
                        Case tSports.sHockey
                            zX = (Scale.Width / 1.25) / 90
                            zY = (Scale.Height / 1.25) / 150

                        Case tSports.sNetball
                            zX = (Scale.Width / 1.25) / 90
                            zY = (Scale.Height / 1.25) / 180

                        Case tSports.sRugbyLeague
                            zX = (Scale.Width / 1.25) / 68
                            zY = (Scale.Height / 1.25) / 122

                        Case tSports.sRugby7
                            zX = (Scale.Width / 1.25) / 70
                            zY = (Scale.Height / 1.25) / 120

                        Case tSports.sBasketball
                            zX = (Scale.Width / 1.25) / 50
                            zY = (Scale.Height / 1.25) / 94

                        Case tSports.sSoccer
                            zX = (Scale.Width / 1.25) / 95
                            zY = (Scale.Height / 1.25) / 150

                    End Select

                    'Draw Plays
                    Select Case frm.ChartType
                        Case frmChart.Chart.ctEventHeatMaps

                            If Not frm.HeatArray Is Nothing Then
                                e.Graphics.TranslateTransform((Rect.Left) - (PitchOffset.X * zX), (Rect.Top) - (PitchOffset.Y * zY))
                                e.Graphics.ScaleTransform(zX, zY)
                                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                                For x As Integer = 0 To frm.HeatArray.GetUpperBound(0) - 1
                                    For y As Integer = 0 To frm.HeatArray.GetUpperBound(1) - 1
                                        Dim ncolor As Color = frm.ClusterColorArray(x, y)
                                        e.Graphics.FillRegion(New SolidBrush(ncolor), New Region(New RectangleF(x + PitchOffset.X, y + PitchOffset.Y, 1, 1)))
                                    Next
                                Next
                                e.Graphics.ResetTransform()
                            End If

                        Case frmChart.Chart.ctBallSpeedHeatMaps

                            If Not frm.HeatArray Is Nothing Then
                                e.Graphics.TranslateTransform((Rect.Left) - (PitchOffset.X * zX), (Rect.Top) - (PitchOffset.Y * zY))
                                e.Graphics.ScaleTransform(zX, zY)
                                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                                For x As Integer = 0 To frm.HeatArray.GetUpperBound(0) - 1
                                    For y As Integer = 0 To frm.HeatArray.GetUpperBound(1) - 1
                                        Dim ncolor As Color = frm.ClusterColorArray(x, y)
                                        e.Graphics.FillRegion(New SolidBrush(ncolor), New Region(New RectangleF(x + PitchOffset.X, y + PitchOffset.Y, 1, 1)))
                                    Next
                                Next
                                e.Graphics.ResetTransform()
                            End If



                        Case frmChart.Chart.ctScatterPlots

                            If Not frm.ScatterPlotPoints Is Nothing Then

                                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                                For Each ScatterPoint As ScatterInfo In frm.ScatterPlotPoints
                                    'Cluster data
                                    ScatterPoint.Location.X = (ScatterPoint.Location.X * zX) + (Rect.Left) - (PitchOffset.X * zX)
                                    ScatterPoint.Location.Y = (ScatterPoint.Location.Y * zY) + (Rect.Top) - (PitchOffset.Y * zY)
                                    e.Graphics.FillEllipse(New SolidBrush(ScatterPoint.TeamColor), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(4, 4)))
                                Next
                            End If


                        Case frmChart.Chart.ctPlayerMaps
                            '*
                            '* Draw player maps
                            '*
                            If frm.Plays.Count > 0 Then
                                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                                e.Graphics.TranslateTransform((Rect.Left) - (PitchOffset.X * zX), (Rect.Top) - (PitchOffset.Y * zY))
                                e.Graphics.ScaleTransform(zX, zY)
                                Dim o As Integer = frm.numPathOpacity.Value
                                Dim k As Integer = 0
                                Dim nColor As Color
                                For Each gp As GamePlay.Instance In frm.Plays
                                    k += 1
                                    nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, frm.Plays.Count)
                                    gp.Pen.Brush = New SolidBrush(Color.FromArgb(20, nColor))
                                    gp.Pen.Width = UserPrefs.pmLineWidth / zX

                                    If gp.Lead Then
                                        If Not frm.chkShowReceives.Checked Then
                                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
                                        Else
                                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                                        End If

                                        gp.Pen.DashStyle = Drawing2D.DashStyle.Dot
                                        gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                                        gp.Pen.Width *= 1.5
                                        If Not frm.chkShowPossession.Checked Then gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                                    ElseIf gp.Posession Then
                                        If Not frm.chkShowReceives.Checked Then gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                                        If Not frm.chkShowPossession.Checked Then
                                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
                                        Else
                                            gp.Pen.Brush = New SolidBrush(nColor)
                                        End If
                                        gp.Pen.DashStyle = Drawing2D.DashStyle.Solid
                                        gp.Pen.Width *= 1.5
                                        gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                                    ElseIf gp.Lag Then
                                        If Not frm.chkShowDeliveries.Checked Then
                                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
                                        Else
                                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                                        End If
                                        gp.Pen.DashStyle = Drawing2D.DashStyle.DashDotDot
                                        gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                                        gp.Pen.Width *= 1.5
                                    Else
                                        If Not frm.chShowOtherPlay.Checked Then
                                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
                                        Else
                                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                                            gp.Pen.Width *= 0.5
                                        End If
                                    End If

                                    If gp.Path.PointCount > 0 Then

                                        e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                                    End If
                                Next
                                e.Graphics.ResetTransform()
                            End If

                        Case frmChart.Chart.ctPathways
                            '*
                            '* Draw pathways
                            '*
                            If frm.Plays.Count > 0 Then
                                e.Graphics.TranslateTransform((Rect.Left) - (PitchOffset.X * zX), (Rect.Top) - (PitchOffset.Y * zY))
                                e.Graphics.ScaleTransform(zX, zY)
                                Dim k As Integer = 0
                                Dim nColor As Color
                                For Each gp As GamePlay.Instance In frm.Plays
                                    k += 1
                                    nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, frm.Plays.Count)
                                    gp.Pen.Brush = New SolidBrush(nColor)
                                    gp.Pen.Width = UserPrefs.pmLineWidth / zX

                                    If gp.Path.PointCount > 0 Then
                                        e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                                    End If
                                Next
                            End If
                            e.Graphics.ResetTransform()

                            If frm.Captions.Count > 0 Then
                                e.Graphics.TranslateTransform((Rect.Left) - (PitchOffset.X * zX), (Rect.Top) - (PitchOffset.Y * zY))
                                e.Graphics.ScaleTransform(zX, zY)

                                If frm.ShowCaptions Then
                                    'Showing a single gameplay path, including the event captions.
                                    Dim lastCaption As New GamePlay.CaptionBox
                                    Dim n As Single = 0

                                    For Each gc As GamePlay.CaptionBox In frm.Captions
                                        gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font("Arial", 3, FontStyle.Regular, GraphicsUnit.Point)).ToSize

                                        If gc.BoxSize.Location = lastCaption.BoxSize.Location Then
                                            'Modify location of caption for viewing ease.
                                            gc.BoxSize.Offset(0, lastCaption.BoxSize.Height + zY)
                                            lastCaption.BoxSize.Height = gc.BoxSize.Bottom - lastCaption.BoxSize.Top
                                        Else
                                            lastCaption = gc
                                        End If

                                        If gc.Index = frm.CurrentCaptionIndex Then
                                            'This caption is selected.
                                            If frm.MouseDownOnCaption Then gc.BoxSize.Location = frm.SelectedCaptionBox.Location
                                            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightSalmon)), gc.BoxSize)
                                            frm.SelectedCaptionPathID = gc.ID
                                        Else
                                            'Not selected
                                            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightGreen)), gc.BoxSize)
                                        End If

                                        Dim aPen As Pen = New Pen(Color.Black, 0.3)
                                        e.Graphics.DrawRectangle(aPen, gc.BoxSize)
                                        e.Graphics.DrawString(gc.Text, New Font("Arial", 3, FontStyle.Regular, GraphicsUnit.Point), _
                                        New SolidBrush(Color.Black), gc.BoxSize.Location)

                                        'Save current caption locations and sizes for mouse-driven movement.
                                        If Not frm.CurrentCaptionInfo Is Nothing Then
                                            frm.CurrentCaptionInfo(gc.Index).CaptionRect = gc.BoxSize
                                        End If

                                    Next
                                    e.Graphics.ResetTransform()
                                End If
                            End If
                            e.Graphics.ResetTransform()

                        Case frmChart.Chart.ctClusters
                            '*
                            '* Draw quadrants
                            '*
                            If frm.ClusterArray.GetUpperBound(0) = UserPrefs.clHorizontalQ - 1 And frm.ClusterArray.GetUpperBound(1) = UserPrefs.clVerticalQ - 1 Then


                                Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
                                Dim h As Single = Rect.Height / UserPrefs.clVerticalQ
                                Dim nPen As New Pen(Color.Black, 1)
                                Dim nFont As New Font(Me.Font, FontStyle.Bold)

                                'Draw colored squares
                                If Not frm.ClusterArray Is Nothing Then
                                    For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                                        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                            If frm.ClusterArray(x, y) > 0 Then
                                                Dim x1 As Single = (x * w) + Rect.Left
                                                Dim y1 As Single = (y * h) + Rect.Top
                                                e.Graphics.FillRectangle(New SolidBrush(frm.ClusterColorArray(x, y)), x1, y1, w, h)
                                            End If
                                        Next
                                    Next
                                End If

                                'Overlay grid lines
                                For hLine As Integer = 1 To UserPrefs.clHorizontalQ - 1
                                    e.Graphics.DrawLine(nPen, (hLine * w) + Rect.Left, Rect.Top, (hLine * w) + Rect.Left, Rect.Top + Rect.Height)
                                Next
                                For vLine As Integer = 1 To UserPrefs.clVerticalQ - 1
                                    e.Graphics.DrawLine(nPen, Rect.Left, (vLine * h) + Rect.Top, Rect.Left + Rect.Width, (vLine * h) + Rect.Top)
                                Next

                                'Overlay frequency values
                                If Not frm.ClusterArray Is Nothing Then
                                    For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                                        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                            Dim ptxt As PointF = e.Graphics.MeasureString(frm.ClusterArray(x, y), Me.Font)
                                            Dim p As PointF = New PointF(Rect.Left + (w * x) + (w / 2) - (ptxt.X / 2), Rect.Top + (h * y) + (h / 2) - (ptxt.Y / 2))
                                            If frm.ClusterArray(x, y) > 0 Then
                                                e.Graphics.DrawString(frm.ClusterArray(x, y).ToString, nFont, Brushes.Black, p)
                                            End If
                                        Next
                                    Next
                                End If
                                'Finally, overlay pitch lines again.
                                RedrawOutline(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality)
                            End If

                    End Select

                    'Print details
                    Dim leftEdge As Single = Rect.Left
                    Dim vertSpace As PointF = e.Graphics.MeasureString(frm.lblChartType.Text, New Font("Arial", 14, FontStyle.Regular))
                    If frm.lblChartType.Text = "Event Frequency Heat Maps: " Then
                        e.Graphics.DrawString("Event Heat Maps:", New Font("Arial", 9, FontStyle.Italic), Brushes.DarkBlue, leftEdge, Rect.Top - (vertSpace.Y * 1.2))
                    ElseIf frm.lblChartType.Text = "Ball Speed Heat Maps: " Then
                        e.Graphics.DrawString("Ball Speed Maps:", New Font("Arial", 9, FontStyle.Italic), Brushes.DarkBlue, leftEdge, Rect.Top - (vertSpace.Y * 1.2))
                    ElseIf frm.lblChartType.Text = "Player Possession Maps: " Then
                        e.Graphics.DrawString("Player Maps:", New Font("Arial", 9, FontStyle.Italic), Brushes.DarkBlue, leftEdge, Rect.Top - (vertSpace.Y * 1.2))
                    Else
                        e.Graphics.DrawString(frm.lblChartType.Text, New Font("Arial", 9, FontStyle.Italic), Brushes.DarkBlue, leftEdge, Rect.Top - (vertSpace.Y * 1.2))
                    End If
                    e.Graphics.DrawString(frm.lblGameIDs.Text, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, leftEdge + 100, Rect.Top - (vertSpace.Y * 1.2))

                    e.Graphics.DrawString("Team Names:", New Font("Arial", 9, FontStyle.Underline), Brushes.Black, leftEdge, (Rect.Bottom + (1 * 15)))
                    e.Graphics.DrawString(frm.lblTeamNames.Text, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, leftEdge + 100, (Rect.Bottom + (1 * 15)))
                    e.Graphics.DrawString("Time Criteria:", New Font("Arial", 9, FontStyle.Underline), Brushes.Black, leftEdge, (Rect.Bottom + (2 * 15)))
                    e.Graphics.DrawString(frm.lblTimeCriterion.Text, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, leftEdge + 100, (Rect.Bottom + (2 * 15)))
                    e.Graphics.DrawString("Outcome Types:", New Font("Arial", 9, FontStyle.Underline), Brushes.Black, leftEdge, (Rect.Bottom + (3 * 15)))
                    e.Graphics.DrawString(frm.lblOutcomes.Text, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, leftEdge + 100, (Rect.Bottom + (3 * 15)))
                    e.Graphics.DrawString("Included Event Names:", New Font("Arial", 9, FontStyle.Underline), Brushes.Black, leftEdge, (Rect.Bottom + (4 * 15)))
                    e.Graphics.DrawString(frm.lblDescriptors.Text, New Font("Arial", 7, FontStyle.Regular), Brushes.Black, leftEdge, (Rect.Bottom + (5 * 15)))

                    ChartCount += 1

                    If ChartCount = 4 Then
                        ChartCount = 0
                    End If

                End If
                ctNumber += 1
            End If
        Next

        If formCount <= 4 Then
            e.HasMorePages = False
        Else

            If PageCount < Int(formCount / 4) + 1 Then
                e.HasMorePages = True
                PageCount += 1
                ctNumber = 0
            Else
                e.HasMorePages = False
            End If

        End If

    End Sub

    Private Sub mnuMergeComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeComplete.Click
        'Merge complete dataset with an existing.
        'Depends on a single game being open.
        If GameCount = 0 Or GamesCurrentlyOpen Is Nothing Then
            MsgBox("At least one game must be opened to merge data with.  This will be the primary game, and merged data will be added to it.")
            Exit Sub
        ElseIf GameCount > 1 Then
            MsgBox("Only one game can be opened to merge data.  This will be the primary game, and merged data will be added to it.")
            Exit Sub
        End If

        'Find database.
        Dim dlgImportGame As OpenFileDialog
        dlgImportGame = New OpenFileDialog

        With dlgImportGame
            .DefaultExt = "*.mdb"
            .Title = "Merge Dataset..."
            .Filter = "Pattern Plotter Game Files|*.mdb|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                'Call merging function.
                MergeComplete(GamesCurrentlyOpen(0), .FileName)
            End If
        End With




    End Sub

    Private Sub mnuMergeEventsOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeEventsOnly.Click
        'Merge complete dataset with an existing.
        'Depends on a single game being open.
        If GameCount = 0 Then
            MsgBox("At least one game must be opened to merge data with.  This will be the primary game, and merged data will be added to it.")
            Exit Sub
        ElseIf GameCount > 1 Then
            MsgBox("Only one game can be opened to merge data.  This will be the primary game, and merged data will be added to it.")
            Exit Sub
        End If


        'Find database.
        Dim dlgImportGame As OpenFileDialog
        dlgImportGame = New OpenFileDialog

        With dlgImportGame
            .DefaultExt = "*.mdb"
            .Title = "Merge Dataset Events..."
            .Filter = "Pattern Plotter Game Files|*.mdb|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                Dim res As MsgBoxResult = MsgBox("Please note: this action will take the 'Events' from the game data stored in:" & vbNewLine & vbNewLine & "Game A: " & .FileName & vbNewLine & _
                   vbNewLine & "..and these events will be added to the data stored in:" & vbNewLine & vbNewLine & "Game B: " & GamesCurrentlyOpen(0).GameID & vbNewLine & _
                   vbNewLine & "The descriptors and outcomes ONLY from Game A will added to the ball movements and remaining data from Game B." & vbNewLine, _
                    MsgBoxStyle.OkCancel, Application.ProductName)

                If res = MsgBoxResult.Cancel Then Exit Sub

                'Call merging function.
                MergeEvents(GamesCurrentlyOpen(0), .FileName)

            End If
        End With

    End Sub

    Private Sub mnuPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintPreview.Click
        pDocMulti = New PrintDocument
        PageCount = 1
        ctNumber = 0
        Dim dlgPrint As New PrintPreviewDialog
        dlgPrint.Document = pDocMulti
        dlgPrint.ShowDialog()

    End Sub

    Private Sub mnuResetTimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuResetTimer.Click
        GameTime_Start = My.Computer.Clock.TickCount / 1000
        GameTime_NoVideo = 0
    End Sub

    Private Sub ToolsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolsToolStripMenuItem.Click

    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub mnuIncrementTimeCriteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIncrementTimeCriteria.Click

        If Not bTimeCriteriaIsIncremented Then
            propsCurrentGame.TimeCriteria &= "_001"
            bTimeCriteriaIsIncremented = True
        Else
            Dim n As Integer = CInt(Mid(propsCurrentGame.TimeCriteria, Len(propsCurrentGame.TimeCriteria) - 2)) + 1
            propsCurrentGame.TimeCriteria = Mid(propsCurrentGame.TimeCriteria, 1, Len(propsCurrentGame.TimeCriteria) - 3) & Format(n, "000")
        End If
        szCurrentTimeCriteria = propsCurrentGame.TimeCriteria

        Me.toolActionStatus.Text = "Game Recording Active - " & propsCurrentGame.GameID & " (" & propsCurrentGame.TimeCriteria & ")"

    End Sub

  
 
    Private Sub mnuGenerateEDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGenerateEDL.Click

    End Sub

    Private Sub mnuExportCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportCSV.Click

        Dim dlgSaveEDL As New SaveFileDialog
        dlgSaveEDL.Title = "Export current game data to CSV..."
        dlgSaveEDL.FileName = "Exported Data.csv"
        dlgSaveEDL.Filter = "CSV Files|*.csv"
        dlgSaveEDL.DefaultExt = "*.csv"
        Dim res As DialogResult = dlgSaveEDL.ShowDialog()
        If Not res = Windows.Forms.DialogResult.Cancel Then
            Dim szFile As String = dlgSaveEDL.FileName
            modLoadSaveGame.ExportCSV(szFile)
        End If

    End Sub
End Class
