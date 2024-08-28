using System;


namespace R5T.F0030
{
    public static class Instances
    {
        public static L0066.IDirectoryNameOperator DirectoryNameOperator => L0066.DirectoryNameOperator.Instance;
        public static L0066.IExitCodeOperator ExitCodeOperator => L0066.ExitCodeOperator.Instance;
        public static L0066.IFileStreamOperator FileStreamOperator => L0066.FileStreamOperator.Instance;
        public static ISftpFileOperator SftpFileOperator { get; } = F0030.SftpFileOperator.Instance;
        public static ISftpOperator SftpOperator { get; } = F0030.SftpOperator.Instance;
        public static ISshOperator SshOperator { get; } = F0030.SshOperator.Instance;
    }
}