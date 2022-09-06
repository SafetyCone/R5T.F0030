using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Renci.SshNet;
using Renci.SshNet.Sftp;

using R5T.T0132;
using R5T.T0144;


namespace R5T.F0030
{
	[FunctionalityMarker]
	public partial interface ISftpOperator : IFunctionalityMarker
	{
        public async Task<SftpFile[]> EnumerateDirectories(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = await sftpClient.ListDirectoryAsync(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsActualDirectory)
                .Now();

            return output;
        }

        public SftpFile[] EnumerateDirectories_Synchronous(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = sftpClient.ListDirectory(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsActualDirectory)
                .Now();

            return output;
        }

        public async Task<SftpFile[]> EnumerateFiles(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = await sftpClient.ListDirectoryAsync(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsFile)
                .Now();

            return output;
        }

        public SftpFile[] EnumerateFiles_Synchronous(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = sftpClient.ListDirectory(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsFile)
                .Now();

            return output;
        }

        public async Task<SftpFile[]> EnumerateFileSystemEntries(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = await sftpClient.ListDirectoryAsync(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsActualFileSystemEntry)
                .Now();

            return output;
        }

        public SftpFile[] EnumerateFileSystemEntries_Synchronous(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntries = sftpClient.ListDirectory(directoryPath);

            var output = fileSystemEntries
                .Where(Instances.SftpFileOperator.IsActualFileSystemEntry)
                .Now();

            return output;
        }

        public async Task<SftpFile[]> EnumerateFileSystemEntries_Recursive(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntriesAggregate = new List<SftpFile>();

            await this.EnumerateFileSystemEntries_Recursive_Internal(
                sftpClient,
                directoryPath,
                fileSystemEntriesAggregate);

            var output = fileSystemEntriesAggregate.ToArray();
            return output;
        }

        private async Task EnumerateFileSystemEntries_Recursive_Internal(
            SftpClient sftpClient,
            string directoryPath,
            List<SftpFile> fileSystemEntriesAggregate)
        {
            var fileSystemEntries = await this.EnumerateFileSystemEntries(
                sftpClient,
                directoryPath);

            foreach (var entry in fileSystemEntries)
            {
                // Add the entry.
                fileSystemEntriesAggregate.Add(entry);

                // Recurse if the entry is a directory.
                if(entry.IsDirectory)
                {
                    await this.EnumerateFileSystemEntries_Recursive_Internal(
                        sftpClient,
                        entry.FullName,
                        fileSystemEntriesAggregate);
                }
            }
        }

        public SftpFile[] EnumerateFileSystemEntries_Recursive_Synchronous(
            SftpClient sftpClient,
            string directoryPath)
        {
            var fileSystemEntriesAggregate = new List<SftpFile>();

            this.EnumerateFileSystemEntries_Recursive_Synchronous_Internal(
                sftpClient,
                directoryPath,
                fileSystemEntriesAggregate);

            var output = fileSystemEntriesAggregate.ToArray();
            return output;
        }

        private void EnumerateFileSystemEntries_Recursive_Synchronous_Internal(
            SftpClient sftpClient,
            string directoryPath,
            List<SftpFile> fileSystemEntriesAggregate)
        {
            var fileSystemEntries = this.EnumerateFileSystemEntries_Synchronous(
                sftpClient,
                directoryPath);

            foreach (var entry in fileSystemEntries)
            {
                // Add the entry.
                fileSystemEntriesAggregate.Add(entry);

                // Recurse if the entry is a directory.
                if (entry.IsDirectory)
                {
                    this.EnumerateFileSystemEntries_Recursive_Synchronous_Internal(
                        sftpClient,
                        entry.FullName,
                        fileSystemEntriesAggregate);
                }
            }
        }

        public SftpClient GetSftpClient(ConnectionInfo connection)
        {
            var output = new SftpClient(connection);
            return output;
        }

        /// <summary>
        /// Chooses connected as the default.
        /// </summary>
        public void InSftpContext_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SftpClient> sftpContextAction)
        {
            this.InSftpContext_Connected_Synchronous(
                remoteServerAuthentication,
                sftpContextAction);
        }

        public void InSftpContext_Connected_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SftpClient> sftpContextAction)
        {
            Instances.SshOperator.InConnectionContext(
                remoteServerAuthentication,
                connection =>
                {
                    this.InSftpContext_Connected_Synchronous(
                        connection,
                        sftpContextAction);
                });
        }

        public void InSftpContext_Unconnected_Synchronous(
            IRemoteServerAuthentication remoteServerAuthentication,
            Action<SftpClient> sftpContextAction)
        {
            Instances.SshOperator.InConnectionContext(
                remoteServerAuthentication,
                connection =>
                {
                    using var sftpClient = this.GetSftpClient(connection);

                    sftpContextAction(sftpClient);
                });
        }

        public void InSftpContext_Connected_Synchronous(
            ConnectionInfo connection,
            Action<SftpClient> sftpContextAction)
        {
            using var sftpClient = this.GetSftpClient(connection);

            sftpClient.Connect();

            sftpContextAction(sftpClient);
        }

        public Task<SftpFile[]> ListDirectory(
            SftpClient sftpClient,
            string directoryPath)
        {
            // Make the extension method the primitive, to allow separation into an SSH.NET extensions library.
            return sftpClient.ListDirectoryAsync(directoryPath);
        }
    }
}