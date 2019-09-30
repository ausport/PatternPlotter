<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateCollection
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCollectionKeyword = New System.Windows.Forms.TextBox
        Me.optCollectionContentSmart = New System.Windows.Forms.RadioButton
        Me.optCollectionContentEmpty = New System.Windows.Forms.RadioButton
        Me.txtNewCollectionName = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(211, 139)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Collection Name:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCollectionKeyword)
        Me.GroupBox1.Controls.Add(Me.optCollectionContentSmart)
        Me.GroupBox1.Controls.Add(Me.optCollectionContentEmpty)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 82)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Collection Content"
        '
        'txtCollectionKeyword
        '
        Me.txtCollectionKeyword.Enabled = False
        Me.txtCollectionKeyword.Location = New System.Drawing.Point(135, 48)
        Me.txtCollectionKeyword.Name = "txtCollectionKeyword"
        Me.txtCollectionKeyword.Size = New System.Drawing.Size(197, 20)
        Me.txtCollectionKeyword.TabIndex = 4
        Me.txtCollectionKeyword.Text = "Enter common GameID keyword..."
        Me.txtCollectionKeyword.Enabled = False
        '
        'optCollectionContentSmart
        '
        Me.optCollectionContentSmart.AutoSize = True
        Me.optCollectionContentSmart.Location = New System.Drawing.Point(21, 51)
        Me.optCollectionContentSmart.Name = "optCollectionContentSmart"
        Me.optCollectionContentSmart.Size = New System.Drawing.Size(108, 17)
        Me.optCollectionContentSmart.TabIndex = 1
        Me.optCollectionContentSmart.Text = "GameID Keyword"
        Me.optCollectionContentSmart.UseVisualStyleBackColor = True
        '
        'optCollectionContentEmpty
        '
        Me.optCollectionContentEmpty.AutoSize = True
        Me.optCollectionContentEmpty.Checked = True
        Me.optCollectionContentEmpty.Location = New System.Drawing.Point(21, 28)
        Me.optCollectionContentEmpty.Name = "optCollectionContentEmpty"
        Me.optCollectionContentEmpty.Size = New System.Drawing.Size(54, 17)
        Me.optCollectionContentEmpty.TabIndex = 0
        Me.optCollectionContentEmpty.TabStop = True
        Me.optCollectionContentEmpty.Text = "Empty"
        Me.optCollectionContentEmpty.UseVisualStyleBackColor = True
        '
        'txtNewCollectionName
        '
        Me.txtNewCollectionName.Location = New System.Drawing.Point(105, 18)
        Me.txtNewCollectionName.Name = "txtNewCollectionName"
        Me.txtNewCollectionName.Size = New System.Drawing.Size(242, 20)
        Me.txtNewCollectionName.TabIndex = 5
        Me.txtNewCollectionName.Text = "New Collection Name"
        Me.txtNewCollectionName.WordWrap = False
        '
        'frmCreateCollection
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(369, 180)
        Me.Controls.Add(Me.txtNewCollectionName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreateCollection"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create New Collection"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optCollectionContentEmpty As System.Windows.Forms.RadioButton
    Friend WithEvents optCollectionContentSmart As System.Windows.Forms.RadioButton
    Friend WithEvents txtCollectionKeyword As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCollectionName As System.Windows.Forms.TextBox

End Class
