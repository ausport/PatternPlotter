Public Class Buttons

    Public Shared Function TypeFromName(ByVal szName As String) As ctlEventButton.ctlButtonType
        Select Case szName
            Case Is = "Team"
                Return ctlEventButton.ctlButtonType.Team
            Case Is = "OutcomePos"
                Return ctlEventButton.ctlButtonType.OutcomePos
            Case Is = "OutcomeNeg"
                Return ctlEventButton.ctlButtonType.OutcomeNeg
            Case Is = "Descriptor"
                Return ctlEventButton.ctlButtonType.Descriptor
            Case Else
                Return Nothing
        End Select

    End Function

    Public Shared Function SizeFromName(ByVal szName As String) As ctlEventButton.ctlSize
        Select Case szName
            Case Is = "Small"
                Return ctlEventButton.ctlSize.Small
            Case Is = "Medium"
                Return ctlEventButton.ctlSize.Medium
            Case Is = "Large"
                Return ctlEventButton.ctlSize.Large
            Case Else
                Return Nothing
        End Select

    End Function

End Class
Public Class ctlEventButton

    'Event control button
    'Copyright 2007
    'Stuart Morgan - Australian Institute of Sport

    Private mvarLocation As Point

    Public Enum ctlSize
        Small = 0
        Medium = 1
        Large = 2
    End Enum
    Public Enum ctlButtonType
        Team = 0
        OutcomePos = 1
        OutcomeNeg = 2
        Descriptor = 3
    End Enum
    Public Enum AnalysisType
        uVideoPlaylist = 0
        uOutcomeClusters = 1
        uPathwayMaps = 2
        uPosessionGraph = 3
        uPlayerMaps = 4
        uEventCountHeatMaps = 5
        uBallSpeedHeatMaps = 6
        uPossessionTimeHeatMaps = 7
        uScatterPlot = 8
    End Enum

    Private mvarSize As ctlSize = ctlSize.Medium
    Private mvarButtonColor As Color = Color.FromKnownColor(KnownColor.Control)
    Private mvarButtonTextSize As Single
    Private mvarButtonTextStyle As FontStyle
    Private mvarButtonTextColor As Color = Color.FromKnownColor(KnownColor.ControlText)
    Private mvarButtonType As ctlButtonType = ctlButtonType.Team
    Private mvarLocked As Boolean = False
    Private mvarHasPreset As Boolean = False
    Private mvarCalibrationFile As String = Nothing
    Private mvarTransmit As Boolean = False
    Private mvarPresetName As String
    Private mvarPresetPosition As Point
    Private mvarPresetZoom As String
    Private mvarKeyStroke As Keys = Keys.None
    Private mvariPhoneChartType As AnalysisType = AnalysisType.uScatterPlot

    Shadows Event Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Shadows Event DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Shadows Event Enter(ByVal sender As Object, ByVal e As System.EventArgs)
    Shadows Event MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Shadows Event MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
    Shadows Event MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Shadows Event MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Shadows Event MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    Shadows Event MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Shadows Event MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Shadows Event KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Shadows Event KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    Shadows Event KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Shadows Event Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    Shadows Event Move(ByVal sender As Object, ByVal e As System.EventArgs)
    Shadows Event Resize(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ButtonMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Click(Me, e)
    End Sub
    Private Sub Button1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.DoubleClick
        RaiseEvent DoubleClick(Me, e)
    End Sub
    Private Sub Button1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Enter
        RaiseEvent Enter(Me, e)
    End Sub
    Private Sub Button1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyDown
        RaiseEvent KeyDown(Me, e)
    End Sub
    Private Sub Button1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Button1.KeyPress
        RaiseEvent KeyPress(Me, e)
    End Sub
    Private Sub Button1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyUp
        RaiseEvent KeyUp(Me, e)
    End Sub
    Private Sub Button1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseClick
        RaiseEvent MouseClick(Me, e)
    End Sub
    Private Sub Button1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDoubleClick
        RaiseEvent MouseDoubleClick(Me, e)
    End Sub
    Private Sub Button1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
        RaiseEvent MouseDown(Me, e)
    End Sub
    Private Sub Button1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.MouseEnter
        RaiseEvent MouseEnter(Me, e)
    End Sub
    Private Sub Button1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.MouseHover
        RaiseEvent MouseHover(Me, e)
    End Sub
    Private Sub Button1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseMove
        RaiseEvent MouseMove(Me, e)
    End Sub
    Private Sub Button1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseUp
        RaiseEvent MouseUp(Me, e)
    End Sub
    Private Sub Button1_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Move
        RaiseEvent Move(Me, e)
    End Sub
    Private Sub Button1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Button1.Paint
        RaiseEvent Paint(Me, e)
    End Sub
    Private Sub Button1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Resize
        RaiseEvent Resize(Me, e)
    End Sub

    Public Property Caption() As String
        Get
            Return Me.Button1.Text
        End Get
        Set(ByVal value As String)
            Me.Button1.Text = value
        End Set
    End Property
    Public Property Position() As System.Drawing.Point
        Get
            Return Location
        End Get
        Set(ByVal value As Point)
            Location = value
        End Set
    End Property
    Public Property ButtonSize() As ctlSize
        Get
            Return mvarSize
        End Get
        Set(ByVal value As ctlSize)
            mvarSize = value
            Select Case value
                Case Is = ctlSize.Small
                    Me.Width = 100
                    Me.Height = 45
                    Me.Button1.FlatAppearance.BorderSize = 1
                Case Is = ctlSize.Medium
                    Me.Width = 150
                    Me.Height = 68
                    Me.Button1.FlatAppearance.BorderSize = 1
                Case ctlSize.Large
                    Me.Width = 200
                    Me.Height = 90
                    Me.Button1.FlatAppearance.BorderSize = 1
            End Select
            Me.Refresh()
        End Set
    End Property
    Public Property ButtonColor() As Color
        Get
            Return mvarButtonColor
        End Get
        Set(ByVal value As Color)
            mvarButtonColor = value
            Me.Button1.BackColor = value
            Me.KeyStrokeLabel.BackColor = value
        End Set
    End Property
    Public Property ButtonTextColor() As Color
        Get
            Return mvarButtonTextColor
        End Get
        Set(ByVal value As Color)
            mvarButtonTextColor = value
            Me.Button1.ForeColor = value
        End Set
    End Property
    Public Property ButtonTextSize() As Single
        Get
            Return mvarButtonTextSize
        End Get
        Set(ByVal value As Single)
            mvarButtonTextSize = value
            Me.Button1.Font = New Font(Me.Font, value)
        End Set
    End Property
    Public Property ButtonTextStyle() As FontStyle
        Get
            Return mvarButtonTextStyle
        End Get
        Set(ByVal value As FontStyle)
            mvarButtonTextStyle = value
            Me.Button1.Font = New Font(Me.Font, value)
        End Set
    End Property
    Public Property ButtonType() As ctlButtonType
        Get
            Return mvarButtonType
        End Get
        Set(ByVal value As ctlButtonType)
            mvarButtonType = value
            Select Case value
                Case ctlButtonType.Team
                    Me.Button1.FlatStyle = FlatStyle.Flat
                    Me.Button1.FlatAppearance.BorderColor = Me.Button1.ForeColor
                Case ctlButtonType.Descriptor
                    Me.Button1.FlatStyle = FlatStyle.Standard
                Case ctlButtonType.OutcomeNeg
                    Me.Button1.FlatStyle = FlatStyle.Popup
                    Me.Button1.BackColor = Color.LightCoral
                Case ctlButtonType.OutcomePos
                    Me.Button1.FlatStyle = FlatStyle.Popup
                    Me.Button1.BackColor = Color.LightGreen
            End Select
        End Set
    End Property
    Public Property Locked() As Boolean
        Get
            Return mvarLocked
        End Get
        Set(ByVal value As Boolean)
            mvarLocked = value
            Me.PictureBox1.Visible = value
        End Set
    End Property
    Public Property iPhoneChart() As AnalysisType
        Get
            Return mvariPhoneChartType
        End Get
        Set(ByVal value As AnalysisType)
            mvariPhoneChartType = value
        End Set
    End Property

    Private Sub ctlEventButton_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub ctlEventButton_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        mvarLocation.X = e.X
        mvarLocation.Y = e.Y
    End Sub
    Private Sub ctlEventButton_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left And Not mvarLocked Then Me.Location = Snap(Me.Location + e.Location - mvarLocation)
        RaiseEvent ButtonMoved(Me, e)
    End Sub

    Private Function Snap(ByVal loc As Point, Optional ByVal nGridSize As Integer = 5) As Point
        Snap.X = (nGridSize * (Int(loc.X / nGridSize)))
        Snap.Y = (nGridSize * (Int(loc.Y / nGridSize)))
        Return Snap
    End Function


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Width = 150
        Me.Height = 68

        Me.ButtonColor = Me.mvarButtonColor
        Me.ButtonTextColor = Me.mvarButtonTextColor
        Me.ButtonSize = Me.mvarSize
        Me.ButtonType = Me.mvarButtonType
        Me.ButtonTextStyle = Me.mvarButtonTextStyle
        Me.Locked = Me.mvarLocked
        Me.IsTransmit = Me.mvarTransmit
        Me.iPhoneChart = Me.mvariPhoneChartType

    End Sub

    Public Sub Kill()
        Me.Dispose(True)
        Me.Finalize()
    End Sub

    Public Property CalibrationFile() As String
        Get
            Return mvarCalibrationFile
        End Get
        Set(ByVal value As String)
            mvarCalibrationFile = value
        End Set
    End Property


    Public ReadOnly Property HasPreset() As Boolean
        Get
            mvarHasPreset = Not String.IsNullOrEmpty(mvarPresetName)
            Return mvarHasPreset
        End Get
    End Property

    Public Property IsTransmit() As Boolean
        Get
            Return mvarTransmit
        End Get
        Set(ByVal value As Boolean)
            mvarTransmit = value
            Me.picTransmit.Visible = mvarTransmit
        End Set
    End Property

    Public Property KeyStroke() As Keys
        Get
            Return mvarKeyStroke
        End Get
        Set(ByVal value As Keys)
            mvarKeyStroke = value
            If mvarKeyStroke = Keys.None Then
                Me.KeyStrokeLabel.Text = ""
            Else
                Me.KeyStrokeLabel.Text = mvarKeyStroke.ToString
            End If
        End Set
    End Property

    Public Property PresetName() As String
        Get
            Return mvarPresetName
        End Get
        Set(ByVal value As String)
            mvarPresetName = value
            mvarHasPreset = Not String.IsNullOrEmpty(mvarPresetName)
            Me.PictureBox2.Visible = mvarHasPreset
        End Set
    End Property

    Public Property PresetPosition() As Point
        Get
            Return mvarPresetPosition
        End Get
        Set(ByVal value As Point)
            mvarPresetPosition = value
        End Set
    End Property

    Public Property PresetZoom() As String
        Get
            Return mvarPresetZoom
        End Get
        Set(ByVal value As String)
            mvarPresetZoom = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
