Option Explicit On
Option Infer On
Option Strict On
Public Enum Endianness As UInt16
    IntelLittleEndian = &H4949 ' = "II"
    MotorolaBigEndian = &H4D4D ' = "MM"
End Enum
Public Structure B8
    Private B0 As Byte 'Byte 7 of 8
    Private B1 As Byte 'Byte 6 of 8
    Private B2 As Byte 'Byte 5 of 4,8
    Private B3 As Byte 'Byte 4 of 2,4,8
    Private B4 As Byte 'Byte 3 of 2,4,8
    Private B5 As Byte 'Byte 2 of 4,8
    Private B6 As Byte 'Byte 1 of 8
    Private B7 As Byte 'Byte 0 of 8
    Public Sub Swap_16()
        'swap 2 Bytes
        Dim tmp As Byte
        tmp = B3 : B3 = B4 : B4 = tmp
    End Sub
    Public Sub Swap_32()
        'swap 4 Bytes
        Dim tmp As Byte
        tmp = B3 : B3 = B4 : B4 = tmp
        tmp = B2 : B2 = B5 : B5 = tmp
    End Sub
    Public Sub Swap_64()
        'swap 8 Bytes
        Dim tmp As Byte
        tmp = B3 : B3 = B4 : B4 = tmp
        tmp = B2 : B2 = B5 : B5 = tmp
        tmp = B1 : B1 = B6 : B6 = tmp
        tmp = B0 : B0 = B7 : B7 = tmp
    End Sub
    Public Overrides Function ToString() As String
        'just for debugging reasons
        Dim sb As New Text.StringBuilder
        sb.Append(B0.ToString("X2")).Append(" ")
        sb.Append(B1.ToString("X2")).Append(" ")
        sb.Append(B2.ToString("X2")).Append(" ")
        sb.Append(B3.ToString("X2")).Append(" ")
        sb.Append(B4.ToString("X2")).Append(" ")
        sb.Append(B5.ToString("X2")).Append(" ")
        sb.Append(B6.ToString("X2")).Append(" ")
        sb.Append(B7.ToString("X2"))
        Return sb.ToString
    End Function
End Structure
<System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)>
Public Structure UnionSBO
    <System.Runtime.InteropServices.FieldOffset(0)>
    Public B8 As B8

    <System.Runtime.InteropServices.FieldOffset(0)>
    Public Int64Val As Int64

    <System.Runtime.InteropServices.FieldOffset(0)>
    Public UInt64Val As UInt64

    <System.Runtime.InteropServices.FieldOffset(0)>
    Public DblVal As Double

    <System.Runtime.InteropServices.FieldOffset(2)>
    Public Int32Val As Int32
    <System.Runtime.InteropServices.FieldOffset(2)>
    Public UInt32Val As UInt32
    <System.Runtime.InteropServices.FieldOffset(2)>
    Public SngVal As Single
    <System.Runtime.InteropServices.FieldOffset(3)>
    Public Int16Val As Int16
    <System.Runtime.InteropServices.FieldOffset(3)>
    Public UInt16Val As UInt16
End Structure
