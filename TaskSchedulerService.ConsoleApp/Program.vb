Imports System.Configuration
Imports Alpha.Utilidades.General
Imports TaskSchedulerService.Lib

Module Program

    ''' <summary>
    ''' The main program. For more details on the implementations, <see cref="Examples"/> and the tests.
    ''' </summary>
    Sub Main()

        ConfigureLog()
        Try

            LogsUtils.Loguear("Starting the app.")

            'A general approach would be to run processes from the AppSettings:
            Dim Executables = ConfigurationManager.AppSettings("ExecutablesToRun").Split("|")
            Dim Proc = TasksHandler.RunProcess(Executables)

            'Mail:
            Dim Mailing = New MailHelper("TaskSchedulerService")
            Mailing.Send("I've executed the notepad.")

        Catch ex As Exception
            'Log an eventual exception:
            LogsUtils.Loguear("Something went wrong: " + ex.Message + "\n" + ex.StackTrace)

        End Try

    End Sub

    Sub ConfigureLog()
        Dim ConfiguracionLog = New ConfiguracionLog

        With ConfiguracionLog
            .LoguearWebApi = False
            .NombreArchivoLog = "TaskSchedulerService"
            .ProyectoOrigen = "TaskSchedulerService.ConsoleApp"
        End With

        LogsUtils.Configuracion = ConfiguracionLog

        If (ConfigurationManager.AppSettings("Debug") = "1") Then
            LogsUtils.Configuracion.Debug = True
        ElseIf (ConfigurationManager.AppSettings("Debug") = "0") Then
            LogsUtils.Configuracion.Debug = False
        End If

    End Sub

End Module
