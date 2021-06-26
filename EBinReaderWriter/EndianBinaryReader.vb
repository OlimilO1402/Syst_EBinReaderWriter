Option Explicit On
Option Infer On
Option Strict On
Imports System.IO
Public Class EndianBinaryReader : Inherits BinaryReader

    Private m_uni As UnionSBO
    Private ReadOnly m_br As BinaryReader

    Public Sub New(br As IO.BinaryReader)
        MyBase.New(br.BaseStream)
        m_br = br
    End Sub
    Public Shared Function DecideEndianReader(ebr As EndianBinaryReader, e As Endianness) As BinaryReader
        Select Case e
            Case Endianness.IntelLittleEndian : Return If(BitConverter.IsLittleEndian, ebr.BaseReader, ebr)
            Case Endianness.MotorolaBigEndian : Return If(BitConverter.IsLittleEndian, ebr, ebr.BaseReader)
            Case Else : Return Nothing
        End Select
    End Function
    Public ReadOnly Property BaseReader As BinaryReader
        Get
            Return m_br
        End Get
    End Property
    Public Overrides Function ReadInt16() As Short
        m_uni.Int16Val = m_br.ReadInt16
        m_uni.B8.Swap_16()
        Return m_uni.Int16Val
    End Function
    Public Overrides Function ReadUInt16() As UInt16
        m_uni.UInt16Val = m_br.ReadUInt16
        m_uni.B8.Swap_16()
        Return m_uni.UInt16Val
    End Function
    Public Overrides Function ReadInt32() As Int32
        m_uni.Int32Val = m_br.ReadInt32
        m_uni.B8.Swap_32()
        Return m_uni.Int32Val
    End Function
    Public Overrides Function ReadUInt32() As UInt32
        m_uni.UInt32Val = m_br.ReadUInt32
        m_uni.B8.Swap_32()
        Return m_uni.UInt32Val
    End Function
    Public Overrides Function ReadSingle() As Single
        m_uni.SngVal = m_br.ReadSingle
        m_uni.B8.Swap_32()
        Return m_uni.SngVal
    End Function
    Public Overrides Function ReadInt64() As Int64
        m_uni.Int64Val = m_br.ReadInt64
        m_uni.B8.Swap_64()
        Return m_uni.Int64Val
    End Function
    Public Overrides Function ReadUInt64() As UInt64
        m_uni.UInt64Val = m_br.ReadUInt64
        m_uni.B8.Swap_64()
        Return m_uni.UInt64Val
    End Function
    Public Overrides Function ReadDouble() As Double
        m_uni.DblVal = m_br.ReadDouble
        m_uni.B8.Swap_64()
        Return m_uni.DblVal
    End Function
End Class