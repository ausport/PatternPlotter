Imports System.Drawing.Printing
Public Class frmDMQuadrants
    Dim zX As Single = 1
    Dim zY As Single = 1
    Dim Rect As RectangleF = Nothing
    Public ScatterPlotPoints As New Microsoft.VisualBasic.Collection
    Public kMean() As Integer

    Public Structure Sequence
        Dim SeqPoint() As Point
        Dim Color As Color
        Dim Weight As Single
    End Structure
    Public Sequences() As Sequence = Nothing
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

        Me.numHorizontalQ.Value = UserPrefs.clHorizontalQ
        Me.numVerticalQ.Value = UserPrefs.clVerticalQ

    End Sub

    Private Sub picPitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPitch.Click

    End Sub

    Private Sub picPitch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPitch.Paint
        Rect = New RectangleF(picPitch.Width / 10, picPitch.Height / 10, picPitch.Width / 1.25, picPitch.Height / 1.25)
        DrawPitch(UserPrefs.Sport, e.Graphics, Rect)

        Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
        Dim h As Single = Rect.Height / UserPrefs.clVerticalQ

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
            '      e.Graphics.ScaleTransform(zX, zY)
            Dim n As Integer = 0
            If Not Sequences Is Nothing Then
                For Each Sequence As Sequence In Sequences
                    If Sequence.SeqPoint.Length > 1 Then
                        Dim Points(Sequence.SeqPoint.Length - 1) As PointF
                        Dim dPen As Pen = New Pen(Sequence.Color, Sequence.Weight)
                        dPen.EndCap = Drawing2D.LineCap.ArrowAnchor
                        dPen.StartCap = Drawing2D.LineCap.RoundAnchor
                        For dp As Integer = 0 To Sequence.SeqPoint.GetUpperBound(0)
                            Points(dp) = New PointF((Rect.Left + (w * Sequence.SeqPoint(dp).X) + (w / 2)) * 1, (Rect.Top + (h * Sequence.SeqPoint(dp).Y) + (h / 2)) * 1)
                        Next
                        e.Graphics.DrawCurve(dPen, Points, 0.25)

                    End If
                Next
            End If
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

        If PathCount = 0 Then Exit Sub

        'compile search string..
        Dim cSearch As New SearchCriteria
        Dim Games() As String = Nothing

        If Me.cboGameID.Text = "*" Then
            Dim n As Integer = 0
            For Each item As GameProperties In GamesCurrentlyOpen
                ReDim Preserve cSearch.szGameID(n)
                cSearch.szGameID(n) = item.GameID
                ReDim Preserve Games(n)
                Games(n) = item.GameID
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

        'First check if all descriptors are checked:
        Dim boolAllChecked As Boolean = True
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Unchecked Then
                boolAllChecked = False
                Exit For
            End If
        Next

        Dim DescriptorItemsList() As String = Nothing
        If Not boolAllChecked Then
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
        Else
            cSearch.szDescriptors = Nothing
        End If

        Dim FreqCount As ItemSet() = GetQuadrantTransitions(cSearch, Me.numMinimumSupport.Value, Me.numSequenceLength.Value)

        If Not FreqCount Is Nothing Then
            Me.picPitch.Refresh()
            Erase Sequences
            Dim sCount As Integer = 0

            'Show frequencies
            With Me.DMGrid
                .Visible = False
                .ColumnCount = 1
                .Columns.Item(0).HeaderCell.Value = "Support"
                .RowCount = FreqCount.Length
                Dim maxVal As Integer = 0
                Dim minVal As Integer = 9999
                For Each item As ItemSet In FreqCount
                    If item.ItemSetFrequency > maxVal Then maxVal = item.ItemSetFrequency
                    If item.ItemSetFrequency < minVal Then minVal = item.ItemSetFrequency
                Next

                For y As Integer = FreqCount.GetUpperBound(0) To 0 Step -1
                    If Not FreqCount(y).ItemName Is Nothing Then
                        ReDim Preserve Sequences(sCount)
                        Sequences(sCount).Weight = FreqCount(y).ItemSetFrequency
                        Sequences(sCount).Color = GetColorGradient2(FreqCount(y).ItemSetFrequency, minVal, maxVal)

                        Dim szHead As String = Nothing
                        For Each item As String In FreqCount(y).ItemName
                            Dim f As Integer = Array.IndexOf(FreqCount(y).ItemName, item)
                            ReDim Preserve Sequences(sCount).SeqPoint(f)
                            Sequences(sCount).SeqPoint(f) = GetQuadrantLocation(FreqCount(y).ItemName(f))
                            szHead &= item
                            If f < FreqCount(y).ItemName.Length - 1 Then szHead &= ", "
                        Next
                        .Rows.Item(y).HeaderCell.Value = "{" & szHead & "}"
                        .Item(0, y).Value = FreqCount(y).ItemSetFrequency.ToString
                        sCount += 1
                    End If
                Next
                .Visible = True
            End With
        Else
            Me.DMGrid.Rows.Clear()
            Erase Sequences
        End If

        Me.picPitch.Refresh()

        'frmMain.toolActionStatus.Text = "Clustering time: " & modDataMining.modLastTime.Seconds + (modDataMining.modLastTime.Milliseconds / 1000).ToString & " seconds. (" & _
        '    modDataMining.modLastIterationCount.ToString & " iterations)"

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


    Private Sub DMGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DMGrid.CellContentClick

    End Sub

    Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved
        Me.picPitch.Refresh()
    End Sub

    Private Sub numHorizontalQ_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numHorizontalQ.ValueChanged
        UserPrefs.clHorizontalQ = Me.numHorizontalQ.Value
    End Sub

    Private Sub numVerticalQ_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVerticalQ.ValueChanged
        UserPrefs.clVerticalQ = Me.numVerticalQ.Value
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click

        If PathCount = 0 Then Exit Sub

        'compile search string..
        Dim cSearch As New SearchCriteria
        Dim Games() As String = Nothing

        If Me.cboGameID.Text = "*" Then
            Dim n As Integer = 0
            For Each item As GameProperties In GamesCurrentlyOpen
                ReDim Preserve cSearch.szGameID(n)
                cSearch.szGameID(n) = item.GameID
                ReDim Preserve Games(n)
                Games(n) = item.GameID
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

        'First check if all descriptors are checked:
        Dim boolAllChecked As Boolean = True
        For i As Integer = 0 To Me.lstDescriptors.Items.Count - 1
            If Me.lstDescriptors.GetItemCheckState(i) = CheckState.Unchecked Then
                boolAllChecked = False
                Exit For
            End If
        Next

        Dim DescriptorItemsList() As String = Nothing
        If Not boolAllChecked Then
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
        Else
            cSearch.szDescriptors = Nothing
        End If

        Dim FreqCount As ItemSet() = GetQuadrantTransitions(cSearch, Me.numMinimumSupport.Value, Me.numSequenceLength.Value)

        If Not FreqCount Is Nothing Then
            Me.picPitch.Refresh()
            Erase Sequences
            Dim sCount As Integer = 0

            'Show frequencies
            With Me.DMGrid
                .Visible = False
                .ColumnCount = 1
                .Columns.Item(0).HeaderCell.Value = "Support"
                .RowCount = FreqCount.Length
                Dim maxVal As Integer = 0
                Dim minVal As Integer = 9999
                For Each item As ItemSet In FreqCount
                    If item.ItemSetFrequency > maxVal Then maxVal = item.ItemSetFrequency
                    If item.ItemSetFrequency < minVal Then minVal = item.ItemSetFrequency
                Next

                For y As Integer = FreqCount.GetUpperBound(0) To 0 Step -1
                    If Not FreqCount(y).ItemName Is Nothing Then
                        ReDim Preserve Sequences(sCount)
                        Sequences(sCount).Weight = FreqCount(y).ItemSetFrequency
                        Sequences(sCount).Color = GetColorGradient2(FreqCount(y).ItemSetFrequency, minVal, maxVal)

                        Dim szHead As String = Nothing
                        For Each item As String In FreqCount(y).ItemName
                            Dim f As Integer = Array.IndexOf(FreqCount(y).ItemName, item)
                            ReDim Preserve Sequences(sCount).SeqPoint(f)
                            Sequences(sCount).SeqPoint(f) = GetQuadrantLocation(FreqCount(y).ItemName(f))
                            szHead &= item
                            If f < FreqCount(y).ItemName.Length - 1 Then szHead &= ", "
                        Next
                        .Rows.Item(y).HeaderCell.Value = "{" & szHead & "}"
                        .Item(0, y).Value = FreqCount(y).ItemSetFrequency.ToString
                        sCount += 1
                    End If
                Next
                .Visible = True
            End With
        Else
            Me.DMGrid.Rows.Clear()
            Erase Sequences
        End If

        Me.picPitch.Refresh()

        'frmMain.toolActionStatus.Text = "Clustering time: " & modDataMining.modLastTime.Seconds + (modDataMining.modLastTime.Milliseconds / 1000).ToString & " seconds. (" & _
        '    modDataMining.modLastIterationCount.ToString & " iterations)"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
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

        Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
        Dim h As Single = Rect.Height / UserPrefs.clVerticalQ

        Select Case UserPrefs.Sport
            Case tSports.sHockey
                zX = (Scale.Width / 1.25) / 90
                zY = (Scale.Height / 1.25) / 150

            Case tSports.sNetball
                zX = (Scale.Width / 1.25) / 90
                zY = (Scale.Height / 1.25) / 180

            Case tSports.sRugby7
                zX = (picPitch.Width / 1.25) / 70
                zY = (picPitch.Height / 1.25) / 120

            Case tSports.sRugbyLeague
                zX = (Scale.Width / 1.25) / 68
                zY = (Scale.Height / 1.25) / 122

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
            Dim n As Integer = 0
            If Not Sequences Is Nothing Then
                For Each Sequence As Sequence In Sequences
                    If Sequence.SeqPoint.Length > 1 Then
                        Dim Points(Sequence.SeqPoint.Length - 1) As PointF
                        Dim dPen As Pen = New Pen(Sequence.Color, Sequence.Weight)
                        dPen.EndCap = Drawing2D.LineCap.ArrowAnchor
                        dPen.StartCap = Drawing2D.LineCap.RoundAnchor
                        For dp As Integer = 0 To Sequence.SeqPoint.GetUpperBound(0)
                            Points(dp) = New PointF((Rect.Left + (w * Sequence.SeqPoint(dp).X) + (w / 2)) * 1, (Rect.Top + (h * Sequence.SeqPoint(dp).Y) + (h / 2)) * 1)
                        Next
                        e.Graphics.DrawCurve(dPen, Points, 0.25)

                    End If
                Next
            End If
        End If

        e.Graphics.ScaleTransform(zX, zY)

        'Print details
        Dim leftEdge As Single = Rect.Left / zX
        Dim vertSpace As PointF = e.Graphics.MeasureString(Me.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point))

        e.Graphics.DrawString(Me.cboGameID.Text, New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge + (1.4 * vertSpace.X) / zX, (1.4 * vertSpace.Y) / zY)
        e.Graphics.DrawString("Path Quadrant Analysis", New Font("Arial", 10, FontStyle.Italic, GraphicsUnit.Document), Brushes.DarkBlue, leftEdge, (1.4 * vertSpace.Y) / zY)

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

    Private Sub cmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
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

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim how As DialogResult = MsgBox("Include chart metadata with image?", MsgBoxStyle.YesNoCancel, "Save Chart to JPEG")
        If how = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        'Save chart to JPEG

        Dim res As DialogResult = Windows.Forms.DialogResult.Cancel

        'Get a new filename if one does not already exist.
        Dim dlgFileName = New SaveFileDialog
        dlgFileName.Filter = "JPG Image|*.jpg"
        dlgFileName.InitialDirectory = UserPrefs.VideoCaptureDir
        dlgFileName.FileName = StripFileName(Me.Text) & ".jpg"
        res = dlgFileName.ShowDialog()
        If res <> Windows.Forms.DialogResult.Cancel Then
            Dim sc As New ScreenShot.ScreenCapture
            Application.DoEvents()
            Select Case how
                Case Is = Windows.Forms.DialogResult.Yes
                    sc.CaptureWindowToFile(Me.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
                Case Is = Windows.Forms.DialogResult.No
                    sc.CaptureWindowToFile(Me.picPitch.Handle, dlgFileName.FileName, Imaging.ImageFormat.Jpeg)
            End Select
        End If
    End Sub
End Class