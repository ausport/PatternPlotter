Module modDataMining

    Public Centroid(,) As Single  ' centroid (X and Y) of clusters; cluster number = column number
    Structure Meloid
        Dim Items() As Single
    End Structure

    Structure MatrixItem
        Dim Value As Single
        Dim dbID() As Long
        Dim supportA As Double   'suuport of the antecedent of a rule (A)
        Dim supportB As Double   'suuport of the antecedent of a rule (A)
        Dim supportAUB As Double   'support of the result of a rule (A U B)
        Dim n As Integer     'number of transactions
        'nb.  n is just a multi-purpose add-on for returning an extra bit of infomation when
        'a MatrixItem struct is returned.  A bit messy, but....
    End Structure

    Public modLastIterationCount As Integer = 0
    Public modLastTime As TimeSpan = Nothing


    Public Function CalculateSupport(ByVal FrequencyTable(,) As MatrixItem, ByVal iTransactions As Integer, ByVal ColumnSupport() As Single) As MatrixItem(,)
        'Return nValue
        Dim x As Integer = FrequencyTable.GetUpperBound(0)
        Dim y As Integer = FrequencyTable.GetUpperBound(1)
        Dim ReturnTable(x, y + 1) As MatrixItem
        'NB.  The columnb support values are stored in an extra level of the y-axis array.
        'This is column support - P(Xn) - for each column value of X.  This is passed on to CalculateConfidence, but is not used to calculate support.

        For z As Integer = 0 To x
            For w As Integer = 0 To y
                ReturnTable(z, w).Value = FrequencyTable(z, w).Value / iTransactions
                ReturnTable(z, w).dbID = FrequencyTable(z, w).dbID
            Next
            ReturnTable(z, y + 1).Value = ColumnSupport(z)
        Next
        Return ReturnTable
    End Function

    Public Function CalculateConfidence(ByVal FrequencyTable(,) As MatrixItem) As Single(,)
        'Return nValue
        Dim x As Integer = FrequencyTable.GetUpperBound(0)
        Dim y As Integer = FrequencyTable.GetUpperBound(1) - 1
        Dim ReturnTable(x, y) As Single

        For z As Integer = 0 To x
            For w As Integer = 0 To y
                On Error Resume Next
                ReturnTable(z, w) = FrequencyTable(z, w).Value / FrequencyTable(z, y + 1).Value
            Next
        Next
        Return ReturnTable
    End Function


    Public Function kMeanXYCluster(ByVal ScatterPoints As Microsoft.VisualBasic.Collection, ByVal iClusterCount As Integer, ByVal iMaxIterations As Integer, _
    Optional ByVal CalculateMeloids As Boolean = False) As Microsoft.VisualBasic.Collection
        'Returns an integer value corresponding to the assigned cluster.
        'iClusterCount = number of apriori clusters selected by user.
        'ScatterPoints = data structure containing x,y coordinates for spatial clustering.
        Dim SearchTime As New Stopwatch
        SearchTime.Start()

        'Declare centroid  variables.
        Dim nCentroid() As PointF = Nothing
        Dim iCluster(ScatterPoints.Count - 1) As Integer

        'Iterate through data structure
        Dim n As Integer = 0
        For Each ScatterPoint As ScatterInfo In ScatterPoints
            If n + 1 <= iClusterCount Then  'n is an index to a zero-based array.
                'NB: each datapoint is its own centroid until the number of points is greater than number of clusters.
                ReDim Preserve nCentroid(n) 'resize array
                nCentroid(n) = ScatterPoint.Location
                iCluster(n) = n

            Else
                Dim leastD As Single = Nothing  'Shortest distance b/w datapoint and centroid.
                Dim nearC As Integer = Nothing  'Nearest centroid to datapoint.

                For c As Integer = 0 To iClusterCount - 1
                    Dim distance As Single = GetDistance(ScatterPoint.Location, nCentroid(c))

                    If leastD = Nothing Or distance < leastD Then
                        leastD = distance
                        nearC = c
                    End If
                Next

                iCluster(n) = nearC
            End If
            n += 1
        Next

        Dim IterateAgain As Boolean = False
        Dim IterateCount As Integer = 0
        Dim tempScatterPoints As New Microsoft.VisualBasic.Collection
        Do
            'Now recalculate centroids.
            Dim SumOfPoints(iClusterCount - 1) As PointF
            Dim xArray(iClusterCount - 1) As Meloid
            Dim yArray(iClusterCount - 1) As Meloid
            Dim nPoints(iClusterCount - 1) As Integer

            n = 0
            IterateAgain = False
            If ScatterPoints Is Nothing Then Return Nothing
            For Each ScatterPoint As ScatterInfo In ScatterPoints
                If CalculateMeloids Then
                    Dim x As Integer = 0
                    If Not xArray(iCluster(n)).Items Is Nothing Then x = xArray(iCluster(n)).Items.Length
                    ReDim Preserve xArray(iCluster(n)).Items(x)
                    xArray(iCluster(n)).Items(x) = ScatterPoint.Location.X
                    Dim y As Integer = 0
                    If Not yArray(iCluster(n)).Items Is Nothing Then y = yArray(iCluster(n)).Items.Length
                    ReDim Preserve yArray(iCluster(n)).Items(y)
                    yArray(iCluster(n)).Items(y) = ScatterPoint.Location.Y
                Else
                    SumOfPoints(iCluster(n)).X += ScatterPoint.Location.X
                    SumOfPoints(iCluster(n)).Y += ScatterPoint.Location.Y
                    nPoints(iCluster(n)) += 1
                End If
                n += 1
            Next

            For c As Integer = 0 To iClusterCount - 1
                If CalculateMeloids Then
                    nCentroid(c).X = Median(xArray(c).Items)
                    nCentroid(c).Y = Median(yArray(c).Items)
                Else
                    nCentroid(c).X = SumOfPoints(c).X / nPoints(c)
                    nCentroid(c).Y = SumOfPoints(c).Y / nPoints(c)
                End If
            Next

            n = 0
            tempScatterPoints.Clear()
            For Each ScatterPoint As ScatterInfo In ScatterPoints
                Dim leastD As Single = Nothing  'Shortest distance b/w datapoint and centroid.
                Dim nearC As Integer = Nothing  'Nearest centroid to datapoint.

                For c As Integer = 0 To iClusterCount - 1
                    Dim distance As Single = GetDistance(ScatterPoint.Location, nCentroid(c))

                    If leastD = Nothing Or distance < leastD Then
                        leastD = distance
                        nearC = c
                    End If
                Next

                If iCluster(n) <> nearC Then IterateAgain = True
                iCluster(n) = nearC
                ScatterPoint.ClusterID = nearC
                tempScatterPoints.Add(ScatterPoint, n)
                n += 1
            Next

            IterateCount += 1
        Loop Until Not IterateAgain Or IterateCount >= iMaxIterations
        SearchTime.Stop()
        modLastIterationCount = IterateCount
        modLastTime = SearchTime.Elapsed

        Return tempScatterPoints

    End Function

    Public Function Median(ByVal NumericArray As Object) _
   As Double
        '******************************************************'
        'INPUT:   An Array of Numbers
        'RETURNS: The statistical median of that array.
        '         If invalid data is passed, i.e., a value that
        '         is not an array, or the Array contains non-numeric
        '         data, an error is raised
        'EXAMPLE:
        '        Dim vNumbers as Variant
        '        dim dblMedian as double
        '        vNumbers = array(4, 9, 1, 5, 3, 1, 7)
        '        dblMedian = Median(vNumbers)
        '****************************************************
        Dim arrLngAns As Object
        Dim lngElement1 As Long
        Dim lngElement2 As Long
        Dim dblSum As Double
        Dim dblAns As Double


        Dim lngElementCount As Long
        'sort array
        '        If Not IsArray(arrLngAns) Then
        ' Err.Raise(30000, , "Invalid Data Passed to function")

        If NumericArray Is Nothing Then Return Nothing
        Array.Sort(NumericArray)
        arrLngAns = NumericArray

        'Exit Function
        '     End If
        lngElementCount = (UBound(arrLngAns) - LBound(arrLngAns)) + 1

        If UBound(arrLngAns) Mod 2 = 0 Then
            lngElement1 = (UBound(arrLngAns) / 2) + _
               (LBound(arrLngAns) / 2)

        Else
            lngElement1 = Int(UBound(arrLngAns) / 2) + _
               Int(LBound(arrLngAns) / 2) + 1
        End If

        If lngElementCount Mod 2 <> 0 Then
            dblAns = arrLngAns(lngElement1)
        Else
            lngElement2 = lngElement1 + 1
            dblSum = arrLngAns(lngElement1 - 1) + arrLngAns(lngElement2 - 1)
            dblAns = dblSum / 2
        End If

        Median = dblAns
    End Function

    Private Function BubbleSortArray(ByVal NumericArray As Object) _
        As Object
        'http://www.freevbcode.com/ShowCode.Asp?ID=580


        Dim vAns As Object
        Dim vTemp As Object
        Dim bSorted As Boolean
        Dim lCtr As Long
        Dim lCount As Long
        Dim lStart As Long


        vAns = NumericArray

        If Not IsArray(vAns) Then
            BubbleSortArray = vbEmpty
            Exit Function
        End If

        On Error GoTo ErrorHandler

        lStart = LBound(vAns)
        lCount = UBound(vAns)

        bSorted = False

        Do While Not bSorted
            bSorted = True

            For lCtr = lCount - 1 To lStart Step -1
                If vAns(lCtr + 1) < vAns(lCtr) Then
                    Application.DoEvents()
                    bSorted = False
                    vTemp = vAns(lCtr)
                    vAns(lCtr) = vAns(lCtr + 1)
                    vAns(lCtr + 1) = vTemp
                End If
            Next lCtr

        Loop

        BubbleSortArray = vAns
        Exit Function

ErrorHandler:
        BubbleSortArray = vbEmpty
        Exit Function
    End Function

    Function GetDistance2(ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        ' calculate Euclidean distance
        GetDistance2 = Math.Sqrt((Y2 - Y1) ^ 2 + (X2 - X1) ^ 2)
    End Function

    Function GetDistance(ByVal pStart As PointF, ByVal pEnd As PointF)
        Return Math.Sqrt(((pEnd.X - pStart.X) ^ 2) + ((pEnd.Y - pStart.Y) ^ 2))
    End Function



    Private Function min2(ByVal num1, ByVal num2)
        ' return minimum value between two numbers
        If num1 < num2 Then
            min2 = num1
        Else
            min2 = num2
        End If
    End Function

End Module
