using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Renci.SshNet;
using Renci.SshNet.Sftp;


namespace System
{
    public static class SftpClientExtensions
    {
        public static async Task<SftpFile[]> ListDirectoryAsync(this SftpClient sftpClient,
            string directoryPath)
        {
            var result = await Task.Factory.FromAsync(
                (asyncCallback, state) => sftpClient.BeginListDirectory(directoryPath, asyncCallback, state),
                sftpClient.EndListDirectory,
                null);

            // Evaluate now since I'm not sure how the enumerable is going to behave with async. (Will the enumerable perform multiple calls, each async? That would be crazy!)
            var output = result.Now();
            return output;
        }
    }
}
