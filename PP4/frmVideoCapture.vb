Imports System.Windows.Forms

Public Class frmVideoCapture
    Private mVideo As clsVideoCaptureClass
    Private mvarFileName As String
    Private mvarDirectory As String
    Private clsDirectory As System.IO.DriveInfo

    Public ReadOnly Property VideoTC() As Double
        Get
            Return mVideo.RecordingTime
        End Get
    End Property


    Private Sub videoCapture_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        bVideoCaptureCurrent = False
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
            dlgFileName.FileName = propsCurrentGame.GameID & " - " & szCurrentTimeCriteria & ".avi"
            res = dlgFileName.ShowDialog()
            If res <> Windows.Forms.DialogResult.Cancel Then
                mvarFileName = dlgFileName.FileName
            Else
                mvarFileName = Nothing
                Exit Sub
            End If
        End If

        If Not mVideo Is Nothing Then mVideo = Nothing
        mVideo = New clsVideoCaptureClass
        If mVideo.ShowPreview(Me.picVideo, UserPrefs.PreviewAudioOnCapture, mVideo.CaptureDeviceIndex) Then
            Me.lblDevice.Text = mVideo.CaptureDeviceString
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
        Dim nCaptureDevice As Integer = mVideo.CaptureDeviceIndex

        If Not mVideo Is Nothing Then
            mVideo.Dispose()
            mVideo = Nothing
        End If

        If mvarFileName Is Nothing Then
            Me.videoCapture_Load(sender, e)
        Else
            mVideo = New clsVideoCaptureClass(nCaptureDevice)
            If mVideo.ShowPreview(Me.picVideo, , nCaptureDevice) Then
                UpdateData()
            End If
        End If

        cmdRefresh.Enabled = True
    End Sub


    Public Function StopStartCapture() As String

        'Stop existing recording.
        mVideo.StopCapture()
        Dim LastDevice As Integer = mVideo.CaptureDeviceIndex
        mVideo.Dispose()
        If Not mVideo Is Nothing Then mVideo = Nothing

        'Update Current szCurrentVideoFile
        szCurrentVideoFile = UpdateFileNameIndex(szCurrentVideoFile)

        'Restart recording..
        mVideo = New clsVideoCaptureClass(LastDevice)
        If mVideo.ShowPreview(Me.picVideo, , LastDevice) Then

            If mVideo.IsOnline(UserPrefs.PreviewAudioOnCapture, mVideo.CaptureDeviceIndex) Then
                mVideo.StartCapture(szCurrentVideoFile, Me.picVideo)
            Else
                MsgBox("A problem has occured attempting to capture video: mVideo::IsOnline() failed.", MsgBoxStyle.Critical, Application.ProductName)
            End If

            bVideoCaptureCurrent = True
        End If

        Return szCurrentVideoFile
    End Function

    Public Function CheckLastFileIndex(ByVal szBaseString As String, ByVal szDir As String) As Integer
        'Returns the highest index (xxx) of files with a base string in a specified directory.
        Dim nTopIndex As Integer = 0
        Try
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(szDir, FileIO.SearchOption.SearchTopLevelOnly, szBaseString & "*")
                Dim n As Integer = GetFileNameIndex(foundFile)
                If n > nTopIndex Then nTopIndex = n
            Next
        Catch ex As Exception
            Return 0
        End Try

        Return nTopIndex

    End Function

    Public Function UpdateFileNameIndex(ByVal szFilename As String) As String
        'Takes a filename - eg Test1_001.avi, and increments it to Test1_002,avi.

        Dim dotIndex As Integer = 0

        For i As Integer = Len(szFilename) To 1 Step -1
            If Mid$(szFilename, i, 1) = "_" Then
                Return Microsoft.VisualBasic.Strings.Left(szFilename, Len(szFilename) - 7) & Format(GetFileNameIndex(szFilename) + 1, "000") & ".avi"
                Exit Function
            End If
        Next

        szFilename = Mid$(szFilename, 1, Len(szFilename) - 4) & "_001.avi"


        Return szFilename

    End Function

    Private Function GetFileNameIndex(ByVal szFilename As String) As Integer
        'Returns the index (xxx) of a file with the format C:\filename_xxx.avi

        For i As Integer = Len(szFilename) - 4 To 1 Step -1

            If Mid$(szFilename, i, 1) = "_" Then
                Dim x As Integer = Mid$(szFilename, i + 1, 3)
                Return x
            End If
        Next
    End Function

    Public Sub cmdRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecord.Click
        If cmdRecord.Text = "Record" Then
            If mVideo.IsOnline(UserPrefs.PreviewAudioOnCapture, mVideo.CaptureDeviceIndex) Then
                mVideo.StartCapture(mvarFileName, Me.picVideo)
            Else
                MsgBox("A problem has occured attempting to capture video: mVideo::IsOnline() failed.", MsgBoxStyle.Critical, Application.ProductName)
                Exit Sub
            End If
            cmdRecord.Text = "Stop"
            cmdPause.Enabled = True
            Me.cmdRefresh.Enabled = False
            Me.Timer1.Enabled = True
            szCurrentVideoFile = mvarFileName
            bVideoCaptureCurrent = True
            frmMain.toolVideoPath.Text = "Capturing --> " & szCurrentVideoFile

            Exit Sub
        Else
            mVideo.StopCapture()
            cmdRecord.Text = "Record"
            cmdPause.Enabled = False
            Me.cmdRefresh.Enabled = True
            Me.Timer1.Enabled = False

            'If user prefs select autoload after capture then run this...
            If UserPrefs.AutoPlay Then
                frmVideo.MdiParent = frmMain
                frmVideo.Show()

                frmVideo.LoadVideoClip(mvarFileName, False)
                frmVideo.Text = mvarFileName
            End If
            szCurrentVideoFile = Nothing
            bVideoCaptureCurrent = False
            frmMain.toolVideoPath.Text = "Stopped --> Video complete."

            UpdateData()
            Me.Close()
        End If


    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        If Not mVideo Is Nothing Then
            If mVideo.GraphStatus = clsVideoCaptureClass.GraphState.StateRunning Then
                mVideo.PauseCapture()
                frmMain.toolVideoPath.Text = "Paused --> " & szCurrentVideoFile
                cmdPause.Text = "Resume"
            Else
                mVideo.PauseCapture()
                frmMain.toolVideoPath.Text = "Capturing --> " & szCurrentVideoFile
                cmdPause.Text = "Pause"
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateData()
    End Sub

    Private Sub UpdateData()
        If Not mVideo Is Nothing Then
            Me.lblDevice.Text = mVideo.CaptureDeviceString
            Me.lblDestination.Text = szCurrentVideoFile
            frmMain.toolVideoPath.Text = szCurrentVideoFile
            clsDirectory = New System.IO.DriveInfo(System.IO.Path.GetPathRoot(mvarFileName))
            If clsDirectory.IsReady Then
                Dim nSpace As Single
                nSpace = clsDirectory.TotalFreeSpace / 1000000000
                Me.lblSpaceRemaining.Text = "[" & clsDirectory.DriveFormat & "] " & _
                    nSpace.ToString("#0.00 Gb")
                If Not mVideo Is Nothing Then
                    lblRecordingTime.Text = GetTimeStringFromSeconds(mVideo.RecordingTime + TotalTC, True)
                    frmMain.toolVideoTimeStatus.Text = lblRecordingTime.Text
                    Application.DoEvents()
                End If
            Else
                Me.lblSpaceRemaining.Text = "Drive not available"
            End If
        End If

    End Sub

    Private Sub picVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picVideo.Click

    End Sub
End Class
