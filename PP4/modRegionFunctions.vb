Imports System.Data.OleDb
Module modRegionFunctions

    Public Sub ShowRegionList(ByVal ListBox As CheckedListBox, ByVal MySport As tSports)

        ListBox.Items.Clear()

        Select Case MySport
            Case tSports.sHockey
                ListBox.Items.Add(GetRegionString(tRegion.HockeyOffensiveCircle), CheckIncludedRegion(tRegion.HockeyOffensiveCircle))
                ListBox.Items.Add(GetRegionString(tRegion.HockeyOffensive25), CheckIncludedRegion(tRegion.HockeyOffensive25))
                ListBox.Items.Add(GetRegionString(tRegion.HockeyOffensiveHalf), CheckIncludedRegion(tRegion.HockeyOffensiveHalf))
                ListBox.Items.Add(GetRegionString(tRegion.HockeyDefensiveHalf), CheckIncludedRegion(tRegion.HockeyDefensiveHalf))
                ListBox.Items.Add(GetRegionString(tRegion.HockeyDefensive25), CheckIncludedRegion(tRegion.HockeyDefensive25))
                ListBox.Items.Add(GetRegionString(tRegion.HockeyDefensiveCircle), CheckIncludedRegion(tRegion.HockeyDefensiveCircle))

            Case tSports.sAFL
                ListBox.Items.Add(GetRegionString(tRegion.rgAFLForward50), CheckIncludedRegion(tRegion.rgAFLForward50))
                ListBox.Items.Add(GetRegionString(tRegion.rgAFLForwardFlank), CheckIncludedRegion(tRegion.rgAFLForwardFlank))
                ListBox.Items.Add(GetRegionString(tRegion.rgAFLCentreCorridor), CheckIncludedRegion(tRegion.rgAFLCentreCorridor))
                ListBox.Items.Add(GetRegionString(tRegion.rgAFLDefensiveFlank), CheckIncludedRegion(tRegion.rgAFLDefensiveFlank))
                ListBox.Items.Add(GetRegionString(tRegion.rgAFLBack50), CheckIncludedRegion(tRegion.rgAFLBack50))

            Case tSports.sRugbyLeague
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_AttGoalArea), CheckIncludedRegion(tRegion.RugbyLeague_AttGoalArea))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att10ToGoal), CheckIncludedRegion(tRegion.RugbyLeague_Att10ToGoal))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att20To10), CheckIncludedRegion(tRegion.RugbyLeague_Att20To10))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att30To20), CheckIncludedRegion(tRegion.RugbyLeague_Att30To20))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att40To30), CheckIncludedRegion(tRegion.RugbyLeague_Att40To30))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Att50To40), CheckIncludedRegion(tRegion.RugbyLeague_Att50To40))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def50To40), CheckIncludedRegion(tRegion.RugbyLeague_Def50To40))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def40To30), CheckIncludedRegion(tRegion.RugbyLeague_Def40To30))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def30To20), CheckIncludedRegion(tRegion.RugbyLeague_Def30To20))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def20To10), CheckIncludedRegion(tRegion.RugbyLeague_Def20To10))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_Def10ToGoal), CheckIncludedRegion(tRegion.RugbyLeague_Def10ToGoal))
                ListBox.Items.Add(GetRegionString(tRegion.RugbyLeague_DefGoalArea), CheckIncludedRegion(tRegion.RugbyLeague_DefGoalArea))

            Case tSports.sNetball
                ListBox.Items.Add(GetRegionString(tRegion.NetballAttackCircle), CheckIncludedRegion(tRegion.NetballAttackCircle))
                ListBox.Items.Add(GetRegionString(tRegion.NetballAttackThird), CheckIncludedRegion(tRegion.NetballAttackThird))
                ListBox.Items.Add(GetRegionString(tRegion.NetballMiddleThird), CheckIncludedRegion(tRegion.NetballMiddleThird))
                ListBox.Items.Add(GetRegionString(tRegion.NetballDefensiveThird), CheckIncludedRegion(tRegion.NetballDefensiveThird))
                ListBox.Items.Add(GetRegionString(tRegion.NetballDefensiveCircle), CheckIncludedRegion(tRegion.NetballDefensiveCircle))

            Case tSports.sSoccer
                ListBox.Items.Add(GetRegionString(tRegion.Soccer_FrontCentre), CheckIncludedRegion(tRegion.Soccer_FrontCentre))
                ListBox.Items.Add(GetRegionString(tRegion.Soccer_FrontThird), CheckIncludedRegion(tRegion.Soccer_FrontThird))
                ListBox.Items.Add(GetRegionString(tRegion.Soccer_MiddleThird), CheckIncludedRegion(tRegion.Soccer_MiddleThird))
                ListBox.Items.Add(GetRegionString(tRegion.Soccer_BackThird), CheckIncludedRegion(tRegion.Soccer_BackThird))

            Case tSports.sBasketball
                ListBox.Items.Add(GetRegionString(tRegion.Basketball_AttackCircle), CheckIncludedRegion(tRegion.Basketball_AttackCircle))
                ListBox.Items.Add(GetRegionString(tRegion.Basketball_AttackCourt), CheckIncludedRegion(tRegion.Basketball_AttackCourt))
                ListBox.Items.Add(GetRegionString(tRegion.Basketball_DefensiveCourt), CheckIncludedRegion(tRegion.Basketball_DefensiveCourt))
                ListBox.Items.Add(GetRegionString(tRegion.Basketball_DefensiveCircle), CheckIncludedRegion(tRegion.Basketball_DefensiveCircle))

            Case tSports.sRugby7
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_InGoal), CheckIncludedRegion(tRegion.Rugby7_Attack_InGoal))
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_22), CheckIncludedRegion(tRegion.Rugby7_Attack_22))
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Attack_Half), CheckIncludedRegion(tRegion.Rugby7_Attack_Half))
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_Half), CheckIncludedRegion(tRegion.Rugby7_Defensive_Half))
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_22), CheckIncludedRegion(tRegion.Rugby7_Defensive_22))
                ListBox.Items.Add(GetRegionString(tRegion.Rugby7_Defensive_InGoal), CheckIncludedRegion(tRegion.Rugby7_Defensive_InGoal))


        End Select


    End Sub

    Private Function CheckIncludedRegion(ByVal Region As tRegion) As Boolean
        If UserPrefs.StatIncludedRegions Is Nothing Then Return False

        For Each item As tRegion In UserPrefs.StatIncludedRegions
            If item = Region Then Return True
        Next
        Return False
    End Function

    Public Function GetRegionEntriesByGameID(ByVal szGameID() As String, ByVal szTeamName As String, ByVal Region As tRegion) As Integer()
        Dim retVar(szGameID.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Game As String In szGameID
            Dim szSearchString As String = "SELECT DISTINCT PlayNumber FROM PathData WHERE GameID = '" & Game & _
                "' AND TeamName = '" & szTeamName & "' AND Region = " & Region

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

    Public Function GetRegionEntriesByTeam(ByVal szTeamName() As String, ByVal szGameID As String, ByVal Region As tRegion) As Integer()
        Dim retVar(szGameID.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Team As String In szTeamName
            Dim szSearchString As String = "SELECT DISTINCT PlayNumber FROM PathData WHERE TeamName = '" & Team & _
                "' AND GameID = '" & szGameID & "' AND Region = " & Region

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

    Public Function GetRegionOutcomesByGameID(ByVal szGameID() As String, ByVal szTeamName As String, _
        ByVal Region As tRegion, ByVal szEventName As String, _
        Optional ByVal OutcomesOnly As Boolean = True) As Integer()

        Dim retVar(szGameID.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Game As String In szGameID
            Dim szSearchString As String = "SELECT PathOutcomes.EventName FROM PathData INNER JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.GameID = '" & Game & _
                "' AND PathData.TeamName = '" & szTeamName & "' AND PathData.Region = " & Region & " AND PathOutcomes.EventName = '" & szEventName & "'"

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

    Public Function GetRegionOutcomesByTeam(ByVal szTeamName() As String, ByVal szGameID As String, _
    ByVal Region As tRegion, ByVal szEventName As String, _
    Optional ByVal OutcomesOnly As Boolean = True) As Integer()

        Dim retVar(szTeamName.Length - 1) As Integer
        Dim dt As DataTable = Nothing
        Dim da As OleDbDataAdapter = Nothing

        'Compile multiple gameID string
        For Each Team As String In szTeamName
            Dim szSearchString As String = "SELECT PathOutcomes.EventName FROM PathData INNER JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE PathData.TeamName = '" & Team & _
                "' AND PathData.GameID = '" & szGameID & "' AND PathData.Region = " & Region & " AND PathOutcomes.EventName = '" & szEventName & "'"

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

    Private Function IsInSoccerFrontThird(ByVal nPoints As PointF) As Boolean
        If nPoints.Y <= 50 Then Return True
        Return False
    End Function

    Private Function IsInSoccerFrontThirdCentre(ByVal nPoints As PointF) As Boolean
        If nPoints.Y <= 50 And nPoints.X >= 18 And nPoints.X <= 77 Then Return True
        Return False
    End Function

    Private Function IsInSoccerMiddleThird(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 50 And nPoints.Y <= 100 Then Return True
        Return False
    End Function

    Private Function IsInSoccerBackThird(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 100 Then Return True
        Return False
    End Function

    'Basketball court regions
    Private Function IsInAttacking3PointArc(ByVal nPoints As PointF) As Boolean
        'First, exclude points out of range
        Dim cX As Single = 25
        Dim cY As Single = 6
        Dim rad As Single = 19

        If nPoints.Y > 25 Then Return False
        If nPoints.X < (cX - rad) Or nPoints.X > (cX + rad) Then Return False

        If nPoints.Y <= cY And nPoints.X >= (cX - rad) And nPoints.X <= (cX + rad) Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        a = nPoints.X - cX
        b = nPoints.Y - cY
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True
    End Function

    Private Function IsInAttackingCourt(ByVal nPoints As PointF) As Boolean
        If nPoints.Y < 47 Then Return True
        Return False
    End Function
    Private Function IsInDefensive3PointArc(ByVal nPoints As PointF) As Boolean
        'First, exclude points out of range
        Dim cX As Single = 25
        Dim cY As Single = 94
        Dim rad As Single = 19

        If nPoints.Y < (cY - (6 + rad)) Then Return False
        If nPoints.X < (cX - rad) Or nPoints.X > (cX + rad) Then Return False

        If nPoints.Y >= (cY - 6) And nPoints.X >= (cX - rad) And nPoints.X <= (cX + rad) Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        a = nPoints.X - cX
        b = nPoints.Y - (cY - 6)
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True



    End Function
    Private Function IsInDefensiveCourt(ByVal nPoints As PointF) As Boolean
        If nPoints.Y >= 47 Then Return True
        Return False
    End Function

    Public Function GetRegionFromPoints(ByVal Sport As tSports, ByVal Location As PointF, ByVal CorrectionFactor As PointF, ByVal xMargin As Single, ByVal yMargin As Single) As tRegion
        Dim temp As tRegion = Nothing

        Location.X = (Location.X / CorrectionFactor.X) - xMargin
        Location.Y = (Location.Y / CorrectionFactor.Y) - yMargin

        Select Case Sport
            Case tSports.sSoccer
                temp = tRegion.None
                If IsInSoccerFrontThird(Location) Then
                    temp = tRegion.Soccer_FrontThird
                    If IsInSoccerFrontThirdCentre(Location) Then
                        temp = tRegion.Soccer_FrontCentre
                    End If
                End If
                If IsInSoccerMiddleThird(Location) Then
                    temp = tRegion.Soccer_MiddleThird
                End If
                If IsInSoccerBackThird(Location) Then
                    temp = tRegion.Soccer_BackThird
                End If

            Case tSports.sHockey
                temp = tRegion.None
                If IsInAttackingHalf(Location) Then
                    temp = tRegion.HockeyOffensiveHalf
                    If IsInAttacking25(Location) Then
                        temp = tRegion.HockeyOffensive25
                        If IsInAttacking16(Location) Then
                            temp = tRegion.HockeyOffensiveCircle
                        End If
                    End If
                End If

                If IsInDefensiveHalf(Location) Then
                    temp = tRegion.HockeyDefensiveHalf
                    If IsInDefensive25(Location) Then
                        temp = tRegion.HockeyDefensive25
                        If IsInDefensive16(Location) Then
                            temp = tRegion.HockeyDefensiveCircle
                        End If
                    End If
                End If

            Case tSports.sNetball
                temp = tRegion.None
                If IsInAttackingThird(Location) Then
                    temp = tRegion.NetballAttackThird
                    If IsInAttackingShootingCircle(Location) Then
                        temp = tRegion.NetballAttackCircle
                    End If
                End If
                If IsInDefensiveThird(Location) Then
                    temp = tRegion.NetballDefensiveThird
                    If IsInDefensiveShootingCircle(Location) Then
                        temp = tRegion.NetballDefensiveCircle
                    End If
                End If
                If IsInMiddleThird(Location) Then
                    temp = tRegion.NetballMiddleThird
                End If

            Case tSports.sRugbyLeague
                temp = tRegion.None
                If IsInAttackingGoalArea(Location) Then Return tRegion.RugbyLeague_AttGoalArea
                If IsInAttack10mToGoal(Location) Then Return tRegion.RugbyLeague_Att10ToGoal
                If IsInAttack20mTo10m(Location) Then Return tRegion.RugbyLeague_Att20To10
                If IsInAttack30mTo20m(Location) Then Return tRegion.RugbyLeague_Att30To20
                If IsInAttack40mTo30m(Location) Then Return tRegion.RugbyLeague_Att40To30
                If IsInAttack50mTo40m(Location) Then Return tRegion.RugbyLeague_Att50To40
                If IsInDefense50mTo40m(Location) Then Return tRegion.RugbyLeague_Def50To40
                If IsInDefense40mTo30m(Location) Then Return tRegion.RugbyLeague_Def40To30
                If IsInDefense30mTo20m(Location) Then Return tRegion.RugbyLeague_Def30To20
                If IsInDefense20mTo10m(Location) Then Return tRegion.RugbyLeague_Def20To10
                If IsInDefense10mToGoal(Location) Then Return tRegion.RugbyLeague_Def10ToGoal
                If IsInDefensiveGoalArea(Location) Then Return tRegion.RugbyLeague_DefGoalArea

            Case tSports.sBasketball
                temp = tRegion.None
                If IsInAttacking3PointArc(Location) Then Return tRegion.Basketball_AttackCircle
                If IsInAttackingCourt(Location) Then Return tRegion.Basketball_AttackCourt
                If IsInDefensive3PointArc(Location) Then Return tRegion.Basketball_DefensiveCircle
                If IsInDefensiveCourt(Location) Then Return tRegion.Basketball_DefensiveCourt

            Case tSports.sRugby7
                temp = tRegion.None
                If IsInRugby7_Attack_InGoal(Location) Then Return tRegion.Rugby7_Attack_InGoal
                If IsInRugby7_Attack_22(Location) Then Return tRegion.Rugby7_Attack_22
                If IsInRugby7_Attack_Half(Location) Then Return tRegion.Rugby7_Attack_Half
                If IsInRugby7_Defensive_Half(Location) Then Return tRegion.Rugby7_Defensive_Half
                If IsInRugby7_Defensive_22(Location) Then Return tRegion.Rugby7_Defensive_22
                If IsInRugby7_Defensive_InGoal(Location) Then Return tRegion.Rugby7_Defensive_InGoal

        End Select

        Return temp

    End Function

    Private Function IsInRugby7_Attack_InGoal(ByVal nPoints As PointF) As Boolean
        If nPoints.Y <= 10 Then Return True
        Return False
    End Function

    Private Function IsInRugby7_Attack_22(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 10 And nPoints.Y <= 32 Then Return True
        Return False
    End Function

    Private Function IsInRugby7_Attack_Half(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 32 And nPoints.Y <= 60 Then Return True
        Return False
    End Function

    Private Function IsInRugby7_Defensive_Half(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 60 And nPoints.Y <= 88 Then Return True
        Return False
    End Function

    Private Function IsInRugby7_Defensive_22(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 88 And nPoints.Y <= 110 Then Return True
        Return False
    End Function

    Private Function IsInRugby7_Defensive_InGoal(ByVal nPoints As PointF) As Boolean
        If nPoints.Y > 110 Then Return True
        Return False
    End Function


    Private Function IsInAttacking16(ByVal nPoints As PointF) As Boolean

        'First, exclude points out of range
        Dim cX As Single = 45
        Dim cY As Single = 0
        Dim rad As Single = 24

        If nPoints.Y > 24 Then Return False
        If nPoints.X < (cX - (rad + 3)) Or nPoints.X > (cX + rad + 3) Then Return False

        If nPoints.Y <= 24 And nPoints.X >= cX - 3 And nPoints.X <= cX + 3 Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        If nPoints.X > cX + 3 Then a = nPoints.X - (cX + 3)
        If nPoints.X < cX - 3 Then a = nPoints.X - (cX - 3)
        b = nPoints.Y - cY
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True

    End Function

    Private Function IsInAttacking25(ByVal nPoints As PointF) As Boolean
        If nPoints.Y <= 37.5 Then Return True
        Return False
    End Function

    Private Function IsInAttackingHalf(ByVal nPoints As PointF) As Boolean
        If nPoints.Y <= 75 Then Return True
        Return False
    End Function

    Private Function IsInDefensiveHalf(ByVal nPoints As PointF) As Boolean
        If nPoints.Y >= 75 Then Return True
        Return False
    End Function

    Private Function IsInDefensive25(ByVal nPoints As PointF) As Boolean
        If nPoints.Y >= 112.5 Then Return True
        Return False

    End Function

    Private Function IsInDefensive16(ByVal nPoints As PointF) As Boolean
        'First, exclude points out of range
        Dim cX As Single = 45
        Dim cY As Single = 150
        Dim rad As Single = 24

        If nPoints.Y < (150 - 24) Then Return False
        If nPoints.X < (cX - (rad + 3)) Or nPoints.X > (cX + rad + 3) Then Return False

        If nPoints.Y >= 126 And nPoints.X >= cX - 3 And nPoints.X <= cX + 3 Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        If nPoints.X > cX + 3 Then a = nPoints.X - (cX + 3)
        If nPoints.X < cX - 3 Then a = nPoints.X - (cX - 3)
        b = nPoints.Y - cY
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True

    End Function


    Private Function IsInAttackingGoalArea(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 7 Then Return True
    End Function
    Private Function IsInAttack10mToGoal(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 17 And npoints.Y > 7 Then Return True
    End Function
    Private Function IsInAttack20mTo10m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 27 And npoints.Y > 17 Then Return True
    End Function
    Private Function IsInAttack30mTo20m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 37 And npoints.Y > 27 Then Return True
    End Function
    Private Function IsInAttack40mTo30m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 47 And npoints.Y > 37 Then Return True
    End Function
    Private Function IsInAttack50mTo40m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 57 And npoints.Y > 47 Then Return True
    End Function
    Private Function IsInDefense10mToGoal(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 109 And npoints.Y > 97 Then Return True
    End Function
    Private Function IsInDefense20mTo10m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 97 And npoints.Y > 87 Then Return True
    End Function
    Private Function IsInDefense30mTo20m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 87 And npoints.Y > 77 Then Return True
    End Function
    Private Function IsInDefense40mTo30m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 77 And npoints.Y > 67 Then Return True
    End Function
    Private Function IsInDefense50mTo40m(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 67 And npoints.Y > 57 Then Return True
    End Function
    Private Function IsInDefensiveGoalArea(ByVal npoints As PointF) As Boolean
        If npoints.Y > 109 Then Return True
    End Function
    Private Function IsInAttackingShootingCircle(ByVal npoints As PointF) As Boolean
        'First, exclude points out of range
        Dim cX As Single = 45
        Dim cY As Single = 0
        Dim rad As Single = 29

        If npoints.Y > 29 Then Return False
        If npoints.X < (cX - rad) Or npoints.X > (cX + rad) Then Return False

        If npoints.Y <= 29 And npoints.X >= cX And npoints.X <= cX Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        a = npoints.X - cX
        b = npoints.Y - cY
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True
    End Function

    Private Function IsInAttackingThird(ByVal npoints As PointF) As Boolean
        If npoints.Y <= 60 Then Return True
    End Function

    Private Function IsInMiddleThird(ByVal npoints As PointF) As Boolean
        If npoints.Y > 60 And npoints.Y <= 120 Then Return True
    End Function

    Private Function IsInDefensiveThird(ByVal npoints As PointF) As Boolean
        If npoints.Y > 120 Then Return True
    End Function

    Private Function IsInDefensiveShootingCircle(ByVal npoints As PointF) As Boolean
        'First, exclude points out of range
        Dim cX As Single = 45
        Dim cY As Single = 180
        Dim rad As Single = 29

        If npoints.Y < (180 - 29) Then Return False
        If npoints.X < (cX - rad) Or npoints.X > (cX + rad) Then Return False

        If npoints.Y >= (180 - 29) And npoints.X >= cX And npoints.X <= cX Then Return True

        'If in range, then detirmine if inside circle region.
        Dim a As Single
        Dim b As Single
        Dim c As Single

        'Pythagoris... useful after all these years!
        a = npoints.X - cX
        b = npoints.Y - cY
        c = System.Math.Sqrt((a ^ 2) + (b ^ 2))
        If c > rad Then Return False

        Return True
    End Function

End Module
