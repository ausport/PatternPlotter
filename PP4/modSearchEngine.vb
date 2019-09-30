Imports System.Data.OleDb
Imports System.IO

Module modSearchEngine
    Public Const SQL_All As String = "SELECT * "
    Public Const SQL_VideoPlayList As String = "SELECT PathData.GameID, PlayNumber, Region, TimeCodeVideoStamp, TimeCodeVideoStampOutcome, OutcomeTime, Outcome, TeamName, EventName, TimeCriteria, PathID, VideoFile, VideoFile2, x, y "
    Public Const SQL_TeamPathwayMap As String = "SELECT PathData.GameID, PathData.ID, PlayNumber, TimeCriteria, X, Y, TimeCode, Status "
    Public Const SQL_PlayerPathwayMap As String = "SELECT PathData.GameID, PathData.ID, PlayNumber, TimeCriteria, X, Y, TimeCode, Status, EventName "
    Public Const SQL_Clusters As String = "SELECT PathData.GameID, PlayNumber, Region, OutcomeTime, Outcome, TeamName, EventName, TimeCriteria, PathID, VideoFile, VideoFile2, x, y "
    Public Const SQL_Graph As String = "SELECT PathData.GameID, PlayNumber, ID, Region, PathID, x, y, Status, TimeCode, TimeCriteria, EventName "
    Public Const SQL_HeatMap As String = "SELECT PathData.GameID, PathData.ID, PlayNumber, TimeCriteria, X, Y, TimeCode, Status "
    Public Const SQL_ScatterPlot As String = "SELECT PathData.GameID, PathData.ID, PlayNumber, TimeCriteria, OutcomeTime, Outcome, TeamName, PathID, x, y "

    Public Structure SearchCriteria
        Public szGameID() As String
        Public szDescriptors() As String
        Public uOutcomes() As OutcomeType
        Public szTeamName() As String
        Public szTimeCriterion() As String
        Public uRegion() As Region
        Public szSQL As String              'Holds compiled search string
        Public nSelectedIDs() As Long       'Holds DB PathID value of path start.
    End Structure

    Public Structure PathInfo
        Dim PlayNumber As Integer
        Dim ID() As Long
        Dim GameID As String
        Dim TimeCriteria As String
        Dim TeamName As String
        Dim StartTimeString As String
        Dim StopTimeString As String
        Dim MousePointer As PointF
    End Structure

    Public Structure ClusterInfo
        Dim PlayNumber() As Integer
        Dim ID() As Long
        Dim GameID() As String
        Dim TimeCriteria() As String
        Dim StartTimeString() As String
        Dim StopTimeString() As String
        Dim MousePointer() As PointF
        Dim TotalDistance As Integer
        Dim TotalTime As Double
    End Structure

    Public Structure ScatterInfo
        Dim PlayNumber As Integer
        Dim ID As Long
        Dim GameID As String
        Dim TimeCriteria As String
        Dim StartTimeString As String
        Dim StopTimeString As String
        Dim Location As PointF
        Dim TeamName As String
        Dim TeamColor As Color
        Dim ClusterID As Integer
    End Structure

    Public Structure GamePlayClass
        Dim Plays() As GamePlay.Instance
        Dim Captions() As GamePlay.CaptionBox
    End Structure

    Public Structure ItemSet
        Dim ItemName() As String
        Dim ItemSetFrequency As Integer
        Dim GameID() As String
        Dim TimeCriteria() As String
        Dim PlayNumber() As Integer
        Dim nIndexOrder() As Integer
    End Structure

    Public Structure Items
        Dim List() As String
        Dim GameID As String
        Dim TimeCriteria As String
        Dim PlayNumber As Integer
    End Structure

    Public Enum AnalysisType
        uVideoPlaylist = 0
        uOutcomeClusters = 1
        uPathwayMaps = 2
        uPosessionGraph = 3
        uPlayerMaps = 4
        uEventCountHeatMaps = 5
        uBallSpeedHeatMaps = 6
        uPossessionTimeHeatMaps = 7
        uScatterPlot = 8
        uCrossTabs = 9
    End Enum


    Public SearchHistory() As SearchCriteria
    Public CurrentSearch As Integer = 0    'Index of SearchHistory
    Public CurrentClusterInfo(,) As ClusterInfo = Nothing
    Private nValue As Integer = 0

    Public Sub iPhoneUpdate()


        'Check that path exists
        If Not System.IO.Directory.Exists(UserPrefs.xmlURL) Then
            MsgBox("The iPhone URL does not exist or is currently unavailable.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        Dim SQL As String

        'First, get team names.
        Dim MyTeams() As String = Nothing
        Dim n As Integer = 0

        If UserPrefs.iPhoneByTeams Then
            SQL = "SELECT DISTINCT TeamName FROM PathData WHERE GameID = '" & propsCurrentGame.GameID & "'"

            strSQL = New OleDbCommand(SQL, dbName)
            dbReader = strSQL.ExecuteReader()

            If dbReader.HasRows Then
                Do While dbReader.Read()
                    ReDim Preserve MyTeams(n)
                    MyTeams(n) = dbReader.Item("TeamName")
                    n += 1
                Loop
            Else
                Exit Sub
            End If
            strSQL = Nothing
            dbReader.Close()
        Else
            ReDim MyTeams(0)
            MyTeams(0) = "*"
        End If

        'New get time criterion
        Dim MyTimeCriteria() As String = Nothing
        n = 0

        If UserPrefs.iPhoneByTimes Then
            SQL = "SELECT DISTINCT TimeCriteria FROM PathData WHERE GameID = '" & propsCurrentGame.GameID & "'"

            strSQL = New OleDbCommand(SQL, dbName)
            dbReader = strSQL.ExecuteReader()

            If dbReader.HasRows Then
                Do While dbReader.Read()
                    ReDim Preserve MyTimeCriteria(n)
                    MyTimeCriteria(n) = dbReader.Item("TimeCriteria")
                    n += 1
                Loop
            Else
                Exit Sub
            End If

            strSQL = Nothing
            dbReader.Close()
        Else
            ReDim MyTimeCriteria(0)
            MyTimeCriteria(0) = "*"
        End If

        'Now, get a list of items to be included as event names.
        Dim MyEvents() As String = Nothing
        Dim MyEventChartType() As EventButton.ctlEventButton.AnalysisType = Nothing

        n = 0
        Dim aButton As EventButton.ctlEventButton
        For Each tag As Windows.Forms.Control In frmTags.Controls
            aButton = TryCast(tag, EventButton.ctlEventButton)
            If Not aButton Is Nothing Then
                If aButton.IsTransmit Then
                    ReDim Preserve MyEvents(n)
                    MyEvents(n) = aButton.Caption
                    ReDim Preserve MyEventChartType(n)
                    MyEventChartType(n) = aButton.iPhoneChart
                    n += 1
                End If
            End If
        Next

        If MyEvents Is Nothing Then
            MsgBox("Please ensure a Tags window is opened and at least one event is set to transmit to iPhone.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If


        'Now create scatter plots if selected.
        n = 0
        Dim xmlFiles() As String = Nothing

        'If UserPrefs.iPhoneScatter Then
        For Each time As String In MyTimeCriteria
            Dim szTimeTempString As String = time
            For Each team As String In MyTeams
                Dim szTeamTempString As String = team
                Dim thisColor As Color = GetTeamColor(team, , propsCurrentGame.GameID)
                If team = "*" Then thisColor = Color.Yellow
                For Each eventname As String In MyEvents

                    If MyEventChartType(Array.IndexOf(MyEvents, eventname)) = EventButton.ctlEventButton.AnalysisType.uScatterPlot Then
                        SQL = "SELECT X, Y FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & propsCurrentGame.GameID & "' AND " & _
                        "EventName = '" & eventname & "'"
                        If Not team = "*" Then SQL &= " AND TeamName = '" & team & "'" Else szTeamTempString = "All Teams"
                        If Not time = "*" Then SQL &= " AND TimeCriteria = '" & time & "'" Else szTimeTempString = "All Match"

                        strSQL = New OleDbCommand(SQL, dbName)
                        dbReader = strSQL.ExecuteReader()

                        If dbReader.HasRows Then
                            Dim fnum As Integer = FreeFile()
                            Dim szFileName As String = Format(Now, "yyMMddHHmmss") & n.ToString & ".xml"
                            szFileName = Trim(szFileName)
                            ReDim Preserve xmlFiles(n)
                            xmlFiles(n) = szFileName
                            szFileName = UserPrefs.xmlURL & szFileName
                            n += 1

                            FileOpen(fnum, szFileName, OpenMode.Output)
                            Print(fnum, "<chart_data>" & vbNewLine)
                            Print(fnum, "<chart_type>scatter</chart_type>" & vbNewLine)
                            Print(fnum, "<title>" & eventname & ": " & szTimeTempString & " - " & szTeamTempString & "</title>" & vbNewLine)
                            Print(fnum, "<team>" & szTeamTempString & "</team>" & vbNewLine)
                            Print(fnum, "<time>" & szTimeTempString & "</time>" & vbNewLine)
                            Print(fnum, "<event>" & eventname & "</event>" & vbNewLine)
                            Print(fnum, "<quadrants_horizontal>" & UserPrefs.clHorizontalQ.ToString & "</quadrants_horizontal>" & vbNewLine)
                            Print(fnum, "<quadrants_vertical>" & UserPrefs.clVerticalQ.ToString & "</quadrants_vertical>" & vbNewLine)
                            Print(fnum, "<points>" & vbNewLine)

                            Do While dbReader.Read()
                                Print(fnum, "   <point x ='" & dbReader.Item("X").ToString & _
                                    "' y='" & dbReader.Item("Y").ToString & _
                                    "' colorr='" & thisColor.R.ToString & _
                                    "' colorg='" & thisColor.G.ToString & _
                                    "' colorb='" & thisColor.B.ToString & _
                                    "' count='1" & _
                                    "' endpath='0" & _
                                    "'>EventName</point>" & vbNewLine)
                            Loop
                            Print(fnum, "</points>" & vbNewLine)
                            Print(fnum, "</chart_data>" & vbNewLine)
                            FileClose(fnum)

                        Else
                            Exit For
                        End If

                    ElseIf MyEventChartType(Array.IndexOf(MyEvents, eventname)) = EventButton.ctlEventButton.AnalysisType.uOutcomeClusters Then
                        SQL = "SELECT X, Y FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & propsCurrentGame.GameID & "' AND " & _
                        "EventName = '" & eventname & "'"
                        If Not team = "*" Then SQL &= " AND TeamName = '" & team & "'" Else szTeamTempString = "All Teams"
                        If Not time = "*" Then SQL &= " AND TimeCriteria = '" & time & "'" Else szTimeTempString = "All Match"

                        Dim ClusterArray(,) As Integer = modSearchEngine.CompileClusterArrays(SQL, Nothing)
                        If Not ClusterArray Is Nothing Then
                            Dim fnum As Integer = FreeFile()
                            Dim szFileName As String = Format(Now, "yyMMddHHmmss") & n.ToString & ".xml"
                            szFileName = Trim(szFileName)
                            ReDim Preserve xmlFiles(n)
                            xmlFiles(n) = szFileName
                            szFileName = UserPrefs.xmlURL & szFileName
                            n += 1

                            FileOpen(fnum, szFileName, OpenMode.Output)
                            Print(fnum, "<chart_data>" & vbNewLine)
                            Print(fnum, "<chart_type>cluster</chart_type>" & vbNewLine)
                            Print(fnum, "<title>" & eventname & ": " & szTimeTempString & " - " & szTeamTempString & "</title>" & vbNewLine)
                            Print(fnum, "<team>" & szTeamTempString & "</team>" & vbNewLine)
                            Print(fnum, "<time>" & szTimeTempString & "</time>" & vbNewLine)
                            Print(fnum, "<event>" & eventname & "</event>" & vbNewLine)
                            Print(fnum, "<quadrants_horizontal>" & UserPrefs.clHorizontalQ.ToString & "</quadrants_horizontal>" & vbNewLine)
                            Print(fnum, "<quadrants_vertical>" & UserPrefs.clVerticalQ.ToString & "</quadrants_vertical>" & vbNewLine)
                            Print(fnum, "<points>" & vbNewLine)

                            For x As Integer = ClusterArray.GetLowerBound(0) To ClusterArray.GetUpperBound(0)
                                For y As Integer = ClusterArray.GetLowerBound(1) To ClusterArray.GetUpperBound(1)
                                    Print(fnum, "   <point x ='" & x.ToString & _
                                    "' y='" & y.ToString & _
                                    "' colorr='0" & _
                                    "' colorg='0" & _
                                    "' colorb='0" & _
                                    "' count='" & ClusterArray(x, y).ToString & _
                                    "' endpath='0" & _
                                    "'>EventName</point>" & vbNewLine)
                                Next
                            Next
                            Print(fnum, "</points>" & vbNewLine)
                            Print(fnum, "</chart_data>" & vbNewLine)
                            FileClose(fnum)
                        End If


                    ElseIf MyEventChartType(Array.IndexOf(MyEvents, eventname)) = EventButton.ctlEventButton.AnalysisType.uPathwayMaps Then
                        If team = "*" Then szTeamTempString = "All Teams"
                        If time = "*" Then szTimeTempString = "All Match"

                        Dim fnum As Integer = FreeFile()
                        Dim szFileName As String = Format(Now, "yyMMddHHmmss") & n.ToString & ".xml"
                        szFileName = Trim(szFileName)
                        ReDim Preserve xmlFiles(n)
                        xmlFiles(n) = szFileName
                        szFileName = UserPrefs.xmlURL & szFileName
                        n += 1

                        FileOpen(fnum, szFileName, OpenMode.Output)
                        Print(fnum, "<chart_data>" & vbNewLine)
                        Print(fnum, "<chart_type>pathway</chart_type>" & vbNewLine)
                        Print(fnum, "<title>" & eventname & ": " & szTimeTempString & " - " & szTeamTempString & "</title>" & vbNewLine)
                        Print(fnum, "<team>" & szTeamTempString & "</team>" & vbNewLine)
                        Print(fnum, "<time>" & szTimeTempString & "</time>" & vbNewLine)
                        Print(fnum, "<event>" & eventname & "</event>" & vbNewLine)
                        Print(fnum, "<quadrants_horizontal>" & UserPrefs.clHorizontalQ.ToString & "</quadrants_horizontal>" & vbNewLine)
                        Print(fnum, "<quadrants_vertical>" & UserPrefs.clVerticalQ.ToString & "</quadrants_vertical>" & vbNewLine)
                        Print(fnum, "<points>" & vbNewLine)

                        Dim iStarted As Integer = 0
                        Dim outcomeMatch As Boolean = False
                        For Each dPoint As PlotPoints In GamePath

                            If dPoint.Status = PathStatus.psStart Then outcomeMatch = False

                            If dPoint.Status = PathStatus.psStart And dPoint.TeamName = team And (dPoint.TimeCriteria = time Or time = "*") Then
                                'this is the start of a possible play.
                                iStarted = Array.IndexOf(GamePath, dPoint)
                                outcomeMatch = True
                            End If

                            If dPoint.OutcomeCount > 0 And outcomeMatch Then
                                Dim isEnd As Integer = 0
                                Dim nColor As Color
                                For Each outcome As PathOutComes In dPoint.OutcomeProp

                                    If outcome.EventName = eventname Then
                                        'A match here is found..
                                        nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, iStarted, GamePath.Length)
                                        nColor = GetTeamColor(dPoint.TeamName, , propsCurrentGame.GameID)

                                        'Add pathways starting from iStarted.
                                        'To the end of the play
                                        'For i As Integer = iStarted To Array.IndexOf(GamePath, dPoint)
                                        For i As Integer = iStarted To (GamePath.GetUpperBound(0) - 1)
                                            'If i = Array.IndexOf(GamePath, dPoint) Then
                                            If GamePath(i + 1).Status = PathStatus.psStart Or i = GamePath.GetUpperBound(0) Then
                                                isEnd = 1

                                            ElseIf i = iStarted Then
                                                isEnd = -1
                                            Else
                                                isEnd = 0
                                            End If

                                            If Not GamePath(i).SpatialCorrection.X > 0 Then
                                                GamePath(i).SpatialCorrection.X = 1
                                                GamePath(i).SpatialCorrection.Y = 1
                                            End If

                                            Print(fnum, "   <point x ='" & (GamePath(i).Coordinates.X / GamePath(i).SpatialCorrection.X).ToString & _
                                             "' y='" & (GamePath(i).Coordinates.Y / GamePath(i).SpatialCorrection.Y).ToString & _
                                             "' colorr='" & nColor.R.ToString & _
                                             "' colorg='" & nColor.G.ToString & _
                                             "' colorb='" & nColor.B.ToString & _
                                             "' count='1" & _
                                             "' endpath='" & isEnd.ToString & _
                                             "'>Path</point>" & vbNewLine)

                                            If isEnd = 1 Then Exit For

                                        Next
                                        outcomeMatch = False
                                    End If
                                Next
                            End If
                        Next

                        Print(fnum, "</points>" & vbNewLine)
                        Print(fnum, "</chart_data>" & vbNewLine)
                        FileClose(fnum)

                    End If

                Next
            Next

            'If UserPrefs.iPhoneIsActive Then
            '    Dim fnum As Integer = FreeFile()
            '    Dim szFileName As String = Format(Now, "yyMMddHHmmss") & n.ToString & ".xml"
            '    szFileName = Trim(szFileName)
            '    ReDim Preserve xmlFiles(n)
            '    xmlFiles(n) = szFileName
            '    szFileName = UserPrefs.xmlURL & szFileName

            '    Dim szTeamTempString As String = "All Teams"
            '    MyTeams = GetTeamNamesFromGameID(propsCurrentGame.GameID)

            '    FileOpen(fnum, szFileName, OpenMode.Output)
            '    Print(fnum, "<chart_data>" & vbNewLine)
            '    Print(fnum, "<chart_type>totals</chart_type>" & vbNewLine)
            '    Print(fnum, "<title>Game Totals: " & szTimeTempString & "</title>" & vbNewLine)
            '    Print(fnum, "<team>" & szTeamTempString & "</team>" & vbNewLine)
            '    Print(fnum, "<time>" & szTimeTempString & "</time>" & vbNewLine)
            '    Print(fnum, "<event>Statistics</event>" & vbNewLine)
            '    Print(fnum, "<quadrants_horizontal>" & UserPrefs.clHorizontalQ.ToString & "</quadrants_horizontal>" & vbNewLine)
            '    Print(fnum, "<quadrants_vertical>" & UserPrefs.clVerticalQ.ToString & "</quadrants_vertical>" & vbNewLine)
            '    Print(fnum, "<points>" & vbNewLine)

            '    For Each team As String In MyTeams
            '        SQL = "SELECT DISTINCT EventName FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & propsCurrentGame.GameID & "'"
            '        Dim events() As String = GetEventNames(SQL)

            '    Next

            '    Print(fnum, "</points>" & vbNewLine)
            '    Print(fnum, "</chart_data>" & vbNewLine)
            '    FileClose(fnum)


            'End If

        Next


 

        'Write index.xml file.
        Dim fnum2 As Integer = FreeFile()
        If System.IO.File.Exists(UserPrefs.xmlURL & "index.xml") Then
            System.IO.File.Delete(UserPrefs.xmlURL & "index.xml")
        End If

        FileOpen(fnum2, UserPrefs.xmlURL & "index.xml", OpenMode.Output)
        Print(fnum2, "<charts>" & vbNewLine)
        For Each xmlFile As String In xmlFiles
            Print(fnum2, "<filename path='" & xmlFile & "'>xml</filename>" & vbNewLine)
        Next
        Print(fnum2, "</charts>" & vbNewLine)
        FileClose(fnum2)
        dbName.Close()

    End Sub

    Private Function GetGraphSQL(ByVal szDataType As String) As String
        'Return SQL prefix for graph data selections.
        Select Case szDataType
            Case Is = "Event Totals"
                Return "SELECT PathData.GameID, TimeCriteria, TeamName, Region, EventName "

            Case Is = "Posession Time"
                Return "SELECT PathData.GameID, TimeCriteria, TeamName, Region, TimeCode, EventName "

            Case Is = "Event Probability"
                Return "SELECT PathData.GameID, TimeCriteria, TeamName, Region, TimeCode, EventName "

            Case Is = "Ball Movements"
                Return "SELECT PathData.GameID, TimeCriteria, TeamName, Region, TimeCode, ID, Status, EventName "

            Case Is = "Distance"
                Return "SELECT PathData.GameID, TimeCriteria, TeamName, Region, x, y, EventName "

            Case Is = "Ball Speed"
                Return "SELECT PlayNumber, ID, TimeCode, x, y, EventName "

            Case Else
                Return "SELECT * "
        End Select
        Return Nothing
    End Function

    Public Function CompileSearchString(ByVal uSearchCriteria As SearchCriteria, ByVal uAnalysisType As AnalysisType, ByVal GraphInfo As GraphType, _
        Optional ByVal x1 As Single = Nothing, Optional ByVal x2 As Single = Nothing, _
        Optional ByVal y1 As Single = Nothing, Optional ByVal y2 As Single = Nothing) As String

        'NB: Return a search string
        'On Error GoTo errCatch
        Dim szTemp As String = ""
        Dim i As Integer = 0

        Select Case uAnalysisType
            Case Is = AnalysisType.uVideoPlaylist
                szTemp = SQL_VideoPlayList
            Case Is = AnalysisType.uPathwayMaps
                szTemp = SQL_TeamPathwayMap
            Case Is = AnalysisType.uOutcomeClusters
                szTemp = SQL_Clusters
            Case Is = AnalysisType.uPosessionGraph
                szTemp = GetGraphSQL(GraphInfo.yAxis)
            Case Is = AnalysisType.uPlayerMaps
                szTemp = SQL_PlayerPathwayMap
            Case AnalysisType.uEventCountHeatMaps, AnalysisType.uBallSpeedHeatMaps, AnalysisType.uPossessionTimeHeatMaps
                szTemp = SQL_HeatMap
            Case AnalysisType.uScatterPlot
                szTemp = SQL_ScatterPlot
            Case Else
                szTemp = SQL_All
        End Select

        szTemp = szTemp & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE"

        '*
        '*  If an advanced search is activated then use preconfigured SQL and exit function.
        '*
        If AdvancedSearchIsActive Then
            szTemp = szTemp & " " & AdvancedSearch.DBSearchString
        Else
            'Simple search - compile SQL including references to eventnames if not a pathway map.

            '* 
            '* Set GameIDs into search string...
            '*
            If Not uSearchCriteria.szGameID Is Nothing Then
                szTemp = szTemp & " ("
                For Each n As String In uSearchCriteria.szGameID
                    szTemp = szTemp & "PathData.GameID = '" & n & "'"
                    If i < uSearchCriteria.szGameID.GetUpperBound(0) Then
                        i = i + 1
                        szTemp = szTemp & " OR "
                    Else
                        i = 0
                        szTemp = szTemp & ")"
                    End If
                Next
            End If

            '* 
            '* Set TeamNames into search string...
            '*
            If Not uSearchCriteria.szTeamName Is Nothing Then
                If uSearchCriteria.szTeamName(0) <> "*" Then
                    szTemp = szTemp & " AND ("
                    For Each n As String In uSearchCriteria.szTeamName
                        szTemp = szTemp & "TeamName = '" & n & "'"
                        If i < uSearchCriteria.szTeamName.GetUpperBound(0) Then
                            i = i + 1
                            szTemp = szTemp & " OR "
                        Else
                            i = 0
                            szTemp = szTemp & ")"
                        End If
                    Next
                End If
            End If

            '* 
            '* Set TimeCriteria into search string...
            '*
            If Not uSearchCriteria.szTimeCriterion Is Nothing Then
                szTemp = szTemp & " AND ("
                For Each n As String In uSearchCriteria.szTimeCriterion
                    szTemp = szTemp & "TimeCriteria = '" & n & "'"
                    If i < uSearchCriteria.szTimeCriterion.GetUpperBound(0) Then
                        i = i + 1
                        szTemp = szTemp & " OR "
                    Else
                        i = 0
                        szTemp = szTemp & ")"
                    End If
                Next
            End If

            '* 
            '* Set Descriptors into search string...
            '*
            If Not uAnalysisType = AnalysisType.uPathwayMaps And Not uAnalysisType = AnalysisType.uPosessionGraph And Not uAnalysisType = AnalysisType.uPlayerMaps And Not uAnalysisType = AnalysisType.uBallSpeedHeatMaps And Not uAnalysisType = AnalysisType.uPossessionTimeHeatMaps And (uAnalysisType <> AnalysisType.uEventCountHeatMaps Or Not UserPrefs.boolShowAllBallMovementsInHeat) Then
                ' If Not uAnalysisType = AnalysisType.uPosessionGraph Then
                If Not uSearchCriteria.szDescriptors Is Nothing Then
                    If uSearchCriteria.szDescriptors.Length > 0 Then
                        If Not uSearchCriteria.szDescriptors(0) = "*" Then
                            'Only add descriptors to search string if the wildcard is not selected
                            szTemp = szTemp & " AND ("
                            For Each n As String In uSearchCriteria.szDescriptors
                                szTemp = szTemp & "EventName = '" & n & "'"
                                If i < uSearchCriteria.szDescriptors.GetUpperBound(0) Then
                                    i = i + 1
                                    szTemp = szTemp & " OR "
                                Else
                                    i = 0
                                    szTemp = szTemp & ")"
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If

        '* 
        '* Optional - Set spatial limits into search string...
        '*
        If Not x1 = Nothing Then    'Spatial limits have been set
            szTemp = szTemp & " AND (x >= " & x1 & " AND x < " & x2 & " AND y >= " & y1 & " AND y < " & y2 & ")"
        End If

        Return szTemp

        Exit Function
errCatch:
        MessageBox.Show("PP4 was unable to compile that search query for the following reason:" & vbCr & _
        Err.Description, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return Nothing
    End Function

    Public Function CompileGameTeamTimeStringFromSearchCriteria(ByVal uSearchCriteria As SearchCriteria, Optional ByVal Prefix As String = "") As String

        Dim szTemp As String = Nothing
        Dim i As Integer = 0

        If Not uSearchCriteria.szGameID Is Nothing Then
            szTemp = szTemp & " ("
            For Each n As String In uSearchCriteria.szGameID
                szTemp = szTemp & Prefix & "GameID = '" & n & "'"
                If i < uSearchCriteria.szGameID.GetUpperBound(0) Then
                    i = i + 1
                    szTemp = szTemp & " OR "
                Else
                    i = 0
                    szTemp = szTemp & ")"
                End If
            Next
        End If

        '* 
        '* Set TeamNames into search string...
        '*
        If Not uSearchCriteria.szTeamName Is Nothing Then
            If uSearchCriteria.szTeamName(0) <> "*" Then
                szTemp = szTemp & " AND ("
                For Each n As String In uSearchCriteria.szTeamName
                    szTemp = szTemp & "TeamName = '" & n & "'"
                    If i < uSearchCriteria.szTeamName.GetUpperBound(0) Then
                        i = i + 1
                        szTemp = szTemp & " OR "
                    Else
                        i = 0
                        szTemp = szTemp & ")"
                    End If
                Next
            End If
        End If

        '* 
        '* Set TimeCriteria into search string...
        '*
        If Not uSearchCriteria.szTimeCriterion Is Nothing Then
            szTemp = szTemp & " AND ("
            For Each n As String In uSearchCriteria.szTimeCriterion
                szTemp = szTemp & "TimeCriteria = '" & n & "'"
                If i < uSearchCriteria.szTimeCriterion.GetUpperBound(0) Then
                    i = i + 1
                    szTemp = szTemp & " OR "
                Else
                    i = 0
                    szTemp = szTemp & ")"
                End If
            Next
        End If

        Return szTemp
    End Function

    Public Function CompileEventProbability(ByVal uSearch As SearchCriteria, ByVal AdvSearch As AdvancedSearchCriteria, ByVal GraphInfo As GraphType) As Single(,)

        Dim szSearchString As String = Nothing
        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"
        Else
            szSearchString &= GetDescriptorListSQL()
        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        '    If nRecords = 0 Then Return Nothing

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        'Get number of plays in search
        Dim szPlayString As String = "SELECT DISTINCT PlayNumber, TimeCriteria, GameID FROM PathData WHERE" & CompileGameTeamTimeStringFromSearchCriteria(uSearch)
        Dim szEvString As String = "SELECT DISTINCT PlayNumber, TimeCriteria, PathData.GameID FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE" & _
            CompileGameTeamTimeStringFromSearchCriteria(uSearch, "PathData.")

        Dim nPlays(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Integer
        Dim rArray(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Single

        If GraphInfo.xAxis = "Time (Minutes)" Then
            If GraphInfo.DataGroup = "TimeCriteria" Then
                For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                    For tStart As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                        nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & (tStart * 60) & " AND " & ((tStart + 1) * 60).ToString & ") AND TimeCriteria = '" & _
                            GraphInfo.DataGroupLabels(i) & "'")

                        rArray(tStart, i) = GetEventCount(szEvString & " AND (TimeCode BETWEEN " & (tStart * 60) & " AND " & ((tStart + 1) * 60).ToString & ") AND (TimeCriteria = '" & _
                          GraphInfo.DataGroupLabels(i) & "')" & szSearchString)
                    Next
                Next

            Else
                'First, get the relevant time criteria.
                Dim szTimeCriterion() As String = GetTimeCriterionFromGameID(uSearch.szGameID)
                Dim nTimeDurations(szTimeCriterion.Length - 1) As Double
                Dim nLastDuration As Double = 0

                For Each szTC As String In szTimeCriterion
                    nTimeDurations(Array.IndexOf(szTimeCriterion, szTC)) = GetHighestTimeCodeByTimeCriteria(uSearch.szGameID, szTC) + nLastDuration
                    nLastDuration = nTimeDurations(Array.IndexOf(szTimeCriterion, szTC))

                Next

                For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                    For tStart As Integer = 0 To GraphInfo.xAxisLabels.Length - 1

                        Dim nOffset As Double = 0
                        Dim szTimeSpec As String = Nothing
                        For Each nTime As Double In nTimeDurations
                            If tStart < nTime Then
                                szTimeSpec &= " AND TimeCriteria = '" & szTimeCriterion(Array.IndexOf(nTimeDurations, nTime)) & "'"
                                If Array.IndexOf(nTimeDurations, nTime) > 0 Then
                                    nOffset = nTimeDurations(Array.IndexOf(nTimeDurations, nTime) - 1)

                                End If
                                Exit For
                            End If
                        Next

                        If GraphInfo.DataGroup = "EventName" Then
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ")" & szTimeSpec)

                            rArray(tStart, i) = GetEventCount(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szTimeSpec)
                        ElseIf GraphInfo.DataGroup = "Region" Or GraphInfo.DataGroup = "Outcome" Then
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                                GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i) & szTimeSpec)

                            rArray(tStart, i) = GetEventCount(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i) & szSearchString & szTimeSpec)
                        Else
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                                GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szTimeSpec)

                            rArray(tStart, i) = GetEventCount(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szSearchString & szTimeSpec)
                        End If
                    Next
                Next
            End If

            'Convert to percent of plays per min
            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If nPlays(k, i) > 0 Then
                        rArray(k, i) = rArray(k, i) / CSng(nPlays(k, i))
                    End If
                Next
            Next

            'Now apply moving average.
            Dim sum(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Single

            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If k >= 6 Then
                        'Get sum of last six
                        For n As Integer = k - 6 To k
                            sum(k, i) += rArray(n, i)
                        Next
                        sum(k, i) = sum(k, i) / 6
                    Else
                        For n As Integer = 0 To k
                            sum(k, i) += rArray(n, i)
                        Next
                        sum(k, i) = sum(k, i) / (k + 1)
                    End If
                Next
            Next
            rArray = sum

        Else
            'Get number of plays in search
            Erase rArray
            ReDim rArray(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1)
            Dim xSearch, iSearch As String

            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                If GraphInfo.DataGroup = "Region" Or GraphInfo.DataGroupLabels(i) = "Outcome" Then
                    iSearch = " AND " & GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i)
                ElseIf GraphInfo.DataGroup = "GameID" Then
                    iSearch = " AND PathData.GameID = '" & GraphInfo.DataGroupLabels(i) & "'"
                Else
                    iSearch = " AND " & GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'"
                End If
                For x As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If GraphInfo.xAxis = "Region" Or GraphInfo.xAxis = "Outcome" Then
                        xSearch = " AND " & GraphInfo.xAxis & " = " & GraphInfo.xAxisLabels(x)
                    ElseIf GraphInfo.xAxis = "GameID" Then
                        xSearch = " AND PathData.GameID = '" & GraphInfo.xAxisLabels(x) & "'"
                    Else
                        xSearch = " AND " & GraphInfo.xAxis & " = '" & GraphInfo.xAxisLabels(x) & "'"
                    End If

                    If GraphInfo.DataGroup = "EventName" Then
                        nPlays(x, i) = GetEventCount(szEvString & xSearch)
                    ElseIf GraphInfo.xAxis = "EventName" Then
                        nPlays(x, i) = GetEventCount(szEvString & iSearch)
                    Else
                        nPlays(x, i) = GetEventCount(szEvString & xSearch & iSearch)
                    End If

                    If GraphInfo.DataGroup = "EventName" Or GraphInfo.xAxis = "EventName" Then
                        rArray(x, i) = GetEventCount(szEvString & xSearch & iSearch)
                    Else
                        rArray(x, i) = GetEventCount(szEvString & xSearch & iSearch & szSearchString)
                    End If
                Next
            Next

            'Convert to percent of plays per min
            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If nPlays(k, i) > 0 Then
                        rArray(k, i) = rArray(k, i) / CSng(nPlays(k, i))
                    End If
                Next
            Next


        End If



        SearchTime.Stop()
        ' frmMain.toolProgressBar.Value = 1
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return rArray
    End Function

    Public Function CompileMovingPossession(ByVal uSearch As SearchCriteria, ByVal AdvSearch As AdvancedSearchCriteria, ByVal GraphInfo As GraphType) As Single(,)

        Dim szSearchString As String = Nothing
        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"
        Else
            szSearchString &= GetDescriptorListSQL()
        End If

        'Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        '    If nRecords = 0 Then Return Nothing

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        'Get number of plays in search
        Dim szPlayString As String = "SELECT DISTINCT PlayNumber, TimeCriteria, GameID FROM PathData WHERE" & CompileGameTeamTimeStringFromSearchCriteria(uSearch)
        Dim szEvString As String = "SELECT DISTINCT PlayNumber, TimeCriteria, PathData.GameID FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE" & _
            CompileGameTeamTimeStringFromSearchCriteria(uSearch, "PathData.")

        Dim nPlays(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Integer
        Dim rArray(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Single

        If GraphInfo.xAxis = "Time (Minutes)" Then
            If GraphInfo.DataGroup = "TimeCriteria" Then
                For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                    For tStart As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                        nPlays(tStart, i) = GetPlayDuration(szPlayString & " AND (TimeCode BETWEEN " & (tStart * 60) & " AND " & ((tStart + 1) * 60).ToString & ") AND TimeCriteria = '" & _
                            GraphInfo.DataGroupLabels(i) & "'")

                        rArray(tStart, i) = GetPlayDuration(szEvString & " AND (TimeCode BETWEEN " & (tStart * 60) & " AND " & ((tStart + 1) * 60).ToString & ") AND (TimeCriteria = '" & _
                          GraphInfo.DataGroupLabels(i) & "')" & szSearchString)
                    Next
                Next

            Else
                'First, get the relevant time criteria.
                Dim szTimeCriterion() As String = GetTimeCriterionFromGameID(uSearch.szGameID)
                Dim nTimeDurations(szTimeCriterion.Length - 1) As Double
                Dim nLastDuration As Double = 0

                For Each szTC As String In szTimeCriterion
                    nTimeDurations(Array.IndexOf(szTimeCriterion, szTC)) = GetHighestTimeCodeByTimeCriteria(uSearch.szGameID, szTC) + nLastDuration
                    nLastDuration = nTimeDurations(Array.IndexOf(szTimeCriterion, szTC))

                Next

                For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                    For tStart As Integer = 0 To GraphInfo.xAxisLabels.Length - 1

                        Dim nOffset As Double = 0
                        Dim szTimeSpec As String = Nothing
                        For Each nTime As Double In nTimeDurations
                            If tStart < nTime Then
                                szTimeSpec &= " AND TimeCriteria = '" & szTimeCriterion(Array.IndexOf(nTimeDurations, nTime)) & "'"
                                If Array.IndexOf(nTimeDurations, nTime) > 0 Then
                                    nOffset = nTimeDurations(Array.IndexOf(nTimeDurations, nTime) - 1)

                                End If
                                Exit For
                            End If
                        Next

                        If GraphInfo.DataGroup = "EventName" Then
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ")" & szTimeSpec)

                            rArray(tStart, i) = GetPlayDuration(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szTimeSpec)
                        ElseIf GraphInfo.DataGroup = "Region" Or GraphInfo.DataGroup = "Outcome" Then
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                                GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i) & szTimeSpec)

                            rArray(tStart, i) = GetPlayDuration(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i) & szSearchString & szTimeSpec)
                        Else
                            nPlays(tStart, i) = GetEventCount(szPlayString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                                GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szTimeSpec)

                            rArray(tStart, i) = GetPlayDuration(szEvString & " AND (TimeCode BETWEEN " & ((tStart - nOffset) * 60) & " AND " & (((tStart - nOffset) + 1) * 60).ToString & ") AND " & _
                              GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'" & szSearchString & szTimeSpec)
                        End If
                    Next
                Next
            End If

            'Convert to percent of plays per min
            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If nPlays(k, i) > 0 Then
                        rArray(k, i) = rArray(k, i) / CSng(nPlays(k, i))
                    End If
                Next
            Next

            'Now apply moving average.
            Dim sum(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1) As Single

            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If k >= 6 Then
                        'Get sum of last six
                        For n As Integer = k - 6 To k
                            sum(k, i) += rArray(n, i)
                        Next
                        sum(k, i) = sum(k, i) / 6
                    Else
                        For n As Integer = 0 To k
                            sum(k, i) += rArray(n, i)
                        Next
                        sum(k, i) = sum(k, i) / (k + 1)
                    End If
                Next
            Next
            rArray = sum

        Else
            'Get number of plays in search
            Erase rArray
            ReDim rArray(GraphInfo.xAxisLabels.Length - 1, GraphInfo.DataGroupLabels.Length - 1)
            Dim xSearch, iSearch As String

            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                If GraphInfo.DataGroup = "Region" Or GraphInfo.DataGroupLabels(i) = "Outcome" Then
                    iSearch = " AND " & GraphInfo.DataGroup & " = " & GraphInfo.DataGroupLabels(i)
                ElseIf GraphInfo.DataGroup = "GameID" Then
                    iSearch = " AND PathData.GameID = '" & GraphInfo.DataGroupLabels(i) & "'"
                Else
                    iSearch = " AND " & GraphInfo.DataGroup & " = '" & GraphInfo.DataGroupLabels(i) & "'"
                End If
                For x As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If GraphInfo.xAxis = "Region" Or GraphInfo.xAxis = "Outcome" Then
                        xSearch = " AND " & GraphInfo.xAxis & " = " & GraphInfo.xAxisLabels(x)
                    ElseIf GraphInfo.xAxis = "GameID" Then
                        xSearch = " AND PathData.GameID = '" & GraphInfo.xAxisLabels(x) & "'"
                    Else
                        xSearch = " AND " & GraphInfo.xAxis & " = '" & GraphInfo.xAxisLabels(x) & "'"
                    End If

                    If GraphInfo.DataGroup = "EventName" Then
                        nPlays(x, i) = GetEventCount(szEvString & xSearch)
                    ElseIf GraphInfo.xAxis = "EventName" Then
                        nPlays(x, i) = GetEventCount(szEvString & iSearch)
                    Else
                        nPlays(x, i) = GetEventCount(szEvString & xSearch & iSearch)
                    End If

                    If GraphInfo.DataGroup = "EventName" Or GraphInfo.xAxis = "EventName" Then
                        rArray(x, i) = GetEventCount(szEvString & xSearch & iSearch)
                    Else
                        rArray(x, i) = GetEventCount(szEvString & xSearch & iSearch & szSearchString)
                    End If
                Next
            Next

            'Convert to percent of plays per min
            For i As Integer = 0 To GraphInfo.DataGroupLabels.Length - 1
                For k As Integer = 0 To GraphInfo.xAxisLabels.Length - 1
                    If nPlays(k, i) > 0 Then
                        rArray(k, i) = rArray(k, i) / CSng(nPlays(k, i))
                    End If
                Next
            Next


        End If



        SearchTime.Stop()
        ' frmMain.toolProgressBar.Value = 1
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return rArray
        'Return Nothing
    End Function

    Public Function CompileScatterArray(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Microsoft.VisualBasic.Collection

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        If nRecords = 0 Then Return Nothing

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim rArray As New Microsoft.VisualBasic.Collection
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Scatter Plot..."
            frmMain.toolProgressBar.Maximum = nRecords

            Do While dbReader.Read()
                Dim n As Integer = frmMain.toolProgressBar.Value
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()
                Dim Scatter As New ScatterInfo
                Scatter.GameID = dbReader.Item("GameID")
                Scatter.ID = dbReader.Item("ID")
                Scatter.Location = New PointF(CSng(dbReader.Item("x")), CSng(dbReader.Item("y")))
                Scatter.PlayNumber = dbReader.Item("PlayNumber")
                If Not dbReader.IsDBNull(4) Then
                    Scatter.StartTimeString = GetTimeStringFromSeconds(CDbl(dbReader.Item("OutcomeTime")) - UserPrefs.LeadTime, True)
                    Scatter.StopTimeString = GetTimeStringFromSeconds(CDbl(dbReader.Item("OutcomeTime")) + UserPrefs.LagTime, True)
                End If
                Scatter.TimeCriteria = dbReader.Item("TimeCriteria")
                Scatter.TeamName = dbReader.Item("TeamName")
                Scatter.TeamColor = GetTeamColor(Scatter.TeamName, , Scatter.GameID)
                Scatter.ClusterID = 0
                rArray.Add(Scatter, n)
            Loop
        End If

        strSQL.Dispose()
        dbReader.Close()
        dbName.Close()
        SearchTime.Stop()
        frmMain.toolProgressBar.Value = frmMain.toolProgressBar.Minimum
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return rArray
    End Function

    Public Function CompileEventCountHeatMap(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Single(,)

        Dim szEventNames() As String = GetCheckedDescriptorList(True)

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        If nRecords = 0 Then Return Nothing

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        'Dimension array...
        UserPrefs.hHorizontalQ = UserPrefs.clHorizontalQ
        UserPrefs.hVerticalQ = UserPrefs.clVerticalQ

        Dim cArray(0, 0) As Single
        ReDim cArray(UserPrefs.hHorizontalQ, UserPrefs.hVerticalQ)
        ReDim CurrentClusterInfo(UserPrefs.hHorizontalQ - 1, UserPrefs.hVerticalQ - 1)

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim CourtLength As Integer = 0
        Dim CourtWidth As Integer = 0
        Dim lastAddedPlay As Integer = -1

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Event Frequency Heat Map..."
            frmMain.toolProgressBar.Maximum = nRecords

            'NB - width = 90, height = 150
            Select Case UserPrefs.Sport
                Case tSports.sHockey
                    CourtWidth = 90
                    CourtLength = 150
                Case tSports.sNetball
                    CourtWidth = 90
                    CourtLength = 180
                Case tSports.sRugbyLeague
                    CourtWidth = 68
                    CourtLength = 122
                Case tSports.sRugby7
                    CourtWidth = 70
                    CourtLength = 120
                Case tSports.sBasketball
                    CourtWidth = 50
                    CourtLength = 94
                Case tSports.sSoccer
                    CourtWidth = 95
                    CourtLength = 150
            End Select

            On Error Resume Next
            Dim EventsMatch As Boolean = False

            Do While dbReader.Read()
                frmMain.toolProgressBar.Value += 1

                Application.DoEvents()

                If dbReader.Item("x") > 0 And dbReader.Item("y") > 0 Then
                    If AdvancedSearchIsActive Then
                        EventsMatch = VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch)
                    Else
                        EventsMatch = VerifyEventNamesSimple(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), szEventNames)
                    End If

                    Dim nx As Integer = Int((dbReader.Item("x") - PitchOffset.X) / (CourtWidth / UserPrefs.hHorizontalQ))
                    Dim ny As Integer = Int((dbReader.Item("y") - PitchOffset.Y) / (CourtLength / UserPrefs.hVerticalQ))
                    If EventsMatch And nx >= 0 And ny >= 0 Then
                        If AdvSearch.EventNameSet Is Nothing Or AdvancedSearchIsActive = False Then
                            'No advanced search functions have been invoked - no include all matching data
                            cArray(nx, ny) += 1
                            ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                            CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                            CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                            CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                            CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                        Else
                            'The AdvancedSearch array has been passed to AdvSearch, containing the array of required event names.
                            'If Not dbReader.Item("PlayNumber") = lastAddedPlay Then
                            If AdvSearch.EventNameSet.Length = 1 Then
                                cArray(nx, ny) += 1
                                ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                lastAddedPlay = dbReader.Item("PlayNumber")
                            Else
                                'Check that the same play is not
                                If VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch) Then
                                    cArray(nx, ny) += 1
                                    ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                    CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                    CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                    CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                    CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                    lastAddedPlay = dbReader.Item("PlayNumber")
                                End If
                            End If
                            'End If
                        End If

                    End If
                End If
            Loop
        End If

        Dim ReturnValues As Single(,)
        ReDim ReturnValues(CourtWidth, CourtLength)

        Dim rGrid(UserPrefs.hHorizontalQ, UserPrefs.hVerticalQ) As ReferenceGrid
        Dim sgWidth As Single = CourtWidth / UserPrefs.hHorizontalQ
        Dim sgHeight As Single = CourtLength / UserPrefs.hVerticalQ

        For x As Integer = 0 To UserPrefs.hHorizontalQ ' - 1
            For y As Integer = 0 To UserPrefs.hVerticalQ ' - 1
                rGrid(x, y).Coordinate.X = ((x * sgWidth) + ((x + 1) * sgWidth)) / 2
                rGrid(x, y).Coordinate.Y = ((y * sgHeight) + ((y + 1) * sgHeight)) / 2
                rGrid(x, y).Value = cArray(x, y)
            Next
        Next

        For x As Integer = 0 To CourtWidth - 1
            For y As Integer = 0 To CourtLength - 1
                ReturnValues(x, y) = Interpolate(New PointF(x, y), rGrid)
            Next
        Next

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return ReturnValues
    End Function

    Public Function CompileBallSpeedHeatMap(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Single(,)

        Dim szEventNames() As String = GetCheckedDescriptorList(True)

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        If nRecords = 0 Then Return Nothing

        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        'Dimension array...
        UserPrefs.hHorizontalQ = UserPrefs.clHorizontalQ
        UserPrefs.hVerticalQ = UserPrefs.clVerticalQ

        Dim cArray(0, 0) As Single
        ReDim cArray(UserPrefs.hHorizontalQ, UserPrefs.hVerticalQ)
        ReDim CurrentClusterInfo(UserPrefs.hHorizontalQ - 1, UserPrefs.hVerticalQ - 1)

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim CourtLength As Integer = 0
        Dim CourtWidth As Integer = 0
        Dim lastAddedPlay As Integer = -1

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Ball Speed Heat Map..."
            frmMain.toolProgressBar.Maximum = nRecords

            'NB - width = 90, height = 150
            Select Case UserPrefs.Sport
                Case tSports.sHockey
                    CourtWidth = 90
                    CourtLength = 150
                Case tSports.sNetball
                    CourtWidth = 90
                    CourtLength = 180
                Case tSports.sRugbyLeague
                    CourtWidth = 68
                    CourtLength = 122
                Case tSports.sRugby7
                    CourtWidth = 70
                    CourtLength = 120
                Case tSports.sBasketball
                    CourtWidth = 50
                    CourtLength = 94
                Case tSports.sSoccer
                    CourtWidth = 95
                    CourtLength = 150
            End Select

            On Error Resume Next
            Dim EventsMatch As Boolean = False

            Dim lastQX As Integer = Nothing     'Quadrant from previous record.
            Dim lastQY As Integer = Nothing     'Quadrant from previous record.
            Dim prevTime As Double              'TimeCode from previous record.
            Dim prevLocation As PointF          'X,Y pointF from previous record.

            Dim qDistance As Single = 0
            Dim qTime As Double = 0

            Do While dbReader.Read()
                frmMain.toolProgressBar.Value += 1

                Application.DoEvents()

                If dbReader.Item("x") > 0 And dbReader.Item("y") > 0 Then
                    If AdvancedSearchIsActive Then
                        EventsMatch = VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch)
                    Else
                        EventsMatch = VerifyEventNamesSimple(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), szEventNames)
                    End If

                    Dim nx As Integer = Int((dbReader.Item("x") - PitchOffset.X) / (CourtWidth / UserPrefs.hHorizontalQ))
                    Dim ny As Integer = Int((dbReader.Item("y") - PitchOffset.Y) / (CourtLength / UserPrefs.hVerticalQ))


                    If Not lastQX = Nothing Then
                        'If nothing, then this is the first item, and there is nothing to compare time/dist to.

                        'Get the time of the previous movement.
                        qDistance = GetDistance(New PointF(dbReader.Item("X"), dbReader.Item("Y")), prevLocation)
                        qTime = Math.Max(prevTime, dbReader.Item("TimeCode")) - Math.Min(prevTime, dbReader.Item("TimeCode"))
                    End If

                    'Update value of the last record.
                    prevLocation = New PointF(dbReader.Item("X"), dbReader.Item("Y"))
                    prevTime = dbReader.Item("TimeCode")

                    If EventsMatch And nx >= 0 And ny >= 0 Then
                        If AdvSearch.EventNameSet Is Nothing Or AdvancedSearchIsActive = False Then
                            'No advanced search functions have been invoked - no include all matching data
                            cArray(nx, ny) += 1
                            ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                            ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                            CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                            CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                            CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                            CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")
                            CurrentClusterInfo(nx, ny).TotalDistance += qDistance
                            CurrentClusterInfo(nx, ny).TotalTime += qTime
                            If lastQX <> nx And lastQY <> ny Then
                                CurrentClusterInfo(lastQX, lastQY).TotalDistance += qDistance
                                CurrentClusterInfo(lastQX, lastQY).TotalTime += qTime
                            End If

                        Else
                            'The AdvancedSearch array has been passed to AdvSearch, containing the array of required event names.
                            If AdvSearch.EventNameSet.Length = 1 Then
                                cArray(nx, ny) += 1
                                ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                lastAddedPlay = dbReader.Item("PlayNumber")
                            Else
                                'Check that the same play is not
                                If VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch) Then
                                    cArray(nx, ny) += 1
                                    ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                    CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                    CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                    CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                    CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                    lastAddedPlay = dbReader.Item("PlayNumber")
                                End If
                            End If
                            'End If
                        End If

                    End If
                    lastQX = nx : lastQY = ny
                End If

            Loop
        End If

        Dim ReturnValues As Single(,)
        ReDim ReturnValues(CourtWidth, CourtLength)

        Dim rGrid(UserPrefs.hHorizontalQ, UserPrefs.hVerticalQ) As ReferenceGrid
        Dim sgWidth As Single = CourtWidth / UserPrefs.hHorizontalQ
        Dim sgHeight As Single = CourtLength / UserPrefs.hVerticalQ

        For x As Integer = 0 To UserPrefs.hHorizontalQ ' - 1
            For y As Integer = 0 To UserPrefs.hVerticalQ ' - 1
                rGrid(x, y).Coordinate.X = ((x * sgWidth) + ((x + 1) * sgWidth)) / 2
                rGrid(x, y).Coordinate.Y = ((y * sgHeight) + ((y + 1) * sgHeight)) / 2
                '    rGrid(x, y).Value = cArray(x, y)
                CurrentClusterInfo(x, y).TotalDistance /= cArray(x, y)
                CurrentClusterInfo(x, y).TotalTime /= cArray(x, y)
                If CurrentClusterInfo(x, y).TotalTime > 0 Then
                    rGrid(x, y).Value = CurrentClusterInfo(x, y).TotalDistance / CurrentClusterInfo(x, y).TotalTime
                End If
            Next
        Next

        For x As Integer = 0 To CourtWidth - 1
            For y As Integer = 0 To CourtLength - 1
                ReturnValues(x, y) = Interpolate(New PointF(x, y), rGrid)
            Next
        Next

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return ReturnValues
    End Function

    Public Function CompileVPL(ByVal szSearchString As String, ByVal objDestGrid As DataGridView, ByVal AdvSearch As AdvancedSearchCriteria) As Long

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim n As Long = 0
        Dim nLastOutTime As Integer = 0
        Dim nLastIndex As Integer = 0
        Dim iTime As Double = 0
        Dim oTime As Double = 0
        Dim EventMatch As Boolean = False

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Playlist..."
            frmMain.toolProgressBar.Maximum = nRecords

            Do While dbReader.Read()
                frmMain.toolProgressBar.Value += 1

                Application.DoEvents()

                EventMatch = False
                With objDestGrid
                    If Not dbReader.IsDBNull(dbReader.GetOrdinal("OutcomeTime")) Then
                        If nLastIndex <> dbReader.Item("PlayNumber") Or dbReader.Item("PlayNumber") = 0 Then
                            'NB - if no spatial entries have preceded an eventname, then PlayNumber will be 0.

                            'Check event matching criteria
                            If AdvSearch.EventNameSet Is Nothing Or AdvancedSearchIsActive = False Then
                                EventMatch = True
                            Else
                                If VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch) Then
                                    EventMatch = True
                                End If
                            End If

                            'Match found, add item.
                            If EventMatch Then
                                'New id - create a new row
                                If dbReader.Item("TimeCodeVideoStampOutcome") >= 0 Then
                                    oTime = CDbl(dbReader.Item("TimeCodeVideoStampOutcome"))
                                Else
                                    oTime = CDbl(dbReader.Item("OutcomeTime"))
                                End If

                                Try
                                    .Rows.Add(dbReader.Item("PathID").ToString, dbReader.Item("GameID"), _
                                    dbReader.Item("TeamName"), _
                                    dbReader.Item("TimeCriteria"), _
                                    GetTimeStringFromSeconds(oTime - UserPrefs.LeadTime), _
                                    GetTimeStringFromSeconds(oTime + UserPrefs.LagTime), _
                                    dbReader.Item("EventName"), _
                                    GetRegionString(dbReader.Item("Region")), _
                                    dbReader.Item("VideoFile"), _
                                    dbReader.Item("VideoFile2"))
                                Catch ex As Exception
                                    .Rows.Add(dbReader.Item("PathID").ToString, dbReader.Item("PathData.GameID"), _
                                    dbReader.Item("TeamName"), _
                                    dbReader.Item("TimeCriteria"), _
                                    GetTimeStringFromSeconds(oTime - UserPrefs.LeadTime), _
                                    GetTimeStringFromSeconds(oTime + UserPrefs.LagTime), _
                                    dbReader.Item("EventName"), _
                                    GetRegionString(dbReader.Item("Region")), _
                                    dbReader.Item("VideoFile"), _
                                    dbReader.Item("VideoFile2"))
                                End Try

                                n = .Rows.GetRowCount(DataGridViewElementStates.None)
                                nLastIndex = dbReader.Item("PlayNumber")
                            End If

                        Else
                            'Existing id - append descriptors, and out-point.
                            .Rows(n - 1).Cells("Descriptors").Value = .Rows(n - 1).Cells("Descriptors").Value & ", " & dbReader.Item("EventName")
                            .Rows(n - 1).Cells("OutPoint").Value = GetTimeStringFromSeconds(CDbl(dbReader.Item("OutcomeTime")) + UserPrefs.LagTime)
                        End If
                    End If
                End With


            Loop
            frmMain.toolProgressBar.Value = 0
        End If
        SearchTime.Stop()
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return n
    End Function

    Public Function GetQuadrantLocation(ByVal szQuadrantString As String) As Point
        Dim x As Integer = CInt(Right(szQuadrantString, 1))
        Dim y As Integer = CInt(Asc(Left(szQuadrantString, 1))) - 65

        Dim ret As Point = Nothing
        ret.X = x
        ret.Y = y
        Return ret
    End Function

    Public Function GetQuadrantString(ByVal x As Single, ByVal y As Single) As String
        'NB - width = 90, height = 150
        Dim CourtLength As Integer = 0
        Dim CourtWidth As Integer = 0
        Select Case UserPrefs.Sport
            Case tSports.sHockey
                CourtWidth = 90
                CourtLength = 150
            Case tSports.sNetball
                CourtWidth = 90
                CourtLength = 180
            Case tSports.sRugbyLeague
                CourtWidth = 68
                CourtLength = 122
            Case tSports.sRugbyLeague
                CourtWidth = 70
                CourtLength = 120
            Case tSports.sBasketball
                CourtWidth = 50
                CourtLength = 94
            Case tSports.sSoccer
                CourtWidth = 95
                CourtLength = 150
        End Select

        Dim nx As Integer = Int(x - PitchOffset.X) / (CourtWidth / UserPrefs.clHorizontalQ)
        Dim ny As Integer = Int(y - PitchOffset.Y) / (CourtLength / UserPrefs.clVerticalQ)

        Return GetRowCharacter(ny) & nx.ToString
    End Function

    Public Function GetQuadrantAsPoint(ByVal x As Single, ByVal y As Single) As Point
        'NB - width = 90, height = 150
        Dim CourtLength As Integer = 0
        Dim CourtWidth As Integer = 0
        Select Case UserPrefs.Sport
            Case tSports.sHockey
                CourtWidth = 90
                CourtLength = 150
            Case tSports.sNetball
                CourtWidth = 90
                CourtLength = 180
            Case tSports.sRugbyLeague
                CourtWidth = 68
                CourtLength = 122
            Case tSports.sRugbyLeague
                CourtWidth = 70
                CourtLength = 120
            Case tSports.sBasketball
                CourtWidth = 50
                CourtLength = 94
            Case tSports.sSoccer
                CourtWidth = 95
                CourtLength = 150
        End Select

        Dim aPoint As Point

        aPoint.X = Int(x - PitchOffset.X) / (CourtWidth / UserPrefs.clHorizontalQ)
        aPoint.Y = Int(y - PitchOffset.Y) / (CourtLength / UserPrefs.clVerticalQ)

        Return aPoint
    End Function

    Public Function GetRectForQuadrant(ByVal x As Integer, ByVal y As Integer) As Rectangle
        'NB - width = 90, height = 150
        Dim CourtLength As Integer = 0
        Dim CourtWidth As Integer = 0
        Select Case UserPrefs.Sport
            Case tSports.sHockey
                CourtWidth = 90
                CourtLength = 150
            Case tSports.sNetball
                CourtWidth = 90
                CourtLength = 180
            Case tSports.sRugbyLeague
                CourtWidth = 68
                CourtLength = 122
            Case tSports.sRugbyLeague
                CourtWidth = 70
                CourtLength = 120
            Case tSports.sBasketball
                CourtWidth = 50
                CourtLength = 94
            Case tSports.sSoccer
                CourtWidth = 95
                CourtLength = 150
        End Select

        Dim aRect As Rectangle

        aRect.X = x * (CourtWidth / UserPrefs.clHorizontalQ)
        aRect.Y = y * (CourtLength / UserPrefs.clVerticalQ)

        aRect.Width = CourtWidth / UserPrefs.clHorizontalQ
        aRect.Height = CourtLength / UserPrefs.clVerticalQ


        Return aRect
    End Function



    Public Function CompileClusterArrays(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Integer(,)

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        Dim SearchTime As New Stopwatch
        If nRecords = 0 Then Return Nothing
        SearchTime.Start()

        'Dimension array...
        Dim cArray(0, 0) As Integer
        ReDim cArray(UserPrefs.clHorizontalQ - 1, UserPrefs.clVerticalQ - 1)
        ReDim CurrentClusterInfo(UserPrefs.clHorizontalQ - 1, UserPrefs.clVerticalQ - 1)

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim lastAddedPlay As Integer = -1

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Clusters..."
            frmMain.toolProgressBar.Maximum = nRecords

            'NB - width = 90, height = 150
            Dim CourtLength As Integer = 0
            Dim CourtWidth As Integer = 0
            Select Case UserPrefs.Sport
                Case tSports.sHockey
                    CourtWidth = 90
                    CourtLength = 150
                Case tSports.sNetball
                    CourtWidth = 90
                    CourtLength = 180
                Case tSports.sRugbyLeague
                    CourtWidth = 68
                    CourtLength = 122
                Case tSports.sRugbyLeague
                    CourtWidth = 70
                    CourtLength = 120
                Case tSports.sBasketball
                    CourtWidth = 50
                    CourtLength = 94
                Case tSports.sSoccer
                    CourtWidth = 95
                    CourtLength = 150
            End Select

            On Error Resume Next
            Do While dbReader.Read()
                frmMain.toolProgressBar.Value += 1

                Application.DoEvents()

                If Not dbReader.IsDBNull(dbReader.GetOrdinal("OutcomeTime")) Then
                    If dbReader.Item("x") > 0 And dbReader.Item("y") > 0 Then
                        Dim nx As Integer = Int((dbReader.Item("x") - PitchOffset.X) / (CourtWidth / UserPrefs.clHorizontalQ))
                        Dim ny As Integer = Int((dbReader.Item("y") - PitchOffset.Y) / (CourtLength / UserPrefs.clVerticalQ))
                        If nx >= 0 And ny >= 0 Then
                            If AdvSearch.EventNameSet Is Nothing Or AdvancedSearchIsActive = False Then
                                'No advanced search functions have been invoked - no include all matching data
                                cArray(nx, ny) += 1
                                ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                            Else
                                'The AdvancedSearch array has been passed to AdvSearch, containing the array of required event names.
                                'If Not dbReader.Item("PlayNumber") = lastAddedPlay Then
                                If AdvSearch.EventNameSet.Length = 1 Then
                                    cArray(nx, ny) += 1
                                    ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                    ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                    CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                    CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                    CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                    CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                    lastAddedPlay = dbReader.Item("PlayNumber")
                                Else
                                    'Check that the same play is not
                                    If VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch) Then
                                        cArray(nx, ny) += 1
                                        ReDim Preserve CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1)
                                        ReDim Preserve CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1)
                                        ReDim Preserve CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1)
                                        ReDim Preserve CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1)

                                        CurrentClusterInfo(nx, ny).GameID(cArray(nx, ny) - 1) = dbReader.Item("GameID")
                                        CurrentClusterInfo(nx, ny).ID(cArray(nx, ny) - 1) = dbReader.Item("ID")
                                        CurrentClusterInfo(nx, ny).PlayNumber(cArray(nx, ny) - 1) = dbReader.Item("PlayNumber")
                                        CurrentClusterInfo(nx, ny).TimeCriteria(cArray(nx, ny) - 1) = dbReader.Item("TimeCriteria")

                                        lastAddedPlay = dbReader.Item("PlayNumber")
                                    End If
                                End If
                                'End If
                            End If

                        End If
                    End If
                End If
            Loop
        End If
        SearchTime.Stop()
        frmMain.toolProgressBar.Value = frmMain.toolProgressBar.Minimum
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return cArray
    End Function

    Public Function CompileClusterColors(ByVal cArray(,) As Integer) As Color(,)
        Dim maxCount As MaxMinValues = GetMaxClusterArrayValues(cArray)
        Dim ColorArray(,) As Color
        ReDim ColorArray(UserPrefs.clHorizontalQ - 1, UserPrefs.clVerticalQ - 1)
        For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
            For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                ColorArray(x, y) = GetColorGradient(Color.Blue, Color.Yellow, cArray(x, y), maxCount.nMax, 110)
            Next
        Next

        Return ColorArray
    End Function

    Public Function CompileClusterColors2(ByVal cArray(,) As Single) As Color(,)
        Dim maxCount As MaxMinValues = GetMaxClusterArrayValues2(cArray)
        Dim low3 = (maxCount.nMax - maxCount.nMin) / 3
        Dim high3 = maxCount.nMax - low3
        Dim ColorArray(,) As Color
        ReDim ColorArray(cArray.GetUpperBound(0) - 1, cArray.GetUpperBound(1) - 1)
        For x As Integer = 0 To cArray.GetUpperBound(0) - 1
            For y As Integer = 0 To cArray.GetUpperBound(1) - 1
                If cArray(x, y) = 0 Then
                    ColorArray(x, y) = Color.FromArgb(125, Color.DarkBlue)
                ElseIf cArray(x, y) > 0 And cArray(x, y) < low3 Then
                    ColorArray(x, y) = GetColorGradient(Color.DarkBlue, Color.Green, cArray(x, y), low3, 125)
                ElseIf cArray(x, y) >= low3 And cArray(x, y) < high3 Then
                    ColorArray(x, y) = GetColorGradient(Color.Green, Color.Yellow, cArray(x, y) - low3, high3 - low3, 125)
                ElseIf cArray(x, y) >= high3 Then
                    ColorArray(x, y) = GetColorGradient(Color.Yellow, Color.Red, cArray(x, y) - high3, maxCount.nMax - high3, 125)
                End If
            Next
        Next

        Return ColorArray
    End Function

    Public Function GetColorGradient2(ByVal nValue As Single, ByVal nMinValue As Single, ByVal nMaxValue As Single) As Color
        Dim low3 = (nMaxValue - nMinValue) / 3
        Dim high3 = nMaxValue - low3

        If nValue = 0 Then
            Return Color.DarkBlue
        ElseIf nValue > 0 And nValue < low3 Then
            Return GetColorGradient(Color.DarkBlue, Color.Green, nValue, low3, 125)
        ElseIf nValue >= low3 And nValue < high3 Then
            Return GetColorGradient(Color.Green, Color.Yellow, nValue - low3, high3 - low3, 125)
        ElseIf nValue >= high3 Then
            Return GetColorGradient(Color.Yellow, Color.Red, nValue - high3, nMaxValue - high3, 125)
        End If

        Return Color.DarkBlue

    End Function

    Public Function CompilePlayerMap(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Microsoft.VisualBasic.Collection
        Dim szEventNames() As String = Nothing
        Dim bUnfinishedMatchFound As Boolean = False
        szEventNames = GetCheckedDescriptorList(True)

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString & " ORDER BY ID", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        Dim Play As New GamePlay.Instance
        Dim Plays As New Microsoft.VisualBasic.Collection
        Dim Index_Play As Integer = 0
        Dim lastPoint As PointF
        Dim thisPoint As PointF
        Dim lastBallStatus As PathStatus = Nothing
        Dim EventsMatch As Boolean = False
        Dim FlagNextItemAsPossession As Boolean = False
        Dim FlagNextItemAsFollowOn As Boolean = False

        If dbReader.HasRows Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Compiling Player Maps..."
            frmMain.toolProgressBar.Maximum = nRecords

            Do While dbReader.Read()
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()

                If dbReader.Item("Status") = PathStatus.psStart Then
                    FlagNextItemAsFollowOn = False

                    'Resolve previous path.
                    If lastBallStatus = PathStatus.psCarry Then
                        Play.EndsHere = False
                        Index_Play += 1
                        Plays.Add(Play, Index_Play)
                    End If

                    'New path found - search for matches.
                    If AdvancedSearchIsActive Then
                        EventsMatch = VerifyEventNamesAdvanced(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), AdvSearch)
                    Else
                        EventsMatch = VerifyEventNamesSimple(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")), szEventNames)
                    End If

                    If EventsMatch Then
                        bUnfinishedMatchFound = True
                        Play = New GamePlay.Instance
                        Play.Pen = New Pen(Color.FromArgb(10, Color.DodgerBlue), 0.5)
                        Play.Path = New Drawing2D.GraphicsPath
                        Play.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                        Play.ID = dbReader.Item("ID")
                        Play.PlayNumber = dbReader.Item("PlayNumber")
                        Play.VideoStartTime = dbReader.Item("TimeCode")
                        thisPoint.X = dbReader.Item("X")
                        thisPoint.Y = dbReader.Item("Y")
                        Index_Play += 1
                        Plays.Add(Play, Index_Play)
                        lastBallStatus = PathStatus.psStart
                    End If

                ElseIf dbReader.Item("Status") = PathStatus.psPass And EventsMatch Then
                    'This is a pass movement.  Add the path and check if the eventname belongs to this path segment.
                    'If so, previous path if modified, and notify the next path is a ball delivery.
                    If lastBallStatus = PathStatus.psCarry Then
                        Play.EndsHere = False
                        Index_Play += 1
                        Plays.Add(Play, Index_Play)
                    End If

                    'First, check if this segment includes the event name.
                    If Not dbReader.IsDBNull(dbReader.GetOrdinal("EventName")) And bUnfinishedMatchFound = True Then
                        If Array.IndexOf(szEventNames, dbReader.Item("EventName")) >= 0 Then
                            Dim LastPlay As GamePlay.Instance = Plays(Index_Play)
                            LastPlay.Lead = True
                            Plays.Remove(Index_Play)
                            Plays.Add(LastPlay, Index_Play)
                            bUnfinishedMatchFound = False
                            FlagNextItemAsPossession = True
                        End If
                    End If

                    'Now add the new play segment
                    Play = New GamePlay.Instance
                    Play.Pen = New Pen(Color.FromArgb(10, Color.DodgerBlue), 0.5)
                    Play.Path = New Drawing2D.GraphicsPath
                    Play.ID = dbReader.Item("ID")
                    Play.PlayNumber = dbReader.Item("PlayNumber")
                    Play.VideoStartTime = dbReader.Item("TimeCode")
                    'If this segment is an event name holder then set it as so.
                    If FlagNextItemAsFollowOn Then
                        Play.Lag = True
                        FlagNextItemAsFollowOn = False
                    End If
                    If FlagNextItemAsPossession Then
                        Play.Posession = True
                        Play.EndsHere = True
                        FlagNextItemAsPossession = False
                        FlagNextItemAsFollowOn = True
                    End If
                    lastPoint = thisPoint

                    thisPoint.X = dbReader.Item("X")
                    thisPoint.Y = dbReader.Item("Y")

                    Play.Path.AddLine(lastPoint, thisPoint)
                    Index_Play += 1
                    Plays.Add(Play, Index_Play)
                    lastBallStatus = PathStatus.psPass

                ElseIf dbReader.Item("Status") = PathStatus.psCarry And EventsMatch Then
                    'This is a carry movement.  Add the path and check if the eventname belongs to this path segment.

                    'First add the new play segment
                    If lastBallStatus <> PathStatus.psCarry Then
                        'This is the start of a new chain of carry movements.
                        Play = New GamePlay.Instance
                        Play.Pen = New Pen(Color.FromArgb(10, Color.DodgerBlue), 0.5)
                        Play.Path = New Drawing2D.GraphicsPath
                        Play.ID = dbReader.Item("ID")
                        Play.PlayNumber = dbReader.Item("PlayNumber")
                        Play.VideoStartTime = dbReader.Item("TimeCode")
                    End If

                    lastPoint = thisPoint
                    thisPoint.X = dbReader.Item("X")
                    thisPoint.Y = dbReader.Item("Y")

                    Play.Path.AddLine(lastPoint, thisPoint)
                    lastBallStatus = PathStatus.psCarry

                    'Now, check if this segment includes the event name.
                    If Not dbReader.IsDBNull(dbReader.GetOrdinal("EventName")) And bUnfinishedMatchFound = True Then
                        If Array.IndexOf(szEventNames, dbReader.Item("EventName")) >= 0 Then
                            Dim LastPlay As GamePlay.Instance = Plays(Index_Play)
                            LastPlay.Lead = True
                            Plays.Remove(Index_Play)
                            Plays.Add(LastPlay, Index_Play)

                            'Play.Lead = True
                            bUnfinishedMatchFound = False
                            FlagNextItemAsPossession = True
                        End If
                    End If

                    'If this segment is an event name holder then set it as so.
                    If FlagNextItemAsFollowOn Then
                        Play.Lag = True
                        FlagNextItemAsFollowOn = False
                    End If
                    If FlagNextItemAsPossession Then
                        Play.Posession = True
                        Play.EndsHere = True
                        FlagNextItemAsPossession = False
                        FlagNextItemAsFollowOn = True
                    End If
                End If
            Loop

            'Finally, add play in case last entry was a carry.
            If lastBallStatus = PathStatus.psCarry And EventsMatch Then
                Index_Play += 1
                Plays.Add(Play, Index_Play)
            End If

            Return Plays
        End If

        Return Nothing
    End Function

    Public Function CompilePathwayMap(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria) As Microsoft.VisualBasic.Collection

        If Not AdvSearch.EventNameSet Is Nothing And AdvancedSearchIsActive Then
            szSearchString = szSearchString & " AND ("
            For Each item As String In AdvSearch.EventNameSet
                szSearchString = szSearchString & "EventName = '" & item & "'"
                If Array.IndexOf(AdvSearch.EventNameSet, item) < AdvSearch.EventNameSet.Length - 1 Then
                    szSearchString = szSearchString & " OR "
                End If
            Next
            szSearchString = szSearchString & ")"

        End If

        Dim nRecords As Integer = GetSearchingRecordsCount(szSearchString)
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString & " ORDER BY ID", dbName)

        Try
            Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
            Dim Play As New GamePlay.Instance
            Dim Plays As New Microsoft.VisualBasic.Collection
            Dim Index_Play As Integer = 0
            Dim lastPoint As PointF
            Dim thisPoint As PointF
            Dim lastBallStatus As Integer = 0
            Dim EventsMatch As Boolean = False

            If dbReader.HasRows Then
                frmMain.toolProgressBar.Minimum = 0
                frmMain.toolProgressBar.Value = 0
                frmMain.toolActionStatus.Text = "Compiling Pathways..."
                frmMain.toolProgressBar.Maximum = nRecords


                If AdvancedSearchIsActive Then
                    Do While dbReader.Read()
                        frmMain.toolProgressBar.Value += 1

                        Application.DoEvents()

                        'New path found - search for matches.
                        Dim TheseEvents() As String = Nothing

                        'First check whether an array of event names is needed - not so if no eventnamelist has been specified..
                        If Not AdvSearch.EventNameSet Is Nothing Then
                            TheseEvents = GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria"))
                            'If so, then cross check combinations and inclusions..
                            EventsMatch = VerifyEventNamesAdvanced(TheseEvents, AdvSearch)

                        Else
                            'If not, all events are included to whatever events are in this playnumber, they must match.
                            EventsMatch = True
                        End If

                        If EventsMatch Then
                            'If a match has been found with the event names, then check for region compatibility if a region criteria has been specified.
                            If Not AdvSearch.Regions Is Nothing Then
                                EventsMatch = EventsMatch And VerifyEventNameRegion(TheseEvents, dbReader.Item("PlayNumber"), dbReader.Item("GameID"), AdvSearch.Regions)
                            End If
                        End If

                        If EventsMatch = True Then 'Criteria satisfied...
                            'Get entire play sequence.
                            Dim GamePlaySet As GamePlayClass = GetPlays(dbReader.Item("GameID"), dbReader.Item("TimeCriteria"), dbReader.Item("PlayNumber"), nRecords)
                            If Not GamePlaySet.Plays Is Nothing Then
                                For Each NewPlay As GamePlay.Instance In GamePlaySet.Plays
                                    Index_Play += 1
                                    Plays.Add(NewPlay, Index_Play)
                                Next
                            End If
                        End If

                    Loop
                    SearchTime.Stop()
                    frmMain.toolProgressBar.Value = 0
                    frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

                    Return Plays

                Else

                    Dim DifferentiateTypes As Boolean = True
                    Do While dbReader.Read()
                        frmMain.toolProgressBar.Value += 1

                        Application.DoEvents()

                        If dbReader.Item("Status") = PathStatus.psStart Then
                            'New path found - search for matches.
                            EventsMatch = VerifyEventNamesSimple(GetEventNameArrayFromPlay(dbReader.Item("PlayNumber"), dbReader.Item("GameID"), dbReader.Item("TimeCriteria")))

                            If EventsMatch = True Then
                                'Criteria satisfied...
                                'First, if a preceding play exists, then add an arrow to the end of the previous play.
                                If Not Play.ID = Nothing Then
                                    Play.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
                                    'lastBallStatus = -1
                                End If

                                'Next add new play to a collection
                                Play = New GamePlay.Instance
                                Play.Pen = New Pen(Color.DodgerBlue, 0.5)
                                Play.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                                Play.Pen.DashStyle = Drawing2D.DashStyle.Dash

                                Play.Path = New Drawing2D.GraphicsPath
                                Play.ID = dbReader.Item("ID")
                                Play.PlayNumber = dbReader.Item("PlayNumber")
                                Play.VideoStartTime = dbReader.Item("TimeCode")
                                thisPoint.X = dbReader.Item("X")
                                thisPoint.Y = dbReader.Item("Y")

                                lastPoint = thisPoint
                                lastBallStatus = 0

                                Index_Play += 1
                                Plays.Add(Play, Index_Play)

                                If nRecords > UserPrefs.pmDifferntiateThreshold Then
                                    DifferentiateTypes = False
                                    Play.Pen.DashStyle = Drawing2D.DashStyle.Solid
                                Else
                                    DifferentiateTypes = True
                                End If
                            End If

                        Else
                            If EventsMatch Then

                                If (dbReader.Item("Status") <> lastBallStatus Or dbReader.Item("Status") = PathStatus.psPass) And DifferentiateTypes Then
                                    'This ball movement is NOT of the same type as the previous ~ begin new path.
                                    'Begin adding play to a collection
                                    Play = New GamePlay.Instance
                                    Play.Pen = New Pen(Color.DodgerBlue, 0.5)
                                    Play.Path = New Drawing2D.GraphicsPath
                                    Play.ID = dbReader.Item("ID")
                                    Play.PlayNumber = dbReader.Item("PlayNumber")
                                    Play.VideoStartTime = dbReader.Item("TimeCode")
                                    thisPoint.X = dbReader.Item("X")
                                    thisPoint.Y = dbReader.Item("Y")

                                    If lastBallStatus = 0 Then
                                        Play.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                                    End If

                                    If dbReader.Item("Status") = 1 Then
                                        'Ball movement is a pass.
                                        Play.Pen.DashStyle = Drawing2D.DashStyle.Dash
                                        lastBallStatus = 1
                                    Else
                                        'Ball movement is a carry.
                                        Play.Pen.DashStyle = Drawing2D.DashStyle.Solid
                                        lastBallStatus = 2
                                    End If

                                    Index_Play += 1
                                    Play.Path.AddLine(lastPoint, thisPoint)
                                    lastPoint = thisPoint
                                    Plays.Add(Play, Index_Play)

                                Else
                                    'This ball movement is of the same type as the previous ~ continue previous path.
                                    thisPoint.X = dbReader.Item("X")
                                    thisPoint.Y = dbReader.Item("Y")

                                    If lastPoint = Nothing Then lastPoint = thisPoint
                                    Play.Path.AddLine(lastPoint, thisPoint)
                                    lastPoint = thisPoint

                                End If

                            End If
                        End If
                    Loop


                    Return Plays
                End If
            End If

        Catch ex As Exception
            MsgBox("An error has occured compiling this pathway map:" & vbNewLine & ex.Message, MsgBoxStyle.Critical)
        End Try

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = frmMain.toolProgressBar.Minimum
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return Nothing
    End Function

    Public Function CompileGraphData(ByVal szSearchString As String, ByVal AdvSearch As AdvancedSearchCriteria, ByVal GraphInfo As GraphType) As Single(,)

        'Set levels of data available.
        Dim k As Integer = 0
        If Not GraphInfo.DataGroupLabels Is Nothing Then
            k = GraphInfo.DataGroupLabels.Length - 1
        End If
        Dim Data(GraphInfo.xAxisLabels.Length - 1, k) As Single

        Dim dt As New DataTable
        Dim x, y As Integer

        'Compile graph data sets
        Select Case GraphInfo.yAxis

            Case Is = "Frequency"
                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count
                    Dim q(k) As Integer
                    y = 1
                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Frequency Graph Data..."
                        frmMain.toolProgressBar.Value = y - 1
                        Application.DoEvents()

                        If GraphInfo.xAxis = "Region" Or GraphInfo.xAxis = "Outcome" Then
                            Try
                                x = Array.IndexOf(GraphInfo.DataGroupLabels, CStr(value.Item(GraphInfo.xAxis)))
                            Catch ex As Exception
                                x = -1
                            End Try
                        Else
                            x = Array.IndexOf(GraphInfo.DataGroupLabels, value.Item(GraphInfo.xAxis))
                        End If

                        If x >= 0 Then
                            q(x) += 1
                        End If
                        For z As Integer = 0 To k
                            Data(y, z) = q(z)
                        Next
                        y += 1
                    Next
                End If

            Case Is = "DMDistance"
                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count
                    Dim q(k) As Integer
                    y = 1
                    Dim ThisPoint(k) As PointF
                    Dim LastPoint(k) As PointF

                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Distance Graph Data..."
                        frmMain.toolProgressBar.Value = y - 1
                        Application.DoEvents()

                        If GraphInfo.xAxis = "Region" Or GraphInfo.xAxis = "Outcome" Then
                            Try
                                x = Array.IndexOf(GraphInfo.DataGroupLabels, CStr(value.Item(GraphInfo.xAxis)))
                            Catch ex As Exception
                                x = -1
                            End Try
                        Else
                            x = Array.IndexOf(GraphInfo.DataGroupLabels, value.Item(GraphInfo.xAxis))
                        End If

                        If x >= 0 Then
                            ThisPoint(x) = New PointF(value.Item("x"), value.Item("y"))
                            If LastPoint(x) = Nothing Then LastPoint(x) = ThisPoint(x)

                            q(x) += GetDistanceBetweenPoints(ThisPoint(x), LastPoint(x))
                            LastPoint(x) = ThisPoint(x)
                        End If
                        For z As Integer = 0 To k
                            Data(y, z) = q(z)
                        Next
                        y += 1
                    Next
                End If

            Case Is = "Time"
                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count
                    Dim q(k) As Integer
                    y = 1
                    Dim ThisPoint(k) As Single
                    Dim LastPoint(k) As Single

                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Possession Time Graph Data..."
                        frmMain.toolProgressBar.Value = y - 1
                        Application.DoEvents()

                        If GraphInfo.xAxis = "Region" Or GraphInfo.xAxis = "Outcome" Then
                            Try
                                x = Array.IndexOf(GraphInfo.DataGroupLabels, CStr(value.Item(GraphInfo.xAxis)))
                            Catch ex As Exception
                                x = -1
                            End Try
                        Else
                            x = Array.IndexOf(GraphInfo.DataGroupLabels, value.Item(GraphInfo.xAxis))
                        End If

                        If x >= 0 Then
                            ThisPoint(x) = value.Item("TimeCode")
                            If LastPoint(x) = Nothing Then LastPoint(x) = ThisPoint(k)

                            If ThisPoint(x) > LastPoint(x) Then q(x) += (ThisPoint(x) - LastPoint(x))
                            LastPoint(x) = ThisPoint(x)
                        End If
                        For z As Integer = 0 To k
                            Data(y, z) = q(z)
                        Next
                        y += 1
                    Next
                End If

            Case Is = "Event Totals"
                'Append descriptor list to query.
                szSearchString = szSearchString & GetDescriptorListSQL()
                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count

                    'Calculate the frequency of each event by axis values.
                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Event Totals Graph Data..."
                        frmMain.toolProgressBar.Value = y
                        Application.DoEvents()

                        If Not value.IsNull(GraphInfo.xAxis) Then 'And GraphInfo.DataGroup <> "None" Then
                            'Get index matching event name
                            x = Array.IndexOf(GraphInfo.xAxisLabels, TryCast(value.Item(GraphInfo.xAxis), String))
                            If x = -1 Then
                                Try
                                    x = Array.IndexOf(GraphInfo.xAxisLabels, Trim(CStr(value.Item(GraphInfo.xAxis))))
                                Catch ex As Exception
                                    x = -1
                                End Try
                            End If
                            If k > 0 Then
                                y = Array.IndexOf(GraphInfo.DataGroupLabels, TryCast(value.Item(GraphInfo.DataGroup), String))
                                If y = -1 Then
                                    Try
                                        y = Array.IndexOf(GraphInfo.DataGroupLabels, Trim(CStr(value.Item(GraphInfo.DataGroup))))
                                    Catch ex As Exception
                                        y = -1
                                    End Try
                                End If
                            Else
                                y = 0
                            End If

                            If x >= 0 And y >= 0 Then Data(x, y) += 1
                        End If
                    Next
                End If

            Case Is = "Posession Time"
                'Ignore other selections - possession time requires teams names and game ids and time frames only.
                szSearchString = szSearchString & " ORDER BY Pathdata.ID"

                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count

                    Dim nLastTime As Double = 0
                    Dim LastGameID As String = Nothing
                    Dim LastTimeCriteria As String = Nothing
                    Dim LastTeamName As String = Nothing

                    'Calculate the frequency of each event by axis values.
                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Possession Time Graph Data..."
                        frmMain.toolProgressBar.Value = y
                        Application.DoEvents()

                        If Not value.IsNull(GraphInfo.xAxis) And Not value.IsNull(GraphInfo.DataGroup) Then

                            'Get index matching event name
                            x = Array.IndexOf(GraphInfo.xAxisLabels, TryCast(value.Item(GraphInfo.xAxis), String))
                            If x = -1 Then
                                Try
                                    x = Array.IndexOf(GraphInfo.xAxisLabels, Trim(CStr(value.Item(GraphInfo.xAxis))))
                                Catch ex As Exception
                                    x = -1
                                End Try
                            End If
                            If k > 0 Then
                                y = Array.IndexOf(GraphInfo.DataGroupLabels, TryCast(value.Item(GraphInfo.DataGroup), String))
                                If y = -1 Then
                                    Try
                                        y = Array.IndexOf(GraphInfo.DataGroupLabels, Trim(CStr(value.Item(GraphInfo.DataGroup))))
                                    Catch ex As Exception
                                        y = -1
                                    End Try
                                End If
                            Else
                                y = 0
                            End If

                            If y >= 0 Then
                                If LastGameID = value.Item("GameID") And LastTimeCriteria = value.Item("TimeCriteria") And LastTeamName = value.Item("TeamName") Then
                                    'This event is in the same graph category as the previous event, so the change in time can be subtracted.
                                    Data(x, y) += (value.Item("TimeCode") - nLastTime)
                                    nLastTime = value.Item("TimeCode")
                                Else
                                    LastGameID = value.Item("GameID")
                                    LastTimeCriteria = value.Item("TimeCriteria")
                                    LastTeamName = value.Item("TeamName")
                                    nLastTime = value.Item("TimeCode")
                                End If
                            End If
                        End If

                    Next
                End If

            Case Is = "Ball Movements"
                'Append descriptor list to query.
                szSearchString = szSearchString & GetDescriptorListSQL()
                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count

                    'Calculate the frequency of each event by axis values.
                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Ball Movement Graph Data..."
                        frmMain.toolProgressBar.Value = y
                        Application.DoEvents()

                        'Get index matching event name
                        If value.Item("Status") = 1 Then
                            'Passes only
                            x = Array.IndexOf(GraphInfo.xAxisLabels, TryCast(value.Item(GraphInfo.xAxis), String))
                            If x = -1 Then
                                Try
                                    x = Array.IndexOf(GraphInfo.xAxisLabels, Trim(CStr(value.Item(GraphInfo.xAxis))))
                                Catch ex As Exception
                                    x = -1
                                End Try
                            End If
                            If k > 0 Then
                                y = Array.IndexOf(GraphInfo.DataGroupLabels, TryCast(value.Item(GraphInfo.DataGroup), String))
                                If y = -1 Then
                                    Try
                                        y = Array.IndexOf(GraphInfo.DataGroupLabels, Trim(Str(value.Item(GraphInfo.DataGroup))))
                                    Catch ex As Exception
                                        y = -1
                                    End Try
                                End If
                            Else
                                y = 0
                            End If

                            If x >= 0 And y >= 0 Then Data(x, y) += 1
                        End If

                    Next
                End If

            Case Is = "Distance"
                'Ignore other selections - possession time requires teams names and game ids and time frames only.
                szSearchString = szSearchString & " ORDER BY Pathdata.ID"

                Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    frmMain.toolProgressBar.Maximum = dt.Rows.Count

                    Dim nLastPoint As PointF
                    Dim LastGameID As String = Nothing
                    Dim LastTimeCriteria As String = Nothing
                    Dim LastTeamName As String = Nothing

                    'Calculate the frequency of each event by axis values.
                    For Each value As DataRow In dt.Rows
                        frmMain.toolActionStatus.Text = "Compiling Distance Graph Data..."
                        frmMain.toolProgressBar.Value = y
                        Application.DoEvents()

                        'Get index matching event name
                        x = Array.IndexOf(GraphInfo.xAxisLabels, CStr(value.Item(GraphInfo.xAxis)))
                        If k > 0 Then
                            y = Array.IndexOf(GraphInfo.DataGroupLabels, CStr(value.Item(GraphInfo.DataGroup)))
                        Else
                            y = 0
                        End If

                        If LastGameID = value.Item("GameID") And LastTimeCriteria = value.Item("TimeCriteria") And LastTeamName = value.Item("TeamName") Then
                            Data(x, y) += GetDistanceBetweenPoints(nLastPoint, New PointF(value.Item("x"), value.Item("y")))
                            nLastPoint = New PointF(value.Item("x"), value.Item("y"))
                        Else
                            LastGameID = value.Item("GameID")
                            LastTimeCriteria = value.Item("TimeCriteria")
                            LastTeamName = value.Item("TeamName")
                            nLastPoint = Nothing
                        End If

                    Next
                End If
        End Select

        Return Data

    End Function

    Public Function AddStandAloneEventToVPL(ByVal szCaption As String, ByVal szSource As String, ByVal InPoint As Double, ByVal OutPoint As Double, _
    ByVal szGameID As String, ByVal szTeamName As String, ByVal szTimeCriteria As String) As Boolean

        'Search for any existing playlists
        If countVPL > 0 Then
            If frmVPL(lastVPLFormUsed).Visible Then
                Dim res As MsgBoxResult = MsgBox("Would you like to append this event to the Video Play List that is already open: " & _
                frmVPL(lastVPLFormUsed).Text, MsgBoxStyle.YesNo, Application.ProductName)

                If res = MsgBoxResult.No Then

                    countVPL = countVPL + 1
                    lastVPLFormUsed = countVPL
                    ReDim Preserve frmVPL(countVPL)
                    frmVPL(countVPL) = New frmVideoPlayList(countVPL)

                End If
            Else

                countVPL = countVPL + 1
                lastVPLFormUsed = countVPL
                ReDim Preserve frmVPL(countVPL)
                frmVPL(countVPL) = New frmVideoPlayList(countVPL)
            End If

        Else

            countVPL = countVPL + 1
            lastVPLFormUsed = countVPL
            ReDim Preserve frmVPL(countVPL)
            frmVPL(countVPL) = New frmVideoPlayList(countVPL)

        End If

        'frmVPL(lastVPLFormUsed).vplGrid.Rows.Add("", "", "", "", GetTimeStringFromSeconds(InPoint), GetTimeStringFromSeconds(OutPoint), szCaption, "", szSource)
        Try
            frmVPL(lastVPLFormUsed).vplGrid.Rows.Add("", szGameID, szTeamName, szTimeCriteria, GetTimeStringFromSeconds(InPoint), GetTimeStringFromSeconds(OutPoint), szCaption, "", szSource)
            frmVPL(lastVPLFormUsed).formDirty = True
            frmVPL(lastVPLFormUsed).MdiParent = frmMain
            frmVPL(lastVPLFormUsed).Show()

        Catch ex As Exception

        End Try
    End Function

    Public Function GetPlays(ByVal szGameID As String, ByVal szTimeCriteria As String, ByVal nPlayNumber As Long, ByVal nRecords As Integer) As GamePlayClass

        Dim ThisPlayClass As GamePlayClass
        ThisPlayClass.Captions = Nothing
        ThisPlayClass.Plays = Nothing

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim lastPoint As PointF
        Dim thisPoint As PointF

        Dim SQL As String = "SELECT ID, TimeCode, X, Y, Status, PlayNumber, EventName FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE (PlayNumber = " & nPlayNumber.ToString & _
            ") AND (TimeCriteria = '" & szTimeCriteria & "') AND (PathData.GameID = '" & szGameID & "') ORDER BY ID"

        dbName.Open()

        Dim strSQL As New OleDbCommand(SQL, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Dim n As Integer = -1
            Dim k As Integer = -1
            Dim dY As Integer = 0
            Dim lastBallStatus As Integer = -1
            Dim lastCaptionID As Long = 0
            Dim DifferentiateTypes As Boolean = True
            Dim ID_BeginningOfMovement As Long = Nothing

            Do While dbReader.Read()
                If dbReader.Item("Status") = 0 Then
                    lastPoint.X = dbReader.Item("X")
                    lastPoint.Y = dbReader.Item("Y")
                Else

                    If (dbReader.Item("Status") <> lastBallStatus Or dbReader.Item("Status") = PathStatus.psPass) And DifferentiateTypes Then
                        'This ball movement is NOT of the same type as the previous ~ begin new path.
                        'Begin adding play to a collection
                        'NB: DifferentiateTypes is true to start with, but after the first item will be set to false if records exceed 50

                        n += 1
                        ReDim Preserve ThisPlayClass.Plays(n)

                        ThisPlayClass.Plays(n) = New GamePlay.Instance
                        ThisPlayClass.Plays(n).Path = New Drawing2D.GraphicsPath
                        ThisPlayClass.Plays(n).Pen = New Pen(Color.DodgerBlue, 0.5)
                        ThisPlayClass.Plays(n).ID = dbReader.Item("ID")
                        ThisPlayClass.Plays(n).PlayNumber = dbReader.Item("PlayNumber")
                        ThisPlayClass.Plays(n).VideoStartTime = dbReader.Item("TimeCode")
                        thisPoint.X = dbReader.Item("X")
                        thisPoint.Y = dbReader.Item("Y")
                        ID_BeginningOfMovement = ThisPlayClass.Plays(n).ID

                        If dbReader.Item("Status") = 1 Then
                            'Ball movement is a pass.
                            ThisPlayClass.Plays(n).Pen.DashStyle = Drawing2D.DashStyle.Dash
                            lastBallStatus = 1
                        Else
                            'Ball movement is a carry.
                            ThisPlayClass.Plays(n).Pen.DashStyle = Drawing2D.DashStyle.Solid
                            lastBallStatus = 2
                        End If

                        ThisPlayClass.Plays(n).Path.AddLine(lastPoint, thisPoint)
                        lastPoint = thisPoint

                        'Disengage the DifferentiateTypes variable if record count > 50 - performance.
                        If nRecords > UserPrefs.pmDifferntiateThreshold Then
                            DifferentiateTypes = False
                            ThisPlayClass.Plays(n).Pen.DashStyle = Drawing2D.DashStyle.Solid
                        End If

                    Else
                        'This ball movement is of the same type as the previous ~ continue previous path.
                        thisPoint.X = dbReader.Item("X")
                        thisPoint.Y = dbReader.Item("Y")

                        If lastPoint = Nothing Then lastPoint = thisPoint
                        ThisPlayClass.Plays(n).Path.AddLine(lastPoint, thisPoint)
                        lastPoint = thisPoint

                    End If

                    If Not dbReader.IsDBNull(dbReader.GetOrdinal("EventName")) Then
                        'Add Caption if this is an event name
                        k += 1
                        ReDim Preserve ThisPlayClass.Captions(k)
                        ThisPlayClass.Captions(k).BoxSize.X = dbReader.Item("X")
                        ThisPlayClass.Captions(k).BoxSize.Y = dbReader.Item("Y")
                        ThisPlayClass.Captions(k).Text = dbReader.Item("EventName")
                        ThisPlayClass.Captions(k).ID = dbReader.Item("ID")
                        ThisPlayClass.Captions(k).PlayNumber = dbReader.Item("PlayNumber")
                    End If
                End If
            Loop
            dbReader.Close()

            If Not ThisPlayClass.Plays Is Nothing Then
                ThisPlayClass.Plays(0).Pen.StartCap = Drawing2D.LineCap.RoundAnchor
                ThisPlayClass.Plays(n).Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
            End If
        End If
        dbName.Close()

        Return ThisPlayClass

    End Function

    Public Function GetEventCount(ByVal szSearchString As String) As Integer


        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
        GetEventCount = 0

        Try
            da.Fill(dt)
            GetEventCount = dt.Rows.Count
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
        dt.Dispose()
        da.Dispose()

        Return GetEventCount
    End Function

    Public Function GetEventCount(ByVal cSearch As SearchCriteria) As Integer

        '1.1. Compile multiple gameID string
        Dim szParamString As String = "(PathData.GameID = "
        For Each Game As String In cSearch.szGameID
            szParamString &= "'" & Game & "'"
            If Array.IndexOf(cSearch.szGameID, Game) < cSearch.szGameID.Length - 1 Then
                szParamString &= " OR PathData.GameID = "
            End If
        Next
        szParamString &= ")"

        '1.2. Add Time criteria if selected
        If Not cSearch.szTimeCriterion Is Nothing Then
            szParamString &= " AND (TimeCriteria = '" & cSearch.szTimeCriterion(0) & "')"
        End If

        '1.3. Add Team criteria if selected
        If Not cSearch.szTeamName Is Nothing Then
            szParamString &= " AND (TeamName = '" & cSearch.szTeamName(0) & "')"
        End If

        '1.4. Add Team criteria if selected
        If Not cSearch.uOutcomes Is Nothing Then
            If cSearch.uOutcomes(0) <> OutcomeType.outAll Then szParamString &= " AND (Outcome = " & cSearch.uOutcomes(0) & ")"
        End If

        '1.5. Add descriptors to string
        If Not cSearch.szDescriptors Is Nothing Then
            szParamString &= " AND (EventName = "
            For Each Item As String In cSearch.szDescriptors
                szParamString &= "'" & Item & "'"
                If Array.IndexOf(cSearch.szDescriptors, Item) < cSearch.szDescriptors.Length - 1 Then
                    szParamString &= " OR EventName = "
                End If
            Next
            szParamString &= ")"
        End If

        Dim szSearchString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szParamString

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
        GetEventCount = 0

        Try
            da.Fill(dt)
            GetEventCount = dt.Rows.Count
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
        dt.Dispose()
        da.Dispose()

        Return GetEventCount
    End Function
 
    Public Function GetPlayDuration(ByVal szSearchString As String) As Integer


        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
        GetPlayDuration = 0

        Try
            da.Fill(dt)
            GetPlayDuration = dt.Rows.Count
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
        dt.Dispose()
        da.Dispose()

        Return GetPlayDuration
    End Function

    Public Function GetEventNameArrayFromPlay(ByVal DbPlayNumber As Long, ByVal szGameID As String, ByVal szTimeCriteria As String) As String()
        'Returns a list of all event names from an ID that is part of a play.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szEventsArray() As String = Nothing
        Dim EventCount As Integer = -1

        Dim SQL As String = "SELECT DISTINCT EventName FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '"
        SQL = SQL & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & "' AND PlayNumber = " & DbPlayNumber

        dbName.Open()

        Dim strSQL As New OleDbCommand(SQL, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        If dbReader.HasRows Then
            Do While dbReader.Read()
                If Not dbReader.IsDBNull(dbReader.GetOrdinal("EventName")) Then
                    EventCount += 1
                    ReDim Preserve szEventsArray(EventCount)
                    szEventsArray(EventCount) = dbReader.Item("EventName")
                End If
            Loop
        End If
        dbName.Close()

        Return szEventsArray
    End Function

    Private Function VerifyEventNameRegion(ByVal EventNames() As String, ByVal DbPlayNumber As Long, ByVal szGameID As String, ByVal Regions() As tRegion) As Boolean
        'Returns true if EventNames occured in specified regions.
        If Regions Is Nothing Then Return True
        ' If EventNames Is Nothing Then Return False

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim SQL As String = "SELECT Region FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '"
        SQL = SQL & szGameID & "' AND PlayNumber = " & DbPlayNumber

        If Not EventNames Is Nothing Then
            SQL = SQL & " AND ("
            Dim n As Integer = 1
            For Each item As String In EventNames
                SQL = SQL & "EventName = '" & item & "'"
                If n < EventNames.Length Then
                    SQL = SQL & " OR "
                    n += 1
                End If
            Next
            SQL = SQL & ")"
        End If

        dbName.Open()

        Dim strSQL As New OleDbCommand(SQL, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        If dbReader.HasRows Then
            Do While dbReader.Read()

                For Each region As tRegion In Regions
                    If dbReader.Item("Region") = region Then
                        dbName.Close()
                        Return True
                    End If
                Next
            Loop
        End If
        dbName.Close()

        Return False

    End Function

    Private Function VerifyEventNamesSimple(ByVal szEventName() As String, Optional ByVal DescriptorList() As String = Nothing) As Boolean
        If szEventName Is Nothing Then Return False

        'NB: a set of matching descriptors can be passed into this routine directly, or else, it can be derived here.
        If DescriptorList Is Nothing Then DescriptorList = GetCheckedDescriptorList(True)

        For Each item As String In szEventName
            For Each descriptor As String In DescriptorList
                If item = descriptor Then
                    Return True

                End If
            Next
        Next

        Return False


    End Function

    Private Function VerifyEventNamesAdvanced(ByVal EventsFound() As String, ByVal AdvancedSearchArray As AdvancedSearchCriteria) As Boolean
        If EventsFound Is Nothing Then Return False
        If AdvancedSearchArray.EventNameSet Is Nothing Then Return True

        'NB: a set of matching descriptors can be passed into this routine directly, or else, it can be derived here.
        'It might be passed to the routine if from an advanced or automated search where the frmAnalysis form is not visible or active.

        'First, find all AND events that must be in the array.
        With AdvancedSearchArray

            Dim HasCompulsoryEvents As Boolean = False
            Dim HasRegionCriteria As Boolean = False
            If Not AdvancedSearchArray.Regions Is Nothing Then HasRegionCriteria = True

            ' If AdvancedSearchArray.EventNameSet.Length = 1 Then Return True


            For i As Integer = 0 To .EventNameSet.GetUpperBound(0)

                If .EventNameBoolean(i) Then
                    'This item is compulsory.
                    HasCompulsoryEvents = True
                    Dim MatchFound As Boolean = False
                    For Each item As String In EventsFound
                        If item = .EventNameSet(i) Then
                            MatchFound = True
                            Exit For
                        End If
                    Next

                    If Not MatchFound Then
                        'No match was found for a compulsory eventname.
                        Return False
                    End If
                End If
            Next

            'Get this far and all of the compulsory events were found, or all of the events are optionals (OR).
            'Return true is all compulsory events were found since that is the minimum requirement.
            If HasCompulsoryEvents Then
                Return True
            End If

            'Now check for at least one of the optionals.

            For i As Integer = 0 To .EventNameSet.GetUpperBound(0)

                'If Not .EventNameBoolean(i) Then
                'This item is optional.
                For Each item As String In EventsFound
                    If item = .EventNameSet(i) Then
                        Return True
                    End If
                Next
                ' End If
            Next


        End With

        'If the routine gets this far then no cumpulsory matches were found, and no optionals were found.
        Return False


    End Function

    Public Function GetCheckedDescriptorList(Optional ByVal bNoWildcard As Boolean = False) As String()
        Dim szTemp() As String = Nothing
        Dim CheckedItems As CheckedListBox.CheckedItemCollection = frmAnalysis.lstDescriptors.CheckedItems

        'if all of the items are checkd then redim to a single string "*"
        If CheckedItems.Count = frmAnalysis.lstDescriptors.Items.Count And Not bNoWildcard Then
            ReDim szTemp(0)
            szTemp(0) = "*"
            Return szTemp

        Else
            'Otherise, gather checked items and set to array.
            ReDim szTemp(CheckedItems.Count - 1)

            For i As Integer = 0 To CheckedItems.Count - 1
                szTemp(i) = CheckedItems(i)
            Next
        End If

        Return szTemp

    End Function

    Public Function GetDescriptorListSQL() As String
        Dim res As String = Nothing

        If AdvancedSearchIsActive = True Then
            If Not AdvancedSearch.EventNameSet Is Nothing Then
                res = " AND ("
                For Each item As String In AdvancedSearch.EventNameSet
                    res = res & "EventName = '" & item & "'"
                    If Array.IndexOf(AdvancedSearch.EventNameSet, item) < AdvancedSearch.EventNameSet.Length - 1 Then
                        res = res & " OR "
                    Else
                        res = res & ")"
                    End If
                Next
            End If

        Else

            'Dim szTemp() As String = GetCheckedDescriptorList(True) 
            Dim szTemp() As String = GetCheckedDescriptorList(False)    'Set to false to save time 19/11/08.  Not sure of implications for other things???


            If szTemp.Length > 0 Then

                If szTemp(0) = "*" Then
                    res = Nothing
                Else
                    res = " AND ("
                    For Each item As String In szTemp
                        res = res & "EventName = '" & item & "'"
                        If Array.IndexOf(szTemp, item) < szTemp.Length - 1 Then
                            res = res & " OR "
                        Else
                            res = res & ")"
                        End If
                    Next
                End If

            End If
        End If

        Return res
    End Function

    Public Function GetSearchingRecordsCount(ByVal szSearchString As String) As Integer
        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
        Try
            da.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
        da.Dispose()
    End Function

    Public Function GetXTabsFrequency2(ByVal szGameID() As String, ByVal szFieldX As String, ByVal ItemsX() As String, ByVal szFieldY As String, ByVal ItemsY() As String, _
    Optional ByVal ExcludeList() As String = Nothing, Optional ByVal ParameterString As String = Nothing, Optional ByVal ReturnSupport As Boolean = False) As MatrixItem(,)

        '*  
        '*  Returns a matrix of values relating to X and Y fields
        '*  - Default is to return frequencies
        '*  - ReturnSupport option returns the % of all plays that relates to each frequency node.
        '*  

        'Compile frequency matrix for two fields.
        'Event count +=1 when items union in a given play.

        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        Dim SearchTime As New Stopwatch
        Stopwatch.StartNew()

        '1. compile matrix.
        nValue = 0
        Dim FreqMatrix(,) As MatrixItem = Nothing
        Dim szTable As String = Nothing
        ReDim FreqMatrix(ItemsX.Length - 1, ItemsY.Length - 1)


        '2. Open database.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()


        '3. Create search string prefix
        Dim szSearchString As String = Nothing

        'Compile multiple gameID string
        Dim szGameString As String = "(PathData.GameID = "
        For Each Game As String In szGameID
            szGameString &= "'" & Game & "'"
            If Array.IndexOf(szGameID, Game) < szGameID.Length - 1 Then
                szGameString &= " OR PathData.GameID = "
            End If
        Next
        szGameString &= ")"

        '4. Get total number of transactions for support option (if selected) - this only works for 
        Dim nSupport(ItemsX.Length - 1) As Single
        Dim iTransactions As Integer = 0
        If ReturnSupport Then
            szSearchString = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString
            iTransactions = GetDistinctIDs(szSearchString & ParameterString)
        End If


        '5. Create table data.
        frmMain.toolProgressBar.Maximum = ItemsX.Length
        frmMain.toolActionStatus.Text = "Generating Association Matrix..."
        SearchTime.Start()

        If szFieldX = "Event Name" And szFieldY = "Event Name" Then
            'NB: EventName x EventName is different from all other table modes because the search function required must
            'find multiple DB entries with matching a PlayNumber. This is complicated by the repetition in PlayNumber
            'over GameIDs and TimeCriteria's.  Therefore, each matching transaction is stored with the PlayNumber, GameID and
            'TimeCriteria, followed by a sequential search for other transactions matching those parameters.

            For Each EventX As String In ItemsX
                frmMain.toolProgressBar.Value = Array.IndexOf(ItemsX, EventX)
                Application.DoEvents()

                Dim nX As Integer = Array.IndexOf(ItemsX, EventX)

                'a. Find matching plays that include the EventX.
                szSearchString = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString & " AND EventName = '" & EventX & "'" & ParameterString
                'Get column support for ItemX
                If ReturnSupport Then nSupport(nX) = GetDistinctIDs(szSearchString) / iTransactions

                strSQL = New OleDbCommand(szSearchString, dbName)
                Try
                    dbReader = strSQL.ExecuteReader()
                    If dbReader.HasRows Then
                        'b. Loop through matches with EventX, searching for possible Y matches.
                        Do While dbReader.Read()

                            Dim szIDSearchString As String = "SELECT PathData.ID, EventName FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & _
                                "PathData.GameID = '" & dbReader.Item("GameID") & "' AND TimeCriteria = '" & dbReader.Item("TimeCriteria") & _
                                "' AND PlayNumber = " & dbReader.Item("PlayNumber")
                            Dim strIDSQL = New OleDbCommand(szIDSearchString, dbName)

                            Try
                                'c. Check EventName matches in existing array of Y items.
                                Dim dbIDReader = strIDSQL.ExecuteReader()
                                If dbIDReader.HasRows Then
                                    Do While dbIDReader.Read()
                                        Dim nY As Integer = Array.IndexOf(ItemsY, dbIDReader.Item("EventName"))
                                        If nY >= 0 Then
                                            FreqMatrix(nX, nY).Value += 1
                                            ReDim Preserve FreqMatrix(nX, nY).dbID(FreqMatrix(nX, nY).Value)
                                            FreqMatrix(nX, nY).dbID(FreqMatrix(nX, nY).Value - 1) = dbIDReader.Item("ID")
                                        End If
                                    Loop
                                End If

                            Catch ex As Exception

                            End Try
                        Loop
                    End If
                Catch ex As Exception
                    Return Nothing
                End Try

                strSQL = Nothing
                dbReader = Nothing
            Next

        Else
            'NB: The alternative to the EventName x EventName format is to find coincident matches within the same
            'records.  For instance: EventName = a$ AND TimeCriteria = b$.
            'Therefore, no need to find sets of matching PlayNumbers first.

            Dim szExclude As String = ""

            If Not ExcludeList Is Nothing Then
                szExclude = " AND NOT ("
                For Each item As String In ExcludeList
                    szExclude &= "EventName = '" & item & "'"
                    If Array.IndexOf(ExcludeList, item) < ExcludeList.Length - 1 Then
                        szExclude = szExclude & " OR "
                    End If
                Next
                szExclude &= ")"
            End If

            For Each EventX As String In ItemsX
                frmMain.toolProgressBar.Value = Array.IndexOf(ItemsX, EventX)
                Application.DoEvents()

                Dim nX As Integer = Array.IndexOf(ItemsX, EventX)
                For Each EventY As String In ItemsY
                    'a. Create base-string by field types.
                    Dim BaseString As String = "SELECT DISTINCT Pathdata.ID, PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString
                    If szFieldX = "Outcome Type" Or szFieldX = "Region" Then
                        szSearchString = BaseString & szExclude & " AND " & TranslateField(szFieldX, True) & " = " & EventX
                    Else
                        szSearchString = BaseString & szExclude & " AND " & TranslateField(szFieldX, True) & " = '" & EventX & "'"
                    End If

                    'Get column support for ItemX
                    If ReturnSupport Then nSupport(nX) = GetDistinctIDs(szSearchString & ParameterString) / iTransactions

                    If szFieldY = "Outcome Type" Or szFieldY = "Region" Then
                        szSearchString &= " AND " & TranslateField(szFieldY, True) & " = " & EventY
                    Else
                        szSearchString &= " AND " & TranslateField(szFieldY, True) & " = '" & EventY & "'"
                    End If

                    strSQL = New OleDbCommand(szSearchString & ParameterString, dbName)

                    Try
                        'b. Loop through matches and add to matrix.
                        dbReader = strSQL.ExecuteReader()
                        If dbReader.HasRows Then
                            Do While dbReader.Read()
                                Dim nY As Integer = Array.IndexOf(ItemsY, EventY)
                                If nY >= 0 Then
                                    FreqMatrix(nX, nY).Value += 1
                                    ReDim Preserve FreqMatrix(nX, nY).dbID(FreqMatrix(nX, nY).Value)
                                    FreqMatrix(nX, nY).dbID(FreqMatrix(nX, nY).Value - 1) = dbReader.Item("ID")
                                End If
                            Loop
                        End If
                    Catch ex As Exception
                        'Return Nothing
                    End Try
                Next
            Next
        End If

        dbName.Close()

        If ReturnSupport Then
            'Modify frematrix as function: support = x ==> y (%)
            FreqMatrix = CalculateSupport(FreqMatrix, iTransactions, nSupport)
        End If

        SearchTime.Stop()


        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds."

        Return FreqMatrix

    End Function

    Public Function GetXTabsItemSets2(ByVal szGameID() As String, ByVal szFieldY As String, ByVal ItemsY() As String, ByVal minSupport As Integer) As ItemSet()

        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        '1. Open database and compile gamestring
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        '1.1. Compile multiple gameID string
        Dim szGameString As String = "(PathData.GameID = "
        For Each Game As String In szGameID
            szGameString &= "'" & Game & "'"
            If Array.IndexOf(szGameID, Game) < szGameID.Length - 1 Then
                szGameString &= " OR PathData.GameID = "
            End If
        Next
        szGameString &= ")"

        '2. Prepare an array of matching playnumbers.
        Dim szSearchString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString
        Dim nPlayNumber() As Integer = Nothing
        Dim nGameID() As String = Nothing
        Dim nTimeCriteria() As String = Nothing
        strSQL = New OleDbCommand(szSearchString, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Dim q As Integer = -1
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        q += 1
                        ReDim Preserve nPlayNumber(q)
                        ReDim Preserve nGameID(q)
                        ReDim Preserve nTimeCriteria(q)
                        nPlayNumber(q) = dbReader.Item("PlayNumber")
                        nGameID(q) = dbReader.Item("GameID")
                        nTimeCriteria(q) = dbReader.Item("TimeCriteria")
                    End If
                Loop
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        '3.  Create a temporary flat record structure for each playnumber.
        Dim Events() As Items = Nothing
        If nPlayNumber.Length > 0 Then
            ReDim Events(nPlayNumber.GetUpperBound(0))

            frmMain.toolProgressBar.Maximum = nPlayNumber.GetUpperBound(0)
            frmMain.toolActionStatus.Text = "Generating Event Matrix..."

            Application.DoEvents()

            For i As Integer = 0 To nPlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = i

                szSearchString = "SELECT DISTINCT EventName FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PlayNumber = " & nPlayNumber(i) & _
                " AND PathData.GameID = '" & nGameID(i) & "' AND TimeCriteria = '" & nTimeCriteria(i) & "'"

                Events(i).List = GetEventNames(szSearchString)
                Events(i).PlayNumber = nPlayNumber(i)
                Events(i).GameID = nGameID(i)
                Events(i).TimeCriteria = nTimeCriteria(i)
            Next
        End If

        If Events Is Nothing Then Return Nothing

        ''2. Prepare an array of matching event names.
        '    Array.Sort(ItemsY)
        Dim ItemSets(ItemsY.Length - 1) As ItemSet
        Dim j As Integer = 0
        For Each item As String In ItemsY
            ReDim Preserve ItemSets(j).ItemName(0)
            ReDim Preserve ItemSets(j).GameID(0)
            ReDim Preserve ItemSets(j).PlayNumber(0)
            ReDim Preserve ItemSets(j).TimeCriteria(0)

            ItemSets(j).ItemName(0) = item

            For k As Integer = 0 To Events.GetUpperBound(0)
                If Not Events(k).List Is Nothing Then
                    If Array.IndexOf(Events(k).List, item) >= 0 Then
                        ItemSets(j).ItemSetFrequency += 1
                        ItemSets(j).GameID(0) = Events(k).GameID
                        ItemSets(j).PlayNumber(0) = Events(k).PlayNumber
                        ItemSets(j).TimeCriteria(0) = Events(k).TimeCriteria
                    End If
                End If
            Next
            j += 1
        Next

        '4.  Iteratively reduce datset to supported combinations.
        Dim IterateDone As Boolean = False
        Dim IterateCount As Integer = 0
        Dim ReturnItemSet() As ItemSet = Nothing

        Do While IterateCount < 15
            Dim n As Integer = -1
            Dim tItemSet() As ItemSet = ItemSets    'Temp dataset
            Dim ActiveItems() As String = ItemSets(0).ItemName  'Contains the items to be iterated with.
            Erase ItemSets

            'Reduce set to itemsets >= minimum support and compile new itemsets
            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                If tItemSet(i).ItemSetFrequency >= minSupport Then
                    n += 1
                    ReDim Preserve ItemSets(n)
                    ItemSets(n) = tItemSet(i)

                    For Each item As String In ItemSets(n).ItemName
                        If Not Array.IndexOf(ActiveItems, item) >= 0 Then
                            ReDim Preserve ActiveItems(ActiveItems.Length)
                            ActiveItems(ActiveItems.Length - 1) = item
                        End If
                    Next
                End If
            Next

            If ItemSets Is Nothing Then
                Exit Do
            Else
                If IterateCount = 1 Then
                    ReturnItemSet = ItemSets
                ElseIf IterateCount > 1 Then
                    Dim c As Integer = ReturnItemSet.Length
                    ReDim Preserve ReturnItemSet(c + ItemSets.Length - 1)

                    For Each item As ItemSet In ItemSets
                        ReturnItemSet(c) = item
                        c += 1
                    Next
                End If
            End If

            Array.Sort(ActiveItems)

            tItemSet = ItemSets

            n = -1
            Erase ItemSets

            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                'Get last item in itemset.
                Dim s As Integer = tItemSet(i).ItemName.GetUpperBound(0)
                Dim szLastItem As String = tItemSet(i).ItemName(s)
                'Get next item in sequence from original itemset.
                Dim k As Integer = Array.IndexOf(ActiveItems, szLastItem)

                If k < ActiveItems.Length - 1 Then
                    'There is at least one more item candidate.
                    For m As Integer = k + 1 To ActiveItems.Length - 1
                        Dim szExtraItem As String = ActiveItems(m)

                        n += 1
                        ReDim Preserve ItemSets(n)
                        ItemSets(n).ItemName = tItemSet(i).ItemName
                        ItemSets(n).ItemSetFrequency = 0
                        'ItemSets(n).GameID = tItemSet(i).GameID
                        'ItemSets(n).PlayNumber = tItemSet(i).PlayNumber
                        'ItemSets(n).TimeCriteria = tItemSet(i).TimeCriteria

                        ReDim Preserve ItemSets(n).ItemName(ItemSets(n).ItemName.Length)
                        ItemSets(n).ItemName(ItemSets(n).ItemName.Length - 1) = szExtraItem

                        'Now find matches.
                        'a. get a list of playnumber/gameid/timecriteria instances.

                        'Scroll through each play.
                        For Each playset As Items In Events
                            If Not playset.List Is Nothing Then
                                If playset.List.Length >= ItemSets(n).ItemName.Length Then

                                    Dim ItemFound As Boolean = True

                                    'Check if events from this play match any itemsets.
                                    For Each itemstring As String In ItemSets(n).ItemName
                                        If Array.IndexOf(playset.List, itemstring) < 0 Then
                                            'No match - move on.
                                            ItemFound = False
                                            Exit For
                                        End If
                                        If Not ItemFound Then Exit For
                                    Next
                                    If ItemFound Then
                                        ItemSets(n).ItemSetFrequency += 1
                                        Dim pBool As Boolean = False
                                        If Not ItemSets(n).PlayNumber Is Nothing Then
                                            For id As Integer = 0 To ItemSets(n).PlayNumber.GetUpperBound(0)
                                                If playset.PlayNumber = ItemSets(n).PlayNumber(id) Then
                                                    If playset.GameID = ItemSets(n).GameID(id) And playset.TimeCriteria = ItemSets(n).TimeCriteria(id) Then
                                                        pBool = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If

                                        If Not pBool Then
                                            'if a unique addition then add to array.
                                            Dim f As Integer = 0
                                            If Not ItemSets(n).GameID Is Nothing Then f = ItemSets(n).GameID.Length
                                            ReDim Preserve ItemSets(n).GameID(f)
                                            ReDim Preserve ItemSets(n).PlayNumber(f)
                                            ReDim Preserve ItemSets(n).TimeCriteria(f)
                                            ItemSets(n).GameID(f) = playset.GameID
                                            ItemSets(n).PlayNumber(f) = playset.PlayNumber
                                            ItemSets(n).TimeCriteria(f) = playset.TimeCriteria
                                        End If

                                    End If
                                End If
                            End If
                        Next


                    Next
                End If
            Next
            If ItemSets Is Nothing Then Exit Do
            IterateCount += 1
        Loop

        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds. (" & IterateCount.ToString & " iterations)"

        dbName.Close()
        Return ReturnItemSet
    End Function


    Public Function GetQuadrantOriginFrequencyWithSearchString(ByVal szSearchString As String, _
        Optional ByVal szStatusMessage As String = "Getting origin frequencies") As ItemSet()

        '1. Open database and compile gamestring
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        '2. Prepare an array of matching playnumbers.
        Dim nPlayNumber() As Integer = Nothing
        Dim nGameID() As String = Nothing
        Dim nTimeCriteria() As String = Nothing
        strSQL = New OleDbCommand(szSearchString, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Dim q As Integer = -1
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        q += 1
                        ReDim Preserve nPlayNumber(q)
                        ReDim Preserve nGameID(q)
                        ReDim Preserve nTimeCriteria(q)
                        nPlayNumber(q) = dbReader.Item("PlayNumber")
                        nGameID(q) = dbReader.Item("GameID")
                        nTimeCriteria(q) = dbReader.Item("TimeCriteria")
                    End If
                Loop
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        If nPlayNumber Is Nothing Then Return Nothing

        '3.  Create a temporary array with a structure for each playnumber.
        Dim Quadrants() As Items = Nothing
        If nPlayNumber.Length > 0 Then
            ReDim Quadrants(nPlayNumber.GetUpperBound(0))
            frmMain.toolProgressBar.Maximum = nPlayNumber.GetUpperBound(0)
            frmMain.toolActionStatus.Text = szStatusMessage
            Application.DoEvents()

            For i As Integer = 0 To nPlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = i

                szSearchString = "SELECT x, y FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PlayNumber = " & nPlayNumber(i) & _
                " AND PathData.GameID = '" & nGameID(i) & "' AND TimeCriteria = '" & nTimeCriteria(i) & "' AND Status = 0"

                'find origin.
                Quadrants(i).List = GetOriginQuadrant(szSearchString)
                Quadrants(i).PlayNumber = nPlayNumber(i)
            Next
        End If

        If Quadrants Is Nothing Then Return Nothing

        'Create a string-defined list of all quadrants.
        Dim z As Integer = 0
        Dim ItemsY() As String = Nothing
        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
            For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                ReDim Preserve ItemsY(z)
                ItemsY(z) = GetRowCharacter(y) & x.ToString
                z += 1
            Next
        Next

        ''2. Prepare an array of matching position instances by quadrant. (Support A U B)

        Dim ItemSets(ItemsY.Length - 1) As ItemSet
        Dim j As Integer = 0
        For Each item As String In ItemsY
            ReDim Preserve ItemSets(j).ItemName(0)
            ItemSets(j).ItemName(0) = item

            For k As Integer = 0 To Quadrants.GetUpperBound(0)
                If Not Quadrants(k).List Is Nothing Then
                    If Array.IndexOf(Quadrants(k).List, item) >= 0 Then
                        ItemSets(j).ItemSetFrequency += 1
                    End If
                End If
            Next
            j += 1
        Next

        Return ItemSets
    End Function


    Public Function GetQuadrantFrequencyCountsWithSearchString(ByVal szSearchString As String, _
        Optional ByVal szStatusMessage As String = "Getting quadrant frequencies") As ItemSet()

        '1. Open database and compile gamestring
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        '2. Prepare an array of matching playnumbers.
        Dim nPlayNumber() As Integer = Nothing
        Dim nGameID() As String = Nothing
        Dim nTimeCriteria() As String = Nothing
        strSQL = New OleDbCommand(szSearchString, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Dim q As Integer = -1
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        q += 1
                        ReDim Preserve nPlayNumber(q)
                        ReDim Preserve nGameID(q)
                        ReDim Preserve nTimeCriteria(q)
                        nPlayNumber(q) = dbReader.Item("PlayNumber")
                        nGameID(q) = dbReader.Item("GameID")
                        nTimeCriteria(q) = dbReader.Item("TimeCriteria")
                    End If
                Loop
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        If nPlayNumber Is Nothing Then Return Nothing

        '3.  Create a temporary array with a structure for each playnumber.
        Dim Quadrants() As Items = Nothing
        If nPlayNumber.Length > 0 Then
            ReDim Quadrants(nPlayNumber.GetUpperBound(0))
            frmMain.toolProgressBar.Maximum = nPlayNumber.GetUpperBound(0)
            frmMain.toolActionStatus.Text = szStatusMessage
            Application.DoEvents()

            For i As Integer = 0 To nPlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = i

                szSearchString = "SELECT x, y FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PlayNumber = " & nPlayNumber(i) & _
                " AND PathData.GameID = '" & nGameID(i) & "' AND TimeCriteria = '" & nTimeCriteria(i) & "'"

                'If this quadrant is different to the last quadrant.
                Quadrants(i).List = GetQuadrantSequences(szSearchString, True)
                Quadrants(i).PlayNumber = nPlayNumber(i)
            Next
        End If

        If Quadrants Is Nothing Then Return Nothing

        'Create a string-defined list of all quadrants.
        Dim z As Integer = 0
        Dim ItemsY() As String = Nothing
        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
            For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                ReDim Preserve ItemsY(z)
                ItemsY(z) = GetRowCharacter(y) & x.ToString
                z += 1
            Next
        Next

        ''2. Prepare an array of matching position instances by quadrant. (Support A U B)

        Dim ItemSets(ItemsY.Length - 1) As ItemSet
        Dim j As Integer = 0
        For Each item As String In ItemsY
            ReDim Preserve ItemSets(j).ItemName(0)
            ItemSets(j).ItemName(0) = item

            For k As Integer = 0 To Quadrants.GetUpperBound(0)
                If Not Quadrants(k).List Is Nothing Then
                    If Array.IndexOf(Quadrants(k).List, item) >= 0 Then
                        ItemSets(j).ItemSetFrequency += 1
                    End If
                End If
            Next
            j += 1
        Next

        Return ItemSets
    End Function


    Public Function GetAssociationRules(ByVal cSearch As SearchCriteria, _
        ByVal minSupport As Integer, _
        Optional ByVal boolOrigins As Boolean = False) As MatrixItem(,)

        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        Dim SearchTime As New Stopwatch
        SearchTime.Start()


        '1.1. Compile multiple gameID string
        Dim szParamString As String = "(PathData.GameID = "
        For Each Game As String In cSearch.szGameID
            szParamString &= "'" & Game & "'"
            If Array.IndexOf(cSearch.szGameID, Game) < cSearch.szGameID.Length - 1 Then
                szParamString &= " OR PathData.GameID = "
            End If
        Next
        szParamString &= ")"

        '1.2. Add Time criteria if selected
        If Not cSearch.szTimeCriterion Is Nothing Then
            szParamString &= " AND (TimeCriteria = '" & cSearch.szTimeCriterion(0) & "')"
        End If

        '1.3. Add Team criteria if selected
        If Not cSearch.szTeamName Is Nothing Then
            szParamString &= " AND (TeamName = "

            For Each Team As String In cSearch.szTeamName
                szParamString &= "'" & Team & "'"
                If Array.IndexOf(cSearch.szTeamName, Team) < cSearch.szTeamName.Length - 1 Then
                    szParamString &= " OR TeamName = "
                End If
            Next
            szParamString &= ")"

        End If




        Dim QuadrantSets(UserPrefs.clHorizontalQ - 1, UserPrefs.clVerticalQ - 1) As MatrixItem


        '1.4 Obtain total play count, unfiltered by descriptor selections
        Dim szTotalPlaysSearchString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szParamString
        Dim allPlaysCount As Double = CDbl(GetEventCount(szTotalPlaysSearchString))

        '*
        '*
        '*  first get support(A)
        '*
        '*
        Dim quadrantItemSets() As modSearchEngine.ItemSet = Nothing

        If boolOrigins = False Then
            'calculate transactions using ball movements
            quadrantItemSets = modSearchEngine.GetQuadrantFrequencyCountsWithSearchString(szTotalPlaysSearchString, "Calculating independent quadrant support A")
        Else
            'calculate transactions using the single play origin.
            quadrantItemSets = modSearchEngine.GetQuadrantOriginFrequencyWithSearchString(szTotalPlaysSearchString, "Calculating independent origin support A")
        End If

        For Each item As ItemSet In quadrantItemSets

            Dim szQuadrant As String = item.ItemName(0)
            'x value derived from char
            Dim y As Integer = GetRowIntegerFromCharacter(szQuadrant.Substring(0, 1))

            Dim x As Integer = Int(szQuadrant.Substring(1, item.ItemName(0).Length - 1))

            QuadrantSets(x, y).supportA = item.ItemSetFrequency

        Next



        '1.5. Add descriptors to string
        If Not cSearch.szDescriptors Is Nothing Then
            szParamString &= " AND (EventName = "
            For Each Item As String In cSearch.szDescriptors
                szParamString &= "'" & Item & "'"
                If Array.IndexOf(cSearch.szDescriptors, Item) < cSearch.szDescriptors.Length - 1 Then
                    szParamString &= " OR EventName = "
                End If
            Next
            szParamString &= ")"
        End If

        Dim szSearchString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szParamString

        '1.4 Obtain total plays including descriptors
        Dim supportB As Double = CDbl(GetEventCount(szSearchString))


        '*
        '*
        '*  Now get support(A u B)  - NB: Support(B) is also the length of the Quadrants array.
        '*
        '*

        quadrantItemSets = Nothing

        If boolOrigins = False Then
            'calculate transactions using ball movements
            quadrantItemSets = modSearchEngine.GetQuadrantFrequencyCountsWithSearchString(szSearchString, "Calculating conditional quadrant support B")
        Else
            'calculate transactions using the single play origin.
            quadrantItemSets = modSearchEngine.GetQuadrantOriginFrequencyWithSearchString(szSearchString, "Calculating conditional origin quadrant support B")
        End If



        For Each item As ItemSet In quadrantItemSets

            Dim szQuadrant As String = item.ItemName(0)
            'x value derived from char
            Dim y As Integer = GetRowIntegerFromCharacter(szQuadrant.Substring(0, 1))

            Dim x As Integer = Int(szQuadrant.Substring(1, item.ItemName(0).Length - 1))

            QuadrantSets(x, y).supportAUB = item.ItemSetFrequency
            QuadrantSets(x, y).supportB = supportB
            QuadrantSets(x, y).n = allPlaysCount

        Next

        Return QuadrantSets
    End Function


    Public Function GetQuadrantTransitions(ByVal cSearch As SearchCriteria, ByVal minSupport As Integer, ByVal seqLength As Integer) As ItemSet()

        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        '1. Open database and compile gamestring
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        '1.1. Compile multiple gameID string
        Dim szParamString As String = "(PathData.GameID = "
        For Each Game As String In cSearch.szGameID
            szParamString &= "'" & Game & "'"
            If Array.IndexOf(cSearch.szGameID, Game) < cSearch.szGameID.Length - 1 Then
                szParamString &= " OR PathData.GameID = "
            End If
        Next
        szParamString &= ")"

        '1.2. Add Time criteria if selected
        If Not cSearch.szTimeCriterion Is Nothing Then
            szParamString &= " AND (TimeCriteria = '" & cSearch.szTimeCriterion(0) & "')"
        End If

        '1.3. Add Team criteria if selected
        If Not cSearch.szTeamName Is Nothing Then
            szParamString &= " AND (TeamName = '" & cSearch.szTeamName(0) & "')"
        End If

        '1.4. Add Team criteria if selected
        If Not cSearch.uOutcomes Is Nothing Then
            If cSearch.uOutcomes(0) <> OutcomeType.outAll Then szParamString &= " AND (Outcome = " & cSearch.uOutcomes(0) & ")"
        End If

        '1.5. Add descriptors to string
        If Not cSearch.szDescriptors Is Nothing Then
            szParamString &= " AND (EventName = "
            For Each Item As String In cSearch.szDescriptors
                szParamString &= "'" & Item & "'"
                If Array.IndexOf(cSearch.szDescriptors, Item) < cSearch.szDescriptors.Length - 1 Then
                    szParamString &= " OR EventName = "
                End If
            Next
            szParamString &= ")"
        End If

        '2. Prepare an array of matching playnumbers.
        Dim szSearchString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szParamString
        Dim nPlayNumber() As Integer = Nothing
        Dim nGameID() As String = Nothing
        Dim nTimeCriteria() As String = Nothing
        strSQL = New OleDbCommand(szSearchString, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Dim q As Integer = -1
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        q += 1
                        ReDim Preserve nPlayNumber(q)
                        ReDim Preserve nGameID(q)
                        ReDim Preserve nTimeCriteria(q)
                        nPlayNumber(q) = dbReader.Item("PlayNumber")
                        nGameID(q) = dbReader.Item("GameID")
                        nTimeCriteria(q) = dbReader.Item("TimeCriteria")
                    End If
                Loop
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        If nPlayNumber Is Nothing Then Return Nothing

        '3.  Create a temporary flat record structure for each playnumber.
        Dim Quadrants() As Items = Nothing
        If nPlayNumber.Length > 0 Then
            ReDim Quadrants(nPlayNumber.GetUpperBound(0))
            frmMain.toolProgressBar.Maximum = nPlayNumber.GetUpperBound(0)
            frmMain.toolActionStatus.Text = "Generating Quadrant Matrix..."
            Application.DoEvents()

            For i As Integer = 0 To nPlayNumber.GetUpperBound(0)
                frmMain.toolProgressBar.Value = i

                szSearchString = "SELECT x, y FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PlayNumber = " & nPlayNumber(i) & _
                " AND PathData.GameID = '" & nGameID(i) & "' AND TimeCriteria = '" & nTimeCriteria(i) & "'"

                'If this quadrant is different to the last quadrant.
                Quadrants(i).List = GetQuadrantSequences(szSearchString)
            Next
        End If

        If Quadrants Is Nothing Then Return Nothing

        Dim z As Integer = 0
        Dim ItemsY() As String = Nothing
        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
            For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                ReDim Preserve ItemsY(z)
                ItemsY(z) = GetRowCharacter(y) & x.ToString
                z += 1
            Next
        Next

        ''2. Prepare an array of matching event names.
        Dim ItemSets(ItemsY.Length - 1) As ItemSet
        Dim j As Integer = 0
        For Each item As String In ItemsY
            ReDim Preserve ItemSets(j).ItemName(0)
            ItemSets(j).ItemName(0) = item

            For k As Integer = 0 To Quadrants.GetUpperBound(0)
                If Not Quadrants(k).List Is Nothing Then
                    If Array.IndexOf(Quadrants(k).List, item) >= 0 Then
                        ItemSets(j).ItemSetFrequency += 1
                    End If
                End If
            Next
            j += 1
        Next

        '4.  Iteratively reduce datset to supported combinations.
        Dim IterateDone As Boolean = False
        Dim IterateCount As Integer = 0
        Dim ReturnItemSet() As ItemSet = Nothing

        Do While IterateCount < 30
            Dim n As Integer = -1
            Dim tItemSet() As ItemSet = ItemSets    'Temp dataset
            Dim ActiveItems() As String = ItemSets(0).ItemName  'Contains the items to be iterated with.
            Erase ItemSets

            'Reduce set to itemsets >= minimum support and compile new itemsets
            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                If tItemSet(i).ItemSetFrequency >= minSupport Then
                    n += 1
                    ReDim Preserve ItemSets(n)
                    ItemSets(n) = tItemSet(i)

                    For Each item As String In ItemSets(n).ItemName
                        If Not Array.IndexOf(ActiveItems, item) >= 0 Then
                            ReDim Preserve ActiveItems(ActiveItems.Length)
                            ActiveItems(ActiveItems.Length - 1) = item
                        End If
                    Next
                End If
            Next

            If ItemSets Is Nothing Then
                Exit Do
            Else
                If IterateCount = 1 Then
                    ReturnItemSet = ItemSets
                ElseIf IterateCount > 1 Then
                    Dim c As Integer = ReturnItemSet.Length
                    ReDim Preserve ReturnItemSet(c + ItemSets.Length - 1)

                    For Each item As ItemSet In ItemSets
                        ReturnItemSet(c) = item
                        c += 1
                    Next
                End If
            End If

            Array.Sort(ActiveItems)

            tItemSet = ItemSets

            n = -1
            Erase ItemSets

            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                'Get last item in itemset.
                Dim s As Integer = tItemSet(i).ItemName.GetUpperBound(0)
                Dim szLastItem As String = tItemSet(i).ItemName(s)
                'Get next item in sequence from original itemset.
                Dim k As Integer = Array.IndexOf(ActiveItems, szLastItem)

                If k < ActiveItems.Length - 1 Then
                    'There is at least one more item candidate.
                    For m As Integer = k + 1 To ActiveItems.Length - 1
                        Dim szExtraItem As String = ActiveItems(m)

                        n += 1
                        ReDim Preserve ItemSets(n)
                        ItemSets(n).ItemName = tItemSet(i).ItemName
                        ItemSets(n).ItemSetFrequency = 0
                        ReDim Preserve ItemSets(n).ItemName(ItemSets(n).ItemName.Length)
                        ItemSets(n).ItemName(ItemSets(n).ItemName.Length - 1) = szExtraItem


                        'Now find matches.
                        'a. get a list of playnumber/gameid/timecriteria instances.

                        'Scroll through each play.
                        For Each playset As Items In Quadrants
                            If Not playset.List Is Nothing Then
                                If playset.List.Length >= ItemSets(n).ItemName.Length Then

                                    Dim ItemFound As Boolean = True

                                    'Check if events from this play match any itemsets.
                                    For Each itemstring As String In ItemSets(n).ItemName
                                        If Array.IndexOf(playset.List, itemstring) < 0 Then
                                            'No match - move on.
                                            ItemFound = False
                                            Exit For
                                        End If
                                        If Not ItemFound Then Exit For
                                    Next
                                    If ItemFound Then
                                        ItemSets(n).ItemSetFrequency += 1

                                    End If
                                End If
                            End If
                        Next


                    Next
                End If
            Next
            If ItemSets Is Nothing Then Exit Do
            IterateCount += 1
        Loop

        Dim tempFreq As ItemSet() = ReturnItemSet
        Erase ReturnItemSet
        Dim fCount As Integer = 0

        If Not tempFreq Is Nothing Then
            For Each item As ItemSet In tempFreq
                If item.ItemName.Length >= seqLength Then
                    ReDim Preserve ReturnItemSet(fCount)
                    ReturnItemSet(fCount) = item
                    fCount += 1
                End If
            Next
        End If

        ' Reorder data to natural sequence.
        If Not ReturnItemSet Is Nothing Then
            frmMain.toolProgressBar.Maximum = ReturnItemSet.Length
            frmMain.toolActionStatus.Text = "Finalising Matrix..."
            For Each item As ItemSet In ReturnItemSet
                frmMain.toolProgressBar.Value = Array.IndexOf(ReturnItemSet, item)
                Application.DoEvents()
                For Each q As Items In Quadrants
                    If CompareArrays(item.ItemName, q.List) Then
                        'match found
                        ReturnItemSet(Array.IndexOf(ReturnItemSet, item)).ItemName = ReOrderArray(item.ItemName, q.List)
                        Exit For
                    End If
                Next
            Next
        End If



        SearchTime.Stop()
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Last search time: " & SearchTime.Elapsed.Seconds + (SearchTime.Elapsed.Milliseconds / 1000).ToString & " seconds. (" & IterateCount.ToString & " iterations)"

        dbName.Close()


        Return ReturnItemSet
    End Function

    Public Function ReOrderArray(ByVal arr As String(), ByVal comp As String()) As String()
        Dim retString(arr.Length - 1) As String
        Dim n As Integer = 0

        For Each item As String In comp
            If Array.IndexOf(arr, item) >= 0 And Not Array.IndexOf(retString, item) >= 0 Then
                retString(n) = item
                n += 1
            End If
        Next
        Return retString
    End Function

    Public Function CompareArrays(ByVal arr1 As String(), ByVal arr2 As String()) As Boolean
        'Return true is all arrays identical in items and size.
        'If arr1.Length <> arr2.Length Then Return False

        For Each item As String In arr1
            If Not Array.IndexOf(arr2, item) > 0 Then Return False
        Next
        Return True
    End Function

    Public Function GetRowCharacter(ByVal y As Integer) As String
        Dim ch As Integer = 65 + y
        Return Chr(ch).ToString
    End Function

    Public Function GetRowIntegerFromCharacter(ByVal ch As String) As Integer
        Return Asc(ch) - 65
    End Function

    Public Function GetOutcomeCountFromID(ByVal nID As Long) As Integer
        'Return team name string from record with ID = nID.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As New OleDbCommand("SELECT OutcomeCount FROM PathData WHERE ID = " & nID.ToString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim ret As Integer = 0
        dbName.Open()
        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    ret = dbReader.Item("OutcomeCount")
                    Exit Do
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try

        Return ret

    End Function

    Public Function GetTeamNameFromID(ByVal nID As Long) As String
        'Return team name string from record with ID = nID.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As New OleDbCommand("SELECT TeamName FROM PathData WHERE ID = " & nID.ToString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim ret As String = Nothing
        dbName.Open()
        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    ret = dbReader.Item("TeamName")
                    Exit Do
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try

        Return ret

    End Function

    Public Function GetTeamNamesFromGameID(ByVal szGameID As String) As String()
        'Return team names string from GameID.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As New OleDbCommand("SELECT DISINCT TeamName FROM PathData WHERE GameID = '" & szGameID & "'", dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim ret() As String = Nothing
        Dim n As Integer = 0
        dbName.Open()
        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    ReDim Preserve ret(n)
                    ret(n) = dbReader.Item("TeamName")
                    n += 1
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try

        Return ret

    End Function

    Public Function GetNearestIDbyTime(ByVal nTime As Double, ByVal szGameID As String, ByVal szTimeCriteria As String, ByVal szTeamName As String) As Long
        'Return Database ID for entry nearest to nTime.
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szString As String = "SELECT MIN(TimeCode) AS 'Nearest' FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & "' AND TimeCode >= " & nTime
        szString &= " AND TeamName = '" & szTeamName & "'"

        Dim strSQL As New OleDbCommand(szString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim HighTime As Double = Nothing
        Dim LowTime As Double = Nothing

        dbName.Open()

        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    HighTime = dbReader.GetValue(0)
                    Exit Do
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try

        If HighTime <> nTime Then
            'Now find the nearesr lower item.

            szString = "SELECT MAX(TimeCode) AS 'Nearest' FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & "' AND TimeCode <= " & nTime
            szString &= " AND TeamName = '" & szTeamName & "'"

            strSQL = New OleDbCommand(szString, dbName)
            dbReader = Nothing

            dbName.Open()

            Try
                dbReader = strSQL.ExecuteReader()
                If dbReader.HasRows Then
                    Do While dbReader.Read
                        LowTime = dbReader.GetValue(0)
                        Exit Do
                    Loop
                End If
                dbName.Close()

            Catch ex As Exception
                dbName.Close()
            End Try
        End If

        If (HighTime - nTime) < (nTime - LowTime) Then
            LowTime = HighTime
        End If

        szString = "SELECT DISTINCT ID FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & "' AND TimeCode = " & LowTime
        szString &= " AND TeamName = '" & szTeamName & "' ORDER BY ID"

        strSQL = New OleDbCommand(szString, dbName)
        dbReader = Nothing
        Dim ret As Long = Nothing

        dbName.Open()

        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    ret = dbReader.Item("ID")
                    Exit Do
                Loop
            End If
            dbName.Close()
        Catch ex As Exception
            dbName.Close()
        End Try

        Return ret
    End Function

    Public Function GetEventNames(ByVal szSearchString As String, Optional ByVal altConnectString As String = Nothing) As String()

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        If Not altConnectString Is Nothing Then
            dbName = New OleDbConnection(altConnectString)
        End If

        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim Events() As String = Nothing
        dbName.Open()

        Try
            Dim n As Integer = 0
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        ReDim Preserve Events(n)
                        Events(n) = dbReader.Item("EventName")
                        n += 1
                    End If
                Loop
            End If
            dbName.Close()
        Catch ex As Exception
            Return Nothing
        End Try
        Return Events
    End Function

    Public Function GetOriginQuadrant(ByVal szSearchString As String) As String()
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        'Return Nothing
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim Events() As String = Nothing
        dbName.Open()

        Try
            Dim n As Integer = 0
            Dim lastQuadrant As String = Nothing
            Dim lastPoint As Point = Nothing

            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        'First, get the current quadrant of ball contact.
                        Dim thisPoint As Point
                        thisPoint.X = CSng(dbReader.Item("x"))
                        thisPoint.Y = CSng(dbReader.Item("y"))

                        Dim szQuadrant As String = GetQuadrantString(thisPoint.X, thisPoint.Y)

                        ReDim Preserve Events(n)
                        Events(n) = szQuadrant
                        n += 1

                    End If
                Loop
            End If
            dbName.Close()
        Catch ex As Exception
            dbName.Close()
            Return Events
        End Try
        Return Events
    End Function

    Public Function GetQuadrantSequences(ByVal szSearchString As String, Optional ByVal includeBetweenQuadrantInvasions As Boolean = False) As String()
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        'Return Nothing
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Dim Events() As String = Nothing
        dbName.Open()

        Try
            Dim n As Integer = 0
            Dim lastQuadrant As String = Nothing
            Dim lastPoint As Point = Nothing

            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        'First, get the current quadrant of ball contact.
                        Dim thisPoint As Point
                        thisPoint.X = CSng(dbReader.Item("x"))
                        thisPoint.Y = CSng(dbReader.Item("y"))

                        Dim szQuadrant As String = GetQuadrantString(thisPoint.X, thisPoint.Y)

                        'Check if the last quadrant is different from the new.
                        If szQuadrant <> lastQuadrant Then
                            'Record new quadrant in the array of quadrant invasions.
                            ReDim Preserve Events(n)
                            Events(n) = szQuadrant
                            n += 1

                            'Now, if the "between-contact" quadrant invasions are required solve.
                            If includeBetweenQuadrantInvasions And Not lastPoint = Nothing Then
                                Dim invasions() As String = GetQuadrantInvasionsBetweenSectors(lastPoint, thisPoint)
                                For Each invasion As String In invasions
                                    ReDim Preserve Events(n)
                                    Events(n) = invasion
                                    n += 1
                                Next

                            End If

                            lastQuadrant = szQuadrant
                            lastPoint = thisPoint

                        End If
                    End If
                Loop
            End If
            dbName.Close()
        Catch ex As Exception
            dbName.Close()
            Return Events
        End Try
        Return Events
    End Function

    Public Function GetQuadrantInvasionsBetweenSectors(ByVal firstSector As Point, ByVal secondSector As Point) As String()

        'return all the quadrant sectors that a straight line passes through
        'between the first point and the second point.

        Dim quadrantPoint1 As Point = GetQuadrantAsPoint(firstSector.X, firstSector.Y)
        Dim quadrantPoint2 As Point = GetQuadrantAsPoint(secondSector.X, secondSector.Y)

        'first grab the low hanging fruit.
        'if the line remains in the same horizontal or vertical columns then
        'all we need is the sequence of sectors along the line
        If quadrantPoint1 = quadrantPoint2 Then Return Nothing

        Dim returnStrings() As String = Nothing
        Dim n As Integer = 0


        If Math.Abs(quadrantPoint2.X - quadrantPoint1.X) <= 1 And Math.Abs(quadrantPoint2.Y - quadrantPoint1.Y) >= 2 Then
            'the x plane doesn't change, but the y does, so we iterate through each value of y and return the valid sectors
            For y As Integer = Min(quadrantPoint1.Y, quadrantPoint2.Y) + 1 To Max(quadrantPoint1.Y, quadrantPoint2.Y) - 1
                ReDim Preserve returnStrings(n)
                ' returnStrings(n) = GetQuadrantString(quadrantPoint1.X, y)
                returnStrings(n) = GetRowCharacter(y) & quadrantPoint1.X
                n = n + 1
            Next
            Return returnStrings
        End If

        If Math.Abs(quadrantPoint2.X - quadrantPoint1.X) >= 2 And Math.Abs(quadrantPoint2.Y - quadrantPoint1.Y) <= 1 Then
            'the y plane doesn't change, but the x does, so we iterate through each value of x and return the valid sectors
            For x As Integer = Min(quadrantPoint1.X, quadrantPoint2.X) + 1 To Max(quadrantPoint1.X, quadrantPoint2.X) - 1
                ReDim Preserve returnStrings(n)
                'returnStrings(n) = GetQuadrantString(x, quadrantPoint2.Y)
                returnStrings(n) = GetRowCharacter(quadrantPoint2.Y) & x.ToString
                n = n + 1
            Next
            Return returnStrings
        End If

        'now we need to iterate through the widest spread of values to ensure we
        'return the optimal number of invaded sectors.

        Dim m As Single = (quadrantPoint2.Y - quadrantPoint1.Y) / (quadrantPoint2.X - quadrantPoint1.X)
        Dim c As Single = quadrantPoint2.Y - (quadrantPoint2.X * m)

        If Math.Abs(quadrantPoint1.X - quadrantPoint2.X) > Math.Abs(quadrantPoint1.Y - quadrantPoint2.Y) Then
            'the range of values across the x plane is greater, so we iterate through x and solve for y

            'iterate through x quadrants and solve y
            For x As Integer = Min(quadrantPoint1.X, quadrantPoint2.X) + 1 To Max(quadrantPoint1.X, quadrantPoint2.X) - 1
                'solve y
                Dim y As Integer = m * x + c
                ReDim Preserve returnStrings(n)
                returnStrings(n) = GetRowCharacter(y) & x.ToString
                n = n + 1
            Next

        Else
            'the range of values across the y plane is greater, so we iterate through y and solve for x

            'iterate through y quadrants and get x value
            For y As Integer = Min(quadrantPoint1.Y, quadrantPoint2.Y) + 1 To Max(quadrantPoint1.Y, quadrantPoint2.Y) - 1
                'solve x
                Dim x As Integer = (y - c) / m
                ReDim Preserve returnStrings(n)
                returnStrings(n) = GetRowCharacter(y) & x.ToString
                n = n + 1
            Next
        End If

        Return returnStrings

    End Function

    Function Max(ByVal value1 As Double, ByVal value2 As Double) As Double

        If (value1 > value2) Then
            Max = value1

        Else
            Max = value2

        End If

    End Function

    Function Min(ByVal value1 As Double, ByVal value2 As Double) As Double

        If (value1 < value2) Then
            Min = value1

        Else
            Min = value2

        End If

    End Function
    Public Function AddTempStringRecord(ByVal szArray() As String, ByVal szTableName As String) As Boolean
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As String = "INSERT INTO " & szTableName & " ("
        Dim n As Integer = 0
        For Each item As String In szArray
            strSQL &= "Event" & n.ToString
            If Array.IndexOf(szArray, item) < szArray.Length - 1 Then strSQL &= ", "
            n += 1
        Next
        strSQL &= ") VALUES ("
        n = 0
        For Each item As String In szArray
            strSQL &= "'" & item & "'"
            If Array.IndexOf(szArray, item) < szArray.Length - 1 Then strSQL &= ", "
            n += 1
        Next
        strSQL &= ")"

        Try
            Dim cmdSQL = New OleDbCommand(strSQL, dbName).ExecuteNonQuery
            dbName.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Function AddTempTable(ByVal szTableName As String, Optional ByVal FieldCount As Integer = 10) As Boolean
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim szCMD As String = "CREATE TABLE " & szTableName & " ("
        For i As Integer = 0 To FieldCount - 1
            szCMD &= "Event" & i.ToString & " CHAR(50)"
            If i < FieldCount Then szCMD &= ", "
        Next
        szCMD &= ")"

        Dim strSQL As New OleDbCommand(szCMD, dbName)
        Try
            strSQL.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Function DumpTempTable(ByVal szTableName As String) As Boolean
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand("DROP TABLE " & szTableName, dbName)
        Try
            strSQL.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Function GetXTabsItemSets(ByVal szGameID() As String, ByVal szFieldY As String, ByVal ItemsY() As String, ByVal minSupport As Integer) As ItemSet()

        '1. Open database and compile gamestring
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        '1.1. Compile multiple gameID string
        Dim szGameString As String = "(PathData.GameID = "
        For Each Game As String In szGameID
            szGameString &= "'" & Game & "'"
            If Array.IndexOf(szGameID, Game) < szGameID.Length - 1 Then
                szGameString &= " OR PathData.GameID = "
            End If
        Next
        szGameString &= ")"

        '2. Prepare an array of matching event names.
        Dim ItemSets(ItemsY.Length - 1) As ItemSet

        Dim szDBField As String = TranslateField(szFieldY, True)
        Dim szSearchString As String = "SELECT " & szDBField & " FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString

        strSQL = New OleDbCommand(szSearchString, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    If Not dbReader.IsDBNull(0) Then
                        ReDim Preserve ItemSets(Array.IndexOf(ItemsY, dbReader.Item(TranslateField(szFieldY, False)).ToString)).ItemName(0)
                        ItemSets(Array.IndexOf(ItemsY, dbReader.Item(TranslateField(szFieldY, False)).ToString)).ItemName(0) = dbReader.Item(TranslateField(szFieldY, False)).ToString
                        ItemSets(Array.IndexOf(ItemsY, dbReader.Item(TranslateField(szFieldY, False)).ToString)).ItemSetFrequency += 1
                    End If
                Loop
            End If

        Catch ex As Exception
            Return ItemSets
        End Try


        '3.  Iteratively reduce datset to supported combinations.
        Dim IterateDone As Boolean = False
        Dim IterateCount As Integer = 0

        Do While IterateCount < 2


            Dim n As Integer = -1
            Dim tItemSet() As ItemSet = ItemSets    'Temp dataset
            Dim ActiveItems() As String = ItemSets(0).ItemName  'Contains the items to be iterated with.
            Erase ItemSets

            'Reduce set to itemsets >= minimum support and compile new itemsets
            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                If tItemSet(i).ItemSetFrequency >= minSupport Then
                    n += 1
                    ReDim Preserve ItemSets(n)
                    ItemSets(n) = tItemSet(i)

                    For Each item As String In ItemSets(n).ItemName
                        If Not Array.IndexOf(ActiveItems, item) >= 0 Then
                            ReDim Preserve ActiveItems(ActiveItems.Length)
                            ActiveItems(ActiveItems.Length - 1) = item
                        End If
                    Next
                End If
            Next

            If IterateCount > 2 Then
                Return ItemSets
            End If

            Array.Sort(ActiveItems)

            tItemSet = ItemSets

            n = -1
            Erase ItemSets

            For i As Integer = 0 To tItemSet.GetUpperBound(0)
                'Get last item in itemset.
                Dim s As Integer = tItemSet(i).ItemName.GetUpperBound(0)
                Dim szLastItem As String = tItemSet(i).ItemName(s)
                'Get next item in sequence from original itemset.
                Dim k As Integer = Array.IndexOf(ActiveItems, szLastItem)

                If k < ActiveItems.Length - 1 Then
                    'There is at least one more item candidate.
                    For m As Integer = k + 1 To ActiveItems.Length - 1
                        Dim szExtraItem As String = ActiveItems(m)

                        Dim szOR As String = " AND ("
                        For Each item As String In tItemSet(i).ItemName
                            szOR &= szDBField & " = '" & item & "'"
                            If Array.IndexOf(tItemSet(i).ItemName, item) < tItemSet(i).ItemName.Length - 1 Then szOR &= " OR "
                        Next
                        szOR &= " OR " & szDBField & " = '" & szExtraItem & "')"

                        n += 1
                        ReDim Preserve ItemSets(n)
                        ItemSets(n).ItemName = tItemSet(i).ItemName
                        ItemSets(n).ItemSetFrequency = 0
                        ReDim Preserve ItemSets(n).ItemName(ItemSets(n).ItemName.Length)
                        ItemSets(n).ItemName(ItemSets(n).ItemName.Length - 1) = szExtraItem

                        'Debug.Print(szOR)

                        'Now find matches.
                        'a. get a list of playnumber/gameid/timecriteria instances.
                        Dim BaseString As String = "SELECT DISTINCT PlayNumber, PathData.GameID, TimeCriteria FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & szGameString & szOR
                        Dim nPlayNumber() As Integer = Nothing
                        Dim nGameID() As String = Nothing
                        Dim nTimeCriteria() As String = Nothing
                        strSQL = New OleDbCommand(BaseString, dbName)
                        Try
                            dbReader = strSQL.ExecuteReader()
                            Dim q As Integer = -1
                            If dbReader.HasRows Then
                                Do While dbReader.Read
                                    If Not dbReader.IsDBNull(0) Then
                                        q += 1
                                        ReDim Preserve nPlayNumber(q)
                                        ReDim Preserve nGameID(q)
                                        ReDim Preserve nTimeCriteria(q)
                                        nPlayNumber(q) = dbReader.Item("PlayNumber")
                                        nGameID(q) = dbReader.Item("GameID")
                                        nTimeCriteria(q) = dbReader.Item("TimeCriteria")
                                    End If
                                Loop
                            End If
                        Catch ex As Exception
                            'Beep()
                            'MsgBox("Table error.")
                        End Try

                        If Not nPlayNumber Is Nothing Then
                            Application.DoEvents()
                            For q As Integer = 0 To nPlayNumber.GetUpperBound(0)
                                szSearchString = "SELECT DISTINCT " & szDBField & " FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & _
                                "PlayNumber = " & nPlayNumber(q) & " AND PathData.GameID = '" & nGameID(q) & "' AND TimeCriteria = '" & nTimeCriteria(q) & "'"
                                Dim szItemCollection As String() = GetItemCollection(szSearchString, szFieldY)
                                If szItemCollection.Length > ItemSets(n).ItemName.Length Then
                                    'Now check if all of the required items are in the new collection
                                    Dim match As Boolean = True
                                    For Each item As String In ItemSets(n).ItemName
                                        If Array.IndexOf(szItemCollection, item) < 0 Then
                                            match = False
                                        End If
                                    Next
                                    If match Then
                                        If Array.IndexOf(szItemCollection, szExtraItem) >= 0 Then
                                            'A match has been found
                                            ItemSets(n).ItemSetFrequency += 1
                                        End If
                                    End If
                                End If

                            Next
                        End If
                    Next
                End If
            Next
            IterateCount += 1
        Loop

        Return ItemSets

    End Function

    Public Function GetItemCollection(ByVal szSearchString As String, ByVal szFieldY As String) As String()
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As OleDbCommand = Nothing
        Dim dbReader As OleDbDataReader = Nothing
        dbName.Open()

        strSQL = New OleDbCommand(szSearchString, dbName)
        Dim szItemCollection() As String = Nothing
        Dim ic As Integer = -1
        dbReader = strSQL.ExecuteReader()
        If dbReader.HasRows Then
            Do While dbReader.Read
                ic += 1
                ReDim Preserve szItemCollection(ic)
                szItemCollection(ic) = dbReader.Item(TranslateField(szFieldY, False)).ToString
            Loop
        End If
        dbName.Close()

        Return szItemCollection
    End Function

    Public Function GetItemsFromField(ByVal szGameID() As String, ByVal szField As String, ByVal szTable As String, _
        Optional ByVal szParameters As String = Nothing) As String()

        Dim nCount As Integer = 0
        Dim Found() As String = Nothing
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szfield2 As String = szField
        If szField = "GameID" Then szfield2 = "PathData.GameID"
        Dim szSearchString As String = "SELECT DISTINCT " & szfield2 & " FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE (PathData.GameID = "
        'Dim szSearchString As String = "SELECT DISTINCT " & szfield2 & " FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE Region > 0 AND (PathData.GameID = "
        dbName.Open()

        'Compile multiple gameID string
        For Each Game As String In szGameID
            szSearchString = szSearchString & "'" & Game & "'"
            If Array.IndexOf(szGameID, Game) < szGameID.Length - 1 Then
                szSearchString = szSearchString & " OR PathData.GameID = "
            End If
        Next
        szSearchString &= ")" & szParameters


        Try
            Dim strSQL As New OleDbCommand(szSearchString, dbName)
            Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read()
                    ReDim Preserve Found(nCount)
                    If Not dbReader.IsDBNull(0) Then
                        Found(nCount) = dbReader.Item(szField)
                        nCount += 1
                    End If
                Loop
            End If


        Catch ex As Exception
        End Try

        dbName.Close()
        Return Found

    End Function

    Public Function GetHighestTimeCode(ByVal uSearchCriteria As SearchCriteria, Optional ByVal AddTimesForHalfs As Boolean = False) As Integer

        '* 
        '* Set GameIDs into search string...
        '*
        Dim szTemp As String = Nothing
        Dim i As Integer = 0
        Dim nTime As Integer = Nothing

        If Not uSearchCriteria.szGameID Is Nothing Then
            szTemp = szTemp & " ("
            For Each n As String In uSearchCriteria.szGameID
                szTemp = szTemp & "GameID = '" & n & "'"
                If i < uSearchCriteria.szGameID.GetUpperBound(0) Then
                    i = i + 1
                    szTemp = szTemp & " OR "
                Else
                    i = 0
                    szTemp = szTemp & ")"
                End If
            Next
        End If
        i = 0

        'Just find the longest of any timecriterion
        szTemp = "SELECT MAX(TimeCode) AS MaxTime FROM PathData WHERE" & szTemp
        If AddTimesForHalfs Then szTemp &= " GROUP BY TimeCriteria"

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As New OleDbCommand(szTemp, dbName)
        Dim dbReader As OleDbDataReader = Nothing


        dbName.Open()

        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    nTime += (dbReader.Item(0) / 60)
                    i += 1
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try


        Return nTime



    End Function

    Public Function GetHighestTimeCodeByTimeCriteria(ByVal szGameIDs() As String, ByVal TimeCriteria As String) As Integer

        '* 
        '* Set GameIDs into search string...
        '*
        Dim szTemp As String = Nothing
        Dim i As Integer = 0
        Dim nTime As Integer = Nothing

        If Not szGameIDs Is Nothing Then
            szTemp = szTemp & " ("
            For Each n As String In szGameIDs
                szTemp = szTemp & "GameID = '" & n & "'"
                If i < szGameIDs.GetUpperBound(0) Then
                    i = i + 1
                    szTemp = szTemp & " OR "
                Else
                    i = 0
                    szTemp = szTemp & ")"
                End If
            Next
        End If
        i = 0

        'Just find the longest of any timecriterion
        szTemp = "SELECT MAX(TimeCode) AS MaxTime FROM PathData WHERE" & szTemp & " AND TimeCriteria = '" & TimeCriteria & "'"

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim strSQL As New OleDbCommand(szTemp, dbName)
        Dim dbReader As OleDbDataReader = Nothing


        dbName.Open()

        Try
            dbReader = strSQL.ExecuteReader()
            If dbReader.HasRows Then
                Do While dbReader.Read
                    nTime += (dbReader.Item(0) / 60)
                    i += 1
                Loop
            End If
            dbName.Close()

        Catch ex As Exception
            dbName.Close()
        End Try


        Return nTime



    End Function

    Public Function GetTimeCriterion(ByVal szMDBPath As String) As String()
        'Return separate time criterion from game id
        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szMDBPath

        Dim dbName As New OleDbConnection(SourceString)
        Dim retString() As String = Nothing
        Dim n As Integer = 0

        Dim szSearchString As String = "SELECT DISTINCT TimeCriteria FROM PathData"

        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Do While dbReader.Read()
                ReDim Preserve retString(n)
                retString(n) = dbReader.Item("TimeCriteria")
                n += 1
            Loop
        End If

        dbName.Close()
        Return retString

    End Function

    Public Function GetTimeCriterionFromGameID(ByVal szGameID() As String) As String()
        'Return separate time criterion from game id
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim retString() As String = Nothing
        Dim n As Integer = 0
        Dim szTemp As String = Nothing


        If Not szGameID Is Nothing Then
            szTemp = szTemp & " ("
            For Each sz As String In szGameID
                szTemp = szTemp & "GameID = '" & sz & "'"
                If n < szGameID.GetUpperBound(0) Then
                    n = n + 1
                    szTemp = szTemp & " OR "
                Else
                    n = 0
                    szTemp = szTemp & ")"
                End If
            Next
        End If
        n = 0

        Dim szSearchString As String = "SELECT DISTINCT TimeCriteria FROM PathData WHERE" & szTemp

        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Do While dbReader.Read()
                ReDim Preserve retString(n)
                retString(n) = dbReader.Item("TimeCriteria")
                n += 1
            Loop
        End If

        dbName.Close()
        Return retString

    End Function

    Public Function GetHighestPlayNumber(ByVal szGameID As String, Optional ByVal szTimeCriteria As String = Nothing, Optional ByVal szMDBPath As String = Nothing) As Integer
        If szMDBPath Is Nothing Then szMDBPath = UserPrefs.dbPath

        'Return the highest play number from the external database where TimeCriteria = szTimeCriteria
        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szMDBPath

        Dim dbName As New OleDbConnection(SourceString)
        Dim n As Integer = 0

        Dim szSearchString As String = "SELECT DISTINCT PlayNumber FROM PathData WHERE GameID = '" & szGameID & "' "
        If Not szTimeCriteria Is Nothing Then
            szSearchString &= "AND TimeCriteria = '" & szTimeCriteria & "' "
        End If
        szSearchString &= "ORDER BY PlayNumber DESC"

        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Do While dbReader.Read()
                n = dbReader.Item("PlayNumber")
                If n > 0 Then Exit Do
            Loop
        End If

        dbName.Close()
        Return n

    End Function

    Public Function GetOutcomeTime(ByVal PlayNumber As Integer, ByVal GameID As String, ByVal TimeCriteria As String, Optional ByVal GetFirstTime As Boolean = True) As Double
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szSearchString As String = "SELECT DISTINCT OutcomeTime FROM " & _
            "PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & GameID & "' AND TimeCriteria = '" & _
            TimeCriteria & "' AND PlayNumber = " & PlayNumber.ToString
        dbName.Open()
        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Dim nOutComeTime As Double = 0

            Do While dbReader.Read()
                If Not dbReader.IsDBNull(dbReader.GetOrdinal("OutcomeTime")) Then
                    nOutComeTime = dbReader.Item("OutcomeTime")
                    If GetFirstTime Then
                        dbName.Close()
                        Return nOutComeTime
                    End If
                End If
            Loop
            dbName.Close()
            Return nOutComeTime
        End If

        Return 0
    End Function

    Public Function GetTeamsList() As String()
        Dim List(frmAnalysis.cboTeamName.Items.Count) As String
        Dim n As Integer = 0
        For Each item As String In frmAnalysis.cboTeamName.Items
            If item <> "*" Then
                List(n) = item
                n += 1
            End If
        Next

        Return List
    End Function

    Public Function GetPlayNumberFromID(ByVal nId As Long) As Integer
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szSearchString As String = "SELECT PlayNumber FROM PathData WHERE ID = " & nId.ToString
        dbName.Open()

        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim result As Integer = Nothing

        If dbReader.HasRows Then
            Do While dbReader.Read()
                result = CInt(dbReader.Item("PlayNumber"))
            Loop
        End If
        dbReader.Close()

        Return result
    End Function

    Public Function GetPathInfo(ByVal nID As Long) As PathInfo
        'First, get the basic play info from the ID
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        Dim szSearchString As String = "SELECT GameID, PlayNumber, TeamName, TimeCriteria FROM PathData WHERE ID = " & nID.ToString
        dbName.Open()

        Dim strSQL As New OleDbCommand(szSearchString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Dim Result As PathInfo = Nothing

        If dbReader.HasRows Then
            Do While dbReader.Read()
                Result.GameID = dbReader.Item("GameID")
                Result.PlayNumber = dbReader.Item("PlayNumber")
                Result.TeamName = dbReader.Item("TeamName")
                Result.TimeCriteria = dbReader.Item("TimeCriteria")
            Loop
        End If
        dbReader.Close()

        'Then, iterate through the IDs from the PlayNumber to populate the rest of the PathInfo structure.
        szSearchString = "SELECT ID, TimeCode FROM PathData WHERE PlayNumber = " & Result.PlayNumber & _
            " AND GameID = '" & Result.GameID & "' AND TimeCriteria = '" & Result.TimeCriteria & "' ORDER BY ID"

        strSQL = New OleDbCommand(szSearchString, dbName)
        dbReader = strSQL.ExecuteReader()

        If dbReader.HasRows Then
            Dim n As Integer = 0
            Dim nStart As Integer = 99999
            Dim nStop As Integer = 0

            Do While dbReader.Read()
                ReDim Preserve Result.ID(n)
                Result.ID(n) = dbReader.Item("ID")
                If dbReader.Item("TimeCode") < nStart Then nStart = dbReader.Item("TimeCode")
                If dbReader.Item("TimeCode") > nStop Then nStop = dbReader.Item("TimeCode")
                n += 1
            Loop
            Result.StartTimeString = GetTimeStringFromSeconds(nStart, True)
            Result.StopTimeString = GetTimeStringFromSeconds(nStop, True)
        End If


        dbReader.Close()
        dbName.Close()

        Return Result
    End Function

    Public Function GetOutcomeTotalsByGameID(ByVal szGameID() As String, ByVal szTeamName As String, ByVal szEventName As String, _
    Optional ByVal OutcomesOnly As Boolean = True) As Integer()

        Dim retVar(szGameID.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Game As String In szGameID
            Dim szSearchString As String = "SELECT PathOutcomes.EventName FROM PathData INNER JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & Game & _
                "' AND PathData.TeamName = '" & szTeamName & "' AND PathOutcomes.EventName = '" & szEventName & "'"

            If OutcomesOnly Then
                szSearchString = szSearchString & " AND PathOutcomes.Outcome <> 2"
            End If

            dt = New DataTable
            da = New OleDbDataAdapter(szSearchString, CONNECT_STRING)

            Try
                da.Fill(dt)
                retVar(Array.IndexOf(szGameID, Game)) = dt.Rows.Count
            Catch ex As Exception
                retVar(Array.IndexOf(szGameID, Game)) = 0
            End Try
        Next

        dt.Dispose()
        da.Dispose()

        Return retVar
    End Function

    Public Function GetOutcomeTotalsByTeam(ByVal szTeamName() As String, ByVal szGameID As String, ByVal szEventName As String, _
            Optional ByVal OutcomesOnly As Boolean = True) As Integer()

        Dim retVar(szTeamName.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Team As String In szTeamName
            Dim szSearchString As String = "SELECT PathOutcomes.EventName FROM PathData INNER JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.TeamName = '" & Team & _
                "' AND PathData.GameID = '" & szGameID & "' AND PathOutcomes.EventName = '" & szEventName & "'"

            If OutcomesOnly Then
                szSearchString = szSearchString & " AND PathOutcomes.Outcome <> 2"
            End If

            dt = New DataTable
            da = New OleDbDataAdapter(szSearchString, CONNECT_STRING)

            Try
                da.Fill(dt)
                retVar(Array.IndexOf(szTeamName, Team)) = dt.Rows.Count
            Catch ex As Exception
                retVar(Array.IndexOf(szTeamName, Team)) = 0
            End Try
        Next

        dt.Dispose()
        da.Dispose()

        Return retVar
    End Function

End Module
