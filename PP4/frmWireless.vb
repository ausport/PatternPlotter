Imports System.Windows.Forms

Public Class frmWireless
    Dim szSettingsFile As String


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DisconnectCamera()
        Me.Close()
    End Sub

    Private Sub Wireless_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not String.IsNullOrEmpty(szSettingsFile) Then
                SaveSetting(AppName, "Settings", "SonySettingsFile", szSettingsFile)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Application.ProductName)
        End Try

    End Sub

    Private Sub Wireless_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim szConnected As String = ConnectCamera(UserPrefs.CameraIP)

        If Not String.IsNullOrEmpty(szConnected) Then
            Me.lblIPAddress.Text = szConnected
            Me.lblStatus.Text = "Active"
            Me.lblStatus.ForeColor = Color.Green
        End If

        'Set update timer
        Me.Timer1.Enabled = SonyCameraIsConnected

        'Set view list style
        Me.lstPresets.View = View.List

        'Load previous settings
        LoadSonySettings(GetSetting(AppName, "Settings", "SonySettingsFile"))

    End Sub

    Private Sub cmdHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHome.Click

        SendPTZFcommand("visca=81010604FF")

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub StopCamMovement()

        'Stop Pan/Tilt
        SendPTZFcommand("visca=8101060118140303FF")

        If CAM.OpenRequest("command/visca-inquiry.cgi") Then
            CAM.SendRequest("visca=81090612FF")
            Dim pTemp As Point = GetCalibratedPosition(CAM.GetResponse())
            Me.lblStatus.Text = "Active -> " & pTemp.ToString
            Me.trackHorizontal.Value = pTemp.X
            Me.trackVertical.Value = pTemp.Y
        End If


    End Sub

    Private Sub cmdDown_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdDown.MouseDown
        SendPTZFcommand("visca=8101060118140301FF")
    End Sub

    Private Sub cmdDown_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdDown.MouseUp
        StopCamMovement()
    End Sub

    Private Sub cmdUp_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdUp.MouseDown
        SendPTZFcommand("visca=8101060118140302FF")
    End Sub

    Private Sub cmdUp_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdUp.MouseUp
        StopCamMovement()
    End Sub

    Private Sub cmdLeft_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdLeft.MouseDown
        SendPTZFcommand("visca=8101060118140203FF")
    End Sub

    Private Sub cmdLeft_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdLeft.MouseUp
        StopCamMovement()
    End Sub

    Private Sub cmdRight_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdRight.MouseDown
        SendPTZFcommand("visca=8101060118140103FF")
    End Sub

    Private Sub cmdRight_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdRight.MouseUp
        StopCamMovement()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PictureBox1.ImageLocation = "http://" & UserPrefs.CameraIP & "/oneshotimage.jpg"
    End Sub

    Private Sub cmdWide_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdWide.MouseDown
        SendPTZFcommand("visca=8101040737FF")
    End Sub

    Private Sub cmdWide_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdWide.MouseUp
        'Stop Zoom
        SendPTZFcommand("visca=8101040700FF")
    End Sub


    Private Sub cmdTele_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdTele.MouseDown
        SendPTZFcommand("visca=8101040727FF")
    End Sub

    Private Sub cmdTele_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdTele.MouseUp
        'Stop Zoom
        SendPTZFcommand("visca=8101040700FF")
    End Sub

    Private Sub cmdNewPreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewPreset.Click
        'Add new preset
        Dim szRes As String = InputBox("Please specify a name for this preset:", , "New Position " & _
            Me.lstPresets.Items.Count.ToString)

        If szRes.Length = 0 Then Exit Sub

        'Get current camera positions
        Dim pZoom As String = ""
        If CAM.OpenRequest("command/visca-inquiry.cgi") Then
            CAM.SendRequest("visca=81090447FF")
            pZoom = GetZoomPosition(CAM.GetResponse())
        End If

        If CAM.OpenRequest("command/visca-inquiry.cgi") Then
            CAM.SendRequest("visca=81090612FF")
            Dim pTemp As Point = GetCalibratedPosition(CAM.GetResponse())
            lstPresets.Items.Add(szRes & ": " & pTemp.ToString & "{Z=" & Convert.ToInt32(pZoom, 16) & "}", "SonyRemote.jpg")


            'Save data to preset list
            ReDim Preserve CameraPositions(Me.lstPresets.Items.Count)
            CameraPositions(Me.lstPresets.Items.Count).pLocation = pTemp
            CameraPositions(Me.lstPresets.Items.Count).szName = szRes
            CameraPositions(Me.lstPresets.Items.Count).pZoom = pZoom
            CameraPositions(Me.lstPresets.Items.Count).pPitchLocation = Nothing
        End If

    End Sub

    Private Sub lstPresets_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPresets.MouseDoubleClick

        Dim ret As ListViewHitTestInfo = Me.lstPresets.HitTest(e.X, e.Y)
        Me.Text = CameraPositions(ret.Item.Index).szName

        Dim szReq As String = "visca=" & GetHexFromAngle(CameraPositions(ret.Item.Index).pLocation)
        SendPTZFcommand(szReq)

        szReq = "visca=" & GetHexFromZoom(CameraPositions(ret.Item.Index).pZoom)
        SendPTZFcommand(szReq)

        Me.lblStatus.Text = "Active -> " & CameraPositions(ret.Item.Index).pLocation.ToString
    End Sub

    Private Sub cmdSavePresets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSavePresets.Click
        Dim dlgPlaylist As New SaveFileDialog

        If Me.lstPresets.Items.Count = 0 Then Exit Sub

        With dlgPlaylist
            .Title = "Save remote camera settings..."
            .Filter = "Remote Camera Settings (*.ppx)|*.ppx"
            .OverwritePrompt = True
            Dim res As DialogResult = .ShowDialog(frmMain)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim fnum As Integer = FreeFile()

            FileOpen(fnum, .FileName, OpenMode.Output)
            For Each gRow As CameraPreset In CameraPositions
                If Not gRow.szName Is Nothing Then
                    Print(fnum, gRow.szName & ";")
                    Print(fnum, gRow.pLocation.X & ";")
                    Print(fnum, gRow.pLocation.Y & ";")
                    Print(fnum, gRow.pZoom & ";")
                    Print(fnum, gRow.pPitchLocation.X & ";")
                    Print(fnum, gRow.pPitchLocation.Y & ";")
                End If
            Next
            FileClose(fnum)

        End With
        Exit Sub

errCatch:
        Err.Clear()
    End Sub

    Private Sub cmdLoadPresets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadPresets.Click
        Dim dlgPresets As New OpenFileDialog
        With dlgPresets
            .Title = "Open remote camera settings..."
            .Filter = "Remote Camera Settings (*.ppx)|*.ppx"
            Dim res As DialogResult = .ShowDialog(frmMain)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

            LoadSonySettings(.FileName)
        End With

    End Sub

    Public Function LoadSonySettings(ByVal szFileName As String) As Integer

        If szFileName.Length = 0 Then
            Return 0
            Exit Function
        End If

        Dim currentRow() As String
        Dim szParse As Microsoft.VisualBasic.FileIO.TextFieldParser = My.Computer.FileSystem.OpenTextFieldParser(szFileName, ";")
        Dim cCount As Integer = 1

        Erase CameraPositions
        Me.lstPresets.Items.Clear()

        While Not szParse.EndOfData
            Try
                currentRow = szParse.ReadFields()

                For i As Integer = currentRow.GetLowerBound(0) To currentRow.GetUpperBound(0) - 6 Step 6
                    ReDim Preserve CameraPositions(cCount)
                    CameraPositions(cCount).szName = currentRow(i)
                    CameraPositions(cCount).pLocation.X = currentRow(i + 1)
                    CameraPositions(cCount).pLocation.Y = currentRow(i + 2)
                    CameraPositions(cCount).pZoom = currentRow(i + 3)
                    CameraPositions(cCount).pPitchLocation.X = currentRow(i + 4)
                    CameraPositions(cCount).pPitchLocation.Y = currentRow(i + 5)
                    CameraPositions(cCount).nZoom = Convert.ToInt32(CameraPositions(cCount).pZoom, 16)

                    'If cCount = 0 Then
                    '    CameraPositions(cCount).Quadrant = Calibration.Centre
                    'Else
                    '    Dim CP As New CameraPreset  'Centre point
                    '    CP = CameraPositions(0)

                    '    If CameraPositions(cCount).pPitchLocation.X < CP.pPitchLocation.X And CameraPositions(cCount).pPitchLocation.Y < CP.pPitchLocation.Y Then
                    '        'Top left quadrant
                    '        CameraPositions(cCount).Quadrant = Calibration.TopLeft

                    '    ElseIf CameraPositions(cCount).pPitchLocation.X >= CP.pPitchLocation.X And CameraPositions(cCount).pPitchLocation.Y < CP.pPitchLocation.Y Then
                    '        'Top right quadrant
                    '        CameraPositions(cCount).Quadrant = Calibration.TopRight

                    '    ElseIf CameraPositions(cCount).pPitchLocation.X >= CP.pPitchLocation.X And CameraPositions(cCount).pPitchLocation.Y >= CP.pPitchLocation.Y Then
                    '        'bottom right quadrant
                    '        CameraPositions(cCount).Quadrant = Calibration.BottomRight

                    '    ElseIf CameraPositions(cCount).pPitchLocation.X < CP.pPitchLocation.X And CameraPositions(cCount).pPitchLocation.Y >= CP.pPitchLocation.Y Then
                    '        'bottom left quadrant
                    '        CameraPositions(cCount).Quadrant = Calibration.BottomLeft

                    '    End If

                    'End If

                    If currentRow(i) = "Pitch Calibration Point" Then
                        lstPresets.Items.Add(CameraPositions(cCount).szName & ": " & _
                        CameraPositions(cCount).pLocation.ToString & _
                        "{Z=" & Convert.ToInt32(CameraPositions(cCount).pZoom, 16) & "}", "AddPoint.png")
                    Else
                        lstPresets.Items.Add(CameraPositions(cCount).szName & ": " & _
                        CameraPositions(cCount).pLocation.ToString & _
                        "{Z=" & Convert.ToInt32(CameraPositions(cCount).pZoom, 16) & "}", "SonyRemote.jpg")
                    End If
                    cCount += 1
                Next


            Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                MsgBox("Line " & ex.Message & _
                "is not valid and will be skipped.", MsgBoxStyle.Critical, Application.ProductName)
            End Try
        End While
        szParse.Close()
        szSettingsFile = szFileName

        'Set quadrants
        Return cCount


errCatch:
        Err.Clear()

    End Function

    Private Sub cmdDeletePreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeletePreset.Click
        Dim res As DialogResult = MsgBox("Are you sure you want to clear this set of camera presets?", MsgBoxStyle.OkCancel, Application.ProductName)
        If res = Windows.Forms.DialogResult.OK Then
            Erase CameraPositions
            Me.lstPresets.Items.Clear()
            szSettingsFile = ""
        End If
    End Sub


    Private Sub cmdPitchCalibration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPitchCalibration.Click
        frmPitch.MdiParent = frmMain
        frmPitch.Show()
        frmPitch.CalibrationMode = True

        Exit Sub

    End Sub

    Public Sub AddCalibrationPoint(ByVal cPoint As Point)
        'Add new preset
        'Dim szRes As String = InputBox("Please specify a name for this preset:", , "New Position " & _
        '    Me.lstPresets.Items.Count.ToString)


        'Get current camera positions
        Dim pZoom As String = ""
        If CAM.OpenRequest("command/visca-inquiry.cgi") Then
            CAM.SendRequest("visca=81090447FF")
            pZoom = GetZoomPosition(CAM.GetResponse())
        End If

        If CAM.OpenRequest("command/visca-inquiry.cgi") Then
            CAM.SendRequest("visca=81090612FF")
            Dim pTemp As Point = GetCalibratedPosition(CAM.GetResponse())
            lstPresets.Items.Add("Pitch Calibration Point" & ": " & pTemp.ToString & "{Z=" & Convert.ToInt32(pZoom, 16) & "}", "AddPoint.png")


            'Save data to preset list
            ReDim Preserve CameraPositions(Me.lstPresets.Items.Count)
            CameraPositions(Me.lstPresets.Items.Count).pLocation = pTemp
            CameraPositions(Me.lstPresets.Items.Count).szName = "Pitch Calibration Point"
            CameraPositions(Me.lstPresets.Items.Count).pZoom = pZoom
            CameraPositions(Me.lstPresets.Items.Count).nZoom = Convert.ToInt32(pZoom, 16)
            CameraPositions(Me.lstPresets.Items.Count).pPitchLocation = cPoint
        End If


    End Sub
End Class
