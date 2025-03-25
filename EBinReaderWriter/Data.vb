Option Explicit On
Option Infer On
Option Strict On
Imports Microsoft.VisualBasic.Logging

Public Class Data
    'just for example a few sample data
    Public i16 As Int16   ' 2
    Public ui16 As UInt16 ' 2
    Public i32 As Int32   ' 4
    Public ui32 As UInt32 ' 4
    Public sng As Single  ' 4 
    Public i64 As Int64   ' 8
    Public ui64 As UInt64 ' 8
    Public dbl As Double  ' 8
    '                 Sum: 40
    Public Shared Function DefaultData() As Data
        Dim d As New Data
        With d
            .i16 = &H4242 : .ui16 = &H3456 : .i32 = &H12345678 : .ui32 = &H23456789 : .sng = 123456.789!
            .i64 = &H12345678ABCDEF00 : .ui64 = &H12345678ABCDEF00 : .dbl = 12345678.12345678#
        End With
        Return d
    End Function
    Public Function IsEqual(other As Data) As Boolean
        If i16 <> other.i16 Then Return False
        If ui16 <> other.ui16 Then Return False
        If i32 <> other.i32 Then Return False
        If ui32 <> other.ui32 Then Return False
        If sng <> other.sng Then Return False
        If i64 <> other.i64 Then Return False
        If ui64 <> other.ui64 Then Return False
        If dbl <> other.dbl Then Return False
        Return True
    End Function
    Public Sub Write(bw As IO.BinaryWriter)
        bw.Write(i16) : bw.Write(ui16) : bw.Write(i32) : bw.Write(ui32)
        bw.Write(sng) : bw.Write(i64) : bw.Write(ui64) : bw.Write(dbl)
    End Sub
    Public Sub Read(br As IO.BinaryReader)
        i16 = br.ReadInt16 : ui16 = br.ReadUInt16 : i32 = br.ReadInt32 : ui32 = br.ReadUInt32
        sng = br.ReadSingle : i64 = br.ReadInt64 : ui64 = br.ReadUInt64 : dbl = br.ReadDouble
    End Sub
End Class

Public Class DataBAGZZlash
    Private Structure GenesteterUDT
        Public TestDouble As Double
    End Structure

    Private Structure TestUDT
        Public TestString As String
        Public TestLong As Integer
        Public NestUDT() As GenesteterUDT
    End Structure
    Dim MyDocVersion As Version
    Dim MyTest As TestUDT
    '-----------------
    Public Sub New()
        ' leave this version hardocded and do not change the version until any data-structure has actually changed!!!
        MyDocVersion = New Version(2025, 3, 14)
        ReDim MyTest.NestUDT(0 To 1)
        MyTest.NestUDT(0).TestDouble = 0.2
        MyTest.NestUDT(1).TestDouble = 0.4
        MyTest.TestLong = 123
        MyTest.TestString = "Dies ist ein Test!"
    End Sub
    Public Function IsEqual(other As DataBAGZZlash) As Boolean
        If MyTest.TestString <> other.Teststring Then Return False
        If MyTest.TestLong <> other.TestLong Then Return False
        If MyTest.NestUDT.Length <> other.NestUDT.Length Then Return False
        For i As Integer = 0 To MyTest.NestUDT.Length - 1
            If MyTest.NestUDT(i).TestDouble <> other.NestUDT(i).TestDouble Then Return False
        Next
        Return True
    End Function
    Public ReadOnly Property MyTestUDT As TestUDT
        Get() 
            Return MyTest
        End Get
    End Property

    Function Version_ToDate(V As Version) As Date
        Version_ToDate = DateSerial(V.Major, V.Minor, V.Revision)
    End Function
    Function Date_ToVersion(dt As Date) As Version
        Date_ToVersion = New Version(Year(dt), Month(dt), Day(dt))
    End Function
    Public Sub Write_V20250314(bw As IO.BinaryWriter)
        bw.Write(Version_ToDate(New Version(2025, 3, 14)))
        bw.Write(MyTest.TestString)
        bw.Write(MyTest.TestLong)
        bw.Write(MyTest.NestUDT.Length)
        Dim i As Integer
        For i = 0 To MyTest.NestUDT.Length - 1
            bw.Write(MyTest.NestUDT(i).TestDouble)
        Next
    End Sub
    Public Sub Read(br As IO.BinaryReader)
        'Dim dt As Date
        'Dim D As Double : D = br.ReadDouble()
        Dim dt As Double = br.ReadDouble()
        Dim v As Version = Date_ToVersion(dt)
        If v.Equals(New Version(2025, 3, 14)) Then
            Read_V20250314(br)
        End If
    End Sub
    Public Sub Read_V20250314(br As IO.BinaryReader)
        ' read wo version, version already read in "Sub Read"
        ' we never change this routine, it is only for reading data in the old version-format
        MyTest.TestString = br.ReadString()
        MyTest.TestLong = br.ReadInt32
        Dim l As Integer = br.ReadInt32
        ReDim MyTest.NestUDT(0 To l)
        For i As Integer = 0 To l - 1
            MyTest.NestUDT(i).TestDouble = br.ReadDouble
        Next
    End Sub

End Class
