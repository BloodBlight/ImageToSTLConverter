Public Class frmMain
    Structure Triangle
        'Dim intXN As Single
        'Dim intYN As Single
        'Dim intZN As Single
        Dim intX1 As Single
        Dim intY1 As Single
        Dim intZ1 As Single
        Dim intX2 As Single
        Dim intY2 As Single
        Dim intZ2 As Single
        Dim intX3 As Single
        Dim intY3 As Single
        Dim intZ3 As Single
        'Dim intABC As UInt16
    End Structure


    Const conEmptySingle As Single = 0
    Const conEmptyUInt16 As UInt16 = 0

    Dim bitUpdateNeeded As Boolean = True
    Dim bitWorking As Boolean = False
    Dim bitBinMode As Boolean = True
    Dim bitUpdateingSize As Boolean = False

    Dim intErrorCount As UInt16 = 0

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
            picSource.Load(strFileName)
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
        bitUpdateNeeded = True
    End Sub


    Private Sub cmdCreate_Click(sender As Object, e As EventArgs) Handles cmdCreate.Click
        SetFormMode(False)
        Application.DoEvents()
        If chkSpike.Checked = False And chkAntiSpike.Checked = False Then
            Dim objResult As DialogResult = MessageBox.Show("The spike filters are NOT on, would you like to run them now?", "Spike Filters", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If objResult = Windows.Forms.DialogResult.Cancel Then
                SetFormMode(True)
                Exit Sub
            ElseIf objResult = Windows.Forms.DialogResult.Yes Then
                chkSpike.Checked = True
                chkAntiSpike.Checked = True
                bitUpdateNeeded = True
                Do While bitUpdateNeeded
                    Application.DoEvents()
                Loop
            End If
        End If
        Dim objOF As New Windows.Forms.SaveFileDialog
        With objOF
            .Filter = "STL File (*.stl)|*.stl"
            .AddExtension = True
            .DefaultExt = "stl"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                CreateSTLFile(.FileName)
                MessageBox.Show("Done!", "STL File Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End With
        SetFormMode(True)
    End Sub


    Private Sub tbBWTH_ValueChanged(sender As Object, e As EventArgs) Handles tbBWTH.ValueChanged
        chkBW.Text = "Enable B/W Threshold: " & tbBWTH.Value
        bitUpdateNeeded = True
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        If bitWorking Then
            If picDest.BackColor = Color.White Then
                picDest.BackColor = Color.Black
                'If chkSpike.Checked Then
                'lblSpike.Visible = True
                'End If
            Else
                picDest.BackColor = Color.White
                'If chkSpike.Checked Then
                'lblSpike.Visible = False
                'End If
            End If
        ElseIf bitUpdateNeeded Then
            Try

                bitUpdateNeeded = False
                bitWorking = True
                cmdCreate.Enabled = False
                picDest.BackColor = Color.White


                Application.DoEvents()

                Dim X As Integer
                Dim Y As Integer

                'Dim dblImageZ As Double = Val(txtZ.Text)
                'Dim dblBaseZ As Double = Val(txtBase.Text)

                'Dim intBaseZC As UInt16 = 255 - 255 / (dblImageZ + dblBaseZ) * dblBaseZ
                'Dim dblImageZRatio As Double = (intBaseZC) / 255

                Dim intImageWidth As Integer = picSource.Image.Size.Width
                Dim intImageHeight As Integer = picSource.Image.Size.Height
                Dim bitDoBW As Boolean = chkBW.Checked
                Dim bitDoAlpha As Boolean = chkAlpha.Checked
                Dim bitInvert As Boolean = chkInvert.Checked

                'Dim objPic As System.Drawing.Image
                Dim objSource As New System.Drawing.Bitmap(intImageWidth, intImageHeight)
                Dim objTarget As New System.Drawing.Bitmap(picSource.Image.Size.Width, picSource.Image.Size.Height)
                Dim objColor As System.Drawing.Color
                Dim intNewGray As UInt16
                Dim intThresh As Integer = tbBWTH.Value

                Dim objBaseColor As System.Drawing.Color
                Dim objTopColor As System.Drawing.Color

                If bitInvert Then
                    objBaseColor = Color.White
                    objTopColor = Color.Black
                Else
                    objBaseColor = Color.Black
                    objTopColor = Color.White
                End If

                'If Val(txtBase.Text) > 0 Then
                'Dim intGray As Integer = 255 - (Val(txtBase.Text) / (Val(txtZ.Text) + Val(txtBase.Text)) * 255)
                'objBaseColor = System.Drawing.Color.FromArgb(intBaseZC, intBaseZC, intBaseZC)
                'Else
                'objBaseColor = Color.White
                'End If


                objSource = picSource.Image
                picDest.Image = objTarget

                For X = 1 To picSource.Image.Size.Width
                    For Y = 1 To picSource.Image.Size.Height
                        objColor = objSource.GetPixel(X - 1, Y - 1)

                        'Compute gray
                        intNewGray = (Int(objColor.R) + Int(objColor.B) + Int(objColor.G)) / 3

                        'Compute Alpha
                        If bitDoAlpha Then
                            intNewGray = 255 - ((255 - intNewGray) * objColor.A / 255)
                        End If

                        If bitDoBW Then
                            If intNewGray >= intThresh Then
                                objTarget.SetPixel(X - 1, Y - 1, objTopColor)
                            Else
                                objTarget.SetPixel(X - 1, Y - 1, objBaseColor)
                            End If
                        Else
                            If bitInvert Then
                                objTarget.SetPixel(X - 1, Y - 1, Color.FromArgb(255 - intNewGray, 255 - intNewGray, 255 - intNewGray))
                            Else
                                objTarget.SetPixel(X - 1, Y - 1, Color.FromArgb(intNewGray, intNewGray, intNewGray))
                            End If
                        End If
                    Next
                    Application.DoEvents()
                    If bitUpdateNeeded Then
                        bitWorking = False
                        Exit Sub
                    End If
                Next



                Dim intLoops As UInt16 = 0
                Dim intSpikesFound As UInt32 = 0

                If chkSpike.Checked Then
                    Dim bitSpikeFound As Boolean

                    Dim intMinFriends As UInt16 = tbSpike.Value

                    Do
                        picDest.Image = objTarget
                        Application.DoEvents()

                        bitSpikeFound = False

                        For X = 1 To picSource.Image.Size.Width - 2
                            For Y = 1 To picSource.Image.Size.Height - 2

                                If CheckForSpikeAt(X, Y, objTarget, intMinFriends, intSpikesFound) Then
                                    bitSpikeFound = True
                                    If X > 1 And X < picSource.Image.Size.Width - 2 Then
                                        If Y > 1 And Y < picSource.Image.Size.Height - 2 Then
                                            CheckForSpikeAt(X - 1, Y - 1, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X, Y - 1, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X + 1, Y - 1, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X - 1, Y, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X + 1, Y, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X - 1, Y + 1, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X, Y + 1, objTarget, intMinFriends, intSpikesFound)
                                            CheckForSpikeAt(X + 1, Y + 1, objTarget, intMinFriends, intSpikesFound)
                                        End If
                                    End If
                                End If
                            Next

                            If bitUpdateNeeded Then
                                bitWorking = False
                                Exit Sub
                            End If

                            Me.Text = "Image To STL Converter - Found " & intSpikesFound & " spikes in " & intLoops & " loops..."
                            Application.DoEvents()

                        Next
                        intLoops += 1
                    Loop While bitSpikeFound And intLoops < 10
                End If




                If chkAntiSpike.Checked Then
                    Dim bitSpikeFound As Boolean

                    Dim intMinFriends As UInt16 = tbAntiSpike.Value


                    Do
                        picDest.Image = objTarget
                        Application.DoEvents()

                        bitSpikeFound = False

                        For X = 1 To picSource.Image.Size.Width - 2
                            For Y = 1 To picSource.Image.Size.Height - 2

                                If CheckForSpikeAt(X, Y, objTarget, intMinFriends, intSpikesFound, True) Then
                                    bitSpikeFound = True
                                    If X > 1 And X < picSource.Image.Size.Width - 2 Then
                                        If Y > 1 And Y < picSource.Image.Size.Height - 2 Then
                                            CheckForSpikeAt(X - 1, Y - 1, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X, Y - 1, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X + 1, Y - 1, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X - 1, Y, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X + 1, Y, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X - 1, Y + 1, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X, Y + 1, objTarget, intMinFriends, intSpikesFound, True)
                                            CheckForSpikeAt(X + 1, Y + 1, objTarget, intMinFriends, intSpikesFound, True)
                                        End If
                                    End If
                                End If
                            Next

                            If bitUpdateNeeded Then
                                bitWorking = False
                                Exit Sub
                            End If

                            Me.Text = "Image To STL Converter - Found " & intSpikesFound & " spikes in " & intLoops & " loops..."
                            Application.DoEvents()

                        Next
                        intLoops += 1
                    Loop While bitSpikeFound And intLoops < 20
                End If

                picDest.Image = objTarget
            Catch ex As Exception
                intErrorCount += 1
                If intErrorCount > 3 Then
                    MessageBox.Show("Ok, I'm broken bad!  We have a bug!" & vbNewLine & vbNewLine & _
                                    "Sorry, but I'm shutting down...", "You're doomed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                ElseIf intErrorCount > 1 Then
                    MessageBox.Show("EGAD!  We had an error!" & vbNewLine & vbNewLine & _
                                    "This can happen if you load an image while processing is still running (multiple times)." & vbNewLine & vbNewLine & _
                                    "If the image was NOT still processing and you received this alert, please report it with the following error:" & vbNewLine & vbNewLine & _
                                    ex.Message, "Compute Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show("I will try to continue processing your image.  If you get the same alert again, well, you’re doomed!", "It's all good!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Try

            intErrorCount = 0

            bitWorking = False
            cmdCreate.Enabled = True
            picDest.BackColor = Color.Black
            'lblSpike.Visible = True
        End If
    End Sub

    Function CheckForSpikeAt(ByRef X As UInt16, ByRef Y As UInt16, ByRef objTarget As Bitmap, ByRef intMinFriends As UInt16, ByRef intSpikesFound As UInt32, Optional ByRef bitAntiMode As Boolean = False) As Boolean
        Dim objTallFriend As UInt16 = 255
        Dim objTargetPixel As UInt16 = objTarget.GetPixel(X, Y).R
        Dim intFriends As UInt16 = 0
        Dim bitCouldBeSpike As Boolean = True

        If bitAntiMode Then objTallFriend = 0

        bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X - 1, Y - 1).R, objTallFriend, intFriends, bitAntiMode)
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X, Y - 1).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X + 1, Y - 1).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X - 1, Y).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X + 1, Y).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X - 1, Y + 1).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X, Y + 1).R, objTallFriend, intFriends, bitAntiMode)
        End If
        If bitCouldBeSpike Then
            bitCouldBeSpike = FriendLogic(objTargetPixel, objTarget.GetPixel(X + 1, Y + 1).R, objTallFriend, intFriends, bitAntiMode)
        End If

        If bitCouldBeSpike Then
            If intFriends < intMinFriends Then
                objTarget.SetPixel(X, Y, Color.FromArgb(objTallFriend, objTallFriend, objTallFriend))
                intSpikesFound += 1
                Return True
                'End iF
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Function FriendLogic(ByRef objTarget As UInt16, ByRef objFreind As UInt16, ByRef objTallFriend As UInt16, ByRef intFreindCount As UInt16, ByRef bitAntiMode As Boolean) As Boolean
        If bitAntiMode Then
            'Returns true if a freind isn't shorter.
            If objTarget < objFreind Then
                Return False
            Else
                If objFreind > objTallFriend And objFreind < objTarget Then
                    objTallFriend = objFreind
                ElseIf objFreind = objTarget Then
                    intFreindCount += 1
                End If
                Return True
            End If
        Else
            'Returns true if a freind isn't taller.
            If objTarget > objFreind Then
                Return False
            Else
                If objFreind < objTallFriend And objFreind > objTarget Then
                    objTallFriend = objFreind
                ElseIf objFreind = objTarget Then
                    intFreindCount += 1
                End If
                Return True
            End If
        End If
    End Function



    Private Sub txtBase_TextChanged(sender As Object, e As EventArgs) Handles txtBase.TextChanged
        With txtBase
            If SafeNumber(.Text) Then
                .SelectionStart = .Text.Length
            End If
        End With
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
        End If
    End Sub

    Private Sub txtZ_TextChanged(sender As Object, e As EventArgs) Handles txtZ.TextChanged
        With txtZ
            If SafeNumber(.Text) Then
                .SelectionStart = .Text.Length
            End If
        End With
    End Sub

    Sub CreateSTLFile(ByRef strFile As String)
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
        Dim bitInverted As Boolean = False

        Dim strHeader As String

        Dim X As UInt32
        Dim Y As UInt32


        'Dim objTriangles(intImageWidth * intImageHeight * 12) As Triangle
        Dim objTriangle As Triangle
        Dim objTriangleC As UInt32 = 0


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



        Dim objImage As System.Drawing.Bitmap = picDest.Image
        Dim intImageWidth As Integer = objImage.Size.Width
        Dim intImageHeight As Integer = objImage.Size.Height
        Dim dblImageBase As Double = Val(txtBase.Text)

        Dim dblScaleX As Double = CDbl(txtX.Text) / CDbl(intImageWidth)
        Dim dblScaleY As Double = CDbl(txtY.Text) / CDbl(intImageHeight)
        Dim dblScaleZ As Double = CDbl(txtZ.Text) / 255


        Dim intHeights(objImage.Size.Width + 2, objImage.Size.Height + 2) As Single
        Dim intTotalHeight As UInt16 = Val(txtZ.Text) + Val(txtBase.Text)

        Dim bitTopDone(objImage.Size.Width + 2, objImage.Size.Height + 2) As Boolean
        Dim bitBottomDone(2, 2) As Boolean

        Dim intMinDepth As UInt32 = 255
        Dim bitDoBase As Boolean = True

        For X = 0 To intImageWidth - 1
            For Y = 0 To intImageHeight - 1
                If bitInverted Then
                    intHeights(X + 1, (intImageHeight) - Y) = dblImageBase + CDbl(objImage.GetPixel(X, Y).R) * dblScaleZ
                Else
                    intHeights(X + 1, (intImageHeight) - Y) = dblImageBase + (255 - CDbl(objImage.GetPixel(X, Y).R)) * dblScaleZ
                End If
                If intHeights(X + 1, (intImageHeight) - Y) < intMinDepth Then
                    intMinDepth = intHeights(X + 1, (intImageHeight) - Y)
                End If
            Next
        Next

        'Point order is important to calculate an "outside surface"!

        If intMinDepth > 0 Then
            bitDoBase = False

            'Lets do a simple base...
            objTriangleC += 1
            With objTriangle
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
            WriteTriangle(objTriangle, objFile)

            objTriangleC += 1
            With objTriangle
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
            WriteTriangle(objTriangle, objFile)
        Else
            'Waste not want not!
            ReDim bitBottomDone(objImage.Size.Width + 2, objImage.Size.Height + 2)
        End If

        Dim I As UInt16
        Dim intTemp1 As UInt16
        Dim intTemp2 As UInt16
        'Dim intHeight As Single
        Dim intMatchSize As UInt16
        Dim intBlockSize As UInt16
        Dim bitNoMatch As Boolean

        'I should NOT need to do this!
        For X = 0 To intImageWidth
            For Y = 0 To intImageHeight
                bitTopDone(X, Y) = False
            Next
        Next

        For X = 0 To intImageWidth
            For Y = 0 To intImageHeight
                If Not bitTopDone(X, Y) Then
                    'If not bitDoBase, we know we have to map everything, skip the check.
                    If (Not bitDoBase) OrElse _
                        intHeights(X, Y) > 0 OrElse _
                        intHeights(X + 1, Y) > 0 OrElse _
                        intHeights(X, Y + 1) > 0 OrElse _
                        intHeights(X + 1, Y + 1) > 0 Then

                        intBlockSize = 0
                        bitNoMatch = False

                        intTemp1 = intImageWidth - X
                        intTemp2 = intImageHeight - Y
                        If intTemp1 > intTemp2 Then
                            intMatchSize = intTemp1
                        Else
                            intMatchSize = intTemp2
                        End If

                        
                        For I = 1 To intMatchSize
                            For intTemp1 = 0 To I
                                'Yes I know we do one ex6tra check per loop.
                                If bitTopDone(X, Y) Then
                                    bitNoMatch = True
                                    Exit For
                                Else
                                    If intHeights(X, Y) <> intHeights(X + I, Y + intTemp1) Then
                                        bitNoMatch = True
                                        Exit For
                                    ElseIf intHeights(X, Y) <> intHeights(X + intTemp1, Y + I) Then
                                        bitNoMatch = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If bitNoMatch Then
                                Exit For
                            Else
                                intBlockSize = I - 1
                            End If
                        Next

                        If intBlockSize < 0 Then
                            intBlockSize = 0
                        End If

                        For intTemp1 = 0 To intBlockSize
                            For intTemp2 = 0 To intBlockSize
                                bitTopDone(X + intTemp1, Y + intTemp2) = True
                            Next
                        Next

                        intBlockSize += 1

                        'Below
                        objTriangleC += 1
                        With objTriangle
                            .intX1 = X * dblScaleX
                            .intY1 = Y * dblScaleY
                            .intZ1 = intHeights(X, Y)

                            .intX2 = (X + intBlockSize) * dblScaleX
                            .intY2 = (Y + intBlockSize) * dblScaleY
                            .intZ2 = intHeights(X + intBlockSize, Y + intBlockSize)

                            .intX3 = (X) * dblScaleX
                            .intY3 = (Y + intBlockSize) * dblScaleY
                            .intZ3 = intHeights(X, Y + intBlockSize)
                        End With
                        WriteTriangle(objTriangle, objFile)

                        'Right
                        objTriangleC += 1
                        With objTriangle
                            .intX1 = X * dblScaleX
                            .intY1 = Y * dblScaleY
                            .intZ1 = intHeights(X, Y)

                            .intX2 = (X + intBlockSize) * dblScaleX
                            .intY2 = Y * dblScaleY
                            .intZ2 = intHeights(X + intBlockSize, Y)

                            .intX3 = (X + intBlockSize) * dblScaleX
                            .intY3 = (Y + intBlockSize) * dblScaleY
                            .intZ3 = intHeights(X + intBlockSize, Y + intBlockSize)
                        End With
                        WriteTriangle(objTriangle, objFile)

                    End If
                End If
            Next
        Next

        If bitDoBase Then
            For X = 0 To objImage.Size.Width
                For Y = 0 To objImage.Size.Height
                    If Not bitBottomDone(X, Y) Then
                        'If not bitDoBase, we know we have to map everything, skip the check.
                        If intHeights(X, Y) > 0 OrElse _
                            intHeights(X, Y) > 0 OrElse _
                            intHeights(X + 1, Y) > 0 OrElse _
                            intHeights(X, Y + 1) > 0 OrElse _
                            intHeights(X + 1, Y + 1) > 0 Then





                            intBlockSize = 0
                            bitNoMatch = False

                            intTemp1 = intImageWidth - X
                            intTemp2 = intImageHeight - Y
                            If intTemp1 > intTemp2 Then
                                intMatchSize = intTemp1
                            Else
                                intMatchSize = intTemp2
                            End If


                            For I = 1 To intMatchSize
                                For intTemp1 = 0 To I
                                    'Yes I know we do one extra check per loop.
                                    If bitBottomDone(X, Y) Then
                                        bitNoMatch = True
                                        Exit For
                                    Else
                                        If intHeights(X + I, Y + intTemp1) = 0 Then
                                            bitNoMatch = True
                                            Exit For
                                        ElseIf intHeights(X + intTemp1, Y + I) = 0 Then
                                            bitNoMatch = True
                                            Exit For
                                        End If
                                    End If
                                Next
                                If bitNoMatch Then
                                    Exit For
                                Else
                                    intBlockSize = I - 1
                                End If
                            Next

                            If intBlockSize < 0 Then
                                intBlockSize = 0
                            End If

                            For intTemp1 = 0 To intBlockSize
                                For intTemp2 = 0 To intBlockSize
                                    bitBottomDone(X + intTemp1, Y + intTemp2) = True
                                Next
                            Next

                            intBlockSize += 1

                            'Below
                            objTriangleC += 1
                            With objTriangle
                                .intX1 = X * dblScaleX
                                .intY1 = Y * dblScaleY
                                .intZ1 = 0

                                .intX2 = X * dblScaleX
                                .intY2 = (Y + intBlockSize) * dblScaleY
                                .intZ2 = 0

                                .intX3 = (X + intBlockSize) * dblScaleX
                                .intY3 = (Y + intBlockSize) * dblScaleY
                                .intZ3 = 0
                            End With
                            WriteTriangle(objTriangle, objFile)

                            'Right
                            objTriangleC += 1
                            With objTriangle
                                .intX1 = X * dblScaleX
                                .intY1 = Y * dblScaleY
                                .intZ1 = 0

                                .intX2 = (X + intBlockSize) * dblScaleX
                                .intY2 = (Y + intBlockSize) * dblScaleY
                                .intZ2 = 0

                                .intX3 = (X + intBlockSize) * dblScaleX
                                .intY3 = Y * dblScaleY
                                .intZ3 = 0
                            End With
                            WriteTriangle(objTriangle, objFile)







                            ''Lets do a simple base...
                            'objTriangleC += 1
                            'With objTriangle
                            '    .intX1 = X * dblScaleX
                            '    .intY1 = Y * dblScaleY
                            '    .intZ1 = 0

                            '    .intX2 = X * dblScaleX
                            '    .intY2 = (Y - 1) * dblScaleY
                            '    .intZ2 = 0

                            '    .intX3 = (X - 1) * dblScaleX
                            '    .intY3 = (Y - 1) * dblScaleY
                            '    .intZ3 = 0
                            'End With
                            'WriteTriangle(objTriangle, objFile)

                            'objTriangleC += 1
                            'With objTriangle
                            '    .intX1 = X * dblScaleX
                            '    .intY1 = Y * dblScaleY
                            '    .intZ1 = 0

                            '    .intX2 = (X - 1) * dblScaleX
                            '    .intY2 = (Y - 1) * dblScaleY
                            '    .intZ2 = 0

                            '    .intX3 = (X - 1) * dblScaleX
                            '    .intY3 = Y * dblScaleY
                            '    .intZ3 = 0
                            'End With
                            'WriteTriangle(objTriangle, objFile)
                        End If
                    End If
                Next
            Next
        End If


        If bitBinMode Then
            WriteTriangleCount(objTriangleC, objFile)
        Else
            WriteByteArray("endsolid" & vbNewLine, objFile)
        End If

        Try
            objFile.Close()
        Catch ex As Exception

        End Try
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
                'WriteByteArray(.intXN, objFile)
                'WriteByteArray(.intYN, objFile)
                'WriteByteArray(.intZN, objFile)
                WriteByteArray(conEmptySingle, objFile)
                WriteByteArray(conEmptySingle, objFile)
                WriteByteArray(conEmptySingle, objFile)


                WriteByteArray(.intX1, objFile)
                WriteByteArray(.intY1, objFile)
                WriteByteArray(.intZ1, objFile)

                WriteByteArray(.intX2, objFile)
                WriteByteArray(.intY2, objFile)
                WriteByteArray(.intZ2, objFile)

                WriteByteArray(.intX3, objFile)
                WriteByteArray(.intY3, objFile)
                WriteByteArray(.intZ3, objFile)

                'WriteByteArray(.intABC, objFile)
                WriteByteArray(conEmptyUInt16, objFile)

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


    Sub WriteTriangleCount(ByRef intTriangleC As UInt32, ByRef objFile As IO.FileStream)
        Dim bytBytes As Byte() = BitConverter.GetBytes(intTriangleC)
        objFile.Seek(80, IO.SeekOrigin.Begin)
        For Each bytByte In bytBytes
            objFile.WriteByte(bytByte)
        Next
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

    Private Sub txtRez_TextChanged(sender As Object, e As EventArgs)
        bitUpdateNeeded = True
    End Sub

    Private Sub chkSpike_CheckedChanged(sender As Object, e As EventArgs) Handles chkSpike.CheckedChanged
        bitUpdateNeeded = True
        tbSpike.Enabled = chkSpike.Checked
        If Not chkSpike.Checked Then
            Me.Text = "Image To STL Converter"
        End If
    End Sub

    Private Sub chkBW_CheckedChanged(sender As Object, e As EventArgs) Handles chkBW.CheckedChanged
        bitUpdateNeeded = True
        tbBWTH.Enabled = chkBW.Checked
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        SetFormMode(False)
        Application.DoEvents()
        Dim strFile As String = ""
        Dim objOF As New Windows.Forms.OpenFileDialog
        objOF.Filter = "Images Files|*.png; *.jpg; *.gif; *.bmp; *.tif; *.tiff"
        Dim objResult As DialogResult = objOF.ShowDialog()
        If objResult = Windows.Forms.DialogResult.OK Then
            Try
                strFile = objOF.FileNames(0)
                LoadImage(strFile)
            Catch ex As Exception
                MessageBox.Show("Sorry, I was unable to load that file." & vbNewLine _
                                & "Error: " & ex.Message, "Error Loading File", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        SetFormMode(True)
    End Sub


    Sub SetFormMode(ByRef bitMode As Boolean)
        txtBase.Enabled = bitMode
        txtX.Enabled = bitMode
        txtY.Enabled = bitMode
        txtZ.Enabled = bitMode
        chkBW.Enabled = bitMode
        chkSpike.Enabled = bitMode
        chkAntiSpike.Enabled = bitMode
        chkLocked.Enabled = bitMode
        chkAlpha.Enabled = bitMode
        chkInvert.Enabled = bitMode
        tbBWTH.Enabled = bitMode
        cmdCreate.Enabled = bitMode
        cmdOpen.Enabled = bitMode
        If bitMode = True Then
            tbSpike.Enabled = chkSpike.Checked
            tbAntiSpike.Enabled = chkAntiSpike.Checked
        Else
            tbSpike.Enabled = bitMode
            tbAntiSpike.Enabled = bitMode
        End If
    End Sub

    Private Sub chkAlpha_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlpha.CheckedChanged
        bitUpdateNeeded = True
    End Sub

    Private Sub chkInvert_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvert.CheckedChanged
        bitUpdateNeeded = True
    End Sub

    Private Sub tbSpike_Scroll(sender As Object, e As EventArgs) Handles tbSpike.Scroll
        bitUpdateNeeded = True
    End Sub

    Private Sub chkAntiSpike_CheckedChanged(sender As Object, e As EventArgs) Handles chkAntiSpike.CheckedChanged
        bitUpdateNeeded = True
        tbAntiSpike.Enabled = chkAntiSpike.Checked
        If Not chkAntiSpike.Checked Then
            Me.Text = "Image To STL Converter"
        End If
    End Sub
End Class



