Imports System.Windows.Forms

Public Class frmDMXTabs

    Dim MyMatrix(,) As MatrixItem
    Dim MyItemSet() As ItemSet
    Dim expectedConf As Single



    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DMGrid.CellContentClick

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

    Public Sub cmdRefreshDataSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshDataSet.Click
        If Not PathCount > 0 Then Exit Sub

        Dim DescriptorItemsList() As String = Nothing
        Dim SearchParameters As String = ""

        Dim szTableName As String = "PathData"

        Dim CurrentGameIDs() As String
        If Not Me.cboGameID.Text = "*" Then
            SearchParameters &= " AND PathData.GameID = '" & Me.cboGameID.Text & "'"
            ReDim CurrentGameIDs(0)
            CurrentGameIDs(0) = Me.cboGameID.Text

        Else
            ReDim CurrentGameIDs(GamesCurrentlyOpen.Length - 1)
            For Each game As GameProperties In GamesCurrentlyOpen
                CurrentGameIDs(Array.IndexOf(GamesCurrentlyOpen, game)) = game.GameID
            Next
        End If

        'If selected, then add specific paramters to the search string
        If Not Me.cboTimeCriteria.Text = "*" Then SearchParameters &= " AND TimeCriteria = '" & Me.cboTimeCriteria.Text & "'"

        If Not Me.cboTeamName.Text = "*" Then
            If Me.chkExlcudeTeam.CheckState = CheckState.Checked Then
                'Exclude all selected teams/players
                SearchParameters &= " AND TeamName <> '" & Me.cboTeamName.Text & "'"
            Else
                SearchParameters &= " AND TeamName = '" & Me.cboTeamName.Text & "'"
            End If
        End If

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
        If Me.cboDM_Yaxis.Text = "Outcome Type" Or Me.cboDM_Yaxis.Text = "Event Name" Then szTableName = "PathOutcomes" Else szTableName = "PathData"
        Dim ItemsY As String() = GetItemsFromField(CurrentGameIDs, TranslateField(cboDM_Yaxis.Text), szTableName, SearchParameters)

        If ItemsX Is Nothing Or ItemsY Is Nothing Then
            MsgBox("No valid data was found for one of the axis data groups.  The data table was not generated.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With Me.DMGrid
            'set number of x/y-axis items
            .ColumnCount = ItemsX.Length + 1
            .RowCount = ItemsY.Length
            .Visible = False

            For c As Integer = 0 To ItemsX.Length
                .Columns(c).Visible = True
            Next
            .Columns(ItemsX.Length).Visible = False

            Application.DoEvents()

            If Me.rdoPositionSets.Checked = False Then
                For Each item As String In ItemsX
                    If Me.cboDM_Xaxis.Text = "Outcome Type" Then
                        .Columns.Item(Array.IndexOf(ItemsX, item)).HeaderCell.Value = GetOutcomeString(CInt(item))
                    ElseIf Me.cboDM_Xaxis.Text = "Region" Then
                        .Columns.Item(Array.IndexOf(ItemsX, item)).HeaderCell.Value = GetRegionString(CInt(item))
                    Else
                        .Columns.Item(Array.IndexOf(ItemsX, item)).HeaderCell.Value = item
                    End If
                Next
                For Each item As String In ItemsY
                    If Me.cboDM_Yaxis.Text = "Outcome Type" Then
                        .Rows.Item(Array.IndexOf(ItemsY, item)).HeaderCell.Value = GetOutcomeString(CInt(item))
                    ElseIf Me.cboDM_Yaxis.Text = "Region" Then
                        .Rows.Item(Array.IndexOf(ItemsY, item)).HeaderCell.Value = GetRegionString(CInt(item))
                    Else
                        .Rows.Item(Array.IndexOf(ItemsY, item)).HeaderCell.Value = item
                    End If
                Next
            End If

            If Me.rdoFrequency.Checked Then
                MyMatrix = GetXTabsFrequency2(CurrentGameIDs, Me.cboDM_Xaxis.Text, ItemsX, Me.cboDM_Yaxis.Text, ItemsY, , SearchParameters)

                'Show frequencies
                For x As Integer = 0 To MyMatrix.GetUpperBound(0)
                    For y As Integer = 0 To MyMatrix.GetUpperBound(1)
                        .Item(x, y).Value = MyMatrix(x, y).Value.ToString
                        .Item(MyMatrix.GetUpperBound(0) + 1, y).Value = y
                    Next
                Next

            ElseIf Me.rdoPositionSets.Checked Then
                'set search criteria
                Dim cSearch As SearchCriteria = Nothing
                cSearch.szGameID = CurrentGameIDs

                'set team name
                If Me.cboTeamName.Text <> "*" Then
                    If Me.chkExlcudeTeam.CheckState = CheckState.Unchecked Or Me.cboTeamName.Items.Count < 2 Then
                        ReDim cSearch.szTeamName(0)
                        cSearch.szTeamName(0) = Me.cboTeamName.Text
                        Me.chkExlcudeTeam.CheckState = CheckState.Unchecked
                    Else
                        ReDim cSearch.szTeamName(Me.cboTeamName.Items.Count - 3)

                        Dim i As Integer = 0
                        For Each team As String In Me.cboTeamName.Items
                            If team <> Me.cboTeamName.Text And team <> "*" Then
                                cSearch.szTeamName(i) = team
                                i = i + 1
                            End If
                        Next
                     End If
                End If

                'set time criteria
                If Me.cboTimeCriteria.Text <> "*" Then
                    ReDim cSearch.szTimeCriterion(0)
                    cSearch.szTimeCriterion(0) = Me.cboTimeCriteria.Text
                End If

                'First check if all descriptors are checked:
                Dim boolAllChecked As Boolean = True
                For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
                    If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Unchecked Then
                        boolAllChecked = False
                        Exit For
                    End If
                Next

                Dim eventlist() As String = Nothing
                If Not boolAllChecked Then
                    For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
                        If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Checked Then
                            If eventlist Is Nothing Then
                                ReDim eventlist(0)
                            Else
                                ReDim Preserve eventlist(eventlist.Length)
                            End If
                            eventlist(eventlist.Length - 1) = Me.lstDescriptors.Items.Item(i)
                        End If
                    Next
                    If eventlist Is Nothing Then
                        MsgBox("No data has been selected for this analysis.  It will not procede.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                    cSearch.szDescriptors = eventlist
                Else
                    cSearch.szDescriptors = Nothing
                End If


                .ColumnCount = UserPrefs.clHorizontalQ
                .RowCount = UserPrefs.clVerticalQ

                'First some explanation of the logic:
                '1. we calculate the support (% of all plays that pass through a quadrant)
                '2. we calculate the conditional probability P(B|A)(confidence of A->B) where
                'A is the invasion of a given quadrant, and B is the probability that any such
                'play will also include each of the selected events and descriptors.
                '
                'If the user selects only the support, then the numerical quotents of the support
                'for the event selections will be shown.
                '
                'If the user selects %support, then the same values are reported as a percent of the 
                'number of all plays.  If all the events are selected, then this is really just the support
                'quotent for eacg quadrant.  If a selection of events is made, then the value would be the 
                'confidence of the rule that quadrantX => eventY; or P(eventY|quadrantX)
                '
                'Finally, if the user selects "Lift", the we return the ratio of the rule confidence
                'to the "expected confidence", which is really the independent probability of the event.
                'As such, lift = confidence(quadrantX => eventY) / support(eventY), where the denominator in the
                'ratio is the percent of all plays that include eventY, regardless of quadrant.

                'Dim Plays As New Microsoft.VisualBasic.Collection

                'Dim szSearchString = modSearchEngine.CompileSearchString(cSearch, AnalysisType.uPathwayMaps, Nothing)

                'Dim eventsSearch As New AdvancedSearchCriteria
                'eventsSearch.EventNameSet = eventlist
                'ReDim Preserve eventsSearch.EventNameBoolean(eventlist.Length - 1)
                'For Each eventname As String In eventlist
                'eventsSearch.EventNameBoolean(System.Array.IndexOf(eventlist, eventname)) = True
                'Next
                'AdvancedSearchIsActive = True
                'Plays = modSearchEngine.CompilePathwayMap(szSearchString, eventsSearch)
                'AdvancedSearchIsActive = False

                MyMatrix = GetAssociationRules(cSearch, Me.numSupport.Value, Me.cboUseOrigins.Checked)



                'Show all transactions
                Dim rowNumber As Integer = 0
                For col As Integer = 0 To UserPrefs.clHorizontalQ - 1
                    For row As Integer = 0 To UserPrefs.clVerticalQ - 1
                        If Me.chkShowQuadrants.CheckState = CheckState.Checked Then
                            'show raw quadrant probabilities: p(A)
                            .Item(col, rowNumber).Value = Format(MyMatrix(col, row).supportA / MyMatrix(col, row).n, "#0.000%")

                        Else
                            If MyMatrix(col, row).supportAUB = 0 Then
                                .Item(col, rowNumber).Value = "-"
                            Else
                                Dim confidence As Double = (MyMatrix(col, row).supportAUB / MyMatrix(col, row).supportA)

                                If Me.chkRuleProbability.CheckState = CheckState.Checked Then
                                    'show support probability: p(B|A)
                                    .Item(col, rowNumber).Value = Format(MyMatrix(col, row).supportAUB / MyMatrix(col, row).n, "#0.000%")

                                ElseIf Me.chkRuleConfidence.CheckState = CheckState.Checked Then
                                    'show rule confidence.
                                    ' == support(A U B)/support(A)
                                    .Item(col, rowNumber).Value = Format(confidence, "#0.000%")

                                ElseIf Me.chkRuleLift.CheckState = CheckState.Checked Then
                                    'show lift: confidence(quadrantX => eventY) / (support(eventY) / n)
                                    Dim pB As Double = MyMatrix(col, row).supportB / MyMatrix(col, row).n
                                    .Item(col, rowNumber).Value = Format(confidence / pB, "#0.000")

                                Else
                                    'otherwise, show numerical support value.
                                    .Item(col, rowNumber).Value = Format(MyMatrix(col, row).supportAUB, "#0")
                                End If
                            End If
                        End If
                        rowNumber = rowNumber + 1

                    Next
                    .Columns(col).HeaderCell.Value = col.ToString
                    .Columns(col).Visible = True
                    rowNumber = 0
                Next


            ElseIf Me.rdoInterestingness.Checked Then
                    MyMatrix = GetXTabsFrequency2(CurrentGameIDs, Me.cboDM_Xaxis.Text, ItemsX, Me.cboDM_Yaxis.Text, ItemsY, , SearchParameters, True)
                    If Not MyMatrix Is Nothing Then
                        Dim ConfidenceMatrix As Single(,) = CalculateConfidence(MyMatrix)

                        'Show all trans
                        For x As Integer = 0 To MyMatrix.GetUpperBound(0)
                            For y As Integer = 0 To MyMatrix.GetUpperBound(1) - 1
                                If MyMatrix(x, y).Value = 0 Then
                                    .Item(x, y).Value = "-"
                                Else
                                    .Item(x, y).Value = Format(MyMatrix(x, y).Value, "#0.0%") & ", " & Format(ConfidenceMatrix(x, y), "#0.0%")
                                End If
                                .Item(MyMatrix.GetUpperBound(0) + 1, y).Value = y
                            Next
                        Next

                    End If

            ElseIf Me.rdoAssociations.Checked Then
                    MyItemSet = GetXTabsItemSets2(CurrentGameIDs, Me.cboDM_Yaxis.Text, ItemsY, Me.numSupport.Value)
                    If MyItemSet Is Nothing Then Exit Sub

                    frmMain.toolProgressBar.Minimum = 0
                    frmMain.toolProgressBar.Value = 0
                    frmMain.toolProgressBar.Maximum = MyItemSet.GetUpperBound(0)

                    'Show frequencies
                    .ColumnCount = 2
                    .Columns.Item(0).HeaderCell.Value = "Support"
                    .Columns(0).Visible = True
                    .Columns(1).Visible = False
                    .RowCount = MyItemSet.Length

                    For y As Integer = MyItemSet.GetUpperBound(0) To 0 Step -1
                        frmMain.toolActionStatus.Text = "Finalizing Matrix... " & Format((MyItemSet.GetUpperBound(0) - y) / MyItemSet.GetUpperBound(0), "#0.0%") & " (Please wait)"
                        frmMain.toolProgressBar.Value = (MyItemSet.GetUpperBound(0) - y)

                        Application.DoEvents()

                        Dim szHead As String = Nothing
                        For Each item As String In MyItemSet(y).ItemName
                            szHead &= item
                            If Array.IndexOf(MyItemSet(y).ItemName, item) < MyItemSet(y).ItemName.Length - 1 Then szHead &= ", "
                        Next
                        .Rows.Item(y).HeaderCell.Value = "{" & szHead & "}"
                        .Item(0, y).Value = MyItemSet(y).ItemSetFrequency.ToString
                        .Item(1, y).Value = y
                    Next

            End If
            .Visible = True
        End With

    End Sub

    Private Sub rdoAssociations_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAssociations.CheckedChanged
        Me.numSupport.Enabled = Me.rdoAssociations.Checked
        Me.lblMinSupport.Enabled = Me.rdoAssociations.Checked
    End Sub

    Private Sub DMGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DMGrid.CellDoubleClick
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

        Tabs2VPL(e.ColumnIndex, DMGrid.Item(DMGrid.Columns.Count - 1, e.RowIndex).Value)

    End Sub

    Private Sub Tabs2VPL(ByVal col As Integer, ByVal row As Integer)
        If Not Me.rdoAssociations.Checked Then
            If MyMatrix(col, row).dbID Is Nothing Then Exit Sub
            If MyMatrix(col, row).dbID.Length = 0 Then Exit Sub

            If MyMatrix(col, row).dbID.Length > 100 Then
                MsgBox("This query is too large for this analysis mode.  Try selecting a smaller dataset, or using the normal analysis criteria window.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            'Create VPL from contents of selected cell.
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)

            SearchHistory(CurrentSearch).nSelectedIDs = MyMatrix(col, row).dbID
            Dim szTemp As String = SQL_VideoPlayList & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE "

            'Status update
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Generating Playlist Data..."
            frmMain.toolProgressBar.Maximum = MyMatrix(col, row).dbID.Length

            For Each id As Long In MyMatrix(col, row).dbID
                frmMain.toolProgressBar.Value = Array.IndexOf(MyMatrix(col, row).dbID, id)
                Application.DoEvents()

                If id > 0 Then
                    szTemp &= "PathData.ID = " & id.ToString
                End If
                If Array.IndexOf(MyMatrix(col, row).dbID, id) < MyMatrix(col, row).dbID.Length - 2 Then
                    szTemp &= " OR "
                End If
            Next
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Playlist Data Complete..."

            SearchHistory(CurrentSearch).szSQL = szTemp

            'Search for any existing playlists
            If countVPL > 0 Then

                If frmVPL(lastVPLFormUsed) Is Nothing Then GoTo RecoverIfNotVisible
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
        Else

            'Create VPL from contents of selected cell.
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)

            'Status update
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Generating Playlist Data..."
            frmMain.toolProgressBar.Maximum = MyItemSet(row).PlayNumber.GetUpperBound(0)

            Dim szTemp As String = SQL_VideoPlayList & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE "
            For id As Integer = 0 To MyItemSet(row).PlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = id
                Application.DoEvents()

                If id <= MyItemSet(row).PlayNumber.GetUpperBound(0) Then
                    szTemp &= "(PlayNumber = " & MyItemSet(row).PlayNumber(id).ToString & _
                        " AND TimeCriteria = '" & MyItemSet(row).TimeCriteria(id) & _
                        "' AND PathData.GameID = '" & MyItemSet(row).GameID(id) & "')"
                End If

                If id < MyItemSet(row).PlayNumber.GetUpperBound(0) Then
                    szTemp &= " OR "
                End If
            Next
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Playlist Data Complete..."

            SearchHistory(CurrentSearch).szSQL = szTemp

            'Search for any existing playlists
            If countVPL > 0 Then
                If frmVPL(lastVPLFormUsed) Is Nothing Then GoTo RecoverIfNotVisible2
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
                        GoTo RecoverIfNotVisible2
                    End If

                Else
                    GoTo RecoverIfNotVisible2
                End If
            Else
RecoverIfNotVisible2:
                countVPL = countVPL + 1
                ReDim Preserve frmVPL(countVPL)
                frmVPL(countVPL) = New frmVideoPlayList(countVPL)
                If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid, AdvancedSearch) > 0 Then frmVPL(countVPL).formDirty = True
                frmVPL(countVPL).MdiParent = frmMain
                frmVPL(countVPL).SetLabels(CurrentSearch)
                frmVPL(countVPL).Show()
            End If

        End If
    End Sub

    Private Sub Tabs2Pathways(ByVal col As Integer, ByVal row As Integer)
        If Not Me.rdoAssociations.Checked Then
            If MyMatrix(col, row).dbID Is Nothing Then Exit Sub
            If MyMatrix(col, row).dbID.Length = 0 Then Exit Sub

            If MyMatrix(col, row).dbID.Length > 100 Then
                MsgBox("This query is too large for this analysis mode.  Try selecting a smaller dataset, or using the normal analysis criteria window.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            'Create VPL from contents of selected cell.
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)

            'Status update
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Generating Playlist Data..."
            frmMain.toolProgressBar.Maximum = MyMatrix(col, row).dbID.Length

            SearchHistory(CurrentSearch).nSelectedIDs = MyMatrix(col, row).dbID

            Dim szTemp As String = SQL_ScatterPlot & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE "
            For Each id As Long In MyMatrix(col, row).dbID
                frmMain.toolProgressBar.Value = Array.IndexOf(MyMatrix(col, row).dbID, id)
                Application.DoEvents()

                If id > 0 Then
                    szTemp &= "PathData.ID = " & id.ToString
                End If
                If Array.IndexOf(MyMatrix(col, row).dbID, id) < MyMatrix(col, row).dbID.Length - 2 Then
                    szTemp &= " OR "
                End If
            Next
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Playlist Data Complete..."


            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = szTemp
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).ScatterPlotPoints = CompileScatterArray(SearchHistory(CurrentSearch).szSQL, AdvancedSearch)
            frmC(countC).ChartType = frmChart.Chart.ctScatterPlots
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()



        Else

            'Create VPL from contents of selected cell.
            CurrentSearch += 1
            ReDim Preserve SearchHistory(CurrentSearch)

            'Status update
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Generating Playlist Data..."
            frmMain.toolProgressBar.Maximum = MyItemSet(row).PlayNumber.GetUpperBound(0)

            Dim szTemp As String = SQL_TeamPathwayMap & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE "
            For id As Integer = 0 To MyItemSet(row).PlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = id
                Application.DoEvents()

                If id <= MyItemSet(row).PlayNumber.GetUpperBound(0) Then
                    szTemp &= "(PlayNumber = " & MyItemSet(row).PlayNumber(id).ToString & _
                        " AND TimeCriteria = '" & MyItemSet(row).TimeCriteria(id) & _
                        "' AND PathData.GameID = '" & MyItemSet(row).GameID(id) & "')"
                End If

                If id < MyItemSet(row).PlayNumber.GetUpperBound(0) Then
                    szTemp &= " OR "
                End If
            Next
            SearchHistory(CurrentSearch).szSQL = szTemp

            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Playlist Data Complete..."

            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            SearchHistory(CurrentSearch).szSQL = szTemp
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = CurrentSearch
            frmC(countC).AddPlays(CompilePathwayMap(SearchHistory(CurrentSearch).szSQL, AdvancedSearch))
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(CurrentSearch)
            frmC(countC).Show()


        End If
    End Sub

    Private Function ParseItemsFromHeader(ByVal szString As String) As String()
        'Strip "{}"
        Dim Items() As String = Nothing
        Dim wCount As Integer = 0
        Dim tWord As String = Nothing
        Dim BuildingWord As Boolean = False

        For Each c As Char In szString
            If c = "{" Or (c = " " And BuildingWord = False) Then
                'next char is the start of a word.
                BuildingWord = True
            ElseIf c = "," Or c = "}" Then
                'this char ends the word.
                ReDim Preserve Items(wCount)
                Items(wCount) = tWord
                tWord = Nothing
                BuildingWord = False
                wCount += 1
            Else
                If BuildingWord Then tWord &= c
            End If



        Next

        Return Items
    End Function

    Private Sub DMGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DMGrid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuTabsDropDown.Show(Me, e.X + Me.GroupBox2.Left, e.Y + Me.DMGrid.Top)
        End If

    End Sub


    Private Sub mnuClusterAddVPL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClusterAddVPL.Click
        If DMGrid.SelectedCells(0).RowIndex < 0 Or DMGrid.SelectedCells(0).ColumnIndex < 0 Then Exit Sub
        Tabs2VPL(DMGrid.SelectedCells(0).ColumnIndex, DMGrid.Item(DMGrid.Columns.Count - 1, DMGrid.SelectedCells(0).RowIndex).Value)
    End Sub

    Private Sub mnuClusterAddToPathway_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuClusterAddToPathway.Click
        If DMGrid.SelectedCells(0).RowIndex < 0 Or DMGrid.SelectedCells(0).ColumnIndex < 0 Then Exit Sub
        Tabs2Pathways(DMGrid.SelectedCells(0).ColumnIndex, DMGrid.Item(DMGrid.Columns.Count - 1, DMGrid.SelectedCells(0).RowIndex).Value)
    End Sub

    Private Sub rdoPositionSets_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoPositionSets.CheckedChanged
        Me.numSupport.Enabled = Me.rdoPositionSets.Checked
        Me.lblMinSupport.Enabled = Me.rdoPositionSets.Checked

        Me.cboDM_Xaxis.Enabled = Not Me.rdoPositionSets.Checked
        Me.cboDM_Yaxis.Enabled = Not Me.rdoPositionSets.Checked

        Me.chkRuleConfidence.Enabled = Me.rdoPositionSets.Checked
        Me.chkRuleProbability.Enabled = Me.rdoPositionSets.Checked
        Me.chkRuleLift.Enabled = Me.rdoPositionSets.Checked
        Me.chkShowQuadrants.Enabled = Me.rdoPositionSets.Checked

    End Sub

    Private Sub mnuExportCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportCSV.Click
        Dim dlgSaveEDL As New SaveFileDialog
        dlgSaveEDL.Title = "Export itemset to CSV..."
        dlgSaveEDL.FileName = "Itemset   Data.csv"
        dlgSaveEDL.Filter = "CSV Files|*.csv"
        dlgSaveEDL.DefaultExt = "*.csv"
        Dim res As DialogResult = dlgSaveEDL.ShowDialog()
        If Not res = Windows.Forms.DialogResult.Cancel Then
            Dim szFileName As String = dlgSaveEDL.FileName

            Dim app As Excel.Application
            app = New Excel.Application

            Dim wb As Excel.Workbook
            wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

            Dim ws As Excel.Worksheet
            ws = app.Worksheets.Add()

            With ws


                'Show all transactions
                'Dim rowNumber As Integer = 0
                For col As Integer = 0 To UserPrefs.clHorizontalQ - 1
                    For row As Integer = 0 To UserPrefs.clVerticalQ - 1
                        If Me.chkShowQuadrants.CheckState = CheckState.Checked Then
                            'show raw quadrant probabilities: p(A)
                            .Cells(row + 1, col + 2) = MyMatrix(col, row).supportA / MyMatrix(col, row).n

                        Else
                            If MyMatrix(col, row).supportAUB = 0 Then
                                .Cells(row + 1, col + 2) = -1
                            Else
                                Dim confidence As Double = (MyMatrix(col, row).supportAUB / MyMatrix(col, row).supportA)

                                If Me.chkRuleProbability.CheckState = CheckState.Checked Then
                                    'show support probability: p(B|A)
                                    .Cells(row + 1, col + 2) = MyMatrix(col, row).supportAUB / MyMatrix(col, row).n

                                ElseIf Me.chkRuleConfidence.CheckState = CheckState.Checked Then
                                    'show rule confidence.
                                    ' == support(A U B)/support(A)
                                    .Cells(row + 1, col + 2) = confidence

                                ElseIf Me.chkRuleLift.CheckState = CheckState.Checked Then
                                    'show lift: confidence(quadrantX => eventY) / (support(eventY) / n)
                                    Dim pB As Double = MyMatrix(col, row).supportB / MyMatrix(col, row).n
                                    .Cells(row + 1, col + 2) = confidence / pB

                                Else
                                    'otherwise, show numerical support value.
                                    .Cells(row + 1, col + 2) = Format(MyMatrix(col, row).supportAUB, "#0")
                                End If
                            End If
                        End If
                    Next
                Next




                'set headers

                'For row As Integer = 0 To UserPrefs.clVerticalQ - 1
                '    For col As Integer = 0 To UserPrefs.clHorizontalQ - 1
                '        If Me.chkSupportIsPercent.CheckState = CheckState.Checked Then
                '            .Cells(row + 1, col + 2) = MyMatrix(col, row).Value / MyMatrix(col, row).n

                '        ElseIf Me.chkRuleLift.CheckState = CheckState.Checked Then
                '            .Cells(row + 1, col + 2) = (MyMatrix(col, row).Value / MyMatrix(col, row).n) / (expectedConf / MyMatrix(col, row).n)

                '        Else
                '            .Cells(row + 1, col + 2) = MyMatrix(col, row).Value
                '        End If

                '    Next
                'Next

            End With

            For Each sheet As Excel.Worksheet In app.Worksheets
                If Not sheet.Name = "Itemset" Then
                    sheet.Delete()
                Else
                    sheet.Name = IO.Path.GetFileNameWithoutExtension(szFileName)
                End If
            Next

            Dim zFileFormat As Excel.XlFileFormat = Excel.XlFileFormat.xlTextMac
            Dim fname As String = IO.Path.GetFileNameWithoutExtension(szFileName)

            wb.SaveAs(szFileName, zFileFormat)
            app.ActiveWorkbook.Close(False)
            ws = Nothing
            wb = Nothing
            app.Quit()
            MsgBox("CSV Export complete...", MsgBoxStyle.Information, Application.ProductName)
        End If

    End Sub


 
    Private Sub chkShowLift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuleLift.CheckedChanged
        If Me.chkRuleLift.CheckState = CheckState.Checked Then
            Me.chkRuleConfidence.CheckState = CheckState.Unchecked
            Me.chkRuleProbability.CheckState = CheckState.Unchecked
            Me.chkShowQuadrants.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub chkRuleConfidence_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuleConfidence.CheckedChanged
        If Me.chkRuleConfidence.CheckState = CheckState.Checked Then
            Me.chkRuleLift.CheckState = CheckState.Unchecked
            Me.chkRuleProbability.CheckState = CheckState.Unchecked
            Me.chkShowQuadrants.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub chkRuleProbability_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuleProbability.CheckedChanged
        If Me.chkRuleProbability.CheckState = CheckState.Checked Then
            Me.chkRuleLift.CheckState = CheckState.Unchecked
            Me.chkRuleConfidence.CheckState = CheckState.Unchecked
            Me.chkShowQuadrants.CheckState = CheckState.Unchecked
        End If

    End Sub

    Private Sub chkShowQuadrants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowQuadrants.CheckedChanged
        If Me.chkShowQuadrants.CheckState = CheckState.Checked Then
            Me.chkRuleConfidence.CheckState = CheckState.Unchecked
            Me.chkRuleProbability.CheckState = CheckState.Unchecked
            Me.chkRuleLift.CheckState = CheckState.Unchecked
        End If

    End Sub


End Class
