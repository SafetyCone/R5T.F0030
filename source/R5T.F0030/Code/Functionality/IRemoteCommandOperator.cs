using System;

using Microsoft.Extensions.Logging;

using Renci.SshNet;

using R5T.T0132;


namespace R5T.F0030
{
    [FunctionalityMarker]
    public partial interface IRemoteCommandOperator : IFunctionalityMarker
    {
        public bool IsFailure(SshCommand command)
        {
            var isFailure = F0000.Instances.ExitCodeOperator.IsFailure(command.ExitStatus);
            return isFailure;
        }

        public void LogCommandResult(
            SshCommand command,
            ILogger logger)
        {
            logger.LogInformation(command.Result);

            var isFailure = this.IsFailure(command);
            if (isFailure)
            {
                logger.LogError(command.Error);
            }
        }
    }
}
