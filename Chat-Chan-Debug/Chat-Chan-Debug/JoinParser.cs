using Newtonsoft.Json;

namespace Chat_Chan_Debug
{
    class JoinParser
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("port")]
        public int[] Port { get; set; } = new int[1];
        [JsonProperty("people")]
        public int? People { get; set; }
        [JsonProperty("token")]
        public string? Token { get; set; }
        [JsonProperty("decryptKey")]
        public string? DecryptKey { get; set; }
        [JsonProperty("encryptKey")]
        public string? EncryptKey { get; set; }
    }
}
