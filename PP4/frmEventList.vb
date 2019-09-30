Imports System.Data.OleDb

Public Class frmEventList
    Public idEventForm As Integer
    Public ListDirty As Boolean = False

    Private SyncTimeCriteria As String = Nothing

    Private Structure TimeShift
        Public GameID As String
        Public TimeCriteria As String
        Public nTimeShift As Double
    End Structure
    Private TimeShifts() As TimeShift

    Public Structure RecentEntries
        Dim IsOutcome As Boolean
        Dim DBid As Integer
        Dim RowID As Long
    End Structure
    Public Entries() As RecentEntries

    Private MyStartPathCount As Integer = 0
    Private MyEndPathCount As Integer = 0

    Public ReadOnly Property PathCountIndexStart() As Integer
        Get
            Return MyStartPathCount
        End Get
    End Property
    Public ReadOnly Property PathCountIndexEnd() As Integer
        Get
            Return MyEndPathCount
        End Get
    End Property

    Public Sub New(ByVal intID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        idEventForm = intID
        lastEventFormUsed = idEventForm
    End Sub

    Public Sub RePopulateGrid(ByVal nStartIndex As Integer, ByVal nEndIndex As Integer, Optional ByVal bShowAll As Boolean = False)
        If nStartIndex = 0 Then Exit Sub
        'Get current position
        Dim mvarLocation As Point = Me.Location

        Me.Visible = False
        Application.DoEvents()

        MyStartPathCount = nStartIndex
        MyEndPathCount = nEndIndex

        Me.dataGrid.Rows.Clear()

        dataGrid.Font = New Font("Verdana", 6.75, FontStyle.Regular, GraphicsUnit.Point)
        'Pos Outcome cell style
        Dim posStyle As New DataGridViewCellStyle
        posStyle.ForeColor = Color.DarkGreen
        posStyle.BackColor = Color.PaleGreen
        posStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Neg Outcome cell style
        Dim negStyle As New DataGridViewCellStyle
        negStyle.ForeColor = Color.DarkViolet
        negStyle.BackColor = Color.LightSalmon
        negStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Descriptor cell style
        Dim desStyle As New DataGridViewCellStyle
        desStyle.ForeColor = Color.DarkGoldenrod
        desStyle.BackColor = Color.LightGoldenrodYellow
        desStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Start cell style
        Dim startStyle As New DataGridViewCellStyle
        startStyle.Font = New Font(dataGrid.Font, FontStyle.Bold)

        'Clear list..
        With dataGrid
            frmMain.toolProgressBar.Maximum = nEndIndex
            frmMain.toolProgressBar.Minimum = nStartIndex

            Dim i, k, n As Integer
            For i = nStartIndex To nEndIndex
                If bShowAll Then
                    n = .Rows.Add(False, i.ToString, GetTimeStringFromSeconds(GamePath(i).GameTC), _
                    GamePath(i).TimeCriteria, _
                    GamePath(i).TeamName, _
                    GetStatusString(GamePath(i).Status), "", "", _
                    GetRegionString(GamePath(i).Region), GamePath(i).RecordID, _
                    GamePath(i).VideoTC)
                    If GamePath(i).Status = PathStatus.psStart Then .Rows(n).DefaultCellStyle = startStyle
                End If

                If GamePath(i).OutcomeCount > 0 Then

                    'Add outcome lines following event
                    'NB: outcomes are shown without team or status, and are color coded.
                    For k = 0 To GamePath(i).OutcomeCount - 1
                        Try
                            n = .Rows.Add(False, i.ToString, GetTimeStringFromSeconds(GamePath(i).OutcomeProp(k).GameTC), _
                            GamePath(i).TimeCriteria, _
                            GamePath(i).TeamName, _
                            "", _
                            GamePath(i).OutcomeProp(k).EventName, _
                            GetOutcomeString(GamePath(i).OutcomeProp(k).Outcome), _
                            GetRegionString(GamePath(i).Region), GamePath(i).RecordID, _
                            GamePath(i).OutcomeProp(k).VideoTC)
                            If GamePath(i).OutcomeProp(k).Outcome = OutcomeType.outDescriptor Then
                                .Rows(n).DefaultCellStyle = desStyle
                            ElseIf GamePath(i).OutcomeProp(k).Outcome = OutcomeType.outPositive Then
                                .Rows(n).DefaultCellStyle = posStyle
                            ElseIf GamePath(i).OutcomeProp(k).Outcome = OutcomeType.outNegative Then
                                .Rows(n).DefaultCellStyle = negStyle
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                End If
                frmMain.toolProgressBar.Value = i
            Next
        End With
        Me.Visible = True
        Me.Location = mvarLocation
        Me.mnuShowAll.Checked = bShowAll

        'Only enable time changing actions when all data is visible.
        Me.mnuChangeTimeCriteria.Enabled = True
        Me.NudgeVideoTimesToolStripMenuItem.Enabled = True

        frmMain.toolProgressBar.Value = nStartIndex
    End Sub

    Public Overloads Function ReLinkVideo(ByVal szGameID As String, ByVal szTimeCriteria As String, ByVal szNewSource As String) As Boolean

        Dim dt As New DataTable
        Dim strSQL As String = "SELECT PathData.ID, VideoFile, TimeCode, OutcomeTime, TimeCodeVideoStamp, TimeCodeVideoStampOutcome " & _
            "FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE TimeCriteria = '" & _
            szTimeCriteria & "' AND PathData.GameID = '" & szGameID & "' ORDER by PathData.ID"

        Dim da As New OleDbDataAdapter(strSQL, CONNECT_STRING)
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            Dim dbName As New OleDbConnection(CONNECT_STRING)
            dbName.Open()

            'First set all instances to the new video source.
            Dim cmd As New OleDbCommand("UPDATE PathData SET VideoFile = ?, TimeCodeVideoStamp = ? WHERE GameID = ? and TimeCriteria = ?", dbName)
            cmd.Parameters.Add(New OleDbParameter("VideoFile", szNewSource))
            cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStamp", -1))    'Set to -1 to invalidate offsets for multiple video streams
            cmd.Parameters.Add(New OleDbParameter("GameID", szGameID))
            cmd.Parameters.Add(New OleDbParameter("TimeCriteria", szTimeCriteria))
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
                Exit Function
            End Try

            'Now set the TimeCodeVideoStampOutcome to -1 in the PathOutcomes table to revert to the primary timecode.
            cmd = New OleDbCommand("UPDATE PathOutcomes SET TimeCodeVideoStampOutcome = ?", dbName)
            cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStampOutcome", -1))    'Set to -1 to invalidate offsets for multiple video streams
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
                Exit Function
            End Try
            dbName.Close()
        End If


        For i As Integer = LBound(GamePath) To UBound(GamePath)
            If GamePath(i).TimeCriteria = szTimeCriteria Then
                GamePath(i).VideoFile = szNewSource
            End If
        Next

        Return True
    End Function

    Public Overloads Function ReLinkVideo(ByVal szGameID As String, ByVal szTimeCriteria As String, ByVal szNewSources() As String) As Boolean

        Dim nStartTime As Double = 0

        For Each sz As String In szNewSources
            Dim nDuration As Double = GetMediaDuration(sz)

            '*
            '* Set PathData time references
            '* NB- TimeCodeVideoStampOutcome and TimeCodeVideoStamp are secondary timestamps that are enforced when more than one videos are strung together as in
            '* a FAT32 recording using the nNovia device.  Resetting the video source to a single file will revert the secondary timestamp to -1 which will be ignored
            '* during the loading process.
            '* 

            Dim dt As New DataTable
            Dim strSQL As String = "SELECT PathData.ID, VideoFile, TimeCode, OutcomeTime, TimeCodeVideoStamp, TimeCodeVideoStampOutcome, PathOutcomes.PathCount " & _
            "FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE TimeCriteria = '" & _
            szTimeCriteria & "' AND PathData.GameID = '" & szGameID & "' AND TimeCode >= " & nStartTime & " AND TimeCode <= " & _
            (nDuration + nStartTime) & " ORDER by PathData.ID"


            Dim da As New OleDbDataAdapter(strSQL, CONNECT_STRING)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim dbName As New OleDbConnection(CONNECT_STRING)
                dbName.Open()

                For Each row As DataRow In dt.Rows
                    Dim cmd As New OleDbCommand
                    cmd.CommandText = "UPDATE PathData SET VideoFile = ?, TimeCodeVideoStamp = ? WHERE PathData.ID = ?"
                    cmd.Connection = dbName
                    cmd.Parameters.Add(New OleDbParameter("VideoFile", sz))
                    cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStamp", row.Item("TimeCode") - nStartTime))
                    cmd.Parameters.Add(New OleDbParameter("PathData.ID", row.Item("ID")))
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                    End Try

                    If Not String.IsNullOrEmpty(row.Item("OutcomeTime").ToString) Then
                        'OutcomeTime is valid - include in query.
                        cmd = New OleDbCommand
                        cmd.CommandText = "UPDATE PathOutcomes SET TimeCodeVideoStampOutcome = ? WHERE PathOutcomes.PathID = ? AND PathOutcomes.PathCount = ?"
                        cmd.Connection = dbName
                        cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStampOutcome", row.Item("OutcomeTime") - nStartTime))
                        cmd.Parameters.Add(New OleDbParameter("PathOutcomes.ID", row.Item("ID")))
                        cmd.Parameters.Add(New OleDbParameter("PathOutcomes.PathCount", row.Item("PathCount")))
                        Try
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                            Exit Function
                        End Try
                    End If
                Next
                dbName.Close()
            End If
            nStartTime = nStartTime + nDuration
        Next

        Return True
    End Function

    Private Function SaveTimeChanges(ByVal ThisShift As TimeShift) As Boolean

        Dim dt As New DataTable
        Dim strSQL As String = "SELECT PathData.ID, TimeCode, OutcomeTime, TimeCodeVideoStamp, TimeCodeVideoStampOutcome, PathOutcomes.PathCount " & _
            "FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE TimeCriteria = '" & _
            ThisShift.TimeCriteria & "' AND PathData.GameID = '" & Me.Text & "' ORDER by PathData.ID"

        Dim da As New OleDbDataAdapter(strSQL, CONNECT_STRING)
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            Dim dbName As New OleDbConnection(CONNECT_STRING)
            dbName.Open()

            For Each row As DataRow In dt.Rows
                Dim cmd As New OleDbCommand
                cmd.CommandText = "UPDATE PathData SET TimeCode = ?, TimeCodeVideoStamp = ? WHERE PathData.ID = ?"
                cmd.Connection = dbName
                cmd.Parameters.Add(New OleDbParameter("TimeCode", row.Item("TimeCode") + ThisShift.nTimeShift))
                If row.Item("TimeCodeVideoStamp") >= 0 Then
                    cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStamp", row.Item("TimeCodeVideoStamp") + ThisShift.nTimeShift))
                Else
                    cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStamp", -1))
                End If
                cmd.Parameters.Add(New OleDbParameter("PathData.ID", row.Item("ID")))
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                    Exit Function
                End Try

                If Not String.IsNullOrEmpty(row.Item("OutcomeTime").ToString) Then
                    'OutcomeTime is valid - include in query.
                    cmd = New OleDbCommand
                    cmd.CommandText = "UPDATE PathOutcomes SET OutcomeTime = ?, TimeCodeVideoStampOutcome = ? WHERE PathOutcomes.PathID = ? AND PathOutcomes.PathCount = ?"
                    cmd.Connection = dbName
                    cmd.Parameters.Add(New OleDbParameter("OutcomeTime", row.Item("OutcomeTime") + ThisShift.nTimeShift))
                    If row.Item("TimeCodeVideoStampOutcome") >= 0 Then
                        cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStampOutcome", row.Item("TimeCodeVideoStampOutcome") + ThisShift.nTimeShift))
                    Else
                        cmd.Parameters.Add(New OleDbParameter("TimeCodeVideoStampOutcome", -1))
                    End If
                    cmd.Parameters.Add(New OleDbParameter("PathOutcomes.ID", row.Item("ID")))
                    cmd.Parameters.Add(New OleDbParameter("PathOutcomes.PathCount", row.Item("PathCount")))
                    Try
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                    End Try
                End If
            Next
            dbName.Close()
        End If
        Return True

    End Function

    Public Function ChangeTimeCriteria(ByVal szOldCriteria As String, ByVal szNewCriteria As String) As Boolean
        frmMain.toolProgressBar.Minimum = MyStartPathCount
        frmMain.toolProgressBar.Value = MyStartPathCount
        frmMain.toolActionStatus.Text = "Updating Time Criterion..."
        frmMain.toolProgressBar.Maximum = MyEndPathCount

        For n As Integer = MyStartPathCount To MyEndPathCount
            If GamePath(n).TimeCriteria = szOldCriteria Then GamePath(n).TimeCriteria = szNewCriteria
            frmMain.toolProgressBar.Value = n
        Next

        frmAnalysis.AddTimeCriterion(szNewCriteria)

        RePopulateGrid(PathCountIndexStart, PathCountIndexEnd, mnuShowAll.Checked)

        frmMain.toolProgressBar.Value = MyStartPathCount
        frmMain.toolActionStatus.Text = "Update Complete..."

        Return True
    End Function

    Private Function SaveCriteriaChanges(ByVal szGameID As String, ByVal szOldCriteria As String, ByVal szNewCriteria As String) As Boolean
        Dim dt As New DataTable
        Dim strSQL As String = "SELECT TimeCriteria FROM PathData WHERE GameID = '" & szGameID & "' AND TimeCriteria = '" & szOldCriteria & "'"
        Dim da As New OleDbDataAdapter(strSQL, CONNECT_STRING)

        Try
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("An error occured saving the time criteria changes.  Module frmEventList::SaveCriteriaChanges", MsgBoxStyle.Critical, Application.ProductName)
            Return False
        End Try

        If dt.Rows.Count > 0 Then
            Dim dbName As New OleDbConnection(CONNECT_STRING)
            Dim cmd As New OleDbCommand
            dbName.Open()

            cmd.Connection = dbName
            cmd.CommandText = "UPDATE PathData SET TimeCriteria = ? WHERE TimeCriteria = ? AND GameID = ?"
            cmd.Parameters.Add(New OleDbParameter("TimeCriteria", szNewCriteria))
            cmd.Parameters.Add(New OleDbParameter("TimeCriteria", szOldCriteria))
            cmd.Parameters.Add(New OleDbParameter("GameID", szGameID))
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
                Exit Function
            End Try

        End If
        Return True

    End Function

    Public Sub SwithCarry2Pass()
        With dataGrid
            .Rows(GamePath(PathCount).ListRow).Cells(5).Value = GetStatusString(PathStatus.psPass)
        End With
    End Sub

    Public Sub AddRow2Grid(ByVal nID As Integer, Optional ByVal cBackColor As KnownColor = KnownColor.White, Optional ByVal bIsOutcome As Boolean = False)

        'MsgBox("nID = " & nID.ToString)
        'MsgBox("cBackColor = " & cBackColor.ToString)
        'MsgBox("bIsOutcome = " & bIsOutcome.ToString)

        dataGrid.Font = New Font("Verdana", 6.75, FontStyle.Regular, GraphicsUnit.Point)
        'Pos Outcome cell style
        Dim posStyle As New DataGridViewCellStyle
        posStyle.ForeColor = Color.DarkGreen
        posStyle.BackColor = Color.PaleGreen
        posStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Neg Outcome cell style
        Dim negStyle As New DataGridViewCellStyle
        negStyle.ForeColor = Color.DarkViolet
        negStyle.BackColor = Color.LightSalmon
        negStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Descriptor cell style
        Dim desStyle As New DataGridViewCellStyle
        desStyle.BackColor = Color.FromKnownColor(cBackColor)
        desStyle.Font = New Font(dataGrid.Font, FontStyle.Italic)

        'Start cell style
        Dim startStyle As New DataGridViewCellStyle
        startStyle.Font = New Font(dataGrid.Font, FontStyle.Bold)
        startStyle.BackColor = Color.FromKnownColor(cBackColor)

        'MsgBox("Line 407 OK")

        With dataGrid

            Dim n As Integer

            If Not bIsOutcome Then
                'MsgBox("Line 407 Not outcome")
                GamePath(nID).ListRow = .Rows.Add(False, nID.ToString, GetTimeStringFromSeconds(GamePath(nID).GameTC), _
                    GamePath(nID).TimeCriteria, _
                    GamePath(nID).TeamName, _
                    GetStatusString(GamePath(nID).Status), "", "", _
                    GetRegionString(GamePath(nID).Region), GamePath(nID).RecordID, _
                    GamePath(nID).VideoTC)

                If GamePath(nID).Status = PathStatus.psStart Then .Rows(GamePath(nID).ListRow).DefaultCellStyle = startStyle
                'MsgBox("Rowcount = " & .RowCount)
                'MsgBox("GamePath(nID).ListRow = " & GamePath(nID).ListRow.ToString)

            Else
                'MsgBox("GamePath(nID).OutcomeCount = " & GamePath(nID).OutcomeCount.ToString)
                If GamePath(nID).OutcomeCount > 0 Then
                    'Add outcome lines following event
                    'NB: outcomes are shown without team or status, and are color coded.
                    n = .Rows.Add(False, nID.ToString, GetTimeStringFromSeconds(GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).GameTC), _
                        GamePath(nID).TimeCriteria, _
                        GamePath(nID).TeamName, _
                        GetStatusString(GamePath(nID).Status), _
                        GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).EventName, _
                        GetOutcomeString(GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).Outcome), _
                        GetRegionString(GamePath(nID).Region), GamePath(nID).RecordID, _
                        GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).VideoTC)

                    'MsgBox("n = " & n.ToString)

                    If GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).Outcome = OutcomeType.outDescriptor Then
                        .Rows(n).DefaultCellStyle = desStyle
                    ElseIf GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).Outcome = OutcomeType.outPositive Then
                        .Rows(n).DefaultCellStyle = posStyle
                    ElseIf GamePath(nID).OutcomeProp(GamePath(nID).OutcomeCount).Outcome = OutcomeType.outNegative Then
                        .Rows(n).DefaultCellStyle = negStyle
                    End If


                End If
            End If

            Try
                'MsgBox("Rowcount = " & .RowCount)
                'MsgBox("GetFirstRow = " & .Rows.GetFirstRow(DataGridViewElementStates.None))
                'MsgBox("GetLastRow = " & .Rows.GetLastRow(DataGridViewElementStates.None))

                .FirstDisplayedCell = .Rows(.RowCount - 1).Cells(0)
            Catch ex As Exception
                'MsgBox("Error Trapped at: " & ex.Message, MsgBoxStyle.Critical)
            End Try

            'Last event entry
            If Entries Is Nothing Then
                ReDim Entries(0)
            Else
                ReDim Preserve Entries(Entries.Length)
            End If

            With Entries(Entries.Length - 1)
                .DBid = PathCount
                .IsOutcome = bIsOutcome
                .RowID = GamePath(PathCount).ListRow
                If .IsOutcome Then
                    'MsgBox(".RowID = " & .RowID.ToString)
                    'MsgBox("GamePath(PathCount).OutcomeCount = " & GamePath(PathCount).OutcomeCount.ToString)
                    .RowID = .RowID + GamePath(PathCount).OutcomeCount
                End If
            End With

        End With
    End Sub

    Public Sub RemoveRowFromGrid(ByVal nID As Integer)
        Dim err As Boolean = False

        If Entries Is Nothing Then
            err = True
        Else
            If Entries.Length = 0 Then
                err = True
            Else
                Try
                    dataGrid.Rows.RemoveAt(Entries(nID).RowID)
                    Application.DoEvents()
                    ReDim Preserve Entries(Entries.Length - 2)
                Catch ex As Exception

                End Try
            End If

            If err Then
                MsgBox("This event has already been saved in the database.  It can be manually after the game is completed.", MsgBoxStyle.Exclamation, Application.ProductName)
            End If

        End If

    End Sub

    Private Sub dataGrid_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dataGrid.DoubleClick
        If Me.dataGrid.SelectedRows.Count = 0 Then Exit Sub
        Dim sRow As DataGridViewRow = Me.dataGrid.SelectedRows(0)

        PlayVideo(GetVideoFiles(CLng(sRow.Cells("gridID").Value))(0), sRow.Cells("gridVideoTC").Value - UserPrefs.LeadTime, _
            sRow.Cells("gridVideoTC").Value + UserPrefs.LagTime)

    End Sub

    Private Sub dataGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dataGrid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuEventListOptions.Show(Me, e.X, e.Y)
        End If
    End Sub

    Private Sub mnuShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowAll.Click
        Me.RePopulateGrid(Me.PathCountIndexStart, Me.PathCountIndexEnd, Me.mnuShowAll.Checked)

    End Sub

    Private Sub mnuRelinkVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRelinkVideo.Click
        'Get current criteria..
        Dim gRow As DataGridViewRow = dataGrid.CurrentRow
        Dim szTimeCriteria As String = gRow.Cells("gridCriteria").Value
        Dim szVideoSource() As String

        'Get video source..
        Dim dlgRelink As New OpenFileDialog
        With dlgRelink
            .Title = "Select new video source(s)..."
            .Multiselect = True
            .Filter = "AVI Video Files|*.avi|All Files|*.*"
            .DefaultExt = ".avi"
            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            szVideoSource = .FileNames
        End With

        If szVideoSource.Length = 0 Then Exit Sub

        'Verify operation..
        Dim szMSG As String = Nothing
        Dim res As DialogResult = Nothing

        If szVideoSource.Length = 1 Then
            'Update for single video file
            szMSG = "Link ALL EVENTS from " & Me.Text & vbCr & _
                "where the Time Criteria is: " & szTimeCriteria & vbCr & _
                "to the new video source: " & szVideoSource(0)

            res = MessageBox.Show(szMSG, "Relink Video", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

            If res = Windows.Forms.DialogResult.OK Then
                If ReLinkVideo(Me.Text, szTimeCriteria, szVideoSource(0)) Then
                    MessageBox.Show("Relinking complete.", "Relink Video", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Relinking failed.", "Relink Video", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Exit Sub
            End If

        ElseIf szVideoSource.Length > 1 Then
            szMSG = "Link ALL EVENTS from " & Me.Text & vbCr & _
                "where the Time Criteria is: " & szTimeCriteria & vbCr & _
                "to the selected sequence of video sources?"

            res = MessageBox.Show(szMSG, "Relink Video", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

            If res = Windows.Forms.DialogResult.OK Then
                If ReLinkVideo(Me.Text, szTimeCriteria, szVideoSource) Then
                    MessageBox.Show("Relinking complete.  This game should now be closed and re-opened to complete the update.", "Relink Video", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Relinking failed.", "Relink Video", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Exit Sub
            End If
        End If
    End Sub

    Private Sub frmEventList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        lastEventFormUsed = idEventForm
    End Sub

    Private Sub frmEventList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting(AppName, "Settings", "EventLeft", Me.Left.ToString)
        SaveSetting(AppName, "Settings", "EventTop", Me.Top.ToString)
        SaveSetting(AppName, "Settings", "EventHeight", Me.Height.ToString)
        SaveSetting(AppName, "Settings", "EventWidth", Me.Width.ToString)

    End Sub

    Private Sub frmEventList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Left = CInt(GetSetting(AppName, "Settings", "EventLeft", 0))
        Me.Top = CInt(GetSetting(AppName, "Settings", "EventTop", 0))
        Me.Height = CInt(GetSetting(AppName, "Settings", "EventHeight", 530))
        Me.Width = CInt(GetSetting(AppName, "Settings", "EventWidth", 640))
    End Sub

    Public Function ShiftVideoTimes(ByVal nTimeShift As Double, ByVal szTimeCriteria As String) As Boolean

        frmMain.toolProgressBar.Minimum = PathCountIndexStart
        frmMain.toolProgressBar.Value = PathCountIndexStart
        frmMain.toolActionStatus.Text = "Shifting Timepoints..."
        frmMain.toolProgressBar.Maximum = PathCountIndexEnd

        For p As Integer = PathCountIndexStart To PathCountIndexEnd
            frmMain.toolProgressBar.Value = p

            If GamePath(p).TimeCriteria = szTimeCriteria Then
                'Update GamePath array..
                GamePath(p).GameTC = GamePath(p).GameTC + nTimeShift
                GamePath(p).VideoTC = GamePath(p).VideoTC + nTimeShift
                If Not GamePath(p).OutcomeProp Is Nothing Then
                    For Each outcome As PathOutComes In GamePath(p).OutcomeProp
                        Dim n As Integer = Array.IndexOf(GamePath(p).OutcomeProp, outcome)
                        GamePath(p).OutcomeProp(n).GameTC = GamePath(p).OutcomeProp(n).GameTC + nTimeShift
                        GamePath(p).OutcomeProp(n).VideoTC = GamePath(p).OutcomeProp(n).VideoTC + nTimeShift
                    Next
                End If
            End If
        Next

        RePopulateGrid(PathCountIndexStart, PathCountIndexEnd, mnuShowAll.Checked)

        frmMain.toolProgressBar.Value = PathCountIndexStart
        frmMain.toolActionStatus.Text = "Complete..."

        Return True
    End Function

    Private Sub NudgeVideoTimesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NudgeVideoTimesToolStripMenuItem.Click

        Dim nRow As DataGridViewSelectedRowCollection = dataGrid.SelectedRows

        Dim szTimeShift As String = InputBox("Enter the time shift value in seconds  for all events matching the current criteria name: " & _
            nRow.Item(0).Cells("gridCriteria").Value.ToString, "Shift Time Points", "5")

        'Save changes to database
        If Not String.IsNullOrEmpty(szTimeShift) Then

            Dim tShift As New TimeShift
            tShift.GameID = Me.Text
            tShift.nTimeShift = CDbl(szTimeShift)
            tShift.TimeCriteria = nRow.Item(0).Cells("gridCriteria").Value.ToString

            If Not SaveTimeChanges(tShift) And ShiftVideoTimes(tShift.nTimeShift, tShift.TimeCriteria) Then
                MsgBox("Time shift failed...", MsgBoxStyle.Critical, Application.ProductName)
            End If
        End If

    End Sub

    Private Sub mnuExportEDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportEDL.Click
        Dim gRow As DataGridViewRow = dataGrid.SelectedRows(0)

        If Not String.IsNullOrEmpty(gRow.Cells("gridCriteria").Value) Then
            Dim dlgSaveEDL As New SaveFileDialog
            dlgSaveEDL.Title = "Export current games to EDL..."
            dlgSaveEDL.FileName = Me.Text & "_" & gRow.Cells("gridCriteria").Value & ".xls"
            dlgSaveEDL.Filter = "EDL Sportscode Files|*.xls"
            dlgSaveEDL.DefaultExt = "*.xls"
            Dim res As DialogResult = dlgSaveEDL.ShowDialog()
            If Not res = Windows.Forms.DialogResult.Cancel Then
                Dim szFile As String = dlgSaveEDL.FileName
                modLoadSaveGame.SaveEditList(szFile, gRow.Cells("gridCriteria").Value)
            End If
        End If

    End Sub

    Private Sub mnuExportVPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportVPL.Click
        Dim gRow As DataGridViewRow = dataGrid.SelectedRows(0)

        If Not String.IsNullOrEmpty(gRow.Cells("gridCriteria").Value) Then
            countVPL = countVPL + 1
            ReDim Preserve frmVPL(countVPL)
            frmVPL(countVPL) = New frmVideoPlayList(countVPL)

            Dim szFiles() As String = GetVideoFiles2(Me.Text, gRow.Cells("gridCriteria").Value)

            frmVPL(countVPL).AddVideoList(szFiles, Me.Text, gRow.Cells("gridCriteria").Value)
            'If CompileVPL(SearchHistory(CurrentSearch).szSQL, frmVPL(countVPL).vplGrid) > 0 Then frmVPL(countVPL).formDirty = True
            frmVPL(countVPL).MdiParent = frmMain
            frmVPL(countVPL).Show()


        End If

    End Sub

    Private Sub mnuEditRows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditRows.Click
        Dim dlgEditRow As New frmEditEventRow
        dlgEditRow.InitForm(dataGrid.SelectedRows, idEventForm)
        If dlgEditRow.ShowDialog() = Windows.Forms.DialogResult.OK Then Me.RePopulateGrid(Me.PathCountIndexStart, Me.PathCountIndexEnd, Me.mnuShowAll.Checked)
    End Sub

    Private Sub mnuSyncVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangeTimeCriteria.Click

        Dim nRow As DataGridViewSelectedRowCollection = dataGrid.SelectedRows

        Dim szNewCriteria As String = InputBox("Enter a new time criteria name for all events matching the current criteria name: " & _
            nRow.Item(0).Cells("gridCriteria").Value.ToString, "Change Time Criteria", _
            nRow.Item(0).Cells("gridCriteria").Value.ToString)

        If szNewCriteria.Length > 0 Then
            'Save changes to database
            SaveCriteriaChanges(Me.Text, nRow(0).Cells.Item("gridCriteria").Value, szNewCriteria)

            'Update Gamepath and event list
            ChangeTimeCriteria(nRow(0).Cells.Item("gridCriteria").Value, szNewCriteria)
        End If

    End Sub

 
    Private Sub dataGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataGrid.CellContentClick

    End Sub
End Class