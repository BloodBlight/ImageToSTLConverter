Imports CsGL.OpenGL
Imports CsGL.OpenGL.OpenGLControl

'Thie is the future home of some OpenGL code!


'Private Enum GLConstants
'    GL_COLOR_BUFFER_BIT = &H4000
'    GL_DEPTH_BUFFER_BIT = &H100
'    GL_SMOOTH = &H1D01
'    GL_DEPTH_TEST = &HB71
'    GL_LEQUAL = &H203
'    GL_PERSPECTIVE_CORRECTION_HINT = &HC50
'    GL_NICEST = &H1102
'    GL_PROJECTION = &H1701
'    GL_MODELVIEW = &H1701
'    GL_POLYGON = &H9
'End Enum


Namespace myOpenGL
    Public Class myView

    End Class
End Namespace

Public Class OpenGL


    Public Sub New()
        'MyBase.New()

        'Me.view = New myOpenGL.myView()
        'Me.view.Parent = Me
        'Me.view.Dock = DockStyle.Fill
        'Me.thrOpenGL = New Threading.Thread(AddressOf OpenGL_Start)
        'Me.thrOpenGL.Start()

        Dim objTest = New myOpenGL.myView()

    End Sub
End Class