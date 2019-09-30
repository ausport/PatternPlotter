Public Class clsDV
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*
    '*
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                    DV_PLAY CLASS.
    '* * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*     Stuart Morgan
    '*     www.in2sport.com.au
    '*     Copyright (C)2004
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*  Revision History:
    '*
    '*  Created in version 1.1
    '*      1.1.0
    '*      * Basic functions.
    '*
    '*      1.2.0
    '*      * Mismatch between single & double declarations for video timepoints fixed..
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Const ThisVersion = "2.1"
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*
    '*
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*              DV_PLAY CLASS DECLARATIONS.
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*
    '*
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Private objVideo As QuartzTypeLib.IBasicVideo         'Basic Video Object
    Private posMedia As QuartzTypeLib.IMediaPosition      'MediaPosition Object
    Private evtMedia As QuartzTypeLib.IMediaEvent         'MediaEvent Object
    Private objMControl As QuartzTypeLib.IMediaControl    'MediaControl Object
    Private vidWindow As QuartzTypeLib.IVideoWindow       'VideoWindow Object
    Private objAudio As QuartzTypeLib.IBasicAudio        'Basic Audio Object
    Private objMediaDet As DexterLib.MediaDet         'Media Details Object

    Private Const VIDEO_CLSID As String = "{73646976-0000-0010-8000-00AA00389B71}"  'video clsid
    Private Const AUDIO_CLSID As String = "{73647561-0000-0010-8000-00AA00389B71}"  'audio clsid

    'local variable(s) to hold property value(s)
    Private mvarAutoPlay As Boolean
    Private mvarDuration As Double
    Private mvarfilename As String
    Private mvarFullScreen As Boolean
    Private mvarPlaySpeed As Single
    Private mvarSourceHeight As Single
    Private mvarSourceWidth As Single
    Private mvarSourceTpFrame As Single
    Private mvarSourceBitRate As Single
    Private mvarCurrentPosition As Double
    Private mvarBOF As Boolean
    Private mvarEOF As Boolean
    Private mvarMedia_StreamCount As Integer
    Private mvarMedia_HasAudio As Boolean
    Private mvarMedia_HasVideo As Boolean
    Private mvarFPS As Single
    Private mvarPlaying As Boolean
    Private mvarVidStream As Long
    Private mvarMuteAudio As Boolean
    Private mvarVolume As Integer
    Private mvarOverlayIsActive As Boolean
    Private mvarVisible As Boolean


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*              DV_PLAY CLASS SUBROUTINES.             *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public Function SetDestPosition(ByVal dimensions As RectangleF) As Boolean
        Try
            vidWindow.SetWindowPosition(dimensions.Left, dimensions.Top, dimensions.Width, dimensions.Height)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub SetDisplayWidth(sWidth As Single):
    '*  Set Output window dimensions to value of sWidth in pixels.
    '*
    '*  Args:
    '*      sWidth as Single: desired display width in pixels
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub SetDisplayWidth(ByVal sWidth As Single)
        On Error Resume Next
        'objVideo.DestinationWidth = sWidth
        'vidWindow.Width = sWidth
        'objVideo.SetDestinationPosition(100, 100, 100, 100)
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub SetDisplayHeight(sHeight As Single):
    '*  Set Output window dimensions to value of sHeight in pixels.
    '*
    '*  Args:
    '*      sHeight as Single: desired display height in pixels
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub SetDisplayHeight(ByVal sHeight As Single)
        On Error Resume Next
        objVideo.DestinationHeight = sHeight
        vidWindow.Height = sHeight
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Public Sub SetDisplayLeft(sLeft As Single):
    '*  Set Output window dimensions to value of sLeft in pixels.
    '*
    '*  Args:
    '*      sWidth as Single: desired display width in pixels
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub SetDisplayLeft(ByVal sLeft As Single)
        On Error Resume Next
        objVideo.DestinationLeft = sLeft
        vidWindow.Left = sLeft
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub SetDisplayTop(nTop As Single):
    '*  Set Output window dimensions to value of nTop in pixels.
    '*
    '*  Args:
    '*      nTop as Single: desired display top in pixels
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub SetDisplayTop(ByVal nTop As Single)
        On Error Resume Next
        objVideo.DestinationTop = nTop
        vidWindow.Top = nTop
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub ShowFilterProps(Obj As Object, szFilter As String):
    '*  Instantiates a filter properties dialog for selected filter.
    '*
    '*  Args:
    '*      Obj As Object: List object to be populated by filters in.
    '*      szFilter as String: Name of filter eg. "DV Video Decoder"
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub ShowFilterProps(ByVal obj As Object, ByVal szFilter As String)
        On Error GoTo errCatch

        Dim gGraph As QuartzTypeLib.IMediaControl
        Dim gRegFilters As Object
        Dim selFilter As QuartzTypeLib.IFilterInfo
        Dim filter As QuartzTypeLib.IRegFilterInfo

        gGraph = New QuartzTypeLib.FilgraphManager
        gRegFilters = gGraph.RegFilterCollection

        'Find filter an instantiate properties page.
        For Each filter In gRegFilters
            If filter.Name = szFilter Then
                selFilter = Nothing
                filter.Filter(selFilter)
                Dim fEx As FSFWRAPLib.SinkInfo
                fEx = New FSFWRAPLib.SinkInfo

                With fEx
                    .Filter = selFilter
                    .ShowPropPage(obj.hWnd)
                End With
                Exit For
            End If
        Next filter

        gGraph = Nothing
        gRegFilters = Nothing

        Exit Sub

errCatch:
        MsgBox("Properties not available for that filter.", vbCritical, Application.ProductName)
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub ShowFilterList(listObject As Object):
    '*  Enumerates current filter graph to list object.
    '*
    '*  Args:
    '*      listObject As Object: List object to be populated by filters.
    '*
    '*  Returns:
    '*      None
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub ShowFilterList(ByVal listObject As Object)
        Dim filt As QuartzTypeLib.IFilterInfo
        On Error Resume Next

        listObject.Clear()

        For Each filt In objMControl.FilterCollection
            listObject.AddItem(filt.Name)
        Next

    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Function LoadClip(szFileName As String, objDisplay As Object) As Boolean:
    '*  Loads and instantiates class for selected media clip.
    '*
    '*  Args:
    '*      szFileName As String: Path & filename.
    '*      objDisplay As Object: Display-capable object to recieve video clip.
    '*
    '*  Returns:
    '*      True if no errors.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function LoadClip(ByVal szFileName As String, ByVal objDisplay As Form) As Boolean
        On Error GoTo ErrLine

        'Instantiate a filter graph for the requested file format.
        objMControl = New QuartzTypeLib.FilgraphManager
        Call objMControl.RenderFile(szFileName)

        'If successful - assign filename to FileName property.
        mvarfilename = szFileName

        'Calling GetVidStream() updates the Media_HasAudio, Media_StreamCount & FPS properties
        mvarVidStream = GetVidStream()

        'If Not mvarMedia_HasVideo Then MsgBox("This file does not contain a video stream.", vbExclamation, "dvClass Module") : LoadClip = False : Exit Function
        Dim objFilters As QuartzTypeLib.FilgraphManager
        objFilters = objMControl

        For Each obj As QuartzTypeLib.IRegFilterInfo In objFilters.RegFilterCollection
            Debug.Print(obj.Name)
        Next
        objVideo = objMControl

        objAudio = objMControl

        'Setup the IVideoWindow object.
        vidWindow = objMControl
        With vidWindow
            .WindowStyle = CLng(&H6000000)
            .Top = 0
            .Left = 0
            .Width = objVideo.VideoWidth
            .Height = objVideo.VideoHeight

            mvarSourceHeight = objVideo.VideoHeight
            mvarSourceWidth = objVideo.VideoWidth
            mvarSourceTpFrame = 1 / mvarFPS
            mvarSourceBitRate = objVideo.BitRate

            .Owner = objDisplay.Handle

            'SB Added: Pass messages to the objDisplay window
            .MessageDrain = objDisplay.Handle

            '.FullScreenMode = CLng(mvarFullScreen)
            .SetWindowForeground(CLng(True))

            .Height = mvarSourceHeight
            .Width = mvarSourceWidth

        End With

        'Setup the IMediaEvent object for event control.
        evtMedia = objMControl

        'Setup the IMediaPosition object for position control.
        posMedia = objMControl

        'Set default play speed. (x1)
        posMedia.Rate = mvarPlaySpeed

        'Set stream duration property
        mvarDuration = posMedia.Duration

        'objMediaDet = Nothing

        LoadClip = True
        Exit Function

ErrLine:
        '    MsgBox "Errors occured in dvClass::LoadClip", vbCritical
        '    MsgBox Err.Description
        LoadClip = False
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub FrameAdvance(Optional jump As double):
    '*  Step forward - default is 1 frame.
    '*
    '*  Args:
    '*      jump As double (Optional): jump by speicified time.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function FrameAdvance(Optional ByVal jump As Double = 0) As Double
        On Error Resume Next
        objMControl.Pause()
        mvarPlaying = False

        If jump = 0 Then jump = mvarSourceTpFrame

        If posMedia.CurrentPosition + jump <= posMedia.Duration Then
            posMedia.CurrentPosition = posMedia.CurrentPosition + jump
        Else
            posMedia.CurrentPosition = posMedia.Duration
        End If
        FrameAdvance = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub FrameBack(Optional jump As double):
    '*  Step backwards - default is 1 frame.
    '*
    '*  Args:
    '*      jump As double (Optional): jump by speicified time.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function FrameBack(Optional ByVal jump As Double = 0) As Double
        On Error Resume Next
        objMControl.Pause()
        mvarPlaying = False

        If jump = 0 Then jump = mvarSourceTpFrame

        If posMedia.CurrentPosition - jump >= 0 Then
            posMedia.CurrentPosition = posMedia.CurrentPosition - jump
        Else
            posMedia.CurrentPosition = 0
        End If
        FrameBack = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub Play():
    '*  Plays current filter graph..
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub StartPlay()
        On Error Resume Next

        objMControl.Run()
        mvarPlaying = True
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub StopPlay():
    '*  Stops current filter graph & returns current positon to start (0).
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Sub StopPlay()
        On Error Resume Next
        objMControl.Stop()

        posMedia.CurrentPosition = 0
        objMControl.Pause()
        mvarPlaying = False
    End Sub

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub PausePlay():
    '*  Pauses current filter graph - current positon remains at last point.
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function PausePlay() As Double
        On Error Resume Next
        objMControl.Pause()
        mvarPlaying = False
        PausePlay = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub ToEnd():
    '*  Sets current positon at last point.
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function ToEnd() As Double
        On Error Resume Next
        objMControl.Pause()
        posMedia.CurrentPosition = posMedia.Duration
        mvarPlaying = False
        ToEnd = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub ToStart():
    '*  Sets current positon at first point.
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function ToStart() As Double
        On Error Resume Next
        objMControl.Pause()
        posMedia.CurrentPosition = 0
        mvarPlaying = False
        ToStart = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub ToFrame():
    '*  Sets current positon at n point.
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      None.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function ToFrame(ByVal newPos As Double) As Double
        On Error Resume Next
        objMControl.Pause()
        posMedia.CurrentPosition = newPos
        mvarPlaying = False
        ToFrame = posMedia.CurrentPosition
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Function SnapShot(szBitmapFileName As String, Optional cPos As Long, Optional Confirm As Boolean) As Boolean:
    '*  Grabs frame to bitmap.
    '*
    '*  Args:
    '*      szBitmapFileName As String: Bitmap filename - .bmp extension required.
    '*      Xpixels as long: number of pixels width.
    '*      Ypixels as long: number of pixels height.
    '*      cPos As Long (Optional): frame number representing current position. (Default = 0)
    '*      Confirm As Boolean (Optional): If true, presents confirmation dialog after file saved.
    '*
    '*  Returns:
    '*      True if no errors.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function SnapShot(ByVal szBitmapFileName As String, ByVal Xpixels As Long, _
    ByVal Ypixels As Long, Optional ByVal cPos As Double = 0, _
    Optional ByVal Confirm As Boolean = False) As Boolean

        Dim SnapDet As DexterLib.MediaDet
        Dim intVidStream As Long

        On Error Resume Next

        SnapDet = New DexterLib.MediaDet

        'Check for BMP extension.
        If Right(szBitmapFileName, 3) <> "bmp" And Right(szBitmapFileName, 3) <> ".BMP" Then
            MsgBox("File extension not '.bmp'", vbCritical, Application.ProductName)
            SnapShot = False
            Exit Function
        End If

        'Get video stream info.
        intVidStream = GetVidStream()

        'Go to specified frame position.
        If Len(cPos) > 0 Then
            posMedia.CurrentPosition = cPos
        End If

        'Catch err is no video stream
        If intVidStream = -1 Then
            MsgBox("The selected file does not contain a video stream.", vbExclamation, Application.ProductName)
            SnapShot = False
            Exit Function
        Else
            'Write bitmap bits to file.
            SnapDet.FileName = mvarfilename
            SnapDet.CurrentStream = intVidStream
            SnapDet.WriteBitmapBits(CDbl(cPos), Xpixels, Ypixels, szBitmapFileName)
            If Confirm Then
                MsgBox("Frame saved as bitmap file to: " & vbCr & vbCr & szBitmapFileName, vbInformation, Application.ProductName)
            End If
            SnapDet = Nothing
            SnapShot = True
        End If
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Function GrabFrame(objPic As Object, Optional cPos As Long) As Boolean:
    '*  Grabs frame to object.
    '*
    '*  Args:
    '*      objPic as object: Recipient object.
    '*      cPos As Long: frame number representing current position. (Default = 0)
    '*      Confirm As Boolean (Optional): If true, presents confirmation dialog after file saved.
    '*
    '*  Returns:
    '*      True if no errors.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function GrabFrame(ByVal objPic As PictureBox, Optional ByVal cPos As Long = 0) As Boolean
        Dim SnapDet As DexterLib.MediaDet
        Const FRAME_FILENAME As String = "c:\bitmap.bmp"

        On Error Resume Next

        SnapDet = New DexterLib.MediaDet
        'Write bitmap bits to file.
        SnapDet.Filename = mvarfilename
        SnapDet.CurrentStream = mvarVidStream
        SnapDet.WriteBitmapBits(CDbl(cPos), CLng(mvarSourceWidth), CLng(mvarSourceHeight), FRAME_FILENAME)
        objPic.Load(FRAME_FILENAME)

        SnapDet = Nothing
        GrabFrame = True
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Function GetVidStream() As Long: (Private)
    '*  Gets video stream info info.
    '*
    '*  Args:
    '*      None.
    '*
    '*  Returns:
    '*      Long - Video stream identifier.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Private Function GetVidStream() As Long
        On Error Resume Next
        Dim boolHasAudio As Boolean
        Dim nCount As Long
        Dim szStreamType As String

        If objMediaDet Is Nothing Then
            objMediaDet = New DexterLib.MediaDet
        End If


        With objMediaDet
            .FileName = mvarfilename
            mvarMedia_StreamCount = .OutputStreams
            mvarFPS = .FrameRate

            mvarMedia_HasAudio = False
            mvarMedia_HasVideo = False
            GetVidStream = -1

            For nCount = 0 To mvarMedia_StreamCount - 1
                'Get current Stream
                .CurrentStream = nCount
                'Get Stream Type
                szStreamType = .StreamTypeB
                If szStreamType = VIDEO_CLSID Then
                    'Stream is Video
                    mvarMedia_HasVideo = True
                    GetVidStream = nCount
                ElseIf szStreamType = AUDIO_CLSID Then
                    'Stream is Audio
                    mvarMedia_HasAudio = True
                End If
            Next

            If mvarMedia_HasVideo And mvarFPS = 0 Then mvarFPS = 25

        End With


        objMediaDet = Nothing
    End Function

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*              DV_PLAY CLASS PROPERTIES.              *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '*                                                     *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * *



    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Set AutoPlay value (Boolean):
    '* If true then clip plays automatically when dvClass::LoadClip
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Property AutoPlay() As Boolean
        Get
            AutoPlay = mvarAutoPlay
        End Get
        Set(ByVal vData As Boolean)
            mvarAutoPlay = vData
        End Set
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get BitRate value (Single):
    '* Returns playing speed for current media clip. 1 = 100%
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property BitRate() As Single
        Get
            BitRate = mvarSourceBitRate / 1000000
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get BOF value (Boolean):
    '* If true then dvClass::CurrentFrame = 0
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property BOF() As Boolean

        Get
            If posMedia.CurrentPosition = 0 Then
                BOF = True
            Else
                BOF = False
            End If
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Set CurrentPosition value (Double):
    '* Sets current frame position - value is equal to time in seconds.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Property CurrentPosition() As Double
        Get
            mvarCurrentPosition = posMedia.CurrentPosition
            CurrentPosition = mvarCurrentPosition
        End Get
        Set(ByVal vData As Double)
            If vData >= 0 And vData <= posMedia.Duration Then
                mvarCurrentPosition = vData
                posMedia.CurrentPosition = mvarCurrentPosition
            End If
        End Set
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get Duration value (Double):
    '* Returns Duration for media clip in seconds.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property Duration() As Double
        Get
            mvarDuration = posMedia.Duration
            Duration = mvarDuration
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get EOF value (Boolean):
    '* If true then dvClass::CurrentFrame = dvClass::Duration
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property EOF() As Boolean
        Get
            If posMedia.CurrentPosition = mvarDuration Then
                EOF = True
            Else
                EOF = False
            End If
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get FileName value (String):
    '* Returns filename path string for media clip.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property FileName() As String
        Get
            FileName = mvarfilename
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get FPS value (Single):
    '* Returns Frames per Second for current clip.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property FPS() As Single
        Get
            If mvarFPS = 0 Then mvarFPS = 25
            FPS = mvarFPS
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Set FullScreen value (Boolean):
    '* If true, sets window state to full screen.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Property FullScreen() As Boolean
        Get
            FullScreen = mvarFullScreen
        End Get
        Set(ByVal vData As Boolean)
            mvarFullScreen = vData
            On Error Resume Next
            vidWindow.FullScreenMode = CLng(mvarFullScreen)
        End Set
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get Media_HasAudio value (Boolean):
    '* If true then audio stream exists for media clip.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property Media_HasAudio() As Boolean
        Get
            Media_HasAudio = mvarMedia_HasAudio
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get Media_HasVideo value (Boolean):
    '* If true then video stream exists for media clip.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property Media_HasVideo() As Boolean
        Get
            Media_HasVideo = mvarMedia_HasVideo
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get Media_StreamCount value (Integer):
    '* Returns number of streams in current media clip.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property Media_StreamCount() As Integer
        Get
            Media_StreamCount = mvarMedia_StreamCount
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Set MuteAudio value (Boolean):
    '* If true then clip plays with no sound.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Property MuteAudio() As Boolean
        Get
            MuteAudio = mvarMuteAudio
        End Get
        Set(ByVal value As Boolean)
            mvarMuteAudio = value
            If value Then
                objAudio.Volume = -4000
            Else
                objAudio.Volume = 0
            End If
        End Set
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get Playing State (Boolean):
    '* Reports if video is playing.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property Playing() As Boolean
        Get
            Playing = mvarPlaying
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Set PlaySpeed value (Single):
    '* Sets playing speed for current media clip. 1 = 100%
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Property PlaySpeed() As Single
        Get
            PlaySpeed = mvarPlaySpeed
        End Get
        Set(ByVal vData As Single)
            mvarPlaySpeed = vData
            posMedia.Rate = mvarPlaySpeed
        End Set
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get SourceHeight value (Single):
    '* Returns height in pixels of video source.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property SourceHeight() As Single
        Get
            SourceHeight = mvarSourceHeight
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get SourceWidth value (Single):
    '* Returns height in pixels of video source.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property SourceWidth() As Single
        Get
            SourceWidth = mvarSourceWidth
        End Get
    End Property

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Get TimePerFrame value (Single):
    '* Returns time for each frame progression.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public ReadOnly Property TimePerFrame() As Single
        Get
            TimePerFrame = mvarSourceTpFrame
        End Get
    End Property

    Public Sub Release()
        Me.Finalize()
    End Sub

    Sub New()
        'Set defaults
        mvarPlaySpeed = 1
        mvarAutoPlay = True
        mvarSourceBitRate = 0

        objMediaDet = New DexterLib.MediaDet
    End Sub

    Protected Overrides Sub Finalize()
        objVideo = Nothing
        posMedia = Nothing
        evtMedia = Nothing
        objMControl = Nothing
        vidWindow = Nothing
        objAudio = Nothing
        objMediaDet = Nothing
        MyBase.Finalize()
    End Sub
End Class