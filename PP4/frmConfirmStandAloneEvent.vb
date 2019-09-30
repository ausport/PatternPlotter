Imports System.Windows.Forms

Public Class frmConfirmStandAloneEvent

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        AddStandAloneEventToVPL(txtCaption.Text, frmVideo.szVideoFileName, frmVideo.startPoint, frmVideo.stopPoint, _
        Me.txtGameID.Text, Me.txtTeamName.Text, Me.txtTimeCriteria.Text)

        szCurrentTimeCriteria = Me.txtTimeCriteria.Text
        szCurrentTeamName = Me.txtTeamName.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmConfirmStandAloneEvent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCaption.Text = "New Video Item"
        Me.txtTeamName.Text = szCurrentTeamName
        Me.txtTimeCriteria.Text = szCurrentTimeCriteria
        Me.txtGameID.Text = System.IO.Path.GetFileNameWithoutExtension(frmVideo.szVideoFileName)


    End Sub
End Class
