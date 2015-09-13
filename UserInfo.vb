Public Class UserInfo
    Dim field As Object = New Object
    Public Sub GetInfo()
        SyncLock field
            API.GETUSER(username, "USER_NAME", Main.License.Text)
            API.GETUSER(email, "USER_EMAIL", Main.License.Text)
            API.GETUSER(phone, "USER_PHONE", Main.License.Text)
            API.GETUSER(tou, "USER_TOU", Main.License.Text)
            API.GETUSER(days, "USER_LEFT", Main.License.Text)
            API.GETUSER(type, "USER_STATUS", Main.License.Text)
            API.GETUSER(ip, "USER_IP", Main.License.Text)
        End SyncLock
    End Sub
    Private Sub UserInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetInfo()
    End Sub
End Class
