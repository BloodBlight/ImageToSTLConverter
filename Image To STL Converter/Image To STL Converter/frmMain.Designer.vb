<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.picSource = New System.Windows.Forms.PictureBox()
        Me.picDest = New System.Windows.Forms.PictureBox()
        Me.cmdCreate = New System.Windows.Forms.Button()
        Me.tbBWTH = New System.Windows.Forms.TrackBar()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBase = New System.Windows.Forms.TextBox()
        Me.txtZ = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkSpike = New System.Windows.Forms.CheckBox()
        Me.chkBW = New System.Windows.Forms.CheckBox()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.chkAlpha = New System.Windows.Forms.CheckBox()
        Me.chkInvert = New System.Windows.Forms.CheckBox()
        Me.tbSpike = New System.Windows.Forms.TrackBar()
        Me.tbAntiSpike = New System.Windows.Forms.TrackBar()
        Me.chkAntiSpike = New System.Windows.Forms.CheckBox()
        Me.txtRes = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.barStatus = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBWTH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSpike, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbAntiSpike, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picSource
        '
        Me.picSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSource.Image = CType(resources.GetObject("picSource.Image"), System.Drawing.Image)
        Me.picSource.InitialImage = Nothing
        Me.picSource.Location = New System.Drawing.Point(12, 12)
        Me.picSource.Name = "picSource"
        Me.picSource.Size = New System.Drawing.Size(486, 560)
        Me.picSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSource.TabIndex = 0
        Me.picSource.TabStop = False
        '
        'picDest
        '
        Me.picDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picDest.InitialImage = Nothing
        Me.picDest.Location = New System.Drawing.Point(504, 12)
        Me.picDest.Name = "picDest"
        Me.picDest.Size = New System.Drawing.Size(486, 560)
        Me.picDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDest.TabIndex = 2
        Me.picDest.TabStop = False
        '
        'cmdCreate
        '
        Me.cmdCreate.Location = New System.Drawing.Point(996, 549)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(158, 23)
        Me.cmdCreate.TabIndex = 3
        Me.cmdCreate.Text = "Create STL"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'tbBWTH
        '
        Me.tbBWTH.Location = New System.Drawing.Point(1003, 498)
        Me.tbBWTH.Maximum = 254
        Me.tbBWTH.Name = "tbBWTH"
        Me.tbBWTH.Size = New System.Drawing.Size(155, 45)
        Me.tbBWTH.TabIndex = 4
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Enabled = True
        Me.tmrUpdate.Interval = 200
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(999, 75)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(76, 20)
        Me.txtX.TabIndex = 6
        Me.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(1078, 75)
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(76, 20)
        Me.txtY.TabIndex = 7
        Me.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(996, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Target Size (X x Y in MM):"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1000, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Add Base (MM):"
        '
        'txtBase
        '
        Me.txtBase.Location = New System.Drawing.Point(1003, 192)
        Me.txtBase.Name = "txtBase"
        Me.txtBase.Size = New System.Drawing.Size(76, 20)
        Me.txtBase.TabIndex = 13
        Me.txtBase.Text = "1"
        Me.txtBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtZ
        '
        Me.txtZ.Location = New System.Drawing.Point(1003, 143)
        Me.txtZ.Name = "txtZ"
        Me.txtZ.Size = New System.Drawing.Size(76, 20)
        Me.txtZ.TabIndex = 16
        Me.txtZ.Text = "10"
        Me.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1000, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Target Height (MM):"
        '
        'chkLocked
        '
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Checked = True
        Me.chkLocked.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLocked.Location = New System.Drawing.Point(999, 101)
        Me.chkLocked.Name = "chkLocked"
        Me.chkLocked.Size = New System.Drawing.Size(90, 17)
        Me.chkLocked.TabIndex = 19
        Me.chkLocked.Text = "Locked Ratio"
        Me.chkLocked.UseVisualStyleBackColor = True
        '
        'chkSpike
        '
        Me.chkSpike.AutoSize = True
        Me.chkSpike.Checked = True
        Me.chkSpike.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSpike.Location = New System.Drawing.Point(999, 278)
        Me.chkSpike.Name = "chkSpike"
        Me.chkSpike.Size = New System.Drawing.Size(53, 30)
        Me.chkSpike.TabIndex = 20
        Me.chkSpike.Text = "Spike" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Filter"
        Me.chkSpike.UseVisualStyleBackColor = True
        '
        'chkBW
        '
        Me.chkBW.AutoSize = True
        Me.chkBW.Location = New System.Drawing.Point(999, 475)
        Me.chkBW.Name = "chkBW"
        Me.chkBW.Size = New System.Drawing.Size(138, 17)
        Me.chkBW.TabIndex = 21
        Me.chkBW.Text = "Enable B/W Threshold:"
        Me.chkBW.UseVisualStyleBackColor = True
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(996, 12)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(158, 23)
        Me.cmdOpen.TabIndex = 22
        Me.cmdOpen.Text = "Open Image"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'chkAlpha
        '
        Me.chkAlpha.AutoSize = True
        Me.chkAlpha.Checked = True
        Me.chkAlpha.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlpha.Location = New System.Drawing.Point(999, 408)
        Me.chkAlpha.Name = "chkAlpha"
        Me.chkAlpha.Size = New System.Drawing.Size(140, 17)
        Me.chkAlpha.TabIndex = 24
        Me.chkAlpha.Text = "Compute Alpha Channel"
        Me.chkAlpha.UseVisualStyleBackColor = True
        '
        'chkInvert
        '
        Me.chkInvert.AutoSize = True
        Me.chkInvert.Location = New System.Drawing.Point(999, 440)
        Me.chkInvert.Name = "chkInvert"
        Me.chkInvert.Size = New System.Drawing.Size(88, 17)
        Me.chkInvert.TabIndex = 25
        Me.chkInvert.Text = "Invert Output"
        Me.chkInvert.UseVisualStyleBackColor = True
        '
        'tbSpike
        '
        Me.tbSpike.LargeChange = 1
        Me.tbSpike.Location = New System.Drawing.Point(1078, 278)
        Me.tbSpike.Maximum = 6
        Me.tbSpike.Minimum = 2
        Me.tbSpike.Name = "tbSpike"
        Me.tbSpike.Size = New System.Drawing.Size(76, 45)
        Me.tbSpike.TabIndex = 26
        Me.tbSpike.Value = 4
        '
        'tbAntiSpike
        '
        Me.tbAntiSpike.LargeChange = 1
        Me.tbAntiSpike.Location = New System.Drawing.Point(1078, 312)
        Me.tbAntiSpike.Maximum = 6
        Me.tbAntiSpike.Minimum = 2
        Me.tbAntiSpike.Name = "tbAntiSpike"
        Me.tbAntiSpike.Size = New System.Drawing.Size(76, 45)
        Me.tbAntiSpike.TabIndex = 28
        Me.tbAntiSpike.Value = 4
        '
        'chkAntiSpike
        '
        Me.chkAntiSpike.AutoSize = True
        Me.chkAntiSpike.Checked = True
        Me.chkAntiSpike.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAntiSpike.Location = New System.Drawing.Point(999, 312)
        Me.chkAntiSpike.Name = "chkAntiSpike"
        Me.chkAntiSpike.Size = New System.Drawing.Size(74, 30)
        Me.chkAntiSpike.TabIndex = 27
        Me.chkAntiSpike.Text = "Anti-Spike" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Filter"
        Me.chkAntiSpike.UseVisualStyleBackColor = True
        '
        'txtRes
        '
        Me.txtRes.Location = New System.Drawing.Point(1003, 240)
        Me.txtRes.Name = "txtRes"
        Me.txtRes.Size = New System.Drawing.Size(76, 20)
        Me.txtRes.TabIndex = 30
        Me.txtRes.Text = "0.1"
        Me.txtRes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1000, 224)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Max Res (MM):"
        '
        'barStatus
        '
        Me.barStatus.Location = New System.Drawing.Point(504, 579)
        Me.barStatus.Maximum = 1000
        Me.barStatus.Name = "barStatus"
        Me.barStatus.Size = New System.Drawing.Size(486, 23)
        Me.barStatus.Step = 5
        Me.barStatus.TabIndex = 31
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(8, 579)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(123, 20)
        Me.lblStatus.TabIndex = 32
        Me.lblStatus.Text = "Status: Booting."
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(996, 579)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(158, 23)
        Me.cmdCancel.TabIndex = 33
        Me.cmdCancel.Text = "Cancel Export"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 610)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.barStatus)
        Me.Controls.Add(Me.txtRes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbAntiSpike)
        Me.Controls.Add(Me.chkAntiSpike)
        Me.Controls.Add(Me.tbSpike)
        Me.Controls.Add(Me.chkInvert)
        Me.Controls.Add(Me.chkAlpha)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.chkBW)
        Me.Controls.Add(Me.chkSpike)
        Me.Controls.Add(Me.chkLocked)
        Me.Controls.Add(Me.txtZ)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.tbBWTH)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.picDest)
        Me.Controls.Add(Me.picSource)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1176, 648)
        Me.MinimumSize = New System.Drawing.Size(1176, 648)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Image To STL Converter"
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBWTH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSpike, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbAntiSpike, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picSource As System.Windows.Forms.PictureBox
    Friend WithEvents picDest As System.Windows.Forms.PictureBox
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents tbBWTH As System.Windows.Forms.TrackBar
    Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBase As System.Windows.Forms.TextBox
    Friend WithEvents txtZ As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkLocked As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpike As System.Windows.Forms.CheckBox
    Friend WithEvents chkBW As System.Windows.Forms.CheckBox
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents chkAlpha As System.Windows.Forms.CheckBox
    Friend WithEvents chkInvert As System.Windows.Forms.CheckBox
    Friend WithEvents tbSpike As System.Windows.Forms.TrackBar
    Friend WithEvents tbAntiSpike As System.Windows.Forms.TrackBar
    Friend WithEvents chkAntiSpike As System.Windows.Forms.CheckBox
    Friend WithEvents txtRes As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents barStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button

End Class
