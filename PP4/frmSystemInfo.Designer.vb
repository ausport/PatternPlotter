<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSystemInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblAvailablePhysicalMemory = New System.Windows.Forms.Label
        Me.lblTotalPhysicalMemory = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.prgPhysicalMemory = New System.Windows.Forms.ProgressBar
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblWindowsVersion = New System.Windows.Forms.Label
        Me.lblPatternPlotterVersion = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.prgDriveUsage = New System.Windows.Forms.ProgressBar
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDriveTotal = New System.Windows.Forms.Label
        Me.lblDriveAvailable = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblDriveFormat = New System.Windows.Forms.Label
        Me.cboDrives = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(237, 353)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(69, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(63, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.prgPhysicalMemory)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblTotalPhysicalMemory)
        Me.GroupBox1.Controls.Add(Me.lblAvailablePhysicalMemory)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 86)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(296, 100)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Physical Memory"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Available:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Total:"
        '
        'lblAvailablePhysicalMemory
        '
        Me.lblAvailablePhysicalMemory.AutoSize = True
        Me.lblAvailablePhysicalMemory.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblAvailablePhysicalMemory.Location = New System.Drawing.Point(76, 20)
        Me.lblAvailablePhysicalMemory.Name = "lblAvailablePhysicalMemory"
        Me.lblAvailablePhysicalMemory.Size = New System.Drawing.Size(31, 13)
        Me.lblAvailablePhysicalMemory.TabIndex = 2
        Me.lblAvailablePhysicalMemory.Text = "0 Mb"
        '
        'lblTotalPhysicalMemory
        '
        Me.lblTotalPhysicalMemory.AutoSize = True
        Me.lblTotalPhysicalMemory.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTotalPhysicalMemory.Location = New System.Drawing.Point(76, 45)
        Me.lblTotalPhysicalMemory.Name = "lblTotalPhysicalMemory"
        Me.lblTotalPhysicalMemory.Size = New System.Drawing.Size(31, 13)
        Me.lblTotalPhysicalMemory.TabIndex = 3
        Me.lblTotalPhysicalMemory.Text = "0 Mb"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Usage:"
        '
        'prgPhysicalMemory
        '
        Me.prgPhysicalMemory.Location = New System.Drawing.Point(79, 70)
        Me.prgPhysicalMemory.Name = "prgPhysicalMemory"
        Me.prgPhysicalMemory.Size = New System.Drawing.Size(194, 13)
        Me.prgPhysicalMemory.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgPhysicalMemory.TabIndex = 5
        Me.prgPhysicalMemory.Value = 45
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblWindowsVersion)
        Me.GroupBox2.Controls.Add(Me.lblPatternPlotterVersion)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(296, 68)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Software"
        '
        'lblWindowsVersion
        '
        Me.lblWindowsVersion.AutoSize = True
        Me.lblWindowsVersion.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblWindowsVersion.Location = New System.Drawing.Point(76, 45)
        Me.lblWindowsVersion.Name = "lblWindowsVersion"
        Me.lblWindowsVersion.Size = New System.Drawing.Size(31, 13)
        Me.lblWindowsVersion.TabIndex = 3
        Me.lblWindowsVersion.Text = "0 Mb"
        '
        'lblPatternPlotterVersion
        '
        Me.lblPatternPlotterVersion.AutoSize = True
        Me.lblPatternPlotterVersion.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblPatternPlotterVersion.Location = New System.Drawing.Point(76, 20)
        Me.lblPatternPlotterVersion.Name = "lblPatternPlotterVersion"
        Me.lblPatternPlotterVersion.Size = New System.Drawing.Size(27, 13)
        Me.lblPatternPlotterVersion.TabIndex = 2
        Me.lblPatternPlotterVersion.Text = "PP4"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Windows:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Version:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.cboDrives)
        Me.GroupBox3.Controls.Add(Me.lblDriveFormat)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.prgDriveUsage)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.lblDriveTotal)
        Me.GroupBox3.Controls.Add(Me.lblDriveAvailable)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 192)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(296, 155)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Storage Capacity"
        '
        'prgDriveUsage
        '
        Me.prgDriveUsage.Location = New System.Drawing.Point(79, 126)
        Me.prgDriveUsage.Name = "prgDriveUsage"
        Me.prgDriveUsage.Size = New System.Drawing.Size(194, 13)
        Me.prgDriveUsage.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgDriveUsage.TabIndex = 5
        Me.prgDriveUsage.Value = 45
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Usage:"
        '
        'lblDriveTotal
        '
        Me.lblDriveTotal.AutoSize = True
        Me.lblDriveTotal.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDriveTotal.Location = New System.Drawing.Point(76, 101)
        Me.lblDriveTotal.Name = "lblDriveTotal"
        Me.lblDriveTotal.Size = New System.Drawing.Size(31, 13)
        Me.lblDriveTotal.TabIndex = 3
        Me.lblDriveTotal.Text = "0 Mb"
        '
        'lblDriveAvailable
        '
        Me.lblDriveAvailable.AutoSize = True
        Me.lblDriveAvailable.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDriveAvailable.Location = New System.Drawing.Point(76, 76)
        Me.lblDriveAvailable.Name = "lblDriveAvailable"
        Me.lblDriveAvailable.Size = New System.Drawing.Size(30, 13)
        Me.lblDriveAvailable.TabIndex = 2
        Me.lblDriveAvailable.Text = "0 Gb"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(26, 101)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Total:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 76)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Available:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(18, 51)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Format:"
        '
        'lblDriveFormat
        '
        Me.lblDriveFormat.AutoSize = True
        Me.lblDriveFormat.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblDriveFormat.Location = New System.Drawing.Point(76, 51)
        Me.lblDriveFormat.Name = "lblDriveFormat"
        Me.lblDriveFormat.Size = New System.Drawing.Size(35, 13)
        Me.lblDriveFormat.TabIndex = 7
        Me.lblDriveFormat.Text = "NTFS"
        '
        'cboDrives
        '
        Me.cboDrives.FormattingEnabled = True
        Me.cboDrives.Location = New System.Drawing.Point(108, 18)
        Me.cboDrives.Name = "cboDrives"
        Me.cboDrives.Size = New System.Drawing.Size(165, 21)
        Me.cboDrives.TabIndex = 8
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(95, 13)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "Available Devices:"
        '
        'frmSystemInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(318, 394)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSystemInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "System Information"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalPhysicalMemory As System.Windows.Forms.Label
    Friend WithEvents lblAvailablePhysicalMemory As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents prgPhysicalMemory As System.Windows.Forms.ProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWindowsVersion As System.Windows.Forms.Label
    Friend WithEvents lblPatternPlotterVersion As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents prgDriveUsage As System.Windows.Forms.ProgressBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDriveTotal As System.Windows.Forms.Label
    Friend WithEvents lblDriveAvailable As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDriveFormat As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboDrives As System.Windows.Forms.ComboBox

End Class
