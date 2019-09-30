Module Module1
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
    '*
    '*  Pattern Plotter
    '*  Copyright 2007 Stuart Morgan
    '*  Australian Institute of Sport
    '*
    '*  Program History:
    '*
    '*  Version 1.1 - Inhouse pre-release version.
    '*  Verison 2.x - SIS/SAS Release 2004.
    '*  Verison 3.x - SIS/SAS Release 2006.
    '*
    '*
    '*  Revision History:
    '*
    '* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

    Public Const AppName = "Pattern Plotter 4"

    '
    ' Enumerations:
    '
    Public Enum tSports
        sHockey = 0
        sSoccer = 1
        sNetball = 2
        sAFL = 3
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
        dlgPlotting = 0
        dlgPostScript = 1
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
    Public Enum tRegion
        rgTotal = -1

        rgNone = 0

        'Hockey Regions
        rgTopCircle = 1
        rgBottomCircle = 2
        rgTopHalf = 3
        rgBottomHalf = 4
        rgTop25 = 5
        rgBottom25 = 6

        'Netball Regions
        rgAttackThird = 11
        rgMiddleThird = 12
        rgDefensiveThird = 13
        rgAttackCircle = 14
        rgDefensiveCircle = 15

        'Soccer regions
        rgSoccerBackThird = 24
        rgSoccerMiddleThird = 23
        rgSoccerFrontThird = 22
        rgSoccerFrontCentre = 21

        'AFL regions
        rgAFLForward50 = 31
        rgAFLForwardFlank = 32
        rgAFLCentreCorridor = 33
        rgAFLDefensiveFlank = 34
        rgAFLBack50 = 35

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
        Public OutcomeTime As Single
        Public TagColor As Color
        Public ThisOutcomeMatchesCriteria As Boolean    'Set to true in some search methods when a pre-search finds a match.
        Public OutcomeMatchTime As Single     'The time for start of first criteria-matching event.
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
    End Structure
    Public Structure PlotPoints
        Public Coordinates As Point
        Public GameInformation As GameProperties
        Public TimeCode As Single
        Public TimeCriteria As String
        Public EndTime As Single
        Public RecordID As Long
        Public Status As PathStatus
        Public BeginningOfPlayAtN As Integer
        Public TeamName As String
        Public Distance As Single
        Public Region As tRegion
        Public TagColor As Color
        Public TagFontColor As Color
        Public OutcomeCount As Integer
        Public MovementCount As Integer    'Number of ball movements leading to outcome
        Public PassCount As Integer            'Number of passes leading to outcome.
        Public OutcomeProp() As PathOutComes
        Public OutcomesExist As Boolean    'True when outcomes exist further along the path sequence.
        Public OutcomesExistAtN As Integer 'Index value n where outcomes start.  eg,. GamePath(n).OutcomesExistAtN = n+5
        Public OutcomesMatchCriteria As Boolean    'True if search finds matches embedded in this play.
        Public PlayHasMatch As Boolean 'True for start of play is a later point in the play has a search match.
        Public VideoFile As String
        Public VideoFile2 As String
        Public TeamIndex As Integer
        Public StartPath As Boolean
        Public CarryStart As Point
        Public CarryEnd As Point
        Public CarryStartTime As Single 'Time point that matches the PathStartX/Y event
        Public UserTimeInterval As Integer     'Value assigned by function during graph routine.  Used to separate arbitrary user time intervals (eg 3 of 7).
        Public ListRow As Integer      'Temp - refers to location on event list.
    End Structure
    Public Structure UTDPrefs
        'Video settings
        Public PreviewAudioOnCapture As Boolean     'True if preview audio during capture
        Public AutoPlay As Boolean                  'AutoPlay after capture
        Public VideoCaptureFormat As String         'Compression format for capture
        Public VideoCaptureDir As String            'Default trimming directory
        Public StopAtEndOfClip As Boolean           'If true, stops selected clips and endopoint, otherwise plays until user intervention.
        Public PlayContinuous As Boolean            'If true then clips play continuously
        Public dbPath As String                     'Location of Database
        Public AutoUpdateDB As Boolean              'If true then any manual changes to action list are automatically applied to DB.
        Public CacheAllData As Boolean              'Cache all data at loading - false setting will speed up loading.

        'Public AddDescriptors As Boolean        'Write descriptor tags as events in edit list
        'Public AddDescriptors2Stats As Boolean  'Add descriptor tags as events to stats
        ''Public DescriptorsTiming As descTime    'Handling method for descriptors in editlist.
        'Public LeadTime As Single               'Pre-event lead time
        'Public LagTime As Single                'Post-event lag time.
        'Public ShowStats() As Boolean           'Show stats for pitch regions.
        'Public AlwaysShowStats As Boolean       'Show and load stats with new and loaded games
        'Public GridCount() As Integer           'Number of hor & vert grids (x, y).
        'Public GridSize() As Integer            'Analysis grid size (default = 20)??
        'Public ShowClusterLabels As Boolean     'Show frequency count labels on cluster graphs
        'Public AlwaysShowCriteria As Boolean
        'Public SelectionTreshold As Single      'Percent threshold for selection of endpoints for analysis
        'Public PassesThreshold As Integer       'Number of consecutive passes/movements as a threshold.
        'Public PassesOnly As Boolean            'If true, threshold applies only to passes.
        'Public ThresholdType As quadThresholdType   '0 = threshold percent; 1 = descrete threshold value.
        'Public ShowGridOnPaths As Boolean           'Show outcome grid on path analysis chart.
        'Public RedBlueArrows As Boolean         'Red for negative outcomes - Blue for Positive
        'Public ShowSuccessPercents As Boolean   'Show percentage labels for success arrows.
        'Public AnimationSpeed As Integer        '0 = slow; 1 = normal; 2 = fast
        'Public ClearBetweenPlays As Boolean     'Refresh screen between play sets
        'Public Sport As tSports                 'Default sport.
        'Public SingleClickOnly As Boolean       'If true, then pitch only responds to single clicks - eg netball where no ball carry.
        'Public DescriptorsBelong2Team As Boolean    'If true then descriptors are shown as "belonging" to a team/player in the stats.
        'Public ShowTotals As Boolean            'If true then show outcome totals for all regions combined.
        'Public FontColor As Color               'Timeline font color.
        'Public GraphTime As Integer             'Time segments (mins) for possession graphs: NB - 0 denotes use of Time Criteria labels.
        ''Public GraphType As VtChChartType       'Default chart type.
        'Public GraphShowByHalfs As Boolean      'If true then show time criteria separately.
        'Public GraphUserTimeIntervals As Integer    'Number of specific time user intervals for a ratio graph.
        'Public AutoUpdateStats As Boolean           'Auto update stats when changes are made to selection criteria
        'Public boolANDOR As Boolean                 'If true, then search term is exlcusive AND - false = inclusive OR
        'Public TransparentTags As Boolean
        'Public BoldTags As Boolean
        'Public EnableTablet As Boolean
        'Public ESC2Undo As Boolean
        'Public UseJog As Boolean
        'Public boolSearchPaths As Boolean           'If true, searches in specified paths for video file if not found in default location.
        'Public nPathCount As Integer                'Number of paths..
        'Public boolUsePath() As Boolean             'Sets individual search paths as active.
        'Public altVideoPath() As String             'Alternate path..
        'Public ShowToolTips As Boolean              'If true shows tool tips on analysis charts.
    End Structure
    Public Structure ButtonProps
        Public Caption As String
        Public PositiveOutcome As OutcomeType
        Public ForeColor As Color
        Public BackColor As Color
        Public Transparency As Integer
    End Structure

    Public PlottingMode As gMode
    Public UserPrefs As UTDPrefs
    Public GamePath() As PlotPoints
    Public PathCount As Integer
    Public GameCount As Integer
    Public szTeamName() As String


    Public Sub GetSettings()
        With UserPrefs
            .VideoCaptureDir = GetSetting(AppName, "Settings", "VideoCaptureDir", "C:\")
            .VideoCaptureFormat = GetSetting(AppName, "Settings", "VideoCaptureFormat", "DV Video Decoder")
            .AutoPlay = CBool(GetSetting(AppName, "Settings", "AutoPlay", "False"))
            .PreviewAudioOnCapture = CBool(GetSetting(AppName, "Settings", "PreviewAudioOnCapture", "True"))
            .PlayContinuous = CBool(GetSetting(AppName, "Settings", "PlayContinuous", "False"))
            .StopAtEndOfClip = CBool(GetSetting(AppName, "Settings", "StopAtEndOfClip", "False"))
            .dbPath = GetSetting(AppName, "Settings", "dbPath", Application.StartupPath & "\GamePath.mdb")
            .AutoUpdateDB = CBool(GetSetting(AppName, "Settings", "AutoUpdateDB", "True"))
            .CacheAllData = CBool(GetSetting(AppName, "Settings", "CacheAllData", "True"))
        End With
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
    Public Function GetTimeStringFromSeconds(ByRef sTime As Double, Optional ByRef trim2secs As Boolean = False) As String
        Dim hrs As Integer
        Dim mins As Integer
        Dim secs As Single
        If sTime < 0 Then sTime = 0
        hrs = Int(sTime / 3600)
        mins = Int((sTime - (hrs * 3600)) / 60)
        secs = sTime - ((hrs * 3600) + (mins * 60))

        If trim2secs Then
            GetTimeStringFromSeconds = VB6.Format(hrs, "00:") & VB6.Format(mins, "00:") & VB6.Format(secs, "00")
        Else
            GetTimeStringFromSeconds = VB6.Format(hrs, "00:") & VB6.Format(mins, "00:") & VB6.Format(secs, "00.00")
        End If
    End Function

    Public Function GetSport(ByVal nIndex As tSports) As String
        GetSport = "Hockey"
        Select Case nIndex
            Case Is = tSports.sHockey
                GetSport = "Hockey"
            Case Is = tSports.sSoccer
                GetSport = "Soccer"
            Case Is = tSports.sNetball
                GetSport = "Netball"
            Case Is = tSports.sAFL
                GetSport = "AFL Football"
        End Select

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

    Public Function GetRegionString(ByVal reg As tRegion) As String

        Select Case reg

            Case Is = tRegion.rgBottomCircle
                GetRegionString = "Defensive 16"
            Case Is = tRegion.rgTopCircle
                GetRegionString = "Offensive 16"
            Case Is = tRegion.rgBottom25
                GetRegionString = "Defensive 25"
            Case Is = tRegion.rgTop25
                GetRegionString = "Offensive 25"
            Case Is = tRegion.rgTopHalf
                GetRegionString = "Offensive Half"
            Case Is = tRegion.rgBottomHalf
                GetRegionString = "Defensive Half"
            Case Is = tRegion.rgAttackThird
                GetRegionString = "Attack 3rd"
            Case Is = tRegion.rgMiddleThird
                GetRegionString = "Middle 3rd"
            Case Is = tRegion.rgDefensiveThird
                GetRegionString = "Defensive 3rd"
            Case Is = tRegion.rgAttackCircle
                GetRegionString = "Circle Attack"
            Case Is = tRegion.rgDefensiveCircle
                GetRegionString = "Circle Defense"

            Case Is = tRegion.rgSoccerBackThird
                GetRegionString = "Back Third"
            Case Is = tRegion.rgSoccerMiddleThird
                GetRegionString = "Middle Third"
            Case Is = tRegion.rgSoccerFrontThird
                GetRegionString = "Front Third"
            Case Is = tRegion.rgSoccerFrontCentre
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
            Case Else
                GetRegionString = ""
        End Select

    End Function

End Module
