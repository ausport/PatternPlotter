Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports DexterLib
Imports QuartzTypeLib


Public Class frmVideoPlayList
    Public idForm As Integer
    Public formDirty As Boolean = False
    Public VPLTitle As String

    Private PlayingAll As Boolean
    Public PlayingItemNow As Integer
    Private PlayingEndTime As Double
    Private PlayingStartTime As Double
    Public ResumePlay As Boolean = False

    Public MySearch As Integer  'Retains the search criteria id.

    'NB:
    'ExportSimple is an instance of the clsDV2Device class which exports items to DV using the old code, and excludes transitions or slow motion.
    'ExportGraphComplex is a filter graph compiled to include transitions and slow motion.
    'The advantage of the clsDV2Device class is that it returns the duration of the media and does not require the user to set the dv encoder
    'properties to either PAL or NTSC.
    'The ExportSimple method is used whenever the user properties for slow motion and transitions are set to false.

    Public ExportSimple As clsDV2Device
    Private ExportGraphComplex As QuartzTypeLib.FilgraphManager = Nothing
    Private ExportComplexPosition As QuartzTypeLib.IMediaPosition
    Public ExportDVDuration As Double = 0

    Public gbl_objTimeline As AMTimeline
    'Public gbl_objRenderEngine As New RenderEngine

    Public Function SelectCurrentRow(Optional ByVal Selected As Boolean = True) As Integer

        For Each row As DataGridViewRow In Me.vplGrid.SelectedRows
            If Selected Then
                row.HeaderCell.Value = "S"
            Else
                row.HeaderCell.Value = ""
            End If
        Next
        Return vplGrid.SelectedRows.Count

    End Function

    Public Sub AddItem2VPL()
        formDirty = True
    End Sub

    Public Sub New(ByVal intID As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        idForm = intID
    End Sub

    Private Sub mnuVPL_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPL_Save.Click
        SaveMe()
    End Sub

    Public Sub SaveMe()
        Dim dlgPlaylist As New SaveFileDialog

        With dlgPlaylist
            .Title = "Save video playlist..."
            .Filter = "Plotter Video Playlist (*.vpl)|*.vpl"
            .FileName = Me.Text
            .OverwritePrompt = True
            Dim res As DialogResult = .ShowDialog(frmMain)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim fnum As Integer = FreeFile()
            Dim szDatabase As String = UserPrefs.dbPath
            FileOpen(fnum, .FileName, OpenMode.Output)
            With vplGrid
                For Each gRow As DataGridViewRow In vplGrid.Rows
                    Print(fnum, gRow.Cells("PathID").Value & ";")
                    Print(fnum, gRow.Cells("GameID").Value & ";")
                    Print(fnum, gRow.Cells("TeamName").Value & ";")
                    Print(fnum, gRow.Cells("Session").Value & ";")
                    Print(fnum, gRow.Cells("InPoint").Value & ";")
                    Print(fnum, gRow.Cells("OutPoint").Value & ";")
                    Print(fnum, gRow.Cells("Descriptors").Value & ";")
                    Print(fnum, gRow.Cells("uRegion").Value & ";")
                    Print(fnum, gRow.Cells("Video").Value & ";")
                    Print(fnum, gRow.Cells("Video2").Value & ";")
                Next
            End With
            FileClose(fnum)
            Me.formDirty = False
            Me.Text = System.IO.Path.GetFileNameWithoutExtension(.FileName)

        End With
        Exit Sub

errCatch:
        Err.Clear()


    End Sub

    Public Function LoadVPL(ByVal szFileName As String) As Integer
        Dim currentRow() As String
        Dim szParse As Microsoft.VisualBasic.FileIO.TextFieldParser = My.Computer.FileSystem.OpenTextFieldParser(szFileName, ";")
        While Not szParse.EndOfData
            Try
                currentRow = szParse.ReadFields()
                For i As Integer = currentRow.GetLowerBound(0) To (currentRow.GetUpperBound(0) - 10) Step 10
                    Me.vplGrid.Rows.Add(currentRow(i), currentRow(i + 1), currentRow(i + 2), currentRow(i + 3), currentRow(i + 4), currentRow(i + 5), _
                    currentRow(i + 6), currentRow(i + 7), currentRow(i + 8), currentRow(i + 9))
                Next

            Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                MsgBox("Line " & ex.Message & _
                "is not valid and will be skipped.", MsgBoxStyle.Critical, Application.ProductName)
            End Try
        End While

        Me.Text = System.IO.Path.GetFileNameWithoutExtension(szFileName)
        VPLTitle = Me.Text

        Dim nCount As Integer = Me.vplGrid.Rows.Count - 1

        If nCount > 1 Then
            Me.mnuVPL_Prev.Enabled = False
            Me.mnuVPL_Next.Enabled = True
        End If

        Return nCount

    End Function

    Public Sub AddVideoList(ByVal Files() As String, ByVal szGameID As String, ByVal szTimeCriteria As String)
        For Each File As String In Files
            Dim szLoc As String = FindMediaFile(File)
            If Not String.IsNullOrEmpty(szLoc) Then
                Me.vplGrid.Rows.Add("", szGameID, "", szTimeCriteria, GetTimeStringFromSeconds(0), GetTimeStringFromSeconds(GetMediaDuration(szLoc)), _
                    "", "", szLoc)
            End If
        Next
    End Sub

    Public Sub SetLabels(ByVal ThisSearch As Integer, Optional ByVal FindDescriptors As Boolean = True)
        'NB: FindDescriptors is used here to avoid implementing a search for checked items in the descriptor list
        'where the analysis window is not open.  If this occurs, it resets the SearchHistory array which results
        'in errors during the wireless automated reporting function, for which the analysis window is not open.
        'So, FindDescriptors will only be false when the SetLabels function is called during the automated search routine.
        Dim szTeamNames As String = Nothing
        Dim szDescriptors As String = Nothing

        If Not SearchHistory(ThisSearch).szGameID Is Nothing Then
            With SearchHistory(ThisSearch)
                If .szTeamName Is Nothing Then
                    szTeamNames = "All Teams "
                Else
                    For Each item As String In .szTeamName
                        If Not szTeamNames Is Nothing Then szTeamNames = szTeamNames & ", "
                        szTeamNames = szTeamNames & item
                        If item = "*" Then szTeamNames = "All Teams "
                    Next

                End If

                'Set descriptors
                If .szDescriptors Is Nothing Then
                    Dim items() As String = GetCheckedDescriptorList(True)
                    For Each item As String In items
                        If Not szDescriptors Is Nothing Then szDescriptors = szDescriptors & ", "
                        szDescriptors = szDescriptors & item
                    Next
                Else

                    For Each item As String In .szDescriptors
                        If Not szDescriptors Is Nothing Then szDescriptors = szDescriptors & ", "
                        szDescriptors = szDescriptors & item
                    Next

                End If

            End With
        End If

        Me.Text = szTeamNames & " "
        Me.Text = Me.Text & Mid(szDescriptors, 1, 40) '& "..."
        ' Me.Text = Me.Text & "..."
        If Me.Text.Length > 1 Then
            Me.Text = Me.Text & "..."
        Else
            Me.Text = "New Video Playlist " & Me.idForm.ToString
        End If
        VPLTitle = Me.Text

    End Sub

    Private Sub frmVideoPlayList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        lastVPLFormUsed = idForm
    End Sub

    Private Sub frmVideoPlayList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.DoEvents()
    End Sub

    Private Sub frmVideoPlayList_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        lastVPLFormUsed = idForm
    End Sub

    Private Sub frmVideoPlayList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Text = "Video Playlist " & idForm.ToString
        lastVPLFormUsed = idForm

    End Sub

    Public Sub vplGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles vplGrid.CellDoubleClick
        'Play current item.
        Try
            Dim n As Integer = Nothing
            If Not e Is Nothing Then
                n = e.RowIndex
            Else
                n = vplGrid.SelectedRows.Item(0).Index
            End If

            PlayingItemNow = e.RowIndex

            PlayVideo(vplGrid.Item(8, n).Value, GetSecondsFromTimeString(vplGrid.Item(4, n).Value), _
                GetSecondsFromTimeString(vplGrid.Item(5, n).Value))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuVPL_PlayAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_PlayAll.Click
        ResumePlay = False
        'Play first item.
        If Me.vplGrid.RowCount = 0 Then Exit Sub

        PlayingItemNow = -1

        PlayingAll = PlayNextItem()
        timerPlayAll.Enabled = PlayingAll


    End Sub

    Public Function PlayNextItem(Optional ByVal PlayPrevious As Boolean = False) As Boolean
        ResumePlay = False

        frmVideo.mnuVID_Next.Visible = True
        frmVideo.mnuVID_Prev.Visible = True
        Me.mnuVPL_Next.Visible = True
        Me.mnuVPL_Prev.Visible = True

        If PlayingItemNow >= (Me.vplGrid.RowCount - 1) And Not PlayPrevious Then
            If frmVideo.Visible = True And UserPrefs.StopAtEndOfClip = True Then
                Dim sender As Object = Nothing
                Dim e As EventArgs = Nothing
                frmVideo.mnuPause_Click(sender, e)
            End If
            PlayingAll = False
            Return False
        End If

        Do
            If PlayPrevious Then
                If PlayingItemNow = 0 Then Return False
                PlayingItemNow -= 1
            Else
                If PlayingItemNow = Me.vplGrid.RowCount - 1 Then Return False
                PlayingItemNow += 1
            End If

            If PlayingItemNow >= (Me.vplGrid.RowCount - 1) Then
                Me.mnuVPL_Next.Enabled = False
                Me.mnuVPL_Prev.Enabled = True
                frmVideo.mnuVID_Next.Enabled = False
                frmVideo.mnuVID_Prev.Enabled = True

            ElseIf PlayingItemNow = 0 And Me.vplGrid.RowCount - 1 > 0 Then
                Me.mnuVPL_Next.Enabled = True
                Me.mnuVPL_Prev.Enabled = False
                frmVideo.mnuVID_Next.Enabled = True
                frmVideo.mnuVID_Prev.Enabled = False

            ElseIf PlayingItemNow > 0 And PlayingItemNow < (Me.vplGrid.RowCount - 1) Then
                Me.mnuVPL_Next.Enabled = True
                Me.mnuVPL_Prev.Enabled = True
                frmVideo.mnuVID_Next.Enabled = True
                frmVideo.mnuVID_Prev.Enabled = True
            End If

            Me.vplGrid.ClearSelection()

            Me.vplGrid.Rows.Item(PlayingItemNow).Selected = True

            Try

                PlayingEndTime = GetSecondsFromTimeString(vplGrid.Item(5, PlayingItemNow).Value)
                PlayingStartTime = GetSecondsFromTimeString(vplGrid.Item(4, PlayingItemNow).Value)

                Dim szVideo As String = FindMediaFile(vplGrid.Item(8, PlayingItemNow).Value)

                If Not szVideo = Nothing Then
                    If szVideo = szCurrentVideoFile Then
                        PlayVideo(szVideo, PlayingStartTime, PlayingEndTime, False)
                    Else
                        PlayVideo(szVideo, PlayingStartTime, PlayingEndTime, True)
                    End If
                End If
                boolCurrentVideoIsVPL = True

                Return True
            Catch ex As Exception

            End Try

        Loop While PlayingItemNow < Me.vplGrid.RowCount

        Return False
    End Function

    Private Sub timerPlayAll_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerPlayAll.Tick
        If Not lastVPLFormUsed = idForm Then Exit Sub
        If Not frmVideo.Visible Then Exit Sub
        If ResumePlay Then Exit Sub

        Dim cnow As Double = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)
        If cnow >= PlayingEndTime Or cnow >= Int(GetMediaDuration(szCurrentVideoFile)) Then
            If frmVideo.Visible = True And UserPrefs.PlayContinuous = False Then
                frmVideo.mnuPause_Click(sender, e)
            Else
                PlayingAll = PlayNextItem()
                timerPlayAll.Enabled = PlayingAll
            End If
        End If
    End Sub



    Private Sub mnuVPL_StartExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPL_StartExport.Click
        'If using ExportMultiStream (which is a filtergraph with fades and slow motion and multiple items)
        If Not ExportGraphComplex Is Nothing Then
            Me.timExport.Enabled = True
            ExportGraphComplex.Run()
        ElseIf Not ExportSimple Is Nothing Then
            ExportSimple.StartTransmit()
        End If

    End Sub

    Private Sub mnuVPL_StopExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPL_StopExport.Click
        If Not ExportGraphComplex Is Nothing Then
            ExportGraphComplex.Stop()
            If Not ExportGraphComplex Is Nothing Then
                On Error Resume Next
                For Each filter As QuartzTypeLib.IFilterInfo In ExportGraphComplex.FilterCollection
                    For Each pin As IPinInfo In filter.Pins
                        pin.Disconnect()
                    Next
                Next
                ExportGraphComplex = Nothing
                ExportComplexPosition = Nothing
            End If
        ElseIf Not ExportSimple Is Nothing Then
            ExportSimple.StopTransmit()
            ExportSimple.Dispose()
            If Not ExportSimple Is Nothing Then ExportSimple = Nothing
        End If

        Me.timExport.Enabled = False
        Me.ToolStripSeparator1.Visible = False
        Me.mnuVPL_StartExport.Visible = False
        Me.mnuVPL_StopExport.Visible = False
        Me.mnuVPL_PauseExport.Visible = False
        Me.mnuVPL_Next.Enabled = True
        Me.mnuVPL_Prev.Enabled = True
        Me.Text = VPLTitle
    End Sub

    Private Sub mnuVPL_PauseExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPL_PauseExport.Click
        'If using ExportMultiStream (which is a filtergraph with fades and slow motion and multiple items)
        If Not ExportGraphComplex Is Nothing Then
            Me.timExport.Enabled = True
            ExportGraphComplex.Pause()
        ElseIf Not ExportSimple Is Nothing Then
            ExportSimple.PauseTransmit()
        End If
    End Sub

    Public Sub mnuVPL_Next_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_Next.Click
        lastVPLFormUsed = idForm
        PlayNextItem()
        timerPlayAll.Enabled = PlayingAll

    End Sub

    Public Sub mnuVPL_Prev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_Prev.Click
        lastVPLFormUsed = idForm
        '     PlayingAll = PlayNextItem(True)
        PlayNextItem(True)
        timerPlayAll.Enabled = PlayingAll

    End Sub

    Private Sub vplGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles vplGrid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.ContextMenuStrip_VPL.Show(Me, e.X, e.Y + Me.vplGrid.Top)
        End If
    End Sub

    Private Sub ToolStripVPL_RemoveItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripVPL_RemoveItem.Click
        For Each item As DataGridViewRow In Me.vplGrid.SelectedRows
            vplGrid.Rows.RemoveAt(item.Index)
        Next
    End Sub

    Private Sub mnuVPL_Export2File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_Export2File.Click

        If Me.vplGrid.RowCount = 0 Then
            MsgBox("There are no items in this playlist to export...", MsgBoxStyle.Exclamation, Application.ProductName)
            Exit Sub
        End If

        Dim dlgSaveClip As SaveFileDialog = New SaveFileDialog
        dlgSaveClip.OverwritePrompt = True
        '  dlgSaveClip.Filter = "AVI Video Files (*.avi)|*.avi"
        dlgSaveClip.Filter = "AVI Video Files (*.avi)|*.avi|XTL DirectX Timeline (*.xtl)|*.xtl|GRF DirectX Graph (*.grf)|*.grf"
        dlgSaveClip.DefaultExt = "*.avi"
        If dlgSaveClip.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim szFilename As String = dlgSaveClip.FileName
        'If IO.File.Exists(szFilename) Then IO.File.Delete(szFilename)


        Dim res As Boolean = False

        If Me.vplGrid.RowCount > 0 Then
            'Code for stitching multiple video reference points into a single AVI.
            Dim nSegments As Integer = vplGrid.Rows.Count - 1
            If UserPrefs.DuplicateInSlowMotion Then
                nSegments = (vplGrid.Rows.Count * 2) - 1
            End If

            Dim szPath(nSegments) As String
            Dim dStart(nSegments) As Double
            Dim dStop(nSegments) As Double
            Dim nSpeed(nSegments) As Single

            'Fill temporary arrays with video playlist data.
            Dim Segment As Integer = 0
            For Each row As DataGridViewRow In vplGrid.Rows
                szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                If Not szPath(Segment) = Nothing Then
                    dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                    dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                    nSpeed(Segment) = 1

                    If UserPrefs.DuplicateInSlowMotion Then
                        Segment += 1
                        szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                        dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                        dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                        nSpeed(Segment) = 0.5
                    End If
                    Segment += 1
                End If
            Next

            If Segment > 0 Then
                If Not String.IsNullOrEmpty(dlgSaveClip.FileName) Then
                    gbl_objTimeline = BuildTimeLineFromMediaSources(nSegments + 1, szPath, dStart, dStop, nSpeed)
                    If Microsoft.VisualBasic.Right(szFilename, 3) = "avi" Then
                        res = BuildAVIfromTimeLine(gbl_objTimeline, szFilename, frmMain.toolProgressBar)
                    ElseIf Microsoft.VisualBasic.Right(szFilename, 3) = "xtl" Then
                        res = BuildXTLfromTimeLine(gbl_objTimeline, szFilename)
                    ElseIf Microsoft.VisualBasic.Right(szFilename, 3) = "grf" Then
                        If Not BuildMUXedGRFfromTimeline(gbl_objTimeline, szFilename, True) Is Nothing Then res = True
                    End If

                End If

                If res Then
                    MsgBox("Done...", MsgBoxStyle.Information, Application.ProductName)
                Else
                    MsgBox("An error has occured, and the file was not saved...", MsgBoxStyle.Critical, Application.ProductName)
                End If
            Else
                MsgBox("No video sources were found in this video playlist.", MsgBoxStyle.Critical, Application.ProductName)
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub mnuVPLPlayItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPLPlayItem.Click
        Me.vplGrid_CellDoubleClick(sender, Nothing)
    End Sub

    Private Sub mnuVPLShowPathways_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPLShowPathways.Click
        If Me.vplGrid.SelectedRows.Count > 0 Then
            '*
            '* Create new Game Chart window.
            '*
            countC = countC + 1
            ReDim Preserve frmC(countC)
            frmC(countC) = New frmChart(countC)
            frmC(countC).MdiParent = frmMain
            frmC(countC).MySearch = Nothing
            frmC(countC).ChartType = frmChart.Chart.ctPathways
            frmC(countC).SetLabels(MySearch)
        End If

        Dim PlaySet As New Microsoft.VisualBasic.Collection
        Dim Index_Play As Integer = 0

        For Each row As DataGridViewRow In vplGrid.SelectedRows
            If row.Cells("PathID").Value <> "" Then
                Dim GamePlaySet As GamePlayClass = GetPlays(row.Cells("GameID").Value, row.Cells("Session").Value, GetPlayNumberFromID(row.Cells("PathID").Value), 1)
                If Not GamePlaySet.Plays Is Nothing Then
                    For Each NewPlay As GamePlay.Instance In GamePlaySet.Plays
                        Index_Play += 1
                        PlaySet.Add(NewPlay, Index_Play)
                    Next
                End If
            End If
        Next

        frmC(countC).AddPlays(PlaySet)
        frmC(countC).Show()
    End Sub

    Private Sub mnuVPSSelectItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPSSelectItem.Click
        SelectCurrentRow(True)
    End Sub

    Private Sub mnuVPLUnselectItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPLUnselectItem.Click
        SelectCurrentRow(False)
    End Sub

    Private Sub mnuVPLRemoveUnSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuVPLRemoveUnSelected.Click

        For i As Integer = vplGrid.Rows.Count - 1 To 0 Step -1
            If vplGrid.Rows(i).HeaderCell.Value <> "S" Then
                vplGrid.Rows.RemoveAt(i)
            End If
        Next

    End Sub

    Private Sub mnuVPL_Export2DV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_Export2DV.Click
        Dim bIsOnline As Boolean = False

        If Me.vplGrid.RowCount = 0 Then
            MsgBox("There are no items in this playlist to preview...", MsgBoxStyle.Exclamation, Application.ProductName)
            Exit Sub
        End If

        If Me.vplGrid.RowCount > 0 Then
            'Code for stitching multiple video reference points into a single AVI.
            Dim nSegments As Integer = vplGrid.Rows.Count - 1
            If UserPrefs.DuplicateInSlowMotion Then
                nSegments = (vplGrid.Rows.Count * 2) - 1
            End If

            Dim szPath(nSegments) As String
            Dim dStart(nSegments) As Double
            Dim dStop(nSegments) As Double
            Dim nSpeed(nSegments) As Single

            'Fill temporary arrays with video playlist data.
            Dim Segment As Integer = 0
            For Each row As DataGridViewRow In vplGrid.Rows
                szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                If Not szPath(Segment) = Nothing Then
                    dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                    dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                    nSpeed(Segment) = 1

                    If UserPrefs.DuplicateInSlowMotion Then
                        Segment += 1
                        szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                        dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                        dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                        nSpeed(Segment) = 0.5
                    End If
                    Segment += 1
                End If
            Next

            'Build graph or instantiate export to dv handling class.
            If Not UserPrefs.AddFadeTransitions And Not UserPrefs.DuplicateInSlowMotion And Segment > 0 Then
                'No transitions or slow motion, so use the clsDV2Device export method.
                ExportSimple = New clsDV2Device
                bIsOnline = ExportSimple.GoOnline2(Segment, szPath, dStart, dStop)
            ElseIf (UserPrefs.AddFadeTransitions Or UserPrefs.DuplicateInSlowMotion) And Segment > 0 Then
                'Transitions or slow motion included, so compile the ExportGraphComplex filtergraph.
                ExportGraphComplex = ExportTimeLine2DV(BuildTimeLineFromMediaSources(nSegments + 1, szPath, dStart, dStop, nSpeed))
                ExportComplexPosition = ExportGraphComplex

                If Not ExportGraphComplex Is Nothing Then bIsOnline = True
            End If

            If Not bIsOnline And Segment > 0 Then
                MsgBox("No DV video devices were found.  Please ensure a DV input device is connected.", MsgBoxStyle.Critical, Application.ProductName)
            End If

        Else
            MsgBox("No video sources were found in this video playlist.", MsgBoxStyle.Critical, Application.ProductName)
        End If

        Me.mnuVPL_StartExport.Visible = bIsOnline
        Me.mnuVPL_StopExport.Visible = bIsOnline
        Me.mnuVPL_PauseExport.Visible = bIsOnline
        Me.ToolStripSeparator1.Visible = bIsOnline
        Me.mnuVPL_Next.Enabled = Not bIsOnline
        Me.mnuVPL_Prev.Enabled = Not bIsOnline
        Me.timExport.Enabled = bIsOnline


    End Sub

    Private Sub mnuVPL_Preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVPL_Preview.Click
        If Me.vplGrid.RowCount = 0 Then
            MsgBox("There are no items in this playlist to preview...", MsgBoxStyle.Exclamation, Application.ProductName)
            Exit Sub
        End If

        If Me.vplGrid.RowCount > 0 Then
            'Code for stitching multiple video reference points into a single AVI.
            Dim nSegments As Integer = vplGrid.Rows.Count - 1
            If UserPrefs.DuplicateInSlowMotion Then
                nSegments = (vplGrid.Rows.Count * 2) - 1
            End If

            Dim szPath(nSegments) As String
            Dim dStart(nSegments) As Double
            Dim dStop(nSegments) As Double
            Dim nSpeed(nSegments) As Single

            'Fill temporary arrays with video playlist data.
            Dim Segment As Integer = 0
            For Each row As DataGridViewRow In vplGrid.Rows
                szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                If Not szPath(Segment) = Nothing Then
                    dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                    dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                    nSpeed(Segment) = 1

                    If UserPrefs.DuplicateInSlowMotion Then
                        Segment += 1
                        szPath(Segment) = FindMediaFile(row.Cells("Video").Value)
                        dStart(Segment) = GetSecondsFromTimeString(row.Cells("InPoint").Value)
                        dStop(Segment) = GetSecondsFromTimeString(row.Cells("OutPoint").Value)
                        nSpeed(Segment) = 0.5
                    End If
                    Segment += 1
                End If
            Next

            If Segment > 0 Then
                PlayPreviewFromTimeLine(BuildTimeLineFromMediaSources(nSegments + 1, szPath, dStart, dStop, nSpeed))
            Else
                MsgBox("No video sources were found in this video playlist.", MsgBoxStyle.Critical, Application.ProductName)
            End If
        End If

    End Sub

    Private Sub timExport_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timExport.Tick
        If Not Me.ExportSimple Is Nothing Then
            Me.Text = GetTimeStringFromSeconds(ExportSimple.CurrentPosition) & " --> " & GetTimeStringFromSeconds(ExportSimple.MediaDuration)

            Select Case ExportSimple.GraphStatus
                Case clsDV2Device.GraphState.StatePaused
                    Me.Text &= " (Paused)"
                Case clsDV2Device.GraphState.StateRunning
                    Me.Text &= " (Running)"
                Case clsDV2Device.GraphState.StateStopped
                    Me.Text &= " (Stopped)"
            End Select
            If ExportSimple.CurrentPosition >= ExportSimple.MediaDuration Then
                Me.mnuVPL_StopExport_Click(Nothing, Nothing)
            End If

        ElseIf Not Me.ExportGraphComplex Is Nothing Then
            Dim cp As Double = ExportComplexPosition.CurrentPosition
            Dim mt As Double = ExportComplexPosition.Duration
            Me.Text = "Exporting Media: " & GetTimeStringFromSeconds(cp) & " --> " & GetTimeStringFromSeconds(mt)
            If cp >= mt Then
                Me.mnuVPL_StopExport_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub mnuAdd2OtherVPL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdd2OtherVPL.Click
        CurrentSearch += 1
        ReDim Preserve SearchHistory(CurrentSearch)

        Dim Index_Play As Integer = 0

        For Each row As DataGridViewRow In vplGrid.Rows
            If row.Cells("PathID").Value <> "" And row.HeaderCell.Value = "S" Then
                ReDim Preserve SearchHistory(CurrentSearch).nSelectedIDs(Index_Play)
                SearchHistory(CurrentSearch).nSelectedIDs(Index_Play) = row.Cells("PathID").Value
                Index_Play += 1
            End If
        Next

        SearchHistory(CurrentSearch).szSQL = SQL_VideoPlayList & "FROM (GameData LEFT JOIN PathData ON GameData.GameID = PathData.GameID) LEFT JOIN PathOutcomes ON PathData.ID = PathOutcomes.PathID WHERE"

        For Each id As Long In SearchHistory(CurrentSearch).nSelectedIDs
            SearchHistory(CurrentSearch).szSQL &= " PathID = " & id.ToString
            If id <> SearchHistory(CurrentSearch).nSelectedIDs(Index_Play - 1) Then SearchHistory(CurrentSearch).szSQL &= " OR"
        Next
        SearchHistory(CurrentSearch).szSQL &= " ORDER BY PathID"

        'Search for any existing playlists
        If countVPL > 0 Then
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
End Class
