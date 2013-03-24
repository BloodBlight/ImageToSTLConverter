Public Class frmMain
    Structure Triangle
        Dim intXN As Single
        Dim intYN As Single
        Dim intZN As Single
        Dim intX1 As Single
        Dim intY1 As Single
        Dim intZ1 As Single
        Dim intX2 As Single
        Dim intY2 As Single
        Dim intZ2 As Single
        Dim intX3 As Single
        Dim intY3 As Single
        Dim intZ3 As Single
        Dim intABC As UInt16
    End Structure

    Dim bitUpdateNeeded As Boolean = True
    Dim bitWorking As Boolean = False
    Dim bitBinMode As Boolean = True
    Dim bitUpdateingSize As Boolean = False


    Private Sub picSource_Click(sender As Object, e As EventArgs) Handles picSource.Click

    End Sub

    Private Sub picSource_DragDrop(sender As Object, e As DragEventArgs) Handles picSource.DragDrop
        Application.DoEvents()
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        picSource.AllowDrop = True
        Me.AllowDrop = True
        tbBWTH.Value = 79
        LoadImage()

    End Sub

    Sub LoadImage(Optional ByRef strFileName As String = "")
        If strFileName <> "" Then
        End If

        Dim X As Integer = picSource.Image.Width
        Dim Y As Integer = picSource.Image.Height
        bitUpdateingSize = True
        If X > Y Then
            txtX.Text = 200
            txtY.Text = Y / X * 200
        Else
            txtY.Text = 200
            txtX.Text = X / Y * 200
        End If
        bitUpdateingSize = False
    End Sub


    Private Sub cmdCreate_Click(sender As Object, e As EventArgs) Handles cmdCreate.Click
        CreateSTLFile()
        Beep()
    End Sub


    Private Sub tbBWTH_ValueChanged(sender As Object, e As EventArgs) Handles tbBWTH.ValueChanged
        chkBW.Text = "Enable B/W Threshold: " & tbBWTH.Value
        bitUpdateNeeded = True
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        If bitWorking Then
            If picDest.BorderStyle = BorderStyle.None Then
                picDest.BorderStyle = BorderStyle.FixedSingle
            Else
                picDest.BorderStyle = BorderStyle.None
            End If
        ElseIf bitUpdateNeeded Then
            bitUpdateNeeded = False
            bitWorking = True
            cmdCreate.Enabled = False
            picDest.BorderStyle = BorderStyle.None

            Application.DoEvents()

            Dim X As Integer
            Dim Y As Integer

            Dim dblImageZ As Double = Val(txtZ.Text)
            Dim dblBaseZ As Double = Val(txtBase.Text)

            Dim intBaseZC As UInt16 = 255 - 255 / (dblImageZ + dblBaseZ) * dblBaseZ
            Dim dblImageZRatio As Double = (intBaseZC) / 255

            Dim intImageWidth As Integer = picSource.Image.Size.Width
            Dim intImageHeight As Integer = picSource.Image.Size.Height
            Dim bitDoBW As Boolean = chkBW.Checked

            'Dim objPic As System.Drawing.Image
            Dim objSource As New System.Drawing.Bitmap(intImageWidth, intImageHeight)
            Dim objTarget As New System.Drawing.Bitmap(picSource.Image.Size.Width, picSource.Image.Size.Height)
            Dim objColor As System.Drawing.Color
            Dim intNewGray As UInt16
            Dim intThresh As Integer = tbBWTH.Value

            Dim objBaseColor As System.Drawing.Color
            If Val(txtBase.Text) > 0 Then
                'Dim intGray As Integer = 255 - (Val(txtBase.Text) / (Val(txtZ.Text) + Val(txtBase.Text)) * 255)
                objBaseColor = System.Drawing.Color.FromArgb(intBaseZC, intBaseZC, intBaseZC)
            Else
                objBaseColor = Color.White
            End If


            objSource = picSource.Image

            For X = 1 To picSource.Image.Size.Width
                For Y = 1 To picSource.Image.Size.Height
                    objColor = objSource.GetPixel(X - 1, Y - 1)
                    If bitDoBW Then
                        If (Int(objColor.R) + Int(objColor.B) + Int(objColor.G)) / 3 >= intThresh Then
                            'objTarget.SetPixel(X - 1, Y - 1, Color.White)
                            objTarget.SetPixel(X - 1, Y - 1, objBaseColor)
                        Else
                            objTarget.SetPixel(X - 1, Y - 1, Color.Black)
                        End If
                    Else
                        intNewGray = ((Int(objColor.R) + Int(objColor.B) + Int(objColor.G)) / 3 * dblImageZRatio)

                        objTarget.SetPixel(X - 1, Y - 1, Color.FromArgb(intNewGray, intNewGray, intNewGray))
                    End If
                Next
                Application.DoEvents()
                If bitUpdateNeeded Then
                    bitWorking = False
                    Exit Sub
                End If
            Next

            If chkSpike.Checked Then
                Dim bitSpikeFound As Boolean
                Dim intSpikeFound As UInt32 = 0
                Dim bitFriendFound As Boolean
                Dim bitHasFriends As Boolean
                Dim objFirstAltLayer As Color

                Do
                    bitSpikeFound = False

                    For X = 1 To picSource.Image.Size.Width - 2
                        For Y = 1 To picSource.Image.Size.Height - 2
                            bitFriendFound = False
                            bitHasFriends = False
                            objColor = objTarget.GetPixel(X, Y)

                            If objColor = objTarget.GetPixel(X - 1, Y - 1) Then
                                bitFriendFound = True
                            Else
                                objFirstAltLayer = objTarget.GetPixel(X - 1, Y - 1)
                            End If

                            If objColor = objTarget.GetPixel(X, Y - 1) Then
                                bitHasFriends = True
                            Else
                                If objFirstAltLayer = objTarget.GetPixel(X, Y - 1) Then
                                    bitFriendFound = False
                                Else
                                    bitHasFriends = True
                                End If
                            End If

                            FriendLogic(objColor, objTarget.GetPixel(X + 1, Y - 1), bitHasFriends, bitFriendFound, objFirstAltLayer)
                            FriendLogic(objColor, objTarget.GetPixel(X + 1, Y), bitHasFriends, bitFriendFound, objFirstAltLayer)
                            FriendLogic(objColor, objTarget.GetPixel(X + 1, Y + 1), bitHasFriends, bitFriendFound, objFirstAltLayer)
                            FriendLogic(objColor, objTarget.GetPixel(X, Y + 1), bitHasFriends, bitFriendFound, objFirstAltLayer)
                            FriendLogic(objColor, objTarget.GetPixel(X - 1, Y + 1), bitHasFriends, bitFriendFound, objFirstAltLayer)
                            FriendLogic(objColor, objTarget.GetPixel(X - 1, Y), bitHasFriends, bitFriendFound, objFirstAltLayer)

                            'Need to do one extra check back to the first.
                            If Not bitHasFriends Then
                                If bitFriendFound Then
                                    If objColor = objTarget.GetPixel(X - 1, Y - 1) Then
                                        bitHasFriends = True
                                    End If
                                End If
                            End If

                            If Not bitHasFriends Then
                                bitSpikeFound = True
                                intSpikeFound += 1
                                objTarget.SetPixel(X, Y, objFirstAltLayer)
                            End If
                        Next

                        If bitUpdateNeeded Then
                            bitWorking = False
                            Exit Sub
                        End If

                        Me.Text = "Image To STL Converter - Found " & intSpikeFound & " spikes..."
                        Application.DoEvents()
                    Next
                Loop While bitSpikeFound
            End If

            picDest.Image = objTarget

            bitWorking = False
            cmdCreate.Enabled = True
            picDest.BorderStyle = BorderStyle.FixedSingle
        End If
    End Sub

    Sub FriendLogic(ByRef objColor1 As Color, _
                    ByRef objColor2 As Color, _
                    ByRef bitHasFriends As Boolean, _
                    ByRef bitFriendFound As Boolean, _
                    ByRef objFirstAltLayer As Color)

        If Not bitHasFriends Then
            If objColor1 = objColor2 Then
                If bitFriendFound Then
                    bitHasFriends = True
                Else
                    bitFriendFound = True
                End If
            Else
                If objColor2 = objFirstAltLayer Then
                    bitFriendFound = False
                Else
                    bitHasFriends = True
                End If
            End If
        End If
    End Sub


    Private Sub txtBase_TextChanged(sender As Object, e As EventArgs) Handles txtBase.TextChanged
        With txtBase
            If SafeNumber(.Text) Then
                .SelectionStart = .Text.Length
            End If
            If Val(.Text) > 0 Then
                txtBaseBoarder.Enabled = True
            Else
                txtBaseBoarder.Enabled = False
            End If
        End With
        bitUpdateNeeded = True
    End Sub

    Function SafeNumber(ByRef strInput As String) As Boolean
        Dim strSourceTemp As String = strInput
        Dim bitHasDot As Boolean = False
        If Len(strSourceTemp) > 0 Then
            If Mid(strSourceTemp, Len(strSourceTemp), 1) = "." Then
                strSourceTemp += "0"
                bitHasDot = True
            End If
        End If
        Dim strTemp As String = Trim(Str(Val(strSourceTemp)))
        If bitHasDot Then strTemp += "."
        If strInput <> strTemp Then
            strInput = strTemp
            Beep()
            Return 1
        End If
        Return 0
    End Function

    Private Sub txtBaseBoarder_TextChanged(sender As Object, e As EventArgs) Handles txtBaseBoarder.TextChanged
        With txtBaseBoarder
            If SafeNumber(.Text) Then
                .SelectionStart = .Text.Length
            End If
        End With
        bitUpdateNeeded = True
    End Sub

    Private Sub txtX_TextChanged(sender As Object, e As EventArgs) Handles txtX.TextChanged
        If Not bitUpdateingSize Then
            With txtX
                If SafeNumber(.Text) Then
                    .SelectionStart = .Text.Length
                End If

                If chkLocked.Checked Then
                    bitUpdateingSize = True
                    txtY.Text = picSource.Image.Height / picSource.Image.Width * Val(.Text)
                    bitUpdateingSize = False
                End If
            End With
            bitUpdateNeeded = True
        End If
    End Sub

    Private Sub txtY_TextChanged(sender As Object, e As EventArgs) Handles txtY.TextChanged
        If Not bitUpdateingSize Then
            With txtY
                If SafeNumber(.Text) Then
                    .SelectionStart = .Text.Length
                End If

                If chkLocked.Checked Then
                    bitUpdateingSize = True
                    txtX.Text = picSource.Image.Width / picSource.Image.Height * Val(.Text)
                    bitUpdateingSize = False
                End If
            End With
            bitUpdateNeeded = True
        End If
    End Sub

    Private Sub txtZ_TextChanged(sender As Object, e As EventArgs) Handles txtZ.TextChanged
        With txtZ
            If SafeNumber(.Text) Then
                .SelectionStart = .Text.Length
            End If
        End With
        bitUpdateNeeded = True
    End Sub

    Sub CreateSTLFile()
        'Binary STL File format:
        '***************************************
        'UINT8[80] – Header
        'UINT32 – Number of triangles
        '
        'foreach triangle
        'REAL32[3] – Normal vector
        'REAL32[3] – Vertex 1
        'REAL32[3] – Vertex 2
        'REAL32[3] – Vertex 3
        'UINT16 – Attribute byte count
        'End
        '***************************************

        Dim strFile As String = "F:\Temp\Test.stl"
        Dim strHeader As String
        
        Dim X As UInt32
        Dim Y As UInt32

        'We can fix it!

        Dim objImage As System.Drawing.Bitmap = picDest.Image
        Dim intImageWidth As Integer = objImage.Size.Width
        Dim intImageHeight As Integer = objImage.Size.Height

        Dim dblScaleX As Double = CDbl(txtX.Text) / CDbl(intImageWidth)
        Dim dblScaleY As Double = CDbl(txtY.Text) / CDbl(intImageHeight)

        Dim objTriangles(intImageWidth * intImageHeight * 12) As Triangle
        Dim objTriangleC As UInt32 = 0

        Dim intHeights(objImage.Size.Width + 2, objImage.Size.Height + 2) As Single
        Dim intTotalHeight As UInt16 = Val(txtZ.Text) + Val(txtBase.Text)

        Dim intMinDepth As UInt32 = 255
        Dim bitDoBase As Boolean = True

        For X = 0 To intImageWidth - 1
            For Y = 0 To intImageHeight - 1
                intHeights((intImageWidth) - X, (intImageHeight) - Y) = (255 - CDbl(objImage.GetPixel(X, Y).R)) / 255 * intTotalHeight
                If intHeights((intImageWidth) - X, (intImageHeight) - Y) < intMinDepth Then
                    intMinDepth = intHeights((intImageWidth) - X, (intImageHeight) - Y)
                End If
            Next
        Next

        'Point order is important to calculate an "outside surface"!

        If intMinDepth > 0 Then
            bitDoBase = False

            'Lets do a simple base...
            objTriangleC += 1
            With objTriangles(objTriangleC)
                .intX1 = (intImageWidth + 1) * dblScaleX
                .intY1 = 0
                .intZ1 = 0

                .intX2 = 0
                .intY2 = 0
                .intZ2 = 0

                .intX3 = 0
                .intY3 = (intImageHeight + 1) * dblScaleY
                .intZ3 = 0
            End With

            objTriangleC += 1
            With objTriangles(objTriangleC)
                .intX1 = (intImageWidth + 1) * dblScaleX
                .intY1 = 0
                .intZ1 = 0

                .intX2 = 0
                .intY2 = (intImageHeight + 1) * dblScaleY
                .intZ2 = 0

                .intX3 = (intImageWidth + 1) * dblScaleX
                .intY3 = (intImageHeight + 1) * dblScaleY
                .intZ3 = 0
            End With

            For X = 0 To intImageWidth - 1
                ''TopMost #1
                'objTriangleC += 1
                'With objTriangles(objTriangleC)
                '    .intX1 = X * dblScaleX
                '    .intY1 = 0
                '    .intZ1 = 0

                '    .intX2 = (X + 1) * dblScaleX
                '    .intY2 = 0
                '    .intZ2 = 0

                '    .intX3 = X * dblScaleX
                '    .intY3 = 0
                '    .intZ3 = intHeights(X + 1, 1)
                'End With

                ''TopMost #2
                'objTriangleC += 1
                'With objTriangles(objTriangleC)
                '    .intX1 = (X + 1) * dblScaleX
                '    .intY1 = 0
                '    .intZ1 = 0

                '    .intX2 = (X + 1) * dblScaleX
                '    .intY2 = 0
                '    .intZ2 = intHeights(X + 1, 1)

                '    .intX3 = X * dblScaleX
                '    .intY3 = 0
                '    .intZ3 = intHeights(X + 1, 1)
                'End With

                ''BottomMost #1
                'objTriangleC += 1
                'With objTriangles(objTriangleC)
                '    .intX1 = X * dblScaleX
                '    .intY1 = intImageHeight * dblScaleY
                '    .intZ1 = 0

                '    .intX2 = X * dblScaleX
                '    .intY2 = intImageHeight * dblScaleY
                '    .intZ2 = intHeights(X + 1, intImageHeight - 1)

                '    .intX3 = (X + 1) * dblScaleX
                '    .intY3 = intImageHeight * dblScaleY
                '    .intZ3 = 0
                'End With

                ''BottomMost #2
                'objTriangleC += 1
                'With objTriangles(objTriangleC)
                '    .intX1 = (X + 1) * dblScaleX
                '    .intY1 = intImageHeight * dblScaleY
                '    .intZ1 = 0

                '    .intX2 = X * dblScaleX
                '    .intY2 = intImageHeight * dblScaleY
                '    .intZ2 = intHeights(X + 1, intImageHeight - 1)

                '    .intX3 = (X + 1) * dblScaleX
                '    .intY3 = intImageHeight * dblScaleY
                '    .intZ3 = intHeights(X + 1, intImageHeight - 1)
                'End With
            Next


            For Y = 0 To intImageHeight - 2
            Next
        End If


        For X = 1 To objImage.Size.Width + 1
            For Y = 1 To objImage.Size.Height + 1
                'If not bitDoBase, we know we have to map everything, skip the check.
                If (Not bitDoBase) OrElse _
                    intHeights(X, Y) > 0 OrElse _
                    intHeights(X - 1, Y) > 0 OrElse _
                    intHeights(X, Y - 1) > 0 OrElse _
                    intHeights(X - 1, Y - 1) > 0 Then

                    If X = objImage.Size.Width And Y = 10 Then
                        Application.DoEvents()
                    End If

                    'Above
                    objTriangleC += 1
                    With objTriangles(objTriangleC)
                        .intX1 = X * dblScaleX
                        .intY1 = Y * dblScaleY
                        .intZ1 = intHeights(X, Y)

                        .intX2 = (X - 1) * dblScaleX
                        .intY2 = (Y - 1) * dblScaleY
                        .intZ2 = intHeights(X - 1, Y - 1)

                        .intX3 = X * dblScaleX
                        .intY3 = (Y - 1) * dblScaleY
                        .intZ3 = intHeights(X, Y - 1)
                    End With

                    'Left
                    objTriangleC += 1
                    With objTriangles(objTriangleC)
                        .intX1 = X * dblScaleX
                        .intY1 = Y * dblScaleY
                        .intZ1 = intHeights(X, Y)

                        .intX2 = (X - 1) * dblScaleX
                        .intY2 = Y * dblScaleY
                        .intZ2 = intHeights(X - 1, Y)

                        .intX3 = (X - 1) * dblScaleX
                        .intY3 = (Y - 1) * dblScaleY
                        .intZ3 = intHeights(X - 1, Y - 1)
                    End With
                End If


                If bitDoBase Then

                End If
            Next
        Next


        'objTriangleC += 1


        Dim objFile As IO.FileStream

        'Try
        strHeader = Space(80)
        Mid(strHeader, 1, 8) = "STL File"

        Try
            FileSystem.Kill(strFile)
        Catch ex As Exception

        End Try

        objFile = IO.File.Open(strFile, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)

        'Write header.
        If bitBinMode Then
            WriteByteArray(strHeader, objFile)
            WriteByteArray(objTriangleC, objFile)
        Else
            WriteByteArray("solid object" & vbNewLine, objFile)
        End If


        'Lets get to it!
        For X = 1 To objTriangleC
            WriteTriangle(objTriangles(X), objFile)
        Next

        'Catch ex As Exception
        'MessageBox.Show("Error: " & ex.Message, "Program Error")
        'End Try

        If Not bitBinMode Then
            WriteByteArray("endsolid" & vbNewLine, objFile)
        End If

        Try
            objFile.Close()
        Catch ex As Exception

        End Try

        End
    End Sub

    Sub DebugTriangle(ByRef objFile As IO.FileStream)
        Dim objTriangle As Triangle
        Dim intTriangles As UInt32 = 6
        objFile.WriteByte(intTriangles)
        With objTriangle
            '#1
            .intX1 = 0
            .intY1 = 0
            .intZ1 = 0

            .intX2 = 20
            .intY2 = 0
            .intZ2 = 0

            .intX3 = 10
            .intY3 = 10
            .intZ3 = 20

            .intABC = 0
        End With
        WriteTriangle(objTriangle, objFile)

        With objTriangle
            '#2
            .intX1 = 0
            .intY1 = 0
            .intZ1 = 0

            .intX2 = 0
            .intY2 = 20
            .intZ2 = 0

            .intX3 = 10
            .intY3 = 10
            .intZ3 = 20

            .intABC = 0
        End With
        WriteTriangle(objTriangle, objFile)

        With objTriangle
            '#3
            .intX1 = 20
            .intY1 = 0
            .intZ1 = 0

            .intX2 = 20
            .intY2 = 20
            .intZ2 = 0

            .intX3 = 10
            .intY3 = 10
            .intZ3 = 20

            .intABC = 0
        End With
        WriteTriangle(objTriangle, objFile)

        With objTriangle
            '#4
            .intX1 = 0
            .intY1 = 20
            .intZ1 = 0

            .intX2 = 20
            .intY2 = 20
            .intZ2 = 0

            .intX3 = 10
            .intY3 = 10
            .intZ3 = 20

            .intABC = 50
        End With
        WriteTriangle(objTriangle, objFile)

        With objTriangle
            '#5
            .intX1 = 0
            .intY1 = 0
            .intZ1 = 0

            .intX2 = 0
            .intY2 = 20
            .intZ2 = 0

            .intX3 = 20
            .intY3 = 0
            .intZ3 = 0

            .intABC = 50
        End With
        WriteTriangle(objTriangle, objFile)

        With objTriangle
            '#6
            .intX1 = 20
            .intY1 = 20
            .intZ1 = 0

            .intX2 = 0
            .intY2 = 20
            .intZ2 = 0

            .intX3 = 20
            .intY3 = 0
            .intZ3 = 0

            .intABC = 50
        End With
        WriteTriangle(objTriangle, objFile)

    End Sub

    Sub WriteTriangle(ByRef objTriangle As Triangle, ByRef objFile As IO.FileStream)
        'Binary STL File format:
        '***************************************
        'UINT8[80] – Header
        'UINT32 – Number of triangles
        '
        'foreach triangle
        'REAL32[3] – Normal vector
        'REAL32[3] – Vertex 1
        'REAL32[3] – Vertex 2
        'REAL32[3] – Vertex 3
        'UINT16 – Attribute byte count
        'End
        '***************************************

        With objTriangle
            '.intXN = (.intY2 - .intY1) * (.intZ3 - .intZ1) - (.intZ2 - .intZ1) * (.intY3 - .intY1)
            '.intYN = (.intZ2 - .intZ1) * (.intX3 - .intX1) - (.intY2 - .intY1) * (.intZ3 - .intZ1)
            '.intZN = (.intX2 - .intX1) * (.intY3 - .intY1) - (.intX2 - .intX1) * (.intX3 - .intX1)

            If bitBinMode Then
                WriteByteArray(.intXN, objFile)
                WriteByteArray(.intYN, objFile)
                WriteByteArray(.intZN, objFile)

                WriteByteArray(.intX1, objFile)
                WriteByteArray(.intY1, objFile)
                WriteByteArray(.intZ1, objFile)

                WriteByteArray(.intX2, objFile)
                WriteByteArray(.intY2, objFile)
                WriteByteArray(.intZ2, objFile)

                WriteByteArray(.intX3, objFile)
                WriteByteArray(.intY3, objFile)
                WriteByteArray(.intZ3, objFile)

                WriteByteArray(.intABC, objFile)

            Else
                WriteByteArray("outer loop" & vbNewLine, objFile)
                WriteByteArray("vertex " & .intX1 & " " & .intY1 & " " & .intZ1 & vbNewLine, objFile)
                WriteByteArray("vertex " & .intX2 & " " & .intY2 & " " & .intZ2 & vbNewLine, objFile)
                WriteByteArray("vertex " & .intX3 & " " & .intY3 & " " & .intZ3 & vbNewLine, objFile)
                WriteByteArray("endloop" & vbNewLine, objFile)
                WriteByteArray("endfacet" & vbNewLine, objFile)
            End If
        End With
    End Sub

    Sub WriteByteArray(ByRef objVar As Single, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As Double, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As Int16, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As Int32, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As UInt16, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As UInt32, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Sub WriteByteArray(ByRef objVar As String, ByRef objFile As IO.FileStream)
        Dim utfTemp As New System.Text.UTF8Encoding()
        Dim bytBytes As Byte() = utfTemp.GetBytes(objVar)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
    End Sub

    Private Sub txtRez_TextChanged(sender As Object, e As EventArgs) Handles txtRez.TextChanged
        bitUpdateNeeded = True
    End Sub

    Private Sub chkSpike_CheckedChanged(sender As Object, e As EventArgs) Handles chkSpike.CheckedChanged
        bitUpdateNeeded = True
    End Sub

    Private Sub chkBW_CheckedChanged(sender As Object, e As EventArgs) Handles chkBW.CheckedChanged
        bitUpdateNeeded = True
        tbBWTH.Enabled = chkBW.Checked
    End Sub
End Class

