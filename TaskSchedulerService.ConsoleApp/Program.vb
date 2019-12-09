Imports System.Configuration
Imports TaskSchedulerService.Lib

Module Program

    Sub Main()

        'Run processes:
        Dim Executables = ConfigurationManager.AppSettings("ExecutablesToRun").Split("|")
        Dim Proc = TasksHandler.RunProcess(Executables)

        'Mail:
        Dim Mailing = New MailHelper("TaskSchedulerService")
        Mailing.Send("I've executed the notepad.")

    End Sub

End Module
