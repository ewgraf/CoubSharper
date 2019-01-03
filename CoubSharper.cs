using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace CoubSharper {
	public class CoubClient : IDisposable {
		private const string AuthorizeAppUri = "http://coub.com/oauth/authorize";
		private const string RequestAccessTokenUri = "http://coub.com/oauth/token";
		private readonly HttpClient _client;
		private readonly string _applicationId;
		private readonly string _secret;
		private readonly string _callbackUri;

		private string _code;
		private CoubToken _token;

		public CoubClient() {
			_client = new HttpClient();
		}

		public CoubClient(string applicationId, string secret, string callbackUri) : base() {
			_applicationId = applicationId;
			_secret = secret;
			_callbackUri = callbackUri;
		}

		public void GetCode() {
			// http://coub.com/oauth/authorize?response_type=code&client_id=eac6f6491031535222d9553440d7048b7f6401a914aedb1303c5a4991840f92f&redirect_uri=http%3A%2F%2Fyourapp.com%2Fauth%2Fcallback&scope=like+recoub
			string authUri = $"{AuthorizeAppUri}"
						   + $"?redirect_uri={_callbackUri}" // the callback Uri which the Coub server  responses in with client credentials.
						   + $"&client_id={_applicationId}" // the unique application identifier
						   + $"&response_type=code"; // should be set to 'code'
						   // By  default,  the  request  will  be   send   for logged_in access mode.
						   // To request any additional modes you need  to  add scope parameter to the URL.
						   // All needed additional modes should  be  set  with the "+" sign separator:
						   //+ $"&scope=like+recoub"

			OpenBrowser(authUri);

			using (Socket server = new Socket(SocketType.Stream, ProtocolType.Tcp)) {
				server.Bind(new IPEndPoint(IPAddress.Any, 80));
				server.Listen(1);
				// 128 is enough to get first line of request
				byte[] buffer = new byte[128];
				using (Socket connection = server.Accept()) {
					connection.Receive(buffer);
					connection.Close();
					connection.Dispose();
				}
				string request = Encoding.ASCII.GetString(buffer);
				// parsing first line 'GET /?code=aed2926c3ede9de608cb359844eae54b815910b55576b15db3fbe545d45d77cf HTTP/1.1'
				_code = request.Split(Environment.NewLine.ToCharArray())[0].Split(' ')[1].Replace("/?code=", string.Empty);
			}
		}

		public void GetToken() {
			string uri = $"{RequestAccessTokenUri}"
					   + $"?client_id={_applicationId}" // the unique application identifier
					   + $"&client_secret={_secret}" // the app's secret token
					   + $"&code={_code}" // the received authorization code
					   + $"&redirect_uri={_callbackUri}" // the callback URL which the Coub server responses in with client credentials
					   + $"&grant_type=authorization_code"; // should be set to 'authorization_code'
			HttpResponseMessage response = _client.PostAsync(uri, null).Result;
			string content = response.Content.ReadAsStringAsync().Result;
			_token = JsonConvert.DeserializeObject<CoubToken>(content);
		}

		public CoubsSearchResponse SearchCoubs(string query, OrderBy orderBy = OrderBy.views_count, int page = 1) {
			// GET /api/v2/search/coubs?q=best coub ever&order_by=likes_count&page=1
			string uri = "http://coub.com/api/v2/search/coubs"
					  + $"?q={query}"
					  + $"&order_by={orderBy}"
					  + $"&page={page}"; // the number of the page containing results (by default is set to 1)
			return JsonConvert.DeserializeObject<CoubsSearchResponse>(
				_client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result
			);
		}

		// credits to: 'https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/'
		private static void OpenBrowser(string url) {
			try {
				Process.Start(url);
			} catch {
				// hack because of this: https://github.com/dotnet/corefx/issues/10361
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
					url = url.Replace("&", "^&");
					Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
				} else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
					Process.Start("xdg-open", url);
				} else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
					Process.Start("open", url);
				} else {
					throw;
				}
			}
		}

		#region IDisposable

		private bool _disposed = false;

		~CoubClient() => Dispose(false);

		public void Dispose() {
			Dispose(true);
			// This object will be cleaned up by the Dispose method. Therefore,
			// you should call GC.SupressFinalize to take this object  off  the
			// finalization queue and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		// Dispose(bool disposing)  executes  in  two  distinct  scenarios.  If
		// disposing equals true,  the  method  has  been  called  directly  or
		// indirectly by a user's code. Managed and unmanaged resources can  be
		// disposed. If disposing equals false, the method has been  called  by
		// the runtime from inside the finalizer and you should  not  reference
		// other objects. Only unmanaged resources can be disposed.
		protected virtual void Dispose(bool disposing) {
			if (!_disposed) {
				if (disposing) {
					// Dispose managed resources.
					_client.Dispose();
				}

				// Dispose unmanaged resources.
				//Kernel32.CloseHandle(handle);
				//handle = IntPtr.Zero;

				_disposed = true;
			}
		}

		#endregion
	}
}
