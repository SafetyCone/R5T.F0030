using System;


namespace R5T.F0030
{
	public class SftpFileOperator : ISftpFileOperator
	{
		#region Infrastructure

	    public static SftpFileOperator Instance { get; } = new();

	    private SftpFileOperator()
	    {
        }

	    #endregion
	}
}