Imports System.Windows.Forms

Public Class frmConfirmVPL
    Private mvarAppendVPL As String = Nothing

    Public ReadOnly Property AppendVPL() As Integer
        Get
            Return mvarAppendVPL
        End Get
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.lstVPLs.SelectedItem = "<New VPL...>" Then
            mvarAppendVPL = -1
        Else
            For Each frm As Form In frmMain.MdiChildren
                If frm.GetType Is GetType(frmVideoPlayList) And frm.Text = Me.lstVPLs.SelectedItem.ToString Then
                    mvarAppendVPL = TryCast(frm, frmVideoPlayList).idForm
                End If
            Next
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmConfirmVPL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstVPLs.Items.Clear()
        Me.lstVPLs.Items.Add("<New VPL...>")
        For Each frm As Form In frmMain.MdiChildren
            If frm.GetType Is GetType(frmVideoPlayList) And frm.Visible Then
                Me.lstVPLs.Items.Add(frm.Text)
            End If
        Next
        Me.lstVPLs.SelectedIndex = 0
    End Sub
End Class
