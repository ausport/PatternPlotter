<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditTags
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.txtEventName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboMirror_BackColor = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboMirror_TextColor = New System.Windows.Forms.ComboBox
        Me.lbliPhoneChart = New System.Windows.Forms.Label
        Me.cboiPhoneCharts = New System.Windows.Forms.ComboBox
        Me.txtKeyStroke = New System.Windows.Forms.TextBox
        Me.chkEnableRemoteTransmission = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblCalibrationFile = New System.Windows.Forms.Label
        Me.cmdBrowse4CalibrationFile = New System.Windows.Forms.Button
        Me.lblButtonType = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.lblButtonType.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(137, 291)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtEventName
        '
        Me.txtEventName.Location = New System.Drawing.Point(102, 19)
        Me.txtEventName.Name = "txtEventName"
        Me.txtEventName.Size = New System.Drawing.Size(165, 20)
        Me.txtEventName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Event Name:"
        '
        'cboMirror_BackColor
        '
        Me.cboMirror_BackColor.Location = New System.Drawing.Point(102, 45)
        Me.cboMirror_BackColor.Name = "cboMirror_BackColor"
        Me.cboMirror_BackColor.Size = New System.Drawing.Size(165, 21)
        Me.cboMirror_BackColor.TabIndex = 4
        Me.cboMirror_BackColor.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Back Color:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Short Cut Key:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Text Color:"
        '
        'cboMirror_TextColor
        '
        Me.cboMirror_TextColor.Location = New System.Drawing.Point(102, 72)
        Me.cboMirror_TextColor.Name = "cboMirror_TextColor"
        Me.cboMirror_TextColor.Size = New System.Drawing.Size(165, 21)
        Me.cboMirror_TextColor.TabIndex = 8
        Me.cboMirror_TextColor.Visible = False
        '
        'lbliPhoneChart
        '
        Me.lbliPhoneChart.AutoSize = True
        Me.lbliPhoneChart.Enabled = False
        Me.lbliPhoneChart.Location = New System.Drawing.Point(27, 128)
        Me.lbliPhoneChart.Name = "lbliPhoneChart"
        Me.lbliPhoneChart.Size = New System.Drawing.Size(71, 13)
        Me.lbliPhoneChart.TabIndex = 11
        Me.lbliPhoneChart.Text = "iPhone Chart:"
        '
        'cboiPhoneCharts
        '
        Me.cboiPhoneCharts.Enabled = False
        Me.cboiPhoneCharts.Items.AddRange(New Object() {"Scatter Plot", "Cluster Chart", "Ball Movement Plot"})
        Me.cboiPhoneCharts.Location = New System.Drawing.Point(102, 125)
        Me.cboiPhoneCharts.Name = "cboiPhoneCharts"
        Me.cboiPhoneCharts.Size = New System.Drawing.Size(165, 21)
        Me.cboiPhoneCharts.TabIndex = 10
        Me.cboiPhoneCharts.Text = "Scatter Plot"
        '
        'txtKeyStroke
        '
        Me.txtKeyStroke.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKeyStroke.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txtKeyStroke.Location = New System.Drawing.Point(102, 99)
        Me.txtKeyStroke.Name = "txtKeyStroke"
        Me.txtKeyStroke.Size = New System.Drawing.Size(100, 20)
        Me.txtKeyStroke.TabIndex = 12
        Me.txtKeyStroke.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkEnableRemoteTransmission
        '
        Me.chkEnableRemoteTransmission.AutoSize = True
        Me.chkEnableRemoteTransmission.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.chkEnableRemoteTransmission.Location = New System.Drawing.Point(102, 152)
        Me.chkEnableRemoteTransmission.Name = "chkEnableRemoteTransmission"
        Me.chkEnableRemoteTransmission.Size = New System.Drawing.Size(159, 17)
        Me.chkEnableRemoteTransmission.TabIndex = 13
        Me.chkEnableRemoteTransmission.Text = "Enable iPhone Transmission"
        Me.chkEnableRemoteTransmission.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblCalibrationFile)
        Me.GroupBox1.Controls.Add(Me.cmdBrowse4CalibrationFile)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 198)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(273, 86)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pitch Calibration File"
        '
        'lblCalibrationFile
        '
        Me.lblCalibrationFile.Location = New System.Drawing.Point(8, 45)
        Me.lblCalibrationFile.Name = "lblCalibrationFile"
        Me.lblCalibrationFile.Size = New System.Drawing.Size(257, 38)
        Me.lblCalibrationFile.TabIndex = 1
        Me.lblCalibrationFile.Text = "None"
        '
        'cmdBrowse4CalibrationFile
        '
        Me.cmdBrowse4CalibrationFile.Location = New System.Drawing.Point(11, 19)
        Me.cmdBrowse4CalibrationFile.Name = "cmdBrowse4CalibrationFile"
        Me.cmdBrowse4CalibrationFile.Size = New System.Drawing.Size(75, 23)
        Me.cmdBrowse4CalibrationFile.TabIndex = 0
        Me.cmdBrowse4CalibrationFile.Text = "Browse"
        Me.cmdBrowse4CalibrationFile.UseVisualStyleBackColor = True
        '
        'lblButtonType
        '
        Me.lblButtonType.Controls.Add(Me.txtEventName)
        Me.lblButtonType.Controls.Add(Me.Label1)
        Me.lblButtonType.Controls.Add(Me.chkEnableRemoteTransmission)
        Me.lblButtonType.Controls.Add(Me.cboMirror_BackColor)
        Me.lblButtonType.Controls.Add(Me.txtKeyStroke)
        Me.lblButtonType.Controls.Add(Me.Label2)
        Me.lblButtonType.Controls.Add(Me.lbliPhoneChart)
        Me.lblButtonType.Controls.Add(Me.Label3)
        Me.lblButtonType.Controls.Add(Me.cboiPhoneCharts)
        Me.lblButtonType.Controls.Add(Me.cboMirror_TextColor)
        Me.lblButtonType.Controls.Add(Me.Label4)
        Me.lblButtonType.Location = New System.Drawing.Point(12, 12)
        Me.lblButtonType.Name = "lblButtonType"
        Me.lblButtonType.Size = New System.Drawing.Size(273, 180)
        Me.lblButtonType.TabIndex = 15
        Me.lblButtonType.TabStop = False
        Me.lblButtonType.Text = "Button Type"
        '
        'frmEditTags
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(295, 332)
        Me.Controls.Add(Me.lblButtonType)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditTags"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Button"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.lblButtonType.ResumeLayout(False)
        Me.lblButtonType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtEventName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboMirror_BackColor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboMirror_TextColor As System.Windows.Forms.ComboBox
    Friend WithEvents lbliPhoneChart As System.Windows.Forms.Label
    Friend WithEvents cboiPhoneCharts As System.Windows.Forms.ComboBox
    Friend WithEvents txtKeyStroke As System.Windows.Forms.TextBox
    Friend WithEvents chkEnableRemoteTransmission As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdBrowse4CalibrationFile As System.Windows.Forms.Button
    Friend WithEvents lblCalibrationFile As System.Windows.Forms.Label
    Friend WithEvents lblButtonType As System.Windows.Forms.GroupBox

End Class
