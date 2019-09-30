Module modMediaFunctions
    Private Const VIDEO_CLSID As String = "{73646976-0000-0010-8000-00AA00389B71}"  'video clsid
    Private Const AUDIO_CLSID As String = "{73647561-0000-0010-8000-00AA00389B71}"  'audio clsid

    Public Function GetMediaDuration(ByVal szFileName As String) As Double
        Dim test As New DexterLib.MediaDet
        Try
            test.Filename = szFileName
            Return test.StreamLength
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetMediaAudio(ByVal szFileName As String) As Boolean
        Dim test As New DexterLib.MediaDet
        Try
            Dim GetVidStream As Integer = -1
            test.Filename = szFileName
            'If test.OutputStreams > 1 Then Return True
            For nCount As Integer = 0 To test.OutputStreams - 1
                'Get Stream Type
                test.CurrentStream = nCount
                If test.StreamTypeB = AUDIO_CLSID Then Return True
            Next
        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function

    Public Function FindMediaFile(ByVal NativeFileName As String) As String
        'Finds media file and checks alternaive locations
        If UserPrefs.boolSearchPaths Then
            If Not IO.File.Exists(NativeFileName) Then
                For Each path As String In UserPrefs.szAlternativePath
                    If IO.File.Exists(path & My.Computer.FileSystem.GetName(NativeFileName)) Then
                        Return path & My.Computer.FileSystem.GetName(NativeFileName)
                    End If
                Next
            Else
                Return NativeFileName
            End If
        Else
            If IO.File.Exists(NativeFileName) Then
                Return NativeFileName
            End If
        End If

        Return Nothing
    End Function
End Module
