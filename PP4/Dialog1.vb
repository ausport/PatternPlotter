Imports System.Windows.Forms

Public Class dlgProgress

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub SetProgress(ByVal nValue As Integer, ByVal nTotal As Integer, Optional ByVal szCaption As String = "")
        Me.ProgressBar1.Minimum = 0
        Me.ProgressBar1.Maximum = nTotal
        Me.ProgressBar1.Value = nValue / nTotal
        If Len(szCaption) > 0 Then
            Me.lblProgressLabel.Text = szCaption
        Else
            Me.lblProgressLabel.Text = "Loading: " & nValue.ToString & " of " & nTotal.ToString & _
                " (" & Val(Int(nValue / nTotal) * 100).ToString & "%)"
        End If
        Me.ProgressPanel.Visible = True
    End Sub

    Public Sub EndProgress()
        Me.Close()
    End Sub

    Private Sub dlgProgress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
