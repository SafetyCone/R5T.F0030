using System;


namespace R5T.F0030
{
	public class SftpOperator : ISftpOperator
	{
		#region Infrastructure

	    public static SftpOperator Instance { get; } = new();

	    private SftpOperator()
	    {
        }

	    #endregion
	}
}