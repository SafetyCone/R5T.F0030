using System;


namespace R5T.F0030
{
    public class RemoteCommandOperator : IRemoteCommandOperator
    {
        #region Infrastructure

        public static IRemoteCommandOperator Instance { get; } = new RemoteCommandOperator();


        private RemoteCommandOperator()
        {
        }

        #endregion
    }
}
