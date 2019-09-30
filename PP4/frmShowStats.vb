Public Class frmShowStats
    Public Event Updated()


    Public Sub UpdateMe()
        Select Case UserPrefs.StatLayout
            Case StatsLayout.statByGame
                UpdateMeByGame()
            Case StatsLayout.statByTeam
                UpdateMeByTeam()
        End Select

        RaiseEvent Updated()

    End Sub

    Private Sub frmShowStats_Updated() Handles Me.Updated

    End Sub

    Private Sub frmShowStats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateMe()
    End Sub

    Public Sub UpdateMeByGame()
        If GamesCurrentlyOpen Is Nothing Then Exit Sub

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim CurrentGames(GamesCurrentlyOpen.Length - 1) As String

        For Each Game As GameProperties In GamesCurrentlyOpen
            CurrentGames(Array.IndexOf(GamesCurrentlyOpen, Game)) = Game.GameID
        Next

        Dim CurrentTeams() As String = GetItemsFromField(CurrentGames, "TeamName", "PathData")
        Dim CurrentOutcomes() As String = GetItemsFromField(CurrentGames, "EventName", "PathOutcomes")
        With gridStat
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Statistics by Game ID..."
            frmMain.toolProgressBar.Maximum = CurrentTeams.Length

            'Set column count
            If CurrentGames.Length = 1 Then
                .ColumnCount = 4
            Else
                .ColumnCount = CurrentGames.Length + 4
            End If
            .Rows.Clear()
            .Rows.Add("Team Statistics by Game ID")
            Dim nLastRow As Integer = .Rows.Count - 2

            'Add column headers with GameID
            For Each Game As String In CurrentGames
                .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentGames, Game)).Value = Game
            Next
            .Rows(nLastRow).Cells(2 + CurrentGames.Length).Value = "Totals:"

            For Each Team As String In CurrentTeams
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()
                .Rows.Add("")
                .Rows.Add(Team & ":", "")

                If Not UserPrefs.StatIncludedRegions Is Nothing Then
                    For Each Region As tRegion In UserPrefs.StatIncludedRegions
                        Dim rCount() As Integer = GetRegionEntriesByGameID(CurrentGames, Team, Region)

                        .Rows.Add(GetRegionString(Region))
                        .Rows.Add("-->", "Region Entries:") : nLastRow = .Rows.Count - 2
                        For Each Game As String In CurrentGames
                            .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentGames, Game)).Value = rCount(Array.IndexOf(CurrentGames, Game)).ToString
                        Next

                        'Add totals.
                        Dim rTotal As Integer = 0
                        For i As Integer = 3 To (2 + CurrentGames.Length)
                            rTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                        Next
                        .Rows(nLastRow).Cells(2 + CurrentGames.Length).Value = rTotal.ToString

                        .Rows.Add("")

                        If Not CurrentOutcomes Is Nothing Then
                            .Rows.Add("", "Outcomes:")
                            For Each Outcome As String In CurrentOutcomes
                                Dim nCount() As Integer = GetRegionOutcomesByGameID(CurrentGames, Team, Region, Outcome, Not UserPrefs.StatShowDescriptors)
                                If Array.Exists(nCount, AddressOf ValueGTZero) Then
                                    'Found at least one item in the array above zero
                                    .Rows.Add("", Outcome & ":") : nLastRow = .Rows.Count - 2

                                    For Each Game As String In CurrentGames
                                        If nCount(Array.IndexOf(CurrentGames, Game)) > 0 Then
                                            .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentGames, Game)).Value = nCount(Array.IndexOf(CurrentGames, Game)).ToString
                                        End If
                                    Next

                                    'Add totals.
                                    Dim nTotal As Integer = 0
                                    For i As Integer = 3 To (2 + CurrentGames.Length)
                                        nTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                                    Next
                                    .Rows(nLastRow).Cells(2 + CurrentGames.Length).Value = nTotal.ToString

                                End If
                            Next
                        End If
                    Next
                End If

                .Rows.Add("")

                If UserPrefs.StatShowTotals Then

                    If Not CurrentOutcomes Is Nothing Then
                        .Rows.Add("All Regions:")
                        .Rows.Add("-->", "Totals:")
                        For Each Outcome As String In CurrentOutcomes
                            Dim nCount() As Integer = GetOutcomeTotalsByGameID(CurrentGames, Team, Outcome, Not UserPrefs.StatShowDescriptors)
                            If Array.Exists(nCount, AddressOf ValueGTZero) Then
                                'Found at least one item in the array above zero
                                .Rows.Add("", Outcome & ":") : nLastRow = .Rows.Count - 2

                                For Each Game As String In CurrentGames
                                    If nCount(Array.IndexOf(CurrentGames, Game)) > 0 Then
                                        .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentGames, Game)).Value = nCount(Array.IndexOf(CurrentGames, Game)).ToString
                                    End If
                                Next

                                'Add totals.
                                Dim nTotal As Integer = 0
                                For i As Integer = 3 To (2 + CurrentGames.Length)
                                    nTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                                Next
                                .Rows(nLastRow).Cells(2 + CurrentGames.Length).Value = nTotal.ToString

                            End If
                        Next
                    End If
                End If

            Next

        End With

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Me.mnuShowByID.Checked = True
        Me.mnuShowByTeam.Checked = False

    End Sub

    Public Sub UpdateMeByTeam()
        If GamesCurrentlyOpen Is Nothing Then Exit Sub

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim CurrentGames(GamesCurrentlyOpen.Length - 1) As String

        For Each Game As GameProperties In GamesCurrentlyOpen
            CurrentGames(Array.IndexOf(GamesCurrentlyOpen, Game)) = Game.GameID
        Next

        Dim CurrentTeams() As String = GetItemsFromField(CurrentGames, "TeamName", "PathData")
        Dim CurrentOutcomes() As String = GetItemsFromField(CurrentGames, "EventName", "PathOutcomes")
        With gridStat
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Statistics by Team..."
            frmMain.toolProgressBar.Maximum = CurrentTeams.Length + 1

            'Set column count
            If CurrentTeams.Length = 1 Then
                .ColumnCount = 4
            Else
                .ColumnCount = CurrentTeams.Length + 4
            End If
            .Rows.Clear()
            .Rows.Add("Team Statistics by Team Name")
            Dim nLastRow As Integer = .Rows.Count - 2

            'Add column headers with Team Name
            For Each Team As String In CurrentTeams
                .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentTeams, Team)).Value = Team
            Next
            .Rows(nLastRow).Cells(2 + CurrentTeams.Length).Value = "Totals:"

            For Each Game As String In CurrentGames
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()
                .Rows.Add("")
                .Rows.Add(Game & ":", "")

                If Not UserPrefs.StatIncludedRegions Is Nothing Then
                    For Each Region As tRegion In UserPrefs.StatIncludedRegions
                        Dim rCount() As Integer = GetRegionEntriesByTeam(CurrentTeams, Game, Region)

                        .Rows.Add(GetRegionString(Region))
                        .Rows.Add("-->", "Region Entries:") : nLastRow = .Rows.Count - 2
                        For Each Team As String In CurrentTeams
                            .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentTeams, Team)).Value = rCount(Array.IndexOf(CurrentTeams, Team)).ToString
                        Next

                        'Add totals.
                        Dim rTotal As Integer = 0
                        For i As Integer = 3 To (2 + CurrentTeams.Length)
                            rTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                        Next
                        .Rows(nLastRow).Cells(2 + CurrentTeams.Length).Value = rTotal.ToString

                        .Rows.Add("")

                        If Not CurrentOutcomes Is Nothing Then
                            .Rows.Add("", "Outcomes:")
                            For Each Outcome As String In CurrentOutcomes
                                Dim nCount() As Integer = GetRegionOutcomesByTeam(CurrentTeams, Game, Region, Outcome, Not UserPrefs.StatShowDescriptors)
                                If Array.Exists(nCount, AddressOf ValueGTZero) Then
                                    'Found at least one item in the array above zero
                                    .Rows.Add("", Outcome & ":") : nLastRow = .Rows.Count - 2

                                    For Each Team As String In CurrentTeams
                                        If nCount(Array.IndexOf(CurrentTeams, Team)) > 0 Then
                                            .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentTeams, Team)).Value = nCount(Array.IndexOf(CurrentTeams, Team)).ToString
                                        End If
                                    Next

                                    'Add totals.
                                    Dim nTotal As Integer = 0
                                    For i As Integer = 3 To (2 + CurrentTeams.Length)
                                        nTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                                    Next
                                    .Rows(nLastRow).Cells(2 + CurrentTeams.Length).Value = nTotal.ToString

                                End If
                            Next
                        End If
                    Next
                End If

                .Rows.Add("")

                If UserPrefs.StatShowTotals Then

                    If Not CurrentOutcomes Is Nothing Then
                        .Rows.Add("All Regions:")
                        .Rows.Add("-->", "Totals:")
                        For Each Outcome As String In CurrentOutcomes
                            Dim nCount() As Integer = GetOutcomeTotalsByTeam(CurrentTeams, Game, Outcome, Not UserPrefs.StatShowDescriptors)
                            If Array.Exists(nCount, AddressOf ValueGTZero) Then
                                'Found at least one item in the array above zero
                                .Rows.Add("", Outcome & ":") : nLastRow = .Rows.Count - 2

                                For Each Team As String In CurrentTeams
                                    If nCount(Array.IndexOf(CurrentTeams, Team)) > 0 Then
                                        .Rows(nLastRow).Cells(2 + Array.IndexOf(CurrentTeams, Team)).Value = nCount(Array.IndexOf(CurrentTeams, Team)).ToString
                                    End If
                                Next

                                'Add totals.
                                Dim nTotal As Integer = 0
                                For i As Integer = 3 To (2 + CurrentTeams.Length)
                                    nTotal += CInt(.Rows(nLastRow).Cells(i - 1).Value)
                                Next
                                .Rows(nLastRow).Cells(2 + CurrentTeams.Length).Value = nTotal.ToString

                            End If
                        Next
                    End If
                End If
            Next
        End With

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Me.mnuShowByID.Checked = False
        Me.mnuShowByTeam.Checked = True

    End Sub

    Public Function ExportMe2Excel() As Boolean
        Dim dlgSaveEDL As New SaveFileDialog
        dlgSaveEDL.Title = "Export current statistics to Excel..."
        dlgSaveEDL.FileName = Me.Text & ".xls"
        dlgSaveEDL.Filter = "Excel Spreadsheet Files|*.xls"
        dlgSaveEDL.DefaultExt = "*.xls"
        Dim res As DialogResult = dlgSaveEDL.ShowDialog()
        If Not res = Windows.Forms.DialogResult.Cancel Then
            Dim szFile As String = dlgSaveEDL.FileName
            modLoadSaveGame.SaveStatsAsExcel(szFile, Me.gridStat.Rows)
        End If



    End Function

    Private Shared Function ValueGTZero(ByVal p As Integer) As Boolean
        If p > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub mnuShowByID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowByID.Click
        UserPrefs.StatLayout = StatsLayout.statByGame
        UpdateMe()
    End Sub

    Private Sub mnuShowByTeam_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuShowByTeam.Click
        UserPrefs.StatLayout = StatsLayout.statByTeam
        UpdateMe()
    End Sub

    Private Sub gridStat_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridStat.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuStatsDropDown.Show(Me, e.Location)
        End If
    End Sub

    Private Sub mnuExportStats2Excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExportStats2Excel.Click
        Me.ExportMe2Excel()

    End Sub
End Class