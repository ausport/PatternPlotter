Public Class clsVideoTrimmer
    '*******************************************************************************
    '*       This is a part of the Microsoft DXSDK Code Samples.
    '*       Copyright (C) 1999-2001 Microsoft Corporation.
    '*       All rights reserved.
    '*       This source code is only intended as a supplement to
    '*       Microsoft Development Tools and/or SDK documentation.
    '*       See these sources for detailed information regarding the
    '*       Microsoft samples programs.
    '*******************************************************************************

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                 ENUMERATION DECLARATIONS
    '* * *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public Enum GraphState
        StateStopped = 0
        StatePaused = 1
        StateRunning = 2
    End Enum

    Private m_objMediaDet As DexterLib.MediaDet
    'To fire this event, use RaiseEvent with the following syntax:
    'RaiseEvent FileComplete[(arg1, arg2, ... , argn)]
    Public Event FileComplete()

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   OTHER CLASS DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private m_dDuration As Double
    Private m_bProcessComplete As Boolean
    Public GraphStatus As GraphState
    Private m_PercentComplete As Single


    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- WIN32 API CONSTANTS
    ' *
    ' *
    Private Const FO_COPY = &H2
    Private Const FO_DELETE = &H3
    Private Const FO_MOVE = &H1
    Private Const FO_RENAME = &H4
    Private Const FOF_ALLOWUNDO = &H40
    Private Const FOF_CONFIRMMOUSE = &H2
    Private Const FOF_FILESONLY = &H80      ''"" on *.*, do only files
    Private Const FOF_MULTIDESTFILES = &H1
    Private Const FOF_NOCONFIRMATION = &H10      ''"" Don't prompt the user.
    Private Const FOF_NOCONFIRMMKDIR = &H200     ''"" don't confirm making any needed dirs
    Private Const FOF_NOCOPYSECURITYATTRIBS = &H800     ''"" dont copy NT file Security Attributes
    Private Const FOF_NOERRORUI = &H400     ''"" don't put up error UI
    Private Const FOF_NORECURSION = &H1000    ''"" don't recurse into directories.
    Private Const FOF_NO_CONNECTED_ELEMENTS = &H2000    ''"" don't operate on connected file elements.
    Private Const FOF_RENAMEONCOLLISION = &H8
    Private Const FOF_SILENT = &H4       ''"" don't create progress"report
    Private Const FOF_SIMPLEPROGRESS = &H100     ''"" means don't show names of files
    Private Const FOF_WANTMAPPINGHANDLE = &H20      ''"" Fill in SHFILEOPSTRUCT.hNameMappings
    Private Const MAX_PATH As Long = 255
    Private Const INVALID_HANDLE_VALUE = -1
    Private Const SEM_FAILCRITICALERRORS = &H1
    Private Const SEM_NOfuncOpenFileERRORBOX = &H8000
    Private Const SEE_MASK_CLASSKEY = &H3
    Private Const SEE_MASK_CLASSNAME = &H1
    Private Const SEE_MASK_CONNECTNETDRV = &H80
    Private Const SEE_MASK_DOENVSUBST = &H200
    Private Const SEE_MASK_FLAG_DDEWAIT = &H100
    Private Const SEE_MASK_FLAG_NO_UI = &H400
    Private Const SEE_MASK_HOTKEY = &H20
    Private Const SEE_MASK_ICON = &H10
    Private Const SEE_MASK_IDLIST = &H4
    Private Const SEE_MASK_INVOKEIDLIST = &HC
    Private Const SEE_MASK_NOCLOSEPROCESS = &H40

    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- WIN32 API DATA STRUCTURES
    ' *
    ' *
    Private Structure FILETIME
        Dim dwLowDateTime As Long
        Dim dwHighDateTime As Long
    End Structure

    Private Structure WIN32_FIND_DATA
        Dim dwFileAttributes As Long
        Dim ftCreationTime As FILETIME
        Dim ftLastAccessTime As FILETIME
        Dim ftLastWriteTime As FILETIME
        Dim nFileSizeHigh As Long
        Dim nFileSizeLow As Long
        Dim dwReserved0 As Long
        Dim dwReserved1 As Long
        Dim cFileName As String '* MAX_PATH
        Dim cAlternate As String '* 14
    End Structure

    Private Structure SHFILEOPSTRUCT
        Dim hWnd As Long
        Dim wFunc As Long
        Dim pFrom As String
        Dim pTo As String
        Dim fFlags As Long
        Dim fAnyOperationsAborted As Long
        Dim hNameMappings As Long
        Dim lpszProgressTitle As String '  only used if FOF_SIMPLEPROGRESS
    End Structure

    Private Structure SHELLEXECUTEINFO
        Dim cbSize As Long
        Dim fMask As Long
        Dim hWnd As Long
        Dim lpVerb As String
        Dim lpFile As String
        Dim lpParameters As String
        Dim lpDirectory As String
        Dim nShow As Long
        Dim hInstApp As Long
        '  Optional fields
        Dim lpIdList As Long
        Dim lpClass As String
        Dim hkeyClass As Long
        Dim dwHotKey As Long
        Dim hIcon As Long
        Dim hProcess As Long
    End Structure


    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- WIN32 API DECLARATIONS
    ' *
    ' *
    Private Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Long) As Long
    Private Declare Function SetErrorMode Lib "kernel32" (ByVal wMode As Long) As Long
    Private Declare Function ShellExecuteEx Lib "shell32" (ByVal lpExecInfo As SHELLEXECUTEINFO) As Long
    Private Declare Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperationA" (ByVal lpFileOp As SHFILEOPSTRUCT) As Long
    Private Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, ByVal lpFindFileData As WIN32_FIND_DATA) As Long
    Private Declare Function GetTempPath Lib "kernel32" Alias "GetTempPathA" (ByVal nBufferLength As Long, ByVal lpBuffer As String) As Long

    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- DEXTER PROCEDURES
    ' *
    ' *
    ' ******************************************************************************************************************************
    ' * procedure name: GetPinInfo
    ' * procedure description:  Returns an IPinInfo interface given a filtergraph manager and IPin object.
    ' *                                       The derived IPinInfo interface can be utilized for gaining information on the elected pin.
    ' ******************************************************************************************************************************
    Private Function GetPinInfo(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager, ByVal objPin As DexterLib.IPin) As QuartzTypeLib.IPinInfo
        On Error GoTo ErrLine
        Dim objPin2 As DexterLib.IPin = Nothing
        Dim objPinInfo As QuartzTypeLib.IPinInfo = Nothing
        Dim objFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objPinCollection As Object = Nothing

        'derive a filter collection from the filtergraph manager
        Dim objlFilterCollection As Object = objFilterGraphManager.FilterCollection

        'enumerate through the filter(s) in the collection
        For Each objFilterInfo In objlFilterCollection
            objPinCollection = objFilterInfo.Pins
            For Each objPinInfo In objPinCollection
                objPin2 = objPinInfo.Pin
                If objPin2 Is objPin Then
                    Return objPinInfo
                    Exit Function
                End If
            Next
        Next

        'clean-up & dereference
        If Not objPin2 Is Nothing Then objPin2 = Nothing
        If Not objPinInfo Is Nothing Then objPinInfo = Nothing
        If Not objFilterInfo Is Nothing Then objFilterInfo = Nothing
        If Not objPinCollection Is Nothing Then objPinCollection = Nothing
        If Not objlFilterCollection Is Nothing Then objlFilterCollection = Nothing
        Return Nothing

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: AddFileWriterAndMux
    ' * procedure description:  Appends a filewriter and mux filter to the given filtergraph.
    ' *                                       The FileName as required for the filewriter and evaluates to the output file destination.
    ' ******************************************************************************************************************************
    Private Sub AddFileWriterAndMux(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager, ByVal bstrFileName As String)
        Dim objFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegisteredFilters As Object = Nothing
        Dim objAVIMuxFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilterInfo As QuartzTypeLib.IRegFilterInfo = Nothing

        'Need this???
        Dim objFileSinkFilterVB As DshowForVBLib.IFileSinkFilterForVB

        On Error GoTo ErrLine

        'derive a collection of registered filters from the filtergraph manager
        objRegisteredFilters = objFilterGraphManager.RegFilterCollection

        'enumerate through the registered filters
        For Each objRegFilterInfo In objRegisteredFilters
            If Trim(LCase(objRegFilterInfo.Name)) = "file writer" Then
                objRegFilterInfo.filter(objFilterInfo)
            ElseIf Trim(LCase(objRegFilterInfo.Name)) = "avi mux" Then
                objRegFilterInfo.filter(objAVIMuxFilterInfo)
            End If
        Next

        'derive the file sink filter tailored for vb
        objFileSinkFilterVB = objFilterInfo.Filter
        'assign the filename to the sink filter
        Call objFileSinkFilterVB.SetFileName(bstrFileName, Nothing)

        'Dim fx As FSFWRAPLib.SinkInfo
        'fx = New FSFWRAPLib.SinkInfo
        'fx.Filter = objFilterInfo.Filter
        'If fx.IsFileSink <> 0 Then fx.Filename = bstrFileName


        'clean-up & dereference
        If Not objFilterInfo Is Nothing Then objFilterInfo = Nothing
        If Not objRegFilterInfo Is Nothing Then objRegFilterInfo = Nothing
        ' If Not objFileSinkFilterVB Is Nothing Then objFileSinkFilterVB = Nothing
        If Not objAVIMuxFilterInfo Is Nothing Then objAVIMuxFilterInfo = Nothing
        If Not objRegisteredFilters Is Nothing Then objRegisteredFilters = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: RenderGroupPins
    ' * procedure description:  Renders the Pins out for the given timeline using the given render engine.
    ' *
    ' ******************************************************************************************************************************
    Private Sub RenderGroupPins(ByVal objRenderEngine As DexterLib.RenderEngine, ByVal objTimeline As DexterLib.AMTimeline)
        Dim objPin As DexterLib.IPin = Nothing
        Dim nCount As Long
        Dim nGroupCount As Long
        Dim objPinInfo As QuartzTypeLib.IPinInfo
        Dim objFilterGraphManager As QuartzTypeLib.FilgraphManager = Nothing
        On Error GoTo ErrLine

        If Not objTimeline Is Nothing Then
            If Not objRenderEngine Is Nothing Then
                'obtain the group count
                objTimeline.GetGroupCount(nGroupCount)
                'exit the procedure if there are no group(s)
                If nGroupCount = 0 Then Exit Sub
                'obtain the filtergraph
                objRenderEngine.GetFilterGraph(objFilterGraphManager)
                'enumerate through the groups & render the pins
                For nCount = 0 To nGroupCount - 1
                    objRenderEngine.GetGroupOutputPin(nCount, objPin)
                    If Not objPin Is Nothing Then
                        objPinInfo = GetPinInfo(objFilterGraphManager, objPin)
                        If Not objPinInfo Is Nothing Then
                            Call objPinInfo.Render()
                        End If
                    End If
                Next
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Resume Next
        Exit Sub
    End Sub



    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- GENERAL PROCEDURES
    ' *
    ' *
    ' ******************************************************************************************************************************
    ' * procedure name: Buffer_ParseEx
    ' * procedure description:   Parse's a fixed length string buffer of all vbNullCharacters AND vbNullStrings.
    ' *                                        Argument bstrBuffer evaluates to either an ANSII or Unicode BSTR string buffer.
    ' *                                        (bstrBuffer is almost always the output from a windows api call which needs parsed)
    ' *
    ' ******************************************************************************************************************************
    Private Function Buffer_ParseEx(ByVal bstrBuffer As String) As String
        Dim iCount As Long, bstrChar As String, bstrReturn As String = Nothing
        On Error GoTo ErrLine

        For iCount = 1 To Len(bstrBuffer) 'set up a loop to remove the vbNullChar's from the buffer.
            bstrChar = Strings.Mid(bstrBuffer, iCount, 1)
            If bstrChar <> vbNullChar And bstrChar <> vbNullString Then bstrReturn = (bstrReturn + bstrChar)
        Next
        Buffer_ParseEx = bstrReturn
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: GetTempDirectory
    ' * procedure description:  Returns a bstr String representing the fully qualified path to the system's temp directory
    ' *
    ' ******************************************************************************************************************************
    Private Function GetTempDirectory() As String
        Dim bstrBuffer As String = Nothing '* MAX_PATH
        On Error GoTo ErrLine

        'call the win32api
        Call GetTempPath(MAX_PATH, bstrBuffer)
        'parse & return the value to the client
        GetTempDirectory = Buffer_ParseEx(bstrBuffer)
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function



    ' ******************************************************************************************************************************
    ' * procedure name: File_Exists
    ' * procedure description:  Returns true if the specified file does in fact exist.
    ' *
    ' ******************************************************************************************************************************
    Private Function File_Exists(ByVal bstrFileName As String) As Boolean
        Dim WFD As WIN32_FIND_DATA = Nothing, hFile As Long
        On Error GoTo ErrLine

        hFile = FindFirstFile(bstrFileName, WFD)
        File_Exists = hFile <> INVALID_HANDLE_VALUE
        Call FindClose(hFile)
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: File_Delete
    ' * procedure description:  This will delete a File. Pass any of the specified optionals to invoke those particular features.
    ' *
    ' ******************************************************************************************************************************
    Private Function File_Delete(ByVal bstrFileName As String, Optional ByVal SendToRecycleBin As Boolean = True, Optional ByVal Confirm As Boolean = True, Optional ByVal ShowProgress As Boolean = True) As Long
        Dim fileop As SHFILEOPSTRUCT = Nothing
        Dim WFD As WIN32_FIND_DATA = Nothing, hFile As Long
        On Error GoTo ErrLine

        'check argument
        If Right(bstrFileName, 1) = "\" Then bstrFileName = Left(bstrFileName, (Len(bstrFileName) - 1))
        'ensure the file exists
        hFile = FindFirstFile(bstrFileName, WFD)
        If hFile = INVALID_HANDLE_VALUE Then
            Call FindClose(hFile)
            Exit Function
        Else : Call FindClose(hFile)
        End If
        'set the error mode
        Call SetErrorMode(SEM_NOfuncOpenFileERRORBOX + SEM_FAILCRITICALERRORS)
        'set up the file operation by the specified optionals
        With fileop
            .hWnd = 0 : .wFunc = FO_DELETE
            .pFrom = UCase(bstrFileName) & vbNullChar & vbNullChar
            If SendToRecycleBin Then   'goes to recycle bin
                .fFlags = FOF_ALLOWUNDO
                If Confirm = False Then .fFlags = .fFlags + FOF_NOCONFIRMATION 'do not confirm
                If ShowProgress = False Then .fFlags = .fFlags + FOF_SILENT 'do not show progress
            Else 'just delete the file
                If Confirm = False Then .fFlags = .fFlags + FOF_NOCONFIRMATION 'do not confirm
                If ShowProgress = False Then .fFlags = .fFlags + FOF_SILENT 'do not show progress
            End If
        End With
        'execute the file operation, return any errors..
        File_Delete = SHFileOperation(fileop)
        Exit Function

ErrLine:
        File_Delete = Err.Number  'if there was a abend in the procedure, return that too..
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: File_Execute
    ' * procedure description:  Executes a file using it's default shell command and returns a handle to the new process.
    ' *                                       Function returns zero if the operation fails.  Never displays any error dialogs for the user.
    ' *
    ' ******************************************************************************************************************************
    Private Function File_Execute(ByVal bstrDirectory As String, ByVal bstrFile As String, Optional ByVal bstrArguments As String = Nothing, Optional ByVal Show As Long = 1) As Long
        Dim ExecInfo As SHELLEXECUTEINFO = Nothing
        On Error GoTo ErrLine

        'verify argument(s)
        If Len(bstrDirectory) > 0 Then
            If Right(bstrDirectory, 1) = "\" Then
                bstrDirectory = Trim(LCase(Mid(bstrDirectory, 1, Len(bstrDirectory) - 1)))
            End If
        ElseIf Len(bstrFile) > 0 Then
            If Right(bstrFile, 1) = "\" Then
                bstrFile = Trim(LCase(Mid(bstrFile, 1, Len(bstrFile) - 1)))
            End If
        End If

        'fill data struct
        With ExecInfo
            .nShow = 1
            .cbSize = Len(ExecInfo)
            .lpFile = bstrFile
            .lpDirectory = bstrDirectory
            .lpParameters = bstrArguments
            .fMask = SEE_MASK_FLAG_NO_UI + SEE_MASK_DOENVSUBST + SEE_MASK_NOCLOSEPROCESS '+ CREATE_NEW_CONSOLE
        End With

        'execute the application
        Call ShellExecuteEx(ExecInfo)
        'return the process id to the client
        File_Execute = ExecInfo.hProcess
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    Public Sub WriteFile(ByVal FileSource As String, ByVal StartTime As Double, ByVal dblDuration As Double, _
        ByVal FileDest As String, Optional ByVal HasAudio As Boolean = False, Optional ByVal owner As Form = Nothing)
        Dim nState As Long
        Dim nReturnCode As Long
        Dim bdlPosition As Double
        Dim StopTime As Double

        Dim objMediaEvent As QuartzTypeLib.IMediaEvent = Nothing
        Dim objMediaPosition As QuartzTypeLib.IMediaPosition = Nothing
        Dim objFilterGraphManager As QuartzTypeLib.FilgraphManager = Nothing

        Dim objTimeline As DexterLib.AMTimeline = Nothing
        Dim objSourceObj As DexterLib.AMTimelineObj = Nothing
        Dim objTrackObject As DexterLib.AMTimelineObj = Nothing
        Dim objAudioGroupObj As DexterLib.AMTimelineObj = Nothing
        Dim objVideoGroupObject As DexterLib.AMTimelineObj = Nothing

        Dim objSource As DexterLib.AMTimelineSrc = Nothing
        Dim objTrack As DexterLib.AMTimelineTrack = Nothing
        Dim objAudioGroup As DexterLib.AMTimelineGroup = Nothing
        Dim objVideoGroup As DexterLib.AMTimelineGroup = Nothing
        Dim objAudioComposition As DexterLib.AMTimelineComp = Nothing
        Dim objVideoComposition As DexterLib.AMTimelineComp = Nothing
        Dim objSmartRenderEngine As New DexterLib.SmartRenderEngine

        On Error GoTo ErrLine

        m_PercentComplete = 0

        'Instantiate the timeline
        objTimeline = New DexterLib.AMTimeline
        'Create empty node on timeline for video
        objTimeline.CreateEmptyNode(objVideoGroupObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'Derive video group object from the timeline object.
        objVideoGroup = objVideoGroupObject
        'Set the media type of the video group.
        objVideoGroup.SetMediaTypeForVB(0)
        'Append the video group to the timeline.
        objTimeline.AddGroup(objVideoGroup)

        'Create empty node on timeline for the track.
        objTimeline.CreateEmptyNode(objTrackObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
        'Obtain a composition from the video group.
        objVideoComposition = objVideoGroup
        'Inset the track into the composition.
        objVideoComposition.VTrackInsBefore(objTrackObject, -1)
        'Derive the track object.
        objTrack = objTrackObject

        'Create empty node on timeline for the source clip.
        objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
        'Derive source clip from the timeline object.
        objSource = objSourceObj

        'Set duration times
        StopTime = StartTime + dblDuration


        'Set Start and stop times to the source clip.
        objSourceObj.SetStartStop2(0, dblDuration)
        objSource.SetMediaTimes2(StartTime, StopTime)
        objSource.SetMediaName(FileSource)
        'Append source clip to the track.
        objTrack.SrcAdd(objSourceObj)

        'Check for and facillitate audio.
        If HasAudio Then
            'Create an empty node on the timeline for the audio group.
            objTimeline.CreateEmptyNode(objAudioGroupObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
            'Derive the audio group from the timeline object.
            objAudioGroup = objAudioGroupObj
            'Set the media type of the audio group.
            objAudioGroup.SetMediaTypeForVB(1)
            'Append the group to the timeline.
            objTimeline.AddGroup(objAudioGroup)

            'Create an empty node on the timeline for the audio track.
            objTimeline.CreateEmptyNode(objTrackObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
            'Derive a composition from the audio group.
            objAudioComposition = objAudioGroup
            'Insert the track into the composition
            objAudioComposition.VTrackInsBefore(objTrackObject, -1)
            'Derive a track object from the timeline object.
            objTrack = objTrackObject

            'Create an empty node for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive a source object from the timeline object
            objSource = objSourceObj
            'Set Start and stop times to the source clip.
            objSourceObj.SetStartStop2(0, dblDuration)
            objSource.SetMediaTimes2(StartTime, StopTime)
            objSource.SetMediaName(FileSource)
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
        End If

        'Set the recompression format of the video group.
        objVideoGroup.SetRecompFormatFromSource(objSource)
        'Set the timeline to the render engine.
        objSmartRenderEngine.SetTimelineObject(objTimeline)
        'Connect-up the render engine.
        objSmartRenderEngine.ConnectFrontEnd()
        'Obtain a reference to the filter graph for the timeline.
        objSmartRenderEngine.GetFilterGraph(objFilterGraphManager)
        'Add a file writer and a mux filter to the filtergraph.
        AddFileWriterAndMux(objFilterGraphManager, FileDest)
        'Render output pins and prepare to smart write file.
        RenderGroupPins(objSmartRenderEngine, objTimeline)
        'Run the graph
        objFilterGraphManager.Run()
        GraphStatus = GraphState.StateRunning

        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        Dim szoldtitle As String = Nothing
        If Not owner Is Nothing Then szOldTitle = owner.Text

        Do : Application.DoEvents()
            If Not objMediaEvent Is Nothing Then
                'Set completed %
                m_PercentComplete = CSng(objMediaPosition.CurrentPosition / objMediaPosition.Duration)
                'If Not owner Is Nothing Then
                '    owner.Text = "Exporting --> Please Wait (" & Int(m_PercentComplete).ToString & ")"
                'End If
                'Debug.Print m_PercentComplete
                Application.DoEvents()
                Call objMediaEvent.WaitForCompletion(100, nReturnCode)
                If nReturnCode = 1 Then Exit Do
            Else : Exit Do
            End If
        Loop
        m_PercentComplete = -1

        'If Not owner Is Nothing Then owner.Text = szoldtitle
        GraphStatus = GraphState.StateStopped
        Beep()

CleanUp:
        'scrap the render engine
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine.ScrapIt()
        'clean-up & dereference quartz object(s)
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaPosition Is Nothing Then objMediaPosition = Nothing
        If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing
        'clean-up & dereference dexter timeline object(s)
        If Not objTimeline Is Nothing Then objTimeline = Nothing
        If Not objSourceObj Is Nothing Then objSourceObj = Nothing
        If Not objTrackObject Is Nothing Then objTrackObject = Nothing
        If Not objAudioGroupObj Is Nothing Then objAudioGroupObj = Nothing
        If Not objVideoGroupObject Is Nothing Then objVideoGroupObject = Nothing
        'clean-up & dereference dexter timeline object(s)
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objSource Is Nothing Then objSource = Nothing
        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing
        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing
        If Not objAudioComposition Is Nothing Then objAudioComposition = Nothing
        If Not objVideoComposition Is Nothing Then objVideoComposition = Nothing
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine = Nothing

        RaiseEvent FileComplete()

        Exit Sub

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal)
                Err.Clear() : GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal) : Err.Clear() : GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal) : Err.Clear() : GoTo CleanUp
        End Select
        Exit Sub
    End Sub

    Public Sub WriteFile2(ByVal iSegmentCount As Integer, ByVal FileSource() As String, _
        ByVal lStartPoint() As Double, ByVal lEndPoint() As Double, _
        ByVal FileDest As String, Optional ByVal HasAudio As Boolean = False, Optional ByVal owner As Form = Nothing)

        Dim nState As Long
        Dim nReturnCode As Long
        Dim bdlPosition As Double
        Dim StopTime As Double
        Dim dTimeOnTrack As Double

        Dim objMediaEvent As QuartzTypeLib.IMediaEvent = Nothing
        Dim objMediaPosition As QuartzTypeLib.IMediaPosition = Nothing
        Dim objFilterGraphManager As QuartzTypeLib.FilgraphManager = Nothing

        Dim m_objRegFilterInfo As Object = Nothing ' IFilterInfo interface represents all registered filters on the system
        Dim m_objFilterInfo As Object = Nothing        'IFilterInfo interface represents all filters in the current graph

        Dim objTimeline As DexterLib.AMTimeline = Nothing
        Dim objSourceObj As DexterLib.AMTimelineObj = Nothing
        Dim objTrackObject As DexterLib.AMTimelineObj = Nothing
        Dim objAudioGroupObj As DexterLib.AMTimelineObj = Nothing
        Dim objVideoGroupObject As DexterLib.AMTimelineObj = Nothing

        Dim objSource As DexterLib.AMTimelineSrc = Nothing
        Dim objTrack As DexterLib.AMTimelineTrack = Nothing
        Dim objAudioGroup As DexterLib.AMTimelineGroup = Nothing
        Dim objVideoGroup As DexterLib.AMTimelineGroup = Nothing
        Dim objAudioComposition As DexterLib.AMTimelineComp = Nothing
        Dim objVideoComposition As DexterLib.AMTimelineComp = Nothing
        Dim objSmartRenderEngine As New DexterLib.SmartRenderEngine

        Dim objRegFilter As QuartzTypeLib.IRegFilterInfo = Nothing
        Dim objFilter As QuartzTypeLib.IFilterInfo = Nothing
        Dim objFilter2 As QuartzTypeLib.IFilterInfo = Nothing
        Dim objFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objPinInfo As QuartzTypeLib.IPinInfo = Nothing
        Dim objConnPinInfo As QuartzTypeLib.IPinInfo = Nothing

        Dim fi_DV As QuartzTypeLib.IFilterInfo = Nothing
        Dim pi_DV_Input As QuartzTypeLib.IPinInfo = Nothing

        Dim fi_ReComp As QuartzTypeLib.IFilterInfo = Nothing
        Dim pi_RC_Output As QuartzTypeLib.IPinInfo = Nothing

        Dim fi_Tee As QuartzTypeLib.IFilterInfo = Nothing
        Dim pi_Tee_Input As QuartzTypeLib.IPinInfo = Nothing
        Dim pi_Tee_Output As QuartzTypeLib.IPinInfo = Nothing
        Dim pi_Tee_Preview As QuartzTypeLib.IPinInfo = Nothing

        'On Local Error GoTo ErrLine

        'Instantiate the timeline
        objTimeline = New DexterLib.AMTimeline
        'Create empty node on timeline for video
        objTimeline.CreateEmptyNode(objVideoGroupObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'Derive video group object from the timeline object.
        objVideoGroup = objVideoGroupObject
        'Set the media type of the video group.
        objVideoGroup.SetMediaTypeForVB(0)
        'Append the video group to the timeline.
        objTimeline.AddGroup(objVideoGroup)

        'Create empty node on timeline for the track.
        objTimeline.CreateEmptyNode(objTrackObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
        'Obtain a composition from the video group.
        objVideoComposition = objVideoGroup
        'Inset the track into the composition.
        objVideoComposition.VTrackInsBefore(objTrackObject, -1)
        'Derive the track object.
        objTrack = objTrackObject
        'Reset time on track object.
        dTimeOnTrack = 0

        'Set sources to track object in sequence.
        For i As Integer = 0 To iSegmentCount - 1
            'Create empty node on timeline for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive source clip from the timeline object.
            objSource = objSourceObj
            'Set Start and stop times to the source clip.
            objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
            objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
            objSource.SetMediaName(FileSource(i))
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
            dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
        Next

        'Check for and facillitate audio.
        If HasAudio Then
            'Create an empty node on the timeline for the audio group.
            objTimeline.CreateEmptyNode(objAudioGroupObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
            'Derive the audio group from the timeline object.
            objAudioGroup = objAudioGroupObj
            'Set the media type of the audio group.
            objAudioGroup.SetMediaTypeForVB(1)
            'Append the group to the timeline.
            objTimeline.AddGroup(objAudioGroup)

            'Create an empty node on the timeline for the audio track.
            objTimeline.CreateEmptyNode(objTrackObject, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
            'Derive a composition from the audio group.
            objAudioComposition = objAudioGroup
            'Insert the track into the composition
            objAudioComposition.VTrackInsBefore(objTrackObject, -1)
            'Derive a track object from the timeline object.
            objTrack = objTrackObject

            'Create an empty node for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive a source object from the timeline object
            objSource = objSourceObj
            'Reset time on track object.
            dTimeOnTrack = 0
            'Set sources to track object in sequence.
            For i As Integer = 0 To iSegmentCount - 1
                'Create empty node on timeline for the source clip.
                objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                'Derive source clip from the timeline object.
                objSource = objSourceObj
                'Set Start and stop times to the source clip.
                objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
                objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                objSource.SetMediaName(FileSource(i))
                'Append source clip to the track.
                objTrack.SrcAdd(objSourceObj)
                dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
            Next
        End If

        '*
        '*  Set FilterGraph "Front End" --> SmartRecompressor..
        '*
        'Set the recompression format of the video group.
        objVideoGroup.SetRecompFormatFromSource(objSource)
        'Set the timeline to the render engine.
        objSmartRenderEngine.SetTimelineObject(objTimeline)
        'Connect-up the render engine.
        objSmartRenderEngine.ConnectFrontEnd()
        'Obtain a reference to the filter graph for the timeline.
        objSmartRenderEngine.GetFilterGraph(objFilterGraphManager)
        'Add a file writer and a mux filter to the filtergraph.
        AddFileWriterAndMux(objFilterGraphManager, FileDest)

        On Error Resume Next

        'Render output pins and prepare to smart write file.
        RenderGroupPins(objSmartRenderEngine, objTimeline)
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        'Run the graph
        objFilterGraphManager.Run()
        GraphStatus = GraphState.StateRunning
        On Error Resume Next

        Dim szOldTitle As String = Nothing
        Dim timing As New Stopwatch
        Dim ts As TimeSpan
        timing.Start()

        If Not owner Is Nothing Then szOldTitle = owner.Text

        Do
            If Not objMediaEvent Is Nothing Then
                Call objMediaEvent.WaitForCompletion(100, nReturnCode)
                If nReturnCode = 1 Then m_bProcessComplete = True : RaiseEvent FileComplete() : Exit Do
                If GraphStatus <> GraphState.StateRunning Then Exit Sub
                If Not owner Is Nothing Then
                    ts = timing.Elapsed

                    ' Format and display the TimeSpan value.
                    owner.Text = "Exporting --> Please Wait (" & String.Format("{0:00}:{1:00}:{2:00}", _
                    ts.Hours, ts.Minutes, ts.Seconds) & ")"
                End If

                Application.DoEvents()
            Else : Exit Do
            End If
        Loop

        If Not owner Is Nothing Then owner.Text = szOldTitle
        GraphStatus = GraphState.StateStopped
        Beep()

CleanUp:
        'scrap the render engine
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine.ScrapIt()
        'clean-up & dereference quartz object(s)
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaPosition Is Nothing Then objMediaPosition = Nothing
        If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing
        'clean-up & dereference dexter timeline object(s)
        If Not objTimeline Is Nothing Then objTimeline = Nothing
        If Not objSourceObj Is Nothing Then objSourceObj = Nothing
        If Not objTrackObject Is Nothing Then objTrackObject = Nothing
        If Not objAudioGroupObj Is Nothing Then objAudioGroupObj = Nothing
        If Not objVideoGroupObject Is Nothing Then objVideoGroupObject = Nothing
        'clean-up & dereference dexter timeline object(s)
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objSource Is Nothing Then objSource = Nothing
        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing
        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing
        If Not objAudioComposition Is Nothing Then objAudioComposition = Nothing
        If Not objVideoComposition Is Nothing Then objVideoComposition = Nothing
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine = Nothing

        RaiseEvent FileComplete()

        Exit Sub

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal)
                Err.Clear() : GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal) : Err.Clear() : GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal) : Err.Clear() : GoTo CleanUp
        End Select
        Exit Sub
    End Sub

    Public Sub New()
        m_objMediaDet = New DexterLib.MediaDet

    End Sub

    Protected Overrides Sub Finalize()
        If Not m_objMediaDet Is Nothing Then m_objMediaDet = Nothing
        MyBase.Finalize()
    End Sub
End Class
