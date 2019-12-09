Imports System.Configuration
Imports TaskSchedulerService.Lib
Imports TaskSchedulerService.Test

Public Class Examples


    ''' <summary>
    ''' Some quick examples on the mailing utility.
    ''' </summary>
    Sub MailingExamples()

        'This will attempt to get the rest (user, password, mailto, etc.) from the App.config:
        Dim Mailing = New MailHelper("TaskSchedulerService")
        Mailing.Send("Here's some email")

        'This will take everything but the user from the App.config:
        Mailing = New MailHelper("TaskSchedulerService", "SomeMail@SomeDomain.com")
        Mailing.Send("Here's another mail")


        'This will take everything but the user and password from the App.config:
        Mailing = New MailHelper("TaskSchedulerService", "SomeMail@SomeDomain.com", "SomePassword")
        Mailing.Send("Here's another mail")

        Mailing = New MailHelper("TaskSchedulerService", "SomeMail@SomeDomain.com", "SomePassword", New String() {"SomeMail@SomeDomain.com"})

        Mailing.Send("Here's another mail")
        Mailing.Send("Here's a mail", "Here's a title")
        Mailing.Send("Here's a mail", "Here's a title", New String() {"SomeMail@SomeDomain.com"}, True)
        Mailing.Send("Here's a mail", "Here's a title", New String() {"SomeMail@SomeDomain.com"}, False)
        Mailing.Send("Here's a mail", "Here's a title", New String() {"SomeMail@SomeDomain.com"}, Nothing, "smtp.gmail.com")

    End Sub

    ''' <summary>
    ''' Some quick examples on how to run processes. For more detailed ones <see cref="ProcessHandlerTests"/>
    ''' </summary>
    Sub RunningProcessExamples()

        'Run a single process:
        Dim Proc = TasksHandler.RunProcess("%windir%/System32/notepad.exe")

        'Run a single process from the config:
        Dim Path = ConfigurationManager.AppSettings("ExecutablesToRun")
        Dim AnotherProc = TasksHandler.RunProcess(Path)

        'Run multiple processes:
        Dim Procs = TasksHandler.RunProcess(New String() {"%windir%/System32/notepad.exe", "%windir%/System32/notepad.exe"})
        'Kill them all:
        Procs.ForEach(Sub(x) x.Kill())

        'We assumme process paths will be separated by pipes, but it could be any valid separator:
        Dim Executables = ConfigurationManager.AppSettings("ExecutablesToRun").Split("|")
        'Run multiple processes:
        Dim MoreProcs = TasksHandler.RunProcess(Executables)

    End Sub

End Class
