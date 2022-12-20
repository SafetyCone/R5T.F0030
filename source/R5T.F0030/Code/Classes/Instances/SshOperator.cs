using System;


namespace R5T.F0030
{
	public class SshOperator : ISshOperator
	{
		#region Infrastructure

	    public static ISshOperator Instance { get; } = new SshOperator();

	    private SshOperator()
	    {
        }

	    #endregion
	}
}