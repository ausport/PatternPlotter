Public Class frmSonyCalibration
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim xWidth As Single = 0
    Dim yHeight As Single = 0
    Dim xMargin As Single = 0
    Dim yMargin As Single = 0

    Dim Pitch As RectangleF = Nothing

    Private IsSettingPosition As Boolean = False
    Private IsPositionSet As Boolean = False
    Private IsSettingZero As Boolean = False
    Private IsZeroSet As Boolean = False
    Private IsSettingQuadrantCount As Boolean = False

    Private ZeroPosition As PointF
    Private ZeroAngle As Double 'Neutral camera position relative to the pitch.  NB: from left edge, straight across ground is 90deg.


    Private Enum CameraLocation
        None = 0
        TopEdge = 1
        LeftEdge = 2
        BottomEdge = 3
        RightEdge = 4
        TopLeftCorner = 5
        TopRightCorner = 6
        BottomRightCorner = 7
        BottomLeftCorner = 8
    End Enum
    Private ThisCameraLocation As CameraLocation = CameraLocation.None

    Dim pic As New PictureBox

    Private Sub cmdPositionRemoteDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPositionRemoteDevice.Click

        Pitch = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)


        'Establish camera sideline dimension
        Dim nPoint As PointF = pic.Location

        If nPoint.X < Pitch.Left And nPoint.Y < Pitch.Top Then
            ThisCameraLocation = CameraLocation.TopLeftCorner

        ElseIf nPoint.X > Pitch.Right And nPoint.Y < Pitch.Top Then
            ThisCameraLocation = CameraLocation.TopRightCorner

        ElseIf nPoint.X < Pitch.Left And nPoint.Y > Pitch.Bottom Then
            ThisCameraLocation = CameraLocation.BottomLeftCorner

        ElseIf nPoint.X > Pitch.Right And nPoint.Y > Pitch.Bottom Then
            ThisCameraLocation = CameraLocation.BottomRightCorner

        ElseIf nPoint.Y < Pitch.Top And nPoint.X > Pitch.Left And nPoint.X < Pitch.Right Then
            ThisCameraLocation = CameraLocation.TopEdge

        ElseIf nPoint.Y > Pitch.Bottom And nPoint.X > Pitch.Left And nPoint.X < Pitch.Right Then
            ThisCameraLocation = CameraLocation.BottomEdge

        ElseIf nPoint.Y > Pitch.Top And nPoint.Y < Pitch.Bottom And nPoint.X < Pitch.Left Then
            ThisCameraLocation = CameraLocation.LeftEdge

        ElseIf nPoint.Y > Pitch.Top And nPoint.Y < Pitch.Bottom And nPoint.X > Pitch.Right Then
            ThisCameraLocation = CameraLocation.RightEdge
        End If


        'Position has been set - finalise selection and enable step 2
        Me.lblStep1.Enabled = False
        Me.cmdPositionRemoteDevice.Enabled = False
        Me.lblStep2.Enabled = True
        Me.lblStep2.Visible = True
        Me.cmdSetViewpoint.Enabled = True
        Me.cmdSetViewpoint.Visible = True

        IsSettingPosition = False
        IsPositionSet = True

        IsZeroSet = False
        IsSettingZero = True

        Me.lblDirections.Text = "Now, click on the playing area to set the neutral camera viewpoint (zero degrees)."
        'Me.Cursor = Cursors.HSplit
        Me.picPitch.Refresh()


    End Sub

    Private Sub picPitch_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseClick
        If Not IsSettingQuadrantCount Then
            If IsSettingPosition Then
                'Add camera icon and identify location
                Me.Cursor = Cursors.Default

                pic.Image = My.Resources.SonyRemote
                pic.SizeMode = PictureBoxSizeMode.StretchImage
                pic.Size = New Size(20, 20)
                pic.BringToFront()

                pic.Location = GetSidelinePosition(e.Location)
                pic.Visible = True
                Me.picPitch.Controls.Add(pic)


            ElseIf IsSettingZero Then
                ZeroPosition = e.Location
                Me.picPitch.Refresh()
            End If
        End If

    End Sub

    Private Function GetSidelinePosition(ByVal loc As Point) As Point
        Dim Adjusted As Point = Nothing
        'If the new point is between the left and right x margins then the y point should be in the middle of the y margin that is closest to the new point

        If loc.X > picPitch.Width / 10 And loc.X < picPitch.Right - (picPitch.Width / 10) Then
            If loc.Y < picPitch.Height / 2 Then
                Adjusted.Y = (picPitch.Height / 20) - 10
            Else
                Adjusted.Y = picPitch.Bottom - ((picPitch.Height / 20) + 20)
            End If

        Else
            Adjusted.Y = loc.Y
        End If

        If loc.Y > picPitch.Height / 10 And loc.Y < picPitch.Bottom - (picPitch.Height / 10) Then
            If loc.X < picPitch.Width / 2 Then
                Adjusted.X = (picPitch.Width / 20) - 10
            Else
                Adjusted.X = picPitch.Right - ((picPitch.Width / 20) + 20)
            End If

        Else
            Adjusted.X = loc.X
        End If

        Return Adjusted
    End Function

    Private Sub picPitch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPitch.Paint
        'Set window size parameters
        Pitch = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)
        DrawPitch(UserPrefs.Sport, e.Graphics, Pitch, Drawing2D.SmoothingMode.HighQuality, , , , KnownColor.White)

        'Draw arrow from camera origin through centre of the ground
        'Dim pen As New Pen(Color.FromArgb(110, 0, 0, 0), 10)

        Dim linGrBrush As New Drawing2D.LinearGradientBrush( _
           New Point(pic.Location), _
           New Point(picPitch.Width / 2, ZeroPosition.Y), _
           Color.FromArgb(0, 255, 0, 0), _
           Color.FromArgb(255, 255, 0, 0))

        Dim pen As New Pen(linGrBrush, 10)

        pen.StartCap = Drawing2D.LineCap.NoAnchor
        pen.EndCap = Drawing2D.LineCap.ArrowAnchor

        'If IsPositionSet Then
        '    'Draw arrow from set position to centre of the pitch
        '    e.Graphics.DrawLine(pen, pic.Location, New Point(picPitch.Width / 2, picPitch.Height / 2))
        If IsSettingZero = True And IsSettingQuadrantCount = False Then
            'Draw arrow from set position to centre of the pitch
            If Not ThisCameraLocation = CameraLocation.BottomEdge And Not ThisCameraLocation = CameraLocation.TopEdge Then
                e.Graphics.DrawLine(pen, pic.Location, New Point(picPitch.Width / 2, ZeroPosition.Y))
                ZeroPosition.X = picPitch.Width / 2
            Else
                linGrBrush = New Drawing2D.LinearGradientBrush( _
                   New Point(pic.Location), _
                   New Point(ZeroPosition.X, picPitch.Height / 2), _
                   Color.FromArgb(0, 255, 0, 0), _
                   Color.FromArgb(255, 255, 0, 0))

                pen = New Pen(linGrBrush, 10)
                pen.StartCap = Drawing2D.LineCap.NoAnchor
                pen.EndCap = Drawing2D.LineCap.ArrowAnchor


                e.Graphics.DrawLine(pen, pic.Location, New Point(ZeroPosition.X, picPitch.Height / 2))
                ZeroPosition.Y = picPitch.Height / 2
            End If
        End If

        If IsSettingQuadrantCount Then
            Dim dX As Single = 0, dY As Single = 0, hyp As Single = 0

            'Overlay panning/tilting quadrants
            pen = New Pen(Color.DarkGray, 1)
            e.Graphics.DrawLine(pen, pic.Location.X, pic.Location.Y, picPitch.Right, pic.Location.Y)

            pen = New Pen(Color.Black, 1)
            Select Case ThisCameraLocation
                Case CameraLocation.LeftEdge
                    'i. get x-plane length to furthest edge.
                    dX = Math.Max(ConvertSign(pic.Location.X - Pitch.Left), ConvertSign(pic.Location.X - Pitch.Right))

                    ' For theta As Single = ZeroAngle - 50 To ZeroAngle + 50 Step (100 / 50)
                    For theta As Single = 1 To 89 Step 3
                        'ii.get y - plane
                        dY = GetOppositeLength(theta, dX) * -1


                        e.Graphics.DrawLine(pen, pic.Location.X, pic.Location.Y, dX + pic.Location.X, pic.Location.Y + dY)
                        'Debug.Print("Theta: " & theta.ToString & " X: " & dX.ToString & " Y: " & dY.ToString)
                    Next


            End Select




            ''Line at zero
            'pen = New Pen(Color.DarkBlue, 2)
            'pen.StartCap = Drawing2D.LineCap.RoundAnchor
            'pen.EndCap = Drawing2D.LineCap.SquareAnchor



        End If


    End Sub

    Private Sub picPitch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPitch.Resize
        Me.picPitch.Refresh()
    End Sub

    Private Sub frmSonyCalibration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub cmdBeginCalibration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeginCalibration.Click
        cmdPositionRemoteDevice.Enabled = True
        lblStep1.Enabled = True

        lblStep2.Enabled = False
        lblStep2.Visible = False
        Me.cmdSetViewpoint.Enabled = False
        Me.cmdSetViewpoint.Visible = False

        Me.cboPan.Enabled = False
        Me.cboPan.Visible = False
        Me.cboTilt.Enabled = False
        Me.cboTilt.Visible = False
        Me.lblStep3.Enabled = False
        Me.lblStep3.Visible = False
        Me.lblPan.Enabled = False
        Me.lblPan.Visible = False
        Me.lblTilt.Enabled = False
        Me.lblTilt.Visible = False

        ZeroAngle = 0

        IsPositionSet = False
        IsSettingPosition = True
        IsZeroSet = False
        IsSettingZero = False
        IsSettingQuadrantCount = False

        Me.lblDirections.Text = "Click to set the remote cameras position relative to the playing area."
        Me.Refresh()
    End Sub

    Private Sub cmdSetViewpoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetViewpoint.Click

        'Get deviation from 90 degress

        'Horizontal line of sight - get change in y from a straight line along x axis from camera position.
        Dim dY As Single = ZeroPosition.Y - pic.Location.Y
        Dim dX As Single = ZeroPosition.X - pic.Location.X
        Dim c As Single = Math.Sqrt((dY ^ 2) + (dX ^ 2))

        'Obtain angle offset between straight line horizontal, and the zero position of the camera.
        Select Case ThisCameraLocation
            Case CameraLocation.LeftEdge
                ZeroAngle = 90 + Math.Atan(dY / dX) / (Math.PI / 180)
            Case CameraLocation.RightEdge
                ZeroAngle = 270 + Math.Atan(dY / dX) / (Math.PI / 180)
            Case CameraLocation.BottomEdge
                ZeroAngle = 360 + Math.Atan((dX * -1) / dY) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360
            Case CameraLocation.TopEdge
                ZeroAngle = 180 + Math.Atan((dX * -1) / dY) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360
            Case CameraLocation.TopLeftCorner
                ZeroAngle = 90 - Math.Atan((dY * -1) / dX) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360
            Case CameraLocation.TopRightCorner
                ZeroAngle = 270 - Math.Atan((dY * -1) / dX) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360
            Case CameraLocation.BottomRightCorner
                ZeroAngle = 270 - Math.Atan((dY * -1) / dX) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360
            Case CameraLocation.BottomLeftCorner
                ZeroAngle = 90 - Math.Atan((dY * -1) / dX) / (Math.PI / 180)
                If ZeroAngle >= 360 Then ZeroAngle -= 360

        End Select

        Me.Text = ZeroAngle.ToString

        Me.pic.Visible = False

        Me.cboPan.Enabled = True
        Me.cboPan.Visible = True
        Me.cboTilt.Enabled = True
        Me.cboTilt.Visible = True
        Me.lblStep3.Enabled = True
        Me.lblStep3.Visible = True
        Me.lblPan.Enabled = True
        Me.lblPan.Visible = True
        Me.lblTilt.Enabled = True
        Me.lblTilt.Visible = True

        Me.lblStep2.Enabled = False
        Me.cmdSetViewpoint.Enabled = False

        'Enable Draw grid.
        IsSettingQuadrantCount = True
        Me.lblDirections.Text = "Set the panning and tilting Quadrant counts."

        Me.Refresh()

    End Sub
End Class