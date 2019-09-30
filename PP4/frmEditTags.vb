Imports System.Windows.Forms

Public Class frmEditTags
    Friend WithEvents cboBackColor As System.Windows.Forms.ComboBox
    Friend WithEvents cboTextColor As System.Windows.Forms.ComboBox
    Dim kColor_Back As KnownColor()
    Dim kColor_Text As KnownColor()
    Dim IsInitialized As Boolean = False
    Dim AcceptKeyStroke As Boolean = False
    Dim kKeyTemp As Keys = Keys.None        'Temporary holder

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub
    Public Sub SetProperties(ByVal objButton As EventButton.ctlEventButton)
        kKeyTemp = objButton.KeyStroke
        Me.txtEventName.Text = objButton.Caption
        Me.cboMirror_BackColor.Text = objButton.ButtonColor.Name
        Me.cboMirror_TextColor.Text = objButton.ButtonTextColor.Name
        If objButton.HasPreset Then
            Me.cboiPhoneCharts.Text = objButton.PresetName
        Else
            Me.cboiPhoneCharts.Text = "None"
        End If
        Me.txtKeyStroke.Text = objButton.KeyStroke.ToString

        Me.chkEnableRemoteTransmission.Checked = objButton.IsTransmit

        If String.IsNullOrEmpty(objButton.CalibrationFile) Then
            Me.lblCalibrationFile.Text = "None"
        Else
            Me.lblCalibrationFile.Text = objButton.CalibrationFile
        End If

        Me.Text = "Edit Tag: " & Me.txtEventName.Text

        Select Case objButton.ButtonType
            Case EventButton.ctlEventButton.ctlButtonType.Descriptor
                Me.lblButtonType.Text = "Tag Mode: Descriptor"
            Case EventButton.ctlEventButton.ctlButtonType.OutcomeNeg
                Me.lblButtonType.Text = "Tag Mode: Negative Outcome"
            Case EventButton.ctlEventButton.ctlButtonType.OutcomePos
                Me.lblButtonType.Text = "Tag Mode: Positive Outcome"
            Case EventButton.ctlEventButton.ctlButtonType.Team
                Me.lblButtonType.Text = "Tag Mode: Team Label"
        End Select

        Select Case objButton.iPhoneChart
            Case EventButton.ctlEventButton.AnalysisType.uScatterPlot
                Me.cboiPhoneCharts.Text = "Scatter Plot"
            Case EventButton.ctlEventButton.AnalysisType.uOutcomeClusters
                Me.cboiPhoneCharts.Text = "Cluster Chart"
            Case EventButton.ctlEventButton.AnalysisType.uPathwayMaps
                Me.cboiPhoneCharts.Text = "Ball Movement Plot"
        End Select

    End Sub
    Private Sub InitializeComboBox()
        'Set Field color box
        Me.cboBackColor = New ComboBox
        Me.cboBackColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboBackColor.Location = New System.Drawing.Point(102, 45)
        Me.cboBackColor.Name = "cboBackColor"
        Me.cboBackColor.Size = New System.Drawing.Size(165, 21)
        Me.cboBackColor.DropDownWidth = 200
        Me.cboBackColor.TabIndex = 0
        Me.cboBackColor.DropDownStyle = ComboBoxStyle.DropDownList
        cboBackColor.DataSource = kColor_Back
        Me.lblButtonType.Controls.Add(cboBackColor)

        'Set highlights color box
        Me.cboTextColor = New ComboBox
        Me.cboTextColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboTextColor.Location = New System.Drawing.Point(102, 72)
        Me.cboTextColor.Name = "cboTextColor"
        Me.cboTextColor.Size = New System.Drawing.Size(165, 21)
        Me.cboTextColor.DropDownWidth = 200
        Me.cboTextColor.TabIndex = 1
        Me.cboTextColor.DropDownStyle = ComboBoxStyle.DropDownList
        cboTextColor.DataSource = kColor_Text
        Me.lblButtonType.Controls.Add(Me.cboTextColor)

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmEditTags_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Enumerate known colors.
        kColor_Back = [Enum].GetValues(GetType(KnownColor))
        kColor_Text = [Enum].GetValues(GetType(KnownColor))

        'Set color swatch combo boxes.
        InitializeComboBox()

        'Set up mirror controls 
        Me.cboMirror_BackColor.DataSource = kColor_Back
        Me.cboMirror_TextColor.DataSource = kColor_Text

        IsInitialized = True

    End Sub

    Private Sub cboBackColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboBackColor.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Back(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Back(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()

    End Sub

    Private Sub cboTextColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTextColor.Click
        frmTags.ChangeButtonState(, , Color.FromName(cboTextColor.Text).ToKnownColor, , , , , kKeyTemp)
    End Sub

    Private Sub cboTextColor_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles cboTextColor.DrawItem
        ' Draw the background of the item.
        e.DrawBackground()

        ' Create color swatch
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height + 5, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor_Text(e.Index))), rectangle)

        'Set text
        e.Graphics.DrawString(Color.FromKnownColor(kColor_Text(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width + 2, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()

    End Sub

    Private Sub txtEventName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEventName.TextChanged
        frmTags.ChangeButtonState(txtEventName.Text, , , , , , , kKeyTemp)
        Me.Text = "Edit Button: " & Me.txtEventName.Text

    End Sub

    Private Sub cboBackColor_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBackColor.SelectedValueChanged
        If IsInitialized Then frmTags.ChangeButtonState(, Color.FromName(cboBackColor.Text).ToKnownColor, , , , , , kKeyTemp)

    End Sub

    Private Sub cboMirror_TextColor_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMirror_TextColor.SelectedValueChanged
        If IsInitialized Then frmTags.ChangeButtonState(, , Color.FromName(cboMirror_TextColor.Text).ToKnownColor, , , , , kKeyTemp)

    End Sub

    Private Sub OK_Button_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles OK_Button.KeyDown
        If e.KeyCode = Keys.ShiftKey Or e.KeyCode = Keys.ControlKey Then Exit Sub
        'NB - use OK button to capture keystroke in AcceptKeyStroke mode to avoid ugly textbox processes.
        If AcceptKeyStroke Then
            kKeyTemp = e.KeyCode
            If kKeyTemp = Keys.Delete Then kKeyTemp = Keys.None
            If e.Shift Then kKeyTemp = kKeyTemp + Keys.Shift
            If e.Control Then kKeyTemp = kKeyTemp + Keys.Control
            Me.Text = kKeyTemp.ToString

            txtKeyStroke.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtKeyStroke.ForeColor = Color.FromKnownColor(KnownColor.WindowText)
            txtKeyStroke.Font = New Font(txtKeyStroke.Font, FontStyle.Regular)
            txtKeyStroke.Text = kKeyTemp.ToString

            frmTags.ChangeButtonState(, , , , , , , kKeyTemp)
            Application.DoEvents()
        End If
    End Sub

    Private Sub OK_Button_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles OK_Button.KeyPress
        AcceptKeyStroke = False
        e.Handled = True
        Me.OK_Button.Focus()
    End Sub

    Private Sub txtKeyStroke_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtKeyStroke.MouseDown
        AcceptKeyStroke = True
        txtKeyStroke.BackColor = Color.LightYellow
        txtKeyStroke.ForeColor = Color.Black
        txtKeyStroke.Font = New Font(txtKeyStroke.Font, FontStyle.Bold)
        Me.OK_Button.Focus()
    End Sub


 

    Private Sub chkEnableRemoteTransmission_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEnableRemoteTransmission.CheckStateChanged
        If Me.chkEnableRemoteTransmission.Checked Then
            frmTags.ChangeButtonState(txtEventName.Text, , , , , , , , 1)
        Else
            frmTags.ChangeButtonState(txtEventName.Text, , , , , , , , 0)
        End If

    End Sub

    Private Sub cmdBrowse4CalibrationFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse4CalibrationFile.Click
        Dim dlgPresets As New OpenFileDialog
        With dlgPresets
            .Title = "Open remote camera settings..."
            .Filter = "Remote Camera Settings (*.ppx)|*.ppx"
            Dim res As DialogResult = .ShowDialog(frmMain)
            If res = Windows.Forms.DialogResult.Cancel Then Exit Sub
            frmTags.ChangeButtonState(txtEventName.Text, , , , , , , , , .FileName)
            Me.lblCalibrationFile.Text = .FileName
        End With
    End Sub

    Private Sub chkEnableRemoteTransmission_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableRemoteTransmission.CheckedChanged
        Me.cboiPhoneCharts.Enabled = Me.chkEnableRemoteTransmission.Checked
        Me.lbliPhoneChart.Enabled = Me.chkEnableRemoteTransmission.Checked
    End Sub

    Private Sub txtKeyStroke_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKeyStroke.TextChanged

    End Sub

    Private Sub cboiPhoneCharts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboiPhoneCharts.SelectedIndexChanged
        If IsInitialized Then

            Dim nChartType As EventButton.ctlEventButton.AnalysisType
            Select Case Me.cboiPhoneCharts.Text
                Case Is = "Scatter Plot"
                    nChartType = EventButton.ctlEventButton.AnalysisType.uScatterPlot
                Case Is = "Cluster Chart"
                    nChartType = EventButton.ctlEventButton.AnalysisType.uOutcomeClusters
                Case Is = "Ball Movement Plot"
                    nChartType = EventButton.ctlEventButton.AnalysisType.uPathwayMaps
            End Select

            frmTags.ChangeButtonState(, , , , , , , , , , nChartType)
        End If

    End Sub
End Class
