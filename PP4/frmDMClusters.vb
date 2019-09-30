Imports System.Drawing.Printing

Public Class frmDMClusters
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim Rect As RectangleF = Nothing
    Public ScatterPlotPoints As New Microsoft.VisualBasic.Collection
    Public kMean() As Integer

    Private SelectedCluster As Integer = Nothing
    Shared SelectedCaptionBox As Rectangle = Nothing
    Private SelectedCaptionPathID As Long = 0
    Private MouseDownOnCaption As Boolean = False
    Private MouseMoveOffset As Point = Nothing
    Public CurrentMousePointer As PointF = Nothing
    Private ClusterColors(10) As Color

    Private WithEvents pDoc As PrintDocument


    Private Sub frmDMClusters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub picPitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPitch.Click

    End Sub

    Private Sub picPitch_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseClick
        Select Case UserPrefs.Sport
            Case tSports.sHockey
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 150

            Case tSports.sNetball
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 180

            Case tSports.sRugbyLeague
                zX = (picPitch.Width / 1.25) / 68
                zY = (picPitch.Height / 1.25) / 122

            Case tSports.sRugby7
                zX = (picPitch.Width / 1.25) / 70
                zY = (picPitch.Height / 1.25) / 120

            Case tSports.sBasketball
                zX = (picPitch.Width / 1.25) / 50
                zY = (picPitch.Height / 1.25) / 94

            Case tSports.sSoccer
                zX = (picPitch.Width / 1.25) / 95
                zY = (picPitch.Height / 1.25) / 150

        End Select

        CurrentMousePointer.X = e.X / zX
        CurrentMousePointer.Y = e.Y / zY


        'Find and play video clip from local point...

        'Get start point and match
        For Each gp As ScatterInfo In ScatterPlotPoints

            If Math.Max(gp.Location.X, e.X / zX) - Math.Min(gp.Location.X, e.X / zX) < 1.5 And _
                Math.Max(gp.Location.Y, e.Y / zY) - Math.Min(gp.Location.Y, e.Y / zY) < 1.5 Then

                'Match found..
                SelectedCluster = gp.ClusterID

                If e.Button = Windows.Forms.MouseButtons.Right Then

                    Me.mnuTabsDropDown.Show(Me, e.Location)
                    Exit Sub
                End If

                Dim szVideos() As String = GetVideoFiles(gp.ID)
                If Not szVideos Is Nothing Then

                    frmVideo.MdiParent = frmMain
                    frmVideo.Show()

                    If Not frmVideo.LoadVideoClip(szVideos(0), False, GetSecondsFromTimeString(gp.StartTimeString) - UserPrefs.LeadTime, _
                        GetSecondsFromTimeString(gp.StartTimeString) + UserPrefs.LagTime, True) Then

                        MsgBox("Cannot locate video files for this game data.", MsgBoxStyle.Exclamation, Application.ProductName)
                    Else
                        frmVideo.Panel2.Focus()
                    End If
                End If

            End If
        Next



    End Sub

    Private Sub picPitch_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPitch.MouseHover

    End Sub

    Private Sub picPitch_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picPitch.MouseMove

    End Sub

    Private Sub picPitch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPitch.Paint
        Rect = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)
        DrawPitch(UserPrefs.Sport, e.Graphics, Rect)

        Select Case UserPrefs.Sport
            Case tSports.sHockey
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 150

            Case tSports.sNetball
                zX = (picPitch.Width / 1.25) / 90
                zY = (picPitch.Height / 1.25) / 180

            Case tSports.sRugbyLeague
                zX = (picPitch.Width / 1.25) / 68
                zY = (picPitch.Height / 1.25) / 122

            Case tSports.sRugby7
                zX = (picPitch.Width / 1.25) / 70
                zY = (picPitch.Height / 1.25) / 120

            Case tSports.sBasketball
                zX = (picPitch.Width / 1.25) / 50
                zY = (picPitch.Height / 1.25) / 94

            Case tSports.sSoccer
                zX = (picPitch.Width / 1.25) / 95
                zY = (picPitch.Height / 1.25) / 150
        End Select

        If Not Me.ScatterPlotPoints Is Nothing Then

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.ScaleTransform(zX, zY)

            Dim n As Integer = 0
            For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
                e.Graphics.FillEllipse(New SolidBrush(ClusterColors(ScatterPoint.ClusterID)), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(2, 2)))
            Next
        End If

    End Sub

    Private Sub cmdSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectNone.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, False)
        Next

    End Sub

    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        If Me.lstDescriptors.Items.Count = 0 Then Exit Sub
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            Me.lstDescriptors.SetItemChecked(i, True)
        Next

    End Sub

    Private Sub lstDescriptors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDescriptors.SelectedIndexChanged

    End Sub

    Private Sub cboDM_Xaxis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdRefreshDataSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub picPitch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPitch.Resize

    End Sub

    Private Sub frmDMClusters_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.picPitch.Refresh()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub


    Private Sub mnuClusterAddVPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClusterAddVPL.Click

        'Create VPL from contents of selected cell.
        CurrentSearch += 1
        ReDim Preserve SearchHistory(CurrentSearch)

        Dim szTemp As String = SQL_VideoPlayList & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE"

        'Status update
        frmMain.toolProgressBar.Minimum = 0
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Generating Playlist Data..."
        frmMain.toolProgressBar.Maximum = Me.ScatterPlotPoints.Count
        Dim n As Integer = 0

        For Each scatterpoint As ScatterInfo In Me.ScatterPlotPoints
            frmMain.toolProgressBar.Value = n
            Application.DoEvents()

            If scatterpoint.ClusterID = Me.SelectedCluster Then
                If n = 0 Then
                    szTemp &= " PathData.ID = " & scatterpoint.ID
                Else
                    szTemp &= " OR PathData.ID = " & scatterpoint.ID
                End If
                n += 1
            End If
        Next
        frmMain.toolProgressBar.Value = 0
        frmMain.toolActionStatus.Text = "Playlist Data Complete..."

        If n > 150 Then
            MsgBox("This query is too large for this analysis mode.  Try selecting a smaller dataset, or using the normal analysis criteria window.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

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

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TableLayoutPanel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        If PathCount = 0 Then Exit Sub

        'compile search string..
        Dim cSearch As New SearchCriteria

        If Me.cboGameID.Text = "*" Then
            Dim n As Integer = 0
            For Each item As GameProperties In GamesCurrentlyOpen
                ReDim Preserve cSearch.szGameID(n)
                cSearch.szGameID(n) = item.GameID
                n += 1
            Next
        Else
            ReDim cSearch.szGameID(0)
            cSearch.szGameID(0) = Me.cboGameID.Text
        End If

        ReDim cSearch.uOutcomes(0)
        cSearch.uOutcomes(0) = GetOutcomeFromString(Me.cboOutcome.Text)

        If Me.cboTeamName.Text <> "*" Then
            ReDim cSearch.szTeamName(0)
            cSearch.szTeamName(0) = Me.cboTeamName.Text
        End If

        If Me.cboTimeCriteria.Text <> "*" Then
            ReDim cSearch.szTimeCriterion(0)
            cSearch.szTimeCriterion(0) = Me.cboTimeCriteria.Text
        End If

        Dim DescriptorItemsList() As String = Nothing
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Checked Then
                If DescriptorItemsList Is Nothing Then
                    ReDim DescriptorItemsList(0)
                Else
                    ReDim Preserve DescriptorItemsList(DescriptorItemsList.Length)
                End If
                DescriptorItemsList(DescriptorItemsList.Length - 1) = Me.lstDescriptors.Items.Item(i)
            End If
        Next

        If DescriptorItemsList Is Nothing Then
            MsgBox("No data has been selected for this analysis.  It will not procede.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        cSearch.szDescriptors = DescriptorItemsList

        Dim sqlClusters As String = CompileSearchString(cSearch, AnalysisType.uScatterPlot, Nothing)
        ScatterPlotPoints = CompileScatterArray(sqlClusters, Nothing)

        If Not ScatterPlotPoints Is Nothing Then
            If Me.rdoKMeans.Checked = True Then
                ScatterPlotPoints = kMeanXYCluster(ScatterPlotPoints, numClusters.Value, numIterations.Value)
            ElseIf Me.rdoMeloids.Checked = True Then
                ScatterPlotPoints = kMeanXYCluster(ScatterPlotPoints, numClusters.Value, numIterations.Value, True)
            End If
        End If

        For n As Integer = 0 To numClusters.Value
            ClusterColors(n) = GetColorGradient2(n, 0, numClusters.Value)
        Next


        Me.picPitch.Refresh()
        frmMain.toolActionStatus.Text = "Clustering time: " & modDataMining.modLastTime.Seconds + (modDataMining.modLastTime.Milliseconds / 1000).ToString & " seconds. (" & _
            modDataMining.modLastIterationCount.ToString & " iterations)"

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'Save chart to JPEG

        Dim res As DialogResult = Windows.Forms.DialogResult.Cancel

        'Get a new filename if one does not already exist.
        Dim dlgFileName = New SaveFileDialog
        dlgFileName.Filter = "JPG Image|*.jpg"
        dlgFileName.InitialDirectory = UserPrefs.VideoCaptureDir
        dlgFileName.FileName = "Data Cluster.jpg"
        res = dlgFileName.ShowDialog()
        If res <> Windows.Forms.DialogResult.Cancel Then
            Dim sc As New ScreenShot.ScreenCapture
            Application.DoEvents()
            sc.CaptureWindowToFile(Me.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
        End If
    End Sub

    Private Sub cmdPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPDF.Click
        pDoc = New PrintDocument
        Dim dlgPrint As New PrintPreviewDialog
        dlgPrint.Document = pDoc
        dlgPrint.ShowDialog()
    End Sub

    Private Sub PrintMe(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDoc.PrintPage

        Dim Scale As RectangleF = e.MarginBounds
        Scale.Width \= 1.25
        Scale.Height \= 1.25

        'Set window size parameters
        Rect = New RectangleF(Scale.Width / 10, Scale.Height / 10, Scale.Width / 1.25, Scale.Height / 1.25)

        'Draw Base Pitch
        DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality, , , KnownColor.Black, KnownColor.Transparent)

        Select Case UserPrefs.Sport
            Case tSports.sHockey
                zX = (Scale.Width / 1.25) / 90
                zY = (Scale.Height / 1.25) / 150

            Case tSports.sNetball
                zX = (Scale.Width / 1.25) / 90
                zY = (Scale.Height / 1.25) / 180

            Case tSports.sRugbyLeague
                zX = (Scale.Width / 1.25) / 68
                zY = (Scale.Height / 1.25) / 122

            Case tSports.sRugby7
                zX = (picPitch.Width / 1.25) / 70
                zY = (picPitch.Height / 1.25) / 120

            Case tSports.sBasketball
                zX = (Scale.Width / 1.25) / 50
                zY = (Scale.Height / 1.25) / 94

            Case tSports.sSoccer
                zX = (Scale.Width / 1.25) / 95
                zY = (Scale.Height / 1.25) / 150

        End Select

        'Draw Plays

        If Not Me.ScatterPlotPoints Is Nothing Then

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.ScaleTransform(zX, zY)
            For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
                'Cluster data
                e.Graphics.FillEllipse(New SolidBrush(ClusterColors(ScatterPoint.ClusterID)), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(2, 2)))
            Next
        End If



        'Print details
        Dim leftEdge As Single = Rect.Left / zX
        Dim vertSpace As PointF = e.Graphics.MeasureString(Me.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point))

        e.Graphics.DrawString(Me.cboGameID.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge + (1.4 * vertSpace.X) / zX, (1.4 * vertSpace.Y) / zY)
        e.Graphics.DrawString("Data Cluster", New Font("Arial", 10, FontStyle.Italic, GraphicsUnit.Document), Brushes.DarkBlue, leftEdge, (1.4 * vertSpace.Y) / zY)

        e.Graphics.DrawString("Team Names:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (1 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.cboTeamName.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (2 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Time Criteria:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (3 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.cboTimeCriteria.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (4 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Outcome Types:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (5 * vertSpace.Y)) / zY)
        e.Graphics.DrawString(Me.cboOutcome.Text, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (6 * vertSpace.Y)) / zY)
        e.Graphics.DrawString("Included Event Names:", New Font("Arial", 6, FontStyle.Underline, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (7 * vertSpace.Y)) / zY)
        'e.Graphics.DrawString(Me.lblDescriptors.Text, New Font("Arial", 5, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (8 * vertSpace.Y)) / zY)

        e.Graphics.DrawString("Printed: " & Now.ToString, New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (14 * vertSpace.Y)) / zY)

    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        pDoc = New PrintDocument
        Dim dlgPrint As New PrintDialog
        dlgPrint.Document = pDoc
        Dim res As Windows.Forms.DialogResult = dlgPrint.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            Try
                dlgPrint.Document.Print()
            Catch ex As Exception
                MsgBox("An error occured trying to print this document.  Check the printer or network connection.")
            End Try
        End If

    End Sub
End Class