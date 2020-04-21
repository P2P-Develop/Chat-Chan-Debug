using System;
using System.Runtime.Serialization;

namespace Chat_Chan_Debug.Exceptions
{
    /// <summary>
    /// プロンプト表示で何らかのエラーが発生した場合に呼び出しされるエラー。<br/>
    /// このエラーが発生された場合、文字のみの仮プロンプトに切り替えしてください。<br/>
    /// このクラスは継承できません。
    /// </summary>
    [Serializable]
    public class PromptException : Exception
    {
        public PromptException() { }
        public PromptException(string message) : base(message) { }
        public PromptException(string message, Exception inner) : base(message, inner) { }
        protected PromptException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
