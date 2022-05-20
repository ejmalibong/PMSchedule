Public Class Program

    <STAThread>
    Public Shared Sub Main()
        Try
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New Main())
        Catch exc As Exception
            MessageBox.Show(exc.Message)
        End Try
    End Sub

End Class