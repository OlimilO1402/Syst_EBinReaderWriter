Option Explicit On
Option Infer On
Option Strict On
Imports System.IO
Public Class EndianBinaryWriter : Inherits BinaryWriter

    Private m_uni As UnionSBO
    Private ReadOnly m_bw As BinaryWriter

    Public Sub New(bw As BinaryWriter)
        MyBase.New(bw.BaseStream)
        m_bw = bw
    End Sub
    Public Shared Function DecideEndianWriter(ebw As EndianBinaryWriter, e As Endianness) As BinaryWriter
        Select Case e
            Case Endianness.IntelLittleEndian : Return If(BitConverter.IsLittleEndian, ebw.BaseWriter, ebw)
            Case Endianness.MotorolaBigEndian : Return If(BitConverter.IsLittleEndian, ebw, ebw.BaseWriter)
            Case Else : Return Nothing
        End Select
    End Function
    Public ReadOnly Property BaseWriter As BinaryWriter
        Get
            Return m_bw
        End Get
    End Property
    Public Overrides Sub Write(Value As Int16)
        m_uni.Int16Val = Value
        m_uni.B8.Swap_16()
        m_bw.Write(m_uni.Int16Val)
    End Sub
    Public Overrides Sub Write(Value As UInt16)
        m_uni.UInt16Val = Value
        m_uni.B8.Swap_16()
        m_bw.Write(m_uni.UInt16Val)
    End Sub
    Public Overrides Sub Write(Value As Int32)
        m_uni.Int32Val = Value
        m_uni.B8.Swap_32()
        m_bw.Write(m_uni.Int32Val)
    End Sub
    Public Overrides Sub Write(Value As UInt32)
        m_uni.UInt32Val = Value
        m_uni.B8.Swap_32()
        m_bw.Write(m_uni.UInt32Val)
    End Sub
    Public Overrides Sub Write(Value As Single)
        m_uni.SngVal = Value
        m_uni.B8.Swap_32()
        m_bw.Write(m_uni.SngVal)
    End Sub
    Public Overrides Sub Write(Value As Int64)
        m_uni.Int64Val = Value
        m_uni.B8.Swap_64()
        m_bw.Write(m_uni.Int64Val)
    End Sub
    Public Overrides Sub Write(Value As UInt64)
        m_uni.UInt64Val = Value
        m_uni.B8.Swap_64()
        m_bw.Write(m_uni.UInt64Val)
    End Sub
    Public Overrides Sub Write(Value As Double)
        m_uni.DblVal = Value
        m_uni.B8.Swap_64()
        m_bw.Write(m_uni.DblVal)
    End Sub
End Class