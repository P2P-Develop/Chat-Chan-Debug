using System;
using System.Runtime.Serialization;

namespace Chat_Chan_Debug.Exceptions
{
    /// <summary>
    /// コマンドが存在しないときに発生するエラー。このクラスは継承できません。
    /// </summary>
    [Serializable]
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() { }
        public CommandNotFoundException(string message) : base(message) { }
        public CommandNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CommandNotFoundException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
