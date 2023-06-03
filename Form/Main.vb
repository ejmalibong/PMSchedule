Imports System.Data.SqlClient
Imports System.Deployment.Application
Imports System.Globalization
Imports System.IO
Imports BlackCoffeeLibrary

Public Class Main
    Private dbConnection As New Connection
    Private dbMain As New BlackCoffeeLibrary.Main
    Private dbMethod As New SqlDbMethod(dbConnection.GetConnectionString)
    Private dicType As New Dictionary(Of String, Integer)

    Private lblCalendar As Label()
    Private lblScheduled As Label()

    Private lstCalendar As ListView()
    Private lstScheduled As ListView()

    Private lsvIndex As Integer

    Private pnlCalendar As Panel()
    Private pnlScheduled As Panel()

    Private selectedDate As Decimal()

    Private totalCurrent As Integer = 0
    Private totalItem As Integer = 0

    Private hostName As String = String.Empty
    Private sectionId As Integer = 0

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        hostName = Environment.MachineName.ToString

        Dim prmMchName(0) As SqlParameter
        prmMchName(0) = New SqlParameter("@Hostname", SqlDbType.NVarChar)
        prmMchName(0).Value = hostName

        sectionId = dbMethod.ExecuteScalar("SELECT SectionId FROM dbo.SecHostname WHERE TRIM(Hostname) = @Hostname", CommandType.Text, prmMchName)

        Select Case sectionId
            Case 1, 4
                rdMt.Enabled = True
                rdFacility.Enabled = True
                rdMt.Checked = True

            Case 2 'mt
                rdMt.Visible = False
                rdFacility.Visible = False
                rdMt.Checked = True

            Case 3 'facility
                rdMt.Visible = False
                rdFacility.Visible = False
                rdFacility.Checked = True
        End Select

        cmbView.SelectedValue = 1 'default view to machine schedule
        txtYear.Text = Year(dbMethod.GetServerDate)

        'set current date as default date
        Dim currentDate As Date = CDate(dbMethod.GetServerDate)
        Dim formattedDate = currentDate.ToString("yyyy-MM-dd").Split("-"c)
        selectedDate = New Decimal() {Decimal.Parse(formattedDate(0)), Decimal.Parse(formattedDate(1)), Decimal.Parse(formattedDate(2))}

        Dim percentCol = 100.0F / CSng(7)
        Dim percentRow = 100.0F / CSng(6)

        'accomplished pm
        pnlCalendar = New Panel(42) {}
        lblCalendar = New Label(42) {}
        lstCalendar = New ListView(42) {}

        For count As Integer = 0 To pnlCalendar.Length - 1
            pnlCalendar(count) = New Panel
        Next

        For count As Integer = 0 To lblCalendar.Length - 1
            lblCalendar(count) = New Label()
        Next

        For count As Integer = 0 To lstCalendar.Length - 1
            lstCalendar(count) = New ListView()

            lstCalendar(count).BeginUpdate()
            lstCalendar(count).Font = New Font("Segoe UI", 9.5, FontStyle.Regular)
            lstCalendar(count).Columns.Add("Name")
            lstCalendar(count).Columns.Add("Id")
            lstCalendar(count).Columns.Add("TrxId")

            lstCalendar(count).Columns.Item(0).Width = -1
            lstCalendar(count).Columns.Item(1).Width = 0
            lstCalendar(count).Columns.Item(2).Width = 0
            lstCalendar(count).HeaderStyle = ColumnHeaderStyle.None
            lstCalendar(count).View = View.Details
            lstCalendar(count).Sorting = Windows.Forms.SortOrder.Ascending
            lstCalendar(count).ShowItemToolTips = True
            lstCalendar(count).FullRowSelect = True
            lstCalendar(count).Scrollable = True
            lstCalendar(count).EndUpdate()

            AddHandler lstCalendar(count).DoubleClick, New EventHandler(AddressOf CalendarDoubleClick)
        Next

        'scheduled pm
        pnlScheduled = New Panel(6) {}
        lblScheduled = New Label(6) {}
        lstScheduled = New ListView(6) {}

        For count As Integer = 0 To pnlScheduled.Length - 1
            pnlScheduled(count) = New Panel
        Next

        For count As Integer = 0 To lblScheduled.Length - 1
            lblScheduled(count) = New Label()
        Next

        For count As Integer = 0 To lstScheduled.Length - 1
            lstScheduled(count) = New ListView()
            lstScheduled(count).BeginUpdate()
            lstScheduled(count).Font = New Font("Segoe UI", 9.5, FontStyle.Regular)
            lstScheduled(count).Columns.Add("Name", HorizontalAlignment.Left)
            lstScheduled(count).Columns.Add("Id", HorizontalAlignment.Left)
            lstScheduled(count).Columns.Add("TrxId", HorizontalAlignment.Left)

            lstScheduled(count).Columns.Item(1).Width = 0
            lstScheduled(count).Columns.Item(2).Width = 0
            lstScheduled(count).HeaderStyle = ColumnHeaderStyle.None
            lstScheduled(count).View = View.Details
            lstScheduled(count).ShowItemToolTips = True
            lstScheduled(count).Sorting = Windows.Forms.SortOrder.Ascending
            lstScheduled(count).Scrollable = True
            lstScheduled(count).EndUpdate()

            AddHandler lstScheduled(count).ItemSelectionChanged, AddressOf LsvScheduled_ItemSelectionChanged
        Next

        Dim indexCalendar As Integer = 0
        Dim indexScheduled As Integer = 0

        Me.SuspendLayout()

        For row As Integer = 0 To tlpCalendar.RowCount - 1
            For col As Integer = 0 To tlpCalendar.ColumnCount - 1
                If col = 0 Then 'default size of scheduled pm column (first column)
                    tlpCalendar.ColumnStyles(0).SizeType = SizeType.Absolute
                    tlpCalendar.ColumnStyles(0).Width = 200
                Else
                    tlpCalendar.ColumnStyles(col).Width = percentCol
                End If

                If row = 0 Then 'default size of row header (day name)
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

                    Me.pnlCalendar(indexCalendar).Controls.Add(lstCalendar(indexCalendar))
                    Dim propLsvPadding As Padding = lstCalendar(indexCalendar).Margin
                    With lstCalendar(indexCalendar)
                        .BorderStyle = BorderStyle.None
                        .Dock = DockStyle.Fill
                        .Margin = New Padding(0)
                        .Name = "lblLsvCalendar" & indexCalendar + 1
                        .Tag = indexCalendar + 1
                        .BringToFront()
                    End With

                    indexCalendar += 1
                End If

                'scheduled pm column
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

                    Me.pnlScheduled(indexScheduled).Controls.Add(lstScheduled(indexScheduled))
                    Dim propLsvPadding As Padding = lstScheduled(indexScheduled).Margin
                    With lstScheduled(indexScheduled)
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

        Me.ResumeLayout()
    End Sub

    Public Sub ChangeCalendar()
        Try
            For count = 0 To lstCalendar.Length - 1
                lblCalendar(count).Text = String.Empty
                lstCalendar(count).Items.Clear()
            Next

            For count = 0 To lstScheduled.Length - 1
                lblScheduled(count).Text = String.Empty
                lstScheduled(count).Items.Clear()
            Next

            SettingCalendar()

            If cmbView.SelectedValue = 1 Then 'machine
                If rdMt.Checked Then
                    Dim prmTotal(1) As SqlParameter
                    prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                    prmTotal(0).Value = CInt(selectedDate(1))
                    prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                    prmTotal(1).Value = selectedDate(0)
                    totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId", CommandType.Text, prmTotal)

                    Dim prmDone(1) As SqlParameter
                    prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                    prmDone(0).Value = CInt(selectedDate(1))
                    prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                    prmDone(1).Value = selectedDate(0)
                    totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND TrxId IS NOT NULL", CommandType.Text, prmDone)
                Else
                    Dim prmTotal(1) As SqlParameter
                    prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                    prmTotal(0).Value = CInt(selectedDate(1))
                    prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                    prmTotal(1).Value = selectedDate(0)
                    totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.FacMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId", CommandType.Text, prmTotal)

                    Dim prmDone(1) As SqlParameter
                    prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                    prmDone(0).Value = CInt(selectedDate(1))
                    prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                    prmDone(1).Value = selectedDate(0)
                    totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.FacMachineSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND TrxId IS NOT NULL", CommandType.Text, prmDone)
                End If

            ElseIf cmbView.SelectedValue = 2 Then 'taping jig
                Dim prmTotal(1) As SqlParameter
                prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmTotal(0).Value = CInt(selectedDate(1))
                prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmTotal(1).Value = selectedDate(0)
                totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                   "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 1)", CommandType.Text, prmTotal)

                Dim prmDone(1) As SqlParameter
                prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmDone(0).Value = CInt(selectedDate(1))
                prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmDone(1).Value = selectedDate(0)
                totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                      "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 1) AND TrxId IS NOT NULL", CommandType.Text, prmDone)

            ElseIf cmbView.SelectedValue = 3 Then 'qcf
                Dim prmTotal(1) As SqlParameter
                prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmTotal(0).Value = CInt(selectedDate(1))
                prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmTotal(1).Value = selectedDate(0)
                totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                   "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 2)", CommandType.Text, prmTotal)

                Dim prmDone(1) As SqlParameter
                prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmDone(0).Value = CInt(selectedDate(1))
                prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmDone(1).Value = selectedDate(0)
                totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                      "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 2) AND TrxId IS NOT NULL", CommandType.Text, prmDone)

            ElseIf cmbView.SelectedValue = 4 Then 'steering
                Dim prmTotal(1) As SqlParameter
                prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmTotal(0).Value = CInt(selectedDate(1))
                prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmTotal(1).Value = selectedDate(0)
                totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                   "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 3)", CommandType.Text, prmTotal)

                Dim prmDone(1) As SqlParameter
                prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmDone(0).Value = CInt(selectedDate(1))
                prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmDone(1).Value = selectedDate(0)
                totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                      "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 3) AND TrxId IS NOT NULL", CommandType.Text, prmDone)

            ElseIf cmbView.SelectedValue = 5 Then 'applicator
                Dim prmTotal(1) As SqlParameter
                prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmTotal(0).Value = CInt(selectedDate(1))
                prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmTotal(1).Value = selectedDate(0)
                totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                   "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 4)", CommandType.Text, prmTotal)

                Dim prmDone(1) As SqlParameter
                prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmDone(0).Value = CInt(selectedDate(1))
                prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmDone(1).Value = selectedDate(0)
                totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                      "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 4) AND TrxId IS NOT NULL", CommandType.Text, prmDone)

            ElseIf cmbView.SelectedValue = 6 Then 'csw/mr
                Dim prmTotal(1) As SqlParameter
                prmTotal(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmTotal(0).Value = CInt(selectedDate(1))
                prmTotal(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmTotal(1).Value = selectedDate(0)
                totalItem = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                   "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 5)", CommandType.Text, prmTotal)

                Dim prmDone(1) As SqlParameter
                prmDone(0) = New SqlParameter("@MonthId", SqlDbType.Int)
                prmDone(0).Value = CInt(selectedDate(1))
                prmDone(1) = New SqlParameter("@YearId", SqlDbType.Int)
                prmDone(1).Value = selectedDate(0)
                totalCurrent = dbMethod.ExecuteScalar("SELECT COUNT(ScheduleId) FROM dbo.MntJigSchedule WHERE MonthId = @MonthId AND YearId = @YearId AND JigId IN " &
                                                      "(SELECT JigId FROM dbo.MntJig WHERE JigTypeId = 5) AND TrxId IS NOT NULL", CommandType.Text, prmDone)
            End If

            If CDbl(totalCurrent) = 0 AndAlso CDbl(totalItem) = 0 Then
                txtFraction.Text = "0 / 0"
                txtPercentage.Text = 0 & "%"
            Else
                Dim percentage As Double = (CDbl(totalCurrent) / CDbl(totalItem)) * 100
                txtFraction.Text = totalCurrent & " / " & totalItem
                txtPercentage.Text = Math.Round(percentage, MidpointRounding.AwayFromZero) & "%"
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    Private Shared Sub PlayNotificationSound(ByVal sound As String)
        Dim dbMainLoc As New BlackCoffeeLibrary.Main

        Try
            Dim soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds")
            Dim soundFile = Path.Combine(soundsFolder, sound & ".wav")

            Using player = New Media.SoundPlayer(soundFile)
                player.Play()
            End Using
        Catch ex As Exception
            MessageBox.Show(dbMainLoc.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCurrent_Click(sender As Object, e As EventArgs) Handles btnCurrent.Click
        Try
            selectedDate(0) = Year(dbMethod.GetServerDate)
            selectedDate(1) = Month(dbMethod.GetServerDate)
            cmbMonth.SelectedValue = 0
            txtYear.Text = Year(dbMethod.GetServerDate)
            ChangeCalendar()
            lblYearMonth.Text = MonthName(selectedDate(1), True) & " " & selectedDate(0)
            Me.ActiveControl = cmbMonth
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Me.SuspendLayout()

            If cmbMonth.SelectedValue = 0 Then
                Me.ActiveControl = cmbMonth
                Return
            End If

            Dim cval As Integer = CInt(selectedDate(2))
            Dim temp As Integer = CInt(selectedDate(2))

            Dim maxDays As Integer = Integer.Parse(Date.DaysInMonth(txtYear.Text, cmbMonth.SelectedValue).ToString())
            selectedDate(2) = CInt(CDate(DateSerial(txtYear.Text, cmbMonth.SelectedValue, maxDays)).ToString("dd"))

            If Integer.Parse(txtYear.Text.Trim) = selectedDate(0) AndAlso Integer.Parse(cmbMonth.SelectedValue) = selectedDate(1) Then

            Else
                selectedDate(0) = txtYear.Text
                selectedDate(1) = cmbMonth.SelectedValue
                ChangeCalendar()
            End If

            lblYearMonth.Text = MonthName(cmbMonth.SelectedValue, True) & " " & txtYear.Text

            Me.ResumeLayout()
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.SuspendLayout()
        Me.ChangeCalendar()
        Me.ResumeLayout()
    End Sub

    Private Sub btnChecksheet_Click(sender As Object, e As EventArgs) Handles btnChecksheet.Click
        Try
            If cmbView.SelectedValue = 1 Then
                If rdMt.Checked Then
                    Dim frmDetail As New MachineMonitoringSystem.MntMchCs()
                    frmDetail.ShowDialog(Me)
                End If

            Else
                Dim frmDetail As New MachineMonitoringSystem.MntJigCs()
                frmDetail.ShowDialog(Me)
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
                If rdMt.Checked Then
                    Dim frmDetail As New MachineMonitoringSystem.MntTrxDetailMch(0, 0, 0, trxId)
                    frmDetail.fromPmCalendar = True
                    frmDetail.ShowDialog()
                Else
                    Dim frmDetail As New MachineMonitoringSystem.FacTrxDetailMch(0, 0, 0, trxId)
                    frmDetail.fromPmCalendar = True
                    frmDetail.ShowDialog()
                End If
            Else
                Dim frmDetail As New MachineMonitoringSystem.MntTrxDetailJig(0, 0, 0, trxId)
                frmDetail.fromPmCalendar = True
                frmDetail.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbView_SelectedValueChanged(sender As Object, e As EventArgs)
        Me.SuspendLayout()
        Me.ChangeCalendar()
        Me.ResumeLayout()
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

        Dim prmIms(0) As SqlParameter
        prmIms(0) = New SqlParameter("SettingsId", SqlDbType.Int)
        prmIms(0).Value = 1

        lblVersion.Text = dbMethod.ExecuteScalar("SELECT TRIM(IMS) AS IMS FROM dbo.SysSetting WHERE SettingsId = @SettingsId", CommandType.Text, prmIms)

        tmrMain.Start()
        Me.ActiveControl = cmbMonth
    End Sub

    Private Sub frmMain_SizeEventHandler(ByVal sender As Object, ByVal e As EventArgs)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.MaximizeBox = True

        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.MaximizeBox = False
        End If
    End Sub

    Private Function GetWeekNumber(prmDate As Date) As Integer
        Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
        Dim calendar As Calendar = cultureInfo.Calendar
        Dim calWeekRule As CalendarWeekRule = 2 'firstfourdayweek rule
        Dim firstDayOfWeek As DayOfWeek = 0 'sunday

        Return calendar.GetWeekOfYear(prmDate, calWeekRule, firstDayOfWeek)
    End Function

    Private Function IsIncomingMonthEnd(dt As Date) As Boolean
        Dim newDate As Date = dt.AddDays(6)
        If newDate.Month > dt.Month Then Return True
        If newDate.Year > dt.Year Then Return True
        Return False
    End Function

    Private Sub LsvScheduled_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs)
        Try
            If e.IsSelected Then
                e.Item.Selected = False
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control AndAlso e.KeyCode.Equals(Keys.F12) Then
                ShowNotification()
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SettingCalendar()
        Dim maxDays As Integer = Integer.Parse(Date.DaysInMonth(selectedDate(0), selectedDate(1)).ToString()) 'get the total day count of selected year and month
        Dim blankCount, tempCt As Integer
        Dim firstDayOfMonth As Date = New DateTime()
        firstDayOfMonth = firstDayOfMonth.AddYears(CInt(selectedDate(0)) - 1).AddMonths(CInt(selectedDate(1)) - 1)

        'identify how many blank boxes to place in the tablelayoutpanel (calendar) based on the first day of the month
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
        Dim row = 1, boxCount = 0, dayCount = 1 ', col = 1

        While row <= 7
            Dim col = 1
            While col < 7
                If blankCount > 0 Or dayCount > maxDays Then
                    lstCalendar(boxCount).BackColor = Color.LightGray
                    pnlCalendar(boxCount).BackColor = Color.LightGray
                    blankCount = blankCount - 1
                Else
                    'current date
                    If Equals(CDate(dbMethod.GetServerDate).ToString("yyyy-MM"), (selectedDate(0).ToString() & "-" & selectedDate(1).ToString("00"))) AndAlso
                           Equals(dayCount, Integer.Parse(CDate(dbMethod.GetServerDate).ToString("dd"))) Then
                        pnlCalendar(dayCount + tempCt).BackColor = Drawing.Color.FromArgb(192, 255, 255)
                        lstCalendar(dayCount + tempCt).BackColor = Drawing.Color.FromArgb(192, 255, 255)

                    ElseIf Equals(firstDayOfMonth.DayOfWeek.ToString(), "Sunday") Then
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lstCalendar(boxCount).BackColor = Drawing.SystemColors.Window

                        Dim rowScheduled As Integer = row - 1
                        Dim indexDate As Date = New Date(selectedDate(0).ToString, selectedDate(1).ToString, dayCount)
                        Dim validDate As Date = indexDate.AddDays(4)

                        If GetWeekNumber(indexDate).Equals(GetWeekNumber(validDate)) Then 'AndAlso Month(indexDate).Equals(Month(validDate)) Then
                            lblScheduled(rowScheduled).Text = "Week " & GetWeekNumber(indexDate)

                            'plot the pending pm schedule
                            If cmbView.SelectedValue = 1 Then 'machine
                                Dim prmForPm(1) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(1).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                If rdMt.Checked Then
                                    Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntMachineSchedule", CommandType.StoredProcedure, prmForPm)

                                    While rdrForPm.Read
                                        totalPerWeek += 1

                                        If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                            Dim arrStr(3) As String
                                            arrStr(0) = rdrForPm("MachineName").ToString
                                            arrStr(1) = rdrForPm("MachineId")
                                            arrStr(2) = ""

                                            Dim lsvItem As ListViewItem
                                            lsvItem = New ListViewItem(arrStr)
                                            lstScheduled(rowScheduled).Items.Add(lsvItem)
                                        End If
                                    End While
                                    rdrForPm.Close()

                                Else
                                    Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdFacMachineSchedule", CommandType.StoredProcedure, prmForPm)

                                    While rdrForPm.Read
                                        totalPerWeek += 1

                                        If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                            Dim arrStr(3) As String
                                            arrStr(0) = rdrForPm("MachineCode").ToString
                                            arrStr(1) = rdrForPm("MachineId")
                                            arrStr(2) = ""

                                            Dim lsvItem As ListViewItem
                                            lsvItem = New ListViewItem(arrStr)
                                            lstScheduled(rowScheduled).Items.Add(lsvItem)
                                        End If
                                    End While
                                    rdrForPm.Close()
                                End If

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 2 Then 'taping jig
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 1
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 3 Then 'qcf
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 2
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 4 Then 'steering
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 3
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 5 Then 'applicator
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 4
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 6 Then 'csw/mr
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 5
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If
                            End If
                        End If

                    ElseIf Equals(firstDayOfMonth.DayOfWeek.ToString(), "Saturday") Then
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lstCalendar(boxCount).BackColor = Drawing.SystemColors.Window

                        Dim rowScheduled As Integer = row - 2
                        Dim indexDate As Date = New Date(selectedDate(0).ToString, selectedDate(1).ToString, dayCount)
                        Dim validDate As Date = indexDate.AddDays(-3)

                        If GetWeekNumber(indexDate).Equals(GetWeekNumber(validDate)) AndAlso Month(indexDate).Equals(Month(validDate)) AndAlso Not Month(indexDate).Equals(Month(indexDate.AddDays(-6))) Then
                            lblScheduled(rowScheduled).Text = "Week " & GetWeekNumber(indexDate)

                            'plot the pending pm schedule
                            If cmbView.SelectedValue = 1 Then 'machine
                                Dim prmForPm(1) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(1).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                If rdMt.Checked Then
                                    Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntMachineSchedule", CommandType.StoredProcedure, prmForPm)

                                    While rdrForPm.Read
                                        totalPerWeek += 1

                                        If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                            Dim arrStr(3) As String
                                            arrStr(0) = rdrForPm("MachineName").ToString
                                            arrStr(1) = rdrForPm("MachineId")
                                            arrStr(2) = ""

                                            Dim lsvItem As ListViewItem
                                            lsvItem = New ListViewItem(arrStr)
                                            lstScheduled(rowScheduled).Items.Add(lsvItem)
                                        End If
                                    End While
                                    rdrForPm.Close()

                                Else
                                    Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdFacMachineSchedule", CommandType.StoredProcedure, prmForPm)

                                    While rdrForPm.Read
                                        totalPerWeek += 1

                                        If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                            Dim arrStr(3) As String
                                            arrStr(0) = rdrForPm("MachineCode").ToString
                                            arrStr(1) = rdrForPm("MachineId")
                                            arrStr(2) = ""

                                            Dim lsvItem As ListViewItem
                                            lsvItem = New ListViewItem(arrStr)
                                            lstScheduled(rowScheduled).Items.Add(lsvItem)
                                        End If
                                    End While
                                    rdrForPm.Close()
                                End If

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 2 Then 'taping
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 1
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 3 Then 'qcf
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 2
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 4 Then 'steering
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 3
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 5 Then 'applicator
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 4
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If

                            ElseIf cmbView.SelectedValue = 6 Then 'csw/mr
                                Dim prmForPm(2) As SqlParameter
                                prmForPm(0) = New SqlParameter("@WeekId", SqlDbType.Int)
                                prmForPm(0).Value = GetWeekNumber(indexDate)
                                prmForPm(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                                prmForPm(1).Value = 5
                                prmForPm(2) = New SqlParameter("@YearId", SqlDbType.Int)
                                prmForPm(2).Value = txtYear.Text

                                Dim accomplished As Integer = 0
                                Dim totalPerWeek As Integer = 0

                                Dim rdrForPm As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmForPm)

                                While rdrForPm.Read
                                    totalPerWeek += 1

                                    If rdrForPm("TrxId") Is DBNull.Value AndAlso rdrForPm("ActivityBy") Is DBNull.Value AndAlso rdrForPm("ActivityDate") Is DBNull.Value Then
                                        Dim arrStr(3) As String
                                        arrStr(0) = rdrForPm("JigCompleteName").ToString
                                        arrStr(1) = rdrForPm("JigId")
                                        arrStr(2) = ""

                                        Dim lsvItem As ListViewItem
                                        lsvItem = New ListViewItem(arrStr)
                                        lstScheduled(rowScheduled).Items.Add(lsvItem)
                                    End If
                                End While
                                rdrForPm.Close()

                                lstScheduled(rowScheduled).Columns.Item(0).Width = -1

                                If accomplished.Equals(totalPerWeek) AndAlso (accomplished <> 0 AndAlso totalPerWeek <> 0) Then
                                    lblScheduled(rowScheduled).Text += "  (100%)"
                                ElseIf accomplished = 0 AndAlso totalPerWeek = 0 Then
                                    lblScheduled(rowScheduled).Text += "  (0%)"
                                Else
                                    accomplished = totalPerWeek - CDbl(lstScheduled(rowScheduled).Items.Count)

                                    Dim percentage As Double = (CDbl(accomplished) / CDbl(totalPerWeek)) * 100
                                    lblScheduled(rowScheduled).Text += "  (" & Math.Round(percentage, MidpointRounding.AwayFromZero) & "%)"
                                End If
                            End If
                        End If

                    Else
                        pnlCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                        lstCalendar(boxCount).BackColor = Drawing.SystemColors.Window
                    End If

                    'plot the finished pm activity
                    If cmbView.SelectedValue = 1 Then 'machine
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(0) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate

                        If rdMt.Checked Then
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

                                    If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                        lsvItem.ForeColor = Color.Red
                                    End If

                                    lstCalendar(boxCount).Items.Add(lsvItem)
                                End If
                            End While
                            rdrSchedule.Close()

                        Else
                            Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdFacMachineSchedule", CommandType.StoredProcedure, prmActDate)

                            lblCalendar(boxCount).Text = dayCount

                            While rdrSchedule.Read
                                If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                    Dim arrStr(3) As String
                                    arrStr(0) = "● " & rdrSchedule("MachineCode").ToString
                                    arrStr(1) = rdrSchedule("MachineId")
                                    arrStr(2) = rdrSchedule("TrxId")

                                    Dim lsvItem As ListViewItem
                                    lsvItem = New ListViewItem(arrStr)

                                    If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                        lsvItem.ForeColor = Color.Red
                                    End If

                                    lstCalendar(boxCount).Items.Add(lsvItem)
                                End If
                            End While
                            rdrSchedule.Close()
                        End If

                    ElseIf cmbView.SelectedValue = 2 Then 'taping
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                        prmActDate(1).Value = 1

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigCompleteName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lstCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()

                    ElseIf cmbView.SelectedValue = 3 Then 'qcf
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                        prmActDate(1).Value = 2

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigCompleteName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lstCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()

                    ElseIf cmbView.SelectedValue = 4 Then 'steering
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                        prmActDate(1).Value = 3

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigCompleteName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lstCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()

                    ElseIf cmbView.SelectedValue = 5 Then 'applicator
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                        prmActDate(1).Value = 4

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigCompleteName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lstCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()

                    ElseIf cmbView.SelectedValue = 6 Then 'csw/mr
                        Dim dateString() = firstDayOfMonth.ToString("yyyy-MM-dd").Split("-"c)
                        Dim dManage As DataManage = New PMSchedule.DataManage(Decimal.Parse(dateString(0)), Decimal.Parse(dateString(1)), Decimal.Parse(dateString(2)))
                        Dim prmDate As New Date(dManage.YearMonthDay(0).ToString, dManage.YearMonthDay(1).ToString, dManage.YearMonthDay(2).ToString)

                        Dim prmActDate(1) As SqlParameter
                        prmActDate(0) = New SqlParameter("@ActivityDate", SqlDbType.Date)
                        prmActDate(0).Value = prmDate
                        prmActDate(1) = New SqlParameter("@JigTypeId", SqlDbType.Int)
                        prmActDate(1).Value = 5

                        Dim rdrSchedule As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure, prmActDate)

                        lblCalendar(boxCount).Text = dayCount

                        While rdrSchedule.Read
                            If Not rdrSchedule.Item("ActivityDate") Is DBNull.Value Then
                                Dim arrStr(3) As String
                                arrStr(0) = "● " & rdrSchedule("JigCompleteName").ToString
                                arrStr(1) = rdrSchedule("JigId")
                                arrStr(2) = rdrSchedule("TrxId")

                                Dim lsvItem As ListViewItem
                                lsvItem = New ListViewItem(arrStr)

                                If rdrSchedule("MonthId") < selectedDate(1).ToString Then
                                    lsvItem.ForeColor = Color.Red
                                End If

                                lstCalendar(boxCount).Items.Add(lsvItem)
                            End If
                        End While
                        rdrSchedule.Close()
                    End If

                    pnlCalendar(boxCount).Enabled = True
                    lstCalendar(boxCount).Enabled = True
                    dayCount = dayCount + 1
                    firstDayOfMonth = firstDayOfMonth.AddDays(1)
                End If

                lstCalendar(boxCount).Columns.Item(0).Width = -1

                boxCount = boxCount + 1
                col = col + 1
            End While

            row = row + 1
        End While
    End Sub

    Private Sub ShowNotification()
        Try
            Dim currentDate As Date = New Date(Year(dbMethod.GetServerDate), Month(dbMethod.GetServerDate), DateAndTime.Day(dbMethod.GetServerDate))

            Dim lastDate As Date = New Date(Year(dbMethod.GetServerDate), Month(dbMethod.GetServerDate), System.DateTime.DaysInMonth(Year(dbMethod.GetServerDate), Month(dbMethod.GetServerDate)))
            Dim dayCount As Integer = DateDiff(DateInterval.Day, currentDate, lastDate)

            Dim lstPending As New List(Of String)
            lstPending.Clear()

            If rdMt.Checked Then
                Dim rdrMchPending As IDataReader = dbMethod.ExecuteReader("RdMntMachineSchedule", CommandType.StoredProcedure)

                While rdrMchPending.Read
                    If rdrMchPending("TrxId") Is DBNull.Value AndAlso rdrMchPending("MonthId").Equals(Month(dbMethod.GetServerDate)) Then
                        lstPending.Add(" ● " & rdrMchPending("MachineName").ToString)
                    End If
                End While
                rdrMchPending.Close()
            Else
                Dim rdrMchPending As IDataReader = dbMethod.ExecuteReader("RdFacMachineSchedule", CommandType.StoredProcedure)

                While rdrMchPending.Read
                    If rdrMchPending("TrxId") Is DBNull.Value AndAlso rdrMchPending("MonthId").Equals(Month(dbMethod.GetServerDate)) Then
                        lstPending.Add(" ● " & rdrMchPending("MachineCode").ToString)
                    End If
                End While
                rdrMchPending.Close()
            End If

            Dim rdrJigPending As IDataReader = dbMethod.ExecuteReader("RdMntJigSchedule", CommandType.StoredProcedure)

            While rdrJigPending.Read
                If rdrJigPending("TrxId") Is DBNull.Value AndAlso rdrJigPending("MonthId").Equals(Month(dbMethod.GetServerDate)) Then
                    lstPending.Add(" ● " & rdrJigPending("JigCompleteName").ToString)
                End If
            End While
            rdrJigPending.Close()

            Dim pendingCount As Integer = lstPending.Count

            If pendingCount > 0 Then
                Dim notification = New Notification(String.Format("There are {0} before the month ends! We still have {1} pending PM {2}!", dayCount & " " & IIf(dayCount > 1, "days", "day"), pendingCount, IIf(pendingCount > 1, "schedules", "schedule")), lstPending, -1, FormAnimator.AnimationMethod.Slide, FormAnimator.AnimationDirection.Up)
                PlayNotificationSound("festival")
                notification.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(dbMain.SetExceptionMessage(ex), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tmrMain_Tick(sender As Object, e As EventArgs) Handles tmrMain.Tick
        If Minute(dbMethod.GetServerDate).Equals(30) AndAlso Second(dbMethod.GetServerDate).Equals(0) Then 'execute every 30 mins
            tmrMain.Enabled = False
            Me.SuspendLayout()
            Me.ChangeCalendar()
            Me.ResumeLayout()
            tmrMain.Enabled = True
        End If

        'show a notification of pending pm every 8 am and 8 pm
        If (Hour(dbMethod.GetServerDate).Equals(8) AndAlso Minute(dbMethod.GetServerDate).Equals(0) AndAlso Second(dbMethod.GetServerDate).Equals(0)) Or
            (Hour(dbMethod.GetServerDate).Equals(20) AndAlso Minute(dbMethod.GetServerDate).Equals(0) AndAlso Second(dbMethod.GetServerDate).Equals(0)) Then

            Dim currentDate As Date = New Date(Year(dbMethod.GetServerDate), Month(dbMethod.GetServerDate), DateAndTime.Day(dbMethod.GetServerDate))

            If IsIncomingMonthEnd(currentDate) Then
                ShowNotification()
            End If
        End If
    End Sub

    Private Sub rdMt_CheckedChanged(sender As Object, e As EventArgs) Handles rdMt.CheckedChanged, rdFacility.CheckedChanged
        cmbView.DataSource = Nothing
        dicType.Clear()

        If rdMt.Checked Then
            dicType.Add(" Machine", 1)
            dicType.Add(" Taping", 2)
            dicType.Add(" QCF", 3)
            dicType.Add(" Steering", 4)
            dicType.Add(" Applicator", 5)
            dicType.Add(" CSW/MR", 6)
            cmbView.DisplayMember = "Key"
            cmbView.ValueMember = "Value"
            cmbView.DataSource = New BindingSource(dicType, Nothing)

            cmbView.Enabled = True
            btnChecksheet.Enabled = True
        ElseIf rdFacility.Checked Then
            dicType.Add(" Machine", 1)
            cmbView.DisplayMember = "Key"
            cmbView.ValueMember = "Value"
            cmbView.DataSource = New BindingSource(dicType, Nothing)

            cmbView.Enabled = False
            btnChecksheet.Enabled = False
        End If
    End Sub

End Class