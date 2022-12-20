using System;


namespace R5T.F0030
{
	public class SftpFileOperator : ISftpFileOperator
	{
		#region Infrastructure

	    public static ISftpFileOperator Instance { get; } = new SftpFileOperator();

	    private SftpFileOperator()
	    {
        }

	    #endregion
	}
}