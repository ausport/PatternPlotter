Imports System.Windows.Forms

Public Class videoCapture
    Private mVideo As mVideoClass
    Private mvarFileName As String
    Private mvarDirectory As String
    Private clsDirectory As System.IO.DriveInfo

    Private Sub videoCapture_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        On Error Resume Next
        If Not mVideo Is Nothing Then
            mVideo.Dispose()
            mVideo = Nothing
        End If
    End Sub

    Private Sub videoCapture_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set filename first..
        Dim dlgFileName As SaveFileDialog
        Dim res As DialogResult = Windows.Forms.DialogResult.Cancel

        'Get a new filename if one does not already exist.
        If mvarFileName Is Nothing Then
            dlgFileName = New SaveFileDialog
            dlgFileName.Filter = "AVI Files|*.avi"
            dlgFileName.InitialDirectory = UserPrefs.VideoCaptureDir
            res = dlgFileName.ShowDialog()
            If res <> Windows.Forms.DialogResult.Cancel Then
                mvarFileName = dlgFileName.FileName
            Else
                mvarFileName = Nothing
                Exit Sub
            End If
        End If

        If Not mVideo Is Nothing Then mVideo = Nothing
        mVideo = New mVideoClass
        If mVideo.ShowPreview(Me.picVideo, UserPrefs.PreviewAudioOnCapture) Then
            Me.lblDevice.Text = mVideo.CaptureDevice
            Me.lblDestination.Text = System.IO.Path.GetFileName(mvarFileName)
            clsDirectory = New System.IO.DriveInfo(System.IO.Path.GetPathRoot(mvarFileName))
            If clsDirectory.IsReady Then
                Dim nSpace As Single
                nSpace = clsDirectory.AvailableFreeSpace / 1000000000
                Me.lblSpaceRemaining.Text = "[" & clsDirectory.DriveFormat & "] " & _
                    nSpace.ToString("#0.00 Gb")
            Else
                Me.lblSpaceRemaining.Text = "Drive not available"
                Exit Sub
            End If
            'Enable record button.
            Me.cmdRecord.Enabled = True
        Else
            'Me.Close()
        End If
    End Sub

    Private Sub videoCapture_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not mVideo Is Nothing Then
            mVideo.SetDisplayHeight(Me.picVideo.Height)
            mVideo.SetDisplayWidth(Me.picVideo.Width)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If Not mVideo Is Nothing Then mVideo = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        cmdRefresh.Enabled = False  'Disable while refreshing

        If Not mVideo Is Nothing Then
            mVideo.Dispose()
            mVideo = Nothing
        End If

        If mvarFileName Is Nothing Then
            Me.videoCapture_Load(sender, e)
        Else
            mVideo = New mVideoClass
            If mVideo.ShowPreview(Me.picVideo) Then
                UpdateData()
            End If
        End If

        cmdRefresh.Enabled = True
    End Sub

    Private Sub cmdRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecord.Click
        If cmdRecord.Text = "Record" Then
            If mVideo.IsOnline(UserPrefs.PreviewAudioOnCapture) Then
                mVideo.StartCapture(mvarFileName, Me.picVideo)
            Else
                MsgBox("A problem has occured attempting to capture video: mVideo::IsOnline() failed.", MsgBoxStyle.Critical)
                Exit Sub
            End If
            cmdRecord.Text = "Stop"
            cmdPause.Enabled = True
            Me.Timer1.Enabled = True
            Exit Sub
        Else
            mVideo.StopCapture()
            cmdRecord.Text = "Record"
            cmdPause.Enabled = False
            Me.Timer1.Enabled = False

            'If user prefs select autoload after capture then run this...
            If UserPrefs.AutoPlay Then
                frmVideo.LoadVideoClip(mvarFileName, False)
                frmVideo.Text = mvarFileName
                frmVideo.Show()
                frmVideo.BringToFront()
                frmVideo.Focus()
            End If

            Me.Close()

            End If


    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        If Not mVideo Is Nothing Then
            mVideo.PauseCapture()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateData()
    End Sub

    Private Sub UpdateData()
        Me.lblDevice.Text = mVideo.CaptureDevice
        Me.lblDestination.Text = System.IO.Path.GetFileName(mvarFileName)
        clsDirectory = New System.IO.DriveInfo(System.IO.Path.GetPathRoot(mvarFileName))
        If clsDirectory.IsReady Then
            Dim nSpace As Single
            nSpace = clsDirectory.TotalFreeSpace / 1000000000
            Me.lblSpaceRemaining.Text = "[" & clsDirectory.DriveFormat & "] " & _
                nSpace.ToString("#0.00 Gb")
            If Not mVideo Is Nothing Then
                lblRecordingTime.Text = GetTimeStringFromSeconds(mVideo.RecordingTime, True)
            End If
        Else
            Me.lblSpaceRemaining.Text = "Drive not available"
        End If

    End Sub
End Class
