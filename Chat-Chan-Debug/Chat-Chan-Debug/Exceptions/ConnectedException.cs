using System;
using System.Runtime.Serialization;

namespace Chat_Chan_Debug.Exceptions
{
    /// <summary>
    /// 既に接続している場合に発生するエラー。このクラスは継承できません。
    /// </summary>
    [Serializable]
    public class ConnectedException : Exception
    {
        public ConnectedException() { }
        public ConnectedException(string message) : base(message) { }
        public ConnectedException(string message, Exception inner) : base(message, inner) { }
        protected ConnectedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
