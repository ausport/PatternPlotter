Module modTrig

    Public Function GetRangeDiameter(ByVal locA As PointF, ByVal uRect As RectangleF) As Single

        'Returns a diameter for the circle that is centered as locA, and extends
        'to the furthest reaches of the rectangle uRect.
        'Any cartesian coordinate relative to the rectangle will that represents
        'the centre of a circle will have a diamater that extends to the furthest
        'point of the rectangle.  The diameter of that circle is returned.
        'This diameter will be the hypotenuse of a triangle with a horizonatal line extending
        'from the origin to the edge of the rectangle.
        'The hypotenuse can then be used to derive the length of whichever edge of the triangle is unknown.

        'First, determine furthest corner of the rectangle.
        Dim x As Single = Math.Max(ConvertSign(locA.X - uRect.Left), ConvertSign(locA.X - uRect.Right))
        Dim y As Single = Math.Max(ConvertSign(locA.Y - uRect.Top), ConvertSign(locA.Y - uRect.Bottom))

        Return Math.Sqrt((x ^ 2) + (y ^ 2))

    End Function

    Public Function ConvertSign(ByVal n As Single, Optional ByVal bConvert2Positive As Boolean = True) As Single
        If n < 0 Then n *= -1
        Return n
    End Function

    Public Function GetOppositeLength(ByVal theta As Single, ByVal adjacent As Single) As Single
        'Returns length of the opposite plane, where theta and adjacent values are known.
        'NB: theta in degrees.


        Return (Math.Tan(theta * (Math.PI / 180)) * adjacent)

    End Function

End Module
