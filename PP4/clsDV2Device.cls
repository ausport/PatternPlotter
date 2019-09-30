
Public Class clsDV2Device
    Implements IDisposable
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                  cDV2Tape CLASS.
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*     Stuart Morgan
    '*     www.in2sport.com.au
    '*     Copyright (C)2007
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *

    '- - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*            cDV2Tape CLASS DECLARATIONS.           *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                 ENUMERATION DECLARATIONS
    '* * *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public Enum GraphState
        StateStopped = 0
        StatePaused = 1
        StateRunning = 2
    End Enum


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   MEDIA FILTER DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private m_objFilterInfo As Object        'IFilterInfo interface represents all filters in the current graph
    Private m_objRegFilterInfo As Object ' IFilterInfo interface represents all registered filters on the system
    Private objMediaControl As QuartzTypeLib.IMediaControl  'IMediaControl interface provided by IFilgraphManager
    Private objMediaEvent As QuartzTypeLib.IMediaEvent
    Private objMediaPosition As QuartzTypeLib.IMediaPosition
    'Private GraphState As GraphState  'enum value indicating whether the video is running, paused, or stopped
    Private vidWindow As QuartzTypeLib.IVideoWindow       'VideoWindow Object
    Private objFilterGraphManager As QuartzTypeLib.FilgraphManager
    Private objMediaDet As DexterLib.MediaDet

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   FILTER PIN & CONNECT DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private objRegFilter As QuartzTypeLib.IRegFilterInfo
    Private objRegFilterInfo As QuartzTypeLib.IRegFilterInfo
    Private objFilter As QuartzTypeLib.IFilterInfo
    Private objFilter2 As QuartzTypeLib.IFilterInfo   'Debug process
    Private objFilterInfo As QuartzTypeLib.IFilterInfo
    Private objPinInfo As QuartzTypeLib.IPinInfo
    Private objConnPinInfo As QuartzTypeLib.IPinInfo

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   TIMELINE FILTER DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Dim objTimeline As DexterLib.AMTimeline
    Dim objSourceObj As DexterLib.AMTimelineObj
    Dim objTrackObject As DexterLib.AMTimelineObj
    Dim objAudioGroupObj As DexterLib.AMTimelineObj
    Dim objVideoGroupObject As DexterLib.AMTimelineObj

    Dim objSource As DexterLib.AMTimelineSrc
    'Dim objSource2() As AMTimelineSrc

    Dim objTrack As DexterLib.AMTimelineTrack
    Dim objAudioGroup As DexterLib.AMTimelineGroup
    Dim objVideoGroup As DexterLib.AMTimelineGroup
    Dim objAudioComposition As DexterLib.AMTimelineComp
    Dim objVideoComposition As DexterLib.AMTimelineComp
    Dim objSmartRenderEngine As New DexterLib.SmartRenderEngine

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   FILTER GRAPH DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    'DV Device Filter
    Dim fi_DV As QuartzTypeLib.IFilterInfo
    'DV Input Filter pin.
    Dim pi_DV_Input As QuartzTypeLib.IPinInfo
    'Smart Recompressor Filter
    Dim fi_ReComp As QuartzTypeLib.IFilterInfo
    'Output pin for Smart Recompressor --> "Front End" output.
    Dim pi_RC_Output As QuartzTypeLib.IPinInfo
    'DV Muxer Filter
    Dim fi_Mux As QuartzTypeLib.IFilterInfo
    'DV Muxer Input 1 (vid)
    Dim pi_Mux_Input1 As QuartzTypeLib.IPinInfo
    'DV Muxer Input 2 (aud)
    Dim pi_Mux_Input2 As QuartzTypeLib.IPinInfo
    'DV Muxer Output --> to Input pin on DV Device (pi_DV_Input)
    Dim pi_Mux_Output As QuartzTypeLib.IPinInfo
    'AVI Splitter
    Dim fi_AVISplitter As QuartzTypeLib.IFilterInfo
    'Splitter Audio Output Pin
    Dim pi_AVI_aud As QuartzTypeLib.IPinInfo

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   OTHER CLASS DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private m_dDuration As Double
    Private m_bProcessComplete As Boolean
    Private m_uStatus As GraphState

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*              PUBLIC INTERFACE- WIN32 API CONSTANTS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*     PUBLIC INTERFACE- WIN32 API DATA STRUCTURES
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*       PUBLIC INTERFACE- WIN32 API DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Long) As Long
    Private Declare Function SetErrorMode Lib "kernel32" (ByVal wMode As Long) As Long
    Private Declare Function ShellExecuteEx Lib "shell32" (ByVal lpExecInfo As SHELLEXECUTEINFO) As Long
    Private Declare Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperationA" (ByVal lpFileOp As SHFILEOPSTRUCT) As Long
    Private Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, ByVal lpFindFileData As WIN32_FIND_DATA) As Long
    Private Declare Function GetTempPath Lib "kernel32" Alias "GetTempPathA" (ByVal nBufferLength As Long, ByVal lpBuffer As String) As Long

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   EVENT DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Event FileComplete()
    Public Event Progress()

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Public Function GoOnline(szVideoSource As String) As Boolean:
    '*
    '* Initialises AMTimeline with Start/End points from video source for DV
    '* export to DV device.
    '*
    '*      --> szVideoSource As String = DV video source
    '*      --> lStartPoint As Double = In-point for exporting
    '*      --> lEndPoint As Double = Out-point for exporting
    '*      --> szDestinationPath As String = Path to destination file.. if nothing then export to DV device.
    '*      --> bAudio As Boolean (optional::default = true) = export audio if present.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function GoOnline(ByVal szVideoSource As String, ByVal lStartPoint As Double, ByVal lEndPoint As Double, _
    Optional ByVal szDestinationPath As String = Nothing, Optional ByVal bAudio As Boolean = True) As Boolean

        'On Error GoTo ErrLine
        m_bProcessComplete = False
        m_uStatus = GraphState.StateStopped

        '*
        '*  Establish timeline properties..
        '*

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

        'Set Start and stop times to the source clip.
        objSourceObj.SetStartStop2(0, (lEndPoint - lStartPoint))
        objSource.SetMediaTimes2(lStartPoint, lEndPoint)
        objSource.SetMediaName(szVideoSource)
        'Append source clip to the track.
        objTrack.SrcAdd(objSourceObj)

        '*
        '*  Check for and facillitate audio..
        '*
        If bAudio Then
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
            objSourceObj.SetStartStop2(0, (lEndPoint - lStartPoint))
            objSource.SetMediaTimes2(lStartPoint, lEndPoint)
            objSource.SetMediaName(szVideoSource)
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
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

        '*
        '*  Prepare to enumerate registered filters to add new items to graph..
        '*
        If Not objFilterGraphManager Is Nothing Then
            If Not objFilterGraphManager.RegFilterCollection Is Nothing Then
                m_objRegFilterInfo = objFilterGraphManager.RegFilterCollection
            End If
        End If

        '*
        '*  Add additional filters for the Back-End.. (DV Muxer & DV-Device)
        '*
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "DV Muxer" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next

        Dim boolDeviceFound As Boolean
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "Microsoft DV Camera and VCR" Then
                objRegFilter.Filter(objFilter)
                boolDeviceFound = True
                Exit For
            End If
        Next
        If Not boolDeviceFound Then
            MsgBox("Cannot detect DV Device... Please ensure device is plugged in and on VTR mode.", vbCritical, Application.ProductName)
            GoOnline = False
            Exit Function
        End If
        '*
        '*  Get Output filter from Smart Recompressor - this is the output line from "Front End"
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "SmartRecompressor" Then
                fi_ReComp = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Output" Then
                        pi_RC_Output = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get Audio Output filter from AVI Splitter - only if audio is chosen.
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "AVI Splitter" Then
                fi_AVISplitter = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 01" Then
                        pi_AVI_aud = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get the DV Muxer filter pins..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "DV Muxer" Then
                fi_Mux = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 0" Then
                        pi_Mux_Input1 = objPinInfo
                    End If
                    If objPinInfo.Name = "Output" Then
                        pi_Mux_Output = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the SmartRecompressor Output pin to the Muxer Input..
        '*
        If pi_Mux_Input1 Is Nothing Then GoTo ErrLine
        If pi_RC_Output Is Nothing Then GoTo ErrLine
        pi_RC_Output.Connect(pi_Mux_Input1)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_RC_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Connect the AVI Splitter Audio Output pin to the Muxer Input..
        '*
        '    If bAudio Then
        '        For Each objFilterInfo In objFilterGraphManager.FilterCollection
        '            If objFilterInfo.Name = "DV Muxer" Then
        '                For Each objPinInfo In objFilterInfo.Pins
        '                    If objPinInfo.Name = "Stream 1" Then
        '                        Set pi_Mux_Input2 = objPinInfo
        '                        Exit For
        '                    End If
        '                Next
        '                Exit For
        '            End If
        '        Next
        '
        '        If pi_Mux_Input2 Is Nothing Then GoTo ErrLine
        '        If pi_AVI_aud Is Nothing Then GoTo ErrLine
        '        pi_AVI_aud.Connect pi_Mux_Input2
        '        If Not objConnPinInfo Is Nothing Then Set objConnPinInfo = Nothing
        '        Set objConnPinInfo = pi_AVI_aud.ConnectedTo
        '        If objConnPinInfo Is Nothing Then GoTo ErrLine
        '    End If
        '*
        '*  Get the DV Device Input Pin..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "Microsoft DV Camera and VCR" Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "DV A/V In" Then
                        pi_DV_Input = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the DV Muxer Output to the DV Device..
        '*
        If pi_Mux_Output Is Nothing Then GoTo ErrLine
        If pi_DV_Input Is Nothing Then GoTo ErrLine
        pi_Mux_Output.Connect(pi_DV_Input)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_Mux_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Render pins & prepare graph for run..
        '*
        RenderGroupPins(objSmartRenderEngine, objTimeline)
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        '*
        '*  Finish the process --> user to initiate cDV2Tape::StartTransmit
        '*
        objFilterGraphManager.Pause()

        GoOnline = True
        Exit Function

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() 'GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() 'GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox("An unknown video connection error occured.  clsDV2Device.cls::GoOnline", vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() 'GoTo CleanUp
        End Select

        GoOnline = False

    End Function


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Public Function GoOnline2(szVideoSource As String) As Boolean:
    '*
    '* Initialises AMTimeline with multiple video sources for DV
    '* export to DV device.
    '*
    '*      --> iSegmentCount As Integer = Number of source clips for timeline
    '*      --> szVideoSource() As String = DV video source array.
    '*      --> lStartPoint() As Double = In-point for exporting array.
    '*      --> lEndPoint() As Double = Out-point for exporting array.
    '*      --> szDestinationPath As String = Path to destination file.. if nothing then export to DV device.
    '*      --> bAudio As Boolean (optional::default = true) = export audio if present.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function GoOnline2(ByVal iSegmentCount As Integer, ByVal szVideoSource() As String, ByVal lStartPoint() As Double, ByVal lEndPoint() As Double, _
    Optional ByVal szDestinationPath As String = Nothing, Optional ByVal bAudio As Boolean = True) As Boolean

        '*
        '*  Housekeeping..
        '*
        Dim i As Integer
        Dim nReturnCode As Long
        Dim dTimeOnTrack As Double
        On Error GoTo ErrLine
        m_bProcessComplete = False
        m_uStatus = GraphState.StateStopped

        '*
        '*  Establish timeline properties..
        '*
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
        For i = 0 To iSegmentCount - 1
            'Create empty node on timeline for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive source clip from the timeline object.
            objSource = objSourceObj
            'Set Start and stop times to the source clip.
            objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
            objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
            objSource.SetMediaName(szVideoSource(i))
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
            dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
        Next

        '*
        '*  Check for and facillitate audio..
        '*
        If Not bAudio Then
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
            For i = 0 To iSegmentCount - 1
                'Create empty node on timeline for the source clip.
                objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                'Derive source clip from the timeline object.
                objSource = objSourceObj
                'Set Start and stop times to the source clip.
                objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
                objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                objSource.SetMediaName(szVideoSource(i))
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

        '*
        '*  Prepare to enumerate registered filters to add new items to graph..
        '*
        If Not objFilterGraphManager Is Nothing Then
            If Not objFilterGraphManager.RegFilterCollection Is Nothing Then
                m_objRegFilterInfo = objFilterGraphManager.RegFilterCollection
            End If
        End If

        '*
        '*  Add additional filters for the Back-End.. (DV Muxer & DV-Device)
        '*
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "DV Muxer" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next

        Dim boolDeviceFound As Boolean
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "Microsoft DV Camera and VCR" Then
                objRegFilter.Filter(objFilter)
                boolDeviceFound = True
                Exit For
            End If
        Next
        If Not boolDeviceFound Then
            MsgBox("Cannot detect DV Device... Please ensure device is plugged in and on VTR mode.", vbCritical, Application.ProductName)
            GoOnline2 = False
            Exit Function
        End If
        '*
        '*  Get Output filter from Smart Recompressor - this is the output line from "Front End"
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "SmartRecompressor" Then
                fi_ReComp = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Output" Then
                        pi_RC_Output = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get Audio Output filter from AVI Splitter - only if audio is chosen.
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "AVI Splitter" Then
                fi_AVISplitter = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 01" Then
                        pi_AVI_aud = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get the DV Muxer filter pins..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "DV Muxer" Then
                fi_Mux = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 0" Then
                        pi_Mux_Input1 = objPinInfo
                    End If
                    If objPinInfo.Name = "Output" Then
                        pi_Mux_Output = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the SmartRecompressor Output pin to the Muxer Input..
        '*
        If pi_Mux_Input1 Is Nothing Then GoTo ErrLine
        If pi_RC_Output Is Nothing Then GoTo ErrLine
        pi_RC_Output.Connect(pi_Mux_Input1)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_RC_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Get the DV Device Input Pin..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "Microsoft DV Camera and VCR" Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "DV A/V In" Then
                        pi_DV_Input = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the DV Muxer Output to the DV Device..
        '*
        If pi_Mux_Output Is Nothing Then GoTo ErrLine
        If pi_DV_Input Is Nothing Then GoTo ErrLine
        pi_Mux_Output.Connect(pi_DV_Input)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_Mux_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Debug function:: show pin connections in current graph..
        '*
        On Error Resume Next
        For Each objFilter In objFilterGraphManager.FilterCollection
            Debug.Print("* * * * * * * * * * * * * * * * *")
            Debug.Print(objFilter.Name)
            For Each objPinInfo In objFilter.Pins
                If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                If Not objFilter2 Is Nothing Then objFilter2 = Nothing
                objConnPinInfo = objPinInfo.ConnectedTo
                objFilter2 = objConnPinInfo.FilterInfo
                If Not objFilter2 Is Nothing Then
                    Debug.Print(objPinInfo.Name & "  -->  " & objFilter2.Name & " (" & objConnPinInfo.Name & ")")
                Else
                    Debug.Print(objPinInfo.Name & "  -->  Empty")
                End If
            Next
            Debug.Print("* * * * * * * * * * * * * * * * *")
        Next
        On Error GoTo ErrLine
        '*
        '*  Render pins & prepare graph for run..
        '*
        RenderGroupPins(objSmartRenderEngine, objTimeline)
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        '*
        '*  Finish the process --> user to initiate cDV2Tape::StartTransmit
        '*
        objFilterGraphManager.Pause()

        GoOnline2 = True
        Exit Function

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() 'GoTo CleanUp
                '                Case 287 'Application-defined or object-defined error
                '                   Err.Clear: Resume Next
                '                Case -2147024864 'The process cannot access the file because it is being used by another process.
                '                   Call MsgBox(Err.Description, vbExclamation + vbApplicationModal): Err.Clear: 'GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox("An error has occured connecting to the DV device.  Please make sure that your device is connected, turned on, and is in VTR mode.", vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() 'GoTo CleanUp
        End Select

        GoOnline2 = False

    End Function


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Public Function GoOnline3(...) As Boolean:
    '*
    '* Initialises AMTimeline with multiple video sources for DV
    '* export to DV device.
    '*
    '*      --> iSegmentCount As Integer = Number of source clips for timeline
    '*      --> szVideoSource() As String = DV video source array.
    '*      --> lStartPoint() As Double = In-point for exporting array.
    '*      --> lEndPoint() As Double = Out-point for exporting array.
    '*      --> szDestinationPath As String = Path to destination file.. if nothing then export to DV device.
    '*      --> bAudio As Boolean (optional::default = true) = export audio if present.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function GoOnline3(ByVal iSegmentCount As Integer, ByVal szVideoSource() As String, ByVal lStartPoint() As Double, ByVal lEndPoint() As Double, _
Optional ByVal szDestinationPath As String = Nothing, Optional ByVal bAudio As Boolean = True, Optional ByVal RepeatSlow As Boolean = False) As Boolean

        '*
        '*  Housekeeping..
        '*
        Dim i As Integer
        Dim nReturnCode As Long
        Dim dTimeOnTrack As Double
        On Error GoTo ErrLine
        m_bProcessComplete = False
        m_uStatus = GraphState.StateStopped

        '*
        '*  Establish timeline properties..
        '*
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

        'Set time correction factor - NB: this is only a temporary work-around.
        Dim tCorrect As Double = 1
        If RepeatSlow Then tCorrect = 1.4

        Dim SaveSource As DexterLib.IAMTimelineSrc = Nothing
        'Set sources to track object in sequence.
        For i = 0 To iSegmentCount - 1

            'Create empty node on timeline for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive source clip from the timeline object.
            objSource = objSourceObj
            'Set Start and stop times to the source clip.
            objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + ((lEndPoint(i) - lStartPoint(i)) * 1))
            objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
            objSource.SetMediaName(szVideoSource(i))
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
            dTimeOnTrack += (lEndPoint(i) - lStartPoint(i)) * 1

            If i = 0 Then SaveSource = objSource

            If RepeatSlow Then
                'Create empty node on timeline for the source clip.
                objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                'Derive source clip from the timeline object.
                objSource = objSourceObj
                'Set Start and stop times to the source clip.
                objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (4 * (lEndPoint(i) - lStartPoint(i))))
                objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                objSource.SetMediaName(szVideoSource(i))
                'Append source clip to the track.
                objTrack.SrcAdd(objSourceObj)
                dTimeOnTrack += (4 * (lEndPoint(i) - lStartPoint(i)))

            End If
        Next

        objVideoGroup.SetRecompFormatFromSource(objSource)
        If RepeatSlow Then
            'Set the recompression format of the video group.
            'Get media type
            Dim srcMediaSource As New DexterLib.MediaDet
            srcMediaSource.Filename = szVideoSource(0)
            Dim srcMediaType As DexterLib._AMMediaType = srcMediaSource.StreamMediaType
            objVideoGroup.SetMediaType(srcMediaType)
        Else
        End If

        '*
        '*  Check for and facillitate audio..
        '*
        If Not bAudio And Not RepeatSlow Then
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
            For i = 0 To iSegmentCount - 1
                'Create empty node on timeline for the source clip.
                objTimeline.CreateEmptyNode(objSourceObj, DexterLib.TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                'Derive source clip from the timeline object.
                objSource = objSourceObj
                'Set Start and stop times to the source clip.
                objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
                objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                objSource.SetMediaName(szVideoSource(i))
                'Append source clip to the track.
                objTrack.SrcAdd(objSourceObj)
                dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
            Next
        End If

        '*
        '*  Set FilterGraph "Front End" --> SmartRecompressor..
        '*
        'Set the recompression format of the video group.
        'objVideoGroup.SetRecompFormatFromSource(objSource)
        'Set the timeline to the render engine.
        objSmartRenderEngine.SetTimelineObject(objTimeline)
        'Connect-up the render engine.
        objSmartRenderEngine.ConnectFrontEnd()
        'Obtain a reference to the filter graph for the timeline.
        objSmartRenderEngine.GetFilterGraph(objFilterGraphManager)

        '*
        '*  Prepare to enumerate registered filters to add new items to graph..
        '*
        If Not objFilterGraphManager Is Nothing Then
            If Not objFilterGraphManager.RegFilterCollection Is Nothing Then
                m_objRegFilterInfo = objFilterGraphManager.RegFilterCollection
            End If
        End If

        '*
        '*  Add additional filters for the Back-End.. (DV Muxer & DV-Device)
        '*
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "DV Muxer" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next

        Dim boolDeviceFound As Boolean
        For Each objRegFilter In m_objRegFilterInfo
            If objRegFilter.Name = "Microsoft DV Camera and VCR" Then
                objRegFilter.Filter(objFilter)
                boolDeviceFound = True
                Exit For
            End If
        Next
        If Not boolDeviceFound Then
            MsgBox("Cannot detect DV Device... Please ensure device is plugged in and on VTR mode.", vbCritical, Application.ProductName)
            GoOnline3 = False
            Exit Function
        End If
        '*
        '*  Get Output filter from Smart Recompressor - this is the output line from "Front End"
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "SmartRecompressor" Then
                fi_ReComp = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Output" Then
                        pi_RC_Output = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get Audio Output filter from AVI Splitter - only if audio is chosen.
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            Debug.Print(objFilterInfo.Name)
            If objFilterInfo.Name = "AVI Splitter" Then
                fi_AVISplitter = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 01" Then
                        pi_AVI_aud = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Get the DV Muxer filter pins..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "DV Muxer" Then
                fi_Mux = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Stream 0" Then
                        pi_Mux_Input1 = objPinInfo
                    End If
                    If objPinInfo.Name = "Output" Then
                        pi_Mux_Output = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the SmartRecompressor Output pin to the Muxer Input..
        '*
        If pi_Mux_Input1 Is Nothing Then GoTo ErrLine
        If pi_RC_Output Is Nothing Then GoTo ErrLine
        pi_RC_Output.Connect(pi_Mux_Input1)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_RC_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Get the DV Device Input Pin..
        '*
        For Each objFilterInfo In objFilterGraphManager.FilterCollection
            If objFilterInfo.Name = "Microsoft DV Camera and VCR" Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "DV A/V In" Then
                        pi_DV_Input = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        '*
        '*  Connect the DV Muxer Output to the DV Device..
        '*
        If pi_Mux_Output Is Nothing Then GoTo ErrLine
        If pi_DV_Input Is Nothing Then GoTo ErrLine
        pi_Mux_Output.Connect(pi_DV_Input)
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        objConnPinInfo = pi_Mux_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        '*
        '*  Debug function:: show pin connections in current graph..
        '*
        On Error Resume Next
        For Each objFilter In objFilterGraphManager.FilterCollection
            Debug.Print("* * * * * * * * * * * * * * * * *")
            Debug.Print(objFilter.Name)
            For Each objPinInfo In objFilter.Pins
                If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                If Not objFilter2 Is Nothing Then objFilter2 = Nothing
                objConnPinInfo = objPinInfo.ConnectedTo
                objFilter2 = objConnPinInfo.FilterInfo
                If Not objFilter2 Is Nothing Then
                    Debug.Print(objPinInfo.Name & "  -->  " & objFilter2.Name & " (" & objConnPinInfo.Name & ")")
                Else
                    Debug.Print(objPinInfo.Name & "  -->  Empty")
                End If
            Next
            Debug.Print("* * * * * * * * * * * * * * * * *")
        Next
        On Error GoTo ErrLine
        '*
        '*  Render pins & prepare graph for run..
        '*
        RenderGroupPins(objSmartRenderEngine, objTimeline)
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        '*
        '*  Finish the process --> user to initiate cDV2Tape::StartTransmit
        '*
        objFilterGraphManager.Pause()
        GoOnline3 = True
        Exit Function

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() 'GoTo CleanUp
                '                Case 287 'Application-defined or object-defined error
                '                   Err.Clear: Resume Next
                '                Case -2147024864 'The process cannot access the file because it is being used by another process.
                '                   Call MsgBox(Err.Description, vbExclamation + vbApplicationModal): Err.Clear: 'GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox("An error has occured connecting to the DV device.  Please make sure that your device is connected, turned on, and is in VTR mode.", vbExclamation + vbApplicationModal) : Err.Clear() 'GoTo CleanUp
        End Select

        GoOnline3 = False

    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Private Sub RenderGroupPins(objRenderEngine As RenderEngine, objTimeline As AMTimeline):
    '*
    '* Renders the Pins out for the given timeline using the given render engine.
    '*
    '*      --> objRenderEngine As RenderEngine
    '*      --> objTimeline As AMTimeline
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Private Function GetPinInfo(objFilterGraphManager As FilgraphManager, objPin As IPin) As IPinInfo:
    '*
    '* Returns an IPinInfo interface given a filtergraph manager and IPin object.
    '*  The derived IPinInfo interface can be utilized for gaining information on the elected pin.
    '*
    '*      --> objFilterGraphManager As FilgraphManager
    '*      --> objPin As IPin
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private Function GetPinInfo(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager, ByVal objPin As DexterLib.IPin) As QuartzTypeLib.IPinInfo
        Dim objPin2 As DexterLib.IPin = Nothing
        Dim objPinInfo As QuartzTypeLib.IPinInfo = Nothing
        Dim objFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objPinCollection As Object = Nothing
        Dim objlFilterCollection As Object
        On Error GoTo ErrLine

        'derive a filter collection from the filtergraph manager
        objlFilterCollection = objFilterGraphManager.FilterCollection

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
        Dim objRegisteredFilters As Object
        Dim objAVIMuxFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilterInfo As QuartzTypeLib.IRegFilterInfo = Nothing
        Dim objFileSinkFilterVB As DshowForVBLib.IFileSinkFilterForVB
        On Error GoTo ErrLine

        'derive a collection of registered filters from the filtergraph manager
        objRegisteredFilters = objFilterGraphManager.RegFilterCollection

        'enumerate through the registered filters
        For Each objRegFilterInfo In objRegisteredFilters
            If Trim(LCase(objRegFilterInfo.Name)) = "file writer" Then
                objRegFilterInfo.Filter(objFilterInfo)
            ElseIf Trim(LCase(objRegFilterInfo.Name)) = "avi mux" Then
                objRegFilterInfo.Filter(objAVIMuxFilterInfo)
            End If
        Next

        'derive the file sink filter tailored for vb
        objFileSinkFilterVB = objFilterInfo.Filter
        'assign the filename to the sink filter
        Call objFileSinkFilterVB.SetFileName(bstrFileName, Nothing)

        'clean-up & dereference
        If Not objFilterInfo Is Nothing Then objFilterInfo = Nothing
        If Not objRegFilterInfo Is Nothing Then objRegFilterInfo = Nothing
        If Not objFileSinkFilterVB Is Nothing Then objFileSinkFilterVB = Nothing
        If Not objAVIMuxFilterInfo Is Nothing Then objAVIMuxFilterInfo = Nothing
        If Not objRegisteredFilters Is Nothing Then objRegisteredFilters = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub



    Public Sub StartTransmit()
        ' Dim nReturnCode As Long
        objFilterGraphManager.Run()
        m_uStatus = GraphState.StateRunning
        'On Error Resume Next
        'Do
        '    If Not objMediaEvent Is Nothing Then
        '        Call objMediaEvent.WaitForCompletion(100, nReturnCode)
        '        If nReturnCode = 1 Then m_bProcessComplete = True : RaiseEvent FileComplete() : Exit Do
        '        If m_uStatus <> GraphState.StateRunning Then Exit Sub
        '        Application.DoEvents()
        '    Else : Exit Do
        '    End If
        'Loop


    End Sub

    Public Sub StopTransmit()
        objFilterGraphManager.Stop()

        m_bProcessComplete = False
        m_uStatus = GraphState.StateStopped

    End Sub

    Public Sub PauseTransmit()

        objFilterGraphManager.Pause()

        m_bProcessComplete = False
        m_uStatus = GraphState.StatePaused

    End Sub







    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*
    '*            cDV2Tape CLASS PROCEDURES.
    '*
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public ReadOnly Property GraphStatus() As GraphState
        Get
            Return m_uStatus
        End Get
    End Property

    Public ReadOnly Property ProcessComplete() As Boolean
        Get
            Return m_bProcessComplete
        End Get
    End Property

    Public ReadOnly Property CurrentPosition() As Double
        Get
            Try
                Return objMediaPosition.CurrentPosition
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property


    Public ReadOnly Property MediaDuration() As Double
        Get
            Return objMediaPosition.Duration
        End Get
    End Property

    Protected Overrides Sub Finalize()
        Dispose()
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        On Error Resume Next
        'clean-up & dereference
        objSmartRenderEngine.ScrapIt()
        objVideoGroupObject.RemoveAll()
        objAudioGroupObj.RemoveAll()
        objTrackObject.RemoveAll()
        objSourceObj.RemoveAll()
        objTimeline.ClearAllGroups()

        'Disconnect DV filters
        If Not pi_DV_Input Is Nothing Then
            pi_DV_Input.Disconnect()
            pi_DV_Input = Nothing
        End If
        If Not fi_DV Is Nothing Then fi_DV = Nothing

        'Disconnect recompressors
        If Not pi_RC_Output Is Nothing Then
            pi_RC_Output.Disconnect()
            pi_RC_Output = Nothing
        End If
        If Not fi_ReComp Is Nothing Then fi_ReComp = Nothing

        'Disconnect Muxer
        If Not pi_Mux_Input1 Is Nothing Then
            pi_Mux_Input1.Disconnect()
            pi_Mux_Input1 = Nothing
        End If
        If Not pi_Mux_Input2 Is Nothing Then
            pi_Mux_Input2.Disconnect()
            pi_Mux_Input2 = Nothing
        End If
        If Not pi_Mux_Output Is Nothing Then
            pi_Mux_Output.Disconnect()
            pi_Mux_Output = Nothing
        End If
        If Not fi_Mux Is Nothing Then fi_Mux = Nothing

        'Disconnect AVI Splitter
        If Not pi_AVI_aud Is Nothing Then
            pi_AVI_aud.Disconnect()
            pi_AVI_aud = Nothing
        End If
        If Not fi_AVISplitter Is Nothing Then fi_AVISplitter = Nothing

        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing

        If Not m_objFilterInfo Is Nothing Then m_objFilterInfo = Nothing
        If Not m_objRegFilterInfo Is Nothing Then m_objRegFilterInfo = Nothing
        If Not objMediaControl Is Nothing Then objMediaControl = Nothing
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaPosition Is Nothing Then objMediaPosition = Nothing
        If Not vidWindow Is Nothing Then vidWindow = Nothing
        If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing
        If Not objMediaDet Is Nothing Then objMediaDet = Nothing


        If Not objRegFilter Is Nothing Then objRegFilter = Nothing
        If Not objRegFilterInfo Is Nothing Then objRegFilterInfo = Nothing
        If Not objFilter Is Nothing Then objFilter = Nothing
        If Not objFilter2 Is Nothing Then objFilter2 = Nothing
        If Not objFilterInfo Is Nothing Then objFilterInfo = Nothing
        If Not objPinInfo Is Nothing Then objPinInfo = Nothing
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing

        If Not objTimeline Is Nothing Then objTimeline = Nothing
        If Not objSourceObj Is Nothing Then objSourceObj = Nothing
        If Not objTrackObject Is Nothing Then objTrackObject = Nothing
        If Not objAudioGroupObj Is Nothing Then objAudioGroupObj = Nothing
        If Not objVideoGroupObject Is Nothing Then objVideoGroupObject = Nothing

        If Not objSource Is Nothing Then objSource = Nothing
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing
        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing
        If Not objAudioComposition Is Nothing Then objAudioComposition = Nothing
        If Not objVideoComposition Is Nothing Then objVideoComposition = Nothing
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine = Nothing
    End Sub

    Public Sub New()

    End Sub

    Private Sub clsDV2Device_FileComplete() Handles Me.FileComplete
        m_bProcessComplete = True
    End Sub

    Private Sub clsDV2Device_Progress() Handles Me.Progress

    End Sub
End Class
