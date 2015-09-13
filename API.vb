Imports System.Net
Imports System.IO
Imports System.Management
Imports System
Imports System.Web
Imports System.Security.Cryptography
Imports System.Base64FormattingOptions
Imports System.Security.Cryptography.ToBase64Transform
Imports System.Text
Imports System.Xml
Imports System.Xml.XPath



Module API
    '##############################YOUR APP INFO#########################################
    Public WebPath As String = "https://961API.com/"             '#
    Public API As String = "L72LP2C2TX760JD6EEI2" '                                    '#
    Public APPID As String = "882071973256"                                            '#
    Public SALTID As String = "a2178248a148e8b2242139d4ffe284bcf4704315"               '#
    Public Valid As String = "fc9adbd656e2e935d4b260c6a7a55941aa095366"                '#
    Public Expired As String = "423dabf3d97e7a9b79f8c5e323677a7fc729e2f5"              '#
    Public Banned As String = "7d8f597b2c5dbcb55ac4fa658d4b92e39132e7fc"               '#
    Public Invalid As String = "058ad46deae6191eddf1258d5faf998d9cb3f925"              '#
    Public PVerison As String = "1" ' this app version                                 '# 
    '####################################################################################

#Region "HWID"

    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                client.Proxy = Nothing
                Using stream = client.OpenRead("https://961API.com/400.shtml")

                End Using
            End Using
        Catch
            MsgBox("ERROR 0001 : CONNECTION TO SERVER")
            End

        End Try

    End Function
    Public Function GetHWID() As String

        Dim HWID As String = BASE64_Encode(GetCPUID())
        If HWID.Contains(" ") Then HWID = HWID.Replace(" ", "")
        Return Convert.ToBase64String(New System.Text.ASCIIEncoding().GetBytes(HWID))
    End Function
    Private Function GetCPUID()
        Dim cpuInfo As String = String.Empty
        Dim mc As New ManagementClass("win32_processor")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        For Each mo As ManagementObject In moc
            cpuInfo = mo.Properties("processorID").Value.ToString()
        Next
        Return cpuInfo
    End Function
    Public Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Return sBuilder.ToString()
    End Function
    Public Function BASE64_Encode(ByVal input As String) As String
        Return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(input))
    End Function
    Public Function BASE64_Decode(ByVal Input As String)
        Dim B As Byte() = System.Convert.FromBase64String(Input)
        Return System.Text.Encoding.UTF8.GetString(B)
    End Function

#End Region

#Region "Password"
    Public Function CreateRandomPassword(ByVal PasswordLength As Integer) As String
        Dim _allowedChars As String = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789"
        Dim randomNumber As New Random()
        Dim chars(PasswordLength - 1) As Char
        Dim allowedCharCount As Integer = _allowedChars.Length
        For i As Integer = 0 To PasswordLength - 1
            chars(i) = _allowedChars.Chars(CInt(Fix((_allowedChars.Length) * randomNumber.NextDouble())))
        Next i
        Return New String(chars)
    End Function
    Public Function CreateRandomPassword1(ByVal PasswordLength As Integer) As String
        Dim _allowedChars As String = "RzBFQ0xvZk1mcjRZMk9MMmhKbFJGNHpvZE5OSVNP"
        Dim randomNumber As New Random()
        Dim chars(PasswordLength - 1) As Char
        Dim allowedCharCount As Integer = _allowedChars.Length
        For i As Integer = 0 To PasswordLength - 1
            chars(i) = _allowedChars.Chars(CInt(Fix((_allowedChars.Length) * randomNumber.NextDouble())))
        Next i
        Return New String(chars)
    End Function
    Public Function CreateRandomPassword2(ByVal PasswordLength As Integer) As String
        Dim _allowedChars As String = "RVNyRnJZUnloak1oTVRFaGlidUpJVEV3SVJiekdS"
        Dim randomNumber As New Random()
        Dim chars(PasswordLength - 1) As Char
        Dim allowedCharCount As Integer = _allowedChars.Length
        For i As Integer = 0 To PasswordLength - 1
            chars(i) = _allowedChars.Chars(CInt(Fix((_allowedChars.Length) * randomNumber.NextDouble())))
        Next i
        Return New String(chars)
    End Function
    Public Function CreateRandomPassword3(ByVal PasswordLength As Integer) As String
        Dim _allowedChars As String = "UzlQVmxGQ1RSb1dmWDVUOUdnWnRISFZJMGhrOWNB"
        Dim randomNumber As New Random()
        Dim chars(PasswordLength - 1) As Char
        Dim allowedCharCount As Integer = _allowedChars.Length
        For i As Integer = 0 To PasswordLength - 1
            chars(i) = _allowedChars.Chars(CInt(Fix((_allowedChars.Length) * randomNumber.NextDouble())))
        Next i
        Return New String(chars)
    End Function
    Public Function CreateRandomPassword4(ByVal PasswordLength As Integer) As String
        Dim _allowedChars As String = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789"
        Dim randomNumber As New Random()
        Dim chars(PasswordLength - 1) As Char
        Dim allowedCharCount As Integer = _allowedChars.Length
        For i As Integer = 0 To PasswordLength - 1
            chars(i) = _allowedChars.Chars(CInt(Fix((_allowedChars.Length) * randomNumber.NextDouble())))
        Next i
        Return New String(chars)
    End Function
#End Region

    Public Sub GETAPI(ByVal APP As Control, ByVal VALUE As String)
        Dim remoteURL = (WebPath & "/API/API.php?PROTECTION=APPAPI&API=" & API)
        Dim remoteText As String = Nothing
        Using WC As New System.Net.WebClient()
            Dim theProxy As IWebProxy = WebRequest.DefaultWebProxy
            theProxy.Credentials = CredentialCache.DefaultCredentials
            Dim proxy As IWebProxy = Nothing
            WC.Proxy = theProxy
            WC.Proxy = Nothing
            remoteText = WC.DownloadString(remoteURL)
        End Using

        If String.IsNullOrWhiteSpace(remoteText) Then
            Throw New ApplicationException("NO DATA RETURNED FROM SERVER.0001 ERROR.")
        End If
        Try
            Dim X = XDocument.Parse((remoteText))

            APP.Text = BASE64_Decode(X.XPathSelectElement("/API/" & (VALUE)).Value)

        Catch Ex As Exception
            MsgBox("API ERROR", MsgBoxStyle.Exclamation, ".0002 ERROR.")

        End Try
    End Sub
    Public Sub GETUSER(ByVal APP As Control, ByVal VALUE As String, ByVal CODE As String)
        Dim remoteURL = (WebPath & "/API/API.php?PROTECTION=USERAPI&API=" & API & "&CODE=" & CODE)
        Dim remoteText As String = Nothing
        Using WC As New System.Net.WebClient()
            Dim theProxy As IWebProxy = WebRequest.DefaultWebProxy
            theProxy.Credentials = CredentialCache.DefaultCredentials
            Dim proxy As IWebProxy = Nothing
            WC.Proxy = theProxy
            remoteText = WC.DownloadString(remoteURL)
        End Using

        If String.IsNullOrWhiteSpace(remoteText) Then
            Throw New ApplicationException("NO DATA RETURNED FROM SERVER.0003 ERROR.")
        End If
        Try
            Dim X = XDocument.Parse(remoteText)
            APP.Text = BASE64_Decode(X.XPathSelectElement("/API/" & (VALUE)).Value)
        Catch Ex As Exception
            MsgBox("API ERROR", MsgBoxStyle.Exclamation, ".0004 ERROR.")

        End Try
    End Sub

    Public Function CheckUserAPI(ByVal Code As String) As String

        Dim Answer1, Answer2, Answer3, Answer4 As String
        Using md5Hash As MD5 = MD5.Create()

            Answer1 = GetMd5Hash(md5Hash, (SALTID) & (Expired) & GetHWID() & Main.ID.Text) 'Expired Code! 
            Answer2 = GetMd5Hash(md5Hash, (SALTID) & (Valid) & GetHWID() & Main.ID.Text) 'Valid Code! 
            Answer3 = GetMd5Hash(md5Hash, (SALTID) & (Banned) & GetHWID() & Main.ID.Text) 'Account Banned! 
            Answer4 = GetMd5Hash(md5Hash, (SALTID) & (Invalid) & GetHWID() & Main.ID.Text) 'Invalid HWID for this Code! 
        End Using
        Dim GET_Data As String = (WebPath & "/API/CHECKCODEAPI.php?PROTECTION=APICHECK&API=" & API & "&CODE=" & Code & "&HWID=" & GetHWID() & "&S=" & Main.ID.Text)
        Try

            Dim WebReq As HttpWebRequest = HttpWebRequest.Create(GET_Data)
            WebReq.Proxy = Nothing
            Using WebRes As HttpWebResponse = WebReq.GetResponse
                Using Reader As New StreamReader(WebRes.GetResponseStream)
                    Dim Str As String = Reader.ReadLine

                    Select Case True
                        Case Str.Contains(Answer1) 'Expired Code!

                            Return Main.PASS1_E.Text
                        Case Str.Contains(Answer2) 'Valid Code!

                            Return Main.PASS2_E.Text
                        Case Str.Contains(Answer3) 'Account Banned!

                            Return Main.PASS3_E.Text
                        Case Str.Contains(Answer4) 'Invalid HWID!

                            Return Main.PASS4_E.Text
                        Case Else
                            Return "ERROR - Invalid API"
                    End Select
                End Using
            End Using
        Catch Ex As Exception
            MsgBox("Unable to contact server, Please try again later!", MsgBoxStyle.Exclamation, "Error")
            Return "Invalid"
        End Try
    End Function

End Module
