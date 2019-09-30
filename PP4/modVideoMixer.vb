Imports DexterLib
Imports QuartzTypeLib

Public Module modVideoMixer

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                 ENUMERATION DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Enum GraphState
        StateStopped = 0
        StatePaused = 1
        StateRunning = 2
    End Enum

    Public Enum DEXExportFormatEnum
        DEXExportXTL = 0
        DEXExportGRF = 1
    End Enum

    'supported import formats
    Public Enum DEXImportFormatEnum
        DEXImportXTL = 0
    End Enum

    'supported media groups
    Public Enum DEXMediaTypeEnum
        DEXMediaTypeAudio = 1
        DEXMediaTypeVideo = 0
    End Enum

    Public Const DEXT_Barn As String = "{C3BDF740-0B58-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_Blinds As String = "{00C429C0-0BA9-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_BurnFilm As String = "{107045D1-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_CenterCurls As String = "{AA0D4D0C-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_ColorFade As String = "{2A54C908-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Compositor As String = "{9A43A844-0831-11D1-817F-0000F87557DB}"
    Public Const DEXT_Curls As String = "{AA0D4D0E-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_Curtains As String = "{AA0D4D12-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_Fade As String = "{16B280C5-EE70-11D1-9066-00C04FD9189D}"
    Public Const DEXT_FadeWhite As String = "{107045CC-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_FlowMotion As String = "{2A54C90B-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_GlassBlock As String = "{2A54C913-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Grid As String = "{2A54C911-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Inset As String = "{93073C40-0BA5-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_Iris As String = "{3F69F351-0379-11D2-A484-00C04F8EFB69}"
    Public Const DEXT_Jaws As String = "{2A54C904-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Lens As String = "{107045CA-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_LightWipe As String = "{107045C8-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Liquid As String = "{AA0D4D0A-06A3-11D2-8F98-00C04FB92EB7}"

    Public Const DEXT_PageCurl As String = "{AA0D4D08-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_PeelABCD As String = "{AA0D4D10-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_Pixelate As String = "{4CCEA634-FBE0-11d1-906A-00C04FD9189D}"
    Public Const DEXT_RadialWipe As String = "{424B71AF-0695-11D2-A484-00C04F8EFB69}"
    Public Const DEXT_Ripple As String = "{AA0D4D03-06A3-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_RollDown As String = "{9C61F46E-0530-11D2-8F98-00C04FB92EB7}"
    Public Const DEXT_Slide As String = "{810E402F-056B-11D2-A484-00C04F8EFB69}"
    Public Const DEXT_SMPTE_Wipe As String = "{dE75D012-7A65-11D2-8CEA-00A0C9441E20}"
    Public Const DEXT_Spiral As String = "{ACA97E00-0C7D-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_Stretch As String = "{7658F2A2-0A83-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_Threshold As String = "{2A54C915-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Twister As String = "{107045CF-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Vacuum As String = "{2A54C90D-07AA-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Water As String = "{107045C5-06E0-11D2-8D6D-00C04F8EF8E0}"
    Public Const DEXT_Wheel As String = "{5AE1DAE0-1461-11d2-A484-00C04F8EFB69}"
    Public Const DEXT_Wipe As String = "{AF279B30-86EB-11D1-81BF-0000F87557DB}"
    Public Const DEXT_WormHole As String = "{0E6AE022-0C83-11D2-8CD4-00104BC75D9A}"
    Public Const DEXT_Zigzag As String = "{E6E73D20-0C8A-11d2-A484-00C04F8EFB69}"


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   EVENT DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                   OTHER CLASS DECLARATIONS
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private m_dDuration As Double
    Private m_bProcessComplete As Boolean
    Public GraphStatus As GraphState
    Private m_PercentComplete As Single

    Private m_objRenderEngine As New RenderEngine
    Private m_objTimeline As AMTimeline
    Private m_objMediaEvent As IMediaEvent


    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- WIN32 API CONSTANTS
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

    Public Const SLIDESHOWVB_CLIPLENGTH As Double = 4.0#  'how long each clip lasts on the timeline
    Public Const SLIDESHOWVB_VIDEOTYPE As String = "{73646976-0000-0010-8000-00AA00389B71}"
    Public Const SLIDESHOWVB_AUDIOTYPE As String = "{73647561-0000-0010-8000-00AA00389B71}"


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '* PUBLIC INTERFACE- WIN32 API DATA STRUCTURES
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

    ' ******************************************************************************************************************************
    ' * procedure name: ClearTimeline
    ' * procedure description: purges the given timeline of all groups
    ' *                                      NOTE: YOU MUST CALL THIS ON ANY AMTIMELINES YOU HAVE BEFORE RELEASING
    ' *                                      THEM (e.g. BEFORE YOUR APP SHUTS DOWN) OR SO AS TO FREE MEMORY RESOURCES
    ' ******************************************************************************************************************************
    Public Sub ClearTimeline(ByVal objTimeline As AMTimeline)
        On Error GoTo ErrLine

        If Not objTimeline Is Nothing Then
            Call objTimeline.ClearAllGroups()
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub



    ' ******************************************************************************************************************************
    ' * procedure name: CreateTimeline
    ' * procedure description:  creates a AMTimeline object
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateTimeline() As AMTimeline
        On Error GoTo ErrLine
        'instantiate return value direct
        CreateTimeline = New AMTimeline
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateGroup
    ' * procedure description:  creates a group object given the passed properties (group name & mediatype)  on the given timeline
    ' *                                       groups can only be inserted into a timeline; so you could use this function with 'InsertGroup' typically
    ' ******************************************************************************************************************************
    Public Function CreateGroup(ByVal objTimeline As AMTimeline, ByVal bstrGroupName As String, ByVal MediaType As DEXMediaTypeEnum, _
        Optional ByVal OutputFPS As Double = 25, Optional ByVal PreviewMode As Long = 1, Optional ByVal OutputBuffer As Long = 32) As AMTimelineGroup

        Dim objGroup As AMTimelineGroup
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'derive the group interface
        objGroup = objTimelineObject
        'set the name of the group
        Call objGroup.SetGroupName(bstrGroupName)
        'set the media type for the group
        Call objGroup.SetMediaTypeForVB(MediaType)
        'set the output buffer for the group
        Call objGroup.SetOutputBuffering(OutputBuffer)
        'set the preview mode for the group
        Call objGroup.SetPreviewMode(PreviewMode)
        'set the output fps for the group
        Call objGroup.SetOutputFPS(OutputFPS)
        'return the group to the client
        CreateGroup = objGroup
        'clean-up & dereference
        If Not objGroup Is Nothing Then objGroup = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateComposite
    ' * procedure description: Creates a Composite object on the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateComposite(ByVal objTimeline As AMTimeline) As AMTimelineComp
        Dim objComp As AMTimelineComp
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_COMPOSITE)
        'derive the composite interface
        objComp = objTimelineObject
        'return the group to the client
        CreateComposite = objComp
        'clean-up & dereference
        If Not objComp Is Nothing Then objComp = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateTrack
    ' * procedure description: Create a track object on the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateTrack(ByVal objTimeline As AMTimeline) As AMTimelineTrack
        Dim objTrack As AMTimelineTrack
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
        'derive the track interface
        objTrack = objTimelineObject
        'return the track to the client
        CreateTrack = objTrack
        'clean-up & dereference
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateEffect
    ' * procedure description: creates an effect object on the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateEffect(ByVal objTimeline As AMTimeline) As AMTimelineEffect
        Dim objEffect As AMTimelineEffect
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_EFFECT)
        'derive the effect interface
        objEffect = objTimelineObject
        'return the group to the client
        CreateEffect = objEffect
        'clean-up & dereference
        If Not objEffect Is Nothing Then objEffect = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateTransition
    ' * procedure description: creates a transition object on the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateTransition(ByVal objTimeline As AMTimeline) As AMTimelineTrans
        Dim objTrans As AMTimelineTrans
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRANSITION)
        'derive the effect interface
        objTrans = objTimelineObject
        'return the group to the client
        CreateTransition = objTrans
        'clean-up & dereference
        If Not objTrans Is Nothing Then objTrans = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: CreateSource
    ' * procedure description: creates a clip/source object on the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function CreateSource(ByVal objTimeline As AMTimeline) As AMTimelineSrc
        Dim objSrc As AMTimelineSrc
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'create an empty node on the timeline
        objTimeline.CreateEmptyNode(objTimelineObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
        'derive the source interface
        objSrc = objTimelineObject
        'return the source to the client
        CreateSource = objSrc
        'clean-up & dereference
        If Not objSrc Is Nothing Then objSrc = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: InsertGroup
    ' * procedure description: appends a group to a timeline object; you can only append groups to a timeline
    ' *
    ' ******************************************************************************************************************************
    Public Sub InsertGroup(ByVal objDestTimeline As AMTimeline, ByVal objSourceGroup As AMTimelineGroup)
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        If Not objSourceGroup Is Nothing Then
            If Not objDestTimeline Is Nothing Then
                'query for the Timelineobj interface
                objTimelineObject = objSourceGroup
                'append the source group to the destination timeline
                objDestTimeline.AddGroup(objTimelineObject)
                'clean-up & dereference
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: InsertComposite
    ' * procedure description: Inserts Composite into a group or into another composite,
    ' *                                      The second argument, objInsetDestination evaluates to either a group or a composite object
    ' ******************************************************************************************************************************
    Public Sub InsertComposite(ByVal objSourceComposite As AMTimelineComp, ByVal objInsetDestination As AMTimelineObj, Optional ByVal Priority As Long = -1)
        Dim objComp As AMTimelineComp
        Dim objTimelineObject As AMTimelineObj
        On Error GoTo ErrLine

        If Not objSourceComposite Is Nothing Then
            If Not objInsetDestination Is Nothing Then
                'query for the composite interface
                objComp = objInsetDestination
                'query for the timelineobj object
                objTimelineObject = objSourceComposite
                'insert the comp into the group; or comp & set the priority
                Call objComp.VTrackInsBefore(objTimelineObject, Priority)
                'clean-up & dereference
                If Not objComp Is Nothing Then objComp = Nothing
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: InsertTrack
    ' * procedure description: Inserts a track into a group or a composite,
    ' *                                      The second argument, objInsetDestination evaluates to either a group or a composite
    ' ******************************************************************************************************************************
    Public Sub InsertTrack(ByVal objTrack As AMTimelineTrack, ByVal objInsetDestination As AMTimelineObj, Optional ByVal Priority As Long = -1)
        Dim objComp As AMTimelineComp
        Dim objTimelineObject As AMTimelineObj
        On Error GoTo ErrLine

        If Not objTrack Is Nothing Then
            If Not objInsetDestination Is Nothing Then
                'query for the composite interface
                objComp = objInsetDestination
                'query for the timelineobj object
                objTimelineObject = objTrack
                'insert the comp into the group; or comp & set the priority
                Call objComp.VTrackInsBefore(objTimelineObject, Priority)
                'clean-up & dereference
                If Not objComp Is Nothing Then objComp = Nothing
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: InsertEffect
    ' * procedure description: appends an effect to a timeline object
    ' *
    ' ******************************************************************************************************************************
    Public Sub InsertEffect(ByVal objSourceEffect As AMTimelineEffect, ByVal objInsetDestination As AMTimelineObj, ByVal bstrEffectCLSID As String, ByVal dblTStart As Double, ByVal dblTStop As Double, Optional ByVal Priority As Long = -1)
        Dim objTimelineObject As AMTimelineObj
        Dim objTimelineEffectable As IAMTimelineEffectable
        On Error GoTo ErrLine

        If Not objSourceEffect Is Nothing Then
            If Not objInsetDestination Is Nothing Then
                'query for the timelineobj object
                objTimelineObject = objSourceEffect
                Call objTimelineObject.SetSubObjectGUIDB(bstrEffectCLSID)
                Call objTimelineObject.SetStartStop2(dblTStart, dblTStop)
                'insert the effect into the destination
                objTimelineEffectable = objInsetDestination
                Call objTimelineEffectable.EffectInsBefore(objTimelineObject, Priority)
                'clean-up & dereference
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
                If Not objTimelineEffectable Is Nothing Then objTimelineEffectable = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: InsertSource
    ' * procedure description: inserts a source clip to a timeline object; you can only append source to a track
    ' *
    ' ******************************************************************************************************************************
    Public Sub InsertSource(ByVal objDestTrack As AMTimelineTrack, ByVal objSourceClip As AMTimelineSrc, ByVal bstrMediaName As String, ByVal dblTStart As Double, ByVal dblTStop As Double, ByVal dblMStart As Double, ByVal dblMStop As Double)
        Dim objTimelineObject As AMTimelineObj
        On Error GoTo ErrLine

        If Not objDestTrack Is Nothing Then
            If Not objSourceClip Is Nothing Then
                'set the media name
                Call objSourceClip.SetMediaName(bstrMediaName)
                'query for the Timelineobj interface
                objTimelineObject = objSourceClip
                'set start/stop times
                Call objTimelineObject.SetStartStop2(dblTStart, dblTStop)
                If dblMStart >= 0 And dblMStop <> 0 Then
                    'set the media times
                    Call objSourceClip.SetMediaTimes2(dblMStart, dblMStop)
                End If
                'append the source clip to the destination track
                objDestTrack.SrcAdd(objTimelineObject)
                'clean-up & dereference
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: InsertTransition
    ' * procedure description: appends a transition to a timeline object
    ' *
    ' ******************************************************************************************************************************
    Public Sub InsertTransition(ByVal objSourceTransition As AMTimelineTrans, ByVal objInsetDestination As AMTimelineObj, ByVal bstrEffectCLSID As String, ByVal dblTStart As Double, ByVal dblTStop As Double, Optional ByVal Swap As Boolean = False)
        Dim objTimelineObject As AMTimelineObj
        Dim objTimelineTransable As IAMTimelineTransable
        On Error GoTo ErrLine

        If Not objSourceTransition Is Nothing Then
            If Not objInsetDestination Is Nothing Then
                'query for the timelineobj object
                objTimelineObject = objSourceTransition
                Call objTimelineObject.SetSubObjectGUIDB(bstrEffectCLSID)
                Call objTimelineObject.SetStartStop2(dblTStart, dblTStop)
                'insert the transition into the destination
                objTimelineTransable = objInsetDestination
                Call objTimelineTransable.TransAdd(objTimelineObject)

                'clean-up & dereference
                If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
                If Not objTimelineTransable Is Nothing Then objTimelineTransable = Nothing
            End If
        End If
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: GetGroupCount
    ' * procedure description: returns the number of groups encapsulated within the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function GetGroupCount(ByVal objTimeline As AMTimeline) As Long
        Dim nCount As Long
        On Error GoTo ErrLine

        'obtain the number of groups
        Call objTimeline.GetGroupCount(nCount)
        'return the group count
        GetGroupCount = nCount
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: GroupFromTimeline
    ' * procedure description:  Returns a group object given the passed arguments (timeline & group)
    ' *                                       Groups can only be inserted into a timeline; use this function with 'InsertGroup'
    ' ******************************************************************************************************************************
    Public Function GetGroupFromTimeline(ByVal objTimeline As AMTimeline, Optional ByVal Group As Long = 0) As AMTimelineGroup
        Dim objGroup As AMTimelineGroup
        Dim objTimelineObject As AMTimelineObj = Nothing
        On Error GoTo ErrLine

        'obtain a Timeline Object from the timeline
        Call objTimeline.GetGroup(objTimelineObject, Group)
        'derive the group interface from the timeline object
        objGroup = objTimelineObject
        'returnt the reference to the client
        GetGroupFromTimeline = objGroup
        'clean-up & dereference
        If Not objGroup Is Nothing Then objGroup = Nothing
        If Not objTimelineObject Is Nothing Then objTimelineObject = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: EngineFromTimeline
    ' * procedure description: renders the timeline for the client
    ' *
    ' ******************************************************************************************************************************
    Public Function GetRenderEngineFromTimeline(ByVal objTimeline As AMTimeline) As RenderEngine
        Dim objRenderEngine As RenderEngine
        On Error GoTo ErrLine

        'instantiate new render engine
        objRenderEngine = New RenderEngine

        'connect everything up..
        Call objRenderEngine.SetTimelineObject(objTimeline)
        objRenderEngine.ConnectFrontEnd()
        objRenderEngine.RenderOutputPins()

        'return the render engine to the client
        GetRenderEngineFromTimeline = objRenderEngine

        'dereference & clean-up
        If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
        Exit Function

ErrLine:

        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: GraphFromTimeline
    ' * procedure description: returns a graph from the given timeline
    ' *
    ' ******************************************************************************************************************************
    Public Function GetGraphFromTimeline(ByVal objTimeline As AMTimeline) As IGraphBuilder
        Dim objGraphBuilder As IGraphBuilder = Nothing
        Dim objRenderEngine As RenderEngine
        On Error GoTo ErrLine
        GetGraphFromTimeline = Nothing

        'instantiate new render engine
        objRenderEngine = New RenderEngine

        'connect everything up..
        Call objRenderEngine.SetTimelineObject(objTimeline)
        objRenderEngine.ConnectFrontEnd()
        objRenderEngine.RenderOutputPins()

        'return the graph builder to the client
        Call objRenderEngine.GetFilterGraph(objGraphBuilder)
        If Not objGraphBuilder Is Nothing Then GetGraphFromTimeline = objGraphBuilder

        'dereference & clean-up
        If Not objGraphBuilder Is Nothing Then objGraphBuilder = Nothing
        If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: HasGroups
    ' * procedure description: returns a boolean indicating wether or not the specified timeline has any any groups inserted
    ' *
    ' ******************************************************************************************************************************
    Public Function HasGroups(ByVal objTimeline As AMTimeline) As Boolean
        Dim nCount As Long
        On Error GoTo ErrLine

        'obtain the number of groups
        Call objTimeline.GetGroupCount(nCount)
        'return the group count
        If nCount > 0 Then HasGroups = True
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: HasStreams
    ' * procedure description:  Returns True if the given media file contains any valid streams at all
    ' *
    ' ******************************************************************************************************************************
    Public Function HasStreams(ByVal bstrFileName As String) As Boolean
        Dim nStreams As Long
        Dim objMediaDet As MediaDet = Nothing
        On Error GoTo ErrLine

        'verify argument(s)
        If bstrFileName <> vbNullString Then
            'instantiate mediadet
            objMediaDet = New MediaDet
            'assign the filename to the MediaDet
            objMediaDet.Filename = bstrFileName
            'obtain the number of streams
            nStreams = objMediaDet.OutputStreams
            'verify there is at least one valid media stream in the assigned file
            If nStreams > 0 Then HasStreams = True
        End If

        'clean-up & dereference
        If Not objMediaDet Is Nothing Then objMediaDet = Nothing
        Exit Function

ErrLine:

        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: HasVideoStream
    ' * procedure description:  Returns True if the given media file contains a valid video stream
    ' *
    ' ******************************************************************************************************************************
    Public Function HasVideoStream(ByVal bstrFileName As String) As Boolean
        Dim nCount As Long
        Dim nStreams As Long
        Dim objMediaDet As MediaDet = Nothing
        Dim bstrMediaCLSID As String
        On Error GoTo ErrLine

        'verify argument(s)
        If bstrFileName <> vbNullString Then
            'instantiate mediadet
            objMediaDet = New MediaDet
            'assign the filename to the MediaDet
            objMediaDet.Filename = bstrFileName
            'obtain the number of streams
            nStreams = objMediaDet.OutputStreams
            'verify there is at least one valid media stream in the assigned file
            If nStreams > 0 Then
                For nCount = 0 To objMediaDet.OutputStreams - 1
                    objMediaDet.CurrentStream = nCount
                    bstrMediaCLSID = objMediaDet.StreamTypeB
                    If bstrMediaCLSID = SLIDESHOWVB_VIDEOTYPE Then
                        HasVideoStream = True : Exit For
                    End If
                Next
            End If
        End If

        'clean-up & dereference
        If Not objMediaDet Is Nothing Then objMediaDet = Nothing
        Exit Function

ErrLine:

        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: HasAudioStream
    ' * procedure description:  Returns True if the given media file contains a valid audio stream
    ' *
    ' ******************************************************************************************************************************
    Public Function HasAudioStream(ByVal bstrFileName As String) As Boolean
        Dim nCount As Long
        Dim nStreams As Long
        Dim objMediaDet As MediaDet = Nothing
        Dim bstrMediaCLSID As String
        On Error GoTo ErrLine

        'verify argument(s)
        If bstrFileName <> vbNullString Then
            'instantiate mediadet
            objMediaDet = New MediaDet
            'assign the filename to the MediaDet
            objMediaDet.Filename = bstrFileName
            'obtain the number of streams
            nStreams = objMediaDet.OutputStreams
            'verify there is at least one valid media stream in the assigned file
            If nStreams > 0 Then
                For nCount = 0 To objMediaDet.OutputStreams - 1
                    objMediaDet.CurrentStream = nCount
                    bstrMediaCLSID = objMediaDet.StreamTypeB
                    If bstrMediaCLSID = SLIDESHOWVB_AUDIOTYPE Then
                        HasAudioStream = True : Exit For
                    End If
                Next
            End If
        End If

        'clean-up & dereference
        If Not objMediaDet Is Nothing Then objMediaDet = Nothing
        Exit Function

ErrLine:

        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: RunFilterGraphSync
    ' * procedure description: playsback the filtergraph for the client synchronously, and returns.
    ' *
    ' ******************************************************************************************************************************
    Public Function RunFilterGraphSync(ByVal objGraph As IFilterGraph) As Boolean
        Dim nExitCode As Long
        Dim objMediaEvent As IMediaEvent
        Dim objMediaControl As IMediaControl
        On Error GoTo ErrLine

        'obtain the media control, event
        objMediaEvent = objGraph
        objMediaControl = objGraph

        'render the graph
        objMediaControl.Run()
        'wait for play to complete..
        objMediaEvent.WaitForCompletion(-1, nExitCode)

        'clean-up & dereference
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaControl Is Nothing Then objMediaControl = Nothing
        Return True

ErrLine:
        Err.Clear()
        Return False
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: RestoreTimeline
    ' * procedure description:  Restores a timeline from a file given the specified format
    ' *
    ' ******************************************************************************************************************************
    Public Sub RestoreTimeline(ByVal objTimeline As AMTimeline, ByVal bstrFileName As String, Optional ByVal Format As DEXImportFormatEnum = DEXImportFormatEnum.DEXImportXTL)
        Dim objXml2Dex As Xml2Dex = Nothing
        On Error GoTo ErrLine

        If Not objTimeline Is Nothing Then
            Select Case LCase(Format)
                Case DEXImportFormatEnum.DEXImportXTL
                    'restore the timeline from a dexter XTL File Format
                    objXml2Dex = New Xml2Dex
                    Call objXml2Dex.ReadXMLFile(objTimeline, bstrFileName)
            End Select
        End If

        'clean-up & dereference
        If Not objXml2Dex Is Nothing Then objXml2Dex = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: SaveMUXedTimeline2GRF
    ' * procedure description:  Returns a graph from a timeline plus mux & filewriter
    ' *
    ' ******************************************************************************************************************************
    Public Function BuildMUXedGRFfromTimeline(ByVal objTimeline As AMTimeline, ByVal szFileDest As String, Optional ByVal SaveToGRF As Boolean = False) As QuartzTypeLib.FilgraphManager

        If BuildXTLfromTimeLine(objTimeline, Application.StartupPath & "\temp.xtl") Then
            Dim objXml2Dex As Xml2Dex = Nothing
            Dim objXMLParser As New Xml2Dex
            Dim objFilterGraph As QuartzTypeLib.FilgraphManager = Nothing
            Dim objRenderEngine As SmartRenderEngine = Nothing
            Dim piMUXInputVideo As QuartzTypeLib.IPinInfo = Nothing
            Dim piMUXInputAudio As QuartzTypeLib.IPinInfo = Nothing
            Dim piMUXOutput As QuartzTypeLib.IPinInfo = Nothing
            Dim piFileIn As QuartzTypeLib.IPinInfo = Nothing
            Dim objConnPinInfo As QuartzTypeLib.IPinInfo = Nothing
            On Error GoTo ErrLine

            'NB - szFileDest is passed with a .grf extension, but we need the video destination filename
            szFileDest = Mid(szFileDest, 1, szFileDest.Length - 3) & "avi"

            'clean-up & dereference
            Call ClearTimeline(m_objTimeline)
            If Not m_objMediaEvent Is Nothing Then m_objMediaEvent = Nothing
            If Not m_objRenderEngine Is Nothing Then m_objRenderEngine = Nothing
            'reinstantiate the timeline & render engine
            m_objTimeline = New AMTimeline
            m_objRenderEngine = New RenderEngine
            'Set dynamic reconnect
            ' m_objRenderEngine.SetDynamicReconnectLevel(1)
            'read in the file
            Call objXMLParser.ReadXMLFile(m_objTimeline, Application.StartupPath & "\temp.xtl")
            'set the timeline
            m_objRenderEngine.SetTimelineObject(m_objTimeline)
            'connect the front
            m_objRenderEngine.ConnectFrontEnd()
            'render the output pins (e.g. 'backend')
            ' m_objRenderEngine.RenderOutputPins()
            ' ask for the graph, so we can control it
            Call m_objRenderEngine.GetFilterGraph(objFilterGraph)
            'Add muxer and filewriter
            AddFileWriterAndMux(objFilterGraph, szFileDest)

            'Now, find and remove the video and audio renderers.
            For Each objFilter As QuartzTypeLib.IFilterInfo In objFilterGraph.FilterCollection

                If objFilter.Name = "AVI Mux" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        If iPin.Name = "Input 01" Then
                            piMUXInputVideo = iPin
                        End If
                        If iPin.Name = "AVI Out" Then
                            piMUXOutput = iPin
                        End If
                    Next
                End If
                If objFilter.Name = "File writer" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        If iPin.Name = "in" Then
                            piFileIn = iPin
                        End If
                    Next
                End If

            Next

            'Find the Output 0 pin from the video DEXFILT - ends in "VIDEO"
            For Each objFilter As QuartzTypeLib.IFilterInfo In objFilterGraph.FilterCollection
                If Right(objFilter.Name, 5) = "VIDEO" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        Debug.Print(iPin.Name)
                        If iPin.Name = "Output 0" Then
                            iPin.Connect(piMUXInputVideo)
                            If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                            objConnPinInfo = iPin.ConnectedTo
                            If objConnPinInfo Is Nothing Then GoTo ErrLine
                        End If
                    Next
                End If
            Next

            'Connecting the video source to Input 01 on the AVI mux will enable pin Input 02
            For Each objFilter As QuartzTypeLib.IFilterInfo In objFilterGraph.FilterCollection
                If objFilter.Name = "AVI Mux" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        If iPin.Name = "Input 02" Then
                            piMUXInputAudio = iPin
                        End If
                    Next
                End If
            Next

            'Find the Output 0 pin from the video DEXFILT - ends in "AUDIO"
            For Each objFilter As QuartzTypeLib.IFilterInfo In objFilterGraph.FilterCollection
                If Right(objFilter.Name, 5) = "AUDIO" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        If iPin.Name = "Output 0" Then
                            iPin.Connect(piMUXInputAudio)
                            If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                            objConnPinInfo = iPin.ConnectedTo
                            If objConnPinInfo Is Nothing Then GoTo ErrLine
                        End If
                    Next
                End If
            Next

            'Finally, connect the Mux output to the file writer.
            piMUXOutput.Connect(piFileIn)
            If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
            objConnPinInfo = piMUXOutput.ConnectedTo
            If objConnPinInfo Is Nothing Then GoTo ErrLine

            'Save the resultant GRF file.
            objXml2Dex = New Xml2Dex
            If SaveToGRF Then objXml2Dex.WriteGrfFile(objFilterGraph, szFileDest & "_.grf")

            BuildMUXedGRFfromTimeline = objFilterGraph

            'clean-up & dereference
            If Not piMUXInputVideo Is Nothing Then piMUXInputVideo = Nothing
            If Not piMUXInputAudio Is Nothing Then piMUXInputAudio = Nothing
            If Not piMUXOutput Is Nothing Then piMUXOutput = Nothing
            If Not piFileIn Is Nothing Then piFileIn = Nothing
            If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing

            If Not objXMLParser Is Nothing Then objXMLParser = Nothing
            If Not objXml2Dex Is Nothing Then objXml2Dex = Nothing
            If Not objFilterGraph Is Nothing Then objFilterGraph = Nothing
            If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
            Return BuildMUXedGRFfromTimeline
        End If

        Return Nothing

ErrLine:
        MsgBox(Err.Description, MsgBoxStyle.Critical, Application.ProductName)
        Err.Clear()
        Return Nothing
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: SaveTimeline
    ' * procedure description:  Persists a timeline to a file given the specified format
    ' *
    ' ******************************************************************************************************************************
    Public Sub SaveTimeline(ByVal objTimeline As AMTimeline, ByVal bstrFileName As String, Optional ByVal Format As DEXExportFormatEnum = DEXExportFormatEnum.DEXExportXTL)
        Dim objXml2Dex As Xml2Dex = Nothing
        Dim objFilterGraph As IGraphBuilder = Nothing
        Dim objRenderEngine As RenderEngine = Nothing
        On Error GoTo ErrLine

        If Not objTimeline Is Nothing Then
            Select Case LCase(Format)
                Case DEXExportFormatEnum.DEXExportXTL
                    'Persist the timeline using the dexter XTL File Format
                    objXml2Dex = New Xml2Dex
                    objXml2Dex.WriteXMLFile(objTimeline, bstrFileName)

                Case DEXExportFormatEnum.DEXExportGRF
                    'Persist the timeline to a DirectShow Filter Graph Format
                    objXml2Dex = New Xml2Dex
                    objRenderEngine = New RenderEngine
                    objRenderEngine.SetTimelineObject(objTimeline)
                    objRenderEngine.ConnectFrontEnd()
                    objRenderEngine.RenderOutputPins()
                    objRenderEngine.GetFilterGraph(objFilterGraph)
                    objXml2Dex.WriteGrfFile(objFilterGraph, bstrFileName)
            End Select
        End If

        'clean-up & dereference
        If Not objXml2Dex Is Nothing Then objXml2Dex = Nothing
        If Not objFilterGraph Is Nothing Then objFilterGraph = Nothing
        If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: GetPinInfo
    ' * procedure description:  Returns an IPinInfo interface given a filtergraph manager and IPin object.
    ' *                                       The derived IPinInfo interface can be utilized for gaining information on the elected pin.
    ' ******************************************************************************************************************************
    Private Function GetPinInfo(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager, ByVal objPin As IPin) As QuartzTypeLib.IPinInfo
        On Error GoTo ErrLine
        Dim objPin2 As IPin = Nothing
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
    ' * procedure name: AddDVExportFilters
    ' * procedure description:  Appends a filewriter and mux filter to the given filtergraph.
    ' *                                       The FileName as required for the filewriter and evaluates to the output file destination.
    ' ******************************************************************************************************************************
    Public Function AddDVExportFilters(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager) As QuartzTypeLib.FilgraphManager

        Dim objColorSpaceFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegisteredFilters As Object = Nothing
        Dim objDVMuxFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objDVEncFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objDVDeviceFilterInfo As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilterInfo As QuartzTypeLib.IRegFilterInfo = Nothing
        Dim objConnPinInfo As QuartzTypeLib.IPinInfo = Nothing
        Dim iPin_CC_Input As IPinInfo = Nothing
        Dim iPin_CC_Output As IPinInfo = Nothing
        Dim iPin_DVEnc_Input As IPinInfo = Nothing
        Dim iPin_DVEnc_Output As IPinInfo = Nothing
        Dim iPin_Mux_Input0 As IPinInfo = Nothing
        Dim iPin_Mux_Input1 As IPinInfo = Nothing
        Dim iPin_Mux_Output As IPinInfo = Nothing
        Dim iPin_VCR_Input As IPinInfo = Nothing

        'derive a collection of registered filters from the filtergraph manager
        objRegisteredFilters = objFilterGraphManager.RegFilterCollection

        'enumerate through the registered filters
        For Each objRegFilterInfo In objRegisteredFilters
            If Trim(LCase(objRegFilterInfo.Name)) = "color space converter" Then
                objRegFilterInfo.Filter(objColorSpaceFilterInfo)
            ElseIf Trim(LCase(objRegFilterInfo.Name)) = "dv muxer" Then
                objRegFilterInfo.Filter(objDVMuxFilterInfo)
            ElseIf Trim(LCase(objRegFilterInfo.Name)) = "dv video encoder" Then
                objRegFilterInfo.Filter(objDVEncFilterInfo)
            ElseIf Trim(LCase(objRegFilterInfo.Name)) = "microsoft dv camera and vcr" Then
                objRegFilterInfo.Filter(objDVDeviceFilterInfo)
            End If
        Next

        'Check for an output device
        If objDVDeviceFilterInfo Is Nothing Then Return Nothing

        'Show DV Encoder property page - selects PAL or NTSC
        Dim obj As New FSFWRAPLib.SinkInfo
        obj.Filter = objDVEncFilterInfo
        obj.ShowPropPage(frmMain.Handle)

        'Now remove the audio and video renderers
        For Each filter As IFilterInfo In objFilterGraphManager.FilterCollection
            Debug.Print(filter.Name)
            If Right$(filter.Name, 8) = "Renderer" Then
                For Each Pin As IPinInfo In filter.Pins
                    Try
                        Pin.Disconnect()
                    Catch ex As Exception

                    End Try
                Next
            End If
        Next

        'Find Color Space Converter Pins
        For Each Pin As IPinInfo In objColorSpaceFilterInfo.Pins
            If Pin.Name = "Input" Then
                iPin_CC_Input = Pin
            End If
            If Pin.Name = "XForm Out" Then
                iPin_CC_Output = Pin
            End If
        Next

        'Find DV Encoder Pins
        For Each Pin As IPinInfo In objDVEncFilterInfo.Pins
            If Pin.Name = "XForm In" Then
                iPin_DVEnc_Input = Pin
            End If
            If Pin.Name = "XForm Out" Then
                iPin_DVEnc_Output = Pin
            End If
        Next

        'Find DV Mux Pins
        For Each Pin As IPinInfo In objDVMuxFilterInfo.Pins
            If Pin.Name = "Stream 0" Then
                iPin_Mux_Input0 = Pin
            End If
            If Pin.Name = "Output" Then
                iPin_Mux_Output = Pin
            End If
        Next

        'Find DV Device Pins
        For Each Pin As IPinInfo In objDVDeviceFilterInfo.Pins
            If Pin.Name = "DV A/V In" Then
                iPin_VCR_Input = Pin
            End If
        Next

        'Find the Output 0 pin from the video DEXFILT - ends in "VIDEO" --> Connect Color Space Converter
        For Each objFilter As IFilterInfo In objFilterGraphManager.FilterCollection
            If Right(objFilter.Name, 5) = "VIDEO" Then
                For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                    If iPin.Name = "Output 0" Then
                        iPin.Connect(iPin_CC_Input.Pin)
                        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                        objConnPinInfo = iPin.ConnectedTo
                        If objConnPinInfo Is Nothing Then GoTo ErrLine
                        Exit For
                    End If
                Next
            End If
        Next

        'Now connect Color Space Converter to DV Encoder
        iPin_CC_Output.Connect(iPin_DVEnc_Input)
        objConnPinInfo = iPin_CC_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        'Now connect DV Encoder to DV Muxer
        iPin_DVEnc_Output.Connect(iPin_Mux_Input0)
        objConnPinInfo = iPin_DVEnc_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo ErrLine

        'Connecting the video source to Input 01 on the AVI mux will enable pin Input 02
        For Each Pin As IPinInfo In objDVMuxFilterInfo.Pins
            If Pin.Name = "Stream 1" Then
                iPin_Mux_Input1 = Pin
            End If
        Next
        If iPin_Mux_Input1 Is Nothing Then
            MsgBox("An audio connection could not be found.", MsgBoxStyle.Exclamation, Application.ProductName)
        Else
            'Find the Output 0 pin from the video DEXFILT - ends in "AUDIO"
            For Each objFilter As IFilterInfo In objFilterGraphManager.FilterCollection
                If Right(objFilter.Name, 5) = "AUDIO" Then
                    For Each iPin As QuartzTypeLib.IPinInfo In objFilter.Pins
                        If iPin.Name = "Output 0" Then
                            iPin.Connect(iPin_Mux_Input1.Pin)
                            If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
                            objConnPinInfo = iPin.ConnectedTo
                            If objConnPinInfo Is Nothing Then GoTo ErrLine
                        End If
                    Next
                End If
            Next
        End If

        'Finally, connect DV Muxer to the VCR device
        Try
            iPin_Mux_Output.Connect(iPin_VCR_Input)
        Catch ex As Exception
            MsgBox("An error occured connecting to the DV device.  Please check the format of the video input matches the recording device.", MsgBoxStyle.Exclamation, Application.ProductName)
            GoTo ErrLine
        End Try

        objConnPinInfo = iPin_Mux_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then
            MsgBox("An error occured connecting to the DV device.  Please check the format of the video input matches the recording device.", MsgBoxStyle.Exclamation, Application.ProductName)
            GoTo ErrLine
        End If

        Dim objXml2Dex As New DexterLib.Xml2Dex
        objXml2Dex.WriteGrfFile(objFilterGraphManager, "C:\Mod_Graph.grf")

        'clean-up & dereference
        If Not objColorSpaceFilterInfo Is Nothing Then objColorSpaceFilterInfo = Nothing
        If Not objRegFilterInfo Is Nothing Then objRegFilterInfo = Nothing
        If Not objDVMuxFilterInfo Is Nothing Then objDVMuxFilterInfo = Nothing
        If Not objDVEncFilterInfo Is Nothing Then objDVEncFilterInfo = Nothing
        If Not objDVDeviceFilterInfo Is Nothing Then objDVDeviceFilterInfo = Nothing
        If Not objRegisteredFilters Is Nothing Then objRegisteredFilters = Nothing
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        If Not iPin_CC_Input Is Nothing Then iPin_CC_Input = Nothing
        If Not iPin_CC_Output Is Nothing Then iPin_CC_Output = Nothing
        If Not iPin_DVEnc_Input Is Nothing Then iPin_DVEnc_Input = Nothing
        If Not iPin_DVEnc_Output Is Nothing Then iPin_DVEnc_Output = Nothing
        If Not iPin_Mux_Input0 Is Nothing Then iPin_Mux_Input0 = Nothing
        If Not iPin_Mux_Input1 Is Nothing Then iPin_Mux_Input1 = Nothing
        If Not iPin_Mux_Output Is Nothing Then iPin_Mux_Output = Nothing
        If Not iPin_VCR_Input Is Nothing Then iPin_VCR_Input = Nothing
        If Not objXml2Dex Is Nothing Then objXml2Dex = Nothing
        If Not obj Is Nothing Then obj = Nothing
        Return objFilterGraphManager
        Exit Function

ErrLine:
        Err.Clear()
        Return Nothing
    End Function

    Public Sub AddFileWriterAndMux(ByVal objFilterGraphManager As QuartzTypeLib.FilgraphManager, ByVal bstrFileName As String)

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
    Public Sub RenderGroupPins(ByVal objRenderEngine As RenderEngine, ByVal objTimeline As AMTimeline)
        Dim objPin As IPin = Nothing
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
    Public Function File_Exists(ByVal bstrFileName As String) As Boolean
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
    Public Function File_Delete(ByVal bstrFileName As String, Optional ByVal SendToRecycleBin As Boolean = True, Optional ByVal Confirm As Boolean = True, Optional ByVal ShowProgress As Boolean = True) As Long
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


    ' ******************************************************************************************************************************
    ' * procedure name: TransitionCLSIDToFriendlyName
    ' * procedure description: returns the localized friendly name of a transition given it's CLSID
    ' *
    ' ******************************************************************************************************************************
    Public Function TransitionCLSIDToFriendlyName(ByVal bstrTransitionCLSID As String, Optional ByVal bstrLanguage As String = "EN-US") As String
        Dim bstrReturn As String = Nothing
        On Error GoTo ErrLine

        If UCase(bstrLanguage) = "EN-US" Then
            Select Case bstrTransitionCLSID
                Case "{C3BDF740-0B58-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Barn"
                Case "{00C429C0-0BA9-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Blinds"
                Case "{107045D1-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "BurnFilm"
                Case "{AA0D4D0C-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "CenterCurls"
                Case "{2A54C908-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "ColorFade"
                Case "{9A43A844-0831-11D1-817F-0000F87557DB}"
                    bstrReturn = "Compositor"
                Case "{AA0D4D0E-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "Curls"
                Case "{AA0D4D12-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "Curtains"
                Case "{16B280C5-EE70-11D1-9066-00C04FD9189D}"
                    bstrReturn = "Fade"
                Case "{107045CC-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "FadeWhite"
                Case "{2A54C90B-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "FlowMotion"
                Case "{2A54C913-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "GlassBlock"
                Case "{2A54C911-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Grid"
                Case "{93073C40-0BA5-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Inset"
                Case "{3F69F351-0379-11D2-A484-00C04F8EFB69}"
                    bstrReturn = "Iris"
                Case "{2A54C904-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Jaws"
                Case "{107045CA-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Lens"
                Case "{107045C8-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "LightWipe"
                Case "{AA0D4D0A-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "Liquid"
                Case "{AA0D4D08-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "PageCurl"
                Case "{AA0D4D10-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "PeelABCD"
                Case "{4CCEA634-FBE0-11d1-906A-00C04FD9189D}"
                    bstrReturn = "Pixelate"
                Case "{424B71AF-0695-11D2-A484-00C04F8EFB69}"
                    bstrReturn = "RadialWipe"
                Case "{AA0D4D03-06A3-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "Ripple"
                Case "{9C61F46E-0530-11D2-8F98-00C04FB92EB7}"
                    bstrReturn = "RollDown"
                Case "{810E402F-056B-11D2-A484-00C04F8EFB69}"
                    bstrReturn = "Slide"
                Case "{dE75D012-7A65-11D2-8CEA-00A0C9441E20}"
                    bstrReturn = "SMPTE Wipe"
                Case "{ACA97E00-0C7D-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Spiral"
                Case "{7658F2A2-0A83-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Stretch"
                Case "{2A54C915-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Threshold"
                Case "{107045CF-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Twister"
                Case "{2A54C90D-07AA-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Vacuum"
                Case "{107045C5-06E0-11D2-8D6D-00C04F8EF8E0}"
                    bstrReturn = "Water"
                Case "{5AE1DAE0-1461-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Wheel"
                Case "{AF279B30-86EB-11D1-81BF-0000F87557DB}"
                    bstrReturn = "Wipe"
                Case "{0E6AE022-0C83-11D2-8CD4-00104BC75D9A}"
                    bstrReturn = "WormHole"
                Case "{E6E73D20-0C8A-11d2-A484-00C04F8EFB69}"
                    bstrReturn = "Zigzag"
                Case Else : bstrReturn = vbNullString
            End Select
        End If
        'return friendly name to the client
        TransitionCLSIDToFriendlyName = bstrReturn
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: TransitionFriendlyNameToCLSID
    ' * procedure description: returns the CLSID of a transition given it's localized friendly name
    ' *
    ' ******************************************************************************************************************************
    Public Function TransitionFriendlyNameToCLSID(ByVal bstrFriendlyName As String, Optional ByVal bstrLanguage As String = "EN-US") As String
        Dim bstrReturn As String = Nothing
        On Error GoTo ErrLine

        If UCase(bstrLanguage) = "EN-US" Then
            Select Case bstrFriendlyName
                Case "Barn"
                    bstrReturn = "{C3BDF740-0B58-11d2-A484-00C04F8EFB69}"
                Case "Blinds"
                    bstrReturn = "{00C429C0-0BA9-11d2-A484-00C04F8EFB69}"
                Case "BurnFilm"
                    bstrReturn = "{107045D1-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "CenterCurls"
                    bstrReturn = "{AA0D4D0C-06A3-11D2-8F98-00C04FB92EB7}"
                Case "ColorFade"
                    bstrReturn = "{2A54C908-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Compositor"
                    bstrReturn = "{9A43A844-0831-11D1-817F-0000F87557DB}"
                Case "Curls"
                    bstrReturn = "{AA0D4D0E-06A3-11D2-8F98-00C04FB92EB7}"
                Case "Curtains"
                    bstrReturn = "{AA0D4D12-06A3-11D2-8F98-00C04FB92EB7}"
                Case "Fade"
                    bstrReturn = "{16B280C5-EE70-11D1-9066-00C04FD9189D}"
                Case "FadeWhite"
                    bstrReturn = "{107045CC-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "FlowMotion"
                    bstrReturn = "{2A54C90B-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "GlassBlock"
                    bstrReturn = "{2A54C913-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Grid"
                    bstrReturn = "{2A54C911-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Inset"
                    bstrReturn = "{93073C40-0BA5-11d2-A484-00C04F8EFB69}"
                Case "Iris"
                    bstrReturn = "{3F69F351-0379-11D2-A484-00C04F8EFB69}"
                Case "Jaws"
                    bstrReturn = "{2A54C904-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Lens"
                    bstrReturn = "{107045CA-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "LightWipe"
                    bstrReturn = "{107045C8-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "Liquid"
                    bstrReturn = "{AA0D4D0A-06A3-11D2-8F98-00C04FB92EB7}"
                Case "PageCurl"
                    bstrReturn = "{AA0D4D08-06A3-11D2-8F98-00C04FB92EB7}"
                Case "PeelABCD"
                    bstrReturn = "{AA0D4D10-06A3-11D2-8F98-00C04FB92EB7}"
                Case "Pixelate"
                    bstrReturn = "{4CCEA634-FBE0-11d1-906A-00C04FD9189D}"
                Case "RadialWipe"
                    bstrReturn = "{424B71AF-0695-11D2-A484-00C04F8EFB69}"
                Case "Ripple"
                    bstrReturn = "{AA0D4D03-06A3-11D2-8F98-00C04FB92EB7}"
                Case "RollDown"
                    bstrReturn = "{9C61F46E-0530-11D2-8F98-00C04FB92EB7}"
                Case "Slide"
                    bstrReturn = "{810E402F-056B-11D2-A484-00C04F8EFB69}"
                Case "SMPTE Wipe"
                    bstrReturn = "{dE75D012-7A65-11D2-8CEA-00A0C9441E20}"
                Case "Spiral"
                    bstrReturn = "{ACA97E00-0C7D-11d2-A484-00C04F8EFB69}"
                Case "Stretch"
                    bstrReturn = "{7658F2A2-0A83-11d2-A484-00C04F8EFB69}"
                Case "Threshold"
                    bstrReturn = "{2A54C915-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Twister"
                    bstrReturn = "{107045CF-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "Vacuum"
                    bstrReturn = "{2A54C90D-07AA-11D2-8D6D-00C04F8EF8E0}"
                Case "Water"
                    bstrReturn = "{107045C5-06E0-11D2-8D6D-00C04F8EF8E0}"
                Case "Wheel"
                    bstrReturn = "{5AE1DAE0-1461-11d2-A484-00C04F8EFB69}"
                Case "Wipe"
                    bstrReturn = "{AF279B30-86EB-11D1-81BF-0000F87557DB}"
                Case "WormHole"
                    bstrReturn = "{0E6AE022-0C83-11D2-8CD4-00104BC75D9A}"
                Case "Zigzag"
                    bstrReturn = "{E6E73D20-0C8A-11d2-A484-00C04F8EFB69}"
                Case Else : bstrReturn = vbNullString
            End Select
        End If
        'return friendly name to the client
        TransitionFriendlyNameToCLSID = bstrReturn
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: EffectCLSIDToFriendlyName
    ' * procedure description: returns the localized friendly name of an effect given it's CLSID
    ' *
    ' ******************************************************************************************************************************
    Public Function EffectCLSIDToFriendlyName(ByVal bstrTransitionCLSID As String, Optional ByVal bstrLanguage As String = "EN-US") As String
        Dim bstrReturn As String = Nothing
        On Error GoTo ErrLine

        If UCase(bstrLanguage) = "EN-US" Then
            Select Case bstrTransitionCLSID
                Case "{16B280C8-EE70-11D1-9066-00C04FD9189D}"
                    bstrReturn = "BasicImage"
                Case "{7312498D-E87A-11d1-81E0-0000F87557DB}"
                    bstrReturn = "Blur"
                Case "{421516C1-3CF8-11D2-952A-00C04FA34F05}"
                    bstrReturn = "Chroma"
                Case "{ADC6CB86-424C-11D2-952A-00C04FA34F05}"
                    bstrReturn = "DropShadow"
                Case "{F515306D-0156-11d2-81EA-0000F87557DB}"
                    bstrReturn = "Emboss"
                Case "{F515306E-0156-11d2-81EA-0000F87557DB}"
                    bstrReturn = "Engrave"
                Case "{16B280C5-EE70-11D1-9066-00C04FD9189D}"
                    bstrReturn = "Fade"
                Case "{4CCEA634-FBE0-11d1-906A-00C04FD9189D}"
                    bstrReturn = "Pixelate"
                Case Else : bstrReturn = vbNullString
            End Select
        End If
        'return friendly name to the client
        EffectCLSIDToFriendlyName = bstrReturn
        Exit Function

ErrLine:
        Err.Clear()
        Exit Function
    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: EffectFriendlyNameToCLSID
    ' * procedure description: returns the CLSID of an effect given it's localized friendly name
    ' *
    ' ******************************************************************************************************************************
    Public Function EffectFriendlyNameToCLSID(ByVal bstrFriendlyName As String, Optional ByVal bstrLanguage As String = "EN-US") As String
        Dim bstrReturn As String = Nothing
        On Error GoTo ErrLine

        If UCase(bstrLanguage) = "EN-US" Then
            Select Case bstrFriendlyName
                Case "BasicImage"
                    bstrReturn = "{16B280C8-EE70-11D1-9066-00C04FD9189D}"
                Case "Blur"
                    bstrReturn = "{7312498D-E87A-11d1-81E0-0000F87557DB}"
                Case "Chroma"
                    bstrReturn = "{421516C1-3CF8-11D2-952A-00C04FA34F05}"
                Case "DropShadow"
                    bstrReturn = "{ADC6CB86-424C-11D2-952A-00C04FA34F05}"
                Case "Emboss"
                    bstrReturn = "{F515306D-0156-11d2-81EA-0000F87557DB}"
                Case "Engrave"
                    bstrReturn = "{F515306E-0156-11d2-81EA-0000F87557DB}"
                Case "Fade"
                    bstrReturn = "{16B280C5-EE70-11D1-9066-00C04FD9189D}"
                Case "Pixelate"
                    bstrReturn = "{4CCEA634-FBE0-11d1-906A-00C04FD9189D}"
                Case Else : bstrReturn = vbNullString
            End Select
        End If
        'return friendly name to the client
        EffectFriendlyNameToCLSID = bstrReturn
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

        Dim objTimeline As AMTimeline = Nothing
        Dim objSourceObj As AMTimelineObj = Nothing
        Dim objTrackObject As AMTimelineObj = Nothing
        Dim objAudioGroupObj As AMTimelineObj = Nothing
        Dim objVideoGroupObject As AMTimelineObj = Nothing

        Dim objSource As AMTimelineSrc = Nothing
        Dim objTrack As AMTimelineTrack = Nothing
        Dim objAudioGroup As AMTimelineGroup = Nothing
        Dim objVideoGroup As AMTimelineGroup = Nothing
        Dim objAudioComposition As AMTimelineComp = Nothing
        Dim objVideoComposition As AMTimelineComp = Nothing
        Dim objSmartRenderEngine As New SmartRenderEngine

        On Error GoTo ErrLine

        m_PercentComplete = 0

        'Instantiate the timeline
        objTimeline = New AMTimeline
        'Create empty node on timeline for video
        objTimeline.CreateEmptyNode(objVideoGroupObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'Derive video group object from the timeline object.
        objVideoGroup = objVideoGroupObject
        'Set the media type of the video group.
        objVideoGroup.SetMediaTypeForVB(0)
        'Append the video group to the timeline.
        objTimeline.AddGroup(objVideoGroup)

        'Create empty node on timeline for the track.
        objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
        'Obtain a composition from the video group.
        objVideoComposition = objVideoGroup


        'Inset the track into the composition.
        objVideoComposition.VTrackInsBefore(objTrackObject, -1)
        'Derive the track object.
        objTrack = objTrackObject

        'Create empty node on timeline for the source clip.
        objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
        'Derive source clip from the timeline object.
        objSource = objSourceObj

        'Set duration times
        StopTime = StartTime + dblDuration
        objSourceObj.SetStartStop2(0, dblDuration)
        objSource.SetMediaTimes2(StartTime, StopTime)
        objSource.SetMediaName(FileSource)

        'Append source clip to the track.
        objTrack.SrcAdd(objSourceObj)

        'Get media type
        'Dim srcMediaSource As New MediaDet
        'srcMediaSource.Filename = FileSource
        'Dim srcMediaType As _AMMediaType = srcMediaSource.StreamMediaType

        'Check for and facillitate audio.
        If HasAudio Then
            'Create an empty node on the timeline for the audio group.
            objTimeline.CreateEmptyNode(objAudioGroupObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
            'Derive the audio group from the timeline object.
            objAudioGroup = objAudioGroupObj
            'Set the media type of the audio group.
            objAudioGroup.SetMediaTypeForVB(1)
            'Append the group to the timeline.
            objTimeline.AddGroup(objAudioGroup)

            'Create an empty node on the timeline for the audio track.
            objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
            'Derive a composition from the audio group.
            objAudioComposition = objAudioGroup
            'Insert the track into the composition
            objAudioComposition.VTrackInsBefore(objTrackObject, -1)
            'Derive a track object from the timeline object.
            objTrack = objTrackObject

            'Create an empty node for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
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

        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        Dim szoldtitle As String = Nothing
        If Not owner Is Nothing Then szoldtitle = owner.Text

        Do : Application.DoEvents()
            If Not objMediaEvent Is Nothing Then
                'Set completed %
                m_PercentComplete = CSng(objMediaPosition.CurrentPosition / objMediaPosition.Duration)
                If Not owner Is Nothing Then
                    owner.Text = "Exporting --> Please Wait (" & Int(m_PercentComplete).ToString & ")"
                End If
                'Debug.Print m_PercentComplete
                Application.DoEvents()
                Call objMediaEvent.WaitForCompletion(100, nReturnCode)
                If nReturnCode = 1 Then Exit Do
            Else : Exit Do
            End If
        Loop
        m_PercentComplete = -1

        If Not owner Is Nothing Then owner.Text = szoldtitle
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
        'If Not srcMediaSource Is Nothing Then srcMediaSource = Nothing

        Exit Sub

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() : GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
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

        Dim objTimeline As AMTimeline = Nothing
        Dim objSourceObj As AMTimelineObj = Nothing
        Dim objTrackObject As AMTimelineObj = Nothing
        Dim objAudioGroupObj As AMTimelineObj = Nothing
        Dim objVideoGroupObject As AMTimelineObj = Nothing

        Dim objSource As AMTimelineSrc = Nothing
        Dim objTrack As AMTimelineTrack = Nothing
        Dim objAudioGroup As AMTimelineGroup = Nothing
        Dim objVideoGroup As AMTimelineGroup = Nothing
        Dim objAudioComposition As AMTimelineComp = Nothing
        Dim objVideoComposition As AMTimelineComp = Nothing
        Dim objSmartRenderEngine As New SmartRenderEngine

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
        objTimeline = New AMTimeline
        'Create empty node on timeline for video
        objTimeline.CreateEmptyNode(objVideoGroupObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'Derive video group object from the timeline object.
        objVideoGroup = objVideoGroupObject
        'Set the media type of the video group.
        objVideoGroup.SetMediaTypeForVB(0)
        'Append the video group to the timeline.
        objTimeline.AddGroup(objVideoGroup)

        'Create empty node on timeline for the track.
        objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
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
            objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
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
            objTimeline.CreateEmptyNode(objAudioGroupObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
            'Derive the audio group from the timeline object.
            objAudioGroup = objAudioGroupObj
            'Set the media type of the audio group.
            objAudioGroup.SetMediaTypeForVB(1)
            'Append the group to the timeline.
            objTimeline.AddGroup(objAudioGroup)

            'Create an empty node on the timeline for the audio track.
            objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
            'Derive a composition from the audio group.
            objAudioComposition = objAudioGroup
            'Insert the track into the composition
            objAudioComposition.VTrackInsBefore(objTrackObject, -1)
            'Derive a track object from the timeline object.
            objTrack = objTrackObject

            'Create an empty node for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive a source object from the timeline object
            objSource = objSourceObj
            'Reset time on track object.
            dTimeOnTrack = 0
            'Set sources to track object in sequence.
            For i As Integer = 0 To iSegmentCount - 1
                If GetMediaAudio(FileSource(i)) Then
                    'Create empty node on timeline for the source clip.
                    objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                    'Derive source clip from the timeline object.
                    objSource = objSourceObj
                    'Set Start and stop times to the source clip.
                    objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
                    objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                    objSource.SetMediaName(FileSource(i))
                    'Append source clip to the track.
                    objTrack.SrcAdd(objSourceObj)
                    dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
                End If
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
        'Run the graph
        objFilterGraphManager.Run()
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        GraphStatus = GraphState.StateRunning
        owner.Text = "Compiling video file now... (Please wait)"

        Do : Application.DoEvents()
            If Not objMediaEvent Is Nothing Then
                Call objMediaEvent.WaitForCompletion(1000, nReturnCode)
                If nReturnCode = 1 Then Exit Do

            Else : Exit Do
            End If
        Loop

        GraphStatus = GraphState.StateStopped
        Beep()
        owner.Text = "Done"

        ''Render output pins and prepare to smart write file.
        'RenderGroupPins(objSmartRenderEngine, objTimeline)
        ''Obtain a media event
        'objMediaEvent = objFilterGraphManager
        ''Obtain the position within the graph
        'objMediaPosition = objFilterGraphManager

        ''Run the graph
        'objFilterGraphManager.Run()
        'GraphStatus = GraphState.StateRunning
        'On Error Resume Next

        'Dim szOldTitle As String = Nothing
        'Dim timing As New Stopwatch
        'Dim ts As TimeSpan
        'timing.Start()

        'If Not owner Is Nothing Then szOldTitle = owner.Text

        'Do
        '    If Not objMediaEvent Is Nothing Then
        '        Call objMediaEvent.WaitForCompletion(100, nReturnCode)
        '        If nReturnCode = 1 Then m_bProcessComplete = True : RaiseEvent FileComplete() : Exit Do
        '        If GraphStatus <> GraphState.StateRunning Then Exit Sub
        '        If Not owner Is Nothing Then
        '            ts = timing.Elapsed

        '            ' Format and display the TimeSpan value.
        '            owner.Text = "Exporting --> Please Wait (" & String.Format("{0:00}:{1:00}:{2:00}", _
        '            ts.Hours, ts.Minutes, ts.Seconds) & ")"
        '        End If

        '        Application.DoEvents()
        '    Else : Exit Do
        '    End If
        'Loop

        'If Not owner Is Nothing Then owner.Text = szOldTitle
        'GraphStatus = GraphState.StateStopped
        'Beep()


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

        Exit Sub

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() : GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
        End Select
        Exit Sub
    End Sub

    ' ******************************************************************************************************************************
    ' * procedure name: BuildTimeLineFromMediaSources
    ' * procedure description: returns a timeline object compiled from media sources.
    ' *
    ' ******************************************************************************************************************************    '
    Public Function BuildTimeLineFromMediaSources(ByVal iSegmentCount As Integer, ByVal FileSource() As String, _
    ByVal lStartPoint() As Double, ByVal lEndPoint() As Double, Optional ByVal nSpeed() As Single = Nothing) As IAMTimeline

        Dim objTimeline As AMTimeline = Nothing
        Dim objSourceObject As AMTimelineObj = Nothing
        Dim objTrackObject As AMTimelineObj = Nothing
        Dim objAudioGroupObject As AMTimelineObj = Nothing
        Dim objVideoGroupObject As AMTimelineObj = Nothing
        Dim objSource As AMTimelineSrc = Nothing
        Dim objTrack As AMTimelineTrack = Nothing
        Dim objAudioGroup As AMTimelineGroup = Nothing
        Dim objVideoGroup As AMTimelineGroup = Nothing
        Dim boolAudioGroupSet As Boolean = False
        Dim boolVideoGroupSet As Boolean = False
        Dim objTransition As AMTimelineTrans = Nothing

        Dim TimelineTime As Double = 0

        'If no play speed information is passed then set value for all instances to 1
        If nSpeed Is Nothing Then
            ReDim nSpeed(iSegmentCount)
            For i As Integer = 0 To iSegmentCount
                nSpeed(i) = 1
            Next
        End If

        'Instantiate the timeline
        If Not objTimeline Is Nothing Then
            ClearTimeline(objTimeline)
            objTimeline = Nothing
        End If
        objTimeline = CreateTimeline()

        'Enable transitions on the timeline.
        objTimeline.EnableTransitions(1)

        'Emumerate the clips and place the groups on the timeline
        For i As Integer = 0 To iSegmentCount - 1

            'First, check that the file has media streams.  Not critical, but good practise.
            If HasStreams(FileSource(i)) Then

                If HasAudioStream(FileSource(i)) Then
                    If Not boolAudioGroupSet Then
                        'Create an audio group for the timeline.
                        objAudioGroup = CreateGroup(objTimeline, "AUDIO", DEXMediaTypeEnum.DEXMediaTypeAudio)
                        'Insert the new audio group into the timeline.
                        InsertGroup(objTimeline, objAudioGroup)
                        objAudioGroupObject = objAudioGroup
                        boolAudioGroupSet = True
                    End If
                End If

                If HasVideoStream(FileSource(i)) Then
                    If Not boolVideoGroupSet Then
                        'Create an video group for the timeline.
                        objVideoGroup = CreateGroup(objTimeline, "VIDEO", DEXMediaTypeEnum.DEXMediaTypeVideo)
                        'Insert the new video group into the timeline.
                        InsertGroup(objTimeline, objVideoGroup)
                        objVideoGroupObject = objVideoGroup
                        boolVideoGroupSet = True
                    End If
                End If
            End If
        Next

        'Emumerate the clips and place tracks and sources on the timeline
        For i As Integer = 0 To iSegmentCount - 1
            Dim nTime As Double = lEndPoint(i) - lStartPoint(i)

            If HasVideoStream(FileSource(i)) Then
                'Insert new video track for the clip in the timeline.
                objTrack = CreateTrack(objTimeline)
                objTrackObject = objTrack
                InsertTrack(objTrack, objVideoGroupObject)
                'Insert a new source clip into the timeline.
                objSource = CreateSource(objTimeline)

                If TimelineTime >= 1 Then TimelineTime -= 1

                InsertSource(objTrack, objSource, FileSource(i), TimelineTime, TimelineTime + (nTime / nSpeed(i)), lStartPoint(i), lEndPoint(i))

                If UserPrefs.AddFadeTransitions Then
                    objTransition = CreateTransition(objTimeline)
                    Dim TransitionStart As Double = TimelineTime - 1
                    Dim TransitionStop As Double = TimelineTime + 1
                    If TransitionStart < 0 Then TransitionStart = 0
                    Call InsertTransition(objTransition, objTrackObject, DEXT_Fade, TransitionStart, TransitionStop, False)
                End If

            End If

            If HasAudioStream(FileSource(i)) And nSpeed(i) = 1 Then
                'Insert new audio track for the clip in the timeline.
                objTrack = CreateTrack(objTimeline)
                objTrackObject = objTrack
                InsertTrack(objTrack, objAudioGroupObject)
                'Insert a new source clip into the timeline.
                objSource = CreateSource(objTimeline)

                InsertSource(objTrack, objSource, FileSource(i), TimelineTime, TimelineTime + (nTime / nSpeed(i)), lStartPoint(i), lEndPoint(i))

            End If

            TimelineTime += (nTime / nSpeed(i))

        Next

        Dim getMedia As New MediaDet
        getMedia.Filename = FileSource(0)
        Dim MediaType As _AMMediaType = Nothing
        MediaType = getMedia.StreamMediaType
        objVideoGroup.SetMediaType(MediaType)

        BuildTimeLineFromMediaSources = objTimeline

        'CleanUp:
        MediaType = Nothing
        If Not getMedia Is Nothing Then getMedia = Nothing
        If Not objTimeline Is Nothing Then objTimeline = Nothing
        If Not objSourceObject Is Nothing Then objSourceObject = Nothing
        If Not objTrackObject Is Nothing Then objTrackObject = Nothing
        If Not objAudioGroupObject Is Nothing Then objAudioGroupObject = Nothing
        If Not objVideoGroupObject Is Nothing Then objVideoGroupObject = Nothing
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objSource Is Nothing Then objSource = Nothing
        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing
        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing

        Return BuildTimeLineFromMediaSources
    End Function

    ' ******************************************************************************************************************************
    ' * procedure name: BuildAVIfromTimeLine
    ' * procedure description: compiles an AVI video from a timeline object - returns true is successful..
    ' *
    ' ******************************************************************************************************************************    '
    Public Function BuildAVIfromTimeLine(ByVal TimeLineObject As AMTimeline, ByVal szFileDest As String, Optional ByVal ctrlProgress As Object = Nothing) As Boolean

        Dim objGraph As IFilterGraph = BuildMUXedGRFfromTimeline(TimeLineObject, szFileDest, False)

        Dim nExitCode As Long
        Dim objMediaEvent As IMediaEvent
        Dim objMediaControl As IMediaControl
        Dim objPosition As IMediaPosition
        Dim res As Integer
        On Error GoTo ErrLine

        'obtain the media control, event
        objMediaEvent = objGraph
        objMediaControl = objGraph
        objPosition = objGraph

        ctrlProgress.Maximum = objPosition.Duration
        ctrlProgress.Value = objPosition.CurrentPosition

        'render the graph
        objMediaControl.Run()

        'display the progress during render
        frmMain.toolActionStatus.Text = "Compiling video file: " & szFileDest & " PLEASE WAIT"
        Application.DoEvents()
        'Do
        '    ctrlProgress.Value = objPosition.CurrentPosition
        '    Application.DoEvents()
        'Loop Until objPosition.CurrentPosition >= objPosition.Duration

        'wait for play to complete..
        objMediaEvent.WaitForCompletion(-1, nExitCode)
        objMediaControl.StopWhenReady()
        Beep()
        frmMain.toolActionStatus.Text = "Ready..."
        ctrlProgress.Value = 0


        'clean-up & dereference
        ClearTimeline(TimeLineObject)
        If Not objGraph Is Nothing Then objGraph = Nothing
        If Not TimeLineObject Is Nothing Then TimeLineObject = Nothing
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaControl Is Nothing Then objMediaControl = Nothing
        Return True

ErrLine:
        Err.Clear()
        '   Resume Next
        Return False
    End Function

    ' ******************************************************************************************************************************
    ' * procedure name: BuildXTLfromTimeLine
    ' * procedure description: compiles an XTL timeline file from a timeline object - returns true is successful..
    ' *
    ' ******************************************************************************************************************************    '
    Public Function BuildXTLfromTimeLine(ByVal TimeLineObject As AMTimeline, ByVal szFileDest As String) As Boolean
        Dim bstrFileName As String
        Dim objXml2Dex As Xml2Dex
        On Error GoTo ErrLine

        'obtain a reference to the filtergraph manager
        If Not TimeLineObject Is Nothing Then
            If Not TimeLineObject Is Nothing Then
                'set the timeline object
                'Call gbl_objRenderEngine.SetTimelineObject(TimeLineObject)
                'render the timeline
                Call SaveTimeline(TimeLineObject, szFileDest, DEXExportFormatEnum.DEXExportXTL)
            End If
        End If

        Return True

ErrLine:
        Err.Clear()
        Resume Next
        Return False

    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: BuildGRFfromTimeLine
    ' * procedure description: compiles a GRF timeline file from a timeline object - returns true is successful..
    ' *
    ' ******************************************************************************************************************************    '
    Public Function BuildGRFfromTimeLine(ByVal TimeLineObject As AMTimeline, ByVal szFileDest As String) As Boolean
        Dim bstrFileName As String
        Dim objXml2Dex As Xml2Dex
        On Error GoTo ErrLine

        'obtain a reference to the filtergraph manager
        If Not TimeLineObject Is Nothing Then
            If Not TimeLineObject Is Nothing Then
                'set the timeline object
                '            Call gbl_objRenderEngine.SetTimelineObject(TimeLineObject)
                'render the timeline
                Call SaveTimeline(TimeLineObject, szFileDest, DEXExportFormatEnum.DEXExportGRF)
            End If
        End If

        Return True

ErrLine:
        Err.Clear()
        '  Resume Next
        Return False

    End Function

    Public Function ExportTimeLine2DV(ByVal TimeLineObject As AMTimeline) As QuartzTypeLib.FilgraphManager
        'Returns true is successful
        ExportTimeLine2DV = Nothing

        If BuildXTLfromTimeLine(TimeLineObject, Application.StartupPath & "\temp.xtl") Then

            Dim m_objMediaEvent As IMediaEvent = Nothing
            Dim objVideoWindow As IVideoWindow = Nothing
            Dim objMediaPosition As IMediaPosition = Nothing
            Dim objFilterGraphManager As FilgraphManager = Nothing
            Dim objXMLParser As New Xml2Dex

            'clean-up & dereference
            Call ClearTimeline(m_objTimeline)
            If Not m_objMediaEvent Is Nothing Then m_objMediaEvent = Nothing
            If Not m_objRenderEngine Is Nothing Then m_objRenderEngine = Nothing

            'reinstantiate the timeline & render engine
            m_objTimeline = New AMTimeline
            m_objRenderEngine = New SmartRenderEngine
            'read in the file
            Call objXMLParser.ReadXMLFile(m_objTimeline, Application.StartupPath & "\temp.xtl")
            'set the timeline
            m_objRenderEngine.SetTimelineObject(m_objTimeline)
            'connect the front
            m_objRenderEngine.ConnectFrontEnd()
            'render the output pins (e.g. 'backend')
            m_objRenderEngine.RenderOutputPins()
            ' ask for the graph, so we can control it
            Call m_objRenderEngine.GetFilterGraph(objFilterGraphManager)
            m_objRenderEngine.UseInSmartRecompressionGraph()
            'add export filters (muxers, devices)
            objFilterGraphManager = AddDVExportFilters(objFilterGraphManager)
            'return nothing if the export filters cannot be added to the objFilterGraphManager (no device found)
            If Not objFilterGraphManager Is Nothing Then
                m_objTimeline.GetDuration2(frmVPL(lastVPLFormUsed).ExportDVDuration)
                ExportTimeLine2DV = objFilterGraphManager
            Else
                ExportTimeLine2DV = Nothing
            End If

            If Not objFilterGraphManager Is Nothing Then objFilterGraphManager.Pause()

            'clean-up & dereference
            If Not objVideoWindow Is Nothing Then objVideoWindow = Nothing
            If Not objMediaPosition Is Nothing Then objMediaPosition = Nothing
            If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing
            If Not m_objMediaEvent Is Nothing Then m_objMediaEvent = Nothing
            If Not m_objRenderEngine Is Nothing Then m_objRenderEngine = Nothing
            If Not objXMLParser Is Nothing Then objXMLParser = Nothing

        End If
        If Not TimeLineObject Is Nothing Then TimeLineObject = Nothing

        Return ExportTimeLine2DV

    End Function


    Public Sub PlayPreviewFromTimeLine(ByVal TimeLineObject As AMTimeline)

        If BuildXTLfromTimeLine(TimeLineObject, Application.StartupPath & "\temp.xtl") Then

            Dim m_objMediaEvent As IMediaEvent = Nothing
            Dim objVideoWindow As IVideoWindow = Nothing
            Dim objMediaPosition As IMediaPosition = Nothing
            Dim objFilterGraphManager As FilgraphManager = Nothing
            Dim objXMLParser As New Xml2Dex

            'clean-up & dereference
            Call ClearTimeline(m_objTimeline)
            If Not m_objMediaEvent Is Nothing Then m_objMediaEvent = Nothing
            If Not m_objRenderEngine Is Nothing Then m_objRenderEngine = Nothing

            'reinstantiate the timeline & render engine
            m_objTimeline = New AMTimeline
            m_objRenderEngine = New RenderEngine
            'Set dynamic reconnect
            m_objRenderEngine.SetDynamicReconnectLevel(1)
            'read in the file
            Call objXMLParser.ReadXMLFile(m_objTimeline, Application.StartupPath & "\temp.xtl")
            'set the timeline
            m_objRenderEngine.SetTimelineObject(m_objTimeline)
            'connect the front
            m_objRenderEngine.ConnectFrontEnd()
            'render the output pins (e.g. 'backend')
            m_objRenderEngine.RenderOutputPins()
            ' ask for the graph, so we can control it
            Call m_objRenderEngine.GetFilterGraph(objFilterGraphManager)

            'if we have a valid instance of a filtergraph, run the graph
            If Not objFilterGraphManager Is Nothing Then
                Call objFilterGraphManager.Stop()
                objMediaPosition = objFilterGraphManager
                If Not objMediaPosition Is Nothing Then objMediaPosition.CurrentPosition = 0
                Call objFilterGraphManager.Run()
                m_objMediaEvent = objFilterGraphManager

                'derive an interface for the video window
                objVideoWindow = objFilterGraphManager
                If Not objVideoWindow Is Nothing Then
                    objVideoWindow.Visible = True
                    objVideoWindow.Left = 0
                    objVideoWindow.Top = 0
                End If

                Dim nResultant As Integer = 0
                'loop with events until the media has finished or the video
                'window has been manually closed (if the timeline has video)
                Do : Application.DoEvents()
                    'check state
                    'If Not m_objMediaEvent Is Nothing Then _
                    '   Call m_objMediaEvent.WaitForCompletion(10, nResultant)
                    'evaluate resultant
                    If nResultant = 1 Then  'EC_COMPLETE
                        If Not objVideoWindow Is Nothing Then
                            objVideoWindow.Visible = False
                        End If
                        If Not objFilterGraphManager Is Nothing Then _
                           Call objFilterGraphManager.Stop()
                        Exit Do
                    ElseIf objVideoWindow.Visible = False Then
                        If Not objFilterGraphManager Is Nothing Then _
                           Call objFilterGraphManager.Stop()
                        Exit Do
                    ElseIf m_objTimeline Is Nothing Then
                        Exit Do
                    ElseIf objFilterGraphManager Is Nothing Then
                        Exit Do
                    End If
                Loop
            End If


            'clean-up & dereference
            If Not objVideoWindow Is Nothing Then objVideoWindow = Nothing
            If Not objMediaPosition Is Nothing Then objMediaPosition = Nothing
            If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing
            If Not m_objMediaEvent Is Nothing Then m_objMediaEvent = Nothing
            If Not m_objRenderEngine Is Nothing Then m_objRenderEngine = Nothing
        End If
        If Not TimeLineObject Is Nothing Then TimeLineObject = Nothing
    End Sub

    Public Sub WriteFile3OLD(ByVal iSegmentCount As Integer, ByVal FileSource() As String, _
ByVal lStartPoint() As Double, ByVal lEndPoint() As Double, _
ByVal FileDest As String, Optional ByVal HasAudio As Boolean = False, Optional ByVal owner As Form = Nothing, _
Optional ByVal RepeatSlow As Boolean = False)

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

        Dim objTimeline As AMTimeline = Nothing
        Dim objSourceObj As AMTimelineObj = Nothing
        Dim objTrackObject As AMTimelineObj = Nothing
        Dim objAudioGroupObj As AMTimelineObj = Nothing
        Dim objVideoGroupObject As AMTimelineObj = Nothing

        Dim objSource As AMTimelineSrc = Nothing
        Dim objTrack As AMTimelineTrack = Nothing
        Dim objAudioGroup As AMTimelineGroup = Nothing
        Dim objVideoGroup As AMTimelineGroup = Nothing
        Dim objAudioComposition As AMTimelineComp = Nothing
        Dim objVideoComposition As AMTimelineComp = Nothing
        Dim objSmartRenderEngine As New SmartRenderEngine

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
        objTimeline = New AMTimeline
        'Create empty node on timeline for video
        objTimeline.CreateEmptyNode(objVideoGroupObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
        'Derive video group object from the timeline object.
        objVideoGroup = objVideoGroupObject
        'Set the media type of the video group.
        objVideoGroup.SetMediaTypeForVB(0)
        'Append the video group to the timeline.
        objTimeline.AddGroup(objVideoGroup)

        'Create empty node on timeline for the track.
        objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
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
        If RepeatSlow Then tCorrect = 1.66667

        'Set sources to track object in sequence.
        For i As Integer = 0 To iSegmentCount - 1

            'Create empty node on timeline for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive source clip from the timeline object.
            objSource = objSourceObj
            'Set Start and stop times to the source clip.
            objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + ((lEndPoint(i) - lStartPoint(i)) * tCorrect))
            objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
            objSource.SetMediaName(FileSource(i))
            'Append source clip to the track.
            objTrack.SrcAdd(objSourceObj)
            dTimeOnTrack = dTimeOnTrack + ((lEndPoint(i) - lStartPoint(i)) * tCorrect)

            If RepeatSlow Then
                'Create empty node on timeline for the source clip.
                objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                'Derive source clip from the timeline object.
                objSource = objSourceObj
                'Set Start and stop times to the source clip.
                objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (4 * (lEndPoint(i) - lStartPoint(i))))
                objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                objSource.SetMediaName(FileSource(i))
                'Append source clip to the track.
                objTrack.SrcAdd(objSourceObj)
                dTimeOnTrack = dTimeOnTrack + (4 * (lEndPoint(i) - lStartPoint(i)))
            End If
        Next

        Dim srcMediaSource As New MediaDet
        If RepeatSlow Then
            'Set the recompression format of the video group.
            'Get media type
            srcMediaSource.Filename = FileSource(0)
            Dim srcMediaType As _AMMediaType = srcMediaSource.StreamMediaType
            objVideoGroup.SetMediaType(srcMediaType)
        Else
            objVideoGroup.SetRecompFormatFromSource(objSource)
        End If


        'Check for and facillitate audio.
        If HasAudio And Not RepeatSlow Then
            'Create an empty node on the timeline for the audio group.
            objTimeline.CreateEmptyNode(objAudioGroupObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_GROUP)
            'Derive the audio group from the timeline object.
            objAudioGroup = objAudioGroupObj
            'Set the media type of the audio group.
            objAudioGroup.SetMediaTypeForVB(1)
            'Append the group to the timeline.
            objTimeline.AddGroup(objAudioGroup)

            'Create an empty node on the timeline for the audio track.
            objTimeline.CreateEmptyNode(objTrackObject, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_TRACK)
            'Derive a composition from the audio group.
            objAudioComposition = objAudioGroup
            'Insert the track into the composition
            objAudioComposition.VTrackInsBefore(objTrackObject, -1)
            'Derive a track object from the timeline object.
            objTrack = objTrackObject

            'Create an empty node for the source clip.
            objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
            'Derive a source object from the timeline object
            objSource = objSourceObj
            'Reset time on track object.
            dTimeOnTrack = 0
            'Set sources to track object in sequence.
            For i As Integer = 0 To iSegmentCount - 1
                If GetMediaAudio(FileSource(i)) Then
                    'Create empty node on timeline for the source clip.
                    objTimeline.CreateEmptyNode(objSourceObj, TIMELINE_MAJOR_TYPE.TIMELINE_MAJOR_TYPE_SOURCE)
                    'Derive source clip from the timeline object.
                    objSource = objSourceObj
                    'Set Start and stop times to the source clip.
                    objSourceObj.SetStartStop2(dTimeOnTrack, dTimeOnTrack + (lEndPoint(i) - lStartPoint(i)))
                    objSource.SetMediaTimes2(lStartPoint(i), lEndPoint(i))
                    objSource.SetMediaName(FileSource(i))
                    'Append source clip to the track.
                    objTrack.SrcAdd(objSourceObj)
                    dTimeOnTrack = dTimeOnTrack + (lEndPoint(i) - lStartPoint(i))
                End If
            Next
        End If

        '*
        '*  Set FilterGraph "Front End" --> SmartRecompressor..
        '*
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
        'Run the graph
        objFilterGraphManager.Run()
        'Obtain a media event
        objMediaEvent = objFilterGraphManager
        'Obtain the position within the graph
        objMediaPosition = objFilterGraphManager

        GraphStatus = GraphState.StateRunning
        owner.Text = "Compiling video file now... (Please wait)"

        Do : Application.DoEvents()
            If Not objMediaEvent Is Nothing Then
                Call objMediaEvent.WaitForCompletion(1000, nReturnCode)
                If nReturnCode = 1 Then Exit Do

            Else : Exit Do
            End If
        Loop

        GraphStatus = GraphState.StateStopped
        Beep()
        owner.Text = "Done"


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
        If Not srcMediaSource Is Nothing Then srcMediaSource = Nothing
        'clean-up & dereference dexter timeline object(s)
        If Not objTrack Is Nothing Then objTrack = Nothing
        If Not objSource Is Nothing Then objSource = Nothing
        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing
        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing
        If Not objAudioComposition Is Nothing Then objAudioComposition = Nothing
        If Not objVideoComposition Is Nothing Then objVideoComposition = Nothing
        If Not objSmartRenderEngine Is Nothing Then objSmartRenderEngine = Nothing

        Exit Sub

ErrLine:

        Select Case Err.Number
            Case 5 'Invalid procedure call or argument
                Call MsgBox("Error creating file.  Verify that the start/stop times are valid before continuing.", vbExclamation + vbApplicationModal, Application.ProductName)
                Err.Clear() : GoTo CleanUp
            Case 287 'Application-defined or object-defined error
                Err.Clear() : Resume Next
            Case -2147024864 'The process cannot access the file because it is being used by another process.
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
            Case Else 'unknown error
                Call MsgBox(Err.Description, vbExclamation + vbApplicationModal, Application.ProductName) : Err.Clear() : GoTo CleanUp
        End Select
        Exit Sub
    End Sub


    ' **************************************************************************************************************************************
    ' * PUBLIC INTERFACE- QUARTZ PROCEDURES
    ' *
    ' *
    ' ******************************************************************************************************************************
    ' * procedure name: RenderTimeline
    ' * procedure description: renders the timeline for the client and returns an instance of the filter graph manager
    ' *                                      NOTE:  THIS PROCEDURE USES MODULE-LEVEL VARIABLES BECAUSE
    ' *                                                   IT WORKS ASYNCRONOUSLY.  IF YOU MOVE THEM OVER LOCALLY
    ' *                                                   YOUR APPLICATION WILL TAKE A READ FAULT BECAUSE QEDIT WILL
    ' *                                                   NOT BE ABLE TO READ YOUR FILTERGRAPH WHEN THE PROCEDURE EXITS.
    ' ******************************************************************************************************************************
    Public Function RenderTimeline(ByVal objTimeline As AMTimeline, Optional ByVal UseDynamicConnections As Boolean = True, Optional ByVal UseSmartRecompression As Boolean = True) As FilgraphManager
        On Error GoTo ErrLine
        Dim m_objFilterGraph As IGraphBuilder = Nothing
        Dim m_objFilterGraphManager As New QuartzTypeLib.FilgraphManager

        'instantiate new render engine
        m_objRenderEngine = New RenderEngine

        'setup dynamic connections
        If UseDynamicConnections = True Then
            Call m_objRenderEngine.SetDynamicReconnectLevel(1)
        Else : Call m_objRenderEngine.SetDynamicReconnectLevel(0)
        End If

        'setup smart recompression
        If UseSmartRecompression = True Then
            'smart recompression is not currently supported in vb
        End If

        'connect everything up..
        Call m_objRenderEngine.SetTimelineObject(objTimeline)
        m_objRenderEngine.ConnectFrontEnd()
        m_objRenderEngine.RenderOutputPins()

        'render the audio/video
        Call m_objRenderEngine.GetFilterGraph(m_objFilterGraph)
        m_objFilterGraphManager = New FilgraphManager
        m_objFilterGraphManager = m_objFilterGraph

        'return an instance of the filgraph manager to the client
        RenderTimeline = m_objFilterGraphManager
        GoTo CleanUp

ErrLine:

        Err.Clear()
        RenderTimeline = Nothing

CleanUp:
        If Not m_objFilterGraph Is Nothing Then m_objFilterGraph = Nothing
        If Not m_objFilterGraphManager Is Nothing Then m_objFilterGraphManager = Nothing

    End Function


    ' ******************************************************************************************************************************
    ' * procedure name: RenderTimelineSync
    ' * procedure description: Renders the timeline for the client, and waits for completion
    ' *                                      until the media completes or until the specified timeout is reached..
    ' *
    ' ******************************************************************************************************************************
    Public Sub RenderTimelineSync(ByVal objTimeline As AMTimeline, Optional ByVal Timeout As Long = -1)
        Dim nExitCode As Long
        Dim objMediaEvent As IMediaEvent
        Dim objMediaControl As IMediaControl
        Dim objFilterGraph As IGraphBuilder = Nothing
        Dim objRenderEngine As RenderEngine
        On Error GoTo ErrLine

        'instantiate new render engine
        objRenderEngine = New RenderEngine

        'connect everything up..
        Call objRenderEngine.SetTimelineObject(objTimeline)
        objRenderEngine.ConnectFrontEnd()
        objRenderEngine.RenderOutputPins()

        'obtain the filtergraph
        Call objRenderEngine.GetFilterGraph(objFilterGraph)
        objMediaEvent = objFilterGraph
        objMediaControl = objFilterGraph

        'render the graph
        objMediaControl.Run()
        'wait for the graph to complete..
        objMediaEvent.WaitForCompletion(Timeout, nExitCode)

        'clean-up & dereference
        If Not objFilterGraph Is Nothing Then objFilterGraph = Nothing
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objMediaControl Is Nothing Then objMediaControl = Nothing
        If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Exit Sub
    End Sub


    ' ******************************************************************************************************************************
    ' * procedure name: RenderTimelineAsync
    ' * procedure description: renders the timeline for the client in true async fashion
    ' *                                      NOTE:  THIS PROCEDURE USES MODULE-LEVEL VARIABLES BECAUSE
    ' *                                                   IT WORKS ASYNCRONOUSLY.  IF YOU MOVE THEM OVER LOCALLY
    ' *                                                   YOUR APPLICATION WILL TAKE A READ FAULT BECAUSE QEDIT WILL
    ' *                                                   NOT BE ABLE TO READ YOUR FILTERGRAPH WHEN THE PROCEDURE EXITS.
    ' ******************************************************************************************************************************
    Public Sub RenderTimelineAsync(ByVal objTimeline As AMTimeline)
        On Error GoTo ErrLine

        ''instantiate new render engine
        'gbl_objRenderEngine = New RenderEngine

        ''connect everything up..
        'Call gbl_objRenderEngine.SetTimelineObject(objTimeline)
        'gbl_objRenderEngine.ConnectFrontEnd()
        'gbl_objRenderEngine.RenderOutputPins()

        ''render the audio/video
        'Call gbl_objRenderEngine.GetFilterGraph(m_objFilterGraph)
        'm_objFilterGraphManager = New FilgraphManager
        'm_objFilterGraphManager = m_objFilterGraph
        'm_objFilterGraphManager.Run()
        'Exit Sub

ErrLine:

        Err.Clear()
        Exit Sub
    End Sub



    ' ******************************************************************************************************************************
    ' * procedure name: RenderTimelineQuasiAsync
    ' * procedure description: Renders the timeline for the client, and waits for completion by using
    ' *                                      a structured loop which constantly checks for EC_COMPLETE to occur.
    ' *                                      By using the VB 'WithEvents' Keyword normal form events are uninhibited.
    ' *                                      as VB will basically handle the multi-threading for you.
    ' *                                      To Stop playback use recursion with syntax: Call RenderTimelineQuasiAsync(Nothing)
    ' ******************************************************************************************************************************
    Public Sub RenderTimelineQuasiAsync(ByVal objTimeline As AMTimeline)
        Static nResultant As Long
        Static objPosition As IMediaPosition
        Static objMediaEvent As IMediaEvent
        Static objFilterGraph As IGraphBuilder
        Static objVideoWindow As IVideoWindow
        Static objRenderEngine As RenderEngine
        Static objFilterGraphManager As New FilgraphManager
        On Error GoTo ErrLine

        If Not objTimeline Is Nothing Then
            'instantiate new render engine
            objRenderEngine = New RenderEngine

            'connect everything up..
            Call objRenderEngine.SetTimelineObject(objTimeline)
            objRenderEngine.ConnectFrontEnd()
            objRenderEngine.RenderOutputPins()

            'render the audio/video
            Call objRenderEngine.GetFilterGraph(objFilterGraph)
            objFilterGraphManager = New FilgraphManager
            objFilterGraphManager = objFilterGraph
            objFilterGraphManager.Run()
            'obtain the position of audio/video
            objPosition = objFilterGraphManager
            'obtain the video window
            objMediaEvent = objFilterGraphManager
            objVideoWindow = objMediaEvent

            'loop with events until the media has finished or the video
            'window has been manually closed (if the timeline has video)
            Do : Application.DoEvents()
                'check state
                If Not objMediaEvent Is Nothing Then _
                   Call objMediaEvent.WaitForCompletion(100, nResultant)
                'evaluate resultant
                If nResultant = 1 Then  'EC_COMPLETE
                    If Not objVideoWindow Is Nothing Then
                        objVideoWindow.Visible = False
                    End If
                    If Not objFilterGraphManager Is Nothing Then _
                       Call objFilterGraphManager.Stop()
                    Exit Do
                ElseIf objVideoWindow.Visible = False Then
                    If Not objFilterGraphManager Is Nothing Then _
                       Call objFilterGraphManager.Stop()
                    Exit Do
                ElseIf objTimeline Is Nothing Then
                    Exit Do
                ElseIf objFilterGraphManager Is Nothing Then
                    Exit Do
                End If
            Loop
        Else : nResultant = 1
        End If
        objFilterGraphManager.Stop()

        'clean-up & dereference
        If Not objPosition Is Nothing Then objPosition = Nothing
        If Not objFilterGraph Is Nothing Then objFilterGraph = Nothing
        If Not objMediaEvent Is Nothing Then objMediaEvent = Nothing
        If Not objVideoWindow Is Nothing Then objVideoWindow = Nothing
        If Not objRenderEngine Is Nothing Then objRenderEngine = Nothing
        If Not objFilterGraphManager Is Nothing Then objFilterGraphManager = Nothing

        ClearTimeline(objTimeline)
        If Not objTimeline Is Nothing Then objTimeline = Nothing
        Exit Sub

ErrLine:
        Err.Clear()
        Resume Next
        Exit Sub
    End Sub



    ' **************************************************************************************************************************************
    ' * PRIVATE INTERFACE- PROCEDURES
    ' *
    ' *
    ' ******************************************************************************************************************************
    ' * procedure name: SpliceVideo
    ' * procedure description:  Splices a variable number of video files together using the given transition.
    ' *                                       DefaultTransitionCLSID evaluates to the CLSID of the desired transition to use.
    ' *                                       Files evaluates to a variable number of BSTR String arguments containing the filename(s)
    ' ******************************************************************************************************************************
    '    Private Function SpliceVideo(ByVal DefaultTransitionCLSID As String, ByVal ParamArray Files()) As AMTimeline
    '        Dim nCount As Long
    '        Dim nCount2 As Long
    '        Dim bstrCurrentFile As String
    '        Dim boolAudioGroup As Boolean
    '        Dim boolVideoGroup As Boolean
    '        Dim dblAudioStartTime As Double
    '        Dim dblAudioStopTime As Double
    '        Dim dblVideoStartTime As Double
    '        Dim dblVideoStopTime As Double
    '        Dim objTimeline As AMTimeline
    '        Dim objNewSource As AMTimelineSrc
    '        Dim objNewTrack As AMTimelineTrack
    '        Dim objTransition As AMTimelineTrans
    '        Dim objAudioGroup As AMTimelineGroup
    '        Dim objVideoGroup As AMTimelineGroup
    '        Dim objTimelineTrackObject As AMTimelineObj
    '        Dim objTimelineSourceObject As AMTimelineObj
    '        Dim objTimelineAudioGroupObject As AMTimelineObj
    '        Dim objTimelineVideoGroupObject As AMTimelineObj
    '            On Local Error GoTo ErrLine

    '        'instantiate new timeline
    '        objTimeline = CreateTimeline

    '        'enable transitions on the timeline
    '        Call objTimeline.EnableTransitions(1)

    '        'enumerate the files and place the group(s) on the timeline
    '        For nCount = LBound(Files) To UBound(Files)
    '            If TypeName(Files(nCount)) = "String" Then
    '                If Files(nCount) <> vbNullString Then
    '                    bstrCurrentFile = Files(nCount)
    '                    If HasStreams(bstrCurrentFile) Then
    '                        If HasAudioStream(bstrCurrentFile) Then
    '                            'enumerate all the groups in the timeline to ensure audio has not yet been added
    '                            If GetGroupCount(objTimeline) > 0 Then
    '                                For nCount2 = 0 To GetGroupCount(objTimeline) - 1
    '                                    If Not GroupFromTimeline(objTimeline, nCount2) Is Nothing Then
    '                                        If GroupFromTimeline(objTimeline, nCount2).GetGroupName = "AUDIO" Then
    '                                            boolAudioGroup = True
    '                                        End If
    '                                    End If
    '                                Next
    '                                If boolAudioGroup = False Then
    '                                    'insert an audio group into the timeline
    '                                    objAudioGroup = CreateGroup(objTimeline, "AUDIO", DEXMediaTypeAudio)
    '                                    Call InsertGroup(objTimeline, objAudioGroup)
    '                                    objTimelineAudioGroupObject = objAudioGroup
    '                                    boolAudioGroup = True
    '                                End If
    '                            Else
    '                                'insert an audio group into the timeline
    '                                objAudioGroup = CreateGroup(objTimeline, "AUDIO", DEXMediaTypeAudio)
    '                                Call InsertGroup(objTimeline, objAudioGroup)
    '                                objTimelineAudioGroupObject = objAudioGroup
    '                                boolAudioGroup = True
    '                            End If
    '                        End If

    '                        If HasVideoStream(bstrCurrentFile) Then
    '                            'enumerate all the groups in the timeline to ensure audio has not yet been added
    '                            If GetGroupCount(objTimeline) > 0 Then
    '                                For nCount2 = 0 To GetGroupCount(objTimeline) - 1
    '                                    If Not GroupFromTimeline(objTimeline, nCount2) Is Nothing Then
    '                                        If GroupFromTimeline(objTimeline, nCount2).GetGroupName = "VIDEO" Then
    '                                            boolVideoGroup = True
    '                                        End If
    '                                    End If
    '                                Next
    '                                If boolVideoGroup = False Then
    '                                    'insert a video group into the timeline
    '                                    objVideoGroup = CreateGroup(objTimeline, "VIDEO", DEXMediaTypeVideo)
    '                                    Call InsertGroup(objTimeline, objVideoGroup)
    '                                    objTimelineVideoGroupObject = objVideoGroup
    '                                    boolVideoGroup = True
    '                                End If
    '                            Else
    '                                'insert a video group into the timeline
    '                                objVideoGroup = CreateGroup(objTimeline, "VIDEO", DEXMediaTypeVideo)
    '                                Call InsertGroup(objTimeline, objVideoGroup)
    '                                objTimelineVideoGroupObject = objVideoGroup
    '                                boolVideoGroup = True
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next

    '        'enumerate the files and place the tracks/source(s) on the timeline
    '        For nCount = LBound(Files) To UBound(Files)
    '            If TypeName(Files(nCount)) = "String" Then
    '                If Files(nCount) <> vbNullString Then
    '                    bstrCurrentFile = Files(nCount)
    '                    If HasVideoStream(bstrCurrentFile) Then
    '                        'insert a new video track for the clip in the timeline
    '                        objNewTrack = CreateTrack(objTimeline)
    '                        objTimelineTrackObject = objNewTrack
    '                        Call InsertTrack(objNewTrack, objTimelineVideoGroupObject)
    '                        'inset a new sourceclip into the timeline
    '                        objNewSource = CreateSource(objTimeline)
    '                        'insert the new source clip into the new track
    '                        If dblVideoStopTime = 0 Then
    '                            dblVideoStartTime = m_nMaximumClipLength * (nCount) : dblVideoStopTime = (m_nMaximumClipLength * (nCount + 1)) + 1
    '                        Else : dblVideoStartTime = (m_nMaximumClipLength * (nCount)) - 1 : dblVideoStopTime = (m_nMaximumClipLength * (nCount + 1)) + 1
    '                        End If
    '                        Call InsertSource(objNewTrack, objNewSource, bstrCurrentFile, dblVideoStartTime, dblVideoStopTime)
    '                        'insert a new transition into each track on the timeline
    '                        If DefaultTransitionCLSID <> vbNullString Then
    '                            objTransition = CreateTransition(objTimeline)
    '                            dblVideoStartTime = ((m_nMaximumClipLength * (nCount))) - 1 : dblVideoStopTime = (m_nMaximumClipLength * nCount + 1)
    '                            If dblVideoStartTime < 0 Then dblVideoStartTime = 0
    '                            Call InsertTransition(objTransition, objTimelineTrackObject, DefaultTransitionCLSID, dblVideoStartTime, dblVideoStopTime)
    '                        End If
    '                    End If

    '                    If HasAudioStream(bstrCurrentFile) Then
    '                        'insert a new audio track for the clip in the timeline
    '                        objNewTrack = CreateTrack(objTimeline)
    '                        objTimelineTrackObject = objNewTrack
    '                        Call InsertTrack(objNewTrack, objTimelineAudioGroupObject)
    '                        'inset a new sourceclip into the timeline
    '                        objNewSource = CreateSource(objTimeline)
    '                        'insert the new source clip into the new track
    '                        If dblAudioStopTime = 0 Then
    '                            dblAudioStartTime = m_nMaximumClipLength * (nCount) : dblAudioStopTime = (m_nMaximumClipLength * (nCount + 1)) + 1
    '                        Else : dblAudioStartTime = (m_nMaximumClipLength * (nCount)) - 1 : dblAudioStopTime = (m_nMaximumClipLength * (nCount + 1)) + 1
    '                        End If
    '                        Call InsertSource(objNewTrack, objNewSource, bstrCurrentFile, dblAudioStartTime, dblAudioStopTime)
    '                    End If
    '                End If
    '            End If
    '        Next

    '        'return the timeline
    '        If Not objTimeline Is Nothing Then SpliceVideo = objTimeline

    '        'clean-up & dereference
    '        If Not objTimeline Is Nothing Then objTimeline = Nothing ' AMTimeline
    '        If Not objNewSource Is Nothing Then objNewSource = Nothing ' AMTimelineSrc
    '        If Not objNewTrack Is Nothing Then objNewTrack = Nothing ' AMTimelineTrack
    '        If Not objTransition Is Nothing Then objTransition = Nothing ' AMTimelineTrans
    '        If Not objAudioGroup Is Nothing Then objAudioGroup = Nothing ' AMTimelineGroup
    '        If Not objVideoGroup Is Nothing Then objVideoGroup = Nothing ' AMTimelineGroup
    '        If Not objTimelineTrackObject Is Nothing Then objTimelineTrackObject = Nothing ' AMTimelineObj
    '        If Not objTimelineSourceObject Is Nothing Then objTimelineSourceObject = Nothing ' AMTimelineObj
    '        If Not objTimelineAudioGroupObject Is Nothing Then objTimelineAudioGroupObject = Nothing ' AMTimelineObj
    '        If Not objTimelineVideoGroupObject Is Nothing Then objTimelineVideoGroupObject = Nothing ' AMTimelineObj
    '        Exit Function

    'ErrLine:
    '        Err.Clear()
    '        Exit Function
    '    End Function


End Module
