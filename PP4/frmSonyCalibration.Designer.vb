<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSonyCalibration
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
        Me.picPitch = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdBeginCalibration = New System.Windows.Forms.Button
        Me.lblDirections = New System.Windows.Forms.Label
        Me.cmdSetViewpoint = New System.Windows.Forms.Button
        Me.lblStep2 = New System.Windows.Forms.Label
        Me.lblStep1 = New System.Windows.Forms.Label
        Me.cmdPositionRemoteDevice = New System.Windows.Forms.Button
        Me.cmdRestart = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.lblStep3 = New System.Windows.Forms.Label
        Me.cboPan = New System.Windows.Forms.NumericUpDown
        Me.lblPan = New System.Windows.Forms.Label
        Me.cboTilt = New System.Windows.Forms.NumericUpDown
        Me.lblTilt = New System.Windows.Forms.Label
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.cboPan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTilt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picPitch
        '
        Me.picPitch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picPitch.BackColor = System.Drawing.Color.GhostWhite
        Me.picPitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPitch.Location = New System.Drawing.Point(13, 12)
        Me.picPitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picPitch.Name = "picPitch"
        Me.picPitch.Size = New System.Drawing.Size(372, 369)
        Me.picPitch.TabIndex = 1
        Me.picPitch.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTilt)
        Me.GroupBox1.Controls.Add(Me.cboTilt)
        Me.GroupBox1.Controls.Add(Me.lblPan)
        Me.GroupBox1.Controls.Add(Me.cboPan)
        Me.GroupBox1.Controls.Add(Me.lblStep3)
        Me.GroupBox1.Controls.Add(Me.cmdBeginCalibration)
        Me.GroupBox1.Controls.Add(Me.lblDirections)
        Me.GroupBox1.Controls.Add(Me.cmdSetViewpoint)
        Me.GroupBox1.Controls.Add(Me.lblStep2)
        Me.GroupBox1.Controls.Add(Me.lblStep1)
        Me.GroupBox1.Controls.Add(Me.cmdPositionRemoteDevice)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 387)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(373, 232)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remote Camera Calibration Settings"
        '
        'cmdBeginCalibration
        '
        Me.cmdBeginCalibration.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBeginCalibration.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBeginCalibration.Location = New System.Drawing.Point(225, 19)
        Me.cmdBeginCalibration.Name = "cmdBeginCalibration"
        Me.cmdBeginCalibration.Size = New System.Drawing.Size(137, 23)
        Me.cmdBeginCalibration.TabIndex = 6
        Me.cmdBeginCalibration.Text = "Begin Calibration"
        Me.cmdBeginCalibration.UseVisualStyleBackColor = False
        '
        'lblDirections
        '
        Me.lblDirections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDirections.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDirections.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDirections.Location = New System.Drawing.Point(9, 187)
        Me.lblDirections.Name = "lblDirections"
        Me.lblDirections.Size = New System.Drawing.Size(358, 39)
        Me.lblDirections.TabIndex = 5
        Me.lblDirections.Text = "Click on 'Begin Calibration' to commence calibration procedure."
        '
        'cmdSetViewpoint
        '
        Me.cmdSetViewpoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSetViewpoint.Enabled = False
        Me.cmdSetViewpoint.Location = New System.Drawing.Point(225, 77)
        Me.cmdSetViewpoint.Name = "cmdSetViewpoint"
        Me.cmdSetViewpoint.Size = New System.Drawing.Size(137, 23)
        Me.cmdSetViewpoint.TabIndex = 3
        Me.cmdSetViewpoint.Text = "Lock Zero Viewpoint"
        Me.cmdSetViewpoint.UseVisualStyleBackColor = True
        Me.cmdSetViewpoint.Visible = False
        '
        'lblStep2
        '
        Me.lblStep2.AutoSize = True
        Me.lblStep2.Enabled = False
        Me.lblStep2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStep2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblStep2.Location = New System.Drawing.Point(13, 82)
        Me.lblStep2.Name = "lblStep2"
        Me.lblStep2.Size = New System.Drawing.Size(158, 13)
        Me.lblStep2.TabIndex = 2
        Me.lblStep2.Text = "2.  Modify Viewpoint Angle"
        Me.lblStep2.Visible = False
        '
        'lblStep1
        '
        Me.lblStep1.AutoSize = True
        Me.lblStep1.Enabled = False
        Me.lblStep1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStep1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblStep1.Location = New System.Drawing.Point(13, 53)
        Me.lblStep1.Name = "lblStep1"
        Me.lblStep1.Size = New System.Drawing.Size(161, 13)
        Me.lblStep1.TabIndex = 1
        Me.lblStep1.Text = "1.  Select Camera Location"
        '
        'cmdPositionRemoteDevice
        '
        Me.cmdPositionRemoteDevice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPositionRemoteDevice.Enabled = False
        Me.cmdPositionRemoteDevice.Location = New System.Drawing.Point(225, 48)
        Me.cmdPositionRemoteDevice.Name = "cmdPositionRemoteDevice"
        Me.cmdPositionRemoteDevice.Size = New System.Drawing.Size(137, 23)
        Me.cmdPositionRemoteDevice.TabIndex = 0
        Me.cmdPositionRemoteDevice.Text = "Lock Camera Location"
        Me.cmdPositionRemoteDevice.UseVisualStyleBackColor = True
        '
        'cmdRestart
        '
        Me.cmdRestart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdRestart.Location = New System.Drawing.Point(80, 3)
        Me.cmdRestart.Name = "cmdRestart"
        Me.cmdRestart.Size = New System.Drawing.Size(67, 23)
        Me.cmdRestart.TabIndex = 4
        Me.cmdRestart.Text = "Restart"
        Me.cmdRestart.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdRestart, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(157, 625)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(228, 29)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(156, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'lblStep3
        '
        Me.lblStep3.AutoSize = True
        Me.lblStep3.Enabled = False
        Me.lblStep3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStep3.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblStep3.Location = New System.Drawing.Point(13, 111)
        Me.lblStep3.Name = "lblStep3"
        Me.lblStep3.Size = New System.Drawing.Size(166, 13)
        Me.lblStep3.TabIndex = 7
        Me.lblStep3.Text = "3.  Set Viewpoint Quadrants"
        Me.lblStep3.Visible = False
        '
        'cboPan
        '
        Me.cboPan.Enabled = False
        Me.cboPan.Location = New System.Drawing.Point(254, 109)
        Me.cboPan.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.cboPan.Name = "cboPan"
        Me.cboPan.Size = New System.Drawing.Size(35, 20)
        Me.cboPan.TabIndex = 8
        Me.cboPan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cboPan.Value = New Decimal(New Integer() {5, 0, 0, 0})
        Me.cboPan.Visible = False
        '
        'lblPan
        '
        Me.lblPan.AutoSize = True
        Me.lblPan.Enabled = False
        Me.lblPan.Location = New System.Drawing.Point(222, 111)
        Me.lblPan.Name = "lblPan"
        Me.lblPan.Size = New System.Drawing.Size(26, 13)
        Me.lblPan.TabIndex = 9
        Me.lblPan.Text = "Pan"
        Me.lblPan.Visible = False
        '
        'cboTilt
        '
        Me.cboTilt.Enabled = False
        Me.cboTilt.Location = New System.Drawing.Point(327, 109)
        Me.cboTilt.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.cboTilt.Name = "cboTilt"
        Me.cboTilt.Size = New System.Drawing.Size(35, 20)
        Me.cboTilt.TabIndex = 10
        Me.cboTilt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.cboTilt.Value = New Decimal(New Integer() {2, 0, 0, 0})
        Me.cboTilt.Visible = False
        '
        'lblTilt
        '
        Me.lblTilt.AutoSize = True
        Me.lblTilt.Enabled = False
        Me.lblTilt.Location = New System.Drawing.Point(300, 111)
        Me.lblTilt.Name = "lblTilt"
        Me.lblTilt.Size = New System.Drawing.Size(21, 13)
        Me.lblTilt.TabIndex = 11
        Me.lblTilt.Text = "Tilt"
        Me.lblTilt.Visible = False
        '
        'frmSonyCalibration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 665)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.picPitch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmSonyCalibration"
        CType(Me.picPitch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.cboPan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTilt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picPitch As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cmdPositionRemoteDevice As System.Windows.Forms.Button
    Friend WithEvents lblStep1 As System.Windows.Forms.Label
    Friend WithEvents cmdSetViewpoint As System.Windows.Forms.Button
    Friend WithEvents lblStep2 As System.Windows.Forms.Label
    Friend WithEvents cmdRestart As System.Windows.Forms.Button
    Friend WithEvents lblDirections As System.Windows.Forms.Label
    Friend WithEvents cmdBeginCalibration As System.Windows.Forms.Button
    Friend WithEvents cboPan As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblStep3 As System.Windows.Forms.Label
    Friend WithEvents lblTilt As System.Windows.Forms.Label
    Friend WithEvents cboTilt As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPan As System.Windows.Forms.Label
End Class
