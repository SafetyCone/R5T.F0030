using System;

using Microsoft.Extensions.Logging;

using Renci.SshNet;

using R5T.T0132;


namespace R5T.F0030
{
    [FunctionalityMarker]
    public partial interface IRemoteCommandOperator : IFunctionalityMarker
    {
        public void LogCommandResult(
            SshCommand command,
            ILogger logger)
        {
            logger.LogInformation(command.Result);

            if (F0000.Instances.ExitCodeOperator.IsFailure(command.ExitStatus))
            {
                logger.LogError(command.Error);
            }
        }
    }
}
