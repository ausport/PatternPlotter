Public Class GamePlay

    Structure CaptionBox
        Dim Index As Integer
        Dim Text As String
        Dim FontStyle As FontStyle
        Dim FontSize As Single
        Dim ForeColor As Color
        Dim BackColor As Color
        Dim SmoothMode As Drawing2D.SmoothingMode
        Dim Shape As Drawing2D.GraphicsPath
        Dim BoxSize As Rectangle
        Dim PlayNumber As Integer
        Dim ID As Long  'Reference to DB
    End Structure

    Structure Instance
        Dim Index As Integer
        Dim Path As Drawing2D.GraphicsPath
        Dim Pen As Pen
        Dim ID As Long  'Reference to DB
        Dim PlayNumber As Integer
        Dim VideoStartTime As Double
        Dim Lead As Boolean         'Path leading to possession
        Dim Posession As Boolean    'Path in possession
        Dim Lag As Boolean          'Path following possession
        Dim EndsHere As Boolean
    End Structure

    Public Caption As CaptionBox
    Public Play As Instance

End Class
