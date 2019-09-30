Imports System.Net
Imports System.IO

Public Class frmTags


    Friend WithEvents btnTeam As New EventButton.ctlEventButton
    Friend WithEvents btnDescriptor As New EventButton.ctlEventButton
    Friend WithEvents btnOutcome As New EventButton.ctlEventButton

    Public formDirty As Boolean

    Private btnTeamCount As Integer = -1
    Private btnDescriptorCount As Integer = -1
    Private btnPosOutCount As Integer = -1
    Private btnNegOutCount As Integer = -1

    Private mvarActive As Boolean = False
    Private mvarSaved As Boolean = False
    Private mvarLockedAll As Boolean = False

    Private lastSender As Object
    Private lastMousePoint As Point

    Private IsInitialised As Boolean = False

    Structure Queue
        Dim FileName As String
        Dim Complete As Boolean
    End Structure

    Structure TrimmingInfo
        Dim szSourceFile As String
        Dim nStart As Double
        Dim nDuration As Double
        Dim szDestinationFile As String
        Dim bAudio As Boolean
    End Structure

    Dim Transmissions() As Queue
    Dim TransmissionIsActive As Boolean
    Dim TrimmingInformation As TrimmingInfo
    Public CurrentlyTransmittingFile As String

    Structure btnSetup
        Dim nLeft As Integer
        Dim nTop As Integer
        Dim szCaption As String
        Dim cForeColor As KnownColor
        Dim cBackColor As KnownColor
        Dim nButtonShape As Integer
    End Structure

    Private Sub frmTags_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub frmTags_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress


    End Sub

    Private Sub frmTags_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            LoadButtons(Application.CommonAppDataPath & "\" & GetSetting(AppName, "Settings", "LastTags", "temp") & ".pp4")
            mvarSaved = True
            Me.Refresh()
            formDirty = False
            UpdateStatus()
            IsInitialised = True
        Catch ex As Exception
            IsInitialised = False

        End Try
    End Sub
    Private Sub frmTags_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveMe(Application.CommonAppDataPath & "\" & Me.Text & ".pp4")
        SaveSetting(AppName, "Settings", "LastTags", Me.Text)
    End Sub

    Public Property IsActive() As Boolean
        Get
            Return mvarActive
        End Get
        Set(ByVal value As Boolean)
            mvarActive = value
            UpdateStatus()
        End Set
    End Property

    Private Sub UpdateStatus()
        Dim szA As String = Nothing
        Exit Sub
        With Me.lblTagsStatus
            szA = "Status: "
            If mvarActive Then
                szA = szA & "Active"
            Else
                szA = szA & "Inactive"
            End If
            If mvarSaved Then
                szA = szA & ", Saved."
            Else
                szA = szA & ", Not Saved."
            End If
            .Text = szA
            Refresh()

        End With
    End Sub
    Private Sub AddEvent2History(ByVal szEventName As String, Optional ByVal cColor As KnownColor = KnownColor.ControlText)

        Me.lblTagsHistory.Text = szEventName & " ," & Me.lblTagsHistory.Text
        If Me.lblTagsHistory.PreferredWidth > Me.lblTagsHistory.Width Then
            For i As Integer = Len(Me.lblTagsHistory.Text) To 0 Step -1
                If Mid(Me.lblTagsHistory.Text, i, 1) = "," Then
                    Me.lblTagsHistory.Text = Microsoft.VisualBasic.Left(Me.lblTagsHistory.Text, i - 1)
                    Exit For
                End If
            Next

        End If

    End Sub
    Private Sub NewButton(ByVal cButtonType As EventButton.ctlEventButton.ctlButtonType, Optional ByVal szCaption As String = Nothing, _
                Optional ByVal cX As Integer = Nothing, Optional ByVal cY As Integer = Nothing, _
                Optional ByVal cSize As EventButton.ctlEventButton.ctlSize = EventButton.ctlEventButton.ctlSize.Small, _
                Optional ByVal cBackColor As KnownColor = KnownColor.Control, Optional ByVal cFontColor As KnownColor = KnownColor.ActiveCaption, _
                Optional ByVal boolLocked As Boolean = False, Optional ByVal kKeyStroke As Keys = Keys.None, Optional ByVal bTransmit As Boolean = False)

        Dim nPoint As Point
        If cX = Nothing Or cY = Nothing Then cX = 14 : cY = 70
        nPoint.X = cX
        nPoint.Y = cY

        Select Case cButtonType
            Case EventButton.ctlEventButton.ctlButtonType.Team
                'Create new instance of ctlEventButton
                btnTeamCount = btnTeamCount + 1
                btnTeam = New EventButton.ctlEventButton
                btnTeam.Name = "TeamButton" & btnTeamCount.ToString
                btnTeam.Location = nPoint
                btnTeam.ButtonType = cButtonType
                btnTeam.ButtonSize = cSize
                btnTeam.ButtonColor = Color.FromKnownColor(cBackColor)
                btnTeam.ButtonTextColor = Color.FromKnownColor(cFontColor)
                btnTeam.ButtonTextStyle = FontStyle.Bold
                btnTeam.KeyStroke = kKeyStroke

                If Not szCaption = Nothing Then
                    btnTeam.Caption = szCaption
                Else
                    btnTeam.Caption = "New Team " & btnTeamCount.ToString
                End If
                Me.Controls.Add(btnTeam)
                AddHandler btnTeam.MouseClick, AddressOf evtButton_MouseClick
                AddHandler btnTeam.MouseDown, AddressOf evtButton_MouseDown
                AddHandler btnTeam.ButtonMoved, AddressOf Me.evtButton_Moved
                AddHandler btnTeam.DoubleClick, AddressOf Me.evtButton_DoubleClick
                AddHandler btnTeam.KeyDown, AddressOf Me.evtButton_KeyDown
                btnTeam.Locked = boolLocked


                'Now dispose of current control instance to avoid double-handling events.
                btnTeam = Nothing

            Case EventButton.ctlEventButton.ctlButtonType.OutcomePos
                'Create new instance of ctlEventButton
                btnPosOutCount = btnPosOutCount + 1
                btnOutcome = New EventButton.ctlEventButton
                btnOutcome.Name = "PosOutcomeButton" & btnPosOutCount.ToString
                btnOutcome.Location = nPoint
                btnOutcome.ButtonType = cButtonType
                btnOutcome.ButtonSize = cSize
                btnOutcome.ButtonColor = Color.LightGreen
                btnOutcome.ButtonTextColor = Color.Black
                btnOutcome.KeyStroke = kKeyStroke
                btnOutcome.IsTransmit = bTransmit

                If Not szCaption = Nothing Then
                    btnOutcome.Caption = szCaption
                Else
                    btnOutcome.Caption = "New Positive " & btnPosOutCount.ToString
                End If
                Me.Controls.Add(btnOutcome)
                AddHandler btnOutcome.MouseClick, AddressOf evtButton_MouseClick
                AddHandler btnOutcome.MouseDown, AddressOf evtButton_MouseDown
                AddHandler btnOutcome.ButtonMoved, AddressOf Me.evtButton_Moved
                AddHandler btnOutcome.KeyDown, AddressOf Me.evtButton_KeyDown
                btnOutcome.Locked = boolLocked

                'Now dispose of current control instance to avoid double-handling events.
                btnOutcome = Nothing

            Case EventButton.ctlEventButton.ctlButtonType.OutcomeNeg
                'Create new instance of ctlEventButton
                btnNegOutCount = btnNegOutCount + 1
                btnOutcome = New EventButton.ctlEventButton
                btnOutcome.Name = "PosOutcomeButton" & btnNegOutCount.ToString
                btnOutcome.Location = nPoint
                btnOutcome.ButtonType = cButtonType
                btnOutcome.ButtonSize = cSize
                btnOutcome.ButtonColor = Color.LightCoral
                btnOutcome.ButtonTextColor = Color.Black
                btnOutcome.KeyStroke = kKeyStroke
                btnOutcome.IsTransmit = bTransmit
                If Not szCaption = Nothing Then
                    btnOutcome.Caption = szCaption
                Else
                    btnOutcome.Caption = "New Negative " & btnNegOutCount.ToString
                End If
                Me.Controls.Add(btnOutcome)
                AddHandler btnOutcome.MouseClick, AddressOf evtButton_MouseClick
                AddHandler btnOutcome.MouseDown, AddressOf evtButton_MouseDown
                AddHandler btnOutcome.ButtonMoved, AddressOf Me.evtButton_Moved
                AddHandler btnOutcome.KeyDown, AddressOf Me.evtButton_KeyDown
                btnOutcome.Locked = boolLocked

                'Now dispose of current control instance to avoid double-handling events.
                btnOutcome = Nothing

            Case EventButton.ctlEventButton.ctlButtonType.Descriptor
                'Create new instance of ctlEventButton
                btnDescriptorCount = btnDescriptorCount + 1
                btnDescriptor = New EventButton.ctlEventButton
                btnDescriptor.Name = "DescriptorButton" & btnDescriptorCount.ToString
                btnDescriptor.Location = nPoint
                btnDescriptor.ButtonType = cButtonType
                btnDescriptor.ButtonSize = cSize
                btnDescriptor.ButtonColor = Color.FromKnownColor(cBackColor)
                btnDescriptor.ButtonTextColor = Color.FromKnownColor(cFontColor)
                btnDescriptor.KeyStroke = kKeyStroke
                btnDescriptor.IsTransmit = bTransmit
                If Not szCaption = Nothing Then
                    btnDescriptor.Caption = szCaption
                Else
                    btnDescriptor.Caption = "New Descriptor " & btnDescriptorCount.ToString
                End If
                Me.Controls.Add(btnDescriptor)
                AddHandler btnDescriptor.MouseClick, AddressOf evtButton_MouseClick
                AddHandler btnDescriptor.MouseDown, AddressOf evtButton_MouseDown
                AddHandler btnDescriptor.ButtonMoved, AddressOf Me.evtButton_Moved
                AddHandler btnDescriptor.KeyDown, AddressOf Me.evtButton_KeyDown
                btnDescriptor.Locked = boolLocked

                'Now dispose of current control instance to avoid double-handling events.
                btnDescriptor = Nothing

        End Select

        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()

    End Sub
    Public Sub ChangeButtonState(Optional ByVal szCaption As String = Nothing, Optional ByVal cBackColor As KnownColor = Nothing, _
        Optional ByVal cTextColor As KnownColor = Nothing, Optional ByVal bPresetName As String = Nothing, _
        Optional ByVal bPresetX As Integer = Nothing, Optional ByVal bPresetY As Integer = Nothing, _
        Optional ByVal bPresetZoom As String = Nothing, Optional ByVal kKeyStroke As Keys = Nothing, _
        Optional ByVal bTransmit As Integer = -1, Optional ByVal bCalibrationFile As String = Nothing, _
        Optional ByVal iPhoneChartType As EventButton.ctlEventButton.AnalysisType = Nothing)

        If Not szCaption = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).Caption = szCaption
        If Not cBackColor = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).ButtonColor = Color.FromKnownColor(cBackColor)
        If Not cTextColor = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).ButtonTextColor = Color.FromKnownColor(cTextColor)
        If Not kKeyStroke = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).KeyStroke = kKeyStroke
        If Not iPhoneChartType = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).iPhoneChart = iPhoneChartType

        If Not bPresetName = Nothing Then
            DirectCast(lastSender, EventButton.ctlEventButton).PresetName = bPresetName
            Dim bLoc As Point
            bLoc.X = bPresetX
            bLoc.Y = bPresetY
            DirectCast(lastSender, EventButton.ctlEventButton).PresetPosition = bLoc
            DirectCast(lastSender, EventButton.ctlEventButton).PresetZoom = bPresetZoom
        Else
            DirectCast(lastSender, EventButton.ctlEventButton).PresetName = Nothing
            DirectCast(lastSender, EventButton.ctlEventButton).PresetZoom = Nothing
            DirectCast(lastSender, EventButton.ctlEventButton).PresetPosition = Nothing
        End If

        If bTransmit >= 0 Then DirectCast(lastSender, EventButton.ctlEventButton).IsTransmit = CBool(bTransmit)

        If Not bCalibrationFile = Nothing Then DirectCast(lastSender, EventButton.ctlEventButton).CalibrationFile = bCalibrationFile
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Public Sub LoadButtons(ByVal szFileName As String, Optional ByVal ver As Integer = 4)

        'Check the file exists
        If Not Microsoft.VisualBasic.FileIO.FileSystem.FileExists(szFileName) Then Exit Sub

        Dim b As EventButton.ctlEventButton

        If IsInitialised Then
            Me.Visible = False
        End If

        Try
            'Clear previous buttons.
            DeleteAllButtons()

            mvarLockedAll = False

            If ver = 4 Then
                'Current version
                Dim currentRow() As String
                Dim szParse As Microsoft.VisualBasic.FileIO.TextFieldParser = My.Computer.FileSystem.OpenTextFieldParser(szFileName, ";")

                While Not szParse.EndOfData
                    Try
                        currentRow = szParse.ReadFields()
                        'Get first 4 rows as window position information
                        Me.Left = CInt(currentRow(0))
                        Me.Top = CInt(currentRow(1))
                        Me.Height = CInt(currentRow(2))
                        Me.Width = CInt(currentRow(3))

                        For i As Integer = currentRow.GetLowerBound(0) + 4 To (currentRow.GetUpperBound(0) - 11) Step 11
                            NewButton(EventButton.Buttons.TypeFromName(currentRow(i + 4)), _
                                currentRow(i), _
                                CInt(currentRow(i + 6)), CInt(currentRow(i + 7)), _
                                EventButton.Buttons.SizeFromName(currentRow(i + 2)), _
                                Color.FromName(currentRow(i + 1)).ToKnownColor, _
                                Color.FromName(currentRow(i + 5)).ToKnownColor, _
                                CBool(currentRow(i + 8)), _
                                currentRow(i + 9), CBool(currentRow(i + 10)))
                        Next

                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & _
                        "is not valid and will be skipped.")
                    End Try
                End While
                szParse.Close()

            Else
                'Dim currentRow As String
                Dim fReader As System.IO.StreamReader
                fReader = My.Computer.FileSystem.OpenTextFileReader(szFileName)
                Dim uButtons As btnSetup
                Dim oTemp As String
                Dim boolVisible As Boolean = False

                'First thing - get the form window position
                Me.Left = CInt(fReader.ReadLine) / 20
                Me.Top = CInt(fReader.ReadLine) / 20
                Me.Width = CInt(fReader.ReadLine) / 20
                Me.Height = CInt(fReader.ReadLine) / 20

                While Not fReader.EndOfStream

                    'Next, loop through buttons.  In the original files, ALL of the button controls are included in this format, 
                    'so if there were 20 team button controls in the array, then there will be 20 sets of data in this file, even 
                    'if the button was not visible and not used.
                    'The event buttons followed in the same routine.
                    'Now, the only way to separate them is by the number of items stored for each button - 7 for team buttons, 6 for events.
                    'To discriminate, use the true/false dichotomy to detect the point of the Visible property and deduce from there the button type.

                    'The first 3 are all the same:
                    uButtons.nLeft = CInt(fReader.ReadLine) / 20         'Line 1
                    uButtons.nTop = CInt(fReader.ReadLine) / 20          'Line 2
                    uButtons.szCaption = fReader.ReadLine           'Line 3
                    uButtons.nButtonShape = CInt(fReader.ReadLine)  'Line 4   NB: if a team button this will collect the redundant alternative color value
                    oTemp = fReader.ReadLine                        'Line 5

                    If oTemp = "True" Or oTemp = "False" Then
                        'This button is a descriptor or outcome

                        'Collect the remain redundant information
                        fReader.ReadLine()                          'Line 6

                        If CBool(oTemp) Then
                            Select Case uButtons.nButtonShape
                                Case Is = 2
                                    'Descriptor
                                    NewButton(EventButton.ctlEventButton.ctlButtonType.Descriptor, uButtons.szCaption, uButtons.nLeft, uButtons.nTop, _
                                     , KnownColor.LightCyan, KnownColor.ActiveCaption)
                                Case Is = 0
                                    'Positive
                                    NewButton(EventButton.ctlEventButton.ctlButtonType.OutcomePos, uButtons.szCaption, uButtons.nLeft, uButtons.nTop)
                                Case Is = 4
                                    'Negative
                                    NewButton(EventButton.ctlEventButton.ctlButtonType.OutcomeNeg, uButtons.szCaption, uButtons.nLeft, uButtons.nTop)
                            End Select
                        End If

                    Else

                        'Upcolor - if not a boolean, then the Line 5 oTemp data will be the upcolor for a team button
                        uButtons.cBackColor = Color.FromArgb(CInt(oTemp)).ToKnownColor
                        uButtons.cForeColor = Color.FromArgb(CInt(fReader.ReadLine)).ToKnownColor      'Line 6
                        'Now get Line 7 - visible

                        If CBool(fReader.ReadLine) = True Then
                            NewButton(EventButton.ctlEventButton.ctlButtonType.Team, uButtons.szCaption, uButtons.nLeft, uButtons.nTop, _
                                    EventButton.ctlEventButton.ctlSize.Medium)
                        End If
                    End If
                End While
                fReader.Close()

            End If

            Me.Text = GetTitle(szFileName)
            mvarSaved = True
            'Me.UpdateStatus()

        Catch ex As Exception

        End Try

        'Set state of the Locked menu function - if all loaded buttons are locked, and there is at least 1 button then it will be true.
        Dim bCount As Integer = 0
        mvarLockedAll = True
        For Each c As Control In Me.Controls
            b = TryCast(c, EventButton.ctlEventButton)
            If Not b Is Nothing Then
                bCount = bCount + 1
                If Not b.Locked Then mvarLockedAll = False
            End If
        Next
        If bCount = 0 Then mvarLockedAll = False
        mnuLockAll.Checked = mvarLockedAll

        If IsInitialised Then
            Me.Visible = True
        End If

        Exit Sub

errCatch:
        Err.Clear()

    End Sub
    Public Sub SaveMe(Optional ByVal szFilename As String = Nothing)

        Dim dlgPlaylist As New SaveFileDialog

        If szFilename = Nothing Then
            With dlgPlaylist
                .Title = "Save event tags template..."
                .Filter = "PP4 Settings Files|*.pp4"
                .FileName = Me.Text
                .OverwritePrompt = True
                Dim res As DialogResult = .ShowDialog(frmMain)
                If res = Windows.Forms.DialogResult.Cancel Then Exit Sub
                szFilename = .FileName
            End With
        End If

        Dim fnum As Integer = FreeFile()
        Dim b As EventButton.ctlEventButton

        FileOpen(fnum, szFilename, OpenMode.Output)
        Print(fnum, Me.Location.X.ToString & ";")
        Print(fnum, Me.Location.Y.ToString & ";")
        Print(fnum, Me.Size.Height.ToString & ";")
        Print(fnum, Me.Size.Width.ToString & ";")
        For Each c As Windows.Forms.Control In Me.Controls
            b = TryCast(c, EventButton.ctlEventButton)
            If Not b Is Nothing Then
                'Event button information
                Print(fnum, b.Caption & ";")
                Print(fnum, b.ButtonColor.Name & ";")
                Print(fnum, b.ButtonSize.ToString & ";")
                Print(fnum, b.ButtonTextStyle.ToString & ";")
                Print(fnum, b.ButtonType.ToString & ";")
                Print(fnum, b.ForeColor.Name & ";")
                Print(fnum, b.Location.X.ToString & ";")
                Print(fnum, b.Location.Y.ToString & ";")
                Print(fnum, b.Locked.ToString & ";")
                Print(fnum, b.KeyStroke & ";")
                Print(fnum, b.IsTransmit & ";")
            End If
        Next

        FileClose(fnum)
        Me.formDirty = False
        mvarSaved = True
        Me.UpdateStatus()
        Me.Text = IO.Path.GetFileNameWithoutExtension(szFilename)
        Exit Sub

errCatch:
        Err.Clear()


    End Sub

    Private Sub mnuNewTeam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewTeam.Click
        NewButton(EventButton.ctlEventButton.ctlButtonType.Team, , lastMousePoint.X, lastMousePoint.Y, EventButton.ctlEventButton.ctlSize.Medium)
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuNewDescriptor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuNewDescriptor.Click
        NewButton(EventButton.ctlEventButton.ctlButtonType.Descriptor, , lastMousePoint.X, lastMousePoint.Y)
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuPosOutcome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPosOutcome.Click
        NewButton(EventButton.ctlEventButton.ctlButtonType.OutcomePos, , lastMousePoint.X, lastMousePoint.Y)
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuNegOutcome_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuNegOutcome.Click
        NewButton(EventButton.ctlEventButton.ctlButtonType.OutcomeNeg, , lastMousePoint.X, lastMousePoint.Y)
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub frmTags_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            lastMousePoint = e.Location
            Me.ContextMenuStripTags.Show(Me, e.X, e.Y)
        End If
    End Sub

    Public Sub UndoLastEntry()
        Try
            If Not frmE(lastEventFormUsed).Entries Is Nothing Then
                If frmE(lastEventFormUsed).Entries.Length > 0 Then

                    If frmE(lastEventFormUsed).Entries(frmE(lastEventFormUsed).Entries.Length - 1).IsOutcome Then
                        GamePath(PathCount).OutcomeCount = GamePath(PathCount).OutcomeCount - 1
                        If GamePath(PathCount).OutcomeCount = 0 Then GamePath(PathCount).OutcomesExist = False
                    Else
                        PathCount = PathCount - 1
                        ReDim Preserve GamePath(PathCount)
                    End If

                    frmE(lastEventFormUsed).RemoveRowFromGrid(frmE(lastEventFormUsed).Entries.Length - 1)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub evtButton_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTeam.KeyDown

        If e.Control = True And e.KeyCode = System.Windows.Forms.Keys.Z And PathCount > 0 Then
            'Undo last event
            UndoLastEntry()
        End If

        If e.KeyCode = Keys.ShiftKey Or e.KeyCode = Keys.ControlKey Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete
                DirectCast(lastSender, EventButton.ctlEventButton).Dispose()
                formDirty = True
                mvarSaved = False
                Me.UpdateStatus()

            Case Keys.Escape
                'Refresh pitch screen
                frmPitch.ClearPitch()

            Case Else
                Dim ThisKeyStroke As System.Windows.Forms.Keys
                ThisKeyStroke = e.KeyCode
                If e.Shift Then ThisKeyStroke = ThisKeyStroke + Keys.Shift
                If e.Control Then ThisKeyStroke = ThisKeyStroke + Keys.Control

                'Search through buttons for a match with the keycode
                Dim b As EventButton.ctlEventButton
                For Each c As Windows.Forms.Control In Me.Controls
                    b = TryCast(c, EventButton.ctlEventButton)
                    If Not b Is Nothing Then
                        If b.KeyStroke = ThisKeyStroke Then
                            Dim m As System.Windows.Forms.MouseEventArgs = Nothing
                            Me.evtButton_MouseClick(b, m)
                            Exit For
                        End If
                    End If
                Next
        End Select

    End Sub
    Private Sub evtButton_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTeam.DoubleClick
        frmPitch.ClearPitch()
    End Sub
    Private Sub evtButton_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTeam.MouseClick
        If frmEditTags.Created Then
            lastSender = sender
            frmEditTags.SetProperties(lastSender)
            Exit Sub
        End If


        If mvarActive Then
            If DirectCast(sender, EventButton.ctlEventButton).ButtonType = EventButton.ctlEventButton.ctlButtonType.Team Then
                'Check if a team selection
                szCurrentTeamName = DirectCast(sender, EventButton.ctlEventButton).Caption

                AddEvent2History(szCurrentTeamName, _
                    DirectCast(sender, EventButton.ctlEventButton).ButtonColor.ToKnownColor)

                frmPitch.StartNewPlay(DirectCast(sender, EventButton.ctlEventButton).Caption, _
                    DirectCast(sender, EventButton.ctlEventButton).ButtonTextColor, _
                    DirectCast(sender, EventButton.ctlEventButton).ButtonColor)


            ElseIf DirectCast(sender, EventButton.ctlEventButton).ButtonType = EventButton.ctlEventButton.ctlButtonType.Descriptor Then
                If GamePath Is Nothing Then Exit Sub
                'Check if a decriptor item

                AddEvent2History(DirectCast(sender, EventButton.ctlEventButton).Caption, _
                    DirectCast(sender, EventButton.ctlEventButton).ButtonColor.ToKnownColor)

                'If have begun plotting then PlotMode is = dlgPlotting, therefore events added to GamePath in frmPitch_MouseClick
                'Otherwise, in PostScript mode add events to GamePath here.

                If GamePath(PathCount).PlotMode = gMode.dlgPlotting Then
                    frmPitch.AddCaption(DirectCast(sender, EventButton.ctlEventButton).Caption, _
                        DirectCast(sender, EventButton.ctlEventButton).ButtonTextColor, _
                        DirectCast(sender, EventButton.ctlEventButton).ButtonColor)

                Else

                    'Update GamePath with new play.
                    PathCount += 1
                    ReDim Preserve GamePath(PathCount)
                    With GamePath(PathCount)
                        .PlotMode = gMode.dlgPostScript
                        .OutcomeCount = 1
                        .Region = tRegion.None
                        .Status = PathStatus.psPost
                        .TeamName = szCurrentTeamName
                        .GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                        If bVideoCaptureCurrent Then    'Apply video time to second time.  If no video then keep primary game time.
                            .VideoTC = frmVideoCapture.VideoTC
                        Else
                            .VideoTC = .GameTC
                        End If

                        .TimeCriteria = szCurrentTimeCriteria
                        .VideoFile = szCurrentVideoFile
                        .VideoFile2 = szCurrentVideoFile
                        .OutcomesExist = True
                        .Coordinates = New Point(0, 0)
                        .TagColor = DirectCast(sender, EventButton.ctlEventButton).ButtonColor
                        .TagFontColor = DirectCast(sender, EventButton.ctlEventButton).ButtonTextColor
                        If szCurrentVideoFile Is Nothing Then
                            .VideoFile = "None"
                            .VideoFile2 = "None"
                        End If
                    End With
                End If

                'Now add event to list for both plotting modes
                With GamePath(PathCount)
                    If .PlotMode = gMode.dlgPlotting Then .OutcomeCount += 1
                    ReDim Preserve .OutcomeProp(.OutcomeCount)
                    .OutcomeProp(.OutcomeCount).EventName = DirectCast(sender, EventButton.ctlEventButton).Caption
                    .OutcomeProp(.OutcomeCount).Outcome = OutcomeType.outDescriptor
                    .OutcomeProp(.OutcomeCount).GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                    If bVideoCaptureCurrent Then    'Apply video time to sec-outcome time.  If no video then keep primary game time.
                        .OutcomeProp(.OutcomeCount).VideoTC = frmVideoCapture.VideoTC
                    Else
                        .OutcomeProp(.OutcomeCount).VideoTC = .OutcomeProp(.OutcomeCount).GameTC
                    End If

                    .OutcomeProp(.OutcomeCount).TagColor = DirectCast(sender, EventButton.ctlEventButton).ButtonColor
                End With

                frmE(lastEventFormUsed).AddRow2Grid(PathCount, DirectCast(sender, EventButton.ctlEventButton).ButtonColor.ToKnownColor, True)

            Else
                If GamePath Is Nothing Then Exit Sub
                'Otherwise an outcome

                AddEvent2History(DirectCast(sender, EventButton.ctlEventButton).Caption, _
                    DirectCast(sender, EventButton.ctlEventButton).ButtonColor.ToKnownColor)

                'If have begun plotting then PlotMode is = dlgPlotting, therefore events added to GamePath in frmPitch_MouseClick
                'Otherwise, in PostScript mode add events to GamePath here.

                If GamePath(PathCount).PlotMode = gMode.dlgPlotting Then
                    frmPitch.AddCaption(DirectCast(sender, EventButton.ctlEventButton).Caption, _
                        DirectCast(sender, EventButton.ctlEventButton).ButtonTextColor, _
                        DirectCast(sender, EventButton.ctlEventButton).ButtonColor)

                    frmPitch.StartNewPlay()
                Else

                    'Update GamePath with new play.
                    PathCount += 1
                    ReDim Preserve GamePath(PathCount)
                    With GamePath(PathCount)
                        .PlotMode = gMode.dlgPostScript
                        .OutcomeCount = 1
                        .Region = tRegion.None
                        .Status = PathStatus.psPost
                        .TeamName = szCurrentTeamName
                        .GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                        If bVideoCaptureCurrent Then    'Apply video time to second time.  If no video then keep primary game time.
                            .VideoTC = frmVideoCapture.VideoTC
                        Else
                            .VideoTC = .GameTC
                        End If

                        .TimeCriteria = szCurrentTimeCriteria
                        .VideoFile = szCurrentVideoFile
                        .VideoFile2 = szCurrentVideoFile
                        .OutcomesExist = True
                        .Coordinates = New Point(0, 0)
                        .TagColor = DirectCast(sender, EventButton.ctlEventButton).ButtonColor
                        .TagFontColor = DirectCast(sender, EventButton.ctlEventButton).ButtonTextColor
                        If szCurrentVideoFile Is Nothing Then
                            .VideoFile = "None"
                            .VideoFile2 = "None"
                        End If
                    End With
                End If

                'Now add event to list for both plotting modes
                With GamePath(PathCount)
                    If .PlotMode = gMode.dlgPlotting Then .OutcomeCount += 1
                    ReDim Preserve .OutcomeProp(.OutcomeCount)
                    .OutcomeProp(.OutcomeCount).EventName = DirectCast(sender, EventButton.ctlEventButton).Caption
                    If DirectCast(sender, EventButton.ctlEventButton).ButtonType = EventButton.ctlEventButton.ctlButtonType.OutcomePos Then
                        .OutcomeProp(.OutcomeCount).Outcome = OutcomeType.outPositive
                    Else
                        .OutcomeProp(.OutcomeCount).Outcome = OutcomeType.outNegative
                    End If
                    .OutcomeProp(.OutcomeCount).GameTC = GetSecondsFromTimeString(frmMain.toolVideoTimeStatus.Text)

                    If bVideoCaptureCurrent Then    'Apply video time to sec-outcome time.  If no video then keep primary game time.
                        .OutcomeProp(.OutcomeCount).VideoTC = frmVideoCapture.VideoTC
                    Else
                        .OutcomeProp(.OutcomeCount).VideoTC = .OutcomeProp(.OutcomeCount).GameTC
                    End If

                    .OutcomeProp(.OutcomeCount).TagColor = DirectCast(sender, EventButton.ctlEventButton).ButtonColor
                End With


                frmE(lastEventFormUsed).AddRow2Grid(PathCount, , True)

            End If

            'Save record if entry was postscript
            If GamePath(PathCount).PlotMode = gMode.dlgPostScript Then AddRecord(LAST_PATH_SAVED)

            'Set camera position
            If modSonyCamera.SonyCameraIsConnected Then
                'Firstly, check if this button has a calibration file to install.
                If Not String.IsNullOrEmpty(DirectCast(sender, EventButton.ctlEventButton).CalibrationFile) Then
                    'set in place this file
                    frmWireless.LoadSonySettings(DirectCast(sender, EventButton.ctlEventButton).CalibrationFile)
                End If
                If DirectCast(sender, EventButton.ctlEventButton).HasPreset Then

                    Dim uPos As New CameraPreset
                    uPos.szName = DirectCast(sender, EventButton.ctlEventButton).PresetName
                    uPos.pLocation = DirectCast(sender, EventButton.ctlEventButton).PresetPosition
                    uPos.pZoom = DirectCast(sender, EventButton.ctlEventButton).PresetZoom
                    modSonyCamera.GotoPreset(uPos)
                End If
            End If

            'If in video transmit mode then:
            If UserPrefs.VideoTransmitEnabled And DirectCast(sender, EventButton.ctlEventButton).IsTransmit And bVideoCaptureCurrent Then

                Dim sTimer As New Stopwatch
                sTimer.Start()
                Do
                    Application.DoEvents()
                Loop Until sTimer.Elapsed.Seconds >= UserPrefs.LagTime

                'A. End current video
                'B. Increment video filename and initiate new capture
                Dim szCapturedFile As String = szCurrentVideoFile
                Dim nTotalVideoDuration As Double = GetSecondsFromTimeString(frmVideoCapture.lblRecordingTime.Text)
                Dim nCurrentVideoDuration As Double = frmVideoCapture.VideoTC

                frmVideoCapture.StopStartCapture()

                'C. Cut last epoch (userprefs.Transmit_Epoch) - will need to activate save2avi first
                'Send to temp directory
                Dim szDestFile As String = DirectCast(sender, EventButton.ctlEventButton).Caption
                szDestFile = szDestFile & "_" & Format(frmVideoCapture.CheckLastFileIndex(szDestFile, UserPrefs.TempFileDestination) + 1, "000") & ".avi"

                UserPrefs.VideoTransmitEpoch = UserPrefs.LeadTime + UserPrefs.LagTime

                Dim nStart As Double = (nCurrentVideoDuration - 1) - UserPrefs.VideoTransmitEpoch
                Dim nDur As Double = UserPrefs.VideoTransmitEpoch

                TotalTC = nTotalVideoDuration
                If nStart < 0 Then
                    nStart = 0
                    nDur = nCurrentVideoDuration - 1
                End If

                'Write to temp dir
                WriteFile(szCapturedFile, nStart, nDur, UserPrefs.TempFileDestination & szDestFile, GetMediaAudio(szCapturedFile))

                'D. Copy clip to remote destination.
                If My.Computer.FileSystem.DirectoryExists(UserPrefs.FileTransmitDestination) Then
                    If My.Computer.FileSystem.FileExists(UserPrefs.TempFileDestination & szDestFile) = False Then
                        MsgBox("File not copied yet")
                        Exit Sub
                    End If
                    Add2Queue(UserPrefs.TempFileDestination & szDestFile)
                    UpdateTransmissions()
                End If
            End If

        End If

    End Sub



    Private Sub evtButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTeam.MouseDown
        If DirectCast(e, MouseEventArgs).Button = Windows.Forms.MouseButtons.Right Then
            'Get button size - set menu check
            Select Case DirectCast(sender, EventButton.ctlEventButton).ButtonSize
                Case EventButton.ctlEventButton.ctlSize.Large
                    Me.mnuBTN_Large.Checked = True
                    Me.mnuBTN_Medium.Checked = False
                    Me.mnuBTN_Small.Checked = False
                Case EventButton.ctlEventButton.ctlSize.Medium
                    Me.mnuBTN_Large.Checked = False
                    Me.mnuBTN_Medium.Checked = True
                    Me.mnuBTN_Small.Checked = False
                Case EventButton.ctlEventButton.ctlSize.Small
                    Me.mnuBTN_Large.Checked = False
                    Me.mnuBTN_Medium.Checked = False
                    Me.mnuBTN_Small.Checked = True
            End Select

            'Get locked state
            Me.mnuLockButton.Checked = DirectCast(sender, EventButton.ctlEventButton).Locked
            Me.mnuDeleteButton.Enabled = Not Me.mnuLockButton.Checked
            Me.mnuButtonSize.Enabled = Not Me.mnuLockButton.Checked
            Me.mnuRenameButton.Enabled = Not Me.mnuLockButton.Checked
            Me.mnuEditButton.Enabled = Not Me.mnuLockButton.Checked

            'Get button location
            Dim pt As Point = DirectCast(e, MouseEventArgs).Location
            pt = pt + DirectCast(sender, EventButton.ctlEventButton).Location
            Me.ContextMenuButtonMenu.Show(Me, pt)

        Else
            'Left button click code here...

            'If tags window is open then send button data to button edit form.
        End If

        lastSender = sender
        '        Me.Text = DirectCast(lastSender, EventButton.ctlEventButton).Name
    End Sub
    Private Sub evtButton_Moved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTeam.ButtonMoved

    End Sub

    Private Sub mnuBTN_Large_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuBTN_Large.Click
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonSize = EventButton.ctlEventButton.ctlSize.Large
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuBTN_Medium_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuBTN_Medium.Click
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonSize = EventButton.ctlEventButton.ctlSize.Medium
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuBTN_Small_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuBTN_Small.Click
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonSize = EventButton.ctlEventButton.ctlSize.Small
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub

    Private Sub mnuRenameButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRenameButton.Click
        Me.mnuRenameTextbox.TextBox.Text = DirectCast(lastSender, EventButton.ctlEventButton).Caption
    End Sub
    Private Sub mnuRenameTextbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRenameTextbox.TextChanged
        DirectCast(lastSender, EventButton.ctlEventButton).Caption = Me.mnuRenameTextbox.TextBox.Text
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuDeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteButton.Click
        DirectCast(lastSender, EventButton.ctlEventButton).Kill()
        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub
    Private Sub mnuLockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLockButton.Click
        mnuLockButton.Checked = Not mnuLockButton.Checked
        DirectCast(lastSender, EventButton.ctlEventButton).Locked = mnuLockButton.Checked

        'Turn off locked_all check in menu
        If Not mnuLockButton.Checked Then
            mnuLockAll.Checked = False
            mvarLockedAll = False
        End If

    End Sub
    Private Sub mnuEditButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditButton.Click
        frmEditTags.MdiParent = frmMain
        frmEditTags.TopMost = True
        frmEditTags.Show()
        frmEditTags.SetProperties(lastSender)

    End Sub

    Public Sub mnuLoadTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoadTemplate.Click
        'Load transient video into video window.
        Dim dlgOpenButtons As OpenFileDialog
        dlgOpenButtons = New OpenFileDialog

        With dlgOpenButtons
            .DefaultExt = "*.pp4"
            .Title = "Open Event Tags Template..."
            .Filter = "PP4 Settings Files|*.pp4|PSF Settings Files - Version 2|*.psf|All Files|*.*"

            If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                Select Case Microsoft.VisualBasic.Right(.FileName, 3)
                    Case Is = "pp4"
                        'Version 4 file (includes new features)
                        LoadButtons(.FileName)
                    Case Else
                        'Previous versions (new features are set to default values)
                        LoadButtons(.FileName, 3)
                End Select

            End If
        End With

        formDirty = False

    End Sub
    Public Sub mnuSaveTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveTemplate.Click
        SaveMe()
    End Sub

    Private Sub mnuLockAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLockAll.Click

        mnuLockAll.Checked = Not mnuLockAll.Checked
        Me.mvarLockedAll = mnuLockAll.Checked

        Dim b As EventButton.ctlEventButton
        For Each c As Windows.Forms.Control In Me.Controls
            b = TryCast(c, EventButton.ctlEventButton)
            If Not b Is Nothing Then
                b.Locked = mvarLockedAll
            End If
        Next

    End Sub
    Private Sub mnuDuplicate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDuplicate.Click
        NewButton(DirectCast(lastSender, EventButton.ctlEventButton).ButtonType, _
        DirectCast(lastSender, EventButton.ctlEventButton).Caption, _
        DirectCast(lastSender, EventButton.ctlEventButton).Location.X + 10, DirectCast(lastSender, EventButton.ctlEventButton).Location.Y + 10, _
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonSize, _
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonColor.ToKnownColor, _
        DirectCast(lastSender, EventButton.ctlEventButton).ButtonTextColor.ToKnownColor)

        formDirty = True
        mvarSaved = False
        Me.UpdateStatus()
    End Sub

    Private Sub btnTeam_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnTeam.MouseDoubleClick
        'Clear pitch
        frmPitch.ClearPitch()
    End Sub

    Private Function Add2Queue(ByVal FileName As String)
        'Takes a video path and adds it to the queue of transmittions.
        Dim nCount As Integer
        Try
            nCount = Transmissions.GetUpperBound(0) + 1
        Catch ex As Exception
            nCount = 0
        End Try
        ReDim Preserve Transmissions(nCount)
        Transmissions(nCount).FileName = FileName
        Transmissions(nCount).Complete = False

        Return nCount
    End Function

    Public Sub UpdateTransmissions()

        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
            'Nothing being transmitted - find next file for sending.
            For Each ref As Queue In Transmissions
                If Not ref.Complete Then
                    'Send new file to background worker
                    CurrentlyTransmittingFile = ref.FileName
                    frmMain.toolAxillary.Text = "Remote Status: Transmitting -> " & CurrentlyTransmittingFile

                    BackgroundWorker1.RunWorkerAsync(CurrentlyTransmittingFile)
                    Exit For
                End If
            Next
        Else

        End If

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'TransmissionIsActive = True
        'My.Computer.FileSystem.CopyFile(UserPrefs.VideoCaptureDir & CurrentlyTransmittingFile, UserPrefs.VideoTransmitDestination & CurrentlyTransmittingFile)
        My.Computer.Network.UploadFile(e.Argument, UserPrefs.FileTransmitDestination & IO.Path.GetFileName(e.Argument))
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        'MsgBox("Message Delivered...")
        Beep()
        For i As Integer = 0 To Transmissions.GetUpperBound(0)
            If Transmissions(i).Complete = False Then
                Transmissions(i).Complete = True
                Exit For
            End If
        Next
        TransmissionIsActive = False
        frmMain.toolAxillary.Text = "Remote Status: Queue empty..."
        UpdateTransmissions()
    End Sub

    Private Sub Upload(ByVal fileName As String, ByVal uploadUrl As String)
        Dim requestStream As Stream = Nothing
        Dim fileStream As FileStream = Nothing
        Dim uploadResponse As FtpWebResponse = Nothing
        Try
            Dim uploadRequest As FtpWebRequest = WebRequest.Create(uploadUrl)
            uploadRequest.Method = WebRequestMethods.Ftp.UploadFile

            ' UploadFile is not supported through an Http proxy
            ' so we disable the proxy for this request.
            uploadRequest.Proxy = Nothing

            requestStream = uploadRequest.GetRequestStream()
            fileStream = File.Open(fileName, FileMode.Open)

            Dim buffer(1024) As Byte
            Dim bytesRead As Integer
            While True
                bytesRead = fileStream.Read(buffer, 0, buffer.Length)
                If bytesRead = 0 Then
                    Exit While
                End If
                requestStream.Write(buffer, 0, bytesRead)
            End While

            ' The request stream must be closed before getting the response.
            requestStream.Close()

            uploadResponse = uploadRequest.GetResponse()
            Console.WriteLine("Upload complete.")
        Catch ex As UriFormatException
            Console.WriteLine(ex.Message)
        Catch ex As IOException
            Console.WriteLine(ex.Message)
        Catch ex As WebException
            Console.WriteLine(ex.Message)
        Finally
            If uploadResponse IsNot Nothing Then
                uploadResponse.Close()
            End If
            If fileStream IsNot Nothing Then
                fileStream.Close()
            End If
            If requestStream IsNot Nothing Then
                requestStream.Close()
            End If
        End Try
    End Sub

    Private Sub mnuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearAll.Click
        Dim res As MsgBoxResult = MsgBox("This action will remove ALL items from the current tags window.  Continue?", MsgBoxStyle.OkCancel)
        If res = MsgBoxResult.Ok Then
            DeleteAllButtons()
        End If
    End Sub

    Private Sub DeleteAllButtons()
        Dim r As EventButton.ctlEventButton() = Nothing
        For Each button As Control In Me.Controls
            'Get an array of the button controls first.
            Dim k As EventButton.ctlEventButton
            k = TryCast(button, EventButton.ctlEventButton)
            If Not k Is Nothing Then
                If r Is Nothing Then
                    ReDim r(0)
                    r(r.Length - 1) = k
                Else
                    ReDim Preserve r(r.Length)
                    r(r.Length - 1) = k
                End If
            End If
        Next

        If Not r Is Nothing Then
            For i As Integer = 0 To r.Length - 1
                r(i).Kill()
            Next
        End If

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class