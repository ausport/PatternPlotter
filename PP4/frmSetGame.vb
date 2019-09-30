Imports System.Windows.Forms

Public Class frmSetGame

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        bTimeCriteriaIsIncremented = False

        If Not IO.File.Exists(UserPrefs.dbPath) Then
            MsgBox("The game database is not available.  Go to the Options window and check the location of the file GamePath.mdb", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With propsCurrentGame
            .Competition = Me.txtCompetition.Text
            .GameAuthor = Me.txtAuthor.Text
            Date.TryParse(Me.txtGameDate.Text, .GameDate)
            .GameDateString = Me.txtGameDate.Text
            .GameID = Me.txtGameID.Text
            .GameNotes = Me.txtNotes.Text
            .GameOpponent = Me.txtOpponent.Text
            .GameVenue = Me.txtVenue.Text
            .TimeCriteria = Me.cboTimeCriteria.Text
            szCurrentTimeCriteria = .TimeCriteria
            .GameNotes = Me.txtNotes.Text
        End With

        'Set file transmit status
        'UserPrefs.ChartTransmitEnabled = Me.chkActivateChartTransmit.Checked
        UserPrefs.VideoTransmitEnabled = Me.chkActivateVideoTransmit.Checked
        'SaveSetting(AppName, "Settings", "ChartTransmitEnabled", UserPrefs.ChartTransmitEnabled)
        SaveSetting(AppName, "Settings", "VideoTransmitEnabled", UserPrefs.VideoTransmitEnabled)

        If Me.chkActivateChartTransmit.Checked Or Me.chkActivateVideoTransmit.Checked Then
            If CurrentAutoChartTemplates Is Nothing And Me.chkActivateChartTransmit.Checked = True Then
                MsgBox("No automated search templates could be found.  Set the location of the automated templates using the Options window (F5).", MsgBoxStyle.Critical, Application.ProductName)
                Exit Sub
            End If



            frmMain.toolAxillary.Visible = True
            If IO.Directory.Exists(UserPrefs.FileTransmitDestination) Then
                frmMain.toolAxillary.Text = "Transmit Status: Ready..."
            Else
                frmMain.toolAxillary.Text = "Transmit Status: Not available..."
            End If
        Else
            frmMain.toolAxillary.Visible = False
        End If
        frmMain.mnuUpdateiPhone.Enabled = UserPrefs.iPhoneIsActive
        frmMain.toolUpdateiPhone.Visible = UserPrefs.iPhoneIsActive


        'Check if current gameID already exists..
        Dim AppendingGame As Boolean = False
        If GameIDExists(Me.txtGameID.Text) Then
            Dim msg As Microsoft.VisualBasic.MsgBoxResult = MsgBox(Me.txtGameID.Text & " already contains data.  Would you like to append more events this game?", _
            MsgBoxStyle.OkCancel, Application.ProductName)
            If msg = MsgBoxResult.Cancel Then Exit Sub
            AppendingGame = True
        End If

        If Me.optVideoCapture.Checked Then
            frmVideoCapture.MdiParent = frmMain
            frmVideoCapture.TopMost = True
            frmVideoCapture.Show()
        ElseIf Me.optVideoImport.Checked Then
            frmMain.toolOpenVideo_Click(sender, e)
        Else
            GameTime_Start = My.Computer.Clock.TickCount / 1000
            GameTime_NoVideo = 0
            frmMain.timNoVid.Enabled = True
            frmMain.toolResetTimer2Zero.Visible = True
            frmMain.mnuResetTimer.Visible = True
            frmMain.toolUndo.Visible = True
        End If


        frmMain.toolActionStatus.Text = "Game Recording Active - " & propsCurrentGame.GameID & " (" & propsCurrentGame.TimeCriteria & ")"
        frmMain.toolActionStatus.Font = New Font(frmMain.toolActionStatus.Font, FontStyle.Bold)
        frmMain.StatusStrip1.BackColor = Color.PaleGreen
        frmMain.toolStartStopGame.Image = My.Resources.Cancel
        bGameIsActive = True
        frmTags.IsActive = bGameIsActive
        frmMain.toolStartStopGame.Text = "Stop Game"
        frmMain.toolStartStopGame.Font = New Font(frmMain.toolStartStopGame.Font, FontStyle.Bold)

        TimeCriteriaAtStartOfPlay = propsCurrentGame.TimeCriteria


        'Show new events list window
        countE = countE + 1
        ReDim frmE(countE)
        frmE(countE) = New frmEventList(countE)
        frmE(countE).MdiParent = frmMain
        frmE(countE).Text = Me.txtGameID.Text & " (Active)"

        If AppendingGame Then
            'Get all records from current game and time criteria, and add to events.
            'Append from that point.
            PathCount = GetRecords(propsCurrentGame.GameID, GetRecordCount(propsCurrentGame.GameID), False, False)
            PlayCount = GetHighestPlayNumber(propsCurrentGame.GameID)

            LAST_PATH_SAVED = PathCount

        Else
            LAST_PATH_SAVED = 0
            Erase GamePath
            PathCount = 0
            PlayCount = 0
            ReDim GamePath(0)
        End If

        frmE(countE).Show()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        frmTags.Focus()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        frmMain.toolActionStatus.Text = "Ready..."
        frmMain.toolActionStatus.BackColor = Color.FromKnownColor(KnownColor.Control)

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSetGame_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.cboTimeCriteria.Text = "1st Half"
        Me.txtGameDate.Text = Today.ToLongDateString
        Me.txtGameID.Text = propsCurrentGame.GameID
        Me.txtOpponent.Text = propsCurrentGame.GameOpponent
        Me.txtVenue.Text = propsCurrentGame.GameVenue
        Me.txtCompetition.Text = propsCurrentGame.Competition
        Me.txtAuthor.Text = propsCurrentGame.GameAuthor
        Me.txtNotes.Text = propsCurrentGame.GameNotes

        Me.optVideoCapture.Checked = True
    End Sub


End Class
