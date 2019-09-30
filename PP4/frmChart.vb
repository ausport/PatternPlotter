Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class frmChart
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim Rect As RectangleF = Nothing

    Public idForm As Integer
    Public Plays As New Microsoft.VisualBasic.Collection

    Public HeatArray(,) As Single = Nothing

    Public kMean() As Integer

    Public ScatterPlotPoints As New Microsoft.VisualBasic.Collection

    Public ChartLabel As String

    Public ClusterArray(,) As Integer = Nothing
    Public ClusterColorArray(,) As Color = Nothing
    Shared MyClusterInfo(,) As ClusterInfo = Nothing

    Public MySearch As Integer  'Retains the search criteria id.

    Private SelectedPathStart As PointF = Nothing
    Public SelectedCaptionBox As Rectangle = Nothing
    Private SelectedScatter As ScatterInfo = Nothing
    Public SelectedCaptionPathID As Long = 0
    Public MouseDownOnCaption As Boolean = False
    Private MouseMoveOffset As Point = Nothing

    Private DoRefresh As Boolean = False
    Public Captions As New Microsoft.VisualBasic.Collection
    Public ShowCaptions As Boolean = False

    Private WithEvents pDoc As PrintDocument


    Public Structure CaptionInfo
        Dim CaptionRect As Rectangle
        Dim ID As Long
        Dim PlayNumber As Integer
        Dim Text As String
    End Structure
    Public CurrentCaptionInfo() As CaptionInfo
    Public CurrentCaptionIndex As Integer = Nothing

    Shared CurrentPathInfo As PathInfo = Nothing            'Holds the DB PlayNumber of play segment currently under the mouse pointer.  If none, then = nothing.
    Public CurrentClusterLocation As RectangleF = Nothing   'Holds the spatial coordinates of the items currently under the mouse button.  If none, then = nothing.
    Public CurrentMousePointer As PointF = Nothing

    Public Enum Chart
        ctPathways = 0
        ctClusters = 1
        ctDataGraph = 2
        ctPlayerMaps = 3
        ctScatterPlots = 4
        ctEventHeatMaps = 5
        ctBallSpeedHeatMaps = 6
        ctPossessionTimeHeatMaps = 7
    End Enum

    Private mvarChartType As Chart

    Public Property ChartType() As Chart
        Get
            Return mvarChartType
        End Get
        Set(ByVal value As Chart)
            mvarChartType = value
            Select Case mvarChartType
                Case Is = Chart.ctClusters
                    Me.lblChartType.Text = "Clusters: "
                Case Chart.ctPathways
                    Me.lblChartType.Text = "Pathways: "
                Case Chart.ctDataGraph
                    Me.lblChartType.Text = "Data Graphs: "
                Case Chart.ctPlayerMaps
                    Me.lblChartType.Text = "Player Possession Maps: "
                Case Chart.ctEventHeatMaps
                    Me.lblChartType.Text = "Event Frequency Heat Maps: "
                Case Chart.ctBallSpeedHeatMaps
                    Me.lblChartType.Text = "Ball Speed Heat Maps: "
                Case Chart.ctPossessionTimeHeatMaps
                    Me.lblChartType.Text = "Possession Time Heat Maps: "
                Case Chart.ctScatterPlots
                    Me.lblChartType.Text = "Scatter Plots: "
            End Select
            Me.Text = Me.lblChartType.Text
        End Set
    End Property

    Public Sub SetLabels(ByVal ThisSearch As Integer, Optional ByVal FindDescriptors As Boolean = True)
        'NB: FindDescriptors is used here to avoid implementing a search for checked items in the descriptor list
        'where the analysis window is not open.  If this occurs, it resets the SearchHistory array which results
        'in errors during the wireless automated reporting function, for which the analysis window is not open.
        'So, FindDescriptors will only be false when the SetLabels function is called during the automated search routine.

        If ThisSearch = 0 Then
            Me.lblGameIDs.Text = "No information available"
            Me.lblTeamNames.Text = "No information available"
            Me.lblTimeCriterion.Text = "No information available"
            Me.lblOutcomes.Text = "No information available"
            Me.lblDescriptors.Text = "No information available"
            Exit Sub
        End If


        If Not SearchHistory(ThisSearch).szGameID Is Nothing Then
            With SearchHistory(ThisSearch)
                'Set Game IDs
                Me.lblGameIDs.Text = ""
                For Each item As String In .szGameID
                    If Not Me.lblGameIDs.Text = "" Then Me.lblGameIDs.Text = Me.lblGameIDs.Text & ", "
                    Me.lblGameIDs.Text = Me.lblGameIDs.Text & item
                Next

                'Set Game IDs
                Me.lblTeamNames.Text = ""
                If .szTeamName Is Nothing Then
                    Me.lblTeamNames.Text = "All Teams "
                Else
                    For Each item As String In .szTeamName
                        If Not Me.lblTeamNames.Text = "" Then Me.lblTeamNames.Text = Me.lblTeamNames.Text & ", "
                        Me.lblTeamNames.Text = Me.lblTeamNames.Text & item
                        If item = "*" Then Me.lblTeamNames.Text = "All Teams "
                    Next

                End If

                'Set time criterion
                Me.lblTimeCriterion.Text = ""
                If .szTimeCriterion Is Nothing Then
                    Me.lblTimeCriterion.Text = "All Time Periods "
                Else
                    For Each item As String In .szTimeCriterion
                        If Not Me.lblTimeCriterion.Text = "" Then Me.lblTimeCriterion.Text = Me.lblTimeCriterion.Text & ", "
                        Me.lblTimeCriterion.Text = Me.lblTimeCriterion.Text & item
                        If item = "*" Then Me.lblTimeCriterion.Text = "All Time Periods "
                    Next
                End If

                'Set outcomes
                Select Case .uOutcomes(0)
                    Case OutcomeType.outAll
                        Me.lblOutcomes.Text = "All Outcomes "
                    Case OutcomeType.outDescriptor
                        Me.lblOutcomes.Text = "Descriptors Only "
                    Case OutcomeType.outPositive
                        Me.lblOutcomes.Text = "Positive Outcomes "
                    Case OutcomeType.outNegative
                        Me.lblOutcomes.Text = "Negative Outcomes "
                    Case Else
                        Me.lblOutcomes.Text = "No Selection "
                End Select

                'Set descriptors
                Me.lblDescriptors.Text = ""
                If .szDescriptors Is Nothing Then
                    Me.lblDescriptors.Text = ""
                    Dim items() As String = GetCheckedDescriptorList(True)
                    For Each item As String In items
                        If Not Me.lblDescriptors.Text = "" Then Me.lblDescriptors.Text = Me.lblDescriptors.Text & ", "
                        Me.lblDescriptors.Text = Me.lblDescriptors.Text & item
                    Next
                Else

                    For Each item As String In .szDescriptors
                        If Not Me.lblDescriptors.Text = "" Then Me.lblDescriptors.Text = Me.lblDescriptors.Text & ", "
                        Me.lblDescriptors.Text = Me.lblDescriptors.Text & item
                    Next

                End If

            End With
        End If

        Me.Text = Me.Text & Me.lblTeamNames.Text & " "
        Me.Text = Me.Text & Mid(Me.lblDescriptors.Text, 1, 40) '& "..."
        ChartLabel = Me.Text
        Me.Text = Me.Text & "..."
    End Sub

    Public Sub SetClusterInfo()
        If Not CurrentClusterInfo Is Nothing Then
            MyClusterInfo = CurrentClusterInfo
            CurrentClusterInfo = Nothing
        End If
    End Sub

    Private Sub frmChart_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting(AppName, "Settings", "ChartWidth", Me.Size.Width.ToString)
        SaveSetting(AppName, "Settings", "ChartHeight", Me.Size.Height.ToString)

    End Sub

    Private Sub frmChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ClearPlays()
        Plays.Clear()
    End Sub

    Public Function AddPlays(ByVal cPlaySet As Microsoft.VisualBasic.Collection) As Integer
        Try
            If Not cPlaySet Is Nothing Then
                Plays = cPlaySet
                Return Plays.Count
            End If
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function

    Public Function AddCaptions(ByVal cCaptionSet As Microsoft.VisualBasic.Collection) As Integer
        Try
            If Not cCaptionSet Is Nothing Then
                Captions = cCaptionSet

                Dim n As Integer = 1
                Erase CurrentCaptionInfo
                For Each NewCaption As GamePlay.CaptionBox In Captions
                    ReDim Preserve CurrentCaptionInfo(n)
                    CurrentCaptionInfo(n).ID = NewCaption.ID
                    CurrentCaptionInfo(n).PlayNumber = NewCaption.PlayNumber
                    CurrentCaptionInfo(n).Text = NewCaption.Text
                    n += 1
                Next

                Return Captions.Count
            End If
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function

    Private Sub picPitch_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseDown

        If ShowCaptions Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                SelectedCaptionPathID = 0
                'MouseDownOnCaption = False

                For Each ThisCaption As CaptionInfo In CurrentCaptionInfo
                    If ThisCaption.CaptionRect.Contains(e.Location) And ThisCaption.ID <> 0 Then
                        'Found matching caption
                        CurrentCaptionIndex = Array.IndexOf(CurrentCaptionInfo, ThisCaption)
                        MouseDownOnCaption = True
                        MouseMoveOffset.X = ThisCaption.CaptionRect.Left - e.X
                        MouseMoveOffset.Y = ThisCaption.CaptionRect.Top - e.Y
                        SelectedPathStart = ThisCaption.CaptionRect.Location
                        SelectedCaptionBox.X = e.X + MouseMoveOffset.X
                        SelectedCaptionBox.Y = e.Y + MouseMoveOffset.Y
                        Me.Text = ThisCaption.ID.ToString
                        Exit For
                    End If
                Next

                Me.Refresh()
                Exit Sub
            End If
        End If

    End Sub

    Private Sub picPitch_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseUp
        If ShowCaptions And CurrentCaptionIndex > 0 And MouseDownOnCaption Then

            MouseDownOnCaption = False
            picPitch.Refresh()

            'Now check new location of caption to assign to database ID
            Dim OriginalPathStartPoints As PointF = Nothing

            For Each Play As GamePlay.Instance In Plays
                If OriginalPathStartPoints = Nothing Then OriginalPathStartPoints = Play.Path.PathPoints(0)
                If Play.Path.PathPoints(0) = SelectedPathStart Then
                    Me.Text = Play.ID
                    Dim modCaption As GamePlay.CaptionBox = Captions.Item(CurrentCaptionIndex)
                    'Change X, Y properties for current caption in collection.
                    modCaption.BoxSize.Location = ChangeEventNameID(CurrentCaptionInfo(CurrentCaptionIndex).Text, CurrentCaptionInfo(CurrentCaptionIndex).ID, Play.ID)

                    If Not modCaption.BoxSize.Location = Nothing Then
                        Captions.Remove(CurrentCaptionIndex)
                        Captions.Add(modCaption, CurrentCaptionIndex)

                        'Refresh CurrentCaptionInfo array
                        Dim Index_Caption As Integer = 1
                        Dim CaptionSet As New Microsoft.VisualBasic.Collection

                        For Each NewCaption As GamePlay.CaptionBox In Captions
                            If Not NewCaption.Index = 0 Then
                                NewCaption.Index = Index_Caption
                                CaptionSet.Add(NewCaption, Index_Caption)
                                Index_Caption += 1
                            End If
                        Next

                        'Add a final caption that includes the play details.
                        If Not CurrentPathInfo.ID Is Nothing Then
                            Dim PlayInfoCaption As New GamePlay.CaptionBox

                            PlayInfoCaption.Text = CurrentPathInfo.GameID & vbNewLine & CurrentPathInfo.TeamName & vbNewLine & _
                                CurrentPathInfo.TimeCriteria & vbNewLine & CurrentPathInfo.StartTimeString & " --> " & CurrentPathInfo.StopTimeString
                            PlayInfoCaption.BoxSize.X = OriginalPathStartPoints.X
                            PlayInfoCaption.BoxSize.Y = OriginalPathStartPoints.Y
                            CaptionSet.Add(PlayInfoCaption, "b")
                        End If


                        AddCaptions(CaptionSet)

                    End If

                    Me.Refresh()
                    Exit For
                End If

            Next
            SelectedCaptionBox = Nothing
            MouseDownOnCaption = False
        End If


    End Sub

    Private Sub picPitch_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseMove
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

        If Me.ChartType = Chart.ctPathways Then
            'Handle moving caption first.
            If MouseDownOnCaption Then
                SelectedCaptionBox.X = e.X + MouseMoveOffset.X
                SelectedCaptionBox.Y = e.Y + MouseMoveOffset.Y
            Else
                SelectedCaptionBox = Nothing
            End If

            'Get start point and match
            Dim n As Integer = -1
            For Each gp As GamePlay.Instance In Plays
                If gp.Path.PointCount > 0 Then
                    n += 1

                    If Math.Max(gp.Path.PathPoints(0).X, e.X / zX) - Math.Min(gp.Path.PathPoints(0).X, e.X / zX) < 1.5 And _
                        Math.Max(gp.Path.PathPoints(0).Y, e.Y / zY) - Math.Min(gp.Path.PathPoints(0).Y, e.Y / zY) < 1.5 Then

                        If SelectedPathStart <> gp.Path.PathPoints(0) Then
                            'Match found..
                            'Identify the playnumber relating to the graphic...
                            CurrentPathInfo = GetPathInfo(gp.ID)
                            CurrentPathInfo.MousePointer = e.Location

                            Dim NewCaption As New GamePlay.CaptionBox
                            NewCaption.BackColor = BackColor
                            NewCaption.ForeColor = ForeColor
                            NewCaption.BoxSize.X = e.X
                            NewCaption.BoxSize.Y = e.Y

                            NewCaption.Text = CurrentPathInfo.GameID & vbNewLine & CurrentPathInfo.TeamName & vbNewLine & _
                                CurrentPathInfo.TimeCriteria & vbNewLine & CurrentPathInfo.StartTimeString & " --> " & CurrentPathInfo.StopTimeString

                            Captions.Add(NewCaption)

                            SelectedPathStart = gp.Path.PathPoints(0)

                            Me.Refresh()
                            DoRefresh = True
                        End If
                        'Exit here if a path was found under the pointer, otherwise, continue to next code which clears selections then re-paints.
                        Exit Sub

                    End If
                End If
            Next

        ElseIf Me.ChartType = Chart.ctScatterPlots And Not Me.ScatterPlotPoints Is Nothing Then

            'Handle moving caption first.
            If MouseDownOnCaption Then
                SelectedCaptionBox.X = e.X + MouseMoveOffset.X
                SelectedCaptionBox.Y = e.Y + MouseMoveOffset.Y
            Else
                SelectedCaptionBox = Nothing
            End If

            'Get start point and match
            Dim n As Integer = -1
            For Each dp As ScatterInfo In Me.ScatterPlotPoints
                n += 1

                If Math.Max(dp.Location.X, e.X / zX) - Math.Min(dp.Location.X, e.X / zX) < 1.5 And _
                    Math.Max(dp.Location.Y, e.Y / zY) - Math.Min(dp.Location.Y, e.Y / zY) < 1.5 Then

                    If SelectedPathStart <> dp.Location Then
                        'Match found..
                        'Identify the playnumber relating to the graphic...
                        'CurrentPathInfo = GetPathInfo(gp.ID)
                        'CurrentPathInfo.MousePointer = e.Location

                        Dim NewCaption As New GamePlay.CaptionBox
                        NewCaption.BackColor = BackColor
                        NewCaption.ForeColor = ForeColor
                        NewCaption.BoxSize.X = e.X
                        NewCaption.BoxSize.Y = e.Y

                        NewCaption.Text = dp.GameID & vbNewLine & dp.TeamName & vbNewLine & _
                            dp.TimeCriteria & vbNewLine & dp.StartTimeString & " --> " & dp.StopTimeString

                        Captions.Add(NewCaption)

                        SelectedPathStart = dp.Location
                        SelectedScatter = dp

                        Me.Refresh()
                        DoRefresh = True
                    End If
                    'Exit here if a path was found under the pointer, otherwise, continue to next code which clears selections then re-paints.
                    Exit Sub

                End If
            Next

        End If

        If Not SelectedPathStart = Nothing And ShowCaptions = False Then
            SelectedPathStart = Nothing
            CurrentPathInfo = Nothing
            SelectedScatter = Nothing
            Captions.Clear()
        End If
        Me.Refresh()

    End Sub

    Public Sub picPitch_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseClick

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

        CurrentMousePointer.X = e.X / zX
        CurrentMousePointer.Y = e.Y / zY


        If Me.ChartType = Chart.ctPathways Then

            'Find and play video clip from path that starts here...

            'Get start point and match
            For Each gp As GamePlay.Instance In Plays
                If gp.Path.PointCount > 0 Then

                    If Math.Max(gp.Path.PathPoints(0).X, e.X / zX) - Math.Min(gp.Path.PathPoints(0).X, e.X / zX) < 1.5 And _
                        Math.Max(gp.Path.PathPoints(0).Y, e.Y / zY) - Math.Min(gp.Path.PathPoints(0).Y, e.Y / zY) < 1.5 Then

                        'Match found..
                        If e.Button = Windows.Forms.MouseButtons.Right Then
                            Me.mnuPathwayMapDropDown.Show(Me, e.Location)
                            Exit Sub

                        End If

                        Dim szVideos() As String = GetVideoFiles(gp.ID)
                        If Not szVideos Is Nothing Then
                            If Not szVideos(0) = "None" Then
                                PlayVideo(szVideos(0), gp.VideoStartTime - UserPrefs.LeadTime, gp.VideoStartTime + UserPrefs.LagTime)
                            Else
                                MsgBox("There is no video attached to this datapoint.  Re-Link the game data to a new video file.", MsgBoxStyle.Information, Application.ProductName)
                            End If
                        End If

                    End If
                End If
            Next

        ElseIf Me.ChartType = Chart.ctScatterPlots Then

            'Find and play video clip from scatter plot item

            'Get start point and match
            For Each dp As ScatterInfo In Me.ScatterPlotPoints
                If Math.Max(dp.Location.X, e.X / zX) - Math.Min(dp.Location.X, e.X / zX) < 1.5 And _
                        Math.Max(dp.Location.Y, e.Y / zY) - Math.Min(dp.Location.Y, e.Y / zY) < 1.5 Then

                    SelectedScatter = dp

                    'Match found..
                    If e.Button = Windows.Forms.MouseButtons.Right Then
                        Me.mnuScatterPlotDropDown.Show(Me, e.Location)
                        Exit Sub
                    End If

                    Dim szVideos() As String = GetVideoFiles(dp.ID)
                    If Not szVideos Is Nothing Then
                        If Not szVideos(0) = "None" Then
                            PlayVideo(szVideos(0), GetSecondsFromTimeString(dp.StartTimeString) - UserPrefs.LeadTime, GetSecondsFromTimeString(dp.StopTimeString) + UserPrefs.LagTime)
                        Else
                            MsgBox("There is no video attached to this datapoint.  Re-Link the game data to a new video file.", MsgBoxStyle.Information, Application.ProductName)
                        End If
                    End If

                End If
            Next

        ElseIf Me.ChartType = Chart.ctClusters Then

            'Find quadrant matching mouseclick
            Dim xMax, xMin, yMax, yMin As New Single    'Use these to find the y/x limits above and below click point to search with.
            Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ  'width of each quadrant block.
            Dim h As Single = Rect.Height / UserPrefs.clVerticalQ   'height of each quadrant block.
            xMin = 0
            yMin = 0
            For hLine As Integer = 0 To UserPrefs.clHorizontalQ
                If ((hLine * w) + Rect.Left) > e.X Then
                    xMax = ((hLine * w) + Rect.Left) / zX
                    Exit For
                Else
                    xMin = ((hLine * w) + Rect.Left) / zX
                End If
            Next
            For hLine As Integer = 0 To UserPrefs.clVerticalQ
                If ((hLine * h) + Rect.Top) > e.Y Then
                    yMax = ((hLine * h) + Rect.Top) / zY
                    Exit For
                Else
                    yMin = ((hLine * h) + Rect.Top) / zY
                End If
            Next

            CurrentClusterLocation.X = xMin
            CurrentClusterLocation.Width = xMax - xMin
            CurrentClusterLocation.Y = yMin
            CurrentClusterLocation.Height = yMax - yMin

            If e.Button = Windows.Forms.MouseButtons.Right Then
                Me.mnuClusterDropDown.Show(Me, e.Location)
                Exit Sub
            Else
                Me.mnuClusterAddVPL_Click(sender, Nothing)
            End If
        End If


    End Sub

    Private Sub picPitch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPitch.Paint
        'Set window size parameters
        Rect = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)

        'Draw Base Pitch
        DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality)

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

        'Draw Plays
        Select Case mvarChartType
            Case Chart.ctEventHeatMaps, Chart.ctBallSpeedHeatMaps, Chart.ctPossessionTimeHeatMaps
                Me.grpBoxOptions.Visible = False
                'Dim maxCount As MaxMinValues = GetMaxClusterArrayValues2(HeatArray)

                If Not HeatArray Is Nothing Then
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    For x As Integer = 0 To HeatArray.GetUpperBound(0) - 1
                        For y As Integer = 0 To HeatArray.GetUpperBound(1) - 1
                            Dim ncolor As Color = ClusterColorArray(x, y)
                            e.Graphics.FillRegion(New SolidBrush(ncolor), New Region(New RectangleF(x + PitchOffset.X, y + PitchOffset.Y, 1, 1)))
                        Next
                    Next
                End If

            Case Chart.ctScatterPlots
                Me.grpBoxOptions.Visible = False

                If Not Me.ScatterPlotPoints Is Nothing Then

                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
                        'Cluster data
                        e.Graphics.FillEllipse(New SolidBrush(ScatterPoint.TeamColor), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(2, 2)))
                    Next
                End If


            Case Chart.ctPlayerMaps
                Me.grpBoxOptions.Visible = True
                '*
                '* Draw player maps
                '*
                If Plays.Count > 0 Then
                    'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    Dim o As Integer = Me.numPathOpacity.Value
                    Dim k As Integer = 0
                    Dim nColor As Color
                    For Each gp As GamePlay.Instance In Plays
                        k += 1
                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
                        gp.Pen.Brush = New SolidBrush(Color.FromArgb(20, nColor))
                        gp.Pen.Width = UserPrefs.pmLineWidth / zX

                        If gp.Lead Then
                            If Not Me.chkShowReceives.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                            End If

                            gp.Pen.DashStyle = Drawing2D.DashStyle.Dot
                            gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                            gp.Pen.Width *= 1.5
                            If Not Me.chkShowPossession.Checked Then gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                        ElseIf gp.Posession Then
                            If Not Me.chkShowReceives.Checked Then gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                            If Not Me.chkShowPossession.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(nColor)
                            End If
                            gp.Pen.DashStyle = Drawing2D.DashStyle.Solid
                            gp.Pen.Width *= 1.5
                            gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                        ElseIf gp.Lag Then
                            If Not Me.chkShowDeliveries.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                            End If
                            gp.Pen.DashStyle = Drawing2D.DashStyle.DashDotDot
                            gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                            gp.Pen.Width *= 1.5
                        Else
                            If Not Me.chShowOtherPlay.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                                gp.Pen.Width *= 0.5
                            End If
                        End If

                        If gp.Path.PointCount > 0 Then
                            'If not in show captions mode then highlight play under pointer.
                            If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
                            'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
                            If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

                            e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                        End If
                    Next
                End If
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

            Case Chart.ctPathways
                '*
                '* Draw pathways
                '*
                Me.grpBoxOptions.Visible = False

                If Plays.Count > 0 Then
                    e.Graphics.ScaleTransform(zX, zY)
                    Dim k As Integer = 0
                    Dim nColor As Color
                    For Each gp As GamePlay.Instance In Plays
                        k += 1
                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
                        gp.Pen.Brush = New SolidBrush(nColor)
                        gp.Pen.Width = UserPrefs.pmLineWidth / zX

                        If gp.Path.PointCount > 0 Then
                            'If not in show captions mode then highlight play under pointer.
                            If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
                            'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
                            If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

                            e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                        End If
                    Next
                End If



            Case Chart.ctClusters
                '*
                '* Draw quadrants
                '*
                If Me.ClusterArray.GetUpperBound(0) = UserPrefs.clHorizontalQ - 1 And Me.ClusterArray.GetUpperBound(1) = UserPrefs.clVerticalQ - 1 Then


                    Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
                    Dim h As Single = Rect.Height / UserPrefs.clVerticalQ
                    Dim nPen As New Pen(Color.Black, 1)
                    Dim nFont As New Font(Me.Font, FontStyle.Bold)
                    Me.grpBoxOptions.Visible = False

                    'Draw colored squares
                    If Not ClusterArray Is Nothing Then
                        For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                            For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                If Me.ClusterArray(x, y) > 0 Then
                                    Dim x1 As Single = (x * w) + Rect.Left
                                    Dim y1 As Single = (y * h) + Rect.Top
                                    e.Graphics.FillRectangle(New SolidBrush(ClusterColorArray(x, y)), x1, y1, w, h)
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
                    If Not ClusterArray Is Nothing Then
                        For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                            For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                Dim ptxt As PointF = e.Graphics.MeasureString(ClusterArray(x, y), Me.Font)
                                Dim p As PointF = New PointF(Rect.Left + (w * x) + (w / 2) - (ptxt.X / 2), Rect.Top + (h * y) + (h / 2) - (ptxt.Y / 2))
                                If Me.ClusterArray(x, y) > 0 Then
                                    e.Graphics.DrawString(Me.ClusterArray(x, y).ToString, nFont, Brushes.Black, p)
                                End If
                            Next
                        Next
                    End If
                    'Finally, overlay pitch lines again.
                    RedrawOutline(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality)
                End If

        End Select

        If Captions.Count > 0 Then
            e.Graphics.ResetTransform()

            If Me.ShowCaptions Then
                'Showing a single gameplay path, including the event captions.
                Dim lastCaption As New GamePlay.CaptionBox
                Dim n As Single = 0

                For Each gc As GamePlay.CaptionBox In Captions
                    'Scale captions
                    gc.BoxSize.X *= zX
                    gc.BoxSize.Y *= zY
                    gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize

                    If gc.BoxSize.Location = lastCaption.BoxSize.Location Then
                        'Modify location of caption for viewing ease.
                        gc.BoxSize.Offset(0, lastCaption.BoxSize.Height + zY)
                        lastCaption.BoxSize.Height = gc.BoxSize.Bottom - lastCaption.BoxSize.Top
                    Else
                        lastCaption = gc
                    End If

                    If gc.Index = CurrentCaptionIndex Then
                        'This caption is selected.
                        If MouseDownOnCaption Then gc.BoxSize.Location = SelectedCaptionBox.Location
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightSalmon)), gc.BoxSize)
                        SelectedCaptionPathID = gc.ID
                    Else
                        'Not selected
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightGreen)), gc.BoxSize)
                    End If

                    e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)
                    e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
                    New SolidBrush(Color.Black), gc.BoxSize.Location)

                    'Save current caption locations and sizes for mouse-driven movement.
                    If Not CurrentCaptionInfo Is Nothing Then
                        CurrentCaptionInfo(gc.Index).CaptionRect = gc.BoxSize
                    End If

                Next

            Else
                'Showing an information box with details about the play.
                For Each gc As GamePlay.CaptionBox In Captions
                    Try
                        gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize
                        gc.BoxSize.X = gc.BoxSize.Left - gc.BoxSize.Width
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, gc.BackColor)), gc.BoxSize)
                        e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)

                        e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
                        New SolidBrush(gc.ForeColor), gc.BoxSize.Location)

                    Catch ex As Exception

                    End Try
                Next
            End If

        End If
    End Sub

    Public Sub ToArgbToStringExample1(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics

        ' Color structure used for temporary storage.
        Dim someColor As Color = Color.FromArgb(0)

        ' Array to store KnownColor values that match the criteria.
        Dim colorMatches(167) As KnownColor

        ' Number of matches found.
        Dim count As Integer = 0

        ' Iterate through KnownColor enums to find all corresponding colors
        ' that have a non-zero green component and zero-valued red
        ' component and that are not system colors.
        Dim enumValue As KnownColor
        For enumValue = 0 To KnownColor.YellowGreen
            someColor = Color.FromKnownColor(enumValue)
            If someColor.R <> 0 And _
            Not someColor.IsSystemColor Then
                colorMatches(count) = enumValue
                count += 1
            End If
        Next enumValue
        Dim myBrush1 As New SolidBrush(someColor)
        Dim myFont As New Font("Arial", 9)
        Dim x As Integer = 40
        Dim y As Integer = 40

        ' Iterate through the matches found and display each color that
        ' corresponds with the enum value in the array. Also display the
        ' name of the KnownColor and the ARGB components.
        Dim i As Integer
        For i = 0 To count - 1

            ' Display the color.
            someColor = Color.FromKnownColor(colorMatches(i))
            myBrush1.Color = someColor
            g.FillRectangle(myBrush1, x, y, 50, 30)

            ' Display KnownColor name and four component values. To display
            ' component values:  Use the ToArgb method to get the 32-bit
            ' ARGB value of someColor (created from a KnownColor). Create
            ' a Color structure from the 32-bit ARGB value and set someColor
            ' equal to this new Color structure. Then use the ToString method
            ' to convert it to a string.
            g.DrawString(someColor.ToString(), myFont, Brushes.Black, _
            x + 55, y)
            someColor = Color.FromArgb(someColor.ToArgb())
            g.DrawString(someColor.ToString(), myFont, Brushes.Black, _
            x + 55, y + 15)
            y += 40
        Next i
    End Sub

    Private Sub frmChart_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Set window size parameters
        'Rect.width = picPitch.Width / 20   '5% left hand margin
        'Rect.top = picPitch.Height / 20   '5% top margin
        'Rect.width = (picPitch.Width / 1.11)
        'rect.height = (picPitch.Height / 1.11)

        Me.picPitch.Refresh()
    End Sub

    Public Sub New(ByVal intID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        idForm = intID

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim how As DialogResult = MsgBox("Include chart metadata with image?", MsgBoxStyle.YesNoCancel, "Save Chart to JPEG")
        If how = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        'Save chart to JPEG

        Dim res As DialogResult = Windows.Forms.DialogResult.Cancel

        'Get a new filename if one does not already exist.
        Dim dlgFileName = New SaveFileDialog
        dlgFileName.Filter = "JPG Image|*.jpg"
        dlgFileName.InitialDirectory = UserPrefs.VideoCaptureDir
        dlgFileName.FileName = StripFileName(Me.ChartLabel) & ".jpg"
        res = dlgFileName.ShowDialog()
        If res <> Windows.Forms.DialogResult.Cancel Then
            Dim sc As New ScreenShot.ScreenCapture
            Application.DoEvents()
            Select Case how
                Case Is = Windows.Forms.DialogResult.Yes
                    sc.CaptureWindowToFile(Me.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
                Case Is = Windows.Forms.DialogResult.No
                    sc.CaptureWindowToFile(Me.picPitch.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
            End Select
        End If


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

    Public Sub SaveChartImage(ByVal szDestination As String)
        Dim sc As New ScreenShot.ScreenCapture
        sc.CaptureWindowToFile(Me.Handle, szDestination, Imaging.ImageFormat.Jpeg)
    End Sub

    Private Sub mnuAddItem2VPL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddItem2VPL.Click

        If Not CurrentPathInfo.GameID Is Nothing Then
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)
            SearchHistory(CurrentSearch).szDescriptors = Nothing
            SearchHistory(CurrentSearch).szSQL = "SELECT * " & _
            "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID " & _
            "WHERE PlayNumber = " & CurrentPathInfo.PlayNumber & " AND PathData.GameID = '" & CurrentPathInfo.GameID & "' AND TimeCriteria = '" & _
            CurrentPathInfo.TimeCriteria & "' ORDER BY ID"

            'Search for any existing playlists
            If countVPL > 0 Then
                If frmVPL(lastVPLFormUsed).Visible Then
                    Dim nAppend As Integer = Nothing
                    frmConfirmVPL.ShowDialog()
                    If frmConfirmVPL.DialogResult = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    Else
                        nAppend = frmConfirmVPL.AppendVPL
                    End If

                    If nAppend >= 0 Then
                        If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(nAppend).vplGrid, Nothing) > 0 Then frmVPL(nAppend).formDirty = True
                        frmVPL(nAppend).MdiParent = frmMain
                        frmVPL(nAppend).Show()
                    Else
                        GoTo RecoverIfNotVisible
                    End If

                Else
                    GoTo RecoverIfNotVisible
                End If
            Else

RecoverIfNotVisible:
                countVPL = countVPL + 1
                ReDim Preserve frmVPL(countVPL)
                frmVPL(countVPL) = New frmVideoPlayList(countVPL)
                If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid, Nothing) > 0 Then frmVPL(countVPL).formDirty = True
                frmVPL(countVPL).SetLabels(CurrentSearch)
                frmVPL(countVPL).MdiParent = frmMain
                frmVPL(countVPL).Show()
            End If


        End If


    End Sub

    Private Sub mnuPlayItemVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlayItemVideo.Click

        Dim szVideos() As String = GetVideoFiles(CurrentPathInfo.ID(0))
        If Not szVideos Is Nothing Then
            If Not szVideos(0) = "None" Then
                PlayVideo(szVideos(0), GetSecondsFromTimeString(CurrentPathInfo.StartTimeString) - UserPrefs.LeadTime, GetSecondsFromTimeString(CurrentPathInfo.StopTimeString) + UserPrefs.LagTime)
            Else
                MsgBox("There is no video attached to this datapoint.  Re-Link the game data to a new video file.", MsgBoxStyle.Information, Application.ProductName)
            End If
        End If
    End Sub

    Private Sub lblGameIDs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblGameIDs.DoubleClick
        'Edit details
        lblGameIDs.Text = EditBox("Edit GameID details", lblGameIDs.Text)

    End Sub

    Private Sub lblTeamNames_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTeamNames.DoubleClick
        'Edit details
        lblTeamNames.Text = EditBox("Edit Team Name details", lblTeamNames.Text)

    End Sub

    Private Sub lblOutcomes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOutcomes.DoubleClick
        'Edit details
        lblOutcomes.Text = EditBox("Edit Outcome details", lblOutcomes.Text)

    End Sub

    Private Sub lblTimeCriterion_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTimeCriterion.DoubleClick
        'Edit details
        lblTimeCriterion.Text = EditBox("Edit Time Criterion details", lblTimeCriterion.Text)

    End Sub

    Private Sub lblDescriptors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblDescriptors.DoubleClick
        'Edit details
        lblDescriptors.Text = EditBox("Edit Descriptor details", lblDescriptors.Text)

    End Sub

    Private Function EditBox(ByVal message As String, ByVal defaultValue As String, Optional ByVal title As String = "Edit Details") As String

        Dim myValue As String
        ' Display message, title, and default value.
        myValue = InputBox(message, title, defaultValue)
        ' If user has clicked Cancel, set myValue to defaultValue
        If myValue.Length = 0 Then myValue = defaultValue

        Return myValue

    End Function

    Public Sub mnuClusterAddVPL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClusterAddVPL.Click

        CurrentSearch += 1
        ReDim Preserve SearchHistory(CurrentSearch)

        SearchHistory(CurrentSearch) = SearchHistory(MySearch)
        With Me.CurrentClusterLocation
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(MySearch), AnalysisType.uVideoPlaylist, Nothing, _
                .Left, .Right, .Top, .Bottom)
        End With


        'Search for any existing playlists
        If countVPL > 0 Then
            If frmVPL(lastVPLFormUsed).Visible Then
                Dim nAppend As Integer = Nothing
                frmConfirmVPL.ShowDialog()
                If frmConfirmVPL.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                Else
                    nAppend = frmConfirmVPL.AppendVPL
                End If

                If nAppend >= 0 Then
                    If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(nAppend).vplGrid, AdvancedSearch) > 0 Then frmVPL(nAppend).formDirty = True
                    frmVPL(nAppend).MdiParent = frmMain
                    frmVPL(countVPL).SetLabels(CurrentSearch)
                    frmVPL(nAppend).Show()
                Else
                    GoTo RecoverIfNotVisible
                End If

            Else
                GoTo RecoverIfNotVisible
            End If
        Else
RecoverIfNotVisible:
            countVPL = countVPL + 1
            ReDim Preserve frmVPL(countVPL)
            frmVPL(countVPL) = New frmVideoPlayList(countVPL)
            If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid, AdvancedSearch) > 0 Then frmVPL(countVPL).formDirty = True
            frmVPL(countVPL).MdiParent = frmMain
            frmVPL(countVPL).SetLabels(CurrentSearch)
            frmVPL(countVPL).Show()
        End If


    End Sub

    Private Sub mnuClusterAddToPathway_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClusterAddToPathway.Click


        Dim nx As Integer = 0
        Dim ny As Integer = 0

        Select Case UserPrefs.Sport
            Case tSports.sHockey
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (90 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (150 / UserPrefs.clVerticalQ))

            Case tSports.sNetball
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (90 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (180 / UserPrefs.clVerticalQ))

            Case tSports.sRugbyLeague
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (68 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (122 / UserPrefs.clVerticalQ))

            Case tSports.sRugby7
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (70 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (120 / UserPrefs.clVerticalQ))

            Case tSports.sBasketball
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (50 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (94 / UserPrefs.clVerticalQ))

            Case tSports.sSoccer
                nx = Int((CurrentMousePointer.X - PitchOffset.X) / (95 / UserPrefs.clHorizontalQ))
                ny = Int((CurrentMousePointer.Y - PitchOffset.Y) / (150 / UserPrefs.clVerticalQ))
        End Select

        If nx < 0 Or ny < 0 Then Exit Sub

        If ClusterArray(nx, ny) > 0 Then
            '*
            '* Create new Game Chart window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = Nothing
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(MySearch)
        End If

        Dim PlaySet As New Microsoft.VisualBasic.Collection
        Dim Index_Play As Integer = 0

        If Not MyClusterInfo(nx, ny).ID Is Nothing Then
            For n As Integer = 0 To MyClusterInfo(nx, ny).ID.Length - 1
                Dim GamePlaySet As GamePlayClass = GetPlays(MyClusterInfo(nx, ny).GameID(n), MyClusterInfo(nx, ny).TimeCriteria(n), MyClusterInfo(nx, ny).PlayNumber(n), 1)

                If Not GamePlaySet.Plays Is Nothing Then
                    For Each NewPlay As GamePlay.Instance In GamePlaySet.Plays
                        Index_Play += 1
                        PlaySet.Add(NewPlay, Index_Play)
                    Next
                End If
            Next
        End If


        frmC(countC).AddPlays(PlaySet)
        frmC(countC).Show()
    End Sub

    Private Sub mnuShowCaptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuShowCaptions.Click
        If Not CurrentPathInfo.GameID Is Nothing Then
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)

            SearchHistory(CurrentSearch).szDescriptors = Nothing
            SearchHistory(CurrentSearch).szSQL = "SELECT * " & _
            "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID " & _
            "WHERE PlayNumber = " & CurrentPathInfo.PlayNumber & " AND PathData.GameID = '" & CurrentPathInfo.GameID & "' AND TimeCriteria = '" & _
            CurrentPathInfo.TimeCriteria & "' ORDER BY ID"

            '*
            '* Create new Game Chart window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(CurrentSearch)

            Dim PlaySet As New Microsoft.VisualBasic.Collection
            Dim CaptionSet As New Microsoft.VisualBasic.Collection
            Dim Index_Play As Integer = 0
            Dim Index_Caption As Integer = 1

            Dim GamePlaySet As GamePlayClass = GetPlays(CurrentPathInfo.GameID, CurrentPathInfo.TimeCriteria, CurrentPathInfo.PlayNumber, 1)
            If Not GamePlaySet.Plays Is Nothing Then
                For Each NewPlay As GamePlay.Instance In GamePlaySet.Plays
                    Index_Play += 1
                    PlaySet.Add(NewPlay, Index_Play)
                Next
            End If

            If Not GamePlaySet.Captions Is Nothing Then
                For Each NewCaption As GamePlay.CaptionBox In GamePlaySet.Captions
                    NewCaption.Index = Index_Caption
                    CaptionSet.Add(NewCaption, Index_Caption)
                    Index_Caption += 1
                Next
            End If

            'Add a final caption that includes the play details.
            If Not CurrentPathInfo.ID Is Nothing Then
                Dim PlayInfoCaption As New GamePlay.CaptionBox

                PlayInfoCaption.Text = CurrentPathInfo.GameID & vbNewLine & CurrentPathInfo.TeamName & vbNewLine & _
                    CurrentPathInfo.TimeCriteria & vbNewLine & CurrentPathInfo.StartTimeString & " --> " & CurrentPathInfo.StopTimeString
                PlayInfoCaption.BoxSize.X = SelectedPathStart.X
                PlayInfoCaption.BoxSize.Y = SelectedPathStart.Y
                CaptionSet.Add(PlayInfoCaption, "b")
            End If

            frmC(countC).AddPlays(PlaySet)
            frmC(countC).AddCaptions(CaptionSet)
            frmC(countC).ShowCaptions = True
            frmC(countC).Show()




        End If
    End Sub

    Private Sub chkHideReceives_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowReceives.CheckStateChanged
        Me.picPitch.Refresh()
    End Sub

    Private Sub chkHideDeliveries_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowDeliveries.CheckStateChanged
        Me.picPitch.Refresh()
    End Sub

    Private Sub numPathOpacity_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles numPathOpacity.ValueChanged
        Me.picPitch.Refresh()
    End Sub

    Private Sub chkHideOtherPlay_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chShowOtherPlay.CheckStateChanged
        Me.picPitch.Refresh()
    End Sub

    Private Sub chkHidePossession_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowPossession.CheckStateChanged
        Me.picPitch.Refresh()
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        pDoc = New PrintDocument
        Dim dlgPrint As New PrintDialog
        dlgPrint.Document = pDoc
        Dim res As Windows.Forms.DialogResult = dlgPrint.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            Try
                dlgPrint.Document.Print()
            Catch ex As Exception
                MsgBox("An error occured trying to print this document.  Check the printer or network connection.")
            End Try
        End If
    End Sub

    Private Sub cmdPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPDF.Click
        pDoc = New PrintDocument
        Dim dlgPrint As New PrintPreviewDialog
        dlgPrint.Document = pDoc
        dlgPrint.ShowDialog()
    End Sub

    Private Sub PrintMe(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDoc.PrintPage

        Dim Scale As RectangleF = e.MarginBounds
        Scale.Width \= 1.25
        Scale.Height \= 1.25

        'Set window size parameters
        Rect = New RectangleF(Scale.Width / 10, Scale.Height / 10, Scale.Width / 1.25, Scale.Height / 1.25)

        'Draw Base Pitch
        DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality, , , KnownColor.Black, KnownColor.Transparent)

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
        Select Case mvarChartType
            Case Chart.ctEventHeatMaps
                Me.grpBoxOptions.Visible = False
                'Dim maxCount As MaxMinValues = GetMaxClusterArrayValues2(HeatArray)

                If Not HeatArray Is Nothing Then
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    For x As Integer = 0 To HeatArray.GetUpperBound(0) - 1
                        For y As Integer = 0 To HeatArray.GetUpperBound(1) - 1
                            Dim ncolor As Color = ClusterColorArray(x, y)
                            e.Graphics.FillRegion(New SolidBrush(ncolor), New Region(New RectangleF(x + PitchOffset.X, y + PitchOffset.Y, 1, 1)))
                        Next
                    Next
                End If

            Case Chart.ctScatterPlots
                Me.grpBoxOptions.Visible = False

                If Not Me.ScatterPlotPoints Is Nothing Then

                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
                        'Cluster data
                        e.Graphics.FillEllipse(New SolidBrush(ScatterPoint.TeamColor), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(2, 2)))
                    Next
                End If


            Case Chart.ctPlayerMaps
                Me.grpBoxOptions.Visible = True
                '*
                '* Draw player maps
                '*
                If Plays.Count > 0 Then
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.ScaleTransform(zX, zY)
                    Dim o As Integer = Me.numPathOpacity.Value
                    Dim k As Integer = 0
                    Dim nColor As Color
                    For Each gp As GamePlay.Instance In Plays
                        k += 1
                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
                        gp.Pen.Brush = New SolidBrush(Color.FromArgb(20, nColor))
                        gp.Pen.Width = UserPrefs.pmLineWidth / zX

                        If gp.Lead Then
                            If Not Me.chkShowReceives.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                            End If

                            gp.Pen.DashStyle = Drawing2D.DashStyle.Dot
                            gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                            gp.Pen.Width *= 1.5
                            If Not Me.chkShowPossession.Checked Then gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                        ElseIf gp.Posession Then
                            If Not Me.chkShowReceives.Checked Then gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                            If Not Me.chkShowPossession.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(nColor)
                            End If
                            gp.Pen.DashStyle = Drawing2D.DashStyle.Solid
                            gp.Pen.Width *= 1.5
                            gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

                        ElseIf gp.Lag Then
                            If Not Me.chkShowDeliveries.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                            End If
                            gp.Pen.DashStyle = Drawing2D.DashStyle.DashDotDot
                            gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                            gp.Pen.Width *= 1.5
                        Else
                            If Not Me.chShowOtherPlay.Checked Then
                                gp.Pen.Brush = New SolidBrush(Color.Transparent)
                            Else
                                gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
                                gp.Pen.Width *= 0.5
                            End If
                        End If

                        If gp.Path.PointCount > 0 Then
                            'If not in show captions mode then highlight play under pointer.
                            If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
                            'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
                            If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

                            e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                        End If
                    Next
                End If

            Case Chart.ctPathways
                '*
                '* Draw pathways
                '*
                Me.grpBoxOptions.Visible = False

                If Plays.Count > 0 Then
                    e.Graphics.ScaleTransform(zX, zY)
                    Dim k As Integer = 0
                    Dim nColor As Color
                    For Each gp As GamePlay.Instance In Plays
                        k += 1
                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
                        gp.Pen.Brush = New SolidBrush(nColor)
                        gp.Pen.Width = UserPrefs.pmLineWidth / zX

                        If gp.Path.PointCount > 0 Then
                            'If not in show captions mode then highlight play under pointer.
                            If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
                            'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
                            If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

                            e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
                        End If
                    Next
                End If

                If Captions.Count > 0 Then
                    e.Graphics.ResetTransform()

                    If Me.ShowCaptions Then
                        'Showing a single gameplay path, including the event captions.
                        Dim lastCaption As New GamePlay.CaptionBox
                        Dim n As Single = 0

                        For Each gc As GamePlay.CaptionBox In Captions
                            'Scale captions
                            gc.BoxSize.X *= zX
                            gc.BoxSize.Y *= zY
                            gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize

                            If gc.BoxSize.Location = lastCaption.BoxSize.Location Then
                                'Modify location of caption for viewing ease.
                                gc.BoxSize.Offset(0, lastCaption.BoxSize.Height + zY)
                                lastCaption.BoxSize.Height = gc.BoxSize.Bottom - lastCaption.BoxSize.Top
                            Else
                                lastCaption = gc
                            End If

                            If gc.Index = CurrentCaptionIndex Then
                                'This caption is selected.
                                If MouseDownOnCaption Then gc.BoxSize.Location = SelectedCaptionBox.Location
                                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightSalmon)), gc.BoxSize)
                                SelectedCaptionPathID = gc.ID
                            Else
                                'Not selected
                                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightGreen)), gc.BoxSize)
                            End If

                            e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)
                            e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
                            New SolidBrush(Color.Black), gc.BoxSize.Location)

                            'Save current caption locations and sizes for mouse-driven movement.
                            If Not CurrentCaptionInfo Is Nothing Then
                                CurrentCaptionInfo(gc.Index).CaptionRect = gc.BoxSize
                            End If

                        Next
                        e.Graphics.ScaleTransform(zX, zY)


                    Else
                        'Showing an information box with details about the play.
                        For Each gc As GamePlay.CaptionBox In Captions
                            Try
                                gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize
                                gc.BoxSize.X = gc.BoxSize.Left - gc.BoxSize.Width
                                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, gc.BackColor)), gc.BoxSize)
                                e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)

                                e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
                                New SolidBrush(gc.ForeColor), gc.BoxSize.Location)

                            Catch ex As Exception

                            End Try
                        Next
                    End If

                End If

            Case Chart.ctClusters
                '*
                '* Draw quadrants
                '*
                If Me.ClusterArray.GetUpperBound(0) = UserPrefs.clHorizontalQ - 1 And Me.ClusterArray.GetUpperBound(1) = UserPrefs.clVerticalQ - 1 Then


                    Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
                    Dim h As Single = Rect.Height / UserPrefs.clVerticalQ
                    Dim nPen As New Pen(Color.Black, 1)
                    Dim nFont As New Font(Me.Font, FontStyle.Bold)
                    Me.grpBoxOptions.Visible = False

                    'Draw colored squares
                    If Not ClusterArray Is Nothing Then
                        For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                            For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                If Me.ClusterArray(x, y) > 0 Then
                                    Dim x1 As Single = (x * w) + Rect.Left
                                    Dim y1 As Single = (y * h) + Rect.Top
                                    e.Graphics.FillRectangle(New SolidBrush(ClusterColorArray(x, y)), x1, y1, w, h)
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
                    If Not ClusterArray Is Nothing Then
                        For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                            For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                                Dim ptxt As PointF = e.Graphics.MeasureString(ClusterArray(x, y), Me.Font)
                                Dim p As PointF = New PointF(Rect.Left + (w * x) + (w / 2) - (ptxt.X / 2), Rect.Top + (h * y) + (h / 2) - (ptxt.Y / 2))
                                If Me.ClusterArray(x, y) > 0 Then
                                    e.Graphics.DrawString(Me.ClusterArray(x, y).ToString, nFont, Brushes.Black, p)
                                End If
                            Next
                        Next
                    End If
                    'Finally, overlay pitch lines again.
                    RedrawOutline(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality)
                    e.Graphics.ScaleTransform(zX, zY)

                End If

        End Select

        'Print details
        Dim leftEdge As Single = Rect.Left / zX
        Dim vertSpace As PointF = e.Graphics.MeasureString(Me.lblChartType.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point))

        e.Graphics.DrawString(Me.lblGameIDs.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge + (1.4 * vertSpace.X) / zX, (1.4 * vertSpace.Y) / zY)
        e.Graphics.DrawString(Me.lblChartType.Text, New Font("Arial", 10, FontStyle.Italic, GraphicsUnit.Document), Brushes.DarkBlue, leftEdge, (1.4 * vertSpace.Y) / zY)

        e.Graphics.DrawString("Team Names:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (1 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.lblTeamNames.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (2 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Time Criteria:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (3 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.lblTimeCriterion.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (4 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Outcome Types:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (5 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.lblOutcomes.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (6 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Included Event Names:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (7 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.lblDescriptors.Text, New Font("Arial", 5, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (8 * vertSpace.Y)) / zY)

        e.Graphics.DrawString("Printed: " & Now.ToString, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (14 * vertSpace.Y)) / zY)

    End Sub

    Private Sub mnuScatterPlayVideo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScatterPlayVideo.Click
        Dim szVideos() As String = GetVideoFiles(SelectedScatter.ID)
        If Not szVideos Is Nothing Then
            If Not szVideos(0) = "None" Then
                PlayVideo(szVideos(0), GetSecondsFromTimeString(SelectedScatter.StartTimeString) - UserPrefs.LeadTime, GetSecondsFromTimeString(SelectedScatter.StopTimeString) + UserPrefs.LagTime)
            Else
                MsgBox("There is no video attached to this datapoint.  Re-Link the game data to a new video file.", MsgBoxStyle.Information, Application.ProductName)
            End If
        End If
    End Sub

    Private Sub mnuScatterChangeColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScatterChangeColor.Click

        Dim oldColor As Color = SelectedScatter.TeamColor
        Dim dlgColor As New ColorDialog
        dlgColor.Color = SelectedScatter.TeamColor
        Dim res As DialogResult = dlgColor.ShowDialog
        If Not res = Windows.Forms.DialogResult.Cancel Then
            Dim dp As ScatterInfo = Nothing
            Dim tempCollection As New Collection

            For i As Integer = 1 To Me.ScatterPlotPoints.Count
                dp = Me.ScatterPlotPoints.Item(i)
                If dp.TeamName = SelectedScatter.TeamName Then
                    dp.TeamColor = dlgColor.Color
                End If
                tempCollection.Add(dp)
            Next
            ScatterPlotPoints.Clear()
            ScatterPlotPoints = tempCollection
        End If


        Me.Refresh()
    End Sub

    Private Sub mnuScatterItem2VPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScatterItem2VPL.Click
        If Not SelectedScatter.GameID Is Nothing Then
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)
            SearchHistory(CurrentSearch).szDescriptors = Nothing
            SearchHistory(CurrentSearch).szSQL = "SELECT * " & _
            "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID " & _
            "WHERE PlayNumber = " & SelectedScatter.PlayNumber & " AND PathData.GameID = '" & SelectedScatter.GameID & "' AND TimeCriteria = '" & _
            SelectedScatter.TimeCriteria & "' ORDER BY ID"

            'Search for any existing playlists
            If countVPL > 0 Then
                If frmVPL(lastVPLFormUsed).Visible Then
                    Dim nAppend As Integer = Nothing
                    frmConfirmVPL.ShowDialog()
                    If frmConfirmVPL.DialogResult = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    Else
                        nAppend = frmConfirmVPL.AppendVPL
                    End If

                    If nAppend >= 0 Then
                        If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(nAppend).vplGrid, Nothing) > 0 Then frmVPL(nAppend).formDirty = True
                        frmVPL(nAppend).MdiParent = frmMain
                        frmVPL(nAppend).Show()
                    Else
                        GoTo RecoverIfNotVisible
                    End If

                Else
                    GoTo RecoverIfNotVisible
                End If
            Else

RecoverIfNotVisible:
                countVPL = countVPL + 1
                ReDim Preserve frmVPL(countVPL)
                frmVPL(countVPL) = New frmVideoPlayList(countVPL)
                If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid, Nothing) > 0 Then frmVPL(countVPL).formDirty = True
                frmVPL(countVPL).SetLabels(CurrentSearch)
                frmVPL(countVPL).MdiParent = frmMain
                frmVPL(countVPL).Show()
            End If


        End If

    End Sub

    Private Sub mnuScatterItem2PathMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuScatterItem2PathMap.Click
        '*
        '* Create new Game Chart window.
        '*
        If Not SelectedScatter.GameID Is Nothing Then

            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = Nothing
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(MySearch)

            Dim PlaySet As New Microsoft.VisualBasic.Collection
            Dim Index_Play As Integer = 0

            Dim GamePlaySet As GamePlayClass = GetPlays(SelectedScatter.GameID, SelectedScatter.TimeCriteria, SelectedScatter.PlayNumber, 1)
            If Not GamePlaySet.Plays Is Nothing Then
                For Each NewPlay As GamePlay.Instance In GamePlaySet.Plays
                    Index_Play += 1
                    PlaySet.Add(NewPlay, Index_Play)
                Next
            End If

            frmC(countC).AddPlays(PlaySet)
            frmC(countC).Show()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPublishChart.Click

        'Publish current chart data into xml for iPhone.

        Dim thisChart As String
        If Me.ChartType = Chart.ctScatterPlots Then
            thisChart = "scatter"
        ElseIf Me.ChartType = Chart.ctClusters Then
            thisChart = "cluster"
        ElseIf Me.ChartType = Chart.ctPathways Then
            thisChart = "pathway"
        Else
            Exit Sub
        End If


        Dim fnum As Integer = FreeFile()
        Dim szFileName As String = "F:\Developer\Dev\Pattern Plotter Apps\PatternView\"
        If Not System.IO.Directory.Exists(szFileName) Then
            Dim dlgFindFolder As New FolderBrowserDialog
            Dim res As DialogResult = dlgFindFolder.ShowDialog()
            If Not res = Windows.Forms.DialogResult.Cancel Then
                szFileName = dlgFindFolder.SelectedPath & "\"
            Else
                Exit Sub
            End If
        End If

        szFileName &= Format(Now, "yyMMddHHmmss") & Me.lblGameIDs.Text & ".xml"

        FileOpen(fnum, szFileName, OpenMode.Output)
        Print(fnum, "<chart_data>" & vbNewLine)
        Print(fnum, "<chart_type>" & thisChart & "</chart_type>" & vbNewLine)
        Print(fnum, "<title>" & Me.Text & "</title>" & vbNewLine)
        Print(fnum, "<team>" & Me.lblTeamNames.Text & "</team>" & vbNewLine)
        Print(fnum, "<time>" & Me.lblTimeCriterion.Text & "</time>" & vbNewLine)
        Print(fnum, "<event>" & Me.lblDescriptors.Text & "</event>" & vbNewLine)

        Print(fnum, "<quadrants_horizontal>" & UserPrefs.clHorizontalQ.ToString & "</quadrants_horizontal>" & vbNewLine)
        Print(fnum, "<quadrants_vertical>" & UserPrefs.clVerticalQ.ToString & "</quadrants_vertical>" & vbNewLine)
        Print(fnum, "<points>" & vbNewLine)

        If Me.ChartType = Chart.ctScatterPlots Then
            For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
                Print(fnum, "   <point x ='" & ScatterPoint.Location.X.ToString & _
                "' y='" & ScatterPoint.Location.Y & _
                "' colorr='" & ScatterPoint.TeamColor.R.ToString & _
                "' colorg='" & ScatterPoint.TeamColor.G.ToString & _
                "' colorb='" & ScatterPoint.TeamColor.B.ToString & _
                "' count='1" & _
                "' endpath='0" & _
                "'>EventName</point>" & vbNewLine)
            Next

        ElseIf Me.ChartType = Chart.ctClusters Then
            For x As Integer = ClusterArray.GetLowerBound(0) To ClusterArray.GetUpperBound(0)
                For y As Integer = ClusterArray.GetLowerBound(1) To ClusterArray.GetUpperBound(1)
                    Print(fnum, "   <point x ='" & x.ToString & _
                    "' y='" & y.ToString & _
                    "' colorr='0" & _
                    "' colorg='0" & _
                    "' colorb='0" & _
                    "' count='" & Me.ClusterArray(x, y).ToString & _
                    "' endpath='0" & _
                    "'>EventName</point>" & vbNewLine)
                Next
            Next

        ElseIf Me.ChartType = Chart.ctPathways Then
            For Each play As GamePlay.Instance In Me.Plays
                Dim n As Integer = 0
                Dim isEnd As Integer = 0
                Dim nColor As Color
                If play.Path.PointCount > 0 Then
                    For Each datapoint As PointF In play.Path.PathPoints
                        n += 1
                        If n = play.Path.PointCount Then
                            'End of path
                            isEnd = 1
                        End If
                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, n, Plays.Count)

                        Print(fnum, "   <point x ='" & datapoint.X.ToString & _
                         "' y='" & datapoint.Y.ToString & _
                         "' colorr='" & nColor.R.ToString & _
                         "' colorg='" & nColor.G.ToString & _
                         "' colorb='" & nColor.B.ToString & _
                         "' count='1" & _
                         "' endpath='" & isEnd.ToString & _
                         "'>Path</point>" & vbNewLine)
                    Next
                End If
            Next
 
        End If

        Print(fnum, "</points>" & vbNewLine)
        Print(fnum, "</chart_data>" & vbNewLine)
        FileClose(fnum)

        Exit Sub

errCatch:
        Err.Clear()



    End Sub
End Class