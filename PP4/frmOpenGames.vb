Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System
Imports System.IO


Public Class frmOpenGames
    Dim nNodeCount As Integer = 0
    Dim nLastCount As Integer   'Holds the number of records in the most recently selected node.
    Dim boolGameLoaded As Boolean = False   'True if at least one game has been loaded, triggers the append function when loading new games.
    Dim CollectionSelected As Boolean = False
    Dim szCollections(0) As TreeNode
    Dim CONNECT_STRING As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & UserPrefs.dbPath '& "; Persist Security Info=False"


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        'Insert code here --> open game
        If Not tvGames.SelectedNode Is Nothing Then
            If Microsoft.VisualBasic.Left(tvGames.SelectedNode.Name, 4) = "col_" Then
                For Each item As TreeNode In Me.tvGames.SelectedNode.Nodes
                    If item.Parent.Text = tvGames.SelectedNode.Text Then
                        nLastCount = GetRecordCount(item.Text)
                        If GetRecords(item.Text, nLastCount, boolGameLoaded) > 0 Then boolGameLoaded = True
                    End If
                Next

            Else
                Me.ShowGamePreview(tvGames.SelectedNode, True)
                If GetRecords(tvGames.SelectedNode.Text, nLastCount, boolGameLoaded) > 0 Then boolGameLoaded = True
            End If

        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmOpenGames_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmOpenGames_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'check database
        If Not File.Exists(UserPrefs.dbPath) Then
            MsgBox("File 'gamePath.mdb' not found." & vbCr & "Please review database options.", vbCritical, Application.ProductName)
            Exit Sub
        End If

        Me.AllowTransparency = True

        'Connect to Database
        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        'Set OleDB command to search for collections
        Dim strSQL As New OleDbCommand("SELECT DISTINCT Collection FROM GameData", dbName)
        Dim dbReader As OleDbDataReader = Nothing
        Try
            dbReader = strSQL.ExecuteReader()
        Catch ex As Exception
            MsgBox("An error occured attempting to open the games database.  It is possible that the database needs updating." & _
            vbCr & "Refer to the 'Database' tab in Options (F5)", MsgBoxStyle.Critical, Application.ProductName)
            dbName.Close()
            Exit Sub
        End Try

        'Set OleDB command to search for GameIDs
        Dim strSQL2 As OleDbCommand
        Dim dbReader2 As OleDbDataReader

        'Put Collections to treeview
        With tvGames
            Do While dbReader.Read()
                If Len(dbReader.Item("Collection").ToString) > 0 Then
                    nNodeCount = nNodeCount + 1
                    ReDim szCollections(nNodeCount)
                    szCollections(nNodeCount) = Me.tvGames.Nodes.Add("col_" & dbReader.Item("Collection").ToString, dbReader.Item("Collection").ToString, 1, 1)

                    'Add Games under collections
                    strSQL2 = New OleDbCommand("SELECT GameID FROM GameData WHERE Collection = '" _
                    & dbReader.Item("Collection").ToString & "' ORDER BY GameID", dbName)
                    dbReader2 = strSQL2.ExecuteReader()
                    Do While dbReader2.Read()
                        szCollections(nNodeCount).Nodes.Add(dbReader2.Item("GameID".ToString), dbReader2.Item("GameID".ToString), 6, 6)
                    Loop
                End If
            Loop

            'Set OleDB command to search for standalone GameIDs
            strSQL = New OleDbCommand("SELECT * FROM GameData ORDER BY GameID", dbName)
            dbReader = strSQL.ExecuteReader()
            Do While dbReader.Read()
                If Len(dbReader.Item("Collection").ToString) = 0 Then
                    nNodeCount = nNodeCount + 1
                    ReDim szCollections(nNodeCount)
                    szCollections(nNodeCount) = Me.tvGames.Nodes.Add(dbReader.Item("GameID").ToString, dbReader.Item("GameID").ToString, 6, 6)
                End If
            Loop
        End With

        dbName.Close()


    End Sub

    Private Sub tvGames_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvGames.AfterCheck
        'e.Node.StateImageIndex = 5
    End Sub

    Private Sub tvGames_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvGames.AfterLabelEdit
        Dim nNode As TreeNode

        If Microsoft.VisualBasic.Left(e.Node.Name, 4) = "col_" Then
            'This is a collection - rename it...
            For Each nNode In e.Node.Nodes
                Me.AddCollectionField(nNode.Text, e.Label)
            Next
        Else
            'This is a gameid - rename it...
            RenameDatabaseItems("GameData", "GameID", e.Node.Text, e.Label)
            RenameDatabaseItems("PathData", "GameID", e.Node.Text, e.Label)
            RenameDatabaseItems("PathOutcomes", "GameID", e.Node.Text, e.Label)
        End If

        tvGames.LabelEdit = False
    End Sub

    Private Sub tvGames_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvGames.DragDrop
        Dim NewNode As TreeNode
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            Dim pt As Point
            Dim DestinationNode As TreeNode
            pt = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
            DestinationNode = CType(sender, TreeView).GetNodeAt(pt)
            NewNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
            'Ensure that the destination is a collection
            If Microsoft.VisualBasic.Left(DestinationNode.Name, 4) = "col_" And _
            Microsoft.VisualBasic.Left(NewNode.Name, 4) <> "col_" Then
                'Remove original node and add new collection name
                If AddCollectionField(NewNode.Text, DestinationNode.Text) Then
                    DestinationNode.Nodes.Add(NewNode.Clone)
                    NewNode.Remove()
                End If
            End If
        End If

    End Sub

    Private Sub tvGames_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvGames.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub tvGames_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvGames.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub tvGames_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvGames.KeyUp
        ShowGamePreview(tvGames.SelectedNode, True)
    End Sub

    Private Sub tvGames_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvGames.MouseDown
        tvGames.SelectedNode = tvGames.HitTest(e.X, e.Y).Node
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'Verify whether mouse over space, collection or game
            Try
                Select Case Microsoft.VisualBasic.Left(tvGames.SelectedNode.Name, 4)
                    Case Is = "col_"
                        Me.ContextMenuStrip_OverCollection.Show(Me, e.X, e.Y)
                    Case Is <> "col_"
                        Me.ContextMenuStrip_OverGame.Show(Me, e.X, e.Y)
                    Case Else
                        Me.ContextMenuStrip_OverEmptySpace.Show(Me, e.X, e.Y)
                End Select

            Catch ex As Exception
                'Probably empty space...
                Me.ContextMenuStrip_OverEmptySpace.Show(Me, e.X, e.Y)
            End Try


        End If
    End Sub

    Private Sub mnuRemoveCollection_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mnuRemoveCollection2_Click(sender, e)
    End Sub

    Private Sub mnuNewCollection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuNewCollection.Click
        frmCreateCollection.Show(Me)

    End Sub

    Public Sub SetNewCollection(ByVal szCollectionName As String, Optional ByVal szKeyword As String = "")
        nNodeCount = nNodeCount + 1
        ReDim szCollections(nNodeCount)
        szCollections(nNodeCount) = Me.tvGames.Nodes.Add("col_" & szCollectionName, szCollectionName, 1, 1)

        If Len(szKeyword) > 0 Then
            'Search for gameIDs including keyword.
            'Check before relocating games.
            Dim nNode As TreeNode
            For Each nNode In tvGames.Nodes
                'NB - this loop only includes parent nodes as the others are all added as children on the primary nodes.
                If nNode.Text.Contains(szKeyword) And (nNode.Text <> szCollectionName) Then
                    szCollections(nNodeCount).Nodes.Add("col_" & nNode.Text, nNode.Text, 6, 6)
                    If Not AddCollectionField(nNode.Text, szCollectionName) Then
                        MsgBox("Operation failed!  There was a problem creating this collection.", MsgBoxStyle.Critical, Application.ProductName)
                    End If
                    nNode.Remove()

                End If

            Next
        End If
        tvGames.Sort()
        szCollections(nNodeCount).EnsureVisible()
    End Sub

    Private Sub ShowGamePreview(ByVal nNode As TreeNode, Optional ByVal boolShowRecords As Boolean = False)

        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()

        'Set OleDB command to search for collections
        Dim strSQL As New OleDbCommand("SELECT * FROM GameData WHERE GameID = '" & nNode.Text & "'", dbName)
        Dim dbReader As OleDbDataReader = strSQL.ExecuteReader()

        'Put data to preview
        Do While dbReader.Read()
            Me.lblCollection.Text = dbReader.Item("Collection").ToString
            Me.lblGameDate.Text = dbReader.GetDateTime(1).ToLongDateString
            Me.lblGameID.Text = dbReader.Item("GameID").ToString
            Me.lblGameOpponent.Text = dbReader.Item("GameOpponent").ToString
            Me.lblGameVenue.Text = dbReader.Item("GameVenue").ToString
            Me.lblNotes.Text = dbReader.Item("GameNotes").ToString
            If boolShowRecords Then
                nLastCount = GetRecordCount(Me.lblGameID.Text)
                Me.lblRecordCount.Text = nLastCount.ToString
            Else
                Me.lblRecordCount.Text = ""
                nLastCount = 0
            End If
        Loop

        dbReader.Close()

    End Sub

    Private Sub tvGames_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvGames.NodeMouseClick
        If Microsoft.VisualBasic.Left(e.Node.Name, 4) <> "col_" Then
            tvGames.SelectedNode = e.Node
            ShowGamePreview(e.Node, True)
        End If
    End Sub

    Private Sub tvGames_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvGames.NodeMouseDoubleClick
        If Microsoft.VisualBasic.Left(e.Node.Name, 4) = "col_" Then
            tvGames.SelectedNode = e.Node
            ShowGamePreview(e.Node, True)
        Else
            e.Node.EnsureVisible()

        End If

    End Sub

    Private Function AddCollectionField(ByVal szGameID As String, ByVal szCollectionName As String) As Boolean

        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter("SELECT * FROM GameData WHERE GameID = '" & szGameID & "'", _
            CONNECT_STRING)

        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            Dim dbName As New OleDbConnection(CONNECT_STRING)
            dbName.Open()

            Dim cmd As New OleDbCommand("UPDATE GameData SET Collection = ? WHERE (GameID = ?)", dbName)
            cmd.Parameters.Add(New OleDbParameter("Collection", szCollectionName))
            cmd.Parameters.Add(New OleDbParameter("GameID", szGameID))

            Try
                cmd.ExecuteNonQuery()
                AddCollectionField = True

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                AddCollectionField = False

            End Try
            dbName.Close()
        End If

    End Function

    Private Sub mnuNewCollection2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewCollection2.Click
        frmCreateCollection.Show(Me)
    End Sub

    Private Sub mnuRemoveCollection2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveCollection2.Click
        Dim res As DialogResult
        res = MessageBox.Show("This action will remove the Collection Name: " & tvGames.SelectedNode.Text & "." & vbCr & _
            "Do you want to delete the GameID's from within this collection too?" & vbCr & vbCr & _
            "Select YES to remove the GameID's and the Collection Name (WARNING: This action CANNOT be undone!" & vbCr & _
            "Select NO to preserve the GameIDs but remove the Collection Name" & vbCr & _
            "Select CANCEL to abort this action.", "Delete Collection Name", _
            MessageBoxButtons.YesNoCancel, _
            MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button3)

        If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim nNode As TreeNode
        For Each nNode In tvGames.SelectedNode.Nodes
            If res = Windows.Forms.DialogResult.No Then
                'Move node children (Games) into the root of the treeview (only if being preserved).
                Me.AddCollectionField(nNode.Text, "")
                tvGames.Nodes.Add(nNode.Text, nNode.Text, 6, 6)
            ElseIf res = Windows.Forms.DialogResult.Yes Then
                'Delete the GameIDs from the database.
                RemoveGameID(nNode.Text)
            End If
        Next
        For Each nNode In tvGames.SelectedNode.Nodes
            nNode.Remove()
        Next
        tvGames.SelectedNode.Remove()

    End Sub

    Private Sub mnuRenameCollection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRenameCollection.Click
        tvGames.LabelEdit = True
        tvGames.SelectedNode.BeginEdit()
    End Sub


    Private Function RenameDatabaseItems(ByVal szTableName As String, ByVal szField As String, _
        ByVal szOldValue As String, ByVal szNewValue As String) As Boolean


        Dim dbName As New OleDbConnection(CONNECT_STRING)
        dbName.Open()
        Dim cmd As New OleDbCommand("UPDATE " & szTableName & " SET " & szField & " = ? WHERE (" & szField & " = ?)", dbName)
        cmd.Parameters.Add(New OleDbParameter(szField, szNewValue))
        cmd.Parameters.Add(New OleDbParameter(szField, szOldValue))

        Try
            cmd.ExecuteNonQuery()
            RenameDatabaseItems = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            RenameDatabaseItems = False

        End Try

        dbName.Close()

        ShowGamePreview(tvGames.SelectedNode)

    End Function

    Private Sub mnuRemoveGame_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveGame.Click
        Dim res As DialogResult
        res = MessageBox.Show("This action will permenantly DELETE the GameID: " & tvGames.SelectedNode.Text & "." & vbCr & _
            "This action CANNOT be undone!" & vbCr & _
            "Select CANCEL to abort this action.", "Deleting GameID", _
            MessageBoxButtons.OKCancel, _
            MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button3)

        If res = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If RemoveGameID(tvGames.SelectedNode.Text) Then
            tvGames.SelectedNode.Remove()
        Else
            MessageBox.Show("There was a problem deleting the following GameID: " & tvGames.SelectedNode.Text, _
                "Deleting GameID", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub mnuRenameGame_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRenameGame.Click
        tvGames.LabelEdit = True
        tvGames.SelectedNode.BeginEdit()
    End Sub

    Private Sub tvGames_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvGames.AfterSelect
        If Microsoft.VisualBasic.Left(tvGames.SelectedNode.Name, 4) = "col_" Then
            Me.lblCollection.Text = Me.tvGames.SelectedNode.Text
            Me.lblGameDate.Text = "*"
            Me.lblGameID.Text = "*"
            Me.lblGameOpponent.Text = "*"
            Me.lblGameVenue.Text = "*"
            Me.lblNotes.Text = "*"

        End If
    End Sub

    Private Sub frmOpenGames_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Validated

    End Sub
End Class
