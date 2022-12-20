using System;


namespace R5T.F0030
{
	public class SftpOperator : ISftpOperator
	{
		#region Infrastructure

	    public static ISftpOperator Instance { get; } = new SftpOperator();

	    private SftpOperator()
	    {
        }

	    #endregion
	}
}