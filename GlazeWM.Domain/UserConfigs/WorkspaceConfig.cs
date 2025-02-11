using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GlazeWM.Domain.UserConfigs
{
  public class WorkspaceConfig
  {
    [Required]
    public string Name { get; set; }

    private string _bindToMonitor;
    public string BindToMonitor
    {
      get => _bindToMonitor;
      set => _bindToMonitor = int.TryParse(value, out var monitorIndex)
        ? $@"\\.\DISPLAY{monitorIndex}"
        : value;
    }
    public List<DefaultProcesses> DefaultProcesses { get; set; } = new();
    public string DisplayName { get; set; }
    public bool KeepAlive { get; set; }
  }

  public class DefaultProcesses
  {
    public string MatchProcessName { get; set; }
    public string ExecCommand { get; set; }
  }
}
