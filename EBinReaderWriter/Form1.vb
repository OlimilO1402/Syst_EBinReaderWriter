Option Explicit On
Option Infer On
Option Strict On
Public Class Form1
    Private m_PFN As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        m_PFN = Application.ExecutablePath
        Dim Doc1 = New Document(m_PFN & "1.bin", Endianness.IntelLittleEndian, Data.DefaultData)
        Dim Doc2 = New Document(m_PFN & "2.bin", Endianness.MotorolaBigEndian, Data.DefaultData)
        Doc1.Write()
        Doc2.Write()
        UpdateView()
    End Sub
    Sub UpdateView()
        TextBox1.Text = Document.ReadFileToStr(m_PFN & "1.bin")
        TextBox2.Text = Document.ReadFileToStr(m_PFN & "2.bin")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Doc1 = New Document(m_PFN & "1.bin", Endianness.IntelLittleEndian, New Data)
        Dim Doc2 = New Document(m_PFN & "2.bin", Endianness.MotorolaBigEndian, New Data)
        Doc1.Read()
        Doc2.Read()
        MessageBox.Show(Doc1.Data.IsEqual(Doc2.Data).ToString)
    End Sub
End Class






