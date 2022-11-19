using System;


namespace R5T.F0030
{
	public class SshOperator : ISshOperator
	{
		#region Infrastructure

	    public static SshOperator Instance { get; } = new SshOperator();

	    private SshOperator()
	    {
        }

	    #endregion
	}
}