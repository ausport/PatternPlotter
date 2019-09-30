<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetGame
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtGameID = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.txtAuthor = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtOpponent = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtVenue = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCompetition = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtGameDate = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboTimeCriteria = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.groupbox2 = New System.Windows.Forms.GroupBox
        Me.optVideoNone = New System.Windows.Forms.RadioButton
        Me.optVideoImport = New System.Windows.Forms.RadioButton
        Me.optVideoCapture = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblTransmitTemplatesFound = New System.Windows.Forms.Label
        Me.lblTransmitDestination = New System.Windows.Forms.Label
        Me.chkActivateChartTransmit = New System.Windows.Forms.CheckBox
        Me.chkActivateVideoTransmit = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.groupbox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(240, 391)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(161, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.SpringGreen
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(74, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Start Game"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(87, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Set Game ID: "
        '
        'txtGameID
        '
        Me.txtGameID.Location = New System.Drawing.Point(92, 12)
        Me.txtGameID.Name = "txtGameID"
        Me.txtGameID.Size = New System.Drawing.Size(306, 20)
        Me.txtGameID.TabIndex = 6
        Me.txtGameID.Text = "Set GameID"
        Me.txtGameID.WordWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtNotes)
        Me.GroupBox1.Controls.Add(Me.txtAuthor)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtOpponent)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtVenue)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCompetition)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtGameDate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cboTimeCriteria)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(383, 247)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Game Settings"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(57, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Notes:"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(100, 176)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(255, 57)
        Me.txtNotes.TabIndex = 19
        Me.txtNotes.Text = " "
        Me.txtNotes.WordWrap = False
        '
        'txtAuthor
        '
        Me.txtAuthor.Location = New System.Drawing.Point(100, 150)
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.Size = New System.Drawing.Size(255, 20)
        Me.txtAuthor.TabIndex = 18
        Me.txtAuthor.Text = "User Name"
        Me.txtAuthor.WordWrap = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(53, 153)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Author:"
        '
        'txtOpponent
        '
        Me.txtOpponent.Location = New System.Drawing.Point(100, 124)
        Me.txtOpponent.Name = "txtOpponent"
        Me.txtOpponent.Size = New System.Drawing.Size(255, 20)
        Me.txtOpponent.TabIndex = 16
        Me.txtOpponent.Text = "Opposition"
        Me.txtOpponent.WordWrap = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(38, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Opponent:"
        '
        'txtVenue
        '
        Me.txtVenue.Location = New System.Drawing.Point(100, 98)
        Me.txtVenue.Name = "txtVenue"
        Me.txtVenue.Size = New System.Drawing.Size(255, 20)
        Me.txtVenue.TabIndex = 14
        Me.txtVenue.Text = "Australian Institute of Sport"
        Me.txtVenue.WordWrap = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Competition:"
        '
        'txtCompetition
        '
        Me.txtCompetition.Location = New System.Drawing.Point(100, 72)
        Me.txtCompetition.Name = "txtCompetition"
        Me.txtCompetition.Size = New System.Drawing.Size(255, 20)
        Me.txtCompetition.TabIndex = 12
        Me.txtCompetition.Text = "Test Collection"
        Me.txtCompetition.WordWrap = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(53, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Venue:"
        '
        'txtGameDate
        '
        Me.txtGameDate.Location = New System.Drawing.Point(101, 46)
        Me.txtGameDate.Name = "txtGameDate"
        Me.txtGameDate.Size = New System.Drawing.Size(255, 20)
        Me.txtGameDate.TabIndex = 10
        Me.txtGameDate.Text = "1-1-07"
        Me.txtGameDate.WordWrap = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Game Date:"
        '
        'cboTimeCriteria
        '
        Me.cboTimeCriteria.FormattingEnabled = True
        Me.cboTimeCriteria.Items.AddRange(New Object() {"1st Half", "2nd Half", "1st Quarter", "2nd Quarter", "3rd Quarter", "4th Quarter", "1st Period", "2nd Period", "3rd Period", "4th Period", "1st Set", "2nd Set", "3rd Set", "4th Set", "5th Set", "Period", "None"})
        Me.cboTimeCriteria.Location = New System.Drawing.Point(100, 19)
        Me.cboTimeCriteria.Name = "cboTimeCriteria"
        Me.cboTimeCriteria.Size = New System.Drawing.Size(255, 21)
        Me.cboTimeCriteria.TabIndex = 8
        Me.cboTimeCriteria.Text = "1st Half"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Session Name:"
        '
        'groupbox2
        '
        Me.groupbox2.Controls.Add(Me.optVideoNone)
        Me.groupbox2.Controls.Add(Me.optVideoImport)
        Me.groupbox2.Controls.Add(Me.optVideoCapture)
        Me.groupbox2.Location = New System.Drawing.Point(15, 292)
        Me.groupbox2.Name = "groupbox2"
        Me.groupbox2.Size = New System.Drawing.Size(383, 93)
        Me.groupbox2.TabIndex = 8
        Me.groupbox2.TabStop = False
        Me.groupbox2.Text = "Video Mode"
        '
        'optVideoNone
        '
        Me.optVideoNone.AutoSize = True
        Me.optVideoNone.Location = New System.Drawing.Point(101, 65)
        Me.optVideoNone.Name = "optVideoNone"
        Me.optVideoNone.Size = New System.Drawing.Size(69, 17)
        Me.optVideoNone.TabIndex = 2
        Me.optVideoNone.TabStop = True
        Me.optVideoNone.Text = "No Video"
        Me.optVideoNone.UseVisualStyleBackColor = True
        '
        'optVideoImport
        '
        Me.optVideoImport.AutoSize = True
        Me.optVideoImport.Location = New System.Drawing.Point(101, 42)
        Me.optVideoImport.Name = "optVideoImport"
        Me.optVideoImport.Size = New System.Drawing.Size(123, 17)
        Me.optVideoImport.TabIndex = 1
        Me.optVideoImport.TabStop = True
        Me.optVideoImport.Text = "Import Existing Video"
        Me.optVideoImport.UseVisualStyleBackColor = True
        '
        'optVideoCapture
        '
        Me.optVideoCapture.AutoSize = True
        Me.optVideoCapture.Location = New System.Drawing.Point(101, 19)
        Me.optVideoCapture.Name = "optVideoCapture"
        Me.optVideoCapture.Size = New System.Drawing.Size(117, 17)
        Me.optVideoCapture.TabIndex = 0
        Me.optVideoCapture.TabStop = True
        Me.optVideoCapture.Text = "Capture New Video"
        Me.optVideoCapture.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblTransmitTemplatesFound)
        Me.GroupBox3.Controls.Add(Me.lblTransmitDestination)
        Me.GroupBox3.Controls.Add(Me.chkActivateChartTransmit)
        Me.GroupBox3.Controls.Add(Me.chkActivateVideoTransmit)
        Me.GroupBox3.Location = New System.Drawing.Point(404, 292)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(379, 111)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "File Transmission"
        Me.GroupBox3.Visible = False
        '
        'lblTransmitTemplatesFound
        '
        Me.lblTransmitTemplatesFound.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTransmitTemplatesFound.Location = New System.Drawing.Point(29, 84)
        Me.lblTransmitTemplatesFound.Name = "lblTransmitTemplatesFound"
        Me.lblTransmitTemplatesFound.Size = New System.Drawing.Size(344, 18)
        Me.lblTransmitTemplatesFound.TabIndex = 3
        Me.lblTransmitTemplatesFound.Text = "0 items..."
        '
        'lblTransmitDestination
        '
        Me.lblTransmitDestination.Enabled = False
        Me.lblTransmitDestination.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblTransmitDestination.Location = New System.Drawing.Point(29, 66)
        Me.lblTransmitDestination.Name = "lblTransmitDestination"
        Me.lblTransmitDestination.Size = New System.Drawing.Size(344, 18)
        Me.lblTransmitDestination.TabIndex = 2
        Me.lblTransmitDestination.Text = "Not Active."
        '
        'chkActivateChartTransmit
        '
        Me.chkActivateChartTransmit.AutoSize = True
        Me.chkActivateChartTransmit.Location = New System.Drawing.Point(32, 43)
        Me.chkActivateChartTransmit.Name = "chkActivateChartTransmit"
        Me.chkActivateChartTransmit.Size = New System.Drawing.Size(136, 17)
        Me.chkActivateChartTransmit.TabIndex = 1
        Me.chkActivateChartTransmit.Text = "Activate Chart Transmit"
        Me.chkActivateChartTransmit.UseVisualStyleBackColor = True
        '
        'chkActivateVideoTransmit
        '
        Me.chkActivateVideoTransmit.AutoSize = True
        Me.chkActivateVideoTransmit.Location = New System.Drawing.Point(32, 20)
        Me.chkActivateVideoTransmit.Name = "chkActivateVideoTransmit"
        Me.chkActivateVideoTransmit.Size = New System.Drawing.Size(138, 17)
        Me.chkActivateVideoTransmit.TabIndex = 0
        Me.chkActivateVideoTransmit.Text = "Activate Video Transmit"
        Me.chkActivateVideoTransmit.UseVisualStyleBackColor = True
        '
        'frmSetGame
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(413, 432)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.groupbox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtGameID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetGame"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Game Settings"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.groupbox2.ResumeLayout(False)
        Me.groupbox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtGameID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboTimeCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOpponent As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtVenue As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCompetition As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtGameDate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAuthor As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents groupbox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optVideoNone As System.Windows.Forms.RadioButton
    Friend WithEvents optVideoImport As System.Windows.Forms.RadioButton
    Friend WithEvents optVideoCapture As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkActivateChartTransmit As System.Windows.Forms.CheckBox
    Friend WithEvents chkActivateVideoTransmit As System.Windows.Forms.CheckBox
    Friend WithEvents lblTransmitDestination As System.Windows.Forms.Label
    Friend WithEvents lblTransmitTemplatesFound As System.Windows.Forms.Label

End Class
