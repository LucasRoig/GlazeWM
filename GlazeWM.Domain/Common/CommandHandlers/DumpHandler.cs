using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using GlazeWM.Infrastructure.Bussing;
using GlazeWM.Infrastructure.Serialization;
using GlazeWM.Domain.Containers;
using GlazeWM.Domain.Common.Commands;

namespace GlazeWM.Domain.Common.CommandHandlers
{
  internal class DumpHandler : ICommandHandler<DumpCommand>
  {
    private readonly ContainerService _containerService;
    private readonly JsonService _jsonService;

    public DumpHandler(ContainerService containerService, JsonService jsonService)
    {
      _containerService = containerService;
      _jsonService = jsonService;
    }

    public CommandResponse Handle(DumpCommand command)
    {
      var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "./.glaze-wm/dump.log"
              );
      var stateDump = _jsonService.Serialize(
          _containerService.ContainerTree,
          new List<JsonConverter> { new JsonContainerConverter() }
      );
      File.WriteAllText(path, stateDump);
      return CommandResponse.Ok;
    }
  }
}
