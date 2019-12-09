Imports System.Threading
Imports NUnit.Framework
Imports TaskSchedulerService.Lib


''' <summary>
''' As a sidenote, every test takes into account those notepad processes that were executed in the last 5 seconds or less. 
''' This is to avoid failures when you have an instance of it already running.
''' Also, each test does a Thread.Sleep() to let the processes from the previous test die and avoid collisions between assertions.
''' </summary>
<TestFixture>
Public Class ProcessHandlerTests

    <Test>
    Sub RunSingleProcessFromEnvironmentalPath()

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 0)

        Dim ExeProcess = TasksHandler.RunProcess("%windir%/System32/notepad.exe")

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 1)

        ExeProcess.Kill()
        Thread.Sleep(50)
    End Sub

    <Test>
    Sub RunMultipleProcessesFromEnvironmentalPath()

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 0)

        Dim ExeProcesses = TasksHandler.RunProcess(New String() {"%windir%/System32/notepad.exe", "%windir%/System32/notepad.exe"})


        Assert.AreEqual(ExeProcesses.Count, 2)
        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 2)

        ExeProcesses.ForEach(Sub(P) P.Kill())
        Thread.Sleep(50)

    End Sub

    <Test>
    Sub RunSingleProcessFromAbsolutePath()

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 0)

        Dim ExeProcess = TasksHandler.RunProcess("C:/Windows/System32/notepad.exe")

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 1)

        ExeProcess.Kill()
        Thread.Sleep(50)


    End Sub

    <Test>
    Sub RunMultipleProcessesFromAbsolutePath()

        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 0)

        Dim ExeProcesses = TasksHandler.RunProcess(New String() {"C:/Windows/System32/notepad.exe", "C:/Windows/System32/notepad.exe"})

        Assert.AreEqual(ExeProcesses.Count, 2)
        Assert.AreEqual(Process.GetProcessesByName("notepad").Where(Function(x) x.StartTime > Date.Now.AddSeconds(-5)).Count(), 2)

        ExeProcesses.ForEach(Sub(P) P.Kill())
        Thread.Sleep(50)


    End Sub

    <Test>
    Sub ParsePipedProcesses()
        Dim ProcNames = "%windir%/System32/notepad.exe|%windir%/System32/notepad.exe"

        Dim Splitted = ProcNames.Split("|")

        Assert.AreEqual(Splitted.Count(), 2)

    End Sub


End Class
