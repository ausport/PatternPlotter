Public Class clsVideoCaptureClass
    Implements IDisposable

    Enum GraphState
        StateStopped = 0
        StatePaused = 1
        StateRunning = 2
    End Enum

    Private m_objFilterInfo As Object        'IFilterInfo interface represents all filters in the current graph
    Private m_objRegFilterInfo As Object ' IFilterInfo interface represents all registered filters on the system
    Private m_objSelectedPin As Object  ' IPinInfo interface represents the pin the user opted for from listbox
    Private m_objLocalFilterInfo As Object ' IFilterInfo interface represents the local working filter
    Private m_objMediaControl As QuartzTypeLib.IMediaControl  'IMediaControl interface provided by IFilgraphManager
    Private m_objPreviewControl As QuartzTypeLib.IMediaControl  'IMediaControl interface provided by IFilgraphManager
    Private m_GraphState As GraphState  'enum value indicating whether the video is running, paused, or stopped
    Private vidWindow As QuartzTypeLib.IVideoWindow       'VideoWindow Object
    Private objPosition As QuartzTypeLib.IMediaPosition
    Private catlist As FSFWRAPLib.IVBCollection

    Dim fi_DV As QuartzTypeLib.IFilterInfo
    Dim pi_DV_Out As QuartzTypeLib.IPinInfo

    Dim fi_Splitter As QuartzTypeLib.IFilterInfo
    Dim pi_Splitter_Input As QuartzTypeLib.IPinInfo
    Dim pi_Splitter_Out_Video As QuartzTypeLib.IPinInfo
    Dim pi_Splitter_Out_Audio As QuartzTypeLib.IPinInfo

    Dim fi_Splitter2 As QuartzTypeLib.IFilterInfo
    Dim pi_Splitter2_Input As QuartzTypeLib.IPinInfo
    Dim pi_Splitter2_Out_Video As QuartzTypeLib.IPinInfo
    Dim pi_Splitter2_Out_Audio As QuartzTypeLib.IPinInfo

    Dim fi_DVDecoder As QuartzTypeLib.IFilterInfo
    Dim pi_DVDecoder_XFormIn As QuartzTypeLib.IPinInfo
    Dim pi_DVDecoder_XFormOut As QuartzTypeLib.IPinInfo

    Dim fi_Renderer As QuartzTypeLib.IFilterInfo
    Dim pi_Renderer_Input As QuartzTypeLib.IPinInfo

    Dim fi_AudioRenderer As QuartzTypeLib.IFilterInfo
    Dim pi_AudioRenderer_Input As QuartzTypeLib.IPinInfo

    Dim fi_Mux As QuartzTypeLib.IFilterInfo
    Dim pi_Mux_Input1 As QuartzTypeLib.IPinInfo
    Dim pi_Mux_Input2 As QuartzTypeLib.IPinInfo
    Dim pi_Mux_Output As QuartzTypeLib.IPinInfo

    Dim fi_Tee As QuartzTypeLib.IFilterInfo
    Dim pi_Tee_Input As QuartzTypeLib.IPinInfo
    Dim pi_Tee_Capture As QuartzTypeLib.IPinInfo
    Dim pi_Tee_Preview As QuartzTypeLib.IPinInfo

    Dim fi_File As QuartzTypeLib.IFilterInfo
    Dim pi_File_In As QuartzTypeLib.IPinInfo


    Private mvarCaptureDevice() As String
    Private mvarDeviceCount As Integer
    Private mvarCurrentDevice As Integer
    Private mvarfilename As String
    Private filterFileName As QuartzTypeLib.IFilterInfo


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Sub Class_Initialize (void):
    '* Set default values for properties.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Sub New(Optional ByVal nDevice As Integer = -1)
        On Error GoTo ErrLine

        'NB Initialise the m_objPreviewControl graph for preview only.

        Dim strCat As String
        Dim fce As FSFWRAPLib.FilterCatEnumerator
        Dim f As FSFWRAPLib.IFilterClass
        Dim i As Integer = 0
        'Dim objRegFilter As QuartzTypeLib.IRegFilterInfo
        Dim objFilter As QuartzTypeLib.IFilterInfo

        fce = New FSFWRAPLib.FilterCatEnumerator

        'Set GUID for capture device filters
        strCat = "{860BB310-5D01-11d0-BD3B-00A0C911CE86}"
        catlist = fce.Category(strCat)
        mvarDeviceCount = catlist.Count
        ReDim mvarCaptureDevice(mvarDeviceCount - 1)

        ' instantiate the FilgraphManager
        m_objPreviewControl = New QuartzTypeLib.FilgraphManager

        ' add capture devices to filter graph
        For Each f In catlist
            objFilter = f.Create(m_objPreviewControl)
        Next f

        ' enunciate devices in graph
        For Each objFilter In m_objPreviewControl.FilterCollection
            mvarCaptureDevice(i) = objFilter.Name
            i = i + 1
        Next

        If mvarDeviceCount > 1 And nDevice = -1 Then
            'Open window to select device.
            mvarCurrentDevice = frmSelectVideoDevice.GetDefaultVideoDevice(mvarCaptureDevice)
            frmSelectVideoDevice.Close()
        ElseIf mvarDeviceCount = 1 Then
            mvarCurrentDevice = 0
        Else
            mvarCurrentDevice = nDevice
        End If

        'Set GUID for DirectX filters
        strCat = "{083863F1-70DE-11d0-BD40-00A0C911CE86}"
        fce = New FSFWRAPLib.FilterCatEnumerator
        catlist = fce.Category(strCat)

        ' add video renderer to filter graph
        For Each f In catlist
            If f.Name = "Video Renderer" Then
                objFilter = f.Create(m_objPreviewControl)
                Exit For
            End If
        Next f



        Exit Sub

ErrLine:
        MsgBox(Err.Description, MsgBoxStyle.Information, Application.ProductName)
        Exit Sub

    End Sub

    Public ReadOnly Property CaptureDeviceString(Optional ByVal nDeviceID As Integer = -1) As String
        Get
            If nDeviceID = -1 Then
                CaptureDeviceString = mvarCaptureDevice(mvarCurrentDevice)
            Else
                CaptureDeviceString = mvarCaptureDevice(nDeviceID)
            End If
        End Get
    End Property

    Public ReadOnly Property CaptureDeviceIndex() As Integer
        Get
            CaptureDeviceIndex = mvarCurrentDevice
        End Get
    End Property

    Public ReadOnly Property DeviceCount() As Integer
        Get
            DeviceCount = mvarDeviceCount
        End Get
    End Property

    Public Sub SetDisplayWidth(ByVal sWidth As Single)
        On Error Resume Next
        vidWindow.Width = sWidth
    End Sub

    Public Sub SetDisplayHeight(ByVal sHeight As Single)
        On Error Resume Next
        vidWindow.Height = sHeight
    End Sub

    Public Property FileName() As String
        Get
            FileName = mvarfilename
        End Get
        Set(ByVal vNewValue As String)
            mvarfilename = vNewValue
            'Set output filename.
            Dim fx As FSFWRAPLib.SinkInfo
            fx = New FSFWRAPLib.SinkInfo
            fx.Filter = filterFileName
            If fx.IsFileSink <> 0 Then fx.Filename = mvarfilename
        End Set
    End Property

    Public Sub SetDVType(ByVal windowParent As Form)
        '      On Error GoTo ErrLine

        Dim m_objRegFilterInfo As Object
        Dim objFilter As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilter As QuartzTypeLib.IRegFilterInfo
        m_objMediaControl = New QuartzTypeLib.FilgraphManager

        If Not m_objMediaControl Is Nothing Then
            m_objRegFilterInfo = m_objMediaControl.RegFilterCollection

            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "DV Video Decoder" Then
                    objRegFilter.Filter(objFilter)
                    Dim fx As FSFWRAPLib.SinkInfo
                    fx = New FSFWRAPLib.SinkInfo
                    fx.Filter = objFilter
                    fx.ShowPropPage(windowParent.Handle)
                    Exit Sub
                    Exit For
                End If
            Next
        End If
ErrLine:
        MsgBox("Property page for DV Decoder not available.", vbCritical, Application.ProductName)
    End Sub

    Public Function ShowPreview(ByVal destObject As PictureBox, Optional ByVal bool_audio As Boolean = True, Optional ByVal nDeviceNum As Integer = 0) As Boolean

        'On Error GoTo ErrLine

        Try
            If mvarCaptureDevice(nDeviceNum) = Nothing Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            'if no devices present then catch exception and return false
            Return False
            Exit Function
        End Try

        Dim objFilterInfo As QuartzTypeLib.IFilterInfo
        Dim objPinInfo As QuartzTypeLib.IPinInfo
        Dim objConnPinInfo As QuartzTypeLib.IPinInfo

        'Get DV device output pin
        For Each objFilterInfo In m_objPreviewControl.FilterCollection
            If objFilterInfo.Name = mvarCaptureDevice(nDeviceNum) Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If (mvarCaptureDevice(nDeviceNum) = "Apple Built-in iSight" And objPinInfo.Name = "Capture") Or _
                            (mvarCaptureDevice(nDeviceNum) <> "Apple Built-in iSight" And objPinInfo.Name = "DV A/V Out") Then

                        pi_DV_Out = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Get video renderer input pin
        For Each objFilterInfo In m_objPreviewControl.FilterCollection
            If objFilterInfo.Name = "Video Renderer" Then
                fi_Renderer = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "VMR Input0" Then
                        pi_Renderer_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Connect Pins
        'Verify objects exist.
        If pi_DV_Out Is Nothing Then GoTo GetOut
        If pi_Renderer_Input Is Nothing Then GoTo GetOut

        'Make connection
        pi_DV_Out.Connect(pi_Renderer_Input)

        'Check for connection..
        objConnPinInfo = pi_DV_Out.ConnectedTo


        'Run Graph
        If Not m_objPreviewControl Is Nothing Then
            vidWindow = m_objPreviewControl
            With vidWindow
                .WindowStyle = CLng(&H6000000)
                .Top = 0
                .Left = 0
                .Width = destObject.Width
                .Height = destObject.Height

                .SetWindowForeground(CLng(True))
                .Owner = destObject.Handle
                .FullScreenMode = 0
            End With

            'set state to running
            m_objPreviewControl.Run()
            'reset module-level variable
            m_GraphState = GraphState.StateStopped
        End If

        ShowPreview = True
        Exit Function

ErrLine:
        Err.Clear()
        If Not objFilterInfo Is Nothing Then objFilterInfo = Nothing
        If Not objPinInfo Is Nothing Then objPinInfo = Nothing
        If Not objConnPinInfo Is Nothing Then objConnPinInfo = Nothing
        If Not pi_DV_Out Is Nothing Then pi_DV_Out = Nothing
        If Not fi_DV Is Nothing Then fi_DV = Nothing
        If Not pi_Renderer_Input Is Nothing Then pi_Renderer_Input = Nothing
        If Not fi_Renderer Is Nothing Then fi_Renderer = Nothing
        Exit Function

GetOut:
        ShowPreview = False
    End Function

    Public Function IsOnline(Optional ByVal bool_audio As Boolean = False, Optional ByVal nDeviceNum As Integer = 0) As Boolean

        'Exit function if no device has been initialised.
        If mvarCaptureDevice(nDeviceNum) = Nothing Then
            IsOnline = False
            Exit Function
        End If

        'Dispose of previous capture graph
        'Me.Dispose()
        If Not m_objPreviewControl Is Nothing Then

            m_objPreviewControl.Stop()
            pi_DV_Out.Disconnect()
            If Not pi_DV_Out Is Nothing Then pi_DV_Out = Nothing
            If Not fi_DV Is Nothing Then fi_DV = Nothing
            If Not pi_Renderer_Input Is Nothing Then pi_Renderer_Input = Nothing
            If Not fi_Renderer Is Nothing Then fi_Renderer = Nothing
            m_objPreviewControl = Nothing

        End If

        'Define capture graph filters.
        Dim objFilter As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilter As QuartzTypeLib.IRegFilterInfo
        Dim objPinInfo As QuartzTypeLib.IPinInfo

        'Define capture class enumerators
        Dim strCat As String
        Dim fce As FSFWRAPLib.FilterCatEnumerator
        Dim f As FSFWRAPLib.IFilterClass
        Dim i As Integer
        fce = New FSFWRAPLib.FilterCatEnumerator

        'Set GUID for capture device filters
        strCat = "{860BB310-5D01-11d0-BD3B-00A0C911CE86}"
        catlist = fce.Category(strCat)
        mvarDeviceCount = catlist.Count
        ReDim mvarCaptureDevice(mvarDeviceCount - 1)

        ' instantiate the FilgraphManager
        m_objMediaControl = New QuartzTypeLib.FilgraphManager
        m_objRegFilterInfo = m_objMediaControl.RegFilterCollection

        ' add capture devices to filter graph
        For Each f In catlist
            objFilter = f.Create(m_objMediaControl)
        Next f

        ' enunciate devices in graph
        For Each objFilter In m_objMediaControl.FilterCollection
            mvarCaptureDevice(i) = objFilter.Name
            i = i + 1
        Next



        '*
        '*  Add remaining filters to filtergraph
        '*
        If Not m_objMediaControl Is Nothing Then

            '
            ' Run separate graph for iSight camera
            '
            If CaptureDeviceString(nDeviceNum) = "Apple Built-in iSight" Then
                Return Render_iSight()
                Exit Function
            End If

            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "DV Splitter" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "AVI Mux" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                'Debug.Print(objRegFilter.Name)
                If LCase(objRegFilter.Name) = "file writer" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "Smart Tee" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "DV Splitter" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "DV Video Decoder" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "Video Renderer" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
            For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
                If objRegFilter.Name = "Default WaveOut Device" Then
                    objRegFilter.Filter(objFilter)
                    Exit For
                End If
            Next
        End If


        '*
        '*  Connect filter pins
        '*

        '*
        '*  DV Camera to Smart Tee
        '*
        Dim objFilterInfo As QuartzTypeLib.IFilterInfo '= Nothing
        Dim objConnPinInfo As QuartzTypeLib.IPinInfo '= Nothing

        'Locate "Microsoft DV Camera and VCR" Filter - "DV A/V Out" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = mvarCaptureDevice(nDeviceNum) Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "DV A/V Out" Then
                        pi_DV_Out = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Locate "Smart Tee" Filter - "Input" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "Smart Tee" Then
                fi_Tee = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Input" Then
                        pi_Tee_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect DV Device Output to Smart Tee input
        'Verify objects exist.
        If pi_DV_Out Is Nothing Then GoTo GetOut
        If pi_Tee_Input Is Nothing Then GoTo GetOut

        'Make connection
        pi_DV_Out.Connect(pi_Tee_Input)

        'Check for connection..
        objConnPinInfo = pi_DV_Out.ConnectedTo



        '*
        '*  Smart Tee (Capture) to DV Splitter
        '*
        'Locate "Smart Tee" Filter - "Capture" Pin
        For Each objPinInfo In fi_Tee.Pins
            If objPinInfo.Name = "Capture" Then
                pi_Tee_Capture = objPinInfo
                Exit For
            End If
        Next
        'Locate "DV Splitter" Filter - "Input" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "DV Splitter" Then
                fi_Splitter = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Input" Then
                        pi_Splitter_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect DV Device Output to DV Splitter input
        'Verify objects exist.
        If pi_Tee_Capture Is Nothing Then GoTo GetOut
        If pi_Splitter_Input Is Nothing Then GoTo GetOut

        'Make connection
        pi_Tee_Capture.Connect(pi_Splitter_Input)

        'Check for connection..
        objConnPinInfo = pi_Tee_Capture.ConnectedTo


        '*
        '*  DV Splitter (Capture) to AVI Mux
        '*
        'Locate "DV Splitter" Filter - "Output" Pins
        For Each objPinInfo In fi_Splitter.Pins
            If objPinInfo.Name = "DVVidOut0" Then
                pi_Splitter_Out_Video = objPinInfo
                Exit For
            End If
        Next
        For Each objPinInfo In fi_Splitter.Pins
            If objPinInfo.Name = "AudOut00" Then
                pi_Splitter_Out_Audio = objPinInfo
                Exit For
            End If
        Next

        'Locate "AVI Mux" Filter - "Input1" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "AVI Mux" Then
                fi_Mux = objFilterInfo
                Exit For
            End If
        Next
        For Each objPinInfo In fi_Mux.Pins
            If objPinInfo.Name = "Input 01" Then
                pi_Mux_Input1 = objPinInfo
                Exit For
            End If
        Next
        For Each objPinInfo In fi_Mux.Pins
            If objPinInfo.Name = "AVI Out" Then
                pi_Mux_Output = objPinInfo
                Exit For
            End If
        Next

        'Connect DV Splitter DVVidOut0 to AVI Mux Input 01
        'Verify objects exist.
        If pi_Splitter_Out_Video Is Nothing Then GoTo GetOut
        If pi_Mux_Input1 Is Nothing Then GoTo GetOut
        'Make connection
        pi_Splitter_Out_Video.Connect(pi_Mux_Input1)
        'Check for connection..
        objConnPinInfo = pi_Splitter_Out_Video.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo GetOut
        'Find audio input - only presents after video input is connected.
        For Each objPinInfo In fi_Mux.Pins
            If objPinInfo.Name = "Input 02" Then
                pi_Mux_Input2 = objPinInfo
                Exit For
            End If
        Next
        'Connect DV Splitter AudOut00 to AVI Mux Input 02
        'Verify objects exist.
        If pi_Splitter_Out_Audio Is Nothing Then GoTo GetOut
        If pi_Mux_Input2 Is Nothing Then GoTo GetOut
        'Make connection
        pi_Splitter_Out_Audio.Connect(pi_Mux_Input2)
        'Check for connection..
        objConnPinInfo = pi_Splitter_Out_Audio.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo GetOut


        '*
        '*  AVI Mux(Capture) to File Writer
        '*
        'Locate "File Writer" Filter - "in" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If LCase(objFilterInfo.Name) = "file writer" Then
                fi_File = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "in" Then
                        pi_File_In = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect AVI Mux to File Writer
        'Verify objects exist.
        If pi_Mux_Output Is Nothing Then GoTo GetOut
        If pi_File_In Is Nothing Then GoTo GetOut
        'Make connection
        pi_Mux_Output.Connect(pi_File_In)
        'Check for connection..
        objConnPinInfo = pi_Mux_Output.ConnectedTo
        If objConnPinInfo Is Nothing Then GoTo GetOut

        '*
        '*  Smart Tee (Preview) to DV Splitter
        '*
        'Locate "Smart Tee" Filter - "Preview" Pin
        For Each objPinInfo In fi_Tee.Pins
            If objPinInfo.Name = "Preview" Then
                pi_Tee_Preview = objPinInfo
                Exit For
            End If
        Next
        'Locate "DV Splitter" Filter - "Input" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "DV Splitter 0002" Or objFilterInfo.Name = "DV Splitter 0001" Then
                fi_Splitter2 = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Input" Then
                        pi_Splitter2_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect DV Device Output to DV Splitter input
        'Verify objects exist.
        If pi_Tee_Preview Is Nothing Then GoTo GetOut
        If pi_Splitter2_Input Is Nothing Then GoTo GetOut

        'Make connection
        pi_Tee_Preview.Connect(pi_Splitter2_Input)

        'Check for connection..
        objConnPinInfo = pi_Tee_Preview.ConnectedTo

        '*
        '*  DV Splitter (Preview) to DV Video Decoder
        '*
        'Locate "DV Splitter" Filter - "Output" Pins
        For Each objPinInfo In fi_Splitter2.Pins
            If objPinInfo.Name = "DVVidOut0" Then
                pi_Splitter2_Out_Video = objPinInfo
                Exit For
            End If
        Next
        'Locate "DV Splitter" Filter - "Audio Output" Pin
        For Each objPinInfo In fi_Splitter2.Pins
            If objPinInfo.Name = "AudOut00" Then
                pi_Splitter2_Out_Audio = objPinInfo
                Exit For
            End If
        Next
        'Locate "DV Video Decoder" Filter - "XFormIn" Pins
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "DV Video Decoder" Then
                fi_DVDecoder = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "XForm In" Then
                        pi_DVDecoder_XFormIn = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect Pins
        'Verify objects exist.
        If pi_DVDecoder_XFormIn Is Nothing Then GoTo GetOut
        If pi_Splitter2_Out_Video Is Nothing Then GoTo GetOut

        'Make connection
        pi_Splitter2_Out_Video.Connect(pi_DVDecoder_XFormIn)

        'Check for connection..
        objConnPinInfo = pi_Splitter2_Out_Video.ConnectedTo

        '*
        '*  DV Video Decoder to Video Renderer
        '*
        'Locate "DV Video Decoder" Filter - "XForm Out" Pins
        For Each objPinInfo In fi_DVDecoder.Pins
            If objPinInfo.Name = "XForm Out" Then
                pi_DVDecoder_XFormOut = objPinInfo
                Exit For
            End If
        Next
        'Locate "Video Renderer" Filter - "Input" Pins
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "Video Renderer" Then
                fi_Renderer = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "VMR Input0" Then
                        pi_Renderer_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        'Connect Pins
        'Verify objects exist.
        If pi_DVDecoder_XFormOut Is Nothing Then GoTo GetOut
        If pi_Renderer_Input Is Nothing Then GoTo GetOut

        'Make connection
        pi_DVDecoder_XFormOut.Connect(pi_Renderer_Input)

        'Check for connection..
        objConnPinInfo = pi_DVDecoder_XFormOut.ConnectedTo


        'Connect Audio preview
        If bool_audio Then
            'Locate ""Default WaveOut Device"" Filter - "XFormIn" Pins
            For Each objFilterInfo In m_objMediaControl.FilterCollection
                If objFilterInfo.Name = "Default WaveOut Device" Then
                    fi_AudioRenderer = objFilterInfo
                    For Each objPinInfo In objFilterInfo.Pins
                        If objPinInfo.Name = "Audio Input pin (rendered)" Then
                            pi_AudioRenderer_Input = objPinInfo
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next
            'Connect pins
            'Verify objects exist.
            If pi_Splitter2_Out_Audio Is Nothing Then GoTo GetOut
            If pi_AudioRenderer_Input Is Nothing Then GoTo GetOut

            'Make connection
            pi_Splitter2_Out_Audio.Connect(pi_AudioRenderer_Input)

            'Check for connection..
            objConnPinInfo = pi_Splitter2_Out_Audio.ConnectedTo
        End If

        IsOnline = True

        Exit Function
ErrLine:
        Err.Clear()
        Resume Next

        Exit Function

GetOut:
        IsOnline = False
    End Function

    Private Function Render_iSight() As Boolean
        Dim objFilter As QuartzTypeLib.IFilterInfo = Nothing
        Dim objRegFilter As QuartzTypeLib.IRegFilterInfo
        Dim objPinInfo As QuartzTypeLib.IPinInfo

        For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
            If LCase(objRegFilter.Name) = "file writer" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next
        For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
            If objRegFilter.Name = "AVI Mux" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next
        For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
            If objRegFilter.Name = "Smart Tee" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next
        For Each objRegFilter In m_objRegFilterInfo ' listRegFilters
            If objRegFilter.Name = "Video Renderer" Then
                objRegFilter.Filter(objFilter)
                Exit For
            End If
        Next

        Dim objFilterInfo As QuartzTypeLib.IFilterInfo '= Nothing
        Dim objConnPinInfo As QuartzTypeLib.IPinInfo '= Nothing

        'Locate "Apple Built-in iSight" - "Capture" Pin (out)
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "Apple Built-in iSight" Then
                fi_DV = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Capture" Then
                        pi_DV_Out = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Locate "Smart Tee" Filters
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "Smart Tee" Then
                fi_Tee = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Capture" Then
                        pi_Tee_Capture = objPinInfo
                    End If
                    If objPinInfo.Name = "Input" Then
                        pi_Tee_Input = objPinInfo
                    End If
                    If objPinInfo.Name = "Preview" Then
                        pi_Tee_Preview = objPinInfo
                    End If
                Next
                Exit For
            End If
        Next

        'Locate "AVI Mux" input Filter
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "AVI Mux" Then
                fi_Mux = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "Input 01" Then
                        pi_Mux_Input1 = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Locate "Video Renderer" Filter Pins
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If objFilterInfo.Name = "Video Renderer" Then
                fi_Renderer = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "VMR Input0" Then
                        pi_Renderer_Input = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        'Locate "File Writer" Filter - "in" Pin
        For Each objFilterInfo In m_objMediaControl.FilterCollection
            If LCase(objFilterInfo.Name) = "file writer" Then
                fi_File = objFilterInfo
                For Each objPinInfo In objFilterInfo.Pins
                    If objPinInfo.Name = "in" Then
                        pi_File_In = objPinInfo
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next


        'Connect iSight Device Output to Smart Tee input
        If pi_DV_Out Is Nothing Then Return False
        If pi_Tee_Input Is Nothing Then Return False
        pi_DV_Out.Connect(pi_Tee_Input)
        objConnPinInfo = pi_DV_Out.ConnectedTo


        'Connect SmartTee output to AVI Mux input
        If pi_Tee_Capture Is Nothing Then Return False
        If pi_Mux_Input1 Is Nothing Then Return False
        pi_Tee_Capture.Connect(pi_Mux_Input1)
        objConnPinInfo = pi_Tee_Capture.ConnectedTo

        'Locate "AVI Mux" Filter - "AVI Out" Pin
        For Each objPinInfo In fi_Mux.Pins
            If objPinInfo.Name = "AVI Out" Then
                pi_Mux_Output = objPinInfo
                Exit For
            End If
        Next

        'Connect Mux output to file writer
        If pi_File_In Is Nothing Then Return False
        If pi_Mux_Output Is Nothing Then Return False
        pi_Mux_Output.Connect(pi_File_In)
        objConnPinInfo = pi_Mux_Output.ConnectedTo

        'Connect Smart Tee Preview to video renderer
        If pi_Renderer_Input Is Nothing Then Return False
        If pi_Tee_Preview Is Nothing Then Return False
        pi_Tee_Preview.Connect(pi_Renderer_Input)
        objConnPinInfo = pi_Tee_Preview.ConnectedTo




        Return True

    End Function

    Public Sub StartCapture(ByVal mvarfilename As String, ByVal destObject As PictureBox)
        If Len(mvarfilename) = 0 Then Exit Sub
        'Set output filename.
        Dim fx As FSFWRAPLib.SinkInfo
        fx = New FSFWRAPLib.SinkInfo

        fx.Filter = fi_File
        If fx.IsFileSink <> 0 Then fx.Filename = mvarfilename

        'Run Graph
        If Not m_objMediaControl Is Nothing Then
            'obtain the current position
            objPosition = m_objMediaControl
            vidWindow = m_objMediaControl

            With vidWindow
                .WindowStyle = CLng(&H6000000)
                .Top = 0
                .Left = 0
                .Width = destObject.Width
                .Height = destObject.Height
                .SetWindowForeground(CLng(True))
                .Owner = destObject.Handle
                .FullScreenMode = 0
            End With

            'set state to running
            m_objMediaControl.Run()
            'reset module-level variable
            m_GraphState = GraphState.StateRunning
        End If

ErrLine:
        Err.Clear()
        Exit Sub

    End Sub

    Public Sub PauseCapture()
        If m_GraphState = GraphState.StateRunning Then
            m_objMediaControl.Pause()
            m_GraphState = GraphState.StatePaused
            Exit Sub
        ElseIf m_GraphState = GraphState.StatePaused Then
            m_objMediaControl.Run()
            m_GraphState = GraphState.StateRunning
        End If
    End Sub

    Public Sub StopCapture()
        m_objMediaControl.Stop()
        m_GraphState = GraphState.StateStopped
        Exit Sub

    End Sub

    Public ReadOnly Property RecordingTime() As Double
        Get
            If m_GraphState = GraphState.StateRunning Then
                RecordingTime = objPosition.CurrentPosition
            Else
                RecordingTime = 0
            End If
        End Get
    End Property

    Public ReadOnly Property GraphStatus() As GraphState
        Get
            Return m_GraphState
        End Get
    End Property

    Public Sub Dispose() Implements System.IDisposable.Dispose
        On Error Resume Next

        If Not m_objMediaControl Is Nothing Then
            m_objMediaControl.Stop()
        End If
        If Not m_objPreviewControl Is Nothing Then
            m_objPreviewControl.Stop()
        End If

        If Not pi_DV_Out Is Nothing Then
            pi_DV_Out.Disconnect()
            pi_DV_Out = Nothing
        End If
        If Not pi_Tee_Input Is Nothing Then pi_Tee_Input = Nothing
        If Not fi_DV Is Nothing Then fi_DV = Nothing

        If Not pi_Tee_Input Is Nothing Then pi_Tee_Input = Nothing
        If Not pi_Tee_Capture Is Nothing Then
            pi_Tee_Capture.Disconnect()
            pi_Tee_Capture = Nothing
        End If
        If Not pi_Tee_Preview Is Nothing Then
            pi_Tee_Preview.Disconnect()
            pi_Tee_Preview = Nothing
        End If
        If Not fi_Tee Is Nothing Then fi_Tee = Nothing

        If Not pi_Splitter_Input Is Nothing Then pi_Splitter_Input = Nothing
        If Not pi_Splitter_Out_Video Is Nothing Then
            pi_Splitter_Out_Video.Disconnect()
            pi_Splitter_Out_Video = Nothing
        End If
        If Not pi_Splitter_Out_Audio Is Nothing Then
            pi_Splitter_Out_Audio.Disconnect()
            pi_Splitter_Out_Audio = Nothing
        End If
        If Not fi_Splitter Is Nothing Then fi_Splitter = Nothing

        If Not pi_Splitter2_Input Is Nothing Then pi_Splitter2_Input = Nothing
        If Not pi_Splitter2_Out_Video Is Nothing Then
            pi_Splitter2_Out_Video.Disconnect()
            pi_Splitter2_Out_Video = Nothing
        End If
        If Not pi_Splitter2_Out_Audio Is Nothing Then
            pi_Splitter2_Out_Audio.Disconnect()
            pi_Splitter2_Out_Audio = Nothing
        End If
        If Not fi_Splitter2 Is Nothing Then fi_Splitter2 = Nothing

        If Not pi_DVDecoder_XFormIn Is Nothing Then pi_DVDecoder_XFormIn = Nothing
        If Not pi_DVDecoder_XFormOut Is Nothing Then
            pi_DVDecoder_XFormOut.Disconnect()
            pi_DVDecoder_XFormOut = Nothing
        End If
        If Not fi_DVDecoder Is Nothing Then fi_DVDecoder = Nothing

        If Not pi_Renderer_Input Is Nothing Then pi_Renderer_Input = Nothing
        If Not fi_Renderer Is Nothing Then fi_Renderer = Nothing

        If Not pi_AudioRenderer_Input Is Nothing Then pi_AudioRenderer_Input = Nothing
        If Not fi_AudioRenderer Is Nothing Then fi_AudioRenderer = Nothing

        If Not pi_Mux_Input1 Is Nothing Then pi_Mux_Input1 = Nothing
        If Not pi_Mux_Input2 Is Nothing Then pi_Mux_Input2 = Nothing
        If Not pi_Mux_Output Is Nothing Then
            pi_Mux_Output.Disconnect()
            pi_Mux_Output = Nothing
        End If
        If Not fi_Mux Is Nothing Then fi_Mux = Nothing

        If Not pi_File_In Is Nothing Then pi_File_In = Nothing
        If Not fi_File Is Nothing Then fi_File = Nothing

        If Not m_objFilterInfo Is Nothing Then m_objFilterInfo = Nothing
        If Not m_objRegFilterInfo Is Nothing Then m_objRegFilterInfo = Nothing
        If Not m_objSelectedPin Is Nothing Then m_objSelectedPin = Nothing
        If Not m_objLocalFilterInfo Is Nothing Then m_objLocalFilterInfo = Nothing
        If Not m_objMediaControl Is Nothing Then m_objMediaControl = Nothing
        If Not objPosition Is Nothing Then objPosition = Nothing
        If Not catlist Is Nothing Then catlist = Nothing
        If Not vidWindow Is Nothing Then vidWindow = Nothing

    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub
End Class

