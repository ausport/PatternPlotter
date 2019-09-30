Imports System.Windows.Forms

Public Class dlgColor

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgColor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub tabWeb_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles tabWeb.Paint
        'Draw swatches
        Dim x, y, dx, dy, i As Integer
        x = 10
        y = 10
        dx = 25
        dy = 25

        With e.Graphics
            For Each clr As KnownColor In kColor
                If Not Color.FromKnownColor(clr).IsSystemColor Then

                    .FillRectangle(New SolidBrush(Color.FromKnownColor(clr)), dx + x, dy + y, 22, 22)
                    .DrawRectangle(New Pen(Color.DarkGray), dx + x, dy + y, 22, 22)
                    i = i + 1
                    x = x + 25
                    If i = 15 Then
                        x = 10
                        y = y + 25
                        i = 0
                    End If
                End If
            Next

        End With


    End Sub


    ' This method initializes the owner-drawn combo box.
    ' The drop-down width is set much wider than the size of the combo box
    ' to accomodate the large items in the list.  The drop-down style is set to 
    ' ComboBox.DropDown, which requires the user to click on the arrow to 
    ' see the list.


    ' You must handle the DrawItem event for owner-drawn combo boxes.  
    ' This event handler changes the color, size and font of an 
    ' item based on its position in the array.
    Private Sub ComboBox1_DrawItem(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DrawItemEventArgs) _
        Handles ComboBox1.DrawItem

        ' Draw the background of the item.
        e.DrawBackground()

        ' Create a square filled with the animals color. Vary the size
        ' of the rectangle based on the length of the animals name.
        Dim rectangle As Rectangle = New Rectangle(2, e.Bounds.Top + 2, _
            e.Bounds.Height, e.Bounds.Height - 4)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromKnownColor(kColor(e.Index))), rectangle)

        ' Draw each string in the array, using a different size, color,
        ' and font for each item.
        'myFont = New Font(family, size, FontStyle.Bold)
        e.Graphics.DrawString(Color.FromKnownColor(kColor(e.Index)).Name, Me.Font, System.Drawing.Brushes.Black, _
            New RectangleF(e.Bounds.X + rectangle.Width, e.Bounds.Y, _
            e.Bounds.Width, e.Bounds.Height))

        ' Draw the focus rectangle if the mouse hovers over an item.
        e.DrawFocusRectangle()
    End Sub


End Class
