Imports System.Windows.Forms

Public Class frmCreateCollection

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.optCollectionContentSmart.Checked = True Then
            frmOpenGames.SetNewCollection(Me.txtNewCollectionName.Text, Me.txtCollectionKeyword.Text)
        Else
            frmOpenGames.SetNewCollection(Me.txtNewCollectionName.Text)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub optCollectionContentSmart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCollectionContentSmart.CheckedChanged
        Me.txtCollectionKeyword.Enabled = Me.optCollectionContentSmart.Checked
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class
