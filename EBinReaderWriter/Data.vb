Option Explicit On
Option Infer On
Option Strict On
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