''' <summary>
''' Class in charge of executing processes.
''' </summary>
Public Class TasksHandler


    ''' <summary>
    ''' Runs a process and returns the Process object.
    ''' </summary>
    ''' <param name="Path">Path to the process. It can be absolute or enviromental.</param>
    ''' <param name="Params">Optional parameters for the process.</param>
    ''' <param name="CreateNoWindow">CreateNoWindow</param>
    ''' <returns>A Process object.</returns>
    Public Shared Function RunProcess(Path As String, ByVal Optional Params As String = Nothing,
                                      ByVal Optional CreateNoWindow As Boolean = False) As Process
        Path = NormalizePath(Path)
        Dim Proc As Process = New Process()
        Proc.StartInfo = New ProcessStartInfo(Path) With {
            .Arguments = Params,
            .UseShellExecute = False,
            .CreateNoWindow = CreateNoWindow
        }
        Proc.Start()
        Return Proc

    End Function

    ''' <summary>
    ''' Runs multiple processes and returns a List of them. The actual running is delegated to the single-process overload.
    ''' </summary>
    ''' <returns>A List of Process objects.</returns>
    Public Shared Function RunProcess(Paths As String(), ByVal Optional Params As String = Nothing,
                                      ByVal Optional CreateNoWindow As Boolean = False) As List(Of Process)
        Paths = NormalizePaths(Paths)
        Dim StartedProcesses = New List(Of Process)

        For Each Path In Paths
            StartedProcesses.Add(RunProcess(Path))
        Next

        Return StartedProcesses

    End Function

    ''' <summary>
    ''' Expands environmental variables from the given paths.
    ''' </summary>
    ''' <returns>The expanded version of the paths.</returns>
    Shared Function NormalizePaths(ParamArray Paths As String()) As String()

        For Each Path In Paths
            Path = NormalizePath(Path)
        Next

        Return Paths

    End Function
    ''' <summary>
    ''' Expands environmental variables from the given path.
    ''' </summary>
    ''' <returns>The expanded version of the path.</returns>
    Shared Function NormalizePath(Path As String)
        Return Environment.ExpandEnvironmentVariables(Path)
    End Function

End Class
