Imports System.Data.OleDb
Imports System
Imports System.Reflection ' For Missing.Value and BindingFlags
Imports System.Runtime.InteropServices ' For COMException
Imports Microsoft.Office.Core

Module modLoadSaveGame
    Public CONNECT_STRING As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & UserPrefs.dbPath '& "; Persist Security Info=False"

    Public LAST_PATH_SAVED As Integer = 0       'Holds the most recently saved GamePath items.
    Public GAME_HEADER_SAVED As Boolean = False
    Public TimeCriteriaAtStartOfPlay As String = Nothing

    Private Structure Category
        Public Name As String
        Public Count As Integer
    End Structure
    Private Categories() As Category

    Public Function CheckDBVersion(Optional ByVal szDB As String = Nothing) As Boolean

        'Check database fields

        'Check the new Collection field
        If CheckDBFields("Collection", "GameData", szDB) = False Then Return False
        'Check the new DBVersion field
        If CheckDBFields("DBVersion", "GameData", szDB) = False Then Return False
        'Check the new VideoFile2 field
        If CheckDBFields("VideoFile2", "PathData", szDB) = False Then Return False
        'Check for old version files in DBVersion field
        If CheckDBFields("TimeCodeVideoStamp", "PathData", szDB) = False Then Return False
        'Check for old version files in DBVersion field
        If CheckDBFields("TimeCodeVideoStampOutcome", "PathOutcomes", szDB) = False Then Return False
        'Check for old version files in DBVersion field
        If CheckDBFields("Region", "PathData", szDB) = False Then Return False
        'Check for old version files in DBVersion field
        If CheckDBFields("PlayNumber", "PathData", szDB) = False Then Return False
        'Check for old version files in DBVersion field
        If CheckVersionString(DBVer, szDB) = False Then Return False

        Return True
    End Function

    Public Function UpgradeDBVersion(Optional ByVal szDB As String = Nothing) As Boolean

        If Not CheckDBFields("Collection", "GameData", szDB) Then
            AddNewStringField("Collection", "GameData", "New Collection", szDB)
        End If

        If Not CheckDBFields("DBVersion", "GameData", szDB) Then
            AddNewStringField("DBVersion", "GameData", "1.0", szDB)
        End If

        If Not CheckDBFields("VideoFile2", "PathData", szDB) Then
            AddNewStringField("VideoFile2", "PathData", "None", szDB)
        End If

        If Not CheckDBFields("TimeCodeVideoStamp", "PathData", szDB) Then
            AddNewIntegerField("TimeCodeVideoStamp", "PathData", , szDB)
        End If

        If Not CheckDBFields("TimeCodeVideoStampOutcome", "PathOutcomes", szDB) Then
            AddNewIntegerField("TimeCodeVideoStampOutcome", "PathOutcomes", , szDB)
        End If

        If Not CheckDBFields("PlayNumber", "PathData", szDB) Then
            AddNewIntegerField("PlayNumber", "PathData", , szDB)
        End If

        If Not CheckDBFields("Region", "PathData", szDB) Then
            AddNewIntegerField("Region", "PathData", , szDB)
        End If

        'Check and update version string, and if not latest then re-calibrate spatial data.
        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("SELECT * FROM GameData", tempConnectString)

        Try
            da.Fill(dt)
            For Each Row As DataRow In dt.Rows
                If Row.Item("DBVersion") < 4 Then
                    frmMain.toolActionStatus.Text = "Re-Calibrating " & Row.Item("GameID") & "..."
                    Application.DoEvents()

                    'Older data - update spatial coords.
                    If ConvertDataPoints(Row.Item("GameID"), szDB) Then
                        SetVersionString(Row.Item("GameID"), DBVer, szDB)
                    End If
                    GoTo Upgrade4to5

                ElseIf Row.Item("DBVersion") = 4 Then
Upgrade4to5:
                    frmMain.toolActionStatus.Text = "Upgrading Version 4.0 to 5.0: " & Row.Item("GameID") & "..."
                    Application.DoEvents()

                    'Adding PlayNumber and region field.
                    If AddPlayNumberField(Row.Item("GameID"), szDB) And AddRegionField(Row.Item("GameID"), szDB) Then
                        SetVersionString(Row.Item("GameID"), DBVer, szDB)
                    End If

                End If
            Next
            frmMain.toolActionStatus.Text = "Calibration Complete..."

        Catch ex As Exception
            frmMain.toolActionStatus.Text = "Calibration Failed..."
            Return False
        End Try


        Return True
    End Function

    Public Function ReCalcRegions() As Boolean

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("SELECT GameID FROM GameData", CONNECT_STRING)

        Try
            da.Fill(dt)
            For Each Row As DataRow In dt.Rows
                frmMain.toolActionStatus.Text = "Recalibrating: " & Row.Item("GameID") & "..."
                Application.DoEvents()

                'Adding PlayNumber and region field.
                If Not AddRegionField(Row.Item("GameID")) Then
                    MsgBox("Unable to re-calibrate data for " & Row.Item("GameID"))
                End If

            Next
            frmMain.toolActionStatus.Text = "Calibration Complete..."

        Catch ex As Exception
            frmMain.toolActionStatus.Text = "Calibration Failed..."
            Return False
        End Try

    End Function

    Public Function AddNewIntegerField(ByVal szFieldName As String, ByVal szTableName As String, Optional ByVal nDefaultValue As Integer = -1, _
        Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Try
            Dim dbName As New OleDbConnection(tempConnectString)
            dbName.Open()
            Dim cmd As New OleDbCommand("ALTER TABLE " & szTableName & " ADD " & szFieldName & " INT NULL", dbName)
            cmd.ExecuteNonQuery()
            cmd = New OleDbCommand("UPDATE " & szTableName & " SET " & szFieldName & " = " & nDefaultValue, dbName)
            cmd.ExecuteNonQuery()
            dbName.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function AddNewStringField(ByVal szFieldName As String, ByVal szTableName As String, Optional ByVal szDefaultValue As String = "Nothing", _
        Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Try
            Dim dbName As New OleDbConnection(tempConnectString)
            dbName.Open()
            Dim cmd As New OleDbCommand("ALTER TABLE " & szTableName & " ADD " & szFieldName & " VARCHAR(50) NULL", dbName)
            cmd.ExecuteNonQuery()
            cmd = New OleDbCommand("UPDATE " & szTableName & " SET " & szFieldName & " = '" & szDefaultValue & "'", dbName)
            cmd.ExecuteNonQuery()
            dbName.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function CheckDBFields(ByVal szFieldName As String, ByVal szTableName As String, _
        Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("SELECT DISTINCT " & szFieldName & " FROM " & szTableName, tempConnectString)

        Try
            da.Fill(dt)
            If Not String.IsNullOrEmpty(dt.Columns(szFieldName).ColumnName) Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function CheckVersionString(ByVal VersionString As String, Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("SELECT * FROM GameData", tempConnectString)

        Try
            da.Fill(dt)
            For Each Row As DataRow In dt.Rows
                If Not Row.Item("DBVersion") = VersionString Then
                    Return False
                End If
            Next

        Catch ex As Exception
            'MsgBox("An error occured in modLoadSaveGame::CheckDBVersion.", MsgBoxStyle.Critical)
            Return False
        End Try

        Return True

    End Function

    Private Function SetVersionString(ByVal szGameID As String, ByVal szVersionString As String, Optional ByVal szDB As String = Nothing) As Boolean
        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dbName As New OleDbConnection(tempConnectString)
        dbName.Open()

        Dim cmdSQL = New OleDbCommand("UPDATE GameData SET DBVersion = '" & szVersionString & "' WHERE GameID = '" & szGameID & "'", dbName)
        Try
            cmdSQL.ExecuteNonQuery()
        Catch ex As Exception
            Return False
        End Try

        dbName.Close()

        Return True
    End Function

    Private Function AddRegionField(ByVal szGameID As String, Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dbName As New OleDbConnection(tempConnectString)
        dbName.Open()

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("Select ID, X, Y from PathData WHERE GameID = '" & szGameID & "' ORDER BY ID", tempConnectString)
        da.Fill(dt)

        Dim pTemp As tRegion = tRegion.None

        For Each Row As DataRow In dt.Rows
            pTemp = modRegionFunctions.GetRegionFromPoints(UserPrefs.Sport, New PointF(Row.Item("X"), Row.Item("Y")), New PointF(1, 1), PitchOffset.X, PitchOffset.Y)

            Dim cmdSQL = New OleDbCommand("UPDATE PathData SET Region = " & pTemp & " WHERE ID = " & Row.Item("ID"), dbName)
            Try
                cmdSQL.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
        Next
        dbName.Close()

        Return True

    End Function

    Private Function AddPlayNumberField(ByVal szGameID As String, Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dbName As New OleDbConnection(tempConnectString)

        dbName.Open()

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("Select ID, PlayNumber, Status from PathData WHERE GameID = '" & szGameID & "' ORDER BY ID", tempConnectString)
        da.Fill(dt)

        Dim pTemp As Long = 0

        For Each Row As DataRow In dt.Rows
            If Row.Item("Status") = PathStatus.psStart Then
                'New play - update
                pTemp += 1
            End If

            Dim cmdSQL = New OleDbCommand("UPDATE PathData SET PlayNumber = " & pTemp & " WHERE ID = " & Row.Item("ID"), dbName)
            Try
                cmdSQL.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
        Next
        dbName.Close()

        Return True

    End Function

    Private Function ConvertDataPoints(ByVal szGameID As String, Optional ByVal szDB As String = Nothing) As Boolean

        Dim tempConnectString As String = Nothing
        If Not szDB Is Nothing Then
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDB
        Else
            tempConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath
        End If

        Dim dbName As New OleDbConnection(tempConnectString)
        dbName.Open()

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("Select ID, X, Y from PathData WHERE GameID = '" & szGameID & "'", CONNECT_STRING)
        da.Fill(dt)

        Dim pTemp As New PointF

        For Each Row As DataRow In dt.Rows
            pTemp.X = (Row.Item("X") * 0.0222) + 1.25
            pTemp.Y = (Row.Item("Y") * 0.0222) + 13.75
            Dim cmdSQL = New OleDbCommand("UPDATE PathData SET X = " & pTemp.X.ToString & ", Y = " & pTemp.Y.ToString & " WHERE ID = " & Row.Item("ID"), dbName)
            Try
                cmdSQL.ExecuteNonQuery()
            Catch ex As Exception
                Return False
            End Try
        Next
        dbName.Close()

        Return True

    End Function

    Public Function ChangeEventNameID(ByVal szEventName As String, ByVal ID_prev As Long, ByVal ID_new As Long) As Point

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        'Get old eventname details.
        Dim newOutcomeCount As Integer = 0
        Dim newTimeCode As Single = 0
        Dim newPoints As Point = Nothing
        Dim oldOutcomeCount As Integer = 0
        Dim oldTimeCode As Single = 0

        Dim strSQL As New OleDbCommand("SELECT * FROM PathData WHERE ID = " & ID_prev & " OR ID = " & ID_new, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Do While dbReader.Read()
            If dbReader.Item("ID") = ID_new Then
                newOutcomeCount = dbReader.Item("OutcomeCount")
                newTimeCode = dbReader.Item("TimeCode")
                newPoints.X = dbReader.Item("X")
                newPoints.Y = dbReader.Item("Y")
            ElseIf dbReader.Item("ID") = ID_prev Then
                oldOutcomeCount = dbReader.Item("OutcomeCount")
            End If
        Loop

        'Update OutcomeCounts in PathData
        If oldOutcomeCount > 0 Then oldOutcomeCount -= 1
        Dim cmdSQL = New OleDbCommand("UPDATE PathData SET OutcomeCount = " & oldOutcomeCount & " WHERE ID = " & ID_prev, dbName)
        Try
            cmdSQL.ExecuteNonQuery()
        Catch ex As Exception
            dbName.Close()
            Return Nothing
        End Try

        cmdSQL = New OleDbCommand("UPDATE PathData SET OutcomeCount = " & newOutcomeCount + 1 & " WHERE ID = " & ID_new, dbName)
        Try
            cmdSQL.ExecuteNonQuery()
        Catch ex As Exception
            dbName.Close()
            Return Nothing
        End Try

        'Update items in PathOutcomes
        cmdSQL = New OleDbCommand("UPDATE PathOutcomes SET PathID = ?, OutcomeTime = ? WHERE PathID = " & ID_prev & " AND EventName = '" & szEventName & "'", dbName)
        cmdSQL.Parameters.Add(New OleDbParameter("PathID", ID_new))
        cmdSQL.Parameters.Add(New OleDbParameter("OutcomeTime", newTimeCode))
        Try
            cmdSQL.ExecuteNonQuery()
        Catch ex As Exception
            dbName.Close()
            Return Nothing
        End Try

        dbName.Close()
        Return newPoints

    End Function

    Public Sub TempCal(ByVal szGameID As String)
        'NB: Not in use - use only to manually recalibrate a dataset.

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("Select ID, X, Y from PathData WHERE GameID = '" & szGameID & "'", CONNECT_STRING)
        da.Fill(dt)

        Dim pTemp As New PointF

        For Each Row As DataRow In dt.Rows
            pTemp.X = (Row.Item("X") / 4.8889)
            pTemp.Y = (Row.Item("Y") / 3.536)
            Dim cmdSQL = New OleDbCommand("UPDATE PathData SET X = " & pTemp.X.ToString & ", Y = " & pTemp.Y.ToString & " WHERE ID = " & Row.Item("ID"), dbName)
            Try
                cmdSQL.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Failed...", MsgBoxStyle.Critical, Application.ProductName)
            End Try
        Next
        dbName.Close()

        MsgBox("Done..", MsgBoxStyle.Information, Application.ProductName)
    End Sub

    Function GetRecords(ByVal szGameID As String, ByVal nCount As Integer, Optional ByVal AppendDataSet As Boolean = False, Optional ByVal ShowAnalysisForm As Boolean = True) As Integer
        UserPrefs.CacheAllData = False

        'Benchmarking
        Dim LoadTime As New Stopwatch
        LoadTime.Start()

        Dim oCount As Integer
        Dim PlayerCount As Integer = 0
        Dim PlayerNames() As String = Nothing

        Dim DataSetStartIndex As Integer = PathCount + 1

        GetRecords = 0
        If Len(szGameID) = 0 Then Exit Function
        If Not AppendDataSet Then Erase GamePath

        '*
        '* Show progress dialog
        '*

        frmMain.toolProgressBar.Maximum = nCount + PathCount

        If PathCount < 0 Then
            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Value = 0
        Else
            frmMain.toolProgressBar.Minimum = PathCount
            frmMain.toolProgressBar.Value = PathCount
        End If
        GameCount = GameCount + 1
        frmMain.toolActionStatus.Text = "Loading " & szGameID & " (" & nCount.ToString & " records)..."

        Application.DoEvents()

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        '*
        '* Set OleDB command to search for game data in GameData
        '*

        Dim tempGameData As GameProperties = Nothing
        Dim strSQL As New OleDbCommand("SELECT * FROM GameData WHERE GameID = '" & szGameID & "'", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Try

            'Put game data to temporary array to apply to the GamePath() array for each record.
            Do While dbReader.Read()
                tempGameData.GameDateString = dbReader.GetDateTime(1).ToLongDateString
                tempGameData.GameDate = dbReader.GetDateTime(1)
                tempGameData.GameID = dbReader.Item("GameID").ToString
                tempGameData.GameOpponent = dbReader.Item("GameOpponent").ToString
                tempGameData.GameVenue = dbReader.Item("GameVenue").ToString
                tempGameData.GameNotes = dbReader.Item("GameNotes").ToString
                tempGameData.GameAuthor = dbReader.Item("GameAuthor").ToString
                tempGameData.Competition = dbReader.Item("Collection").ToString
                tempGameData.Cached = False

                Exit Do 'Only add data to first record, then just copy directly from memory.
            Loop
            dbReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message & " Error 2")
        End Try

        'Update array containing all the information relating to currently open games.
        ReDim Preserve GamesCurrentlyOpen(GameCount - 1)
        GamesCurrentlyOpen(GameCount - 1) = tempGameData

        '*
        '* Set OleDB command to search for records in PathData
        '*

        Dim strSQLX As String = "SELECT PathData.GameID, ID, X, Y, OutcomeCount, Status, TeamName, TimeCriteria, TimeCode, TimeCodeVideoStamp, EventName, Outcome, OutcomeTime, TimeCodeVideoStampOutcome, " & _
            "PathOutcomes.TagColor FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & szGameID & "' ORDER BY ID, PathOutcomes.PathCount"

        Dim nLastID As Long = 0
        Dim nStartOfPlay As Long = 0
        Dim nMovementNumber As Integer = 0

        Try
            strSQL = New OleDbCommand(strSQLX, dbName)
            dbReader = strSQL.ExecuteReader()

            'Disallow frmAnalysis::RefreshVisibleDescriptor list until after loading.
            frmAnalysis.AllowOutcomesUpdate = False

            Do While dbReader.Read()
                If nLastID <> dbReader.Item("ID") Then
                    'New ID
                    PathCount = PathCount + 1
                    ReDim Preserve GamePath(PathCount)
                    frmMain.toolProgressBar.Value = PathCount
                    frmMain.toolGameCount.Text = GameCount.ToString & " (" & PathCount.ToString & ")"
                    With GamePath(PathCount)
                        .GameInformation = tempGameData
                        .RecordID = dbReader.Item("ID")
                        .OutcomeCount = dbReader.Item("OutcomeCount")
                        .Status = dbReader.Item("Status")
                        .TeamName = dbReader.Item("TeamName")
                        .GameTC = dbReader.Item("TimeCode")
                        .VideoTC = dbReader.Item("TimeCodeVideoStamp")
                        If .VideoTC < 0 Then .VideoTC = .GameTC
                        .TimeCriteria = dbReader.Item("TimeCriteria")
                        .Coordinates.X = dbReader.Item("X")
                        .Coordinates.Y = dbReader.Item("Y")

                        'NB: New PointF(1, 1) refers to the assumption than data in the database is not scaled - hence correction multiple is 1 or both X and Y.
                        '.Region = modRegionFunctions.GetRegionFromPoints(UserPrefs.Sport, New PointF(dbReader.Item("X"), dbReader.Item("Y")), New PointF(1, 1), 11.25, 18.75)

                        oCount = 0
                        nLastID = .RecordID
                        'populate analysis window
                        'frmAnalysis.AddPlayers(.TeamName)
                        'frmAnalysis.AddTimeCriterion(.TimeCriteria)


                        If .OutcomeCount > 0 Then
                            Try
                                ReDim .OutcomeProp(oCount)
                                .OutcomeProp(0).EventName = dbReader.Item("EventName")
                                .OutcomeProp(0).Outcome = dbReader.Item("Outcome")
                                .OutcomeProp(0).GameTC = dbReader.Item("OutcomeTime")
                                .OutcomeProp(0).VideoTC = dbReader.Item("TimeCodeVideoStampOutcome")
                                If .OutcomeProp(0).VideoTC < 0 Then .OutcomeProp(0).VideoTC = .OutcomeProp(0).GameTC
                                .OutcomeProp(0).TagColor = Color.FromArgb(CInt(dbReader.Item("Tagcolor")))
                                .OutcomesExist = True
                                'frmAnalysis.AddDescriptors(.OutcomeProp(0).EventName, CInt(.OutcomeProp(0).Outcome))
                                oCount = oCount + 1
                            Catch ex As Exception
                                'MsgBox(ex.Message & " Error 3")
                            End Try
                        End If
                    End With
                Else
                    'Existing ID, therefore adding further outcomes. (First outcome is dealt with above.)
                    With GamePath(PathCount)
                        'Increment counter
                        ReDim Preserve .OutcomeProp(oCount)
                        .OutcomeProp(oCount).EventName = dbReader.Item("EventName")
                        .OutcomeProp(oCount).Outcome = dbReader.Item("Outcome")
                        .OutcomeProp(oCount).GameTC = dbReader.Item("OutcomeTime")
                        .OutcomeProp(oCount).VideoTC = dbReader.Item("TimeCodeVideoStampOutcome")
                        If .OutcomeProp(oCount).VideoTC < 0 Then .OutcomeProp(oCount).VideoTC = .OutcomeProp(oCount).GameTC
                        .OutcomeProp(oCount).TagColor = Color.FromArgb(CInt(dbReader.Item("Tagcolor")))
                        'frmAnalysis.AddDescriptors(.OutcomeProp(oCount).EventName, .OutcomeProp(oCount).Outcome)
                        oCount = oCount + 1

                    End With
                End If


            Loop
            frmAnalysis.AllowOutcomesUpdate = True
            dbReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message & " Error 4")
        End Try


        'Now add unique descriptors, players and times.
        strSQLX = "SELECT DISTINCT TeamName FROM PathData WHERE GameID = '" & szGameID & "' ORDER BY TeamName"
        strSQL = New OleDbCommand(strSQLX, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Do While dbReader.Read
                If Not dbReader.IsDBNull(0) Then
                    frmAnalysis.AddPlayers(dbReader.Item("TeamName"))
                End If
            Loop
        Catch ex As Exception
        End Try
        dbReader.Close()

        strSQLX = "SELECT DISTINCT TimeCriteria FROM PathData WHERE GameID = '" & szGameID & "' ORDER BY TimeCriteria"
        strSQL = New OleDbCommand(strSQLX, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Do While dbReader.Read
                If Not dbReader.IsDBNull(0) Then
                    frmAnalysis.AddTimeCriterion(dbReader.Item("TimeCriteria"))
                End If
            Loop
        Catch ex As Exception
        End Try
        dbReader.Close()


        strSQLX = "SELECT DISTINCT EventName, Outcome FROM PathOutcomes WHERE GameID = '" & szGameID & "' ORDER BY EventName"
        strSQL = New OleDbCommand(strSQLX, dbName)
        Try
            dbReader = strSQL.ExecuteReader()
            Do While dbReader.Read
                If Not dbReader.IsDBNull(0) Then
                    frmAnalysis.AddDescriptors(dbReader.Item("EventName"), dbReader.Item("Outcome"))
                End If
            Loop
        Catch ex As Exception
        End Try
        dbReader.Close()

        dbName.Close()


        LoadTime.Stop()
        frmMain.toolActionStatus.Text = "Load Time: " & GetTimeStringFromSeconds(LoadTime.Elapsed.TotalSeconds)



        'NB: if CacheAllData = true then load all data to array and populate event list.  Overwise, only
        'load to the cache the data required to populate the eventlist.

        GetRecords = PathCount
        frmMain.toolProgressBar.Value = frmMain.toolProgressBar.Minimum
        frmMain.toolGameCount.Text = GameCount.ToString & " (" & PathCount.ToString & ")"
        ReDim Preserve szGamesLoaded(GameCount)
        szGamesLoaded(GameCount) = szGameID
        frmMain.mnuGenerateEDL.Enabled = True

        countE = countE + 1
        ReDim frmE(countE)
        frmE(countE) = New frmEventList(countE)

        frmE(countE).RePopulateGrid(DataSetStartIndex, DataSetStartIndex + nCount - 1)
        frmE(countE).MdiParent = frmMain
        frmE(countE).Text = tempGameData.GameID
        frmE(countE).Show()

        If ShowAnalysisForm Then
            frmAnalysis.MdiParent = frmMain
            frmAnalysis.Show()
        End If


    End Function

    Public Function GetRecordCount(ByVal szGameID As String, Optional ByVal szTimeCriteria As String = Nothing) As Integer
        Dim dt As New DataTable
        Dim szSQL As String = "SELECT ID FROM PathData WHERE GameID = '" & szGameID & "'"
        If Not szTimeCriteria Is Nothing Then
            szSQL &= " AND TimeCriteria = '" & szTimeCriteria & "'"
        End If

        Dim da As New OleDbDataAdapter(szSQL, CONNECT_STRING)
        Try
            da.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
        da.Dispose()
    End Function

    Public Function GetDistinctIDs(ByVal szSearchString As String) As Integer
        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(szSearchString, CONNECT_STRING)
        Dim rValue As Integer = 0
        Try
            da.Fill(dt)
            rValue = dt.Rows.Count
        Catch ex As Exception
            rValue = 0
        End Try
        da.Dispose()
        dt.Dispose()
        Return rValue
    End Function

    Public Function GetVideoFiles(ByVal nPathID As Long) As String()
        Dim szTemp(1) As String
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand("SELECT VideoFile, VideoFile2 FROM PathData WHERE ID = " & nPathID.ToString, dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        'Put game data to temporary array to apply to the GamePath() array for each record.
        Try
            Do While dbReader.Read()
                szTemp(0) = dbReader.Item("VideoFile")
                szTemp(1) = dbReader.Item("VideoFile2")
            Loop
        Catch ex As Exception

        End Try
        dbName.Close()

        Return szTemp
    End Function

    Public Function GetVideoFiles2(ByVal szGameID As String, ByVal szTimeCriteria As String) As String()
        Dim szTemp() As String = Nothing
        Dim nCount As Integer = 0
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand("SELECT DISTINCT VideoFile FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & "'", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        Try
            Do While dbReader.Read()
                ReDim Preserve szTemp(nCount)
                szTemp(nCount) = dbReader.Item("VideoFile")
                nCount += 1
            Loop
        Catch ex As Exception

        End Try
        dbName.Close()

        Return szTemp
    End Function

    Public Function GetCurrentDBVersion(ByVal szGameID As String) As String

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand("SELECT DBVersion FROM GameData WHERE GameID = '" & szGameID & "'", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        'Put game data to temporary array to apply to the GamePath() array for each record.
        On Error Resume Next
        Do While dbReader.Read()
            Return dbReader.Item("DBVersion")
            Exit Function
        Loop
        dbName.Close()
        Return Nothing
    End Function

    Public Function AddRecord(ByVal nIndex As Integer, Optional ByVal alternateCONNECT_STRING As String = Nothing) As Long

        'NB: alternateCONNECT_STRING refers to a database for exported data.  This is to re-use the AddRecord code in the same way as
        'the normal data collection process.
        Dim TempCS As String = CONNECT_STRING
        If Not alternateCONNECT_STRING Is Nothing Then
            CONNECT_STRING = alternateCONNECT_STRING
        End If

        Dim nID As Long = 0

        'If this is the first item then save header

        If Not GAME_HEADER_SAVED Then
            If Not GameIDExists(propsCurrentGame.GameID) Then GAME_HEADER_SAVED = AddGameHeader(propsCurrentGame, CONNECT_STRING)
        End If

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        For n As Integer = nIndex + 1 To PathCount

            If GamePath(n).Status = PathStatus.psStart Then PlayCount += 1 
            If GamePath(n).SpatialCorrection.X = 0 Or GamePath(n).SpatialCorrection.Y = 0 Then GamePath(n).SpatialCorrection = New PointF(1, 1)

            If GamePath(n).VideoFile Is Nothing Then
                Dim szTemp() As String = GetVideoFiles(GamePath(n).RecordID)
                GamePath(n).VideoFile = szTemp(0)
                GamePath(n).VideoFile2 = szTemp(1)
            End If

            Dim cmd As New OleDbCommand("INSERT INTO PathData (GameID, PathCount, TimeCriteria, VideoFile, VideoFile2," & _
                " X, Y, TimeCode, TeamName, Status, OutcomeCount, TagColor, TagFontColor, TimeCodeVideoStamp, PlayNumber, Region) VALUES (" & _
                "'" & propsCurrentGame.GameID & "', " & _
                n & ", " & _
                "'" & TimeCriteriaAtStartOfPlay & "', " & _
                "'" & GamePath(n).VideoFile & "', " & _
                "'None', " & _
                GamePath(n).Coordinates.X / GamePath(n).SpatialCorrection.X & ", " & _
                GamePath(n).Coordinates.Y / GamePath(n).SpatialCorrection.Y & ", " & _
                GamePath(n).GameTC & ", " & _
                "'" & GamePath(n).TeamName & "', " & _
                GamePath(n).Status & ", " & _
                GamePath(n).OutcomeCount & ", " & _
                GamePath(n).TagColor.ToArgb & ", " & _
                GamePath(n).TagFontColor.ToArgb & ", " & _
                GamePath(n).VideoTC & ", " & _
                PlayCount & ", " & _
                GetRegionFromPoints(UserPrefs.Sport, GamePath(n).Coordinates, GamePath(n).SpatialCorrection, PitchOffset.X, PitchOffset.Y) & ")", _
                dbName)

            Try
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MessageBox.Show(ex.Message, AppName, MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Return False
                Exit Function
            End Try
            dbName.Close()

            'Get record ID
            nID = GetRecordID(n, propsCurrentGame.GameID, TimeCriteriaAtStartOfPlay)

            dbName = New OleDbConnection(CONNECT_STRING)
            dbName.Open()

            If GamePath(n).OutcomeCount > 0 Then
                For o As Integer = 1 To GamePath(n).OutcomeCount
                    Dim cmd2 As New OleDbCommand("INSERT INTO PathOutcomes (GameID, PathID, PathCount, Outcome, EventName," & _
                        " OutcomeTime, TagColor, TimeCodeVideoStampOutcome) VALUES (" & _
                        "'" & propsCurrentGame.GameID & "', " & _
                        nID & ", " & _
                        o & ", " & _
                        GamePath(n).OutcomeProp(o).Outcome & ", " & _
                        "'" & GamePath(n).OutcomeProp(o).EventName & "', " & _
                        "'" & GamePath(n).OutcomeProp(o).GameTC & "', " & _
                        GamePath(n).OutcomeProp(o).TagColor.ToArgb & _
                        ", " & GamePath(n).OutcomeProp(o).VideoTC & ")", dbName)
                    Try
                        cmd2.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Exit For
                    End Try
                Next
            End If
        Next
        dbName.Close()
        LAST_PATH_SAVED = PathCount
        TimeCriteriaAtStartOfPlay = propsCurrentGame.TimeCriteria

        Erase frmE(lastEventFormUsed).Entries

        'Restore connect_string if modified.
        CONNECT_STRING = TempCS
        Return nID

    End Function

    Public Function AddGameHeader(ByVal GameInfo As GameProperties, ByVal DBConnectString As String) As Boolean

        Dim dbName As New OleDbConnection
        Try
            dbName.ConnectionString = DBConnectString
            dbName.Open()
        Catch ex As Exception
            dbName.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBConnectString
            dbName.Open()
        End Try

        If GameInfo.GameNotes.Length = 0 Then GameInfo.GameNotes = "Game notes: "
        Dim cmd As New OleDbCommand("INSERT INTO GameData (GameID, GameDate, GameOpponent, GameVenue, GameAuthor," & _
            " GameNotes, Collection, DBVersion) VALUES (" & _
            "'" & GameInfo.GameID & "', " & _
            "'" & GameInfo.GameDate.ToShortDateString & "', " & _
            "'" & GameInfo.GameOpponent & "', " & _
            "'" & GameInfo.GameVenue & "', " & _
            "'" & GameInfo.GameAuthor & "', " & _
            "'" & GameInfo.GameNotes & "', " & _
            "'" & GameInfo.Competition & "', " & _
            "'" & DBVer & "'" & _
            ")", dbName)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
            Exit Function
        End Try

        dbName.Close()

        Return True

    End Function

    Public Function GetLastRecordID(ByVal szgameid As String, ByVal szTimeCriteria As String, Optional ByVal altDB As String = Nothing) As Long
        Dim szTempCS As String = CONNECT_STRING
        If Not altDB Is Nothing Then
            CONNECT_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & altDB
        End If

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        CONNECT_STRING = szTempCS
        Dim nVal As Long = 0
        dbName.Open()

        '*
        '* Set OleDB command to search for game data in GameData
        '*
        Dim strSQL As New OleDbCommand("SELECT ID FROM PathData WHERE GameID = '" & szgameid & "' AND TimeCriteria = '" & szTimeCriteria & _
        "' ORDER BY ID DESC", dbName)

        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        'Put game data to temporary array to apply to the GamePath() array for each record.
        Do While dbReader.Read()
            nVal = dbReader.Item("ID")
            If nVal > 0 Then Exit Do
        Loop
        dbReader.Close()

        dbName.Close()
        If nVal = 0 Then
            MsgBox("Couldn't get handle on ID", MsgBoxStyle.Critical, Application.ProductName)
        End If

        Return nVal

    End Function

    Public Function GetRecordID(ByVal nIndex As Integer, ByVal szGameID As String, ByVal szTimeCriteria As String, Optional ByVal altDB As String = Nothing) As Long

        Dim szTempCS As String = CONNECT_STRING
        If Not altDB Is Nothing Then
            CONNECT_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & altDB
        End If

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        CONNECT_STRING = szTempCS
        Dim nVal As Long = 0
        dbName.Open()

        '*
        '* Set OleDB command to search for game data in GameData
        '*
        Dim strSQL As New OleDbCommand("SELECT ID FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szTimeCriteria & _
        "' AND PathCount = " & nIndex & " ORDER BY ID DESC", dbName)

        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        'Put game data to temporary array to apply to the GamePath() array for each record.
        Do While dbReader.Read()
            nVal = dbReader.Item("ID")
            'If nVal > 0 Then Exit Do
        Loop
        dbReader.Close()

        dbName.Close()
        If nVal = 0 Then
            ' MsgBox("Couldn't get handle on ID", MsgBoxStyle.Critical, Application.ProductName)
        End If

        Return nVal


    End Function

    Public Function GameIDExists(ByVal szGameID As String) As Boolean

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim strSQL As New OleDbCommand("SELECT GameID FROM GameData WHERE GameID = '" & szGameID & "'", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()
        Return dbReader.HasRows
        dbReader.Close()
        dbName.Close()

    End Function

    Public Function TranslateField(ByVal szVerboseName As String, Optional ByVal AddTablePrefix As Boolean = False) As String
        Select Case szVerboseName
            Case "Team Name"
                Return "TeamName"
            Case "Game ID"
                If AddTablePrefix Then
                    Return "PathData.GameID"
                Else
                    Return "GameID"
                End If
            Case "Outcome Type"
                Return "Outcome"
            Case "Event Name"
                Return "EventName"
            Case "Time Criteria"
                Return "TimeCriteria"
            Case "Region"
                Return "Region"
        End Select
        Return Nothing
    End Function

    Public Function TransferGameFromDatabase(ByVal szGameID As String, ByVal szSourceDB As String, ByVal szDestinationDB As String, _
    Optional ByVal OperationReportString As String = "Exporting") As Boolean

        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szSourceDB
        Dim dbSource As New OleDbConnection(SourceString)
        Dim dbDestination As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szDestinationDB)

        Dim cmdSQL As OleDbCommand = Nothing
        dbSource.Open()

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("Select * from PathData WHERE GameID = '" & szGameID & "' ORDER BY ID", SourceString)
        da.Fill(dt)

        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Maximum = dt.Rows.Count
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = OperationReportString & " " & szGameID & " to --> " & szDestinationDB

        For Each Row As DataRow In dt.Rows
            frmMain.toolProgressBar.Value += 1
            Application.DoEvents()

            Dim szVid1 As String = CheckVideoString(Row.Item("VideoFile"))
            Dim szVid2 As String = CheckVideoString(Row.Item("VideoFile2"))

            cmdSQL = New OleDbCommand("INSERT INTO PathData (GameID, PathCount, TimeCriteria, VideoFile, VideoFile2," & _
                " X, Y, TimeCode, TeamName, Status, OutcomeCount, TagColor, TagFontColor, Region, PlayNumber, TimeCodeVideoStamp) VALUES (" & _
                "'" & Row.Item("GameID") & "', " & _
                Row.Item("PathCount") & ", " & _
                "'" & Row.Item("TimeCriteria") & "', " & _
                "'" & szVid1 & "', " & _
                "'" & szVid2 & "', " & _
                Row.Item("X") & ", " & _
                Row.Item("Y") & ", " & _
                Row.Item("TimeCode") & ", " & _
                "'" & Row.Item("TeamName") & "', " & _
                Row.Item("Status") & ", " & _
                Row.Item("OutcomeCount") & ", " & _
                Row.Item("TagColor") & ", " & _
                Row.Item("TagFontColor") & ", " & _
                Row.Item("Region") & ", " & _
                Row.Item("PlayNumber") & ", " & _
                Row.Item("TimeCodeVideoStamp") & ")", dbDestination)

            Try
                dbDestination.Open()
                cmdSQL.ExecuteNonQuery()
                dbDestination.Close()
            Catch ex As Exception
                dbDestination.Close()
                MsgBox("Failed...", MsgBoxStyle.Critical, Application.ProductName)
            End Try

            If Row.Item("OutcomeCount") > 0 Then
                Dim nID_Old As Long = GetRecordID(Row.Item("PathCount"), Row.Item("GameID"), Row.Item("TimeCriteria"), szSourceDB)
                Dim nID_New As Long = GetRecordID(Row.Item("PathCount"), Row.Item("GameID"), Row.Item("TimeCriteria"), szDestinationDB)
                Dim dt2 As New DataTable
                Dim da2 As New OleDbDataAdapter("Select * from PathOutcomes WHERE PathID = " & nID_Old, SourceString)
                da2.Fill(dt2)

                For Each outcomeRow As DataRow In dt2.Rows

                    cmdSQL = New OleDbCommand("INSERT INTO PathOutcomes (GameID, PathID, PathCount, Outcome, EventName," & _
                        " OutcomeTime, TagColor, TimeCodeVideoStampOutcome) VALUES (" & _
                        "'" & outcomeRow.Item("GameID") & "', " & _
                        nID_New & ", " & _
                        outcomeRow.Item("PathCount") & ", " & _
                        outcomeRow.Item("Outcome") & ", " & _
                        "'" & outcomeRow.Item("EventName") & "', " & _
                        "'" & outcomeRow.Item("OutcomeTime") & "', " & _
                       outcomeRow.Item("TagColor") & _
                        ", " & outcomeRow.Item("TimeCodeVideoStampOutcome") & ")", dbDestination)
                    Try
                        dbDestination.Open()
                        cmdSQL.ExecuteNonQuery()
                        dbDestination.Close()
                    Catch ex As Exception
                        dbDestination.Close()
                        MsgBox("Failed...", MsgBoxStyle.Critical, Application.ProductName)
                        Exit For
                    End Try
                Next
            End If



        Next
        frmMain.toolProgressBar.Value = 0
        dbSource.Close()


    End Function

    Public Function CheckVideoString(ByVal VideoString As String) As String
        Dim returnString As String = Nothing
        For Each c As Char In VideoString
            If (Asc(c) >= 47 And Asc(c) <= 57) Or (Asc(c) >= 64 And Asc(c) <= 93) Or Asc(c) = 95 Or (Asc(c) >= 97 And Asc(c) <= 122) Or c = "." Or c = " " Or c = "-" Then
                returnString &= c
            End If
        Next
        Return returnString
    End Function

    Public Function ImportDB(ByVal szSourceDB As String) As String()

        'Get GameIDs and game information.

        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szSourceDB
        Dim dbName As New OleDbConnection(SourceString)
        dbName.Open()

        Erase GamesCurrentlyOpen
        Dim GameCount As Integer = 0
        Dim Games() As String = Nothing

        '*
        '* Set OleDB command to search for game data in GameData
        '*

        Dim strSQL As New OleDbCommand("SELECT * FROM GameData", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        Try
            'Put game data to temporary array to apply to the GamePath() array for each record.
            Do While dbReader.Read()
                If GameIDExists(dbReader.Item("GameID")) Then
                    Dim res As MsgBoxResult = MsgBox("The GameID: '" & dbReader.Item("GameID") & _
                    "' already exists in your database.  Would you like to replace the old game with this one?", _
                    MsgBoxStyle.YesNo, Application.ProductName)

                    If res = MsgBoxResult.Yes Then
                        'Remove old version.
                        RemoveGameID(dbReader.Item("GameID"))
                    Else
                        Continue Do
                    End If
                End If

                GameCount += 1
                ReDim Preserve GamesCurrentlyOpen(GameCount - 1)
                With GamesCurrentlyOpen(GameCount - 1)
                    .GameDateString = dbReader.GetDateTime(1).ToLongDateString
                    .GameDate = dbReader.GetDateTime(1)
                    .GameID = dbReader.Item("GameID").ToString
                    .GameOpponent = dbReader.Item("GameOpponent").ToString
                    .GameVenue = dbReader.Item("GameVenue").ToString
                    .GameNotes = dbReader.Item("GameNotes").ToString
                    .GameAuthor = dbReader.Item("GameAuthor").ToString
                    .Competition = dbReader.Item("Collection").ToString
                    ReDim Preserve Games(GameCount - 1)
                    Games(GameCount - 1) = .GameID

                    'Transfer data from imported DB to default DB
                    AddGameHeader(GamesCurrentlyOpen(GameCount - 1), CONNECT_STRING)
                    TransferGameFromDatabase(.GameID, szSourceDB, UserPrefs.dbPath, "Importing")
                End With
            Loop
            dbReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message & "Unable to read game data.", MsgBoxStyle.Critical, Application.ProductName)
            dbReader.Close()
            Return Games
        End Try

        Return Games
    End Function

    Public Function RemoveGameID(ByVal szGameID As String) As Boolean

        If DeleteDatabaseRows("GameData", "GameID", szGameID) And _
        DeleteDatabaseRows("PathData", "GameID", szGameID) = True And _
        DeleteDatabaseRows("PathOutcomes", "GameID", szGameID) = True Then
            RemoveGameID = True
        End If
    End Function

    Public Function DeleteDatabaseRows(ByVal szTableName As String, ByVal szField As String, _
         ByVal szCriterionValue As String) As Boolean


        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim cmd As New OleDbCommand("DELETE * FROM " & szTableName & " WHERE (" & szField & " = ?)", dbName)
        cmd.Parameters.Add(New OleDbParameter(szField, szCriterionValue))

        Try
            cmd.ExecuteNonQuery()
            DeleteDatabaseRows = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            DeleteDatabaseRows = False

        End Try

        dbName.Close()

    End Function

    Private Function GetNthInstance(ByVal szCategoryName As String) As Integer

        If Not Categories Is Nothing Then
            For Each item As Category In Categories
                If item.Name = szCategoryName Then
                    Dim i As Integer = Array.IndexOf(Categories, item)
                    Categories(i).Count += 1
                    Return Categories(i).Count
                End If
            Next

            'No matches - add to array
            Dim nCount As Integer = Categories.Length
            ReDim Preserve Categories(nCount)

            Categories(nCount).Name = szCategoryName
            Categories(nCount).Count = 1
        Else
            ReDim Categories(0)
            Categories(0).Name = szCategoryName
            Categories(0).Count = 1
        End If

        Return 1


    End Function

    Public Sub SaveStatsAsExcel(ByVal szFileName As String, ByVal grid As DataGridViewRowCollection)

        Dim app As Excel.Application
        app = New Excel.Application

        Dim wb As Excel.Workbook
        wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

        Dim ws As Excel.Worksheet
        ws = app.Worksheets.Add()

        With ws
            .Name = "Statistics"
            For Each row As DataGridViewRow In grid
                For Each col As DataGridViewCell In row.Cells
                    .Cells(row.Index + 1, col.ColumnIndex + 1) = row.Cells(col.ColumnIndex).Value
                    'Debug.Print(row.Cells(col.ColumnIndex).Value.ToString)

                Next
            Next
        End With

        For Each sheet As Excel.Worksheet In app.Worksheets
            If Not sheet.Name = "Statistics" Then
                sheet.Delete()
            Else
                sheet.Name = IO.Path.GetFileNameWithoutExtension(szFileName)
            End If
        Next

        Try
            Dim zFileFormat As Excel.XlFileFormat = Excel.XlFileFormat.xlTextMac
            Dim fname As String = IO.Path.GetFileNameWithoutExtension(szFileName)
            wb.SaveAs(szFileName, zFileFormat)
            MsgBox("Export complete...", MsgBoxStyle.Information, Application.ProductName)

        Catch ex As Exception
            MsgBox("Export failed..." & vbNewLine & "Reason: " & ex.Message, MsgBoxStyle.Information, Application.ProductName)
        Finally
            app.ActiveWorkbook.Close(False)
            ws = Nothing
            wb = Nothing
            app.Quit()
        End Try
        Exit Sub

    End Sub

    Public Sub ExportCSV(ByVal szFileName As String)

        If PathCount = 0 Then Exit Sub

        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
        '*  Send all current data to a csv file
        '*
        '*  We want to export the following items:
        '*      * GameID
        '*      * Team
        '*      * GameID
        '*      * GameID
        '*
        '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
        Dim app As Excel.Application
        app = New Excel.Application

        Dim wb As Excel.Workbook
        wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

        Dim ws As Excel.Worksheet
        ws = app.Worksheets.Add()

        With ws
            'set headers
            .Cells(1, 1).Value = "GameID"
            .Cells(1, 2).Value = "TimeCriteria"
            .Cells(1, 3).Value = "TeamName"
            .Cells(1, 4).Value = "RecordID"
            .Cells(1, 5).Value = "TimeString"
            .Cells(1, 6).Value = "X"
            .Cells(1, 7).value = "Y"
            .Cells(1, 8).Value = "Label 1"
            .Cells(1, 9).Value = "Label 2"
            .Cells(1, 10).Value = "Label 3"
            .Cells(1, 11).Value = "Label 4"
            .Cells(1, 12).Value = "Label 5"
            .Name = "Data"

            Dim RowCount As Integer = 2
            Dim PlayCount As Integer = 0

            For Each item As PlotPoints In GamePath

                '*Game id
                .Cells(RowCount, 1) = item.GameInformation.GameID
                .Cells(RowCount, 2) = item.TimeCriteria
                .Cells(RowCount, 3) = item.TeamName

                If item.Status = PathStatus.psStart Then PlayCount = PlayCount + 1
                .Cells(RowCount, 4) = PlayCount.ToString
                .Cells(RowCount, 5) = GetTimeStringFromSeconds(item.VideoTC, , True)
                .Cells(RowCount, 6) = item.Coordinates.X.ToString
                .Cells(RowCount, 7) = item.Coordinates.Y.ToString

                If item.OutcomeCount > 0 Then
                    Dim col As Integer = 8
                    Dim nIndex As Integer = Array.IndexOf(GamePath, item)
                    For Each out As PathOutComes In item.OutcomeProp
                        If Not String.IsNullOrEmpty(out.EventName) Then
                            .Cells(RowCount, col) = out.EventName
                            col = col + 1
                        End If
                    Next
                End If
                RowCount = RowCount + 1
            Next
        End With

        For Each sheet As Excel.Worksheet In app.Worksheets
            If Not sheet.Name = "EditList" Then
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
        Exit Sub


    End Sub


    Public Sub SaveEditList(ByVal szFileName As String, ByVal szTimeCriteria As String)

        Dim app As Excel.Application
        app = New Excel.Application

        Dim wb As Excel.Workbook
        wb = app.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

        Dim ws As Excel.Worksheet
        ws = app.Worksheets.Add()

        With ws
            'set headers
            .Cells(1, 1).Value = "start time"
            .Cells(1, 2).Value = "end time"
            .Cells(1, 3).Value = "category"
            .Cells(1, 4).Value = "Nth instance"
            .Cells(1, 5).Value = "# descriptors"
            .Cells(1, 6).Value = "descriptors..."
            .Name = "EditList"

            Dim RowCount As Integer = 1

            For Each item As PlotPoints In GamePath
                If item.OutcomeCount > 0 And item.TimeCriteria = szTimeCriteria Then
                    Dim nIndex As Integer = Array.IndexOf(GamePath, item)
                    For Each out As PathOutComes In item.OutcomeProp
                        If Not String.IsNullOrEmpty(out.EventName) Then
                            RowCount += 1
                            .Cells(RowCount, 1) = GetTimeStringFromSeconds(out.VideoTC - UserPrefs.LeadTime, , True)
                            .Cells(RowCount, 2) = GetTimeStringFromSeconds(out.VideoTC + UserPrefs.LagTime, , True)
                            .Cells(RowCount, 3) = out.EventName
                            .Cells(RowCount, 4) = GetNthInstance(out.EventName)
                            .Cells(RowCount, 5) = 1
                            .Cells(RowCount, 6) = item.TeamName
                        End If
                    Next
                End If
            Next
        End With

        For Each sheet As Excel.Worksheet In app.Worksheets
            If Not sheet.Name = "EditList" Then
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
        MsgBox("Export complete...", MsgBoxStyle.Information, Application.ProductName)
        Exit Sub

    End Sub

    Public Sub MergeComplete(ByVal PrimaryGame As GameProperties, ByVal szSourceDataMDB As String)

        Dim nID_New As Long = 0
        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szSourceDataMDB
        Dim dbSource As New OleDbConnection(SourceString)
        Dim dbDestination As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath)

        Dim cmdSQL As OleDbCommand = Nothing
        dbSource.Open()

        Dim TimeCriterion() As String = GetTimeCriterion(szSourceDataMDB)

        For Each time As String In TimeCriterion
            PathCount = GetRecordCount(PrimaryGame.GameID, time)
            PlayCount = GetHighestPlayNumber(PrimaryGame.GameID, time)

            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter("Select * from PathData WHERE TimeCriteria = '" & time & "' ORDER BY ID", SourceString)
            da.Fill(dt)

            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Maximum = dt.Rows.Count
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Merging " & szSourceDataMDB & " into --> " & PrimaryGame.GameID

            For Each Row As DataRow In dt.Rows
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()

                cmdSQL = New OleDbCommand("INSERT INTO PathData (GameID, PathCount, TimeCriteria, VideoFile, VideoFile2," & _
                    " X, Y, TimeCode, TeamName, Status, OutcomeCount, TagColor, TagFontColor, Region, PlayNumber, TimeCodeVideoStamp) VALUES (" & _
                    "'" & PrimaryGame.GameID & "', " & _
                    frmMain.toolProgressBar.Value + PathCount & ", " & _
                    "'" & Row.Item("TimeCriteria") & "', " & _
                    "'" & Row.Item("VideoFile") & "', " & _
                    "'" & Row.Item("VideoFile2") & "', " & _
                    Row.Item("X") & ", " & _
                    Row.Item("Y") & ", " & _
                    Row.Item("TimeCode") & ", " & _
                    "'" & Row.Item("TeamName") & "', " & _
                    Row.Item("Status") & ", " & _
                    Row.Item("OutcomeCount") & ", " & _
                    Row.Item("TagColor") & ", " & _
                    Row.Item("TagFontColor") & ", " & _
                    Row.Item("Region") & ", " & _
                    Row.Item("PlayNumber") + PlayCount & ", " & _
                    Row.Item("TimeCodeVideoStamp") & ")", dbDestination)

                Try
                    dbDestination.Open()
                    cmdSQL.ExecuteNonQuery()
                    dbDestination.Close()
                Catch ex As Exception
                    dbDestination.Close()
                    MsgBox("Failed...", MsgBoxStyle.Critical, Application.ProductName)
                End Try

                If Row.Item("OutcomeCount") > 0 Then
                    Dim nID_Old As Long = GetRecordID(Row.Item("PathCount"), Row.Item("GameID"), Row.Item("TimeCriteria"), szSourceDataMDB)
                    Dim dt2 As New DataTable
                    nID_New = GetLastRecordID(PrimaryGame.GameID, Row.Item("TimeCriteria"), UserPrefs.dbPath)
                    Dim da2 As New OleDbDataAdapter("Select * from PathOutcomes WHERE PathID = " & nID_Old & " ORDER BY PathCount", SourceString)
                    da2.Fill(dt2)

                    For Each outcomeRow As DataRow In dt2.Rows

                        cmdSQL = New OleDbCommand("INSERT INTO PathOutcomes (GameID, PathID, PathCount, Outcome, EventName," & _
                            " OutcomeTime, TagColor, TimeCodeVideoStampOutcome) VALUES (" & _
                            "'" & PrimaryGame.GameID & "', " & _
                            nID_New & ", " & _
                            outcomeRow.Item("PathCount") & ", " & _
                            outcomeRow.Item("Outcome") & ", " & _
                            "'" & outcomeRow.Item("EventName") & "', " & _
                            "'" & outcomeRow.Item("OutcomeTime") & "', " & _
                           outcomeRow.Item("TagColor") & _
                            ", " & outcomeRow.Item("TimeCodeVideoStampOutcome") & ")", dbDestination)
                        Try
                            dbDestination.Open()
                            cmdSQL.ExecuteNonQuery()
                            dbDestination.Close()
                        Catch ex As Exception
                            dbDestination.Close()
                            MsgBox("Failed...", MsgBoxStyle.Critical, Application.ProductName)
                            Exit For
                        End Try
                    Next
                End If



            Next

        Next

        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Merge Complete.."
        dbSource.Close()

    End Sub

    Public Sub MergeEvents(ByVal PrimaryGame As GameProperties, ByVal szSourceDataMDB As String)

        Dim nID_New As Long = 0
        Dim SourceString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & szSourceDataMDB
        Dim dbSource As New OleDbConnection(SourceString)
        Dim dbDestination As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & UserPrefs.dbPath)
        Dim MergeErrors As Boolean = True

        Dim cmdSQL As OleDbCommand = Nothing
        dbSource.Open()

        Dim TimeCriterion() As String = GetTimeCriterion(szSourceDataMDB)

        For Each time As String In TimeCriterion
            Dim strSQLX As String = "SELECT TeamName, TimeCriteria, EventName, OutcomeCount, PathOutcomes.Pathcount, OutcomeTime, PathOutcomes.TagColor, TimeCodeVideoStampOutcome FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE " & _
                "TimeCriteria = '" & time & "' AND OutcomeTime > 0 AND Outcome < 3 ORDER BY ID"

            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter(strSQLX, SourceString)
            da.Fill(dt)

            frmMain.toolProgressBar.Minimum = 0
            frmMain.toolProgressBar.Maximum = dt.Rows.Count
            frmMain.toolProgressBar.Value = 0
            frmMain.toolActionStatus.Text = "Merging " & szSourceDataMDB & " into --> " & PrimaryGame.GameID

            Dim lastEvent As String = Nothing
            Dim lastTime As Double = Nothing

            For Each Row As DataRow In dt.Rows
                frmMain.toolProgressBar.Value += 1
                Application.DoEvents()

                If Row.Item("EventName") <> lastEvent Or Row.Item("OutcomeTime") <> lastTime Then

                    'Get matching time
                    Dim id As Long = GetNearestIDbyTime(Row.Item("OutcomeTime"), PrimaryGame.GameID, time, Row.Item("TeamName"))

                    If Row.Item("TeamName") <> GetTeamNameFromID(id) Then
                        Dim res As DialogResult = MsgBox("There is a mismatch in the team name values between the original and merged datasets at the game time: " & Row.Item("OutcomeTime").ToString & vbNewLine & _
                        "for the time criteria: " & time & "." & vbNewLine & "The event '" & Row.Item("EventName") & "' belongs to '" & GetTeamNameFromID(id) & _
                        "' in the original game data, but in the merging data this event belongs to '" & Row.Item("TeamName").ToString & "'" & vbNewLine & vbNewLine & _
                        "Do you want to continue with this merge?", MsgBoxStyle.YesNo, Application.ProductName)

                        MergeErrors = True
                        If res = DialogResult.Yes Then
                            'Continue - force change.
                            Dim res2 As DialogResult = MsgBox("This action will force the merged data into the original dataset and change the teamname for this event.  Your data could become out of sync.  Are you sure you want to continue?", MsgBoxStyle.YesNo, Application.ProductName)

                            If res2 = DialogResult.No Then
                                MsgBox("Merge cancelled.", MsgBoxStyle.Exclamation)
                                Exit For
                            End If

                        ElseIf res = DialogResult.No Then
                            MsgBox("Merge cancelled.", MsgBoxStyle.Exclamation)
                            Exit For
                        End If
                    End If

                    'Increment outcomecount.
                    Dim nOutcomes As Integer = GetOutcomeCountFromID(id) + 1
                    cmdSQL = New OleDbCommand("UPDATE PathData SET OutcomeCount = " & nOutcomes.ToString & " WHERE ID = " & id.ToString, dbDestination)
                    Try
                        dbDestination.Open()
                        cmdSQL.ExecuteNonQuery()
                        dbDestination.Close()
                    Catch ex As Exception
                        dbDestination.Close()
                        MessageBox.Show(ex.Message)
                        MergeErrors = True
                        frmMain.toolActionStatus.Text = "Merge Incomplete.. possible errors!"
                        Exit Sub
                    End Try

                    cmdSQL = New OleDbCommand("INSERT INTO PathOutcomes (GameID, PathID, PathCount, Outcome, EventName," & _
                            " OutcomeTime, TagColor, TimeCodeVideoStampOutcome) VALUES (" & _
                            "'" & PrimaryGame.GameID & "', " & _
                            id & ", " & _
                            nOutcomes & ", 2, " & _
                            "'" & Row.Item("EventName") & "', " & _
                            Row.Item("OutcomeTime") & ", " & _
                            Row.Item("TagColor") & _
                            ", " & Row.Item("TimeCodeVideoStampOutcome") & ")", dbDestination)
                    Try
                        dbDestination.Open()
                        cmdSQL.ExecuteNonQuery()
                        dbDestination.Close()
                    Catch ex As Exception
                        dbDestination.Close()
                        MessageBox.Show(ex.Message)
                        MergeErrors = True
                    End Try

                    lastEvent = Row.Item("EventName")
                    lastTime = Row.Item("OutcomeTime")
                End If

            Next

        Next

        frmMain.toolProgressBar.Value = 0
        If MergeErrors Then
            frmMain.toolActionStatus.Text = "Merge Incomplete.. possible errors!"
        Else
            frmMain.toolActionStatus.Text = "Merge Complete.."
        End If
        dbSource.Close()

    End Sub


End Module
