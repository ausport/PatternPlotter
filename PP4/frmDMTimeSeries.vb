Imports System.Windows.Forms
Imports System.Drawing.Printing


Public Class frmDMTimeSeries
    Private WithEvents pDoc As PrintDocument
    Private PreviewPrintImage As Image

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub frmDMTabs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not GamesCurrentlyOpen Is Nothing Then
            For Each item As GameProperties In GamesCurrentlyOpen
                Me.cboGameID.Items.Add(item.GameID)
            Next

            For Each item As String In frmAnalysis.cboTeamName.Items
                If item <> "*" Then Me.cboTeamName.Items.Add(item)
            Next

            For Each item As String In frmAnalysis.cboTimeCriteria.Items
                If item <> "*" Then Me.cboTimeCriteria.Items.Add(item)
            Next

            For Each item As String In frmAnalysis.lstDescriptors.Items
                If item <> "*" Then Me.lstDescriptors.Items.Add(item, True)
            Next

        End If

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub cmdSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectNone.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub rdoInterestingness_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDistance.CheckedChanged
        'Me.cmdRefreshDataSet_Click(sender, e)
    End Sub

    Public Sub cmdRefreshDataSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Function GetGraphColors(ByVal Col As Integer, ByVal DataGroupLabel As String, ByVal ItemLabel As String, ByVal szGameIDs() As String)

        Dim tc As Color
        If DataGroupLabel = "TeamName" Then
            tc = GetTeamColor(ItemLabel, szGameIDs)
        Else
            tc = KnownGraphColors(Col + 98)
        End If

        axChart.Plot.SeriesCollection(Col).DataPoints(-1).Brush.FillColor.Set(tc.R, tc.G, tc.B)
        axChart.Plot.SeriesCollection(Col).DataPoints(-1).EdgePen.VtColor.Set(10, 10, 10)
        Return Nothing
    End Function

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        If Not PathCount > 0 Then Exit Sub

        Dim szTableName As String = "PathData"
        Dim CurrentGameIDs(GamesCurrentlyOpen.Length - 1) As String
        For Each game As GameProperties In GamesCurrentlyOpen
            CurrentGameIDs(Array.IndexOf(GamesCurrentlyOpen, game)) = game.GameID
        Next

        Dim DescriptorItemsList() As String = Nothing
        Dim SearchParameters As String = ""

        '1.1. Compile multiple gameID string
        Dim szSearchString As String = "(PathData.GameID = "
        For Each Game As String In CurrentGameIDs
            szSearchString &= "'" & Game & "'"
            If Array.IndexOf(CurrentGameIDs, Game) < CurrentGameIDs.Length - 1 Then
                szSearchString &= " OR PathData.GameID = "
            End If
        Next
        szSearchString &= ")"

        'If selected, then add specific paramters to the search string
        If Not Me.cboGameID.Text = "*" Then SearchParameters &= " AND PathData.GameID = '" & Me.cboGameID.Text & "'"
        If Not Me.cboTimeCriteria.Text = "*" Then SearchParameters &= " AND TimeCriteria = '" & Me.cboTimeCriteria.Text & "'"
        If Not Me.cboTeamName.Text = "*" Then SearchParameters &= " AND TeamName = '" & Me.cboTeamName.Text & "'"
        If Not Me.cboOutcome.Text = "*" Then SearchParameters &= " AND Outcome = " & GetOutcomeFromString(Me.cboOutcome.Text)

        'If any descriptors are UN-selected, then add to search string
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Unchecked Then
                If DescriptorItemsList Is Nothing Then
                    ReDim DescriptorItemsList(0)
                Else
                    ReDim Preserve DescriptorItemsList(DescriptorItemsList.Length)
                End If
                DescriptorItemsList(DescriptorItemsList.Length - 1) = Me.lstDescriptors.Items.Item(i)
            End If
        Next

        If Not DescriptorItemsList Is Nothing Then
            SearchParameters &= " AND "
            For Each xItem As String In DescriptorItemsList
                SearchParameters &= "EventName <> '" & xItem & "'"
                If Array.IndexOf(DescriptorItemsList, xItem) < DescriptorItemsList.Length - 1 Then
                    SearchParameters = SearchParameters & " AND "
                End If
            Next
        End If


        If Me.cboDM_Xaxis.Text = "Outcome Type" Or Me.cboDM_Xaxis.Text = "Event Name" Then szTableName = "PathOutcomes" Else szTableName = "PathData"
        Dim ItemsX As String() = GetItemsFromField(CurrentGameIDs, TranslateField(cboDM_Xaxis.Text), szTableName, SearchParameters)

        If ItemsX Is Nothing Then
            MsgBox("No valid data was found for this analysis.  The chart was not generated.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim ThisGraph As New GraphType
        szSearchString = "SELECT Timecode, x, y, EventName, " & TranslateField(cboDM_Xaxis.Text, True) & " FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szSearchString & SearchParameters
        Dim n As Integer = GetEventCount(szSearchString)
        If n = 0 Then
            MsgBox("No valid data was found for this analysis.  The chart was not generated.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim xTicks(n) As String

        With ThisGraph
            Me.axChart.Visible = False
            .ChartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
            .xAxis = TranslateField(cboDM_Xaxis.Text, False)
            If Me.rdoFrequency.Checked Then
                .yAxis = "Frequency"
            ElseIf Me.rdoDistance.Checked Then
                .yAxis = "DMDistance"
            ElseIf Me.rdoRatios.Checked Then
                .yAxis = "Time"
            End If
            .DataGroup = cboDM_Xaxis.Text
            .DataGroupLabels = ItemsX
            .xAxisLabels = xTicks   'n events
        End With

        With Me.axChart
            Dim szOrder As String = Nothing
            If Me.cboDM_Xaxis.Text = "Time Criteria" Then
                szOrder = " ORDER BY TimeCode"
            Else
                szOrder = " ORDER BY TimeCriteria, TimeCode"
            End If

            .ChartData = CompileGraphData(szSearchString & szOrder, Nothing, ThisGraph)
            'set number of x/y-axis items
            .ColumnCount = ThisGraph.DataGroupLabels.Length
            frmMain.toolProgressBar.Maximum = .ColumnCount


            For Each Col As String In ThisGraph.DataGroupLabels
                frmMain.toolActionStatus.Text = "Formatting Frequency Graph Data..."

                .Column = Array.IndexOf(ThisGraph.DataGroupLabels, Col) + 1
                frmMain.toolProgressBar.Value = .Column
                Application.DoEvents()

                If ThisGraph.DataGroup = "Region" Then
                    .ColumnLabel() = GetRegionString(Col)
                ElseIf ThisGraph.DataGroup = "Outcome Type" Then
                    .ColumnLabel() = GetOutcomeString(Col)
                Else
                    .ColumnLabel() = Col
                End If

                'If teams, then get colors.
                Dim tColor As Color = Color.Red
                If cboDM_Xaxis.Text = "Team Name" Then
                    tColor = GetTeamColor(Col, CurrentGameIDs)
                Else
                    tColor = KnownGraphColors(.Column + 98)
                End If
            Next
            .Legend.Location.Visible = True
            .Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom

            Me.axChart.Visible = True
        End With

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub OK_Button_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Save chart to JPEG

        Dim res As DialogResult = Windows.Forms.DialogResult.Cancel

        'Get a new filename if one does not already exist.
        Dim dlgFileName = New SaveFileDialog
        dlgFileName.Filter = "JPG Image|*.jpg"
        dlgFileName.InitialDirectory = UserPrefs.VideoCaptureDir
        dlgFileName.FileName = "Random Walk.jpg"
        res = dlgFileName.ShowDialog()
        If res <> Windows.Forms.DialogResult.Cancel Then
            Dim sc As New ScreenShot.ScreenCapture
            Application.DoEvents()
            sc.CaptureWindowToFile(Me.axChart.hWnd, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
        End If
    End Sub

    Private Sub cmdPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPDF.Click
        Dim sc As New ScreenShot.ScreenCapture
        Application.DoEvents()
        PreviewPrintImage = sc.CaptureWindow(axChart.hWnd)

        pDoc = New PrintDocument
        Dim dlgPrint As New PrintPreviewDialog
        dlgPrint.Document = pDoc
        dlgPrint.ShowDialog()
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim sc As New ScreenShot.ScreenCapture
        Application.DoEvents()
        PreviewPrintImage = sc.CaptureWindow(axChart.hWnd)

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

    Private Sub mnuTimeSeriesEditGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTimeSeriesEditGraph.Click
        Me.axChart.ShowPropertyPages()
    End Sub

    Private Sub axChart_MouseDownEvent(ByVal sender As Object, ByVal e As AxMSChart20Lib._DMSChartEvents_MouseDownEvent) Handles axChart.MouseDownEvent
        If e.button = 2 Then
            Me.mnuTimeSeries.Show(Me, e.x, e.y)
        End If

    End Sub
End Class
