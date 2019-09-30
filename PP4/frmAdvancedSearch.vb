Imports System.Windows.Forms

Public Class frmAdvancedSearch

    Dim itemList As CheckedListBox = Nothing

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        frmAnalysis.chkUseAdvancedSearch.Checked = False
        AdvancedSearch = Nothing

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chkAND_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAND1.CheckedChanged
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub RefreshCheckList(ByVal CheckBox As CheckedListBox, ByVal DropList As ComboBox)
        CheckBox.Items.Clear()
        Select Case DropList.Text
            Case Is = "Game ID"
                For Each item As GameProperties In GamesCurrentlyOpen
                    CheckBox.Items.Add(item.GameID, True)
                Next

            Case Is = "Team Name"
                For Each item As String In frmAnalysis.cboTeamName.Items
                    If item <> "*" Then CheckBox.Items.Add(item, True)
                Next

            Case Is = "Time Criteria"
                For Each item As String In frmAnalysis.cboTimeCriteria.Items
                    If item <> "*" Then CheckBox.Items.Add(item, True)
                Next

            Case Is = "Event Name"
                For Each item As String In frmAnalysis.lstDescriptors.Items
                    If item <> "*" Then CheckBox.Items.Add(item, True)
                Next

            Case Is = "Game Author"
                Dim items() As String = Nothing
                Dim index As Integer = 0
                For Each item As GameProperties In GamesCurrentlyOpen
                    ReDim Preserve items(index)
                    items(index) = item.GameAuthor
                    index += 1
                Next

                For Each item As String In ExclusiveList(items)
                    CheckBox.Items.Add(item, True)
                Next

            Case Is = "Game Opponent"
                Dim items() As String = Nothing
                Dim index As Integer = 0
                For Each item As GameProperties In GamesCurrentlyOpen
                    ReDim Preserve items(index)
                    items(index) = item.GameOpponent
                    index += 1
                Next

                For Each item As String In ExclusiveList(items)
                    CheckBox.Items.Add(item, True)
                Next

            Case Is = "Game Venue"
                Dim items() As String = Nothing
                Dim index As Integer = 0
                For Each item As GameProperties In GamesCurrentlyOpen
                    ReDim Preserve items(index)
                    items(index) = item.GameVenue
                    index += 1
                Next

                For Each item As String In ExclusiveList(items)
                    CheckBox.Items.Add(item, True)
                Next

            Case Is = "Region"
                Select Case UserPrefs.Sport
                    Case tSports.sHockey
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyOffensiveCircle), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyOffensive25), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyOffensiveHalf), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyDefensiveHalf), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyDefensive25), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.HockeyDefensiveCircle), True)

                    Case tSports.sNetball
                        CheckBox.Items.Add(GetRegionString(tRegion.NetballAttackCircle), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.NetballAttackThird), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.NetballMiddleThird), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.NetballDefensiveThird), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.NetballDefensiveCircle), True)

                    Case tSports.sRugbyLeague
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_AttGoalArea), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att10ToGoal), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att20To10), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att30To20), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att40To30), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att50To40), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def50To40), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def40To30), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def30To20), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def20To10), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def10ToGoal), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.RugbyLeague_DefGoalArea), True)

                    Case tSports.sBasketball
                        CheckBox.Items.Add(GetRegionString(tRegion.Basketball_AttackCircle), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Basketball_AttackCourt), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Basketball_DefensiveCourt), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Basketball_DefensiveCircle), True)

                    Case tSports.sRugby7
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_InGoal), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_22), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_Half), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_Half), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_22), True)
                        CheckBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_InGoal), True)
                End Select


        End Select

    End Sub

    Private Function ExclusiveList(ByVal szStringArray() As String) As String()
        If szStringArray Is Nothing Then Return Nothing
        Dim UniqueCollection() As String = Nothing
        Dim Index As Integer = 0

        For Each item As String In szStringArray
            If Not UniqueCollection Is Nothing Then
                Dim MatchFound As Boolean = False
                For Each ExistingItem As String In UniqueCollection
                    If ExistingItem = item Then
                        MatchFound = True
                        Exit For
                    End If
                Next
                If Not MatchFound Then
                    Index += 1
                    ReDim Preserve UniqueCollection(Index)
                    UniqueCollection(Index) = item
                End If
            Else
                ReDim UniqueCollection(Index)
                UniqueCollection(Index) = item
            End If
        Next

        Return UniqueCollection
    End Function

    Private Sub DropDown1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDown1.SelectedValueChanged
        If DropDown1.Text = "None" Then
            Me.chkCriteria1.Items.Clear()
            Me.chkCriteria1.Enabled = False
        Else
            Me.chkCriteria1.Enabled = True
            RefreshCheckList(Me.chkCriteria1, Me.DropDown1)
        End If
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub DropDown2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDown2.SelectedValueChanged
        If DropDown2.Text = "None" Then
            Me.chkCriteria2.Items.Clear()
            Me.chkCriteria2.Enabled = False
        Else
            Me.chkCriteria2.Enabled = True
            RefreshCheckList(Me.chkCriteria2, Me.DropDown2)
        End If
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub DropDown3_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDown3.SelectedValueChanged
        If DropDown3.Text = "None" Then
            Me.chkCriteria3.Items.Clear()
            Me.chkCriteria3.Enabled = False
        Else
            Me.chkCriteria3.Enabled = True
            RefreshCheckList(Me.chkCriteria3, Me.DropDown3)
        End If
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub DropDown4_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDown4.SelectedValueChanged
        If DropDown1.Text = "None" Then
            Me.chkCriteria4.Items.Clear()
            Me.chkCriteria4.Enabled = False
        Else
            Me.chkCriteria4.Enabled = True
            RefreshCheckList(Me.chkCriteria4, Me.DropDown4)
        End If
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Function CompileAdvancedSearchString() As String
        Dim szTemp As String = Nothing  'Used to temporarily collate selections into either szEV or szSQL
        Dim szEV As String = Nothing    'Event names exluded from SQL search.
        Dim szSQL As String = Nothing

        Erase AdvancedSearch.EventNameBoolean
        Erase AdvancedSearch.EventNameSet
        AdvancedSearch.DBSearchString = Nothing
        Dim EventNameComboCount As Integer = -1
        Erase AdvancedSearch.Regions
        Erase AdvancedSearch.TeamName
        Erase AdvancedSearch.TimeCriteria
        Erase AdvancedSearch.GameID

        If DropDown1.Text <> "None" Then
            Dim CheckedItems As CheckedListBox.CheckedItemCollection = Me.chkCriteria1.CheckedItems

            If CheckedItems.Count > 0 Then
                AddSelectionToSearchHistory(DropDown1.Text, CheckedItems)

                'First, compile search string for this component.
                szTemp = "(" & GetField(DropDown1.Text) & " = "
                For i As Integer = 0 To CheckedItems.Count - 1

                    If DropDown1.Text = "Region" Then
                        szTemp = szTemp & CInt(GetRegionFromString(CheckedItems(i).ToString))
                    Else
                        szTemp = szTemp & "'" & CheckedItems(i).ToString & "'"
                    End If

                    If i < CheckedItems.Count - 1 Then
                        If chkAND1.Checked Then
                            szTemp = szTemp & " AND " & GetField(DropDown1.Text) & " = "
                        Else
                            szTemp = szTemp & " OR " & GetField(DropDown1.Text) & " = "
                        End If
                    End If

                Next
                szTemp = szTemp & ")"

                'Second, add items to advanced event name search array if required.
                If DropDown1.Text = "Event Name" Then
                    For i As Integer = 0 To CheckedItems.Count - 1
                        EventNameComboCount += 1
                        ReDim Preserve AdvancedSearch.EventNameSet(EventNameComboCount)
                        AdvancedSearch.EventNameSet(EventNameComboCount) = CheckedItems(i).ToString
                        ReDim Preserve AdvancedSearch.EventNameBoolean(EventNameComboCount)
                        AdvancedSearch.EventNameBoolean(EventNameComboCount) = chkAND1.Checked
                    Next

                    If Len(szEV) > 0 Then
                        szEV = szEV & " OR " & szTemp
                    Else
                        szEV = szTemp
                    End If


                Else
                    If Len(szSQL) > 0 Then
                        szSQL = szSQL & " AND " & szTemp
                    Else
                        szSQL = szTemp
                    End If

                End If

            End If
        Else
            Return Nothing

        End If

        If DropDown2.Text <> "None" Then
            Dim CheckedItems As CheckedListBox.CheckedItemCollection = Me.chkCriteria2.CheckedItems

            If CheckedItems.Count > 0 Then
                AddSelectionToSearchHistory(DropDown2.Text, CheckedItems)

                'First, compile search string for this component.
                szTemp = "(" & GetField(DropDown2.Text) & " = "
                For i As Integer = 0 To CheckedItems.Count - 1
                    If DropDown2.Text = "Region" Then
                        szTemp = szTemp & CInt(GetRegionFromString(CheckedItems(i).ToString))
                    Else
                        szTemp = szTemp & "'" & CheckedItems(i).ToString & "'"
                    End If

                    If i < CheckedItems.Count - 1 Then
                        If chkAND2.Checked Then
                            szTemp = szTemp & " AND " & GetField(DropDown2.Text) & " = "
                        Else
                            szTemp = szTemp & " OR " & GetField(DropDown2.Text) & " = "
                        End If
                    End If

                Next
                szTemp = szTemp & ")"

                'Second, add items to advanced event name search array if required.
                If DropDown2.Text = "Event Name" Then
                    For i As Integer = 0 To CheckedItems.Count - 1
                        EventNameComboCount += 1
                        ReDim Preserve AdvancedSearch.EventNameSet(EventNameComboCount)
                        AdvancedSearch.EventNameSet(EventNameComboCount) = CheckedItems(i).ToString
                        ReDim Preserve AdvancedSearch.EventNameBoolean(EventNameComboCount)
                        AdvancedSearch.EventNameBoolean(EventNameComboCount) = chkAND2.Checked
                    Next

                    If Len(szEV) > 0 Then
                        szEV = szEV & " OR " & szTemp
                    Else
                        szEV = szTemp
                    End If

                Else
                    If Len(szSQL) > 0 Then
                        szSQL = szSQL & " AND " & szTemp
                    Else
                        szSQL = szTemp
                    End If
                End If

            End If
        End If

        If DropDown3.Text <> "None" Then
            Dim CheckedItems As CheckedListBox.CheckedItemCollection = Me.chkCriteria3.CheckedItems

            If CheckedItems.Count > 0 Then
                AddSelectionToSearchHistory(DropDown3.Text, CheckedItems)

                'First, compile search string for this component.
                szTemp = "(" & GetField(DropDown3.Text) & " = "
                For i As Integer = 0 To CheckedItems.Count - 1
                    If DropDown3.Text = "Region" Then
                        szTemp = szTemp & CInt(GetRegionFromString(CheckedItems(i).ToString))
                    Else
                        szTemp = szTemp & "'" & CheckedItems(i).ToString & "'"
                    End If

                    If i < CheckedItems.Count - 1 Then
                        If chkAND3.Checked Then
                            szTemp = szTemp & " AND " & GetField(DropDown3.Text) & " = "
                        Else
                            szTemp = szTemp & " OR " & GetField(DropDown3.Text) & " = "
                        End If
                    End If

                Next
                szTemp = szTemp & ")"

                'Second, add items to advanced event name search array if required.
                If DropDown3.Text = "Event Name" Then
                    For i As Integer = 0 To CheckedItems.Count - 1
                        EventNameComboCount += 1
                        ReDim Preserve AdvancedSearch.EventNameSet(EventNameComboCount)
                        AdvancedSearch.EventNameSet(EventNameComboCount) = CheckedItems(i).ToString
                        ReDim Preserve AdvancedSearch.EventNameBoolean(EventNameComboCount)
                        AdvancedSearch.EventNameBoolean(EventNameComboCount) = chkAND3.Checked
                    Next

                    If Len(szEV) > 0 Then
                        szEV = szEV & " OR " & szTemp
                    Else
                        szEV = szTemp
                    End If


                Else
                    If Len(szSQL) > 0 Then
                        szSQL = szSQL & " AND " & szTemp
                    Else
                        szSQL = szTemp
                    End If
                End If

            End If
        End If


        If DropDown4.Text <> "None" Then
            Dim CheckedItems As CheckedListBox.CheckedItemCollection = Me.chkCriteria4.CheckedItems

            If CheckedItems.Count > 0 Then
                AddSelectionToSearchHistory(DropDown4.Text, CheckedItems)

                'First, compile search string for this component.
                szTemp = "(" & GetField(DropDown4.Text) & " = "
                For i As Integer = 0 To CheckedItems.Count - 1
                    If DropDown4.Text = "Region" Then
                        szTemp = szTemp & CInt(GetRegionFromString(CheckedItems(i).ToString))
                    Else
                        szTemp = szTemp & "'" & CheckedItems(i).ToString & "'"
                    End If

                    If i < CheckedItems.Count - 1 Then
                        If chkAND4.Checked Then
                            szTemp = szTemp & " AND " & GetField(DropDown4.Text) & " = "
                        Else
                            szTemp = szTemp & " OR " & GetField(DropDown4.Text) & " = "
                        End If
                    End If

                Next
                szTemp = szTemp & ")"

                'Second, add items to advanced event name search array if required.
                If DropDown4.Text = "Event Name" Then
                    For i As Integer = 0 To CheckedItems.Count - 1
                        EventNameComboCount += 1
                        ReDim Preserve AdvancedSearch.EventNameSet(EventNameComboCount)
                        AdvancedSearch.EventNameSet(EventNameComboCount) = CheckedItems(i).ToString
                        ReDim Preserve AdvancedSearch.EventNameBoolean(EventNameComboCount)
                        AdvancedSearch.EventNameBoolean(EventNameComboCount) = chkAND4.Checked
                    Next

                    If Len(szEV) > 0 Then
                        szEV = szEV & " OR " & szTemp
                    Else
                        szEV = szTemp
                    End If


                Else
                    If Len(szSQL) > 0 Then
                        szSQL = szSQL & " AND " & szTemp
                    Else
                        szSQL = szTemp
                    End If

                End If

            End If
        End If


        AdvancedSearch.DBSearchString = szSQL  'This string doesn't include reference to the event names.

        'Now add descriptors.
        'NB: There is a difficulty in obtaining sets of descriptors in AND-based searches because a simple 'a AND b AND c' database
        'search will fail (event items are stored as separate lines in the DB, so are now directly connectable.
        'Hence, for advanced searches that require confirmation that item A exists in the same play as item B, a dual search model
        'is employed.  All that can be derived directly from the DB, such as time criteria, GameID etc, is included in the SQL statement.
        'But, eventnames are added to a memory-based array so that after the short list of possible matches is derived from the DB using the SQL
        'search, a final cross-match routine is employed to compare the PathCollection array with the sets of eventnames selected using the
        'search mechanisms.
        If Len(szSQL) > 0 And Len(szEV) > 0 Then
            Return szSQL & " AND " & szEV
        ElseIf Len(szSQL) = 0 And Len(szEV) > 0 Then
            Return szEV
        ElseIf Len(szSQL) > 0 And Len(szEV) = 0 Then
            Return szSQL
        Else
            Return Nothing
        End If

    End Function

    Private Sub AddSelectionToSearchHistory(ByVal szFieldName As String, ByVal chkList As CheckedListBox.CheckedItemCollection)

        Select Case szFieldName
            Case Is = "Game ID"
                ReDim AdvancedSearch.GameID(chkList.Count - 1)
                For i As Integer = 0 To chkList.Count - 1
                    AdvancedSearch.GameID(i) = chkList.Item(i)
                Next

            Case Is = "Team Name"
                ReDim AdvancedSearch.TeamName(chkList.Count - 1)
                For i As Integer = 0 To chkList.Count - 1
                    AdvancedSearch.TeamName(i) = chkList.Item(i)
                Next

            Case Is = "Time Criteria"
                ReDim AdvancedSearch.TimeCriteria(chkList.Count - 1)
                For i As Integer = 0 To chkList.Count - 1
                    AdvancedSearch.TimeCriteria(i) = chkList.Item(i)
                Next

            Case Is = "Region"
                ReDim AdvancedSearch.Regions(chkList.Count - 1)
                For i As Integer = 0 To chkList.Count - 1
                    AdvancedSearch.Regions(i) = GetRegionFromString(chkList.Item(i))
                Next


        End Select



    End Sub

    Private Function GetField(ByVal szDropdownTerm As String) As String

        Select Case szDropdownTerm
            Case Is = "Game ID"
                Return "PathData.GameID"
            Case Is = "Team Name"
                Return "TeamName"
            Case Is = "Time Criteria"
                Return "TimeCriteria"
            Case Is = "Game Author"
                Return "GameAuthor"
            Case Is = "Game Opponent"
                Return "GameOpponent"
            Case Is = "Event Name"
                Return "EventName"
            Case Is = "Game Venue"
                Return "GameVenue"
            Case Else
                Return szDropdownTerm
        End Select

    End Function

    Private Sub chkCriteria1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            itemList = chkCriteria1
            Me.AnalysisMenu.Show(Me.chkCriteria1, e.X, e.Y)
        End If
    End Sub


    Private Sub chkCriteria1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria1.MouseUp
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkCriteria2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            itemList = chkCriteria2
            Me.AnalysisMenu.Show(Me.chkCriteria2, e.X, e.Y)
        End If
    End Sub

 
    Private Sub chkCriteria2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria2.MouseUp
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkCriteria3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria3.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            itemList = chkCriteria3
            Me.AnalysisMenu.Show(Me.chkCriteria3, e.X, e.Y)
        End If
    End Sub

    Private Sub chkCriteria3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria3.MouseUp
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkCriteria4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria4.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            itemList = chkCriteria4
            Me.AnalysisMenu.Show(Me.chkCriteria4, e.X, e.Y)
        End If
    End Sub

    Private Sub chkCriteria4_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkCriteria4.MouseUp
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkAND2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAND2.CheckedChanged
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkAND3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAND3.CheckedChanged
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub chkAND4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAND4.CheckedChanged
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()

    End Sub

    Private Sub lblPreviewSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPreviewSQL.Click

    End Sub

    Private Sub chkCriteria1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCriteria1.SelectedIndexChanged

    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        If itemList.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To itemList.Items.Count - 1
            itemList.SetItemChecked(i, True)
        Next
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub mnuSelectNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSelectNone.Click
        If itemList.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To itemList.Items.Count - 1
            itemList.SetItemChecked(i, False)
        Next
        Me.lblPreviewSQL.Text = CompileAdvancedSearchString()
    End Sub

    Private Sub frmAdvancedSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
