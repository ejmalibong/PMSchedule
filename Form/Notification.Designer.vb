﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Notification
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.tmrNotification = New System.Windows.Forms.Timer(Me.components)
        Me.lsvDetail = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.DarkRed
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Yellow
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Padding = New System.Windows.Forms.Padding(5)
        Me.lblHeader.Size = New System.Drawing.Size(300, 45)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Notification Header"
        '
        'tmrNotification
        '
        '
        'lsvDetail
        '
        Me.lsvDetail.BackColor = System.Drawing.Color.DarkRed
        Me.lsvDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lsvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!)
        Me.lsvDetail.ForeColor = System.Drawing.Color.White
        Me.lsvDetail.GridLines = True
        Me.lsvDetail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lsvDetail.HideSelection = False
        Me.lsvDetail.Location = New System.Drawing.Point(0, 45)
        Me.lsvDetail.MultiSelect = False
        Me.lsvDetail.Name = "lsvDetail"
        Me.lsvDetail.ShowGroups = False
        Me.lsvDetail.ShowItemToolTips = True
        Me.lsvDetail.Size = New System.Drawing.Size(300, 355)
        Me.lsvDetail.TabIndex = 3
        Me.lsvDetail.UseCompatibleStateImageBehavior = False
        '
        'Notification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(300, 400)
        Me.ControlBox = False
        Me.Controls.Add(Me.lsvDetail)
        Me.Controls.Add(Me.lblHeader)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Notification"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Notification"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents lblHeader As Label
    Private WithEvents tmrNotification As Timer
    Friend WithEvents lsvDetail As ListView
End Class
