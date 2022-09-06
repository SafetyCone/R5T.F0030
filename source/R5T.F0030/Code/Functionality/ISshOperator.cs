using System;

using Renci.SshNet;

using R5T.T0132;
using R5T.T0144;


namespace R5T.F0030
{
	[FunctionalityMarker]
	public partial interface ISshOperator : IFunctionalityMarker
	{
        public ConnectionInfo GetConnectionInfo(
            string hostUrl,
            string username,
            string password,
            string privateKeyFilePath)
        {
            var privateKeyFiles = new[]
            {
                new PrivateKeyFile(privateKeyFilePath, password),
            };

            var authenticationMethods = new AuthenticationMethod[]
            {
                new PasswordAuthenticationMethod(username, password),
                new PrivateKeyAuthenticationMethod(username, privateKeyFiles)
            };

            // Get a connection.
            var output = new ConnectionInfo(
                hostUrl,
                username,
                authenticationMethods);

            return output;
        }

        public ConnectionInfo GetConnectionInfo(IRemoteServerAuthentication remoteServerAuthentication)
        {
            var connectionInfo = this.GetConnectionInfo(
                remoteServerAuthentication.HostUrl,
                remoteServerAuthentication.Username,
                remoteServerAuthentication.Password,
                remoteServerAuthentication.PrivateKeyFilePath);

            return connectionInfo;
        }

        public SshClient GetSshClient(ConnectionInfo connection)
        {
            var output = new SshClient(connection);
            return output;
        }

        public void InConnectionContext(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<ConnectionInfo> connectionContextAction)
        {
            var connectionInfo = this.GetConnectionInfo(remoteServerAuthentication);

            connectionContextAction(connectionInfo);
        }

        /// <summary>
        /// Chooses <see cref="InSshContext_Connected_Synchronous(IRemoteServerAuthentication, Action{SshClient})"/> as the default.
        /// </summary>
        public void InSshContext_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SshClient> sshContextAction)
        {
            this.InSshContext_Connected_Synchronous(
                remoteServerAuthentication,
                sshContextAction);
        }

        public void InSshContext_Connected_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SshClient> sshContextAction)
        {
            this.InConnectionContext(
                remoteServerAuthentication,
                connection =>
                {
                    this.InSshContext_Connected_Synchronous(
                        connection,
                        sshContextAction);
                });
        }

        public void InSshContext_Unconnected_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SshClient> sshContextAction)
        {
            this.InConnectionContext(
                remoteServerAuthentication,
                connection =>
                {
                    using var sshClient = this.GetSshClient(connection);

                    sshContextAction(sshClient);
                });
        }

        public void InSshContext_Connected_Synchronous(
            ConnectionInfo connection,
            Action<SshClient> sshContextAction)
        {
            using var sshClient = this.GetSshClient(connection);

            sshClient.Connect();

            sshContextAction(sshClient);
        }
    }
}