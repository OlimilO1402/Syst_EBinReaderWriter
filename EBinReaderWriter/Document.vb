Option Explicit On
Option Infer On
Option Strict On
Imports System.IO
Public Class Document
    Private ReadOnly m_pfn As String
    Private m_en As Endianness
    Public Data As Data
    Public Sub New(aPFN As String, en As Endianness, aData As Data)
        m_pfn = aPFN : m_en = en : Data = aData
    End Sub
    Public Shared Function ReadFileToStr(aPFN As String) As String
        Dim fs As New FileStream(aPFN, FileMode.Open)
        Dim b(CInt(fs.Length) - 1) As Byte
        fs.Read(b, 0, CInt(fs.Length))
        Dim sb As New Text.StringBuilder
        For i As Integer = 0 To b.Length - 1
            sb.Append(b(i).ToString("x2"))
            If i < b.Length - 1 Then sb.Append(" ")
            If (0 < i) And (((i + 1) Mod 16) = 0) Then sb.AppendLine()
        Next
        fs.Close()
        Return sb.ToString
    End Function
    Public Function IsEqual(other As Document) As Boolean
        Return Data.IsEqual(other.Data)
    End Function
    Public Sub Write()
        Dim fs As New FileStream(m_pfn, FileMode.OpenOrCreate)
        Dim bw As New BinaryWriter(fs)
        bw.Write(m_en)
        Dim ebw As New EndianBinaryWriter(bw)
        bw = EndianBinaryWriter.DecideEndianWriter(ebw, m_en)
        Data.Write(bw)
        fs.Close()
    End Sub
    Public Sub Read()
        Dim fs As New FileStream(m_pfn, FileMode.OpenOrCreate)
        Dim br As New BinaryReader(fs)
        m_en = Endianness_Parse(br.ReadUInt16)
        Dim ebr As New EndianBinaryReader(br)
        br = EndianBinaryReader.DecideEndianReader(ebr, m_en)
        Data.Read(br)
        fs.Close()
    End Sub
    Private Function Endianness_Parse(s As String) As Endianness
        Return If(s.Substring(1).ToUpper() = "I", Endianness.IntelLittleEndian, Endianness.MotorolaBigEndian)
    End Function
    Private Function Endianness_Parse(Value As UInt16) As Endianness
        Return If(Value = &H4949, Endianness.IntelLittleEndian, Endianness.MotorolaBigEndian)
    End Function
End Class
