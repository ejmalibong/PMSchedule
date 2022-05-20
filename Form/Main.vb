Imports System.Data.SqlClient
Imports System.Deployment.Application
Imports System.Globalization
Imports BlackCoffeeLibrary
Imports MachineMonitoringSystem

Public Class Main
    Private dbConnection As New Connection
    Private dbMethod As New SqlDbMethod(dbConnection.GetConnectionString)
    Private dbMain As New BlackCoffeeLibrary.Main
    'accomplished pm activity
    Private pnlCalendar As Panel()
    Private lblCalendar As Label()
    Private lsvCalendar As ListView()
    'pending pm activity
    Private pnlScheduled As Panel()
    Private lblScheduled As Label()
    Private lsvScheduled As ListView()

    Private lsvIndex As Integer
    Private selectedDate As Decimal()

    Private dicType As New Dictionary(Of String, Integer)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dicType.Add(" Machine", 1)
        dicType.Add(" Jig", 2)
        cmbView.DisplayMember = "Key"
        cmbView.ValueMember = "Value"
        cmbView.DataSource = New BindingSource(dicType, Nothing)

        cmbView.SelectedValue = 1 'default view to machine schedule

        'setting current date/default date
        Dim currentDate As Date = CDate(dbMethod.GetServerDate)
        Dim formattedDate = currentDate.ToString("yyyy-MM-dd").Split("-"c)
        selectedDate = New Decimal() {Decimal.Parse(formattedDate(0)), Decimal.Parse(formattedDate(1)), Decimal.Parse(formattedDate(2))}

        Dim percentCol = 100.0F / CSng(7)
        Dim percentRow = 100.0F / CSng(6)

        'accomplished pm
        pnlCalendar = New Panel(42) {}
        lblCalendar = New Label(42) {}
        lsvCalendar = New ListView(42) {}

        For count As Integer = 0 To pnlCalendar.Length - 1
            pnlCalendar(count) = New Panel
        Next

        For count As Integer = 0 To lblCalendar.Length - 1
            lblCalendar(count) = New Label()
        Next

        For count As Integer = 0 To lsvCalendar.Length - 1
            lsvCalendar(count) = New ListView()
            lsvCalendar(count).Columns.Add("Name")
            lsvCalendar(count).Columns.Add("Id")
            lsvCalendar(count).Columns.Add("TrxId")

            lsvCalendar(count).Columns.Item(1).Width = 0
            lsvCalendar(count).Columns.Item(2).Width = 0
            lsvCalendar(count).HeaderStyle = ColumnHeaderStyle.None
            lsvCalendar(count).AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            lsvCalendar(count).View = View.SmallIcon
            lsvCalendar(count).Sorting = Windows.Forms.SortOrder.Ascending
            lsvCalendar(count).FullRowSelect = True

            AddHandler lsvCalendar(count).DoubleClick, New EventHandler(AddressOf CalendarDoubleClick)
        Next

        'scheduled colum
        pnlScheduled = New Panel(6) {}
        lblScheduled = New Label(6) {}
        lsvScheduled = New ListView(6) {}

        For count As Integer = 0 To pnlScheduled.Length - 1
            pnlScheduled(count) = New Panel
        Next

        For count As Integer = 0 To lblScheduled.Length - 1
            lblScheduled(count) = New Label()
        Next

        For count As Integer = 0 To lsvScheduled.Length - 1
            lsvScheduled(count) = New ListView()
            lsvScheduled(count).Font = New Font("Segoe UI", 9.5, FontStyle.Regular)
            lsvScheduled(count).Columns.Add("Name", HorizontalAlignment.Left)
            lsvScheduled(count).Columns.Add("Id", HorizontalAlignment.Left)
            lsvScheduled(count).Columns.Add("TrxId", HorizontalAlignment.Left)

            lsvScheduled(count).Columns.Item(1).Width = 0
            lsvScheduled(count).Columns.Item(2).Width = 0
            lsvScheduled(count).HeaderStyle = ColumnHeaderStyle.None
            lsvScheduled(count).AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            lsvScheduled(count).View = View.SmallIcon
            lsvScheduled(count).Sorting = Windows.Forms.SortOrder.Ascending

            AddHandler lsvScheduled(count).ItemSelectionChanged, AddressOf LsvScheduled_ItemSelectionChanged
        Next

        Dim indexCalendar As Integer = 0
        Dim indexScheduled As Integer = 0

        For row As Integer = 0 To tlpCalendar.RowCount - 1
            For col As Integer = 0 To tlpCalendar.ColumnCount - 1
                If col = 0 Then 'default size of scheduled column (first column)
                    tlpCalendar.ColumnStyles(0).SizeType = SizeType.Absolute
                    tlpCalendar.ColumnStyles(0).Width = 200
                Else
                    tlpCalendar.ColumnStyles(col).Width = percentCol
                End If

                If row = 0 Then 'default size of row headers (first row)
                    tlpCalendar.RowStyles(0).SizeType = SizeType.Absolute
                    tlpCalendar.RowStyles(0).Height = 30
                Else
                    tlpCalendar.RowStyles(row).Height = percentRow
                End If

                If col <> 0 AndAlso row <> 0 Then
                    Me.tlpCalendar.Controls.Add(pnlCalendar(indexCalendar), col, row)
                    Dim propPnlPadding As Padding = pnlCalendar(indexCalendar).Margin
                    With pnlCalendar(indexCalendar)
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Fill
                        .Margin = New Padding(0)
                        .Name = "pnlCalendar" & indexCalendar + 1
                        .Tag = indexCalendar + 1
                    End With

                    Me.pnlCalendar(indexCalendar).Controls.Add(lblCalendar(indexCalendar))
                    Dim propLblPadding As Padding = lblCalendar(indexCalendar).Margin
                    With lblCalendar(indexCalendar)
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Top
                        .AutoSize = True
                        .Margin = New Padding(0)
                        .Name = "lblCalendar" & indexCalendar + 1
                        .Tag = indexCalendar + 1
                    End With

                    Me.pnlCalendar(indexCalendar).Controls.Add(lsvCalendar(indexCalendar))
                    Dim propLsvPadding As Padding = lsvCalendar(indexCalendar).Margin
                    With lsvCalendar(indexCalendar)
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Fill
                        .Margin = New Padding(0)
                        .Name = "lblLsvCalendar" & indexCalendar + 1
                        .Tag = indexCalendar + 1
                        .BringToFront()
                    End With

                    indexCalendar += 1
                End If

                'pm scheduled column
                If col = 0 AndAlso row <> 0 Then
                    Me.tlpCalendar.Controls.Add(pnlScheduled(indexScheduled), col, row)
                    Dim propPnlPadding As Padding = pnlScheduled(indexScheduled).Margin
                    With pnlScheduled(indexScheduled)
                        .BackColor = Color.Orange
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Fill
                        .Margin = New Padding(0)
                        .Name = "lblLstScheduled" & indexScheduled + 1
                        .Tag = indexScheduled + 1
                    End With

                    Me.pnlScheduled(indexScheduled).Controls.Add(lblScheduled(indexScheduled))
                    Dim propLblPadding As Padding = lblScheduled(indexScheduled).Margin
                    With lblScheduled(indexScheduled)
                        .BackColor = Color.Orange
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Top
                        .AutoSize = True
                        .Margin = New Padding(0)
                        .Name = "lblScheduled" & indexScheduled + 1
                        .Tag = indexScheduled + 1
                    End With

                    Me.pnlScheduled(indexScheduled).Controls.Add(lsvScheduled(indexScheduled))
                    Dim propLsvPadding As Padding = lsvScheduled(indexScheduled).Margin
                    With lsvScheduled(indexScheduled)
                        .BackColor = Color.Orange
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Fill
                        .Margin = New Padding(0)
                        .Name = "lblLsvScheduled" & indexScheduled + 1
                        .Tag = indexScheduled + 1
                        .BringToFront()
                    End With

                    indexScheduled += 1
                End If
            Next
        Next

        lblYearMonth.Text = MonthName(selectedDate(1), True) & " " & selectedDate(0)
        ChangeCalendar()
        dbMethod.FillCmbWithCaption("RdGenMonth", CommandType.StoredProcedure, "MonthId", "MonthName", cmbMonth, "")
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler Me.SizeChanged, AddressOf frmMain_SizeEventHandler
        Me.MaximizeBox = False

        If ApplicationDeployment.IsNetworkDeployed Then
            Me.Text = " PM Calendar - ver. " & ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            Me.Text = " PM Calendar - ver. " & Application.ProductVersion.ToString
        End If

        AddHandler cmbView.SelectedValueChanged, AddressOf cmbView_SelectedValueChanged

        tmrMain.Start()
        Me.ActiveControl = cmbMonth
    End Sub

    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        If Minute(dbMethod.GetServerDate).Equals(30) AndAlso Second(dbMethod.GetServerDate).Equals(0) Then
            tmrMain.Enabled = False
            Me.SuspendLayout()
            Me.ChangeCalendar()
            Me.ResumeLayout()
            tmrMain.Enabled = True
        End If
    End Sub

    Private Sub cmbView_SelectedValueChanged(sender As Object, e As EventArgs)
        Me.SuspendLayout()
        Me.ChangeCalendar()
        Me.ResumeLayout()
    End Sub

    Private Sub LsvScheduled_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs)
        Try
            If e.IsSelected Then
                e.Item.Selected = False
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'listview double click event
    Private Sub CalendarDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lsv = CType(sender, ListView)
            Dim trxId As Integer = lsv.Items(lsv.FocusedItem.Index).SubItems(2).Text

            If cmbView.SelectedValue = 1 Then
                Dim frmDetail As New MachineMonitoringSystem.MntTrxDetailMch(0, 0, 0, trxId)
                frmDetail.fromPmCalendar = True
                frmDetail.ShowDialog()
            Else
                Dim frmDetail As New MachineMonitoringSystem.MntTrxDetailJig(0, 0, 0, trxId)
                frmDetail.fromPmCalendar = True
                frmDetail.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If cmbMonth.SelectedValue = 0 Then
            Me.ActiveControl = cmbMonth
            Return
        End If

        Me.SuspendLayout()

        Dim cval As Integer = CInt(selectedDate(2))
        Dim temp As Integer = CInt(selectedDate(2))

        Dim maxDays As Integer = Integer.Parse(Date.DaysInMonth(txtYear.Text, cmbMonth.SelectedValue).ToString())
        selectedDate(2) = CInt(CDate(DateSerial(txtYear.Text, cmbMonth.SelectedValue, maxDays)).ToString("dd"))

        If Integer.Parse(txtYear.Text.Trim) = selectedDate(0) AndAlso Integer.Parse(cmbMonth.SelectedValue) = selectedDate(1) Then
            cval = CInt(selectedDate(2)) - cval
            If cval = 0 Then Return
            Dim sat_index As Integer() = {7, 15, 23, 31, 39, 47}
            Dim sun_index As Integer() = {8, 16, 24, 32, 40}
            Dim correct_sat_sun_index As Boolean = False

            '(blue) 192 192 255 sat
            For Each index In sat_index
                If lsvIndex = index Then
                    lsvCalendar(index).BackColor = Drawing.Color.FromArgb(192, 192, 255)
                    correct_sat_sun_index = True
                    Exit For
                End If
            Next

            '(red) 255 192 192 sun
            If Not correct_sat_sun_index Then
                For Each index In sun_index
                    If lsvIndex = index Then
                        lsvCalendar(index).BackColor = Drawing.Color.FromArgb(255, 192, 192)
                        correct_sat_sun_index = True
                        Exit For
                    End If
                Next
            End If

            '(white) weekdays
            If Not correct_sat_sun_index Then lsvCalendar(lsvIndex).BackColor = Drawing.SystemColors.Window

            '(skyblue) 192 255 255 current date
            If Equals(CDate(dbMethod.GetServerDate).ToString("yyyy-MM-dd"), (selectedDate(0).ToString() & "-" & selectedDate(1).ToString("00") & "-" & temp.ToString("00"))) Then lsvCalendar(lsvIndex).BackColor = Drawing.Color.FromArgb(192, 255, 255)

            'selected cell
            lsvIndex = lsvIndex + cval
            lsvCalendar(lsvIndex).BackColor = Drawing.Color.FromArgb(255, 255, 192)
        Else
            selectedDate(0) = txtYear.Text
            selectedDate(1) = cmbMonth.SelectedValue
            ChangeCalendar()
        End If

        lblYearMonth.Text = MonthName(cmbMonth.SelectedValue, True) & " " & txtYear.Text

        Me.ResumeLayout()
    End Sub

    Private Sub btnCurrent_Click(sender As Object, e As EventArgs) Handles btnCurrent.Click
        selectedDate(0) = Year(dbMethod.GetServerDate)
        selectedDate(1) = Month(dbMethod.GetServerDate)
        cmbMonth.SelectedValue = 0
        txtYear.Text = Year(dbMethod.GetServerDate)
        ChangeCalendar()
        lblYearMonth.Text = MonthName(selectedDate(1), True) & " " & selectedDate(0)
        Me.ActiveControl = cmbMonth
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.SuspendLayout()
        Me.ChangeCalendar()
        Me.ResumeLayout()
    End Sub

    Private Sub SettingCalendar()
        Dim maxDays As Integer = Integer.Parse(Date.DaysInMonth(selectedDate(0), selectedDate(1)).ToString()) 'get the total day count of selected year and month
        Dim blankCount, tempCt As Integer
        Dim firstDayOfMonth As Date = New DateTime()
        firstDayOfMonth = firstDayOfMonth.AddYears(CInt(selectedDate(0)) - 1).AddMonths(CInt(selectedDate(1)) - 1)

        'identify how many blank boxes to place based on the first day of the month
        Select Case firstDayOfMonth.DayOfWeek.ToString()
            Case "Sunday"
                blankCount = 7
            Case "Monday"
                blankCount = 1
            Case "Tuesday"
                blankCount = 2
            Case "Wednesday"
                blankCount = 3
            Case "Thursday"
                blankCount = 4
            Case "Friday"
                blankCount = 5
            Case Else
                blankCount = 6
        End Select

        tempCt = blankCount - 1
        Dim row = 1, boxCount = 0, dayCount = 1

        While row <= 7
            Dim col = 1
            While col < 7
                If blankCount > 0 Or dayCount > maxDays Then
                    lsvCalendar(boxCount).BackColor = Color.LightGray
                    pnlCalendar(boxCount).BackColor = Color.LightGray
                    blankCount = blankCount - 1
                Else
                    Dim indexDateAll As Date = New Date(selectedDate(0).ToString, selectedDate(1).ToString, dayCount)

                    'current date
                    If Equals(CDate(dbMethod.GetServerDate).ToString("yyyy-MM"), (selectedDate(0).ToString() & "-" & selectedDate(1).ToString("00"))) AndAlso
                           Equals(dayCount, Integer.Parse(CDate(dbMethod.GetServerDate).ToString("dd"))) Then
                        pnlCalendar(dayCount + tempCt).BackColor = Drawing.Color.FromArgb(192, 255, 255)
                        lsvCalendar(dayCount + tempCt).BackColor = Drawing.Color.FromArgb(192, 255, 255)

                    ElseIf Equals(firstDayOfMonth.DayOfWeek.ToString(), "Sunday") Then
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lsvCalendar(boxCount).BackColor = Drawing.SystemColors.Window

                        'plot the pending pm schedule
                        If cmbView.SelectedValue = 1 Then
                            Dim rowScheduled As Integer = row - 1
                            Dim indexDate As Date = New Date(selectedDate(0).ToString, selectedDate(1).ToString, dayCount)
                            lblScheduled(rowScheduled).Text = "Week " & GetWeekNumber(indexDate)

                            Dim prmForPm(1) As SqlParameter
                            prmForPm(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                            prmForPm(0).Value = Nothing
                            prmForPm(1) = New SqlParameter("@WeekId", SqlDbType.Int)
                            prmForPm(1).Value = GetWeekNumber(indexDate)

                            Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntMachineSchedule", CommandType.StoredProcedure, prmForPm)

                            While rdrForPm.Read
                                If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                    Dim arrStr(3) As String
                                    arrStr(0) = rdrForPm("MachineName").ToString
                                    arrStr(1) = rdrForPm("MachineId")
                                    arrStr(2) = ""

                                    Dim lsvItem As ListViewItem
                                    lsvItem = New ListViewItem(arrStr)
                                    lsvScheduled(rowScheduled).Items.Add(lsvItem)
                                End If
                            End While
                            rdrForPm.Close()
                        Else
                            Dim rowScheduled As Integer = row - 1
                            Dim indexDate As Date = New Date(selectedDate(0).ToString, selectedDate(1).ToString, dayCount)
                            lblScheduled(rowScheduled).Text = "Week " & GetWeekNumber(indexDate)

                            Dim prmForPm(1) As SqlParameter
                            prmForPm(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                            prmForPm(0).Value = Nothing
                            prmForPm(1) = New SqlParameter("@WeekId", SqlDbType.Int)
                            prmForPm(1).Value = GetWeekNumber(indexDate)

                            Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                            While rdrForPm.Read
                                If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                    Dim arrStr(3) As String
                                    arrStr(0) = rdrForPm("JigName").ToString
                                    arrStr(1) = rdrForPm("JigId")
                                    arrStr(2) = ""

                                    Dim lsvItem As ListViewItem
                                    lsvItem = New ListViewItem(arrStr)
                                    lsvScheduled(rowScheduled).Items.Add(lsvItem)
                                End If
                            End While
                            rdrForPm.Close()
                        End If

                    ElseIf Equals(firstDayOfMonth.DayOfWeek.ToString(), "Saturday") Then
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lsvCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                    Else
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lsvCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                    End If

                    'plot the finished pm activity
                    If cmbView.SelectedValue = 1 Then
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@WeekId", SqlDbType.Int)
                        prmActDate(1).Value = Nothing

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntMachineSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("MachineName").ToString
                                arrStr(1) = rdrSchedule("MachineId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("WeekId") <> GetWeekNumber(indexDateAll) Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lsvCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()
                    Else
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@WeekId", SqlDbType.Int)
                        prmActDate(1).Value = Nothing

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)
                                lsvCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()
                    End If

                    pnlCalendar(boxCount).Enabled = True
                lsvCalendar(boxCount).Enabled = True
                    dayCount = dayCount + 1
                    firstDayOfMonth = firstDayOfMonth.AddDays(1)
                End If

                boxCount = boxCount + 1
                col = col + 1
            End While

            row = row + 1
        End While

        'lsvIndex = CInt(selectedDate(2)) + tempCt
        'lsvCalendar(lsvIndex).BackColor = System.Drawing.Color.FromArgb(255, 255, 192)
    End Sub

    Public Sub ChangeCalendar()
        For count = 0 To lsvCalendar.Length - 1
            lblCalendar(count).Text = String.Empty
            lsvCalendar(count).Items.Clear()
        Next

        For count = 0 To lsvScheduled.Length - 1
            lblScheduled(count).Text = String.Empty
            lsvScheduled(count).Items.Clear()
        Next

        SettingCalendar()

        If cmbView.SelectedValue = 1 Then
            Dim prmTotal(1) As SqlParameter
            prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
            prmTotal(0).Value = CInt(selectedDate(1))
            prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
            prmTotal(1).Value = selectedDate(0)
            txtTotal.Text = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId", CommandType.Text, prmTotal)

            Dim prmDone(1) As SqlParameter
            prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
            prmDone(0).Value = CInt(selectedDate(1))
            prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
            prmDone(1).Value = selectedDate(0)
            txtCompleted.Text = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND TrxId IS NOT NULL", CommandType.Text, prmDone)
        Else
            Dim prmTotal(1) As SqlParameter
            prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
            prmTotal(0).Value = CInt(selectedDate(1))
            prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
            prmTotal(1).Value = selectedDate(0)
            txtTotal.Text = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId", CommandType.Text, prmTotal)

            Dim prmDone(1) As SqlParameter
            prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
            prmDone(0).Value = CInt(selectedDate(1))
            prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
            prmDone(1).Value = selectedDate(0)
            txtCompleted.Text = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND TrxId IS NOT NULL", CommandType.Text, prmDone)
        End If

        If CDbl(txtTotal.Text) = 0 AndAlso CDbl(txtCompleted.Text) = 0 Then
            txtPercentage.Text = 0 & "%"
        Else
            Dim percentage As Double = (CDbl(txtCompleted.Text) / CDbl(txtTotal.Text)) * 100
            txtPercentage.Text = Math.Round(percentage, MidpointRounding.AwayFromZero) & "%"
        End If
    End Sub

    Private Function GetWeekNumber(prmDate As Date) As Integer
        Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
        Dim calendar As Calendar = cultureInfo.Calendar
        Dim calWeekRule As CalendarWeekRule = 2 'firstfourdayweek rule
        Dim firstDayOfWeek As DayOfWeek = 0 'sunday

        Return calendar.GetWeekOfYear(prmDate, calWeekRule, firstDayOfWeek)
    End Function

    'prevent form resizing when double clicked the titlebar or dragged
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCLBUTTONDBLCLK As Integer = 163 'define doubleclick event
        Const WM_NCLBUTTONDOWN As Integer = 161 'define leftbuttondown event
        Const WM_SYSCOMMAND As Integer = 274 'define move action
        Const HTCAPTION As Integer = 2 'define that the WM_NCLBUTTONDOWN is at titlebar
        Const SC_MOVE As Integer = 61456 'trap move action
        'disable moving of title bar
        If (m.Msg = WM_SYSCOMMAND) AndAlso (m.WParam.ToInt32() = SC_MOVE) Then
            Exit Sub
        End If
        'track whether clicked on title bar
        If (m.Msg = WM_NCLBUTTONDOWN) AndAlso (m.WParam.ToInt32() = HTCAPTION) Then
            Exit Sub
        End If
        'disable double click on title bar
        If (m.Msg = WM_NCLBUTTONDBLCLK) Then
            Exit Sub
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub frmMain_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = True

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub

End Class