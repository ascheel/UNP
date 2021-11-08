using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNP
{
	class Token
	{
		public string token { get; set; }
		public string user { get; set; }
		public DateTime created { get; set; }
		public DateTime expires { get; set; }
		public Token(string token, string user, DateTime created, DateTime expires)
		{
			this.token = token;
			this.user = user;
			this.created = created;
			this.expires = expires;
		}
		public Token(string serializedToken)
		{
			Token _token = JsonConvert.DeserializeObject<Token>(serializedToken);
			this.token = _token.token;
			this.user = _token.user;
			this.expires = _token.expires;
			this.created = _token.created;
		}
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
	class TokenHandling
	{
		public Token token = null;
		public string id = null;
		public string secret = null;
		public bool authenticated = false;
		public int default_token_bytesize;
		private string timestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
		public double maxAge;
		public TokenHandling(string user)
		{
			this.default_token_bytesize = 32;
			this.id = "";
			this.maxAge = 3;
		}

		public Token generate_token()
		{
			string token = "";
			var mybytes = this.token_bytes();
			token = Convert.ToBase64String(mybytes);

			while (token.Substring(token.Length - 1) == "=")
			{
				token = token.Substring(0, token.Length - 1);
			}
			token = token.Replace("+", "-");
			token = token.Replace("/", "_");

			return new Token(token, this.id, DateTime.Now, DateTime.Now.AddDays(this.maxAge));
		}

		private byte[] token_bytes()
		{
			return this.token_bytes(this.default_token_bytesize);
		}

		private byte[] token_bytes(int bytecount)
		{
			byte[] token_bytes = new byte[bytecount];

			var randomGenerator = new Random();
			randomGenerator.NextBytes(token_bytes);

			return token_bytes;
		}
	}
}
