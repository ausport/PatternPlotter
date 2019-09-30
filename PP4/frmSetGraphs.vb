Imports System.Windows.Forms

Public Class frmSetGraphs

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If Me.lstYAxisList.CheckedItems.Count = 0 Then
            MsgBox("Please select at least one item for the y-axis", MsgBoxStyle.Exclamation, Application.ProductName)
            Exit Sub
        End If

        For Each item As Object In Me.lstYAxisList.CheckedItems
            If CurrentGraphs Is Nothing Then
                ReDim CurrentGraphs(0)
            Else
                ReDim Preserve CurrentGraphs(CurrentGraphs.Length)
            End If

            With CurrentGraphs(CurrentGraphs.Length - 1)
                .ChartType = [Enum].Parse(GetType(MSChart20Lib.VtChChartType), Me.cboGraphTypes.Text)
                .Stacked = Me.chkGraphStacked.Checked
                .xAxis = Me.cboXAxis.Text
                .DataGroup = Me.cboDataGroup.Text
                .yAxis = item
                .Title = .yAxis & " by " & .xAxis
                If .DataGroup <> "None" Then
                    .Title = .Title & " and " & .DataGroup
                End If

                Me.lstCurrentGraphs.Items.Add(.Title, True)

            End With
        Next



    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        If Not CurrentGraphs Is Nothing Then
            For Each Graph As GraphType In CurrentGraphs
                Dim n As Integer = Me.lstCurrentGraphs.Items.IndexOf(Graph.Title)
                If n >= 0 Then
                    SaveSetting(AppName, "Settings", "GraphTitle" & CStr(n), Graph.Title)
                    SaveSetting(AppName, "Settings", "GraphxAxis" & CStr(n), Graph.xAxis)
                    SaveSetting(AppName, "Settings", "GraphyAxis" & CStr(n), Graph.yAxis)
                    SaveSetting(AppName, "Settings", "GraphDataGroup" & CStr(n), Graph.DataGroup)
                    SaveSetting(AppName, "Settings", "GraphStacked" & CStr(n), Graph.Stacked)
                    SaveSetting(AppName, "Settings", "GraphChartType" & CStr(n), CStr(Graph.ChartType))
                    SaveSetting(AppName, "Settings", "GraphChecked" & CStr(n), lstCurrentGraphs.GetItemChecked(n))
                End If
            Next

            SaveSetting(AppName, "Settings", "GraphCount", Me.lstCurrentGraphs.Items.Count)
        End If

        'Reload graphs into memory - if items have been deleted they will not yet be removed from the CurrentGraphs array.
        CurrentGraphs = GetGraphs()

        Me.Close()
    End Sub

    Private Sub frmSetGraphs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim types() As String = [Enum].GetNames(GetType(MSChart20Lib.VtChChartType))
        For Each type As String In types
            Me.cboGraphTypes.Items.Add(type)
        Next
        Me.cboGraphTypes.Text = types(0)

        'Show previously saved graphs in list
        CurrentGraphs = GetGraphs()

        Me.lstCurrentGraphs.Items.Clear()
        If Not CurrentGraphs Is Nothing Then
            For Each graph As GraphType In CurrentGraphs
                Me.lstCurrentGraphs.Items.Add(graph.Title, graph.GraphChecked)

            Next
        End If

    End Sub

    Public Sub cboGraphTypes_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGraphTypes.SelectedValueChanged
        Try
            Me.ChartPreview.chartType = [Enum].Parse(GetType(MSChart20Lib.VtChChartType), Me.cboGraphTypes.Text)
            Me.ChartPreview.Visible = True
            Me.ChartPreview.Stacking = Me.chkGraphStacked.Checked
        Catch ex As Exception
            Me.ChartPreview.Visible = False
        End Try
    End Sub

    Private Sub chkGraphStacked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGraphStacked.CheckedChanged
        Me.cboGraphTypes_SelectedValueChanged(sender, e)
    End Sub

    Private Sub cmdRemoveGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveGraph.Click

        Me.lstCurrentGraphs.Items.Remove(Me.lstCurrentGraphs.SelectedItem)

    End Sub

    Private Sub lstYAxisList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstYAxisList.SelectedIndexChanged

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cboGraphTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGraphTypes.SelectedIndexChanged

    End Sub

    Private Sub cboXAxis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboXAxis.SelectedIndexChanged
        If cboXAxis.Text = "Time (Minutes)" Then
            'Deselect all other charts except Event Probability
            Me.lstYAxisList.Items.Clear()
            Me.lstYAxisList.Items.Add("Event Probability")
            Me.lstYAxisList.Items.Add("Posession Time")

        Else
            Me.lstYAxisList.Items.Clear()
            Me.lstYAxisList.Items.Add("Event Totals")
            Me.lstYAxisList.Items.Add("Posession Time")
            Me.lstYAxisList.Items.Add("Ball Movements")
            Me.lstYAxisList.Items.Add("Distance")
            Me.lstYAxisList.Items.Add("Event Probability")
        End If
    End Sub
End Class
