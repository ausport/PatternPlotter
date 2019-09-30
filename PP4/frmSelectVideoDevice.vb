Imports System.Windows.Forms

Public Class frmSelectVideoDevice
    Dim wait As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        wait = True
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        wait = True
        Me.Close()
    End Sub

    Public Sub New()
        'ByVal szDevices() As String
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Function GetDefaultVideoDevice(ByVal szDevices() As String) As Integer

        For Each sz As String In szDevices
            Me.lstVideoDevices.Items.Add(sz)
        Next
        Me.MdiParent = frmMain
        Me.Show()

        While Not wait
            Application.DoEvents()
        End While

        Return Me.lstVideoDevices.SelectedIndex
    End Function

    Private Sub lstVideoDevices_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstVideoDevices.DoubleClick
        wait = True
    End Sub

    Private Sub frmSelectVideoDevice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
