Public Class DataManage
    Inherits ArrayList

    Private year As Decimal
    Private month As Decimal
    Private day As Decimal
    Private hour As Decimal
    Private minute As Decimal

    'constructors
    Public Sub New(year As Decimal, month As Decimal, day As Decimal, hour As Decimal, minute As Decimal, text As String, alarm As Boolean)
        Me.year = year
        Me.month = month
        Me.day = day
        Me.hour = hour
        Me.minute = minute
        Me.Text = text
        Active = alarm
    End Sub

    Public Sub New(year As Decimal, month As Decimal, day As Decimal)
        Me.year = year
        Me.month = month
        Me.day = day
    End Sub

    Public Sub New(hour As Decimal, minute As Decimal)
        Me.hour = hour
        Me.minute = minute
    End Sub

    'properties
    Public Property YearMonthDay As Decimal()
        Get
            Return New Decimal() {year, month, day}
        End Get
        Set(value As Decimal())
            year = value(0)
            month = value(1)
            day = value(2)
        End Set
    End Property

    Public Property YearMonth As Decimal()
        Get
            Return New Decimal() {year, month}
        End Get
        Set(value As Decimal())
            year = value(0)
            month = value(1)
        End Set
    End Property

    Public Property HourMinute As Decimal()
        Get
            Return New Decimal() {hour, minute}
        End Get
        Set(value As Decimal())
            hour = value(0)
            minute = value(1)
        End Set
    End Property

    Public Property Text As String
    Public Property Active As Boolean
End Class