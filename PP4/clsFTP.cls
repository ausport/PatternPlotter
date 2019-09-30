'This class permits you to perform direct connections to FTP sites in Visual Basic. NET.
' The class supports the following FTP commands:
'   - Upload a file
'   - Download a file
'   - Create a directory
'   - Remove a directory
'   - Change directory
'   - Remove a file
'   - Rename a file
'   - Set the user name of the remote user
'   - Set the password of the remote user

Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.Sockets


'FTP Class
Public Class clsFTP

#Region "Class Variable Declarations"
    Private m_sRemoteHost, m_sRemotePath, m_sRemoteUser As String
    Private m_sRemotePassword, m_sMess As String
    Private m_iRemotePort, m_iBytes As Int32
    Private m_objClientSocket As Socket

    Private m_iRetValue As Int32
    Private m_bLoggedIn As Boolean
    Private m_sMes, m_sReply As String

    'Set the size of the packet that is used to read and to write data to the FTP server 
    'to the following specified size.
    Public Const BLOCK_SIZE = 512
    Private ASCII As Encoding = Encoding.ASCII
    Public flag_bool As Boolean
    Private m_aBuffer(BLOCK_SIZE) As Byte
    'General variable declaration
    Private m_sMessageString As String
#End Region

#Region "Class Constructors"

    ' Main class constructor
    Public Sub New()
        m_sRemoteHost = "microsoft"
        m_sRemotePath = "."
        m_sRemoteUser = "anonymous"
        m_sRemotePassword = ""
        m_sMessageString = ""
        m_iRemotePort = 21
        m_bLoggedIn = False
    End Sub

    ' Parameterized constructor
    Public Sub New(ByVal sRemoteHost As String, _
                   ByVal sRemotePath As String, _
                   ByVal sRemoteUser As String, _
                   ByVal sRemotePassword As String, _
                   Optional ByVal iRemotePort As Int32 = 21)
        m_sRemoteHost = sRemoteHost
        m_sRemotePath = sRemotePath
        m_sRemoteUser = sRemoteUser
        m_sRemotePassword = sRemotePassword
        m_sMessageString = ""
        m_iRemotePort = iRemotePort
        m_bLoggedIn = False
    End Sub
#End Region

#Region "Public Properties"

    'Set or Get the name of the FTP server that you want to connect to.
    Public Property RemoteHostFTPServer() As String
        'Get the name of the FTP server. 
        Get
            Return m_sRemoteHost
        End Get
        'Set the name of the FTP server. 
        Set(ByVal Value As String)
            m_sRemoteHost = Value
        End Set
    End Property

    'Set or Get the FTP port number of the FTP server that you want to connect to.
    Public Property RemotePort() As Int32
        'Get the FTP port number. 
        Get
            Return m_iRemotePort
        End Get
        'Set the FTP port number. 
        Set(ByVal Value As Int32)
            m_iRemotePort = Value

        End Set
    End Property

    'Set or Get the remote path of the FTP server that you want to connect to.
    Public Property RemotePath() As String
        'Get the remote path. 
        Get
            Return m_sRemotePath
        End Get
        'Set the remote path. 
        Set(ByVal Value As String)
            m_sRemotePath = Value
        End Set
    End Property

    'Set the remote password of the FTP server that you want to connect to.
    Public Property RemotePassword() As String
        Get
            Return m_sRemotePassword
        End Get
        Set(ByVal Value As String)
            m_sRemotePassword = Value
        End Set
    End Property

    'Set or Get the remote user of the FTP server that you want to connect to.
    Public Property RemoteUser() As String
        Get
            Return m_sRemoteUser
        End Get
        Set(ByVal Value As String)
            m_sRemoteUser = Value
        End Set
    End Property

    'Set the class messagestring.
    Public Property MessageString() As String
        Get
            Return m_sMessageString
        End Get
        Set(ByVal Value As String)
            m_sMessageString = Value
        End Set
    End Property

#End Region

#Region "Public Subs and Functions"

    'Return a list of files from the file system. Return these files in a string() array.
    Public Function GetFileList(ByVal sMask As String) As String()
        Dim cSocket As Socket
        Dim bytes As Int32
        Dim seperator As Char = ControlChars.Lf
        Dim mess() As String

        m_sMes = ""
        'Check to see if you are logged on to the FTP server.
        If (Not (m_bLoggedIn)) Then
            Login()
        End If

        cSocket = CreateDataSocket()
        'Send an FTP command. 
        SendCommand("NLST " & sMask)

        If (Not (m_iRetValue = 150 Or m_iRetValue = 125)) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        m_sMes = ""
        Do While (True)
            m_aBuffer.Clear(m_aBuffer, 0, m_aBuffer.Length)
            bytes = cSocket.Receive(m_aBuffer, m_aBuffer.Length, 0)
            m_sMes += ASCII.GetString(m_aBuffer, 0, bytes)

            If (bytes < m_aBuffer.Length) Then
                Exit Do
            End If
        Loop

        mess = m_sMes.Split(seperator)
        cSocket.Close()
        ReadReply()

        If (m_iRetValue <> 226) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        Return mess
    End Function

    ' Get the size of the file on the FTP server.
    Public Function GetFileSize(ByVal sFileName As String) As Long
        Dim size As Long

        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command. 
        SendCommand("SIZE " & sFileName)
        size = 0

        If (m_iRetValue = 213) Then
            size = Int64.Parse(m_sReply.Substring(4))
        Else
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        Return size
    End Function


    'Log on to the FTP server.
    Public Function Login() As Boolean

        m_objClientSocket = _
        New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        'Dim ep As New IPEndPoint(Dns.Resolve(m_sRemoteHost).AddressList(0), m_iRemotePort)
        Dim ep As New IPEndPoint(Dns.GetHostEntry(m_sRemoteHost).AddressList(0), m_iRemotePort)

        Try
            m_objClientSocket.Connect(ep)
        Catch ex As Exception
            MessageString = m_sReply
            Throw New IOException("Cannot connect to the remote server")

        End Try

        ReadReply()
        If (m_iRetValue <> 220) Then
            CloseConnection()
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If
        'Send an FTP command to send a user logon ID to the server.
        SendCommand("USER " & m_sRemoteUser)
        If (Not (m_iRetValue = 331 Or m_iRetValue = 230)) Then
            Cleanup()
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        If (m_iRetValue <> 230) Then
            'Send an FTP command to send a user logon password to the server.
            SendCommand("PASS " & m_sRemotePassword)
            If (Not (m_iRetValue = 230 Or m_iRetValue = 202)) Then
                Cleanup()
                MessageString = m_sReply
                Throw New IOException(m_sReply.Substring(4))
            End If
        End If

        m_bLoggedIn = True
        'Call the ChangeDirectory user-defined function to change the directory to the 
        'remote FTP folder that is mapped.
        ChangeDirectory(m_sRemotePath)

        'Return the final result.
        Return m_bLoggedIn
    End Function

    'If the value of mode is true, set binary mode for downloads. Otherwise, set ASCII mode.
    Public Sub SetBinaryMode(ByVal bMode As Boolean)

        If (bMode) Then
            'Send the FTP command to set the binary mode.
            '(TYPE is an FTP command that is used to specify representation type.)
            SendCommand("TYPE I")
        Else
            'Send the FTP command to set ASCII mode. 
            '(TYPE is an FTP command that is used to specify representation type.)
            SendCommand("TYPE A")
        End If

        If (m_iRetValue <> 200) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If
    End Sub
    ' Download a file to the local directory of the assembly. Keep the same file name.
    Public Sub DownloadFile(ByVal sFileName As String)
        DownloadFile(sFileName, "", False)
    End Sub
    ' Download a remote file to the local directory of the Assembly. Keep the same file name.
    Public Sub DownloadFile(ByVal sFileName As String, _
                            ByVal bResume As Boolean)
        DownloadFile(sFileName, "", bResume)
    End Sub
    'Download a remote file to a local file name. You must include a path.
    'The local file name will be created or will be overwritten, but the path must exist.
    Public Sub DownloadFile(ByVal sFileName As String, _
                            ByVal sLocalFileName As String)
        DownloadFile(sFileName, sLocalFileName, False)
    End Sub
    ' Download a remote file to a local file name. You must include a path. Set the 
    ' resume flag. The local file name will be created or will be overwritten, but the path must exist.
    Public Sub DownloadFile(ByVal sFileName As String, _
                            ByVal sLocalFileName As String, _
                            ByVal bResume As Boolean)
        Dim st As Stream
        Dim output As FileStream
        Dim cSocket As Socket
        Dim offset, npos As Long

        If (Not (m_bLoggedIn)) Then
            Login()
        End If

        SetBinaryMode(True)

        If (sLocalFileName.Equals("")) Then
            sLocalFileName = sFileName
        End If

        If (Not (File.Exists(sLocalFileName))) Then
            st = File.Create(sLocalFileName)
            st.Close()
        End If

        output = New FileStream(sLocalFileName, FileMode.Open)
        cSocket = CreateDataSocket()
        offset = 0

        If (bResume) Then
            offset = output.Length

            If (offset > 0) Then
                'Send an FTP command to restart.
                SendCommand("REST " & offset)
                If (m_iRetValue <> 350) Then
                    offset = 0
                End If
            End If

            If (offset > 0) Then
                npos = output.Seek(offset, SeekOrigin.Begin)
            End If
        End If
        'Send an FTP command to retrieve a file.
        SendCommand("RETR " & sFileName)

        If (Not (m_iRetValue = 150 Or m_iRetValue = 125)) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        Do While (True)
            m_aBuffer.Clear(m_aBuffer, 0, m_aBuffer.Length)
            m_iBytes = cSocket.Receive(m_aBuffer, m_aBuffer.Length, 0)
            output.Write(m_aBuffer, 0, m_iBytes)

            If (m_iBytes <= 0) Then
                Exit Do
            End If
        Loop

        output.Close()
        If (cSocket.Connected) Then
            cSocket.Close()
        End If

        ReadReply()
        If (Not (m_iRetValue = 226 Or m_iRetValue = 250)) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

    End Sub
    'This is a function that is used to upload a file from your local hard disk to your FTP site.
    Public Sub UploadFile(ByVal sFileName As String)
        UploadFile(sFileName, False)
    End Sub
    ' This is a function that is used to upload a file from your local hard disk to your FTP site 
    ' and then set the resume flag.
    Public Sub UploadFile(ByVal sFileName As String, _
                          ByVal bResume As Boolean)
        Dim cSocket As Socket
        Dim offset As Long
        Dim input As FileStream
        Dim bFileNotFound As Boolean

        If (Not (m_bLoggedIn)) Then
            Login()
        End If

        cSocket = CreateDataSocket()
        offset = 0

        If (bResume) Then
            Try
                SetBinaryMode(True)
                offset = GetFileSize(sFileName)
            Catch ex As Exception
                offset = 0
            End Try
        End If

        If (offset > 0) Then
            SendCommand("REST " & offset)
            If (m_iRetValue <> 350) Then

                'The remote server may not support resuming.
                offset = 0
            End If
        End If
        'Send an FTP command to store a file.
        SendCommand("STOR " & Path.GetFileName(sFileName))
        If (Not (m_iRetValue = 125 Or m_iRetValue = 150)) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        'Check to see if the file exists before the upload.
        bFileNotFound = False
        If (File.Exists(sFileName)) Then
            ' Open the input stream to read the source file.
            input = New FileStream(sFileName, FileMode.Open)
            If (offset <> 0) Then
                input.Seek(offset, SeekOrigin.Begin)
            End If

            'Upload the file. 
            m_iBytes = input.Read(m_aBuffer, 0, m_aBuffer.Length)
            Do While (m_iBytes > 0)
                cSocket.Send(m_aBuffer, m_iBytes, 0)
                m_iBytes = input.Read(m_aBuffer, 0, m_aBuffer.Length)
            Loop
            input.Close()
        Else
            bFileNotFound = True
        End If

        If (cSocket.Connected) Then
            cSocket.Close()
        End If

        'Check the return value if the file was not found.
        If (bFileNotFound) Then
            MessageString = m_sReply
            Throw New IOException("The file: " & sFileName & " was not found. " & _
           "Cannot upload the file to the FTP site")
        End If

        ReadReply()
        If (Not (m_iRetValue = 226 Or m_iRetValue = 250)) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If
    End Sub
    ' Delete a file from the remote FTP server.
    Public Function DeleteFile(ByVal sFileName As String) As Boolean
        Dim bResult As Boolean

        bResult = True
        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command to delete a file.
        SendCommand("DELE " & sFileName)
        If (m_iRetValue <> 250) Then
            bResult = False
            MessageString = m_sReply
        End If

        ' Return the final result.
        Return bResult
    End Function
    ' Rename a file on the remote FTP server.
    Public Function RenameFile(ByVal sOldFileName As String, _
                               ByVal sNewFileName As String) As Boolean
        Dim bResult As Boolean

        bResult = True
        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command to rename from a file.
        SendCommand("RNFR " & sOldFileName)
        If (m_iRetValue <> 350) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        'Send an FTP command to rename a file to a new file name.
        'It will overwrite if newFileName exists. 
        SendCommand("RNTO " & sNewFileName)
        If (m_iRetValue <> 250) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If
        ' Return the final result.
        Return bResult
    End Function

    'This is a function that is used to create a directory on the remote FTP server.
    Public Function CreateDirectory(ByVal sDirName As String) As Boolean
        Dim bResult As Boolean

        bResult = True
        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command to make directory on the FTP server.
        SendCommand("MKD " & sDirName)
        If (m_iRetValue <> 257) Then
            bResult = False
            MessageString = m_sReply
        End If

        ' Return the final result.
        Return bResult
    End Function
    ' This is a function that is used to delete a directory on the remote FTP server.
    Public Function RemoveDirectory(ByVal sDirName As String) As Boolean
        Dim bResult As Boolean

        bResult = True
        'Check if logged on to the FTP server
        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command to remove directory on the FTP server.
        SendCommand("RMD " & sDirName)
        If (m_iRetValue <> 250) Then
            bResult = False
            MessageString = m_sReply
        End If

        ' Return the final result.
        Return bResult
    End Function
    'This is a function that is used to change the current working directory on the remote FTP server.
    Public Function ChangeDirectory(ByVal sDirName As String) As Boolean
        Dim bResult As Boolean

        bResult = True
        'Check if you are in the root directory.
        If (sDirName.Equals(".")) Then
            Exit Function
        End If
        'Check if logged on to the FTP server
        If (Not (m_bLoggedIn)) Then
            Login()
        End If
        'Send an FTP command to change directory on the FTP server.
        SendCommand("CWD " & sDirName)
        If (m_iRetValue <> 250) Then
            bResult = False
            MessageString = m_sReply
        End If

        Me.m_sRemotePath = sDirName

        ' Return the final result.
        Return bResult
    End Function
    ' Close the FTP connection of the remote server.
    Public Sub CloseConnection()
        If (Not (m_objClientSocket Is Nothing)) Then
            'Send an FTP command to end an FTP server system.
            SendCommand("QUIT")
        End If

        Cleanup()
    End Sub

#End Region

#Region "Private Subs and Functions"
    ' Read the reply from the FTP server.
    Private Sub ReadReply()
        m_sMes = ""
        m_sReply = ReadLine()
        m_iRetValue = Int32.Parse(m_sReply.Substring(0, 3))
    End Sub

    ' Clean up some variables.
    Private Sub Cleanup()
        If Not (m_objClientSocket Is Nothing) Then
            m_objClientSocket.Close()
            m_objClientSocket = Nothing
        End If

        m_bLoggedIn = False
    End Sub
    ' Read a line from the FTP server.
    Private Function ReadLine(Optional ByVal bClearMes As Boolean = False) As String
        Dim seperator As Char = ControlChars.Lf
        Dim mess() As String

        If (bClearMes) Then
            m_sMes = ""
        End If
        Do While (True)
            m_aBuffer.Clear(m_aBuffer, 0, BLOCK_SIZE)
            m_iBytes = m_objClientSocket.Receive(m_aBuffer, m_aBuffer.Length, 0)
            m_sMes += ASCII.GetString(m_aBuffer, 0, m_iBytes)
            If (m_iBytes < m_aBuffer.Length) Then
                Exit Do
            End If
        Loop

        mess = m_sMes.Split(seperator)
        If (m_sMes.Length > 2) Then
            m_sMes = mess(mess.Length - 2)
        Else
            m_sMes = mess(0)
        End If

        If (Not (m_sMes.Substring(3, 1).Equals(" "))) Then
            Return ReadLine(True)
        End If

        Return m_sMes
    End Function
    ' This is a function that is used to send a command to the FTP server that you are connected to.
    Private Sub SendCommand(ByVal sCommand As String)
        sCommand = sCommand & ControlChars.CrLf
        Dim cmdbytes As Byte() = ASCII.GetBytes(sCommand)
        m_objClientSocket.Send(cmdbytes, cmdbytes.Length, 0)
        ReadReply()
    End Sub
    ' Create a data socket.
    Private Function CreateDataSocket() As Socket
        Dim index1, index2, len As Int32
        Dim partCount, i, port As Int32
        Dim ipData, buf, ipAddress As String
        Dim parts(6) As Int32
        Dim ch As Char
        Dim s As Socket
        Dim ep As IPEndPoint
        'Send an FTP command to use passive data connection. 
        SendCommand("PASV")
        If (m_iRetValue <> 227) Then
            MessageString = m_sReply
            Throw New IOException(m_sReply.Substring(4))
        End If

        index1 = m_sReply.IndexOf("(")
        index2 = m_sReply.IndexOf(")")
        ipData = m_sReply.Substring(index1 + 1, index2 - index1 - 1)

        len = ipData.Length
        partCount = 0
        buf = ""

        For i = 0 To ((len - 1) And partCount <= 6)
            ch = Char.Parse(ipData.Substring(i, 1))
            If (Char.IsDigit(ch)) Then
                buf += ch
            ElseIf (ch <> ",") Then
                MessageString = m_sReply
                Throw New IOException("Malformed PASV reply: " & m_sReply)
            End If

            If ((ch = ",") Or (i + 1 = len)) Then
                Try
                    parts(partCount) = Int32.Parse(buf)
                    partCount += 1
                    buf = ""
                Catch ex As Exception
                    MessageString = m_sReply
                    Throw New IOException("Malformed PASV reply: " & m_sReply)
                End Try
            End If
        Next

        ipAddress = parts(0) & "." & parts(1) & "." & parts(2) & "." & parts(3)

        ' Make this call in Visual Basic .NET 2002. You want to 
        ' bitshift the number by 8 bits. In Visual Basic .NET 2002 you must
        ' multiply the number by 2 to the power of 8.
        'port = parts(4) * (2 ^ 8)

        ' Make this call and then comment out the previous line for Visual Basic .NET 2003.
        port = parts(4) << 8

        ' Determine the data port number.
        port = port + parts(5)

        s = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        'ep = New IPEndPoint(Dns.Resolve(ipAddress).AddressList(0), port)
        ep = New IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList(0), port)

        Try
            s.Connect(ep)
        Catch ex As Exception
            MessageString = m_sReply
            Throw New IOException("Cannot connect to remote server.")
            'If you cannot connect to the FTP server that is 
            'specified, make the boolean variable false.
            flag_bool = False
        End Try
        'If you can connect to the FTP server that is specified, make the boolean variable true.
        flag_bool = True
        Return s
    End Function

#End Region
End Class
