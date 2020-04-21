using System;
using System.Runtime.Serialization;

namespace Chat_Chan_Debug.Exceptions
{
    /// <summary>
    /// サーバーが何らかの原因によってアクセス自体出来ない時に発生するエラー。<br/>
    /// <see cref="System.Net.Sockets.SocketException"/>などからスローされた場合このエラー処理としてリスローしてください。<br/>
    /// このクラスは継承できません。
    /// </summary>
    [Serializable]
    public class ServerClosedException : Exception
    {
        public ServerClosedException() { }
        public ServerClosedException(string message) : base(message) { }
        public ServerClosedException(string message, Exception inner) : base(message, inner) { }
        protected ServerClosedException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
