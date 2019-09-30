Imports System.Drawing.Printing
Imports System.Drawing.Imaging

Public Class frmGraph
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim Rect As RectangleF = Nothing

    Public idForm As Integer
    Private myChart As New Excel.Chart

    Public ChartLabel As String

    Public MySearch As Integer  'Retains the search criteria id.

    Private mvarGraphType As MSChart20Lib.VtChChartType = Nothing

    Friend ThisGraph As GraphType

    Private ColorColumnsSet As Boolean = False
    Private ColorRowsSet As Boolean = False

    Private WithEvents pDoc As PrintDocument
    Private PreviewPrintImage As Image


    Public Property ChartType() As MSChart20Lib.VtChChartType

        Get
            Return mvarGraphType
        End Get
        Set(ByVal value As MSChart20Lib.VtChChartType)
            mvarGraphType = value
            Select Case mvarGraphType
                Case Is = MSChart20Lib.VtChChartType.VtChChartType2dBar Or MSChart20Lib.VtChChartType.VtChChartType3dBar
                    Me.lblChartType.Text = "Bar Graph: "
                Case MSChart20Lib.VtChChartType.VtChChartType2dLine Or MSChart20Lib.VtChChartType.VtChChartType3dLine
                    Me.lblChartType.Text = "Line Graph: "
                Case MSChart20Lib.VtChChartType.VtChChartType2dPie
                    Me.lblChartType.Text = "Pie Chart: "
                Case MSChart20Lib.VtChChartType.VtChChartType2dXY
                    Me.lblChartType.Text = "XY Plot: "
                Case MSChart20Lib.VtChChartType.VtChChartType2dStep Or MSChart20Lib.VtChChartType.VtChChartType3dStep
                    Me.lblChartType.Text = "Step Chart: "
            End Select
            Me.Text = Me.lblChartType.Text
        End Set
    End Property

    Public Sub SetLabels(ByVal ThisSearch As Integer, Optional ByVal FindDescriptors As Boolean = True)
        'NB: FindDescriptors is used here to avoid implementing a search for checked items in the descriptor list
        'where the analysis window is not open.  If this occurs, it resets the SearchHistory array which results
        'in errors during the wireless automated reporting function, for which the analysis window is not open.
        'So, FindDescriptors will only be false when the SetLabels function is called during the automated search routine.

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

    Private Sub frmGraph_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting(AppName, "Settings", "GraphWidth", Me.Size.Width.ToString)
        SaveSetting(AppName, "Settings", "GraphHeight", Me.Size.Height.ToString)

    End Sub

    Private Sub frmGraph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub SetData(ByVal Data(,) As Single)
        If Data Is Nothing Then Exit Sub

        With Me.Graph
            .RandomFill = False
            .chartType = ThisGraph.ChartType
            .ChartData = Data

            'Set columns
            If ThisGraph.DataGroup <> "None" Then
                .ColumnCount = ThisGraph.DataGroupLabels.Length
                For Each Col As String In ThisGraph.DataGroupLabels
                    .Column = Array.IndexOf(ThisGraph.DataGroupLabels, Col) + 1

                    If ThisGraph.DataGroup = "Region" Then
                        .ColumnLabel() = ConvertRegionGraphLabels(Col)
                    Else
                        .ColumnLabel() = Col
                    End If

                    'If teams, then get colors.
                    AssignGraphColors(.Column, ThisGraph.DataGroup)

                Next
                .Legend.Location.Visible = True
                .Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom
            Else
                .Legend.Location.Visible = False
                .ColumnCount = 1
            End If

            'Set rows
            .RowCount = ThisGraph.xAxisLabels.Length
            For Each Row As String In ThisGraph.xAxisLabels
                .Row = Array.IndexOf(ThisGraph.xAxisLabels, Row) + 1

                If ThisGraph.xAxis = "Region" Then
                    .RowLabel() = ConvertRegionGraphLabels(Row)
                Else
                    .RowLabel() = Row
                End If
            Next

            .Stacking = ThisGraph.Stacked
            .TitleText = ThisGraph.Title
            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = ThisGraph.yAxis & ThisGraph.yAxisScale

            Me.mnuGraphTitle.Text = ThisGraph.Title
            Me.mnuGraphStacked.Checked = ThisGraph.Stacked
            UpdateCheckState(.chartType)

            .Refresh()
        End With


    End Sub

    Private Sub AssignGraphColors(ByVal Col As Integer, ByVal DataGroupLabel As String)

        Dim tc As Color
        If DataGroupLabel = "TeamName" Then
            tc = GetTeamColor(SearchHistory(MySearch).szTeamName(Col - 1), SearchHistory(MySearch).szGameID)
        Else
            tc = KnownGraphColors(Col + 98)
        End If

        Graph.Plot.SeriesCollection(Col).DataPoints(-1).Brush.FillColor.Set(tc.R, tc.G, tc.B)
        Graph.Plot.SeriesCollection(Col).DataPoints(-1).EdgePen.VtColor.Set(10, 10, 10)

    End Sub

    Public Overloads Function ConvertRegionGraphLabels(ByVal RegionString() As String) As String()
        Dim retString(RegionString.Length) As String

        For Each item As Object In RegionString
            retString(Array.IndexOf(RegionString, item)) = GetRegionString(item)
        Next

        Return retString

    End Function

    Public Overloads Function ConvertRegionGraphLabels(ByVal RegionString As String) As String
        Return GetRegionString(RegionString)
    End Function

    Private Sub UpdateCheckState(ByVal ChartType As MSChart20Lib.VtChChartType)

        Me.mnuGraph2DBar.Checked = False
        Me.mnuGraph2DLine.Checked = False
        Me.mnuGraphPie.Checked = False
        Me.mnuGraph2DStep.Checked = False
        Me.mnuGraph3DBar.Checked = False
        Me.mnuGraph3DLine.Checked = False
        Me.mnuGraph3DStep.Checked = False

        Select Case ChartType
            Case MSChart20Lib.VtChChartType.VtChChartType2dBar
                Me.mnuGraph2DBar.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType2dLine
                Me.mnuGraph2DLine.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType2dPie
                Me.mnuGraphPie.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType2dStep
                Me.mnuGraph2DStep.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType3dBar
                Me.mnuGraph3DBar.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType3dLine
                Me.mnuGraph3DLine.Checked = True
            Case MSChart20Lib.VtChChartType.VtChChartType3dStep
                Me.mnuGraph3DStep.Checked = True
        End Select

    End Sub

    Public Sub New(ByVal intID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        idForm = intID

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            sc.CaptureWindowToFile(Me.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
        End If


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Public Sub SaveChartImage(ByVal szDestination As String)
        Dim sc As New ScreenShot.ScreenCapture
        sc.CaptureWindowToFile(Me.Handle, szDestination, Imaging.ImageFormat.Jpeg)
    End Sub

    Private Sub Graph_MouseDownEvent(ByVal sender As Object, ByVal e As AxMSChart20Lib._DMSChartEvents_MouseDownEvent) Handles Graph.MouseDownEvent
        If e.button = 2 Then
            Me.mnuGraphType.Show(Me, e.x, e.y)
        End If
    End Sub

    Private Sub mnuGraph2DBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph2DBar.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType2dBar
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraph2DLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph2DLine.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraph2DStep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph2DStep.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType2dStep
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraph3DBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph3DBar.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType3dBar
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraph3DLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph3DLine.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType3dLine
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraph3DStep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraph3DStep.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType3dStep
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraphPie_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphPie.Click
        Me.Graph.chartType = MSChart20Lib.VtChChartType.VtChChartType2dPie
        Me.Graph.Refresh()
        UpdateCheckState(Me.Graph.chartType)

    End Sub

    Private Sub mnuGraphRowAsSeries_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphRowAsSeries.Click
        Me.Graph.Plot.DataSeriesInRow = Me.mnuGraphRowAsSeries.Checked
        Application.DoEvents()

        'If teams, then get colors.
        If Graph.Plot.DataSeriesInRow And Not ColorRowsSet Then
            For Row As Integer = 1 To Graph.RowCount
                AssignGraphColors(Row, ThisGraph.xAxis)
            Next
            ColorRowsSet = True

        ElseIf Not Graph.Plot.DataSeriesInRow And Not ColorColumnsSet Then
            For Col As Integer = 1 To Graph.ColumnCount
                AssignGraphColors(Col, ThisGraph.DataGroup)
            Next
            ColorColumnsSet = True
        End If

        Me.Graph.Refresh()
    End Sub

    Private Sub mnuGraphStacked_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphStacked.Click
        Me.Graph.Stacking = Me.mnuGraphStacked.Checked
        Me.Graph.Refresh()
    End Sub

    Private Sub mnuGraphTitle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphTitle.TextChanged
        Me.Graph.TitleText = Me.mnuGraphTitle.Text
        Me.Graph.Refresh()

    End Sub

    Private Sub mnuGraphTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGraphTitle.Click

    End Sub

    Private Sub mnuGraphHideGridLines_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphHideGridLines.CheckStateChanged

        If Me.mnuGraphHideGridLines.Checked Then
            Me.Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleNull
            Me.Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleNull
        Else
            Me.Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
            Me.Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
        End If

    End Sub

    Private Sub Cancel_Button_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

    Private Sub OK_Button_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
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
            sc.CaptureWindowToFile(Graph.hWnd, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
        End If

    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim sc As New ScreenShot.ScreenCapture
        Application.DoEvents()
        PreviewPrintImage = sc.CaptureWindow(Graph.hWnd)

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

    Private Sub cmdPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPDF.Click
        Dim sc As New ScreenShot.ScreenCapture
        Application.DoEvents()
        PreviewPrintImage = sc.CaptureWindow(Graph.hWnd)

        pDoc = New PrintDocument
        Dim dlgPrint As New PrintPreviewDialog
        dlgPrint.Document = pDoc
        dlgPrint.ShowDialog()

    End Sub

    Private Sub PrintMe(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDoc.PrintPage
        Try

            Dim grfx As Graphics = e.Graphics
            grfx.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            Dim gSize As Rectangle = e.MarginBounds
            gSize.Height /= 2
            grfx.DrawImage(PreviewPrintImage, gSize)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub mnuEditChartProps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditChartProps.Click
        Me.Graph.ShowPropertyPages()
    End Sub

End Class