Imports System.Data.OleDb

Module modGraphFunctions

    Public Structure GraphType
        Dim Title As String          'Chart title    
        Dim xAxis As String          'Category Labels
        Dim xAxisLabels() As String
        Dim yAxis As String          'Values
        Dim yAxisScale As String    'Measurement scale
        Dim DataGroup As String      'Series - sub categories.
        Dim DataGroupLabels() As String
        Dim ChartType As MSChart20Lib.VtChChartType
        Dim Stacked As Boolean
        Dim GraphChecked As Boolean
        Dim nCriteria() As String
    End Structure

    Public CurrentGraphs() As GraphType
    Public KnownGraphColors() As Color = GetAllColors()

    Private Function GetAllColors() As Color()
        Dim colors([Enum].GetValues(GetType(KnownColor)).Length) As Color

        For Each c As KnownColor In [Enum].GetValues(GetType(KnownColor))
            colors(Array.IndexOf([Enum].GetValues(GetType(KnownColor)), c)) = Color.FromKnownColor(c)
        Next
        Return colors
    End Function

    Public Function GetGraphs() As GraphType()
        Dim GraphCount As Integer = GetSetting(AppName, "Settings", "GraphCount", 0)

        On Error Resume Next

        If GraphCount > 0 Then
            ReDim CurrentGraphs(GraphCount - 1)
            For n As Integer = 0 To GraphCount - 1
                CurrentGraphs(n).Title = GetSetting(AppName, "Settings", "GraphTitle" & CStr(n))
                CurrentGraphs(n).xAxis = GetSetting(AppName, "Settings", "GraphxAxis" & CStr(n))
                CurrentGraphs(n).yAxis = GetSetting(AppName, "Settings", "GraphyAxis" & CStr(n))
                CurrentGraphs(n).DataGroup = GetSetting(AppName, "Settings", "GraphDataGroup" & CStr(n))
                CurrentGraphs(n).ChartType = GetSetting(AppName, "Settings", "GraphChartType" & CStr(n))
                CurrentGraphs(n).Stacked = CBool(GetSetting(AppName, "Settings", "GraphStacked" & CStr(n)))
                CurrentGraphs(n).GraphChecked = CBool(GetSetting(AppName, "Settings", "GraphChecked" & CStr(n), True))

                Select Case CurrentGraphs(n).yAxis
                    Case Is = "Event Totals"
                        CurrentGraphs(n).yAxisScale = ""
                    Case Is = "Posession Time"
                        CurrentGraphs(n).yAxisScale = " (s)"
                    Case Is = "Ball Movements"
                        CurrentGraphs(n).yAxisScale = ""
                    Case Is = "Distance"
                        CurrentGraphs(n).yAxisScale = " (m)"
                    Case Is = "Ball Speed"
                        CurrentGraphs(n).yAxisScale = " (m/sec)"
                End Select
            Next

        Else
            CurrentGraphs = Nothing
        End If

        Return CurrentGraphs

    End Function

    Public Function GetCategories(ByVal FieldName As String, ByVal CurrentSearch As Integer, Optional ByVal DescriptorList As String = Nothing) As String()

        'NB: "DescriptorList" is nothing unless the fieldname is EventNames.  The point, is to restrict the range of
        'graph categories to only those eventnames that have at least one record.


        Dim szTemp As String = " FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE "
        Dim szSearchString As String = Nothing
        If FieldName = "GameID" Then
            szSearchString = "SELECT DISTINCT PathData." & FieldName & szTemp
            'ElseIf FieldName = "Time (Minutes)" Then
            '    szSearchString = "SELECT DISTINCT TimeCode" & szTemp
        Else
            szSearchString = "SELECT DISTINCT " & FieldName & szTemp
        End If

        Dim szReturn() As String = Nothing

        If AdvancedSearchIsActive Then
            'Add advanced search criteria.
            szSearchString = szSearchString & AdvancedSearch.DBSearchString
        Else
            'Add GameID criteria.
            szSearchString = szSearchString & " ("
            For Each Game As String In SearchHistory(CurrentSearch).szGameID
                szSearchString = szSearchString & "PathData.GameID = '" & Game & "'"
                If Array.IndexOf(SearchHistory(CurrentSearch).szGameID, Game) <> SearchHistory(CurrentSearch).szGameID.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                Else
                    szSearchString = szSearchString & ")"
                End If
            Next

            If Not SearchHistory(CurrentSearch).szTeamName Is Nothing Then
                'Add TeamName criteria.
                szSearchString = szSearchString & " AND ("
                For Each Team As String In SearchHistory(CurrentSearch).szTeamName
                    szSearchString = szSearchString & "TeamName = '" & Team & "'"
                    If Array.IndexOf(SearchHistory(CurrentSearch).szTeamName, Team) <> SearchHistory(CurrentSearch).szTeamName.Length - 1 Then
                        szSearchString = szSearchString & " OR "
                    Else
                        szSearchString = szSearchString & ")"
                    End If
                Next

            End If

        End If

        If Not DescriptorList = Nothing Then szSearchString = szSearchString & DescriptorList

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Do While dbReader.Read()
                If Not dbReader.IsDBNull(dbReader.GetOrdinal(FieldName)) Then
                    If szReturn Is Nothing Then
                        ReDim szReturn(0)
                    Else
                        ReDim Preserve szReturn(szReturn.Length)
                    End If
                    szReturn(szReturn.Length - 1) = dbReader.Item(FieldName)
                End If
            Loop
        End If


        Return szReturn
    End Function

    Public Function GetTeamColor(ByVal szTeamName As String, Optional ByVal GameIDs() As String = Nothing, Optional ByVal szGameID As String = Nothing) As Color

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        Dim GameString As String = "(GameID = '"

        If Not GameIDs Is Nothing Then
            For Each Game As String In GameIDs
                GameString = GameString & Game & "'"
                If Array.IndexOf(GameIDs, Game) < GameIDs.Length - 1 Then
                    GameString = GameString & " OR '"
                Else
                    GameString = GameString & ")"
                End If
            Next

        ElseIf Not szGameID Is Nothing Then
            'Only a single gameid
            GameString &= (szGameID & "')")
        Else
            Return Color.Green
        End If

        Dim strSQL As New OleDbCommand("SELECT DISTINCT TagColor FROM [PathData] WHERE " & GameString & _
                " AND TeamName = '" & szTeamName & "'", dbName)

        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        Try
            Do While dbReader.Read()
                GetTeamColor = Color.FromArgb(dbReader.Item("TagColor"))
                Exit Do
            Loop
        Catch ex As Exception
            dbName.Close()
            Return Color.Green
        End Try

        dbName.Close()

        If Not dbName Is Nothing Then dbName = Nothing

        Return GetTeamColor
    End Function

End Module
