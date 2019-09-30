<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVideo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVideo))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.mnuVideoOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuPlay = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuStop = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPause = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFullScreen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuPlayDouble = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPlayNormal = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPlayHalf = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPlayQuarter = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuMute = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuPlayNext = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuPlayPrev = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSaveCurrentClip = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuExportCurrent = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSetIn = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSetOut = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSelectInPlaylist = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRemoveFromVPL = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.toolVideoCompact = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSetOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip_MediaControl = New System.Windows.Forms.ToolStrip
        Me.mnuVID_Prev = New System.Windows.Forms.ToolStripButton
        Me.mnuVID_Next = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVIDFullScreen = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVID_Save2AVI = New System.Windows.Forms.ToolStripButton
        Me.mnuVID_Add2VPL = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuVID_Send2DV = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton_VideoSync = New System.Windows.Forms.ToolStripButton
        Me.ToolStripPanelTop = New System.Windows.Forms.ToolStripPanel
        Me.ToolStrip_Export2DV = New System.Windows.Forms.ToolStrip
        Me.toolExport_Play = New System.Windows.Forms.ToolStripButton
        Me.toolExport_Pause = New System.Windows.Forms.ToolStripButton
        Me.toolExport_Stop = New System.Windows.Forms.ToolStripButton
        Me.timExport = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.mediaRewind = New System.Windows.Forms.ToolStripButton
        Me.mediaPause = New System.Windows.Forms.ToolStripButton
        Me.mediaStop = New System.Windows.Forms.ToolStripButton
        Me.mediaPlay = New System.Windows.Forms.ToolStripButton
        Me.mediaFF = New System.Windows.Forms.ToolStripButton
        Me.vidSlider = New System.Windows.Forms.TrackBar
        Me.vidStatus = New System.Windows.Forms.StatusStrip
        Me.vidStatusFileName = New System.Windows.Forms.ToolStripStatusLabel
        Me.vidStatusCurrentPosition = New System.Windows.Forms.ToolStripStatusLabel
        Me.vidStatusType = New System.Windows.Forms.ToolStripStatusLabel
        Me.vidStatusFPS = New System.Windows.Forms.ToolStripStatusLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.mnuVideoOptions.SuspendLayout()
        Me.ToolStrip_MediaControl.SuspendLayout()
        Me.ToolStripPanelTop.SuspendLayout()
        Me.ToolStrip_Export2DV.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.vidSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vidStatus.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 40
        '
        'mnuVideoOptions
        '
        Me.mnuVideoOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPlay, Me.mnuStop, Me.mnuPause, Me.ToolStripSeparator1, Me.mnuFullScreen, Me.ToolStripSeparator2, Me.mnuPlayDouble, Me.mnuPlayNormal, Me.mnuPlayHalf, Me.mnuPlayQuarter, Me.ToolStripSeparator3, Me.mnuMute, Me.ToolStripSeparator4, Me.mnuPlayNext, Me.mnuPlayPrev, Me.mnuSaveCurrentClip, Me.mnuExportCurrent, Me.ToolStripSeparator5, Me.mnuSetIn, Me.mnuSetOut, Me.ToolStripSeparator10, Me.mnuSelectInPlaylist, Me.mnuRemoveFromVPL, Me.ToolStripSeparator6, Me.toolVideoCompact, Me.ToolStripSeparator11, Me.mnuSetOptions})
        Me.mnuVideoOptions.Name = "mnuVideoOptions"
        Me.mnuVideoOptions.Size = New System.Drawing.Size(242, 492)
        '
        'mnuPlay
        '
        Me.mnuPlay.ImageTransparentColor = System.Drawing.Color.LightGray
        Me.mnuPlay.Name = "mnuPlay"
        Me.mnuPlay.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlay.Text = "Play"
        '
        'mnuStop
        '
        Me.mnuStop.Name = "mnuStop"
        Me.mnuStop.Size = New System.Drawing.Size(241, 22)
        Me.mnuStop.Text = "Stop"
        '
        'mnuPause
        '
        Me.mnuPause.Name = "mnuPause"
        Me.mnuPause.Size = New System.Drawing.Size(241, 22)
        Me.mnuPause.Text = "Pause"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(238, 6)
        '
        'mnuFullScreen
        '
        Me.mnuFullScreen.Image = CType(resources.GetObject("mnuFullScreen.Image"), System.Drawing.Image)
        Me.mnuFullScreen.ImageTransparentColor = System.Drawing.Color.White
        Me.mnuFullScreen.Name = "mnuFullScreen"
        Me.mnuFullScreen.Size = New System.Drawing.Size(241, 22)
        Me.mnuFullScreen.Text = "Full Screen"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(238, 6)
        '
        'mnuPlayDouble
        '
        Me.mnuPlayDouble.Name = "mnuPlayDouble"
        Me.mnuPlayDouble.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayDouble.Text = "Play x2"
        '
        'mnuPlayNormal
        '
        Me.mnuPlayNormal.Name = "mnuPlayNormal"
        Me.mnuPlayNormal.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayNormal.Text = "Play x1"
        '
        'mnuPlayHalf
        '
        Me.mnuPlayHalf.Name = "mnuPlayHalf"
        Me.mnuPlayHalf.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayHalf.Text = "Play x(1/2)"
        '
        'mnuPlayQuarter
        '
        Me.mnuPlayQuarter.Name = "mnuPlayQuarter"
        Me.mnuPlayQuarter.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayQuarter.Text = "Play x(1/4)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(238, 6)
        '
        'mnuMute
        '
        Me.mnuMute.Name = "mnuMute"
        Me.mnuMute.Size = New System.Drawing.Size(241, 22)
        Me.mnuMute.Text = "Mute"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(238, 6)
        '
        'mnuPlayNext
        '
        Me.mnuPlayNext.Enabled = False
        Me.mnuPlayNext.Image = CType(resources.GetObject("mnuPlayNext.Image"), System.Drawing.Image)
        Me.mnuPlayNext.ImageTransparentColor = System.Drawing.Color.Fuchsia
        Me.mnuPlayNext.Name = "mnuPlayNext"
        Me.mnuPlayNext.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayNext.Text = "Next Clip"
        '
        'mnuPlayPrev
        '
        Me.mnuPlayPrev.Enabled = False
        Me.mnuPlayPrev.Image = CType(resources.GetObject("mnuPlayPrev.Image"), System.Drawing.Image)
        Me.mnuPlayPrev.ImageTransparentColor = System.Drawing.Color.Fuchsia
        Me.mnuPlayPrev.Name = "mnuPlayPrev"
        Me.mnuPlayPrev.Size = New System.Drawing.Size(241, 22)
        Me.mnuPlayPrev.Text = "Play Previous"
        '
        'mnuSaveCurrentClip
        '
        Me.mnuSaveCurrentClip.Image = CType(resources.GetObject("mnuSaveCurrentClip.Image"), System.Drawing.Image)
        Me.mnuSaveCurrentClip.ImageTransparentColor = System.Drawing.Color.White
        Me.mnuSaveCurrentClip.Name = "mnuSaveCurrentClip"
        Me.mnuSaveCurrentClip.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuSaveCurrentClip.Size = New System.Drawing.Size(241, 22)
        Me.mnuSaveCurrentClip.Text = "Save Current Clip"
        '
        'mnuExportCurrent
        '
        Me.mnuExportCurrent.Name = "mnuExportCurrent"
        Me.mnuExportCurrent.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuExportCurrent.Size = New System.Drawing.Size(241, 22)
        Me.mnuExportCurrent.Text = "Export Clip to Device"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(238, 6)
        '
        'mnuSetIn
        '
        Me.mnuSetIn.Name = "mnuSetIn"
        Me.mnuSetIn.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.mnuSetIn.Size = New System.Drawing.Size(241, 22)
        Me.mnuSetIn.Text = "Set In-Point"
        '
        'mnuSetOut
        '
        Me.mnuSetOut.Name = "mnuSetOut"
        Me.mnuSetOut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuSetOut.Size = New System.Drawing.Size(241, 22)
        Me.mnuSetOut.Text = "Set Out-Point"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(238, 6)
        '
        'mnuSelectInPlaylist
        '
        Me.mnuSelectInPlaylist.Name = "mnuSelectInPlaylist"
        Me.mnuSelectInPlaylist.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSelectInPlaylist.Size = New System.Drawing.Size(241, 22)
        Me.mnuSelectInPlaylist.Text = "Select Item in Playlist"
        '
        'mnuRemoveFromVPL
        '
        Me.mnuRemoveFromVPL.Name = "mnuRemoveFromVPL"
        Me.mnuRemoveFromVPL.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuRemoveFromVPL.Size = New System.Drawing.Size(241, 22)
        Me.mnuRemoveFromVPL.Text = "De-Select Item in Playlist"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(238, 6)
        '
        'toolVideoCompact
        '
        Me.toolVideoCompact.CheckOnClick = True
        Me.toolVideoCompact.Name = "toolVideoCompact"
        Me.toolVideoCompact.Size = New System.Drawing.Size(241, 22)
        Me.toolVideoCompact.Text = "Compact Window"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(238, 6)
        '
        'mnuSetOptions
        '
        Me.mnuSetOptions.Name = "mnuSetOptions"
        Me.mnuSetOptions.Size = New System.Drawing.Size(241, 22)
        Me.mnuSetOptions.Text = "Set Video Options"
        '
        'ToolStrip_MediaControl
        '
        Me.ToolStrip_MediaControl.AllowMerge = False
        Me.ToolStrip_MediaControl.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip_MediaControl.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolStrip_MediaControl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVID_Prev, Me.mnuVID_Next, Me.ToolStripSeparator7, Me.mnuVIDFullScreen, Me.ToolStripSeparator8, Me.mnuVID_Save2AVI, Me.mnuVID_Add2VPL, Me.ToolStripSeparator9, Me.mnuVID_Send2DV, Me.ToolStripButton_VideoSync})
        Me.ToolStrip_MediaControl.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip_MediaControl.Name = "ToolStrip_MediaControl"
        Me.ToolStrip_MediaControl.Size = New System.Drawing.Size(433, 68)
        Me.ToolStrip_MediaControl.TabIndex = 3
        Me.ToolStrip_MediaControl.Text = "ToolStrip1"
        '
        'mnuVID_Prev
        '
        Me.mnuVID_Prev.Enabled = False
        Me.mnuVID_Prev.Image = Global.PatternPlotter4.My.Resources.Resources.left_48x48
        Me.mnuVID_Prev.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVID_Prev.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVID_Prev.Name = "mnuVID_Prev"
        Me.mnuVID_Prev.Size = New System.Drawing.Size(52, 65)
        Me.mnuVID_Prev.Text = "Prev"
        Me.mnuVID_Prev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVID_Next
        '
        Me.mnuVID_Next.Enabled = False
        Me.mnuVID_Next.Image = Global.PatternPlotter4.My.Resources.Resources.right_48x48
        Me.mnuVID_Next.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVID_Next.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVID_Next.Name = "mnuVID_Next"
        Me.mnuVID_Next.Size = New System.Drawing.Size(52, 65)
        Me.mnuVID_Next.Text = "Next"
        Me.mnuVID_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 68)
        '
        'mnuVIDFullScreen
        '
        Me.mnuVIDFullScreen.Image = Global.PatternPlotter4.My.Resources.Resources.SlideShow1
        Me.mnuVIDFullScreen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVIDFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVIDFullScreen.Name = "mnuVIDFullScreen"
        Me.mnuVIDFullScreen.Size = New System.Drawing.Size(63, 65)
        Me.mnuVIDFullScreen.Text = "Full Screen"
        Me.mnuVIDFullScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 68)
        '
        'mnuVID_Save2AVI
        '
        Me.mnuVID_Save2AVI.Image = Global.PatternPlotter4.My.Resources.Resources.Video_Folder
        Me.mnuVID_Save2AVI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVID_Save2AVI.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVID_Save2AVI.Name = "mnuVID_Save2AVI"
        Me.mnuVID_Save2AVI.Size = New System.Drawing.Size(70, 65)
        Me.mnuVID_Save2AVI.Text = "Save To AVI"
        Me.mnuVID_Save2AVI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuVID_Add2VPL
        '
        Me.mnuVID_Add2VPL.Image = Global.PatternPlotter4.My.Resources.Resources.Add_Folder
        Me.mnuVID_Add2VPL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVID_Add2VPL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVID_Add2VPL.Name = "mnuVID_Add2VPL"
        Me.mnuVID_Add2VPL.Size = New System.Drawing.Size(81, 65)
        Me.mnuVID_Add2VPL.Text = "Add To Playlist"
        Me.mnuVID_Add2VPL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 68)
        '
        'mnuVID_Send2DV
        '
        Me.mnuVID_Send2DV.Image = Global.PatternPlotter4.My.Resources.Resources.FireWire_48
        Me.mnuVID_Send2DV.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuVID_Send2DV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuVID_Send2DV.Name = "mnuVID_Send2DV"
        Me.mnuVID_Send2DV.Size = New System.Drawing.Size(85, 65)
        Me.mnuVID_Send2DV.Text = "Send To Device"
        Me.mnuVID_Send2DV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton_VideoSync
        '
        Me.ToolStripButton_VideoSync.Image = Global.PatternPlotter4.My.Resources.Resources.ChronoSync
        Me.ToolStripButton_VideoSync.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton_VideoSync.Name = "ToolStripButton_VideoSync"
        Me.ToolStripButton_VideoSync.Size = New System.Drawing.Size(141, 65)
        Me.ToolStripButton_VideoSync.Text = "Synchronise To Game Start"
        Me.ToolStripButton_VideoSync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton_VideoSync.Visible = False
        '
        'ToolStripPanelTop
        '
        Me.ToolStripPanelTop.Controls.Add(Me.ToolStrip_MediaControl)
        Me.ToolStripPanelTop.Controls.Add(Me.ToolStrip_Export2DV)
        Me.ToolStripPanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolStripPanelTop.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripPanelTop.Name = "ToolStripPanelTop"
        Me.ToolStripPanelTop.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.ToolStripPanelTop.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.ToolStripPanelTop.Size = New System.Drawing.Size(1028, 68)
        '
        'ToolStrip_Export2DV
        '
        Me.ToolStrip_Export2DV.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip_Export2DV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolExport_Play, Me.toolExport_Pause, Me.toolExport_Stop})
        Me.ToolStrip_Export2DV.Location = New System.Drawing.Point(436, 0)
        Me.ToolStrip_Export2DV.Name = "ToolStrip_Export2DV"
        Me.ToolStrip_Export2DV.Size = New System.Drawing.Size(168, 68)
        Me.ToolStrip_Export2DV.TabIndex = 6
        Me.ToolStrip_Export2DV.Text = "ToolStrip2"
        Me.ToolStrip_Export2DV.Visible = False
        '
        'toolExport_Play
        '
        Me.toolExport_Play.Image = Global.PatternPlotter4.My.Resources.Resources.Play_Export
        Me.toolExport_Play.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.toolExport_Play.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolExport_Play.Name = "toolExport_Play"
        Me.toolExport_Play.Size = New System.Drawing.Size(52, 65)
        Me.toolExport_Play.Text = "Export"
        Me.toolExport_Play.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'toolExport_Pause
        '
        Me.toolExport_Pause.Image = Global.PatternPlotter4.My.Resources.Resources.Pause_Export
        Me.toolExport_Pause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.toolExport_Pause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolExport_Pause.Name = "toolExport_Pause"
        Me.toolExport_Pause.Size = New System.Drawing.Size(52, 65)
        Me.toolExport_Pause.Text = "Pause"
        Me.toolExport_Pause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'toolExport_Stop
        '
        Me.toolExport_Stop.Image = Global.PatternPlotter4.My.Resources.Resources.Stop_Export
        Me.toolExport_Stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.toolExport_Stop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolExport_Stop.Name = "toolExport_Stop"
        Me.toolExport_Stop.Size = New System.Drawing.Size(52, 65)
        Me.toolExport_Stop.Text = "Stop"
        Me.toolExport_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'timExport
        '
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mediaRewind, Me.mediaPause, Me.mediaStop, Me.mediaPlay, Me.mediaFF})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 545)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ToolStrip1.Size = New System.Drawing.Size(1028, 55)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 7
        '
        'mediaRewind
        '
        Me.mediaRewind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mediaRewind.Image = Global.PatternPlotter4.My.Resources.Resources.XP_Rewind
        Me.mediaRewind.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mediaRewind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mediaRewind.Name = "mediaRewind"
        Me.mediaRewind.Size = New System.Drawing.Size(52, 52)
        Me.mediaRewind.Text = "Rewind"
        '
        'mediaPause
        '
        Me.mediaPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mediaPause.Image = Global.PatternPlotter4.My.Resources.Resources.XP_Pause
        Me.mediaPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mediaPause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mediaPause.Name = "mediaPause"
        Me.mediaPause.Size = New System.Drawing.Size(52, 52)
        Me.mediaPause.Text = "Pause"
        '
        'mediaStop
        '
        Me.mediaStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mediaStop.Image = Global.PatternPlotter4.My.Resources.Resources.XP_Stop
        Me.mediaStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mediaStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mediaStop.Name = "mediaStop"
        Me.mediaStop.Size = New System.Drawing.Size(52, 52)
        Me.mediaStop.Text = "ToolStripButton1"
        '
        'mediaPlay
        '
        Me.mediaPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mediaPlay.Image = CType(resources.GetObject("mediaPlay.Image"), System.Drawing.Image)
        Me.mediaPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mediaPlay.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mediaPlay.Name = "mediaPlay"
        Me.mediaPlay.Size = New System.Drawing.Size(52, 52)
        Me.mediaPlay.Text = "Play"
        '
        'mediaFF
        '
        Me.mediaFF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mediaFF.Image = Global.PatternPlotter4.My.Resources.Resources.XP_Forward
        Me.mediaFF.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mediaFF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mediaFF.Name = "mediaFF"
        Me.mediaFF.Size = New System.Drawing.Size(52, 52)
        Me.mediaFF.Text = "Fast Forward"
        '
        'vidSlider
        '
        Me.vidSlider.AutoSize = False
        Me.vidSlider.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.vidSlider.Dock = System.Windows.Forms.DockStyle.Top
        Me.vidSlider.Location = New System.Drawing.Point(0, 0)
        Me.vidSlider.Margin = New System.Windows.Forms.Padding(0)
        Me.vidSlider.Name = "vidSlider"
        Me.vidSlider.Size = New System.Drawing.Size(1024, 21)
        Me.vidSlider.TabIndex = 1
        Me.vidSlider.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'vidStatus
        '
        Me.vidStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.vidStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.vidStatusFileName, Me.vidStatusCurrentPosition, Me.vidStatusType, Me.vidStatusFPS})
        Me.vidStatus.Location = New System.Drawing.Point(0, 21)
        Me.vidStatus.Name = "vidStatus"
        Me.vidStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode
        Me.vidStatus.Size = New System.Drawing.Size(1024, 22)
        Me.vidStatus.TabIndex = 2
        Me.vidStatus.Text = "Filename"
        '
        'vidStatusFileName
        '
        Me.vidStatusFileName.Name = "vidStatusFileName"
        Me.vidStatusFileName.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.vidStatusFileName.Size = New System.Drawing.Size(807, 17)
        Me.vidStatusFileName.Spring = True
        Me.vidStatusFileName.Text = "Video Filename:"
        '
        'vidStatusCurrentPosition
        '
        Me.vidStatusCurrentPosition.Name = "vidStatusCurrentPosition"
        Me.vidStatusCurrentPosition.Padding = New System.Windows.Forms.Padding(10, 0, 20, 0)
        Me.vidStatusCurrentPosition.Size = New System.Drawing.Size(75, 17)
        Me.vidStatusCurrentPosition.Text = "00:00.0"
        '
        'vidStatusType
        '
        Me.vidStatusType.Name = "vidStatusType"
        Me.vidStatusType.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.vidStatusType.Size = New System.Drawing.Size(86, 17)
        Me.vidStatusType.Text = "Video Format"
        '
        'vidStatusFPS
        '
        Me.vidStatusFPS.Name = "vidStatusFPS"
        Me.vidStatusFPS.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.vidStatusFPS.Size = New System.Drawing.Size(41, 17)
        Me.vidStatusFPS.Text = "FPS"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.vidStatus)
        Me.Panel1.Controls.Add(Me.vidSlider)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 600)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 47)
        Me.Panel1.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 68)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1028, 477)
        Me.Panel2.TabIndex = 9
        '
        'frmVideo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 647)
        Me.ContextMenuStrip = Me.mnuVideoOptions
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ToolStripPanelTop)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmVideo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmVideo"
        Me.mnuVideoOptions.ResumeLayout(False)
        Me.ToolStrip_MediaControl.ResumeLayout(False)
        Me.ToolStrip_MediaControl.PerformLayout()
        Me.ToolStripPanelTop.ResumeLayout(False)
        Me.ToolStripPanelTop.PerformLayout()
        Me.ToolStrip_Export2DV.ResumeLayout(False)
        Me.ToolStrip_Export2DV.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.vidSlider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vidStatus.ResumeLayout(False)
        Me.vidStatus.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents mnuVideoOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuPlay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFullScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPlayDouble As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayNormal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayHalf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayQuarter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMute As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPlayNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayPrev As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveCurrentClip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportCurrent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSetIn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSelectInPlaylist As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveFromVPL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSetOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip_MediaControl As System.Windows.Forms.ToolStrip
    Friend WithEvents mnuVIDFullScreen As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVID_Prev As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVID_Next As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuVID_Save2AVI As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuVID_Add2VPL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuVID_Send2DV As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripPanelTop As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ToolStrip_Export2DV As System.Windows.Forms.ToolStrip
    Friend WithEvents toolExport_Play As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolExport_Pause As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolExport_Stop As System.Windows.Forms.ToolStripButton
    Friend WithEvents timExport As System.Windows.Forms.Timer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents vidSlider As System.Windows.Forms.TrackBar
    Friend WithEvents vidStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents vidStatusFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents vidStatusCurrentPosition As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents vidStatusType As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents vidStatusFPS As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents mediaRewind As System.Windows.Forms.ToolStripButton
    Private WithEvents mediaPause As System.Windows.Forms.ToolStripButton
    Friend WithEvents mediaPlay As System.Windows.Forms.ToolStripButton
    Friend WithEvents mediaFF As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_VideoSync As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolVideoCompact As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mediaStop As System.Windows.Forms.ToolStripButton
End Class
