Imports System.Windows.Forms

Public Class frmSystemInfo

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSystemInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For Each drive As System.IO.DriveInfo In My.Computer.FileSystem.Drives
            If drive.IsReady Then
                Me.cboDrives.Items.Add(drive.Name & "[" & drive.VolumeLabel & "]")
            End If
        Next
        Me.cboDrives.Text = cboDrives.Items(0)

        UpdateMe()
    End Sub
    Private Sub UpdateMe()

        Me.lblPatternPlotterVersion.Text = My.Application.Info.ProductName
        Me.lblWindowsVersion.Text = My.Computer.Info.OSFullName
        Me.lblAvailablePhysicalMemory.Text = Format(My.Computer.Info.AvailablePhysicalMemory / 1000000, "0.0 Mb")
        Me.lblTotalPhysicalMemory.Text = Format(My.Computer.Info.TotalPhysicalMemory / 1000000, "0.0 Mb")
        Me.prgPhysicalMemory.Maximum = (My.Computer.Info.TotalPhysicalMemory / 1000000)
        Me.prgPhysicalMemory.Value = (My.Computer.Info.TotalPhysicalMemory - My.Computer.Info.AvailablePhysicalMemory) / 1000000

        For Each drive As System.IO.DriveInfo In My.Computer.FileSystem.Drives
            If drive.IsReady Then
                If drive.Name & "[" & drive.VolumeLabel & "]" = Me.cboDrives.Text Then
                    Me.lblDriveFormat.Text = drive.DriveFormat
                    Me.lblDriveAvailable.Text = Format(drive.AvailableFreeSpace / 1000000000, "0.0 Gb")
                    Me.lblDriveTotal.Text = Format(drive.TotalSize / 1000000000, "0.0 Gb")
                    Me.prgDriveUsage.Maximum = drive.TotalSize / 1000000
                    Me.prgDriveUsage.Value = (drive.TotalSize - drive.AvailableFreeSpace) / 1000000
                End If
            End If
        Next
    End Sub

  
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        UpdateMe()
    End Sub

    Private Sub cboDrives_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDrives.SelectedIndexChanged
        UpdateMe()
    End Sub

    Private Sub cboDrives_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDrives.SelectedValueChanged

    End Sub
End Class
