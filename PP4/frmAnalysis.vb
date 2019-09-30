Imports System.Windows.Forms

Public Class frmAnalysis
    Public AllowOutcomesUpdate As Boolean = False


    Public Function AddPlayers(ByVal szPlayerName As String) As Integer
        'NB returns numner of players in list.
        Dim k As Integer
        For k = 0 To Me.cboTeamName.Items.Count - 1
            If cboTeamName.Items(k).ToString = szPlayerName Or szPlayerName = "" Then
                Return cboTeamName.Items.Count
                Exit Function
            End If
        Next

        cboTeamName.Items.Add(szPlayerName)
        Return cboTeamName.Items.Count

    End Function

    Public Function AddTimeCriterion(ByVal szTimeCriterion As String) As Integer
        'NB returns numner of players in list.
        Dim k As Integer
        For k = 0 To Me.cboTimeCriteria.Items.Count - 1
            If cboTimeCriteria.Items(k).ToString = szTimeCriterion Or szTimeCriterion = "" Then
                Return cboTimeCriteria.Items.Count
                Exit Function
            End If
        Next

        cboTimeCriteria.Items.Add(szTimeCriterion)
        Return cboTimeCriteria.Items.Count

    End Function

    Public Function AddDescriptors(ByVal szDescriptor As String, ByVal uType As Integer) As Integer
        'NB returns numner of players in list.
        Dim k As Integer
        Dim bFound As Boolean = False

        If Not VisibleDescriptorList Is Nothing Then
            k = UBound(VisibleDescriptorList) + 1

            For Each item As VisibleDescriptor In VisibleDescriptorList
                If item.Text = szDescriptor And CInt(item.Type) = uType Then
                    bFound = True
                    Exit For
                End If
            Next item

        Else
            k = 0
            bFound = False
        End If

        If Not bFound Then
            ReDim Preserve VisibleDescriptorList(k)
            VisibleDescriptorList(k).Type = uType
            VisibleDescriptorList(k).Text = szDescriptor
            VisibleDescriptorList(k).Visible = True
            Me.lstDescriptors.Items.Add(szDescriptor, True)
        End If

        Return Me.lstDescriptors.Items.Count

    End Function

    Private Sub RefreshDescriptorList(ByVal uType As OutcomeType)
        If Not VisibleDescriptorList Is Nothing Then
            Me.lstDescriptors.Items.Clear()
            For Each item As VisibleDescriptor In VisibleDescriptorList
                If (item.Type = uType Or uType = OutcomeType.outAll) Then
                    lstDescriptors.Items.Add(item.Text, True)
                End If
            Next
        End If
    End Sub

    Private Sub SaveAutoSearch()

        Dim dlgPlaylist As New SaveFileDialog

        With dlgPlaylist
            .Title = "Save automated search profile..."
            .Filter = "PP4 Search Files|*.psh"
            .FileName = frmTags.Text
            .OverwritePrompt = True
            Dim res As DialogResult = .ShowDialog(frmMain)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim CheckedItems As CheckedListBox.CheckedItemCollection = Me.lstDescriptors.CheckedItems
            Dim fnum As Integer = FreeFile()
            FileOpen(fnum, .FileName, OpenMode.Output)

            Print(fnum, Me.chkOutcomeClusters.Checked.ToString & ";")   'Activate outcome clusters
            Print(fnum, Me.chkPathwayMaps.Checked.ToString & ";")       'Activate pathway maps
            Print(fnum, Me.chkVideoPlaylist.Checked.ToString & ";")     'Activate VPL
            Print(fnum, Me.chkPossessionGraphs.Checked.ToString & ";")  'Activate graphs.

            Print(fnum, Me.cboTeamName.Text & ";")  'Team names.
            Print(fnum, Me.cboOutcome.Text & ";")  'Outcomes .
            Print(fnum, Me.cboTimeCriteria.Text & ";")  'Time criteria .

            For i As Integer = 0 To CheckedItems.Count - 1
                Print(fnum, CheckedItems(i) & ";")
            Next

            FileClose(fnum)
        End With

        Exit Sub

errCatch:
        Err.Clear()
    End Sub

    Public Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If GameCount = 0 Then Exit Sub

        'If form used to save a search...
        If OK_Button.Text = "Save Search" Then
            SaveAutoSearch()
            Exit Sub
        End If

        '*
        '* Update SearchHistory()
        '*
        CurrentSearch += 1
        ReDim Preserve SearchHistory(CurrentSearch)

        'Set GameID collection
        If Not Me.chkUseAdvancedSearch.Checked Then
            'Not using an advanced search set, so set GameID collection to all games.
            Erase SearchHistory(CurrentSearch).szGameID
            Dim n As Integer = 0
            For Each nGame As String In szGamesLoaded
                If Not nGame Is Nothing Then
                    ReDim Preserve SearchHistory(CurrentSearch).szGameID(n)
                    SearchHistory(CurrentSearch).szGameID(n) = nGame
                    n = n + 1
                End If
            Next

            'Set outcome type if not already set
            Me.AllowOutcomesUpdate = False
            cboOutcome_SelectedValueChanged(Nothing, Nothing)
            Me.AllowOutcomesUpdate = True

            'Set team names
            Me.cboTeamName_SelectedIndexChanged(Nothing, Nothing)

            Me.cboTimeCriteria_SelectedIndexChanged(Nothing, Nothing)

            'Set descriptor items
            SearchHistory(CurrentSearch).szDescriptors = GetCheckedDescriptorList(True)
        Else
            With AdvancedSearch

                'If using an advanced search set.
                SearchHistory(CurrentSearch).szGameID = AdvancedSearch.GameID

                'Set outcome type if not already set
                ReDim SearchHistory(CurrentSearch).uOutcomes(0)
                SearchHistory(CurrentSearch).uOutcomes(0) = OutcomeType.outAll

                'Set team names
                'NB: This is needed currently because failing to populate the .TeamName array will casue an
                'error when creating a graph by TeamName from the advanced search feature.  It searches the array
                'several times to find matching color sets.
                If .TeamName Is Nothing Then
                    SearchHistory(CurrentSearch).szTeamName = modSearchEngine.GetTeamsList
                Else
                    SearchHistory(CurrentSearch).szTeamName = .TeamName
                End If

                SearchHistory(CurrentSearch).szTimeCriterion = .TimeCriteria

                'Set descriptor items
                SearchHistory(CurrentSearch).szDescriptors = .EventNameSet
            End With

        End If


        '*
        '* Create analysis reports using SQL database search and then cross-referenced event names.
        '*

        If Me.chkVideoPlaylist.CheckState = CheckState.Checked Then
            '*
            '* Create new VPL window.
            '*

            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uVideoPlaylist, Nothing)

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
                frmVPL(countVPL).SetLabels(CurrentSearch)
                frmVPL(countVPL).MySearch = CurrentSearch
                frmVPL(countVPL).MdiParent = frmMain
                frmVPL(countVPL).Show()
            End If


        End If

        If Me.chkScatterPlot.CheckState = CheckState.Checked Then
            '*
            '* Create new Scatter Plot window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uScatterPlot, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).ScatterPlotPoints = CompileScatterArray(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            frmC(countC).ChartType = frmChart.Chart.ctScatterPlots
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If


        If Me.chkPosessionHeat.CheckState = CheckState.Checked Then
            '*
            '* Create new Event Count Heat Map window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uEventCountHeatMaps, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).HeatArray = CompileEventCountHeatMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            If Not frmC(countC).HeatArray Is Nothing Then
                frmC(countC).ClusterColorArray = CompileClusterColors2(frmC(countC).HeatArray)
            End If
            frmC(countC).ChartType = frmChart.Chart.ctEventHeatMaps
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If

        If Me.chkBallSpdHeat.CheckState = CheckState.Checked Then
            '*
            '* Create new Ball Speed Heat Map window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uBallSpeedHeatMaps, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).HeatArray = CompileBallSpeedHeatMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            If Not frmC(countC).HeatArray Is Nothing Then
                frmC(countC).ClusterColorArray = CompileClusterColors2(frmC(countC).HeatArray)
            End If
            frmC(countC).ChartType = frmChart.Chart.ctBallSpeedHeatMaps
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If

        If Me.chkPossessTimeHeat.CheckState = CheckState.Checked Then
            '*
            '* Create new Possession Time Heat Map window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uPossessionTimeHeatMaps, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).HeatArray = CompileEventCountHeatMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            If Not frmC(countC).HeatArray Is Nothing Then
                frmC(countC).ClusterColorArray = CompileClusterColors2(frmC(countC).HeatArray)
            End If
            frmC(countC).ChartType = frmChart.Chart.ctPossessionTimeHeatMaps
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If

        If Me.chkPathwayMaps.CheckState = CheckState.Checked Then
            '*
            '* Create new Game Chart window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uPathwayMaps, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).AddPlays(CompilePathwayMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch))
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If

        If Me.chkPlayerMaps.CheckState = CheckState.Checked Then
            '*
            '* Create new Game Player Map window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uPlayerMaps, Nothing)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).AddPlays(CompilePlayerMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch))
            frmC(countC).ChartType = frmChart.Chart.ctPlayerMaps
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()
        End If

        If Me.chkOutcomeClusters.CheckState = CheckState.Checked Then
            '*
            '* Create new Clusters Chart window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uOutcomeClusters, Nothing)
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).ClusterArray = CompileClusterArrays(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            If Not frmC(countC).ClusterArray Is Nothing Then
                frmC(countC).ClusterColorArray = CompileClusterColors(frmC(countC).ClusterArray)
            End If
            frmC(countC).MdiParent = frmMain
            frmC(countC).ChartType = frmChart.Chart.ctClusters
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).SetClusterInfo()
            frmC(countC).Show()
        End If

        If Me.chkPossessionGraphs.CheckState = CheckState.Checked Then
            '*
            '* Create new Graph window.
            '*

            'Catch error if not graphs have been set.
            If CurrentGraphs Is Nothing Then
                MsgBox("No graphs profiles have been set.  Use the 'Set Graphs' function to configure a new graph profile.", MsgBoxStyle.Critical, Application.ProductName)
                Exit Sub
            End If

            Dim SearchTime As New Stopwatch
            SearchTime.Start()

            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Graphs..."
            frmMain.toolProgressBar.Maximum = CurrentGraphs.Length

            For Each Graph As GraphType In CurrentGraphs
                If Graph.GraphChecked Then
                    countG = countG + 1
                    ReDim Preserve frmG(countG)

                    'Create basic SQL query.
                    SearchHistory(CurrentSearch).szSQL = CompileSearchString(SearchHistory(CurrentSearch), AnalysisType.uPosessionGraph, Graph)

                    'Get categories of xAxis and DataGroups, and apply to the GraphType structure: Graph
                    If Graph.xAxis = "EventName" Then
                        Graph.xAxisLabels = GetCategories(Graph.xAxis, CurrentSearch, GetDescriptorListSQL)
                    ElseIf Graph.xAxis = "Time (Minutes)" Then

                        If Graph.DataGroup = "TimeCriteria" Then
                            'Get x-axis as 0 to 35mins
                            ReDim Preserve Graph.xAxisLabels(modSearchEngine.GetHighestTimeCode(SearchHistory(CurrentSearch)))
                        Else
                            'Get x-axis as 0 to 35 and then 36 to 70.
                            ReDim Preserve Graph.xAxisLabels(modSearchEngine.GetHighestTimeCode(SearchHistory(CurrentSearch), True))
                        End If

                        For i As Integer = 0 To Graph.xAxisLabels.GetUpperBound(0)
                            Graph.xAxisLabels(i) = i.ToString
                        Next

                    Else
                        Graph.xAxisLabels = GetCategories(Graph.xAxis, CurrentSearch)
                    End If


                    If Not Graph.DataGroup = "None" Then
                        If Graph.DataGroup = "EventName" Then
                            Graph.DataGroupLabels = GetCategories(Graph.DataGroup, CurrentSearch, GetDescriptorListSQL)
                        Else
                            Graph.DataGroupLabels = GetCategories(Graph.DataGroup, CurrentSearch)
                        End If
                    End If

                    'Apply updated Graph data structure to the graph form.
                    frmG(countG) = New frmGraph(countG)
                    frmG(countG).ThisGraph = Graph
                    frmG(countG).MySearch = CurrentSearch

                    'Derive data sets into array. 
                    If Graph.yAxis = "Event Probability" Then
                        frmG(countG).SetData(CompileEventProbability(SearchHistory(CurrentSearch), AdvancedSearch, Graph))
                        frmG(countG).Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleNull
                        frmG(countG).mnuGraphHideGridLines.Checked = True
                    ElseIf Graph.yAxis = "Posession Time" And Graph.xAxis = "Time (Minutes)" Then
                        frmG(countG).SetData(CompileMovingPossession(SearchHistory(CurrentSearch), AdvancedSearch, Graph))
                        frmG(countG).Graph.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisGrid.MajorPen.Style = MSChart20Lib.VtPenStyle.VtPenStyleNull
                        frmG(countG).mnuGraphHideGridLines.Checked = True
                    Else
                        frmG(countG).SetData(CompileGraphData(SearchHistory(CurrentSearch).szSQL, AdvancedSearch, Graph))
                    End If
                    frmG(countG).MdiParent = frmMain
                    frmG(countG).ChartType = Graph.ChartType
                    frmG(countG).SetLabels(CurrentSearch)
                    frmG(countG).Show()
                End If

                '    frmMain.toolProgressBar.Value += 1

            Next
            frmMain.toolProgressBar.Value = 0
            SearchTime.Stop()
            frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."



        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmAnalysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveSetting(AppName, "Analysis", "OutcomeClusters", Me.chkOutcomeClusters.Checked)
        SaveSetting(AppName, "Analysis", "PathwayMaps", Me.chkPathwayMaps.Checked)
        SaveSetting(AppName, "Analysis", "PossessionGraphs", Me.chkPossessionGraphs.Checked)
        SaveSetting(AppName, "Analysis", "VideoPlaylist", Me.chkVideoPlaylist.Checked)
        SaveSetting(AppName, "Analysis", "PlayerMaps", Me.chkPlayerMaps.Checked)
        SaveSetting(AppName, "Analysis", "HeatMaps", Me.chkPosessionHeat.Checked)
        SaveSetting(AppName, "Analysis", "ScatterPlots", Me.chkScatterPlot.Checked)
    End Sub

    Private Sub frmAnalysis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AnalysisSettings.OutcomeClusters = CBool(GetSetting(AppName, "Analysis", "OutcomeClusters", "False"))
        AnalysisSettings.PathwayMaps = CBool(GetSetting(AppName, "Analysis", "PathwayMaps", "False"))
        AnalysisSettings.PossessionGraphs = CBool(GetSetting(AppName, "Analysis", "PossessionGraphs", "False"))
        AnalysisSettings.VideoPlaylist = CBool(GetSetting(AppName, "Analysis", "VideoPlaylist", "False"))
        AnalysisSettings.PlayerMaps = CBool(GetSetting(AppName, "Analysis", "PlayerMaps", "False"))
        AnalysisSettings.HeatMaps = CBool(GetSetting(AppName, "Analysis", "HeatMaps", "False"))
        AnalysisSettings.ScatterPlots = CBool(GetSetting(AppName, "Analysis", "ScatterPlots", "False"))
        Me.chkOutcomeClusters.Checked = AnalysisSettings.OutcomeClusters
        Me.chkPathwayMaps.Checked = AnalysisSettings.PathwayMaps
        Me.chkPossessionGraphs.Checked = AnalysisSettings.PossessionGraphs
        Me.chkVideoPlaylist.Checked = AnalysisSettings.VideoPlaylist
        Me.chkPlayerMaps.Checked = AnalysisSettings.PlayerMaps
        Me.chkPosessionHeat.Checked = AnalysisSettings.HeatMaps
        Me.chkScatterPlot.Checked = AnalysisSettings.ScatterPlots

        Me.cboTeamName.Items.Add("*")
        Me.cboTimeCriteria.Items.Add("*")
        'RefreshDescriptorList(OutcomeType.outAll)

    End Sub

    Private Sub frmAnalysis_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.chkOutcomeClusters.Checked = AnalysisSettings.OutcomeClusters
        Me.chkPathwayMaps.Checked = AnalysisSettings.PathwayMaps
        Me.chkPossessionGraphs.Checked = AnalysisSettings.PossessionGraphs
        Me.chkVideoPlaylist.Checked = AnalysisSettings.VideoPlaylist
        Me.chkScatterPlot.Checked = AnalysisSettings.ScatterPlots
    End Sub

    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        Dim i As Integer
        'ReDim SearchHistory(CurrentSearch).szDescriptors(Me.lstDescriptors.Items.Count - 1)
        For i = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, True)
            'SearchHistory(CurrentSearch).szDescriptors(0) = Me.lstDescriptors.Items.Item(i)
        Next


    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        Me.cmdSelectAll_Click(sender, e)
    End Sub

    Private Sub mnuSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectNone.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        Dim i As Integer
        For i = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, False)
        Next

        'Erase SearchHistory(CurrentSearch).szDescriptors


    End Sub

    Private Sub lstDescriptors_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDescriptors.Click

    End Sub

    Private Sub lstDescriptors_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstDescriptors.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.AnalysisMenu.Show(Me.lstDescriptors, e.X, e.Y)
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        'Set search history
        ReDim SearchHistory(0)
        CurrentSearch = 0
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub cboTeamName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTeamName.SelectedIndexChanged
        ReDim SearchHistory(CurrentSearch).szTeamName(0)
        SearchHistory(CurrentSearch).szTeamName(0) = Me.cboTeamName.Text
        If Me.cboTeamName.Text = "*" Then
            ReDim SearchHistory(CurrentSearch).szTeamName(Me.cboTeamName.Items.Count - 2)
            Dim n As Integer = 0
            For Each item As Object In Me.cboTeamName.Items
                If Not item = "*" Then
                    SearchHistory(CurrentSearch).szTeamName(n) = item
                    n += 1
                End If
            Next
        Else
            SearchHistory(CurrentSearch).szTeamName(0) = Me.cboTeamName.Text
        End If
    End Sub

    Public Sub cboTimeCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTimeCriteria.SelectedIndexChanged
        ReDim SearchHistory(CurrentSearch).szTimeCriterion(0)
        If Me.cboTimeCriteria.Text = "*" Then
            Erase SearchHistory(CurrentSearch).szTimeCriterion
        Else
            SearchHistory(CurrentSearch).szTimeCriterion(0) = Me.cboTimeCriteria.Text
        End If
    End Sub

    Public Sub cboOutcome_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOutcome.SelectedValueChanged
        ReDim SearchHistory(CurrentSearch).uOutcomes(0)
        Select Case Me.cboOutcome.Text
            Case Is = "Positive Outcomes"
                SearchHistory(CurrentSearch).uOutcomes(0) = OutcomeType.outPositive
            Case Is = "Negative Outcomes"
                SearchHistory(CurrentSearch).uOutcomes(0) = OutcomeType.outNegative
            Case Is = "Descriptors"
                SearchHistory(CurrentSearch).uOutcomes(0) = OutcomeType.outDescriptor
            Case Else
                SearchHistory(CurrentSearch).uOutcomes(0) = OutcomeType.outAll
        End Select

        If AllowOutcomesUpdate Then
            RefreshDescriptorList(SearchHistory(CurrentSearch).uOutcomes(0))
        End If

    End Sub

    Private Sub cmdSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectNone.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        Dim i As Integer
        'ReDim SearchHistory(CurrentSearch).szDescriptors(Me.lstDescriptors.Items.Count - 1)
        For i = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, False)
            'SearchHistory(CurrentSearch).szDescriptors(0) = Me.lstDescriptors.Items.Item(i)
        Next

    End Sub

    Private Sub cmdAdvancedSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdvancedSearch.Click
        If PathCount = 0 Then Exit Sub
        frmAdvancedSearch.MdiParent = frmMain
        frmAdvancedSearch.TopMost = True
        frmAdvancedSearch.Show()
        chkUseAdvancedSearch.Checked = True
        SetAdvancedSearchMode(True)

    End Sub

    Private Sub chkUseAdvancedSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseAdvancedSearch.CheckedChanged
        SetAdvancedSearchMode(chkUseAdvancedSearch.Checked)
    End Sub

    Private Sub SetAdvancedSearchMode(ByVal IsActive As Boolean)
        If PathCount = 0 Then Exit Sub

        AdvancedSearchIsActive = IsActive

        'Disable standard search functions.
        If IsActive Then
            cboOutcome.Text = "*"
            cboTeamName.Text = "*"
            cboTimeCriteria.Text = "*"
        End If
        Me.cboTimeCriteria.Enabled = Not IsActive
        Me.cboOutcome.Enabled = Not IsActive
        Me.cboTeamName.Enabled = Not IsActive
        Me.lstDescriptors.Enabled = Not IsActive

        frmAdvancedSearch.Enabled = IsActive

    End Sub

    Private Sub lstDescriptors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDescriptors.SelectedIndexChanged

    End Sub

    Private Sub lnkSetGraphs_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSetGraphs.LinkClicked
        frmSetGraphs.MdiParent = frmMain
        frmSetGraphs.Show()

    End Sub

    Private Sub chkPosessionHeat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPosessionHeat.CheckedChanged

    End Sub

    Private Sub chkPlayerMaps_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlayerMaps.CheckedChanged

    End Sub

    Private Sub grpReportCharts_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpReportCharts.Enter

    End Sub
End Class
