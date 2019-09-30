<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class videoCapture
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(videoCapture))
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdPause = New System.Windows.Forms.Button
        Me.cmdRecord = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblRecordingTime = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSpaceRemaining = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDevice = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblDestination = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.picVideo = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdRefresh.BackColor = System.Drawing.Color.Transparent
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.Location = New System.Drawing.Point(11, 3)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(108, 23)
        Me.cmdRefresh.TabIndex = 2
        Me.cmdRefresh.Text = "Check Device"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdPause
        '
        Me.cmdPause.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdPause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdPause.BackColor = System.Drawing.Color.Transparent
        Me.cmdPause.Enabled = False
        Me.cmdPause.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPause.Image = CType(resources.GetObject("cmdPause.Image"), System.Drawing.Image)
        Me.cmdPause.Location = New System.Drawing.Point(142, 3)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(108, 23)
        Me.cmdPause.TabIndex = 2
        Me.cmdPause.Text = "Pause"
        Me.cmdPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdPause.UseVisualStyleBackColor = False
        '
        'cmdRecord
        '
        Me.cmdRecord.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmdRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdRecord.BackColor = System.Drawing.Color.Transparent
        Me.cmdRecord.Enabled = False
        Me.cmdRecord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRecord.Image = CType(resources.GetObject("cmdRecord.Image"), System.Drawing.Image)
        Me.cmdRecord.Location = New System.Drawing.Point(273, 3)
        Me.cmdRecord.Name = "cmdRecord"
        Me.cmdRecord.Size = New System.Drawing.Size(109, 23)
        Me.cmdRecord.TabIndex = 1
        Me.cmdRecord.Text = "Record"
        Me.cmdRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdRecord.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblRecordingTime)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblSpaceRemaining)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblDevice)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblDestination)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 308)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(393, 94)
        Me.Panel1.TabIndex = 1
        '
        'lblRecordingTime
        '
        Me.lblRecordingTime.AutoSize = True
        Me.lblRecordingTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecordingTime.Location = New System.Drawing.Point(86, 66)
        Me.lblRecordingTime.Margin = New System.Windows.Forms.Padding(5)
        Me.lblRecordingTime.Name = "lblRecordingTime"
        Me.lblRecordingTime.Padding = New System.Windows.Forms.Padding(3)
        Me.lblRecordingTime.Size = New System.Drawing.Size(47, 18)
        Me.lblRecordingTime.TabIndex = 7
        Me.lblRecordingTime.Text = "00:00:00"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(-1, 66)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3)
        Me.Label5.Size = New System.Drawing.Size(78, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Recording Time:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSpaceRemaining
        '
        Me.lblSpaceRemaining.AutoSize = True
        Me.lblSpaceRemaining.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpaceRemaining.Location = New System.Drawing.Point(86, 46)
        Me.lblSpaceRemaining.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSpaceRemaining.Name = "lblSpaceRemaining"
        Me.lblSpaceRemaining.Padding = New System.Windows.Forms.Padding(3)
        Me.lblSpaceRemaining.Size = New System.Drawing.Size(41, 18)
        Me.lblSpaceRemaining.TabIndex = 5
        Me.lblSpaceRemaining.Text = "0.0 Gb."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(22, 47)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Disk Space:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDevice
        '
        Me.lblDevice.AutoSize = True
        Me.lblDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevice.Location = New System.Drawing.Point(86, 26)
        Me.lblDevice.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDevice.Name = "lblDevice"
        Me.lblDevice.Padding = New System.Windows.Forms.Padding(3)
        Me.lblDevice.Size = New System.Drawing.Size(87, 18)
        Me.lblDevice.TabIndex = 3
        Me.lblDevice.Text = "No device found..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(39, 28)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Device:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDestination
        '
        Me.lblDestination.AutoSize = True
        Me.lblDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDestination.Location = New System.Drawing.Point(86, 6)
        Me.lblDestination.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDestination.Name = "lblDestination"
        Me.lblDestination.Padding = New System.Windows.Forms.Padding(3)
        Me.lblDestination.Size = New System.Drawing.Size(22, 18)
        Me.lblDestination.TabIndex = 1
        Me.lblDestination.Text = "c:\"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Destination:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdRefresh, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPause, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdRecord, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 408)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(394, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'picVideo
        '
        Me.picVideo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picVideo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picVideo.Location = New System.Drawing.Point(12, 12)
        Me.picVideo.Name = "picVideo"
        Me.picVideo.Size = New System.Drawing.Size(393, 290)
        Me.picVideo.TabIndex = 2
        Me.picVideo.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'videoCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 449)
        Me.Controls.Add(Me.picVideo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "videoCapture"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Video Capture"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picVideo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdRecord As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblRecordingTime As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSpaceRemaining As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDevice As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDestination As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents picVideo As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
