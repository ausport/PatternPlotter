Module modSonyCamera
    Const POS_X As Single = 2448 / 170
    Const POS_Y As Single = 972 / 47.5
    Public CAM As VISWirelessCam.CameraClass

    Private mvarIP As String
    Private mvarPort As Short

    Public Enum Resolution
        n736x480 = 763
        n640x480 = 640
        n480x360 = 480
        n320x240 = 320
        n160x120 = 160
    End Enum

    Public Enum Calibration
        Centre = 0
        TopLeft = 1
        TopRight = 2
        BottomRight = 3
        BottomLeft = 4
        None = 5
    End Enum

    Public Structure CameraPreset
        Public szName As String
        Public pLocation As Point
        Public pZoom As String
        Public nZoom As Single
        Public pPitchLocation As Point
        Public Quadrant As Calibration
    End Structure

    Public CameraPositions() As CameraPreset
    Public SonyCameraIsConnected As Boolean = False


    Public Function ConnectCamera(ByVal IPString As String, Optional ByVal PortA As Short = 80) As String


        Dim ConnectString As String = Nothing

        'Remove previous instance if there is one...
        If Not CAM Is Nothing Then
            CAM = Nothing
            SonyCameraIsConnected = False
        End If

        'Restart
        CAM = New VISWirelessCam.CameraClass

        'Open connection
        If CAM.OpenConnection() Then
            If CAM.Connect(IPString, PortA, "", "") Then
                ConnectString = CAM.getHost & ":" & CAM.getPort
                mvarPort = CAM.getPort
                mvarIP = CAM.getHost
                SonyCameraIsConnected = True
                frmMain.toolRemoteCamera.Visible = SonyCameraIsConnected
            End If
        End If

        Return ConnectString
    End Function

    Public Sub DisconnectCamera()
        CAM.CloseConnection()
        SonyCameraIsConnected = False
        frmMain.toolRemoteCamera.Visible = SonyCameraIsConnected

    End Sub

    Public Function SendPTZFcommand(ByVal ptzf As String) As Boolean
        'If CAM.Connect(mvarIP, mvarPort, "", "") Then
        If CAM.OpenRequest("command/ptzf.cgi") Then
            CAM.SendRequest(ptzf)
            Return True
            Exit Function
        End If
        'End If
        Return False
    End Function

    Public Sub GotoPreset(ByVal uPos As CameraPreset)
        If SonyCameraIsConnected Then
            If Not uPos.pZoom Is Nothing Then
                If CAM.OpenRequest("command/ptzf.cgi") Then
                    Dim szReq As String = "visca=" & GetHexFromZoom(uPos.pZoom)
                    CAM.SendRequest(szReq)
                End If
            End If
            If CAM.OpenRequest("command/ptzf.cgi") Then
                Dim szReq As String = "visca=" & GetHexFromAngle(uPos.pLocation)
                CAM.SendRequest(szReq)
            End If

        End If

    End Sub

    Public Function GetRawPosition(ByVal szResponse As String) As Point

        'Parse postion information from camera response.

        'NB: 9050_0w_0w_0w_0w_0z_0z_0z_0z_FF
        Try
            Dim n As String = ""
            For i As Integer = 6 To 12 Step 2
                n = n & Mid(szResponse, i, 1)
            Next i
            GetRawPosition.X = Convert.ToInt32(n, 16)

            n = ""
            For i As Integer = 14 To 20 Step 2
                n = n & Mid(szResponse, i, 1)
            Next i
            GetRawPosition.Y = Convert.ToInt32(n, 16)

        Catch ex As Exception

            MsgBox("There might be a network connection problem with the Remote Camera device.", MsgBoxStyle.Critical, Application.ProductName)

        End Try

        Return GetRawPosition

    End Function

    Public Function GetZoomPosition(ByVal szResponse As String) As String

        'Parse postion information from camera response.

        Dim n As String = ""
        'NB: 9050_0p_0q_0r_0s_FF
        Try
            For i As Integer = 6 To 12 Step 2
                n = n & Mid(szResponse, i, 1)
            Next i
            'GetZoomPosition = Convert.ToInt32(n, 16)

        Catch ex As Exception

            MsgBox("There might be a network connection problem with the Remote Camera device.", MsgBoxStyle.Critical, Application.ProductName)

        End Try

        Return n


    End Function


    Public Function GetCalibratedPosition(ByVal szResponse As String) As Point

        If szResponse Is Nothing Then Exit Function
        Dim nPos As Point = GetRawPosition(szResponse)

        ' 0 Deg = 0
        ' +170 Deg = 2448

        'Determine if angle is greater than 0
        If nPos.X <= 2448 Then
            'Positive angle
            nPos.X = nPos.X / POS_X
        Else
            'Negative angle
            nPos.X = (((nPos.X - 60630) / POS_X) - 340)
        End If

        If nPos.Y <= 972 Then
            'Positive angle
            nPos.Y = nPos.Y / POS_Y
        Else
            'Negative angle
            nPos.Y = ((66089 - nPos.Y) / POS_Y) * -1
        End If

        Return nPos

    End Function

    Public Function GetHexSetPreset(ByVal nIndex As Integer) As String
        'Return command string for nIndex preset.
        Dim szRes As String = "8101043F01" & Hex(nIndex) & "pFF"
        Return szRes
    End Function

    Public Function GetHexGetPreset(ByVal nIndex As Integer) As String
        'Return command string for nIndex preset.
        Dim szRes As String = "8101043F02" & Hex(nIndex) & "pFF"
        Return szRes
    End Function

    Public Function GetHexFromZoom(ByVal szValue As String) As String
        Dim szRez As String = "81010447"
        For i As Integer = 1 To szValue.Length
            szRez = szRez & "0" & Mid(szValue, i, 1)
        Next
        szRez = szRez & "FF"

        Return szRez

    End Function

    Public Function GetHexFromAngle(ByVal nValue As Point) As String
        'NB return hex value for Absolute Position in format: 810106031814_0w_0w_0w_0w_0z_0z_0z_0z_FF
        Dim szRes As String = "810106021814"

        If nValue.X > 0 Then
            'Positive angle
            szRes = szRes & PadHex(Hex(Int(nValue.X * POS_X)))
        ElseIf nValue.X = 0 Then
            'Home
            szRes = szRes & PadHex("0")
        Else
            'Negative angle
            szRes = szRes & PadHex(Hex(65536 + Int(nValue.X * POS_X)))
        End If

        If nValue.Y > 0 Then
            'Positive angle
            szRes = szRes & PadHex(Hex(Int(nValue.Y * POS_Y)))
        ElseIf nValue.Y = 0 Then
            'Home
            szRes = szRes & PadHex("0")
        Else
            'Negative angle
            szRes = szRes & PadHex(Hex(66089 + Int(nValue.Y * POS_Y)))
        End If

        Return szRes & "FF"
    End Function

    Private Function PadHex(ByVal szHex As String) As String
        Dim retVal As String = ""

        For i As Integer = 1 To szHex.Length
            retVal = retVal & "0" & Mid(szHex, i, 1)
        Next

        If szHex.Length < 4 Then
            For i As Integer = 1 To 4 - szHex.Length
                retVal = "00" & retVal
            Next
        End If

        Return retVal

    End Function

End Module
