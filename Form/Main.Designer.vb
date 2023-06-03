<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnChecksheet = New PinkieControls.ButtonXP()
        Me.txtFraction = New System.Windows.Forms.Label()
        Me.lblEquals = New System.Windows.Forms.Label()
        Me.lblCompletion = New System.Windows.Forms.Label()
        Me.lblView = New System.Windows.Forms.Label()
        Me.cmbView = New System.Windows.Forms.ComboBox()
        Me.btnRefresh = New PinkieControls.ButtonXP()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.cmbMonth = New SergeUtils.EasyCompletionComboBox()
        Me.btnCurrent = New PinkieControls.ButtonXP()
        Me.btnGo = New PinkieControls.ButtonXP()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.MaskedTextBox()
        Me.lblYearMonth = New System.Windows.Forms.Label()
        Me.txtPercentage = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.rdFacility = New System.Windows.Forms.RadioButton()
        Me.rdMt = New System.Windows.Forms.RadioButton()
        Me.tmrMain = New System.Windows.Forms.Timer(Me.components)
        Me.cmsMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tlpCalendar = New PMSchedule.ExtTableLayoutPanel()
        Me.lblSunday = New System.Windows.Forms.Label()
        Me.lblWednesday = New System.Windows.Forms.Label()
        Me.lblMonday = New System.Windows.Forms.Label()
        Me.lblTuesday = New System.Windows.Forms.Label()
        Me.lblThursday = New System.Windows.Forms.Label()
        Me.lblFriday = New System.Windows.Forms.Label()
        Me.lblSaturday = New System.Windows.Forms.Label()
        Me.lblScheduledHeader = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.tlpCalendar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.btnChecksheet)
        Me.pnlTop.Controls.Add(Me.txtFraction)
        Me.pnlTop.Controls.Add(Me.lblEquals)
        Me.pnlTop.Controls.Add(Me.lblCompletion)
        Me.pnlTop.Controls.Add(Me.lblView)
        Me.pnlTop.Controls.Add(Me.cmbView)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.lblMonth)
        Me.pnlTop.Controls.Add(Me.cmbMonth)
        Me.pnlTop.Controls.Add(Me.btnCurrent)
        Me.pnlTop.Controls.Add(Me.btnGo)
        Me.pnlTop.Controls.Add(Me.lblYear)
        Me.pnlTop.Controls.Add(Me.txtYear)
        Me.pnlTop.Controls.Add(Me.lblYearMonth)
        Me.pnlTop.Controls.Add(Me.txtPercentage)
        Me.pnlTop.Controls.Add(Me.lblVersion)
        Me.pnlTop.Controls.Add(Me.rdFacility)
        Me.pnlTop.Controls.Add(Me.rdMt)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1384, 56)
        Me.pnlTop.TabIndex = 0
        '
        'btnChecksheet
        '
        Me.btnChecksheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChecksheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnChecksheet.DefaultScheme = True
        Me.btnChecksheet.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnChecksheet.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnChecksheet.Hint = "View checksheet form"
        Me.btnChecksheet.Image = CType(resources.GetObject("btnChecksheet.Image"), System.Drawing.Image)
        Me.btnChecksheet.Location = New System.Drawing.Point(1338, 22)
        Me.btnChecksheet.Name = "btnChecksheet"
        Me.btnChecksheet.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnChecksheet.Size = New System.Drawing.Size(42, 28)
        Me.btnChecksheet.TabIndex = 247
        '
        'txtFraction
        '
        Me.txtFraction.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtFraction.BackColor = System.Drawing.Color.Transparent
        Me.txtFraction.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFraction.Location = New System.Drawing.Point(403, 14)
        Me.txtFraction.Margin = New System.Windows.Forms.Padding(0)
        Me.txtFraction.Name = "txtFraction"
        Me.txtFraction.Size = New System.Drawing.Size(106, 26)
        Me.txtFraction.TabIndex = 246
        Me.txtFraction.Text = "100 / 100"
        Me.txtFraction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEquals
        '
        Me.lblEquals.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblEquals.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEquals.Location = New System.Drawing.Point(514, 20)
        Me.lblEquals.Name = "lblEquals"
        Me.lblEquals.Size = New System.Drawing.Size(19, 16)
        Me.lblEquals.TabIndex = 245
        Me.lblEquals.Text = "="
        '
        'lblCompletion
        '
        Me.lblCompletion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblCompletion.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompletion.Location = New System.Drawing.Point(276, 20)
        Me.lblCompletion.Name = "lblCompletion"
        Me.lblCompletion.Size = New System.Drawing.Size(124, 16)
        Me.lblCompletion.TabIndex = 243
        Me.lblCompletion.Text = "Completion Rate:"
        '
        'lblView
        '
        Me.lblView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblView.BackColor = System.Drawing.SystemColors.Control
        Me.lblView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblView.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblView.ForeColor = System.Drawing.Color.Black
        Me.lblView.Location = New System.Drawing.Point(753, 24)
        Me.lblView.Name = "lblView"
        Me.lblView.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblView.Size = New System.Drawing.Size(48, 25)
        Me.lblView.TabIndex = 224
        Me.lblView.Text = "View"
        Me.lblView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbView
        '
        Me.cmbView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbView.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbView.FormattingEnabled = True
        Me.cmbView.Location = New System.Drawing.Point(800, 24)
        Me.cmbView.Name = "cmbView"
        Me.cmbView.Size = New System.Drawing.Size(90, 25)
        Me.cmbView.TabIndex = 9
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnRefresh.DefaultScheme = True
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnRefresh.Hint = "Refresh"
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(1292, 22)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnRefresh.Size = New System.Drawing.Size(42, 28)
        Me.btnRefresh.TabIndex = 233
        '
        'lblMonth
        '
        Me.lblMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMonth.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMonth.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblMonth.ForeColor = System.Drawing.Color.Black
        Me.lblMonth.Location = New System.Drawing.Point(894, 24)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblMonth.Size = New System.Drawing.Size(55, 25)
        Me.lblMonth.TabIndex = 218
        Me.lblMonth.Text = "Month"
        Me.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbMonth
        '
        Me.cmbMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMonth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Location = New System.Drawing.Point(948, 24)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(140, 25)
        Me.cmbMonth.TabIndex = 217
        '
        'btnCurrent
        '
        Me.btnCurrent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCurrent.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnCurrent.DefaultScheme = True
        Me.btnCurrent.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCurrent.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnCurrent.Hint = "Go to current month"
        Me.btnCurrent.Image = CType(resources.GetObject("btnCurrent.Image"), System.Drawing.Image)
        Me.btnCurrent.Location = New System.Drawing.Point(1247, 22)
        Me.btnCurrent.Name = "btnCurrent"
        Me.btnCurrent.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnCurrent.Size = New System.Drawing.Size(42, 28)
        Me.btnCurrent.TabIndex = 221
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnGo.DefaultScheme = True
        Me.btnGo.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnGo.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnGo.Hint = ""
        Me.btnGo.Location = New System.Drawing.Point(1202, 22)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnGo.Size = New System.Drawing.Size(42, 28)
        Me.btnGo.TabIndex = 15
        Me.btnGo.Text = "Go"
        '
        'lblYear
        '
        Me.lblYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblYear.ForeColor = System.Drawing.Color.Black
        Me.lblYear.Location = New System.Drawing.Point(1092, 24)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblYear.Size = New System.Drawing.Size(48, 25)
        Me.lblYear.TabIndex = 219
        Me.lblYear.Text = "Year"
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtYear
        '
        Me.txtYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYear.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtYear.Location = New System.Drawing.Point(1139, 24)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(60, 25)
        Me.txtYear.TabIndex = 220
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblYearMonth
        '
        Me.lblYearMonth.AutoSize = True
        Me.lblYearMonth.BackColor = System.Drawing.Color.Transparent
        Me.lblYearMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblYearMonth.ForeColor = System.Drawing.Color.Black
        Me.lblYearMonth.Location = New System.Drawing.Point(6, 10)
        Me.lblYearMonth.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblYearMonth.Name = "lblYearMonth"
        Me.lblYearMonth.Size = New System.Drawing.Size(193, 37)
        Me.lblYearMonth.TabIndex = 14
        Me.lblYearMonth.Text = "Month Year"
        '
        'txtPercentage
        '
        Me.txtPercentage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.txtPercentage.Font = New System.Drawing.Font("Segoe UI", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercentage.ForeColor = System.Drawing.Color.Black
        Me.txtPercentage.Location = New System.Drawing.Point(532, -2)
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.Size = New System.Drawing.Size(149, 58)
        Me.txtPercentage.TabIndex = 244
        Me.txtPercentage.Text = "100%"
        Me.txtPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(1088, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(292, 15)
        Me.lblVersion.TabIndex = 3
        Me.lblVersion.Text = "F-MNT-002-4 Eff: 05/18/2022"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdFacility
        '
        Me.rdFacility.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdFacility.AutoSize = True
        Me.rdFacility.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdFacility.Location = New System.Drawing.Point(950, 2)
        Me.rdFacility.Name = "rdFacility"
        Me.rdFacility.Size = New System.Drawing.Size(60, 17)
        Me.rdFacility.TabIndex = 249
        Me.rdFacility.TabStop = True
        Me.rdFacility.Text = "Facility"
        Me.rdFacility.UseVisualStyleBackColor = True
        '
        'rdMt
        '
        Me.rdMt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdMt.AutoSize = True
        Me.rdMt.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdMt.Location = New System.Drawing.Point(757, 2)
        Me.rdMt.Name = "rdMt"
        Me.rdMt.Size = New System.Drawing.Size(157, 17)
        Me.rdMt.TabIndex = 248
        Me.rdMt.TabStop = True
        Me.rdMt.Text = "Manufacturing Techology"
        Me.rdMt.UseVisualStyleBackColor = True
        '
        'tmrMain
        '
        Me.tmrMain.Interval = 1000
        '
        'cmsMain
        '
        Me.cmsMain.Name = "contextMenuStrip"
        Me.cmsMain.Size = New System.Drawing.Size(61, 4)
        '
        'tlpCalendar
        '
        Me.tlpCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpCalendar.ColumnCount = 8
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.Controls.Add(Me.lblSunday, 1, 0)
        Me.tlpCalendar.Controls.Add(Me.lblWednesday, 4, 0)
        Me.tlpCalendar.Controls.Add(Me.lblMonday, 2, 0)
        Me.tlpCalendar.Controls.Add(Me.lblTuesday, 3, 0)
        Me.tlpCalendar.Controls.Add(Me.lblThursday, 5, 0)
        Me.tlpCalendar.Controls.Add(Me.lblFriday, 6, 0)
        Me.tlpCalendar.Controls.Add(Me.lblSaturday, 7, 0)
        Me.tlpCalendar.Controls.Add(Me.lblScheduledHeader, 0, 0)
        Me.tlpCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpCalendar.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlpCalendar.Location = New System.Drawing.Point(0, 56)
        Me.tlpCalendar.Name = "tlpCalendar"
        Me.tlpCalendar.RowCount = 7
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.tlpCalendar.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.tlpCalendar.Size = New System.Drawing.Size(1384, 643)
        Me.tlpCalendar.TabIndex = 2
        '
        'lblSunday
        '
        Me.lblSunday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSunday.BackColor = System.Drawing.Color.Transparent
        Me.lblSunday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblSunday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSunday.ForeColor = System.Drawing.Color.Black
        Me.lblSunday.Location = New System.Drawing.Point(204, 1)
        Me.lblSunday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSunday.Name = "lblSunday"
        Me.lblSunday.Size = New System.Drawing.Size(163, 90)
        Me.lblSunday.TabIndex = 1
        Me.lblSunday.Text = "SUN"
        Me.lblSunday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWednesday
        '
        Me.lblWednesday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWednesday.BackColor = System.Drawing.Color.Transparent
        Me.lblWednesday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblWednesday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWednesday.ForeColor = System.Drawing.Color.Black
        Me.lblWednesday.Location = New System.Drawing.Point(708, 1)
        Me.lblWednesday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblWednesday.Name = "lblWednesday"
        Me.lblWednesday.Size = New System.Drawing.Size(163, 90)
        Me.lblWednesday.TabIndex = 3
        Me.lblWednesday.Text = "WED"
        Me.lblWednesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMonday
        '
        Me.lblMonday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMonday.BackColor = System.Drawing.Color.Transparent
        Me.lblMonday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblMonday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMonday.ForeColor = System.Drawing.Color.Black
        Me.lblMonday.Location = New System.Drawing.Point(372, 1)
        Me.lblMonday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblMonday.Name = "lblMonday"
        Me.lblMonday.Size = New System.Drawing.Size(163, 90)
        Me.lblMonday.TabIndex = 4
        Me.lblMonday.Text = "MON"
        Me.lblMonday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTuesday
        '
        Me.lblTuesday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTuesday.BackColor = System.Drawing.Color.Transparent
        Me.lblTuesday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblTuesday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTuesday.ForeColor = System.Drawing.Color.Black
        Me.lblTuesday.Location = New System.Drawing.Point(540, 1)
        Me.lblTuesday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTuesday.Name = "lblTuesday"
        Me.lblTuesday.Size = New System.Drawing.Size(163, 90)
        Me.lblTuesday.TabIndex = 2
        Me.lblTuesday.Text = "TUE"
        Me.lblTuesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblThursday
        '
        Me.lblThursday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblThursday.BackColor = System.Drawing.Color.Transparent
        Me.lblThursday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblThursday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblThursday.ForeColor = System.Drawing.Color.Black
        Me.lblThursday.Location = New System.Drawing.Point(876, 1)
        Me.lblThursday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblThursday.Name = "lblThursday"
        Me.lblThursday.Size = New System.Drawing.Size(163, 90)
        Me.lblThursday.TabIndex = 5
        Me.lblThursday.Text = "THU"
        Me.lblThursday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFriday
        '
        Me.lblFriday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFriday.BackColor = System.Drawing.Color.Transparent
        Me.lblFriday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblFriday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFriday.ForeColor = System.Drawing.Color.Black
        Me.lblFriday.Location = New System.Drawing.Point(1044, 1)
        Me.lblFriday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFriday.Name = "lblFriday"
        Me.lblFriday.Size = New System.Drawing.Size(163, 90)
        Me.lblFriday.TabIndex = 6
        Me.lblFriday.Text = "FRI"
        Me.lblFriday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSaturday
        '
        Me.lblSaturday.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSaturday.BackColor = System.Drawing.Color.Transparent
        Me.lblSaturday.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblSaturday.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSaturday.ForeColor = System.Drawing.Color.Black
        Me.lblSaturday.Location = New System.Drawing.Point(1212, 1)
        Me.lblSaturday.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSaturday.Name = "lblSaturday"
        Me.lblSaturday.Size = New System.Drawing.Size(169, 90)
        Me.lblSaturday.TabIndex = 7
        Me.lblSaturday.Text = "SAT"
        Me.lblSaturday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblScheduledHeader
        '
        Me.lblScheduledHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblScheduledHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblScheduledHeader.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblScheduledHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblScheduledHeader.ForeColor = System.Drawing.Color.Black
        Me.lblScheduledHeader.Location = New System.Drawing.Point(3, 1)
        Me.lblScheduledHeader.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblScheduledHeader.Name = "lblScheduledHeader"
        Me.lblScheduledHeader.Size = New System.Drawing.Size(196, 90)
        Me.lblScheduledHeader.TabIndex = 8
        Me.lblScheduledHeader.Text = "FOR PM"
        Me.lblScheduledHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1384, 699)
        Me.Controls.Add(Me.tlpCalendar)
        Me.Controls.Add(Me.pnlTop)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " PM Calendar"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tlpCalendar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As Panel
    Private WithEvents lblYearMonth As Label
    Friend WithEvents tmrMain As Timer
    Private WithEvents lblSunday As Label
    Private WithEvents lblWednesday As Label
    Private WithEvents lblMonday As Label
    Private WithEvents lblTuesday As Label
    Private WithEvents lblThursday As Label
    Private WithEvents lblFriday As Label
    Private WithEvents lblSaturday As Label
    Private WithEvents lblScheduledHeader As Label
    Friend WithEvents btnGo As PinkieControls.ButtonXP
    Friend WithEvents lblYear As Label
    Friend WithEvents txtYear As MaskedTextBox
    Friend WithEvents lblMonth As Label
    Friend WithEvents cmbMonth As SergeUtils.EasyCompletionComboBox
    Friend WithEvents btnCurrent As PinkieControls.ButtonXP
    Friend WithEvents tlpCalendar As ExtTableLayoutPanel
    Friend WithEvents lblView As Label
    Friend WithEvents cmbView As ComboBox
    Friend WithEvents btnRefresh As PinkieControls.ButtonXP
    Friend WithEvents cmsMain As ContextMenuStrip
    Friend WithEvents txtFraction As Label
    Friend WithEvents lblEquals As Label
    Friend WithEvents txtPercentage As Label
    Friend WithEvents lblCompletion As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents btnChecksheet As PinkieControls.ButtonXP
    Friend WithEvents rdFacility As RadioButton
    Friend WithEvents rdMt As RadioButton
End Class
