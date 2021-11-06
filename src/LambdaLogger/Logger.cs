using Amazon.Lambda.Core;
using Ardalis.GuardClauses;
using System.Runtime.CompilerServices;

namespace LambdaLogger
{
    public class Logger : ILogger
    {
        public void SetLoggerContext(ILambdaLogger logger)
        {
            this._logger = Guard.Against.Null(logger, nameof(logger));
        }

        public void LogInfo(string msg, [CallerMemberName] string caller = null)
        {
            this.WriteLog($"[info] [caller: {caller}] - {msg}");
        }

        public void LogWarning(string msg, [CallerMemberName] string caller = null)
        {
            this.WriteLog($"[warning] [caller: {caller}] - {msg}");
        }
        public void LogError(string msg, [CallerMemberName] string caller = null)
        {
            this.WriteLog($"[error] [caller: {caller}] - {msg}");
        }

        private void WriteLog(string msg)
        {
            if (this._logger != null)
            {
                this._logger.LogLine(msg);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private ILambdaLogger _logger;
    }
}
