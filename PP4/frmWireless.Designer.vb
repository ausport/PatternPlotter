<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWireless
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWireless))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdLeft = New System.Windows.Forms.Button
        Me.cmdRight = New System.Windows.Forms.Button
        Me.cmdHome = New System.Windows.Forms.Button
        Me.trackHorizontal = New System.Windows.Forms.TrackBar
        Me.trackVertical = New System.Windows.Forms.TrackBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cboCameraResolution = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdTele = New System.Windows.Forms.Button
        Me.cmdWide = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pbConnectionStrength = New System.Windows.Forms.ProgressBar
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblIPAddress = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lstPresets = New System.Windows.Forms.ListView
        Me.imgLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.cmdNewPreset = New System.Windows.Forms.Button
        Me.cmdDeletePreset = New System.Windows.Forms.Button
        Me.cmdSavePresets = New System.Windows.Forms.Button
        Me.cmdLoadPresets = New System.Windows.Forms.Button
        Me.cmdPitchCalibration = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.trackHorizontal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trackVertical, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(190, 676)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(266, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(116, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Close"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(131, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(126, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Disconnect and Close"
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(59, 19)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(44, 30)
        Me.cmdUp.TabIndex = 1
        Me.cmdUp.Text = "Up"
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        Me.cmdDown.Location = New System.Drawing.Point(59, 91)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(44, 30)
        Me.cmdDown.TabIndex = 2
        Me.cmdDown.Text = "Down"
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'cmdLeft
        '
        Me.cmdLeft.Location = New System.Drawing.Point(9, 55)
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(44, 30)
        Me.cmdLeft.TabIndex = 3
        Me.cmdLeft.Text = "Left"
        Me.cmdLeft.UseVisualStyleBackColor = True
        '
        'cmdRight
        '
        Me.cmdRight.Location = New System.Drawing.Point(109, 55)
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(44, 30)
        Me.cmdRight.TabIndex = 4
        Me.cmdRight.Text = "Right"
        Me.cmdRight.UseVisualStyleBackColor = True
        '
        'cmdHome
        '
        Me.cmdHome.Location = New System.Drawing.Point(59, 55)
        Me.cmdHome.Name = "cmdHome"
        Me.cmdHome.Size = New System.Drawing.Size(44, 30)
        Me.cmdHome.TabIndex = 5
        Me.cmdHome.Text = "Home"
        Me.cmdHome.UseVisualStyleBackColor = True
        '
        'trackHorizontal
        '
        Me.trackHorizontal.Enabled = False
        Me.trackHorizontal.Location = New System.Drawing.Point(508, 12)
        Me.trackHorizontal.Maximum = 170
        Me.trackHorizontal.Minimum = -170
        Me.trackHorizontal.Name = "trackHorizontal"
        Me.trackHorizontal.Size = New System.Drawing.Size(171, 45)
        Me.trackHorizontal.TabIndex = 6
        Me.trackHorizontal.TickStyle = System.Windows.Forms.TickStyle.None
        Me.trackHorizontal.Visible = False
        '
        'trackVertical
        '
        Me.trackVertical.Enabled = False
        Me.trackVertical.Location = New System.Drawing.Point(613, 12)
        Me.trackVertical.Maximum = 47
        Me.trackVertical.Minimum = -67
        Me.trackVertical.Name = "trackVertical"
        Me.trackVertical.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.trackVertical.Size = New System.Drawing.Size(45, 132)
        Me.trackVertical.TabIndex = 7
        Me.trackVertical.TickStyle = System.Windows.Forms.TickStyle.None
        Me.trackVertical.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 40
        '
        'cboCameraResolution
        '
        Me.cboCameraResolution.FormattingEnabled = True
        Me.cboCameraResolution.Items.AddRange(New Object() {"736 x 544", "640 x 480", "320 x 240", "160 x 120"})
        Me.cboCameraResolution.Location = New System.Drawing.Point(559, 147)
        Me.cboCameraResolution.Name = "cboCameraResolution"
        Me.cboCameraResolution.Size = New System.Drawing.Size(163, 21)
        Me.cboCameraResolution.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(559, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Image Resolution"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdUp)
        Me.GroupBox1.Controls.Add(Me.cmdDown)
        Me.GroupBox1.Controls.Add(Me.cmdLeft)
        Me.GroupBox1.Controls.Add(Me.cmdRight)
        Me.GroupBox1.Controls.Add(Me.cmdHome)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 157)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(162, 129)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pan Control"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdTele)
        Me.GroupBox2.Controls.Add(Me.cmdWide)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 292)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(162, 49)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Zoom"
        '
        'cmdTele
        '
        Me.cmdTele.Location = New System.Drawing.Point(83, 19)
        Me.cmdTele.Name = "cmdTele"
        Me.cmdTele.Size = New System.Drawing.Size(70, 23)
        Me.cmdTele.TabIndex = 1
        Me.cmdTele.Text = "Tele |->"
        Me.cmdTele.UseVisualStyleBackColor = True
        '
        'cmdWide
        '
        Me.cmdWide.Location = New System.Drawing.Point(9, 19)
        Me.cmdWide.Name = "cmdWide"
        Me.cmdWide.Size = New System.Drawing.Size(71, 23)
        Me.cmdWide.TabIndex = 0
        Me.cmdWide.Text = "<-| Wide"
        Me.cmdWide.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.pbConnectionStrength)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblStatus)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.lblIPAddress)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(160, 138)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Camera Status"
        '
        'pbConnectionStrength
        '
        Me.pbConnectionStrength.Location = New System.Drawing.Point(9, 117)
        Me.pbConnectionStrength.Name = "pbConnectionStrength"
        Me.pbConnectionStrength.Size = New System.Drawing.Size(142, 15)
        Me.pbConnectionStrength.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Connection Strength"
        '
        'lblStatus
        '
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Location = New System.Drawing.Point(9, 71)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(142, 19)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Inactive"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Connection Status"
        '
        'lblIPAddress
        '
        Me.lblIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIPAddress.Location = New System.Drawing.Point(9, 29)
        Me.lblIPAddress.Name = "lblIPAddress"
        Me.lblIPAddress.Size = New System.Drawing.Size(142, 19)
        Me.lblIPAddress.TabIndex = 1
        Me.lblIPAddress.Text = "192.168.1.100"
        Me.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "IP Address"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.lstPresets)
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox4.Location = New System.Drawing.Point(180, 13)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(273, 328)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Preset Positions"
        '
        'lstPresets
        '
        Me.lstPresets.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lstPresets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstPresets.GridLines = True
        Me.lstPresets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstPresets.HotTracking = True
        Me.lstPresets.HoverSelection = True
        Me.lstPresets.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lstPresets.LargeImageList = Me.imgLarge
        Me.lstPresets.Location = New System.Drawing.Point(7, 20)
        Me.lstPresets.MultiSelect = False
        Me.lstPresets.Name = "lstPresets"
        Me.lstPresets.Size = New System.Drawing.Size(257, 209)
        Me.lstPresets.SmallImageList = Me.imgLarge
        Me.lstPresets.TabIndex = 4
        Me.lstPresets.UseCompatibleStateImageBehavior = False
        '
        'imgLarge
        '
        Me.imgLarge.ImageStream = CType(resources.GetObject("imgLarge.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLarge.TransparentColor = System.Drawing.Color.Transparent
        Me.imgLarge.Images.SetKeyName(0, "SonyRemote.gif")
        Me.imgLarge.Images.SetKeyName(1, "SonyRemote.png")
        Me.imgLarge.Images.SetKeyName(2, "SonyRemote.jpg")
        Me.imgLarge.Images.SetKeyName(3, "AddPoint.png")
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.cmdNewPreset, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdDeletePreset, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdSavePresets, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdLoadPresets, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdPitchCalibration, 1, 2)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(7, 235)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.88372!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.55814!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(260, 86)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'cmdNewPreset
        '
        Me.cmdNewPreset.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNewPreset.Location = New System.Drawing.Point(3, 3)
        Me.cmdNewPreset.Name = "cmdNewPreset"
        Me.cmdNewPreset.Size = New System.Drawing.Size(124, 22)
        Me.cmdNewPreset.TabIndex = 1
        Me.cmdNewPreset.Text = "New"
        Me.cmdNewPreset.UseVisualStyleBackColor = True
        '
        'cmdDeletePreset
        '
        Me.cmdDeletePreset.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDeletePreset.Location = New System.Drawing.Point(133, 3)
        Me.cmdDeletePreset.Name = "cmdDeletePreset"
        Me.cmdDeletePreset.Size = New System.Drawing.Size(124, 22)
        Me.cmdDeletePreset.TabIndex = 2
        Me.cmdDeletePreset.Text = "Delete"
        Me.cmdDeletePreset.UseVisualStyleBackColor = True
        '
        'cmdSavePresets
        '
        Me.cmdSavePresets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSavePresets.Location = New System.Drawing.Point(3, 31)
        Me.cmdSavePresets.Name = "cmdSavePresets"
        Me.cmdSavePresets.Size = New System.Drawing.Size(124, 23)
        Me.cmdSavePresets.TabIndex = 3
        Me.cmdSavePresets.Text = "Save"
        Me.cmdSavePresets.UseVisualStyleBackColor = True
        '
        'cmdLoadPresets
        '
        Me.cmdLoadPresets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdLoadPresets.Location = New System.Drawing.Point(133, 31)
        Me.cmdLoadPresets.Name = "cmdLoadPresets"
        Me.cmdLoadPresets.Size = New System.Drawing.Size(124, 23)
        Me.cmdLoadPresets.TabIndex = 4
        Me.cmdLoadPresets.Text = "Load"
        Me.cmdLoadPresets.UseVisualStyleBackColor = True
        '
        'cmdPitchCalibration
        '
        Me.cmdPitchCalibration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPitchCalibration.Location = New System.Drawing.Point(133, 60)
        Me.cmdPitchCalibration.Name = "cmdPitchCalibration"
        Me.cmdPitchCalibration.Size = New System.Drawing.Size(124, 23)
        Me.cmdPitchCalibration.TabIndex = 5
        Me.cmdPitchCalibration.Text = "Add Pitch Calibration"
        Me.cmdPitchCalibration.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.ImageLocation = ""
        Me.PictureBox1.Location = New System.Drawing.Point(12, 347)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(444, 323)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'frmWireless
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 717)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCameraResolution)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.trackVertical)
        Me.Controls.Add(Me.trackHorizontal)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWireless"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Wireless Camera"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.trackHorizontal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trackVertical, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdLeft As System.Windows.Forms.Button
    Friend WithEvents cmdRight As System.Windows.Forms.Button
    Friend WithEvents cmdHome As System.Windows.Forms.Button
    Friend WithEvents trackHorizontal As System.Windows.Forms.TrackBar
    Friend WithEvents trackVertical As System.Windows.Forms.TrackBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cboCameraResolution As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblIPAddress As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pbConnectionStrength As System.Windows.Forms.ProgressBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmdTele As System.Windows.Forms.Button
    Friend WithEvents cmdWide As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdNewPreset As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdDeletePreset As System.Windows.Forms.Button
    Friend WithEvents cmdSavePresets As System.Windows.Forms.Button
    Friend WithEvents cmdLoadPresets As System.Windows.Forms.Button
    Friend WithEvents lstPresets As System.Windows.Forms.ListView
    Friend WithEvents imgLarge As System.Windows.Forms.ImageList
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents cmdPitchCalibration As System.Windows.Forms.Button

End Class
