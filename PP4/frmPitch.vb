Public Class frmPitch
    Public idForm As Integer
    Dim LastPoints As Point
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim Rect As RectangleF = Nothing

    Dim xWidth As Single = 0
    Dim yHeight As Single = 0
    Dim xMargin As Single = 0
    Dim yMargin As Single = 0
    Dim nSpatialCorrection As PointF = New PointF(1, 1)

    Dim szLastEvent As String = Nothing

    Dim uBackColor As Color
    Dim uForeColor As Color
    Dim uText As String

    Public Plays As New Microsoft.VisualBasic.Collection
    Public Captions As New Microsoft.VisualBasic.Collection
    Dim NewPlay As New GamePlay.Instance
    Dim NewCaption As New GamePlay.CaptionBox

    Dim Calibrate As Boolean = False

    Dim Index_Play As Integer = 1
    Dim lastPoint As Point
    Dim boolResumePath As Boolean = False    'This is true to allow the resumption of continuous ball carries after a pass.

    Dim nVerticalPadding As Integer = 3             'Sets the amount of vertical padding from path point.
    Dim nHorizontalPadding As Integer = 3

    Dim boolStartNewPlay As Boolean = True 'Set to true for the start of a new play.
    Dim boolPlayTerminated As Boolean = False 'Set to true after adding an outcome to terminate play.

    Dim NewPreset As New CameraPreset

    Private Sub frmPitch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Send pitch key press data to Tags form to handle.
        frmTags.evtButton_KeyDown(sender, e)
    End Sub

    Private Sub frmPitch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = CInt(GetSetting(AppName, "Settings", "PitchWidth", Me.Size.Width.ToString))
        Me.Height = CInt(GetSetting(AppName, "Settings", "PitchHeight", Me.Size.Height.ToString))

     
    End Sub

    Private Sub frmPitch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting(AppName, "Settings", "PitchWidth", Me.Size.Width.ToString)
        SaveSetting(AppName, "Settings", "PitchHeight", Me.Size.Height.ToString)
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Calibrate Then
            Me.Text = "Pitch Calibration Mode: " & UserPrefs.Sport.ToString
        Else
            Me.Text = "Pattern Plotter Pitch: " & UserPrefs.Sport.ToString
        End If
        'UserPrefs.Sport = tSports.sHockey
    End Sub

    Public Property CalibrationMode() As Boolean
        Get
            Return Calibrate
        End Get
        Set(ByVal value As Boolean)
            Calibrate = value
            If Calibrate Then
                Me.Text = "Pitch Calibration Mode: " & UserPrefs.Sport.ToString
                Me.Cursor = Cursors.Cross
            Else
                Me.Text = "Pattern Plotter Pitch: " & UserPrefs.Sport.ToString
                Me.Cursor = Cursors.Arrow
            End If

        End Set
    End Property

    Private Sub mnuResetPitch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Width = 500
        Me.Height = Me.Width / 0.6
    End Sub

    Public Overloads Sub StartNewPlay()
        'Local vars don't change, just set boolStartNewPlay to true
        boolStartNewPlay = True
    End Sub
    Public Overloads Sub StartNewPlay(ByVal szTeamName As String, ByVal ForeColor As Color, ByVal BackColor As Color)
        'Set local vars to hold text and color data, then set boolStartNewPLay to true, then on mouseclick, add this code.
        uBackColor = BackColor
        uForeColor = ForeColor
        uText = szTeamName

        boolStartNewPlay = True

    End Sub

    Public Sub AddCaption(ByVal szText As String, ByVal ForeColor As Color, ByVal BackColor As Color, Optional ByVal bTerminate As Boolean = False)
        Try
            NewCaption = New GamePlay.CaptionBox
            NewCaption.BackColor = BackColor
            NewCaption.ForeColor = ForeColor
            NewCaption.FontStyle = FontStyle.Italic
            NewCaption.BoxSize.X = NewPlay.Path.GetLastPoint.X + nHorizontalPadding
            NewCaption.BoxSize.Y = NewPlay.Path.GetLastPoint.Y + nVerticalPadding
            NewCaption.Text = szText
            Captions.Add(NewCaption)

            nVerticalPadding = nVerticalPadding + 19

            If bTerminate Then boolStartNewPlay = bTerminate

        Catch ex As Exception

        End Try
        Me.Refresh()

    End Sub

    Public Sub ClearPitch()
        NewCaption = Nothing
        NewPlay = Nothing
        Captions.Clear()
        Plays.Clear()

        Refresh()

    End Sub

    Private Sub picPitch_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseClick


        'Mouse click event... ball carry

        If Not bGameIsActive Then Exit Sub

        If uText Is Nothing Then Exit Sub

        If boolStartNewPlay Then
            'Firstly, check if an unsaved play exists.
            'Then if so, save previous play to DB

            If PathCount >= 0 Then GamePath(PathCount).RecordID = AddRecord(LAST_PATH_SAVED)

            'Set new Graphics Path.
            NewPlay = New GamePlay.Instance
            NewPlay.Pen = New Pen(uBackColor, 3)
            NewPlay.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
            NewPlay.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
            NewPlay.Pen.DashStyle = Drawing2D.DashStyle.Solid
            NewPlay.Path = New Drawing2D.GraphicsPath

            'Reset plays collection and add new play.
            Plays.Clear()
            Index_Play = 1
            Plays.Add(NewPlay, Index_Play)

            nVerticalPadding = 3

            'Set new Caption
            NewCaption = New GamePlay.CaptionBox
            NewCaption.BackColor = uBackColor
            NewCaption.ForeColor = uForeColor
            NewCaption.FontStyle = FontStyle.Italic
            NewCaption.Text = uText
            NewCaption.BoxSize.Location = e.Location + New Point(nHorizontalPadding, nVerticalPadding) 'Offset by 3

            'Reset caption collection and add new play.
            Captions.Clear()
            Captions.Add(NewCaption)

            lastPoint = e.Location
            boolStartNewPlay = False
            boolResumePath = False

            'Update GamePath with new play.
            PathCount += 1
            ReDim Preserve GamePath(PathCount)
            With GamePath(PathCount)
                .PlotMode = gMode.dlgPlotting
                .OutcomeCount = 0
                .Region = modRegionFunctions.GetRegionFromPoints(UserPrefs.Sport, e.Location, nSpatialCorrection, PitchOffset.X, PitchOffset.Y)
                .Status = PathStatus.psStart
                .TeamName = uText
                .GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                If bVideoCaptureCurrent Then    'Apply video time to second time.  If no video then keep primary game time.
                    .VideoTC = frmVideoCapture.VideoTC
                Else
                    .VideoTC = .GameTC
                End If

                .TimeCriteria = szCurrentTimeCriteria
                .VideoFile = szCurrentVideoFile
                .VideoFile2 = szCurrentVideoFile
                .OutcomesExist = False
                .Coordinates = e.Location
                .SpatialCorrection = nSpatialCorrection
                .TagColor = uBackColor
                .TagFontColor = uForeColor
                If szCurrentVideoFile Is Nothing Then
                    .VideoFile = "None"
                    .VideoFile2 = "None"
                End If
            End With
            frmE(lastEventFormUsed).AddRow2Grid(PathCount, uBackColor.ToKnownColor)


        Else
            If boolResumePath Then
                'Last entry was a pass, so default is to resume carry mode.

                'Set end of previous path to no anchor
                Dim cTemp As Color = Color.FromName(NewPlay.Pen.Color.Name)
                NewPlay.Pen.EndCap = Drawing2D.LineCap.NoAnchor

                NewPlay = New GamePlay.Instance
                NewPlay.Pen = New Pen(cTemp, 3)
                NewPlay.Pen.StartCap = Drawing2D.LineCap.NoAnchor
                NewPlay.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                NewPlay.Pen.DashStyle = Drawing2D.DashStyle.Solid
                NewPlay.Path = New Drawing2D.GraphicsPath

                Index_Play += 1
                Plays.Add(NewPlay, Index_Play)

                boolResumePath = False
            End If

            'UpdateGathpath with new ball movement
            PathCount += 1
            ReDim Preserve GamePath(PathCount)
            With GamePath(PathCount)
                .PlotMode = gMode.dlgPlotting
                .OutcomeCount = 0
                '.Region = modRegionFunctions.GetRegionFromPoints(UserPrefs.Sport, e.Location, nSpatialCorrection, xMargin, yMargin)
                .Region = modRegionFunctions.GetRegionFromPoints(UserPrefs.Sport, e.Location, nSpatialCorrection, PitchOffset.X, PitchOffset.Y)
                .Status = PathStatus.psCarry
                .TeamName = uText
                .GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                If bVideoCaptureCurrent Then    'Apply video time to second time.  If no video then keep primary game time.
                    .VideoTC = frmVideoCapture.VideoTC
                Else
                    .VideoTC = .GameTC
                End If

                .TimeCriteria = szCurrentTimeCriteria
                .VideoFile = szCurrentVideoFile
                .VideoFile2 = szCurrentVideoFile
                .Coordinates = e.Location
                .SpatialCorrection = nSpatialCorrection

                .TagColor = uBackColor
                .TagFontColor = uForeColor
                If szCurrentVideoFile Is Nothing Then
                    .VideoFile = "None"
                    .VideoFile2 = "None"
                End If
            End With
            frmE(lastEventFormUsed).AddRow2Grid(PathCount)

            NewPlay.Path.AddLine(lastPoint, e.Location)
        End If

        lastPoint = e.Location

        Me.Refresh()

        If UserPrefs.SingleClickForPass Then Me.picPitch_MouseDoubleClick(sender, e)


    End Sub

    Public Sub picPitch_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseDoubleClick

        If Not bGameIsActive Then Exit Sub

        On Error GoTo errCatch

        If NewPlay.Path Is Nothing Then Exit Sub

        If boolStartNewPlay Then
            NewCaption.BoxSize.Location = e.Location + New Point(3, 3) 'Offset by 3
            boolStartNewPlay = False
        Else
            'Recall last points
            Dim nTemp As Integer = NewPlay.Path.PathPoints.Length - 1
            lastPoint = New Point(NewPlay.Path.PathPoints(nTemp - 1).X, NewPlay.Path.PathPoints(nTemp - 1).Y)

            Dim temp(nTemp) As PointF
            NewPlay.Path.PathPoints.CopyTo(temp, 0)
            Array.Resize(temp, nTemp)

            Dim temp2(nTemp) As Byte
            NewPlay.Path.PathTypes.CopyTo(temp2, 0)
            Array.Resize(temp2, nTemp)
            NewPlay.Path = New Drawing2D.GraphicsPath(temp, temp2)
            NewPlay.Pen.EndCap = Drawing2D.LineCap.NoAnchor

            'Remove old path and add modified version.
            Plays.Remove(Index_Play)
            Plays.Add(NewPlay, Index_Play)

            'Set end of previous path to no anchor
            Dim cTemp As Color = Color.FromName(NewPlay.Pen.Color.Name)

            'Now create a new draw path
            NewPlay = New GamePlay.Instance
            NewPlay.Pen = New Pen(cTemp, 3)
            NewPlay.Pen.StartCap = Drawing2D.LineCap.NoAnchor
            NewPlay.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
            NewPlay.Pen.DashStyle = Drawing2D.DashStyle.Dot
            NewPlay.Path = New Drawing2D.GraphicsPath

            NewPlay.Path.AddLine(lastPoint, e.Location)
            Index_Play += 1
            Plays.Add(NewPlay, Index_Play)

            'Reset last path to a carry
            GamePath(PathCount).Status = PathStatus.psPass
            frmE(lastEventFormUsed).SwithCarry2Pass()

        End If

errCatch:
        Err.Clear()

        lastPoint = e.Location
        boolResumePath = True

        Me.Refresh()


    End Sub

    Private Sub picPitch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseDown

        If Calibrate Then
            frmWireless.AddCalibrationPoint(e.Location)
            'Add icon
            Dim pic As New PictureBox
            pic.Location = e.Location
            pic.Image = My.Resources.AddPoint
            pic.BackColor = Color.Transparent
            pic.SizeMode = PictureBoxSizeMode.AutoSize
            picPitch.Controls.Add(pic)
        End If

        If modSonyCamera.SonyCameraIsConnected And Not Me.Calibrate Then
            Select Case UserPrefs.Sport
                Case tSports.sHockey
                    zX = (picPitch.Width / 1.25) / 90
                    zY = (picPitch.Height / 1.25) / 150

                Case tSports.sNetball
                    zX = (picPitch.Width / 1.25) / 90
                    zY = (picPitch.Height / 1.25) / 180

                Case tSports.sRugbyLeague
                    zX = (picPitch.Width / 1.25) / 68
                    zY = (picPitch.Height / 1.25) / 122

                Case tSports.sRugby7
                    zX = (picPitch.Width / 1.25) / 70
                    zY = (picPitch.Height / 1.25) / 120

                Case tSports.sBasketball
                    zX = (picPitch.Width / 1.25) / 50
                    zY = (picPitch.Height / 1.25) / 94

                Case tSports.sSoccer
                    zX = (picPitch.Width / 1.25) / 95
                    zY = (picPitch.Height / 1.25) / 150
            End Select

            Dim whereAmI As PointF = e.Location
            whereAmI.X /= zX
            whereAmI.Y /= zY

            Rect = New RectangleF((picPitch.Width / 10) / zX, (picPitch.Height / 10) / zY, (picPitch.Width / 1.25) / zX, (picPitch.Height / 1.25) / zY)

            NewPreset.pLocation.X = InterpolatePitchCoordinates(whereAmI, Rect, CameraPositions(3).pLocation.X, _
                CameraPositions(1).pLocation.X, _
                CameraPositions(4).pLocation.X, _
                CameraPositions(2).pLocation.X)

            NewPreset.pLocation.Y = InterpolatePitchCoordinates(whereAmI, Rect, CameraPositions(4).pLocation.Y, _
                CameraPositions(2).pLocation.Y, _
                CameraPositions(3).pLocation.Y, _
                CameraPositions(1).pLocation.Y)

            NewPreset.pZoom = Hex$(InterpolatePitchCoordinates(whereAmI, Rect, CameraPositions(3).nZoom, _
                CameraPositions(1).nZoom, _
                CameraPositions(4).nZoom, _
                CameraPositions(2).nZoom)).ToString

            If Len(NewPreset.pZoom) = 3 Then NewPreset.pZoom = "0" & NewPreset.pZoom

            'NewPreset.pZoom = Nothing '(((y2 - e.Y) / (y2 - y1)) * zPosX1) + (((e.Y - y1) / (y2 - y1)) * zPosX2)
            If Not Me.BackgroundWorker1.IsBusy Then Me.BackgroundWorker1.RunWorkerAsync()

        End If

    End Sub

    Private Sub picPitch_Paint1(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPitch.Paint
        'Set window size parameters
        Rect = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)

        DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality, _
            UserPrefs.FieldBackground.ToKnownColor, UserPrefs.FieldHighlights.ToKnownColor, _
            UserPrefs.FieldLines.ToKnownColor, UserPrefs.FieldPerimeter.ToKnownColor)

        LastPoints.X = 0
        LastPoints.Y = 0
        idForm = idForm + 1

        Select Case UserPrefs.Sport
            Case tSports.sHockey
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 150

            Case tSports.sNetball
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 180

            Case tSports.sRugbyLeague
                zX = (picPitch.Width / 1.25) / 68
                zY = (picPitch.Height / 1.25) / 122

            Case tSports.sRugby7
                zX = (picPitch.Width / 1.25) / 70
                zY = (picPitch.Height / 1.25) / 120

            Case tSports.sBasketball
                zX = (picPitch.Width / 1.25) / 50
                zY = (picPitch.Height / 1.25) / 94

            Case tSports.sSoccer
                zX = (picPitch.Width / 1.25) / 95
                zY = (picPitch.Height / 1.25) / 150
        End Select

        nSpatialCorrection.X = zX
        nSpatialCorrection.Y = zY

        'Draw Plays
        For Each gp As GamePlay.Instance In Plays
            Try
                If gp.Pen.DashStyle = Drawing2D.DashStyle.Solid Then
                    'Ball carry
                    e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints)
                Else
                    'Ball pass
                    e.Graphics.DrawLines(gp.Pen, gp.Path.PathPoints)
                End If
            Catch ex As Exception

            End Try
        Next

        For Each gc As GamePlay.CaptionBox In Captions
            Try
                gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize
                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, gc.BackColor)), gc.BoxSize)
                e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)

                e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
                New SolidBrush(gc.ForeColor), gc.BoxSize.Location)


            Catch ex As Exception

            End Try
        Next

    End Sub

    Private Sub frmPitch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Set window size parameters
        xMargin = picPitch.Width / 20   '5% left hand margin
        yMargin = picPitch.Height / 20   '5% top margin
        xWidth = (picPitch.Width / 1.11)
        yHeight = (picPitch.Height / 1.11)

        Me.picPitch.Refresh()

    End Sub


    Private Sub picPitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPitch.Click

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        modSonyCamera.GotoPreset(NewPreset)
    End Sub

End Class