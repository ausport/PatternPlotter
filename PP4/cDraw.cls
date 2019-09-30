Option Explicit On
Public Class cDraw
    Implements IDisposable


    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*                                                                 *
    '*                        ENUMERATIONS                             *
    '*                                                                 *
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Enum DrawShapeConstants
        dsFreehand = 1
        dsLine = 2
        dsBox = 3
        dsPoint = 4
        dsEllipse = 5
        dsArrow1 = 6
        dsArrow2 = 7
        dsworm = 8
        dsAsterix = 9
        dsClosedCircle = 10
        dsClosedSquare = 11
        dsCross = 12
        dsOpenCircle = 13
        dsOpenSquare = 14
        dsX = 15
        dsOpenTriangle = 16
        dsHat = 17
        dsText = 18
    End Enum


    Enum ResizeConstants
        szTopLeft = 1
        szBottomRight = 2
        szTopRight = 3
        szBottomLeft = 4
        szMiddleLeft = 5
        szMiddleRight = 6
        szMiddleTop = 7
        szMiddleBottom = 8
        szCenter = 9
        szPoint = 0
    End Enum

    Private Structure PenPoints
        Dim x As Double
        Dim y As Double
    End Structure


    Private FreeHandPoints() As PenPoints
    Private dxWorm(1440) As PenPoints
    Private WormPoints(1440) As PenPoints
    Private Const pi180 = (3.14159265358979 / 180)

    Private PointCount As Long
    Private FreeHandActive As Boolean

    Public SelectedHandle As ResizeConstants

    'Private userStyle As Drawing.Drawing2D.DashStyle
    'Private userMode As DrawModeConstants
    'Private userFill As Drawing.Drawing2D.FillMode

    Private Height As Single
    Private Width As Single

    Private StartX As Single
    Private StartY As Single
    Private EndX As Single
    Private EndY As Single

    Private Rubberbanding As Boolean
    Private ShapeComplete As Boolean

    Public Enum CaptionPositionConstants
        capTopLeft = 1
        capBottomRight = 2
        capTopCenter = 3
        capBottomCenter = 4
        capMiddleRight = 5
        capMiddleLeft = 6
    End Enum

    Private Structure Line2
        Dim x1 As Single
        Dim y1 As Single
        Dim x2 As Single
        Dim y2 As Single
    End Structure

    Private lblCaption() As Label
    Private CaptionCreated As Boolean

    Private rectShape As Rectangle
    Private lineShape As Line2
    Private xOffset As Double
    Private yOffset As Double

    'local variable(s) to hold property value(s)
    Private xmin As Single 'local copy
    Private xmax As Single 'local copy
    Private ymin As Single 'local copy
    Private ymax As Single 'local copy
    Private mvarShape As DrawShapeConstants 'local copy
    Private mvarStyle As DrawStyleConstants 'local copy
    Private mvarLineWidth As Single 'local copy
    Private mvarLineColor As Color 'local copy
    Private mvarFillColor As Color 'local copy
    Private mvarFillStyle As FillStyleConstants 'local copy
    Private mvarDrawMode As DrawModeConstants 'local copy
    Private mvarHandleMode As Boolean 'local copy
    Private mvarSelectMode As Boolean 'local copy
    Private mvarMoveMode As Boolean 'local copy
    Private mvarCaption As String   'local copy
    Private mvarCaptionPosition As CaptionPositionConstants
    Private mvarEnableCaption As Boolean
    Private mvarFont As Font
    Private mvarFontSize As Single
    Private mvarFontBold As Boolean
    Private mvarFontItalic As Boolean
    Private mvarFontUnderline As Boolean
    Private mvarFontColor As Color
    Private lblUnique As String     'Label control name
    Private mvarCaptionID As Long 'local copy
    Private frmObject As Object
    Private mvarCaptionTransparent As Boolean 'local copy
    Private mvarCaptionBackColor As Color 'local copy
    Private mvarShapeSize As Long
    Private mvarTimeLineIndex As Long 'local copy
    Private mvarEnabled As Boolean 'local copy


    Public Property Enabled() As Boolean
        Get
            Return mvarEnabled
        End Get
        Set(ByVal value As Boolean)
            mvarEnabled = value
        End Set
    End Property

    Public Property TimeLineIndex() As Long
        Get
            Return mvarTimeLineIndex
        End Get
        Set(ByVal value As Long)
            mvarTimeLineIndex = value
        End Set
    End Property


    Public Property CaptionBackColor() As Color
        Get
            Return mvarCaptionBackColor
        End Get
        Set(ByVal value As Color)
            mvarCaptionBackColor = value
        End Set
    End Property


    Public Property CaptionTransparent() As Boolean
        Get
            Return mvarCaptionTransparent
        End Get
        Set(ByVal value As Boolean)
            mvarCaptionTransparent = value
        End Set
    End Property

    Public Sub KillCaption(ByVal shape As Long)
        Attribute(KillCaption.VB_Description = "Must be initiated if the class is to be re-used.")
        lblCaption(shape).Visible = False
        lblCaption(shape).Enabled = False
        DoEvents()
        frmObject.Controls.Remove(lblUnique)
        CaptionCreated = False
    End Sub

    Public Property CaptionID() As Long
        Get
            Return mvarCaptionID
        End Get
        Set(ByVal value As Long)
            mvarCaptionID = value
        End Set
    End Property

    Public Property Font() As Font
        Get
            Return mvarFont
        End Get
        Set(ByVal value As Font)
            mvarFont = value
        End Set
    End Property


    Public Property CaptionPosition() As CaptionPositionConstants
        Get
            Return mvarCaptionPosition
        End Get
        Set(ByVal value As CaptionPositionConstants)
            mvarCaptionPosition = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return mvarCaption
        End Get
        Set(ByVal value As String)
            mvarCaption = value
        End Set
    End Property

    Public Property FontColor() As Color
        Get
            Return mvarFontColor
        End Get
        Set(ByVal value As Color)
            mvarFontColor = value
        End Set
    End Property

    Public Property FontSize() As Single
        Get
            Return mvarFontSize
        End Get
        Set(ByVal value As Single)
            mvarFontSize = value
        End Set
    End Property

    Public Property FontBold() As Boolean
        Get
            Return mvarFontBold
        End Get
        Set(ByVal value As Boolean)
            mvarFontBold = value
        End Set
    End Property

    Public Property FontItalic() As Boolean
        Get
            Return mvarFontItalic
        End Get
        Set(ByVal value As Boolean)
            mvarFontItalic = value
        End Set
    End Property

    Public Property FontUnderline() As Boolean
        Get
            Return mvarFontUnderline
        End Get
        Set(ByVal value As Boolean)
            mvarFontUnderline = value
        End Set
    End Property

    Public Property ShowCaption() As Boolean
        Get
            Return mvarEnableCaption
        End Get
        Set(ByVal value As Boolean)
            mvarEnableCaption = value
            If mvarEnableCaption And CaptionCreated Then
                lblCaption(CaptionID).Visible = True
            ElseIf Not CaptionCreated Or Not mvarEnableCaption Then
                lblCaption(CaptionID).Visible = False
            End If
        End Set
    End Property

    Public Property MoveMode() As Boolean
        Get
            Return mvarMoveMode
        End Get
        Set(ByVal value As Boolean)
            mvarMoveMode = value
        End Set
    End Property

    Public Property HandleMode() As Boolean
        Get
            Return mvarHandleMode
        End Get
        Set(ByVal value As Boolean)
            mvarHandleMode = value
        End Set
    End Property

    Public Property Selected() As Boolean
        Get
            Return mvarSelectMode
        End Get
        Set(ByVal value As Boolean)
            mvarSelectMode = value
        End Set
    End Property

    Public Function SelectionRange(ByVal obj As Object, ByVal x As Single, ByVal y As Single) As Boolean
        ' "True if x,y coordinates are inside the object handle.  HandleMode must be true."
        Dim locX As Single
        Dim locY As Single

        If shape = dsPoint Then
            If x >= StartX - 75 And x <= StartX + 75 And y >= StartY - 75 And y <= StartY + 75 Then
                'Top left for point only
                SelectionRange = True
                SelectedHandle = szPoint
                obj.MousePointer = vbCrosshair
                Exit Function
            End If
        End If

        If shape <> dsEllipse Then
            If x >= StartX - 75 And x <= StartX + 75 And y >= StartY - 75 And y <= StartY + 75 Then
                'Top left
                SelectionRange = True
                SelectedHandle = szTopLeft
                If shape = dsLine Then
                    obj.MousePointer = vbSizePointer
                Else
                    obj.MousePointer = vbSizeNWSE
                End If
                Exit Function

            ElseIf x >= EndX - 75 And x <= EndX + 75 And y >= EndY - 75 And y <= EndY + 75 Then
                'Bottom right
                SelectionRange = True
                SelectedHandle = szBottomRight
                If shape = dsLine Then
                    obj.MousePointer = vbSizePointer
                Else
                    obj.MousePointer = vbSizeNWSE
                End If
                Exit Function
            End If
        End If


        If shape = dsBox Then
            If x >= EndX - 75 And x <= EndX + 75 And y >= StartY - 75 And y <= StartY + 75 Then
                'Top right
                SelectionRange = True
                SelectedHandle = szTopRight
                obj.MousePointer = vbSizeNESW
                Exit Function

            ElseIf x >= StartX - 75 And x <= StartX + 75 And y >= EndY - 75 And y <= EndY + 75 Then
                'Bottom left
                SelectionRange = True
                SelectedHandle = szBottomLeft
                obj.MousePointer = vbSizeNESW
                Exit Function
            End If
        End If

        If shape <> dsLine Then     'Middle handles for Ellipse & Box only.
            locY = ((High(StartY, EndY) - Low(StartY, EndY)) / 2) + Low(StartY, EndY)
            locX = ((High(StartX, EndX) - Low(StartX, EndX)) / 2) + Low(StartX, EndX)

            If x >= StartX - 75 And x <= StartX + 75 And y >= locY - 75 And y <= locY + 75 Then
                'Left middle
                SelectionRange = True
                SelectedHandle = szMiddleLeft
                obj.MousePointer = vbSizeWE
                Exit Function

            ElseIf x >= EndX - 75 And x <= EndX + 75 And y >= locY - 75 And y <= locY + 75 Then
                'Right middle
                SelectionRange = True
                SelectedHandle = szMiddleRight
                obj.MousePointer = vbSizeWE
                Exit Function

            ElseIf x >= locX - 75 And x <= locX + 75 And y >= StartY - 75 And y <= StartY + 75 Then
                'Top middle
                SelectionRange = True
                SelectedHandle = szMiddleTop
                obj.MousePointer = vbSizeNS
                Exit Function

            ElseIf x >= locX - 75 And x <= locX + 75 And y >= EndY - 75 And y <= EndY + 75 Then
                'Bottom middle
                SelectionRange = True
                SelectedHandle = szMiddleBottom
                obj.MousePointer = vbSizeNS
                Exit Function
            End If
        End If

        If x >= StartX And x <= EndX And y >= StartY And y <= EndY Then
            SelectionRange = True
            SelectedHandle = szCenter
            obj.MousePointer = vbSizePointer
        ElseIf x <= StartX And x >= EndX And y >= StartY And y <= EndY Then
            SelectionRange = True
            SelectedHandle = szCenter
            obj.MousePointer = vbSizePointer
        ElseIf x >= StartX And x <= EndX And y <= StartY And y >= EndY Then
            SelectionRange = True
            SelectedHandle = szCenter
            obj.MousePointer = vbSizePointer
        ElseIf x <= StartX And x >= EndX And y <= StartY And y >= EndY Then
            SelectionRange = True
            SelectedHandle = szCenter
            obj.MousePointer = vbSizePointer
        Else
            SelectionRange = False
            obj.MousePointer = vbDefault
        End If

        If SelectedHandle = szCenter Then
            With rectShape
                .Top = Low(StartY, EndY)
                .Left = Low(StartX, EndX)
                .Width = High(StartX, EndX) - .Left
                .Height = High(StartY, EndY) - .Top

                'This enables the grab handle to be placed anywhere within the shape.
                xOffset = x - .Left
                yOffset = y - .Top
            End With
        End If



End Property


Public Property Let DrawMode(ByVal vData As DrawModeConstants)
Attribute DrawMode.VB_Description = "Set drawing mode (Default = vbCopyPen)."
    mvarDrawMode = vData
    End Property


Public Property Get DrawMode() As DrawModeConstants
    DrawMode = mvarDrawMode
    End Property


Public Property Let FillStyle(ByVal vData As FillStyleConstants)
Attribute FillStyle.VB_Description = "Sets shape fill style (Default = vbTransparent)"
    mvarFillStyle = vData
    End Property


Public Property Get FillStyle() As FillStyleConstants
    FillStyle = mvarFillStyle
    End Property


Public Property Let FillColor(ByVal vData As ColorConstants)
Attribute FillColor.VB_Description = "Sets shape fill color."
    mvarFillColor = vData
    End Property


Public Property Get FillColor() As ColorConstants
    FillColor = mvarFillColor
    End Property


Public Property Let LineColor(ByVal vData As ColorConstants)
    mvarLineColor = vData
    End Property


Public Property Get LineColor() As ColorConstants
Attribute LineColor.VB_Description = "Sets line color (Default = vbBlack)."
    LineColor = mvarLineColor
    FontColor = mvarLineColor
    End Property


Public Property Let LineWidth(ByVal vData As Single)
    mvarLineWidth = vData
    End Property


Public Property Get LineWidth() As Single
Attribute LineWidth.VB_Description = "Sets line width.  (Default = 1)."
    LineWidth = mvarLineWidth
    End Property



Public Property Let Style(ByVal vData As DrawStyleConstants)
Attribute Style.VB_Description = "Sets style of line."
    mvarStyle = vData
    End Property


Public Property Get Style() As DrawStyleConstants
    Style = mvarStyle
    End Property


Public Property Let shape(ByVal vData As DrawShapeConstants)
Attribute shape.VB_Description = "Sets shape of current drawing object."
    mvarShape = vData
    End Property


Public Property Get shape() As DrawShapeConstants
    shape = mvarShape
    End Property


    Public Sub ReSize(ByRef obj As Object, ByVal x As Single, ByVal y As Single)
        If Not Rubberbanding Then
            ' Save user settings on first loop for drawing after rubberbanding.
            userStyle = Style
            userMode = DrawMode
            userFill = FillStyle
        Else
            If shape = dsLine Or dsArrow1 Or dsArrow2 Or dsworm Then
                Style = vbSolid
            Else
                Style = vbDot
            End If

            ' Erase the previous object.
            DrawMode = vbInvert
            DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

        End If
        Rubberbanding = True



        If SelectedHandle = szTopLeft Then  'user selected (x1,y1) intercept.
            StartX = x
            StartY = y
            '        EndX = xmax
            '        EndY = ymax

        ElseIf SelectedHandle = szBottomRight Then  'user selected (x2,y2) intercept.
            '        StartX = xmin
            '        StartY = ymin
            EndX = x
            EndY = y

        ElseIf SelectedHandle = szTopRight Then  'user selected (x2,y1) intercept.
            '        StartX = xmin
            StartY = y
            EndX = x
            '        EndY = ymax

        ElseIf SelectedHandle = szBottomLeft Then  'user selected (x1,y2) intercept.
            StartX = x
            '        StartY = ymin
            '        EndX = xmax
            EndY = y

        ElseIf SelectedHandle = szMiddleLeft Then  'user selected (x1,y2) intercept.
            StartX = x
            '        StartY = ymin
            '        EndX = xmax
            '        EndY = ymax

        ElseIf SelectedHandle = szMiddleRight Then  'user selected (x1,y2) intercept.
            '        StartX = xmin
            '        StartY = ymin
            EndX = x
            '        EndY = ymax

        ElseIf SelectedHandle = szMiddleTop Then  'user selected (x1,y2) intercept.
            '        StartX = xmin
            StartY = y
            '        EndX = xmax
            '        EndY = ymax

        ElseIf SelectedHandle = szMiddleBottom Then  'user selected (x1,y2) intercept.
            '        StartX = xmin
            '        StartY = ymin
            '        EndX = xmax
            EndY = y

        ElseIf SelectedHandle = szPoint Then  'user selected single point.
            StartX = x
            StartY = y
            EndX = x
            EndY = y

        ElseIf SelectedHandle = szCenter Then  'user selected inside shape (move-mode).

            With rectShape
                If x < StartX Then  'Shape is inverted
                    StartX = x + xOffset
                    EndX = StartX - .Width
                Else                'Shape is not inverted
                    StartX = x - xOffset
                    EndX = StartX + .Width
                End If

                If y < StartY Then  'Shape is inverted
                    StartY = y + yOffset
                    EndY = StartY - .Height
                Else                'Shape is not inverted
                    StartY = y - yOffset
                    EndY = StartY + .Height
                End If
            End With
        End If

        ' Draw the new rubberband object.

        If shape = dsLine Or dsArrow1 Or dsArrow2 Or dsworm Then
            Style = vbSolid
        Else
            Style = vbDot
        End If
        DrawMode = vbInvert
        FillStyle = vbFSTransparent
        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)


    End Sub


    Public Sub ReleaseSelected(ByRef obj As Object, ByVal x As Single, ByVal y As Single)
        ' If not rubberbanding, do nothing.
        If Not Rubberbanding Then Exit Sub

        ' End rubberbanding.
        Rubberbanding = False

        ' Resinstate user settings after rubberbanding.
        Style = userStyle
        DrawMode = userMode
        FillStyle = userFill

        Selected = False

        ' Draw the final object.
        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

        ' Set object dimension properties.

        If mvarShape = dsBox Or mvarShape = dsEllipse Then

            xmin = Low(StartX, EndX)
            ymin = Low(StartY, EndY)
            xmax = High(StartX, EndX)
            ymax = High(StartY, EndY)

            StartX = xmin
            StartY = ymin
            EndX = xmax
            EndY = ymax

            rectShape.Left = StartX
            rectShape.Top = StartY
            rectShape.Width = EndX - StartX
            rectShape.Height = EndY - StartY

        Else
            xmin = StartX
            ymin = StartY
            xmax = EndX
            ymax = EndY
        End If

        Height = CSng(Sqr((ymax - ymin) ^ 2))
        Width = CSng(Sqr((xmax - xmin) ^ 2))

        obj.MousePointer = vbDefault

    End Sub


    Public Sub DrawShape(ByRef obj As Object, ByVal x1 As Single, ByVal y1 As Single, _
            ByVal x2 As Single, ByVal y2 As Single, ByVal shape As DrawShapeConstants, _
    ByVal ShowShape As Boolean, ByVal ShowCaption As Boolean)

        If shape = dsArrow1 Or dsArrow2 Then
            Dim dx As Single
            Dim dy As Single
            Dim Length As Single
        End If

        obj.ScaleMode = vbTwips     'Set object scalemode.

        obj.DrawMode = DrawMode
        obj.DrawStyle = Style
        obj.DrawWidth = LineWidth
        obj.FillStyle = FillStyle
        If HandleMode = True Then obj.FillStyle = vbFSTransparent
        obj.ForeColor = LineColor
        obj.FillColor = FillColor

        If ShowCaption And Len(Caption) > 0 Then AddCaption(obj, x1, y1, x2, y2)

        If Not ShowShape Then
            Exit Sub
        End If

        Select Case shape


            Case dsLine
                If LineWidth = 1 Or Style = vbSolid Then
                    obj.Line (x1, y1)-(x2, y2)
                Else
                    If Style = 2 Then
                        DashLine(obj, x1, y1, x2, y2, 40, 200, LineWidth)
                    ElseIf Style = vbDash Or vbDashDot Or vbDashDotDot Then
                        DashLine(obj, x1, y1, x2, y2, 200, 200, LineWidth)
                    End If
                End If

            Case dsArrow1
                If LineWidth = 1 Or Style = vbSolid Then
                    obj.Line (x1, y1)-(x2, y2)
                Else
                    If Style = 2 Then
                        DashLine(obj, x1, y1, x2, y2, 40, 200, LineWidth)
                    ElseIf Style = vbDash Or vbDashDot Or vbDashDotDot Then
                        DashLine(obj, x1, y1, x2, y2, 200, 200, LineWidth)
                    End If
                End If

                dx = x2 - x1
                dy = y2 - y1
                Length = Sqr(dx * dx + dy * dy)
                If Length > 0 Then
                    dx = dx / Length * mvarShapeSize
                    dy = dy / Length * mvarShapeSize
                    obj.Line (x2, y2)-Step(-dx - dy, -dy + dx)
                    obj.Line (x2, y2)-Step(-dx + dy, -dy - dx)
                End If

            Case dsArrow2
                If LineWidth = 1 Or Style = vbSolid Then
                    obj.Line (x1, y1)-(x2, y2)
                Else
                    If Style = 2 Then
                        DashLine(obj, x1, y1, x2, y2, 40, 200, LineWidth)
                    ElseIf Style = vbDash Or vbDashDot Or vbDashDotDot Then
                        DashLine(obj, x1, y1, x2, y2, 200, 200, LineWidth)
                    End If
                End If

                dx = x2 - x1
                dy = y2 - y1
                Length = Sqr(dx * dx + dy * dy)
                If Length > 0 Then
                    dx = dx / Length * mvarShapeSize
                    dy = dy / Length * mvarShapeSize
                    obj.Line (x2, y2)-Step(-dx - dy, -dy + dx)
                    obj.Line (x2, y2)-Step(-dx + dy, -dy - dx)
                End If

                dx = x1 - x2
                dy = y1 - y2
                Length = Sqr(dx * dx + dy * dy)
                If Length > 0 Then
                    dx = dx / Length * mvarShapeSize
                    dy = dy / Length * mvarShapeSize
                    obj.Line (x1, y1)-Step(-dx - dy, -dy + dx)
                    obj.Line (x1, y1)-Step(-dx + dy, -dy - dx)
                End If

            Case dsworm
                DrawWorm(obj, x1, y1, x2, y2)
            Case dsBox
                obj.Line (x1, y1)-(x2, y2), , B
            Case dsEllipse
                DrawEllipse(obj, x1, y1, x2, y2)
            Case dsPoint
                obj.PSet(x1, y1)
            Case dsAsterix
                obj.Line (x1, y1 + ShapeSize)-(x1, y1 - ShapeSize), LineColor
                obj.Line (x1 - ShapeSize, y1)-(x1 + ShapeSize, y1), LineColor
                obj.Line (x1 - ShapeSize, y1 + ShapeSize)-(x1 + ShapeSize, y1 - ShapeSize), LineColor
                obj.Line (x1 - ShapeSize, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), LineColor
            Case dsClosedCircle
                obj.FillStyle = vbFSSolid
                obj.FillColor = FillColor
                obj.Circle (x1, y1), ShapeSize, LineColor
            Case dsClosedSquare
                obj.FillStyle = vbFSSolid
                obj.Line (x1 - ShapeSize, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), FillColor, BF
                obj.Line (x1 - ShapeSize, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), LineColor, B
            Case dsCross
                obj.FillStyle = vbFSTransparent
                    obj.Line (x1, y1 + ShapeSize)-(x1, y1 - ShapeSize), LineColor
                    obj.Line (x1 - ShapeSize, y1)-(x1 + ShapeSize, y1), LineColor
            Case dsOpenCircle
                obj.FillStyle = vbFSTransparent
                    obj.Circle (x1, y1), ShapeSize, LineColor
            Case dsOpenSquare
                obj.FillStyle = vbFSTransparent
                    obj.Line (x1 - ShapeSize, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), LineColor, B
            Case dsX
                    obj.Line (x1 - ShapeSize, y1 + ShapeSize)-(x1 + ShapeSize, y1 - ShapeSize), LineColor
                    obj.Line (x1 - ShapeSize, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), LineColor
            Case dsOpenTriangle
                    obj.Line (x1, y1 - ShapeSize)-(x1 + ShapeSize, y1 + ShapeSize), LineColor
                    obj.Line -(x1 - ShapeSize, y1 + ShapeSize), LineColor
                    obj.Line -(x1, y1 - ShapeSize), LineColor
            Case dsHat
                    obj.Line (x1, y1 - ShapeSize)-(x1 + (ShapeSize * 0.6), y1 + ShapeSize), LineColor
                    obj.Line (x1, y1 - ShapeSize)-(x1 - (ShapeSize * 0.6), y1 + ShapeSize), LineColor
                obj.DrawWidth = LineWidth + 1
                    obj.Line (x1 + ShapeSize, y1 + ShapeSize)-(x1 - ShapeSize, y1 + ShapeSize), LineColor
                obj.DrawWidth = LineWidth

            Case dsText
                obj.DrawStyle = vbDot
                obj.DrawWidth = 1

                obj.ForeColor = mvarFontColor
                obj.FontSize = mvarFontSize
                obj.FontName = mvarFont
                obj.FontBold = mvarFontBold
                obj.FontUnderline = mvarFontUnderline
                obj.FontItalic = mvarFontItalic

                If HandleMode Or Rubberbanding Then obj.Line (x1, y1)-(x2, y2), , B
                obj.CurrentX = x1
                obj.CurrentY = y1

                Dim txtLen As Long
                Dim pos1 As Long
                Dim pos2 As Long
                Dim new_word As String

                txtLen = Len(mvarCaption)
                pos1 = 1
                Do
                    'get the next word..
                    pos2 = InStr(pos1, mvarCaption, " ")
                    If pos2 = 0 Then
                        new_word = Mid$(mvarCaption, pos1)
                    Else
                        new_word = Mid$(mvarCaption, pos1, pos2 - pos1)
                    End If
                    pos1 = pos2 + 1

                    'if the word wont fit then start new line.
                    If obj.CurrentX + obj.TextWidth(new_word) > x2 Then
                        obj.CurrentX = x1
                        obj.CurrentY = obj.CurrentY + 1.25 * obj.TextHeight(new_word)
                    End If

                    obj.Print new_word; " ";
                Loop While pos2 > 0


                'obj.Print mvarCaption


            Case dsFreehand
                Dim i As Long
                For i = 2 To PointCount
                    obj.Line (FreeHandPoints(i - 1).x, FreeHandPoints(i - 1).y)- _
                        (FreeHandPoints(i).x, FreeHandPoints(i).y)
                Next i
                Exit Sub
        End Select


        If HandleMode = True Then
            'obj.DrawStyle = vbCopyPen
            obj.DrawWidth = 1
            obj.FillStyle = vbFSSolid
            obj.ForeColor = LineColor
            obj.FillColor = vbWhite

            Dim handlesize As Single
            Select Case mvarLineWidth
                Case Is <= 2
                    handlesize = 40
                Case 3 To 6
                    handlesize = mvarLineWidth * 15
                Case Is > 6
                    handlesize = mvarLineWidth * 7
            End Select

            If shape <> dsEllipse And shape <> dsPoint Then
                'top left
                DrawEllipse(obj, x1 - handlesize, y1 - handlesize, x1 + handlesize, y1 + handlesize)
                'obj.Line (x1 - handlesize, y1 - handlesize)-(x1 + handlesize, y1 + handlesize), , B
                'bottom right
                DrawEllipse(obj, x2 - handlesize, y2 - handlesize, x2 + handlesize, y2 + handlesize)
                'obj.Line (x2 - handlesize, y2 - handlesize)-(x2 + handlesize, y2 + handlesize), , B
            End If

            If shape = dsBox Or shape = dsText And shape <> dsPoint Then
                'bottom left
                DrawEllipse(obj, x2 - handlesize, y1 - handlesize, x2 + handlesize, y1 + handlesize)
                'obj.Line (x2 - handlesize, y1 - handlesize)-(x2 + handlesize, y1 + handlesize), , B
                'top right
                DrawEllipse(obj, x1 - handlesize, y2 - handlesize, x1 + handlesize, y2 + handlesize)
                'obj.Line (x1 - handlesize, y2 - handlesize)-(x1 + handlesize, y2 + handlesize), , B
            End If

            If shape <> dsLine And shape <> dsPoint And _
                shape <> dsArrow1 And shape <> dsArrow2 _
                And shape <> dsworm Then
                Dim y As Single
                Dim x As Single
                'middle left
                y = ((High(y1, y2) - Low(y1, y2)) / 2) + Low(y1, y2)
                'obj.Line (x1 - handlesize, y - handlesize)-(x1 + handlesize, y + handlesize), , B
                DrawEllipse(obj, x1 - handlesize, y - handlesize, x1 + handlesize, y + handlesize)
                'middle right
                'obj.Line (x2 - handlesize, y - handlesize)-(x2 + handlesize, y + handlesize), , B
                DrawEllipse(obj, x2 - handlesize, y - handlesize, x2 + handlesize, y + handlesize)
                'middle top
                x = ((High(x1, x2) - Low(x1, x2)) / 2) + Low(x1, x2)
                'obj.Line (x - handlesize, y1 - handlesize)-(x + handlesize, y1 + handlesize), , B
                DrawEllipse(obj, x - handlesize, y1 - handlesize, x + handlesize, y1 + handlesize)
                'middle bottom
                'obj.Line (x - handlesize, y2 - handlesize)-(x + handlesize, y2 + handlesize), , B
                DrawEllipse(obj, x - handlesize, y2 - handlesize, x + handlesize, y2 + handlesize)

            End If
        End If

    End Sub

    Private Sub DrawWorm(ByVal obj As Object, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single)
        'Initialise worm.
        Dim i As Double
        Dim dx1 As Double : Dim dx2 As Double : Dim dy1 As Double : Dim dy2 As Double
        Dim Angle As Double
        Dim Length As Double
        Dim Height As Double
        Dim Amplitude As Double
        Dim Hypotenuse As Double

        Length = x2 - x1
        Height = y2 - y1

        If x2 > x1 Then
            Hypotenuse = ((x1 - x2) ^ 2) + ((y1 - y2) ^ 2)
            Angle = Atn(Height / Length)    'ArcTan - return angle in radians.
        ElseIf x1 > x2 Then
            Hypotenuse = ((x2 - x1) ^ 2) + ((y1 - y2) ^ 2)
            Angle = Atn(Height / Length)    'ArcTan - return angle in radians.
            Angle = Angle + (180 * pi180)
        ElseIf x1 = x2 Then
            Hypotenuse = (Height ^ 2)
            Angle = Atn(Height)    'ArcTan - return angle in radians.
        End If

        Hypotenuse = Sqr(Hypotenuse)

        'Create sinusoidal pattern along x-axis
        Dim dxScale As Double
        dxScale = Hypotenuse / 1440     '4 cycles
        For i = 0 To 1440
            WormPoints(i).x = i * dxScale
            WormPoints(i).y = Sin(i * pi180) * 80 '(Hypotenuse / 30)
        Next i

        'Rotate coordinates by angle radians
        For i = 0 To 1440
            With WormPoints(i)
                dxWorm(i).x = x1 + (.x * Cos(Angle)) - (.y * Sin(Angle))
                dxWorm(i).y = (y1 + (.x * Sin(Angle)) + (.y * Cos(Angle)) * 1)
            End With
        Next

        Dim dxStep As Long
        Dim lastx As Double
        Dim lasty As Double
        dxStep = 50
        For i = 0 To 1440 - dxStep Step dxStep
            With dxWorm(i)
            obj.Line (.x, .y)-(dxWorm(i + dxStep).x, dxWorm(i + dxStep).y)
                lastx = dxWorm(i + dxStep).x
                lasty = dxWorm(i + dxStep).y
            End With
        Next

        Dim dx As Single
        Dim dy As Single
        Dim head As Single
        dx = x2 - x1
        dy = y2 - y1
        head = Sqr(dx * dx + dy * dy)
        If head > 0 Then
            dx = dx / head * 150
            dy = dy / head * 150
        obj.Line (lastx, lasty)-Step(-dx - dy, -dy + dx)
        obj.Line (lastx, lasty)-Step(-dx + dy, -dy - dx)
        End If
    End Sub

    Private Function High(ByVal val1 As Single, ByVal val2 As Single) As Single
        If val1 >= val2 Then
            High = val1
        Else
            High = val2
        End If
    End Function

    Private Function Low(ByVal val1 As Single, ByVal val2 As Single) As Single
        If val1 <= val2 Then
            Low = val1
        Else
            Low = val2
        End If
    End Function

    Private Sub DrawEllipse(ByVal obj As Object, ByVal xmin As Single, ByVal ymin As Single, ByVal xmax As Single, ByVal ymax As Single)
        Dim cx As Single
        Dim cy As Single
        Dim wid As Single
        Dim hgt As Single
        Dim aspect As Single
        Dim radius As Single

        ' Find the center.
        cx = (xmin + xmax) / 2
        cy = (ymin + ymax) / 2

        ' Get the ellipse's size.
        wid = xmax - xmin
        hgt = ymax - ymin

        ' Do nothing if the width or height is zero.
        If (wid = 0) Or (hgt = 0) Then Exit Sub

        aspect = hgt / wid

        ' See which dimension is larger.
        If wid > hgt Then
            ' The major axis is horizontal.
            ' Get the radius in custom coordinates.
            radius = wid / 2
        Else
            ' The major axis is vertical.
            ' Get the radius in custom coordinates.
            radius = hgt / 2
        End If

        ' Draw the circle.
        On Error Resume Next
    obj.Circle (cx, cy), radius, , , , aspect
    End Sub

    Public Sub PenDown(ByVal obj As Object, ByVal x As Single, ByVal y As Single)

        ShapeComplete = False

        ' Set spatial coordinates.
        StartX = x
        StartY = y
        EndX = x
        EndY = y

        'Draw point at this time only.
        If Me.shape = dsPoint Then
            DrawShape(obj, StartX, StartY, EndX, EndY, Me.shape, True, False)
            Rubberbanding = False
        End If

        If shape = dsFreehand Then
            FreeHandActive = True
            'Save freehand start point.
            Erase FreeHandPoints
                ReDim FreeHandPoints(1) As PenPoints
            PointCount = 1
            FreeHandPoints(1).x = x
            FreeHandPoints(1).y = y
            'Draw first penpoint.
            DrawShape(obj, StartX, StartY, EndX, EndY, dsFreehand, True, False)
            Rubberbanding = False
            Exit Sub
        End If

        ' Save user settings for drawing after rubberbanding.
        userStyle = Style
        userMode = DrawMode
        userFill = FillStyle

        Rubberbanding = True

        ' Draw the new rubberband object.
        Style = vbDot
        DrawMode = vbCopyPen
        FillStyle = vbFSTransparent
        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

    End Sub

    Public Sub PenMove(ByVal obj As Object, ByVal x As Single, ByVal y As Single)
        If shape = dsText Then
            If x <= StartX Or y <= StartY Then Exit Sub
        End If

        If shape = dsFreehand And FreeHandActive = True Then

            DrawShape(obj, x, y, EndX, EndY, dsFreehand, True, False)
            PointCount = PointCount + 1
        ReDim Preserve FreeHandPoints(PointCount) As PenPoints
            FreeHandPoints(PointCount).x = x
            FreeHandPoints(PointCount).y = y

            ' Save the new ending coordinates.
            EndX = x
            EndY = y
        End If

        If Not Rubberbanding Then Exit Sub


        ' Erase the previous rubberband object.
        If shape = dsLine Or dsArrow1 Or dsArrow2 Or dsworm Then
            Style = vbSolid
        Else
            Style = vbDot
        End If

        DrawMode = vbInvert
        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

        ' Save the new ending coordinates.
        EndX = x
        EndY = y

        ' Draw the new rubberband object.
        If shape = dsLine Or dsArrow1 Or dsArrow2 Or dsworm Then
            Style = vbSolid
        Else
            Style = vbDot
        End If

        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

    End Sub

    Public Sub PenUp(ByVal obj As Object, ByVal x As Single, ByVal y As Single)

        FreeHandActive = False

        ' If not rubberbanding, do nothing.
        If Not Rubberbanding Then Exit Sub

        'If text is selected then show dialog
        If shape = dsText Then mvarCaption = InputBox("Enter text:", App.Title)

        ' End rubberbanding.
        Rubberbanding = False

        ' Erase the previous rubberband object.
        DrawMode = vbInvert
        DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, False)

        ' Resinstate user settings after rubberbanding.
        Style = userStyle
        DrawMode = userMode
        FillStyle = userFill

        ' Rectify shape dimensions (not for lines & arrows).
        If shape = dsBox Or shape = dsEllipse Then
            xmin = Low(StartX, EndX)
            ymin = Low(StartY, EndY)
            xmax = High(StartX, EndX)
            ymax = High(StartY, EndY)

            StartX = xmin
            StartY = ymin
            EndX = xmax
            EndY = ymax
        Else
            xmin = StartX
            xmax = EndX
            ymin = StartY
            ymax = EndY
        End If

        Height = CSng(Sqr((ymax - ymin) ^ 2))
        Width = CSng(Sqr((xmax - xmin) ^ 2))

        ' Draw the final object.
        ShapeComplete = True
        If mvarEnabled Then
            DrawShape(obj, StartX, StartY, EndX, EndY, shape, True, True)
        End If


    End Sub

    Public Sub EditText()
        'If text is selected then show dialog
        Dim newText As String
        newText = InputBox("Enter text:", App.Title, mvarCaption)
        If Len(newText) > 0 Then mvarCaption = newText

    End Sub

    Private Sub DashLine(ByVal obj As Object, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single, ByVal dash_length As Single, ByVal skip_length As Single, ByVal draw_width As Long)
        Dim dash_dx As Single
        Dim dash_dy As Single
        Dim skip_dx As Single
        Dim skip_dy As Single
        Dim Length As Single
        Dim x As Single
        Dim y As Single
        Dim i As Long
        Dim i_max As Long

        ' Get vectors in the desired direction
        ' with the right length.
        skip_dx = x2 - x1
        skip_dy = y2 - y1
        Length = Sqr(skip_dx * skip_dx + skip_dy * skip_dy)
        If Length > 0 Then
            dash_dx = skip_dx / Length * dash_length
            dash_dy = skip_dy / Length * dash_length
            skip_dx = skip_dx / Length * skip_length
            skip_dy = skip_dy / Length * skip_length

            obj.DrawWidth = draw_width
            x = x1
            y = y1
            i_max = Int(Length / (dash_length + skip_length))
            For i = 1 To i_max
            obj.Line (x, y)-Step(dash_dx, dash_dy)
                x = x + dash_dx + skip_dx
                y = y + dash_dy + skip_dy
            Next i

            ' See how much line is undrawn.
            Length = Length - i_max * (dash_length + skip_length)
            If Length > dash_length Then
            obj.Line (x, y)-Step(dash_dx, dash_dy)
            Else
            obj.Line (x, y)-(x2, y2)
            End If
        End If
    End Sub

Public Property Get Editable() As Boolean
    If shape = dsFreehand Then
        Editable = False
    Else
        Editable = True
    End If
End Property

Public Property Get x1() As Single
    x1 = xmin
    End Property

Public Property Let x1(ByVal vData As Single)
    xmin = vData
    Width = CSng(Sqr((xmax - xmin) ^ 2))
    StartX = xmin
    End Property

Public Property Get x2() As Single
    x2 = xmax
    End Property
Public Property Let x2(ByVal vData As Single)
    xmax = vData
    Width = CSng(Sqr((xmax - xmin) ^ 2))
    EndX = xmax
    End Property

Public Property Get y1() As Single
    y1 = ymin
    End Property
Public Property Let y1(ByVal vData As Single)
    ymin = vData
    Height = CSng(Sqr((ymax - ymin) ^ 2))
    StartY = ymin
    End Property

Public Property Get y2() As Single
    y2 = ymax
    End Property
Public Property Let y2(ByVal vData As Single)
    ymax = vData
    Height = CSng(Sqr((ymax - ymin) ^ 2))
    EndY = ymax
    End Property

Public Property Let ShapeSize(ByVal vData As Long)
    mvarShapeSize = vData
    End Property


Public Property Get ShapeSize() As Long
    ShapeSize = mvarShapeSize
    End Property




    Private Sub Class_Initialize()
        'set defaults
        With Me
            .DrawMode = vbCopyPen
            .FillColor = vbBlack
            .FillStyle = vbFSTransparent
            .LineColor = vbBlack
            .LineWidth = 1
            .Style = vbSolid
            .shape = dsBox
            .Caption = ""
            .ShowCaption = False
            .Font = "Arial"
            .FontSize = 10
            .FontBold = False
            .FontItalic = False
            .FontUnderline = False
            .CaptionPosition = capTopLeft
            .FontColor = .LineColor
            .ShapeSize = 75
        End With


    End Sub

    Private Sub Class_Terminate()
        On Error Resume Next
        lblCaption(shape).Visible = False
        lblCaption(shape).Enabled = False
        frmObject.Controls.Remove(lblUnique)
        CaptionCreated = False

        '    If CaptionCreated = True Then
        '        On Error GoTo ExitTerm
        '        lblCaption(CaptionID).Visible = False
        '        Set lblCaption(CaptionID) = Nothing
        '    End If
        '
        'ExitTerm:
    End Sub
End Class

