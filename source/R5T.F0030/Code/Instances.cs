using System;


namespace R5T.F0030
{
    public static class Instances
    {
        public static ISftpFileOperator SftpFileOperator { get; } = F0030.SftpFileOperator.Instance;
        public static ISftpOperator SftpOperator { get; } = F0030.SftpOperator.Instance;
        public static ISshOperator SshOperator { get; } = F0030.SshOperator.Instance;
    }
}