using System;

using Renci.SshNet;
using Renci.SshNet.Sftp;

using R5T.T0132;


namespace R5T.F0030
{
	[FunctionalityMarker]
	public partial interface ISftpFileOperator : IFunctionalityMarker
	{
        /// <summary>
		/// <para>Returns true if the file system entry is a directory, and the directory name is *not* the <see cref="F0002.IDirectoryNames.CurrentDirectory"/> or <see cref="F0002.IDirectoryNames.ParentDirectory"/> name.</para>
		/// <para><inheritdoc cref="F0002.IDirectoryNameOperator.IsActualDirectoryName(string)" path="/useful-when"/></para>
		/// </summary>
		public bool IsActualDirectory(SftpFile fileSystemEntry)
        {
            var output = true
                && this.IsDirectory(fileSystemEntry)
                && F0002.Instances.DirectoryNameOperator.IsActualDirectoryName(fileSystemEntry.Name)
                ;

            return output;
        }

        /// <summary>
		/// <para>Returns true if the file system entry is not a directory with the <see cref="F0002.IDirectoryNames.CurrentDirectory"/> or <see cref="F0002.IDirectoryNames.ParentDirectory"/> name.</para>
		/// <para><inheritdoc cref="F0002.IDirectoryNameOperator.IsActualDirectoryName(string)" path="/useful-when"/></para>
		/// </summary>
        public bool IsActualFileSystemEntry(SftpFile fileSystemEntry)
        {
            // Does not matter if the entry is a file, all we need to do is make sure the file system entry name is not the current or parent directory name.
            var output =  F0002.Instances.DirectoryNameOperator.IsActualDirectoryName(fileSystemEntry.Name);
            return output;
        }

        public bool IsDirectory(SftpFile fileSystemEntry)
        {
            var output = fileSystemEntry.IsDirectory;
            return output;
        }

        public bool IsFile(SftpFile fileSystemEntry)
        {
            var output = !fileSystemEntry.IsDirectory;
            return output;
        }
    }
}