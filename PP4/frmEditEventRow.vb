Imports System.Windows.Forms
Imports System.Data.OleDb

Public Class frmEditEventRow
    Private mRows As DataGridViewSelectedRowCollection

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If MsgBox("Are you sure you want to save these changes?  This action cannot be undone.", MsgBoxStyle.OkCancel, My.Application.Info.ProductName) = MsgBoxResult.Ok Then
            SaveChanges()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        End If
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub InitForm(ByVal Rows As DataGridViewSelectedRowCollection, ByVal nEventFormID As Integer)

        'Fill combos with options
        If frmAnalysis.Visible Then
            For Each sz As String In frmAnalysis.lstDescriptors.Items
                cboOutcomeName.Items.Add(sz)
            Next
            For Each sz As String In frmAnalysis.cboTeamName.Items
                If sz <> "*" Then cboTeamName.Items.Add(sz)
            Next
            For Each sz As String In frmAnalysis.cboTimeCriteria.Items
                If sz <> "*" Then cboTimeCriteria.Items.Add(sz)
            Next
        End If

        If Rows.Count = 1 Then
            Me.lblTimeStamp.Text = Rows(0).Cells("gridTime").Value
            Me.cboOutcomeType.Text = Rows(0).Cells("gridType").Value
            Me.cboOutcomeName.Text = Rows(0).Cells("gridOutcome").Value
            Me.cboTeamName.Text = Rows(0).Cells("gridTeamName").Value
            Me.cboTimeCriteria.Text = Rows(0).Cells("gridCriteria").Value
        Else
            Me.lblTimeStamp.Text = "#:##.##"

            'Check for selection matches
            cboOutcomeType.Text = Rows(0).Cells("gridType").Value
            For Each row As DataGridViewRow In Rows
                If row.Cells("gridType").Value <> cboOutcomeType.Text Then
                    cboOutcomeType.Items.Add("")
                    cboOutcomeType.Text = ""
                    Exit For
                End If
            Next

            cboOutcomeName.Text = Rows(0).Cells("gridOutcome").Value
            For Each row As DataGridViewRow In Rows
                If row.Cells("gridOutcome").Value <> cboOutcomeName.Text Then
                    cboOutcomeName.Items.Add("")
                    cboOutcomeName.Text = ""
                    Exit For
                End If
            Next

            Me.cboTeamName.Text = Rows(0).Cells("gridTeamName").Value
            For Each row As DataGridViewRow In Rows
                If row.Cells("gridTeamName").Value <> cboTeamName.Text Then
                    cboTeamName.Items.Add("")
                    cboTeamName.Text = ""
                    Exit For
                End If
            Next

            Me.cboTimeCriteria.Text = Rows(0).Cells("gridCriteria").Value
            For Each row As DataGridViewRow In Rows
                If row.Cells("gridCriteria").Value <> cboTimeCriteria.Text Then
                    cboTimeCriteria.Items.Add("")
                    cboTimeCriteria.Text = ""
                    Exit For
                End If
            Next

        End If

        mRows = Rows

    End Sub

    Private Function SaveChanges() As Boolean

        Dim dt As New DataTable

        Dim szTimeCriteria As String = Me.cboTimeCriteria.Text
        Dim szTeamName As String = Me.cboTeamName.Text
        Dim szOutcomeName As String = Me.cboOutcomeName.Text
        Dim szOutcomeType As String = Me.cboOutcomeType.Text
        Dim dbID(mRows.Count - 1) As Long
        Dim nCount As Integer = 0

        'Get GamePath() indices.
        Dim iPathCount(PathCount) As Integer

        Dim strSQL As String = "SELECT PathData.ID, TimeCriteria, TeamName, Outcome, EventName, PathOutcomes.PathCount " & _
            "FROM PathData LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE"

        For Each row As DataGridViewRow In mRows
            iPathCount(row.Cells.Item("gridPathcount").Value) = row.Cells.Item("gridID").Value
            strSQL &= " PathData.ID = " & row.Cells.Item("gridID").Value.ToString
            nCount += 1
            If nCount < mRows.Count Then strSQL &= " OR"
        Next
        strSQL &= " ORDER by PathData.ID"



        Dim da As New OleDbDataAdapter(strSQL, CONNECT_STRING)

        Try
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("An error occured completing this action.  " & ex.Message & vbNewLine & "Try editing a smaller number of rows.", MsgBoxStyle.Critical, Application.ProductName)
        End Try

        If dt.Rows.Count > 0 Then
            Dim dbName As New OleDbConnection(CONNECT_STRING)
            dbName.Open()
            Dim cmd As New OleDbCommand

            For Each row As DataRow In dt.Rows
                If szTimeCriteria.Length > 0 Or szTeamName.Length > 0 Then
                    cmd = New OleDbCommand
                    cmd.CommandText = "UPDATE PathData SET "
                    If szTimeCriteria.Length > 0 Then cmd.CommandText &= "TimeCriteria = '" & szTimeCriteria & "', "
                    If szTeamName.Length > 0 Then cmd.CommandText &= "TeamName = '" & szTeamName & "'"
                    If Microsoft.VisualBasic.Right(cmd.CommandText, 2) = ", " Then cmd.CommandText = Microsoft.VisualBasic.Left(cmd.CommandText, cmd.CommandText.Length - 2)
                    cmd.CommandText &= " WHERE PathData.ID = " & row.Item("ID")
                    cmd.Connection = dbName

                    Try
                        cmd.ExecuteNonQuery()

                        'DB change successful, now update GamePath()
                        Dim n As Integer = Array.IndexOf(iPathCount, row.Item("ID"))
                        If szTimeCriteria.Length > 0 Then GamePath(n).TimeCriteria = szTimeCriteria
                        If szTeamName.Length > 0 Then GamePath(n).TeamName = szTeamName

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                    End Try
                End If

                If Not String.IsNullOrEmpty(row.Item("EventName").ToString) And (szOutcomeName.Length > 0 Or szOutcomeType.Length > 0) Then
                    'Outcome name is valid - include in query.
                    cmd = New OleDbCommand
                    cmd.CommandText = "UPDATE PathOutcomes SET "
                    If szOutcomeName.Length > 0 Then cmd.CommandText &= "EventName = '" & szOutcomeName & "', "
                    If szOutcomeType.Length > 0 Then cmd.CommandText &= "Outcome = " & GetOutcomeFromString(szOutcomeType)
                    If Microsoft.VisualBasic.Right(cmd.CommandText, 2) = ", " Then cmd.CommandText = Microsoft.VisualBasic.Left(cmd.CommandText, cmd.CommandText.Length - 2)
                    cmd.CommandText &= " WHERE PathOutcomes.PathID = " & row.Item("ID") & " AND PathOutcomes.PathCount = " & row.Item("PathCount")
                    cmd.Connection = dbName

                    Try
                        cmd.ExecuteNonQuery()

                        'DB change successful, now update GamePath()
                        Dim n As Integer = Array.IndexOf(iPathCount, row.Item("ID"))
                        Dim o As Integer = row.Item("PathCount")
                        If GamePath(n).OutcomeCount > 0 Then
                            If szOutcomeName.Length > 0 Then GamePath(n).OutcomeProp(o - 1).EventName = szOutcomeName
                            If szOutcomeType.Length > 0 Then GamePath(n).OutcomeProp(o - 1).Outcome = GetOutcomeFromString(szOutcomeType)
                        End If

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                        Exit Function
                End Try
                End If
            Next
            dbName.Close()
        End If

        'Add items to analysis criteria lists
        If szTimeCriteria.Length > 0 Then frmAnalysis.AddTimeCriterion(szTimeCriteria)
        If szTeamName.Length > 0 Then frmAnalysis.AddPlayers(szTeamName)
        If szOutcomeName.Length > 0 And szOutcomeType.Length > 0 Then frmAnalysis.AddDescriptors(szOutcomeName, GetOutcomeFromString(szOutcomeType))

        Return True

    End Function

End Class
