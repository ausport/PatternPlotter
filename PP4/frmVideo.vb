Public Class frmVideo
    Public szVideoFileName As String
    Private cVideo As clsDV
    Public startPoint As Double
    Public stopPoint As Double
    Private boolMouseDown As Boolean
    Private ExportDV As clsDV2Device
    Private bCompactWindow As Boolean = False

    Public Function LoadVideoClip(ByVal szFile As String, _
        Optional ByVal boolFullScreen As Boolean = False, _
        Optional ByVal nStartPoint As Double = 0, Optional ByVal nEndPoint As Double = 0, _
        Optional ByVal bAutoStart As Boolean = False) As Boolean

        On Error GoTo errCatch

        Dim bPlaying As Boolean = bAutoStart

        Dim bPreviousFSmode As Boolean = False
        If Not cVideo Is Nothing Then
            bPreviousFSmode = cVideo.FullScreen
            bPlaying = cVideo.Playing
            szVideoFileName = cVideo.FileName
        End If

        If cVideo Is Nothing Then cVideo = New clsDV

        If szFile <> szVideoFileName Then 'Need to load a new clip.
            'Stop current clip if playing.
            If Not cVideo Is Nothing Then cVideo.PausePlay()
            'Find location for new clip
            szFile = FindMediaFile(szFile)
            'Add video handling code here...
            If szFile = Nothing Or cVideo.LoadClip(szFile, Me) = False Then Return False
        End If

        With cVideo
            '.FullScreen = bPreviousFSmode   'Returns new instance of cVideo to its previous full screen mode.

            Me.startPoint = nStartPoint

            .CurrentPosition = nStartPoint
            Me.ResizeMe()
            If nEndPoint > 0 And nEndPoint < .Duration Then
                stopPoint = nEndPoint
            Else
                stopPoint = .Duration
            End If
            'Me.stopPoint = nEndPoint
            szVideoFileName = .FileName
            szCurrentVideoFile = .FileName

            If .Media_HasAudio Then
                .MuteAudio = Me.mnuMute.Checked
            End If

            With vidSlider
                .Minimum = 0
                .Maximum = Int(cVideo.Duration * cVideo.FPS)
                .Value = Int(cVideo.CurrentPosition * cVideo.FPS)
            End With

            cVideo.PausePlay()
            If bPlaying Or bAutoStart Then cVideo.StartPlay()

            If Not .FullScreen Then ShowTime()
            Timer1.Enabled = True


            If boolFullScreen Then .FullScreen = True

        End With

        frmMain.toolVideoTimeStatus.Text = GetTimeStringFromSeconds(cVideo.CurrentPosition, True)
        frmMain.toolVideoPath.Text = szVideoFileName

        Me.vidStatusFileName.Text = szVideoFileName
        Me.vidStatusFPS.Text = cVideo.FPS & " f.p.s."
        Me.vidStatusType.Text = "DV Video"
        boolCurrentVideoIsVPL = False   'Set as false by default, then set to true if run from a VPL

        Return True
        Exit Function

errCatch:
        LoadVideoClip = False
    End Function

    Public Sub ShowTime()
        If cVideo Is Nothing Then Exit Sub
        Dim nCP As Double

        nCP = cVideo.CurrentPosition
        Me.vidStatusCurrentPosition.Text = GetTimeStringFromSeconds(nCP)
        Me.vidSlider.Value = Int(nCP * cVideo.FPS)
        frmMain.toolVideoTimeStatus.Text = Me.vidStatusCurrentPosition.Text

        Me.Text = GetTimeStringFromSeconds(CSng(startPoint)) & "-->" & _
            GetTimeStringFromSeconds(CSng(stopPoint)) & "  (" & _
            vidStatusCurrentPosition.Text & ")"

        Application.DoEvents()

    End Sub

    Private Sub Step2()
        cVideo.PausePlay()
        cVideo.FrameAdvance(5)
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub Back2()
        cVideo.PausePlay()
        cVideo.FrameBack(5)
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub frmVideo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        szCurrentVideoFile = Nothing
        Timer1.Enabled = False
        SaveSetting(AppName, "Settings", "VideoWidth", Me.Width)
        SaveSetting(AppName, "Settings", "VideoHeight", Me.Height)
        SaveSetting(AppName, "Settings", "VideoCompact", Me.bCompactWindow)

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.ShowTime()
    End Sub

    Private Sub frmVideo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = GetSetting(AppName, "Settings", "VideoWidth", 650)
        Me.Height = GetSetting(AppName, "Settings", "VideoHeight", 450)
        Me.bCompactWindow = CBool(GetSetting(AppName, "Settings", "VideoCompact", False))
        SetCompactMode(bCompactWindow)

    End Sub

    Private Sub vidSlider_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles vidSlider.MouseDown
        cVideo.PausePlay()
        If Not cVideo.FullScreen Then
            ShowTime()
            Timer1.Enabled = False
            Me.Focus()
        End If
        boolMouseDown = True
    End Sub

    Private Sub vidSlider_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles vidSlider.MouseUp
        boolMouseDown = False
    End Sub

    Private Sub vidSlider_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles vidSlider.ValueChanged
        If boolMouseDown Then
            cVideo.CurrentPosition = vidSlider.Value / 25
            If Not cVideo.FullScreen Then
                ShowTime()
                Me.Focus()
            End If
        End If
    End Sub

    Private Sub frmVideo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuVideoOptions.Show(Me, e.X, e.Y)
        End If
    End Sub

    Private Sub frmVideo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        With cVideo

            'Handle keypress events.
            Select Case e.KeyCode

                Case Is = Keys.F And e.Control
                    Me.mnuFullScreen_Click(Nothing, Nothing)

                Case Is = Keys.N
                    If e.Control = True Then
                        Try
                            frmVPL(lastVPLFormUsed).mnuVPL_Next_Click(sender, e)
                        Catch ex As Exception

                        End Try
                    End If

                Case Is = Keys.P
                    If e.Control = True Then
                        Try
                            frmVPL(lastVPLFormUsed).mnuVPL_Prev_Click(sender, e)
                        Catch ex As Exception

                        End Try
                    End If

                Case Is = Keys.Space
                    'Space bar: play/pause
                    If .Playing Then
                        If .FullScreen Then .PausePlay() : Exit Sub
                        Me.mediaPause_Click(sender, e)
                    Else
                        If cVideo.FullScreen Then cVideo.StartPlay() : Exit Sub
                        Me.mediaPlay_Click(sender, e)
                        Try
                            frmVPL(lastVPLFormUsed).ResumePlay = True

                        Catch ex As Exception

                        End Try
                    End If

                Case Is = Keys.Escape
                    'Esc: revert from full screen
                    If .FullScreen Then
                        Me.mnuFullScreen_Click(sender, e)
                    End If

                Case Is = Keys.Right
                    'Right Arrow Key: advance 1 frame
                    Me.Timer1.Enabled = False
                    If e.Shift = False Then
                        If .FullScreen Then .FrameAdvance() : Exit Sub
                        .FrameAdvance()
                        ShowTime()
                    Else
                        If .FullScreen Then .FrameAdvance(5) : Exit Sub
                        Me.Step2()
                    End If

                Case Is = Keys.Left
                    'Left Arrow Key: back 1 frame
                    If e.Shift = False Then
                        If .FullScreen Then .FrameBack() : Exit Sub
                        .FrameBack()
                        ShowTime()
                    Else
                        If .FullScreen Then .FrameBack(5) : Exit Sub
                        Me.Back2()
                    End If

                Case Is = Keys.Up
                    'Go to in point or start
                    .CurrentPosition = startPoint
                    .PausePlay()
                    If Not .FullScreen Then ShowTime()

                Case Is = Keys.Down
                    'Go to out point or end
                    .CurrentPosition = stopPoint
                    .PausePlay()
                    If Not .FullScreen Then ShowTime()
            End Select
        End With

    End Sub

    Private Sub mnuPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPlay.Click
        On Error Resume Next
        If Not cVideo Is Nothing Then
            With cVideo
                If cVideo.FullScreen Then cVideo.StartPlay() : Exit Sub
                Me.mediaPlay_Click(sender, e)
                frmVPL(lastVPLFormUsed).ResumePlay = True
            End With
        End If
    End Sub

    Public Sub mnuPause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPause.Click
        On Error Resume Next
        If Not cVideo Is Nothing Then
            With cVideo
                If cVideo.FullScreen Then cVideo.PausePlay() : Exit Sub
                Me.mediaPause_Click(sender, e)
            End With
        End If

    End Sub

    Private Sub mnuPlayDouble_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPlayDouble.Click
        cVideo.PlaySpeed = 2
        Me.mnuPlay_Click(sender, e)
    End Sub

    Private Sub mnuPlayHalf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPlayHalf.Click
        cVideo.PlaySpeed = 0.5
        Me.mnuPlay_Click(sender, e)
    End Sub

    Private Sub mnuPlayQuarter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPlayQuarter.Click
        cVideo.PlaySpeed = 0.25
        Me.mnuPlay_Click(sender, e)
    End Sub

    Private Sub mnuPlayNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPlayNormal.Click
        cVideo.PlaySpeed = 1
        Me.mnuPlay_Click(sender, e)
    End Sub

    Private Sub mnuStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuStop.Click
        On Error Resume Next
        If Not cVideo Is Nothing Then
            With cVideo
                If cVideo.FullScreen Then cVideo.StopPlay() : Exit Sub
                Me.mediaStop_Click(sender, e)
            End With
        End If
    End Sub

    Private Sub mnuSetOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSetOptions.Click
        frmOptions.MdiParent = frmMain
        frmOptions.tabVideo.Show()
        frmOptions.Focus()

    End Sub

    Public Sub mnuFullScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFullScreen.Click
        mnuFullScreen.Checked = Not mnuFullScreen.Checked
        cVideo.FullScreen = mnuFullScreen.Checked
        Me.Timer1.Enabled = Not cVideo.FullScreen
        If Not cVideo.FullScreen Then

            Me.Activate()
        End If
        If cVideo.Playing Then
            cVideo.StartPlay()
        Else
            cVideo.PausePlay()
        End If
    End Sub

    Private Sub mnuSetIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSetIn.Click
        startPoint = cVideo.CurrentPosition
        If Not cVideo.FullScreen Then ShowTime()
        If Not frmVPL Is Nothing Then
            If boolCurrentVideoIsVPL = True And frmVPL(lastVPLFormUsed).Visible Then
                frmVPL(lastVPLFormUsed).vplGrid.Item("InPoint", frmVPL(lastVPLFormUsed).PlayingItemNow).Value = GetTimeStringFromSeconds(startPoint)
            End If
        End If

        If stopPoint < startPoint Then
            stopPoint = cVideo.CurrentPosition
            If Not cVideo.FullScreen Then ShowTime()
            If Not frmVPL Is Nothing Then
                If boolCurrentVideoIsVPL = True And frmVPL(lastVPLFormUsed).Visible Then
                    frmVPL(lastVPLFormUsed).vplGrid.Item("OutPoint", frmVPL(lastVPLFormUsed).PlayingItemNow).Value = GetTimeStringFromSeconds(stopPoint)
                End If
            End If
        End If

    End Sub

    Private Sub mnuSetOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSetOut.Click
        cVideo.PausePlay()
        stopPoint = cVideo.CurrentPosition
        If Not cVideo.FullScreen Then ShowTime()
        If Not frmVPL Is Nothing Then
            If boolCurrentVideoIsVPL = True And frmVPL(lastVPLFormUsed).Visible Then
                frmVPL(lastVPLFormUsed).vplGrid.Item("OutPoint", frmVPL(lastVPLFormUsed).PlayingItemNow).Value = GetTimeStringFromSeconds(stopPoint)
            End If
        End If
    End Sub

    Private Sub mnuVIDFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVIDFullScreen.Click
        Me.mnuFullScreen_Click(sender, e)
    End Sub

    Public Sub ResizeMe()
        Me.Panel2_Resize1(Nothing, Nothing)
    End Sub

    Private Sub mnuVID_Add2VPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVID_Add2VPL.Click
        frmConfirmStandAloneEvent.TopMost = True
        frmConfirmStandAloneEvent.ShowDialog()
        frmConfirmStandAloneEvent.Focus()

    End Sub

    Public Sub mnuVID_Save2AVI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVID_Save2AVI.Click
        Dim dlgSaveClip As SaveFileDialog = New SaveFileDialog
        dlgSaveClip.OverwritePrompt = True
        dlgSaveClip.Filter = "AVI Video Files (*.avi)|*.avi"
        If dlgSaveClip.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim szPath() As String = {szCurrentVideoFile}
        Dim nStart() As Double = {startPoint}
        Dim nStop() As Double = {stopPoint}
        Dim res As Boolean = False

        Dim trim As New clsVideoTrimmer
        Me.Timer1.Enabled = False
        Me.Text = "Exporting to AVI...  Please wait."
        Application.DoEvents()
        trim.WriteFile(szCurrentVideoFile, startPoint, stopPoint - startPoint, dlgSaveClip.FileName, cVideo.Media_HasAudio, frmMain)
        Do
            Application.DoEvents()
        Loop Until trim.GraphStatus = clsVideoTrimmer.GraphState.StateStopped
        Me.Timer1.Enabled = True
        MsgBox("Done...", MsgBoxStyle.Information, Application.ProductName)
        Exit Sub
        'Dim gbl_objTimeline As DexterLib.AMTimeline = BuildTimeLineFromMediaSources(1, szPath, nStart, nStop)
        'res = BuildAVIfromTimeLine(gbl_objTimeline, dlgSaveClip.FileName, frmMain.toolProgressBar)
        'ClearTimeline(gbl_objTimeline)
        'gbl_objTimeline = Nothing

        If res Then
            MsgBox("Done...", MsgBoxStyle.Information, Application.ProductName)
        Else
            MsgBox("There was a problem compiling that video clip...", MsgBoxStyle.Critical, Application.ProductName)
        End If
    End Sub

    Public Sub mnuVID_Send2DV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVID_Send2DV.Click

        ExportDV = New clsDV2Device
        Dim vidfile(0) As String
        Dim nStart(0) As Double
        Dim nStop(0) As Double
        vidfile(0) = szCurrentVideoFile
        nStart(0) = startPoint
        nStop(0) = stopPoint

        Dim bIsOnline As Boolean = ExportDV.GoOnline2(1, vidfile, nStart, nStop)
        Me.ToolStrip_Export2DV.Visible = bIsOnline
        Me.timExport.Enabled = bIsOnline
        Me.Timer1.Enabled = Not bIsOnline

    End Sub

    Private Sub toolExport_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolExport_Play.Click
        If Not ExportDV Is Nothing Then
            ExportDV.StartTransmit()
        End If
    End Sub

    Private Sub toolExport_Pause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolExport_Pause.Click
        If Not ExportDV Is Nothing Then
            ExportDV.PauseTransmit()
        End If
    End Sub

    Private Sub toolExport_Stop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolExport_Stop.Click
        If Not ExportDV Is Nothing Then
            Me.timExport.Enabled = False
            Me.Timer1.Enabled = True
            ExportDV.StopTransmit()
            Me.ToolStrip_Export2DV.Visible = False
            ExportDV.Dispose()
            ExportDV = Nothing
        End If
    End Sub

    Private Sub timExport_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timExport.Tick
        If Not ExportDV Is Nothing Then
            Me.Text = GetTimeStringFromSeconds(ExportDV.CurrentPosition) & " --> " & GetTimeStringFromSeconds(ExportDV.MediaDuration)

            Select Case ExportDV.GraphStatus
                Case clsDV2Device.GraphState.StatePaused
                    Me.Text &= " (Paused)"
                Case clsDV2Device.GraphState.StateRunning
                    Me.Text &= " (Running)"
                Case clsDV2Device.GraphState.StateStopped
                    Me.Text &= " (Stopped)"
            End Select

            If ExportDV.CurrentPosition >= ExportDV.MediaDuration Then
                Me.toolExport_Stop_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub mediaFrameBack_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        cVideo.FrameBack()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub mediaToEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cVideo.ToEnd()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub mediaPause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mediaPause.Click
        cVideo.PausePlay()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub mediaPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mediaPlay.Click
        If cVideo.EOF Then cVideo.StopPlay()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = True
            cVideo.StartPlay()
            Me.Focus()
        End If
        Try
            frmVPL(lastVPLFormUsed).ResumePlay = True
        Catch ex As Exception
        End Try

    End Sub

    Private Sub mediaToStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cVideo.ToStart()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub mediaFrameAdv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        cVideo.FrameAdvance()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            ShowTime()
            Me.Focus()
        End If
    End Sub

    Private Sub mediaRewind_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mediaRewind.MouseDown
        Me.Back2()
    End Sub

    Private Sub mediaFF_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mediaFF.MouseDown
        Me.Step2()
    End Sub

    Private Sub mnuVID_Prev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVID_Prev.Click
        frmVPL(countVPL).mnuVPL_Prev_Click(sender, e)
    End Sub

    Private Sub mnuVID_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVID_Next.Click
        frmVPL(countVPL).mnuVPL_Next_Click(sender, e)
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub frmVideo_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

    End Sub

    Private Sub frmVideo_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        Me.Refresh()
    End Sub

    Private Sub frmVideo_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.Refresh()
    End Sub

    Private Sub Panel2_Resize1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Resize
        If Not cVideo Is Nothing Then
            If cVideo.FullScreen Then Exit Sub

            With cVideo
                .SetDestPosition(New RectangleF(Me.Panel2.Location, Me.Panel2.Size))
            End With
        End If
    End Sub

    Private Sub frmVideo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus

    End Sub

    Private Sub mnuSelectInPlaylist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectInPlaylist.Click
        AddRemoveSelectInVPL(True)
    End Sub

    Private Sub mnuRemoveFromVPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveFromVPL.Click
        AddRemoveSelectInVPL(False)
    End Sub

    Private Sub AddRemoveSelectInVPL(ByVal Selected As Boolean)
        If Not frmVPL Is Nothing Then
            If boolCurrentVideoIsVPL = True And frmVPL(lastVPLFormUsed).Visible Then
                If frmVPL(lastVPLFormUsed).SelectCurrentRow(Selected) = -1 Then
                    MsgBox("Unable to select this item in a playlist", MsgBoxStyle.Critical, Application.ProductName)
                End If
            End If
        End If
    End Sub

    Private Sub mnuSaveCurrentClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveCurrentClip.Click
        Me.mnuVID_Save2AVI_Click(Nothing, Nothing)
    End Sub

    Private Sub mnuExportCurrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportCurrent.Click
        Me.mnuVID_Send2DV_Click(Nothing, Nothing)
    End Sub

    Private Sub mnuMute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMute.Click
        If Not cVideo Is Nothing Then
            mnuMute.Checked = Not mnuMute.Checked
            cVideo.MuteAudio = mnuMute.Checked
        End If
    End Sub

    Private Sub toolVideoCompact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolVideoCompact.Click
        SetCompactMode(toolVideoCompact.Checked)
    End Sub

    Private Sub SetCompactMode(ByVal bMode As Boolean)
        bCompactWindow = bMode
        toolVideoCompact.Checked = bMode
        Me.ToolStripPanelTop.Visible = Not bMode
        Me.ToolStrip1.Visible = Not bMode
    End Sub

    Protected Overrides Sub Finalize()
        If Not cVideo Is Nothing Then cVideo = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub frmVideo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not cVideo Is Nothing Then
            cVideo.StopPlay()
            cVideo.Release()
            cVideo = Nothing
        End If
        System.Windows.Forms.Application.DoEvents()
        frmMain.toolVideoPath.Text = "No video..."
        frmMain.toolVideoTimeStatus.Text = GetTimeStringFromSeconds(0, True)

    End Sub

    Private Sub ContextMenuStrip_PlayIcons_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub mediaStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mediaStop.Click
        cVideo.StopPlay()
        If Not cVideo.FullScreen Then
            Timer1.Enabled = False
            Me.Focus()
        End If

    End Sub

    Private Sub mediaFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mediaFF.Click

    End Sub

    Private Sub Panel2_Paint_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub mnuVideoOptions_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnuVideoOptions.Opening

    End Sub
End Class