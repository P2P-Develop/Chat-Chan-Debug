using System;
using System.Runtime.Serialization;

namespace Chat_Chan_Debug.Exceptions
{
    /// <summary>
    /// コマンドに渡された引数が無効な場合に発生するエラー。このクラスは継承できません。
    /// </summary>
    [Serializable]
    public class InvalidArgumentException : ArgumentException
    {
        public InvalidArgumentException() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, Exception inner) : base(message, inner) { }
        protected InvalidArgumentException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
