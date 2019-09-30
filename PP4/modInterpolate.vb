Module modInterpolate

    Public Structure ReferenceGrid
        Dim Coordinate As PointF
        Public Value As Single
    End Structure


    Function InterpolatePitchCoordinates(ByVal Pt As PointF, ByVal pitchRect As RectangleF, ByVal Q11 As Single, ByVal Q12 As Single, ByVal Q21 As Single, ByVal Q22 As Single) As Single
        'First, set x1, y1, x2, y2 reference values
        'NB Q11 = bottom-left
        ' Q21 = bottom-right
        ' Q12 = top-left
        ' Q22 = top-right


        Dim x1 As Single = pitchRect.Left
        Dim x2 As Single = pitchRect.Right
        Dim y1 As Single = pitchRect.Bottom
        Dim y2 As Single = pitchRect.Top

        'Linear interpolation in the x plane
        Dim r1 As Single = (((x2 - Pt.X) / (x2 - x1)) * Q11) + (((Pt.X - x1) / (x2 - x1)) * Q21)  'where r1 = (x, y1)
        Dim r2 As Single = (((x2 - Pt.X) / (x2 - x1)) * Q12) + (((Pt.X - x1) / (x2 - x1)) * Q22)  'where r2 = (x, y2)
        InterpolatePitchCoordinates = (((y2 - Pt.Y) / (y2 - y1)) * r1) + (((Pt.Y - y1) / (y2 - y1)) * r2)
        Return InterpolatePitchCoordinates
    End Function


    Function Interpolate(ByVal Pt As PointF, ByVal Refs(,) As ReferenceGrid) As Single
        'Pt = x, y

        'First, set x1, y1, x2, y2 reference values
        Dim x1, x2, y1, y2 As Single
        Dim Rx1, Rx2, Ry1, Ry2 As Integer

        'Get x1, x2
        For xi As Integer = 1 To Refs.GetUpperBound(0)
            If Refs(xi, 0).Coordinate.X > Pt.X Then
                x2 = Refs(xi, 0).Coordinate.X
                x1 = Refs(xi - 1, 0).Coordinate.X
                Rx2 = xi
                Rx1 = xi - 1
                Exit For
            End If
        Next

        'Get y1, y2
        For yi As Integer = 1 To Refs.GetUpperBound(1)
            If Refs(0, yi).Coordinate.Y > Pt.Y Then
                y2 = Refs(0, yi).Coordinate.Y
                y1 = Refs(0, yi - 1).Coordinate.Y
                Ry2 = yi
                Ry1 = yi - 1
                Exit For
            End If
        Next

        'Linear interpolation in the x plane
        Dim r1 As Single = (((x2 - Pt.X) / (x2 - x1)) * Refs(Rx1, Ry1).Value) + (((Pt.X - x1) / (x2 - x1)) * Refs(Rx2, Ry1).Value)  'where r1 = (x, y1)
        Dim r2 As Single = (((x2 - Pt.X) / (x2 - x1)) * Refs(Rx1, Ry2).Value) + (((Pt.X - x1) / (x2 - x1)) * Refs(Rx2, Ry2).Value)  'where r2 = (x, y2)
        Interpolate = (((y2 - Pt.Y) / (y2 - y1)) * r1) + (((Pt.Y - y1) / (y2 - y1)) * r2)

        If Not Interpolate > 0 Then Interpolate = 0.0

    End Function

    Function Linearinter22d(ByVal inputs As Object, ByVal X As Double, ByVal Y As Double) As Double

        Dim nx As Long, ny As Long
        Dim lowerx As Long, lowery As Long, upperx As Long, uppery As Long, i As Long

        nx = inputs.Rows.Count
        ny = inputs.Columns.Count

        If X < inputs(2, 1) Then
            lowerx = 2
            upperx = 2
        ElseIf X > inputs(nx, 1) Then
            lowerx = nx
            upperx = nx
        Else
            For i = 2 To nx
                If inputs(i, 1) >= X Then
                    upperx = i
                    lowerx = i - 1
                    Exit For
                End If
            Next
        End If

        If Y < inputs(1, 2) Then
            lowery = 2
            uppery = 2
        ElseIf Y > inputs(1, ny) Then
            lowery = ny
            uppery = ny
        Else
            For i = 1 To ny
                If inputs(1, i) >= Y Then
                    uppery = i
                    lowery = i - 1
                    Exit For
                End If
            Next
        End If

        Dim XL As Double, XU As Double, YL As Double, YU As Double
        Dim temp1 As Double, temp2 As Double

        XL = inputs(lowerx, 1)
        XU = inputs(upperx, 1)
        YL = inputs(1, lowery)
        YU = inputs(1, uppery)
        temp1 = (inputs(lowerx, lowery) * (XU - X) _
        + inputs(upperx, lowery) * (X - XL)) / (XU - XL)
        temp2 = (inputs(lowerx, uppery) * (XU - X) _
        + inputs(upperx, uppery) * (X - XL)) / (XU - XL)
        Linearinter22d = (temp1 * (YU - Y) + temp2 * (Y - YL)) / (YU - YL)

    End Function



End Module
