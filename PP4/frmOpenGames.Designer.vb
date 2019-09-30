<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpenGames
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpenGames))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.tvGames = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblGameID = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblNotes = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblRecordCount = New System.Windows.Forms.Label
        Me.lblGameOpponent = New System.Windows.Forms.Label
        Me.lblGameVenue = New System.Windows.Forms.Label
        Me.lblGameDate = New System.Windows.Forms.Label
        Me.ContextMenuStrip_OverEmptySpace = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewCollection = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip_OverCollection = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuNewCollection2 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRenameCollection = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuRemoveCollection2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip_OverGame = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRenameGame = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuRemoveGame = New System.Windows.Forms.ToolStripMenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCollection = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip_OverEmptySpace.SuspendLayout()
        Me.ContextMenuStrip_OverCollection.SuspendLayout()
        Me.ContextMenuStrip_OverGame.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(430, 359)
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
        Me.OK_Button.Text = "Add"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Close"
        '
        'tvGames
        '
        Me.tvGames.AllowDrop = True
        Me.tvGames.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvGames.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tvGames.HideSelection = False
        Me.tvGames.ImageIndex = 1
        Me.tvGames.ImageList = Me.ImageList1
        Me.tvGames.Location = New System.Drawing.Point(12, 12)
        Me.tvGames.Name = "tvGames"
        Me.tvGames.SelectedImageIndex = 6
        Me.tvGames.ShowLines = False
        Me.tvGames.ShowPlusMinus = False
        Me.tvGames.ShowRootLines = False
        Me.tvGames.Size = New System.Drawing.Size(296, 373)
        Me.tvGames.StateImageList = Me.ImageList1
        Me.tvGames.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Folder Folders aqua.ico")
        Me.ImageList1.Images.SetKeyName(1, "Folder Games Basketball.ico")
        Me.ImageList1.Images.SetKeyName(2, "Folder Games Soccer.ico")
        Me.ImageList1.Images.SetKeyName(3, "Folder Games Tennis.ico")
        Me.ImageList1.Images.SetKeyName(4, "Games Tennis.ico")
        Me.ImageList1.Images.SetKeyName(5, "Folder Documents.ico")
        Me.ImageList1.Images.SetKeyName(6, "Games Basketball.ico")
        Me.ImageList1.Images.SetKeyName(7, "Folder Library.ico")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Game ID:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(50, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Date:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(42, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Venue:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Opponent:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Record Count:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGameID
        '
        Me.lblGameID.AutoSize = True
        Me.lblGameID.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblGameID.Location = New System.Drawing.Point(89, 51)
        Me.lblGameID.Name = "lblGameID"
        Me.lblGameID.Size = New System.Drawing.Size(11, 13)
        Me.lblGameID.TabIndex = 8
        Me.lblGameID.Text = "*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblCollection)
        Me.GroupBox1.Controls.Add(Me.lblNotes)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblRecordCount)
        Me.GroupBox1.Controls.Add(Me.lblGameOpponent)
        Me.GroupBox1.Controls.Add(Me.lblGameVenue)
        Me.GroupBox1.Controls.Add(Me.lblGameDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblGameID)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(320, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(256, 341)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Game(s) for Review"
        '
        'lblNotes
        '
        Me.lblNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNotes.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblNotes.Location = New System.Drawing.Point(89, 190)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(161, 148)
        Me.lblNotes.TabIndex = 14
        Me.lblNotes.Text = "*"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(45, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Notes:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRecordCount
        '
        Me.lblRecordCount.AutoSize = True
        Me.lblRecordCount.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblRecordCount.Location = New System.Drawing.Point(89, 163)
        Me.lblRecordCount.Name = "lblRecordCount"
        Me.lblRecordCount.Size = New System.Drawing.Size(11, 13)
        Me.lblRecordCount.TabIndex = 12
        Me.lblRecordCount.Text = "*"
        '
        'lblGameOpponent
        '
        Me.lblGameOpponent.AutoSize = True
        Me.lblGameOpponent.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblGameOpponent.Location = New System.Drawing.Point(89, 135)
        Me.lblGameOpponent.Name = "lblGameOpponent"
        Me.lblGameOpponent.Size = New System.Drawing.Size(11, 13)
        Me.lblGameOpponent.TabIndex = 11
        Me.lblGameOpponent.Text = "*"
        '
        'lblGameVenue
        '
        Me.lblGameVenue.AutoSize = True
        Me.lblGameVenue.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblGameVenue.Location = New System.Drawing.Point(89, 107)
        Me.lblGameVenue.Name = "lblGameVenue"
        Me.lblGameVenue.Size = New System.Drawing.Size(11, 13)
        Me.lblGameVenue.TabIndex = 10
        Me.lblGameVenue.Text = "*"
        '
        'lblGameDate
        '
        Me.lblGameDate.AutoSize = True
        Me.lblGameDate.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblGameDate.Location = New System.Drawing.Point(89, 79)
        Me.lblGameDate.Name = "lblGameDate"
        Me.lblGameDate.Size = New System.Drawing.Size(11, 13)
        Me.lblGameDate.TabIndex = 9
        Me.lblGameDate.Text = "*"
        '
        'ContextMenuStrip_OverEmptySpace
        '
        Me.ContextMenuStrip_OverEmptySpace.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewCollection})
        Me.ContextMenuStrip_OverEmptySpace.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_OverEmptySpace.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip_OverEmptySpace.Size = New System.Drawing.Size(192, 26)
        '
        'mnuNewCollection
        '
        Me.mnuNewCollection.Name = "mnuNewCollection"
        Me.mnuNewCollection.Size = New System.Drawing.Size(191, 22)
        Me.mnuNewCollection.Text = "Create New Collection"
        '
        'ContextMenuStrip_OverCollection
        '
        Me.ContextMenuStrip_OverCollection.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewCollection2, Me.mnuRenameCollection, Me.ToolStripSeparator1, Me.mnuRemoveCollection2})
        Me.ContextMenuStrip_OverCollection.Name = "ContextMenuStrip_OverCollection"
        Me.ContextMenuStrip_OverCollection.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip_OverCollection.Size = New System.Drawing.Size(192, 76)
        '
        'mnuNewCollection2
        '
        Me.mnuNewCollection2.Name = "mnuNewCollection2"
        Me.mnuNewCollection2.Size = New System.Drawing.Size(191, 22)
        Me.mnuNewCollection2.Text = "Create New Collection"
        '
        'mnuRenameCollection
        '
        Me.mnuRenameCollection.Name = "mnuRenameCollection"
        Me.mnuRenameCollection.Size = New System.Drawing.Size(191, 22)
        Me.mnuRenameCollection.Text = "Rename Collection"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(188, 6)
        '
        'mnuRemoveCollection2
        '
        Me.mnuRemoveCollection2.Name = "mnuRemoveCollection2"
        Me.mnuRemoveCollection2.Size = New System.Drawing.Size(191, 22)
        Me.mnuRemoveCollection2.Text = "Remove Collection"
        '
        'ContextMenuStrip_OverGame
        '
        Me.ContextMenuStrip_OverGame.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRenameGame, Me.ToolStripSeparator2, Me.mnuRemoveGame})
        Me.ContextMenuStrip_OverGame.Name = "ContextMenuStrip_OverGame"
        Me.ContextMenuStrip_OverGame.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip_OverGame.Size = New System.Drawing.Size(166, 54)
        '
        'mnuRenameGame
        '
        Me.mnuRenameGame.Name = "mnuRenameGame"
        Me.mnuRenameGame.Size = New System.Drawing.Size(165, 22)
        Me.mnuRenameGame.Text = "Rename GameID"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(162, 6)
        '
        'mnuRemoveGame
        '
        Me.mnuRemoveGame.Name = "mnuRemoveGame"
        Me.mnuRemoveGame.Size = New System.Drawing.Size(165, 22)
        Me.mnuRemoveGame.Text = "Remove Game"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Collection:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCollection
        '
        Me.lblCollection.AutoSize = True
        Me.lblCollection.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblCollection.Location = New System.Drawing.Point(89, 25)
        Me.lblCollection.Name = "lblCollection"
        Me.lblCollection.Size = New System.Drawing.Size(0, 13)
        Me.lblCollection.TabIndex = 16
        '
        'frmOpenGames
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(588, 400)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tvGames)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpenGames"
        Me.Opacity = 0.3
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Open Game(s)"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip_OverEmptySpace.ResumeLayout(False)
        Me.ContextMenuStrip_OverCollection.ResumeLayout(False)
        Me.ContextMenuStrip_OverGame.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents tvGames As System.Windows.Forms.TreeView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblGameID As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRecordCount As System.Windows.Forms.Label
    Friend WithEvents lblGameOpponent As System.Windows.Forms.Label
    Friend WithEvents lblGameVenue As System.Windows.Forms.Label
    Friend WithEvents lblGameDate As System.Windows.Forms.Label
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuStrip_OverEmptySpace As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewCollection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip_OverCollection As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuNewCollection2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRenameCollection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRemoveCollection2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip_OverGame As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRenameGame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRemoveGame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCollection As System.Windows.Forms.Label

End Class
