Imports System.Threading
Public Class Main
    Dim field As Object = New Object
    Public Sub GetAppInfo()
        SyncLock field
            API.GETAPI(FormUI1, "APPLICATION_NAME")
            API.GETAPI(News, "APPLICATION_NEWS")
            API.GETAPI(LATEST_VERSION_XML, "APPLICATION_VERSION")
            API.GETAPI(APPLICATION_EMERGENCY, "APPLICATION_EMERGENCY")
            If APPLICATION_EMERGENCY.Text = 1 Then
                MsgBox("LOGIN IS RESTRICTED,TRY AGAIN LATER", MsgBoxStyle.Critical, "EMERGENCY MODE ON")
                End
            End If
            FormUI1.Version = "V " & LATEST_VERSION_XML.Text
            License.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\961API\", FormUI1.Text, "")
            Group.Text = "Login To " & FormUI1.Text
            Me.Text = FormUI1.Text
            HWID.Text = API.GetHWID
            If Not FormUI1.Text = "" Then
                Me.Show()
            End If
        End SyncLock
    End Sub
    Sub GenerateCryp()
        SyncLock field

            ID.Text = CreateRandomPassword(20)
            PASS1_D.Text = API.CreateRandomPassword1(32)
            PASS2_D.Text = API.CreateRandomPassword2(32)
            PASS3_D.Text = API.CreateRandomPassword3(32)
            PASS4_D.Text = API.CreateRandomPassword4(32)
            PASS1_E.Text = API.BASE64_Encode(PASS1_D.Text)
            PASS2_E.Text = API.BASE64_Encode(PASS2_D.Text)
            PASS3_E.Text = API.BASE64_Encode(PASS3_D.Text)
            PASS4_E.Text = API.BASE64_Encode(PASS4_D.Text)
        End SyncLock
    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        CheckForInternetConnection()
        GetAppInfo()
    End Sub

    Private Sub ACCESS_Click(sender As Object, e As EventArgs) Handles ACCESS.Click

        ACCESS.Enabled = False
        REGISTER.Enabled = False
        License.Enabled = False

        GenerateCryp()

        FormUI1.BottomText = ("Validating .... Please Wait ....")

        Dim A = BASE64_Encode(PASS1_D.Text)
        Dim B = BASE64_Encode(PASS2_D.Text)
        Dim C = BASE64_Encode(PASS3_D.Text)
        Dim D = BASE64_Encode(PASS4_D.Text)

        Select Case CheckUserAPI(License.Text)
            Case A
                ACCESS.Enabled = True
                REGISTER.Enabled = True
                License.Enabled = True
                FormUI1.BottomText = ("EXPIRED License")
            Case B
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\961API\", FormUI1.Text, License.Text)

                ACCESS.Enabled = True
                REGISTER.Enabled = True
                License.Enabled = True
                UserInfo.Show()
                FormUI1.BottomText = ("VALID License")
            Case C
                ACCESS.Enabled = True
                License.Enabled = True
                Register.Enabled = True
                ' LicenseBanned.Text = License.Text
                FormUI1.BottomText = ("BANNED License")
                'Group.Hide()
            Case D
                ACCESS.Enabled = True
                License.Enabled = True
                Register.Enabled = True

                FormUI1.BottomText = ("ACCESS DENIED")
            Case Else
                ACCESS.Enabled = True
                License.Enabled = True
                REGISTER.Enabled = True
                FormUI1.BottomText = "ERROR - 0002"
        End Select
    End Sub

    Private Sub Register_Click(sender As Object, e As EventArgs) Handles Register.Click
        If License.Text = Nothing Then
            MsgBox("Enter Your License")
        Else
            System.Diagnostics.Process.Start(API.WebPath & "/activatecode.php?PublicID=" & API.APPID & "&HWID=" & GetHWID() & "&LWEB=" & License.Text)
        End If
    End Sub

End Class
