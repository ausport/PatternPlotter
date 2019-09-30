Imports System.Drawing.Printing

Module modPPMain
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*  Pattern Plotter
    '*  Copyright 2007 Stuart Morgan
    '*  Australian Institute of Sport
    '*
    '*  Program History:
    '*
    '*  Version 1.x - Inhouse pre-release version.
    '*  Version 2.x - SIS/SAS Release 2004.
    '*  Version 3.x - SIS/SAS Release 2006.
    '*  Version 4.x.x - AIS Release 2007
    '*
    '*
    '*  Revision History:
    '*
    '*  Version 4.1.1 - First beta release (July 2nd, 2007).
    '*
    '*  Version 4.1.2 - 1st July 2007
    '*  - Version string in title bar.
    '*  - DB version check skipped on open games for better speed. (May create an undetected error in the future???)
    '*  - Cluster chart click functions fixed.
    '*  - Descriptor list refresh after close games fixed.
    '*  - Alpha added to coloring for tags on pitch.
    '*  - Opponent, author and venue added to saved properties.
    '*
    '*
    '*  Version 4.1.3 - 12th July 2007. (Japan v Hockeyroos series 2007)
    '*  - Button type information shown in edit buttons window.
    '*  - Remote transmission of chart files.
    '*  - Menu buttons for:
    '*      Reset Timer to Zero (during match with no video)
    '*      Update Charts (when acivated)
    '*      Undo function (disabled to begin with)
    '*
    '*  - Revised pathway map function to be more efficient, and more accurate!.
    '*  - Add chart export to JPEG function.
    '*
    '*
    '*  DataBase Version 5.0 Update:
    '*  - Added extra column to PathData which asserts the PlayNumber for that play from Start to the next Start.
    '*  - Automatic updating code added.
    '*  - Region field returned including update code for version 4 DB. (Hockey only - other sports will need to start from scratch)
    '*
    '*
    '*  Version 4.1.4 - 19th July 2007. (pre-Olympic qualifier changes)
    '*  - Complete redesign of the data searching engine for all charts.
    '*  - Implementation of the AdvancedSearch structure which enables faster boolean searches of event names. (see description below)
    '*  - Timer and status update for chart compilation progress.
    '*  - Advanced search window and functionality implemented.
    '*  - Region selectivity included.
    '*  - System info window added - F1 to activate.
    '*  - Minor bug fix: PCs with only one camera did not recognise device due to array initialising error.
    '*
    '* NB: There is a difficulty in obtaining sets of descriptors in AND-based searches because a simple 'a AND b AND c' database
    '* search will fail (event items are stored as separate lines in the DB, so are now directly connectable.
    '* Hence, for advanced searches that require confirmation that item A exists in the same play as item B, a dual search model
    '* is employed.  All that can be derived directly from the DB, such as time criteria, GameID etc, is included in the SQL statement.
    '* But, eventnames are added to a memory-based array so that after the short list of possible matches is derived from the DB using the SQL
    '* search, a final cross-match routine is employed to compare the PathCollection array with the sets of eventnames selected using the
    '* search mechanisms.
    '*
    '* See: frmAdvancedSearch::CompileAdvancedSearchString
    '*
    '*  Version 4.2.1 - 30th July 2007. (pre-Olympic qualifier changes)
    '*  - Basic functionality for statistical reports... circle entries. event counts etc.
    '*  - Advanced search function enabled for regional and selective data sets.
    '*  - Installer to include video support files ?? Fixed ??
    '*  - Posession Charts
    '*  - Fixed video trimming function - repair hasaudio, and add audio check for each call.
    '*  - Time shift functions tightened.
    '*  - Implement Undo function.
    '*
    '*  Minor Revision 4.2.1.1: CInt() handler added to frmAnalysis::AddDescriptors() to correct exception.
    '*
    '*  Minor Revision 4.2.1.2:
    '*  - Recalibration for erroneous region functions.
    '*  - Simple DB recalibration routine added under tools.  This will need to be more sophistocated when multiple sports are added.
    '*  - Undo button added to toolbar during games.
    '*
    '*  Minor Revision 4.2.1.3:
    '*  - Minor ammendments to time shift functions to account for empty outcome arrays.
    '*
    '*  Minor Revision 4.2.1.4:
    '*  - An attempt at addressing the cluster grouping errors for advanced searches.  Still feels a little messy...
    '*
    '*
    '*  NB: from this point, release packages will be compiled for each new version, but not for each minor revision.
    '*
    '*
    '*  Version 4.2.2 - 18th September 2007. (Follow-up chnages from Olympic qualifiers)
    '*
    '*  Minor Revision 4.2.2.1
    '*
    '*  - Clear All function added to tags windows.
    '*  - Confirmation dialog added for appending VPLs from a list of open VPL windows.
    '*  - Dash's and dots added to pathway maps to denote carries and passes.
    '*  - Interaction functions now for viewing:
    '*      * Cluster items in either playlist or pathmaps
    '*      * Pathmap items in playlist
    '*      * Cluster items in playlist
    '*      * Playlist items in pathmaps.
    '*  - New right-click functions for statistics:
    '*      * Show by GameID
    '*      * Show by TeamName
    '*      * Export data to excel.
    '*  - Right-click function for tags allows to clear all.
    '*  - Scaling errors fixed for spatial charts on advanced searches.
    '*  - Toolbar buttons added to open new pitch windows, tags windows, and statistics windows.
    '*
    '*
    '*  Version 4.2.4.1 - 12th October 2007. (Netball WC camp in Melbourne.)
    '*  - Netball functions.
    '*  - Single-click pass function added.
    '*  - Stats regions added.
    '*  - Video window switched to MDI child form.
    '*  - CTRL-F full screen video function added.
    '*  - VPL video handling reworked slightly to fix some intermittant bugs.
    '*
    '*  Minor Revision 4.3.1.1
    '*
    '*  Version 4.3.1 - 24th September 2007. (Follow-up chnages from netball camp)
    '*
    '*  Version 4.3.1.1 - 24th October 2007.
    '*  - Major rebuild of video rendering engine.  
    '*  - Added modVideoMixer module with AVI, XLT, and GRF exporting functions.
    '*  - Rendering engine now builds timelines and GRF files first, then compiles the video from a GraphBuilder interface.
    '*  - Data table error in CompileGraphData fixed.
    '*  - Error when previewing with no valid video fixed.
    '*
    '*  Version 4.3.1.2 - 24th October 2007.
    '*  - MSDATASRC.dll added to compilation from .NET - should correct the graph error on some systems.
    '*  
    '*  Version 4.3.1.3 - 26th October 2007.
    '*  - Self registration qedit.dll and quartz.dll removed.  These are protected system files.
    '*  - Option for transitions inserted.
    '*  - Rendering method changed - now save timeline as xtl, then reload it to a FilGraphManager interface.
    '*  - Using the RenderEngine now, not the SmartRenderEngine.
    '*  
    '*  Version 4.3.1.4 - 1st November 2007.
    '*  - Minor fix to MDIParent code on autoload after capture.
    '*  
    '*  Version 4.3.1.5 - 6th November 2007.
    '*  - Redundant reference to userprefs.cachedata removed.  Caused errors when defaulted to false.
    '*  
    '*     
    '*  Minor Revision 4.3.2.1
    '*  - Added PointF::PitchOffset to hold scaling offsets for different pitches.
    '*  - NB: xMargin = 11.25 (where 11.25 = 10%; width = 90 = 112.5 * 0.8)
    '*  - Posession tracking charts.
    '*  - Error catch added if no graphs are set and the CurrentGraphs structure is nothing.
    '*  - Compact video window mode added to allow low resolution workspaces.
    '*  - Added selective show capabilities to possession tracking charts.
    '*  - Settings recall function added for player maps.
    '*  - Heat charts completed.
    '*  - Export to DV functions revised and tightened.
    '*  - Video transmission function tightened.
    '*  
    '*     
    '*  Major Revision 4.4.1.1  - 2008 AHL Release.
    '*  - Charts functions tightened with trycast and error catch routines added..
    '*  - clsVideoTrimmer added for simple video trimming from the frmVideo instance.
    '*  - video slider control tightened - each tick represents 1 frame rather than 1 second.
    '*  - edit eventlist dialog added for changing timecriteria, teamname, eventname, eventtype.
    '*  - Timeshift function tightened.
    '*  - Splash screen updated.
    '*     
    '*  Minor Revision 4.4.1.2
    '*  - Chart control functions for player possession maps hidden for other chart types.
    '*  
    '*  Minor Revision 4.4.1.3
    '*  - Minor bug fix - error catch for null data sets in heat charts and event clusters.
    '*  - Minor bug fix - error enumerating video devices when only a single device is present.
    '*     
    '*  Minor Revision 4.4.2.1
    '*  - Minor bug fix - play/pause problem when multiple VPLs open fixed.
    '*
    '*  Minor Revision 4.4.3.1
    '*  - Rugby league added.
    '*  - Minor fix - updating playing field automatically repopulates the statistics regions list in the options tab.
    '*
    '*  Minor Revision 4.4.4.1
    '*  - Database problem
    '*
    '*  Minor Revision 4.4.4.2
    '*  - When no spatial entry is made during plotting, events have a playnumber of 0.  Bug compiling VPLs dealing with 0 fixed.
    '*
    '*  Minor Revision 4.4.4.3
    '*  - Error catch added to the .FirstDisplayedCell command in frmEventList::AddRow2Grid
    '*
    '*
    '*  Minor Revision 4.4.5.1  - April 2008.
    '*  * Data mining tools:
    '*  - Crosstabs - association mining
    '*  - Clusters - k-means, k-meloids
    '*  - Quadrants - pathway clusters (still needs some fine tuning to clean up directions of sequences).
    '*
    '*  Minor Revision 4.4.6.1  - May 2008.
    '*  - Added soccer and basketball.
    '*  - Progress updates added to x-tabs functions.
    '*  - Links from x-tabs to VPL.
    '*
    '*  Minor Revision 4.4.7.1  - June 2008.
    '*  - Merge functions (Events merge done, but not thoroughly checked... may need more work.)
    '*  - PlayVideo function added to manage loading of video files and to report on error if no video is found.
    '*  - First effort at heat charts for ball movement speed.
    '*  - Function to add items from a playlist into a new playlist added.
    '*  - GamePath() array reduced to essential elements to improve loading speed.
    '*  - AddDescriptors(), AddTeamNames(), and AddTimeCriteria() functions moved to the end of the load sequence to avoid unnecessary repeats.
    '*  - Print functions finished.
    '*  - Stop-resume functions improved for playlists.
    '*
    '*  Minor Revision 4.4.8.1  - July 2008.
    '*  - Timecriteria increment
    '*
    '*  Minor Revision 4.4.9.1  - July 2008.
    '*  - Timecriteria increment - bug fix.
    '*
    '*  Minor Revision 4.4.10  - July 31 2008.
    '*  - Resume play - bug fix.
    '*  - Print function - extra page bug fix.
    '*  - FPS bug fix.
    '*
    '*  Major Revision 4.5.1  - January 5, 2009.
    '*  - Add iPhone XML export functionality.
    '*
    '*  Minor Revision 4.5.2  - January 24, 2009.
    '*  - Option to exclude metadata from JPEG export.
    '*
    '*  Minor Revision 4.5.3  - February 11, 2009.
    '*  - Option to exclude metadata from JPEG export.
    '*  - Finished remote camera calibration.
    '*  
    '*  Minor Revision 4.5.4  - March 17, 2009.
    '*  - Minor bug fix - progress bar error when set to zero after a game was loaded, closed, and reloaded..
    '*
    '*  Minor Revision 4.5.5  - March 17, 2009.
    '*  - Minor bug fix - chart jpg save for data mining quadrants..
    '*
    '*  Minor Revision 4.5.6  - March 17, 2009.
    '*  - Added Rugby Union..
    '*  - Fixed Basketball region errors.
    '*
    '*  Minor Revision 4.5.7  - August 2, 2010.
    '*  - Minor bug fix - iPhone pathway charts stopped at descriptor - fixed..
    '*   
    '*  Minor Revision 4.5.8  - June 18, 2011.
    '*  - Add csv data export..
    '*      
    '*  Minor Revision 4.5.9  - June 29, 2011.
    '*  - Add itemset export to data mining functions.
    '*  - Add longitudinal and lateral spatial itemsets to crosstabs
    '*   
    '*  Minor Revision 4.5.10  - November 8, 2011.
    '*  - added lift, confidence, and support calculations for xtabs.
    '*   
    '*  Minor Revision 4.5.11  - November 13, 2011.
    '*  - added path origin option for xtabs.
    '*   
    '*  Minor Revision 4.5.12  - June 20, 2012.
    '*  - added team exclusion option for xtabs.
    '*  
    '*  Major Revision 4.6.1  - August 10, 2017.
    '*  - Windows 10 compatibility.
    '*
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public Const AppName = "Pattern Plotter 4"
    Public Const DBVer = "5.0"
    Public Const AppVersion = "4.6.1"

    '
    ' Enumerations:
    '
    Public Enum tSports
        sHockey = 0
        sSoccer = 1
        sNetball = 2
        sAFL = 3
        sRugbyLeague = 4
        sBasketball = 5
        sRugby7 = 6
    End Enum
    Public Enum descTime
        dtFromPreviousStart = 0
        dtStandaloneEvent = 1
    End Enum
    Public Enum quadThresholdType
        qtPercent = 0
        qtValue = 1
    End Enum
    Public Enum gMode
        dlgPlotting = 1
        dlgPostScript = 2
    End Enum
    Public Enum OutcomeType
        outPositive = 0
        outNegative = 1
        outDescriptor = 2
        outNone = 99
        outAll = 999
    End Enum
    Public Enum PathStatus
        psStart = 0
        psPass = 1
        psCarry = 2
        psPost = 3  'Post game addition of descriptors
    End Enum
    Public Enum StatsLayout
        statByGame = 0
        statByTeam = 1
    End Enum
    Public Enum tRegion
        rgTotal = -1

        None = 0

        'Hockey Regions
        HockeyOffensiveCircle = 1
        HockeyOffensive25 = 2
        HockeyOffensiveHalf = 3
        HockeyDefensiveHalf = 4
        HockeyDefensive25 = 5
        HockeyDefensiveCircle = 6

        'Netball Regions
        NetballAttackCircle = 11
        NetballAttackThird = 12
        NetballMiddleThird = 13
        NetballDefensiveThird = 14
        NetballDefensiveCircle = 15

        'Soccer regions
        Soccer_FrontCentre = 21
        Soccer_FrontThird = 22
        Soccer_MiddleThird = 23
        Soccer_BackThird = 24

        'AFL regions
        rgAFLForward50 = 31
        rgAFLForwardFlank = 32
        rgAFLCentreCorridor = 33
        rgAFLDefensiveFlank = 34
        rgAFLBack50 = 35

        'Rugby League regions
        RugbyLeague_AttGoalArea = 40
        RugbyLeague_Att10ToGoal = 41
        RugbyLeague_Att20To10 = 42
        RugbyLeague_Att30To20 = 43
        RugbyLeague_Att40To30 = 44
        RugbyLeague_Att50To40 = 45
        RugbyLeague_Def50To40 = 46
        RugbyLeague_Def40To30 = 47
        RugbyLeague_Def30To20 = 48
        RugbyLeague_Def20To10 = 49
        RugbyLeague_Def10ToGoal = 50
        RugbyLeague_DefGoalArea = 51

        'Basketball
        Basketball_AttackCircle = 61
        Basketball_AttackCourt = 62
        Basketball_DefensiveCourt = 63
        Basketball_DefensiveCircle = 64

        'Rugby 7 regions
        Rugby7_Attack_InGoal = 71
        Rugby7_Attack_22 = 72
        Rugby7_Attack_Half = 73
        Rugby7_Defensive_Half = 74
        Rugby7_Defensive_22 = 75
        Rugby7_Defensive_InGoal = 76

    End Enum
    Public Structure VideoSelection
        Public vsStart As Double
        Public vsEnd As Double
        Public vsSource As String
        Public vsDestination As String
        Public vsHasAudio As Boolean
        Public vsDuration As Double
        Public vsRow As Integer    'Used for playlists - refers to the row number in the playlist window.
    End Structure
    Public Structure PathOutComes
        Public Outcome As OutcomeType
        Public EventName As String
        Public GameTC As Single
        Public VideoTC As Single
        Public TagColor As Color
        Public ThisOutcomeMatchesCriteria As Boolean    'Set to true in some search methods when a pre-search finds a match.
        Public ListRow As Integer      'Temp - refers to location on event list.
    End Structure
    Public Structure GameProperties
        Public GameID As String
        Public GameDate As Date
        Public GameDateString As String
        Public GameOpponent As String
        Public GameVenue As String
        Public TimeCriteria As String
        Public GameAuthor As String
        Public GameNotes As String
        Public Competition As String
        Public Cached As Boolean
    End Structure
    Public Structure PlotPoints
        Public Coordinates As PointF
        Public SpatialCorrection As PointF
        Public GameInformation As GameProperties
        Public GameTC As Single
        Public VideoTC As Single
        Public TimeCriteria As String
        'Public EndTime As Single
        Public RecordID As Long
        Public Status As PathStatus
        'Public BeginningOfPlayAtN As Integer
        Public TeamName As String
        'Public Distance As Single
        Public Region As tRegion
        Public TagColor As Color
        Public TagFontColor As Color
        Public OutcomeCount As Integer
        'Public MovementCount As Integer    'Number of ball movements leading to outcome
        'Public PassCount As Integer            'Number of passes leading to outcome.
        Public OutcomeProp() As PathOutComes
        Public OutcomesExist As Boolean    'True when outcomes exist further along the path sequence.
        'Public OutcomesExistAtN As Integer 'Index value n where outcomes start.  eg,. GamePath(n).OutcomesExistAtN = n+5
        'Public OutcomesMatchCriteria As Boolean    'True if search finds matches embedded in this play.
        'Public PlayHasMatch As Boolean 'True for start of play is a later point in the play has a search match.
        Public VideoFile As String
        Public VideoFile2 As String
        'Public TeamIndex As Integer
        'Public StartPath As Boolean
        'Public CarryStart As PointF
        'Public CarryEnd As PointF
        'Public CarryStartTime As Single 'Time point that matches the PathStartX/Y event
        'Public UserTimeInterval As Integer     'Value assigned by function during graph routine.  Used to separate arbitrary user time intervals (eg 3 of 7).
        Public ListRow As Integer      'Temp - refers to location on event list.
        Public PlotMode As gMode        'Specifies whether this item is plotted or postscript
    End Structure
    Public Structure UTDPrefs
        'Video settings
        Public DuplicateInSlowMotion As Boolean     'Repeat clips in VPLs in slow motion.
        Public AddFadeTransitions As Boolean        'Add transitions between items in VPL. 
        Public PreviewAudioOnCapture As Boolean     'True if preview audio during capture
        Public AutoPlay As Boolean                  'AutoPlay after capture
        Public VideoCaptureFormat As String         'Compression format for capture
        Public VideoCaptureDir As String            'Default trimming directory
        Public StopAtEndOfClip As Boolean           'If true, stops selected clips and endopoint, otherwise plays until user intervention.
        Public PlayContinuous As Boolean            'If true then clips play continuously
        Public dbPath As String                     'Location of Database
        Public AutoUpdateDB As Boolean              'If true then any manual changes to action list are automatically applied to DB.
        Public CacheAllData As Boolean              'Cache all data at loading - false setting will speed up loading.
        Public Sport As tSports                     'Currently selected sport.
        'Field display settings
        Public FieldBackground As Color
        Public FieldPerimeter As Color
        Public FieldLines As Color
        Public FieldHighlights As Color
        'Game management settings
        Public AutoReload As Boolean                'Reload game on completion if true
        Public SingleClickForPass As Boolean        'Netball specific - one click executes a pass.
        'Remote camera
        Public CameraIP As String                   'Camera IP address
        Public CameraResolution As Resolution       'Camera resolution
        'PathwayMaps settings
        Public pmLineWidth As Single
        Public pmLineTension As Single
        Public pmStartColor As Color
        Public pmEndColor As Color
        Public pmDifferntiateThreshold As Integer   'The number of plays above which preformace is slowed by differentiating passes and carries.
        'Clusters setttings         
        Public clHorizontalQ As Integer
        Public clVerticalQ As Integer
        'Heat Map settings
        Public hHorizontalQ As Integer
        Public hVerticalQ As Integer
        Public boolShowAllBallMovementsInHeat As Boolean
        'Enable File Transmit
        Public FileTransmitDestination As String
        Public TempFileDestination As String
        'VideoTransmit settings
        Public VideoTransmitEnabled As Boolean
        Public VideoTransmitEpoch As Double
        'Alternative video paths
        Public boolSearchPaths As Boolean               'If true, searches in specified paths for video file if not found in default location.
        Public szAlternativePath() As String          'Alternate path..
        Public nAlternativePathCount As Integer
        'Video Times
        Public LeadTime As Double
        Public LagTime As Double
        'Statistics
        Public StatIncludedRegions() As tRegion
        Public StatRegionCount As Integer
        Public StatShowDescriptors As Boolean
        Public StatLayout As StatsLayout
        Public StatShowTotals As Boolean
        'iPhone Update
        Public iPhoneIsActive As Boolean
        Public xmlURL As String
        Public xmlUsername As String
        Public xmlPassword As String
        Public iPhoneByTeams As Boolean
        Public iPhoneByTimes As Boolean
        Public iPhoneIncludeStats As Boolean
    End Structure
    Public Structure UserAnalysis
        Public VideoPlaylist As Boolean
        Public OutcomeClusters As Boolean
        Public PathwayMaps As Boolean
        Public PossessionGraphs As Boolean
        Public PlayerMaps As Boolean
        Public HeatMaps As Boolean
        Public ScatterPlots As Boolean
    End Structure
    Public Structure ButtonProps
        Public Caption As String
        Public PositiveOutcome As OutcomeType
        Public ForeColor As Color
        Public BackColor As Color
        Public Transparency As Integer
    End Structure

    Public Structure XMLData
        Public location As Point
        Public color As Color
    End Structure

    Public Structure XMLReport
        Public EventName As String
        Public ByTeam As Boolean
        Public ByTime As Boolean
        Public ChartMode As AnalysisType
        Public Data() As XMLData
    End Structure

    Public Structure VisibleDescriptor
        Public Text As String
        Public Type As OutcomeType
        Public Visible As Boolean
    End Structure

    Public Structure MaxMinValues
        Public nMax As Single
        Public nMin As Single
    End Structure

    Public Structure SearchTemplate
        Dim boolPathwayMap As Boolean
        Dim boolClusters As Boolean
        Dim boolVPL As Boolean
        Dim boolGraphs As Boolean
        Dim szTeamNames As String
        Dim szTimeCriteria As String
        Dim szOutcomes As String
        Dim szSearchItems() As String
    End Structure

    Public Structure AdvancedSearchCriteria
        'Dim boolAnd As Boolean  'If true then all selected events names must be matched.
        'Dim Regions() As Integer        'Array of regions that must match selection.
        Dim GameID() As String          'Array of GameIDs that must match selection.
        Dim TeamName() As String        'Array of TeamNames that must match selection.
        Dim TimeCriteria() As String    'Array of TimeCriteria that must match selection.
        Dim Regions() As tRegion

        Dim DBSearchString As String    'Initial DB SQL string, excludes combinations of eventnames.
        Dim EventNameSet() As String      'Sets of eventname combinations.
        Dim EventNameBoolean() As Boolean   'If true then EventNameSet() is an exclusive AND search: eg. Item1 AND Item2 AND Item3

    End Structure

    Public AdvancedSearch As AdvancedSearchCriteria = Nothing
    Public AdvancedSearchIsActive As Boolean = False

    Structure PlayEvents
        Public ID() As Long             'Array of pathID references contained in the play
        Public EventNames() As String   'Array of eventname strings contained in the play
    End Structure

    Public PlottingMode As gMode
    Public UserPrefs As UTDPrefs
    Public GamePath() As PlotPoints
    Public PathCount As Integer
    Public GameCount As Integer
    Public GamesCurrentlyOpen() As GameProperties

    Public PlayCount As Integer = Nothing

    Public PitchOffset As New PointF(11.25, 18.75)

    Public CurrentAutoChartTemplates() As SearchTemplate = Nothing

    Public propsCurrentGame As GameProperties = Nothing
    Public szCurrentTimeCriteria As String = "1st Half"
    Public szCurrentVideoFile As String = Nothing
    Public boolCurrentVideoIsVPL As Boolean  'True if the current video is derived from a playlist
    Public szCurrentTeamName As String = Nothing
    Public szGamesLoaded() As String
    Public szTeamName() As String
    Public bVideoCaptureCurrent As Boolean = False
    Public bTimeCriteriaIsIncremented As Boolean = False

    Public TotalTC As Single = 0        'Total of current captures (greater than video capture time if several events have been transmitted and cut)

    Public AnalysisSettings As UserAnalysis
    Public VisibleDescriptorList() As VisibleDescriptor

    Public bGameIsActive As Boolean = False

    Public frmVPL() As frmVideoPlayList
    Public countVPL As Integer
    Public lastVPLFormUsed As Integer

    Public frmC() As frmChart
    Public countC As Integer

    Public frmG() As frmGraph
    Public countG As Integer

    Public frmE() As frmEventList
    Public countE As Integer
    Public lastEventFormUsed As Integer

    Public frmAutoSearch As frmAnalysis

    Public GameTime_NoVideo As Integer = 0
    Public GameTime_Start As Integer = My.Computer.Clock.TickCount / 1000
    Public CurrentPrintChart As Integer = Nothing


    Public Function GetPitchOffSets(ByVal sSport As tSports) As PointF
        Dim pf As PointF = Nothing
        'NB: x offset is 10% of total width.
        'ie. 90 (pitch width) / 8
        Select Case sSport
            Case tSports.sHockey
                pf = New PointF(11.25, 18.75)
            Case tSports.sNetball
                pf = New PointF(11.25, 22.5)
            Case tSports.sRugbyLeague
                pf = New PointF(8.5, 15.25)
            Case tSports.sRugby7
                pf = New PointF(8.75, 15.0)
            Case tSports.sBasketball
                pf = New PointF(6.25, 11.75)
            Case tSports.sSoccer
                pf = New PointF(11.875, 18.75)
        End Select

        Return pf
    End Function

    Public Sub GetSettings()
        With UserPrefs
            .AddFadeTransitions = CBool(GetSetting(AppName, "Settings", "AddFadeTransitions", "False"))
            .DuplicateInSlowMotion = CBool(GetSetting(AppName, "Settings", "DuplicateInSlowMotion", "False"))
            .VideoCaptureDir = GetSetting(AppName, "Settings", "VideoCaptureDir", "C:\")
            .VideoCaptureFormat = GetSetting(AppName, "Settings", "VideoCaptureFormat", "DV Video Decoder")
            .AutoPlay = CBool(GetSetting(AppName, "Settings", "AutoPlay", "False"))
            .PreviewAudioOnCapture = CBool(GetSetting(AppName, "Settings", "PreviewAudioOnCapture", "True"))
            .PlayContinuous = CBool(GetSetting(AppName, "Settings", "PlayContinuous", "False"))
            .StopAtEndOfClip = CBool(GetSetting(AppName, "Settings", "StopAtEndOfClip", "False"))
            .dbPath = GetSetting(AppName, "Settings", "dbPath", My.Application.Info.DirectoryPath & "\GamePath.mdb")
            .AutoUpdateDB = CBool(GetSetting(AppName, "Settings", "AutoUpdateDB", "True"))
            .CacheAllData = CBool(GetSetting(AppName, "Settings", "CacheAllData", "True"))
            .FieldBackground = Color.FromName(GetSetting(AppName, "Settings", "FieldBackground", "Blue"))
            .FieldHighlights = Color.FromName(GetSetting(AppName, "Settings", "FieldHighlights", "Light Blue"))
            .FieldLines = Color.FromName(GetSetting(AppName, "Settings", "FieldLines", "White"))
            .FieldPerimeter = Color.FromName(GetSetting(AppName, "Settings", "FieldPerimeter", "Yellow"))
            .Sport = GetSportTypeFromString(GetSetting(AppName, "Settings", "Sport", "Hockey"))
            PitchOffset = GetPitchOffSets(.Sport)
            .AutoReload = CBool(GetSetting(AppName, "Settings", "AutoReload", "True"))
            .CameraIP = GetSetting(AppName, "Settings", "CameraIP", "192.168.1.100")
            .CameraResolution = GetSetting(AppName, "Settings", "CameraResolution", 320)
            .pmLineWidth = GetSetting(AppName, "Settings", "pmLineWidth", 1)
            .pmLineTension = GetSetting(AppName, "Settings", "pmLineTension", 0)
            .pmStartColor = Color.FromName(GetSetting(AppName, "Settings", "pmStartColor", "Blue"))
            .pmEndColor = Color.FromName(GetSetting(AppName, "Settings", "pmEndColor", "Red"))
            .pmDifferntiateThreshold = GetSetting(AppName, "Settings", "pmDifferntiateThreshold", 250)
            .FileTransmitDestination = GetSetting(AppName, "Settings", "FileTransmitDestination", "C:\")
            .TempFileDestination = GetSetting(AppName, "Settings", "TempFileDestination", "C:\")
            .VideoTransmitEnabled = CBool(GetSetting(AppName, "Settings", "VideoTransmitEnabled", "True"))
            .VideoTransmitEpoch = GetSetting(AppName, "Settings", "VideoTransmitEpoch", 10)
            .SingleClickForPass = CBool(GetSetting(AppName, "Settings", "SingleClickForPass", "False"))

            .StatRegionCount = GetSetting(AppName, "Settings", "StatRegionCount", 0)
            .StatShowDescriptors = CBool(GetSetting(AppName, "Settings", "StatShowDescriptors", "True"))
            .StatLayout = GetSetting(AppName, "Settings", "StatLayout", 0)
            .StatShowTotals = CBool(GetSetting(AppName, "Settings", "StatShowTotals", "True"))
            .boolShowAllBallMovementsInHeat = CBool(GetSetting(AppName, "Settings", "boolShowAllBallMovementsInHeat", "False"))

            .iPhoneIsActive = GetSetting(AppName, "Settings", "iPhoneIsActive", "False")
            .xmlPassword = GetSetting(AppName, "Settings", "xmlPassword", "WCA5TM3F")
            .xmlURL = GetSetting(AppName, "Settings", "xmlURL", "http://www.in2sport.com.au/")
            .xmlUsername = GetSetting(AppName, "Settings", "xmlUsername", "iczb9ceb")
            .iPhoneByTeams = GetSetting(AppName, "Settings", "iPhoneByTeams", "True")
            .iPhoneByTimes = GetSetting(AppName, "Settings", "iPhoneByTimes", "True")
            .iPhoneIncludeStats = GetSetting(AppName, "Settings", "iPhoneIncludeStats", "True")

            Erase .StatIncludedRegions
            If .StatRegionCount > 0 Then
                For i As Integer = 0 To .StatRegionCount - 1
                    ReDim Preserve .StatIncludedRegions(i)
                    .StatIncludedRegions(i) = GetRegionFromString(GetSetting(AppName, "Settings", "StatIncludedRegions_" & i.ToString, "None"))
                Next
            End If


            .clHorizontalQ = GetSetting(AppName, "Settings", "clHorizontalQ", 3)
            .clVerticalQ = GetSetting(AppName, "Settings", "clVerticalQ", 5)
            .LeadTime = GetSetting(AppName, "Settings", "LeadTime", 10)
            .LagTime = GetSetting(AppName, "Settings", "LagTime", 7)
            .boolSearchPaths = CBool(GetSetting(AppName, "Settings", "boolSearchPaths", "False"))
            .nAlternativePathCount = GetSetting(AppName, "Settings", "nAlternativePathCount", 0)
            Erase .szAlternativePath
            If .nAlternativePathCount > 0 Then
                For i As Integer = 0 To .nAlternativePathCount - 1
                    ReDim Preserve .szAlternativePath(i)
                    .szAlternativePath(i) = GetSetting(AppName, "Settings", "Path_" & i.ToString)
                Next
            End If
        End With

        With propsCurrentGame
            .GameAuthor = GetSetting(AppName, "Settings", "GameAuthor", My.User.Name)
            .GameDate = Date.Today
            .GameDateString = Date.Today.ToLongDateString
            .GameID = GetSetting(AppName, "Settings", "GameID", "New Game ID")
            .GameNotes = GetSetting(AppName, "Settings", "GameNotes")
            .GameOpponent = GetSetting(AppName, "Settings", "GameOpponent", "Opposition")
            .GameVenue = GetSetting(AppName, "Settings", "GameVenue", "Australian Institute of Sport")
            .Competition = GetSetting(AppName, "Settings", "Competition", "None")
        End With

        CurrentGraphs = GetGraphs()
    End Sub

    

    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '* Public Function GetTimeStringFromSeconds(sTime As Single) As String
    '*
    '*   Returns a formatted time string hh:mm:ss.0.
    '*
    '*  Args:
    '*      sTime As Single: Number of seconds.
    '*
    '*  Returns:
    '*      GetTimeStringFromSeconds = formatted string.
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    Public Function GetTimeStringFromSeconds(ByRef sTime As Double, Optional ByRef trim2secs As Boolean = False, _
        Optional ByVal SportCodeFormat As Boolean = False) As String

        Dim hrs As Integer
        Dim mins As Integer
        Dim secs As Single
        If sTime < 0 Then sTime = 0
        hrs = Int(sTime / 3600)
        mins = Int((sTime - (hrs * 3600)) / 60)
        secs = sTime - ((hrs * 3600) + (mins * 60))


        If SportCodeFormat Then
            Dim msecs As Integer = (secs - Int(secs)) * 100
            Return Format(hrs, "00") & ":" & Format(mins, "00") & ":" & Format(secs, "00") & ":" & Format(msecs, "00")

        End If

        If trim2secs Then
            GetTimeStringFromSeconds = Format(hrs, "00:") & Format(mins, "00:") & Format(secs, "00")
        Else
            GetTimeStringFromSeconds = Format(hrs, "00:") & Format(mins, "00:") & Format(secs, "00.00")
        End If
    End Function

    Public Function GetSecondsFromTimeString(ByVal szTimeString As String) As Double
        Dim seconds As Integer = 0
        Try
            seconds = Microsoft.VisualBasic.Left(szTimeString, 2) * 3600
            seconds = seconds + Microsoft.VisualBasic.Mid(szTimeString, 4, 2) * 60
            seconds = seconds + Microsoft.VisualBasic.Mid(szTimeString, 7, 2)
            Return seconds
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function GetSportStringFromType(ByVal nIndex As tSports) As String

        Select Case nIndex
            Case Is = tSports.sHockey
                Return "Hockey"
            Case Is = tSports.sSoccer
                Return "Soccer"
            Case Is = tSports.sNetball
                Return "Netball"
            Case Is = tSports.sAFL
                Return "Australian Football"
            Case Is = tSports.sRugbyLeague
                Return "Rugby League"
            Case Is = tSports.sBasketball
                Return "Basketball"
            Case Is = tSports.sRugby7
                Return "Rugby 7s"
        End Select
        Return "Hockey"

    End Function

    Public Function GetSportTypeFromString(ByVal szSportName As String) As tSports

        Select Case szSportName
            Case Is = "Hockey"
                Return tSports.sHockey
            Case Is = "Soccer"
                Return tSports.sSoccer
            Case Is = "Netball"
                Return tSports.sNetball
            Case Is = "Australian Football"
                Return tSports.sAFL
            Case Is = "Rugby League"
                Return tSports.sRugbyLeague
            Case Is = "Basketball"
                Return tSports.sBasketball
            Case Is = "Rugby 7s"
                Return tSports.sRugby7
        End Select

        Return tSports.sHockey
    End Function

    Public Function GetStatusString(ByVal uStatus As PathStatus) As String
        GetStatusString = OutcomeType.outNone

        Select Case uStatus
            Case Is = PathStatus.psCarry
                GetStatusString = "Carry"
            Case Is = PathStatus.psPass
                GetStatusString = "Pass"
            Case Is = PathStatus.psPost
                GetStatusString = "Postscript"
            Case Is = PathStatus.psStart
                GetStatusString = "Start"
        End Select
    End Function

    Public Function GetOutcomeString(ByVal outcomeVal As OutcomeType) As String
        GetOutcomeString = OutcomeType.outNone

        Select Case outcomeVal
            Case Is = OutcomeType.outDescriptor
                GetOutcomeString = "Descriptor"
            Case Is = OutcomeType.outNegative
                GetOutcomeString = "Negative"
            Case Is = OutcomeType.outPositive
                GetOutcomeString = "Positive"
            Case Is = OutcomeType.outNone
                GetOutcomeString = "None"
        End Select
    End Function

    Public Function GetOutcomeFromString(ByVal szOutcome As String) As OutcomeType
        If szOutcome.Contains("Descriptor") Then
            Return OutcomeType.outDescriptor
        ElseIf szOutcome.Contains("Positive") Then
            Return OutcomeType.outPositive
        ElseIf szOutcome.Contains("Negative") Then
            Return OutcomeType.outNegative
        End If
        Return OutcomeType.outAll

    End Function
    Public Function GetRegionFromString(ByVal szString As String) As tRegion
        Select Case szString

            Case Is = "Defensive 16"
                Return tRegion.HockeyDefensiveCircle
            Case Is = "Defensive 25"
                Return tRegion.HockeyDefensive25
            Case Is = "Defensive Half"
                Return tRegion.HockeyDefensiveHalf
            Case Is = "Offensive 16"
                Return tRegion.HockeyOffensiveCircle
            Case Is = "Offensive 25"
                Return tRegion.HockeyOffensive25
            Case Is = "Offensive Half"
                Return tRegion.HockeyOffensiveHalf

            Case Is = "Circle Attack"
                Return tRegion.NetballAttackCircle
            Case Is = "Attack 3rd"
                Return tRegion.NetballAttackThird
            Case Is = "Middle 3rd"
                Return tRegion.NetballMiddleThird
            Case Is = "Defensive 3rd"
                Return tRegion.NetballDefensiveThird
            Case Is = "Circle Defense"
                Return tRegion.NetballDefensiveCircle

            Case Is = "Forward Goal Area"
                Return tRegion.RugbyLeague_AttGoalArea
            Case Is = "Forward 10 Meter Line to Goal"
                Return tRegion.RugbyLeague_Att10ToGoal
            Case Is = "Forward 20-10 Meter Line"
                Return tRegion.RugbyLeague_Att20To10
            Case Is = "Forward 30-20 Meter Line"
                Return tRegion.RugbyLeague_Att30To20
            Case Is = "Forward 40-30 Meter Line"
                Return tRegion.RugbyLeague_Att40To30
            Case Is = "Forward Half-40 Meter Line"
                Return tRegion.RugbyLeague_Att50To40

            Case Is = "Defensive Goal Area"
                Return tRegion.RugbyLeague_DefGoalArea
            Case Is = "Defensive 10 Meter Line to Goal"
                Return tRegion.RugbyLeague_Def10ToGoal
            Case Is = "Defensive 20-10 Meter Line"
                Return tRegion.RugbyLeague_Def20To10
            Case Is = "Defensive 30-20 Meter Line"
                Return tRegion.RugbyLeague_Def30To20
            Case Is = "Defensive 40-30 Meter Line"
                Return tRegion.RugbyLeague_Def40To30
            Case Is = "Defensive Half-40 Meter Line"
                Return tRegion.RugbyLeague_Def50To40
            Case Is = "Attacking 3 Point Arc"
                Return tRegion.Basketball_AttackCircle
            Case Is = "Attacking Court"
                Return tRegion.Basketball_AttackCourt
            Case Is = "Defensive 3 Point Arc"
                Return tRegion.Basketball_DefensiveCircle
            Case Is = "Defensive Court"
                Return tRegion.Basketball_DefensiveCourt

            Case Is = "Attacking In Goal Area"
                Return tRegion.Rugby7_Attack_InGoal
            Case Is = "Attacking 22 Meters"
                Return tRegion.Rugby7_Attack_22
            Case Is = "Attacking Half"
                Return tRegion.Rugby7_Attack_Half
            Case Is = "Defensive Half"
                Return tRegion.Rugby7_Defensive_Half
            Case Is = "Defensive 22 Meters"
                Return tRegion.Rugby7_Defensive_22
            Case Is = "Defensive In Goal Area"
                Return tRegion.Rugby7_Defensive_InGoal



            Case Is = ""

            Case Else
                Return tRegion.None

        End Select

    End Function

    Public Function GetRegionString(ByVal reg As tRegion, Optional ByVal adjustBySport As Boolean = False) As String

        If adjustBySport Then
            Select Case UserPrefs.Sport
                Case Is = tSports.sNetball
                    reg = CInt(reg) + 10
                Case Is = tSports.sSoccer
                    reg = CInt(reg) + 20
                Case Is = tSports.sAFL
                    reg = CInt(reg) + 30
                Case Is = tSports.sRugbyLeague
                    reg = CInt(reg) + 39
                Case Is = tSports.sBasketball
                    reg = CInt(reg) + 60
                Case Is = tSports.sRugby7
                    reg = CInt(reg) + 70
            End Select
        End If


        Select Case reg

            Case Is = tRegion.HockeyDefensiveCircle
                GetRegionString = "Defensive 16"
            Case Is = tRegion.HockeyOffensiveCircle
                GetRegionString = "Offensive 16"
            Case Is = tRegion.HockeyDefensive25
                GetRegionString = "Defensive 25"
            Case Is = tRegion.HockeyOffensive25
                GetRegionString = "Offensive 25"
            Case Is = tRegion.HockeyOffensiveHalf
                GetRegionString = "Offensive Half"
            Case Is = tRegion.HockeyDefensiveHalf
                GetRegionString = "Defensive Half"

            Case Is = tRegion.NetballAttackThird
                GetRegionString = "Attack 3rd"
            Case Is = tRegion.NetballMiddleThird
                GetRegionString = "Middle 3rd"
            Case Is = tRegion.NetballDefensiveThird
                GetRegionString = "Defensive 3rd"
            Case Is = tRegion.NetballAttackCircle
                GetRegionString = "Circle Attack"
            Case Is = tRegion.NetballDefensiveCircle
                GetRegionString = "Circle Defense"

            Case Is = tRegion.Soccer_BackThird
                GetRegionString = "Back Third"
            Case Is = tRegion.Soccer_MiddleThird
                GetRegionString = "Middle Third"
            Case Is = tRegion.Soccer_FrontThird
                GetRegionString = "Front Third"
            Case Is = tRegion.Soccer_FrontCentre
                GetRegionString = "Front Centre"

            Case Is = tRegion.rgAFLBack50
                GetRegionString = "Defensive 50m"
            Case Is = tRegion.rgAFLCentreCorridor
                GetRegionString = "Centre Corridor"
            Case Is = tRegion.rgAFLDefensiveFlank
                GetRegionString = "Half Back Flank"
            Case Is = tRegion.rgAFLForward50
                GetRegionString = "Forward 50m"
            Case Is = tRegion.rgAFLForwardFlank
                GetRegionString = "Half Forward Flank"

            Case Is = tRegion.RugbyLeague_AttGoalArea
                GetRegionString = "Forward Goal Area"
            Case Is = tRegion.RugbyLeague_Att10ToGoal
                GetRegionString = "Forward 10 Meter Line to Goal"
            Case Is = tRegion.RugbyLeague_Att20To10
                GetRegionString = "Forward 20-10 Meter Line"
            Case Is = tRegion.RugbyLeague_Att30To20
                GetRegionString = "Forward 30-20 Meter Line"
            Case Is = tRegion.RugbyLeague_Att40To30
                GetRegionString = "Forward 40-30 Meter Line"
            Case Is = tRegion.RugbyLeague_Att50To40
                GetRegionString = "Forward Half-40 Meter Line"
            Case Is = tRegion.RugbyLeague_DefGoalArea
                GetRegionString = "Defensive Goal Area"
            Case Is = tRegion.RugbyLeague_Def10ToGoal
                GetRegionString = "Defensive 10 Meter Line to Goal"
            Case Is = tRegion.RugbyLeague_Def20To10
                GetRegionString = "Defensive 20-10 Meter Line"
            Case Is = tRegion.RugbyLeague_Def30To20
                GetRegionString = "Defensive 30-20 Meter Line"
            Case Is = tRegion.RugbyLeague_Def40To30
                GetRegionString = "Defensive 40-30 Meter Line"
            Case Is = tRegion.RugbyLeague_Def50To40
                GetRegionString = "Defensive Half-40 Meter Line"

            Case Is = tRegion.Basketball_AttackCircle
                GetRegionString = "Attacking 3 Point Arc"
            Case Is = tRegion.Basketball_AttackCourt
                GetRegionString = "Attacking Court"
            Case Is = tRegion.Basketball_DefensiveCircle
                GetRegionString = "Defensive 3 Point Arc"
            Case Is = tRegion.Basketball_DefensiveCourt
                GetRegionString = "Defensive Court"

            Case Is = tRegion.Rugby7_Attack_InGoal
                GetRegionString = "Attacking In Goal Area"
            Case Is = tRegion.Rugby7_Attack_22
                GetRegionString = "Attacking 22 Meters"
            Case Is = tRegion.Rugby7_Attack_Half
                GetRegionString = "Attacking Half"
            Case Is = tRegion.Rugby7_Defensive_Half
                GetRegionString = "Defensive Half"
            Case Is = tRegion.Rugby7_Defensive_22
                GetRegionString = "Defensive 22 Meters"
            Case Is = tRegion.Rugby7_Defensive_InGoal
                GetRegionString = "Defensive In Goal Area"

            Case Else
                GetRegionString = ""
        End Select

    End Function

    Public Function GetTitle(ByVal szFilename As String) As String

        If Len(szFilename) = 0 Then Return Nothing : Exit Function

        'Remove extension
        Dim szTemp As String = szFilename.Remove(Len(szFilename) - 4, 4)

        For i As Integer = Len(szTemp) To 0 Step -1
            If Microsoft.VisualBasic.Mid(szTemp, i, 1) = "\" Then
                szTemp = szTemp.Remove(0, i)
                Exit For
            End If
        Next
        Return szTemp

    End Function

    Public Function GetMaxClusterArrayValues(ByVal nArray(,) As Integer) As MaxMinValues
        Dim n As MaxMinValues = Nothing
        If Not nArray Is Nothing Then
            'Return the highest and lowest frequency counts for a cluster chart
            n.nMax = 0
            n.nMin = 32000
            For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
                For y As Integer = 0 To UserPrefs.clVerticalQ - 1
                    If nArray(x, y) > n.nMax Then n.nMax = nArray(x, y)
                    If nArray(x, y) < n.nMin Then n.nMin = nArray(x, y)
                Next
            Next
        End If

        Return n

    End Function

    Public Function GetMaxClusterArrayValues2(ByVal nArray(,) As Single) As MaxMinValues
        Dim n As MaxMinValues = Nothing
        If Not nArray Is Nothing Then
            'Return the highest and lowest frequency counts for a cluster chart
            n.nMax = 0
            n.nMin = 32000
            For x As Integer = 0 To nArray.GetUpperBound(0) - 1
                For y As Integer = 0 To nArray.GetUpperBound(1) - 1
                    If nArray(x, y) > n.nMax Then n.nMax = nArray(x, y)
                    If nArray(x, y) < n.nMin Then n.nMin = nArray(x, y)
                Next
            Next
        End If

        Return n

    End Function

    Public Function GetColorGradient(ByVal StartColor As Color, ByVal EndColor As Color, ByVal index As Single, ByVal total As Single, _
    Optional ByVal Alpha As Integer = 255) As Color

        On Error GoTo errCatch

        Dim x1, x2, x As Single
        x1 = 0
        x2 = total
        x = index

        'Solve for colors
        Dim kRed As Int16 = (((x2 - x) / (x2 - x1)) * StartColor.R) + (((x - x1) / (x2 - x1)) * EndColor.R)
        Dim kBlue As Int16 = (((x2 - x) / (x2 - x1)) * StartColor.B) + (((x - x1) / (x2 - x1)) * EndColor.B)
        Dim kGreen As Int16 = (((x2 - x) / (x2 - x1)) * StartColor.G) + (((x - x1) / (x2 - x1)) * EndColor.G)

        Return Color.FromArgb(Alpha, kRed, kGreen, kBlue)
        Exit Function

errCatch:
        Err.Clear()
        Return StartColor

    End Function

    Public Function GetDistanceBetweenPoints(ByVal PointA As PointF, ByVal pointB As PointF) As Single
        Dim Distance As Single = 0
        Select Case UserPrefs.Sport
            Case tSports.sHockey
                'NB: 1 point = 0.666 yards
                'Convert feet to meters:: 1 foot = 0.3048 meters
                Dim a As Single = (PointA.X - pointB.X) ^ 2
                Dim b As Single = (PointA.Y - pointB.Y) ^ 2
                Distance = Math.Sqrt(a + b) * 0.3048
            Case tSports.sNetball
                Dim a As Single = (PointA.X - pointB.X) ^ 2
                Dim b As Single = (PointA.Y - pointB.Y) ^ 2
                Distance = Math.Sqrt(a + b) * 1
            Case tSports.sRugbyLeague
                Dim a As Single = (PointA.X - pointB.X) ^ 2
                Dim b As Single = (PointA.Y - pointB.Y) ^ 2
                Distance = Math.Sqrt(a + b) * 1
            Case tSports.sBasketball
                Dim a As Single = (PointA.X - pointB.X) ^ 2
                Dim b As Single = (PointA.Y - pointB.Y) ^ 2
                Distance = Math.Sqrt(a + b) * 0.3048
            Case Else
                Dim a As Single = (PointA.X - pointB.X) ^ 2
                Dim b As Single = (PointA.Y - pointB.Y) ^ 2
                Distance = Math.Sqrt(a + b)

        End Select

        Return Distance
    End Function

    Public Sub DrawPitch(ByVal Sport As tSports, ByVal e As Graphics, ByVal Dimensions As RectangleF, _
            Optional ByVal Smoothing As Drawing.Drawing2D.SmoothingMode = Drawing2D.SmoothingMode.HighQuality, _
            Optional ByVal FieldColor As KnownColor = Nothing, Optional ByVal HighlightColor As KnownColor = Nothing, _
            Optional ByVal LineColor As KnownColor = Nothing, Optional ByVal PerimeterColor As KnownColor = Nothing)

        If FieldColor = Nothing Then FieldColor = KnownColor.WhiteSmoke
        If HighlightColor = Nothing Then HighlightColor = KnownColor.AntiqueWhite
        If LineColor = Nothing Then LineColor = KnownColor.Black
        If PerimeterColor = Nothing Then PerimeterColor = KnownColor.Gainsboro

        'Set colors
        Dim cPen As New Pen(Color.FromKnownColor(LineColor), 2)
        Dim bField As New SolidBrush(Color.FromKnownColor(FieldColor))
        Dim bHighlight As New SolidBrush(Color.FromKnownColor(HighlightColor))

        'Set window size parameters
        Dim zX As Single = 0
        Dim zY As Single = 0

        e.SmoothingMode = Smoothing
        With Dimensions
            Select Case Sport
                Case Is = tSports.sHockey

                    '*
                    '*  Print hockey field
                    '*

                    '
                    'Ground dimensions
                    zX = .Width / 90
                    zY = .Height / 150

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Draw goals
                    'Attack
                    e.FillRectangle(Brushes.Gray, (.Left + (.Width / 2)) - (3 * zX), .Top - (4 * zY), (6 * zX), (4 * zY))
                    e.DrawRectangle(Pens.DarkGray, (.Left + (.Width / 2)) - (3 * zX), .Top - (4 * zY), (6 * zX), (4 * zY))
                    'Defence
                    e.FillRectangle(Brushes.Gray, (.Left + (.Width / 2)) - (3 * zX), .Top + .Height, (6 * zX), (4 * zY))
                    e.DrawRectangle(Pens.DarkGray, (.Left + (.Width / 2)) - (3 * zX), .Top + .Height, (6 * zX), (4 * zY))

                    'Circles
                    'Attack
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (21 * zX), .Top - (24 * zY), 48 * zX, 48 * zY, 0, 90)
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (27 * zX), .Top - (24 * zY), 48 * zX, 48 * zY, 180, -90)
                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (3 * zX), .Top, (7 * zX), (24 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (21 * zX), .Top - (24 * zY), 48 * zX, 48 * zY, 0, 90)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (27 * zX), .Top - (24 * zY), 48 * zX, 48 * zY, 180, -90)
                    e.DrawLine(cPen, (.Left + (.Width / 2)) - (3 * zX), .Top + (24 * zY), (.Left + (.Width / 2)) + (3 * zX), .Top + (24 * zY))
                    'Defence
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (21 * zX), .Top + .Height - (24 * zY), 48 * zX, 48 * zY, 0, -90)
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (27 * zX), .Top + .Height - (24 * zY), 48 * zX, 48 * zY, 180, 90)
                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (3 * zX), .Top + .Height - (24 * zY), (7 * zX), (24 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (21 * zX), .Top + .Height - (24 * zY), 48 * zX, 48 * zY, 0, -90)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (27 * zX), .Top + .Height - (24 * zY), 48 * zX, 48 * zY, 180, 90)
                    e.DrawLine(cPen, (.Left + (.Width / 2)) - (3 * zX), .Top + .Height - (24 * zY), (.Left + (.Width / 2)) + (3 * zX), .Top + .Height - (24 * zY))

                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)

                    'Mid lines
                    e.DrawLine(cPen, .Left, .Top + (.Height / 2) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 2) - (cPen.Width / 2))   'Half

                    Dim s() As Single = {(2 * zX), (2 * zX)}
                    cPen.DashStyle = Drawing2D.DashStyle.Custom
                    cPen.DashPattern = s

                    e.DrawLine(cPen, .Left, .Top + (.Height / 4), .Left + .Width, .Top + (.Height / 4))   'Att 25
                    e.DrawLine(cPen, .Left, .Top + ((.Height / 4) * 3), .Left + .Width, .Top + ((.Height / 4) * 3))   'Att 25

                Case Is = tSports.sNetball

                    '*
                    '*  Print netball court
                    '*

                    '
                    'Court dimensions
                    zX = .Width / 90
                    zY = .Height / 180

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Draw goals
                    'Attack

                    'Defence

                    'Circles
                    'Attack
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (29 * zX), .Top - (29 * zY), 58 * zX, 58 * zY, 0, 180)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (29 * zX), .Top - (29 * zY), 58 * zX, 58 * zY, 0, 180)
                    'Defence
                    e.FillPie(bHighlight, .Left + (.Width / 2) - (29 * zX), .Top + .Height - (29 * zY), 58 * zX, 58 * zY, -180, 180)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (29 * zX), .Top + .Height - (29 * zY), 58 * zX, 58 * zY, -180, 180)

                    'Centre circle
                    e.DrawArc(cPen, .Left + (.Width / 2) - (4 * zX), .Top + (.Height / 2) - (4 * zY), 8 * zY, 8 * zY, 0, 360)

                    'Goal circle
                    e.DrawArc(cPen, .Left + (.Width / 2) - (2 * zX), .Top + (0 * zY), 4 * zY, 4 * zY, 0, 360)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (2 * zX), .Top + .Height - (4 * zY), 4 * zY, 4 * zY, 0, 360)

                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)

                    'Mid lines
                    e.DrawLine(cPen, .Left, .Top + (.Height / 3) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 3) - (cPen.Width / 2))   '3rd

                    e.DrawLine(cPen, .Left, .Bottom - (.Height / 3) - (cPen.Width / 2), _
                    .Left + .Width, .Bottom - (.Height / 3) - (cPen.Width / 2))   '3rd


                Case Is = tSports.sRugbyLeague

                    '*
                    '*  Print league field
                    '*

                    '
                    'Ground dimensions
                    zX = .Width / 68
                    zY = .Height / 122

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)

                    'Mid lines
                    e.DrawLine(cPen, .Left, .Top + (.Height / 2) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 2) - (cPen.Width / 2))   'Half

                    'Dim s() As Single = {(2 * zX), (2 * zX)}
                    'cPen.DashStyle = Drawing2D.DashStyle.Custom
                    'cPen.DashPattern = s

                    '10m lines
                    cPen.Width = 1
                    Dim wedge As Single = .Height / 60
                    Dim f As Font = New Font("Arial", .Height / 40, FontStyle.Bold)

                    For i As Integer = 2 To 10
                        If i = 5 Or i = 7 Then
                            cPen.Brush = Brushes.Red
                        End If
                        e.DrawLine(cPen, .Left, .Top + (.Height / 12) * i, .Left + .Width, .Top + (.Height / 12) * i)

                        'Line markers
                        cPen.Brush = New SolidBrush(Color.FromKnownColor(LineColor))
                        e.DrawLine(cPen, .Left + (.Width / 10) * 2, .Top + ((.Height / 12) * i) - wedge, .Left + (.Width / 10) * 2, .Top + ((.Height / 12) * i) + wedge)
                        e.DrawLine(cPen, .Left + (.Width / 10) * 3, .Top + ((.Height / 12) * i) - wedge, .Left + (.Width / 10) * 3, .Top + ((.Height / 12) * i) + wedge)
                        e.DrawLine(cPen, .Left + (.Width / 10) * 7, .Top + ((.Height / 12) * i) - wedge, .Left + (.Width / 10) * 7, .Top + ((.Height / 12) * i) + wedge)
                        e.DrawLine(cPen, .Left + (.Width / 10) * 8, .Top + ((.Height / 12) * i) - wedge, .Left + (.Width / 10) * 8, .Top + ((.Height / 12) * i) + wedge)

                        If i <= 6 Then
                            e.DrawString((i - 1) * 10.ToString, f, cPen.Brush, .Left + (.Width / 10) * 2, .Top + ((.Height / 12) * i) - wedge)
                        Else
                            e.DrawString((11 - i) * 10.ToString, f, cPen.Brush, .Left + (.Width / 10) * 2, .Top + ((.Height / 12) * i) - wedge)
                        End If

                    Next

                    'Try lines
                    cPen.Width = 4
                    e.DrawLine(cPen, .Left, .Top + (.Height / 12) * 1, .Left + .Width, .Top + (.Height / 12) * 1)   'Att 25
                    e.DrawLine(cPen, .Left, .Top + (.Height / 12) * 11, .Left + .Width, .Top + (.Height / 12) * 11)   'Att 25

                    'Goals
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) - (.Width / 20), ((.Height / 12) + .Top) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) + (.Width / 20), ((.Height / 12) + .Top) - 3, 6, 6)

                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) - (.Width / 20), (.Bottom - (.Height / 12)) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) + (.Width / 20), (.Bottom - (.Height / 12)) - 3, 6, 6)

                    'Corner posts
                    e.FillRectangle(Brushes.DarkGray, .Left - 3, ((.Height / 12) + .Top) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Right - 3, ((.Height / 12) + .Top) - 3, 6, 6)

                    e.FillRectangle(Brushes.DarkGray, .Left - 3, (.Bottom - (.Height / 12)) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Right - 3, (.Bottom - (.Height / 12)) - 3, 6, 6)


                Case Is = tSports.sRugby7

                    '*
                    '*  Print 7's field
                    '*

                    '
                    'Ground dimensions
                    zX = .Width / 70
                    zY = .Height / 120

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)

                    'Mid lines
                    e.DrawLine(cPen, .Left, .Top + (.Height / 2) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 2) - (cPen.Width / 2))   'Half

                    '22m lines
                    cPen.Width = 1
                    e.DrawLine(cPen, .Left, .Top + (32 * zY), .Left + .Width, .Top + (32 * zY))   'Attack
                    e.DrawLine(cPen, .Left, .Bottom - (32 * zY), .Left + .Width, .Bottom - (32 * zY))   'Attack

                    e.DrawLine(cPen, .Left + (15 * zX), .Top + (27 * zY), .Left + (15 * zX), .Top + (37 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Top + (27 * zY), .Right - (15 * zX), .Top + (37 * zY))
                    e.DrawLine(cPen, .Left + (15 * zX), .Bottom - (27 * zY), .Left + (15 * zX), .Bottom - (37 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Bottom - (27 * zY), .Right - (15 * zX), .Bottom - (37 * zY))

                    '10m lines
                    e.DrawLine(cPen, .Left, .Top + (50 * zY), .Left + .Width, .Top + (50 * zY))   'Attack
                    e.DrawLine(cPen, .Left, .Bottom - (50 * zY), .Left + .Width, .Bottom - (50 * zY))   'Attack

                    e.DrawLine(cPen, .Left + (15 * zX), .Top + (45 * zY), .Left + (15 * zX), .Top + (55 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Top + (45 * zY), .Right - (15 * zX), .Top + (55 * zY))
                    e.DrawLine(cPen, .Left + (15 * zX), .Bottom - (45 * zY), .Left + (15 * zX), .Bottom - (55 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Bottom - (45 * zY), .Right - (15 * zX), .Bottom - (55 * zY))

                    e.DrawLine(cPen, .Left + (15 * zX), .Top + (10 * zY), .Left + (15 * zX), .Top + (15 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Top + (10 * zY), .Right - (15 * zX), .Top + (15 * zY))
                    e.DrawLine(cPen, .Left + (15 * zX), .Bottom - (10 * zY), .Left + (15 * zX), .Bottom - (15 * zY))
                    e.DrawLine(cPen, .Right - (15 * zX), .Bottom - (10 * zY), .Right - (15 * zX), .Bottom - (15 * zY))


                    'Try lines
                    cPen.Width = 4
                    e.DrawLine(cPen, .Left, .Top + (.Height / 12) * 1, .Left + .Width, .Top + (.Height / 12) * 1)
                    e.DrawLine(cPen, .Left, .Top + (.Height / 12) * 11, .Left + .Width, .Top + (.Height / 12) * 11)

                    'Goals
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) - (.Width / 20), ((.Height / 12) + .Top) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) + (.Width / 20), ((.Height / 12) + .Top) - 3, 6, 6)

                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) - (.Width / 20), (.Bottom - (.Height / 12)) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Left + (.Width / 2) + (.Width / 20), (.Bottom - (.Height / 12)) - 3, 6, 6)

                    'Corner posts
                    e.FillRectangle(Brushes.DarkGray, .Left - 3, ((.Height / 12) + .Top) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Right - 3, ((.Height / 12) + .Top) - 3, 6, 6)

                    e.FillRectangle(Brushes.DarkGray, .Left - 3, (.Bottom - (.Height / 12)) - 3, 6, 6)
                    e.FillRectangle(Brushes.DarkGray, .Right - 3, (.Bottom - (.Height / 12)) - 3, 6, 6)



                Case Is = tSports.sSoccer
                    '*
                    '*  Print soccer pitch
                    '*

                    'Ground dimensions
                    zX = .Width / 95
                    zY = .Height / 150

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Draw goals
                    'Attack
                    e.FillRectangle(Brushes.Gray, (.Left + (.Width / 2)) - (5 * zX), .Top - (4 * zY), (10 * zX), (4 * zY))
                    e.DrawRectangle(Pens.DarkGray, (.Left + (.Width / 2)) - (5 * zX), .Top - (4 * zY), (10 * zX), (4 * zY))
                    'Defence
                    e.FillRectangle(Brushes.Gray, (.Left + (.Width / 2)) - (5 * zX), .Top + .Height, (10 * zX), (4 * zY))
                    e.DrawRectangle(Pens.DarkGray, (.Left + (.Width / 2)) - (5 * zX), .Top + .Height, (10 * zX), (4 * zY))

                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)

                    'Penalty boxes
                    e.DrawArc(cPen, .Left + (.Width / 2) - (14 * zY), .Top + (5 * zY), (28 * zY), (28 * zY), 10, 160)
                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (25 * zX), .Top, (50 * zX), (25 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (25 * zX), .Top, (50 * zX), (25 * zY))
                    e.FillRectangle(bField, (.Left + (.Width / 2)) - (5 * zX), .Top, (10 * zX), (6 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (5 * zX), .Top, (10 * zX), (6 * zY))

                    e.DrawArc(cPen, .Left + (.Width / 2) - (14 * zY), .Bottom - (33 * zY), (28 * zY), (28 * zY), 190, 160)
                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (25 * zX), .Bottom - (25 * zY), (50 * zX), (25 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (25 * zX), .Bottom - (25 * zY), (50 * zX), (25 * zY))
                    e.FillRectangle(bField, (.Left + (.Width / 2)) - (5 * zX), .Bottom - (6 * zY), (10 * zX), (6 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (5 * zX), .Bottom - (6 * zY), (10 * zX), (6 * zY))

                    'Half  
                    e.DrawLine(cPen, .Left, .Top + (.Height / 2) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 2) - (cPen.Width / 2))
                    'Centre circle
                    e.FillEllipse(bHighlight, .Left + (.Width / 2) - (14 * zY), .Top + (.Height / 2) - (14 * zY), (28 * zY), (28 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (14 * zY), .Top + (.Height / 2) - (14 * zY), 28 * zY, 28 * zY, 0, 360)

                Case Is = tSports.sBasketball
                    '*
                    '*  Print basketball court
                    '*

                    'Ground dimensions
                    zX = .Width / 50
                    zY = .Height / 94

                    'Set perimeter - background
                    e.FillRectangle(New SolidBrush(Color.FromKnownColor(PerimeterColor)), e.ClipBounds)

                    'Fill ground color
                    e.FillRectangle(bField, .X, .Y, .Width, .Height)

                    'Half  
                    e.DrawLine(cPen, .Left, .Top + (.Height / 2) - (cPen.Width / 2), _
                        .Left + .Width, .Top + (.Height / 2) - (cPen.Width / 2))
                    'Centre circle
                    e.FillEllipse(bHighlight, .Left + (.Width / 2) - (6 * zY), .Top + (.Height / 2) - (6 * zY), (12 * zY), (12 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (6 * zY), .Top + (.Height / 2) - (6 * zY), 12 * zY, 12 * zY, 0, 360)

                    'key areas
                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (6 * zX), .Top, (12 * zX), (19 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (6 * zX), .Top, (12 * zX), (19 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (6 * zX), .Top + (19 * zY) - (6 * zY), 12 * zX, 12 * zY, 0, 360)

                    e.FillRectangle(bHighlight, (.Left + (.Width / 2)) - (6 * zX), .Bottom - (19 * zY), (12 * zX), (19 * zY))
                    e.DrawRectangle(cPen, (.Left + (.Width / 2)) - (6 * zX), .Bottom - (19 * zY), (12 * zX), (19 * zY))
                    e.DrawArc(cPen, .Left + (.Width / 2) - (6 * zX), .Bottom - (19 * zY) - (6 * zY), 12 * zX, 12 * zY, 0, 360)

                    'Hoop
                    e.DrawArc(New Pen(Color.LightGray, 2), .Left + (.Width / 2) - (1 * zY), .Top + (3 * zY), 2 * zY, 2 * zY, 0, 360)
                    e.DrawLine(New Pen(Color.LightGray, 2), .Left + (.Width / 2) - (3 * zY), .Top + (3 * zY), .Left + (.Width / 2) + (3 * zY), .Top + (3 * zY))

                    e.DrawArc(New Pen(Color.LightGray, 2), .Left + (.Width / 2) - (1 * zY), .Bottom - (5 * zY), 2 * zY, 2 * zY, 0, 360)
                    e.DrawLine(New Pen(Color.LightGray, 2), .Left + (.Width / 2) - (3 * zY), .Bottom - (3 * zY), .Left + (.Width / 2) + (3 * zY), .Bottom - (3 * zY))

                    '3M arc
                    e.DrawLine(cPen, .Left + (.Width / 2) - (19 * zX), .Top + (6 * zY), .Left + (.Width / 2) - (19 * zX), .Top)
                    e.DrawLine(cPen, .Left + (.Width / 2) + (19 * zX), .Top + (6 * zY), .Left + (.Width / 2) + (19 * zX), .Top)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (19 * zX), .Top - (13 * zY), 38 * zX, 38 * zY, -1, 182)

                    e.DrawLine(cPen, .Left + (.Width / 2) - (19 * zX), .Bottom - (6 * zY), .Left + (.Width / 2) - (19 * zX), .Bottom)
                    e.DrawLine(cPen, .Left + (.Width / 2) + (19 * zX), .Bottom - (6 * zY), .Left + (.Width / 2) + (19 * zX), .Bottom)
                    e.DrawArc(cPen, .Left + (.Width / 2) - (19 * zX), .Bottom - (25 * zY), 38 * zX, 38 * zY, 179, 182)



                    'Draw boundary lines
                    e.DrawRectangle(cPen, .Left, .Top, .Width, .Height)


            End Select
        End With

    End Sub

    Public Sub RedrawOutline(ByVal Sport As tSports, ByVal e As Graphics, ByVal Rect As RectangleF, _
    Optional ByVal Smooth As Drawing2D.SmoothingMode = Drawing2D.SmoothingMode.HighQuality)

        'Set window size parameters
        Dim zX As Single = 0
        Dim zY As Single = 0

        e.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Select Case Sport
            Case Is = tSports.sHockey
                '*
                '*  Print hockey field
                '*

                'Ground dimensions
                zX = Rect.Width / 90
                zY = Rect.Height / 150

                'Set colors
                Dim cPen As New Pen(Color.Black, 2)

                'Set perimeter - background

                'Draw goals
                'Attack
                e.DrawRectangle(Pens.DarkGray, (Rect.Left + (Rect.Width / 2)) - (3 * zX), Rect.Top - (4 * zY), (6 * zX), (4 * zY))
                'Defence
                e.DrawRectangle(Pens.DarkGray, (Rect.Left + (Rect.Width / 2)) - (3 * zX), Rect.Top + Rect.Height, (6 * zX), (4 * zY))

                'Circles
                'Attack
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (21 * zX), Rect.Top - (24 * zY), 48 * zX, 48 * zY, 0, 90)
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (27 * zX), Rect.Top - (24 * zY), 48 * zX, 48 * zY, 180, -90)
                e.DrawLine(cPen, (Rect.Left + (Rect.Width / 2)) - (3 * zX), Rect.Top + (24 * zY), (Rect.Left + (Rect.Width / 2)) + (3 * zX), Rect.Top + (24 * zY))
                'Defence
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (21 * zX), Rect.Top + Rect.Height - (24 * zY), 48 * zX, 48 * zY, 0, -90)
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (27 * zX), Rect.Top + Rect.Height - (24 * zY), 48 * zX, 48 * zY, 180, 90)
                e.DrawLine(cPen, (Rect.Left + (Rect.Width / 2)) - (3 * zX), Rect.Top + Rect.Height - (24 * zY), (Rect.Left + (Rect.Width / 2)) + (3 * zX), Rect.Top + Rect.Height - (24 * zY))

                'Draw boundary lines
                e.DrawRectangle(cPen, Rect.Left, Rect.Top, Rect.Width, Rect.Height)

                'Mid lines
                e.DrawLine(cPen, Rect.Left, Rect.Top + (Rect.Height / 2) - (cPen.Width / 2), _
                    Rect.Left + Rect.Width, Rect.Top + (Rect.Height / 2) - (cPen.Width / 2))   'Half

                Dim s() As Single = {(2 * zX), (2 * zX)}
                cPen.DashStyle = Drawing2D.DashStyle.Custom
                cPen.DashPattern = s

                e.DrawLine(cPen, Rect.Left, Rect.Top + (Rect.Height / 4), Rect.Left + Rect.Width, Rect.Top + (Rect.Height / 4))   'Att 25
                e.DrawLine(cPen, Rect.Left, Rect.Top + ((Rect.Height / 4) * 3), Rect.Left + Rect.Width, Rect.Top + ((Rect.Height / 4) * 3))   'Att 25

            Case Is = tSports.sNetball

                '*
                '*  Print netball court
                '*

                '
                'Court dimensions
                zX = Rect.Width / 90
                zY = Rect.Height / 180

                'Set colors
                Dim cPen As New Pen(Color.Black, 2)

                'Draw goals
                'Attack

                'Defence

                'Circles
                'Attack
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (29 * zX), Rect.Top - (29 * zY), 58 * zX, 58 * zY, 0, 180)
                'Defence
                e.DrawArc(cPen, Rect.Left + (Rect.Width / 2) - (29 * zX), Rect.Top + Rect.Height - (29 * zY), 58 * zX, 58 * zY, -180, 180)

                'Draw boundary lines
                e.DrawRectangle(cPen, Rect.Left, Rect.Top, Rect.Width, Rect.Height)

                'Mid lines
                e.DrawLine(cPen, Rect.Left, Rect.Top + (Rect.Height / 3) - (cPen.Width / 2), _
                    Rect.Left + Rect.Width, Rect.Top + (Rect.Height / 3) - (cPen.Width / 2))   '3rd

                e.DrawLine(cPen, Rect.Left, Rect.Bottom - (Rect.Height / 3) - (cPen.Width / 2), _
                Rect.Left + Rect.Width, Rect.Bottom - (Rect.Height / 3) - (cPen.Width / 2))   '3rd

            Case Is = tSports.sRugbyLeague




        End Select


    End Sub

    Public Function StripFileName(ByVal szFileName As String) As String
        If szFileName Is Nothing Then Return "Empty FileName"
        Dim szRet As String = Nothing
        For Each bit As Char In szFileName
            If Not bit.ToString = "," And Not bit.ToString = ":" Then
                szRet = szRet & bit
            End If
        Next
        Return szRet

    End Function

    'Private Sub PrintSingle(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDocSingle.PrintPage

    '    Dim Quadrant(3) As Rectangle

    '    'Top left quadrant
    '    Quadrant(0) = e.MarginBounds
    '    Quadrant(0).Width \= 2
    '    Quadrant(0).Height \= 2

    '    'Top right quadrant
    '    Quadrant(1) = e.MarginBounds
    '    Quadrant(1).Width \= 2
    '    Quadrant(1).Height \= 2
    '    Quadrant(1).X = Quadrant(0).Right

    '    'Bottom left quadrant
    '    Quadrant(2) = e.MarginBounds
    '    Quadrant(2).Width \= 2
    '    Quadrant(2).Height \= 2
    '    Quadrant(2).Y = Quadrant(0).Bottom

    '    'Top right quadrant
    '    Quadrant(3) = e.MarginBounds
    '    Quadrant(3).Width \= 2
    '    Quadrant(3).Height \= 2
    '    Quadrant(3).X = Quadrant(0).Right
    '    Quadrant(3).Y = Quadrant(0).Bottom

    '    For Each r As Rectangle In Quadrant
    '        e.Graphics.DrawRectangle(Pens.Gray, r)
    '    Next

    '    Dim Scale As RectangleF = e.MarginBounds
    '    Scale.Width \= 2
    '    Scale.Height \= 2


    '    'Set window size parameters
    '    Rect = New RectangleF(Scale.Width / 10, Scale.Height / 10, Scale.Width / 1.25, Scale.Height / 1.25)

    '    'Draw Base Pitch
    '    DrawPitch(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality, , , KnownColor.Black, KnownColor.Transparent)

    '    Select Case UserPrefs.Sport
    '        Case tSports.sHockey
    '            zX = (Scale.Width / 1.25) / 90
    '            zY = (Scale.Height / 1.25) / 150

    '        Case tSports.sNetball
    '            zX = (Scale.Width / 1.25) / 90
    '            zY = (Scale.Height / 1.25) / 180

    '        Case tSports.sRugbyLeague
    '            zX = (Scale.Width / 1.25) / 68
    '            zY = (Scale.Height / 1.25) / 122

    '        Case tSports.sBasketball
    '            zX = (Scale.Width / 1.25) / 50
    '            zY = (Scale.Height / 1.25) / 94

    '        Case tSports.sSoccer
    '            zX = (Scale.Width / 1.25) / 95
    '            zY = (Scale.Height / 1.25) / 150

    '    End Select

    '    'Draw Plays
    '    Select Case mvarChartType
    '        Case Chart.ctEventHeatMaps
    '            Me.grpBoxOptions.Visible = False
    '            'Dim maxCount As MaxMinValues = GetMaxClusterArrayValues2(HeatArray)

    '            If Not HeatArray Is Nothing Then
    '                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '                e.Graphics.ScaleTransform(zX, zY)
    '                For x As Integer = 0 To HeatArray.GetUpperBound(0) - 1
    '                    For y As Integer = 0 To HeatArray.GetUpperBound(1) - 1
    '                        Dim ncolor As Color = ClusterColorArray(x, y)
    '                        e.Graphics.FillRegion(New SolidBrush(ncolor), New Region(New RectangleF(x + PitchOffset.X, y + PitchOffset.Y, 1, 1)))
    '                    Next
    '                Next
    '            End If

    '        Case Chart.ctScatterPlots
    '            Me.grpBoxOptions.Visible = False

    '            If Not Me.ScatterPlotPoints Is Nothing Then

    '                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '                e.Graphics.ScaleTransform(zX, zY)
    '                For Each ScatterPoint As ScatterInfo In ScatterPlotPoints
    '                    'Cluster data
    '                    e.Graphics.FillEllipse(New SolidBrush(ScatterPoint.TeamColor), New RectangleF(ScatterPoint.Location, New System.Drawing.SizeF(2, 2)))
    '                Next
    '            End If


    '        Case Chart.ctPlayerMaps
    '            Me.grpBoxOptions.Visible = True
    '            '*
    '            '* Draw player maps
    '            '*
    '            If Plays.Count > 0 Then
    '                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '                e.Graphics.ScaleTransform(zX, zY)
    '                Dim o As Integer = Me.numPathOpacity.Value
    '                Dim k As Integer = 0
    '                Dim nColor As Color
    '                For Each gp As GamePlay.Instance In Plays
    '                    k += 1
    '                    nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
    '                    gp.Pen.Brush = New SolidBrush(Color.FromArgb(20, nColor))
    '                    gp.Pen.Width = UserPrefs.pmLineWidth / zX

    '                    If gp.Lead Then
    '                        If Not Me.chkShowReceives.Checked Then
    '                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
    '                        Else
    '                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
    '                        End If

    '                        gp.Pen.DashStyle = Drawing2D.DashStyle.Dot
    '                        gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
    '                        gp.Pen.Width *= 1.5
    '                        If Not Me.chkShowPossession.Checked Then gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

    '                    ElseIf gp.Posession Then
    '                        If Not Me.chkShowReceives.Checked Then gp.Pen.StartCap = Drawing2D.LineCap.RoundAnchor
    '                        If Not Me.chkShowPossession.Checked Then
    '                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
    '                        Else
    '                            gp.Pen.Brush = New SolidBrush(nColor)
    '                        End If
    '                        gp.Pen.DashStyle = Drawing2D.DashStyle.Solid
    '                        gp.Pen.Width *= 1.5
    '                        gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor

    '                    ElseIf gp.Lag Then
    '                        If Not Me.chkShowDeliveries.Checked Then
    '                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
    '                        Else
    '                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
    '                        End If
    '                        gp.Pen.DashStyle = Drawing2D.DashStyle.DashDotDot
    '                        gp.Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
    '                        gp.Pen.Width *= 1.5
    '                    Else
    '                        If Not Me.chShowOtherPlay.Checked Then
    '                            gp.Pen.Brush = New SolidBrush(Color.Transparent)
    '                        Else
    '                            gp.Pen.Brush = New SolidBrush(Color.FromArgb(o, nColor))
    '                            gp.Pen.Width *= 0.5
    '                        End If
    '                    End If

    '                    If gp.Path.PointCount > 0 Then
    '                        'If not in show captions mode then highlight play under pointer.
    '                        If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
    '                        'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
    '                        If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

    '                        e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
    '                    End If
    '                Next
    '            End If

    '        Case Chart.ctPathways
    '            '*
    '            '* Draw pathways
    '            '*
    '            Me.grpBoxOptions.Visible = False

    '            If Plays.Count > 0 Then
    '                e.Graphics.ScaleTransform(zX, zY)
    '                Dim k As Integer = 0
    '                Dim nColor As Color
    '                For Each gp As GamePlay.Instance In Plays
    '                    k += 1
    '                    nColor = GetColorGradient(UserPrefs.pmStartColor, UserPrefs.pmEndColor, k, Plays.Count)
    '                    gp.Pen.Brush = New SolidBrush(nColor)
    '                    gp.Pen.Width = UserPrefs.pmLineWidth / zX

    '                    If gp.Path.PointCount > 0 Then
    '                        'If not in show captions mode then highlight play under pointer.
    '                        If Not ShowCaptions And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2
    '                        'If in show captions mode then only highlight the play under pointer when the mouse is down, and 
    '                        If ShowCaptions And MouseDownOnCaption And SelectedPathStart = gp.Path.PathPoints(0) Then gp.Pen.Width *= 2

    '                        e.Graphics.DrawCurve(gp.Pen, gp.Path.PathPoints, UserPrefs.pmLineTension)
    '                    End If
    '                Next
    '            End If

    '            If Captions.Count > 0 Then
    '                e.Graphics.ResetTransform()

    '                If Me.ShowCaptions Then
    '                    'Showing a single gameplay path, including the event captions.
    '                    Dim lastCaption As New GamePlay.CaptionBox
    '                    Dim n As Single = 0

    '                    For Each gc As GamePlay.CaptionBox In Captions
    '                        'Scale captions
    '                        gc.BoxSize.X *= zX
    '                        gc.BoxSize.Y *= zY
    '                        gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize

    '                        If gc.BoxSize.Location = lastCaption.BoxSize.Location Then
    '                            'Modify location of caption for viewing ease.
    '                            gc.BoxSize.Offset(0, lastCaption.BoxSize.Height + zY)
    '                            lastCaption.BoxSize.Height = gc.BoxSize.Bottom - lastCaption.BoxSize.Top
    '                        Else
    '                            lastCaption = gc
    '                        End If

    '                        If gc.Index = CurrentCaptionIndex Then
    '                            'This caption is selected.
    '                            If MouseDownOnCaption Then gc.BoxSize.Location = SelectedCaptionBox.Location
    '                            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightSalmon)), gc.BoxSize)
    '                            SelectedCaptionPathID = gc.ID
    '                        Else
    '                            'Not selected
    '                            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, Color.LightGreen)), gc.BoxSize)
    '                        End If

    '                        e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)
    '                        e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
    '                        New SolidBrush(Color.Black), gc.BoxSize.Location)

    '                        'Save current caption locations and sizes for mouse-driven movement.
    '                        If Not CurrentCaptionInfo Is Nothing Then
    '                            CurrentCaptionInfo(gc.Index).CaptionRect = gc.BoxSize
    '                        End If

    '                    Next

    '                Else
    '                    'Showing an information box with details about the play.
    '                    For Each gc As GamePlay.CaptionBox In Captions
    '                        Try
    '                            gc.BoxSize.Size = e.Graphics.MeasureString(gc.Text, New Font(Me.Font, gc.FontStyle)).ToSize
    '                            gc.BoxSize.X = gc.BoxSize.Left - gc.BoxSize.Width
    '                            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(200, gc.BackColor)), gc.BoxSize)
    '                            e.Graphics.DrawRectangle(Pens.Black, gc.BoxSize)

    '                            e.Graphics.DrawString(gc.Text, New Font(Me.Font, gc.FontStyle), _
    '                            New SolidBrush(gc.ForeColor), gc.BoxSize.Location)

    '                        Catch ex As Exception

    '                        End Try
    '                    Next
    '                End If

    '            End If

    '        Case Chart.ctClusters
    '            '*
    '            '* Draw quadrants
    '            '*
    '            If Me.ClusterArray.GetUpperBound(0) = UserPrefs.clHorizontalQ - 1 And Me.ClusterArray.GetUpperBound(1) = UserPrefs.clVerticalQ - 1 Then


    '                Dim w As Single = Rect.Width / UserPrefs.clHorizontalQ
    '                Dim h As Single = Rect.Height / UserPrefs.clVerticalQ
    '                Dim nPen As New Pen(Color.Black, 1)
    '                Dim nFont As New Font(Me.Font, FontStyle.Bold)
    '                Me.grpBoxOptions.Visible = False

    '                'Draw colored squares
    '                If Not ClusterArray Is Nothing Then
    '                    For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
    '                        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
    '                            If Me.ClusterArray(x, y) > 0 Then
    '                                Dim x1 As Single = (x * w) + Rect.Left
    '                                Dim y1 As Single = (y * h) + Rect.Top
    '                                e.Graphics.FillRectangle(New SolidBrush(ClusterColorArray(x, y)), x1, y1, w, h)
    '                            End If
    '                        Next
    '                    Next
    '                End If

    '                'Overlay grid lines
    '                For hLine As Integer = 1 To UserPrefs.clHorizontalQ - 1
    '                    e.Graphics.DrawLine(nPen, (hLine * w) + Rect.Left, Rect.Top, (hLine * w) + Rect.Left, Rect.Top + Rect.Height)
    '                Next
    '                For vLine As Integer = 1 To UserPrefs.clVerticalQ - 1
    '                    e.Graphics.DrawLine(nPen, Rect.Left, (vLine * h) + Rect.Top, Rect.Left + Rect.Width, (vLine * h) + Rect.Top)
    '                Next

    '                'Overlay frequency values
    '                If Not ClusterArray Is Nothing Then
    '                    For x As Integer = 0 To UserPrefs.clHorizontalQ - 1
    '                        For y As Integer = 0 To UserPrefs.clVerticalQ - 1
    '                            Dim ptxt As PointF = e.Graphics.MeasureString(ClusterArray(x, y), Me.Font)
    '                            Dim p As PointF = New PointF(Rect.Left + (w * x) + (w / 2) - (ptxt.X / 2), Rect.Top + (h * y) + (h / 2) - (ptxt.Y / 2))
    '                            If Me.ClusterArray(x, y) > 0 Then
    '                                e.Graphics.DrawString(Me.ClusterArray(x, y).ToString, nFont, Brushes.Black, p)
    '                            End If
    '                        Next
    '                    Next
    '                End If
    '                'Finally, overlay pitch lines again.
    '                RedrawOutline(UserPrefs.Sport, e.Graphics, Rect, Drawing2D.SmoothingMode.HighQuality)
    '            End If

    '    End Select

    '    'Print details
    '    Dim leftEdge As Single = Rect.Left / zX
    '    Dim vertSpace As PointF = e.Graphics.MeasureString("Testing", New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel))
    '    e.Graphics.DrawString(Me.lblGameIDs.Text, New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Document), Brushes.Black, leftEdge, vertSpace.Y / zY)

    '    e.Graphics.DrawString("Team Names:", New Font("Arial", 12, FontStyle.Italic, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (1 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString(Me.lblTeamNames.Text, New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (2 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString("Time Criteria:", New Font("Arial", 12, FontStyle.Italic, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (3 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString(Me.lblTimeCriterion.Text, New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (4 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString("Outcome Types:", New Font("Arial", 12, FontStyle.Italic, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (5 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString(Me.lblOutcomes.Text, New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (6 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString("Included Event Names:", New Font("Arial", 12, FontStyle.Italic, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (7 * vertSpace.Y)) / zY)
    '    e.Graphics.DrawString(Me.lblDescriptors.Text, New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Document), Brushes.Black, leftEdge, (Rect.Bottom + (8 * vertSpace.Y)) / zY)


    'End Sub

    Public Sub PlayVideo(ByVal szFileName As String, ByVal nStart As Double, ByVal nStop As Double, Optional ByVal NewVideoSource As Boolean = True)

        If NewVideoSource Then
            If szFileName = "None" Then
                MsgBox("There is no video file attached to this data.", MsgBoxStyle.Exclamation, Application.ProductName)
                Exit Sub
            End If

            szFileName = FindMediaFile(szFileName)

            If Not My.Computer.FileSystem.FileExists(szFileName) Then
                MsgBox("The video file:" & vbNewLine & vbNewLine & szFileName & vbNewLine & vbNewLine & _
                "has not been found in any of the specified paths." & vbNewLine & vbNewLine & _
                "It is recommended that you locate the required video file, and either re-link this data to that video file," & vbNewLine & _
                "or specify the location of that video file using the 'Paths' tab in Options (F5).", MsgBoxStyle.Exclamation, Application.ProductName)
                Exit Sub
            End If

            If Not frmVideo.Visible Then
                frmVideo.MdiParent = frmMain
                frmVideo.Visible = False
                frmVideo.Show()
            End If
        End If

        If Not frmVideo.LoadVideoClip(szFileName, False, nStart, nStop, True) Then
            MsgBox("Cannot locate video files for this game data.", MsgBoxStyle.Exclamation, Application.ProductName)
        End If

        frmVideo.Panel2.Focus()
        frmVideo.Visible = True




    End Sub


End Module
